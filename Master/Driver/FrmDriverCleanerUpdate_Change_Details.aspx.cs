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

public partial class Master_Driver_FrmDriverCleanerUpdate_Change_Details : System.Web.UI.Page
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

    public int VehicleID
    {
        get { return Util.String2Int(hdn_VehicleId.Value); }
        set
        {
            hdn_VehicleId.Value = Util.Int2String(value);
        }
    }

    private string ErrorMessage
    {
        set { lblErrors.Text = value.ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
  
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        
        if (!IsPostBack)
        {

            hdn_IsCleaner.Value = Request.QueryString["IsCleaner"];

            if (hdn_IsCleaner.Value == "1")
            {
                lbl_DriverCleaner.Text = "Cleaner :";
            }

            txtDriver.Focus();
        }


        lstDriver.Style.Add("visibility", "hidden");
    }





    private bool ValidateUI()
    {
        bool ATS = false;



        if (DriverID <= 0)
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
            objDAL.MakeInParams("@Driver_ID", SqlDbType.Int,0, DriverID), 
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int,0, VehicleID), 
            objDAL.MakeInParams("@License_Valip_UpTo_Old", SqlDbType.DateTime ,0, Convert.ToDateTime(hdn_LicenseExpiry.Value)), 
            objDAL.MakeInParams("@License_Valip_UpTo_New", SqlDbType.DateTime ,0, Convert.ToDateTime(hdn_LicenseExpiry.Value)), 
            objDAL.MakeInParams("@Aadhar_No_Old", SqlDbType.VarChar, 20,hdn_AadharNo.Value),
            objDAL.MakeInParams("@Aadhar_No_New", SqlDbType.VarChar, 20,txt_AadharNo.Text),
            objDAL.MakeInParams("@Mobile1", SqlDbType.VarChar, 20,txt_MobileNo1.Text),
            objDAL.MakeInParams("@Mobile2", SqlDbType.VarChar, 20,txt_MobileNo2.Text),
            objDAL.MakeInParams("@Remark", SqlDbType.VarChar, 500,txtRemarks.Text),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0, UserManager.getUserParam().UserId)};

        objDAL.RunProc("[dbo].[EF_Opr_DriverCleaner_Update_Change_Details_Save]", objSqlParam);

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
