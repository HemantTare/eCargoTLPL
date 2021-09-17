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
using Raj.EF.MasterView;
using Raj.EF.MasterPresenter;
using Raj.EC;



/// <summary>
/// author : Shiv kumar mishra
/// created on : 22 apr 08
/// this form is use for adding/editing for Bank Master
/// </summary>
/// 
public partial class Master_Geo_WucBank : System.Web.UI.UserControl,IBankView 
{
#region ClassVariables
    BankPresenter objIBankPresenter;
#endregion

#region ControlsValues
    public string BankName
    {
        set { txt_Bank.Text = value; }
        get { return txt_Bank.Text; }
    }

    public string AccountNo
    {
        set { txt_AccountNo.Text = value; }
        get { return txt_AccountNo.Text; }
    }


    public string Comments
    {
        set { txt_Comments.Text = value; }
        get { return txt_Comments.Text; }
    }

   
 #endregion

 #region IView
    public bool validateUI()
    {
        bool _isValid = true;

        if (txt_Bank.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Bank Name";// GetLocalResourceObject("Msg_txt_Bank").ToString(); 
            txt_Bank.Focus();
            _isValid = false;
        }
        else if (txt_Comments.Text == string.Empty)
        {
            errorMessage = "Please Enter Comments";// GetLocalResourceObject("Msg_txt_Comments").ToString();
            txt_Comments.Focus();
            _isValid = false;
        }
       
        return _isValid;
    }

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    #endregion

 #region ControlEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Common ObjCommon = new Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Fleet/Master/General/App_LocalResources/WucBank.ascx.resx");
        //}
        objIBankPresenter = new BankPresenter(this, IsPostBack);
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objIBankPresenter.Save();
    }
    #endregion
}
