using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;


public partial class Reports_SalesBilling_Frm_Rpt_Master_Client_List : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds,dsCity;
    string TotalRecords;
  
    DAL objDAL = new DAL();
    #endregion

    public bool Is_OnlyContactNos
    {
        set { chk_IsOnlyContactNos.Checked = value; }
        get { return chk_IsOnlyContactNos.Checked; }
    }

    public bool Is_SMSAlert
    {
        set { chk_IsSMSAlert.Checked = value; }
        get { return chk_IsSMSAlert.Checked; }
    }


    #region EventClick

    protected void Page_Load(object sender, EventArgs e)
    {
        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);


        Wuc_Export_To_Excel1.FileName = "Client List";
        
        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            Fill_City();
            BindGrid("form_and_pageload", e); 
        }
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

        ddl_City.Items.Insert(0, new ListItem("All", "0"));
    }


    protected void btn_view_Click(object sender, EventArgs e)
    {
        DateCommon objDateCommon = new DateCommon();
        string msg = "";

            lbl_Error.Text = "";
            dg_Grid.Visible = false;
            dg_GridMobileNos.Visible = false;
            dg_Grid.CurrentPageIndex = 0;
            dg_GridMobileNos.CurrentPageIndex = 0;
            BindGrid("form", e);

    }

    private void BindGrid(object sender, EventArgs e)
    {
        Common objcommon = new Common();
        string CallFrom = (string)(sender);

        if (Convert.ToBoolean(Is_OnlyContactNos.ToString()) == false)
        {
            dg_Grid.Visible = true;
            int grid_currentpageindex = dg_Grid.CurrentPageIndex;
            int grid_PageSize = dg_Grid.PageSize;

            if (CallFrom == "exporttoexcelusercontrol")
            {
                grid_currentpageindex = 0;
                grid_PageSize = 0;
            }

            int City_id = Convert.ToInt32(ddl_City.SelectedItem.Value);

            SqlParameter[] objSqlParam ={ 
                objDAL.MakeInParams("@CityID", SqlDbType.Int,0,City_id),
                objDAL.MakeInParams("@IsSMSAlert", SqlDbType.Bit,0,Convert.ToBoolean(Is_SMSAlert.ToString())),
                objDAL.MakeInParams("@IsOnlyContactNos", SqlDbType.Bit,0,Convert.ToBoolean(Is_OnlyContactNos.ToString())),
                objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
                objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex)};

            objDAL.RunProc("EC_RPT_Master_Client_List", objSqlParam, ref ds);


            if (CallFrom == "form_and_pageload") return;

            dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
            TotalRecords = ds.Tables[1].Rows[0][0].ToString();
            objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);

            if (CallFrom == "exporttoexcelusercontrol")
            {
                PrepareDTForExportToExcel();
            }
        }

        else 
        {

            dg_GridMobileNos.Visible = true;

            int grid_currentpageindex = dg_GridMobileNos.CurrentPageIndex;
            int grid_PageSize = dg_GridMobileNos.PageSize;

            if (CallFrom == "exporttoexcelusercontrol")
            {
                grid_currentpageindex = 0;
                grid_PageSize = 0;
            }

            int City_id = Convert.ToInt32(ddl_City.SelectedItem.Value);

            SqlParameter[] objSqlParam ={ 
                objDAL.MakeInParams("@CityID", SqlDbType.Int,0,City_id),
                objDAL.MakeInParams("@IsSMSAlert", SqlDbType.Bit,0,Convert.ToBoolean(Is_SMSAlert.ToString())),
                objDAL.MakeInParams("@IsOnlyContactNos", SqlDbType.Bit,0,Convert.ToBoolean(Is_OnlyContactNos.ToString())),
                objDAL.MakeInParams("@PageSize",SqlDbType.Int,0,grid_PageSize),
                objDAL.MakeInParams("@PageIndex",SqlDbType.Int,0,grid_currentpageindex)};

            objDAL.RunProc("EC_RPT_Master_Client_List", objSqlParam, ref ds);


            if (CallFrom == "form_and_pageload") return;

            dg_GridMobileNos.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());
            TotalRecords = ds.Tables[1].Rows[0][0].ToString();
            objcommon.ValidateReportForm(dg_GridMobileNos, ds.Tables[0], CallFrom, lbl_Error, TotalRecords);

            if (CallFrom == "exporttoexcelusercontrol")
            {
                PrepareDTForExportToExcel();
            }

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

    protected void dg_GridMobileNos_ItemDataBound(object sender, DataGridItemEventArgs e)
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

    protected void dg_GridMobileNos_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_GridMobileNos.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
}
