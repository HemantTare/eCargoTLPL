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

public partial class Reports_Direct_Printing_frmGCDlyReceiptViewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    string Crypt, Proc_Name;
    int Menu_Item_ID, Document_ID, User_ID, GC_Count, Printer_ID = 0;
    string Paper_Size, Client_Code;

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


        Crypt = Request.QueryString["Menu_Item_Id"];
        Menu_Item_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        Crypt = Request.QueryString["Document_ID"];
        Document_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);
        //dlyType = Request.QueryString["dlyType"];
        ds = Get_DataSet(Menu_Item_ID, Document_ID); 


        Generate_XmlSchema(Client_Code,ds);
        string Path = Get_Crystal_Report();

        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds);
        //myReportDocument.SetParameterValue("dlyType", dlyType);
        Bind_To_Viewer();
    }

    private DataSet Get_DataSet(int Menu_Item_ID, int Document_ID)
    {
        SqlParameter[] sqlParam = { objDAL.MakeInParams("@GCID", SqlDbType.Int, 0, Document_ID) };
        objDAL.RunProc("EC_Print_Delivered_GC_Ombharat", sqlParam, ref ds);
        return ds;
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

    private string Get_Crystal_Report()
    {
        string Path = "";  
         
            if (Client_Code.ToLower() == "nandwana")
            {
                Path = "~/Reports/Direct_Printing/RPT/EC_RPT_Print_DoorGodown_Delivered_GC.rpt";
                Paper_Size = "Turanth_DlyMemo";
                Printer_ID = 1;
               
            }  
        return Path;
    }

    private void Generate_XmlSchema(string Client_Code, DataSet dsSchema)
    {

        //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_Print_PDS_Delivered_GC.xsd");
           
    }
}

