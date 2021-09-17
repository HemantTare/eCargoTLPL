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
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using Raj.EC;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.Security;

public partial class Master_Driver_FFrmDriverCleanerUpdate_AddEdit_Master : System.Web.UI.Page
{
    bool ATS = false;
    private DataSet objDS;

    public int DriverID
    {
        get { return Util.String2Int(hdn_DriverId.Value); }
        set
        {
            hdn_DriverId.Value = Util.Int2String(value);
        }
    }


    private string ErrorMessage
    {
        set { lblErrors.Text = value.ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {


        if (!IsPostBack)
     
        {
            hdn_IsCleaner.Value = Request.QueryString["IsCleaner"];

            DriverID = Util.DecryptToInt(Request.QueryString["Id"]);
            ReadValues();

            if (hdn_IsCleaner.Value == "1")
            {
                lbl_DriverCleaner.Text = "Cleaner :";
            }

        }


    }

    private void ReadValues()
    {
        DAL _objDAL = new DAL();

        SqlParameter[] objSqlParam ={ _objDAL.MakeInParams("@id", SqlDbType.Int, 0, DriverID),
            _objDAL.MakeInParams("@Is_Cleaner", SqlDbType.BigInt, 0, 0)};

        _objDAL.RunProc("EF_Mst_DriverCleaner_Details", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            txtDriverName.Text = objDR["Driver_Name"].ToString();
            txtNickName.Text = objDR["Nick_Name"].ToString();
            txtLicenceNo.Text = objDR["Driver_License_No"].ToString();
            dtp_LicenseExpiryDate.SelectedDate  = Convert.ToDateTime(objDR["License_Expiry_Date"].ToString());
            txtAadharNo.Text = objDR["AadharNo"].ToString();
            txt_MobileNo1.Text = objDR["Mobile_No_1"].ToString();
            txt_MobileNo2.Text = objDR["Mobile_No_2"].ToString();


        }
    }


    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (ValidateUI())
        {
            Save();
        }
    }


    private bool ValidateUI()
    {
        bool ATS = false;


        if (txtDriverName.Text.Trim() == "")
        {
            lblErrors.Text = "Name Cannot Be Blank.";
            ScriptManager.SetFocus(txtDriverName);
        }
        else if (txtNickName.Text.Trim() == "")
        {
            lblErrors.Text = "Nick Name Cannot Be Blank.";
            ScriptManager.SetFocus(txtNickName);
        }
        //else if (txtLicenceNo.Text.Trim() == "")
        //{
        //    lblErrors.Text = "License No. Cannot Blank.";
        //    ScriptManager.SetFocus(txtLicenceNo);
        //}

        else if ( Util.String2Int(hdn_IsCleaner.Value) == 0 && dtp_LicenseExpiryDate.SelectedDate  <= DateTime.Now )
        {
            lblErrors.Text = "License Is Already Expired.";
            ScriptManager.SetFocus(dtp_LicenseExpiryDate );
        }

        //else if (txtAadharNo.Text.ToString().Length  != 12)
        //{
        //    lblErrors.Text = "Please Enter Aadhar No.";
        //    ScriptManager.SetFocus(txtAadharNo);
        //}

        else if (txt_MobileNo1.Text.ToString().Length != 10)
        {
            lblErrors.Text = "Please Enter Mobile No.";
            ScriptManager.SetFocus(txt_MobileNo1);

        }

        else
        {
            ATS = true;
        }

        return ATS;
    }


    private Message Save()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { 
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Driver_ID", SqlDbType.Int,0, DriverID), 
            objDAL.MakeInParams("@Driver_Name", SqlDbType.VarChar ,50, txtDriverName.Text), 
            objDAL.MakeInParams("@Nick_Name", SqlDbType.VarChar  ,50, txtNickName.Text), 
            objDAL.MakeInParams("@Driver_License_No ", SqlDbType.VarChar, 100,txtLicenceNo.Text),
            objDAL.MakeInParams("@License_Expiry_Date", SqlDbType.DateTime , 0,dtp_LicenseExpiryDate.SelectedDate),
            objDAL.MakeInParams("@Mobile1", SqlDbType.VarChar, 20,txt_MobileNo1.Text),
            objDAL.MakeInParams("@Mobile2", SqlDbType.VarChar, 20,txt_MobileNo2.Text),
            objDAL.MakeInParams("@Aadhar_No", SqlDbType.VarChar, 20,txtAadharNo.Text),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0, UserManager.getUserParam().UserId)};

        objDAL.RunProc("[dbo].[EF_Opr_DriverCleaner_Update_Edit_DriverCleaner_Save]", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {
            ErrorMessage = "Saved SuccessFully";
            string _Msg = "Saved SuccessFully";


            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));

        }
        else
        {
            ErrorMessage = objMessage.message;
        }
        return objMessage;
    }

 }
