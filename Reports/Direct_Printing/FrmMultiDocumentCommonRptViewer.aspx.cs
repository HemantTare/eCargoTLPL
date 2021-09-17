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
using System.Text; 
using System.Drawing.Printing;

public partial class Reports_Direct_Printing_FrmMultiDocumentCommonRptViewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    string Crypt, Proc_Name;
    int Menu_Item_ID, Document_ID, User_ID, GC_Count, Printer_ID = 0, YearCode, MainId;
    string  Paper_Size, Client_Code, HierarchyCode, MemoNoForPrint;

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
        YearCode = UserManager.getUserParam().YearCode;
        MainId = UserManager.getUserParam().MainId;
        HierarchyCode = UserManager.getUserParam().HierarchyCode;
        

        Crypt = Request.QueryString["Menu_Item_Id"];
        Menu_Item_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["MemoNoForPrint"];
        MemoNoForPrint = ClassLibraryMVP.Util.DecryptToString(Crypt);


        Proc_Name = Get_Procedure_Name(Menu_Item_ID); 

        ds = Get_DataSet(Menu_Item_ID,MemoNoForPrint, Proc_Name); 

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
            CrystalReportViewer1.DisplayGroupTree = false;
            
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
        if (Menu_Item_ID == 30)
        {
            Proc_Name = "EC_RPT_MultiDocument_Printing_LR_OmBharat"; 
        }
        
        return Proc_Name;
    }

    private string Get_Crystal_Report(int Menu_Item_ID)
    {
        string Path = "";
        Printer_ID = 0;
        //------------------------------------------GC--------------------------------------
        if (Menu_Item_ID == 30)
        {
            Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_GC_OMBharat.rpt";
                Paper_Size = "OMBharat_GC";
                Printer_ID = 1; 
        }
        
        return Path;
    }

    private DataSet Get_DataSet(int Menu_Item_ID, string MemoNoForPrint, string Proc_Name)
    {
        SqlParameter[] sqlParam = {
            objDAL.MakeInParams("@Menu_Item_ID",SqlDbType.Int,0,Menu_Item_ID),
            objDAL.MakeInParams("@MemoNoForPrint",SqlDbType.VarChar,20,MemoNoForPrint)};

        objDAL.RunProc(Proc_Name, sqlParam, ref ds);
        return ds;
    }
 

    private void Generate_XmlSchema(string Client_Code, DataSet dsSchema)
    {
        if (Client_Code.ToLower() == "nandwana")
        {
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_EC_RPT_GatePass_ATC.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_EC_RPT_Direct_Printing_GR_ATC.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_Direct_Printing_Manifest_ATC.xsd");


            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_GC_Nandwana.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_GC_OMBharat.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_MR_Nandwana.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_Transport_Bill_Nandwana.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_LHPO_Nandwana.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_LHPO_Nandwana_New.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_LHPO_Manifest_Excel_New.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_PDS_New.xsd");
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_LR_New.xsd");
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

