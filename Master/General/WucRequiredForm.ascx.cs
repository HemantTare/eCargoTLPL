using System;
using System.Data;
using System.Web.UI.WebControls;

using Raj.EC.MasterPresenter;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.MasterView;


public partial class Master_General_WucForm : System.Web.UI.UserControl, IRequiredFormView 
{
    private RequiredFormPresenter  _RequiredFormPresenter;
    bool isValid = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        _RequiredFormPresenter = new RequiredFormPresenter(this, IsPostBack);
    }



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
        }
    }

    #endregion


    #region InitInterface

    public string FormName
    {
        set
        {
            txt_FormName.Text = value;
        }
        get
        {
            return txt_FormName.Text;
        }
    }

    public string Description
    {
        set
        {
            txt_Description.Text = value;
        }
        get
        {
            return txt_Description.Text;
        }
    }

    #endregion

    #region Function
    
    public bool validateUI()
    {
        isValid = false;

        errorMessage = "";

        if (txt_FormName.Text.Trim() == String.Empty)
        {
            errorMessage = "Please Enter Form Name.";
            txt_FormName.Focus();
        }
       
        else
        {
            isValid = true;
        }

        return isValid;

    }

    #endregion


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _RequiredFormPresenter.save();
    }
}
