using System;
using System.Data;

using Raj.EC.AdminPresenter;
using Raj.EC.AdminView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC;

/// <summary>
/// Author        : Ankit
/// Created On    : 04/10/2008
/// Description   : This Page is For  Administration Menu group
/// </summary>
/// 

public partial class Administration_Rights_WucMenuGroup : System.Web.UI.UserControl, IMenuGroupView
{
    #region ClassVariables
    MenuGroupPresenter objMenuGroupPresenter;
    #endregion

    #region ControlEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        objMenuGroupPresenter = new MenuGroupPresenter(this, IsPostBack);
        if (!IsPostBack)
        {
            Common objCommon = new Common();
            hdf_ResourceString.Value = objCommon.GetResourceString("Administration/Rights/App_LocalResources/WucMenuGroup.ascx.resx");
        }
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objMenuGroupPresenter.Save();
    }

    protected void ddl_SystemName_SelectedIndexChanged(object sender, EventArgs e)
    {
        objMenuGroupPresenter.FillMenuHead();
    }
    #endregion

    #region ControlsValues

    public string MenuGroupName
    {
        get { return txt_MenuGroupName.Text.Trim(); }
        set { txt_MenuGroupName.Text = value; }
    }

    public int SerialNo
    {
        get { return Util.String2Int(txt_SerialNo.Text.Trim()); }
        set { txt_SerialNo.Text = value.ToString(); }
    }

    public bool Chk_IsTranMenuGroup
    {
        get { return Convert.ToBoolean(chk_IsTranMenuGroup.Checked); }
        set { chk_IsTranMenuGroup.Checked = Convert.ToBoolean(value); }
    }

    public int MenuTypeId
    {
        get { return Util.String2Int(ddl_MenuTypeId.SelectedValue); }
        set { ddl_MenuTypeId.SelectedValue = Util.Int2String(value); }
    }

    public string Description
    {
        get { return txt_Description.Text.Trim(); }
        set { txt_Description.Text = value; }
    }

    public int MenuSystemId
    {
        get { return Util.String2Int(ddl_SystemName.SelectedValue); }
        set { ddl_SystemName.SelectedValue = Util.Int2String(value); }
    }

    public int MenuHeadId
    {
        get { return Util.String2Int(ddl_MenuHeadId.SelectedValue); }
        set { ddl_MenuHeadId.SelectedValue = Util.Int2String(value); }
    }

    #endregion

    #region ControlsBind

    public DataTable BindSystemName
    {
        set
        {
            ddl_SystemName.DataTextField = "System_Name";
            ddl_SystemName.DataValueField = "System_ID";
            ddl_SystemName.DataSource = value;
            ddl_SystemName.DataBind();
        }
    }

    public DataSet BindMenuHead
    {
        set
        {
            ddl_MenuHeadId.DataTextField = "Menu_Head_Name";
            ddl_MenuHeadId.DataValueField = "Menu_Head_Id";
            ddl_MenuHeadId.DataSource = value;
            ddl_MenuHeadId.DataBind();
        }
    }
    public DataTable BindMenuType
    {
        set
        {
            ddl_MenuTypeId.DataTextField = "Menu_TypeName";
            ddl_MenuTypeId.DataValueField = "Menu_TypeID";
            ddl_MenuTypeId.DataSource = value;
            ddl_MenuTypeId.DataBind();
        }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        if (txt_MenuGroupName.Text.Trim() == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_MenuGroupName").ToString(); //"Please Enter Menu Group Name";
            txt_MenuGroupName.Focus();
            _isValid = false;
        }
        else if (txt_SerialNo.Text.Trim() == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_SerialNo").ToString(); //"Please Enter Serial Number";
            txt_SerialNo.Focus();
            _isValid = false;
        }
        else if (ddl_SystemName.SelectedValue == "")
        {
            errorMessage = GetLocalResourceObject("Msg_SystemName").ToString(); //"Please Select System Name";
            ddl_SystemName.Focus();
            _isValid = false;
        }
        else if (ddl_MenuHeadId.SelectedValue == "")
        {
            errorMessage = GetLocalResourceObject("Msg_MenuHead").ToString(); //"Please Select Menu Head";
            ddl_MenuHeadId.Focus();
            _isValid = false;
        }
        else if (ddl_MenuTypeId.SelectedValue == "")
        {
            errorMessage = GetLocalResourceObject("Msg_MenuType").ToString(); //"Please Select Menu Type";
            ddl_MenuTypeId.Focus();
            _isValid = false;
        }
        else if (txt_Description.Text.Trim() == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_Description").ToString(); //"Please Enter Description";
            txt_Description.Focus();
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
            //return 1;
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
