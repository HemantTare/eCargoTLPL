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
using Raj.EC.ControlsView;
using Raj.EF.MasterPresenter;
using Raj.EF.TransactionsView;

/// <summary>
/// author pankaj
/// created on 3rd may 08
/// this is main user control in which other 6 user control of Vehicles are kept
/// </summary>
/// 
public partial class Master_Vehicle_WucVehicle : System.Web.UI.UserControl,IVehicleView
{
    VehiclePresenter objVehiclePresenter;
    PageControls pc = new PageControls();
    string _callFrom = "";
    Raj.EC.Common objComm = new Raj.EC.Common();
    string Mode = "0";

    #region IView Implementation
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
        //get { return 128; }
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public string CallFrom 
    {
        get { return _callFrom; }
    }
    private int VehicleCategoryId
    {
        get { return Util.String2Int(hdn_Vehicle_Category_ID.Value); }
        set { hdn_Vehicle_Category_ID.Value = Util.Int2String(value); }
    }

    public IVehicleInformationView VehicleInformationView
    {
        get { return (IVehicleInformationView)WucVehicleInformation1; }
    }

    public IEngineBodySpecificationView EngineBodySpecificationView
    {
        get { return (IEngineBodySpecificationView)WucEngineBodySpecification1; }
    }

    public IVehicleLoanDetailsView VehicleLoanDetailsView
    {
        get { return (IVehicleLoanDetailsView)WucVehicleLoanDetails1; }
    }

    public IVehicleChasisTyresView VehicleChasisTyresView
    {
        get { return (IVehicleChasisTyresView)WucVehicleChasisTyres1; }
    }

    public IRegistrationFitnessView RegistrationFitnessView
    {
        get { return (IRegistrationFitnessView)WucRegistrationFitness1; }
    }

    public IRegistrationPermitView RegistrationPermitView
    {
        get { return (IRegistrationPermitView)WucRegistrationPermit1; }
    }

    public IVehicleHireDetailsView VehicleHireDetailsView
    {
        get { return (IVehicleHireDetailsView)WucVehicleHireDetails1; }
    }

    public IAttachmentsView AttachmentsView
    {
        get { return (IAttachmentsView)WucAttachments1; }
    }

    public IVehicleInsurancePremiumView VehicleInsurancePremiumView
    {
        get { return (IVehicleInsurancePremiumView)WucVehicleInsurancePremium1; }
    }

    //Added : Anita On : 18 Feb 09
    public void ClearVariables()
    {
        WucEngineBodySpecification1.SessionEngineBodyGrid = null;
        
        WucVehicleLoanDetails1.SesssionPaymentDetailsDT = null;
        WucVehicleLoanDetails1.SesssionBankNameDT = null;
        
        WucVehicleChasisTyres1.SessionChasisTyresGrid = null;
        WucVehicleChasisTyres1.SessionDualType = null;
        
        WucRegistrationPermit1.SessionStateDropDown = null;
        WucRegistrationPermit1.SessionPermitTaxDetails = null;
        WucRegistrationPermit1.SessionTemparayRegistrationPermitGrid = null;
        
        WucVehicleInsurancePremium1.SessionPremiumDetailsGrid = null;
        WucVehicleInsurancePremium1.SessionPremiumTypeDropdown = null;
        
        WucAttachments1.SessionAttachmentsGrid = null;
    }

    #endregion

    #region Validation
    public bool validateUI()
    {
        bool IsValid = false;

        if (WucVehicleInformation1.validateUI() == false)
        {
            MP_Vehicle.SelectedIndex = 0;
            TB_Vehicle.SelectedTab = TB_Vehicle.Tabs[0];
        }
        else if (WucEngineBodySpecification1.validateUI() == false)
        {
            MP_Vehicle.SelectedIndex = 1;
            TB_Vehicle.SelectedTab = TB_Vehicle.Tabs[1];
        }
        else if (VehicleCategoryId != 5 && WucVehicleLoanDetails1.validateUI() == false)
        {
            MP_Vehicle.SelectedIndex = 2;
            TB_Vehicle.SelectedTab = TB_Vehicle.Tabs[2];
        }

        else if (VehicleCategoryId != 5 && WucVehicleChasisTyres1.validateUI() == false)
        {
            MP_Vehicle.SelectedIndex = 3;
            TB_Vehicle.SelectedTab = TB_Vehicle.Tabs[3];
        }
        else if (VehicleCategoryId != 5 && WucRegistrationFitness1.validateUI() == false)
        {
            MP_Vehicle.SelectedIndex = 4;
            TB_Vehicle.SelectedTab = TB_Vehicle.Tabs[4];
        }
        else if (VehicleCategoryId != 5 && WucRegistrationPermit1.validateUI() == false)
        {
            MP_Vehicle.SelectedIndex = 5;
            TB_Vehicle.SelectedTab = TB_Vehicle.Tabs[5];
        }
        else if (VehicleCategoryId != 5 && WucVehicleInsurancePremium1.validateUI() == false)
        {
            MP_Vehicle.SelectedIndex = 6;
            TB_Vehicle.SelectedTab = TB_Vehicle.Tabs[6];
        }
        else if (VehicleCategoryId != 5 && WucVehicleHireDetails1.validateUI() == false)
        {
            MP_Vehicle.SelectedIndex = 7;
            TB_Vehicle.SelectedTab = TB_Vehicle.Tabs[7];
        }
        
        else
        {
            IsValid = true;
        }
        //lbl_Errors.Visible = (!IsValid);
        return IsValid;
    }
    #endregion

    #region Events
    protected void Page_PreRender(object sender, EventArgs e)
    {
        if (Mode == "4")
        {
            btn_Close.Visible = true;
            btn_Close.Enabled = true;
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        btn_Save.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save, btn_Save_Exit, btn_Close));
        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Save, btn_Close));
        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        objVehiclePresenter = new VehiclePresenter(this, IsPostBack);

        btn_null_sessions.Visible = false;

        WucVehicleInformation1.SetScriptManager = SC_Vehicle;
        WucEngineBodySpecification1.SetScriptManager = SC_Vehicle;
        WucVehicleLoanDetails1.SetScriptManager = SC_Vehicle;
        WucVehicleChasisTyres1.SetScriptManager = SC_Vehicle;
        WucRegistrationFitness1.SetScriptManager = SC_Vehicle;
        WucRegistrationPermit1.SetScriptManager = SC_Vehicle;
        WucVehicleHireDetails1.SetScriptManager = SC_Vehicle;
        WucAttachments1.SetScriptManager = SC_Vehicle;
        WucVehicleInsurancePremium1.SetScriptManager = SC_Vehicle;

        if (!IsPostBack)
        {
            pc.AddAttributes(this.Controls);
            VehicleCategoryId = Util.String2Int(StateManager.GetState<string>("QueryString"));
           
            WucAttachments1.AttachmentFormId = 1;

            if (VehicleCategoryId == 5)
            {
                TB_Vehicle.Tabs[2].Visible = false;
                TB_Vehicle.Tabs[3].Visible = false;
                TB_Vehicle.Tabs[4].Visible = false;
                TB_Vehicle.Tabs[5].Visible = false;
                TB_Vehicle.Tabs[6].Visible = false;
                TB_Vehicle.Tabs[7].Visible = false;
            }
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        _callFrom = "SaveAndNew";
        objVehiclePresenter.Save();
    }
    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        _callFrom = "SaveAndExit";
        objVehiclePresenter.Save();
    }
    protected void btn_Close_Click(object sender, EventArgs e)
    {
        ClearVariables();
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
    }
    #endregion
   
}
