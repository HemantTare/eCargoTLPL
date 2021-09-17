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

public partial class Master_Driver_FrmDriverCleanerUpdateNew : System.Web.UI.Page
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

            hdn_IsCleaner.Value = Request.QueryString["IsCleaner"];

            if (hdn_IsCleaner.Value == "1")
            {
                lbl_DriverCleaner.Text = "Cleaner :";
            }

            tr_Incoming.Style.Add("display", "none");
            tr_VehicleChange.Style.Add("display", "none");
            tr_Outgoing.Style.Add("display", "none");

            txtDriver.Focus();

        }


        lstDriver.Style.Add("visibility", "hidden");
        lstVehicleIncoming.Style.Add("visibility", "hidden");
        lstVehicleOld.Style.Add("visibility", "hidden");
        lstVehicleNew.Style.Add("visibility", "hidden");
        lstVehicleOutgoing.Style.Add("visibility", "hidden");

    }



    private bool ValidateUI()
    {
        bool ATS = false;

        if (DriverID <= 0)
        {
            lblErrors.Text = "Please Select Driver";
            ScriptManager.SetFocus(txtDriver);
        }

        else if (Util.String2Int(rbl_ChangeType.SelectedValue) <= 0)
        {
            lblErrors.Text = "Please Select Change Type";
            ScriptManager.SetFocus(rbl_ChangeType);

        }
        else if (Util.String2Int(hdn_ChangeType.Value) == 1)
        {
            if (Util.String2Int(hdn_VehicleIncoming.Value) <= 0)
            {
                lblErrors.Text = "Please Select Vehicle";
                ScriptManager.SetFocus(txtVehicleIncoming);
            }
            else if (Util.String2Int(hdn_PresentDriverCleanerId.Value) > 0)
            {
                lblErrors.Text = hdn_PresentDriverCleanerName.Value +  " Already Attached To This Vehicle " ;
                ScriptManager.SetFocus(txtVehicleIncoming);
            }

            else
            {
                ATS = true;
            }
        }
        else if (Util.String2Int(hdn_ChangeType.Value) == 2)
        {

            if (Util.String2Int(hdn_VehicleIdOld.Value) <= 0)
            {
                lblErrors.Text = "Driver/Cleaner Is Not Attached To Any Vehicle. ";
                ScriptManager.SetFocus(txtVehicleOld);
            }
            else if (Util.String2Int(hdn_VehicleIdNew.Value) <= 0)
            {
                lblErrors.Text = "Please Select New Vehicle No";
                ScriptManager.SetFocus(txtVehicleNew);
            }
            else if (Util.String2Int(hdn_VehicleIdOld.Value) == Util.String2Int(hdn_VehicleIdNew.Value))
            {
                lblErrors.Text = "Please Select New Vehicle No. Both The Vehicle Nos Are Same.";
                ScriptManager.SetFocus(txtVehicleNew);
            }
            else if (Util.String2Int(hdn_PresentDriverCleanerId.Value) > 0)
            {
                lblErrors.Text = hdn_PresentDriverCleanerName.Value + " Already Attached To This Vehicle ";
                ScriptManager.SetFocus(txtVehicleIncoming);
            }

            else if (dtp_LeavingDate.SelectedDate >= dtp_EffectiveDate.SelectedDate)
            {
                lblErrors.Text = "Effective Date Must Be Greater Than Leaving Date.";
                ScriptManager.SetFocus(dtp_EffectiveDate);
            }

            else
            {
                ATS = true;
            }
        }
        else if (Util.String2Int(hdn_ChangeType.Value) == 3)
        {
            if (Util.String2Int(hdn_VehicleIdOutgoing.Value) <= 0)
            {
                lblErrors.Text = "Driver/Cleaner Is Not Attached To Any Vehicle.";
                ScriptManager.SetFocus(txtVehicleOutgoing);
            }
            else if (txtRemarks.Text.Trim() == "")
            {
                lblErrors.Text = "Please Enter Reason For Leave";
                ScriptManager.SetFocus(txtRemarks);
            }
            else
            {
                ATS = true;
            }

        }

        return ATS;
    }

    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (ValidateUI())
        {
            if (Util.String2Int(hdn_ChangeType.Value) == 1)
            {
                SaveIncoming();
            }
            else if (Util.String2Int(hdn_ChangeType.Value) == 2)
            {
                SaveVehicleChange();
            }
            else if (Util.String2Int(hdn_ChangeType.Value) == 3)
            {
                SaveOutgoing();
            }

        }
    }


    private Message SaveIncoming()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { 
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Is_Cleaner", SqlDbType.Int,0, Util.String2Int(hdn_IsCleaner.Value)), 
            objDAL.MakeInParams("@Driver_ID", SqlDbType.Int,0, DriverID),
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int,0, Util.String2Int(hdn_VehicleIncoming.Value)),
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


    private Message SaveVehicleChange()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { 
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Is_Cleaner", SqlDbType.Int,0, Util.String2Int(hdn_IsCleaner.Value)), 
            objDAL.MakeInParams("@Vehicle_IDOld", SqlDbType.Int,0, Util.String2Int(hdn_VehicleIdOld.Value)), 
            objDAL.MakeInParams("@Driver_ID", SqlDbType.Int,0, DriverID), 
            objDAL.MakeInParams("@Leaving_Date", SqlDbType.DateTime ,0, dtp_LeavingDate.SelectedDate), 
            objDAL.MakeInParams("@Vehicle_IDNew", SqlDbType.Int,0, Util.String2Int(hdn_VehicleIdNew.Value)), 
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

    private Message SaveOutgoing()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { 
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Is_Cleaner", SqlDbType.Int,0, Util.String2Int(hdn_IsCleaner.Value)), 
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int,0, Util.String2Int(hdn_VehicleIdOutgoing.Value)), 
            objDAL.MakeInParams("@Driver_ID", SqlDbType.Int,0, DriverID), 
            objDAL.MakeInParams("@Is_Left", SqlDbType.Int,0, IsLeft), 
            objDAL.MakeInParams("@On_Leave_From", SqlDbType.DateTime ,0, dtp_LeavingDate.SelectedDate), 
            objDAL.MakeInParams("@Reason", SqlDbType.VarChar, 500,txtRemarks.Text),
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



    protected void btn_hidden_Click(object sender, EventArgs e)
    {
        FillDriverCleanerDetails();
    }


    private void FillDriverCleanerDetails()
    {
        DAL _objDAL = new DAL();

        SqlParameter[] objSqlParam ={ _objDAL.MakeInParams("@id", SqlDbType.Int, 0, DriverID),
            _objDAL.MakeInParams("@Is_Cleaner", SqlDbType.BigInt, 0, Util.String2Int(hdn_IsCleaner.Value ))};

        _objDAL.RunProc("EF_Mst_DriverCleaner_Details", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            lbl_NickName.Text = objDR["Nick_Name"].ToString();
            lbl_LicenseNo.Text = objDR["Driver_License_No"].ToString();
            lbl_LicenseExpiry.Text = objDR["License_Expiry_Date"].ToString();
            hdn_LicenseExpiry.Value = objDR["License_Expiry_Date"].ToString();
            lbl_AadharNo.Text = objDR["AadharNo"].ToString();
            hdn_AadharNo.Value = objDR["AadharNo"].ToString();
            lbl_Mobile1.Text = objDR["Mobile_No_1"].ToString();
            lbl_Mobile2.Text = objDR["Mobile_No_2"].ToString();

            if (Util.String2Int(objDR["Vehicle_ID"].ToString()) > 0)
            {
                rbl_ChangeType.Items[0].Enabled = false;

                lbl_CurrentVehicleNo.Text = objDR["Vehicle_No"].ToString();

                txtVehicleOld.Text = objDR["Vehicle_No"].ToString();
                hdn_VehicleIdOld.Value = objDR["Vehicle_ID"].ToString();

                txtVehicleOutgoing.Text = objDR["Vehicle_No"].ToString();
                hdn_VehicleIdOutgoing.Value =  objDR["Vehicle_ID"].ToString();

                txtVehicleOld.Enabled = false;
                txtVehicleOutgoing.Enabled = false;


            }
            else
            {
                rbl_ChangeType.Items[0].Enabled = true ;
                lbl_CurrentVehicleNo.Text = "";


                txtVehicleOld.Text = "";
                hdn_VehicleIdOld.Value = "";

                txtVehicleOutgoing.Text = "";
                hdn_VehicleIdOutgoing.Value = "";

            }


        }
        else
        {
            lbl_NickName.Text = "";
            lbl_LicenseNo.Text = "";
            lbl_LicenseExpiry.Text = "";
            hdn_LicenseExpiry.Value = "";
            lbl_AadharNo.Text = "";
            hdn_AadharNo.Value = "";
            lbl_Mobile1.Text = "";
            lbl_Mobile2.Text = "";

        }

        SetLinks();
    }

    private void SetLinks()
    {
        UserRights uObj;
        uObj = StateManager.GetState<UserRights>("UserRights");
        FormRights fRights;

        fRights = uObj.getForm_Rights(10029);
        bool can_add = fRights.canAdd();
        bool can_edit = fRights.canEdit();

        lbtn_EditDriver.Visible = false;


        if (can_edit == true)
        {
            if (Util.String2Int(hdn_DriverId.Value) > 0)
            {
                hdn_EncreptedDriverId.Value = Util.EncryptInteger(Util.String2Int(hdn_DriverId.Value));

                hdn_DriverMaster_Path.Value = "../../Master/Driver/FrmDriverCleanerUpdate_AddEdit_Master.aspx?IsCleaner=" + hdn_IsCleaner.Value +"&Id=" + hdn_EncreptedDriverId.Value;

                lbtn_EditDriver.Visible = true;
            }
            else
            {
                hdn_DriverMaster_Path.Value = "";
            }
        }
        else
        {
            hdn_DriverMaster_Path.Value = "";
        }

    }

}
