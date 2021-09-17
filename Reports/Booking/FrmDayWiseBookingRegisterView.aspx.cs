using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

//Author : Ankit champaneriya
//Desc   : Booking register Report
//Date   : 03-01-09

public partial class Reports_Booking_FrmDayWiseBookingRegisterView : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    decimal Articles, Sub_Total, Basic_Freight, STax_Amt, Total_Freight, GC_Amount, Discount;
    int NoOf_GC;
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        //if (rdl_BookingRegister.SelectedValue == "0")
        //    Wuc_Export_To_Excel1.FileName = "BranchWiseBookingRegister";
        //else
        //    Wuc_Export_To_Excel1.FileName = "DeliveryBranchWiseBookingRegister";

        Wuc_Export_To_Excel1.FileName = "DateWiseBookingRegister";

        if (IsPostBack == false)
        {
            //lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            //lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);

            BindGrid("form_and_pageload", e);
            //WucFilter1.setddldatasource(ds);
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

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom= (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        //int BookingTypeId = Wuc_GC_Parameters1.Booking_Type_ID;
        //int DeliveryTypeId = Wuc_GC_Parameters1.Delivery_Type_ID;
        //int PaymentTypeId = Wuc_GC_Parameters1.Payment_Type_ID;
        //int Division_ID = WucDivisions1.Division_ID;

        bool Is_BookingRpt = Convert.ToBoolean(rbtn_Is_BookingRpt.SelectedValue);   
 
        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@Is_BookingRpt",SqlDbType.Bit,0,Is_BookingRpt) 
        };


        //if (rdl_BookingRegister.SelectedValue == "0")
        //{
        objDAL.RunProc("[EC_RPT_DayWise_Booking_Register_GRD]", objSqlParam, ref ds);
        //}
        //else
        //{
        //    objDAL.RunProc("[EC_RPT_Del_Branchwise_Booking_Register_GRD]", objSqlParam, ref ds);
        //}

        if (CallFrom == "form_and_pageload") return;

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[2].Rows[0][0].ToString();
        calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom,lbl_Error,TotalRecords);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }          
    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        dr["GC_date"] = "Total";
        dr["NoOf_GC"] = NoOf_GC; 
        dr["Articles"] = Articles;
        dr["Basic Freight"] = Basic_Freight;
        dr["Sub Total"] = Sub_Total; 
        dr["STax Amt"] = STax_Amt;
        dr["Total GC Amount"] = GC_Amount;
        dr["Discount"] = Discount; 
        ds.Tables[0].Rows.Add(dr); 

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        NoOf_GC = Util.String2Int(dr["Total_NoOf_GC"].ToString()); 
        Articles = Util.String2Decimal(dr["Total_Articles"].ToString());
        Basic_Freight = Util.String2Decimal(dr["Total_Basic_Freight"].ToString());
        Sub_Total = Util.String2Decimal(dr["Total_Sub_Total"].ToString());
        STax_Amt = Util.String2Decimal(dr["Total_STax"].ToString());
        GC_Amount = Util.String2Decimal(dr["Total_GC_Amount"].ToString());
        Discount = Util.String2Decimal(dr["Total_Discount"].ToString()); 
        
    }
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_GC_date, lbl_NoOf_GC, lbl_Articles, lbl_BasicFreight;
            System.Web.UI.WebControls.Label lbl_SubTotal, lbl_STaxAmt, lbl_TotalGCAmount, lbl_Discount; 


            lbl_NoOf_GC = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_NoOf_GC");
            lbl_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Articles");
            lbl_BasicFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_BasicFreight");
            lbl_SubTotal = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SubTotal");
            lbl_STaxAmt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_STaxAmt");
            lbl_TotalGCAmount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalGCAmount");
            lbl_Discount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Discount");

            lbl_NoOf_GC.Text = NoOf_GC.ToString();
            lbl_Articles.Text = Articles.ToString();
            lbl_BasicFreight.Text = Basic_Freight.ToString();
            lbl_SubTotal.Text = Sub_Total.ToString();
            lbl_STaxAmt.Text = STax_Amt.ToString();
            lbl_TotalGCAmount.Text = GC_Amount.ToString();
            lbl_Discount.Text = Discount.ToString();
             
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

}
