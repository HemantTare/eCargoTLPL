using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;

public class PartyRecieptVoucherPresenter:Presenter
{

    private IPartyRecieptVoucherView objIPartyRecieptVoucherView;
    private PartyRecieptVoucherModel objPartyRecieptVoucherModel;
    private DataSet objDS;

    public PartyRecieptVoucherPresenter(IPartyRecieptVoucherView PartyRecieptVoucherView,bool isPostBack)
	{
        objIPartyRecieptVoucherView = PartyRecieptVoucherView;
        objPartyRecieptVoucherModel = new PartyRecieptVoucherModel(objIPartyRecieptVoucherView);

        base.Init(objIPartyRecieptVoucherView, objPartyRecieptVoucherModel);

        if (!isPostBack)
        {
            initvalues();
        }

	}


    private void initvalues()
    {
        objDS = objPartyRecieptVoucherModel.ReadValues();
        objIPartyRecieptVoucherView.SessionDropDownRefType = objDS.Tables[4];
        objIPartyRecieptVoucherView.Bind_BillGrid = objDS.Tables[0];
        objIPartyRecieptVoucherView.Bind_OtherGrid = objDS.Tables[1];
        objIPartyRecieptVoucherView.SessionBillByBillDT = objDS.Tables[2];
        objIPartyRecieptVoucherView.SessionCostCentreDT = objDS.Tables[3];
        objIPartyRecieptVoucherView.SessionDropDownCostCentre = objDS.Tables[5];


        if (objIPartyRecieptVoucherView.keyID > 0)
        { 
            DataRow dr = objDS.Tables[6].Rows[0];
            DataRow dr1 = objDS.Tables[7].Rows[0];

            objIPartyRecieptVoucherView.VoucherNo = dr["Voucher_No"].ToString();
            objIPartyRecieptVoucherView.VoucherDate = Convert.ToDateTime(dr["Voucher_Date"]);
            objIPartyRecieptVoucherView.SetCashBankLedger(dr["CashBankLedgerName"].ToString(), dr["CashBankLedger_Id"].ToString());
            objIPartyRecieptVoucherView.AmountRecieved = Convert.ToDecimal(dr["AmountReceived"]);
            objIPartyRecieptVoucherView.ChequeNo = Convert.ToInt32(dr["Cheque_No"]);
            objIPartyRecieptVoucherView.ChequeDate = Convert.ToDateTime(dr["Cheque_Date"]);
            objIPartyRecieptVoucherView.ChequeBankName = dr["Bank_Name"].ToString();
            objIPartyRecieptVoucherView.SetClientLedger(dr1["ClientLedgerName"].ToString(), dr1["ClientLedger_Id"].ToString());
            objIPartyRecieptVoucherView.Narration = dr["Narration"].ToString();
            objIPartyRecieptVoucherView.ManualRefNo = dr["Ref_No"].ToString();
            objIPartyRecieptVoucherView.BillLedgerName = "Bill Details for " + dr1["ClientLedgerName"].ToString() + " :";

            objIPartyRecieptVoucherView.TotalAdjustedAmount = Convert.IsDBNull(objIPartyRecieptVoucherView.Session_BillGrid.Compute("Sum(Adjustment_Amount)", "")) == true ? 0 : Convert.ToDecimal(objIPartyRecieptVoucherView.Session_BillGrid.Compute("Sum(Adjustment_Amount)", ""));
            string FilterString = "Ref_Type_Id <> 2";
            objIPartyRecieptVoucherView.TotalAdjustedAmount = objIPartyRecieptVoucherView.TotalAdjustedAmount + (Convert.IsDBNull(objIPartyRecieptVoucherView.Session_BillGrid.Compute("Sum(Bill_Amount)", FilterString)) == true ? 0 : Convert.ToDecimal(objIPartyRecieptVoucherView.Session_BillGrid.Compute("Sum(Bill_Amount)", FilterString)));

            objIPartyRecieptVoucherView.TotalDeduction = Convert.IsDBNull(objIPartyRecieptVoucherView.Session_OtherGrid.Compute("Sum(Amount)", "")) == true ? 0 : Convert.ToDecimal(objIPartyRecieptVoucherView.Session_OtherGrid.Compute("Sum(Amount)", ""));
        }
    }

    public void Save()
    {
        base.DBSave();
    }

    public DataSet SetCreditDaysAmount()
    {
        return objPartyRecieptVoucherModel.SetCreditDaysAmount();
    }

    public DataSet GetLedgerParam()
    {
        return objPartyRecieptVoucherModel.GetLedgerParam();
    }

    public bool IsDupicateRefNo()
    {
        return objPartyRecieptVoucherModel.IsDuplicateRef_No();
    }
}
