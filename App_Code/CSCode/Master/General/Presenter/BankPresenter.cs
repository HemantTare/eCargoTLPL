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
using Raj.EF.MasterView;
using Raj.EF.MasterModel;

/// <summary>
/// Summary description for BankPresenter
/// </summary>
/// 

namespace Raj.EF.MasterPresenter
{

    public class BankPresenter : Presenter 
    {
        private IBankView objIBankView;
        private BankModel objBankModel;
        private DataSet objDS;

        public BankPresenter(IBankView BankView, bool isPostback)
        {
            objIBankView = BankView;
            objBankModel = new BankModel(objIBankView);
            base.Init(objIBankView, objBankModel);

            if (!isPostback)
            {
                initValues();
            }
        }

        private void initValues()
        {
            if (objIBankView.keyID > 0)
            {
                objDS = objBankModel.ReadValues();

                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    objIBankView.BankName = objDR["Bank_Name"].ToString();
                    objIBankView.AccountNo = objDR["AccountNo"].ToString();
                    objIBankView.Comments = objDR["Comments"].ToString();
                }
            }
        }

        public void Save()
        {
            base.DBSave();
           // objBankModel.Save();
        }
    }
}