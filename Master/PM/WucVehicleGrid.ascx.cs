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
using ClassLibraryMVP.General;
using Raj.EF.MasterPresenter;
using Raj.EF.MasterView;
using System.Text;
using ClassLibraryMVP.Security;
using Raj.EF;

/// <summary>
/// Author        : Ashish Lad
/// Created On    : 30/04/2008
/// Description   : This is the Form  For Master Vehicle Grid
/// </summary>
public partial class Master_PM_WucVehicleGrid : System.Web.UI.UserControl,IVehicleGridView 
{

    #region ClassVariables
    VehicleGridPresenter objVehicleGridPresenter;
    DataSet objDS = null;
    Raj.EC.Common objCommon = new Raj.EC.Common();
    int MenuItemId;
    private int _count;
    #endregion

    #region ControlsValues
    public DataSet SessionVehicleGrid
    {
        get { return StateManager.GetState<DataSet>("Vehicle"); }
        set { StateManager.SaveState("Vehicle", value); }
    }
    #endregion

    #region ControlsBind
    public DataSet Bind_dg_Vehicle
    {
        set
        {
            SessionVehicleGrid = value;
            dg_Grid.DataSource = value;
            dg_Grid.DataBind();
        }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        
        return _isValid;
    }

    public string errorMessage
    {
        set
        {
            //lbl_Errors.Text = value;
        }
    }


    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //return -1;
        }
    }

    #endregion

    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        MenuItemId = Raj.EC.Common.GetMenuItemId();
            

        Search.SearchClicked += new EventHandler(EventBindGrid);

        if (!Page.IsPostBack)
        {

            Session["objDS"] = null;

            Search.FillCombo(MenuItemId);
            Search.FillMstGridHeaders(dg_Grid);

            hdn_Sort_Dir.Value = "ASC";
            hdn_Sort_Expression.Value = "Col1";

            Session["objDS"] = null;
            StateManager.SaveState("QueryString", Rights.GetObject().GetLinkDetails(MenuItemId).QueryString);

           
        }
        Link.text = Rights.GetObject().GetLinkDetails(MenuItemId).Link.ToUpper();

      
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


        String ViewUrl = "~/" + Rights.GetObject().GetLinkDetails(MenuItemId).LinkUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Id);

        Response.Redirect(ViewUrl);

    }
    protected void Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        int Id;

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
           

        }
    }
   
    protected void lnk_PM_Click(object sender, EventArgs e)
    {
        Label lbl, lbl_Name;
        int Id;
        LinkButton lnk_PM = (LinkButton)sender;

        DataGridItem _item = (DataGridItem)lnk_PM.Parent.Parent;
        lbl = (Label)_item.FindControl("lbl_Id");
        lbl_Name = (Label)_item.FindControl("lbl_Name");
        Id = Util.String2Int(lbl.Text);

        //String ViewUrl = "~/" + Rights.GetObject().GetLinkDetails(MenuItemId).LinkUrl  + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Id) + "&Vehicle_No=" + ClassLibraryMVP.Util.EncryptString(lbl_Name.Text);


        String ViewUrl = "~/Master/PM/FrmVehiclePM.aspx?Menu_Item_Id=NgA0AA==&Mode=NAA=" + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Id) + "&Vehicle_No=" + ClassLibraryMVP.Util.EncryptString(lbl_Name.Text);

        


        Response.Redirect(ViewUrl);
    }
#endregion 
}
