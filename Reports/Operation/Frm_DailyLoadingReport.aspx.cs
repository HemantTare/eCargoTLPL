
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP;
using ClassLibraryMVP.Security;
using Raj.EC;
using Raj.EC.ControlsView;
using Raj.EC.OperationView;
using Raj.EC.OperationPresenter;


public partial class Reports_Operation_Frm_DailyLoadingReport : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    //decimal ChargedWeight,Actual_Weight,Articles,Basic_Freight,FOV_Charges;
    //decimal ODA_Charges,Other_Charges,Sub_Freight,STax_Amt,Total_Freight,Invoice_Value;
    //decimal Hamali_Charge, DD_Charge, Bilti_Charges;
    //TimePicker Timepicker1;
    //int TotalGC; 

    #endregion

    #region ControlsValues

    public DateTime From_Date
    {
        set
        {
            dtp_From_Date.SelectedDate = value;
            hdn_From_Date.Value = dtp_From_Date.SelectedDate.ToString();
        }
        get { return dtp_From_Date.SelectedDate; }
    }

    public DateTime To_Date
    {
        set
        {
            dtp_To_Date.SelectedDate = value;
            hdn_To_Date.Value = dtp_To_Date.SelectedDate.ToString();
        }
        get { return dtp_To_Date.SelectedDate; }
    }

    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Raj.EC.Common.GetMenuItemId() == 5229)
        {
            Label1.Text = "Daily Loading Report";
        }
        else
        {
            Label1.Text = "Vehicle Weighing Report";
            Wuc_Export_To_Excel1.Visible = false;
            //Wuc_Region_Area_Branch1.Visible = false;
            BranchAreaRegion.Visible = false;
        }

        if (IsPostBack == false)
        {
            From_Date = DateTime.Now;
            To_Date = DateTime.Now;
        }
        //string VehicleNo = txtVehicleNo.Text.Trim();

        //StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        //Path.Append("/");
        //Path.Append("Reports/Operation/Frm_DailyLoadingReportViewer.aspx?Menu_Item_Id=" + Util.EncryptInteger(Raj.EC.Common.GetMenuItemId()) + "&Region_Id=" + Util.EncryptInteger(Wuc_Region_Area_Branch1.RegionID) + "&Area_Id=" + Util.EncryptInteger(Wuc_Region_Area_Branch1.AreaID) + "&Branch_Id=" + Util.EncryptInteger(Wuc_Region_Area_Branch1.BranchID));

        //btn_view.Attributes.Add("onclick", "return Open_Details_Window('" + Path + "'," + hdn_From_Date.ClientID + "," + hdn_To_Date.ClientID + ")");

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(ExportToExcel);
        Wuc_Export_To_Excel1.FileName = "DailyLoadingReport";
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        string VehicleNo = txtVehicleNo.Text.Trim();

        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/");

        Path.Append("Reports/Operation/Frm_DailyLoadingReportViewer.aspx?Menu_Item_Id=" + Util.EncryptInteger(Raj.EC.Common.GetMenuItemId()) + "&Region_Id=" + Util.EncryptInteger(Wuc_Region_Area_Branch1.RegionID) + "&Area_Id=" + Util.EncryptInteger(Wuc_Region_Area_Branch1.AreaID) + "&Branch_Id=" + Util.EncryptInteger(Wuc_Region_Area_Branch1.BranchID));


        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("Open_Details_Window('" + Path + "')");
        //sb.Append("Open_Details_Window('" + Path + "'," + hdn_From_Date.ClientID + "," + hdn_To_Date.ClientID + ")");
        //sb.Append("Open_Details_Window('" + Path + "'," + hdn_From_Date.ClientID + "," + hdn_To_Date.ClientID + ")");
        //sb.Append(Path);
        //sb.Append("');");
        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(), "script", sb.ToString());


        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(dtp_From_Date.SelectedDate, dtp_To_Date.SelectedDate, ref msg)) == true)
        {
            lbl_Error.Text = "";

        }
        else
        {
            lbl_Error.Text = msg;
        }
    }

    private void ExportToExcel(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Vehicle_No", SqlDbType.VarChar,50,txtVehicleNo.Text.Trim() ),
            objDAL.MakeInParams("@FromDate", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@ToDate", SqlDbType.DateTime,0,To_Date),
            objDAL.MakeInParams("@Region_Id", SqlDbType.Int ,0,Region_Id),
            objDAL.MakeInParams("@Area_Id", SqlDbType.Int ,0,Area_id),            
            objDAL.MakeInParams("@Branch_Id", SqlDbType.Int ,0,Branch_id)
        
        };

        objDAL.RunProc("EC_RPT_DailyLoadingReport", objSqlParam, ref ds);


        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }

    }

    private void PrepareDTForExportToExcel()
    {


        DataRow dr = ds.Tables[0].NewRow();
        dr["TripMemoNo"] = "Total";
        ds.Tables[0].Rows.Add(dr);

        ds.Tables[0].Columns.Remove("TripMemoNo");
        ds.Tables[0].Columns.Remove("TripMemoType");
        ds.Tables[0].Columns.Remove("DestinationFrt");
        ds.Tables[0].Columns.Remove("CrossingFrt");

        ds.Tables[0].Columns[0].ColumnName = "Date";
        ds.Tables[0].Columns[1].ColumnName = "Vehicle No.";
        ds.Tables[0].Columns[2].ColumnName = "From";
        ds.Tables[0].Columns[3].ColumnName = "To";
        ds.Tables[0].Columns[4].ColumnName = "Inv. No.";
        ds.Tables[0].Columns[5].ColumnName = "LR";
        ds.Tables[0].Columns[6].ColumnName = "Art";
        ds.Tables[0].Columns[7].ColumnName = "Wt.";

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    #endregion

    public void ClearVariables()
    {
        ds = null;
    }


    protected void dtp_From_Date_SelectionChanged(object sender, EventArgs e)
    {
        hdn_From_Date.Value = dtp_From_Date.SelectedDate.ToString();
    }
    protected void dtp_To_Date_SelectionChanged(object sender, EventArgs e)
    {
        hdn_To_Date.Value = dtp_To_Date.SelectedDate.ToString();

    }
}
