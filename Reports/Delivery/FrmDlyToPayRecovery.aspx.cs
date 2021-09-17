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

public partial class Reports_Delivery_FrmDlyToPayRecovery : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds,dsMonth;
    decimal Cash, Cheque, Credit, Discount, Total, TempoFrt, Bonus, TotalTmpFrt;   
   
    DAL objDAL = new DAL();
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "DlyToPayRecovery"; 

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

    public void ValidateReportForm(GridView dg, DataTable dt,string CallFrom, Label lblError)
    {
        if (CallFrom != "exporttoexcelusercontrol")
        {
            dg.DataSource = dt;
            dg.DataBind();
        }

        if (dt.Rows.Count > 0)
        {
            lblError.Text = ""; 
            dg.Visible = true;
        }
        else
        {
            lblError.Text = "No Record(s) Found";
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
        DateCommon objDateCommon = new DateCommon();
        //string msg = "";
        //if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
        //{
            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.PageIndex = 0;
            int Branch_ID = Wuc_Region_Area_Branch1.BranchID;

            if (Branch_ID == 0)
            {
                lbl_Error.Text = "Please Select One Branch";
                return;
            }
            BindGrid("form", e);
        //}
        //else
        //{
        //    lbl_Error.Text = msg;
        //    dg_Grid.Visible = false;
        //}
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
        int Branch_ID = Wuc_Region_Area_Branch1.BranchID;
        int MonthID = Convert.ToInt32(ddl_Month.SelectedItem.Value);


        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Flag", SqlDbType.Int,0,1), 
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int,0,Branch_ID),
            objDAL.MakeInParams("@StartDate", SqlDbType.DateTime,0,UserManager.getUserParam().StartDate),
            objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,UserManager.getUserParam().EndDate), 
            objDAL.MakeInParams("@MonthID", SqlDbType.Int,0,MonthID) 
             
        };



        objDAL.RunProc("EC_RPT_DlyToPayRecovery_GRD", objSqlParam, ref ds);
        //dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[2].Rows[0][0].ToString();
        calculate_totals();

        if (CallFrom == "form_and_pageload") return; 
       

        ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom,lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }          
    }

    private void PrepareDTForExportToExcel()
    {
        DataRow dr = ds.Tables[0].NewRow();
        dr["DlyDate"] = "Total";
        dr["Cash"] = Cash;
        dr["Cheque"] = Cheque;
        dr["Credit"] = Credit;
        dr["Discount"] = Discount;
        dr["Total"] = Total;
        dr["TempoFrt"] = TempoFrt;
        dr["Bonus"] = Bonus;
        dr["TotalTmpFrt"] = TotalTmpFrt;
        ds.Tables[0].Rows.Add(dr); 
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

    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        Cash = Util.String2Decimal(dr["Cash"].ToString());
        Cheque = Util.String2Decimal(dr["Cheque"].ToString());
        Credit = Util.String2Decimal(dr["Credit"].ToString());
        Discount = Util.String2Decimal(dr["Discount"].ToString());
        Total = Util.String2Decimal(dr["Total"].ToString());
        TempoFrt = Util.String2Decimal(dr["TempoFrt"].ToString());
        Bonus = Util.String2Decimal(dr["Bonus"].ToString());
        TotalTmpFrt = Util.String2Decimal(dr["TotalTmpFrt"].ToString());  
    }


    protected void dg_Grid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        LinkButton lbtn_DlyDate;
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_DlyDate, lbl_Cash, lbl_Cheque, lbl_Credit, lbl_Discount, lbl_Total;
            System.Web.UI.WebControls.Label lbl_TempoFrt, lbl_Bonus, lbl_TotalTmpFrt;
 
             
            lbl_DlyDate = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_DlyDate");
            lbl_Cash = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_Cash");
            lbl_Cheque = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_Cheque");
            lbl_Credit = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_Credit");
            lbl_Discount = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_Discount");
            lbl_Total = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_Total");
            lbl_TempoFrt = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_TempoFrt");
            lbl_Bonus = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_Bonus");
            lbl_TotalTmpFrt = (System.Web.UI.WebControls.Label)e.Row.FindControl("lbl_TotalTmpFrt");

             
            lbl_Cash.Text = Cash.ToString();
            lbl_Cheque.Text = Cheque.ToString();
            lbl_Credit.Text = Credit.ToString();
            lbl_Discount.Text = Discount.ToString();
            lbl_Total.Text = Total.ToString();
            lbl_TempoFrt.Text = TempoFrt.ToString();
            lbl_Bonus.Text = Bonus.ToString();
            lbl_TotalTmpFrt.Text = TotalTmpFrt.ToString(); 

        }
        string DlyToPayRecDateWiseUrl;
        if (e.Row.RowIndex != -1)
        {
            int Region_Id = Wuc_Region_Area_Branch1.RegionID;
            int Area_Id = Wuc_Region_Area_Branch1.AreaID;
            int Branch_Id = Wuc_Region_Area_Branch1.BranchID;
            int MonthID = Convert.ToInt32(ddl_Month.SelectedValue);

            string RegionText = Wuc_Region_Area_Branch1.SelectedRegionText;
            string AreaText = Wuc_Region_Area_Branch1.SelectedAreaText;
            string BranchText = Wuc_Region_Area_Branch1.SelectedBranchText;
            string MONTHNAME = ddl_Month.SelectedItem.Text;
 
            int Menu_Item_Id = Raj.EC.Common.GetMenuItemId();
            HiddenField hdfn_DlyDate;
            hdfn_DlyDate = (HiddenField)e.Row.FindControl("hdfn_DlyDate");

            DlyToPayRecDateWiseUrl = ClassLibraryMVP.Util.GetBaseURL() +
                         "/Reports/Delivery/FrmDlyToPayRecoveryDateWise.aspx?Menu_Item_Id=" + Util.EncryptInteger(Menu_Item_Id) +
                         "&Region_Id=" + Util.EncryptInteger(Region_Id) + "&RegionText=" + Util.EncryptString(RegionText)
                         + "&Area_Id=" + Util.EncryptInteger(Area_Id) + "&AreaText=" + Util.EncryptString(AreaText)
                         + "&Branch_Id=" + Util.EncryptInteger(Branch_Id) + "&BranchText=" + Util.EncryptString(BranchText)
                         + "&DlyDate=" + Convert.ToDateTime(hdfn_DlyDate.Value) 
                         + "&MONTHID=" + Util.EncryptInteger(MonthID) + "&MONTHNAME=" + Util.EncryptString(MONTHNAME);

            lbtn_DlyDate = (LinkButton)e.Row.FindControl("lbtn_DlyDate");
            lbtn_DlyDate.Attributes.Add("onclick", "return DlyToPayRecDate('" + DlyToPayRecDateWiseUrl + "');");
        }
    }
}
