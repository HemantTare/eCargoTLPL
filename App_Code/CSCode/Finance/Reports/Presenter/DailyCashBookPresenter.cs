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
/// Summary description for DailyCashBookPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class DailyCashBookPresenter : Presenter
    {
        private IDailyCashBookView objIDailyCashBookView;
        private DailyCashBookModel objDailyCashBookModel;
        private DataSet objDS;

        public DailyCashBookPresenter(IDailyCashBookView DailyCashBookView, bool IsPostBack)
        {
            objIDailyCashBookView = DailyCashBookView;
            objDailyCashBookModel = new DailyCashBookModel(objIDailyCashBookView);

            base.Init(objIDailyCashBookView, objDailyCashBookModel);
            if (!IsPostBack)
            {
              
                initValues();
            }

        }

        public void initValues()
        {
            objDS = objDailyCashBookModel.ReadValues();
            objIDailyCashBookView.SessionDailyCashBook = objDS;
            objIDailyCashBookView.BindDailyCashBookGrid = objDS;
           
          
           

          //  _Ledger_Voucher_ReportsView.Is_Bank_Ledger = Convert.ToBoolean(ds.Tables["Table1"].Rows[0]["Is_Bank_Ledger"].ToString());
            objIDailyCashBookView.Current_Total_Credit = Util.String2Decimal(objDS.Tables["Table1"].Rows[0]["Total_Credit"].ToString());
            objIDailyCashBookView.Current_Total_Debit = Util.String2Decimal(objDS.Tables["Table1"].Rows[0]["Total_Debit"].ToString());


            if (Util.String2Decimal(objDS.Tables["Table1"].Rows[0]["Opening_Balance"].ToString()) > 0)
            {
                objIDailyCashBookView.Opening_Balance_Credit = Math.Abs(Convert.ToDecimal(objDS.Tables["Table1"].Rows[0]["Opening_Balance"])).ToString();
                //objIDailyCashBookView.Closing_Balance_Credit = Math.Abs(Convert.ToDecimal(objDS.Tables["Table1"].Rows[0]["Closing_Balance"])).ToString();
            }
            else
            {

                objIDailyCashBookView.Opening_Balance_Debit = Math.Abs(Convert.ToDecimal(objDS.Tables["Table1"].Rows[0]["Opening_Balance"])).ToString();
                //objIDailyCashBookView.Closing_Balance_Debit = Math.Abs(Convert.ToDecimal(objDS.Tables["Table1"].Rows[0]["Closing_Balance"])).ToString();

            }

            if (Util.String2Decimal(objDS.Tables["Table1"].Rows[0]["Closing_Balance"].ToString()) > 0)
            {
               objIDailyCashBookView.Closing_Balance_Credit = Math.Abs(Convert.ToDecimal(objDS.Tables["Table1"].Rows[0]["Closing_Balance"])).ToString();
            }
            else
            {

                objIDailyCashBookView.Closing_Balance_Debit = Math.Abs(Convert.ToDecimal(objDS.Tables["Table1"].Rows[0]["Closing_Balance"])).ToString();

            }

        }
	}
}
