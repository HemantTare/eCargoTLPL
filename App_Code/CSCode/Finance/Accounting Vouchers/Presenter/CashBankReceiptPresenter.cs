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
namespace Raj.EC.FinancePresenter
{
    public class CashBankReceiptPresenter : Presenter
    {
    
        private ICashBankReceiptView objICashBankReceiptView;
        private CashBankReceiptModel objCashBankReceiptModel;
        private DataSet objDS;
        public CashBankReceiptPresenter(ICashBankReceiptView CashBankReceiptView, bool IsPostBack)
        {
            objICashBankReceiptView = CashBankReceiptView;
            objCashBankReceiptModel = new CashBankReceiptModel(objICashBankReceiptView);
            base.Init(objICashBankReceiptView, objCashBankReceiptModel);

            if (!IsPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            objICashBankReceiptView.ChequeDate = DateTime.Now.Date;
            fillValues(); 
            readValues();             
        }

        private void fillValues()
        {   
        }

        private void readValues()
        {             
        }
        
        public void Save()
        {
           base.DBSave();
          
        }


  
    }
}
