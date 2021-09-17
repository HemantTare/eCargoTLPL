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
using ClassLibraryMVP.DataAccess;
using Raj.EC;



public partial class Master_Branch_WucODALocation : System.Web.UI.UserControl, IODALocationView
{
    #region ClassVariables
    ODALocationPresenter objODALocationPresenter;
    Common objCommon = new Common();
    DataRow DR = null;
    int _cityId = -1;
    DAL obj;
    String Call_From, FromGCBookingBranchId;
    #endregion

    #region ControlsValue

    public int LocationId
    {
        set { hdn_LocationId.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdn_LocationId.Value.Trim()); }
    }

    public string LocationName
    {
        set { txt_LocationName.Text = value; }
        get { return txt_LocationName.Text; }
    }
    public int BranchId
    {
        set { ddl_ControllingBranch.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_ControllingBranch.SelectedValue); }

    }
    public int DeliveryTypeID
    {
        set { ddl_delivery_Type.SelectedValue = Util.Int2String(value); }
        get { return Util.String2Int(ddl_delivery_Type.SelectedValue); }
    }
    public int DistanceFromBranch
    {
        set { txt_DistFrmBranch.Text = Util.Int2String(value); }
        get { return Util.String2Int(txt_DistFrmBranch.Text); }
    }
    public string PrimaryPinCode
    {
        set { txt_PrimaryPinCode.Text = value; }
        get { return txt_PrimaryPinCode.Text; }
    }
    public string SecondryPinCode
    {
        set { txt_SecondaryPinCode.Text = value; }
        get { return txt_SecondaryPinCode.Text; }
    }
    public bool IsBooking
    {
        set { Chk_IsBookingAllowed.Checked = value; }
        get { return Chk_IsBookingAllowed.Checked; }
    }
    public bool IsODALocation
    {
        set { Chk_IsODALocation.Checked = value; }
        get { return Chk_IsODALocation.Checked; }
    }

    public bool IsOctroiApplicable
    {
        set { Chk_IsOctroiApplicable.Checked = value; }
        get { return Chk_IsOctroiApplicable.Checked; }
    }
    public Decimal ODAChargeUpto
    {
        set { txt_ChargeUpto.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_ChargeUpto.Text); }
    }
    public Decimal ODAChargeAbove
    {
        set { txt_ChargeAbove.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txt_ChargeAbove.Text); }
    }

    public int CityId
    {
        get { return Util.String2Int(ddl_City.SelectedValue); }

    }
    ////public string CityName
    ////{
    ////    get { return ddl_City.SelectedItem; }
    ////}

    public void SetCityId(string text, string value)
    {
        ddl_City.DataTextField = "City_Name";
        ddl_City.DataValueField = "City_ID";

        Common.SetValueToDDLSearch(text, value, ddl_City);
    }

    #endregion

    #region ControlsBind
    public DataTable BindControllingBranch
    {
        set
        {
            ddl_ControllingBranch.DataSource = value;
            ddl_ControllingBranch.DataTextField = "Branch_Name";
            ddl_ControllingBranch.DataValueField = "Branch_Id";
            ddl_ControllingBranch.DataBind();

            ddl_ControllingBranch.Items.Insert(0, new ListItem("Select One", "0"));


        }
    }

    public DataTable BindDeliveryType
    {
        set
        {
            ddl_delivery_Type.DataTextField = "Delivery_Type";
            ddl_delivery_Type.DataValueField = "Delivery_Type_Id";
            ddl_delivery_Type.DataSource = value;
            ddl_delivery_Type.DataBind();
            //Raj.EC.Common.InsertItem(DDL_BranchType);
        }
    }
    #endregion

    #region IView
    public bool validateUI()
    {
        bool _isValid = false;

        if (Util.String2Int(ddl_City.SelectedValue) < 0)
        {
            lbl_Errors.Text = "Please Enter Valid City";
            ddl_City.Focus();
            return false;
        }

        else if (txt_LocationName.Text == string.Empty)
        {
            errorMessage = "Please Enter Location Name";//GetLocalResourceObject("Msg_LocationName").ToString();
            _isValid = false;
        }
        else if (ddl_ControllingBranch.SelectedValue == "0")
        {
            errorMessage = "Please Select Controlling Branch";// GetLocalResourceObject("Msg_ddlControllingBranch").ToString();
            _isValid = false;
        }
        else if (txt_PrimaryPinCode.Text == string.Empty)
        {
            errorMessage = "Please Enter Primary Pin Code";//GetLocalResourceObject("Msg_PrimaryPinCode").ToString();
            _isValid = false;
        }
        else if (Chk_IsODALocation.Checked == true && txt_ChargeUpto.Text == string.Empty)
        {
            errorMessage = "Please Enter ODA Charges Upto 500 Kg";// GetLocalResourceObject("Msg_ChargesUpto").ToString();
            _isValid = false;
        }
        else if (Chk_IsODALocation.Checked == true && txt_ChargeAbove.Text == string.Empty)
        {
            errorMessage = "Please Enter ODA Charges Above 500 Kg";// GetLocalResourceObject("Msg_ChargesAbove").ToString();
            _isValid = false;
        }
        else if (CheckBranchName() == false)
        {
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
        set{lbl_Errors.Text = value; }
    }
    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }

    #endregion
    #region ControlsEvent

    protected void Page_Load(object sender, EventArgs e)
    {
        ddl_City.DataTextField = "City_Name";
        ddl_City.DataValueField = "City_ID";

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        if (!IsPostBack)
        {
            Raj.EC.Common ObjCommon = new Raj.EC.Common();
            //hdf_ResourecString.Value = ObjCommon.GetResourceString("Master/Branch/App_LocalResources/WucODALocation.ascx.resx");
            hdn_Is_FromLocation.Value = Request.QueryString["Is_From_Location"];
            Call_From = Request.QueryString["Call_From"];

            if (Request.QueryString["BookingBranchId"] != null)
                FromGCBookingBranchId = Request.QueryString["BookingBranchId"].ToString();
            else
                FromGCBookingBranchId = "0";

            if (Util.String2Int(FromGCBookingBranchId) > 0 && Call_From == "GC" && hdn_Is_FromLocation.Value == "1")
            {
                ddl_ControllingBranch.Enabled = false;
                ddl_ControllingBranch.SelectedValue = FromGCBookingBranchId;
            }
        }
        objODALocationPresenter = new ODALocationPresenter(this, IsPostBack);
        IODALocationView objIODALocationView;
        if (IsODALocation == false)
        {
            txt_ChargeAbove.Text = "0";
            txt_ChargeUpto.Text = "0";
            OdaShowHide(IsODALocation);
        }
        else
            OdaShowHide(IsODALocation);

        ddl_City.OtherColumns = "0";
    }

    protected void ddl_City_SelectedIndexChanged(object sender, EventArgs e)
    {
        ////objAddressPresenter.FillLables();

        ////if (scm_Address != null)
        ////{
        ////    scm_Address.SetFocus(ddl_City);
        ////}
        ////if (OnCityChanged != null)
        ////{
        ////    OnCityChanged((object)CityId, e);
        ////}

    }

    private void OdaShowHide(Boolean val)
    {
        txt_ChargeUpto.Visible = val;
        txt_ChargeAbove.Visible = val;
        lbl_ChargesAbove.Visible = val;
        lbl_ChargesUpto.Visible = val;
        lbl_MandatoryRs1.Visible = val;
        lbl_MandatoryRs2.Visible = val;
        lbl_Mandatory1.Visible = val;
        lbl_Mandatory2.Visible = val;
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (validateUI())
        {
            objODALocationPresenter.Save();

            string _Msg = "Saved SuccessFully";

            String popupScript = "";
            Call_From = Request.QueryString["Call_From"];

            if (Call_From == "GC")
            {
                popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();Update_Location_Details();</script>";
            }
            else
            {
                //popupScript = "<script language='javascript'>alert('" + _Msg + "');self.close();</script>";
                string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + LinkUrl + "&DecryptUrl='No'");
            }

            System.Web.UI.ScriptManager.RegisterStartupScript(Upd_Pnl_Location, typeof(String), "PopupScript1", popupScript.ToString(), false);
        }

    }
    #endregion

   public bool CheckBranchName()
    {
        //string Query = "select Branch_Name From EC_Master_Branch where Branch_Name=" + "'txt_LocationName.Text'" ;
        string Query = "select Branch_Name From EC_Master_Branch where Branch_Name= '" + txt_LocationName.Text + "'";
        //DataSet objds = objCommon.Get_Values_Where("EC_Master_Branch", "Branch_Name", "Branch_Name= " + ' txt_LocationName.Text', "Branch_Id", true);
       //DataSet objds = obj.RunQuery(Query);
        DataSet objds = objCommon.EC_Common_Pass_Query(Query);
        if (objds.Tables[0].Rows.Count > 0)
        {
            errorMessage = "Service Location Name Already Exist.Please Enter Other Name";
            return false;
        }
        else
        {
            return true;
        }
    }
}