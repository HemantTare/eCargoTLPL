using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_Booking_Frm_Booking_MR_Register : System.Web.UI.Page
{
    private DataSet ds;
    
    decimal Charged_Weight,Gc_Amount, Octroi_Amount,Service_Tax,Demurrage_Charges,
           Octroi_Form_Charge,Octroi_Service_Charge,Detention_Charges;

    int Articles,Demurrage_Days;


    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "BookingMRRegister";

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

    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Total, lbl_Articles, lbl_Charged_Weight,
            lbl_Gc_Amount, lbl_Octroi_Amount, lbl_Service_Tax, lbl_Demurrage_Days, lbl_Demurrage_Charges,
            lbl_Octroi_Form_Charge, lbl_Octroi_Service_Charge, lbl_Detention_Charges;

            lbl_Total = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Total");
            lbl_Articles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Articles");
            lbl_Charged_Weight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Charged_Weight");
            lbl_Gc_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Gc_Amount");
            lbl_Octroi_Amount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Octroi_Amount");
            lbl_Service_Tax = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Service_Tax");
            lbl_Demurrage_Days = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Demurrage_Days");
            lbl_Demurrage_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Demurrage_Charges");
            lbl_Octroi_Form_Charge = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Octroi_Form_Charge");
            lbl_Octroi_Service_Charge = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Octroi_Service_Charge");
            lbl_Detention_Charges = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Detention_Charges");

            lbl_Total.Text = "Total";
            lbl_Articles.Text = Articles.ToString();
            lbl_Charged_Weight.Text=Charged_Weight.ToString();
            lbl_Demurrage_Charges.Text=Demurrage_Charges.ToString();
            lbl_Demurrage_Days.Text=Demurrage_Days.ToString();
            lbl_Detention_Charges.Text=Detention_Charges.ToString();
            lbl_Gc_Amount.Text=Gc_Amount.ToString();
            lbl_Octroi_Amount.Text=Octroi_Amount.ToString();
            lbl_Octroi_Form_Charge.Text=Octroi_Form_Charge.ToString();
            lbl_Octroi_Service_Charge.Text=Octroi_Service_Charge.ToString();
            lbl_Service_Tax.Text=Service_Tax.ToString();
        }
    }

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
       
        Articles = Util.String2Int(dr["Total_Articles"].ToString());
        Charged_Weight = Util.String2Decimal(dr["Total_Charged_Wt"].ToString());
        Demurrage_Days = Util.String2Int(dr["Total_Demurrage_Days"].ToString());
        Detention_Charges = Util.String2Decimal(dr["Total_Detention_Charges"].ToString());
        Demurrage_Charges = Util.String2Decimal(dr["Total_Demurrage_Charges"].ToString());
        Gc_Amount = Util.String2Decimal(dr["Total_GC_Amount"].ToString());
        Octroi_Amount = Util.String2Decimal(dr["Total_Octroi_Amount"].ToString());
        Octroi_Form_Charge = Util.String2Decimal(dr["Total_Octroi_Form_Charge"].ToString());
        Octroi_Service_Charge = Util.String2Decimal(dr["Total_Octroi_Service_Charge"].ToString());
        Service_Tax = Util.String2Decimal(dr["Total_Service_Tax"].ToString());
      
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


        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id),              
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date ", SqlDbType.DateTime,0,To_date),
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

        objDAL.RunProc("[EC_RPT_Booking_MR_Register]", objSqlParam, ref ds);
        if (CallFrom == "form_and_pageload") return;
        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[2].Rows[0][0].ToString();
        calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error,TotalRecords);


        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        dr["gc_caption No"] = "Total";
        dr["Total Articles"] = Articles;
        dr["Charged Wt"]=Charged_Weight;
        dr["Demurrage Days"]=Demurrage_Days;
        dr["Demurrage Charges"]=Demurrage_Charges;
        dr["Detention Charges"]=Detention_Charges;
        dr["gc_caption Amount"] = Gc_Amount;
        dr["Octroi Amount"]=Octroi_Amount;
        dr["Octroi Form Charge"]=Octroi_Form_Charge;
        dr["Octroi Service Charge"]=Octroi_Service_Charge;
        dr["Service Tax"]=Service_Tax;      

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
