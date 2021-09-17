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
using ClassLibraryMVP.General ;
using ClassLibraryMVP.DataAccess;
using Raj.EF.MasterView;

/// <summary>
/// Summary description for BankModel
/// </summary>
/// 

namespace Raj.EF.MasterModel
{
    public class BankModel:IModel 
    {
        private IBankView objIBankView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        //private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;

        public BankModel(IBankView BankView)
        {
            objIBankView = BankView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@BankID", SqlDbType.Int, 0, objIBankView.keyID) };
            objDAL.RunProc("rstil41.EF_Mst_Bank_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam={objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                   objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                   objDAL.MakeInParams("@Bank_ID", SqlDbType.Int, 0, objIBankView.keyID),
                                   objDAL.MakeInParams("@Bank_Name", SqlDbType.VarChar, 100,objIBankView.BankName ),
                                   objDAL.MakeInParams("@AccountNo", SqlDbType.VarChar, 50,objIBankView.AccountNo ), 
                                   objDAL.MakeInParams("@Comments", SqlDbType.VarChar, 500,objIBankView.Comments ), 
                                   objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, _userID)};


            objDAL.RunProc("rstil41.EF_Mst_Bank_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
    }
}

