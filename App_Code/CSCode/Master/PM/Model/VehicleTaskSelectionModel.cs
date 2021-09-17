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
/// Summary description for VehicleTaskSelectionModel
/// </summary>
/// 
namespace Raj.EF.MasterModel
{

    public class VehicleTaskSelectionModel:IModel 
    {
        private IVehicleTaskSelectionView objIVehicleTaskSelectionView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = 1;

        public VehicleTaskSelectionModel(IVehicleTaskSelectionView vehicleTaskSelectionView)
        {
            objIVehicleTaskSelectionView = vehicleTaskSelectionView;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_Model_ID", SqlDbType.Int, 0, objIVehicleTaskSelectionView.keyID) 
                                         };
            //objDAL.RunProc("rstil42.EF_Mst_Vehicle_Model_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();
            string _TaskSelectionXML = objIVehicleTaskSelectionView.SessionTaskSelectionGrid.GetXml(); 
            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),                                               
                                               objDAL.MakeInParams("@Task_ID", SqlDbType.Int, 0, objIVehicleTaskSelectionView.keyID ),
                                               objDAL.MakeInParams("@Task_On_ID", SqlDbType.Int, 0, objIVehicleTaskSelectionView.Vehicle_Id),                         
                                               objDAL.MakeInParams("@TaskSelectionXML", SqlDbType.Xml, 0, _TaskSelectionXML), 
                                               objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, _userID)
                                         };


            objDAL.RunProc("[rstil42].[EF_Mst_Vehicle_Task_Selection_Save]",objSqlParam, ref objDS);
            
            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
        public DataSet FillGrid()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Task_On_ID", SqlDbType.Int, 0, objIVehicleTaskSelectionView.Vehicle_Id) 
                                         };
            objDAL.RunProc("rstil42.EF_Mst_Vehicle_Task_Selection_FillGrid", objSqlParam,ref objDS);
            return objDS;
            
        }
    }
}