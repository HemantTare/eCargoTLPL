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
using ClassLibraryMVP;
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;

public partial class Master_Driver_FrmDriverCleanerUpdateHistory : System.Web.UI.Page
{
    bool ATS = false;
    private DataSet objDS;

    int Total;



    private string ErrorMessage
    {
        set { lblErrors.Text = value.ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "DriverCleanerHistory";


        if (!IsPostBack)
        {
            txtDriver.Focus();
        }


        lstDriver.Style.Add("visibility", "hidden");
        lstVehicle.Style.Add("visibility", "hidden");


    }

    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {

        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            //int Driver_Id;
            //LinkButton lbtn_Name;

            //Driver_Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Driver_ID").ToString());

            //lbtn_Name = (LinkButton)e.Item.FindControl("lbtn_Name");

            //lbtn_Name.Attributes.Add("onclick", "return viewwindow_Driver('" + ClassLibraryMVP.Util.EncryptInteger(Driver_Id) + "','" + hdn_IsCleaner.Value + "')");


        }
    }


    protected void lbtn_Vehicle_Click(object sender, EventArgs e)
    {
        int Vehicle_Id, MenuItemId;
        LinkButton lbtn_Vehicle = (LinkButton)sender;
        Label lbl_VehicleID;

        DataGridItem _item = (DataGridItem)lbtn_Vehicle.Parent.Parent;
        lbl_VehicleID = (Label)_item.FindControl("lbl_VehicleID");

        Vehicle_Id = Util.String2Int(lbl_VehicleID.Text);

        MenuItemId = 85;
        
        StateManager.SaveState("QueryString", "1");
        String ViewUrl = "../../" + Rights.GetObject().GetLinkDetails(MenuItemId).ViewUrl;

        ViewUrl = ViewUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Vehicle_Id);
        //Response.Redirect(ViewUrl);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + ViewUrl + "','_blank', 'width=1200, height=800,top=10, left=10, menubar=no, resizable=no,scrollbars=yes')", true);

    }

    protected void lbtn_Name_Click(object sender, EventArgs e)
    {
        int Driver_Id, MenuItemId;
        LinkButton lbtn_Name = (LinkButton)sender;
        Label lbl_DriverID;

        DataGridItem _item = (DataGridItem)lbtn_Name.Parent.Parent;
        lbl_DriverID  = (Label)_item.FindControl("lbl_DriverID");

        Driver_Id  = Util.String2Int(lbl_DriverID.Text);


        if (rbl_DriverCleaner.SelectedValue == "0")
        {
            MenuItemId = 63;
            StateManager.SaveState("QueryString", "1");
        }
        else
        {
            MenuItemId = 64;
            StateManager.SaveState("QueryString", "0");
        }


        String ViewUrl = "../../" + Rights.GetObject().GetLinkDetails(MenuItemId).ViewUrl;

        ViewUrl = ViewUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Driver_Id);
        //Response.Redirect(ViewUrl);

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + ViewUrl + "','_blank', 'width=1200, height=800,top=10, left=10, menubar=no, resizable=no,scrollbars=yes')", true);

        
    }



    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }



        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@IsCleaner", SqlDbType.Int, 0,Util.String2Int(hdn_IsCleaner.Value)),
            objDAL.MakeInParams("@Driver_ID", SqlDbType.Int, 0,Util.String2Int(hdn_DriverId.Value)),
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0,Util.String2Int(hdn_VehicleId.Value)),
            objDAL.MakeInParams("@FromDate",SqlDbType.DateTime,0,dtp_FromDate.SelectedDate ),
            objDAL.MakeInParams("@ToDate",SqlDbType.DateTime,0,dtp_ToDate.SelectedDate ),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex ),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("EF_Rpt_Driver_Cleaner_History", objSqlParam, ref objDS);

        dg_Grid.VirtualItemCount = Util.String2Int(objDS.Tables[1].Rows[0][0].ToString());

        calculate_totals();


        Common objcommon = new Common();

        objcommon.ValidateReportForm(dg_Grid, objDS.Tables[0], CallFrom, lblErrors);


        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }

    private void calculate_totals()
    {
        DataRow dr = objDS.Tables[1].Rows[0];
        Total = Util.String2Int(dr["Total"].ToString());
    }

    private void PrepareDTForExportToExcel()
    {

        DataRow dr = objDS.Tables[0].NewRow();
        dr["Driver_Name"] = "Total : ";
        dr["Vehicle_No"] = Total;

        objDS.Tables[0].Columns.Remove("TrType");
        objDS.Tables[0].Columns.Remove("Transaction_ID");
        objDS.Tables[0].Columns.Remove("Is_Cleaner");
        objDS.Tables[0].Columns.Remove("Driver_ID");
        objDS.Tables[0].Columns.Remove("Vehicle_ID");



        objDS.Tables[0].Rows.Add(dr);

        Wuc_Export_To_Excel1.SessionExporttoExcel = objDS.Tables[0];

    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private bool ValidateUI()
    {
        bool ATS = false;

        //if (DriverID <= 0)
        //{
        //    lblErrors.Text = "Please Select Driver";
        //    ScriptManager.SetFocus(txtDriver);
        //}


        //else
        //{
        //    ATS = true;
        //}
        ATS = true;

        return ATS;
    }

    protected void btnview_Click(object sender, EventArgs e)
    {

        dg_Grid.CurrentPageIndex = 0;
        lblErrors.Text = "";
        BindGrid("form", e);

    }


    protected void btn_View_Click(object sender, EventArgs e)
    {
        if (ValidateUI())
        {

        }
    }



}
