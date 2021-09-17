using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.DataAccess;
using System.Data.SqlClient;
using Raj.EC;
using ClassLibraryMVP; 

public partial class Finance_Reports_Frm_Rpt_Vehicle_Profitability_GCwise : System.Web.UI.Page
{
    int LHPO_Id;
    private DataSet ds;
    private DAL objDAL = new DAL();

    protected void Page_Load(object sender, EventArgs e)
    {
        LHPO_Id = Convert.ToInt32(Request.QueryString["LHPO_ID"].ToString());
        Fill_Grid();
    }

    private void Fill_Grid()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@LHPO_Id", SqlDbType.Int, 0, LHPO_Id) };
        objDAL.RunProc("dbo.EC_RPT_Vehicle_Profitability_GCWise", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            dg_Vehicle_Profitability_GCwise.DataSource = ds.Tables[0];
            dg_Vehicle_Profitability_GCwise.DataBind();
          

        }
       
    }
    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Footer)
        {
            Label lbl_Total, lbl_Total_Fright_Income;

            lbl_Total = (Label)e.Item.FindControl("lbl_Total");
            lbl_Total_Fright_Income = (Label)e.Item.FindControl("lbl_Total_Fright_Income");
            lbl_Total.Text = "Total : ";
            lbl_Total_Fright_Income.Text = Convert.ToString(ds.Tables[0].Compute("SUM([Total Freight Income])", ""));
       
        }
    }
}
