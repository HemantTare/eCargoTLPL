using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_CL_Nandwana_Booking_FrmBranchWiseBookingRegisterStatusView : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal ChargedWeight, Actual_Weight, Articles, Basic_Freight, FOV_Charges;
    decimal ODA_Charges, Other_Charges, Sub_Freight, STax_Amt, Total_Freight, Invoice_Value;
    decimal Hamali_Charge, DD_Charge, Bilti_Charges, Local_Charges;
    int TotalGC;
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        if (rdl_BookingRegister.SelectedValue == "0")
            Wuc_Export_To_Excel1.FileName = "BranchWiseBookingRegister";
        else
            Wuc_Export_To_Excel1.FileName = "DeliveryBranchWiseBookingRegister";


        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);

            BindGrid("form_and_pageload", e);
            WucFilter1.setddldatasource(ds);
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
        string CallFrom = (string)(sender);

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
        int BookingTypeId = Wuc_GC_Parameters1.Booking_Type_ID;
        int DeliveryTypeId = Wuc_GC_Parameters1.Delivery_Type_ID;
        int PaymentTypeId = Wuc_GC_Parameters1.Payment_Type_ID;
        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@Booking_Type_Id", SqlDbType.Int,0,BookingTypeId),
            objDAL.MakeInParams("@Delivery_Type_Id", SqlDbType.Int,0,DeliveryTypeId),
            objDAL.MakeInParams("@Payment_Type_Id",SqlDbType.Int,0,PaymentTypeId),
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),

            objDAL.MakeInParams("@calledfrom",SqlDbType.VarChar,20,CallFrom),
            objDAL.MakeInParams("@colid",SqlDbType.Int,0,WucFilter1.colid),
            objDAL.MakeInParams("@datatype_id",SqlDbType.Int,0,WucFilter1.Datatype_ID),
            objDAL.MakeInParams("@criteria_id",SqlDbType.Int,0,WucFilter1.criteriaid),
            objDAL.MakeInParams("@Filtered_Text",SqlDbType.VarChar,50,WucFilter1.Filtered_Text),
            objDAL.MakeInParams("@Filtered_Date",SqlDbType.DateTime,0,WucFilter1.Filtered_Date),
            objDAL.MakeInParams("@Filtered_Bit",SqlDbType.Bit,0,WucFilter1.Filtered_bit)
        };
        if (rdl_BookingRegister.SelectedValue == "0")
        {
            objDAL.RunProc("[EC_RPT_Branchwise_Booking_Register_GRD_STATUS]", objSqlParam, ref ds);
        }
        else
        {
            objDAL.RunProc("[EC_RPT_Del_Branchwise_Booking_Register_GRD_STATUS]", objSqlParam, ref ds);
        }

        if (CallFrom == "form_and_pageload") return;

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[2].Rows[0][0].ToString();
        calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        dr["Pay Mode"] = "Total";
        dr["Charged Weight"] = ChargedWeight;
        dr["Actual Weight"] = Actual_Weight;
        dr["Articles"] = Articles;
        dr["Basic Freight"] = Basic_Freight;
        dr["FOV Charges"] = FOV_Charges;
        dr["ODA Charges"] = ODA_Charges;
        dr["Other Charges"] = Other_Charges;
        dr["Sub Freight"] = Sub_Freight;
        dr["STax Amt"] = STax_Amt;
        dr["Total Freight"] = Total_Freight;
        dr["Invoice Value"] = Invoice_Value;
        dr["Hamali Charge"] = Hamali_Charge;
        dr["DD Charge"] = DD_Charge;
        dr["Bilti Charges"] = Bilti_Charges;
        dr["Local Charges"] = Local_Charges;
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
        ChargedWeight = Util.String2Decimal(dr["Total_Charged_Wt"].ToString());
        Actual_Weight = Util.String2Decimal(dr["Total_Actual_Wt"].ToString());
        Articles = Util.String2Decimal(dr["Total_Articles"].ToString());
        Basic_Freight = Util.String2Decimal(dr["Total_Basic_Freight"].ToString());
        FOV_Charges = Util.String2Decimal(dr["Total_Fov_Charge"].ToString());
        ODA_Charges = Util.String2Decimal(dr["Total_ODA_Charge"].ToString());
        Other_Charges = Util.String2Decimal(dr["Total_Other_Charge"].ToString());
        Sub_Freight = Util.String2Decimal(dr["Total_Sub_Freight"].ToString());
        STax_Amt = Util.String2Decimal(dr["Total_Service_Tax"].ToString());
        Total_Freight = Util.String2Decimal(dr["Total_Freight"].ToString());
        Invoice_Value = Util.String2Decimal(dr["Total_Invoice_Value"].ToString());
        Hamali_Charge = Util.String2Decimal(dr["Total_Hamali_Charge"].ToString());
        DD_Charge = Util.String2Decimal(dr["Total_DD_Charge"].ToString());
        Bilti_Charges = Util.String2Decimal(dr["Total_Bilti_Charges"].ToString());
        Local_Charges = Util.String2Decimal(dr["Local_Charges"].ToString());

    }
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_ChargedWeight, lbl_ActualWeight, lbl_Articles, lbl_BasicFreight;
            System.Web.UI.WebControls.Label lbl_FOVCharges, lbl_ODACharges, lbl_OtherCharges, lbl_SubFreight, lbl_STaxAmt;
            System.Web.UI.WebControls.Label lbl_TotalFreight, lbl_InvoiceValue, lbl_HamaliCharge, lbl_DDCharge, lbl_BiltiCharges, lbl_Local_Charges;

            lbl_ChargedWeight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ChargedWeight");
            lbl_ActualWeight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ActualWeight");
            lbl_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Articles");
            lbl_BasicFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_BasicFreight");
            lbl_FOVCharges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_FOVCharges");
            lbl_ODACharges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ODACharges");
            lbl_OtherCharges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_OtherCharges");
            lbl_SubFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SubFreight");
            lbl_STaxAmt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_STaxAmt");
            lbl_TotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalFreight");
            lbl_InvoiceValue = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_InvoiceValue");
            lbl_HamaliCharge = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_HamaliCharge");
            lbl_DDCharge = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_DDCharge");
            lbl_BiltiCharges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_BiltiCharges");
            lbl_Local_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Local_Charges");

            lbl_ChargedWeight.Text = ChargedWeight.ToString();
            lbl_ActualWeight.Text = Actual_Weight.ToString();
            lbl_Articles.Text = Articles.ToString();
            lbl_BasicFreight.Text = Basic_Freight.ToString();
            lbl_FOVCharges.Text = FOV_Charges.ToString();
            lbl_ODACharges.Text = ODA_Charges.ToString();
            lbl_OtherCharges.Text = Other_Charges.ToString();
            lbl_SubFreight.Text = Sub_Freight.ToString();
            lbl_STaxAmt.Text = STax_Amt.ToString();
            lbl_TotalFreight.Text = Total_Freight.ToString();
            lbl_InvoiceValue.Text = Invoice_Value.ToString();
            lbl_HamaliCharge.Text = Hamali_Charge.ToString();
            lbl_DDCharge.Text = DD_Charge.ToString();
            lbl_BiltiCharges.Text = Bilti_Charges.ToString();
            lbl_Local_Charges.Text = Local_Charges.ToString();
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
