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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EF.MasterView;

/// <summary>
/// Summary description for VehicleHireDetailsModel
/// </summary>
/// 
namespace Raj.EF.MasterModel
{

    class VehicleHireDetailsModel:IModel 
    {
        private IVehicleHireDetailsView objIVehicleHireDetailsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
       // private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;

        public VehicleHireDetailsModel(IVehicleHireDetailsView vehicleHireDetailsView)
        {
            objIVehicleHireDetailsView = vehicleHireDetailsView;
        }

        public DataSet FillDDLHireType()
        {
            objDAL.RunProc("[rstil42].[EF_Mst_Vehicle_HireDetails_FillHireType]", ref objDS);
            return objDS;
        }

        public DataSet FillDDLMaintainedBy()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Maintained_By_ID", SqlDbType.Int, 0, objIVehicleHireDetailsView.MaintainedByID) };
            objDAL.RunProc("[rstil42].[EF_Mst_Vehicle_HireDetails_FillValues]",objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objIVehicleHireDetailsView.keyID) };
            objDAL.RunProc("[rstil42].[EF_Mst_Vehicle_Hire_Details_ReadValues]", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            return objMessage;
        }
    }
}