using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using Raj.EC;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.FinancePresenter;
using Raj.EC.FinanceView;

//added by :  Ankit champaneriya
// date    :
// description : un approved voucher cancellation 

public partial class Finance_IBT_WucUnAppVoucherCancellation : System.Web.UI.UserControl, IUnAppVoucherCancellationView
{
    #region class variable
    private UnAppVoucherCancellationPresenter objUnAppVoucherCancellationPresenter;

    //int Main_Id;
    //String Crypt;
    //String Crypt_Id;
    //String Type_Crypt = "";
   // int Branch_Ledger_Id;
    DateTime Voucher_Date;
    Decimal Total_Amt;
    #endregion

    #region IView

    public bool validateUI()
    {
        bool _isValid = false;
        _isValid = true;
        return _isValid;
    }

    public string errorMessage
    {
        set
        {
            //lbl_Error.Text = "";// value;
        }
    }

    public int keyID
    {
        get
        {
            //return Util.DecryptToInt(Request.QueryString["Id"]);
            return 1;
        }
    }

    #endregion

    #region otherProperties

    public int crypt_id
    {
        get
        {
            //return Util.String2Int(hdn_CryptId.Value);
            return 4390;
        }
        set
        {
            hdn_CryptId.Value = Util.Int2String(value);
        }
    }
    public String crypt_Type
    {
        get { return "Approved Vouchers"; }
    }

    #endregion

    #region Page load

    protected void Page_Load(object sender, EventArgs e)
    {
      //  Type_Crypt = "UnApproved Vouchers";
        //Branch_Ledger_Id = 27232;
        Voucher_Date = Convert.ToDateTime("03/01/2008");
        Decimal   debit = 0;
        Decimal  credit = 7000;
        if (debit == 0)
            Total_Amt = credit;
        else
            Total_Amt = -(debit);

        ddl_Ledger_Name.DataTextField = "Ledger_Name";
        ddl_Ledger_Name.DataValueField = "Ledger_Id";
        AjaxPro.Utility.RegisterTypeForAjax(typeof(Raj.EF.CallBackFunction.CallBack));

        objUnAppVoucherCancellationPresenter = new UnAppVoucherCancellationPresenter(this, IsPostBack);

        if (!IsPostBack)
        {
            btn_BillwiseDetails.Visible = false;
            btn_CostCentre.Visible = false;
        }
    }

    protected void ddl_Ledger_Name_TxtChange(object sender, EventArgs e)
    {
        if (ddl_Ledger_Name.SelectedValue.Trim() == String.Empty)
        {
            btn_CostCentre.Visible = false;
            btn_BillwiseDetails.Visible = false;
            return;
        }
        int BillApp = Util.String2Int(ddl_Ledger_Name.GetValueAt(1));
        int Ledger_Id = Util.String2Int(ddl_Ledger_Name.SelectedValue);
        string Ledger_Name = ddl_Ledger_Name.SelectedItem;

        if (Convert.ToBoolean(objUnAppVoucherCancellationPresenter.Is_Cost_Centre(Ledger_Id) == true))
        {
            btn_CostCentre.Attributes.Add("onclick", "return openCostCentre(" + Ledger_Id + ",'" + Ledger_Name + "'," + Total_Amt + ")");
            btn_CostCentre.Visible = true;
        }
        else
            btn_CostCentre.Visible = false;

        if (Convert.ToBoolean(BillApp) == true)
        {
            btn_BillwiseDetails.Attributes.Add("onclick", "return openBillWiseDetails(" + crypt_id + ",'" + Voucher_Date + "'," + Ledger_Id + ",'" + Ledger_Name + "'," + Total_Amt + ")");
            btn_BillwiseDetails.Visible = true;
        }
        else
            btn_BillwiseDetails.Visible = false;

        Boolean isApproveVoucher = false;
        if (isApproveVoucher == true)
        {
            btn_VoucherView.Attributes.Add("onclick", "return openVoucherView('" + isApproveVoucher + "'," + crypt_id + ")");
        }
        else
        { btn_VoucherView.Attributes.Add("onclick", "return openVoucherView('" + isApproveVoucher + "'," + crypt_id + ")"); }

        scm_Reverse.SetFocus(ddl_Ledger_Name);
    }

    #endregion

    #region event click
    protected void btn_VoucherView_Click(object sender, EventArgs e)
    {

    }
    #endregion
}