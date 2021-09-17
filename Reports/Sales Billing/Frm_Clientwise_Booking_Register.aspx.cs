using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_Sales_Billing_Frm_Clientwise_Booking_Register : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();

  

    decimal Charged_Weight, Gc_Amount, Octroi_Amount, Service_Tax, Demurrage_Charges, MR_Amount,
           Octroi_Form_Charge, Octroi_Service_Charge, Detention_Charges,Fov_Charges,Other_Total_Charges,
           Freight_Amt, Invoice_Value, Per_KG_Weight, Actual_Weight,Sub_Total;
            
    int Articles,DDC_Articles;
    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.FileName = "ClientwiseBookingRegister";

        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;

        }
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
    }
    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Total, lbl_Articles, lbl_Charged_Weight, lbl_Freight_Amt,
            lbl_Gc_Amount, lbl_Sub_Total, lbl_Service_Tax, lbl_Other_Total_Charges,
            lbl_Fov_Charges,    
            lbl_Invoice_Value, lbl_Per_KG_Weight, lbl_Actual_Weight, lbl_DDC_Articles;


            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Articles");
            lbl_Charged_Weight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Charged_Weight");
            lbl_Gc_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Gc_Amount");            
            lbl_Service_Tax = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Service_Tax");        
            lbl_Fov_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Fov");
            lbl_Invoice_Value = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_Invoice_Value");
            lbl_Per_KG_Weight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Per_KG_Weight");
            lbl_Actual_Weight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_Actual_Weight");
            lbl_DDC_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total_DDC_Articles");
            lbl_Other_Total_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Other_Total_Charges");
            lbl_Freight_Amt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Freight_Amt");
            lbl_Sub_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Sub_Total");


            lbl_Total.Text = "Total";
            lbl_Articles.Text = Articles.ToString();
            lbl_Charged_Weight.Text = Charged_Weight.ToString();          
            lbl_Gc_Amount.Text = Gc_Amount.ToString();         
            lbl_Service_Tax.Text = Service_Tax.ToString();        
            lbl_Fov_Charges.Text=Fov_Charges.ToString();
            lbl_Other_Total_Charges.Text=Other_Total_Charges.ToString();
            lbl_Freight_Amt.Text=Freight_Amt.ToString();
            lbl_Invoice_Value.Text=Invoice_Value.ToString();
            lbl_Per_KG_Weight.Text=Per_KG_Weight.ToString(); 
            lbl_Actual_Weight.Text=Actual_Weight.ToString();
            lbl_DDC_Articles.Text=DDC_Articles.ToString();
            lbl_Sub_Total.Text = Sub_Total.ToString();

        }
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Articles = Util.String2Int(dr["Total Articles"].ToString());
        Charged_Weight = Util.String2Decimal(dr["Charged Weight"].ToString());
        Gc_Amount = Util.String2Decimal(dr["gc_caption Amount"].ToString());    
        Service_Tax = Util.String2Decimal(dr["Service Tax Amount"].ToString());
        Fov_Charges = Util.String2Decimal(dr["FOV Charges"].ToString());
        Other_Total_Charges=Util.String2Decimal(dr["Other Total Charges"].ToString());
        Freight_Amt=Util.String2Decimal(dr["Freight Amt"].ToString());
        Invoice_Value=Util.String2Decimal(dr["Total Invoice Value"].ToString());
        Per_KG_Weight=Util.String2Decimal(dr["Per KG Weight"].ToString());
        Actual_Weight=Util.String2Decimal(dr["Total Actual Weight"].ToString());
        DDC_Articles = Util.String2Int(dr["Total DDC Articles"].ToString());
        Sub_Total = Util.String2Decimal(dr["Sub Total"].ToString());

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
        string Cnr_name = Txt_Consignor_name.Text;

        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),              
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@Consignor_Name",SqlDbType.VarChar,100,Cnr_name),
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,Division_ID),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("[EC_RPT_Clientwise_Booking_Register]", objSqlParam, ref ds);

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());

        calculate_totals();
        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }       
    }
    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
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
            dg_Grid.Visible =false;
        }
    }
   private void PrepareDTForExportToExcel()
    {

        DataRow dr;
        dr = ds.Tables[0].NewRow();
        dr["gc_caption No"] = "Total";
        dr["Total Articles"] = Articles;
        dr["Charged Weight"] = Charged_Weight;
        dr["gc_caption Amount"] = Gc_Amount;
        dr["Service Tax Amount"] = Service_Tax;
        dr["FOV Charges"] = Fov_Charges;
        dr["Other Total Charges"] = Other_Total_Charges;
        dr["Freight Amt"] = Freight_Amt;
        dr["Total Invoice Value"] = Invoice_Value;
        dr["Per KG Weight"] = Per_KG_Weight;
        dr["Total Actual Weight"] = Actual_Weight;
        dr["Per KG Weight"] = Per_KG_Weight;
        dr["Total DDC Articles"] = DDC_Articles;
        dr["Sub Total"] = Sub_Total;

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
}
