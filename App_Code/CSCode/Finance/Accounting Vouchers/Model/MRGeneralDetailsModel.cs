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
using ClassLibraryMVP.General;
using ClassLibraryMVP.UI;
using ClassLibraryMVP;
using Raj.EC.FinanceView;
using Raj.EC;

/// <summary>
/// Summary description for MRGeneralDetailsModel
/// </summary>
/// 
namespace Raj.EC.FinanceModel
{
    public class MRGeneralDetailsModel : IModel
    {
        private DataSet _ds;
        private IMRGeneralDetailsView _MRGeneralDetailsView;
        private DAL _objDAL = new DAL();

        public MRGeneralDetailsModel(IMRGeneralDetailsView MRGeneralDetailsView)
        {
            _MRGeneralDetailsView = MRGeneralDetailsView;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] sqlpara = { _objDAL.MakeInParams("@GC_No",SqlDbType.VarChar,20,_MRGeneralDetailsView.GCNo),
                                    _objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),
                                    _objDAL.MakeInParams("@MR_Type_ID",SqlDbType.Int,0,_MRGeneralDetailsView.MR_Type_ID),
                                    _objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0, UserManager.getUserParam().DivisionId),
                                    _objDAL.MakeInParams("@Mr_branch_Id",SqlDbType.Int,0,UserManager.getUserParam().MainId),
                                    _objDAL.MakeInParams("@Document_Id",SqlDbType.Int,0,_MRGeneralDetailsView.Document_ID)};

            _objDAL.RunProc("EC_FA_MR_Get_GC_Details", sqlpara, ref _ds);

            return _ds;
        }
    }
}