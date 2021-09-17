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
/// Summary description for FBTComputationModel
/// </summary>
namespace Raj.FA.ReportsModel
{
    public class FBTComputationModel : IModel
    {
        private IFBTComputationView objIFBTComputationView;
        private DataSet objDS;
        private DAL objDAL = new DAL();
        private int _userId = UserManager.getUserParam().UserId;
        private string _hierarchy_Code = UserManager.getUserParam().HierarchyCode;
        private int _main_Id = UserManager.getUserParam().MainId;
        private int _division_id = UserManager.getUserParam().DivisionId;

        public FBTComputationModel(IFBTComputationView fbtComputationView)
        {
            objIFBTComputationView = fbtComputationView;
        }
        public DataSet ReadValues()
        {
            return objDS;
        }

        public DataSet GetValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Is_Consol", SqlDbType.Bit, 0,objIFBTComputationView.Is_Consol),
                                          objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,_division_id),
                                          objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5,objIFBTComputationView.Hierarchy_Code),
                                          objDAL.MakeInParams("@Main_Id", SqlDbType.Int , 0,objIFBTComputationView.Main_Id ),  
                                          objDAL.MakeInParams("@From_Date ",SqlDbType.DateTime,0,objIFBTComputationView.Start_Date),
                                          objDAL.MakeInParams("@To_Date",SqlDbType.DateTime,0,objIFBTComputationView.End_Date),
                                          };

            objDAL.RunProc("FA_FBT_Computation", objSqlParam, ref objDS);

            return objDS;

        }
         public Message Save()
        {
               Message objMsg = new Message();
                return objMsg;

        }


    }
}
