using System;
using System.Data;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

using System.Data.SqlClient;


using Raj.EC.MasterView;

/// <summary>
/// Name : Ankit champaneriya
/// Date : 10-11-2008
/// Summary description for FormModel
/// </summary>
/// 
/// 

namespace Raj.EC.MasterModel
{
    public class RequiredFormModel : IModel
    {
        private IRequiredFormView _iRequiredFormView;
        private DataSet _ds;
        private DAL objDAL = new DAL();
        private int _userId = Convert.ToInt32(UserManager.getUserParam().UserId);

        public RequiredFormModel(IRequiredFormView iRequiredFormView)
        {
            _iRequiredFormView = iRequiredFormView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Form_ID", SqlDbType.Int, 0, _iRequiredFormView.keyID) };
            objDAL.RunProc("[EC_Mst_RequiredForm_ReadValues]", objSqlParam, ref _ds);
            return _ds;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                objDAL.MakeInParams("@Form_Id",SqlDbType.Int,0,_iRequiredFormView.keyID ),
                objDAL.MakeInParams("@Form_Name",SqlDbType.VarChar,50,_iRequiredFormView.FormName ),
                objDAL.MakeInParams("@Description",SqlDbType.VarChar,200,_iRequiredFormView.Description ),
                objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,_userId ),
                objDAL.MakeInParams("@Is_Active",SqlDbType.Bit,0,1)};

            objDAL.RunProc("EC_Mst_General_RequiredForm_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }


    }
}