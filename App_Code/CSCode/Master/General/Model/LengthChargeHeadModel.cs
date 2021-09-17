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
using Raj.EC.GeneralView;

/// <summary>
/// Summary description for LengthChargeHeadModel
/// </summary>
namespace Raj.EC.GeneralModel
{
    class LengthChargeHeadModel : IModel
    {
        private ILengthChargeHeadView objILengthChargeHeadView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;


        public LengthChargeHeadModel(ILengthChargeHeadView lengthChargeHeadView)
        {
            objILengthChargeHeadView = lengthChargeHeadView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@LengthChargeHeadId", SqlDbType.Int, 0,objILengthChargeHeadView.keyID)
                                         };
            objDAL.RunProc("[dbo].[EC_Mst_LengthChargeHeadName_ReadValues]", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@KeyID",SqlDbType.Int,0, objILengthChargeHeadView.keyID),
                                               objDAL.MakeInParams("@LengthCharge", SqlDbType.Decimal, 0,objILengthChargeHeadView.LengthCharge),
                                               objDAL.MakeInParams("@FromLength",SqlDbType.VarChar,5,objILengthChargeHeadView.FromLength),
                                               objDAL.MakeInParams("@ToLength",SqlDbType.VarChar,5,objILengthChargeHeadView.ToLength)
                                               
                                              
                                         };


            objDAL.RunProc("[dbo].[EC_Mst_LengthChargeHeadName_Save]", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }

    }
}
