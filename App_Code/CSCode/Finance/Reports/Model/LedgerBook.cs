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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP;

/// <summary>
/// Summary description for LedgerBookModel
/// </summary>
/// 
namespace Raj.EC
{
    public class LedgerBook
    {
        private DAL objDal = new DAL();
        private DataSet objDS = null;
        //private string _HierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _MainId = UserManager.getUserParam().MainId;
        private int _DivisionId = UserManager.getUserParam().DivisionId;
     




        public LedgerBook()
        {
            //
            // TODO: Add constructor logic here
            //
        }

      
        public DataSet FillLedgerBookGridList(Boolean Is_Consol,string Hierarchy_Code,int Main_Id, DateTime StartDate,DateTime EndDate,int LedgerGroupId)
        {
            SqlParameter[] sqlpar = 
           {
            objDal.MakeInParams("@IsConsolidated", SqlDbType.Bit, 1, Is_Consol),
            objDal.MakeInParams("@HierarchyCode", SqlDbType.VarChar, 2,Hierarchy_Code),
            objDal.MakeInParams("@MainId", SqlDbType.Int, 0, Main_Id),
            objDal.MakeInParams("@DivisionId", SqlDbType.Int, 0, _DivisionId),
            objDal.MakeInParams("@StartDate", SqlDbType.DateTime, 0, StartDate),
            objDal.MakeInParams("@EndDate", SqlDbType.DateTime, 0, EndDate),
            objDal.MakeInParams("@LedgerGroupId",SqlDbType.Int,0,LedgerGroupId)

            };

            objDal.RunProc("[dbo].[FA_Rpt_Ledger_Book]", sqlpar, ref objDS);
            return objDS;
        }


    }
}