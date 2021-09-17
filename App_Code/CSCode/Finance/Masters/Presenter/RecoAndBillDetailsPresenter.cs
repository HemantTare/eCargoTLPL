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
    public class RecoAndBillDetailsPresenter : Presenter
    {
    
        private IRecoAndBillDetailsView objIRecoAndBillDetailsView;
        private RecoAndBillDetailsModel objRecoAndBillDetailsModel;
        private DataSet objDS;
         public RecoAndBillDetailsPresenter(IRecoAndBillDetailsView RecoAndBillDetailsView, bool IsPostBack)
        {
            objIRecoAndBillDetailsView = RecoAndBillDetailsView;
            objRecoAndBillDetailsModel = new RecoAndBillDetailsModel(objIRecoAndBillDetailsView);
            base.Init(objIRecoAndBillDetailsView, objRecoAndBillDetailsModel);

            if (!IsPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            fillValues();

            if (objIRecoAndBillDetailsView.keyID > 0)
            {
               readValues();
            }
            
        }

        private void fillValues()
        {
            //objDS = objRecoAndBillDetailsModel.FillValues();
            //objIRecoAndBillDetailsView.bind_ddl_Under = objDS.Tables[0];
        }

        private void readValues()
        {
            objDS = objRecoAndBillDetailsModel.ReadValues();

             DataRow Dr = objDS.Tables[0].Rows[0];

             objIRecoAndBillDetailsView.VisibleBankReco = false;
             objIRecoAndBillDetailsView.VisibleBillWise = false;

             objIRecoAndBillDetailsView.IsBankReco = Convert.ToBoolean(Dr["IsBankReco"]);
             objIRecoAndBillDetailsView.IsBillWise = Convert.ToBoolean(Dr["IsBillByBill"]);
             

             if (objDS.Tables[1].Rows.Count != 0)
             {
                 Dr = objDS.Tables[1].Rows[0];
                 objIRecoAndBillDetailsView.OpeningBalance = Convert.ToDecimal(Dr["Opening_Balance"]);

                 if (objIRecoAndBillDetailsView.OpeningBalance == 0)
                 { objIRecoAndBillDetailsView.OpeningDrCr = objDS.Tables[0].Rows[0]["DefaultDrCr"].ToString().ToLower() == "cr" ? 1 : -1;}

                 objIRecoAndBillDetailsView.ClearedAmount = Convert.ToDecimal(Dr["Cleared_Amount"]);
             }

             if (objIRecoAndBillDetailsView.IsBankReco)
             {
                 objIRecoAndBillDetailsView.Bind_dg_BankReco = objDS.Tables[2];
                 objIRecoAndBillDetailsView.VisibleBankReco = true;
             }
             else if (objIRecoAndBillDetailsView.IsBillWise)
             {
                 Common.SetPrimaryKeys(new string[] { "Ref_No" }, objDS.Tables[2]);
                 objIRecoAndBillDetailsView.Bind_dg_BillWise = objDS.Tables[2];
                 objIRecoAndBillDetailsView.VisibleBillWise = true;
             }
        }


        public void Save()
        {
            base.DBSave();
           // objRecoAndBillDetailsModel.Save();
        }

        
    }
}
