using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.MasterView;

/// <summary>
/// Summary description for RegionModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class RegionModel : IModel
    {
        private IRegionView objIRegionView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = 1;
        //private int _userID = UserManager.getUserParam().UserId;

        public RegionModel(IRegionView regionView)
        {
            objIRegionView = regionView;
        }
        public DataSet GetCountryValues()
        {

            objDAL.RunProc("[dbo].[EC_Mst_Region_FillValues]", ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@RegionId", SqlDbType.Int, 0, objIRegionView.keyID)
                                         };
            objDAL.RunProc("EC_Mst_Region_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }
        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.[EC_Master_Region_Area_FillValues]", ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@KeyID",SqlDbType.Int,0, objIRegionView.keyID),
                                               objDAL.MakeInParams("@RegionCode", SqlDbType.VarChar, 50,objIRegionView.RegionCode), 
                                               objDAL.MakeInParams("@RegionName", SqlDbType.VarChar,50, objIRegionView.RegionName),
                                               objDAL.MakeInParams("@CountryId", SqlDbType.Int, 0, objIRegionView.CountryId),
                                               objDAL.MakeInParams("@UserId", SqlDbType.Int,0,  _userID),
                                               objDAL.MakeInParams("@CashLedgerId",SqlDbType.Int,0,objIRegionView.CashLedgerId),
                                               objDAL.MakeInParams("@BankLedgerId",SqlDbType.Int,0,objIRegionView.BankLedgerId)
                                              
                                         };


            objDAL.RunProc("EC_Mst_Region_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
    }
}