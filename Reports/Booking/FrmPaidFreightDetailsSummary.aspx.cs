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
using Raj.EC;
public partial class Reports_Booking_FrmPaidFreightDetailsSummary : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    int Region_Id, Area_Id, Branch_Id, Payment_Type_ID, Division_Id;
    DateTime From_Date, To_Date;

    string Cheque_No, Booking_Branch;
    decimal Total_GC, Cash_Amount, Cheque_Amount, GC_Amount, Basic_Amount, Local_Charges, FOV;
    decimal Hamali_Charges, Other_Charges, Bilti_Charges, DD_Charges, Sub_Total, Service_Tax_Amount;
    string TotalRecords;
    #endregion

    #region EventClick
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        
        Get_Parameters();
        Wuc_GC_Parameters1.Set_ddl_bkg_type_visibility = false;
        Wuc_GC_Parameters1.Set_ddl_dly_type_visibility = false;

        if (rbl_Detailed_Summary.SelectedValue == "0")
        {
            Wuc_Export_To_Excel1.FileName = "PaidFreightDetails";
        }
        else
        {
            Wuc_Export_To_Excel1.FileName = "PaidFreightSummary";
        }

        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid_Details);
            objcommon.SetStandardCaptionForGrid(dg_Grid_Summary);
            BindGrid("form_and_pageload", e);
            WucFilter1.setddldatasource(ds);
        }
    }
        
    protected void dg_Grid_Details_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Cheque_No, lbl_Cash_Amount, lbl_Cheque_Amount, lbl_GC_Amount;
            System.Web.UI.WebControls.Label lbl_Basic_Amount, lbl_Local_Charges, lbl_FOV, lbl_Hamali_Charges, lbl_Other_Charges;
            System.Web.UI.WebControls.Label lbl_Bilti_Charges, lbl_DD_Charges, lbl_Sub_Total, lbl_Service_Tax_Amount;

            lbl_Cheque_No = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Cheque_No");
            lbl_Cash_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Cash_Amount");
            lbl_Cheque_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Cheque_Amount");
            lbl_GC_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_GC_Amount");
            lbl_Basic_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Basic_Amount");
            lbl_Local_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Local_Charges");
            lbl_FOV = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_FOV");
            lbl_Hamali_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Hamali_Charges");
            lbl_Other_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Other_Charges");
            lbl_Bilti_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Bilti_Charges");
            lbl_DD_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_DD_Charges");
            lbl_Sub_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Sub_Total");
            lbl_Service_Tax_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Service_Tax_Amount");
            

            lbl_Cheque_No.Text = Cheque_No.ToString();
            lbl_Cash_Amount.Text = Cash_Amount.ToString();
            lbl_Cheque_Amount.Text = Cheque_Amount.ToString();
            lbl_GC_Amount.Text = GC_Amount.ToString();
            lbl_Basic_Amount.Text = Basic_Amount.ToString();
            lbl_Local_Charges.Text = Local_Charges.ToString();
            lbl_FOV.Text = FOV.ToString();
            lbl_Hamali_Charges.Text = Hamali_Charges.ToString();
            lbl_Other_Charges.Text = Other_Charges.ToString();
            lbl_Bilti_Charges.Text = Bilti_Charges.ToString();
            lbl_DD_Charges.Text = DD_Charges.ToString();
            lbl_Sub_Total.Text = Sub_Total.ToString();
            lbl_Service_Tax_Amount.Text = Service_Tax_Amount.ToString();
        }
    }

    protected void dg_Grid_Details_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid_Details.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void dg_Grid_Summary_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Booking_Branch, lbl_Total_GC, lbl_Cash_Amount, lbl_Cheque_Amount, lbl_GC_Amount;
            System.Web.UI.WebControls.Label lbl_Basic_Amount, lbl_Local_Charges, lbl_FOV, lbl_Hamali_Charges, lbl_Other_Charges;
            System.Web.UI.WebControls.Label lbl_Bilti_Charges, lbl_DD_Charges, lbl_Sub_Total, lbl_Service_Tax_Amount;

            lbl_Booking_Branch = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Booking_Branch");
            lbl_Total_GC = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_GC");
            lbl_Cash_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Cash_Amount");
            lbl_Cheque_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Cheque_Amount");
            lbl_GC_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_GC_Amount");
            lbl_Basic_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Basic_Amount");
            lbl_Local_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Local_Charges");
            lbl_FOV = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_FOV");
            lbl_Hamali_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Hamali_Charges");
            lbl_Other_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Other_Charges");
            lbl_Bilti_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Bilti_Charges");
            lbl_DD_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_DD_Charges");
            lbl_Sub_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Sub_Total");
            lbl_Service_Tax_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Service_Tax_Amount");

            lbl_Booking_Branch.Text = Booking_Branch.ToString();
            lbl_Total_GC.Text = Total_GC.ToString();
            lbl_Cash_Amount.Text = Cash_Amount.ToString();
            lbl_Cheque_Amount.Text = Cheque_Amount.ToString();
            lbl_GC_Amount.Text = GC_Amount.ToString();
            lbl_Basic_Amount.Text = Basic_Amount.ToString();
            lbl_Local_Charges.Text = Local_Charges.ToString();
            lbl_FOV.Text = FOV.ToString();
            lbl_Hamali_Charges.Text = Hamali_Charges.ToString();
            lbl_Other_Charges.Text = Other_Charges.ToString();
            lbl_Bilti_Charges.Text = Bilti_Charges.ToString();
            lbl_DD_Charges.Text = DD_Charges.ToString();
            lbl_Sub_Total.Text = Sub_Total.ToString();
            lbl_Service_Tax_Amount.Text = Service_Tax_Amount.ToString();
        }

    }

    protected void dg_Grid_Summary_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid_Summary.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void btn_View_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = "";

            if (rbl_Detailed_Summary.SelectedValue == "0")
            {
                dg_Grid_Details.CurrentPageIndex = 0;
                dg_Grid_Details.Visible = true;
                dg_Grid_Summary.Visible = false;
            }
            else
            {
                dg_Grid_Summary.CurrentPageIndex = 0;
                dg_Grid_Details.Visible = false;
                dg_Grid_Summary.Visible = true;
            }
            BindGrid("form", e);
        }
        else
        {
            lbl_Error.Text = msg;
            dg_Grid_Details.Visible = false;
            dg_Grid_Summary.Visible = false;
        }


    }
    #endregion

    #region Other_Functions

    private void Get_Parameters()
    {
        Region_Id = Wuc_Region_Area_Branch1.RegionID;
        Area_Id = Wuc_Region_Area_Branch1.AreaID;
        Branch_Id = Wuc_Region_Area_Branch1.BranchID;
        Payment_Type_ID = Wuc_GC_Parameters1.Payment_Type_ID;
        From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        To_Date = Wuc_From_To_Datepicker1.SelectedToDate;
        Division_Id = WucDivisions1.Division_ID;
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];

        if (rbl_Detailed_Summary.SelectedValue == "0")
        {
            Cheque_No = "Total";
            
        }
        else
        {
            Booking_Branch = "Total";
            Total_GC = Util.String2Decimal(dr["Total_GC"].ToString());
        }

        Cash_Amount = Util.String2Decimal(dr["Total_Cash_Amount"].ToString());
        Cheque_Amount = Util.String2Decimal(dr["Total_Cheque_Amount"].ToString());
        GC_Amount = Util.String2Decimal(dr["Total_GC_Amount"].ToString());
        Basic_Amount = Util.String2Decimal(dr["Total_Basic_Amount"].ToString());
        Local_Charges = Util.String2Decimal(dr["Total_Local_Charges"].ToString());
        FOV = Util.String2Decimal(dr["Total_FOV"].ToString());
        Hamali_Charges = Util.String2Decimal(dr["Total_Hamali_Charges"].ToString());
        Other_Charges = Util.String2Decimal(dr["Total_Other_Charges"].ToString());
        Bilti_Charges = Util.String2Decimal(dr["Total_Bilti_Charges"].ToString());
        DD_Charges = Util.String2Decimal(dr["Total_DD_Charges"].ToString());
        Sub_Total = Util.String2Decimal(dr["Total_Sub_Total"].ToString());
        Service_Tax_Amount = Util.String2Decimal(dr["Total_Service_Tax_Amount"].ToString());
    }

    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);

        int grid_currentpageindex, grid_PageSize;

        if (rbl_Detailed_Summary.SelectedValue == "0")
        {
            grid_currentpageindex = dg_Grid_Details.CurrentPageIndex;
            grid_PageSize = dg_Grid_Details.PageSize;
        }
        else
        {
            grid_currentpageindex = dg_Grid_Summary.CurrentPageIndex;
            grid_PageSize = dg_Grid_Summary.PageSize;
        }

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        Get_Parameters();

        SqlParameter[] ObjSqlParam = {
            objDAL.MakeInParams("@Region_ID",SqlDbType.Int,4, Region_Id),
            objDAL.MakeInParams("@Area_ID", SqlDbType.Int,4,Area_Id),
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int,4,Branch_Id),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_Date", SqlDbType.DateTime,0,To_Date),
            objDAL.MakeInParams("@Payment_Type_Id",SqlDbType.Int,4,Payment_Type_ID),
            objDAL.MakeInParams("@Division_Id", SqlDbType.Int,4,Division_Id),
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

        if (rbl_Detailed_Summary.SelectedValue == "0")
        {
            objDAL.RunProc("[EC_RPT_Paid_Freight_Details_Grid]", ObjSqlParam, ref ds);
            if (CallFrom == "form_and_pageload") return;
            dg_Grid_Details.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
            TotalRecords = ds.Tables[2].Rows[0][0].ToString();
            //lbl_Error.Visible = true;
            //lbl_Error.Text = "Total " + " " + dg_Grid_Details.VirtualItemCount + " " + "Records Found";
            calculate_totals();
            Common objcommon = new Common();
            objcommon.ValidateReportForm(dg_Grid_Details, ds.Tables[0], CallFrom, lbl_Error,TotalRecords);

        }
        else 
        {
            objDAL.RunProc("[EC_RPT_Paid_Freight_Summary_Grid]", ObjSqlParam, ref ds);
            if (CallFrom == "form_and_pageload") return;
            dg_Grid_Summary.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
            TotalRecords = ds.Tables[2].Rows[0][0].ToString();
            calculate_totals();

            Common objcommon = new Common();
            objcommon.ValidateReportForm(dg_Grid_Summary, ds.Tables[0], CallFrom, lbl_Error,TotalRecords);
        }

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }


    private void PrepareDTForExportToExcel()
    {
        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow dr = ds.Tables[0].NewRow();
            if (rbl_Detailed_Summary.SelectedValue == "0")
            {
                dr["Cheque No"] = Cheque_No;
               // dr["TotalGC"] = TotalGC;
            }
            else
            {
                dr["Booking Branch"] = Booking_Branch;
                dr["Total gc_caption"] = Total_GC;
            }

            dr["Cash Amount"] = Cash_Amount;
            dr["Cheque Amount"] = Cheque_Amount;
            dr["gc_caption Amount"] = GC_Amount;
            dr["Basic Amount"] = Basic_Amount;
            dr["Local Charges"] = Local_Charges;
            dr["FOV"] = FOV;
            dr["Hamali Charges"] = Hamali_Charges;
            dr["Other Charges"] = Other_Charges;
            dr["Bilti Charges"] = Bilti_Charges;
            dr["DD Charges"] = DD_Charges;
            dr["Sub Total"] = Sub_Total;
            dr["Service Tax Amount"] = Service_Tax_Amount;

            ds.Tables[0].Rows.Add(dr);

            Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
            
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
    #endregion
}

