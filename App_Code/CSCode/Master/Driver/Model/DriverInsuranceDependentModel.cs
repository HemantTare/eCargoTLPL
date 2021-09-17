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
using ClassLibraryMVP ;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EF.MasterView;

/// <summary>
/// Summary description for DriverInsuranceDependentModel
/// </summary>
/// 

namespace Raj.EF.MasterModel
{
    public class DriverInsuranceDependentModel:IModel 
    {
        private IDriverInsuranceDependentView objIDriverInsuranceDependentView;
        private DAL _objDAL = new DAL();
        private DataSet _objDS;

        public DriverInsuranceDependentModel(IDriverInsuranceDependentView driverInsuranceDependentView)
        {
            objIDriverInsuranceDependentView = driverInsuranceDependentView;
        }

        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam ={ _objDAL.MakeInParams("@KeyId", SqlDbType.Int, 0, objIDriverInsuranceDependentView.keyID) };
            _objDAL.RunProc("rstil7.EF_Mst_Driver_Insurance_Dependent_FillValues", objSqlParam, ref _objDS);
            return _objDS;
        }

        public DataSet FillInsuranceBranchOnInsuranceCompanyChange()
        {
            SqlParameter[] objSqlParam ={ _objDAL.MakeInParams("@Insurance_Company_ID", SqlDbType.Int, 0, objIDriverInsuranceDependentView.InsuranceCompanyID) };
            _objDAL.RunProc("rstil7.EF_Master_Driver_FillInsuranceBranchOnInsuranceCompanyChanged", objSqlParam, ref _objDS);
            return _objDS;
        }

        public DataSet BindGrid()
        {
            SqlParameter[] objSqlParam = { _objDAL.MakeInParams("@Driver_ID", SqlDbType.Int, 0, objIDriverInsuranceDependentView.keyID) 
                                         };
            _objDAL.RunProc("rstil7.EF_Mst_Driver_Insurance_Dependent_FillGrid", objSqlParam, ref _objDS);
            return _objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { _objDAL.MakeInParams("@Driver_ID", SqlDbType.Int, 0, objIDriverInsuranceDependentView .keyID) 
                                         };
            _objDAL.RunProc("rstil7.EF_Mst_Driver_Insurance_Dependent_ReadValues", objSqlParam, ref _objDS);
            return _objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }
    }
}
