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
using ClassLibraryMVP.DataAccess;
using Raj.FA.ReportsView;
using ClassLibraryMVP;


/// <summary>
/// Summary description for TDSComputationModel
/// </summary>
namespace Raj.FA.ReportsModel
{
    public class TDSComputationModel : IModel
    {
        private ITDSComputationView objITDSComputationView;
        private DataSet objDS;
        private DAL objDAL = new DAL();
        private int _userId = UserManager.getUserParam().UserId;
        private string _hierarchy_Code = UserManager.getUserParam().HierarchyCode;
        private int _main_Id = UserManager.getUserParam().MainId;

        public TDSComputationModel(ITDSComputationView tdsComputationView)
        {
            objITDSComputationView = tdsComputationView;
        }
        public DataSet ReadValues()
        {
            return objDS;
        }

        public DataSet ReadGridValues()
        {
            SqlParameter[] objSqlParam ={ 
                                          objDAL.MakeInParams("@Is_Consol", SqlDbType.Bit, 0, objITDSComputationView.Is_Consol),
                                          objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar  , 0,objITDSComputationView.Hierarchy_Code ),
                                          objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0,UserManager.getUserParam().DivisionId),
                                          objDAL.MakeInParams("@Main_Id", SqlDbType.Int , 0,objITDSComputationView.Main_Id ),  
                                          objDAL.MakeInParams("@From_Date ",SqlDbType.DateTime,0,objITDSComputationView.Start_Date),
                                          objDAL.MakeInParams("@To_Date",SqlDbType.DateTime,0,objITDSComputationView.End_Date),
                                          };
            
            objDAL.RunProc("FA_TDS_Computation", objSqlParam, ref objDS);
          
            objDS.Tables[0].Columns["Is_TDS_Ledger"].DataType=typeof(System.Boolean);

            return objDS;
        }
       
        public Message Save()
        {
            Message objMsg = new Message();
            return objMsg;

        }


    }
}