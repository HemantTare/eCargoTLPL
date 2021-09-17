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
using ClassLibraryMVP;


public partial class CommonControls_WucSearch : System.Web.UI.UserControl
{
   
    public event EventHandler SearchClicked;
    

    public string GetSearchText()
    {
        return  txt_Search.Text;
    }

    public int GetComboValue()
    {
        return Convert.ToInt32(ddl_Search.SelectedValue);
    }

    public void FillCombo(DataGrid dg_Grid)
    {
        int i;
        DataSet ds_Combo = new DataSet();
        DataRow Drow=null;

        ds_Combo.Tables.Add("ComboTable");
        ds_Combo.Tables[0].Columns.Add("id");
        ds_Combo.Tables[0].Columns.Add("name");

        for (i = 0; i <= dg_Grid.Columns.Count - 4; i++)
        {
            if (dg_Grid.Columns[i].Visible)
            {
                Drow = ds_Combo.Tables[0].NewRow();
                Drow["id"] = i + 1;
                Drow["name"] = dg_Grid.Columns[i].HeaderText;
                ds_Combo.Tables[0].Rows.Add(Drow);
            }
        }

        ddl_Search.DataSource = ds_Combo;
        ddl_Search.DataValueField = "id";
        ddl_Search.DataTextField = "name";
        ddl_Search.DataBind();
    }





 
    protected void btn_Search_Click(object sender, ImageClickEventArgs e)
    {
        if (SearchClicked != null)
        {
            SearchClicked(this, e);
        }

    }
    protected void txt_Search_TextChanged(object sender, EventArgs e)
    {
        if (SearchClicked != null)
        {
            SearchClicked(this, e);
        }
    }
}
