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

public partial class Reports_Operation_Inward_Frm_LabelPrintingViewer : System.Web.UI.Page
{
    ReportDocument myReportDocument = new ReportDocument();
    DAL objDAL = new DAL();
    DataSet ds = new DataSet();

    string Crypt, Proc_Name;
    int Menu_Item_ID, User_ID, GC_Count, Printer_ID = 0;
    string VehicleNo, Paper_Size, Client_Code, From_Date, To_Date;

    String scripts;

    public DataTable SessionBindLabelPrintingGrid
    {
        get { return StateManager.GetState<DataTable>("BindLabelPrintingGrid"); }
        set
        {
            StateManager.SaveState("BindLabelPrintingGrid", value);  
        }
    }

    public String LabelPrintingDetailsXML
    {
        get
        {
            DataSet _objDs = new DataSet();  

            _objDs.Merge(SessionBindLabelPrintingGrid); 
            _objDs.Tables[0].TableName = "LabelPrintingGrid_Details";
            return _objDs.GetXml().ToLower();
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Client_Code = CompanyManager.getCompanyParam().ClientCode;
        User_ID = UserManager.getUserParam().UserId; 

        Crypt = Request.QueryString["Menu_Item_Id"];
        Menu_Item_ID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

        ds.Merge(SessionBindLabelPrintingGrid); 

        Generate_XmlSchema(ds);
        string Path = Get_Crystal_Report();

        myReportDocument.Load(MapPath(Path));
        myReportDocument.SetDataSource(ds); 
        Bind_To_Viewer();
    }
 

    private void Bind_To_Viewer()
    {
        ////Printer_Settings pkSizeObj = new Printer_Settings(Printer_ID, Paper_Size);

        try
        {
 
            CrystalReportViewer1.ReportSource = myReportDocument;

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
                Path = "~/Reports/Direct_Printing/RPT/RPT_OMBharat/EC_RPT_CongineeLABELS.rpt";
                Paper_Size = "German Std Fanfold";
            }  
        return Path;
    }

    private void Generate_XmlSchema(DataSet dsSchema)
    { 
        //dsSchema.WriteXmlSchema("C:/XMLSchema_CongineeLABELS.xsd"); 
    }
}

