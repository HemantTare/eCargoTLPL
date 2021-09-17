using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

//Author : Ankit champaneriya
//Desc   : Booking register Report
//Date   : 03-01-09

public partial class Finance_Reports_FrmFrenchiseeMonthlyStatement : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds,dsMonth;
    decimal ToPayFrt, PaidFrt, TBBFrt, FrtDiscount;
    string TotalRecords;
    DAL objDAL = new DAL();
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid); 
        Wuc_Export_To_Excel1.FileName = "FrenchiseeMonthlyStatement"; 

        if (IsPostBack == false)
        {  
             
            SetStandardCaptionForGrid(dg_Grid);
            Fill_AllDropDown();
            BindGrid("form_and_pageload", e); 
        }
    }
    public void SetStandardCaptionForGrid(GridView dg_Grid)
    {
        string col_header = "";

        for (int i = 0; i <= dg_Grid.Columns.Count - 1; i++)
        {
            col_header = dg_Grid.Columns[i].HeaderText;

            if (col_header.ToLower().Contains("gc_caption"))
            {
                dg_Grid.Columns[i].HeaderText = col_header.Replace("gc_caption", CompanyManager.getCompanyParam().GcCaption);
            }
            else if (col_header.ToLower().Contains("lhpo_caption"))
            {
                dg_Grid.Columns[i].HeaderText = col_header.Replace("lhpo_caption", CompanyManager.getCompanyParam().LHPOCaption);
            }
        }
    }

    public void ValidateReportForm(GridView dg, DataTable dt,string CallFrom, Label lbl_Error)
    {
        if (CallFrom != "exporttoexcelusercontrol")
        {
            dg.DataSource = dt;
            dg.DataBind();
        }

        if (dt.Rows.Count > 0)
        {
            lbl_Error.Text = ""; 
            dg.Visible = true;
        }
        else
        {
            lbl_Error.Text = "No Record(s) Found";
            dg.Visible = false;
        }
    }

    private void Fill_AllDropDown()
    {
        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@MonthID", SqlDbType.Int,0,0) 
        };

        objDAL.RunProc("EC_Master_Get_Finacial_Month", objSqlParam, ref dsMonth);

        ddl_Month.DataSource = dsMonth;
        ddl_Month.DataTextField = "MonthName";
        ddl_Month.DataValueField = "MonthID";
        ddl_Month.DataBind();
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        if (Branch_id <= 0)
        {
            dg_Grid.DataSource = null;
            dg_Grid.DataBind();
            dg_Grid.Visible = false;
            dg_Grid.PageIndex = 0;
            lbl_Error.Text = "Please Select One Branch";
        }
        else
        {
            DateCommon objDateCommon = new DateCommon();
            string msg = "";
            BindGrid("form", e);
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.PageIndex = 0;
        }
       
    }

    private void BindGrid(object sender, EventArgs e)
    {
       
        string CallFrom= (string)(sender);

        int grid_currentpageindex = dg_Grid.PageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        int Region_Id = Wuc_Region_Area_Branch1.RegionID;
        int Area_id = Wuc_Region_Area_Branch1.AreaID;
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        int MonthID = Convert.ToInt32(ddl_Month.SelectedItem.Value); 
        Boolean IsBookingBranchWise;
 

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id), 
            objDAL.MakeInParams("@MonthID", SqlDbType.Int,0,MonthID),
            objDAL.MakeInParams("@StartDate", SqlDbType.DateTime,0,UserManager.getUserParam().StartDate),
            objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,UserManager.getUserParam().EndDate), 
            objDAL.MakeInParams("@Memo_ID",SqlDbType.Int,0,0) 
             
        }; 
        objDAL.RunProc("EC_RPT_FrenchiseeMonthlyStatement_GRD", objSqlParam, ref ds);

        if (CallFrom == "form_and_pageload") return;  

        //dg_Grid = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
        TotalRecords = ds.Tables[2].Rows[0][0].ToString();
        calculate_totals();

        ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom,lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }
    }
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];

        ToPayFrt = Util.String2Decimal(dr["TotalToPayFrt"].ToString());
        PaidFrt = Util.String2Decimal(dr["TotalPaidFrt"].ToString());
        TBBFrt = Util.String2Decimal(dr["TotalTBBFrt"].ToString());
        FrtDiscount = Util.String2Decimal(dr["TotalFrtDiscount"].ToString()); 
    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        dr["InvNo"] = "Total";  
        dr["ToPayFrt"] = ToPayFrt; 
        dr["PaidFrt"] = PaidFrt;
        dr["TBBFrt"] = TBBFrt;
        dr["FrtDiscount"] = FrtDiscount;

        ds.Tables[0].Rows.Add(dr); 

        ds.Tables[0].Columns.Remove("Memo_ID"); 
        
        
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    } 
    
    #endregion 

    public void ClearVariables()
    {
        ds = null;
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

    
    
    protected void dg_Grid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        dg_Grid.PageIndex = e.NewPageIndex; 
        BindGrid("form", e); 
    }
   
    protected void dg_Grid_RowDataBound(object sender, GridViewRowEventArgs e)
    { 
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HiddenField hdn_Memo_ID;
            hdn_Memo_ID = (HiddenField)e.Row.FindControl("hdn_Memo_ID");

            string MemoNo = e.Row.Cells[1].ToString();  
            LinkButton lnk_FrtDiscount;
            lnk_FrtDiscount = (LinkButton)e.Row.FindControl("lnk_FrtDiscount");
            lnk_FrtDiscount.Attributes.Add("onclick", "return viewwindow_general('" + ClassLibraryMVP.Util.EncryptString(MemoNo) + "','" + ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(hdn_Memo_ID.Value)) + "','" + ClassLibraryMVP.Util.EncryptString(lnk_FrtDiscount.Text) + "')");
            
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_Tot_ToPayFrt, lbl_Tot_PaidFrt, lbl_Tot_TBBFrt, lbl_Tot_FrtDiscount;

            lbl_Tot_ToPayFrt = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_Tot_ToPayFrt");
            lbl_Tot_PaidFrt = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_Tot_PaidFrt");
            lbl_Tot_TBBFrt = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_Tot_TBBFrt");
            lbl_Tot_FrtDiscount = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_Tot_FrtDiscount");

            lbl_Tot_ToPayFrt.Text = ToPayFrt.ToString();
            lbl_Tot_PaidFrt.Text = PaidFrt.ToString();
            lbl_Tot_TBBFrt.Text = TBBFrt.ToString();
            lbl_Tot_FrtDiscount.Text = FrtDiscount.ToString(); 
        
        }
    }
}
