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
/// Summary description for AreaDepartmentModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class AreaDepartmentModel : IModel
    {
        private IAreaDepartmentView objIAreaDepartmentView;
        private DAL objDAL = new DAL();
        private DataSet objDS;       

        public AreaDepartmentModel(IAreaDepartmentView areaDepartmentView)
        {
            objIAreaDepartmentView = areaDepartmentView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@AreaId", SqlDbType.Int, 0, objIAreaDepartmentView.keyID)
                                         };
            objDAL.RunProc("EC_Mst_Area_ReadDepartmentCheckedValues", objSqlParam, ref objDS);
            return objDS;
        }
        public DataSet ReadParameterValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@AreaId", SqlDbType.Int, 0, objIAreaDepartmentView.keyID)
                                         };
            objDAL.RunProc("EC_Mst_Area_ReadParameterValues", objSqlParam, ref objDS);
            return objDS;              
        }        

        public DataSet GetDepartmentValues()
        {
               objDAL.RunProc("[dbo].[EC_Mst_Area_FillDepartmentValues]", ref objDS);
                return objDS;
        }
        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.[EC_Master_Region_Area_FillValues]", ref objDS);
            return objDS;
        }
      
        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }

    }

}
