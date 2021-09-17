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
/// Summary description for LedgerMonthlyPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class LedgerMonthlyPresenter : Presenter
    {
        private ILedgerMonthlyView objILedgerMonthlyView;
        private LedgerMonthlyModel objLedgerMonthlyModel;
        private DataSet objDS;

        public LedgerMonthlyPresenter(ILedgerMonthlyView ledgerMonthlyView, bool IsPostBack)
        {
            objILedgerMonthlyView = ledgerMonthlyView;
            objLedgerMonthlyModel = new LedgerMonthlyModel(objILedgerMonthlyView);

            base.Init(objILedgerMonthlyView, objLedgerMonthlyModel);
            if (!IsPostBack)
            {
                initValues();
            }

        }

        public void initValues()
        {
            objDS = objLedgerMonthlyModel.ReadValues();


            if (objDS.Tables["Table1"].Rows.Count > 0)
            {

                objILedgerMonthlyView.Total_Credit = Util.String2Decimal(objDS.Tables["Table1"].Rows[0]["Total_Credit"].ToString());

                objILedgerMonthlyView.Total_Debit = Util.String2Decimal(objDS.Tables["Table1"].Rows[0]["Total_Debit"].ToString());
            }

            if (objDS.Tables["Table"].Rows.Count > 0)
            {

                int i = objDS.Tables[0].Rows.Count - 1;
                objILedgerMonthlyView.Closing_Balance = objDS.Tables["Table"].Rows[i]["Closing_Balance"].ToString();
            }
            objILedgerMonthlyView.BindLedgerMonthlyGrid = objDS;  
        }


    }
}

