using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using System.Data.SqlClient;
using Raj.EC.ControlsView;
/// <summary>
/// Summary description for PODSentByModel
/// </summary>
/// 
namespace Raj.EC.ControlsModel
{
    public class PODSentByModel:IModel
    {

        private IPODSentByView objIPODSentByView;
        private DAL objDAL = new DAL();
        DataSet objDS = new DataSet();

        public PODSentByModel(IPODSentByView PODSentByView)
        {
            objIPODSentByView = PODSentByView;
        }

        public DataTable FillSentBy()
        {
            objDAL.RunProc("EC_Opr_PODSentType_FillValues", ref objDS);
            return objDS.Tables[0];
        }
    

        public DataSet ReadValues()
        {
           return objDS;
        }


        public Message Save()
        {
            Message ObjMsg = new Message();


            return ObjMsg;

        }
    }
}
