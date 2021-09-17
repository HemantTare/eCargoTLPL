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


public partial class Master_Location_WucCompany : System.Web.UI.UserControl,ICompanyView
{
    CompanyPresenter objCompanyPresenter;


    # region ControlsValue
    public DataTable SessionBookingParametersGrid
    {
        get { return StateManager.GetState<DataTable>("BookingParametersGrid"); }
        set
        {
            for (int i = 0; i <= value.Rows.Count - 1; i++)
            {
                value.Rows[i]["SrNo"] = i;
            }

            StateManager.SaveState("BookingParametersGrid", value);
        }
    }

    public DataTable SessionDivision
    {
        get { return StateManager.GetState<DataTable>("DivisionDropDown"); }
        set { StateManager.SaveState("DivisionDropDown", value); }
    }

    public DataTable SessionBookingType
    {
        get { return StateManager.GetState<DataTable>("BookingTypeDropDown"); }
        set
        {

            StateManager.SaveState("BookingTypeDropDown", value);

        }
    }
    public DataTable SessionPaymentType
    {
        get { return StateManager.GetState<DataTable>("PaymentTypeDropDown"); }
        set { StateManager.SaveState("PaymentTypeDropDown", value); }
    }

    public DataTable SessionTripHireParametersGrid
    {
        get { return StateManager.GetState<DataTable>("TripHireParametersGrid"); }
        set
        {
            for (int i = 0; i <= value.Rows.Count - 1; i++)
            {
                value.Rows[i]["SrNo"] = i;
            }

            StateManager.SaveState("TripHireParametersGrid", value);
        }
    }
    public DataTable SessionATHGrid
    {
        get { return StateManager.GetState<DataTable>("ATHGrid"); }
        set
        {
            for (int i = 0; i <= value.Rows.Count - 1; i++)
            {
                value.Rows[i]["SrNo"] = i;
            }

            StateManager.SaveState("ATHGrid", value);
        }
    }
    public DataTable SessionTripDivision
    {
        get { return StateManager.GetState<DataTable>("TripDivision"); }
        set { StateManager.SaveState("TripDivision", value); }
    }
    public DataTable SessionATHDivision
    {
        get { return StateManager.GetState<DataTable>("ATHDivision"); }
        set { StateManager.SaveState("ATHDivision", value); }
    }
    public DataTable SessionLHPONatureOfPayment
    {
        get { return StateManager.GetState<DataTable>("LHPONatureOfPayment"); }
        set { StateManager.SaveState("LHPONatureOfPayment", value); }
    }
    public DataTable SessionCompanyDeliveryGrid
    {
        get { return StateManager.GetState<DataTable>("CompanyDeliveryGrid"); }
        set
        {
            for (int i = 0; i <= value.Rows.Count - 1; i++)
            {
                value.Rows[i]["SrNo"] = i;
            }

            StateManager.SaveState("CompanyDeliveryGrid", value);
        }
    }
    public DataTable SessionLocalCollectionVoucherGrid
    {
        get { return StateManager.GetState<DataTable>("LocalCollection"); }
        set
        {
            for (int i = 0; i <= value.Rows.Count - 1; i++)
            {
                value.Rows[i]["SrNo"] = i;
            }
            StateManager.SaveState("LocalCollection", value);
        }
    }
    public DataTable SessionDoorDeliveryExpenseVoucherGrid
    {
        get { return StateManager.GetState<DataTable>("DoorDeliveryExpense"); }
        set
        {
            for (int i = 0; i <= value.Rows.Count - 1; i++)
            {
                value.Rows[i]["SrNo"] = i;
            }

            StateManager.SaveState("DoorDeliveryExpense", value);
        }
    }
    #endregion

    #region Validation
    public bool validateUI()
    {
       bool IsValid = false;

       if (WucCompanyGeneralDetails1.validateUI() == false)
       {
           MP_CompanyDetails.SelectedIndex = 0;
           TB_CompanyDetails.SelectedTab = TB_CompanyDetails.Tabs[0];

       }
       else if (WucCompanyTDSFBTDetails1.validateUI() == false)
       {
           MP_CompanyDetails.SelectedIndex = 1;
           TB_CompanyDetails.SelectedTab = TB_CompanyDetails.Tabs[1];

       }
       else if (WucCompanyParameters1.validateUI() == false)
       {
           MP_CompanyDetails.SelectedIndex = 2;
           TB_CompanyDetails.SelectedTab = TB_CompanyDetails.Tabs[2];

       }
       else if (WucCompanyBookingParameters1.validateUI() == false)
       {
           MP_CompanyDetails.SelectedIndex = 3;
           TB_CompanyDetails.SelectedTab = TB_CompanyDetails.Tabs[3];
       }
       else if (WucCompanyTripHireParameters1.validateUI() == false)
       {
           MP_CompanyDetails.SelectedIndex = 4;
           TB_CompanyDetails.SelectedTab = TB_CompanyDetails.Tabs[4];
       }
       else
       {
           IsValid = true;
       }
       return IsValid;
    }
    #endregion

    #region IView Implementation
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
        //get {return -1;}
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }

    public ICompanyGeneralDetailsView CompanyGeneralDetailsView
    {
        get { return (ICompanyGeneralDetailsView)WucCompanyGeneralDetails1; }
    }

    public ICompanyTDSFBTDetailsView CompanyTDSFBTDetailsView
    {
        get { return (ICompanyTDSFBTDetailsView)WucCompanyTDSFBTDetails1; }
    }

    public ICompanyParametersView CompanyParametersView
    {
        get { return (ICompanyParametersView)WucCompanyParameters1; }
    }
    public IBookingParametersView CompanyBookingParametersView
    {
        get { return (IBookingParametersView)WucCompanyBookingParameters1; }
    }
    public ICompanyDeliveryView CompanyDeliveryView
    {
        get { return (ICompanyDeliveryView)WucCompanyDelivery1; }
    }

    public ICompanyTripHireParametersView CompanyTripHireParametersView
    {
        get { return (ICompanyTripHireParametersView)WucCompanyTripHireParameters1; }
    }
    public ILocalCollectionVoucherView LocalCollectionVoucherView
    {
        get { return (ILocalCollectionVoucherView)WucLocalCollectionVoucher1; }
    }
    public ICompanyCaptionView CompanyCaptionView
    {
        get { return (ICompanyCaptionView)WucCompanyCaption1; }
    }
    //Added : Anita On: 18 Feb 09
    public void ClearVariables()
    {
        WucCompanyBookingParameters1.SessionAdvanceBookingIncomeDropDown = null;
        WucCompanyBookingParameters1.SessionBookingIncomeDropDown = null;
        WucCompanyBookingParameters1.SessionBookingType = null;
        WucCompanyBookingParameters1.SessionDivision = null;
        WucCompanyBookingParameters1.SessionPaymentType = null;
        WucCompanyBookingParameters1.SessionServiceTaxLedgerDropDown = null;
        WucCompanyBookingParameters1.SessionBookingParametersGrid=null;
        
        WucCompanyTripHireParameters1.SessionTripHireParametersGrid = null;
        WucCompanyTripHireParameters1.SessionATHGrid = null;
        WucCompanyTripHireParameters1.SessionDivision = null;
        WucCompanyTripHireParameters1.SessionLHPONatureOfPayment = null;
        WucCompanyTripHireParameters1.SessionTripHireParametersGrid = null;
        
        WucCompanyDelivery1.SessionCompanyDeliveryGrid = null;
        WucCompanyDelivery1.SessionBookingType = null;
        WucCompanyDelivery1.SessionDeliveryIncomeDropDown = null;
        WucCompanyDelivery1.SessionDivision = null;
        WucCompanyDelivery1.SessionOctroiReceivableDropDown = null;
        WucCompanyDelivery1.SessionServiceTaxLedgerDropDown = null;

        WucLocalCollectionVoucher1.SessionLocalCollectionVoucherGrid = null;
        WucLocalCollectionVoucher1.SessionDoorDeliveryExpenseVoucherGrid = null;        
    }
    
    protected void btn_null_session_Click(object sender, EventArgs e)
    {
        ClearVariables();
    }

    #endregion

    #region events
    protected void Page_Load(object sender, EventArgs e)
    {
        
        TB_CompanyDetails.SiteMapXmlFile = "~/XML/Location/CompanyDetails.xml";
        if (UserManager.getUserParam().HierarchyCode == "AD")
        {
            TB_CompanyDetails.Tabs[7].Visible = true;
        }
        else
        {
            TB_CompanyDetails.Tabs[7].Visible = false;
        }
        objCompanyPresenter = new CompanyPresenter(this, IsPostBack);
        WucCompanyGeneralDetails1.SetScriptManager = SC_Company;
        WucCompanyTDSFBTDetails1.SetScriptManager = SC_Company;
        WucCompanyParameters1.SetScriptManager = SC_Company;
        WucCompanyBookingParameters1.SetScriptManager = SC_Company;
        WucCompanyDelivery1.SetScriptManager = SC_Company;
        WucCompanyTripHireParameters1.SetScriptManager = SC_Company;
        WucLocalCollectionVoucher1.SetScriptManager = SC_Company;
        WucCompanyCaption1.SetScriptManager = SC_Company;

        btn_null_sessions.Visible = false;

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
       objCompanyPresenter.Save();     

    }
  
    #endregion
}
