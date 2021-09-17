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

public partial class Reports_Sales_Billing_FrmWalkInRegular_ClientView : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds; 
    int TotalGC;
    HiddenField hdn_From_Date, hdn_To_Date;



    public DateTime From_Date
    {
        set
        {
            Wuc_From_To_Datepicker1.SelectedFromDate = value;
        }
        get { return Wuc_From_To_Datepicker1.SelectedFromDate; }
    }

    public DateTime To_Date
    {
        set
        {
            Wuc_From_To_Datepicker1.SelectedToDate = value;
        }
        get { return Wuc_From_To_Datepicker1.SelectedToDate; }
    }

#endregion



    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
        Wuc_Export_To_Excel1.FileName = "WalkInNRegularClientDetails"; 
        
        if (IsPostBack == false)
        { 
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);

            BindGrid("form_and_pageload", e);

            From_Date = DateTime.Now;
            To_Date = DateTime.Now;
        }


        StringBuilder Path = new StringBuilder(Util.GetBaseURL());
        Path.Append("/");
        Path.Append("Reports/Sales Billing/FrmWalkInRegularClientLablesViewer.aspx?Menu_Item_Id=" + Util.EncryptInteger(Raj.EC.Common.GetMenuItemId())); 
        btn_Labels.Attributes.Add("onclick", "return Open_Details_Window('" + Path + "','" + From_Date  + "','" + To_Date + "','" + ddl_FilterType.SelectedValue + "','" + txt_SearchFor.Text + "','" + rdl_IsCreatedBy.SelectedValue  + "')");


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
        string CallFrom= (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        
        DateTime From_Date = Wuc_From_To_Datepicker1.SelectedFromDate;
        DateTime To_date = Wuc_From_To_Datepicker1.SelectedToDate; 

        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@IsCreateBy", SqlDbType.Int,0,Convert.ToInt32(rdl_IsCreatedBy.SelectedValue)),
            objDAL.MakeInParams("@From_Date", SqlDbType.DateTime,0,From_Date),
            objDAL.MakeInParams("@To_date", SqlDbType.DateTime,0,To_date), 
            objDAL.MakeInParams("@FilterTypeID",SqlDbType.Int,0,ddl_FilterType.SelectedValue),
            objDAL.MakeInParams("@SearchFor",SqlDbType.VarChar ,25,txt_SearchFor.Text),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)  
        };

        objDAL.RunProc("EC_RPT_WalkIn_Regular_Client_Details", objSqlParam, ref ds); 

        if (CallFrom == "form_and_pageload") return;

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
        string TotalRecords = ds.Tables[1].Rows[0][0].ToString();
        //calculate_totals();

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }          
    }

    private void PrepareDTForExportToExcel()
    {
        //DataRow dr = ds.Tables[0].NewRow();
        //dr["DlyArea"] = "Total";
        //dr["LRCount"] = LRCount;
        //dr["TotalArticles"] = TotalArticles;
        //dr["TotalFreight"] = TotalFreight;
        //dr["LRWiseBusiness"] = LRWiseBusiness;
        //dr["ParcelWiseBusiness"] = ParcelWiseBusiness;
        //dr["FreightWiseBusiness"] = FreightWiseBusiness;

        //ds.Tables[0].Rows.Add(dr);

        //ds.Tables[0].Columns.Remove("SrNo");    
        Wuc_Export_To_Excel1.has_last_row_as_total = false;
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }

     
    //dg_Grid.CurrentPageIndex = e.NewPageIndex;
    //    BindGrid("form", e);
    private void calculate_totals()
    {
        //DataRow dr = ds.Tables[1].Rows[0];
        //LRCount = Util.String2Decimal(dr["TotalLRCount"].ToString());
        //TotalArticles = Util.String2Decimal(dr["TotalTotalArticles"].ToString());
        //TotalFreight = Util.String2Decimal(dr["TotalTotalFreight"].ToString()); 
    }
    #endregion


    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            System.Web.UI.WebControls.Label lbl_LRCount,lbl_TotalArticles, lbl_TotalFreight, lbl_LRWiseBusiness;
            System.Web.UI.WebControls.Label lbl_ParcelWiseBusiness, lbl_FreightWiseBusiness;

            //lbl_LRCount = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_LRCount"); 
            //lbl_TotalArticles = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalArticles");
            //lbl_TotalFreight = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_TotalFreight");
            //lbl_LRWiseBusiness = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_LRWiseBusiness");
            //lbl_ParcelWiseBusiness = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_ParcelWiseBusiness");
            //lbl_FreightWiseBusiness = (System.Web.UI.WebControls.Label)e.Item.FindControl("lbl_FreightWiseBusiness");
            
            //lbl_LRCount.Text = LRCount.ToString(); 
            //lbl_TotalArticles.Text = TotalArticles.ToString();
            //lbl_TotalFreight.Text = TotalFreight.ToString();
            //lbl_LRWiseBusiness.Text = LRWiseBusiness.ToString();
            //lbl_ParcelWiseBusiness.Text = ParcelWiseBusiness.ToString();
            //lbl_FreightWiseBusiness.Text = FreightWiseBusiness.ToString(); 
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

    protected void btn_Labels_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";
        if ((objDateCommon.Vaildate_Date(From_Date, To_Date, ref msg)) == true)
        {
            lbl_Error.Text = "";
        }
        else
        {
            lbl_Error.Text = msg;
        }
    }

    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
}
