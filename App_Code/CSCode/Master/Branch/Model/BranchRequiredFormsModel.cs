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
/// Summary description for BranchRequiredFormsModel
/// </summary>
namespace Raj.EC.MasterModel
{
    public class BranchRequiredFormsModel : IModel
    {
        private IBranchRequiredFormsView objIBranchRequiredFormsView;
        private DAL objDAL = new DAL();
        private DataSet objDS= null;

        public BranchRequiredFormsModel(IBranchRequiredFormsView BranchRequiredFormsView)
        {
            objIBranchRequiredFormsView = BranchRequiredFormsView;
        }


        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam ={
                objDAL.MakeInParams("@City_ID", SqlDbType.Int, 0, objIBranchRequiredFormsView.CityID),
               objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, objIBranchRequiredFormsView.keyID)};

            objDAL.RunProc("dbo.EC_Master_Branch_RequiredForms_FillValues",objSqlParam, ref objDS);
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