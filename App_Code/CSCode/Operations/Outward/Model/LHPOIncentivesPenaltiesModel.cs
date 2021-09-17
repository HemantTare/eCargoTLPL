using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.OperationView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;


/// <summary>
/// Summary description for LHPOIncentivesPenaltiesModel
/// </summary>
/// 

namespace Raj.EC.OperationModel
{
    public class LHPOIncentivesPenaltiesModel : IModel
    {
        private ILHPOIncentivesPenaltiesView _iLHPOIncentivesPenaltiesView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _keyID;
        int LHPOTypeID = 0;
        int LHPOID = 0;
        public LHPOIncentivesPenaltiesModel(ILHPOIncentivesPenaltiesView iLHPOIncentivesPenaltiesView)
        {
            _iLHPOIncentivesPenaltiesView = iLHPOIncentivesPenaltiesView;
            GetKeyID();
        }

        public DataSet FillValues()
        {
            //objDAL.RunProc("dbo.EC_Opr_LHPOIncentive_Penalties_ReadValues", ref objDS);
            return objDS;
        }
        private void GetKeyID()
        {
           

            if (System.Web.HttpContext.Current.Session["SessionLHPOTypeID"] != null && System.Web.HttpContext.Current.Session["SessionLHPOID"] != null)
            {
                LHPOTypeID = (int)System.Web.HttpContext.Current.Session["SessionLHPOTypeID"];
                LHPOID = (int)System.Web.HttpContext.Current.Session["SessionLHPOID"];
            }
            _keyID = _iLHPOIncentivesPenaltiesView.keyID;
            if (LHPOTypeID == 2 && _iLHPOIncentivesPenaltiesView.keyID <= 0)
            {
                _keyID = LHPOID;

            }            
        }
        public DataSet ReadValues()
        {
            GetKeyID();
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@LHPO_ID", SqlDbType.Int, 0, _keyID)
                                           };
            objDAL.RunProc("EC_Opr_LHPOIncentive_Penalties_ReadValues", objSqlParam, ref objDS);            
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }


    }
}