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
using ClassLibraryMVP.General;
using Raj.EC.MasterView;
using Raj.EC.MasterPresenter;
using ClassLibraryMVP;

/// <summary>
/// author Krutarth
/// created on 17th oct 08
/// Master for Hold On Reason
/// </summary>
/// 


public partial class Master_General_WucHoldOnReason : System.Web.UI.UserControl,IHoldOnReasonView
{
    HoldOnReasonPresenter objHoldOnReasonPresenter;

    public string HoldOnReason
    {
        set { txt_Hold_On_Reason.Text = value; }
        get { return txt_Hold_On_Reason.Text; }
    }
    public bool validateUI()
    {
        bool _isValid = false;
        if (HoldOnReason == string.Empty)
        {
            errorMessage = "Please Enter Hold On Reason"; // GetLocalResourceObject("Msg_HoldOnReason").ToString();
            txt_Hold_On_Reason.Focus();
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
            return Util.DecryptToInt(Request.QueryString["Id"]);        
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        objHoldOnReasonPresenter = new HoldOnReasonPresenter(this, IsPostBack);
        //if (!IsPostBack)
        //{
        //    Raj.EC.Common objCommon = new Raj.EC.Common();
        //    hdn_Resource.Value=objCommon.GetResourceString("Master/General/App_LocalResources/WucHoldOnReason.ascx.resx");
        //}
        

    }

    protected void Btn_Save_Click(object sender, EventArgs e)
    {
        objHoldOnReasonPresenter.Save();
    }
}
