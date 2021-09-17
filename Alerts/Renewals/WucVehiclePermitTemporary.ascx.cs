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

public partial class Alerts_Renewals_WucVehiclePermitTemporary : System.Web.UI.UserControl,IVehiclePermitTemporaryView
{
    #region ClassVariables
    VehiclePermitTemporaryPresenter objVehiclePermitTemporaryPresenter;
    DataSet objDS = null;
    #endregion

    #region ControlsValues
    public DataSet SessionVehiclePermitTemporaryGrid
    {
        get { return StateManager.GetState<DataSet>("VehiclePermitTemporary"); }
        set { StateManager.SaveState("VehiclePermitTemporary", value); }
    }
    #endregion

    #region ControlsBind
    public DataSet BindVehiclePermitTemporaryGrid
    {
        set
        {
            SessionVehiclePermitTemporaryGrid = value;
            dg_VehiclePermitTemporaryGrid.DataSource = value;
            dg_VehiclePermitTemporaryGrid.DataBind();


        }
    }

    public int TaskDefinationId
    {
        //set
        //{
        //   TaskDefinationId = 1;
        //   //hdn_TaskDefinationId.Value = 1;
        //}
        get
        {
            return 4;
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
        objVehiclePermitTemporaryPresenter = new VehiclePermitTemporaryPresenter(this, IsPostBack);

        if (!Page.IsPostBack)
        {

            Session["objDS"] = null;

            hdn_Sort_Dir.Value = "ASC";
            hdn_Sort_Expression.Value = "Vehicle_No";

            Session["objDS"] = null;



        }




    }
    private void BindGrid()
    {
        //objDS = (DataSet)Session["objDS"];
        objDS = SessionVehiclePermitTemporaryGrid;

        if (objDS != null)
        {

            DataView objDV = new DataView(objDS.Tables[0]);

            objDV.Sort = hdn_Sort_Expression.Value + " " + hdn_Sort_Dir.Value;

            dg_VehiclePermitTemporaryGrid.DataSource = objDV;
            dg_VehiclePermitTemporaryGrid.DataBind();
        }

    }

    protected void Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_VehiclePermitTemporaryGrid.CurrentPageIndex = e.NewPageIndex;

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

    protected void Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {

            int Is_Due = Convert.ToInt32(DataBinder.Eval(e.Item.DataItem, "IsDue"));

            if (Is_Due == 0)
            {
                e.Item.CssClass = "NOTUPDATEDLBL";
            }
            else
            {
                e.Item.CssClass = "UPDATEDLBL";
            }
        }
        //if (e.Item.ItemIndex != -1)
        //{
        //    e.Item.Attributes.Add("onmouseover", "this.className='COMMONHOVER';");

        //    if (e.Item.ItemType == ListItemType.Item)
        //    {
        //        e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUT';");
        //    }
        //    else
        //    {
        //        e.Item.Attributes.Add("onmouseout", "this.className='COMMONHOUTALT';");
        //    }

        //}
    }
    protected void lnk_VehiclePermitTemporary_Click(object sender, EventArgs e)
    {
        Label lbl, lbl_Name, lbl_MenuItemId, lbl_DocumentTypeId, lbl_PermitTypeId;
        int VehicleId, MenuItemId, DocumentTypeId, Mode,PermitTypeId;
        LinkButton lnk_TaskList = (LinkButton)sender;

        DataGridItem _item = (DataGridItem)lnk_TaskList.Parent.Parent;
        lbl = (Label)_item.FindControl("lbl_Id");
        lbl_MenuItemId = (Label)_item.FindControl("lbl_MenuItemId");
        lbl_DocumentTypeId = (Label)_item.FindControl("lbl_DocumentTypeId");
        lbl_Name = (Label)_item.FindControl("lbl_Name");
        lbl_PermitTypeId = (Label)_item.FindControl("lbl_PermitTypeId");
        VehicleId = Util.String2Int(lbl.Text);

        MenuItemId = Util.String2Int(lbl_MenuItemId.Text);
        DocumentTypeId = Util.String2Int(lbl_DocumentTypeId.Text);
        Mode = ClassLibraryMVP.General.Mode.ADD;
        PermitTypeId = Util.String2Int(lbl_PermitTypeId.Text);

        StateManager.SaveState("QueryString", DocumentTypeId.ToString());
        String ViewUrl = "~/" + Rights.GetObject().GetLinkDetails(MenuItemId).AddUrl;

        ViewUrl = ViewUrl + "&DocumentTypeId=" + ClassLibraryMVP.Util.EncryptInteger(DocumentTypeId) + "&VehicleId=" + ClassLibraryMVP.Util.EncryptInteger(VehicleId) + "&PermitTypeId=" + ClassLibraryMVP.Util.EncryptInteger(PermitTypeId);
        Response.Redirect(ViewUrl);
    }
    #endregion
}

