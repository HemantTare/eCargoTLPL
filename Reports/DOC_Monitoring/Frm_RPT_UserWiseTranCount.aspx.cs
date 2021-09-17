using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;
using System.Web.UI.WebControls;

public partial class Reports_CL_Nandwana_DOC_Monitoring_Frm_RPT_UserWiseTranCount : System.Web.UI.Page
{
    private DataSet ds;
    private DAL objDAL = new DAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        lbl_Error.Text = "";
        Wuc_Export_To_Excel1.FileName = "User Wise Tran Count";

        if (IsPostBack == false)
        {
            Common objcommon = new Common();
            objcommon.SetStandardCaptionForGrid(dg_Grid);
        }

        Wuc_Export_To_Excel1.btn_export_to_excel_click += new EventHandler(BindGrid);
    }
    protected void btn_view_Click(object sender, EventArgs e)
    {
        BindGrid("form", e);
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

    protected void dg_Grid_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;
        BindGrid("form", e);
    }
    protected void dg_Grid_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
    {

    }
    private void BindGrid(object sender, EventArgs e)
    {
        string CallFrom = (string)(sender);

        SqlParameter[] objSqlParam = {  
                objDAL.MakeInParams("@TranDate", SqlDbType.DateTime,0,WucDatePicker1.SelectedDate)
            };

        objDAL.RunProc("[EC_RPT_UserWiseTranCount]", objSqlParam, ref ds);

        Common objcommon = new Common();
        objcommon.ValidateReportForm(dg_Grid, ds.Tables[0], CallFrom, lbl_Error);

        if (CallFrom == "exporttoexcelusercontrol")
        {
            PrepareDTForExportToExcel();
        }

        dg_Grid.Columns[0].ItemStyle.Width = 100;
    }

    private void PrepareDTForExportToExcel()
    {
        Wuc_Export_To_Excel1.SessionExporttoExcel = ds.Tables[0];
    }
}
