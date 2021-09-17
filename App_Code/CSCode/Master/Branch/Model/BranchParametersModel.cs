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
/// Summary description for BranchParametersModel
/// </summary>
namespace Raj.EC.MasterModel
{
    public class BranchParametersModel : IModel
    {
        private IBranchParametersView objIBranchParametersView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        public BranchParametersModel(IBranchParametersView BranchParametersView)
        {
            objIBranchParametersView = BranchParametersView;
        }


        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Master_Branch_Parameters_FillValues", ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, objIBranchParametersView.keyID),
            objDAL.MakeInParams("@Flag", SqlDbType.Int, 0, 3)};
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

