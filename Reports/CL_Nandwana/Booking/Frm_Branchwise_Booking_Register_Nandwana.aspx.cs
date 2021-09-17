using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Web.UI.WebControls;

public partial class Reports_CL_Nandwana_Booking_Frm_Branchwise_Booking_Register_Nandwana : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
    decimal ChargedWeight, Actual_Weight, Articles, Freight, Lorry_Hire_Charge,No_of_CN,All_OtherCharges,ServiceTaxAmount,Total_Freight,Local_Charges;
  
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        if (rbl_Detailed_Summary.SelectedValue == "0")
        {
            Wuc_Export_To_Excel1.FileName = "BranchWise Booking Register";
        }
        else
        {
            Wuc_Export_To_Excel1.FileName = " BranchWise Booking Register Summary";
        }
        if (IsPostBack == false)
        {
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            objcommon.SetStandardCaptionForGrid(dg_GridSummary);
        }
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        {
            lbl_Error.Text = "";
            if (rbl_Detailed_Summary.SelectedValue == "0")
            {
               
                dg_Grid.CurrentPageIndex = 0;
                dg_Grid.Visible = true;
                dg_GridSummary.Visible = false;
            }
            
            else
            {
                dg_GridSummary.CurrentPageIndex = 0;
                dg_Grid.Visible = false;
                dg_GridSummary.Visible = true;
            }
            BindGrid("form", e);
        }
        else
        {
            lbl_Error.Text = msg;
            dg_Grid.Visible = false;
            dg_GridSummary.Visible = false;
        }
    }

   protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_ChargedWeight,
                lbl_ActualWeight, lbl_Articles, lbl_Freight, lbl_Lorry_Hire_Charge, lbl_Total,
                lbl_AllOtherCharges, lbl_ServiceTaxAmount, lbl_TotalFreight, lbl_Local_Charges;

            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_ChargedWeight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ChargedWeight");
            lbl_ActualWeight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ActualWeight");
            lbl_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Articles");
            lbl_Freight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Freight");
            lbl_Lorry_Hire_Charge = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Lorry_Hire_Charge");
            lbl_AllOtherCharges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_AllOtherCharges");
            lbl_ServiceTaxAmount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ServiceTaxAmount");
            lbl_TotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalFreight");
            //lbl_Local_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Local_Charges");


            lbl_ChargedWeight.Text = ChargedWeight.ToString();
            lbl_ActualWeight.Text = Actual_Weight.ToString();
            lbl_Articles.Text = Articles.ToString();
            lbl_Freight.Text = Freight.ToString();
            lbl_Lorry_Hire_Charge.Text = Lorry_Hire_Charge.ToString();
            lbl_Total.Text = No_of_CN.ToString();
            lbl_AllOtherCharges.Text = All_OtherCharges.ToString();
            lbl_ServiceTaxAmount.Text = ServiceTaxAmount.ToString();
            lbl_TotalFreight.Text = Total_Freight.ToString();
            //lbl_Local_Charges.Text = Local_Charges.ToString();

        }
    }
    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    protected void dg_GridSummary_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_ChargedWeight,
             lbl_ActualWeight, lbl_Articles,lbl_Total,
            lbl_TotalFreight,lbl_Local_Charges;

            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_ChargedWeight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ChargedWeight");
            lbl_ActualWeight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ActualWeight");
            lbl_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Articles");
            lbl_TotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalFreight");
            //lbl_Local_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Local_Charges");

            lbl_ChargedWeight.Text = ChargedWeight.ToString();
            lbl_ActualWeight.Text = Actual_Weight.ToString();
            lbl_Articles.Text = Articles.ToString();
            lbl_Total.Text = No_of_CN.ToString();
            lbl_TotalFreight.Text = Total_Freight.ToString();
            //lbl_Local_Charges.Text = Local_Charges.ToString();

        }
    }
    protected void dg_GridSummary_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_GridSummary.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        if (rbl_Detailed_Summary.SelectedValue == "0")
        {
            Freight = Util.String2Decimal(dr["Freight"].ToString());
            Lorry_Hire_Charge = Util.String2Decimal(dr["Lorry Hire Charge"].ToString());
            All_OtherCharges = Util.String2Decimal(dr["All Other Charges"].ToString());
            ServiceTaxAmount = Util.String2Decimal(dr["Service Tax Amount"].ToString());
        }
        No_of_CN = Util.String2Decimal(dr["CN No"].ToString());
        ChargedWeight = Util.String2Decimal(dr["Charged Weight"].ToString());
        Actual_Weight = Util.String2Decimal(dr["Actual Weight"].ToString());
        Articles = Util.String2Decimal(dr["Articles"].ToString());      
        Total_Freight = Util.String2Decimal(dr["Total Freight"].ToString());
        //Local_Charges = Util.String2Decimal(dr["Local Charges"].ToString());
    }
    private void BindGrid(object sender, EventArgs e)
    {
        DAL objDAL = new DAL();
        string CallFrom = (string)(sender);
        int grid_currentpageindex, grid_PageSize;
        Common objcommon = new Common();

        if (rbl_Detailed_Summary.SelectedValue == "0")
        {
             grid_currentpageindex = dg_Grid.CurrentPageIndex;
             grid_PageSize = dg_Grid.PageSize;
             dg_GridSummary.Visible = false;
        }
        else
        {
             grid_currentpageindex = dg_GridSummary.CurrentPageIndex;
             grid_PageSize = dg_GridSummary.PageSize;
             dg_Grid.Visible = false;
        }
       

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
        string Client_name = Txt_Client.Text;
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
            //objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            //objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@Client_Name",SqlDbType.VarChar,100,Client_name),
            objDAL.MakeInParams("@IsSummary",SqlDbType.Int,0,rbl_Detailed_Summary.SelectedValue)

        };
        objDAL.RunProc("[EC_RPT_Branchwise_Booking_Register_Nandwana_GRD]", objSqlParam, ref ds);

        if (rbl_Detailed_Summary.SelectedValue == "0")
        {
            dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
            calculate_totals();
            objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

        }
        else
        {
            dg_GridSummary.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
            calculate_totals();
            objcommon.ValidateReportForm(dg_GridSummary, ds.Tables[0], CallFrom, lbl_Error);

        }

        //calculate_totals();

        

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        
        if (rbl_Detailed_Summary.SelectedValue == "0")
        {
            dr["Freight"] = Freight;
            dr["All Other Charges"] = All_OtherCharges;
            dr["Service Tax Amount"] = ServiceTaxAmount;
            dr["Lorry Hire Charge"] = Lorry_Hire_Charge;

        }
        dr["CN No"] = "Total";
        dr["CN No"] = No_of_CN;
        dr["Charged Weight"] = ChargedWeight;
        dr["Actual Weight"] = Actual_Weight;
        dr["Articles"] = Articles;       
        dr["Total Freight"] = Total_Freight;
        //dr["Local Charges"] = Local_Charges;
   
        ds.Tables[0].Rows.Add(dr);

        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
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
    protected void rbl_Detailed_Summary_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbl_Error.Text = "";
        if (rbl_Detailed_Summary.SelectedValue == "0")
        {

            dg_Grid.CurrentPageIndex = 0;
            dg_Grid.Visible = true;
            dg_GridSummary.Visible = false;
        }

        else
        {
            dg_GridSummary.CurrentPageIndex = 0;
            dg_Grid.Visible = false;
            dg_GridSummary.Visible = true;
        }
        BindGrid("form", e);
    }
}
