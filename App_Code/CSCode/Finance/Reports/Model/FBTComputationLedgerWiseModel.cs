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
/// Summary description for FBTComputationLedgerWiseModel
/// </summary>
namespace Raj.FA.ReportsModel
{
    public class FBTComputationLedgerWiseModel : IModel
    {
        private IFBTComputationLedgerWiseView objIFBTComputationLedgerWiseView;
        private DataSet objDS;
        private DAL objDAL = new DAL();
        private int _userId = UserManager.getUserParam().UserId;
        private string _hierarchy_Code = UserManager.getUserParam().HierarchyCode;
        private int _main_Id = UserManager.getUserParam().MainId;

        public FBTComputationLedgerWiseModel(IFBTComputationLedgerWiseView fbtComputationLedgerWiseView)
        {
            objIFBTComputationLedgerWiseView = fbtComputationLedgerWiseView;
        }
        public DataSet ReadValues()
        {
            return objDS;
        }

        public DataSet GetValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Is_Consol", SqlDbType.Bit, 0, objIFBTComputationLedgerWiseView.Is_Consol),
                                          objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,5,objIFBTComputationLedgerWiseView.Hierarchy_Code),
                                          objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0,UserManager.getUserParam().DivisionId),
                                          objDAL.MakeInParams("@FBT_Category_Id",SqlDbType.Int,0,objIFBTComputationLedgerWiseView.FBTCategoryId),
                                          objDAL.MakeInParams("@Main_Id", SqlDbType.Int , 0,objIFBTComputationLedgerWiseView.Main_Id),  
                                          objDAL.MakeInParams("@From_Date ",SqlDbType.DateTime,0,objIFBTComputationLedgerWiseView.Start_Date ),
                                          objDAL.MakeInParams("@To_Date",SqlDbType.DateTime,0,objIFBTComputationLedgerWiseView.End_Date)
                                         };
            
            objDAL.RunProc("FA_FBT_Computation_Ledger_Wise", objSqlParam, ref objDS);

            return objDS;//Tables[1];

        }
        public DataSet FillLabelValues()
        {
            SqlParameter[] objSqlParam ={
                                          objDAL.MakeInParams("@FBTCategoryId",SqlDbType.Int,0,objIFBTComputationLedgerWiseView.FBTCategoryId)
                                         };
            objDAL.RunProc("FA_FBTComputation_LedgerWise_FillLabelValues", objSqlParam, ref objDS);
            return objDS;

        }
        public Message Save()
        {
            Message objMsg = new Message();
            return objMsg;

        }


    }
}
