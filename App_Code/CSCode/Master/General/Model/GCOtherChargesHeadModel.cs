using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

using Raj.EC.MasterView  ;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;

/// <summary>
/// Created : Ankit champanriya
/// Date    : 18/12/08 
/// Summary description for GCOtherChargesHeadModel
/// </summary>
/// 

namespace Raj.EC.MasterModel
{
    public class GCOtherChargesHeadModel : IModel
    {
        private IGCOtherChargesHeadView objIGCOtherChargesHeadView;
        private DAL objDAL = new DAL();
        private DataSet objDS = new DataSet();

        public GCOtherChargesHeadModel(IGCOtherChargesHeadView GCOtherChargesHeadView)
        {
            objIGCOtherChargesHeadView = GCOtherChargesHeadView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@GC_Other_Charge_Head_ID", SqlDbType.Int, 0, objIGCOtherChargesHeadView.keyID) };

            objDAL.RunProc("EC_Mst_GC_Other_ChargesHead_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@GC_Other_Charge_Head_ID", SqlDbType.Int, 0,objIGCOtherChargesHeadView.keyID ),
            objDAL.MakeInParams("@GC_Other_Charge_Head", SqlDbType.VarChar, 100,objIGCOtherChargesHeadView.GC_Other_Charges_Head  )};

            objDAL.RunProc("EC_Mst_GC_Other_ChargesHead_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
    }
}