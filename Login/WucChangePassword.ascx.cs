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
using System.Text;
using System.Text.RegularExpressions;

public partial class Login_WucChangePassword : System.Web.UI.UserControl, IChangePasswordView
{
    #region ClassVariables
    private ChangePasswordPresenter objChangePasswordPresenter;
    #endregion
    #region ControlsValue
    public string Login
    {
        get { return lbl_Login.Text.ToUpper(); }
        set { lbl_Login.Text = value.ToUpper(); }
    }
    public string OldPassword
    {
        get { return txt_OldPassword.Text; }
    }
    public string NewPassword
    {
        get { return txt_NewPassword.Text; }
    }
    public string Msg
    {
        set { lbl_Error.Text = value; }
    }
    #endregion
    #region IView
    public string errorMessage
    {
        set { lbl_Error.Text = value; }
    }
    public int keyID
    {
        get { return UserManager.getUserParam().UserId; }
    }
    public int IsTrue
    {
        set { hdn_IsTrue.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_IsTrue.Value); }
    }

    public bool validateUI()
    {
        bool _isValid = false;
        if (OldPassword == string.Empty)
        {
            errorMessage = "Plase Enter Old Password!";
            txt_OldPassword.Focus();
        }
        else if (NewPassword == string.Empty)
        {
            errorMessage = "Please Enter New Password!";
            txt_NewPassword.Focus();
        }
        else if (txt_NewPassword.Text.Length < 6)
        {
            errorMessage = "Password should not be less than 6 Digit";
            txt_NewPassword.Focus();
        }
        //else if (!Regex.IsMatch(txt_NewPassword.Text, "(?=\\w*\\d)"))
        //{
        //    errorMessage = "Password Should contains atleast one Digit";
        //    txt_NewPassword.Focus();
        //}
        //else if (!Regex.IsMatch(txt_NewPassword.Text, "(?!^[0-9]*$)(?!^[a-zA-Z]* $)^([a-zA-Z0-9]{6,10})$"))
        //{
        //    errorMessage = "Password Should contains atleast one alphabet";
        //    txt_NewPassword.Focus();
        //}
        else if (txt_ConPassword.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Confirm Password!";
            txt_ConPassword.Focus();
        }

        else if (Login.ToUpper() == NewPassword.ToUpper())
        {
            errorMessage = "User Name And Password Should Not Be Same";
            txt_NewPassword.Focus();
        }
        else if (NewPassword.ToUpper() == OldPassword.ToUpper())
        {
            errorMessage = "Please Enter different password!";
            txt_NewPassword.Focus();
        }
        else if (NewPassword.ToUpper() != txt_ConPassword.Text.ToUpper())
        {
            errorMessage = "New Password and Confirm Password do not match!";
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    #endregion
    #region OtherMethod
    private void SaveCloseWindow()
    {
        //string strpath = "Display/frmDisplay.aspx";
        //System.Web.HttpContext.Current.Response.Write("<script language='javascript'> { alert('Saved Successfully');} </script>");

     
        //string strscript = "<scriptlanguage=javascript>window.top.close();</script>";
        //if (!Page.IsStartupScriptRegistered("clientScript"))
        //{
        //    Page.RegisterStartupScript("clientScript", strscript);
        //}

        //string Alert = "<script type='text/javascript'> {alert('Password Changed SuccessFully!')} </script>";
        //Page.RegisterStartupScript("Alert1", Alert);

        //string close = "<script language='javascript'> { self.close() }</script>";
        //Page.RegisterStartupScript("Alert11", close);

        string Alert = "<script type='text/javascript'> {alert('Password Changed SuccessFully!')} </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(btn_submit, typeof(string), "Alert1", Alert, false);

        string close = "<script language='javascript'> { self.close() }</script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(btn_submit, typeof(string), "Alert11", close, false);
    }
    #endregion
    #region ControlEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        objChangePasswordPresenter = new ChangePasswordPresenter(this, IsPostBack);

    }
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            objChangePasswordPresenter.Save();
            if (IsTrue == 0)
            {
                SaveCloseWindow();
            }
        }
    }
    #endregion
}
