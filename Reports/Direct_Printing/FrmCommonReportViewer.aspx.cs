using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using Raj.EC.Printers;
using System.Drawing.Printing;   

public partial class Reports_Direct_Printing_FrmCommonReportViewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    string Crypt, Proc_Name, AUSTYPE;
    int Menu_Item_ID, Document_ID, User_ID, GC_Count, Printer_ID = 0, SpecialBillFormat;
    string Paper_Size, Client_Code;

    int BillingClientID;

    String scripts;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Menu_Item_ID = 143;
        //Document_ID = 144;
        //User_ID = 87;
        //Client_Code = "Reach";

        if (!IsPostBack)
        {
            Session["count"] = 0;
        }

        if (Session["count"] == null)
        {
            Session["count"] = 1;
        }
        else
        {
            Session["count"] = Convert.ToInt32(Session["count"]) + 1;
        }

        Client_Code = CompanyManager.getCompanyParam().ClientCode;
        User_ID = UserManager.getUserParam().UserId;

        if (Session["SaveAndNewAndPrint"] != null)
        {
            if (Session["SaveAndNewAndPrint"].ToString() == "1")
            {
                Menu_Item_ID = Util.String2Int(Request.QueryString["Menu_Item_Id"].ToString());
                Document_ID = Util.String2Int(Request.QueryString["Document_ID"].ToString());
            }
            else
            {
                Crypt = Request.QueryString["Menu_Item_Id"];
                Menu_Item_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

                Crypt = Request.QueryString["Document_ID"];
                Document_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            }
        }
        else
        {
            Crypt = Request.QueryString["Menu_Item_Id"];
            Menu_Item_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            Crypt = Request.QueryString["Document_ID"];
            Document_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

            if (Menu_Item_ID == 72)
            {
                Crypt = Request.QueryString["AUSTYPE"];
                AUSTYPE = ClassLibraryMVP.Util.DecryptToString(Crypt);
            }

            if (Menu_Item_ID == 143)
            {
                Crypt = Request.QueryString["SpecialBillFormat"];
                SpecialBillFormat = Util.String2Int(Crypt);
            }

        }
        Proc_Name = Get_Procedure_Name(Menu_Item_ID);


        if (Client_Code.ToLower() == "reach" && (Menu_Item_ID == 106 || Menu_Item_ID == 108 || Menu_Item_ID == 195))
        {
            ds = Get_DataSet_Reach(Menu_Item_ID, Document_ID, User_ID, Proc_Name);
        }
        else if (Menu_Item_ID == 515151)
        {
            ds = Get_DataSetInwardInvoicePrint(Document_ID, Proc_Name);
        }
        else if (Menu_Item_ID == 525252)
        {
            ds = Get_DataSetInwardInvoicePrint(Document_ID, Proc_Name);
        }
        else 
        {
            ds = Get_DataSet(Menu_Item_ID, Document_ID, Proc_Name);
        }

        HiddenField1.Value = Client_Code.ToLower();
        HiddenField2.Value = Menu_Item_ID.ToString();

        Generate_XmlSchema(Client_Code,ds);
        string Path = Get_Crystal_Report(Menu_Item_ID);
    
        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds);

        if (
                (Menu_Item_ID == 143 && Client_Code.ToLower() == "reach") ||
                ((Menu_Item_ID == 106 || Menu_Item_ID == 108 || Menu_Item_ID == 195) && Client_Code.ToLower() == "nandwana")
           )
        {
            myReportDocument.SetParameterValue("Para_Menu_Item_ID", Menu_Item_ID);
        }

        Bind_To_Viewer();
    }

    private void Bind_To_Viewer()
    {
        Printer_Settings pkSizeObj = new Printer_Settings(Printer_ID, Paper_Size);

        try
        {
            CrystalReportViewer1.ReportSource = myReportDocument;
            //System.Drawing.Printing.PrintDocument printDocument = new System.Drawing.Printing.PrintDocument();
				
            myReportDocument.PrintOptions.PaperSize = pkSizeObj.Papersize;
            myReportDocument.PrintOptions.PrinterName = pkSizeObj.PrinterName.ToString();
            //myReportDocument.PrintToPrinter(1, true, 1, 1);

            CrystalReportViewer1.DataBind();
            CrystalReportViewer1.PrintMode = CrystalDecisions.Web.PrintMode.ActiveX;
            CrystalReportViewer1.HasZoomFactorList = false;
            CrystalReportViewer1.HasSearchButton = false;
            CrystalReportViewer1.HasCrystalLogo = false;
            CrystalReportViewer1.HasDrillUpButton = false;
            CrystalReportViewer1.HasViewList = false;
            CrystalReportViewer1.HasToggleGroupTreeButton = false;
            
        }
        catch (Exception ex)
        {
            //Destroy_objects();
        }      
    }

    protected void Page_Unload(object sender, EventArgs e)
    {
        Destroy_objects();
    }

    private void Destroy_objects()
    {
        CrystalReportViewer1.Dispose();
        CrystalReportViewer1 = null;
        myReportDocument.Close();
        myReportDocument.Dispose();
        myReportDocument = null;
        GC.Collect();
    }

    private string Get_Procedure_Name(int Menu_Item_ID)
    {
        //------------------------------------------GC--------------------------------------
        if (Menu_Item_ID == 30 || Menu_Item_ID == 213)
        {
            if (Client_Code.ToLower() == "nandwana")
            {
                Proc_Name = "EC_RPT_Direct_Printing_GC_OMBharat";
            }
            else if (Client_Code.ToLower() == "excel")
            {
                Proc_Name = "EC_RPT_Direct_Printing_GC_Excel";
            }
        }
        //------------------------------------------MR Bkg, Dly/CreditMemo--------------------------------------
        else if (Menu_Item_ID == 106 || Menu_Item_ID == 108 || Menu_Item_ID == 195)
        {
            if (Client_Code.ToLower() == "nandwana")
            {
                Proc_Name = "EC_RPT_Direct_Printing_MR_Nandwana";
            }
            else if (Client_Code.ToLower() == "excel")
            {
                Proc_Name = "EC_RPT_Direct_Printing_MR_Excel";
            }
            else if (Client_Code.ToLower() == "reach")
            {
                Proc_Name = "EC_RPT_Direct_Printing_MR_Reach";
            }
        }
        //------------------------------------------Transport Bill--------------------------------------
        else if (Menu_Item_ID == 143)
        {
            if (Client_Code.ToLower() == "nandwana")
            {
                if (SpecialBillFormat == 1)
                {
                    Proc_Name = "EC_RPT_Direct_Printing_Transport_Bill_SpecialBillFormat";
                }
                else
                {
                    Proc_Name = "EC_RPT_Direct_Printing_Transport_Bill_Nandwana";
                }
            }
            else if (Client_Code.ToLower() == "excel")
            {
                Proc_Name = "EC_RPT_Direct_Printing_Transport_Bill_Excel";
            }
            else if (Client_Code.ToLower() == "reach")
            {
                Proc_Name = "EC_RPT_Direct_Printing_Transport_Bill_Reach";
            }
        }
        //------------------------------------------ALS--------------------------------------
        else if (Menu_Item_ID == 154)
        {
            if (Client_Code.ToLower() == "excel")
            {
                Proc_Name = "EC_RPT_Direct_Printing_ALS_Excel";
            }
        }
        //------------------------------------------Manifest--------------------------------------
        else if (Menu_Item_ID == 51)
        {
            if (Client_Code.ToLower() == "nandwana")
            {
                Proc_Name = "EC_RPT_Direct_Printing_Manifest_Excel";
            }
        }
        //------------------------------------------Inward Invoice Printing--------------------------------------
        else if (Menu_Item_ID == 515151)
        {
            Proc_Name = "EC_RPT_Menifest_Printing_From_Pending_Inward";
        }
        //------------------------------------------Inward Invoice Printing For Creator--------------------------------------
        else if (Menu_Item_ID == 525252)
        {
            Proc_Name = "EC_RPT_Menifest_Printing_From_Pending_Inward";
        }
        //------------------------------------------LHPO--------------------------------------
        else if (Menu_Item_ID == 73)
        {
            if (Client_Code.ToLower() == "nandwana")
            {
                //Proc_Name = "EC_RPT_Direct_Printing_LHPO_Nandwana";
                Proc_Name = "EC_RPT_Direct_Printing_LHPO_Nandwana_MemoWise";
            }
            else if (Client_Code.ToLower() == "excel")
            {
                Proc_Name = "EC_RPT_Direct_Printing_LHPO_Excel";
            }
            else if (Client_Code.ToLower() == "reach")
            {
                Proc_Name = "EC_RPT_Direct_Printing_LHPO_Reach";
            }

        }
        //------------------------------------------TAS--------------------------------------
        else if (Menu_Item_ID == 158)
        {
            if (Client_Code.ToLower() == "excel")
            {
                Proc_Name = "EC_RPT_Direct_Printing_TAS_Excel";
            }
        }
        //------------------------------------------AUS--------------------------------------
        else if (Menu_Item_ID == 72 || Menu_Item_ID == 115)
        {
            if (Client_Code.ToLower() == "excel")
            {
                Proc_Name = "EC_RPT_Direct_Printing_AUS_Excel";
            }
            else if (Client_Code.ToLower() == "nandwana" && AUSTYPE == "")
            {
                Proc_Name = "EC_RPT_Direct_Printing_Common";
            }
            else if (Client_Code.ToLower() == "nandwana" && AUSTYPE == "label")
            {
                Proc_Name = "EC_Opr_AUS_Print_Label";
            }
        }
        else if (Menu_Item_ID == 254)
        {
            if (Client_Code.ToLower() == "nandwana")
            {
                Proc_Name = "EC_RPT_Direct_Printing_Trip_Settelment_2_Nandwana";
            }
        }
        else
        {
            Proc_Name = "EC_RPT_Direct_Printing_Common";
        }
        return Proc_Name;
    }

    private string Get_Crystal_Report(int Menu_Item_ID)
    {
        string Path = "";
        Printer_ID = 0;
        //------------------------------------------GC--------------------------------------
        if (Menu_Item_ID == 30 || Menu_Item_ID == 213)
        {
            if (Client_Code.ToLower() == "nandwana")
            {
                Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_GC_OMBharat.rpt";
                Paper_Size = "OMBharat_GC";
                Printer_ID = 1;
            }
            else if (Client_Code.ToLower() == "excel")
            {
                Path = "~/Reports/Direct_Printing/RPT/RPT_Excel/EC_RPT_GC_Excel.rpt";
                Paper_Size = "Excel_GC";
                Printer_ID = 1;
            }
        }
        //------------------------------------------MR Bkg, Dly/CreditMemo--------------------------------------
        else if (Menu_Item_ID == 106 || Menu_Item_ID == 108 || Menu_Item_ID == 195)
        {
            if (Client_Code.ToLower() == "nandwana")
            {
                Path = "~/Reports/Direct_Printing/RPT/RPT_Nandwana/EC_RPT_MR_Nandwana.rpt";
                Printer_ID = 2;
                Paper_Size = "Nandwana_MR";
            }
            else if (Client_Code.ToLower() == "excel")
            {
                Path = "~/Reports/Direct_Printing/RPT/RPT_Excel/EC_RPT_CR_Excel.rpt";
                Paper_Size = "Excel_CR";
            }
            else if (Client_Code.ToLower() == "reach")
            {
                Path = "~/Reports/Direct_Printing/RPT/RPT_Reach/EC_RPT_CR_Reach.rpt";
                Paper_Size = "A4";
            }
        }
        //------------------------------------------Transport Bill--------------------------------------
        else if (Menu_Item_ID == 143)
        {
            if (Client_Code.ToLower() == "nandwana")
            {
                BillingClientID = Util.String2Int(ds.Tables[0].Rows[0][2].ToString());

                if (BillingClientID == 186)
                {
                    Path = "~/Reports/Direct_Printing/RPT/RPT_Nandwana/EC_RPT_Transport_Bill_Vashi_Electric.rpt";
                }
                else if (SpecialBillFormat == 1)
                {
                    Path = "~/Reports/Direct_Printing/RPT/RPT_Nandwana/EC_RPT_Transport_Bill_Special_Format.rpt";
                }
                else
                {
                    Path = "~/Reports/Direct_Printing/RPT/RPT_Nandwana/EC_RPT_Transport_Bill_Nandwana.rpt";
                }
                Paper_Size = "Nandwana_Transport_Bill";
                Printer_ID = 1;
            }
            else if (Client_Code.ToLower() == "excel")
            {
                Path = "~/Reports/Direct_Printing/RPT/RPT_Excel/EC_RPT_Transport_Bill_Excel.rpt";
                Paper_Size = "";
            }
            else if (Client_Code.ToLower() == "reach")
            {
                Path = "~/Reports/Direct_Printing/RPT/RPT_Reach/EC_RPT_Transport_Bill_Reach.rpt";
                Paper_Size = "A4";
            }
        }
        //------------------------------------------ALS--------------------------------------
        else if (Menu_Item_ID == 154)
        {
            if (Client_Code.ToLower() == "excel")
            {
                Path = "~/Reports/Direct_Printing/RPT/RPT_Excel/EC_RPT_ALS_Excel.rpt";
                Paper_Size = "Excel_Common_Size";
            }
            else
            {
                Path = "~/Reports/Direct_Printing/RPT/EC_RPT_ALS.rpt";
            }
        }
        //------------------------------------------Manifest--------------------------------------
        else if (Menu_Item_ID == 51)
        {
            if (Client_Code.ToLower() == "nandwana")
            {
                Path = "~/Reports/Direct_Printing/RPT/RPT_Excel/EC_RPT_Manifest_Excel.rpt";
                Paper_Size = "Excel_Common_Size";
            }
            else
            {
                Path = "~/Reports/Direct_Printing/RPT/EC_RPT_Manifest.rpt";
            }
        }

        //------------------------------------------Inward Invoice Printing--------------------------------------
        else if (Menu_Item_ID == 515151)
        {
            Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_PendingInwardMenifestPrinting.rpt";
        }
        //------------------------------------------Inward Invoice Printing For Creator--------------------------------------

        else if (Menu_Item_ID == 525252)
        {
            Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_PendingInwardMenifestPrinting.rpt";
        }
        //------------------------------------------LHPO--------------------------------------
        else if (Menu_Item_ID == 73)
        {
            if (Client_Code.ToLower() == "nandwana")
            {
                //Path = "~/Reports/Direct_Printing/RPT/RPT_Nandwana/EC_RPT_LHPO_Nandwana.rpt";
                Path = "~/Reports/Direct_Printing/RPT/RPT_Nandwana/EC_RPT_LHPO_Nandwana_New.rpt";
                Paper_Size = "A4";
            }
            else if (Client_Code.ToLower() == "excel")
            {
                Path = "~/Reports/Direct_Printing/RPT/RPT_Excel/EC_RPT_LHPO_Excel.rpt";
                Paper_Size = "Excel_Common_Size";
            }
            else if (Client_Code.ToLower() == "reach")
            {
                Path = "~/Reports/Direct_Printing/RPT/RPT_Reach/EC_RPT_LHPO_Reach.rpt";
                Paper_Size = "A4";
            }
            else
            {
                Path = "~/Reports/Direct_Printing/RPT/EC_RPT_LHPO.rpt";
            }
        }
        //------------------------------------------TAS--------------------------------------
        else if (Menu_Item_ID == 158)
        {
            if (Client_Code.ToLower() == "excel")
            {
                Path = "~/Reports/Direct_Printing/RPT/RPT_Excel/EC_RPT_TAS_Excel.rpt";
                Paper_Size = "Excel_Common_Size";
            }
            else
            {
                Path = "~/Reports/Direct_Printing/RPT/EC_RPT_TAS.rpt";
            }
        }
        //------------------------------------------AUS--------------------------------------
        else if (Menu_Item_ID == 72 || Menu_Item_ID == 115)
        {
            if (Client_Code.ToLower() == "excel")
            {
                Path = "~/Reports/Direct_Printing/RPT/RPT_Excel/EC_RPT_AUS_Excel.rpt";
                Paper_Size = "Excel_Common_Size";
            }
            else if (Client_Code.ToLower() == "nandwana" && AUSTYPE == "label")
            {
                Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_AUSLABELNew.rpt";
                Paper_Size = "Turanth_Label";
            }
            else if (Client_Code.ToLower() == "nandwana" && AUSTYPE == "")
            {
                Path = "~/Reports/Direct_Printing/RPT/EC_RPT_AUS.rpt";
            }
        }
        //------------------------------------------PDS--------------------------------------
        else if (Menu_Item_ID == 77)
        {
            Path = "~/Reports/Direct_Printing/RPT/EC_RPT_PDS.rpt";
        }
        else if (Menu_Item_ID == 254)
        {
            if (Client_Code.ToLower() == "nandwana")
            {
                Path = "~/Reports/Direct_Printing/RPT/RPT_Nandwana/EC_Rpt_Trip_Settelment_2_Nandwana.rpt";
                Paper_Size = "A4";
            }
        }
        //------------------------------------------PDS--------------------------------------
        else if (Menu_Item_ID == 278)
        {
            Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_TruckBharai.rpt";
        }
        //------------------------------------------GDS--------------------------------------
        else if (Menu_Item_ID == 80)
        {
            Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_GDS.rpt";
        }
        //------------------------------------------DDC--------------------------------------
        else if (Menu_Item_ID == 82)
        {
            Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_DDC.rpt";
        }
        //------------------------------------------DDC Tempo Freight--------------------------------------
        else if (Menu_Item_ID == 286)
        {
            Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_DDCTempoFrgt_OMBharat.rpt";
        }
        return Path;
    }

    private DataSet Get_DataSet(int Menu_Item_ID, int Document_ID, string Proc_Name)
    {
        SqlParameter[] sqlParam = {
            objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int,0,Menu_Item_ID),
            objDAL.MakeInParams("@Document_ID",SqlDbType.Int,0,Document_ID)};

        objDAL.RunProc(Proc_Name, sqlParam, ref ds);
        return ds;
    }

    private DataSet Get_DataSetInwardInvoicePrint(int Document_ID, string Proc_Name)
    {
        SqlParameter[] sqlParam = {
            objDAL.MakeInParams("@Memo_Id",SqlDbType.Int,0,Document_ID)};

        objDAL.RunProc(Proc_Name, sqlParam, ref ds);
        return ds;
    }

    private DataSet Get_DataSet_Reach(int Menu_Item_ID, int Document_ID, int User_ID, string Proc_Name)
    {
        SqlParameter[] sqlParam = {
            objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int,0,Menu_Item_ID),
            objDAL.MakeInParams("@Document_ID",SqlDbType.Int,0,Document_ID),
            objDAL.MakeInParams("@User_ID",SqlDbType.Int,0,User_ID)};

        objDAL.RunProc(Proc_Name, sqlParam, ref ds);
        return ds;
    }

    private void Generate_XmlSchema(string Client_Code, DataSet dsSchema)
    {
        if (Client_Code.ToLower() == "nandwana")
        {
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_GC_Nandwana.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_GC_OMBharat.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_MR_Nandwana.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_Transport_Bill_SpecialFormat.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_LHPO_Nandwana.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_LHPO_Nandwana_New.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_LHPO_Manifest_Excel_New.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_PDS_New.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_LR_New.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_TruckBharai.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_GDS.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_DDCTempoFrgt_OMBharat.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_AUSLABEL_OMBharat.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_Manifest_Excel.xsd");
        }
        else if (Client_Code.ToLower() == "excel")
        {
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_GC_Excel.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_CR_Excel.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_Transport_Bill_Excel.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_ALS_Excel.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_Manifest_Excel.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_TAS_Excel.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_LHPO_Excel.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_AUS_Excel.xsd");
        }
        else if (Client_Code.ToLower() == "reach")
        {
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_MR_Reach.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_Transport_Bill_Reach.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_LHPO_Reach.xsd");
        }
        else
        {
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_ALS.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_Manifest.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_LHPO.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_TAS.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_AUS.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_PDS.xsd");
        }
    }
}

