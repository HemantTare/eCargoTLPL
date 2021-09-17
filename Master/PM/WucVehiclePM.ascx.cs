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

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.Security;

using Raj.EF.MasterPresenter;
using Raj.EF.MasterView;
using Raj.EF;

/// <summary>
/// Author        : Ashish Lad
/// Created On    : 30/04/2008
/// Description   : This is the Form  For Master Vehicle PM
/// </summary>

public partial class Master_PM_WucVehiclePM : System.Web.UI.UserControl,IVehiclePMView 
{
    #region ClassVariables
    VehiclePMPresenter objVehiclePMPresenter;
    DataSet objDS = null;
    Raj.EC.Common objCommon = new Raj.EC.Common();
    int MenuItemId;
    private int _count;
    #endregion

    #region ControlsValues
    public DataSet SessionVehicleGrid
    {
        get { return StateManager.GetState<DataSet>("VehiclePM"); }
        set { StateManager.SaveState("VehiclePM", value); }
    }
    public int Vehicle_Id
    {
        get
        {
            return Util.String2Int(hdn_Vehicle_ID.Value);

        }
        set
        {
            hdn_Vehicle_ID.Value = Util.Int2String(value);
        }
    }
    #endregion

    #region ControlsBind
    public DataSet Bind_dg_Grid
    {
        set
        {            
            SessionVehicleGrid = value;
            objDS = value;
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
    #region OtherMethods
    private void RedirectOnAddClick()
    {
       
        string Url;

        Url = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(MenuItemId).AddUrl   + "&Vehicle_Id=" + ClassLibraryMVP.Util.EncryptInteger(Vehicle_Id);
       
        btn_Add.Attributes.Add("onclick", "return Open_Add_Window('" + Url + "')");
         
    }

    private void BindGrid()
    {
        
        objDS = StateManager.GetState<DataSet>("VehiclePM");
   

        if (objDS != null)
        {

            DataView objDV = new DataView(objDS.Tables[0]);

            dg_Grid.DataSource = objDV;
            dg_Grid.DataBind();
        }

    }
   
    #endregion
    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        //MenuItemId = Raj.EF.Common.GetMenuItemId();
        MenuItemId = 330; //10039;         //Modified By Vajiha
        
        Vehicle_Id = ClassLibraryMVP.Util.DecryptToInt(Request.QueryString["Id"]);
        lbl_Truck_No.Text = ClassLibraryMVP.Util.DecryptToString(Request.QueryString["Vehicle_No"]);
        objVehiclePMPresenter = new VehiclePMPresenter(this, IsPostBack);
        
        
        if (!Page.IsPostBack)
        {
            Session["objDS"] = null;        
            StateManager.SaveState("QueryString", Rights.GetObject().GetLinkDetails(MenuItemId).QueryString);
            
            if (Raj.EC.Common.IsPopupMenuItem(MenuItemId))
            {
                 RedirectOnAddClick();
            }
        }
      
        string Url;
        Url = "FrmVehicleTaskSelection.aspx?Vehicle_Id=" + ClassLibraryMVP.Util.EncryptInteger(Vehicle_Id);
        btn_Apply_Task_Template.Attributes.Add("onclick", "return Open_Add_Window('" + Url + "')");
        btn_Add.Visible = Convert.ToBoolean(Rights.GetObject().getForm_Rights(MenuItemId).canAdd());
        dg_Grid.Columns[7].Visible = Convert.ToBoolean(Rights.GetObject().getForm_Rights(MenuItemId).canRead());
        dg_Grid.Columns[8].Visible = Convert.ToBoolean(Rights.GetObject().getForm_Rights(MenuItemId).canEdit());


      
        
        btn_Add.Visible = Convert.ToBoolean(Rights.GetObject().getForm_Rights(MenuItemId).canAdd());
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
        
    protected void Grid_EditCommand(object source, DataGridCommandEventArgs e)
    {
        int Id;
        Id = Convert.ToInt32(((Label)e.Item.Cells[0].FindControl("lbl_Id")).Text);


        String EditUrl = "~/" + Rights.GetObject().GetLinkDetails(MenuItemId).EditUrl  + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Id);

        Response.Redirect(EditUrl);

    }
    protected void Grid_ItemDataBound(object sender, DataGridItemEventArgs e)
    {

        int Id;
        LinkButton lnk_Btn;


        
        Lbl_Total_Records.Text = SessionVehicleGrid.Tables[0].Rows.Count + " Record(s) Found";
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

            //Id = Convert.ToInt32(((Label)e.Item.Cells[0].FindControl("lbl_Id")).Text);
            Id = Util.String2Int(DataBinder.Eval(e.Item.DataItem, "Task_ID").ToString());

            //VIEW BUTTON
            lnk_Btn = ((LinkButton)e.Item.Cells[7].FindControl("lnk_View"));
            String ViewPath = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(MenuItemId).ViewUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Id);
            lnk_Btn.Attributes.Add("onclick", "return Open_View_Window('" + ViewPath + "')");

            //EDIT BUTTON
            if (Raj.EC.Common.IsPopupMenuItem(MenuItemId))
            {
                lnk_Btn = ((LinkButton)e.Item.Cells[8].FindControl("lnk_Edit"));
                String EditPath = Util.GetBaseURL() + "/" + Rights.GetObject().GetLinkDetails(MenuItemId).EditUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Id);
                lnk_Btn.Attributes.Add("onclick", "return Open_Edit_Window('" + EditPath + "')");
            }
        }
    }
    protected void lnk_Edit_Click(object sender, EventArgs e)
    {
        Label lbl;
        int Id;
        LinkButton lnk_Edit = (LinkButton)sender;

        DataGridItem _item = (DataGridItem)lnk_Edit.Parent.Parent;
        lbl = (Label)_item.FindControl("lbl_Id");
        Id = Util.String2Int(lbl.Text);

        String EditUrl = "~/" + Rights.GetObject().GetLinkDetails(MenuItemId).EditUrl + "&Id=" + ClassLibraryMVP.Util.EncryptInteger(Id);

        Response.Redirect(EditUrl);
    }

    #endregion
    //protected void btn_Add_Click(object sender, EventArgs e)
    //{
    //    ////Label lbl;
    //    ////int Id;
    //    ////LinkButton lnk_Edit = (LinkButton)sender;

    //    ////DataGridItem _item = (DataGridItem)lnk_Edit.Parent.Parent;
    //    ////lbl = (Label)_item.FindControl("lbl_Id");
    //    ////Id = Util.String2Int(lbl.Text);

    //    //String AddUrl = "~/" + Rights.GetObject().GetLinkDetails(MenuItemId).EditUrl + "&Vehicle_Id=" + ClassLibraryMVP.Util.EncryptInteger(Vehicle_Id);

    //    ////Url = "FrmTaskTemplate.aspx?Vehicle_Id=" + ClassLibraryMVP.Util.EncryptInteger(Vehicle_Id);
    //    ////btn_Add.Attributes.Add("onclick", "return Open_Add_Window('" + Url + "')");

    //    //Response.Redirect(AddUrl);
    //}

    
}
