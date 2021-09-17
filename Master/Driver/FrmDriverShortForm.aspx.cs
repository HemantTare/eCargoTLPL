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
public partial class Master_Driver_FrmDriverShortForm : ClassLibraryMVP.UI.Page
{
    #region members
    public bool validateUI()
    {
        bool ATS;
        ATS = false;

        if (txt_DriverName.Text.Trim().Length <= 0)
        {
            lblErrors.Text = "Please Enter Driver Name";
            ScriptManager.SetFocus(txt_DriverName);
        }
        else if (txt_LicenseNo.Text.Trim().Length <= 0)
        {
            lblErrors.Text = "Please Enter License No.";
            ScriptManager.SetFocus(txt_LicenseNo);
        }
        else if (dtp_ExpiryDate.SelectedDate <= DateTime.Now)
        {
            lblErrors.Text = "Driver License Is Expired";
            ScriptManager.SetFocus(dtp_ExpiryDate);
        }
        else if (txt_LicenseIssueCity.Text.Trim().Length <= 0)
        {
            lblErrors.Text = "Please Enter License Issueing City";
            ScriptManager.SetFocus(txt_LicenseIssueCity);
        }
        else if (txtMobileNo1.Text.Trim().Length <= 0)
        {
            lblErrors.Text = "Please Enter Mobile No. 1";
            ScriptManager.SetFocus(txtMobileNo1);
        }
        else
        {
            ATS = true;
        }
        return ATS;
    }


    Message objMessage = new Message();

    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            hdnKeyID.Value = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));

            txt_DriverName.Text = Request.QueryString["DriverName"];

            txt_DriverName.Focus();
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

        SqlParameter[] objSqlParam = 
        { 
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Driver_Name", SqlDbType.VarChar, 100,txt_DriverName.Text), 
            objDAL.MakeInParams("@Mobile_No",SqlDbType.VarChar,25,txtMobileNo1.Text),
            objDAL.MakeInParams("@Phone_No",SqlDbType.VarChar,25,txtMobileNo2.Text),
            objDAL.MakeInParams("@Driver_License_No",SqlDbType.VarChar,50,txt_LicenseNo.Text),
            objDAL.MakeInParams("@License_Issue_City",SqlDbType.VarChar,50,txt_LicenseIssueCity.Text),
            objDAL.MakeInParams("@License_Expiry_Date",SqlDbType.DateTime,0,dtp_ExpiryDate.SelectedDate),
            objDAL.MakeInParams("@Is_Cleaner", SqlDbType.Bit,0,0),
            objDAL.MakeInParams("@Is_Company_Driver", SqlDbType.Bit,0,1),
            objDAL.MakeInParams("@User_Id",SqlDbType.Int,0,UserManager.getUserParam().UserId)
        };


        objDAL.RunProc("[rstil7].[EF_Master_Driver_Short_Form_Save]", objSqlParam);


        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);


        if (objMessage.messageID == 0)
        {
            lblErrors.Text = "Saved SuccessFully";
            hdnKeyID.Value = Convert.ToString(objSqlParam[2].Value);

            string popupScript = "<script language='javascript'>updateparentwindow('" + txt_DriverName.Text + "');</script>";
            ScriptManager.RegisterStartupScript(Page, typeof(string), "PopupScript", popupScript.ToString(), false);

        }
        else
        {
            lblErrors.Text = objMessage.message;
        }
        return objMessage;

    }
}
