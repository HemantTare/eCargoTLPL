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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.FinanceView;


/// <summary>
/// Summary description for DayBookModel
/// </summary>
namespace Raj.EC.FinanceModel
{
    public class DayBookModel : IModel
    {
        private IDayBookView objIDayBookView;
        private DAL objDAL = new DAL();
        private DataSet objDS = new DataSet();
        private string _HierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _MainId = UserManager.getUserParam().MainId;
        private int _DivisionId = UserManager.getUserParam().DivisionId;


        public DayBookModel(IDayBookView dayBookView)
        {
            objIDayBookView = dayBookView;
        }

        public Message Save()
        {
        
           Message objMessage = new Message();
            return objMessage;
        }
        public DataSet ReadValues()
        {

            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@IsConsolidated", SqlDbType.Bit, 0,objIDayBookView.Is_Consol),
                                          objDAL.MakeInParams("@HierarchyCode", SqlDbType.VarChar  , 0, objIDayBookView.Hierarchy_Code),
                                          objDAL.MakeInParams("@MainId", SqlDbType.Int , 0,objIDayBookView.Main_Id),  
                                          objDAL.MakeInParams("@DivisionId",SqlDbType.Int,0,_DivisionId),
                                          objDAL.MakeInParams("@StartDate ",SqlDbType.DateTime,0,objIDayBookView.StartDate),
                                          objDAL.MakeInParams("@EndDate",SqlDbType.DateTime,0,objIDayBookView.EndDate),
                                          objDAL.MakeInParams("@VoucherType_Xml",SqlDbType.Xml,0,objIDayBookView.GetVoucherTypeID_XML)
                                          };
            objDAL.RunProc("[dbo].[FA_Rpt_Day_Book]", objSqlParam, ref objDS);
            return objDS;
      

        }

	}
}
