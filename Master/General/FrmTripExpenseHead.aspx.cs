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
using Raj.EC;
using ClassLibraryMVP.Security;
using System.Data.SqlClient;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;

public partial class Master_General_FrmTripExpenseHead : ClassLibraryMVP.UI.Page
{
    Common ObjCommon = new Common();
    private DAL objDAL = new DAL();
    private DataSet objDS;
    public int ExpenseHeadID
    {
        set { hdnExpenseHeadID.Value = Util.Int2String(value); }
        get { return Util.String2Int(hdnExpenseHeadID.Value); }
    }
    public string ExpenseHead
    {
        set { txtExpenseHead.Text = value; }
        get { return txtExpenseHead.Text; }
    }
    public int SrNo
    {
        set { txtSrNo.Text = Util.Int2String(value); }
        get { return Util.String2Int(txtSrNo.Text == string.Empty ? "0" : txtSrNo.Text); }
    }
    public decimal RsPerDay
    {
        set { txtRsPerDay.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txtRsPerDay.Text == string.Empty ? "0" : txtRsPerDay.Text); }
    }
    public decimal FixedAmount
    {
        set { txtFixedRs.Text = Util.Decimal2String(value); }
        get { return Util.String2Decimal(txtFixedRs.Text == string.Empty ? "0" : txtFixedRs.Text); }
    }
    public string Description
    {
        set { txtDescription.Text = value; }
        get { return txtDescription.Text; }
    }
    public bool IsActive
    {
        set { chkIsActive.Checked = value; }
        get { return chkIsActive.Checked; }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ExpenseHeadID = Util.DecryptToInt(Request.QueryString["Id"]);

            if (ExpenseHeadID > 0)
            {
                TrIsActive.Visible = true;
                ReadValues();
            }
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        if (AllowToSave())
        {
            Save();
        }
    }
    private void ReadValues()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@ExpenseHeadID", SqlDbType.Int, 0, ExpenseHeadID) };

        objDAL.RunProc("EC_Mst_Trip_Expense_Head_ReadValues", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            ExpenseHead = objDR["TripExpenseHead"].ToString();
            SrNo = Util.String2Int(objDR["SrNo"].ToString());
            RsPerDay = Util.String2Decimal(objDR["RsPerDay"].ToString());
            FixedAmount = Util.String2Decimal(objDR["FixedAmount"].ToString());
            Description = objDR["Description"].ToString();
            IsActive = Util.String2Bool(objDR["Is_Active"].ToString());

            if (RsPerDay > 0)
            {
                txtRsPerDay.Enabled = true;
                txtFixedRs.Enabled = false;
            }
            else if (FixedAmount >0)
            {
                txtRsPerDay.Enabled = false;
                txtFixedRs.Enabled = true;
            }
        }
    }

    private bool AllowToSave()
    {
        bool ATS = false;
        lbl_Errors.Text = "Fields with * mark are mandatory";
        if (ExpenseHead.Trim() == string.Empty)
        {
            lbl_Errors.Text = "Please enter Trip Expense Head";
            ScriptManager.SetFocus(txtExpenseHead);
        }
        else if (SrNo <= 0)
        {
            lbl_Errors.Text = "Please enter Sr No.";
            ScriptManager.SetFocus(txtSrNo);
        }
        //else if (RsPerDay <= 0)
        //{
        //    lbl_Errors.Text = "Please enter Rs Per Day.";
        //    ScriptManager.SetFocus(txtRsPerDay);
        //}
        //else if (FixedAmount <= 0)
        //{
        //    lbl_Errors.Text = "Please enter Fixed Rs.";
        //    ScriptManager.SetFocus(txtFixedRs);
        //}
        else
        {
            ATS = true;
        }

        return ATS;
    }
    private Message Save()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@TripExpenseHeadID", SqlDbType.Int, 0,ExpenseHeadID),
            objDAL.MakeInParams("@TripExpenseHead", SqlDbType.VarChar, 50, ExpenseHead),
            objDAL.MakeInParams("@SrNo", SqlDbType.Int, 0,SrNo),
            objDAL.MakeInParams("@RsPerDay", SqlDbType.Decimal, 0,RsPerDay),
            objDAL.MakeInParams("@FixedAmount", SqlDbType.Decimal, 0,FixedAmount),
            objDAL.MakeInParams("@Decription", SqlDbType.VarChar, 250,Description),
            objDAL.MakeInParams("@IsActive", SqlDbType.Bit, 0,IsActive),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0,  UserManager.getUserParam().UserId)};

        objDAL.RunProc("dbo.EC_Mst_TripExpenseHead_Save", objSqlParam);

        Message objMessage = new Message();
        objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
        objMessage.message = Convert.ToString(objSqlParam[1].Value);

        if (objMessage.messageID == 0)
        {
            string _Msg;
            _Msg = "Saved SuccessFully";
            System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
        }
        else
        {
            lbl_Errors.Text = objMessage.message;
        }

        return objMessage;
    }  
}
