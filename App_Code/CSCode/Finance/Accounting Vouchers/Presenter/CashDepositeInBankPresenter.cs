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
    public class CashDepositeInBankPresenter : Presenter
    {

        private ICashDepositeInBankView objICashDepositeInBankView;
        private CashDepositeInBankModel objCashDepositeInBankModel;
        private DataSet objDS;
        public CashDepositeInBankPresenter(ICashDepositeInBankView CashDepositeInBankView, bool IsPostBack)
        {
            objICashDepositeInBankView = CashDepositeInBankView;
            objCashDepositeInBankModel = new CashDepositeInBankModel(objICashDepositeInBankView);
            base.Init(objICashDepositeInBankView, objCashDepositeInBankModel);

            if (!IsPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            fillValues();

            //if (objICashExpensesView.keyID > 0)
            //{
               readValues();
               Get_BranchRateParameter();
            //}
            
        }

        private void fillValues()
        {
            //objDS = objCashExpensesModel.FillValues();
            //objICashExpensesView.bind_ddl_Under = objDS.Tables[0];
        }

        private void readValues()
        {
            //objDS = objCashExpensesModel.ReadValues();
            //DataSet objDS_FromHelper = new DataSet();

            //if (System.Web.HttpContext.Current.Session["FromTDSHelper"] != null && Convert.ToBoolean(System.Web.HttpContext.Current.Session["FromTDSHelper"]))
            //{
            //    objDS_FromHelper = (DataSet)System.Web.HttpContext.Current.Session["TDS_FBT_CashExpensess"];
            //    objDS_FromHelper.Tables[0].TableName = "VoucherDetails";
            //    objDS_FromHelper.Tables[1].TableName = "VoucherBillByBill";
   
            //    Common.SetPrimaryKeys(new string[] { "Ledger_Id" }, objDS_FromHelper.Tables["CashExpensesDetails"]);
             
            //}
            //else if (System.Web.HttpContext.Current.Session["FromFBTHelper"] != null && Convert.ToBoolean(System.Web.HttpContext.Current.Session["FromFBTHelper"]))
            //{
            //    objDS_FromHelper = (DataSet)System.Web.HttpContext.Current.Session["TDS_FBT_CashExpensess"];
            //    objDS_FromHelper.Tables[0].TableName = "CashExpensesDetails";
                 
            //    Common.SetPrimaryKeys(new string[] { "Ledger_Id" }, objDS_FromHelper.Tables["CashExpensesDetails"]);
               
            //}
            //else
            //{
            //}
            
           
            //System.Web.HttpContext.Current.Session["FromTDSHelper"] = null;
            //System.Web.HttpContext.Current.Session["FromFBTHelper"] = null;

            //if (objICashExpensesView.keyID > 0)
            //{
            //    DataRow Dr = objDS.Tables["CashExpenses"].Rows[0]; 
            //    objICashExpensesView.Details = Dr["Details"].ToString();

            //    objICashExpensesView.RefNo = Dr["Ref_No"].ToString();  

            //}

        }


        public void Get_BranchRateParameter()
        {
            objDS = objCashDepositeInBankModel.Get_BranchRateParameter();

            if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];

                    objICashDepositeInBankView.Default_Bank_Ledger_Id= Util.String2Int(objDR["Default_Bank_Ledger_Id"].ToString());
                    objICashDepositeInBankView.Default_Bank_Ledger_Name = objDR["Default_Bank_Ledger_Name"].ToString();

                }
        }

        public void Save()
        {
           base.DBSave();
          
        }


  
    }
}
