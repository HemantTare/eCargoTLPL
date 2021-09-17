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

public partial class Reports_Direct_Printing_FrmCommonReportPopupViewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    string Crypt, Proc_Name;
    int Menu_Item_ID, Document_ID, User_ID, Printer_ID = 0;
    string Paper_Size, Client_Code;

    String scripts;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Menu_Item_ID = 143;
        //Document_ID = 143;
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

        Menu_Item_ID = Convert.ToInt32(Request.QueryString["Menu_Item_Id"]);
        Document_ID = Convert.ToInt32(Request.QueryString["Document_ID"]);

        Proc_Name = Get_Procedure_Name(Menu_Item_ID);
        ds = Get_DataSet(Menu_Item_ID, Document_ID, Proc_Name);

        Generate_XmlSchema(Client_Code, ds);
        string Path = Get_Crystal_Report(Menu_Item_ID);

        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds);

        Bind_To_Viewer();
    }

    private void Bind_To_Viewer()
    {
        Printer_Settings pkSizeObj = new Printer_Settings(Printer_ID, Paper_Size);

        try
        {
            CrystalReportViewer1.ReportSource = myReportDocument;

            myReportDocument.PrintOptions.PaperSize = pkSizeObj.Papersize;
            myReportDocument.PrintOptions.PrinterName = pkSizeObj.PrinterName.ToString();

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
        //------------------------------------------Transport Bill--------------------------------------
        if (Menu_Item_ID == 143)
        {
           if (Client_Code.ToLower() == "reach")
            {
                Proc_Name = "EC_RPT_Direct_Printing_Transport_Bill_Annexure_Reach";
            }
        }

        return Proc_Name;
    }

    private string Get_Crystal_Report(int Menu_Item_ID)
    {
        string Path = "";
        Printer_ID = 0;
        
        //------------------------------------------Transport Bill--------------------------------------
        if (Menu_Item_ID == 143)
        {
            if (Client_Code.ToLower() == "reach")
            {
                Path = "~/Reports/Direct_Printing/RPT/RPT_Reach/EC_RPT_Transport_Bill_Annexure_Reach.rpt";
                Paper_Size = "A4";
            }
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

    private void Generate_XmlSchema(string Client_Code, DataSet dsSchema)
    {
        if (Client_Code.ToLower() == "reach")
        {
            //dsSchema.WriteXmlSchema("C:/XMLSchema_EC_RPT_Transport_Bill_Annexure_Reach.xsd");
        }
    }
}

