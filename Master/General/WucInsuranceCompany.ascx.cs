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
using Raj.EF.MasterPresenter;
using Raj.EF.MasterView;
using Raj.EC;


/// <summary>
/// Author        : Ashish Lad
/// Created On    : 21/04/2008
/// Description   : This is the Form  For Master Vehicle Type
/// </summary>

public partial class Master_Vehicle_WucInsuranceCompany : System.Web.UI.UserControl,IInsuranceCompanyView
{
    #region ClassVariables
    InsuranceCompanyPresenter objInsuranceCompanyPresenter; 
    #endregion

    #region ControlsValue
    public string InsuranceCompanyName
    {
        set
        {
            txt_Insurance_Company_Name.Text = value;
        }
        get
        {
            return txt_Insurance_Company_Name.Text;
        }
    }
    #endregion

    #region ControlsBind

    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        if (txt_Insurance_Company_Name.Text.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please Enter Insurance Company Name";// GetLocalResourceObject("Msg_txt_Insurance_Company_Name").ToString();
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
            return Util.DecryptToInt(Request.QueryString["Id"]); 
           // return -1;
        }
    }

    #endregion


    #region OtherProperties

    #endregion


    #region OtherMethods

    #endregion
    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Common ObjCommon = new Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Fleet/Master/General/App_LocalResources/WucInsuranceCompany.ascx.resx");
        //}
        objInsuranceCompanyPresenter = new InsuranceCompanyPresenter(this, IsPostBack);
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objInsuranceCompanyPresenter.Save();
    }
    #endregion
}
