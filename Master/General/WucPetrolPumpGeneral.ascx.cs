
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

using Raj.EC.MasterView;
using Raj.EC.MasterPresenter;
using System.Data.SqlClient;
using Raj.EC.ControlsView ;


public partial class EC_Master_WucPetrolPumpGeneral : System.Web.UI.UserControl, IPetrolPumpGeneralView
{
    #region ClassVariables
    PetrolPumpGeneralPresenter objPetrolPumpGeneralPresenter;
    PageControls pc = new PageControls();

    #endregion

    #region ControlsValues


    public String ContactPerson
    {
        set
        {
            txt_ContactPerson.Text = value;
        }
        get
        {
            return txt_ContactPerson.Text;
        }
    }

    public String PetrolPumpName
    {
        set
        {
            txt_PetrolPumpName.Text = value;
        }
        get
        {
            return txt_PetrolPumpName.Text;
        }
    }


    public String MailingName
    {
        set
        {
            txt_MailingName.Text = value;
        }
        get
        {
            return txt_MailingName.Text;
        }
    }

    public String PetrolCompany
    {
        set
        {
            txt_PetrolCompany.Text = value;
        }
        get
        {
            return txt_PetrolCompany.Text;
        }
    }

    public String CSTNo
    {
        set
        {
            txt_CSTNo.Text = value;
        }
        get
        {
            return txt_CSTNo.Text;
        }
    }

    public String TINNo
    {
        set
        {
            txt_TINNo.Text = value;
        }
        get
        {
            return txt_TINNo.Text;
        }
    }

    public IAddressView AddressView
    {
        get { return (IAddressView)WucAddress1; }
    }

    #endregion


    #region IView
    public bool validateUI()
    {
        Double HO_Amt;
        Boolean ValidateBeforeSave = false;
        ValidateBeforeSave = false;
        //  String errorMsg = "";

        errorMessage = "";

        if (txt_PetrolPumpName.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Petrol Pump Name.";
            txt_PetrolPumpName.Focus();
            //ValidateBeforeSave = true;
        }
        else if (txt_MailingName.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Mailing Name.";
            txt_MailingName.Focus();
            //ValidateBeforeSave = true;
        }
        else if (txt_PetrolCompany.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Petrol Company.";
            txt_PetrolCompany.Focus();
            //ValidateBeforeSave = true;
        }
        else if (txt_ContactPerson.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Contact Person Name.";
            txt_ContactPerson.Focus();
            //ValidateBeforeSave = true;
        }
        else if (!WucAddress1.ValidateWucAddress(lbl_Errors))
        {

        }
        else if (txt_CSTNo.Text.Trim() == string.Empty && pc.Control_Is_Mandatory(txt_CSTNo))  //added : Ankit champaneriya: 02-01-09: 5.30 pm
        {
            errorMessage = "Please Enter CST No.";
            txt_CSTNo.Focus();
            //ValidateBeforeSave = true;
        }
        else if (txt_TINNo.Text.Trim() == string.Empty && pc.Control_Is_Mandatory(txt_TINNo))
        {
            errorMessage = "Please Enter TIN No.";
            txt_TINNo.Focus();
            //ValidateBeforeSave = true;
        }
        else
        {
            ValidateBeforeSave = true;
        }

        return ValidateBeforeSave;
        // return (ValidityBeforeSave( ));
    }


    public string errorMessage
    {
        set
        {
            //errorMessage = value;
            lbl_Errors.Text = value;
        }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //return 42009;
        }
    }

    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        objPetrolPumpGeneralPresenter = new PetrolPumpGeneralPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            if (keyID > 0)
            {
                txt_PetrolPumpName.Focus();
            }
        }
    }


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        errorMessage = "";

        if (validateUI())
        {
            errorMessage = "";

            objPetrolPumpGeneralPresenter.save();
            //    Response.Redirect("~/Display/frm_DataGrid.aspx?id=116");             
        }
        else
        {

        }
    }


    private Boolean ValidityBeforeSave()
    {

        Double HO_Amt;
        Boolean ValidateBeforeSave = false;
        ValidateBeforeSave = false;
        //  String errorMsg = "";

        errorMessage = "";

        if (txt_PetrolPumpName.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Petrol Pump Name.";
            txt_PetrolPumpName.Focus();
            //ValidateBeforeSave = true;

        }

        else if (txt_MailingName.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Mailing Name.";
            txt_MailingName.Focus();
            //ValidateBeforeSave = true;

        }

        else if (txt_PetrolCompany.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Petrol Company.";
            txt_PetrolCompany.Focus();
            //ValidateBeforeSave = true;

        }

        else if (txt_ContactPerson.Text.Trim() == string.Empty)
        {
            errorMessage = "Please Enter Contact Person Name.";
            txt_ContactPerson.Focus();
            //ValidateBeforeSave = true;

        }
        else if (txt_CSTNo.Text.Trim() == string.Empty )
        {
            errorMessage = "Please Enter CST No.";
            txt_CSTNo.Focus();
            //ValidateBeforeSave = true;

        }
        else if (txt_TINNo.Text.Trim() == string.Empty )
        {
            errorMessage = "Please Enter TIN No.";
            txt_TINNo.Focus();
            //ValidateBeforeSave = true;
        }
        else
        {
            ValidateBeforeSave = true;
        }





        return ValidateBeforeSave;
    }









    #endregion
}
