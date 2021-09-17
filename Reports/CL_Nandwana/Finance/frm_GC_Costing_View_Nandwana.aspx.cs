using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC;

public partial class Reports_Finance_frm_GC_Costing_View_Nandwana : System.Web.UI.Page
{
    int GC_ID;
    DAL objDAL = new DAL();
    DataSet ds;

    protected void Page_Load(object sender, EventArgs e)
    {
        GC_ID = Convert.ToInt32(Request.QueryString["GC_Id"]);

        if (IsPostBack == false)
        {
            Get_DS();
            BindRepeaters();
            Bind_Grid();
        }
    }

    private void Get_DS()
    {
        SqlParameter[] objSqlParam = 
                { 
                    objDAL.MakeInParams("@GC_ID", SqlDbType.Int, 0, GC_ID)
                };

        objDAL.RunProc("[EC_RPT_GC_Costing_Details_Nandwana]", objSqlParam, ref ds);
    }

    private void Bind_Grid()
    {
        dg_Grid.DataSource = ds.Tables[1];
        dg_Grid.DataBind();
    }

    private void BindRepeaters()
    {
        HtmlTableRow tr;
        HtmlTableCell tc;
        DataRow dr;
        int i;

        if (ds.Tables.Count > 0)
        {
            for (i = 0; i <= ds.Tables[0].Columns.Count - 1; i++)
            {
                tr = new HtmlTableRow();
                tc = new HtmlTableCell();
                tc.InnerText = ds.Tables[0].Columns[i].ColumnName;
                tc.Style.Add("font-weight", "bold");
                tr.Cells.Insert(0, tc);
                tc = new HtmlTableCell();

                if (ds.Tables[0].Columns[i].ColumnName == "GC No")
                {
                    LinkButton lnk = new LinkButton();
                    lnk.Font.Bold = true;
                    lnk.Text = ds.Tables[0].Rows[0][ds.Tables[0].Columns[i].ColumnName].ToString();
                    lnk.Attributes.Add("OnClick", "return CallTrackNTrace('GC','" + GC_ID + "')");
                    tc.Controls.Add(lnk);
                }
                else
                {
                    tc.InnerText = ds.Tables[0].Rows[0][ds.Tables[0].Columns[i].ColumnName].ToString();
                }

                tr.Cells.Insert(1, tc);
                i = i + 1;
                if (i < ds.Tables[0].Columns.Count)
                {
                    tc = new HtmlTableCell();
                    tc.InnerText = ds.Tables[0].Columns[i].ColumnName;
                    tc.Style.Add("font-weight", "bold");
                    tr.Cells.Insert(2, tc);
                    tc = new HtmlTableCell();
                    tc.InnerText = ds.Tables[0].Rows[0][ds.Tables[0].Columns[i].ColumnName].ToString();
                    tr.Cells.Insert(3, tc);
                }
                tbl_GC_Details.Rows.Add(tr);
            }
        }
    }

    protected void dg_Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
        {
            e.Item.BackColor = Color.FromName(DataBinder.Eval(e.Item.DataItem, "color").ToString());
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
}
