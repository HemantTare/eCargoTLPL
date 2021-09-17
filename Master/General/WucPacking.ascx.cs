using System;
using System.Data;
using System.Web.UI.WebControls;

using Raj.EC.MasterView;
using Raj.EC.MasterPresenter;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;


public partial class Master_General_WucPacking : System.Web.UI.UserControl, IPackingView
{
    private PackingPresenter _PackingPresenter;
    bool isValid = false;

    #region IView Members

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
    public string PackingType
    {
        set
        {
            txt_PackingType.Text = value;
        }
        get
        {
            return txt_PackingType.Text;
        }
    }

    #endregion

    public bool validateUI()
    {
        isValid = false;

        errorMessage = "";

        if (txt_PackingType.Text.Trim() == String.Empty)
        {
            errorMessage = "Please Enter Packing Type";
            sm_PackingType.SetFocus(txt_PackingType);
        }
        else
        {
            isValid = true;
        }

        return isValid;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        _PackingPresenter = new PackingPresenter(this, IsPostBack);
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _PackingPresenter.save();
    }
}