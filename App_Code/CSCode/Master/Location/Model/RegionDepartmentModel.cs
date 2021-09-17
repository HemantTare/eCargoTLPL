using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.MasterView;


/// <summary>
/// Summary description for RegionDepartmentModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class RegionDepartmentModel : IModel
    {
        private IRegionDepartmentView objIRegionDepartmentView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        public RegionDepartmentModel(IRegionDepartmentView regionDepartmentView)
        {
            objIRegionDepartmentView = regionDepartmentView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@RegionId", SqlDbType.Int, 0, objIRegionDepartmentView.keyID)
                                         };
            objDAL.RunProc("EC_Mst_Region_ReadDepartmentCheckedValues", objSqlParam, ref objDS);
            return objDS;
        }
        public DataSet ReadParameterValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@RegionId", SqlDbType.Int, 0, objIRegionDepartmentView.keyID)
                                         };
            objDAL.RunProc("EC_Mst_Region_ReadParameterValues", objSqlParam, ref objDS);
            return objDS;

        }



        public DataSet GetDepartmentValues()
        {

            objDAL.RunProc("[dbo].[EC_Mst_Region_FillDepartmentValues]", ref objDS);
            return objDS;
        }


        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }

    }

}
