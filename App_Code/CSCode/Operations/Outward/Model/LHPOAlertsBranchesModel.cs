using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.OperationView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for LHPOAlertsBranchesModel
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{

    public class LHPOAlertsBranchesModel : IModel
    {
        private ILHPOAlertsBranchesView objILHPOAlertsBranchesView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;
        private int _keyID;
        int LHPOTypeID = 0;
        int LHPOID = 0;

        public LHPOAlertsBranchesModel(ILHPOAlertsBranchesView lHPOAlertsBranchesView)
        {
            objILHPOAlertsBranchesView = lHPOAlertsBranchesView;
            GetKeyID();
        }
        private void GetKeyID()
        {
            if (System.Web.HttpContext.Current.Session["SessionLHPOTypeID"] != null && System.Web.HttpContext.Current.Session["SessionLHPOID"] != null)
            {
                LHPOTypeID = (int)System.Web.HttpContext.Current.Session["SessionLHPOTypeID"];
                LHPOID = (int)System.Web.HttpContext.Current.Session["SessionLHPOID"];
            }
            _keyID = objILHPOAlertsBranchesView.keyID;

            if (LHPOTypeID == 2 && objILHPOAlertsBranchesView.keyID <= 0)
            {
                _keyID = LHPOID;
            }
        }
        public DataSet FillValues()
        {
            GetKeyID();
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@LHPO_ID", SqlDbType.Int, 0, _keyID)
                                           };
            objDAL.RunProc("EC_Opr_LHPOAlertsBranches_FillValues",objSqlParam, ref objDS);
            Raj.EC.Common.SetTableName(new string[] { "AlertBranchGrid", "ATHGrid"}, objDS);
            Raj.EC.Common.SetPrimaryKeys(new string[] { "BranchID" }, objDS.Tables["AlertBranchGrid"]);
            Raj.EC.Common.SetPrimaryKeys(new string[] { "ATH_Payable_Hierarchy_Code", "ATH_Payable_Main_ID" }, objDS.Tables["ATHGrid"]);
            return objDS;
        }
       
        public DataSet ReadValues()
        {
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();          
            return objMessage;
        }
    }
}