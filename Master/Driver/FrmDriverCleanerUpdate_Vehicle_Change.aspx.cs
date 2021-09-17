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

public partial class Master_Driver_FrmDriverCleanerUpdate_Vehicle_Change : System.Web.UI.Page
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

    public int VehicleIDNew
    {
        get { return Util.String2Int(hdn_VehicleIdNew.Value); }
        set
        {
            hdn_VehicleIdNew.Value = Util.Int2String(value);
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
        lstVehicleNew.Style.Add("visibility", "hidden");

    }


    private bool ValidateUI()
    {
        bool ATS = false;



        if (VehicleIDOld <= 0)
        {
            lblErrors.Text = "Please Select Old Vehicle No";
            txtVehicleOld.Focus();
        }
        else if (VehicleIDNew <= 0)
        {
            lblErrors.Text = "Please Select New Vehicle No";
            txtVehicleNew.Focus();
        }
        else if (VehicleIDOld == VehicleIDNew)
        {
            lblErrors.Text = "Please Select New Vehicle No. Both The Vehicle Nos Are Same.";
            txtVehicleNew.Focus();
        }
        else if (DriverID <= 0)
        {
            lblErrors.Text = "Please Select Driver/Cleaner Name Cannot Be Blank";
       }
        else if (dtp_LeavingDate.SelectedDate >= dtp_EffectiveDate.SelectedDate)
        {
            lblErrors.Text = "Effective Date Must Be Greater Than Leaving Date.";
            dtp_EffectiveDate.Focus();
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
            objDAL.MakeInParams("@Vehicle_IDOld", SqlDbType.Int,0, VehicleIDOld), 
            objDAL.MakeInParams("@Driver_ID", SqlDbType.Int,0, DriverID), 
            objDAL.MakeInParams("@Leaving_Date", SqlDbType.DateTime ,0, dtp_LeavingDate.SelectedDate), 
            objDAL.MakeInParams("@Vehicle_IDNew", SqlDbType.Int,0, VehicleIDNew), 
            objDAL.MakeInParams("@Effective_Date", SqlDbType.DateTime ,0, dtp_EffectiveDate.SelectedDate), 
            objDAL.MakeInParams("@Remark", SqlDbType.VarChar, 500,txtRemarks.Text),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0, UserManager.getUserParam().UserId)};

        objDAL.RunProc("[dbo].[EF_Opr_DriverCleaner_Update_Vehicle_Change_Save]", objSqlParam);

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
