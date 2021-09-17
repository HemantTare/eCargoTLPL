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
    class LedgerGroupModel : IModel
    {
        private ILedgerGroupView objILedgerGroupView;
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
        
        
        public LedgerGroupModel(ILedgerGroupView LedgerGroupView)
        {
            objILedgerGroupView = LedgerGroupView;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("[EC_FA_Mst_LedgerGroup_FillValues]", ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeInParams("@Key_Id", SqlDbType.Int,0,objILedgerGroupView.keyID)
                                         };

            objDAL.RunProc("[EC_FA_Mst_LedgerGroup_ReadValues]", objSqlParam,ref objDS); 
            return objDS;
        }


        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {  objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
                                            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                            objDAL.MakeInParams("@Key_Id", SqlDbType.Int, 0, objILedgerGroupView.keyID),

                                            objDAL.MakeInParams("@Ledger_Group_Name", SqlDbType.VarChar, 100, objILedgerGroupView.LedgerGroupName),
                                            objDAL.MakeInParams("@Alias", SqlDbType.VarChar, 100,objILedgerGroupView.Alias),
                                            objDAL.MakeInParams("@Parent_Ledger_Group_Id", SqlDbType.Int, 0, objILedgerGroupView.UnderId),
                                            objDAL.MakeInParams("@Nature", SqlDbType.VarChar, 15, objILedgerGroupView.UnderId==1?objILedgerGroupView.NatureName:"0"),
                                            objDAL.MakeInParams("@Index_No", SqlDbType.Int, 0,objILedgerGroupView.IndexNo>0?objILedgerGroupView.IndexNo:10000),
                                            objDAL.MakeInParams("@Affect_Gross_Profit", SqlDbType.Bit, 1,objILedgerGroupView.UnderId==1?objILedgerGroupView.IsAffectGrossProfit:false),
                                            
                                          };

            objDAL.RunProc("[EC_FA_Mst_LedgerGroup_Save]", objSqlParam); 

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            objILedgerGroupView.errorMessage = objMessage.message;
            string _Msg="";
            if (objMessage.messageID == 2627)
            {
                objILedgerGroupView.errorMessage = "Duplicate Ledger Group Name";
            }
            else if (objMessage.messageID == 0)
            {
                _Msg = "Saved SuccessFully";
                objILedgerGroupView.errorMessage = _Msg;
                //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&RefreshParentPage=1");
            }

            return objMessage;
        }
    }
}
