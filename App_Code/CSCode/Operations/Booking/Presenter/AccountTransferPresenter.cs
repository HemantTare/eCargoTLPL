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
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

/// <summary>
/// Summary description for AccountTransferPresenter
/// </summary>
namespace Raj.EC.OperationPresenter
{
    public class AccountTransferPresenter : Presenter
    {
        private IAccountTransferView objIAccountTransferView;
        private AccountTransferModel objAccountTransferModel;
        private DataSet objDS;

        public AccountTransferPresenter(IAccountTransferView AccountTransferView, bool isPostback)
        {
            objIAccountTransferView = AccountTransferView;
            objAccountTransferModel = new AccountTransferModel(objIAccountTransferView);
            base.Init(objIAccountTransferView, objAccountTransferModel);

            if (!isPostback)
            {
                objIAccountTransferView.AccountTransferDate = DateTime.Now.Date;
                initValues();
            }
        }

        private void fillValues()
        {
            objDS = objAccountTransferModel.FillValues();
            objIAccountTransferView.BindVA = objDS.Tables[0];
        }

        public void fillgrid()
        {
            objDS = objAccountTransferModel.ReadValues();
            objIAccountTransferView.SessionBindAccountTransferGrid = objDS.Tables[0];
        }

        private void initValues()
        {
            fillValues();

            objDS = objAccountTransferModel.ReadValues();
            objIAccountTransferView.SessionBindAccountTransferGrid = objDS.Tables[0];

            if (objIAccountTransferView.keyID > 0)
            {
                if (objDS.Tables[1].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];
                    objIAccountTransferView.VAId = Util.String2Int(objDR["VA_Id"].ToString());
                    objIAccountTransferView.AccountTransferDate = Convert.ToDateTime(objDR["Pickup_Sheet_Date"].ToString());
                    objIAccountTransferView.Remarks = objDR["Remarks"].ToString();
                    objIAccountTransferView.AccountTransferNo = objDR["Pickup_Sheet_No_For_Print"].ToString();
                    objIAccountTransferView.Total_Weight = Util.String2Decimal(objDR["Total_Actual_Weight"].ToString());
                    objIAccountTransferView.Total_Freight = Util.String2Decimal(objDR["Total_Basic_Freight"].ToString());
                    objIAccountTransferView.Total_ServiceTax = Util.String2Decimal(objDR["Total_Service_Tax"].ToString());
                    objIAccountTransferView.Total_GCAmount = Util.String2Decimal(objDR["Total_GC_Amount"].ToString());
                    objIAccountTransferView.Total_GC = Util.String2Int(objDR["Total_GC"].ToString());
                    objIAccountTransferView.Total_Article = Util.String2Int(objDR["Total_Articles"].ToString());
                }
            }
        }

        public void Save()
        {
            base.DBSave();
        }
    }
}
