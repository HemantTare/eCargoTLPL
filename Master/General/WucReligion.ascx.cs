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
/// Author :<Nilesh Kumar Jha>
/// Created On:<22/04/2008>
/// Description :This page is for Religion Add and Edit
/// </summary>

public partial class Master_Vehicle_WucReligion : System.Web.UI.UserControl,IReligionView 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Common ObjCommon = new Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Fleet/Master/General/App_LocalResources/WucReligion.ascx.resx");
        //}
        objReligionPresenter = new ReligionPresenter(this, IsPostBack);
    }
    #region "ClassVariables"
    ReligionPresenter objReligionPresenter;
    #endregion
    #region "ControlsVariables"
    public string Religion
    {
        set
        {
            txt_Religion.Text = value;
        }
        get
        {
            return txt_Religion.Text;
        }
    }
    #endregion
    #region Iview
    public bool validateUI()
    {
        bool _isValid = false;
        if (txt_Religion.Text == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Religion";// GetLocalResourceObject("Msg_txt_Religion").ToString();
            txt_Religion.Focus();
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
            return Util.DecryptToInt(Request.QueryString["id"]);
            //return -1;
        }
    }
#endregion


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objReligionPresenter.Save();
    }
}
