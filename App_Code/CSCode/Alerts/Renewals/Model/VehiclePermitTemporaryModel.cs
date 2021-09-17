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
/// Summary description for VehiclePermitTemporaryModel
/// </summary>
namespace Raj.EF.MasterModel
{

    public class  VehiclePermitTemporaryModel : IModel
    {
        private IVehiclePermitTemporaryView objIVehiclePermitTemporaryView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = 1;
        //private int _userID = UserManager.getUserParam().UserId;

        public VehiclePermitTemporaryModel(IVehiclePermitTemporaryView  vehiclePermitTemporaryView)
        {
            objIVehiclePermitTemporaryView =  vehiclePermitTemporaryView;
        }

        public DataSet ReadValues()
        {
            //SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_Model_ID", SqlDbType.Int, 0, objIVehiclePMView.keyID) 
            //                             };
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();

            //SqlParameter[] objSqlParam = { 
            //                                   objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            //                                   objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),                                               
            //                                   objDAL.MakeInParams("@Vehicle_Model_ID", SqlDbType.Int, 0, objITaskListView.keyID ),                                               
            //                                   objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, _userID)
            //                             };


            //objDAL.RunProc("rstil42.EF_Mst_Vehicle_Model_Save", objSqlParam);

            //objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            //objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
        public DataSet FillGrid()
        {

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeInParams("@TaskDefinationId", SqlDbType.Int, 0,objIVehiclePermitTemporaryView.TaskDefinationId)
                                              };

            objDAL.RunProc("rstil8.EF_Trn_PM_Task_Alert_FillGrid", objSqlParam, ref objDS);
            return objDS;
        }

    }
}

