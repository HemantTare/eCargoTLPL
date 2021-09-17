using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using Raj.EC;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;
using System.Text;
using System.Text.RegularExpressions;

//added by :  Ankit champaneriya
// date    :
// description : un approved voucher cancellation 

public partial class Finance_IBT_WucUnAppVoucherCancellation : System.Web.UI.UserControl, IReverseVoucherView
{
    #region class variable
    private ReverseVoucherPresenter objReverseVoucherPresenter;
    Common objCommon = new Common();
    //int Branch_Ledger_Id;
    //DateTime Voucher_Date;
    //Decimal Total_Amt;
     
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;

        if (Ledger_Id <= 0)
        {
            errorMessage = "Please Select Ledger";
        }
        else if(Reason == string.Empty)
        {
            errorMessage = "Please Enter Reason";
        }
        else if(btn_CostCentre.Visible == true && Amount != Math.Abs(Util.String2Decimal(SessionVoucherCostCentreDT.Compute("Sum(Amount)", "").ToString())))
        {
            errorMessage = "Amount Does not Match in Cost Center";
        }
        else if(btn_BillwiseDetails.Visible == true && Amount != Math.Abs(Util.String2Decimal(SessionVoucherBillByBillDT.Compute("Sum(Amount)", "").ToString())))
        {
            errorMessage = "Amount Does not Match in Bill By Bill";
        }
        else
        {
            _isValid = true;
        }
        return _isValid;
    }

    public string errorMessage
    {
        set
        {
           lbl_Error.Text = value;
        }
    }

    public int keyID
    {
        get
        {
            return Util.DecryptToInt(Request.QueryString["Id"]);
            //return 1;
        }
    }

    #endregion

    #region init interface
    public int Ledger_Id 
    {
        get { return Util.String2Int(ddl_Ledger_Name.SelectedValue);}
        set {ddl_Ledger_Name.SelectedValue = value.ToString();}
    }

    public decimal Debit
    {
        get { return  Util.DecryptToDecimal(Request.QueryString["Debit"]); }
    }

    public decimal Credit
    {
        get { return Util.DecryptToDecimal(Request.QueryString["Credit"]); }
    }

    public decimal Amount
    {
        get { 
            return Debit>0?Debit:Credit;}
    }
    
    public int BranchLedgerId
    {
        get { return Util.DecryptToInt(Request.QueryString["Branch_Ledger_ID"]); }
    }

    public int VoucherTypeID
    {
        get { return Util.DecryptToInt(Request.QueryString["Voucher_Type_ID"]); }
        //get { return 2; }
    }

    public int Voucher_Id
    {
        get { return Util.DecryptToInt(Request.QueryString["Id"]); }
        //get { return 448342; }
    }
 
    public string Voucher_Type
    {
        get
        {
            string type = "";
            switch (VoucherTypeID)
            {
                case 2:
                    type = "CreditNote";
                    break;
                case 3:
                    type = "DebitNote";
                    break;
            }
            return type;
        }


    }

    public int Menu_Item_Id
    {
        get
        {
            int MenuId;
            switch (VoucherTypeID)
            {
                case 2:
                    MenuId = 137;
                    break;
                case 3:
                    MenuId = 116;
                    break;
                default:
                    MenuId = 0;
                    break;
            }
            return MenuId;
        }

    }

    public string Reason 
    {
        get {return txt_Reason.Text.Trim();}
        set {txt_Reason.Text = value;}
    }

    public string CostCentreXML
    {
        get {
            DataSet ds = new DataSet();
            ds.Tables.Add(SessionVoucherCostCentreDT.Copy());
            ds.Tables[0].TableName = "CostCenter";
            return ds.GetXml();
            }
    }

    public string BillByBillXML
    {
        get
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(SessionVoucherBillByBillDT.Copy());
            ds.Tables[0].TableName = "BillByBill";
            return ds.GetXml();
        }
    }

    #endregion

    #region Control Bind
    public DataTable SessionVoucherBillByBillDT
    {
        set { StateManager.SaveState("VoucherBillByBill_DT", value); }
        get { return StateManager.GetState<DataTable>("VoucherBillByBill_DT"); }
    }

    public DataTable SessionVoucherCostCentreDT
    {
        set { StateManager.SaveState("VoucherCostCentre_DT", value); }
        get { return StateManager.GetState<DataTable>("VoucherCostCentre_DT"); }
    }

    public DataTable SessionDropDownCostCentre
    {
        set { StateManager.SaveState("DropDownCostCentre_DT", value); }
        get { return StateManager.GetState<DataTable>("DropDownCostCentre_DT"); }
    }

    public DataTable SessionDropDownRefType
    {
        set { StateManager.SaveState("SessionDropDownRefType_DT", value); }
        get { return StateManager.GetState<DataTable>("SessionDropDownRefType_DT"); }
    }
    #endregion

    #region Page load

    protected void Page_Load(object sender, EventArgs e)
    {
        //Branch_Ledger_Id = 27232;

        btn_Save.Attributes.Add("onclick",objCommon.ClickedOnceScript_For_JS_Validation(Page,btn_Save));

        ddl_Ledger_Name.DataTextField = "Ledger_Name";
        ddl_Ledger_Name.DataValueField = "Ledger_Id";

        ddl_Ledger_Name.OtherColumns = Voucher_Type+","+"BO";

        WucVoucher1.Tr_Heading = false;

        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));
        objReverseVoucherPresenter = new ReverseVoucherPresenter(this, IsPostBack);
        //btn_VoucherView.Attributes.Add("onclick", "return openPopUp('" + _makePath(3) + "')");

        if (!IsPostBack)
        {
            btn_BillwiseDetails.Visible = false;
            btn_CostCentre.Visible = false;
        }

     //   btn_BillwiseDetails.Visible = true;
    }

    protected void ddl_Ledger_Name_TxtChange(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = objReverseVoucherPresenter.GetLedgerParams();

        if (ddl_Ledger_Name.SelectedValue.Trim() == String.Empty || ds.Tables[0].Rows.Count <= 0)
        {
            btn_CostCentre.Visible = false;
            btn_BillwiseDetails.Visible = false;
            return;
        }
          
   
        btn_CostCentre.Attributes.Add("onclick", "return openPopUp('" + _makePath(1) + "')");
        btn_BillwiseDetails.Attributes.Add("onclick", "return openPopUp('" + _makePath(2) + "')");
        
        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IsCostCentre"]) == true)
        {
            btn_CostCentre.Visible = true;
        }
        else
            btn_CostCentre.Visible = false;

        if (Convert.ToBoolean(ds.Tables[0].Rows[0]["IsBillByBill"]) == true)
        {
          btn_BillwiseDetails.Visible = true;
        }
        else
            btn_BillwiseDetails.Visible = false;

        scm_Reverse.SetFocus(ddl_Ledger_Name);
    
    }

    #endregion

    #region Other method
    private string _makePath(int flag)
    {
        Common _objCommon = new Common();
        StringBuilder _path = new StringBuilder(_objCommon.getBaseURL());
        if (flag == 1)
        {
            _path.Append("/Finance/Accounting Vouchers/FrmVoucherCostCentre.aspx");
            _path.Append("?Voucher_Id=" + Util.EncryptInteger(Voucher_Id));//Util.String2Int(Request.QueryString["Voucher_Id"])));
            _path.Append("&Ledger_Id=" + Util.EncryptInteger(Util.String2Int(ddl_Ledger_Name.SelectedValue)));
            _path.Append("&Ledger_Name=" + Util.EncryptString(ddl_Ledger_Name.SelectedItem));
            _path.Append("&Debit=" + Util.EncryptDecimal(Debit));
            _path.Append("&Credit=" + Util.EncryptDecimal(Credit));
            _path.Append("&Voucher_Type=" + Util.EncryptString(Voucher_Type));
        }
        else if (flag == 2)
        {
            _path.Append("/Finance/Accounting Vouchers/FrmVoucherBillByBill.aspx");
            _path.Append("?Voucher_Id=" + Util.EncryptInteger(Voucher_Id));//Util.String2Int(Request.QueryString["Voucher_Id"])));
            _path.Append("&Ledger_Id=" + Util.EncryptInteger(Util.String2Int(ddl_Ledger_Name.SelectedValue)));
            _path.Append("&Ledger_Name=" + Util.EncryptString(ddl_Ledger_Name.SelectedItem));
            _path.Append("&Debit=" + Util.EncryptDecimal(Debit));
            _path.Append("&Credit=" + Util.EncryptDecimal(Credit));
            _path.Append("&Voucher_Type=" + Util.EncryptString(Voucher_Type));
        }
        return _path.ToString();
    }
    #endregion

    #region Event Click
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        objReverseVoucherPresenter.save();
    }
    #endregion
}