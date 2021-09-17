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
using Raj.EC.ControlsPresenter;
using Raj.EC.ControlsView;
using ClassLibraryMVP.General;
using Raj.EC;

public partial class Master_Control_Address : System.Web.UI.UserControl, IAddressView
{
    #region ClassVariables
    AddressPresenter objAddressPresenter;
    int _cityId = -1;
    ScriptManager scm_Address;
    public EventHandler OnCityChanged;
    #endregion


    #region ControlsValues

    //public int OnCityChanged
    //{
    //    set { _cityId = value; }
    //}


    public int CityId
    {
        get { return Util.String2Int(ddl_City.SelectedValue); }
        set { _cityId = value; }
    }

    public string GSTStateCode
    {
        get { return hdnGSTStateCode.Value; }
        set { hdnGSTStateCode.Value = value; }
    }

    public string AddressLine1
    {
        get { return txt_AddressLine1.Text; }
        set { txt_AddressLine1.Text = value; }
    }
    public string AddressLine2
    {
        get { return txt_AddressLine2.Text; }
        set { txt_AddressLine2.Text = value; }
    }

    public string PinCode
    {
        get { return txt_PinCode.Text; }
        set { txt_PinCode.Text = value; }
    }


    public string StdCode
    {
        get { return txt_Std_Code.Text; }
        set { txt_Std_Code.Text = value; }
    }

    public string MobileNo
    {
        get { return txt_Mobile_No.Text; }
        set { txt_Mobile_No.Text = value; }
    }

    public string Phone1
    {
        get { return txt_Phone1.Text; }
        set { txt_Phone1.Text = value; }
    }

    public string Phone2
    {
        get { return txt_Phone2.Text; }
        set { txt_Phone2.Text = value; }
    }

    public string FaxNo
    {
        get { return txt_Fax_No.Text; }
        set { txt_Fax_No.Text = value; }
    }

    public string EmailId
    {
        get { return txt_Email_Id.Text; }
        set { txt_Email_Id.Text = value; }
    }

    public string CityName
    {
        get { return ddl_City.SelectedItem; }
    }

    public void SetCityId(string text, string value)
    {
        Common.SetValueToDDLSearch(text, value, ddl_City);
    }

    public int RegionDetailID 
    {
        get { return Util.String2Int(hdn_RegionDetailID.Value) ;}
        set { hdn_RegionDetailID.Value = value.ToString() ;}
    }

    public bool SMSAlert
    {
        get { return chk_SMS_Alert.Checked; }
        set { chk_SMS_Alert.Checked = value; }
    }

    public bool eMailAlert
    {
        get { return chk_eMail_Alert.Checked; }
        set { chk_eMail_Alert.Checked = value; }
    }

    public void ShowAlertRow()
    {
        //trAlert.Style.Add("display", "inline");
        hdnShowAlertRow.Value = "1";
        trAlert.Visible = true;
    }

    public string SetTDCaptionWidth
    {
        set
        {
            td_lblCountry.Style.Add("width", value);
            td_lblEmailID.Style.Add("width", value);
            td_lblFaxNo.Style.Add("width", value);
            td_lblMobile.Style.Add("width", value);
            td_lblPhone1.Style.Add("width", value);
            td_lblPhone2.Style.Add("width", value);
            td_lblPinCode.Style.Add("width", value);
            td_lblRegion.Style.Add("width", value);
            td_lblState.Style.Add("width", value);
            td_lblStdCode.Style.Add("width", value);
            td_lblCity.Style.Add("width", value);
            td_lblEmailAlert.Style.Add("width", value);
            td_lblSMSAlert.Style.Add("width", value);
        }
    }

    public string SetTDDataWidth
    {
        set
        {
            td_Country.Style.Add("width", value);            
            td_Region.Style.Add("width", value);
            td_State.Style.Add("width", value);
            td_txtEmail.Style.Add("width", value);
            td_txtFaxNo.Style.Add("width", value);
            td_txtMobile.Style.Add("width", value);
            td_txtPhone1.Style.Add("width", value);
            td_txtPhone2.Style.Add("width", value);
            td_txtPinCode.Style.Add("width", value);
            td_txtStdCode.Style.Add("width", value);
            td_ddlCity.Style.Add("width", value);
            td_chkEmailAlert.Style.Add("width", value);
            td_chkSMSAlert.Style.Add("width", value);
        }
    }

    #endregion


    #region IView
    /// <summary>
    /// Set City_ID In Edit Mode
    /// </summary>
    public int KeyId 
    {
        get { return _cityId; }
        set { _cityId = value; }
    }

    public bool ValidateWucAddress(Label lbl_Errors)
    {
        PageControls pc = new PageControls();
        TextBox ddlsearchtb;
        ddlsearchtb = (TextBox)ddl_City.Controls[0];

        if (pc.Control_Is_Mandatory(ddlsearchtb) && Util.String2Int(ddl_City.SelectedValue) < 0)
        { 
            lbl_Errors.Text = GetLocalResourceObject("Msg_ddl_City").ToString();
            ddl_City.Focus();
            return false; 
        }

        //else if (txt_Email_Id.Visible && txt_Email_Id.Text.Trim() != "" && txt_Email_Id.Text.IndexOf("/^'\'w+((-'\'w+)|('\'.'\'w+))*'\'@[A-Za-z0-9]+(('\'.|-)[A-Za-z0-9]+)*'\'.[A-Za-z0-9]+$/") == -1)
        //{ lbl_Errors.Text = "Please Enter Email"; return false; }

        else { return true; }
    }

    public string ErrorMessage { set { ;} }

    #endregion



    #region ControlsBind

    public DataTable SetLables
    {
        set
        {
            if (value.Rows.Count != 0)
            {
                DataRow Dr = value.Rows[0];
                lbl_Country.Text = Dr["Country_Name"].ToString();
                lbl_State.Text = Dr["State_Name"].ToString();
                lbl_Zone.Text = Dr["Region_Name"].ToString();
                hdnGSTStateCode.Value = Dr["GSTStateCode"].ToString();
            }
            else
            {
                lbl_Country.Text = "";
                lbl_State.Text = "";
                lbl_Zone.Text = "";
                hdnGSTStateCode.Value = "0";
            }
        }
    }

    public DataTable BindCity
    {

        set
        {
            ddl_City.DataTextField = "City_Name";
            ddl_City.DataValueField = "City_ID";
        }
    }

    #endregion



    #region OtherMethods

    private void DefaultSettings()
    {  
        if (!IsPostBack)
        {
            
            //if (KeyId > 0)
            //{ 
            //    ddl_City.SelectedValue = Util.Int2String(KeyId); 
            //}

            if (txt_Std_Code.Visible == false && txt_Mobile_No.Visible == false)
            {
                tr_Std_Mobile.Visible = false;
            }

            if (txt_Phone1.Visible == false && txt_Phone2.Visible == false)
            {
                tr_PhoneNo.Visible = false;
            }
            //if (lbl_Zone.Visible == false && lblZone.Visible == false)
            //{
            //    UpdatePanel2.Visible = false;
            //}
            
         }
    }

    #endregion



    #region OtherProperties


    public bool EnableAddress
    {
        set
        {
            ddl_City.Enabled = value;
            txt_AddressLine1.Enabled = value;
            txt_AddressLine2.Enabled = value;
            txt_PinCode.Enabled = value;
            txt_Std_Code.Enabled = value;
            txt_Mobile_No.Enabled = value;
            txt_Phone1.Enabled = value;
            txt_Phone2.Enabled = value;
            txt_Fax_No.Enabled = value;
            txt_Email_Id.Enabled = value;
            lbl_Zone.Enabled = value;
            
        }
    }

    public string Header
    {
        set
        {
            //Pnl_Address.GroupingText = value;
        }
    }

    public string PhoneNoText1
    {
        set
        {
            lbl_Phone1.Text = value;
        }
    }

    public string MobileNoText
    {
        set
        {
            lbl_Mobile_No.Text = value;
        }
    }

    public bool VisibleAddress1
    {
        set { tr_AddressLine1.Visible = value; }
    }

    public bool VisibleAddress2
    {
        set { tr_AddressLine2.Visible = value; }
    }

    public bool VisiblePinCode
    {
        set
        {
            lbl_PinCode.Visible = value;
            txt_PinCode.Visible = value;
        }
    }
    public bool VisibleZone
    {
        set
        {
            lblZone.Visible = value;
            lbl_Zone.Visible = value;
        }
    }

    public bool VisibleStdCode
    {
        set
        {
            lbl_Std_Code.Visible = value;
            txt_Std_Code.Visible = value;
        }
    }

    public bool VisibleMobileNo
    {
        set
        {
            lbl_Mobile_No.Visible = value;
            txt_Mobile_No.Visible = value;
        }
    }

    public bool VisiblePhoneNo1
    {
        set
        {
            lbl_Phone1.Visible = value;
            txt_Phone1.Visible = value;
        }
    }

    public bool VisiblePhoneNo2
    {
        set
        {
            lbl_Phone2.Visible = value;
            txt_Phone2.Visible = value;
        }
    }

    public bool VisibleFaxNo
    {
        set
        {
            lbl_Fax_No.Visible = value;
            txt_Fax_No.Visible = value;
        }
    }

    public bool VisibleEmailId
    {
        set
        {
            lbl_Email_Id.Visible = value;
            txt_Email_Id.Visible = value;
        }
    }

   

    #endregion



    #region ControlsEvents

   
    protected void Page_Load(object sender, EventArgs e)
    { 
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {
            ddl_City.DataTextField = "City_Name";
            ddl_City.DataValueField = "City_ID";

            Common ObjCommon = new Common();
            hdf_ResourecString.Value = ObjCommon.GetResourceString("CommonControls/App_LocalResources/WucAddress.ascx.resx");
        }
       
        objAddressPresenter = new AddressPresenter(this, IsPostBack);
        DefaultSettings();

        //ddl_City.DataTextField = "City_Name";
        //ddl_City.DataValueField = "City_ID";
        ddl_City.OtherColumns = RegionDetailID.ToString();
        
     }

    void Page_PreRender(Object o, EventArgs e)
    {
        if (!IsPostBack && KeyId > 0)
        {
            if (OnCityChanged != null)
            {
                OnCityChanged(CityId,  e);
            }
        }
    }
    protected void ddl_City_SelectedIndexChanged(object sender, EventArgs e)
    {
        objAddressPresenter.FillLables();

        if (scm_Address != null)
        {
            scm_Address.SetFocus(ddl_City);
        }
        if (OnCityChanged != null)
        {
            OnCityChanged((object)CityId, e);
        }
        
    }
    #endregion


}

