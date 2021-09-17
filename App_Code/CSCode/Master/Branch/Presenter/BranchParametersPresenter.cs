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
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for BranchParametersPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class BranchParametersPresenter : Presenter
    {
        private IBranchParametersView objIBranchParametersView;
        private BranchParametersModel objBranchParametersModel;
        private DataSet objDS;

        public BranchParametersPresenter(IBranchParametersView BranchParametersView, bool isPostback)
        {
            objIBranchParametersView = BranchParametersView;
            objBranchParametersModel = new BranchParametersModel(objIBranchParametersView);
            base.Init(objIBranchParametersView, objBranchParametersModel);

            if (!isPostback)
            { 
                fillValues();
                initValues();
            }
        }

        private void fillValues()
        {
            objDS = objBranchParametersModel.FillValues();
            objIBranchParametersView.BindDefaultBankLedger = objDS.Tables[0];
            objIBranchParametersView.BindDefaultCashLedger = objDS.Tables[1];
       }

        private void initValues()
        {
            if (objIBranchParametersView.keyID > 0)
            {
                objDS = objBranchParametersModel.ReadValues();

                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    objIBranchParametersView.GCMinStock = Util.String2Int(objDR["GC_Min_Stock"].ToString());
                    objIBranchParametersView.CRMinStock = Util.String2Int(objDR["CR_Min_Stock"].ToString());
                    objIBranchParametersView.MemoMinStock = Util.String2Int(objDR["Memo_Min_Stock"].ToString());
                    objIBranchParametersView.LHPOMinStock = Util.String2Int(objDR["LHPO_Min_Stock"].ToString());
                    objIBranchParametersView.DefaultBankLedgerId = Util.String2Int(objDR["Bank_Ledger_Id"].ToString());
                    objIBranchParametersView.DefaultCashLedgerId = Util.String2Int(objDR["Cash_Ledger_Id"].ToString());
                    objIBranchParametersView.BookingStartTime = objDR["Bkg_Start_Time"].ToString();
                    objIBranchParametersView.BookingEndTime = objDR["Bkg_End_Time"].ToString(); 
                }
            }
        }
    }
}

