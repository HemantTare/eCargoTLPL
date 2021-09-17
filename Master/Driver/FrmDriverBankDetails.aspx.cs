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

public partial class Master_Driver_FrmDriverBankDetails : ClassLibraryMVP.UI.Page
{
    Common objCommon = new Common();

    private string DriverID
    {
        set
        {

            if (value == null)
                hdnKeyID.Value = "0";
            else
                hdnKeyID.Value = value.ToString();
        }
        get
        {
            if (Util.String2Int(hdnKeyID.Value) <= 0)
            {
                return "0";
            }
            return hdnKeyID.Value;
        }
    }

    private string DriverName
    {
        set
        {
            lbl_Driver.Text = value.ToString();
        }
        get
        {
            return lbl_Driver.Text.Trim();
        }
    }


    private string Beneficiary
    {
        set
        {
            txtBeneficiary.Text = value.ToString();
        }
        get
        {
            return txtBeneficiary.Text.Trim();
        }
    }

    private string AccountNo
    {
        set
        {
            txtAccountNo.Text = value.ToString();
        }
        get
        {
            return txtAccountNo.Text.Trim();
        }
    }


    private string IFSCCode
    {
        set
        {
            txtIFSCCode.Text = value.ToString();
        }
        get
        {
            return txtIFSCCode.Text.Trim();
        }
    }


    private string Bank
    {
        set
        {
            txtBank.Text = value.ToString();
        }
        get
        {
            return txtBank.Text.Trim();
        }
    }

    

    private string ErrorMessage
    {
        set { lblErrors.Text = value.ToString(); }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        DriverID  = Util.Int2String(Util.DecryptToInt(Request.QueryString["Id"]));

        if (!IsPostBack)
        {
            ReadValues();
        }

    }




    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (AllowToSave())
        {
            Save();
        }
    }

    private bool AllowToSave()
    {
        bool ATS = false;

         
        if (Util.String2Int(DriverID) <= 0)
        {
            lblErrors.Text = "Please Select Driver";
            ScriptManager.SetFocus(txtBeneficiary);
        }
        else if (txtBeneficiary.Text.Trim() == "")
        {
            lblErrors.Text = "Enter Beneficiary Name";
            ScriptManager.SetFocus(txtBeneficiary);
        }
        else if (txtAccountNo.Text.Trim() == "")
        {
            lblErrors.Text = "Enter Account No.";
            ScriptManager.SetFocus(txtAccountNo);
        }
        else if (txtIFSCCode.Text.Trim() == "")
        {
            lblErrors.Text = "Enter IFSC Code";
            ScriptManager.SetFocus(txtIFSCCode);
        }
        else if (txtBank.Text.Trim() == "")
        {
            lblErrors.Text = "Enter Bank Name";
            ScriptManager.SetFocus(txtBank);
        }
        else
            ATS = true;

        return ATS;
    }


    private Message Save()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = { 
            objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@DriverID", SqlDbType.Int,0, Util.String2Int(DriverID)), 
            objDAL.MakeInParams("@Beneficiary", SqlDbType.VarChar, 150,Beneficiary ),
            objDAL.MakeInParams("@AccountNo", SqlDbType.VarChar, 25,AccountNo ),
            objDAL.MakeInParams("@IFSCCode", SqlDbType.VarChar, 20,IFSCCode ),
            objDAL.MakeInParams("@Bank", SqlDbType.VarChar, 150,Bank ),
            objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0, UserManager.getUserParam().UserId)};

        objDAL.RunProc("[dbo].[EF_Master_Driver_Bank_Details_Save]", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {
            ClearVariables();
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

    public void ClearVariables()
    {
        DriverID = "0";

    }


    private void ReadValues()
    {
        DAL objDAL = new DAL();
        DataSet ds = new DataSet();

        SqlParameter[] objSqlParam = {
        objDAL.MakeInParams("@DriverID", SqlDbType.Int,0, Util.String2Int(DriverID ))};

        objDAL.RunProc("[dbo].[EF_Master_Driver_Bank_Details_ReadValues]", objSqlParam, ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = ds.Tables[0].Rows[0];

            DriverName  = objDR["Driver_Name"].ToString();
            Beneficiary = objDR["Beneficiary_Name"].ToString();
            AccountNo  = objDR["Account_No"].ToString();
            IFSCCode  = objDR["IFSC_Code"].ToString();
            Bank  = objDR["Bank_Name"].ToString();

        }

    }


}
