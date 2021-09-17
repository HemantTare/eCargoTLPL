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
    public class LedgerGeneralPresenter : Presenter
    {
    
        private ILedgerGeneralView objILedgerGeneralView;
        private LedgerGeneralModel objLedgerGeneralModel;
        private DataSet objDS;
         public LedgerGeneralPresenter(ILedgerGeneralView LedgerGeneralView, bool IsPostBack)
        {
            objILedgerGeneralView = LedgerGeneralView;
            objLedgerGeneralModel = new LedgerGeneralModel(objILedgerGeneralView);
            base.Init(objILedgerGeneralView, objLedgerGeneralModel);

            if (!IsPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            fillValues();

            if (objILedgerGeneralView.keyID > 0)
            {
              //readValues();
            }
            
        }

        private void fillValues()
        {
            objDS = objLedgerGeneralModel.FillValues();
            objILedgerGeneralView.bind_ddl_Under = objDS.Tables[0];
            objILedgerGeneralView.bind_ddl_ServiceTaxCategory = objDS.Tables[1];
            objILedgerGeneralView.bind_ddl_Deductee_Type = objDS.Tables[2];
            objILedgerGeneralView.bind_ddl_NatureOfPayment = objDS.Tables[3];
            objILedgerGeneralView.bind_ddl_FBTCategory = objDS.Tables[4];
        }

        //private void readValues()
        //{
        //    objDS = objLedgerGeneralModel.ReadValues();

        //    DataRow Dr = objDS.Tables[0].Rows[0];

        //    //objILedgerGeneralView.LedgerGeneralName = Dr["LedgerGeneral_Group_Name"].ToString();
        //    //objILedgerGeneralView.Alias = Dr["Alias"].ToString();
        //    //objILedgerGeneralView.IndexNo =Util.String2Int(Dr["Index_No"].ToString());
        //    //objILedgerGeneralView.NatureName = Dr["Nature"].ToString();
        //    //objILedgerGeneralView.UnderId =Util.String2Int(Dr["Parent_LedgerGeneral_Group_Id"].ToString());
        //    //objILedgerGeneralView.IsAffectGrossProfit =Util.String2Bool(Dr["Affect_Gross_Profit"].ToString());
        //}


        public void Save()
        {
            //base.DBSave();
            //objLedgerGeneralModel.Save();
        }



        //public void FillLocation()
        //{
        //    objDS = objLedgerGeneralModel.FillLocation();
        //    objILedgerGeneralView.bind_ddl_Location = objDS.Tables[0];
        //}
    }
}
