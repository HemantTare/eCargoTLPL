using System;
using System.Data;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.AdminPresenter;
using Raj.EC.AdminView;
using Raj.EC;

/// <summary>
/// Author        : Ankit
/// Created On    : 04/10/2008
/// Description   : This Page is For Admin Menu Item.
/// </summary>

public partial class Administration_Rights_WucMenuItem : System.Web.UI.UserControl,IMenuItemView
{
    #region ClassVariables
    MenuItemPresenter objMenuItemPresenter;
    #endregion

    #region ControlsValues
    
    public int SerialNo
    {
        get { return Util.String2Int(txt_SerialNo.Text); }
        set { txt_SerialNo.Text = value.ToString(); }
    }
    public string MenuItemName
    {
        get { return txt_MenuItemName.Text; }
        set { txt_MenuItemName.Text = value; }
    }
    public int MenuSystemId
    {
        get { return Util.String2Int(ddl_MenuSystemId.SelectedValue); }
        set { ddl_MenuSystemId.SelectedValue = value.ToString(); }
    }
    
    public int MenuHeadId
    {
        get { return Util.String2Int(ddl_MenuHeadId.SelectedValue); }
        set { ddl_MenuHeadId.SelectedValue = value.ToString(); }
    }
   
    public int MenuGroupId
    {
        get { return Util.String2Int(ddl_MenuGroupId.SelectedValue); }
        set { ddl_MenuGroupId.SelectedValue = value.ToString(); }
    }

  
    public string MenuItemLink
    {
        get { return txt_MenuItemLink.Text; }
        set { txt_MenuItemLink.Text = value; }
    }
   
    public string Description
    {
        get { return txt_Description.Text; }
        set { txt_Description.Text = value; }
    }

    public string LinkUrl
    {
        get { return txt_LinkUrl.Text; }
        set { txt_LinkUrl.Text = value; }
    }
    public string ViewUrl
    {
        get { return txt_ViewUrl.Text; }
        set { txt_ViewUrl.Text = value; }
    }

    public string AddUrl
    {
        get { return txt_AddUrl.Text; }
        set { txt_AddUrl.Text = value; }
    }
    public string EditUrl
    {
        get { return txt_EditUrl.Text; }
        set { txt_EditUrl.Text = value; }
    }
    public string DeleteUrl
    {
        get { return txt_DeleteUrl.Text; }
        set { txt_DeleteUrl.Text = value; }
    }
    public string ReportUrl
    {
        get { return txt_ReportUrl.Text; }
        set { txt_ReportUrl.Text = value; }
    }
    public string QueryString
    {
        get { return txt_QueryString.Text; }
        set { txt_QueryString.Text = value; }
    }
    public string TableName
    {
        get { return txt_TableName.Text; }
        set { txt_TableName.Text = value; }
    }
    public string KeyColumnName
    {
        get { return txt_KeyColumnName.Text; }
        set { txt_KeyColumnName.Text = value; }
    }

    public Boolean Is_Active
    {
        get { return chkIsActive.Checked; }
        set { chkIsActive.Checked = value; }
    }
    public Boolean Is_PopUp_From_Link
    {
        get { return chk_IsPopupFromLink.Checked; }
        set { chk_IsPopupFromLink.Checked = value; }
    }
    public string MenuItemCode
    {
        get { return txt_MenuItemCode.Text; }
        set { txt_MenuItemCode.Text = value; }
    }  //added 11-02-09

    public string MenuitemType
    {
        get { return StateManager.GetState<string>("QueryString"); }
    }  //added 11-02-09

    #endregion

    #region ControlEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        objMenuItemPresenter = new MenuItemPresenter(this, IsPostBack);
        Common objCommon = new Common();
        hdf_ResourceString.Value = objCommon.GetResourceString("Administration/Rights/App_LocalResources/WucMenuItem.ascx.resx");

        if (UserManager.getUserParam().HierarchyCode == "AD")
            tr_IsActive.Visible = true;
        else
            tr_IsActive.Visible = false;
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objMenuItemPresenter.Save();
    }
    protected void ddl_MenuSystemId_SelectedIndexChanged(object sender, EventArgs e)
    {
        objMenuItemPresenter.FillMenuHead();
        objMenuItemPresenter.FillMenuGroup();
    }
    protected void ddl_MenuHeadId_SelectedIndexChanged(object sender, EventArgs e)
    {
        objMenuItemPresenter.FillMenuGroup();
    }
    #endregion

    #region ControlsBind

    public DataSet BindMenuGroup
    {
        set
        {
            ddl_MenuGroupId.DataSource = value;
            ddl_MenuGroupId.DataTextField = "MenuGroup_Name";
            ddl_MenuGroupId.DataValueField = "MenuGroup_ID";
            ddl_MenuGroupId.DataBind();
        }
    }

    public DataSet BindMenuSystem
    {
        set
        {
            ddl_MenuSystemId.DataSource = value;
            ddl_MenuSystemId.DataTextField = "System_Name";
            ddl_MenuSystemId.DataValueField = "System_ID";
            ddl_MenuSystemId.DataBind();
        }
    }

    public DataSet BindMenuHead
    {
        set
        {
            ddl_MenuHeadId.DataSource = value;
            ddl_MenuHeadId.DataTextField = "Menu_Head_Name";
            ddl_MenuHeadId.DataValueField = "Menu_Head_Id";
            ddl_MenuHeadId.DataBind();
        }
    }
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
       
        if (txt_MenuItemName.Text.Trim()==string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_MenuItemName").ToString();// "Please Enter Menu Item Name";
            _isValid= false;
        }
        else if (SerialNo <= 0)
        {
            errorMessage = GetLocalResourceObject("Msg_SerialNo").ToString() ; //"Please Enter Serial No.";
            _isValid= false ;
        }
        else if (ddl_MenuSystemId.SelectedValue == "0" && Util.String2Int(ddl_MenuSystemId.SelectedValue) == -1)
        {
            errorMessage = GetLocalResourceObject("Msg_MenuSystem").ToString(); //"Please Select Menu System";
            _isValid= false;
        }
        else if (ddl_MenuHeadId.SelectedValue == "0" && Util.String2Int(ddl_MenuHeadId.SelectedValue) == -1)
        {
            errorMessage = GetLocalResourceObject("Msg_MenuHead").ToString(); //"Please Select Menu Head";
            _isValid= false;
        }
        else if (ddl_MenuGroupId.SelectedValue == "0" && Util.String2Int(ddl_MenuGroupId.SelectedValue) == -1)
        {
            errorMessage = GetLocalResourceObject("Msg_MenuGroup").ToString(); //"Please Select Menu Group";
             _isValid=false;
        }
        
        else if (txt_MenuItemLink.Text.Trim()==string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_MenuItemLink").ToString(); //"Please Enter Menu Item Link";
            _isValid= false ;
        }
        else if (txt_Description.Text.Trim()==string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_Description").ToString(); //"Please Enter Description";
            _isValid= false;
        }
        else if (txt_LinkUrl.Text.Trim()==string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_LinkUrl").ToString();//"Please Enter Link URL";
            _isValid= false;
        }
        else if (txt_ViewUrl.Text.Trim()==string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_ViewUrl").ToString(); //"Please Enter View URL";
            _isValid= false;
        }
        else if (txt_AddUrl.Text.Trim()==string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_AddUrl").ToString(); //"Please Enter Add URL";
            return false;
        }
        else if (txt_EditUrl.Text.Trim()==string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_EditUrl").ToString(); //"Please Enter Edit URL";
            _isValid= false;
        }
        else if (txt_DeleteUrl.Text.Trim()==string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_DeleteUrl").ToString(); //"Please Enter Delete URL";
            _isValid = false;
        }        
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }

    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;
        }
    }

 
    #endregion

    #region OtherProperties
    #endregion

    #region OtherMethods

    #endregion
   
}
