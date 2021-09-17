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
    class LedgerGeneralModel : IModel
    {
        private ILedgerGeneralView objILedgerGeneralView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        //private int _userID = (int)Param.getUserParam().UserID;
        //private int _yearCode = (int)Param.getUserParam().YearCode;
        //private string _hierarchyCode = (string)Param.getUserParam().HierarchyCode;
        //private int _mainId = (int)Param.getUserParam().MainId;

        private int _userID =1;
        private int _yearCode = 8;
        private string _hierarchyCode = "HO";
        private int _mainId = 1;
        
        
        public LedgerGeneralModel(ILedgerGeneralView LedgerGeneralView)
        {
            objILedgerGeneralView = LedgerGeneralView;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("[EC_FA_Mst_Ledger_FillValues]", ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeInParams("@Key_Id", SqlDbType.Int,0,objILedgerGeneralView.keyID)
                                         };

            objDAL.RunProc("[EC_FA_Mst_Ledger_ReadValues]", objSqlParam,ref objDS); 
            return objDS;
        }


        public Message Save()
        {
            Message objMessage = new Message();

            //SqlParameter[] objSqlParam = {  objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
            //                                objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000)
            //                                //objDAL.MakeInParams("@Key_Id", SqlDbType.Int, 0, objILedgerView.keyID),

            //                                //objDAL.MakeInParams("@Ledger_Group_Name", SqlDbType.VarChar, 100, objILedgerView.LedgerName),
            //                                //objDAL.MakeInParams("@Alias", SqlDbType.VarChar, 100,objILedgerView.Alias),
            //                                //objDAL.MakeInParams("@Parent_Ledger_Group_Id", SqlDbType.Int, 0, objILedgerView.UnderId),
            //                                //objDAL.MakeInParams("@Nature", SqlDbType.VarChar, 15, objILedgerView.UnderId==1?objILedgerView.NatureName:"0"),
            //                                //objDAL.MakeInParams("@Index_No", SqlDbType.Int, 0,objILedgerView.IndexNo>0?objILedgerView.IndexNo:10000),
            //                                //objDAL.MakeInParams("@Affect_Gross_Profit", SqlDbType.Bit, 1,objILedgerView.UnderId==1?objILedgerView.IsAffectGrossProfit:false),
                                            
            //                              };

            //objDAL.RunProc("[EC_FA_Mst_Ledger_Save]", objSqlParam); 

            //objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            //objMessage.message = Convert.ToString(objSqlParam[1].Value);

            //objILedgerView.errorMessage = objMessage.message;
            //string _Msg="";
            //if (objMessage.messageID == 2627)
            //{
            //    objILedgerView.errorMessage = "Duplicate Ledger Group Name";
            //}
            //else if (objMessage.messageID == 0)
            //{
            //    _Msg = "Saved SuccessFully";
            //    objILedgerView.errorMessage = _Msg;
            //    //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&RefreshParentPage=1");
            //}

            return objMessage;
        }

        public DataSet FillLocation()
        {
            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeInParams("@SelectedHierarchy", SqlDbType.VarChar,10,objILedgerGeneralView.SelectedHierarchy)
                                         };

            objDAL.RunProc("[EC_FA_Mst_Ledger_FillLocation]", objSqlParam, ref objDS);
            return objDS;
        }
    }
}
