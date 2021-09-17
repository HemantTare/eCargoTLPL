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
using Raj.EC.LoginView;
using Raj.EC.LoginPresenter;
using ClassLibraryMVP;

public partial class Login_WucLoginChangePwd : System.Web.UI.UserControl, ILoginChangePwdView  
{

    private LoginChangePwdPresenters _LoginChangePwdPresenters;

    #region initInterface

    public int keyID
    {
        get { return UserManager.getUserParam().UserId; }
    }

    public string Login
    {
        get { return lbl_Login.Text.ToUpper(); }
        set { lbl_Login.Text = value.ToUpper(); }
    }

    public string NewPassword
    {
        get { return txt_New_Password.Text.Trim(); }
    }

    public string ConfirmPassword
    {
        get { return txt_Confirm_Password.Text.Trim(); }
    }

    public bool validateUI()
    {
        if (NewPassword == "")
        {
            errorMessage = "Please Enter New Password";
            return false;
        }
        else if (ConfirmPassword == "")
        {
            errorMessage = "Please Enter Confirm Password";
            return false;
        }
        else if (NewPassword == Login)
        {
            errorMessage = "Please choose different password!";
            return false;
        }
        else if (NewPassword != ConfirmPassword)
        {
            errorMessage = "New password and confirm password do not match!";
            return false;
        }

        return true;
    }

    public string errorMessage
    {
        set { lbl_Client_Error.Text = value; }
    }

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        _LoginChangePwdPresenters  = new LoginChangePwdPresenters(this, IsPostBack);
        txt_New_Password.Focus();
    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
       

        if (validateUI())
        {
            _LoginChangePwdPresenters.Save();
            Response.Redirect("~/Display/frmDisplay.aspx");
        }
    }
}
