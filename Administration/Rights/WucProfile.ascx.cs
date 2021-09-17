using System;
using System.Data;

using ClassLibraryMVP;
using Raj.EC.AdminPresenter;
using Raj.EC.AdminView;
using Raj.EC;

/// <summary>
/// Author        : Ankit
/// Created On    : 03/10/2008
/// Description   : This Page is For  Administration Profile
/// </summary>

public partial class Administration_Rights_WucProfile : System.Web.UI.UserControl,IProfileView
{

    #region ClassVariables
    ProfilePresenter objProfilePresenter;
    #endregion

    #region ControlEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        objProfilePresenter = new ProfilePresenter(this, IsPostBack);
        Common objCommon = new Common();
        hdf_ResourceString.Value = objCommon.GetResourceString("Administration/Rights/App_LocalResources/WucProfile.ascx.resx");
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objProfilePresenter.Save();
    }

    #endregion

    #region ControlsValues

    public string Profile_Name
    {
        get { return txt_ProfileName.Text.Trim(); }
        set { txt_ProfileName.Text = value; }
    }

    public string Description
    {
        get { return txt_Description.Text.Trim(); }
        set { txt_Description.Text = value; }
    }
    public Boolean IsCSA
    {
        get { return chkIsCSA.Checked; }
        set { chkIsCSA.Checked = value; }
    }

    public string Hierarchy_Name
    {
        get { return ddl_Hierarchy.SelectedItem.Text; }
        set { ddl_Hierarchy.SelectedItem.Text = value; }
    }

    public string Hierarchy_Code
    {
        get { return ddl_Hierarchy.SelectedValue; }
        set { ddl_Hierarchy.SelectedValue = value; }
    }
      
    #endregion

    #region ControlsBind

    public DataSet BindHierarchy
    {
        set
        {
            ddl_Hierarchy.DataTextField = "Hierarchy_Name";
            ddl_Hierarchy.DataValueField = "Hierarchy_Code";
            ddl_Hierarchy.DataSource = value;
            ddl_Hierarchy.DataBind();
        }
    }  

    #endregion


    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (txt_ProfileName.Text.Trim() == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_txt_ProfileName").ToString();  // "Please Enter Profile Name";
            txt_ProfileName.Focus();
            _isValid = false;
        }
        else if (ddl_Hierarchy.SelectedValue == " ")
        {
            errorMessage = GetLocalResourceObject("Msg_txt_HierarchyName").ToString();// "Please Select Hierarchy Name";
            ddl_Hierarchy.Focus();
            _isValid = false;
        }
        else if (txt_Description.Text.Trim() == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_txt_Description").ToString();// "Please Enter Description";
            txt_Description.Focus();
            _isValid = false;
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    public string errorMessage
    {
        set
        {
            lbl_Errors.Text = value;

        }
    }

    public int keyID
    {
        get
        {
            //return 1;
            return Util.DecryptToInt(Request.QueryString["Id"]); 
        }
    }
    #endregion

    #region OtherProperties   
    #endregion

    #region OtherMethods

    #endregion

}
