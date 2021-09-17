using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Raj.EC;
using System.Text;

//Author : Ankit champaneriya
//Desc   : Booking register Report
//Date   : 03-01-09

public partial class Reports_Sales_Billing_FrmTopWalkinClientDetails : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    int TotalClient;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();

    private DateTime From_Date
    {
        set
        {
            Wuc_From_To_Datepicker1.SelectedFromDate = value;
        }
        get { return Wuc_From_To_Datepicker1.SelectedFromDate; }
    }

    private DateTime To_Date
    {
        set
        {
            Wuc_From_To_Datepicker1.SelectedToDate = value;
        }
        get { return Wuc_From_To_Datepicker1.SelectedToDate; }
    }

    private int TargetBranchID
    {

        get { return Util.String2Int(ddl_TargetBranch.SelectedValue); }
        set
        {
            ddl_TargetBranch.SelectedValue = Util.Int2String(value);
        }
    }


    private DateTime From_Date2
    {
        set
        {
            Wuc_From_To_Datepicker2.SelectedFromDate = value;
        }
        get { return Wuc_From_To_Datepicker2.SelectedFromDate; }
    }

    private DateTime To_Date2
    {
        set
        {
            Wuc_From_To_Datepicker2.SelectedToDate = value;
        }
        get { return Wuc_From_To_Datepicker2.SelectedToDate; }
    }

    #endregion



    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        //Wuc_Export_To_Excel1.FileName = "WalkInNRegularClientDetails(" + Convert.ToString(TotalClient) + ")";

        if (IsPostBack == false)
        {
            Wuc_Region_Area_Branch1.SetDDLBranchAutoPostback = true;


            Fill_DeliveryArea();

            FillTargetBranch();

            BindGrid("form_and_pageload", e);

            From_Date = DateTime.Now;
            To_Date = DateTime.Now;
        }
        Wuc_Region_Area_Branch1.BranchIndexChange += new EventHandler(Fill_DlyArea);
    }


    private void Fill_DlyArea(object sender, EventArgs e)
    {
        Fill_DeliveryArea();
    }



    private void Fill_DeliveryArea()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@BranchId", SqlDbType.Int, 0, Wuc_Region_Area_Branch1.BranchID) };
        objDAL.RunProc("EC_Fill_DeliveryArea_List", objSqlParam, ref ds);

        ddl_DeliveryArea.DataSource = ds;
        ddl_DeliveryArea.DataTextField = "DeliveryAreaName";
        ddl_DeliveryArea.DataValueField = "DeliveryAreaID";
        ddl_DeliveryArea.DataBind();

    }

    private void FillTargetBranch()
    {
        string query;

        if (rdl_ReportType.SelectedValue == "0")
        {
            query = "Select '0' Branch_ID,' Select One' Branch_Name Union Select Branch_ID,Branch_Name from EC_Master_Branch where Is_Active = " + 1 + " order by Branch_Name";
        }
        else
        {
            query = "Select '0' Branch_ID,' Select One' Branch_Name Union Select Branch_ID,Branch_Name from EC_Master_Branch where Is_Active = " + 1 + " And Branch_ID Not in (12,13,72) order by Branch_Name";
        }

        DataSet ds = new DataSet();
        ds = ObjCommon.EC_Common_Pass_Query(query);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_TargetBranch.DataSource = ds;
            ddl_TargetBranch.DataValueField = "Branch_ID";
            ddl_TargetBranch.DataTextField = "Branch_Name";
            ddl_TargetBranch.DataBind();
        }
        else
        {
            ddl_TargetBranch.Items.Clear();
        }
    }


    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if (From_Date > To_Date == true)
        {
            lbl_Error.Text = "Invalid Date";
            dg_Grid.Visible = false;
        }
        else if (rdl_ReportType.SelectedValue == "0" && ddl_DeliveryArea.SelectedIndex < 0)
        {
            lbl_Error.Text = "Select Delivery Area";
            dg_Grid.Visible = false;

        }
        else if (rdl_ReportType.SelectedValue == "1" && Wuc_Region_Area_Branch1.BranchID <= 0)
        {
            lbl_Error.Text = "Select Branch";
            dg_Grid.Visible = false;

        }

        else
        {
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.CurrentPageIndex = 0;
            BindGrid("form", e);

        }
    }



    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "form_and_pageload") return;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;

        if (rdl_ReportType.SelectedValue == "0" && ddl_DeliveryArea.SelectedIndex >= 0)
        {
            SqlParameter[] objSqlParam ={  
                objDAL.MakeInParams("@DeliveryAreaId", SqlDbType.Int,0,Convert.ToInt32(ddl_DeliveryArea.SelectedValue)),
                objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
                objDAL.MakeInParams("@To_date", SqlDbType.DateTime,0,To_date), 
                objDAL.MakeInParams("@BkgBranchId", SqlDbType.Int,0,TargetBranchID),
                objDAL.MakeInParams("@From_Date2", SqlDbType.DateTime,0,From_Date2),
                objDAL.MakeInParams("@To_date2", SqlDbType.DateTime,0,To_Date2),
                objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
                objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)  
            };

            objDAL.RunProc("EC_RPT_Top_Client_Details_DeliveryAreaWise", objSqlParam, ref ds);

        }
        else
        {
            SqlParameter[] objSqlParam ={  
                objDAL.MakeInParams("@BranchId", SqlDbType.Int,0,Convert.ToInt32(Wuc_Region_Area_Branch1.BranchID)),
                objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
                objDAL.MakeInParams("@To_date", SqlDbType.DateTime,0,To_Date),
                objDAL.MakeInParams("@DlyBranchId", SqlDbType.Int,0,TargetBranchID),
                objDAL.MakeInParams("@From_Date2", SqlDbType.DateTime,0,From_Date2),
                objDAL.MakeInParams("@To_date2", SqlDbType.DateTime,0,To_Date2),
                objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
                objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)  
            };

            objDAL.RunProc("EC_RPT_Top_Client_Details_BookingBranchWise", objSqlParam, ref ds);

        }


        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[1].Rows[0][0].ToString();

        TotalClient = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());

        //calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);


        if (rdl_ReportType.SelectedValue == "0" && ddl_DeliveryArea.SelectedIndex >= 0)
        {
            dg_Grid.Columns[12].Visible = false;
            dg_Grid.Columns[13].Visible = false;
            dg_Grid.Columns[14].Visible = false;
        }
        else
        {
            dg_Grid.Columns[12].Visible = true;
            dg_Grid.Columns[13].Visible = true;
            dg_Grid.Columns[14].Visible = true;
        }

        if (TargetBranchID <= 0)
        {
            dg_Grid.Columns[11].Visible = false;
        }
        else
        {
            dg_Grid.Columns[11].HeaderText = ddl_TargetBranch.SelectedItem.Text + " NoOfLR";
            dg_Grid.Columns[11].Visible = true;
        }

        if (CallFrom == "exporttoexcelusercontrol")
        {

            if (rdl_ReportType.SelectedValue == "0" && ddl_DeliveryArea.SelectedIndex > 0)
            {
                Wuc_Export_To_Excel1.FileName = ddl_DeliveryArea.SelectedItem.Text + "(" + Convert.ToString(TotalClient) + ")";
            }
            else
            {
                Wuc_Export_To_Excel1.FileName = Wuc_Region_Area_Branch1.SelectedBranchText.ToString() + "(" + Convert.ToString(TotalClient) + ")";
            }

            if (ds.Tables[0].Rows.Count > 0)
            {
                PrepareDTForExportToExcel();
            }
        }
    }

    private void PrepareDTForExportToExcel()
    {
        if (rdl_ReportType.SelectedValue == "0" && ddl_DeliveryArea.SelectedIndex >= 0)
        {
            ds.Tables[0].Columns.Remove("NoOfLRSurat");
            ds.Tables[0].Columns.Remove("NoOfLRVapi");
            ds.Tables[0].Columns.Remove("NoOfLRPune");
        }

        if (TargetBranchID <= 0)
        {
            ds.Tables[0].Columns.Remove("NoOfLR2");
        }

        Wuc_Export_To_Excel1.has_last_row_as_total = false;
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    #endregion


    private void ClearVariables()
    {
        ds = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void ddl_Branch_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_DeliveryArea();
    }

    protected void rdl_ReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdl_ReportType.SelectedValue == "0")
        {
            lbl_DeliveryArea.Visible = true;
            ddl_DeliveryArea.Visible = true;
            lbl_TargetBranch.Text = "Booking Branch:";
        }
        else
        {
            lbl_DeliveryArea.Visible = false;
            ddl_DeliveryArea.Visible = false;
            lbl_TargetBranch.Text = "Delivery Branch:";
        }

        FillTargetBranch();

    }

    protected void ddl_TargetBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (TargetBranchID > 0)
        {
            dg_Grid.Columns[11].HeaderText = ddl_TargetBranch.SelectedItem.Text + " NoOfLR";
        }

    }

    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) //  == DataControlRowType.DataRow)
        {

            LinkButton lnk_Client_Name = new LinkButton();
            lnk_Client_Name = (LinkButton)e.Item.FindControl("lnk_Client_Name");

            int Client_ID,TotalLR;
            string Mobile_No, Phone1, Phone2;

            Client_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Client_ID").ToString());
            Mobile_No = DataBinder.Eval(e.Item.DataItem, "Mobile_No").ToString();
            Phone1 = DataBinder.Eval(e.Item.DataItem, "Phone1").ToString();
            Phone2 = DataBinder.Eval(e.Item.DataItem, "Phone2").ToString();
            TotalLR = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "NoOfLR").ToString());


            if (rdl_ReportType.SelectedValue == "1")
            {
                StringBuilder PathClient = new StringBuilder(Util.GetBaseURL());
                PathClient.Append("/");
                PathClient.Append("Reports/Sales Billing/FrmTopWalkinClientDetailsMonthly.aspx?&Branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Wuc_Region_Area_Branch1.BranchID)
                    + "&Client_ID=" + Client_ID + "&From_Date=" + From_Date + "&To_Date=" + To_Date + "&Mobile_No=" + Mobile_No + "&Phone1=" + Phone1 + "&Phone2=" + Phone2 + "&Branch_Name=" + Wuc_Region_Area_Branch1.SelectedBranchText + "&Client_Name=" + lnk_Client_Name.Text + "&TotalLR=" + TotalLR + "&DeliveryAreaId=0" + "&DeliveryAreaName=" );

                lnk_Client_Name.Attributes.Add("onclick", "return viewwindow_TopClientDetails('" + PathClient + "')");
            }
            else 
            {
                StringBuilder PathClient = new StringBuilder(Util.GetBaseURL());
                PathClient.Append("/");
                PathClient.Append("Reports/Sales Billing/FrmTopWalkinClientDetailsMonthly.aspx?&Branch_Id=" + ClassLibraryMVP.Util.EncryptInteger(Wuc_Region_Area_Branch1.BranchID)
                    + "&Client_ID=" + Client_ID + "&From_Date=" + From_Date + "&To_Date=" + To_Date + "&Mobile_No=" + Mobile_No + "&Phone1=" + Phone1 + "&Phone2=" + Phone2 + "&Branch_Name=" + Wuc_Region_Area_Branch1.SelectedBranchText + "&Client_Name=" + lnk_Client_Name.Text + "&TotalLR=" + TotalLR + "&DeliveryAreaId=" + ddl_DeliveryArea.SelectedValue + "&DeliveryAreaName=" + ddl_DeliveryArea.SelectedItem.Text);

                lnk_Client_Name.Attributes.Add("onclick", "return viewwindow_TopClientDetails('" + PathClient + "')");
            }
        }
    }
}
