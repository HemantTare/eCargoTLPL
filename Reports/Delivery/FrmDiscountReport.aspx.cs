using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Raj.EC;
using System.Text;

//Author : Ankit champaneriya
//Desc   : Booking register Report
//Date   : 03-01-09

public partial class Reports_Delivery_DiscountReport : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;
   
    int TotLR, Tot_Art, SubTotal, TotST, TotRoundOff, TotGCAmt, TotDiscount;
    
    public DateTime From_Date
    {
        get { return Wuc_From_To_Datepicker1.SelectedFromDate; }
        
    }
    public DateTime To_Date
    {
        get { return Wuc_From_To_Datepicker1.SelectedToDate; }
        
    }
    public String Name
    {
        get { return txt_ConsigneeName.Text.Trim(); }
        set { txt_ConsigneeName.Text = value; }
    }
    public int RegionID
    {
        get { return Wuc_Region_Area_Branch1.RegionID ; }
        
    }
    public int AreaID
    {
        get { return Wuc_Region_Area_Branch1.AreaID; }
       
    }
    public int BranchID
    {
        get { return Wuc_Region_Area_Branch1.BranchID; }
        
    }




    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);

        Wuc_Export_To_Excel1.FileName = "MonthWiseDiscountReport";
       

        if (IsPostBack == false)
        {
            
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
        }
            MonthDayWise();
    }
    private string _makePath()
    {

        Common _objCommon = new Common();
        StringBuilder _path = new StringBuilder(_objCommon.getBaseURL());
        
        int MONTHID = 0;
        string MONTHNAME;
        MONTHNAME = "DateRange";

        _path.Append("/Reports/Delivery/FrmDiscountReportMonthWise.aspx");
        _path.Append("?RegionID=" + ClassLibraryMVP.Util.EncryptInteger(RegionID));
        _path.Append("&AreaID=" + ClassLibraryMVP.Util.EncryptInteger(AreaID));
        _path.Append("&BranchID=" + ClassLibraryMVP.Util.EncryptInteger(BranchID));
        _path.Append("&From_Date=" + From_Date.ToString());
        _path.Append("&To_Date=" + To_Date.ToString());
        _path.Append("&StartDate=" + From_Date.ToString());
        _path.Append("&EndDate=" + To_Date.ToString());
        _path.Append("&Name=" + ClassLibraryMVP.Util.EncryptString(Name.ToString()));
        _path.Append("&MONTHID=" + ClassLibraryMVP.Util.EncryptInteger(MONTHID));
        _path.Append("&MONTHNAME="  + ClassLibraryMVP.Util.EncryptString(MONTHNAME.ToString()));
         


        return _path.ToString();
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";

        
            lbl_Error.Text = "";
        
            if (rdbtn_MonthDayWise.SelectedValue == "Month Wise")
            {
                dg_Grid.Visible = true;
                dg_Grid.CurrentPageIndex = 0;
                BindGrid("form", e);
            }
            else
            { 
                int MONTHID = 0;
                string MONTHNAME;
                //MONTHNAME = "DateRange";
                MONTHNAME = From_Date.ToString("dd MMM yyyy") + " To " + To_Date.ToString("dd MMM yyyy");

                lbl_Error.Text = msg;
                dg_Grid.Visible = false;
                //"return viewwindow_general('" + ClassLibraryMVP.Util.EncryptInteger(RegionID) + "','" + ClassLibraryMVP.Util.EncryptInteger(AreaID) + "','" + ClassLibraryMVP.Util.EncryptInteger(BranchID) + "','" + StartDate + "','" + EndDate + "','" + StartDate + "','" + EndDate + "','" + ClassLibraryMVP.Util.EncryptString(Name) + "','" + ClassLibraryMVP.Util.EncryptInteger(MONTHID) + "','" + ClassLibraryMVP.Util.EncryptString(lnk_MONTHNAME.Text) + "')");
                //ClientScript.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", _makePath()));
                ClientScript.RegisterStartupScript(this.GetType(), "newWindow", "viewwindow_general('" + ClassLibraryMVP.Util.EncryptInteger(RegionID) + "','" + ClassLibraryMVP.Util.EncryptInteger(AreaID) + "','" + ClassLibraryMVP.Util.EncryptInteger(BranchID) + "','" + From_Date + "','" + To_Date + "','" + From_Date + "','" + To_Date + "','" + ClassLibraryMVP.Util.EncryptString(Name) + "','" + ClassLibraryMVP.Util.EncryptInteger(MONTHID) + "','" + ClassLibraryMVP.Util.EncryptString(MONTHNAME) + "');", true);
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
        int YearCode = UserManager.getUserParam().YearCode;
        DateTime StartDate = UserManager.getUserParam().StartDate;
        DateTime EndDate = UserManager.getUserParam().EndDate;
        int MainId = UserManager.getUserParam().MainId;

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Region_id", SqlDbType.Int,0,Region_Id),
            objDAL.MakeInParams("@Area_id", SqlDbType.Int,0,Area_id),
            objDAL.MakeInParams("@Branch_id", SqlDbType.Int,0,Branch_id), 
            objDAL.MakeInParams("@FromDate", SqlDbType.DateTime,0,StartDate),
            objDAL.MakeInParams("@ToDate", SqlDbType.DateTime,0,EndDate),
            objDAL.MakeInParams("@StartDate", SqlDbType.DateTime,0,Convert.ToDateTime(StartDate)),
            objDAL.MakeInParams("@EndDate", SqlDbType.DateTime,0,Convert.ToDateTime(EndDate)), 
            objDAL.MakeInParams("@Name", SqlDbType.VarChar,100,txt_ConsigneeName.Text), 
            objDAL.MakeInParams("@MONTHID", SqlDbType.Int,0,0), 
            objDAL.MakeInParams("@CallFrom",SqlDbType.Int,0,1),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize) 
        };

        objDAL.RunProc("EC_Discount_Report_GRD", objSqlParam, ref ds);
         

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
        dr["MONTHNAME"] = "Total";
        
        dr["TotLR"] = TotLR;
        dr["Tot_Art"] = Tot_Art; 
        dr["TotGCAmt"] = TotGCAmt;
        dr["TotDiscount"] = TotDiscount; 
        ds.Tables[0].Rows.Add(dr);

        if (ds.Tables[0].Columns.Contains("MONTHNAME"))
        {
            ds.Tables[0].Columns["MONTHNAME"].ColumnName = "MonthName";
        }
        if (ds.Tables[0].Columns.Contains("TotGCAmt"))
        {
            ds.Tables[0].Columns["TotGCAmt"].ColumnName = "TotalFreight";
        }
        

        ds.Tables[0].Columns.Remove("SRNO");
        ds.Tables[0].Columns.Remove("MONTHID"); 
        ds.Tables[0].Columns.Remove("ClientName");
        ds.Tables[0].Columns.Remove("YearID");
        ds.Tables[0].Columns.Remove("StartDate");
        ds.Tables[0].Columns.Remove("EndDate"); 

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

        TotLR = Util.String2Int(dr["TotalTotLR"].ToString());
        Tot_Art = Util.String2Int(dr["TotalTot_Art"].ToString()); 
        TotGCAmt = Util.String2Int(dr["TotalTotGCAmt"].ToString());
        TotDiscount = Util.String2Int(dr["TotalTotDiscount"].ToString()); 
        
    }
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_TotLR, lbl_Tot_Art, lbl_TotGCAmt, lbl_TotST,  lbl_TotDiscount; 
 
            lbl_TotLR = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotLR");
            lbl_Tot_Art = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_Tot_Art"); 
            lbl_TotGCAmt = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotGCAmt");
            lbl_TotDiscount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotDiscount"); 
            
            lbl_TotLR.Text = TotLR.ToString();
            lbl_Tot_Art.Text = Tot_Art.ToString(); 
            lbl_TotGCAmt.Text = TotGCAmt.ToString();
            lbl_TotDiscount.Text = TotDiscount.ToString(); 
        }
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            int RegionID, AreaID, BranchID, MONTHID;
            HiddenField hdn_MONTHID;
            LinkButton lnk_MONTHNAME;

            RegionID = Wuc_Region_Area_Branch1.RegionID;
            AreaID = Wuc_Region_Area_Branch1.AreaID;
            BranchID = Wuc_Region_Area_Branch1.BranchID;  

            DateTime StartDate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "StartDate").ToString());
            DateTime EndDate = Convert.ToDateTime(DataBinder.Eval(e.Item.DataItem, "EndDate").ToString());
            hdn_MONTHID = (HiddenField)e.Item.FindControl("hdn_MONTHID");
            MONTHID = Convert.ToInt32(hdn_MONTHID.Value);

            lnk_MONTHNAME = (LinkButton)e.Item.FindControl("lnk_MONTHNAME");
            string Name = txt_ConsigneeName.Text.Trim();
            lnk_MONTHNAME.Attributes.Add("onclick", "return viewwindow_general('" + ClassLibraryMVP.Util.EncryptInteger(RegionID) + "','" + ClassLibraryMVP.Util.EncryptInteger(AreaID) + "','" + ClassLibraryMVP.Util.EncryptInteger(BranchID) + "','" + StartDate + "','" + EndDate + "','" + StartDate + "','" + EndDate + "','" + ClassLibraryMVP.Util.EncryptString(Name) + "','" + ClassLibraryMVP.Util.EncryptInteger(MONTHID) + "','" + ClassLibraryMVP.Util.EncryptString(lnk_MONTHNAME.Text) + "')");
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

    protected void rdbtn_MonthDayWise_SelectedIndexChanged(object sender, EventArgs e)
    {
        MonthDayWise();
    }
    public void MonthDayWise()
    {
        if (rdbtn_MonthDayWise.SelectedValue == "Date Wise")
        {
            tbl_DateRange.Visible = true;
            
            dg_Grid.DataSource = null;
            dg_Grid.DataBind();

            //btn_view.Attributes.Add("onclick", "return viewwindow_general('" + _makePath() + "')");
        }
        else
        {
            tbl_DateRange.Visible = false;

            btn_view.Attributes.Remove("onclick"); 
        }
    
    }
}
