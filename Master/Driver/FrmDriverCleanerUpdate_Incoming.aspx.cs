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

public partial class Master_Driver_FrmDriverCleanerUpdate_Incoming : System.Web.UI.Page
{
    bool ATS = false;
    private DataSet objDS;

    public int VehicleID
    {
        get { return DDLVehicle.VehicleID; }
        set
        {
            DDLVehicle.VehicleID = value;
            hdn_VehicleID.Value = Util.Int2String(value);
        }
    }

    public int DriverID
    {
        get { return Util.String2Int(hdn_DriverId.Value); }
        set
        {
            hdn_DriverId.Value = Util.Int2String(value);
        }
    }

    public string VehicleCategoryIds
    {
        get { return DDLVehicle.VehicleCategoryIds; }
        set
        {
            DDLVehicle.VehicleCategoryIds = value;
        }
    }

    private string ErrorMessage
    {
        set { lblErrors.Text = value.ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        VehicleCategoryIds = "";

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        DDLVehicle.DDLVehicleIndexChange += new EventHandler(OnDDLVehicleSelection);

        if (!IsPostBack)
        {
            DDLVehicle.Focus();

            hdn_IsCleaner.Value = Request.QueryString["IsCleaner"];

            if (hdn_IsCleaner.Value == "1")
            {
                lbl_DriverCleaner.Text = "Cleaner :";
            }

        }


        if (!IsPostBack)
        {
            DDLVehicle.SetAutoPostBack = true;
            DDLVehicle.VehicleCategoryIds = "";
        }
        lstDriver.Style.Add("visibility", "hidden");
    }





    private bool ValidateUI()
    {
        bool ATS = false;



        if (DDLVehicle.VehicleID <= 0)
        {
            lblErrors.Text = "Please Select Vehicle No";
        }
        else if (DriverID <= 0)
        {
            lblErrors.Text = "Please Select Driver";
            txtDriver.Focus();
        }
        else if (txt_AadharNo.Text.ToString().Length != 12)
        {
            lblErrors.Text = "Please Enter Proper Aadhar No.";
            txt_AadharNo.Focus();
        }
        else if (txt_MobileNo1.Text.ToString().Length != 10)
        {
            lblErrors.Text = "Please Enter Driver Mobile No.";
            txt_MobileNo1.Focus();
        }

        else
        {
            ATS = true;
        }

        return ATS;
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (ValidateUI())
        {
            Save();
        }
    }


    private Message Save()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { 
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Is_Cleaner", SqlDbType.Int,0, Util.String2Int(hdn_IsCleaner.Value)), 
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int,0, VehicleID), 
            objDAL.MakeInParams("@Driver_ID", SqlDbType.Int,0, DriverID), 
            objDAL.MakeInParams("@License_Valip_UpTo_Old", SqlDbType.DateTime ,0, Convert.ToDateTime(hdn_LicenseExpiry.Value)), 
            objDAL.MakeInParams("@License_Valip_UpTo_New", SqlDbType.DateTime ,0, Convert.ToDateTime(hdn_LicenseExpiry.Value)), 
            objDAL.MakeInParams("@Aadhar_No_Old", SqlDbType.VarChar, 20,hdn_AadharNo.Value),
            objDAL.MakeInParams("@Aadhar_No_New", SqlDbType.VarChar, 20,txt_AadharNo.Text),
            objDAL.MakeInParams("@Mobile1", SqlDbType.VarChar, 20,txt_MobileNo1.Text),
            objDAL.MakeInParams("@Mobile2", SqlDbType.VarChar, 20,txt_MobileNo2.Text),
            objDAL.MakeInParams("@Joining_Date", SqlDbType.DateTime ,0, dtp_JoiningDate.SelectedDate), 
            objDAL.MakeInParams("@Remark", SqlDbType.VarChar, 500,txtRemarks.Text),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0, UserManager.getUserParam().UserId)};

        objDAL.RunProc("[dbo].[EF_Opr_DriverCleaner_Update_Incoming_Save]", objSqlParam);

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


    public DataSet GetVehicleInformationOnVehicleChanged()
    {
        DAL objDAL = new DAL();
        SqlParameter[] objSqlParam = { 
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, VehicleID),
            objDAL.MakeInParams("@Vehicle_Category_ID", SqlDbType.Int, 0, VehicleCategoryIds)};
        objDAL.RunProc("EC_Opr_LHPOHireDetails_GetValuesOnVahicleChanged", objSqlParam, ref objDS);
        return objDS;
    }

    public void SetVehicleInfoOnVehicleChanged()
    {
        if (VehicleID > 0)
        {
            hdn_VehicleID.Value = VehicleID.ToString();
            objDS = GetVehicleInformationOnVehicleChanged();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                if (hdn_IsCleaner.Value == "0")
                {
                    txtDriver.Text = objDR["Driver_Name"].ToString();
                    hdn_DriverId.Value = objDR["Driver_ID"].ToString();

                    txt_MobileNo1.Text = objDR["Mobile_No_1"].ToString();
                    txt_MobileNo2.Text = objDR["Mobile_No_2"].ToString();

                    lbl_NickName.Text = objDR["Driver_Nick_Name"].ToString();
                    lbl_Name.Text = objDR["Driver_Name"].ToString();

                    lbl_LicenseNo.Text = objDR["Driver_License_No"].ToString();

                    lbl_LicenseExpiry.Text = objDR["Driver_License_Expiry_Date"].ToString();
                    txt_AadharNo.Text = objDR["Driver_AadharNo"].ToString();
                }
                else
                {
                    txtDriver.Text = objDR["Cleaner_Name"].ToString();
                    hdn_DriverId.Value = objDR["Cleaner_ID"].ToString();

                    txt_MobileNo1.Text = objDR["Mobile_No_1CL"].ToString();
                    txt_MobileNo2.Text = objDR["Mobile_No_2CL"].ToString();

                    lbl_NickName.Text = objDR["Cleaner_Nick_Name"].ToString();
                    lbl_Name.Text = objDR["Cleaner_Name"].ToString();

                    lbl_LicenseNo.Text = objDR["Cleaner_License_No"].ToString();

                    lbl_LicenseExpiry.Text = objDR["Cleaner_License_Expiry_Date"].ToString();
                    txt_AadharNo.Text = objDR["Cleaner_AadharNo"].ToString();

                }
            }
            txtDriver.Focus();
        }
    }

    private void OnDDLVehicleSelection(object sender, EventArgs e)
    {
        VehicleCategoryIds = DDLVehicle.GetVehicleParameter("Vehicle_Category_ID");
        hdn_VehicleCategoryIds.Value = VehicleCategoryIds;

        SetVehicleInfoOnVehicleChanged();
    }

 }
