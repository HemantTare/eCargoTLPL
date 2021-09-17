using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;


/// <summary>
/// Summary description for LedgerVoucherPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class LedgerVoucherPresenter : Presenter
    {
        private ILedgerVoucherView objILedgerVoucherView;
        private LedgerVoucherModel objLedgerVoucherModel;
        private DataSet objDS;

        public LedgerVoucherPresenter(ILedgerVoucherView ledgerVoucherView, bool IsPostBack)
        {
            objILedgerVoucherView = ledgerVoucherView;
            objLedgerVoucherModel = new LedgerVoucherModel(objILedgerVoucherView);

            base.Init(objILedgerVoucherView, objLedgerVoucherModel);
            if (!IsPostBack)
            {
              
                initValues();
            }

        }

        public void initValues()
        {
            objDS = objLedgerVoucherModel.ReadValues();
            objILedgerVoucherView.SessionLedgerVoucher = objDS;
            objILedgerVoucherView.BindLedgerVoucherGrid = objDS;

            HttpContext.Current.Session["LV_DS"] = objILedgerVoucherView.SessionLedgerVoucher;
          
           

          //  _Ledger_Voucher_ReportsView.Is_Bank_Ledger = Convert.ToBoolean(ds.Tables["Table1"].Rows[0]["Is_Bank_Ledger"].ToString());
            objILedgerVoucherView.Current_Total_Credit = Util.String2Decimal(objDS.Tables["Table1"].Rows[0]["Total_Credit"].ToString());
            objILedgerVoucherView.Current_Total_Debit = Util.String2Decimal(objDS.Tables["Table1"].Rows[0]["Total_Debit"].ToString());


            if (Util.String2Decimal(objDS.Tables["Table1"].Rows[0]["Opening_Balance"].ToString()) > 0)
            {
                objILedgerVoucherView.Opening_Balance_Credit = Math.Abs(Convert.ToDecimal(objDS.Tables["Table1"].Rows[0]["Opening_Balance"])).ToString();
                //objILedgerVoucherView.Closing_Balance_Credit = Math.Abs(Convert.ToDecimal(objDS.Tables["Table1"].Rows[0]["Closing_Balance"])).ToString();
            }
            else
            {

                objILedgerVoucherView.Opening_Balance_Debit = Math.Abs(Convert.ToDecimal(objDS.Tables["Table1"].Rows[0]["Opening_Balance"])).ToString();
                //objILedgerVoucherView.Closing_Balance_Debit = Math.Abs(Convert.ToDecimal(objDS.Tables["Table1"].Rows[0]["Closing_Balance"])).ToString();

            }

            if (Util.String2Decimal(objDS.Tables["Table1"].Rows[0]["Closing_Balance"].ToString()) > 0)
            {
               objILedgerVoucherView.Closing_Balance_Credit = Math.Abs(Convert.ToDecimal(objDS.Tables["Table1"].Rows[0]["Closing_Balance"])).ToString();
            }
            else
            {

                objILedgerVoucherView.Closing_Balance_Debit = Math.Abs(Convert.ToDecimal(objDS.Tables["Table1"].Rows[0]["Closing_Balance"])).ToString();

            }

        }
	}
}
