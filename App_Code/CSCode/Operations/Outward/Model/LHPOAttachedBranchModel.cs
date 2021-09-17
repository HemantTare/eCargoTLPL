using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.OperationView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for LHPOAttachedBranchModel
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{
    public class LHPOAttachedBranchModel : IModel
    {
        private ILHPOAttachedBranchView objILHPOAttachedBranchView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;
        private int _keyID;
        int LHPOTypeID = 0;
        int LHPOID = 0;

        public LHPOAttachedBranchModel(ILHPOAttachedBranchView lHPOAttachedBranchView)
        {
            objILHPOAttachedBranchView = lHPOAttachedBranchView;
            GetKeyID();
        }
        public DataSet ReadValues()
        {
            return objDS;
        }
        private void GetKeyID()
        {


            if (System.Web.HttpContext.Current.Session["SessionLHPOTypeID"] != null && System.Web.HttpContext.Current.Session["SessionLHPOID"] != null)
            {
                LHPOTypeID = (int)System.Web.HttpContext.Current.Session["SessionLHPOTypeID"];
                LHPOID = (int)System.Web.HttpContext.Current.Session["SessionLHPOID"];
            }

            _keyID = objILHPOAttachedBranchView.keyID;

            if (LHPOTypeID == 2 && objILHPOAttachedBranchView.keyID <= 0)
            {
                _keyID = LHPOID;

            }
        }
        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }
        public DataSet FillGrid()
        {
            GetKeyID();
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@LHPO_ID", SqlDbType.Int, 0, _keyID)
                                           };
            objDAL.RunProc("EC_Opr_LHPOAttachedBranches_FillGrid", objSqlParam, ref objDS);
            Raj.EC.Common.SetTableName(new string[] { "AttachedBranchGrid" }, objDS);
            Raj.EC.Common.SetPrimaryKeys(new string[] { "BranchID" }, objDS.Tables["AttachedBranchGrid"]);



            return objDS;

        }
    }
}