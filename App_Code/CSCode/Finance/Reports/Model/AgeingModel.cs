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
/// Summary description for AgeingModel
/// </summary>
namespace Raj.EC.FinanceModel
{
    public class AgeingModel : IModel
    {
        private IAgeingView objIAgeingView;
        private DAL objDAL = new DAL();
        private DataSet objDS = new DataSet();
        private string _HierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _MainId = UserManager.getUserParam().MainId;
        private int _DivisionId = UserManager.getUserParam().DivisionId;


        public AgeingModel(IAgeingView ageingView)
        {
            objIAgeingView = ageingView;
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
    }
}