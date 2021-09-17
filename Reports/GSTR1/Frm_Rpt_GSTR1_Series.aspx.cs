using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;


public partial class Reports_GSTR1_Frm_Rpt_Series : System.Web.UI.Page
{
    #region Declaration
    private DataSet ds;

    public DateTime FromDate
    {
        set
        {
            Wuc_From_To_Datepicker1.SelectedFromDate = value;
        }
        get
        {
            return Wuc_From_To_Datepicker1.SelectedFromDate;
        }
    }

    public DateTime ToDate
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

        Wuc_Export_To_Excel1.FileName = "GSTR1-Document Series";

        if (IsPostBack == false)
        {
            Fill_State();
        }

    }

    private void Fill_State()
    {
        DAL objDAL = new DAL();

        DataSet dsState = new DataSet();

        objDAL.RunProc("EC_RPT_GSTR1_Fill_State", ref dsState);

        ddl_State.DataSource = dsState;
        ddl_State.DataTextField = "State_Name";
        ddl_State.DataValueField = "State_ID";
        ddl_State.DataBind();
    }

    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == System.Web.UI.WebControls.ListItemType.Footer)
        {
        }

        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {

        }
    }



    #endregion

    #region Other Function

    protected void btn_view_Click(object sender, EventArgs e)
    {

            dg_Grid.CurrentPageIndex =0;
            lbl_Error.Text = "";
            BindGrid("form", e);
       
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


        
        SqlParameter[] objSqlParam ={ 
            objDAL.MakeInParams("@StateID", SqlDbType.Int, 0,ddl_State.SelectedValue),
            objDAL.MakeInParams("@FromDate",SqlDbType.DateTime,0,FromDate),
            objDAL.MakeInParams("@ToDate",SqlDbType.DateTime,0,ToDate)
        };

        objDAL.RunProc("EC_RPT_GSTR1_Series_StateWise", objSqlParam, ref ds);

        dg_Grid.VirtualItemCount = Util.String2Int(ds.Tables[1].Rows[0][0].ToString());


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

    private void PrepareDTForExportToExcel()
    {
 
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
    #endregion


}
