using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_Delivery_Frm_Delivery_MR_Register : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();

    int TotalSubTotal, TotalSTax, TotalAmount, TotalTotalSubTotal, TotalTotalSTax, TotalTotalAmount;
    int TotalLR, TotalPKGS, TotalRoundOff, TotRecord, TotalTotalLR, TotalTotalPKGS, TotalTotalRoundOff;

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.FileName = "DeliveryMRRegister";
        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
            lbl_division.Text = CompanyManager.getCompanyParam().DivisionCaption;
            lbl_division.Visible = CompanyManager.getCompanyParam().IsActivateDivision;
            Wuc_Region_Area_Branch1.SetRegionCaption = "Delivery Region";
            Wuc_Region_Area_Branch1.SetAreaCaption = "Delivery Area";
            Wuc_Region_Area_Branch1.SetBranchCaption = "Delivery Branch";
            Fill_AllDropDown();

        }
        
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
    }

    private void Fill_AllDropDown()
    {
        Common objcommon = new Common();
        DataSet ds = new DataSet();
        string Query = "";

        Query = "select 0 as Dly_Pay_Mode_Id,'All' as Dly_Pay_Mode_Name Union select Dly_Pay_Mode_Id, Dly_Pay_Mode_Name from ec_master_delivery_PayMode where IsActive = 1 order by Dly_Pay_Mode_Name";
        ds = objcommon.EC_Common_Pass_Query(Query);
        ddl_Dly_Pay_Mode.DataSource = ds;
        ddl_Dly_Pay_Mode.DataTextField = "Dly_Pay_Mode_Name";
        ddl_Dly_Pay_Mode.DataValueField = "Dly_Pay_Mode_Id";
        ddl_Dly_Pay_Mode.DataBind();

        Query = "select 0 as Payment_Type_Id,'All' as Payment_Type Union select * from ec_master_payment_type order by Payment_Type";
        ds = objcommon.EC_Common_Pass_Query(Query);
        ddl_payment_type.DataSource = ds;
        ddl_payment_type.DataTextField = "Payment_Type";
        ddl_payment_type.DataValueField = "Payment_Type_Id";
        ddl_payment_type.DataBind();
    }

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_PKGS, lbl_LRSubTotal, lbl_STax, lbl_RoundOff, lbl_TotalAmount;
            
            lbl_PKGS = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_PKGS");
            lbl_LRSubTotal = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_LRSubTotal");
            lbl_STax = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_STax");
            lbl_RoundOff = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_RoundOff");
            lbl_TotalAmount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalAmount");

            lbl_PKGS.Text = TotalPKGS.ToString();
            lbl_LRSubTotal.Text = TotalSubTotal.ToString();
            lbl_STax.Text = TotalSTax.ToString();
            lbl_RoundOff.Text = TotalRoundOff.ToString();
            lbl_TotalAmount.Text = TotalAmount.ToString(); 
            
            e.Item.ForeColor = System.Drawing.Color.DarkBlue;
            e.Item.BackColor = System.Drawing.Color.LightSkyBlue;
            e.Item.Font.Bold = true; 
        }
        
    }

    protected void dg_Grid_Summary_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_TotalDlyBranch, lbl_TotalTotRecord, lbl_TotalTotalLR, lbl_TotalTotalPKGS, lbl_TotalTotalSubTotal, lbl_TotalTotalSTax, lbl_TotalTotalRoundOff, lbl_TotalTotalAmount;

            lbl_TotalDlyBranch = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalDlyBranch");
            lbl_TotalTotRecord = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalTotRecord");
            lbl_TotalTotalLR = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalTotalLR");
            lbl_TotalTotalPKGS = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalTotalPKGS");

            lbl_TotalTotalSubTotal = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalTotalSubTotal");
            lbl_TotalTotalSTax = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalTotalSTax");
            lbl_TotalTotalRoundOff = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalTotalRoundOff");
            lbl_TotalTotalAmount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalTotalAmount");

            lbl_TotalTotRecord.Text = TotRecord.ToString();
            lbl_TotalTotalLR.Text = TotalTotalLR.ToString();
            lbl_TotalTotalPKGS.Text = TotalTotalPKGS.ToString();

            lbl_TotalTotalSubTotal.Text = TotalTotalSubTotal.ToString();
            lbl_TotalTotalSTax.Text = TotalTotalSTax.ToString();
            lbl_TotalTotalRoundOff.Text = TotalTotalRoundOff.ToString();
            lbl_TotalTotalAmount.Text = TotalTotalAmount.ToString();

            e.Item.ForeColor = System.Drawing.Color.DarkBlue;
            e.Item.BackColor = System.Drawing.Color.LightSkyBlue;
            e.Item.Font.Bold = true; 
        }
    }
   
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        TotalLR = Util.String2Int(dr["TotalLR"].ToString());
        TotalPKGS = Util.String2Int(dr["TotalPKGS"].ToString());
        TotalSubTotal = Util.String2Int(dr["TotalSubTotal"].ToString());
        TotalSTax = Util.String2Int(dr["TotalSTax"].ToString());
        TotalRoundOff = Util.String2Int(dr["TotalRoundOff"].ToString());
        TotalAmount = Util.String2Int(dr["TotalAmount"].ToString());

    }
    private void calculate_Summarytotals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        TotRecord = Util.String2Int(dr["TotRecord"].ToString());
        TotalTotalLR = Util.String2Int(dr["TotalTotalLR"].ToString());
        TotalTotalPKGS = Util.String2Int(dr["TotalTotalPKGS"].ToString());
        TotalTotalSubTotal = Util.String2Int(dr["TotalTotalSubTotal"].ToString());
        TotalTotalSTax = Util.String2Int(dr["TotalTotalSTax"].ToString());
        TotalTotalRoundOff = Util.String2Int(dr["TotalTotalRoundOff"].ToString());
        TotalTotalAmount = Util.String2Int(dr["TotalTotalAmount"].ToString());

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
            if (rbtn_SummaryDetails.SelectedValue == "1")
            {
                rbtnSummDetlsChange(true, false);
            }
            else if (rbtn_SummaryDetails.SelectedValue == "0")
            {
                rbtnSummDetlsChange(false, true);
            }

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
        int LRPayMode_ID = Convert.ToInt32(ddl_payment_type.SelectedValue);
        int DlyPayMode_ID = Convert.ToInt32(ddl_Dly_Pay_Mode.SelectedValue);

        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        bool Is_Summary = Convert.ToBoolean(Convert.ToInt32(rbtn_SummaryDetails.SelectedValue));  

        int Division_ID = WucDivisions1.Division_ID;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@DlyRegion_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@DlyArea_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@DlyBranch_id", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@Dly_FromDate", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@Dly_ToDate", SqlDbType.DateTime,0,To_date),
            objDAL.MakeInParams("@LRPayMode_ID",SqlDbType.Int,0,LRPayMode_ID),
            objDAL.MakeInParams("@DlyPayMode_ID",SqlDbType.Int,0,DlyPayMode_ID), 
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@Is_Summary",SqlDbType.Bit,0,Is_Summary)
        };

        objDAL.RunProc("EC_RPT_Delivery_MR_Register_OMTuranth", objSqlParam, ref ds);

       

        if (Is_Summary == false)
        {
            dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
            string TotalRecords = ds.Tables[2].Rows[0][0].ToString();
            calculate_totals(); 
            Common objcommon = new Common();
            objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);  
            if (CallFrom == "exporttoexcelusercontrol")
            {
                PrepareDTForExportToExcel(false);
            } 
        }
        else if (Is_Summary == true)
        {
            dg_Grid_Summary.VirtualItemCount = Util.String2Int(ds.Tables[2].Rows[0][0].ToString());
            string TotalRecords = ds.Tables[2].Rows[0][0].ToString();
            calculate_Summarytotals();
            Common objcommon = new Common();
            objcommon.ValidateReportForm(dg_Grid_Summary, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);
            if (CallFrom == "exporttoexcelusercontrol")
            {
                PrepareDTForExportToExcel(true);
            } 
        }
    }

    private void PrepareDTForExportToExcel(bool Is_Summary)
    {
        if (Is_Summary == false)
        {
            DataRow dr;
            dr = ds.Tables[0].NewRow();
            dr["DlyDate"] = "Total";
            dr["LRNo"] = TotalLR;
            dr["PKGS"] = TotalPKGS;
            dr["LRSubTotal"] = TotalSubTotal;
            dr["STax"] = TotalSTax;
            dr["RoundOff"] = TotalRoundOff;
            dr["TotalAmount"] = TotalAmount;

            ds.Tables[0].Rows.Add(dr);
            if (ds.Tables[0].Columns.Contains("SRNO") == true)
            {
                ds.Tables[0].Columns.Remove("SRNO");
            }
        }
        else if (Is_Summary == true)
        {
            DataRow dr;
            dr = ds.Tables[0].NewRow();
            dr["DlyBranch"] = "Total";
            dr["DlyDate"] = TotRecord;
            dr["TotalPKGS"] = TotalTotalPKGS;
            dr["TotalLR"] = TotalTotalLR;
            dr["TotalSubTotal"] = TotalTotalSubTotal;
            dr["TotalSTax"] = TotalTotalSTax;
            dr["TotalRoundOff"] = TotalTotalRoundOff;
            dr["TotalAmount"] = TotalTotalAmount;

            ds.Tables[0].Rows.Add(dr);
        }
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

    protected void rbtn_SummaryDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        //if (rbtn_SummaryDetails.SelectedValue == "1")
        //{
        //    rbtnSummDetlsChange(true, false);        
        //}
        //else if (rbtn_SummaryDetails.SelectedValue == "0")
        //{
        //    rbtnSummDetlsChange(false, true);        
        //}
    }

    private void rbtnSummDetlsChange(bool Summ, bool Dtls)
    {
        if (Summ == true && Dtls == false)
        {
            dg_Grid.DataSource = null;
            dg_Grid.DataBind();
            dg_Grid.Visible = false;
            
            tb_Details.Visible = false;

            dg_Grid_Summary.Visible = true; 
            tb_Summary.Visible = true; 
        }
        else if (Summ == false && Dtls == true)
        {
            dg_Grid_Summary.DataSource = null;
            dg_Grid_Summary.DataBind();
            dg_Grid_Summary.Visible = false;
            
            tb_Details.Visible = true;
            tb_Summary.Visible = false; 

            dg_Grid.Visible = true;
           
        }
    
    }
}

