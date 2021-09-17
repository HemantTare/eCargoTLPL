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
/// Summary description for RegionGeneralDetailsModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class RegionGeneralDetailsModel : IModel
    {
        private IRegionGeneralDetailsView objIRegionGeneralDetailsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;


        public RegionGeneralDetailsModel(IRegionGeneralDetailsView regionGeneralDetailsView)
        {
            objIRegionGeneralDetailsView = regionGeneralDetailsView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@RegionId", SqlDbType.Int, 0, objIRegionGeneralDetailsView.keyID)
                                         };
            objDAL.RunProc("dbo.EC_Mst_Region_ReadDivisionCheckedValues", objSqlParam, ref objDS);
            return objDS;
        }


        public DataSet ReadGeneralValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@RegionId", SqlDbType.Int, 0, objIRegionGeneralDetailsView.keyID)
                                         };
            objDAL.RunProc("EC_Mst_Region_ReadGeneralDetailsValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet GetDivision()
        {


            objDAL.RunProc("[dbo].[EC_Mst_Region_FillDivision]", ref objDS);



           
            return objDS;
        }

        public DataSet GetLabelValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@RegionId", SqlDbType.Int, 0, objIRegionGeneralDetailsView.keyID)
                                         };
            objDAL.RunProc("EC_Mst_Region_FillLabelValues", objSqlParam, ref objDS);
            return objDS;
        }

        

        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;

        }
    }
}

