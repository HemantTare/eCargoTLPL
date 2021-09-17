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
using Raj.EF;
using Raj.EC;


/// <summary>
/// Author        :  Nilesh Kumar Jha
/// Created Date  :  22/05/2008
/// Description   :  This is the form for Registration Fitness 
/// </summary>
/// 
public partial class Master_Vehicle_WucRegistrationFitness : System.Web.UI.UserControl, IRegistrationFitnessView
{
    #region ClassVariables
    private RegistrationFitnessPresenter objRegistrationFitnessPresenter;
    private ScriptManager scm_VehicleRegistrationFitness;
    #endregion

    public ScriptManager SetScriptManager
    {
        set { scm_VehicleRegistrationFitness = value; }
    }

    #region ControlValues
    public DateTime RegistrationDate
    {
        get { return DtpRegistrationDate.SelectedDate; }
        set { DtpRegistrationDate.SelectedDate = value; }
    }
    public string RegistraionCertificateNo
    {
        get { return txt_RegistrationCertificateNo.Text; }
        set { txt_RegistrationCertificateNo.Text = value; }
    }
    public int RegistrationStateID
    {
        set { ddl_RegistrationState.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_RegistrationState.SelectedValue); }
    }
    public int RegistraionRtoId
    {
        get { return Util.String2Int(ddl_RTO.SelectedValue); }
        set { ddl_RTO.SelectedValue = Util.Int2String(value); }
    }
    public decimal RegistrationFee
    {
        get { return Util.String2Decimal(txt_RegistrationFee.Text); }
        set { txt_RegistrationFee.Text = Util.Decimal2String(value); }
    }
    public string FitnesCertificateNo
    {
        get { return txt_FitnessCertificateNo.Text; }
        set { txt_FitnessCertificateNo.Text = value; }
    }
    public int FitnessRtoId
    {
        get { return Util.String2Int(ddl_FitnessRTO.SelectedValue); }
        set { ddl_FitnessRTO.SelectedValue = Util.Int2String(value); }
    }
    public decimal Amount
    {
        get { return Util.String2Decimal(txt_Amount.Text); }
        set { txt_Amount.Text = Util.Decimal2String(value); }
    }
    public DateTime IssueDate
    {
        get { return DtpIssueDate.SelectedDate; }
        set { DtpIssueDate.SelectedDate = value; }
    }
    public DateTime ValidUpTO
    {
        get { return DtpValidUpTo.SelectedDate; }
        set { DtpValidUpTo.SelectedDate = value; }
    }



    #endregion

    #region ControlBinds
    public DataTable Fill_DDL_RegistrationState
    {
        set
        {
            ddl_RegistrationState.DataSource = value;
            ddl_RegistrationState.DataTextField = "State_Name";
            ddl_RegistrationState.DataValueField = "State_Id";
            ddl_RegistrationState.DataBind();
            ddl_RegistrationState.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable Fill_RTO
    {
        set
        {
            ddl_RTO.DataSource = value;
            ddl_RTO.DataTextField = "City_Name";
            ddl_RTO.DataValueField = "City_ID";
            ddl_RTO.DataBind();
            ddl_RTO.Items.Insert(0, new ListItem("Select One", "0"));

            ddl_FitnessRTO.DataSource = value;
            ddl_FitnessRTO.DataTextField = "City_Name";
            ddl_FitnessRTO.DataValueField = "City_Id";
            ddl_FitnessRTO.DataBind();
            ddl_FitnessRTO.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if (RegistraionCertificateNo == string.Empty)
        {
            errorMessage = "Please Enter Registration Certificate No";// GetLocalResourceObject("MsgRegistraionCertificateNo").ToString();
            scm_VehicleRegistrationFitness.SetFocus(txt_RegistrationCertificateNo);
        }
        else if (RegistrationStateID <= 0)
        {
            errorMessage = "Please Select Registration State";// GetLocalResourceObject("MsgRegistrationState").ToString();
            scm_VehicleRegistrationFitness.SetFocus(ddl_RegistrationState);
        }
        else if (RegistraionRtoId <= 0)
        {
            errorMessage = "Please Select Registraion RTO";// GetLocalResourceObject("MsgRegistraionRto").ToString();
            scm_VehicleRegistrationFitness.SetFocus(ddl_RTO);
        }
        else if (RegistrationFee <= 0 || txt_RegistrationFee.Text == string.Empty)
        {
            errorMessage = "Please Enter Registration Fee";// GetLocalResourceObject("MsgRegistrationFee").ToString();
            scm_VehicleRegistrationFitness.SetFocus(txt_RegistrationFee);
        }
        else if (FitnesCertificateNo == string.Empty)
        {
            errorMessage = "Please Enter Fitnes Certificate No";// GetLocalResourceObject("MsgFitnesCertificateNo").ToString();
            scm_VehicleRegistrationFitness.SetFocus(txt_FitnessCertificateNo);
        }
        else if (FitnessRtoId <= 0)
        {
            errorMessage = "Please Select Fitness RTO";// GetLocalResourceObject("MsgFitnessRto").ToString();
            scm_VehicleRegistrationFitness.SetFocus(ddl_FitnessRTO);
        }
        else if (Amount <= 0 || txt_Amount.Text == string.Empty)
        {
            errorMessage = "Please Enter Amount";// GetLocalResourceObject("MsgAmount").ToString();
            scm_VehicleRegistrationFitness.SetFocus(txt_Amount);
        }
        else if (IssueDate > ValidUpTO)
        {
            errorMessage = "Valid UpTo Should Be Greater Than Or Equal To Issue Date";// GetLocalResourceObject("MsgIssueDate").ToString();
        }
        //else if (Datemanager.IsValidProcessDate("VEH_RD", RegistrationDate) == false)
        //{
        //    errorMessage = "Invalid Registration Date";
        //}
        else if (RegistrationDate > DateTime.Now)  //Ankit
        {
            errorMessage = "Invalid Registration Date";
            DtpRegistrationDate.Focus();
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }
    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }

    }
    #endregion

    #region ControlsEvent
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (!IsPostBack)
        //{
        //    Common ObjCommon = new Common();
        //    hdf_ResourecString.Value = ObjCommon.GetResourceString("Fleet/Master/Vehicle/App_LocalResources/WucRegistrationFitness.ascx.resx");
        //}
        objRegistrationFitnessPresenter = new RegistrationFitnessPresenter(this, IsPostBack);
    }
    protected void ddl_RegistrationState_SelectedIndexChanged(object sender, EventArgs e)
    {
        objRegistrationFitnessPresenter.Fill_RTO();
    }
    #endregion

}
