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
/// Summary description for UserMasterModel
/// </summary>
namespace Raj.EC.GeneralModel
{
    class UserMasterModel : IModel
    {
        private IUserMasterView objIUserMasterView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;


        public UserMasterModel(IUserMasterView userMasterView)
        {
            objIUserMasterView = userMasterView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@NonEmpUserId", SqlDbType.Int, 0,objIUserMasterView.keyID)
                                         };
            objDAL.RunProc("[dbo].[EC_Mst_UserMaster_ReadValues]", objSqlParam, ref objDS);         
            return objDS;
        }

        public DataSet FillValues()
        {
            
            objDAL.RunProc("EC_Mst_UserMaster_FillProfileValues",  ref objDS);
            return objDS;
        }

        public DataSet FillBranch()
        {

            objDAL.RunProc("EC_Mst_UserMaster_FillBranchValues", ref objDS);
            return objDS;
        }


        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                                objDAL.MakeOutParams("@User_Name",SqlDbType.VarChar, 10),                               
                                               objDAL.MakeInParams("@NonEmpUserID",SqlDbType.Int,0, objIUserMasterView.keyID),
                                               objDAL.MakeInParams("@NonEmpUserName", SqlDbType.VarChar, 100,objIUserMasterView.NonEmpUserName),                                                
                                               objDAL.MakeInParams("@AddressLine1", SqlDbType.VarChar,100,objIUserMasterView.AddressView.AddressLine1),
                                               objDAL.MakeInParams("@AddressLine2", SqlDbType.VarChar,100,objIUserMasterView.AddressView.AddressLine2),
                                               objDAL.MakeInParams("@CityId", SqlDbType.Int,0,objIUserMasterView.AddressView.CityId),
                                               objDAL.MakeInParams("@PinCode", SqlDbType.NVarChar,15,objIUserMasterView.AddressView.PinCode),
                                               objDAL.MakeInParams("@StdCode", SqlDbType.NVarChar,15,objIUserMasterView.AddressView.StdCode),
                                               objDAL.MakeInParams("@Phone1", SqlDbType.NVarChar,20,objIUserMasterView.AddressView.Phone1),
                                               objDAL.MakeInParams("@Phone2", SqlDbType.NVarChar,20,objIUserMasterView.AddressView.Phone2),
                                               objDAL.MakeInParams("@MobileNo",SqlDbType.NVarChar,20,objIUserMasterView.AddressView.MobileNo),
                                               objDAL.MakeInParams("@Fax", SqlDbType.NVarChar,20,objIUserMasterView.AddressView.FaxNo),
                                               objDAL.MakeInParams("@EmailId", SqlDbType.VarChar,100,objIUserMasterView.AddressView.EmailId),                                              
                                               objDAL.MakeInParams("@UserId",SqlDbType.Int,0,_userID),
                                               objDAL.MakeInParams("@ProfileId",SqlDbType.Int,0,objIUserMasterView.ProfileId),
                                               objDAL.MakeInParams("@BranchId",SqlDbType.Int,0,objIUserMasterView.BranchId),
                                               objDAL.MakeInParams("@IsActive",SqlDbType.Bit,1,objIUserMasterView.IsActive)
                                              
                                               
                                              
                                         };


            objDAL.RunProc("[dbo].[EC_Mst_UserMaster_Save]", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);
            string UserName = Convert.ToString(objSqlParam[2].Value);


            if (objMessage.messageID == 0)
            {

                string _Msg;
                if (objIUserMasterView.keyID < 0)
                {
                    _Msg = UserName + " " + "Created SuccessFully";
                }
                else
                {
                    _Msg = "saved SuccessFully";
                }
                string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + LinkUrl + "&DecryptUrl='No'");
            }
            else
            {
                Common.DisplayErrors(objMessage.messageID);
            }


            return objMessage;
        }
    }
}
