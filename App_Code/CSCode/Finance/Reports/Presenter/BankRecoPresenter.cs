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
using Raj.FA.ReportsView;
using Raj.FA.ReportsModel;


/// <summary>
/// Summary description for BankRecoPresenter
/// </summary>
/// 

namespace Raj.FA.ReportsPresenter
{
    public class BankRecoPresenter : Presenter
    {
        private IBankRecoView _objIBankRecoView;
        private BankRecoModel _objBankRecoModel;
        private DataSet Ds;
      
        //private Boolean Is_VT = false;

        public BankRecoPresenter(IBankRecoView objIBankRecoView, bool isPostBack)
        {
            _objIBankRecoView = objIBankRecoView;
            _objBankRecoModel = new BankRecoModel(_objIBankRecoView);
            base.Init(_objIBankRecoView, _objBankRecoModel);
            if (!isPostBack)
            {
                initControl();
            }
        }

        private void initControl()
        {
            if (_objIBankRecoView.keyID != -1)
            {

              
                 Ds = _objBankRecoModel.ReadValues();
               
                makeTable();
                _objIBankRecoView.SetVariables = Ds.Tables[1];
                _objIBankRecoView.bind_dg_BankReco = Ds.Tables[0];

            }

        }
        public void Save()
        {
            //base.DBSave();
            Message _objMessage = new Message();
           
             _objMessage = _objBankRecoModel.Save();
           

            if (_objMessage.messageID == 0)
            { _objIBankRecoView.errorMessage = "Saved Successfully"; }
            else { _objIBankRecoView.errorMessage = _objMessage.message; }

        }


        private void makeTable()
        {
            //Ds.Tables[0].Columns.Add("Is_Select", Type.GetType("System.Boolean"));
            //Ds.Tables[0].Columns.Add("Bank_Date", Type.GetType("System.DateTime"));
            Ds.Tables[0].Columns.Add("Sr_No", Type.GetType("System.Int64"));
            DataTable Dt_Temp = Ds.Tables[0];
            for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
            {
                //Dt_Temp.Rows[i]["Is_Select"] = false;
                //Dt_Temp.Rows[i]["Bank_Date"] = DateTime.Now;
                Dt_Temp.Rows[i]["Sr_No"] = i + 1;
            }
        }

    }

}