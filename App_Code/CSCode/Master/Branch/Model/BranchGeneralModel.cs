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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.MasterView;
/// <summary>
/// Summary description for BranchGeneralModel
/// </summary>
namespace Raj.EC.MasterModel
{
    public class BranchGeneralModel : IModel
    {
        private IBranchGeneralView objIBranchGeneralView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private string Hierarchy_Code = UserManager.getUserParam().HierarchyCode;

        public BranchGeneralModel(IBranchGeneralView BranchGeneralView)
        {
            objIBranchGeneralView = BranchGeneralView;
        }


        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Master_Branch_General_FillValues", ref objDS);
            return objDS;
        }

        public DataSet FillDivisionOnAreaSelection()
        {
            SqlParameter[] objSqlParam ={
                //objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, objIBranchGeneralView.keyID) ,
                objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, objIBranchGeneralView.Branch_ID) ,
                objDAL.MakeInParams("@Area_ID", SqlDbType.Int, 0, objIBranchGeneralView.AreaID) ,
                objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, Hierarchy_Code)};
            objDAL.RunProc("EC_Master_Branch_Fill_Division_On_AreaSelection", objSqlParam, ref objDS);
            return objDS;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, objIBranchGeneralView.keyID),
            objDAL.MakeInParams("@Flag", SqlDbType.Int, 0, 1)};
            objDAL.RunProc("dbo.EC_Master_Branch_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }
        
        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }
    }
}
