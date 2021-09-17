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
using Raj.EC.SalesView;


/// <summary>
/// Summary description for RegularClientModel
/// </summary>
namespace Raj.EC.SalesModel
{
    class RegularClientModel : IModel
    {
        private IRegularClientView objIRegularClientView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;


        public RegularClientModel(IRegularClientView regularClientView)
        {
            objIRegularClientView = regularClientView;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Mst_RegularClient_FillValues", ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@ClientId", SqlDbType.Int, 0,objIRegularClientView.keyID)
                                         };
            objDAL.RunProc("[dbo].[EC_Mst_RegularClient_ReadValues]", objSqlParam, ref objDS);




            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeOutParams("@ClientId", SqlDbType.Int, 0), 
                                               objDAL.MakeInParams("@KeyID",SqlDbType.Int,0, objIRegularClientView.keyID),
                                               objDAL.MakeInParams("@RegularClientName", SqlDbType.VarChar, 100,objIRegularClientView.RegularClientName), 
                                               objDAL.MakeInParams("@ContactPerson",SqlDbType.VarChar,50,objIRegularClientView.ContactPerson),
                                               objDAL.MakeInParams("@AddressLine1", SqlDbType.VarChar,100,objIRegularClientView.AddressView.AddressLine1),
                                               objDAL.MakeInParams("@AddressLine2", SqlDbType.VarChar,100,objIRegularClientView.AddressView.AddressLine2),
                                               objDAL.MakeInParams("@BranchId", SqlDbType.Int,0,0),
                                               objDAL.MakeInParams("@CityId", SqlDbType.Int,0,objIRegularClientView.AddressView.CityId),
                                               objDAL.MakeInParams("@PinCode", SqlDbType.NVarChar,15,objIRegularClientView.AddressView.PinCode),
                                               objDAL.MakeInParams("@StdCode", SqlDbType.NVarChar,15,objIRegularClientView.AddressView.StdCode),
                                               objDAL.MakeInParams("@Phone1", SqlDbType.NVarChar,20,objIRegularClientView.AddressView.Phone1),
                                               objDAL.MakeInParams("@Phone2", SqlDbType.NVarChar,20,objIRegularClientView.AddressView.Phone2),
                                               objDAL.MakeInParams("@MobileNo",SqlDbType.NVarChar,20,objIRegularClientView.AddressView.MobileNo),
                                               objDAL.MakeInParams("@Fax", SqlDbType.NVarChar,20,objIRegularClientView.AddressView.FaxNo),
                                               objDAL.MakeInParams("@EmailId", SqlDbType.VarChar,100,objIRegularClientView.AddressView.EmailId),
                                               objDAL.MakeInParams("@SMS_Alert", SqlDbType.Bit, 0,objIRegularClientView.AddressView.SMSAlert),
                                               objDAL.MakeInParams("@eMail_Alert", SqlDbType.Bit, 0,objIRegularClientView.AddressView.eMailAlert),
                                               objDAL.MakeInParams("@IsServiceTaxApplicable",SqlDbType.Bit,0,objIRegularClientView.IsServiceTaxPayable),
                                               objDAL.MakeInParams("@CSTNo",SqlDbType.VarChar,50,objIRegularClientView.CSTNo),
                                               objDAL.MakeInParams("@ServiceTaxNo",SqlDbType.VarChar,50,objIRegularClientView.ServiceTaxNo),
                                               objDAL.MakeInParams("@DeliveryAreaID", SqlDbType.Int, 0,objIRegularClientView.DeliveryAreaId),                           
                                               objDAL.MakeInParams("@UserId",SqlDbType.Int,0,_userID),
                                               objDAL.MakeInParams("@CategoryID",SqlDbType.Int,0,objIRegularClientView.ClientCategoryID),
                                               objDAL.MakeInParams("@Delivery_Type_Id",SqlDbType.Int,0,objIRegularClientView.DeliveryTypeID),
                                               objDAL.MakeInParams("@Is_ToPay_Allowed",SqlDbType.Bit,0,objIRegularClientView.Is_ToPay_Allowed),
                                               objDAL.MakeInParams("@Client_Group_ID", SqlDbType.Int, 0,objIRegularClientView.ClientGroupID),
                                               objDAL.MakeInParams("@Is_Casual_Taxable", SqlDbType.Int, 0,objIRegularClientView.Is_Casual_Taxable),
                                               objDAL.MakeInParams("@Remarks", SqlDbType.VarChar,1000,objIRegularClientView.Remarks),
                                               objDAL.MakeInParams("@Landmark1ID", SqlDbType.Int, 0,objIRegularClientView.Landmark1ID),
                                               objDAL.MakeInParams("@Landmark2ID", SqlDbType.Int, 0,objIRegularClientView.Landmark2ID),
                                               objDAL.MakeInParams("@GSTName",SqlDbType.VarChar,100,objIRegularClientView.GSTName),
                                               objDAL.MakeInParams("@IsWithCompleteDetails", SqlDbType.Int, 0,objIRegularClientView.IsWithCompleteDetails)};


            objDAL.RunProc("[dbo].[EC_Mst_RegularClient_Save]", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            objIRegularClientView.ClientId = Convert.ToInt32(objSqlParam[2].Value);

            if (objMessage.messageID == 0)
            {

                string _Msg;
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                //string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;
                //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + LinkUrl + "&DecryptUrl='No'");
            }
            else
            {
                Common.DisplayErrors(objMessage.messageID);
            }


            return objMessage;
        }
    }
}
