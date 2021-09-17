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
/// Summary description for FilterationModel
/// </summary>
namespace Raj.EC.FinanceModel
{
    public class FilterationModel : IModel
    {
        private IFilterationView objIFilterationView;
        private DAL objDAL = new DAL();
        private DataSet objDS = new DataSet();
        private int _DivisionId = UserManager.getUserParam().DivisionId;


        public FilterationModel(IFilterationView filterationView)
        {
            objIFilterationView = filterationView;
        }

        public Message Save()
        {

            Message objMessage = new Message();
            return objMessage;
        }
        public DataSet ReadValues()
        {
              return objDS;


        }
        public DataSet FillLedgerGroup()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@MenuItemCode", SqlDbType.Int, 0,objIFilterationView.MenuItemCode)
                                          };
            objDAL.RunProc("FA_RPT_FillFilterationLedgerGroup",objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet Get_Voucher_Type()
        {
            objDAL.RunProc("FA_RPT_Day_Book_Fill_Voucher_Type", ref objDS);
            return objDS;
        }


    }
}
