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
/// Summary description for BranchDeptServiceModel
/// </summary>
namespace Raj.EC.MasterModel
{
    public class BranchDeptServiceModel : IModel
    {
        private IBranchDeptServiceView objIBranchDeptServiceView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private string Hierarchy_Code = UserManager.getUserParam().HierarchyCode;

        public BranchDeptServiceModel(IBranchDeptServiceView BranchDeptServiceView)
        {
            objIBranchDeptServiceView = BranchDeptServiceView;
        }


        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam ={ 
                objDAL.MakeInParams("@Main_ID", SqlDbType.Int, 0, objIBranchDeptServiceView.keyID) ,
                objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, Hierarchy_Code)};

            objDAL.RunProc("dbo.EC_Master_Branch_Dept_Services_FillValues",objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, objIBranchDeptServiceView.keyID),
            objDAL.MakeInParams("@Flag", SqlDbType.Int, 0, 2)};
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
