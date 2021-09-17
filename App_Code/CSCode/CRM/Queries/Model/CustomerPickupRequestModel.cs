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
using Raj.CRM.QueryView;
//using Raj.eCargo.Init;
//using SendingSMS;
using System.Text;
/// <summary>
/// Summary description for Customer_PickupRequestModel
/// </summary>
/// 
namespace Raj.CRM.QueryModel
{
    public class CustomerPickupRequestModel:IModel
    {
        private ICustomerPickupRequestView objICustomerPickupRequestView;
        private DAL _objDAL = new DAL();
        private DataSet _objDS;

        public CustomerPickupRequestModel(ICustomerPickupRequestView CustomerPickupRequest)
        {
            objICustomerPickupRequestView = CustomerPickupRequest;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { _objDAL.MakeInParams("@PickUp_Id", SqlDbType.Int, 0, objICustomerPickupRequestView.keyID) };
            _objDAL.RunProc("dbo.EC_CRM_PickUP_Request_ReadValues", objSqlParam, ref _objDS);
            return _objDS;
        }

        public DataSet FillValues()
        {
            _objDAL.RunProc("dbo.EC_CRM_PickUp_Request_FillValues",ref _objDS);
            return _objDS;
        }

        public DataSet Fill_VA_On_Branch_Selection()
        {
            SqlParameter[] SqlPara ={
                _objDAL.MakeInParams("@Branch_Id",SqlDbType.Int,0,objICustomerPickupRequestView.ForwardBranchId)};

            _objDAL.RunProc("dbo.EC_CRM_Fill_VA_On_Branch_Selection", SqlPara, ref _objDS);
            return _objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {_objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            _objDAL.MakeOutParams("@Pickup_No_For_Print", SqlDbType.VarChar,20), 
            _objDAL.MakeInParams("@Pickup_ID", SqlDbType.Int, 0,objICustomerPickupRequestView.keyID),
            _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0,(int)UserManager.getUserParam().MainId),
            _objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,(int)UserManager.getUserParam().DivisionId),
            _objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,(int)Raj.EC.Common.GetMenuItemId()),
            _objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,(int)UserManager.getUserParam().YearCode),
            _objDAL.MakeInParams("@HierarchyCode", SqlDbType.VarChar, 5,(string)UserManager.getUserParam().HierarchyCode),
            _objDAL.MakeInParams("@Origin", SqlDbType.VarChar, 100,objICustomerPickupRequestView.Orgin),
            _objDAL.MakeInParams("@Destination", SqlDbType.VarChar, 100,objICustomerPickupRequestView.Destination),
            _objDAL.MakeInParams("@Pickup_Date", SqlDbType.DateTime, 0,objICustomerPickupRequestView.PickUpDate),
            _objDAL.MakeInParams("@PickUp_Time", SqlDbType.VarChar, 5,objICustomerPickupRequestView.PickUpTime),
            _objDAL.MakeInParams("@Weight", SqlDbType.SmallMoney, 5,objICustomerPickupRequestView.Weight),
            _objDAL.MakeInParams("@Packages", SqlDbType.Int, 0,objICustomerPickupRequestView.Packeges),
            _objDAL.MakeInParams("@Name_Of_Consignor", SqlDbType.VarChar, 100,objICustomerPickupRequestView.Consigner),
            _objDAL.MakeInParams("@Booking_Mode_Type_ID", SqlDbType.Int, 0,objICustomerPickupRequestView.BookingTypeModeId),
            _objDAL.MakeInParams("@Packing_Type_ID", SqlDbType.Int, 0,objICustomerPickupRequestView.PackingTypeId),
            _objDAL.MakeInParams("@Commodity_ID", SqlDbType.Int, 0,objICustomerPickupRequestView.CommodityId),
            _objDAL.MakeInParams("@Contact_Name", SqlDbType.VarChar, 100,objICustomerPickupRequestView.ContactName),
            _objDAL.MakeInParams("@Contact_Mobile_No", SqlDbType.VarChar, 15,objICustomerPickupRequestView.ContactMobile),
            _objDAL.MakeInParams("@Contact_Address", SqlDbType.VarChar, 300, objICustomerPickupRequestView.ContactAddress ),
            _objDAL.MakeInParams("@Contact_Telephone", SqlDbType.VarChar, 25, objICustomerPickupRequestView.ContactTelephone ),
            _objDAL.MakeInParams("@Contact_Email", SqlDbType.VarChar, 50,objICustomerPickupRequestView.ContactEmailId),
            _objDAL.MakeInParams("@City", SqlDbType.VarChar, 50,objICustomerPickupRequestView.ContactCity),
            _objDAL.MakeInParams("@State", SqlDbType.VarChar, 50,objICustomerPickupRequestView.ContactState),
            _objDAL.MakeInParams("@VA_ID", SqlDbType.Int, 0, objICustomerPickupRequestView.VendorId),
            _objDAL.MakeInParams("@Forward_Branch_ID", SqlDbType.Int, 0, objICustomerPickupRequestView.ForwardBranchId),
            _objDAL.MakeInParams("@GC_Docket_No", SqlDbType.Int, 0, objICustomerPickupRequestView.GC_Docket_No),
            _objDAL.MakeInParams("@GC_Id", SqlDbType.Int, 0, objICustomerPickupRequestView.GC_Id),
            _objDAL.MakeInParams("@Reason", SqlDbType.VarChar,250, objICustomerPickupRequestView.Reason),
            _objDAL.MakeInParams("@User_ID", SqlDbType.Int, 0,(int)UserManager.getUserParam().UserId)};

            _objDAL.RunProc("dbo.EC_CRM_PickUp_Request_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);
            string PickUp_No = Convert.ToString(objSqlParam[2].Value);

            objICustomerPickupRequestView.errorMessage = objMessage.message;
            if (objMessage.messageID == 0)
            {

                string _Msg;
                _Msg = "Saved SuccessFully";

                if (objICustomerPickupRequestView.keyID <= 0)
                {
                    MailSender ms = new MailSender(objICustomerPickupRequestView.ContactEmailId,PickUp_No ,3);
                    _Msg = "PickUp No " + PickUp_No + " Generated SuccessFully";
                }
                //SendSMS(PickUp_No);
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&RefreshParentPage=1");
            }

            return objMessage;
        }

        //private void SendSMS(string PickUp_No)
        //{
        //    StringBuilder SMS = new StringBuilder();
        //    SMS.Append("Pickup No:");
        //    SMS.Append(PickUp_No);
        //    SMS.Append("PickUp Point");
        //    SMS.Append(objICustomerPickupRequestView.Orgin);
        //    SMS.Append("Pick Up Date & Time ");
        //    SMS.Append(objICustomerPickupRequestView.PickUpDateAndTime);
        //    SMS.Append("Weight");
        //    SMS.Append(objICustomerPickupRequestView.Weight);
        //    SMS.Append("Pkgs");
        //    SMS.Append(objICustomerPickupRequestView.Packeges);
        //    SMS.Append("Packing Type");
        //    SMS.Append(objICustomerPickupRequestView.PackingType);
        //    SMS.Append("Commodity");
        //    SMS.Append(objICustomerPickupRequestView.Commodity);
        //    SMS.Append("Consignor");
        //    SMS.Append(objICustomerPickupRequestView.Consigner);
        //    SMS.Append("Contact Name");
        //    SMS.Append(objICustomerPickupRequestView.ContactName);
        //    SMS.Append("Mobile No.");
        //    SMS.Append(objICustomerPickupRequestView.ContactMobile);
        //    SMS.Append("Pick Up Address");
        //    SMS.Append(objICustomerPickupRequestView.ContactAddress);
        //    SMS.Append("Tel. No");
        //    SMS.Append(objICustomerPickupRequestView.ContactTelephone);

        //    SendSMS objsendsms = new SendSMS(objICustomerPickupRequestView.VAMobileNo, SMS.ToString());
        //}

        public string GetVaMobNo()
        {
            if (objICustomerPickupRequestView.VendorId > 0)
            {
                string _Branch_ID = "";
                string _Mobile_No = "";

                _objDS = _objDAL.RunQuery("select Mobile_No,Branch_ID from EC_Master_Associates where VA_ID=" + objICustomerPickupRequestView.VendorId);

                _Mobile_No = _objDS.Tables[0].Rows[0][0].ToString();
                _Branch_ID = _objDS.Tables[0].Rows[0][1].ToString();

                if (_Mobile_No == "")
                {
                    _objDS = _objDAL.RunQuery("select Phone_1 from EC_Master_Branch where Branch_ID=" + _Branch_ID);
                    _Mobile_No = _objDS.Tables[0].Rows[0][0].ToString();
                }

                return _Mobile_No;
            }
            return "";
        }
    }
}