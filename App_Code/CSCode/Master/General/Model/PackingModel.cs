using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.MasterView;

/// <summary>
/// Name: Ankit champaneriya
/// date : 20/11/08
/// Summary description for PackingModel
/// </summary>
/// 

namespace Raj.EC.MasterModel
{
    public class PackingModel:IModel 
    {
        private IPackingView _iPackingView;
        private DataSet _DS;
        private DAL objDAL = new DAL();
        private int _userId = Convert.ToInt32(UserManager.getUserParam().UserId);
      
        public PackingModel(IPackingView iPackingView)
        {
            _iPackingView = iPackingView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Packing_ID", SqlDbType.Int, 0, _iPackingView.keyID) };
            objDAL.RunProc("EC_Mst_Packing_ReadValues", objSqlParam, ref _DS);
            return _DS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Packing_ID", SqlDbType.Int, 0,_iPackingView.keyID ),
            objDAL.MakeInParams("@Packing_Type", SqlDbType.VarChar, 50,_iPackingView.PackingType  ),            
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userId)};

            objDAL.RunProc("EC_Mst_Packing_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            //if (objMessage.messageID == 0)
            //{

            //    string _Msg;
            //    _Msg = "Saved SuccessFully";
            //    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            //}


            return objMessage;
        }
    }
}