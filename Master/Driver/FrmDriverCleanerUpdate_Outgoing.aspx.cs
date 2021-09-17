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

public partial class Master_Driver_FrmDriverCleanerUpdate_Outgoing : System.Web.UI.Page
{
    bool ATS = false;
    private DataSet objDS;

    public int VehicleIDOld
    {
        get { return Util.String2Int(hdn_VehicleIdOld.Value); }
        set
        {
            hdn_VehicleIdOld.Value = Util.Int2String(value);
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


    private string IsLeft
    {
        set
        {
            rbl_OnLeaveOrLeft.SelectedValue = value;
        }
        get
        {
            return rbl_OnLeaveOrLeft.SelectedValue;
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

            txtVehicleOld.Focus();

            hdn_IsCleaner.Value = Request.QueryString["IsCleaner"];

            if (hdn_IsCleaner.Value == "1")
            {
                lbl_DriverCleaner.Text = "Cleaner :";
            }
            else
            {
                lbl_DriverCleaner.Text = "Driver :";
            }

        }

        lstVehicleOld.Style.Add("visibility", "hidden");

    }


    private bool ValidateUI()
    {
        bool ATS = false;



        if (VehicleIDOld <= 0)
        {
            lblErrors.Text = "Please Select Old Vehicle No";
            txtVehicleOld.Focus();
        }
        else if (DriverID <= 0)
        {
            lblErrors.Text = "Please Select Driver/Cleaner Name Cannot Be Blank";
        }
        else if (txtReason.Text.Trim() == "")
        {
            lblErrors.Text = "Please Enter Reason For Leave";
            txtReason.Focus();
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
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int,0, VehicleIDOld), 
            objDAL.MakeInParams("@Driver_ID", SqlDbType.Int,0, DriverID), 
            objDAL.MakeInParams("@Is_Left", SqlDbType.Int,0, IsLeft), 
            objDAL.MakeInParams("@On_Leave_From", SqlDbType.DateTime ,0, dtp_LeavingDate.SelectedDate), 
            objDAL.MakeInParams("@Reason", SqlDbType.VarChar, 500,txtReason.Text),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0, UserManager.getUserParam().UserId)};

        objDAL.RunProc("[dbo].[EF_Opr_DriverCleaner_Update_Outgoing_Save]", objSqlParam);

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
