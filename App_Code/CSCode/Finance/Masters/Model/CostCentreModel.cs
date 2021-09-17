using System;
using System.Data;
using System.Data.SqlClient;

using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.FinanceView ;

/// Author        : Ankit Champaneriya 
/// Created On    : 15/10/2008
/// Description   : This Page is For  Cost centre model details
/// 
/// <summary>
/// Summary description for CostCentreModel
/// </summary>
/// 
namespace Raj.EC.FinanceModel
{
    public class CostCentreModel : IModel
    {
        private ICostCentreView _iCostCentreView;
        private DAL _objDAL = new DAL();
        private DataSet objDS;

        private string  _hierarchy_Code = UserManager.getUserParam().HierarchyCode;
        private int _main_id = UserManager.getUserParam().MainId;

        public CostCentreModel(ICostCentreView iCostCentreView)
        {
            _iCostCentreView = iCostCentreView;
        }

        public DataSet GetUnder()
        {
            SqlParameter[] objSqlParam ={ 
                _objDAL.MakeInParams("@CostCentreId",SqlDbType.Int,0,_iCostCentreView.keyID )
            };
            _objDAL.RunProc("[dbo].[EC_FA_Mst_Cost_Centre_Fill]", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet GetLedgers()
        {
            _objDAL.RunProc("[dbo].[EC_FA_Mst_Ledgers_Fill]", ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { 
                                          _objDAL.MakeInParams("@Key_Id", SqlDbType.Int,0,_iCostCentreView.keyID)
                                         };

            _objDAL.RunProc("[EC_FA_Mst_CostCentre_ReadValues]", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            string xml = _iCostCentreView.xmlLedgerId;

            SqlParameter[] objSqlParam = {  _objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
                                            _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                            _objDAL.MakeInParams("@Key_Id", SqlDbType.Int, 0, _iCostCentreView.keyID),
                                            _objDAL.MakeInParams("@XML", SqlDbType.Xml , 0, _iCostCentreView.xmlLedgerId ),
                                            _objDAL.MakeInParams("@Cost_Centre_Name", SqlDbType.VarChar, 100, _iCostCentreView.Cost_Centre_Name),
                                            _objDAL.MakeInParams("@Parent_Cost_Centre_ID", SqlDbType.Int, 0, _iCostCentreView.Cost_Centre_ID ),
                                            _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar , 0, _hierarchy_Code),
                                            _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0, _main_id ),
                                          };

            _objDAL.RunProc("[EC_FA_Mst_CostCentre_Save]", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";
                //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
                                
            return objMessage;
        }
    }
}