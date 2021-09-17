using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_Finance_frm_GC_Costing_Register_Nandwana : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();       

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        ddl_Party.DataValueField = "Client_Name";
        ddl_Party.DataTextField = "Client_ID";

        ddl_Party.OtherColumns = ddl_Party_Type.SelectedValue;

        Wuc_Export_To_Excel1.FileName = CompanyManager.getCompanyParam().GcCaption + " Costing Register";

        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);

            lbl_Search.Visible = false;
            ddl_Party.Visible = false; 
        }
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;

        int Booking_Type_Id = Wuc_GC_Parameters1.Booking_Type_ID;
        int Delivery_Type_Id = Wuc_GC_Parameters1.Delivery_Type_ID;
        int Payment_Type_Id = Wuc_GC_Parameters1.Payment_Type_ID;

        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        int Division_ID = 0;
        int Search_id = 0;
        if (ddl_Party.SelectedValue == "")
        {
            Search_id = 0;
        }
        else
        {
            Search_id = Convert.ToInt32(ddl_Party.SelectedValue);
        }

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
              
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID),

            objDAL.MakeInParams("@Booking_Type_Id", SqlDbType.Int,0,Booking_Type_Id),
            objDAL.MakeInParams("@Delivery_Type_Id", SqlDbType.Int,0,Delivery_Type_Id),
            objDAL.MakeInParams("@Payment_Type_Id", SqlDbType.Int,0,Payment_Type_Id),

            objDAL.MakeInParams("@Search_Name",SqlDbType.VarChar,100,ddl_Party.SelectedText),
            objDAL.MakeInParams("@Search_Id",SqlDbType.Int,0,Search_id),
            objDAL.MakeInParams("@Party_Type_Flg", SqlDbType.Int, 0, Convert.ToInt32(ddl_Party_Type.SelectedValue))

            //objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            //objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("[EC_Rpt_GC_Costing_List_Nandwana]", objSqlParam, ref ds);

        int TotalRecords = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
        dg_Grid.VirtualItemCount = TotalRecords;

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error, TotalRecords.ToString());

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.CurrentPageIndex = 0;
            BindGrid("form", e);
        }
        else
        {
            lbl_Error.Text = msg;
            dg_Grid.Visible = false;
        }
    }

    public void ClearVariables()
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

    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int GC_ID;
            LinkButton lnk_GC_No;

            GC_ID = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "GC_ID").ToString());
            lnk_GC_No = (LinkButton)e.Item.FindControl("lnk_GC_No");

            lnk_GC_No.Attributes.Add("onclick", "return viewwindow_general('" + GC_ID + "')");
        }
        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbl_Total, lbl_booking_Income, lbl_Delivery_Income, lbl_total_discount_amount, lbl_Total_Income,
                  lbl_Crossing_Cost, lbl_Hamali_Cost, lbl_Incidental_Expense,
                  lbl_Cartage_Cost, lbl_door_delivery_Cost, lbl_BTH_Other_Charges,
                  lbl_Total_Expense, lbl_Profit_Loss;

            lbl_Total = (Label)e.Item.FindControl("lbl_Total");
            lbl_booking_Income = (Label)e.Item.FindControl("lbl_booking_Income");
            lbl_Delivery_Income = (Label)e.Item.FindControl("lbl_Delivery_Income");
            lbl_total_discount_amount = (Label)e.Item.FindControl("lbl_total_discount_amount");
            lbl_Total_Income = (Label)e.Item.FindControl("lbl_Total_Income");

            lbl_Crossing_Cost = (Label)e.Item.FindControl("lbl_Crossing_Cost");
            lbl_Hamali_Cost = (Label)e.Item.FindControl("lbl_Hamali_Cost");
            lbl_Incidental_Expense = (Label)e.Item.FindControl("lbl_Incidental_Expense");
            lbl_Cartage_Cost = (Label)e.Item.FindControl("lbl_Cartage_Cost");
            lbl_door_delivery_Cost = (Label)e.Item.FindControl("lbl_door_delivery_Cost");
            lbl_BTH_Other_Charges = (Label)e.Item.FindControl("lbl_BTH_Other_Charges");
            lbl_Total_Expense = (Label)e.Item.FindControl("lbl_Total_Expense");

            lbl_Profit_Loss = (Label)e.Item.FindControl("lbl_Profit_Loss");


            lbl_Total.Text = "Total : ";
            lbl_booking_Income.Text = Convert.ToString(ds.Tables[0].Compute("SUM([Booking Income])", ""));
            lbl_Delivery_Income.Text = Convert.ToString(ds.Tables[0].Compute("SUM([Delivery Income])", ""));
            lbl_total_discount_amount.Text = Convert.ToString(ds.Tables[0].Compute("SUM([total discount amount])", ""));
            lbl_Total_Income.Text = Convert.ToString(ds.Tables[0].Compute("SUM([Total Income])", ""));

            lbl_Crossing_Cost.Text = Convert.ToString(ds.Tables[0].Compute("SUM([Direct Cost])", ""));
            lbl_Hamali_Cost.Text = Convert.ToString(ds.Tables[0].Compute("SUM([Hamali Cost])", ""));
            lbl_Incidental_Expense.Text = Convert.ToString(ds.Tables[0].Compute("SUM([Incidental Cost])", ""));
            lbl_Cartage_Cost.Text = Convert.ToString(ds.Tables[0].Compute("SUM([Local Cartage Cost])", ""));
            lbl_door_delivery_Cost.Text = Convert.ToString(ds.Tables[0].Compute("SUM([Door Delivery Expense])", ""));
            lbl_BTH_Other_Charges.Text = Convert.ToString(ds.Tables[0].Compute("SUM([BTH Other charges])", ""));
            lbl_Total_Expense.Text = Convert.ToString(ds.Tables[0].Compute("SUM([Total Expense])", ""));

            lbl_Profit_Loss.Text = Convert.ToString(ds.Tables[0].Compute("SUM([Profit Loss])", ""));

        }

    }

    private void PrepareDTForExportToExcel()
    {
            ds.Tables[0].Columns.Remove("GC_Id");
            ds.Tables[0].Columns.Remove("profit_loss_sr_no");

            DataRow dr;
            dr = ds.Tables[0].NewRow();
            dr["CN No"] = "Total";
            dr["total income"] = ds.Tables[0].Compute("SUM([total income])", "");
            dr["Total Expense"] = ds.Tables[0].Compute("SUM([total Expense])", "");
            dr["Profit Loss"] = ds.Tables[0].Compute("SUM([profit loss])", "");
            dr["Booking Income"] = ds.Tables[0].Compute("SUM([booking income])", "");
            dr["Total Discount Amount"] = ds.Tables[0].Compute("SUM([total discount Amount])", "");
            dr["Delivery Income"] = ds.Tables[0].Compute("SUM([delivery income])", "");
            dr["Direct Cost"] = ds.Tables[0].Compute("SUM([direct cost])", "");
            dr["Hamali Cost"] = ds.Tables[0].Compute("SUM([hamali cost])", "");
            dr["Incidental Cost"] = ds.Tables[0].Compute("SUM([incidental cost])", "");
            dr["Local Cartage Cost"] = ds.Tables[0].Compute("SUM([local cartage cost])", "");
            dr["Door Delivery Expense"] = ds.Tables[0].Compute("SUM([door delivery expense])", "");
            dr["BTH Other Charges"] = ds.Tables[0].Compute("SUM([BTH Other Charges])", "");
            ds.Tables[0].Rows.Add(dr);
        
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    protected void ddl_Party_Type_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddl_Party.OtherColumns = ddl_Party_Type.SelectedValue;
        Raj.EC.Common.SetValueToDDLSearch("", "0", ddl_Party);

        if (Util.String2Int(ddl_Party_Type.SelectedValue) == 0)
        {
            ddl_Party.Visible = false;
            lbl_Search.Visible = false;
        }
        else if (Util.String2Int(ddl_Party_Type.SelectedValue) == 1)
        {
            ddl_Party.Visible = true;
            lbl_Search.Visible = true;
        }
        else if (Util.String2Int(ddl_Party_Type.SelectedValue) == 2)
        {
            ddl_Party.Visible = true;
            lbl_Search.Visible = true;
        }
        else if (Util.String2Int(ddl_Party_Type.SelectedValue) == 3)
        {
            ddl_Party.Visible = true;
            lbl_Search.Visible = true;
        }
        dg_Grid.CurrentPageIndex = 0;
        //lbl_Error.Text = "";
        btn_view_Click(sender, e);
    }

}
