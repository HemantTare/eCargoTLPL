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
using Raj.EC.GeneralPresenter;
using Raj.EC.GeneralView;
using System.Data.SqlClient;
using Raj.EC.ControlsView;
using Raj.EC;


public partial class Master_General_WucNonEmployeeUsers : System.Web.UI.UserControl,IUserMasterView
{
    #region ClassVariables
    PageControls pc = new PageControls();
    UserMasterPresenter objUserMasterPresenter;
    DataRow DR = null;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    #endregion

    #region ControlsValue
    public string NonEmpUserName
    {
        set { txt_NonEmpUserName.Text = value; }
        get { return txt_NonEmpUserName.Text; }
    }
    public int ProfileId
    {
        set { ddl_profile.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_profile.SelectedValue); }
    }

    public int BranchId
    {
        set { ddl_branch.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_branch.SelectedValue); }
    }

    public bool IsActive
    {
        set{chk_IsActive.Checked = Convert.ToBoolean(value);}
        get{return Convert.ToBoolean(chk_IsActive.Checked);}
    }
    public IAddressView AddressView
    {
        get { return (IAddressView)WucAddress1; }
    }
    #endregion

    #region ControlsBind

    #endregion

    public DataSet BindProfile
    {
        set
        {
            ddl_profile.DataSource = value;
            ddl_profile.DataTextField = "Profile_Name";
            ddl_profile.DataValueField = "Profile_Id";
            ddl_profile.DataBind();           

            Raj.EC.Common.InsertItem(ddl_profile);
        }
    }

    public DataSet BindBranch
    {
        set
        {
            ddl_branch.DataSource = value;
            ddl_branch.DataTextField = "Branch_Name";
            ddl_branch.DataValueField = "Branch_Id";
            ddl_branch.DataBind();

            Raj.EC.Common.InsertItem(ddl_branch);
        }
    }

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if (NonEmpUserName == string.Empty)
        {
            errorMessage = "Please Enter User Name";
            txt_NonEmpUserName.Focus();
            _isValid = false;
        }
        else if (ProfileId <= 0)
        {
            errorMessage = "Please Select Profile";
            ddl_profile.Focus();
            _isValid = false;
        }
        else if (WucAddress1.MobileNo.Trim().Length != 10)
        {
            errorMessage = "Please Enter Proper Mobile No.";
            _isValid = false;
        }
        else if (BranchId  <= 0)
        {
            errorMessage = "Please Select Branch";
            ddl_branch.Focus();
            _isValid = false;
        }
        else
        {
            return _isValid = true;
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
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //return -1;
        }
    }

    #endregion
     #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            pc.AddAttributes(this.Controls);
            Raj.EC.Common ObjCommon = new Raj.EC.Common();
            if (keyID > 0)
            {
                TR_IsActive.Visible = true;
            }

        }
     
        objUserMasterPresenter = new UserMasterPresenter(this, IsPostBack);
        WucAddress1.SetTDCaptionWidth = "20%";
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objUserMasterPresenter.Save();   
                   
    }

     #endregion
}




