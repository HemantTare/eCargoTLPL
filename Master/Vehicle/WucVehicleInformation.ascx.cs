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
using Raj.EC.ControlsView;
using Raj.EC.ControlsPresenter;

using Raj.EF;
using Raj.EC;

//using System.Data.SqlClient;
////using ClassLibrary;

public partial class Master_Vehicle_WucVehicleInformation : System.Web.UI.UserControl, IVehicleInformationView
{
    #region ClassVariables
    VehicleInformationPresenter objVehicleInformationPresenter;
    TDSAppPresenter objTDSAppPresenter;
    PageControls pc = new PageControls();

    Raj.EC.Common ObjCommon = new Raj.EC.Common();

    private ScriptManager _scmVehicleInformation;
    Common objCommon = new Common();
    #endregion

    #region Iview Implementaion
    public string VehicleNo
    {
        get { return txt_Number_Part1.Text + txt_Number_Part2.Text + txt_Number_Part3.Text + txt_Number_Part4.Text; }
    }
    public string NumberPart1
    {
        set { txt_Number_Part1.Text = value; }
        get { return txt_Number_Part1.Text; }
    }
    public string NumberPart2
    {
        set { txt_Number_Part2.Text = value; }
        get { return txt_Number_Part2.Text; }
    }
    public string NumberPart3
    {
        set { txt_Number_Part3.Text = value; }
        get { return txt_Number_Part3.Text; }
    }
    public string NumberPart4
    {
        set { txt_Number_Part4.Text = value; }
        get { return txt_Number_Part4.Text; }
    }
    public DateTime OnRoadDate
    {
        set { Wuc_DatePickerRoadDate.SelectedDate = value; }
        get { return Wuc_DatePickerRoadDate.SelectedDate; }
    }
    //public int BrokerId
    //{
    //    get { return Util.String2Int(ddl_Broker_Name.SelectedValue); }
    //}

    public int BrokerId
    {
        get { return Util.String2Int(hdn_BrokerId.Value); }
    }

    public string OwnerName
    {
        set { txt_Owner_Name.Text = value; }
        get { return txt_Owner_Name.Text; }
    }
    public int YearOfManufacture
    {
        set { txt_Yr_Manufacture.Text = Util.Int2String(value); }
        get { return Util.String2Int(txt_Yr_Manufacture.Text); }
    }
    public int DriverId1
    {
        get { return Util.String2Int(ddl_Driver_1.SelectedValue); }
    }
    public int DriverId2
    {
        get { return Util.String2Int(ddl_Driver_2.SelectedValue); }
    }

    public string DriverMobile1
    {
        set { txt_DriverMobile1.Text = value; }
        get { return txt_DriverMobile1.Text; }
    }

    public string  DriverMobile2
    {
        set { txt_DriverMobile2.Text = value; }
        get { return txt_DriverMobile2.Text; }
    }

    public int CleanerId
    {
        get { return Util.String2Int(ddl_Cleaner.SelectedValue); }
    }
    public string Notes
    {
        set { txt_Notes.Text = value; }
        get { return txt_Notes.Text; }
    }
    //public bool IsTdsApplicable
    //{
    //    set{Chk_Is_Tds.Checked = value;}
    //    get{return Convert.ToBoolean(Chk_Is_Tds.Checked);}
    //}
    //public int TdsId
    //{
    //    set{ddl_Tds_Category.SelectedValue = Util.Int2String(value);}
    //    get{return Util.String2Int(ddl_Tds_Category.SelectedValue);}
    //}
    public int VehicleTypeId
    {
        set { ddl_Vehicle_Type.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Vehicle_Type.SelectedValue); }
    }
    //public bool TDSCertificateForBroker
    //{ 
    //   set 
    //   { 
    //       rbl_TDSCertificate.Items[0].Selected=value;
    //       //rbl_TDSCertificate.Items[1].Selected = (!value);
    //   }

    //   get { return rbl_TDSCertificate.Items[0].Selected; }
    //}
    public bool TDSCertificateForOwner
    {
        set
        {
            rbl_TDSCertificate.Items[0].Selected = (!value);
            rbl_TDSCertificate.Items[1].Selected = value;
        }
        get
        {
            if (rbl_TDSCertificate.SelectedValue == "0")
                return false;
            else
                return true;
        }
    }
    public int VehicleBodyId
    {
        set { ddl_Vehicle_Body.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Vehicle_Body.SelectedValue); }
    }
    public int CarrierCategoryId
    {
        set { ddl_Carrier_Category.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Carrier_Category.SelectedValue); }
    }
    public int ManufacturerId
    {
        set { ddl_Manufacturer.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Manufacturer.SelectedValue); }
    }
    public int VehicleModelId
    {
        set { ddl_Model.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_Model.SelectedValue); }

    }
    //public decimal TdsExemptionLimit
    //{
    //    set{txt_Tds_Exemption_Limit.Text = Util.Decimal2String(value);}
    //    get{return Util.String2Decimal(txt_Tds_Exemption_Limit.Text);}
    //}
    public string GPSConnectivityId
    {
        set { txt_GPS_Con_Id.Text = value; }
        get { return txt_GPS_Con_Id.Text; }
    }
    public int VehicleCategoryId
    {
        get { return Util.String2Int(hdn_Vehicle_Category_ID.Value); }
        set { hdn_Vehicle_Category_ID.Value = Util.Int2String(value); }
    }
    public int OpenOdometer
    {
        set
        {
            txt_opening_Odometer.Text = Util.Int2String(value);
            hdn_Old_Open_Odometer.Value = Util.Int2String(value);
        }
        get { return Util.String2Int(txt_opening_Odometer.Text); }
    }

    public int OldCurrentOdometer
    {
        set { hdn_Old_Curr_Odometer.Value = Util.Int2String(value); }
    }

    public int CurrentOdometer
    {
        set
        {
            lbl_Current_Odometer.Text = Util.Int2String(value);
            hdn_Curr_Odometer.Value = Util.Int2String(value);
        }
        get { return Util.String2Int(hdn_Curr_Odometer.Value); }
    }

    //public decimal TdsRatePercent
    //{
    //    set{txt_Tds_Rate_Percent.Text = Util.Decimal2String(value);}
    //    get{return Util.String2Decimal(txt_Tds_Rate_Percent.Text);}
    //}
    public string DriverName1
    {
        get { return ddl_Driver_1.SelectedText; }
    }
    public string DriverName2
    {
        get { return ddl_Driver_2.SelectedText; }
    }
    public IAddressView AddressView
    {
        get { return (IAddressView)WucAddress1; }
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }
    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //return 128;
        }
    }

    public void SetDriver1Id(string text, string value)
    {
        ddl_Driver_1.DataTextField = "Driver_Name";
        ddl_Driver_1.DataValueField = "Driver_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Driver_1);
    }

    public void SetDriver2Id(string text, string value)
    {
        ddl_Driver_2.DataTextField = "Driver_Name";
        ddl_Driver_2.DataValueField = "Driver_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Driver_2);
    }
    public void SetCleanerId(string text, string value)
    {
        ddl_Cleaner.DataTextField = "CleanerName";
        ddl_Cleaner.DataValueField = "Cleaner_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Cleaner);
    }
    public void SetVendorId(string text, string value)
    {
        ddl_Broker_Name.DataTextField = "VendorName";
        ddl_Broker_Name.DataValueField = "Vendor_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Broker_Name);
        hdn_BrokerId.Value = value;
    }

    public ITDSAppView TDSAppView
    {
        get { return (ITDSAppView)WucTDSApp1; }
    }
    #endregion

    #region Iview Implementaion dropdown and grid
    //public DataTable BindTds
    //{
    //    set
    //    {
    //        ddl_Tds_Category.DataTextField = "TDS_Name";
    //        ddl_Tds_Category.DataValueField = "TDS_ID";
    //        ddl_Tds_Category.DataSource = value;
    //        ddl_Tds_Category.DataBind();
    //        ddl_Tds_Category.Items.Insert(0, new ListItem("Select One", "0"));
    //    }
    //}

    protected void ddl_Driver_1_TxtChange(object sender, EventArgs e)
    {
        BindDriver_1_MobileNos();
    }


    public void BindDriver_1_MobileNos()
    {


        string query = "select Mobile_No as Mobile_No1,Phone_No as Mobile_No2 from EF_Master_Driver Where Driver_ID = " + Util.Int2String(DriverId1);
        DataSet ds = new DataSet();
        ds = ObjCommon.EC_Common_Pass_Query(query);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = ds.Tables[0].Rows[0];

            txt_DriverMobile1.Text = objDR["Mobile_No1"].ToString();
            txt_DriverMobile2.Text = objDR["Mobile_No2"].ToString();
            
        }
        else
        {
            txt_DriverMobile1.Text = "";
            txt_DriverMobile2.Text = "";
        }

    }

    public DataTable BindVehicleType
    {
        set
        {
            ddl_Vehicle_Type.DataTextField = "Vehicle_Type";
            ddl_Vehicle_Type.DataValueField = "Vehicle_Type_ID";
            ddl_Vehicle_Type.DataSource = value;
            ddl_Vehicle_Type.DataBind();
            ddl_Vehicle_Type.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable BindVehicleBody
    {
        set
        {
            ddl_Vehicle_Body.DataTextField = "Vehicle_Body";
            ddl_Vehicle_Body.DataValueField = "Vehicle_Body_ID";
            ddl_Vehicle_Body.DataSource = value;
            ddl_Vehicle_Body.DataBind();

            ddl_Vehicle_Body.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable BindCarrierCategory
    {
        set
        {
            ddl_Carrier_Category.DataTextField = "Carrier_Category";
            ddl_Carrier_Category.DataValueField = "Carrier_Category_ID";
            ddl_Carrier_Category.DataSource = value;
            ddl_Carrier_Category.DataBind();

            ddl_Carrier_Category.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataTable BindVehicleManufacturer
    {
        set
        {
            ddl_Manufacturer.DataTextField = "Manufacturer";
            ddl_Manufacturer.DataValueField = "Manufacturer_ID";
            ddl_Manufacturer.DataSource = value;
            ddl_Manufacturer.DataBind();

            ddl_Manufacturer.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }
    public DataSet BindVehicleModel
    {
        set
        {
            ddl_Model.DataTextField = "Vehicle_Model";
            ddl_Model.DataValueField = "Vehicle_Model_ID";
            ddl_Model.DataSource = value;
            ddl_Model.DataBind();

            ddl_Model.Items.Insert(0, new ListItem("Select One", "0"));
        }
    }

    //public DataTable BindCleaner
    //{
    //    set
    //    {
    //        //ddl_Cleaner.DataTextField = "Driver_Name";
    //        //ddl_Cleaner.DataValueField = "Driver_ID";
    //        //ddl_Cleaner.DataSource = value;
    //        //ddl_Cleaner.DataBind();

    //        //ddl_Cleaner.Items.Insert(0, new ListItem("Select One", "0"));
    //    }

    //}
    //public DataSet BindBroker
    //{
    //    set
    //    {
    //        //ddl_Broker_Name.DataTextField = "Vendor_Name";
    //        //ddl_Broker_Name.DataValueField = "Vendor_ID";
    //        //ddl_Broker_Name.DataSource = value;
    //        //ddl_Broker_Name.DataBind();

    //        //ddl_Broker_Name.Items.Insert(0, new ListItem("Select One", "0"));
    //    }
    //}
    #endregion

    #region OtherProperties
    public ScriptManager SetScriptManager
    {
        set { _scmVehicleInformation = value; }

    }
    #endregion

    #region Validation
    public bool validateUI()
    {

        bool _isValid = false;

        if (txt_Number_Part1.Text.Trim().Length < 2)
        {
            errorMessage = "Truck Number Should Not Be less than 2 characters";
            _scmVehicleInformation.SetFocus(txt_Number_Part1);
        }
        else if (txt_Number_Part4.Text.Trim().Length < 1)
        {
            errorMessage = "Truck Number Should Should have atleast 1 Digit";
            _scmVehicleInformation.SetFocus(txt_Number_Part4);
        }
        else if (VehicleTypeId <= 0 && pc.Control_Is_Mandatory(ddl_Vehicle_Type) == true)
        {
            errorMessage = "Please Select Vehicle Type";
            _scmVehicleInformation.SetFocus(ddl_Vehicle_Type);
        }
        else if (VehicleBodyId <= 0 && pc.Control_Is_Mandatory(ddl_Vehicle_Body) == true)
        {
            errorMessage = "Please Select Vehicle Body";
            _scmVehicleInformation.SetFocus(ddl_Vehicle_Body);
        }       
        else if(CarrierCategoryId <= 0 && pc.Control_Is_Mandatory(ddl_Carrier_Category) == true)
        {
            errorMessage = "Please Select Carrier Category";
            _scmVehicleInformation.SetFocus(ddl_Carrier_Category);
        }       
        else if(ManufacturerId <= 0 && pc.Control_Is_Mandatory(ddl_Manufacturer) == true)
        {
            errorMessage = "Please Select Manufacturer";
            _scmVehicleInformation.SetFocus(ddl_Manufacturer);
        }       
        else if(VehicleModelId <= 0 && pc.Control_Is_Mandatory(ddl_Model) == true)
        {
            errorMessage = "Please Select Vehicle Model";
            _scmVehicleInformation.SetFocus(ddl_Model);
        }      
        else if (DriverId1 > 0 && DriverId2 > 0 && DriverId1 == DriverId2)
        {
            errorMessage = "Driver1 And Driver2 Cannot Be Same";
        }
        else if (YearOfManufacture > 0 && txt_Yr_Manufacture.Text.Trim().Length < 4)
        {
            errorMessage = "Year of Manufacturer Should be 4 Digits";
            _scmVehicleInformation.SetFocus(txt_Yr_Manufacture);
        }
        //else if (ddl_Cleaner.SelectedValue == "0" || Util.String2Int(ddl_Cleaner.SelectedValue) == -1)
        //{
        //    errorMessage = "Please Select Cleaner";
        //    _scmVehicleInformation.SetFocus(ddl_Cleaner);

        //}
        else if ((VehicleCategoryId == 2 || VehicleCategoryId == 3) && BrokerId <= 0)//2- managed,3 - attched
        {
            errorMessage = "Please Select Owner Name";
            _scmVehicleInformation.SetFocus(ddl_Broker_Name);
        }
        else if (VehicleCategoryId == 5 && BrokerId <= 0 && pc.Control_Is_Mandatory(td_BrokerName) == true)//Market
        {
            errorMessage = "Please Select Broker Name";
            _scmVehicleInformation.SetFocus(ddl_Broker_Name);
        }      
        else if (VehicleCategoryId == 5 && txt_Owner_Name.Text.Trim() == string.Empty && pc.Control_Is_Mandatory(txt_Owner_Name) == true) //Market
        {
            errorMessage = "Please Enter Owner Name";
            _scmVehicleInformation.SetFocus(txt_Owner_Name);
        }
        else if (!WucTDSApp1.ValidateWucTDSApp(lbl_Errors) && VehicleCategoryId == 5) //Market
        {

        }

        //else if (VehicleCategoryId != 5 && Datemanager.IsValidProcessDate("VEH_ORD", OnRoadDate) == false)
        //{
        //    //if (Datemanager.IsValidProcessDate("VEH_ORD", OnRoadDate) == false)
        //    //{
        //    errorMessage = "Invalid OnRoad Date";
        //    //}
        //}
        else if (VehicleCategoryId != 5 && OnRoadDate > DateTime.Now) //Ankit
        {
            errorMessage = "Invalid OnRoad Date";
            Wuc_DatePickerRoadDate.Focus();
        }
        else if (VehicleCategoryId == 5 && WucAddress1.ValidateWucAddress(lbl_Errors) == false) { }
        //else if (VehicleCategoryId != 1 && validateTDS() == false)
        //{ }

        else
        {
            _isValid = true;
        }

        return _isValid;
    }

    //private bool  validateTDS()
    //{
    //    bool ATS = true;
    //    if (rbl_TDSCertificate.Items[1].Selected== true && Chk_Is_Tds.Checked ==true )
    //    {
    //        if (TdsId == 0)
    //        {
    //            errorMessage = GetLocalResourceObject("MsgTDSCategory").ToString();
    //            _scmVehicleInformation.SetFocus(ddl_Tds_Category);
    //            ATS = false;
    //        }
    //        else if(TdsRatePercent <= 0)
    //        {
    //            errorMessage = GetLocalResourceObject("MsgTDSPercent").ToString();
    //            _scmVehicleInformation.SetFocus(txt_Tds_Rate_Percent);
    //            ATS = false;
    //        }
    //        else if (TdsExemptionLimit <= 0)
    //        {
    //            errorMessage = "Please Enter TDS Exemption Limit";
    //            _scmVehicleInformation.SetFocus(txt_Tds_Exemption_Limit);
    //            ATS = false;
    //        }
    //        else
    //            ATS = true;
    //    }

    //    return ATS;
    //}
    #endregion

    #region OtherMethods
    private void DefaultSettings()
    {
        if (IsPostBack == false)
        {
            string VehicleCategory = "";
            if (VehicleCategoryId == 5) // MARKET
                VehicleCategory = GetLocalResourceObject("Msg_Market").ToString();
            else if (VehicleCategoryId == 1) // Own
                VehicleCategory = GetLocalResourceObject("Msg_Own").ToString();
            else if (VehicleCategoryId == 2) // Managed
                VehicleCategory = GetLocalResourceObject("Msg_Managed").ToString();
            else if (VehicleCategoryId == 3) // Attached
                VehicleCategory = GetLocalResourceObject("Msg_Attached").ToString();

            if (VehicleCategoryId == 1) // Owner
            {
                lbl_Vehicle_Category.Text = VehicleCategory;
                lbl_Vendor_Name.Text = "Owner Name:";
                rbl_TDSCertificate.Items[1].Selected = true;
                //rbl_TDSCertificate.Visible = false;
                //lbl_Select.Visible = false;
                rbl_TDSCertificate.Style.Add("display", "none");
                lbl_Select.Style.Add("display", "none");

                pnl_Broker.Visible = false;    //shiv
                pnl_Owner.Visible = false;     //shiv
                pnl_TDS.Visible = false;       //shiv
            }
            else if (VehicleCategoryId == 2 || VehicleCategoryId == 3)//2-managed,3-attached,
            {
                lbl_Vehicle_Category.Text = VehicleCategory;
                lbl_Vendor_Name.Text = "Owner Name:";
                pnl_Owner.Visible = false;
                rbl_TDSCertificate.Items[1].Selected = true;
                //rbl_TDSCertificate.Visible = false;
                //lbl_Select.Visible = false;
                rbl_TDSCertificate.Style.Add("display", "none");
                lbl_Select.Style.Add("display", "none");
            }
            else //5-market
            {
                pnl_Broker.GroupingText = "Broker Details";
                lbl_Vehicle_Category.Text = VehicleCategory;
                lbl_Vendor_Name.Text = "Broker Name:";
                txt_Owner_Name.Visible = true;
                lbl_Owner_Name.Visible = true;
                lbl_Current_Odometer.Visible = false;
                lbl_CurrentOdometer.Visible = false;
                lbl_OpeningOdometer.Visible = false;
                rbl_TDSCertificate.Items[0].Selected = true;
                rbl_TDSCertificate.Items[1].Selected = false;
                lbl_OnRoadDate.Visible = false;
                txt_opening_Odometer.Visible = false;
                Wuc_DatePickerRoadDate.Visible = false;
            }
        }
    }
    #endregion

    #region ControlEvents
    protected void Page_Load(object sender, EventArgs e)
    {
        string Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        if (!IsPostBack)
        {
            VehicleCategoryId = Util.String2Int(StateManager.GetState<string>("QueryString"));//owner
            if (Mode == "4")
            {
                rbl_TDSCertificate.Enabled = false;
                Wuc_DatePickerRoadDate.disableForView = false;
            }
        }
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        ddl_Broker_Name.OtherColumns = VehicleCategoryId.ToString();

        DefaultSettings();

        objVehicleInformationPresenter = new VehicleInformationPresenter(this, IsPostBack);
        objTDSAppPresenter = new TDSAppPresenter(WucTDSApp1, IsPostBack);

        if (IsPostBack == false)
        {
            
               Common ObjCommon = new Common();
                hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/Vehicle/App_LocalResources/WucVehicleInformation.ascx.resx");
            

            if (keyID > 0 && VehicleCategoryId == 5 && rbl_TDSCertificate.SelectedValue == "0") //broker
            {
                WucTDSApp1.Enable_Disable_Controls(false);
                //Chk_Is_Tds.Enabled = false;
                //ddl_Tds_Category.Enabled = false;
                //txt_Tds_Rate_Percent.Enabled = false;
                //txt_Tds_Exemption_Limit.Enabled = false;
            }
            else if (keyID > 0 && VehicleCategoryId != 5 && rbl_TDSCertificate.SelectedValue == "1" == true)
            {
                WucTDSApp1.Enable_Disable_Controls(false);
                //Chk_Is_Tds.Enabled = false;
                //ddl_Tds_Category.Enabled = false;
                //txt_Tds_Rate_Percent.Enabled = false;
                //txt_Tds_Exemption_Limit.Enabled = false;
            }
            //ChkTDSChange();
        }

        if (VehicleCategoryId == 1)//owner
        {
            tr_TDSDetails.Visible = false;
        }
        else
        {
            tr_TDSDetails.Visible = true;
        }

        if (VehicleCategoryId == 2 || VehicleCategoryId == 3)
        {
            WucTDSApp1.Enable_Disable_Controls(false);
        }

        WucAddress1.SetTDCaptionWidth = "20";
        WucAddress1.SetTDDataWidth = "29";

    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objVehicleInformationPresenter.Save();
    }

    protected void ddl_Manufacturer_SelectedIndexChanged(object sender, EventArgs e)
    {
        objVehicleInformationPresenter.FillVehicleModelOnManufactureChange();
    }

    //private void GetTDSDetails()
    //{
    //    if (rbl_TDSCertificate.SelectedValue == "1" || rbl_TDSCertificate.SelectedValue == "0") //owner
    //    {
    //        objVehicleInformationPresenter.FillTDSDetailsOnRadioButtonChanged();

    //        Chk_Is_Tds.Enabled = false;

    //        ddl_Tds_Category.Enabled = false;
    //        txt_Tds_Rate_Percent.Enabled = false;
    //        txt_Tds_Exemption_Limit.Enabled = false;
    //   }
    //    else //broker
    //    {
    //        Chk_Is_Tds.Enabled = true;
    //        Chk_Is_Tds.Checked = false;

    //        ddl_Tds_Category.SelectedValue = "0";
    //        txt_Tds_Rate_Percent.Text = "";
    //        txt_Tds_Exemption_Limit.Text = "";

    //        ddl_Tds_Category.Enabled = true;
    //        txt_Tds_Rate_Percent.Enabled = true;
    //        txt_Tds_Exemption_Limit.Enabled = true; 

    //    }
    //}

    protected void rbl_TDSCertificate_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetTDSDetails();
        if (rbl_TDSCertificate.SelectedValue == "1") //owner
        {
            WucTDSApp1.Enable_Disable_Controls(true);
            TDSAppView.IsTDSApp = false;
            //Chk_Is_Tds.Enabled = true;
            //Chk_Is_Tds.Checked = false;

            //ddl_Tds_Category.SelectedValue = "0";
            //txt_Tds_Rate_Percent.Text = "";
            //txt_Tds_Exemption_Limit.Text = "";

            //ddl_Tds_Category.Enabled = true;
            //txt_Tds_Rate_Percent.Enabled = true;
            //txt_Tds_Exemption_Limit.Enabled = true;
        }
        else //broker
        {
            if (BrokerId > 0)
            {
                TDSAppView.Call_From = 1;//broker
                TDSAppView.ID = BrokerId;
                objTDSAppPresenter.FillDetails();
                WucTDSApp1.Enable_Disable_Controls(false);
            }
            else
            {
                TDSAppView.IsTDSApp = false;
                WucTDSApp1.Enable_Disable_Controls(true);

            }
            //objVehicleInformationPresenter.FillTDSDetailsOnRadioButtonChanged();

            //Chk_Is_Tds.Enabled = false;

            //ddl_Tds_Category.Enabled = false;
            //txt_Tds_Rate_Percent.Enabled = false;
            //txt_Tds_Exemption_Limit.Enabled = false;
        }




        UpdatePanel4.Update();
        String Script = "<script type='text/javascript'>Hide_Control(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
        //ChkTDSChange();
    }

    //private void ChkTDSChange()
    //{
    //    //lbl_Tds_Category.Visible = Chk_Is_Tds.Checked;
    //    //lbl_Tds_Rate_Percent.Visible = Chk_Is_Tds.Checked;
    //    //lbl_Tds_Exemption_Limit.Visible = Chk_Is_Tds.Checked;

    //    //ddl_Tds_Category.Visible = Chk_Is_Tds.Checked;
    //    //txt_Tds_Rate_Percent.Visible = Chk_Is_Tds.Checked;
    //    //txt_Tds_Exemption_Limit.Visible = Chk_Is_Tds.Checked;
    //}
    //protected void Chk_Is_Tds_CheckedChanged(object sender, EventArgs e)
    //{
    //    ChkTDSChange();
    //} 

    protected void ddl_Broker_Name_TxtChange(object sender, EventArgs e)
    {
        hdn_BrokerId.Value = Util.String2Int(ddl_Broker_Name.SelectedValue) <= 0 ? "0" : ddl_Broker_Name.SelectedValue;

        if (BrokerId <= 0)
        {
            //Chk_Is_Tds.Enabled = false;
            //Chk_Is_Tds.Checked = false;
            //GetTDSDetails();
            rbl_TDSCertificate.Items[1].Selected = true;
            rbl_TDSCertificate.Items[0].Selected = false;
            TDSAppView.IsTDSApp = false;
            WucTDSApp1.Enable_Disable_Controls(true);
            //ChkTDSChange();
        }
        else
        {
            if (rbl_TDSCertificate.Items[0].Selected == true)
            {
                TDSAppView.Call_From = 1;//broker
                TDSAppView.ID = BrokerId;
                objTDSAppPresenter.FillDetails();
                WucTDSApp1.Enable_Disable_Controls(false);
            }

            if (VehicleCategoryId == 2 || VehicleCategoryId == 3)
            {
                TDSAppView.Call_From = 1;//broker
                TDSAppView.ID = BrokerId;
                objTDSAppPresenter.FillDetails();
                WucTDSApp1.Enable_Disable_Controls(false);
            }
            //GetTDSDetails();
            //ChkTDSChange();
        }



        UpdatePanel4.Update();
        String Script = "<script type='text/javascript'>Hide_Control(); </script>";
        System.Web.UI.ScriptManager.RegisterStartupScript(Page, typeof(string), "Script", Script, false);
    }
    #endregion
}