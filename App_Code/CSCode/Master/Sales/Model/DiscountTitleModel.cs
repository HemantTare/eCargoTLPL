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
/// Summary description for DiscountTitleModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class  DiscountTitleModel : IModel
    {
        private IDiscountTitleView objIDiscountTitleView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = 1;
        //private int _userID = UserManager.getUserParam().UserId;

        public DiscountTitleModel(IDiscountTitleView DiscountTitleView)
        {
            objIDiscountTitleView = DiscountTitleView;
        } 
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@DiscountTitleID", SqlDbType.Int, 0, objIDiscountTitleView.keyID)
                                         };
            objDAL.RunProc("EC_Mst_DiscountTitle_ReadValues", objSqlParam, ref objDS);
            return objDS;
        } 

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@DiscountTitleID",SqlDbType.Int,0, objIDiscountTitleView.keyID),
                                               objDAL.MakeInParams("@DiscountTitleName", SqlDbType.VarChar, 50,objIDiscountTitleView.DiscountTitleName),  
                                               objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 500,objIDiscountTitleView.Remarks),  
                                               objDAL.MakeInParams("@UserId", SqlDbType.Int,0,  _userID)
                                              
                                         };


            objDAL.RunProc("EC_Mst_DiscountTitle_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }
       
    }

}
