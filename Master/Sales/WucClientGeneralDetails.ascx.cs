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
using System.Text.RegularExpressions;
using ClassLibraryMVP;
using Raj.EC.MasterPresenter;
using Raj.EC.MasterView;
using Raj.EC.ControlsView;
using Raj.EC;

/// <summary>
/// Author        : Shiv kumar mishra
/// Created On    : 13/10/2008
/// Description   : This Page is For Master Client General Details
/// </summary>
/// 

public partial class Master_Sales_WucClientGeneralDetails : System.Web.UI.UserControl, IClientGeneralView
{
    #region ClassVariables
    PageControls pc = new PageControls();
    ClientGeneralPresenter objClientGeneralPresenter;
    private ScriptManager scm_ClientGeneral;
    public EventHandler OnClientGroupChanged;
    public EventHandler OnCopyFromClient;
    TextBox txt_Branch;
    bool _IsServiceTaxPay;

    Raj.EC.Common ObjCommon = new Raj.EC.Common();

    #endregion

    #region ControlsValues

    public int BranchId
    {
        get { return Util.String2Int(ddl_Branch.SelectedValue); }
    }

    public int Regular_Client_Id
    {
        get
        {         
           return ddl_Client.SelectedText == "" ? 0 :Util.String2Int(ddl_Client.SelectedValue);
        }
    }

    public string ClientCode
    {
        set { txt_ClientCode.Text = value; }
        get { return txt_ClientCode.Text; }
    }
    public bool IsServiceTaxPay
    {
        set { _IsServiceTaxPay = value; }
        get { return _IsServiceTaxPay; }
    }
    public string ClientName
    {
        set { txt_ClientName.Text = value; }
        get { return txt_ClientName.Text; }
    }
    public int ClientGroupID
    {
        set { ddl_ClientGroup.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_ClientGroup.SelectedValue); }
    }
    public int ClientCategoryID
    {
        set { ddl_ClientCategory.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_ClientCategory.SelectedValue); }
    }
    public string ContactPersonName
    {
        set { txt_ContactPerson.Text = value; }
        get { return txt_ContactPerson.Text; }
    }
    public string CSTTINNo
    {
        set { txt_CSTTINNo.Text = value; }
        get { return txt_CSTTINNo.Text; }
    }
    public bool Is_Casual_Taxable
    {
        set { chk_Is_Casual_Taxable.Checked = value; }
        get { return chk_Is_Casual_Taxable.Checked; }
    }
    public string ServiceTaxNo
    {
        set { txt_ServiceTaxNo.Text = value; }
        get { return txt_ServiceTaxNo.Text; }
    }
    public void SetBranchId(string text, string value)
    {
        ddl_Branch.DataTextField = "Branch_Name";
        ddl_Branch.DataValueField = "Branch_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Branch);
    }

    public void SetClientId(string text, string value)
    {
        ddl_Branch.DataTextField = "Client_Name";
        ddl_Branch.DataValueField = "Client_ID";

        Raj.EC.Common.SetValueToDDLSearch(text, value, ddl_Client);
    } 

    public int CityID
    {
        get { return WucAddress1.CityId; }
        set
        {
            WucAddress1.CityId = value;
            hdn_City_ID.Value = Util.Int2String(value);
        }
    }

    public string GSTStateCode
    {
        get { return WucAddress1.GSTStateCode; }
        set
        {
            WucAddress1.GSTStateCode = value;
            hdn_GST_State_Code.Value = value;
        }
    }
    public int DeliveryAreaId
    {
         
        get { return Util.String2Int(ddlDeliveryArea.SelectedValue); }
        set
        {
            ddlDeliveryArea.SelectedValue = Util.Int2String(value);
            hdn_DeliveryAreaID.Value = Util.Int2String(value);
        }
    }

    public int Landmark1ID
    {

        get { return Util.String2Int(ddlLandmark1.SelectedValue); }
        set
        {
            ddlLandmark1.SelectedValue = Util.Int2String(value);
            hdn_Landmark1.Value = Util.Int2String(value);
        }
    }

    public int Landmark2ID
    {

        get { return Util.String2Int(ddlLandmark2.SelectedValue); }
        set
        {
            ddlLandmark2.SelectedValue = Util.Int2String(value);
            hdn_Landmark2.Value = Util.Int2String(value);
        }
    }

    public int DeliveryTypeID
    {

        get { return Util.String2Int(ddl_dly_type.SelectedValue); }
        set
        {
            ddl_dly_type.SelectedValue = Util.Int2String(value);
            //hdn_DeliveryAreaID.Value = Util.Int2String(value);
        }
    }

    public string CreatedBy
    {
        set { lbl_CreateBy.Text = value; }
        get { return lbl_CreateBy.Text; }
    }

    public string UpdatedBy
    {
        set { lbl_UpdatedBy.Text = value; }
        get { return lbl_UpdatedBy.Text; }
    }

    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }

    public bool Is_OutwardBilling
    {
        set { chk_OutwardBilling.Checked = value; }
        get { return chk_OutwardBilling.Checked; }
    }

    public bool Is_InwardBilling
    {
        set { chk_InwardBilling.Checked = value; }
        get { return chk_InwardBilling.Checked; }
    }

    #endregion

    #region ControlsBind

    //public DataTable BindBranch
    //{
    //    set
    //    {
    //        ddl_Branch.DataTextField = "Branch_Name";
    //        ddl_Branch.DataValueField = "Branch_ID";
    //        ddl_Branch.DataSource = value;
    //        ddl_Branch.DataBind();
    //    }
    //}
    public DataTable BindClientGroup
    {
        set
        {
            ddl_ClientGroup.DataTextField = "Client_Group_Name";
            ddl_ClientGroup.DataValueField = "Client_Group_ID";
            ddl_ClientGroup.DataSource = value;
            ddl_ClientGroup.DataBind();
            ddl_ClientGroup.Items.Insert(0, new ListItem("None", "0"));
        }
    }
    public DataTable BindClientCategory
    {
        set
        {
            ddl_ClientCategory.DataTextField = "Category_Name";
            ddl_ClientCategory.DataValueField = "Category_ID";
            ddl_ClientCategory.DataSource = value;
            ddl_ClientCategory.DataBind();
            ddl_ClientCategory.Items.Insert(0, new ListItem("None ", "0"));
        }
    }
    public DataTable BindDeliveryArea
    {
        set
        { 
            ddlDeliveryArea.DataTextField = "DeliveryAreaName";
            ddlDeliveryArea.DataValueField = "DeliveryAreaID";
            ddlDeliveryArea.DataSource = value;
            ddlDeliveryArea.DataBind();
        }
    }
    public DataTable BindDeliveryType
    {
        set
        {
            ddl_dly_type.DataTextField = "Delivery_Type";
            ddl_dly_type.DataValueField = "Delivery_Type_Id";
            ddl_dly_type.DataSource = value;
            ddl_dly_type.DataBind();
            //ddl_dly_type.Items.Insert(0, new ListItem("None ", "0"));
        }
    }


    public IAddressView AddressView
    {
        get { return (IAddressView)WucAddress1; }
    }

    #endregion

    # region OtherMethod

    public ScriptManager SetScriptManager
    {
        set { scm_ClientGeneral = value; }
    }
    private bool checkRegularClient()
    {
        bool IsRegularClient = objClientGeneralPresenter.Isregularclient();
        return IsRegularClient;
    }
    private bool checkRegularClientGeneral_Discount()
    {
        bool IsRegularClientGeneral_Discount = objClientGeneralPresenter.IsregularclientGeneral_Discount();
        return IsRegularClientGeneral_Discount;
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        errorMessage = "";
        bool _isValid = false;
        
        txt_Branch = (TextBox)ddl_Branch.FindControl("txtBoxddl_Branch");

        if (BranchId <= 0)
        {
            errorMessage = "Please Select Branch";
            scm_ClientGeneral.SetFocus(txt_Branch);
        }
        else if (ClientCode == string.Empty)
        {
            errorMessage = "Please Enter Client Code";
            scm_ClientGeneral.SetFocus(txt_ClientCode);
        }
        else if (ClientName == string.Empty)
        {
            errorMessage = "Please Enter Client Name";
            scm_ClientGeneral.SetFocus(txt_ClientName);
        }
        //else if (ClientName.Length < 5)
        //{
        //    errorMessage = "Client Name should be greater than 5 characters";
        //    scm_ClientGeneral.SetFocus(txt_ClientName);
        //}
        //else if (ClientGroupID == 0)
        //{
        //    errorMessage = "Please Select Client Group";
        //    scm_ClientGeneral.SetFocus(ddl_ClientGroup);
        //}
        else if (ContactPersonName == string.Empty && pc.Control_Is_Mandatory(txt_ContactPerson) == true)
        {
            errorMessage = "Please Enter Contact Person";
            scm_ClientGeneral.SetFocus(txt_ContactPerson);
        }
        else if (WucAddress1.ValidateWucAddress(lbl_Errors) == false) { }
        
        //else if (CSTTINNo == string.Empty && pc.Control_Is_Mandatory(txt_CSTTINNo) == true)
        //{
        //    errorMessage = "Please Enter CST/TIN No";
        //    scm_ClientGeneral.SetFocus(txt_CSTTINNo);
        //}
        else if (ServiceTaxNo == string.Empty && pc.Control_Is_Mandatory(txt_ServiceTaxNo) == true)
        {
            errorMessage = "Please Enter Service Tax No";
            scm_ClientGeneral.SetFocus(txt_ServiceTaxNo);
        }
        else if (keyID <= 0 && checkRegularClient() == true)
        {
            errorMessage = "Client is Existed as Regular Client";
            scm_ClientGeneral.SetFocus(txt_ClientName);
        }
        else if (keyID > 0 && checkRegularClientGeneral_Discount() == true && ClientCategoryID > 0)
        {
            errorMessage = "Selected Party has Party Wise Discount. Please Edit Validity Period";
            scm_ClientGeneral.SetFocus(txt_ClientName);
        }
        else if (IsServiceTaxPay && txt_CSTTINNo.Text.Trim().Length != 15)
        {
            errorMessage = "Please Enter 15 Digits GST No";
            txt_CSTTINNo.Focus();
            _isValid = false;
        }
        else if (IsServiceTaxPay && !validateGST())
        {
            //errorMessage = "Please Enter 15 Digits GST No";
            txt_CSTTINNo.Focus();
            _isValid = false;
        }
        else if (txt_CSTTINNo.Text.Trim().Length > 0 && !validateGST())
        {
            //errorMessage = "Please Enter 15 Digits GST No";
            txt_CSTTINNo.Focus();
            _isValid = false;
        }
        //else if (Is_OutwardBilling == false  && Is_InwardBilling == false)
        //{
        //    errorMessage = "Please Select Atleast Outward Or Inward Billing";
        //    chk_OutwardBilling.Focus();
        //    _isValid = false;
        //}

        else
        {
            _isValid = true;
        }

        return _isValid;
    }
    public bool validateGST()
    {
        bool _isValid = false;
        string gstinVal = txt_CSTTINNo.Text.Trim();
        string GSTCode = GSTStateCode;

        Regex reggst = new Regex("^([0-9]){2}([a-zA-Z]){5}([0-9]){4}([a-zA-Z]){1}([1-9a-zA-Z]){1}([Z]){1}([a-zA-Z0-9]){1}?$");
        Regex reggstsecond5val = new Regex("^([a-zA-Z]){5}?$");
        Regex reggstthird4val = new Regex("^([0-9]){4}?$");
        Regex regchar = new Regex("^[a-zA-Z]*$");

        if (gstinVal != string.Empty)
        {
            string first2val = "";
            string second5val = "";
            string third4val = "";
            string fourth1Val = "";
            string fifth1Val = "";
            string sixth1Val = "";
            if (gstinVal.Length == 15)
            {
                first2val = gstinVal.Substring(0, 2);
                second5val = gstinVal.Substring(2, 5);
                third4val = gstinVal.Substring(7, 4);
                fourth1Val = gstinVal.Substring(11, 1);
                fifth1Val = gstinVal.Substring(12, 1);
                sixth1Val = gstinVal.Substring(13, 1);
            }

            if (gstinVal.Length < 15)
            {
                errorMessage = "Please Enter 15 digit GST No.";
                txt_CSTTINNo.Focus();
                _isValid = false;
            }
            else if (Is_Casual_Taxable == false && first2val != GSTCode)
            {
                errorMessage = "First 2 digits must be GST state code : " + first2val;
                txt_CSTTINNo.Focus();
                _isValid = false;
            }
            else if (reggstsecond5val.IsMatch(second5val) == false)
            {
                errorMessage = "From 3rd digit to 7th digit must be characters :" + second5val;
                txt_CSTTINNo.Focus();
                _isValid = false;
            }
            else if (reggstthird4val.IsMatch(third4val) == false)
            {
                errorMessage = "From 8th digit to 11th digit must be numbers :" + third4val;
                txt_CSTTINNo.Focus();
                _isValid = false;
            }
            else if (regchar.IsMatch(fourth1Val) == false)
            {
                errorMessage = "12th digit must be character :" + fourth1Val;
                _isValid = false;
            }
            else if (sixth1Val.ToUpper() != "Z")
            {
                errorMessage = "14th digit must be Z :" + sixth1Val;
                txt_CSTTINNo.Focus();
                _isValid = false;
            }
            else if (reggst.IsMatch(gstinVal) == false)
            {
                errorMessage = "Please Enter Valid GST No.";
                _isValid = false;
            }
            else
                _isValid = true;
        }
        else
        {
            errorMessage = "Please Enter 15 digit GST No.";
            _isValid = false;
        }
        return _isValid;
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
            //return 12;
        }
    }
    #endregion

    #region EventClick
   
    protected void Page_Load(object sender, EventArgs e)
    {
        SetPostBackValues();
        txt_Branch = (TextBox)ddl_Branch.FindControl("txtBoxddl_Branch");

        ddl_Branch.DataTextField = "Branch_Name";
        ddl_Branch.DataValueField = "Branch_ID";

        ddl_Client.DataTextField = "Client_Name";
        ddl_Client.DataValueField = "Client_Id";

        ddlDeliveryArea.DataTextField = "DeliveryAreaName";
        ddlDeliveryArea.DataValueField = "DeliveryAreaID";

        ddl_Client.OtherColumns = keyID.ToString();
        if (keyID > 0)
        {
            lbl_Regular_Client.Visible = false;
            ddl_Client.Visible = false;
        }

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        objClientGeneralPresenter = new ClientGeneralPresenter(this, IsPostBack);

        WucAddress1.SetTDCaptionWidth = "20%";
        WucAddress1.SetTDDataWidth = "29%";
        WucAddress1.ShowAlertRow();
        GSTStateCode = WucAddress1.GSTStateCode;
        if (!IsPostBack)
        {
            txt_Branch.Focus();
        }
    }
    private void SetPostBackValues()
    {
        WucAddress1.OnCityChanged += new EventHandler(OnDDLCitySelection);
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
    }

    private void OnDDLCitySelection(object sender, EventArgs e)
    {
          CityID = WucAddress1.CityId;
          fillDlyArea(CityID);
          FillLandmark1();
          FillLandmark2();
    }

    protected void ddl_ClientGroup_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (OnClientGroupChanged != null)
        {
            OnClientGroupChanged((object)ClientGroupID, e);
            //upnl_clientgroup.Update();
        }
            ddl_ClientCategory.Focus();
    }

    public void fillDlyArea(int CityID)
    { 
        Common objCommon = new Common();
        ddlDeliveryArea.DataTextField = "DeliveryAreaName";
        ddlDeliveryArea.DataValueField = "DeliveryAreaID"; 

        DataTable dt = new DataTable();
        dt = objCommon.GetDeliveryArea(0, true, true, true, CityID);
        
        ddlDeliveryArea.DataSource = dt;
        ddlDeliveryArea.DataBind();
    }

    #endregion

    protected void ddl_Client_TxtChange(object sender, EventArgs e)
    {
        CityID = WucAddress1.CityId;
        fillDlyArea(CityID);

        FillLandmark1();
        FillLandmark2();

        if (objClientGeneralPresenter.CopyRegClient() == true)
        {
            txt_ClientName.Enabled = false;
        }
        else
        {
            clearcontrol();
            AddressView.SetCityId("", "0");
        }

        if (OnCopyFromClient != null)
        {
            OnCopyFromClient((object)IsServiceTaxPay, e);
        }

        up_Client.Update();
        up_RegistrationDetails.Update();
    }

    public void clearcontrol()
    {
        txt_ClientName.Enabled = true;
        ContactPersonName = "";
        ClientName = "";
        CSTTINNo = "";
        ServiceTaxNo = "";
        AddressView.AddressLine1 = "";
        AddressView.AddressLine2 = "";
        
        AddressView.EmailId = "";
        AddressView.StdCode = "";
        AddressView.Phone1 = "";
        AddressView.Phone2 = "";
        AddressView.FaxNo = "";
        AddressView.MobileNo = "";
        AddressView.PinCode = "";
    }
    protected void ddlDeliveryArea_SelectedIndexChanged(object sender, EventArgs e)
    {
        DeliveryAreaId = Convert.ToInt32(ddlDeliveryArea.SelectedValue);
        FillLandmark1();
        FillLandmark2();
    }

    private void FillLandmark1()
    {
        string query = "Select '0' Landmark1ID,' Select One' Landmark1 Union Select Landmark1ID,Landmark1 from EC_Master_Landmark1 where DeliveryAreaId = " + DeliveryAreaId + " order by Landmark1";
        DataSet ds = new DataSet();
        ds = ObjCommon.EC_Common_Pass_Query(query);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlLandmark1.DataSource = ds;
            ddlLandmark1.DataValueField = "Landmark1ID";
            ddlLandmark1.DataTextField = "Landmark1";
            ddlLandmark1.DataBind();
            //ddlLandmark1.Items.Insert(0, new ListItem("Select One", "0"));
        }
        else
        {
            ddlLandmark1.Items.Clear();
        }
    }

    protected void ddlLandmark1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Landmark1ID = Convert.ToInt32(ddlLandmark1.SelectedValue);
        FillLandmark2();
    }

    private void FillLandmark2()
    {
        string query = "Select '0' Landmark2ID,' Select One' Landmark2 Union  Select Landmark2ID,Landmark2 from EC_Master_Landmark2 where Landmark1ID = " + Landmark1ID + " order by Landmark2";
        DataSet ds = new DataSet();
        ds = ObjCommon.EC_Common_Pass_Query(query);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlLandmark2.DataSource = ds;
            ddlLandmark2.DataValueField = "Landmark2ID";
            ddlLandmark2.DataTextField = "Landmark2";
            ddlLandmark2.DataBind();
            //ddlLandmark2.Items.Insert(0, new ListItem("Select One", "0"));
        }
        else
        {
            ddlLandmark2.Items.Clear();
        }
    }

    protected void ddlLandmark2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Landmark2ID = Convert.ToInt32(ddlLandmark2.SelectedValue);
    }


}