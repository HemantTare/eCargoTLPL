using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

using Raj.EF.MasterView;
using Raj.EC.ControlsView;

/// <summary>
/// Summary description for DriverDetailsModel
/// </summary>
/// 

namespace Raj.EF.MasterModel
{
    public class DriverDetailsModel : IModel
    {
       // private IAddressView objIAddressView;
        private IDriverDetailsView objIDriverDetailsView;
        private DAL _objDAL = new DAL();
        private DataSet _objDS;

        public DriverDetailsModel(IDriverDetailsView DriverDetailsView)
        {
            objIDriverDetailsView = DriverDetailsView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { _objDAL.MakeInParams("@Driver_ID", SqlDbType.Int, 0, objIDriverDetailsView.keyID) };
            _objDAL.RunProc("rstil7.EF_Master_Driver_ReadValues", objSqlParam, ref _objDS);
            return _objDS;
        }

        public DataSet FillValues()
        {
             SqlParameter[] objSqlParam = { _objDAL.MakeInParams("@KeyId", SqlDbType.Int, 0, objIDriverDetailsView.keyID) };
            _objDAL.RunProc("rstil7.EF_Master_Driver_FillValues", objSqlParam,ref _objDS);
            return _objDS;
        }

        public string   ISDuplicateDriverCheck()
        {
            bool  _isDuplicate;
            string _Duplicate_Field;
            SqlParameter[] SqlPara ={_objDAL.MakeInParams("@Driver_Code",SqlDbType.VarChar,0,objIDriverDetailsView.DriverCode),
                                    _objDAL.MakeInParams("@Driver_License_No",SqlDbType.VarChar,0,objIDriverDetailsView.DriverLicenseNo),
                                     _objDAL.MakeInParams("@Driver_Id",SqlDbType.Int,0,objIDriverDetailsView.keyID),
                                     _objDAL.MakeOutParams("@Duplicate",SqlDbType.Bit,0),
                                     _objDAL.MakeOutParams("@Duplicate_Field",SqlDbType.VarChar,20)};

            _objDAL.RunProc("rstil22.EF_Master_Driver_Check_Duplication", SqlPara);
           
            _isDuplicate = Convert.ToBoolean(SqlPara[3].Value.ToString());
            _Duplicate_Field = SqlPara[4].Value.ToString();

            return _Duplicate_Field;

        }

    
        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }
    }
}