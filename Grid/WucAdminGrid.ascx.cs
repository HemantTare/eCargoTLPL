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
using System.Text;
using ClassLibraryMVP.Security;
using ClassLibraryMVP;
using Raj.EC;


public partial class GridWucAdminGrid : System.Web.UI.UserControl
{
    DataSet objDS = null;
    Common objCommon = new Common();
    int MenuItemId;
    private int _count;



    protected void Page_Load(object sender, EventArgs e)
    {

        MenuItemId = Common.GetMenuItemId();
        
        
        Search.SearchClicked += new EventHandler(EventBindGrid);
       
        if (!Page.IsPostBack)
        {
            Session["objDS"] = null;

            Search.FillCombo(MenuItemId);
            Search.FillAdminGridHeaders(dg_Grid);

            hdn_Sort_Dir.Value = "ASC";
            hdn_Sort_Expression.Value = "Col1";

            Session["objDS"] = null;

            StateManager.SaveState("QueryString", Rights.GetObject().GetLinkDetails(MenuItemId).QueryString);


        }
        Link.text = Rights.GetObject().GetLinkDetails(MenuItemId).Link.ToUpper();

        btn_Add.Visible = Convert.ToBoolean(Rights.GetObject().getForm_Rights(MenuItemId).canAdd());
        dg_Grid.Columns[10].Visible = Convert.ToBoolean(Rights.GetObject().getForm_Rights(MenuItemId).canRead());
        dg_Grid.Columns[11].Visible = Convert.ToBoolean(Rights.GetObject().getForm_Rights(MenuItemId).canEdit());
        dg_Grid.Columns[12].Visible = Convert.ToBoolean(Rights.GetObject().getForm_Rights(MenuItemId).canDelete());

        ForceRights();
    }

    private void ForceRights()
    {
        //pankaj 17 nov as some forms required rights externally
        bool can_add = true;
        bool can_edit = true;
        bool can_cancel = true;
        bool can_read = true;

        objCommon.ForceRights(ref can_read, ref can_add, ref can_edit, ref can_cancel);

        if (can_read == false)
            dg_Grid.Columns[10].Visible = false;

        if (can_add == false)
            btn_Add.Visible = false;

        if (can_edit == false)
            dg_Grid.Columns[11].Visible = false;

        if (can_cancel == false)
            dg_Grid.Columns[12].Visible = false;
        //pankaj 17 nov as some forms required rights externally
    }

    

    private void BindGrid()
    {
        objDS = (DataSet)Session["objDS"];

        if (objDS != null)
        {
          
            DataView objDV = new DataView(objDS.Tables[0]);

            objDV.Sort = hdn_Sort_Expression.Value + " " + hdn_Sort_Dir.Value;

            dg_Grid.DataSource = objDV;
            dg_Grid.DataBind();
        }
       
    }

    private void EventBindGrid(object source, EventArgs e)
    {
        dg_Grid.CurrentPageIndex = 0;
        BindGrid();
    }

    protected void Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_Grid.CurrentPageIndex = e.NewPageIndex;

        BindGrid();

    }
    protected void Grid_SortCommand(object source, DataGridSortCommandEventArgs e)
    {

        hdn_Sort_Expression.Value = e.SortExpression;

        if (hdn_Sort_Dir.Value == "DESC")
        {
            hdn_Sort_Dir.Value = "ASC";
        }
        else
        {
            hdn_Sort_Dir.Value = "DESC";
        }

        BindGrid();
    }
    protected void Grid_EditCommand(object source, DataGridCommandEventArgs e)
    {
        int Id;
        Id = Convert.ToInt32(((Label)e.Item.Cells[0].FindControl("lbl_Id")).Text);


        String EditUrl = "~/" + Rights.GetObject().GetLinkDetails(MenuItemId).EditUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Id);
        
        Response.Redirect(EditUrl);

    }
    protected void Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        int Id;
        LinkButton lnk_Btn;
        
        
        if (Session["objDS"] == null)
        {
            _count = 0;
        }
        else
        {
            _count = ((DataSet)(Session["objDS"])).Tables[0].Rows.Count;
        }
        if (_count == 0)
        {
            Lbl_Total_Records.Text = "";
        }
        else
        {
          Lbl_Total_Records.Text = _count.ToString() + " Record(s) Found";
        }
        if (e.Item.ItemIndex != -1)
        {
            e.Item.Attributes.Add("onmouseover", "this.className='COMMONHOVER';");

            if (e.Item.ItemType == ListItemType.Item)
            {
                e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUT';");
            }
            else
            {
                e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUTALT';");
            }

            Id = Convert.ToInt32(((Label)e.Item.Cells[0].FindControl("lbl_Id")).Text);
            lnk_Btn = ((LinkButton)e.Item.Cells[10].FindControl("lnk_View"));
            

            String ViewPath = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(MenuItemId).ViewUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Id);

            lnk_Btn.Attributes.Add("onclick", "return Open_Popup_Window('" + ViewPath + "')");


        }
    }

    protected void btn_Add_Click(object sender, EventArgs e)
    {

        string AddUrl = "~/" + Rights.GetObject().GetLinkDetails(MenuItemId).AddUrl;
        
        Response.Redirect(AddUrl);
       
    }

}
