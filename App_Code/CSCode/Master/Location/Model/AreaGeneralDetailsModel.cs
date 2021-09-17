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
/// Summary description for AreaGeneralDetailsModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class AreaGeneralDetailsModel : IModel
    {
        private IAreaGeneralDetailsView objIAreaGeneralDetailsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
       
        public AreaGeneralDetailsModel(IAreaGeneralDetailsView areaGeneralDetailsView)
        {
            objIAreaGeneralDetailsView = areaGeneralDetailsView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@AreaId", SqlDbType.Int, 0, objIAreaGeneralDetailsView.keyID),
                                           objDAL.MakeInParams("@CityId",SqlDbType.Int,0,objIAreaGeneralDetailsView.AddressView.CityId)
                                         };
            objDAL.RunProc("dbo.EC_Mst_Area_ReadDivisionCheckedValues", objSqlParam, ref objDS);
            return objDS;
        }


        public DataSet ReadGeneralValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@AreaId", SqlDbType.Int, 0, objIAreaGeneralDetailsView.keyID)
                                         };
            objDAL.RunProc("EC_Mst_Area_ReadGeneralDetailsValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet GetDivision()
        {         
            objDAL.RunProc("[dbo].[EC_Mst_Area_FillDivision]", ref objDS);                   
            return objDS;
        }      

        public Message Save()
        {
            Message objMessage = new Message();            
            return objMessage;
	     }
    }
}
