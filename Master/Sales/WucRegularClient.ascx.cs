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
using ClassLibraryMVP.General;
using Raj.EC.SalesPresenter;
using Raj.EC.SalesView;
using System.Data.SqlClient;
using Raj.EC.ControlsView;
using Raj.EC;


public partial class Master_Sales_WucRegularClient : System.Web.UI.UserControl, IRegularClientView
{
    #region ClassVariables
    PageControls pc = new PageControls();
    RegularClientPresenter objRegularClientPresenter;
    public EventHandler OnClientGroupChanged;
    DataRow DR = null;
    Raj.EC.Common ObjCommon = new Raj.EC.Common();
    #endregion

    #region ControlsValue


    public int ClientId
    {
        set { hdn_ClientId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_ClientId.Value); }
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

    public int ContractualClientId
    {
        set { hdn_ContractualClientId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_ContractualClientId.Value); }
    }


    public string RegularClientName
    {
        set { txt_RegularClientName.Text = value; }
        get { return txt_RegularClientName.Text; }
    }
    public string ContactPerson
    {
        set { txt_ContactPerson.Text = value; }
        get { return txt_ContactPerson.Text; }
    }
    public bool IsServiceTaxPayable
    {
        set { Chk_IsServiceTaxPayable.Checked = value; }
        get { return Chk_IsServiceTaxPayable.Checked; }
    }
    public bool Is_Casual_Taxable
    {
        set { chk_Is_Casual_Taxable.Checked = value; }
        get { return chk_Is_Casual_Taxable.Checked; }
    }
    public bool Is_ToPay_Allowed
    {
        set { chk_Is_ToPay_Allowed.Checked = value; }
        get { return chk_Is_ToPay_Allowed.Checked; }
    }
    public IAddressView AddressView
    {
        get { return (IAddressView)WucAddress1; }
    }
    public string CSTNo
    {
        set { txt_CstNo.Text = value; }
        get { return txt_CstNo.Text; }
    }

    public string GSTName
    {
        set { txt_GSTName.Text = value; }
        get { return txt_GSTName.Text; }
    }

    public string ServiceTaxNo
    {
        set { txt_ServiceTaxNo.Text = value; }
        get { return txt_ServiceTaxNo.Text; }
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

    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
        }
    }

    public bool IsWithCompleteDetails
    {
        set { chk_IsWithCompleteDetails.Checked = value; }
        get { return chk_IsWithCompleteDetails.Checked; }
    }


    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;
        bool Check_DuplicateClientName = false;

        if (RegularClientName == string.Empty)
        {
            errorMessage = GetLocalResourceObject("Msg_ClientName").ToString();
            txt_RegularClientName.Focus();
            _isValid = false;
        }
        else if (ContactPerson == string.Empty && pc.Control_Is_Mandatory(txt_ContactPerson) == true)
        {
            errorMessage = "Please Enter Contact Person";
            txt_ContactPerson.Focus();
            _isValid = false;
        }
        else if (!WucAddress1.ValidateWucAddress(lbl_Errors))
        {
            _isValid = false;
        }
        else if (IsServiceTaxPayable && !validateGST())
        {
            //errorMessage = "Please Enter 15 Digits GST No";
            txt_CstNo.Focus();
            _isValid = false;
        }
        else if (txt_CstNo.Text.Trim().Length > 0 && !validateGST())
        {
            //errorMessage = "Please Enter 15 Digits GST No";
            txt_CstNo.Focus();
            _isValid = false;
        }
        //else if (txt_CstNo.Text == string.Empty && pc.Control_Is_Mandatory(txt_CstNo) == true )
        //{
        //    errorMessage = "Please Enter CST /TIN No";
        //    txt_CstNo.Focus();
        //    _isValid = false;
        //}

        else if (txt_ServiceTaxNo.Text == string.Empty && pc.Control_Is_Mandatory(txt_ServiceTaxNo) == true)
        {
            errorMessage = "Please Enter Service Tax No";
            txt_ServiceTaxNo.Focus();
            _isValid = false;
        }
        else if (RegularClientName != string.Empty)
        {
            Check_DuplicateClientName = ObjCommon.IsCheck_Duplicate("Ec_Master_Client_VTrans", keyID, RegularClientName);
            if (Check_DuplicateClientName == true)
            {
                errorMessage = GetLocalResourceObject("Msg_ClientValidation").ToString();
                txt_RegularClientName.Focus();
                _isValid = false;
            }
            else
            {
                _isValid = true;
            }
        }

        else
        {
            return _isValid = true;
        }

        return _isValid;
    }
    public bool validateGST()
    {
        bool _isValid = false;
        string gstinVal = txt_CstNo.Text.Trim();
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
                txt_CstNo.Focus();
                _isValid = false;
            }
            else if (Is_Casual_Taxable == false && first2val != GSTCode)
            {
                errorMessage = "First 2 digits must be GST state code : " + first2val;
                txt_CstNo.Focus();
                _isValid = false;
            }
            else if (reggstsecond5val.IsMatch(second5val) == false)
            {
                errorMessage = "From 3rd digit to 7th digit must be characters :" + second5val;
                txt_CstNo.Focus();
                _isValid = false;
            }
            else if (reggstthird4val.IsMatch(third4val) == false)
            {
                errorMessage = "From 8th digit to 11th digit must be numbers :" + third4val;
                txt_CstNo.Focus();
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
                txt_CstNo.Focus();
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
        set
        {
            lbl_Errors.Text = value;
        }
    }




    #endregion

    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        hdn_Mode.Value = Util.DecryptToString(Request.QueryString["Mode"]);
        SetPostBackValues();

        if (!IsPostBack)
        {
            pc.AddAttributes(this.Controls);
            Raj.EC.Common ObjCommon = new Raj.EC.Common();
            hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/Sales/App_LocalResources/WucRegularClient.ascx.resx");

            hdn_Is_Consignor.Value = Request.QueryString["Is_Consignor"];
        }
        objRegularClientPresenter = new RegularClientPresenter(this, IsPostBack);
        GSTStateCode = WucAddress1.GSTStateCode;
        WucAddress1.SetTDCaptionWidth = "20%";
        WucAddress1.ShowAlertRow();
        //  WucAddress1.SetTDDataWidth = "79%";

        Label lbl_MobileNo = (Label)WucAddress1.FindControl("lbl_Mobile_No");

        lbl_MobileNo.Text = "Mobile No. For SMS :";


        if (keyID > 0)
        {
            CheckBox chkSMSAlert = (CheckBox)WucAddress1.FindControl("chk_SMS_Alert");
            CheckBox chkeMailAlert = (CheckBox)WucAddress1.FindControl("chk_eMail_Alert");

            if (UserManager.getUserParam().ProfileId != 20 && UserManager.getUserParam().ProfileId != 0 && IsWithCompleteDetails == false)
            {
                ddl_ClientCategory.Enabled = false;
                //txt_CstNo.Enabled = false;
                ddl_dly_type.Enabled = false;
                chk_Is_Casual_Taxable.Enabled = false;
                chk_Is_ToPay_Allowed.Enabled = false;
                txt_Remarks.Enabled = false;
                //Chk_IsServiceTaxPayable.Enabled = false;
                chkSMSAlert.Enabled = false;
                chkeMailAlert.Enabled = false;
                chk_IsWithCompleteDetails.Enabled = false;

            }
            else if (UserManager.getUserParam().ProfileId != 20 && UserManager.getUserParam().ProfileId != 0 && IsWithCompleteDetails == true)
            {
                ddl_ClientGroup.Enabled = false;
                txt_RegularClientName.Enabled = false;
                txt_ContactPerson.Enabled = false;
                ddl_ClientCategory.Enabled = false;
                WucAddress1.EnableAddress = false;
                ddlDeliveryArea.Enabled = false;
                ddlLandmark1.Enabled = false;
                ddlLandmark2.Enabled = false;
                ddl_dly_type.Enabled = false;
                Chk_IsServiceTaxPayable.Enabled = false;
                chk_Is_Casual_Taxable.Enabled = false;
                txt_CstNo.Enabled = false;
                txt_GSTName.Enabled = false;
                txt_ServiceTaxNo.Enabled = false;
                chk_Is_ToPay_Allowed.Enabled = false;
                txt_Remarks.Enabled = false;
                chk_IsWithCompleteDetails.Enabled = false;
                chkeMailAlert.Enabled = false;
                chkSMSAlert.Enabled = false;
                btn_Save.Visible = false;
            }
            
        }
        else if (UserManager.getUserParam().ProfileId != 20 && UserManager.getUserParam().ProfileId != 0) 
        {
            tr_WithComplteDetails.Visible=false;
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
        txt_RegularClientName.Focus();
    }

    public void fillDlyArea(int CityID)
    {
        Common objCommon = new Common();
        ddlDeliveryArea.DataTextField = "DeliveryAreaName";
        ddlDeliveryArea.DataValueField = "DeliveryAreaID";


        ddlDeliveryArea.DataSource = objCommon.GetDeliveryArea(0, true, true, true, CityID);
        ddlDeliveryArea.DataBind();
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


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        string Url = Request.QueryString["Url"];

        if (validateUI())
        {
            objRegularClientPresenter.Save();

            string _Msg;
            _Msg = "Saved SuccessFully";

            String Call_From;

            Call_From = Request.QueryString["Call_From"];

            String popupScript = "";

            if (Call_From == "GC")
            {
                popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();Update_Consignor_Consignee_Details();</script>";
            }
            else
            {
                //popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();</script>";

                string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + LinkUrl + "&DecryptUrl='No'");

            }

            System.Web.UI.ScriptManager.RegisterStartupScript(Upd_Pnl_RegularClient, typeof(String), "PopupScript1", popupScript.ToString(), false);

        }
    }

    #endregion
}
