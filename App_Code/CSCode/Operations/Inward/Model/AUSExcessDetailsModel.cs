using System;
using System.Data;
using System.Data.SqlClient;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationView;

/// <summary>
/// Summary description for AUS Excess Details model
/// </summary>
/// 
namespace Raj.EC.OperationModel
{
    public class AUSExcessDetailsModel : IModel
    {
        private IAUSExcessDetailsView  objIAUSExcessDetailsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        public AUSExcessDetailsModel(IAUSExcessDetailsView AUSExcessDetailsView)
        {
            objIAUSExcessDetailsView = AUSExcessDetailsView;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Operation_Excess_FillValues", ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Actual_Unloading_Sheet_ID", SqlDbType.Int, 0, objIAUSExcessDetailsView.keyID) };
            objDAL.RunProc("dbo.EC_Operation_Excess_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }


        public DataSet FillItemOnCommodityChanged()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Commodity_Id", SqlDbType.Int, 0, objIAUSExcessDetailsView.CommodityId) };
            objDAL.RunProc("EC_Operation_Excess_FillItemOnCommodityChanged", objSqlParam, ref objDS);

            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }
    }
}
