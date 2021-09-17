using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;

using ClassLibraryMVP.DataAccess;
using Raj.EC.FinanceView;

namespace Raj.EC.FinanceModel
{
    class RecoAndBillDetailsModel : IModel
    {
        private IRecoAndBillDetailsView objIRecoAndBillDetailsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = (int)UserManager.getUserParam().UserId;
        private int _yearCode = (int)UserManager.getUserParam().YearCode;
        private string _hierarchyCode = (string)UserManager.getUserParam().HierarchyCode;
        private int _mainId = (int)UserManager.getUserParam().MainId;
        private int _divisionId = (int)UserManager.getUserParam().DivisionId;


    //private int _userID =1;
        //private int _yearCode = 8;
        //private string _hierarchyCode = "HO";
        //private int _mainId = 1;
        
        
        public RecoAndBillDetailsModel(IRecoAndBillDetailsView RecoAndBillDetailsView)
        {
            objIRecoAndBillDetailsView = RecoAndBillDetailsView;
        }

        public DataSet FillValues()
        {

            objDAL.RunProc("[EC_FA_Mst_RecoAndBillDetails_FillValues]", ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { 
                                            objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,objIRecoAndBillDetailsView.keyID),
                                            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2,_hierarchyCode),
                                            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_mainId),
                                            objDAL.MakeInParams("@Division_Id", SqlDbType.Int,0,_divisionId),
                                         };

            objDAL.RunProc("[EC_FA_Mst_RecoAndBillDetails_ReadValues]", objSqlParam,ref objDS);
            return objDS;
        }


        public Message Save()
        {
            Message objMessage = new Message();

            string _detailsXML;
            
            if (objIRecoAndBillDetailsView.IsBankReco)
            { _detailsXML = objIRecoAndBillDetailsView.GetBankRecoXML; }
            else if (objIRecoAndBillDetailsView.IsBillWise)
            { _detailsXML = objIRecoAndBillDetailsView.GetBillWiseXML; }
            else { _detailsXML = "<NewDataSet></NewDataSet>"; }

            

            SqlParameter[] objSqlParam = {  objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
                                            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                            objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int, 0, objIRecoAndBillDetailsView.keyID),

                                            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2,_hierarchyCode),
                                            objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,_mainId),
                                            objDAL.MakeInParams("@Division_Id", SqlDbType.Int,0,_divisionId),
                                            objDAL.MakeInParams("@Cleared_Amount", SqlDbType.Decimal,0,objIRecoAndBillDetailsView.OpeningBalance==0? 0 :objIRecoAndBillDetailsView.ClearedAmount),
                                            objDAL.MakeInParams("@Opening_Balance", SqlDbType.Decimal,0,objIRecoAndBillDetailsView.OpeningBalance),
                                            objDAL.MakeInParams("@IsBankReco", SqlDbType.Bit, 0,objIRecoAndBillDetailsView.IsBankReco),
                                            objDAL.MakeInParams("@IsBillWise", SqlDbType.Bit,0,objIRecoAndBillDetailsView.IsBillWise),
                                            objDAL.MakeInParams("@Details_XML", SqlDbType.Xml,0,_detailsXML),
                                           };

            objDAL.RunProc("[EC_FA_Mst_LedgerRecoAndBillDetails_Save]", objSqlParam); 

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            objIRecoAndBillDetailsView.errorMessage = objMessage.message;
            string _Msg="";
            if (objMessage.messageID == 2627)
            {

            }
            else if (objMessage.messageID == 0)
            {
                _Msg = "Saved SuccessFully";
                objIRecoAndBillDetailsView.errorMessage = _Msg;
                //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&RefreshParentPage=1");
            }

            return objMessage;
        }
    }
}
