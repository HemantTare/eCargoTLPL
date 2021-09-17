using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;


public partial class Reports_Booking_FrmRptLossClientMonthWise : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds,dsMonth,dsYear,dsCity;
    string GC_No_For_Print, TotalRecords;
  
    DAL objDAL = new DAL();
    #endregion

    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);


        Wuc_Export_To_Excel1.FileName = "Loss Client Month Wise";
        
        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            Fill_City();
            Fill_AllDropDown(); 
            Fill_Year();
            BindGrid("form_and_pageload", e); 
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

    private void Fill_Year()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@YearID", SqlDbType.Int,0,0) 
        };

        objDAL.RunProc("EC_Master_Get_Year", objSqlParam, ref dsYear);

        ddl_Year.DataSource = dsYear;
        ddl_Year.DataTextField = "YearName";
        ddl_Year.DataValueField = "YearID";
        ddl_Year.DataBind();
    }

    private void Fill_City()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam ={  
            objDAL.MakeInParams("@CityID", SqlDbType.Int,0,0) 
        };

        objDAL.RunProc("EC_Fill_City", objSqlParam, ref dsCity);

        ddl_City.DataSource = dsCity;
        ddl_City.DataTextField = "City_Name";
        ddl_City.DataValueField = "City_ID";
        ddl_City.DataBind();
    }


    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";

            lbl_Error.Text = "";
            dg_Grid.Visible = true;
            dg_Grid.CurrentPageIndex = 0;
            BindGrid("form", e);

    }

    private void BindGrid(object sender, EventArgs e)
    {
        Common objcommon = new Common();
        string CallFrom= (string)(sender);

        int grid_currentpageindex = dg_Grid.CurrentPageIndex;
        int grid_PageSize = dg_Grid.PageSize;

        if (CallFrom == "exporttoexcelusercontrol")
        {
            grid_currentpageindex = 0;
            grid_PageSize = 0;
        }

        int City_id = Convert.ToInt32(ddl_City.SelectedItem.Value);
        int MonthID = Convert.ToInt32(ddl_Month.SelectedItem.Value);
        int Year = Convert.ToInt32(ddl_Year.SelectedItem.Value); 

        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@Cityid", SqlDbType.Int,0,City_id),
            objDAL.MakeInParams("@MonthID", SqlDbType.Int,0,MonthID),
            objDAL.MakeInParams("@Year", SqlDbType.Int,0,Year),
            objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex),
            objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize)
        };

        objDAL.RunProc("EC_Rpt_Loss_Client_MonthWise", objSqlParam, ref ds);
        

        if (CallFrom == "form_and_pageload") return;

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
        TotalRecords = ds.Tables[1].Rows[0][0].ToString();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }          
    }



    private void PrepareDTForExportToExcel()
    { 
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

    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
            //System.Web.UI.WebControls.Label lbl_ChargedWeight

        }
    }

    protected void dg_Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }

}
