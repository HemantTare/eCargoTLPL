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
/// Summary description for BookingParametersModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class BookingParametersModel : IModel
    {
        private IBookingParametersView objIBookingParametersView;
        private DAL objDAL = new DAL();
        private DataSet objDS;


        public BookingParametersModel(IBookingParametersView bookingParametersView)
        {
            objIBookingParametersView = bookingParametersView;
        }
                

        public DataSet ReadValues()
        {
           
            objDAL.RunProc("Ec_Mst_CompanyBookingParameters_ReadValues", ref objDS);
            return objDS;
        }   
     

        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;

        }
    }
}
