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

public partial class Alerts_PM_WucPMTaskList : System.Web.UI.UserControl,ITaskListView
{
    #region ClassVariables
    TaskListPresenter objTaskListPresenter;
    DataSet objDS = null;   
    #endregion

    #region ControlsValues
    public DataSet SessionTaskListGrid
    {
        get { return StateManager.GetState<DataSet>("TaskList"); }
        set { StateManager.SaveState("TaskList", value); }
    }
    #endregion

    #region ControlsBind
    public DataSet BindTaskListGrid
    {
        set
        {
            SessionTaskListGrid = value;
            dg_TaskGrid.DataSource = value;
            dg_TaskGrid.DataBind();            
        }
    }

    public int TaskDefinationId
    {       
        get{return 1;}
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
        }
    }

    #endregion

    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        objTaskListPresenter = new TaskListPresenter(this, IsPostBack);

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

        objDS = SessionTaskListGrid; 

        if (objDS != null)
        {
            DataView objDV = new DataView(objDS.Tables[0]);

            objDV.Sort = hdn_Sort_Expression.Value + " " + hdn_Sort_Dir.Value;

            dg_TaskGrid.DataSource = objDV;
            dg_TaskGrid.DataBind();
        }
    }
    
    protected void Grid_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        dg_TaskGrid.CurrentPageIndex = e.NewPageIndex;
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
    }
    
    protected void Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemIndex != -1)
        {
          
           int Is_Due= Convert.ToInt32(DataBinder.Eval(e.Item.DataItem,"IsDue"));

           if (Is_Due==0)
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
    protected void lnk_PM_TaskList_Click(object sender, EventArgs e)
    {
        Label lbl, lbl_Name, lbl_ServiceCategoryId, lbl_ServiceId, lbl_MenuItemId, lbl_DocumentTypeId, lbl_BranchId, lbl_VendorId, lbl_WorkedAtCompanyWorkShop, lbl_VendorName;
        int VehicleId, ServiceCategoryId, ServiceId, MenuItemId, DocumentTypeId, Mode, BranchId, VendorId, WorkedAtCompanyWorkShop;
        LinkButton lnk_TaskList = (LinkButton)sender;

        DataGridItem _item = (DataGridItem)lnk_TaskList.Parent.Parent;
        lbl = (Label)_item.FindControl("lbl_Id");
        lbl_ServiceCategoryId = (Label)_item.FindControl("lbl_ServiceCategoryId");
        lbl_ServiceId = (Label)_item.FindControl("lbl_ServiceId");
        lbl_MenuItemId = (Label)_item.FindControl("lbl_MenuItemId");
        lbl_DocumentTypeId = (Label)_item.FindControl("lbl_DocumentTypeId");
        lbl_Name = (Label)_item.FindControl("lbl_Name");
        lbl_BranchId=(Label)_item.FindControl("lbl_BranchId");
        lbl_VendorId=(Label)_item.FindControl("lbl_VendorId");
        lbl_VendorName = (Label)_item.FindControl("lbl_VendorName");
        lbl_WorkedAtCompanyWorkShop = (Label)_item.FindControl("lbl_WorkedAtCompanyWorkShop");

        VehicleId = Util.String2Int(lbl.Text);
        ServiceCategoryId = Util.String2Int(lbl_ServiceCategoryId.Text);
        ServiceId = Util.String2Int(lbl_ServiceId.Text);
        MenuItemId = Util.String2Int(lbl_MenuItemId.Text);
        DocumentTypeId = Util.String2Int(lbl_DocumentTypeId.Text);
        Mode = ClassLibraryMVP.General.Mode.ADD;
        BranchId=Util.String2Int(lbl_BranchId.Text);
        VendorId = Util.String2Int(lbl_VendorId.Text);
        WorkedAtCompanyWorkShop = Util.String2Int(lbl_WorkedAtCompanyWorkShop.Text);

        StateManager.SaveState("QueryString", DocumentTypeId.ToString());
        String ViewUrl = "~/" + Rights.GetObject().GetLinkDetails(MenuItemId).AddUrl;

        ViewUrl = ViewUrl + "&Document_Type_Id=" + ClassLibraryMVP.Util.EncryptInteger(DocumentTypeId) + "&VehicleId=" + ClassLibraryMVP.Util.EncryptInteger(VehicleId) + "&ServiceCategoryId=" + ClassLibraryMVP.Util.EncryptInteger(ServiceCategoryId) + "&ServiceId=" + ClassLibraryMVP.Util.EncryptInteger(ServiceId) + "&BranchId=" + ClassLibraryMVP.Util.EncryptInteger(BranchId) + "&VendorId=" + ClassLibraryMVP.Util.EncryptInteger(VendorId)
            + "&VendorName=" + ClassLibraryMVP.Util.EncryptString(lbl_VendorName.Text) + "&CompanyWorkShop=" + ClassLibraryMVP.Util.EncryptInteger(WorkedAtCompanyWorkShop);
        Response.Redirect(ViewUrl);
    }
#endregion 
}
