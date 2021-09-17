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
using System.Data.SqlClient;
using ClassLibrary;
using ClassLibraryMVP.General;
using Raj.EC;
public partial class Master_Sales_FrmRegularClientPendingAddress : ClassLibraryMVP.UI.Page
{

    Common objCommon = new Common();

    #region members

    private int DeliveryTypeID
    {
        set
        {

            ddl_dly_type.SelectedValue = Util.Int2String(value);
        }
        get
        {
            return Convert.ToInt32(ddl_dly_type.SelectedValue);
        }
    }

    private void FillDeliveryType()
    {
        string query = "Select Delivery_Type_ID,Delivery_Type from EC_Master_Delivery_Type order by Delivery_Type_ID Desc";
        DataSet ds = new DataSet();
        ds = objCommon.EC_Common_Pass_Query(query);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddl_dly_type.DataSource = ds;
            ddl_dly_type.DataValueField = "Delivery_Type_ID";
            ddl_dly_type.DataTextField = "Delivery_Type";
            ddl_dly_type.DataBind();
        }
        else
        {
            ddl_dly_type.Items.Clear();
        }
    }

    public bool validateUI()
    {
        bool ATS;
        ATS = false;
        if (txtAddress.Text.Trim() == "")
        {
            txtAddress.Focus();
            lblErrors.Text = "Enter Address";
        }
        else if (Chk_IsServiceTaxPayable.Checked == true  && !validateGST())
        {
            //errorMessage = "Please Enter 15 Digits GST No";
            ScriptManager.SetFocus(txt_CstNo);
            ATS = false;
        }
        else
        {
            ATS = true;
        }
        return ATS;
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

    Message objMessage = new Message();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));
            hdnGCId.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["GCId"]));

            FillDeliveryType();
            
            ReadValues();
            fillDlyArea(Convert.ToInt32(hdnCityID.Value));
            txtMobileNo.Focus();

            lnk_btnClientSearch.Text = "Client Search";
            lnk_btnClientSearch.Attributes.Add("onclick", "return Clientsearch()");

            lnk_btnGoogleMap.Text = "Google Maps";
            lnk_btnGoogleMap.Attributes.Add("onclick", "return GoogleMaps()");

            lnk_btnGSTNoSearch.Text = "Search GSTNo.";
            lnk_btnGSTNoSearch.Attributes.Add("onclick", "return SearchGSTNo()");
        }
    }
    private void ReadValues()
    {
        if (Util.String2Int(hdnKeyID.Value) > 0)
        {
            DAL objDAL = new DAL();
            DataSet ds = new DataSet();
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Client_Id", SqlDbType.Int, 0, Util.String2Int(hdnKeyID.Value)) };
            objDAL.RunProc("EC_Mst_Regular_Client_ReadValues", objSqlParam, ref ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = ds.Tables[0].Rows[0];

                lbl_ClientName.Text = objDR["Client_Name"].ToString();
                txtMobileNo.Text = objDR["Mobile_No"].ToString();
                txtPhone1.Text = objDR["Phone1"].ToString();
                txtAddress.Text = objDR["Address1"].ToString();
                lblCity.Text = objDR["City_name"].ToString();
                hdnCityID.Value = objDR["City_ID"].ToString();
                txt_PinCode.Text = objDR["Pin_Code"].ToString();
                DeliveryTypeID = Util.String2Int(objDR["Delivery_Type_ID"].ToString());
                hdn_GST_State_Code.Value = objDR["GSTStateCode"].ToString();
                Chk_IsServiceTaxPayable.Checked = Util.String2Bool(objDR["Is_Service_Tax_Applicable"].ToString());
                txt_CstNo.Text = objDR["CST_TIN_No"].ToString();

            }
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {


        if (validateUI())
            Save();

       
    }

    private Message Save()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { 
       objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
       objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
       objDAL.MakeInParams("@KeyID",SqlDbType.Int,0, Util.String2Int(hdnKeyID.Value)),
       objDAL.MakeInParams("@AddressLine1", SqlDbType.VarChar,100,txtAddress.Text),
       objDAL.MakeInParams("@PinCode", SqlDbType.NVarChar,15,""),
       objDAL.MakeInParams("@Phone1", SqlDbType.NVarChar,20,txtPhone1.Text),
       objDAL.MakeInParams("@MobileNo",SqlDbType.NVarChar,20,txtMobileNo.Text),
       objDAL.MakeInParams("@UserId",SqlDbType.Int,0,UserManager.getUserParam().UserId),
       objDAL.MakeInParams("@DeliveryAreaID",SqlDbType.Int,0,ddlDeliveryArea.SelectedValue),
       objDAL.MakeInParams("@GC_ID",SqlDbType.Int,0,Util.String2Int(hdnGCId.Value)),
       objDAL.MakeInParams("@Delivery_Type_ID",SqlDbType.Int,0,DeliveryTypeID),
       objDAL.MakeInParams("@Is_Service_Tax_Applicable",SqlDbType.Bit,0,Chk_IsServiceTaxPayable.Checked),
       objDAL.MakeInParams("@CST_TIN_No", SqlDbType.NVarChar,20,txt_CstNo.Text)
        };


       objDAL.RunProc("[dbo].[EC_Mst_RegularClient_PendingAddress_Save]", objSqlParam);


        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            lblErrors.Text = "Saved SuccessFully";
            hdnKeyID.Value = Convert.ToString(objSqlParam[2].Value);

            string popupScript = "<script language='javascript'>updateparentwindow('" + lbl_ClientName.Text + "');</script>";
            ScriptManager.RegisterStartupScript(Page, typeof(string), "PopupScript", popupScript.ToString(), false);
        }
        else
        {
            lblErrors.Text = objMessage.message; 
        }
        return objMessage; 
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
    }


    public bool validateGST()
    {
        bool _isValid = false;
        string gstinVal = txt_CstNo.Text.Trim();
        string GSTCode = hdn_GST_State_Code.Value;

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
                lblErrors.Text = "Please Enter 15 digit GST No.";
                ScriptManager.SetFocus(txt_CstNo);
                _isValid = false;
            }
            else if (first2val != GSTCode)
            {
                lblErrors.Text = "First 2 digits must be GST state code : " + first2val;
                ScriptManager.SetFocus(txt_CstNo);
                _isValid = false;
            }
            else if (reggstsecond5val.IsMatch(second5val) == false)
            {
                lblErrors.Text = "From 3rd digit to 7th digit must be characters :" + second5val;
                ScriptManager.SetFocus(txt_CstNo);
                _isValid = false;
            }
            else if (reggstthird4val.IsMatch(third4val) == false)
            {
                lblErrors.Text = "From 8th digit to 11th digit must be numbers :" + third4val;
                ScriptManager.SetFocus(txt_CstNo);
                _isValid = false;
            }
            else if (regchar.IsMatch(fourth1Val) == false)
            {
                lblErrors.Text = "12th digit must be character :" + fourth1Val;
                ScriptManager.SetFocus(txt_CstNo);
                _isValid = false;
            }
            else if (sixth1Val.ToUpper() != "Z")
            {
                lblErrors.Text = "14th digit must be Z :" + sixth1Val;
                ScriptManager.SetFocus(txt_CstNo);
                _isValid = false;
            }
            else if (reggst.IsMatch(gstinVal) == false)
            {
                lblErrors.Text = "Please Enter Valid GST No.";
                ScriptManager.SetFocus(txt_CstNo);
                _isValid = false;
            }
            else
                _isValid = true;
        }
        else
        {
            lblErrors.Text = "Please Enter 15 digit GST No.";
            ScriptManager.SetFocus(txt_CstNo);
            _isValid = false;
        }
        return _isValid;
    }
}
