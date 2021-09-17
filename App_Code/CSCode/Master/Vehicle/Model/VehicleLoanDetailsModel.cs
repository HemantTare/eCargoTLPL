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
/// Summary description for VehicleLoanDetailsModel
/// </summary>
/// 
namespace Raj.EF.MasterModel
{

     class VehicleLoanDetailsModel:IModel 
     {
         private IVehicleLoanDetailsView objIVehicleLoanDetailsView;
         private DAL objDAL = new DAL();
         private DataSet objDS;
        // private int _userID = 1;
         private int _userID = UserManager.getUserParam().UserId;

         public VehicleLoanDetailsModel(IVehicleLoanDetailsView vehicleLoanDetailsView)
        {
            objIVehicleLoanDetailsView = vehicleLoanDetailsView; 
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objIVehicleLoanDetailsView.keyID) 
                                         };
            objDAL.RunProc("rstil7.EF_Master_Vehicle_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }

        public DataSet FillValues()
        {
         SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objIVehicleLoanDetailsView.keyID) 
                                     };
         objDAL.RunProc("[rstil42].[EF_Mst_Vehicle_LoanDetails_FillValues]", objSqlParam, ref objDS);
         return objDS;
        }

     }
}
