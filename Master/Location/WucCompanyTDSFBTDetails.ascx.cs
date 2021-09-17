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
using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;
using System.Data.SqlClient;
using Raj.EC.ControlsView;
using Raj.EC;

public partial class Master_Location_WucCompanyTDSFBTDetails : System.Web.UI.UserControl,ICompanyTDSFBTDetailsView
{
    #region ClassVariable
    CompanyTDSFBTDetailsPresenter objCompanyTDSFBTDetailsPresenter;
    DataRow DR = null;
    private ScriptManager scm_CompanyTDSFBT;

    #endregion

    #region Controls Value

   
    public string TaxAssessmentNumber
    {
        set { txt_Tax_Assessment_Number.Text = value; }
        get { return txt_Tax_Assessment_Number.Text; }
    }
   

    public string IncomeTaxCircle
    {
        set { txt_Income_Tax_Ward.Text = value; }
        get { return txt_Income_Tax_Ward.Text; }
    }

    public string Designation
    {
        set { txt_Designation.Text = value; }
        get { return txt_Designation.Text; }
    }
    public string DeductorType
    {
        set { ddl_Deductor_Type.SelectedValue = value; }
        get { return ddl_Deductor_Type.SelectedValue; }
    }
   
    public int PersonResponsible
    {
        set { ddl_Person_Responsible.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Person_Responsible.SelectedValue); }
    }
    public bool IsAllowSelectionFBTCategory
    {
        set { Chk_Allow_Selection_FBT.Checked = value; }
        get { return Chk_Allow_Selection_FBT.Checked; }
    }
    public string PanNo
    {
        set { txt_Pan_No.Text = value; }
        get { return txt_Pan_No.Text; }
    }
    public int AssesseeType
    {
        set { ddl_Assessee_Type.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Assessee_Type.SelectedValue); }
    }
    public bool IsSurchargeApplicable
    {
        set { Chk_Is_Surcharge_Applicable.Checked = value; }
        get { return Chk_Is_Surcharge_Applicable.Checked; }
    }
    public int AssesseeCategory
    {
        set { ddl_Assessee_Category.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Assessee_Category.SelectedValue); }
    }


    #endregion



    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if (Util.String2Int(ddl_Person_Responsible.SelectedValue) == 0)
        {
            errorMessage = "Please select Name of the  Person Responsible";//GetLocalResourceObject("Msg_ddlEmployee").ToString();
            _isValid = false;
        }
        else if (txt_Tax_Assessment_Number.Text == string.Empty)
        {
            errorMessage = "Please Enter Tax Assessment Number";// GetLocalResourceObject("Msg_CompanyName").ToString();
            _isValid = false;
        }
        else if (Util.String2Int(ddl_Assessee_Type.SelectedValue) == 0)
        {
            errorMessage = "Please select Assessee Type";//GetLocalResourceObject("Msg_ddlAssesseeType").ToString();
            _isValid = false;
        }
        else if (Util.String2Int(ddl_Assessee_Category.SelectedValue) == 0)
        {
            errorMessage = "Please select Assessee Category";// GetLocalResourceObject("Msg_ddlAssesseeCategory").ToString();
            _isValid = false;
        }
        else if (Util.String2Int(ddl_Deductor_Type.SelectedValue) == 0)
        {
            errorMessage = "Please Select Deductor Type";
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
        //get { return Util.DecryptToInt(Request.QueryString["Id"]); }
        set { keyID = value; }
        get { return keyID; }
       

    }


    #endregion



    #region ControlsBind
    public DataTable BindPersonResponsible
    {
        set
        {
            ddl_Person_Responsible.DataTextField = "EmployeeName";
            ddl_Person_Responsible.DataValueField = "EMP_ID";
            ddl_Person_Responsible.DataSource = value;
            ddl_Person_Responsible.DataBind();
            ddl_Person_Responsible.Items.Insert(0, new ListItem("Select One", "0"));


        }
    }

    public DataTable BindAssesseeType
    {
        set
        {
            ddl_Assessee_Type.DataTextField = "Assessee_Type_Name";
            ddl_Assessee_Type.DataValueField = "Assessee_Type_Id";
            ddl_Assessee_Type.DataSource = value;
            ddl_Assessee_Type.DataBind();

            ddl_Assessee_Type.Items.Insert(0, new ListItem("Select One", "0"));


        }
    }

    public DataTable BindAssesseeCategory
    {
        set
        {
            ddl_Assessee_Category.DataTextField = "Assessee_Category_Name";
            ddl_Assessee_Category.DataValueField = "Assessee_Category_Id";
            ddl_Assessee_Category.DataSource = value;
            ddl_Assessee_Category.DataBind();

            ddl_Assessee_Category.Items.Insert(0, new ListItem("Select One", "0"));

        }
    }
    public DataTable BindDeducteeType
    {
        set
        {
            ddl_Deductor_Type.DataSource = value;
            ddl_Deductor_Type.DataTextField = "TDS_Deductee_Type_Name";
            ddl_Deductor_Type.DataValueField = "TDS_Deductee_Type_ID";
            ddl_Deductor_Type.DataBind();

            ddl_Deductor_Type.Items.Insert(0, new ListItem("Select One", "0"));
           
        }
    }


    #endregion

    #region OtherMethod

    public ScriptManager SetScriptManager
    {
        set { scm_CompanyTDSFBT = value; }
    }
    #endregion


     #region PageLoadEvents

    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Raj.EC.Common ObjCommon = new Raj.EC.Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/Location/App_LocalResources/WucCompanyTDSFBTDetails.ascx.resx");
        //}

        objCompanyTDSFBTDetailsPresenter = new CompanyTDSFBTDetailsPresenter(this, IsPostBack);
    }
     #endregion

}
