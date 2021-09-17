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
public partial class Master_Sales_FrmRegularClientGC : ClassLibraryMVP.UI.Page
{
    #region members
    public bool validateUI()
    {
        bool ATS;
        ATS = false;
        if (hdnCityID.Value == "0")
        {
            lblErrors.Text = "City cannot be blank";
        }
        else if (ChkIsServiceTaxPayableByClient.Checked == true && !validateGST())
        {
            //lblErrors.Text = "Please Enter 15 Digits GST No";
            txtCSTNo.Focus();
        }
        else if (txtCSTNo.Text.Trim().Length > 0 && !validateGST())
        {
            //lblErrors.Text = "Please Enter 15 Digits GST No";
            txtCSTNo.Focus();
        }
        else
        {
            ATS = true;
        }
        return ATS;
    }

    public bool validateGST()
    {
        bool _isValid = false;
        string gstinVal = txtCSTNo.Text.Trim();
        string GSTCode = hdnGSTStateCode.Value;

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
                _isValid = false;
            }
            else if (first2val != GSTCode)
            {
                lblErrors.Text = "First 2 digits must be GST state code : " + first2val;
                txtCSTNo.Focus();
                _isValid = false;
            }
            else if (reggstsecond5val.IsMatch(second5val) == false)
            {
                lblErrors.Text = "From 3rd digit to 7th digit must be characters :" + second5val;
                txtCSTNo.Focus();
                _isValid = false;
            }
            else if (reggstthird4val.IsMatch(third4val) == false)
            {
                lblErrors.Text = "From 8th digit to 11th digit must be numbers :" + third4val;
                txtCSTNo.Focus();
                _isValid = false;
            }
            else if (regchar.IsMatch(fourth1Val) == false)
            {
                lblErrors.Text = "12th digit must be character :" + fourth1Val;
                txtCSTNo.Focus();
                _isValid = false;
            }
            else if (sixth1Val.ToUpper() != "Z")
            {
                lblErrors.Text = "14th digit must be Z :" + sixth1Val;
                txtCSTNo.Focus();
                _isValid = false;
            }
            else if (reggst.IsMatch(gstinVal) == false)
            {
                lblErrors.Text = "Please Enter Valid GST No.";
                _isValid = false;
            }
            else
                _isValid = true;
        }
        else
        {
            lblErrors.Text = "Please Enter 15 digit GST No.";
            _isValid = false;
        }
        return _isValid;
    }
    string txt_ConsignorName, txt_ConsigneeName, Is_Consignor;
    Message objMessage = new Message();
    
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));
            txt_ConsignorName = Request.QueryString["txt_ConsignorName"];
            txt_ConsigneeName = Request.QueryString["txt_ConsigneeName"]; 
            Is_Consignor = Request.QueryString["Is_Consignor"];            
            ReadValues();
            txtMobileNo.Focus();
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

                txtClientName.Text = objDR["Client_Name"].ToString();
                txtMobileNo.Text = objDR["Mobile_No"].ToString();
                txtPhone1.Text = objDR["Phone1"].ToString();
                txtAddress.Text = objDR["Address1"].ToString();
                lblCity.Text = objDR["City_name"].ToString();
                hdnBranchID.Value = objDR["Branch_ID"].ToString();
                hdnCityID.Value = objDR["City_ID"].ToString();
                ChkIsServiceTaxPayableByClient.Checked = Util.String2Bool(objDR["Is_Service_Tax_Applicable"].ToString());
                txtCSTNo.Text = objDR["CST_TIN_No"].ToString();
                hdnGSTStateCode.Value = objDR["GSTStateCode"].ToString();
            }
        }
        else
        {
            string branchID = Request.QueryString["BranchID"];
            string ServiceLocationID = Request.QueryString["ServiceLocationID"];

            if (branchID == "" || Util.String2Int(branchID) <=0)
            {
                hdnBranchID.Value = "0";
                hdnCityID.Value = "0";
            }
            else
            {
                DAL objDAL = new DAL();
                DataSet ds = new DataSet();

                //SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, Util.String2Int(ServiceLocationID)) };
                //objDAL.RunProc("EC_Mst_GetCityStateFromBranch", objSqlParam, ref ds);

                SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Service_Location_Id", SqlDbType.Int, 0, Util.String2Int(ServiceLocationID)) };
                objDAL.RunProc("EC_Mst_GetCityStateFromServiceLocation", objSqlParam, ref ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = ds.Tables[0].Rows[0];
                    hdnBranchID.Value = branchID;
                    hdnCityID.Value = objDR["City_ID"].ToString();
                    lblCity.Text = objDR["City_name"].ToString();
                    hdnGSTStateCode.Value = objDR["GSTStateCode"].ToString();
                }
            }
            if (Is_Consignor == "1")
            {
                txtClientName.Text = txt_ConsignorName;
            }
            else
            {
                txtClientName.Text = txt_ConsigneeName;            
            }
            txtClientName.Focus();
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        //string popupScript = "<script language='javascript'>updateparentwindow('" + txtClientName.Text + "');</script>";
        //ScriptManager.RegisterStartupScript(Page, typeof(string), "PopupScript", popupScript.ToString(), false);

        if (validateUI())
            Save();

        if (objMessage.messageID == 0)
        {

            string popupScript = "<script language='javascript'>updateparentwindow('" + txtClientName.Text + "');</script>";
            ScriptManager.RegisterStartupScript(Page, typeof(string), "PopupScript", popupScript.ToString(), false);
            //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx");
        }
    }

    private Message Save()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { 
       objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
       objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
       objDAL.MakeOutParams("@ClientId", SqlDbType.Int, 0), 
       objDAL.MakeInParams("@KeyID",SqlDbType.Int,0, Util.String2Int(hdnKeyID.Value)),
       objDAL.MakeInParams("@RegularClientName", SqlDbType.VarChar, 100,txtClientName.Text), 
       objDAL.MakeInParams("@ContactPerson",SqlDbType.VarChar,50,""),
       objDAL.MakeInParams("@AddressLine1", SqlDbType.VarChar,100,txtAddress.Text),
       objDAL.MakeInParams("@AddressLine2", SqlDbType.VarChar,100,""),
       objDAL.MakeInParams("@BranchId", SqlDbType.Int,0,hdnBranchID.Value),
       objDAL.MakeInParams("@CityId", SqlDbType.Int,0,hdnCityID.Value),
       objDAL.MakeInParams("@PinCode", SqlDbType.NVarChar,15,""),
       objDAL.MakeInParams("@StdCode", SqlDbType.NVarChar,15,""),
       objDAL.MakeInParams("@Phone1", SqlDbType.NVarChar,20,txtPhone1.Text),
       objDAL.MakeInParams("@Phone2", SqlDbType.NVarChar,20,""),
       objDAL.MakeInParams("@MobileNo",SqlDbType.NVarChar,20,txtMobileNo.Text),
       objDAL.MakeInParams("@Fax", SqlDbType.NVarChar,20,""),
       objDAL.MakeInParams("@EmailId", SqlDbType.VarChar,100,""),
       objDAL.MakeInParams("@SMS_Alert",SqlDbType.Bit,0,true),
       objDAL.MakeInParams("@eMail_Alert",SqlDbType.Bit,0,false),
       objDAL.MakeInParams("@IsServiceTaxApplicable",SqlDbType.Bit,0,ChkIsServiceTaxPayableByClient.Checked),
       objDAL.MakeInParams("@CSTNo",SqlDbType.VarChar,50,txtCSTNo.Text),
       objDAL.MakeInParams("@ServiceTaxNo",SqlDbType.VarChar,50,""),
       objDAL.MakeInParams("@UserId",SqlDbType.Int,0,UserManager.getUserParam().UserId),
       objDAL.MakeInParams("@DeliveryAreaID",SqlDbType.Int,0,0),
       objDAL.MakeInParams("@CategoryID",SqlDbType.Int,0,0),
       objDAL.MakeInParams("@Delivery_Type_Id",SqlDbType.Int,0,2),
       objDAL.MakeInParams("@Is_ToPay_Allowed",SqlDbType.Bit,0,1),
       objDAL.MakeInParams("@Client_Group_ID",SqlDbType.Int,0,0),
       objDAL.MakeInParams("@Is_Casual_Taxable",SqlDbType.Bit,0,0),
       objDAL.MakeInParams("@Remarks", SqlDbType.VarChar,1000,""),
       objDAL.MakeInParams("@Landmark1ID",SqlDbType.Int,0,0),
       objDAL.MakeInParams("@Landmark2ID",SqlDbType.Int,0,0),
       objDAL.MakeInParams("@GSTName",SqlDbType.VarChar,100,""),
       objDAL.MakeInParams("@IsWithCompleteDetails",SqlDbType.Int ,0,0)
        };


        objDAL.RunProc("[dbo].[EC_Mst_RegularClient_Save]", objSqlParam);


        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            lblErrors.Text = "Saved SuccessFully";
            hdnKeyID.Value = Convert.ToString(objSqlParam[2].Value); 
        }
        else
        {
            lblErrors.Text = objMessage.message; 
        }
        return objMessage; 
    }
}
