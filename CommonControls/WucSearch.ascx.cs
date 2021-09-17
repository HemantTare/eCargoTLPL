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
using System.Text.RegularExpressions;
using ClassLibraryMVP;
using Raj.EC;



/// <summary>
/// Common search Control which will be used in All Grids
/// </summary>

public partial class CommonControls_WucSearch : System.Web.UI.UserControl
{
    
    DataSet objDS = new DataSet();
    DataSet objDS1 = new DataSet();
    Common ObjCommon=new Common();
    public event EventHandler SetDateInSession;
    public event EventHandler SearchClicked;


    public string GetSearchText()
    {
        return txt_Search.Text;
    }

    public string  GetDdlSearchValue()
    {
        return ddl_Search.SelectedValue;
    }

    public string GetMenuItemForPrint       //added by shiv for document printing
    {
        set{hdn_MenuForPrint.Value = value;}
        get { return hdn_MenuForPrint.Value; }
    }

    public void FillCombo(int MenuItemId)
    {
        objDS1 = ObjCommon.FillDetailsInGrid(MenuItemId, "", "", true);
        ddl_Search.DataSource = objDS1;
        ddl_Search.DataValueField = "id";
        ddl_Search.DataTextField = "name";
        ddl_Search.DataBind();
        StateManager.SaveState("DsCombo", objDS1);

    }

   

    public void FillMstGridHeaders(DataGrid dg_Grid)
    {

        int i;
        DataTable objDT = new DataTable();

        int ColCount = 10;
        
        objDT.Columns.Add("col1");
        objDT.Columns.Add("col2");
        objDT.Columns.Add("col3");
        objDT.Columns.Add("col4");
        objDT.Columns.Add("col5");
        objDT.Columns.Add("col6");
        objDT.Columns.Add("col7");
        objDT.Columns.Add("col8");
        objDT.Columns.Add("col9");
        objDT.Columns.Add("col10");


        for (i = 0; i <= ddl_Search.Items.Count - 1; i++)
        {
            dg_Grid.Columns[i].HeaderText = ddl_Search.Items[i].Text;
        }

        for (i = ddl_Search.Items.Count + 1; i <= ColCount; i++)
        {
            dg_Grid.Columns[i - 1].Visible = false;
        }

        objDS.Tables.Add(objDT);
        dg_Grid.DataSource = objDS;
        dg_Grid.DataBind();

        ddl_Search.Items.RemoveAt(0);


    }


    public void FillAdminGridHeaders(DataGrid dg_Grid)
    {

        int i;
        DataTable objDT = new DataTable();

        int ColCount = 10;

        objDT.Columns.Add("col1");
        objDT.Columns.Add("col2");
        objDT.Columns.Add("col3");
        objDT.Columns.Add("col4");
        objDT.Columns.Add("col5");
        objDT.Columns.Add("col6");
        objDT.Columns.Add("col7");
        objDT.Columns.Add("col8");
        objDT.Columns.Add("col9");
        objDT.Columns.Add("col10");


        for (i = 0; i <= ddl_Search.Items.Count - 1; i++)
        {
            dg_Grid.Columns[i].HeaderText = ddl_Search.Items[i].Text;
        }

        for (i = ddl_Search.Items.Count + 1; i <= ColCount; i++)
        {
            dg_Grid.Columns[i - 1].Visible = false;
        }

        objDS.Tables.Add(objDT);
        dg_Grid.DataSource = objDS;
        dg_Grid.DataBind();

        ddl_Search.Items.RemoveAt(0);


    }


    public void FillOprGridHeaders(DataGrid dg_Grid)
    {

        int i;
        DataTable objDT = new DataTable();

        int ColCount = 10;
        objDT.Columns.Add("col1");
        objDT.Columns.Add("col2");
        objDT.Columns.Add("col3");
        objDT.Columns.Add("col4");
        objDT.Columns.Add("col5");
        objDT.Columns.Add("col6");
        objDT.Columns.Add("col7");
        objDT.Columns.Add("col8");
        objDT.Columns.Add("col9");
        objDT.Columns.Add("col10");


        for (i = 0; i <= ddl_Search.Items.Count - 1; i++)
        {
            dg_Grid.Columns[i].HeaderText = ddl_Search.Items[i].Text;
        }

        for (i = ddl_Search.Items.Count + 1; i <= ColCount; i++)
        {
            dg_Grid.Columns[i - 1].Visible = false;
        }

        objDS.Tables.Add(objDT);
        dg_Grid.DataSource = objDS;
        dg_Grid.DataBind();

        ddl_Search.Items.RemoveAt(0);

    }



    public void FillGrid(int MenuItemId)
    {

        string ColName = ddl_Search.SelectedValue;
        string SearchText = Regex.Replace(txt_Search.Text, "'", "");

        objDS = ObjCommon.FillDetailsInGrid(MenuItemId, ColName, SearchText, false);

        StateManager.SaveState("objDS", objDS);
        
    }

   

    protected void btn_Search_Click(object sender, ImageClickEventArgs e)
    {

        if (SetDateInSession != null)
        {
            SetDateInSession(this, e);
        }

        if (Util.String2Int(GetMenuItemForPrint) > 0)  //added by shiv for document printing
        {
            FillGrid(Util.String2Int(GetMenuItemForPrint));
        }
        else
        {
            FillGrid(Common.GetMenuItemId());
        }

        if (SearchClicked != null)
        {
            SearchClicked(this, e);
        }

    }
    protected void txt_Search_TextChanged(object sender, EventArgs e)
    {
        if (Util.String2Int(GetMenuItemForPrint) > 0) //added by shiv for document printing
        {
            FillGrid(Util.String2Int(GetMenuItemForPrint));
        }
        else
        {
            FillGrid(Common.GetMenuItemId());
        }

        if (SearchClicked != null)
        {
            SearchClicked(this, e);
        }
    }
}
