using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.DataAccess;
using Raj.EC.ControlsView;
namespace Raj.EC.ControlsModel
{
    public class AddressModel
    {  
 
            private IAddressView objIAddressView;
            private DAL objDAL = new DAL();
            DataSet objDS = new DataSet();
           
   
        public AddressModel(IAddressView _objIAddressView)
        {
           objIAddressView = _objIAddressView;
        }

        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@CityId", SqlDbType.Int, 0, objIAddressView.KeyId) };
            objDAL.RunProc("dbo.EC_Master_CC_Address_FillValues", objSqlParam, ref objDS);
            return objDS;
        }


        public DataSet FillOnCityChanged()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@CityId", SqlDbType.Int, 0, objIAddressView.CityId) };
            objDAL.RunProc("EC_Master_CC_Address_FillOnCityChanged", objSqlParam, ref objDS);
            return objDS;
        }

    }
 }
 
