using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_Inward_Frm_Delivery_Branch_ToPay_Summary : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();

    decimal TotalToPayFrt,SummTotalToPayFrt;
    int TotalPKGS, SummTotalPKGS;
    string TotalAUSDate,SummTotalAUSDate,TotalVehicle_No,SummTotalVehicle_No,Particulars;

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.FileName = "DeliveryBranchToPaySummary";
        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid); 
            Wuc_Region_Area_Branch1.SetRegionCaption = "Delivery Region";
            Wuc_Region_Area_Branch1.SetAreaCaption = "Delivery Area";
            Wuc_Region_Area_Branch1.SetBranchCaption = "Delivery Branch"; 

        }
        
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
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
            System.Web.UI.WebControls.Label lbl_TotalAUSDate, lbl_TotalVehicle_No, lbl_TotalPKGS, lbl_TotalToPayFrt;
            
            lbl_TotalAUSDate = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalAUSDate");
            lbl_TotalVehicle_No = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalVehicle_No");
            lbl_TotalPKGS = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalPKGS");
            lbl_TotalToPayFrt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalToPayFrt");

            lbl_TotalAUSDate.Text = TotalAUSDate.ToString();
            //lbl_TotalVehicle_No.Text = TotalVehicle_No.ToString();
            lbl_TotalPKGS.Text = TotalPKGS.ToString();
            lbl_TotalToPayFrt.Text = TotalToPayFrt.ToString(); 
            
            e.Item.ForeColor = System.Drawing.Color.DarkBlue;
            e.Item.BackColor = System.Drawing.Color.LightSkyBlue;
            e.Item.Font.Bold = true; 
        }
        
    }

    protected void dg_Grid_Summary_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_SummTotalAUSDate, lbl_SummTotalVehicle_No, lbl_SummTotalPKGS, lbl_SummTotalToPayFrt;

            lbl_SummTotalAUSDate = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SummTotalAUSDate");
            lbl_SummTotalVehicle_No = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SummTotalVehicle_No");
            lbl_SummTotalPKGS = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SummTotalPKGS");
            lbl_SummTotalToPayFrt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_SummTotalToPayFrt"); 

            lbl_SummTotalAUSDate.Text = SummTotalAUSDate.ToString();
            lbl_SummTotalVehicle_No.Text = SummTotalVehicle_No.ToString();
            lbl_SummTotalPKGS.Text = SummTotalPKGS.ToString();
            lbl_SummTotalToPayFrt.Text = SummTotalToPayFrt.ToString(); 

            e.Item.ForeColor = System.Drawing.Color.DarkBlue;
            e.Item.BackColor = System.Drawing.Color.LightSkyBlue;
            e.Item.Font.Bold = true; 
        }
        //if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Header)
        //{
        //    e.Item.Cells[1].Visible = false;  
        //}
    }
   
    private void calculate_totals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        TotalAUSDate = dr["Particulars"].ToString();
        TotalVehicle_No = dr["TotRecord"].ToString();
        TotalPKGS = Util.String2Int(dr["TotalPKGS"].ToString());
        TotalToPayFrt = Util.String2Decimal(dr["TotalToPayFrt"].ToString()); 

    }
    private void calculate_Summarytotals()
    {
        DataRow dr = ds.Tables[1].Rows[0];
        SummTotalAUSDate = dr["Particulars"].ToString();
        SummTotalVehicle_No = dr["TotRecord"].ToString();
        SummTotalPKGS = Util.String2Int(dr["TotalPKGS"].ToString());
        SummTotalToPayFrt = Util.String2Decimal(dr["TotalToPayFrt"].ToString()); 

    }
   
    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        int Branch_id = Wuc_Region_Area_Branch1.BranchID;
        if (Branch_id == 0)
        {
            msg = "Please Select Atleast One Branch";
            lbl_Error.Text = msg;
            ClearVariables();
        }
        else if ((objDateCommon.Vaildate_Date(Wuc_From_To_Datepicker1.SelectedFromDate, Wuc_From_To_Datepicker1.SelectedToDate, ref msg)) == true)
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

        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate;
        bool Is_Summary = Convert.ToBoolean(Convert.ToInt32(rbtn_SummaryDetails.SelectedValue));  

        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@AUSBranchID", SqlDbType.Int,0,Branch_id),
            objDAL.MakeInParams("@FromDate", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@ToDate", SqlDbType.DateTime,0,To_date), 
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
            objDAL.MakeInParams("@Is_Summary",SqlDbType.Bit,0,Is_Summary)
        };

        objDAL.RunProc("EC_RPT_Delivery_Branch_ToPay_Summary", objSqlParam, ref ds);

       

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
            dr["Actual_Unloading_Sheet_Date"] = "Total :";
            dr["Vehicle_No"] = TotalVehicle_No;
            dr["PKGS"] = TotalPKGS;
            dr["ToPayFrt"] = TotalToPayFrt; 

            ds.Tables[0].Rows.Add(dr);
        }
        else if (Is_Summary == true)
        {
            DataRow dr;
            dr = ds.Tables[0].NewRow();
            dr["Actual_Unloading_Sheet_Date"] = "Total :";
            dr["Vehicle_No"] = SummTotalVehicle_No;
            dr["PKGS"] = SummTotalPKGS;
            dr["ToPayFrt"] = SummTotalToPayFrt;

            ds.Tables[0].Rows.Add(dr);
            if (ds.Tables[0].Columns.Contains("AUS_No") == true)
            {
                ds.Tables[0].Columns.Remove("AUS_No");
            }
            if (ds.Tables[0].Columns.Contains("Vehicle_No") == true)
            {
                ds.Tables[0].Columns.Remove("Vehicle_No");
            }
        }
            Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0]; 
    }
    
    public void ClearVariables()
    {
        ds = null;
        dg_Grid.DataSource = null;
        dg_Grid.DataBind();
        dg_Grid.Visible = false;
        tb_Details.Visible = false; 
        
        dg_Grid_Summary.DataSource = null;
        dg_Grid_Summary.DataBind();
        dg_Grid_Summary.Visible = false;         
        tb_Summary.Visible = false; 
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

            dg_Grid_Summary.Columns[1].Visible = false; 

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

