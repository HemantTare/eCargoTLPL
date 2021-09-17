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
using Raj.EC.MasterView;
using System.Data.SqlClient;
/// <summary>
/// Summary description for SizeModel
/// </summary>
/// 
namespace Raj.EC.MasterModel
{
    public class SizeModel : IModel
    {
        private ISizeView objISizeView;
        private DataSet _ds;
        private DAL objDAL = new DAL();
        private int _userId = Convert.ToInt32(UserManager.getUserParam().UserId);

        public SizeModel(ISizeView SizeView)
        {
            objISizeView = SizeView;
        }

        public DataSet Fill_Values()
        {

            Common objcommon = new Common();
            return objcommon.EC_Common_Pass_Query("Select * from ec_master_function");
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@SizeID", SqlDbType.Int, 0, objISizeView.keyID) };
            objDAL.RunProc("EC_Mst_Size_ReadValues", objSqlParam, ref _ds);
            return _ds;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                    objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                    objDAL.MakeInParams("@SizeID",SqlDbType.Int,0,objISizeView.keyID),
                    objDAL.MakeInParams("@SizeName",SqlDbType.VarChar,50,objISizeView.SizeName),
                    objDAL.MakeInParams("@ApproxChargeWeight",SqlDbType.Decimal,0,objISizeView.ApproxChargeWieght),
                    objDAL.MakeInParams("@FunctionId",SqlDbType.TinyInt,0,objISizeView.Function),
                    objDAL.MakeInParams("@FactorAmount",SqlDbType.Decimal,0,objISizeView.FactorAmount),
                    objDAL.MakeInParams("@MinChrgQty",SqlDbType.Decimal,0,objISizeView.MinChrgQty),
                    objDAL.MakeInParams("@Description",SqlDbType.VarChar,255,objISizeView.Description),
                    objDAL.MakeInParams("@UpdatedBy",SqlDbType.Int,0,_userId)};

            objDAL.RunProc("EC_Mst_Size_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";
            }
            return objMessage;
        }

    }
}