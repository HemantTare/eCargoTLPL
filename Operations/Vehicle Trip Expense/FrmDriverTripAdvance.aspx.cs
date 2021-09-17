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

public partial class Operations_VehicleTripExpense_FrmDriverTripAdvance : ClassLibraryMVP.UI.Page
{
    Raj.EC.Common objComm = new Raj.EC.Common();
    string Mode = "0";
    private DataSet objDS;
    DataTable objDT = new DataTable();
    private DAL objDAL = new DAL();
    string _flag;

    public int keyID
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
    }


    private int TripExpenseAprovalID
    {
        set { hdnTripExpenseAprovalID.Value = value.ToString(); }
        get
        {
            if (Util.String2Int(hdnTripExpenseAprovalID.Value) <= 0)
                return 0;
            else
                return Util.String2Int(hdnTripExpenseAprovalID.Value);
        }
    }

    public String VoucherNo
    {
        set { lbl_VoucherNoValue.Text = value; }
        get { return lbl_VoucherNoValue.Text; }
    }

    private DateTime VoucherDate
    {
        set { dtpVoucherDate.SelectedDate = value; }
        get { return dtpVoucherDate.SelectedDate; }
    }
   
    private decimal AdvanceToBePaid
    {
        set
        {
            lbl_AdvanceToBePaidValue.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(lbl_AdvanceToBePaidValue.Text); }
    }

    private decimal AdvancePaid
    {
        set
        {
            txt_Advance_Paid.Text = Util.Decimal2String(value);
        }
        get { return Util.String2Decimal(txt_Advance_Paid.Text); }
    }

    public string Vehicle_No
    {
        set { lblVehicleNoValue.Text = value; }
        get { return lblVehicleNoValue.Text; }
    }

    public string Driver_Name
    {
        set { lbl_DriverValue.Text = value; }
        get { return lbl_DriverValue.Text; }
    }

    public int PaidBy
    {
        get { return Util.String2Int(Rbl_Paidby.SelectedValue); }
        set
        {
            Rbl_Paidby.SelectedValue = value.ToString();
        }
    }


    private int PaymentByID
    {

        get { return Util.String2Int(ddl_BankPaymentType.SelectedValue); }
        set
        {
            ddl_BankPaymentType.SelectedValue = Util.Int2String(value);
        }
    }

    private DateTime ChequeDate
    {
        set { dtpChequeDate.SelectedDate = value; }
        get { return dtpChequeDate.SelectedDate; }
    }

    public string BankRefNo
    {
        set { txt_BankRefNo.Text = value; }
        get { return txt_BankRefNo.Text; }
    }

    public string Remarks
    {
        set { txt_Remarks.Text = value; }
        get { return txt_Remarks.Text; }
    }

    public string errorMessage
    {
        set { lbl_Errors.Text = value; }
    }


    
    protected void Page_Load(object sender, EventArgs e)
    {
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        Mode = Util.DecryptToString(Request.QueryString["Mode"].ToString());

        errorMessage = "";

        btn_Save_Exit.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Save_Exit, btn_Close));
        btn_Close.Attributes.Add("onclick", objComm.ClickedOnceScript_For_JS_Validation(Page, btn_Close, btn_Save_Exit));



        if (!IsPostBack)
        {
            tr_BankDetails.Attributes.Add("style", "display:none");
            tr_BankDetails2.Attributes.Add("style", "display:none");


            FillBankPaymentType();

            if (keyID <= 0)
            {
                Next_Advance_Voucher_Number();


                string Crypt = "";

                Crypt = System.Web.HttpContext.Current.Request.QueryString["TripExpenseAprovalID"];
                TripExpenseAprovalID = ClassLibraryMVP.Util.DecryptToInt(Crypt);

                if (TripExpenseAprovalID > 0)
                {
                    Vehicle_No  = Util.DecryptToString(Request.QueryString["Vehicle_No"].ToString());
                    Driver_Name  = Util.DecryptToString(Request.QueryString["Driver_Name"].ToString());
                    AdvanceToBePaid  = Util.DecryptToDecimal (Request.QueryString["Advance"].ToString());
                    AdvancePaid = Util.DecryptToDecimal(Request.QueryString["Advance"].ToString());
                }
            }
            else
            {
                ReadValues();
            }
        }

       

        txt_Advance_Paid.Focus();
    }


    private void FillBankPaymentType()
    {
        DAL objDAL = new DAL();
        DataSet ds = new DataSet();
        objDAL.RunProc("EC_Mst_Fill_Bank_Payment_Type", ref ds);

        if (ds.Tables[0].Rows.Count > 0)
        {
            Bind_BankPaymentType(ds.Tables[0]);
        }
    }


    public void Bind_BankPaymentType(DataTable dt)
    {
        ddl_BankPaymentType.DataTextField = "PaymentBy";
        ddl_BankPaymentType.DataValueField = "PaymentByID";
        ddl_BankPaymentType.DataSource = dt;
        ddl_BankPaymentType.DataBind();

    }


    private void Next_Advance_Voucher_Number()
    {
        VoucherNo = objComm.Get_Next_Number();
    }

    
    
    
    
    public bool validateUI()
    {
        bool _isValid = false;

        errorMessage = "";

        return _isValid;
    }

    

    protected void btn_Save_Exit_Click(object sender, EventArgs e)
    {
        
        if (Allow_To_Save())
        {
            btn_Save_Exit.Enabled = false;

            _flag = "SaveAndExit";

            Save();
        }
    }


    private bool Allow_To_Save()
    {
        bool ATS = false;
        lbl_Errors.Text = "";

        if (TripExpenseAprovalID <= 0)
        {
            lbl_Errors.Text = "Please Select Vehicle No";
        }
        else if (AdvanceToBePaid  < AdvancePaid )
        {
            lbl_Errors.Text = "Advance Paid Can Not Be Greater Than Advance To Be Paid";
            txt_Advance_Paid.Focus();
        }
        //else if (Remarks.Trim() == "")
        //{
        //    lbl_Errors.Text = "Please Enter Remark";
        //    txt_Remarks.Focus();
        //}
        else
        {
            ATS = true;
            btn_Save_Exit.Enabled = false;
        }

        return ATS;
    }

    private Message Save()
    {
        DAL objDAL = new DAL();

        SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@Main_ID", SqlDbType.Int,0,UserManager.getUserParam().MainId),
            objDAL.MakeInParams("@Voucher_ID", SqlDbType.Int, 0,keyID ),
            objDAL.MakeInParams("@Voucher_No", SqlDbType.VarChar, 25, VoucherNo ),
            objDAL.MakeInParams("@Voucher_Date", SqlDbType.DateTime, 0,VoucherDate),
            objDAL.MakeInParams("@TripExpenseAprovalID", SqlDbType.Int, 0,TripExpenseAprovalID),
            objDAL.MakeInParams("@AdvanceToBePaid", SqlDbType.Decimal, 0, AdvanceToBePaid),
            objDAL.MakeInParams("@AdvancePaid", SqlDbType.Decimal, 0, AdvancePaid),
            objDAL.MakeInParams("@PaidBy", SqlDbType.Int , 0, PaidBy),       
            objDAL.MakeInParams("@BankPaymentById", SqlDbType.Int, 0,PaymentByID),
            objDAL.MakeInParams("@Cheque_Date", SqlDbType.DateTime, 0,ChequeDate),
            objDAL.MakeInParams("@BankRefNo", SqlDbType.VarChar,30, BankRefNo),           
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 500,Remarks),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0,  UserManager.getUserParam().UserId)};

        objDAL.RunProc("dbo.EF_Opr_DriverTripAdvance_Save", objSqlParam);

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

    private void ReadValues()
    {
        SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Voucher_ID", SqlDbType.Int, 0, keyID) };

        objDAL.RunProc("EF_Opr_DriverTripAdvance_Details", objSqlParam, ref objDS);

        if (objDS.Tables[0].Rows.Count > 0)
        {
            DataRow objDR = objDS.Tables[0].Rows[0];

            VoucherNo = objDR["Voucher_No"].ToString();
            VoucherDate  = Convert.ToDateTime(objDR["Voucher_Date"].ToString());
            TripExpenseAprovalID = Convert.ToInt32(objDR["TripExpenseAprovalID"].ToString());
            Vehicle_No  = objDR["Vehicle_No"].ToString();
            Driver_Name = objDR["Driver_Name"].ToString();
            AdvanceToBePaid = Convert.ToDecimal(objDR["AdvanceToBePaid"].ToString());
            AdvancePaid = Convert.ToDecimal(objDR["AdvancePaid"].ToString());
            Remarks = objDR["Remark"].ToString();
            PaidBy = Convert.ToInt32(objDR["PaidBy"].ToString());
            PaymentByID = Convert.ToInt32(objDR["BankPaymentById"].ToString());
            ChequeDate = Convert.ToDateTime(objDR["Cheque_Date"].ToString());
            BankRefNo = objDR["BankRefNo"].ToString();

            if (PaidBy == 1)
            {
                tr_BankDetails.Attributes.Add("style", "display:block");
                tr_BankDetails2.Attributes.Add("style", "display:block");

            }
            else
            {
                tr_BankDetails.Attributes.Add("style", "display:none");
                tr_BankDetails2.Attributes.Add("style", "display:none");
                dtpChequeDate.SelectedDate = DateTime.Now;
            }

        }

    }

    protected void btn_Close_Click(object sender, EventArgs e)
    {
        Response.Write("<script language='javascript'>{self.close()}</script>");
    }

}
