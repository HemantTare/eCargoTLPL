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
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.MasterView;

/// <summary>
/// Summary description for LocalCollectionVoucherModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{
    public class LocalCollectionVoucherModel:IModel 
    {
        private ILocalCollectionVoucherView objILocalCollectionVoucherView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;
        public LocalCollectionVoucherModel(ILocalCollectionVoucherView localCollectionVoucherView)
        {
            objILocalCollectionVoucherView = localCollectionVoucherView;
        }
        public DataSet ReadValues()
        {
            return objDS;
        }       
        
        public Message Save()
        {
            Message objMessage = new Message();     
            return objMessage;
        }
    }
}