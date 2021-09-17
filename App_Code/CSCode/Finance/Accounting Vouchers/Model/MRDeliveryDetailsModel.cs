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
/// Summary description for MRDeliveryDetailsModel
/// </summary>
namespace Raj.EC.FinanceModel
{
    public class MRDeliveryDetailsModel : IModel
    {
        private DataSet objDS = new DataSet();
        private IMRDeliveryDetailsView ObjIMRDeliveryDetailsView;
        private DAL objDAL = new DAL();
      

        public MRDeliveryDetailsModel(IMRDeliveryDetailsView MRDeliveryDetailsView)
        {
            ObjIMRDeliveryDetailsView = MRDeliveryDetailsView;
        }
       

        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] sqlpara = 
                                {objDAL.MakeInParams("@MR_ID",SqlDbType.Int,0, ObjIMRDeliveryDetailsView.keyID)};
            objDAL.RunProc("EC_FA_MRDeliveryDetails_ReadValues", sqlpara, ref objDS);
            return objDS;                       
       
           
        }

	}
}
