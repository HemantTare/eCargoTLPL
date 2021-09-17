using System;
using System.Data;
using System.Web.UI.WebControls;
using Raj.EC.MasterView;
using Raj.EC.MasterPresenter;

using ClassLibraryMVP;
using ClassLibraryMVP.General;

/// Created : Ankit champanriya
/// Date    : 18/12/08 

public partial class Master_General_WucOtherChargesHead : System.Web.UI.UserControl, IGCOtherChargesHeadView
{
    private GCOtherChargesHeadPresenter objGCOtherChargesHeadPresenter;
    bool isValid = false;

    #region IView 

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //  return 34;
        }
    }
    #endregion

    #region InitInterface

    public string GC_Other_Charges_Head
    {
        set
        {
            txt_GCOtherChargesHead.Text = value;
        }
        get
        {
            return txt_GCOtherChargesHead.Text;
        }
    }

    #endregion

    #region Function

    public bool validateUI()
    {

        isValid = false;

        errorMessage = "";

        if (txt_GCOtherChargesHead.Text.Trim() == String.Empty)
        {
            errorMessage = "Please Enter OtherChargesHead";
            txt_GCOtherChargesHead.Focus();
        }
        else
        {
            isValid = true;
        }

        return isValid;

    }
    #endregion


    protected void Page_Load(object sender, EventArgs e)
    {
        objGCOtherChargesHeadPresenter = new GCOtherChargesHeadPresenter(this, IsPostBack);
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objGCOtherChargesHeadPresenter.save();
    }

}