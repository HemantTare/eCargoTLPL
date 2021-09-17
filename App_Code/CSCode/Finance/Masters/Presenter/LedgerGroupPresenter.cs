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
    public class LedgerGroupPresenter : Presenter
    {
    
        private ILedgerGroupView objILedgerGroupView;
        private LedgerGroupModel objLedgerGroupModel;
        private DataSet objDS;
         public LedgerGroupPresenter(ILedgerGroupView LedgerGroupView, bool IsPostBack)
        {
            objILedgerGroupView = LedgerGroupView;
            objLedgerGroupModel = new LedgerGroupModel(objILedgerGroupView);
            base.Init(objILedgerGroupView, objLedgerGroupModel);

            if (!IsPostBack)
            {
                
                initValues();
            }
        }

        private void initValues()
        {
            fillValues();

            if (objILedgerGroupView.keyID > 0)
            {
               readValues();
            }
            
        }

        private void fillValues()
        {
            objDS = objLedgerGroupModel.FillValues();
            objILedgerGroupView.bind_ddl_Under = objDS.Tables[0];
        }

        private void readValues()
        {
            objDS = objLedgerGroupModel.ReadValues();

            DataRow Dr = objDS.Tables[0].Rows[0];

            objILedgerGroupView.LedgerGroupName = Dr["Ledger_Group_Name"].ToString();
            objILedgerGroupView.Alias = Dr["Alias"].ToString();
            objILedgerGroupView.IndexNo =Util.String2Int(Dr["Index_No"].ToString());
            objILedgerGroupView.NatureName = Dr["Nature"].ToString();
            objILedgerGroupView.UnderId =Util.String2Int(Dr["Parent_Ledger_Group_Id"].ToString());
            objILedgerGroupView.IsAffectGrossProfit =Util.String2Bool(Dr["Affect_Gross_Profit"].ToString());
            objILedgerGroupView.EnableControls = (Util.String2Bool(Dr["Reserved"].ToString()) == true ? false : true);
        }


        public void Save()
        {
            base.DBSave();
            //objLedgerGroupModel.Save();
        }

        
    }
}
