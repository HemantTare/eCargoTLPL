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

using Raj.EC.MasterView;
/// <summary>
/// Summary description for ClientModel
/// </summary>
namespace Raj.EC.MasterModel
{
    public class ClientModel : IModel
    {
        private IClientView objClientView;
        private DAL objDAL = new DAL();
        DataSet objDS = null;
        private int _userID = UserManager.getUserParam().UserId;

        public ClientModel(IClientView ClientView)
        {
            objClientView = ClientView;
        }

        public DataSet ReadValues()
        {
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Client_ID", SqlDbType.Int, 0,objClientView.keyID),
            objDAL.MakeInParams("@Client_Code", SqlDbType.VarChar, 11,objClientView.ClientGeneralView.ClientCode),
            objDAL.MakeInParams("@Client_Name", SqlDbType.VarChar, 100,objClientView.ClientGeneralView.ClientName),
            objDAL.MakeInParams("@Client_Group_ID", SqlDbType.Int, 0,objClientView.ClientGeneralView.ClientGroupID),

            objDAL.MakeInParams("@DeliveryAreaID", SqlDbType.Int, 0,objClientView.ClientGeneralView.DeliveryAreaId),
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0,objClientView.ClientGeneralView.BranchId),
            objDAL.MakeInParams("@Contact_Person", SqlDbType.VarChar, 100,objClientView.ClientGeneralView.ContactPersonName),
            objDAL.MakeInParams("@Address1", SqlDbType.VarChar,100,objClientView.ClientGeneralView.AddressView.AddressLine1),
            objDAL.MakeInParams("@Address2", SqlDbType.VarChar, 100,objClientView.ClientGeneralView.AddressView.AddressLine2),
            objDAL.MakeInParams("@City_ID", SqlDbType.Int, 0,objClientView.ClientGeneralView.AddressView.CityId),
            objDAL.MakeInParams("@Pin_Code", SqlDbType.NVarChar, 30,objClientView.ClientGeneralView.AddressView.PinCode),
            objDAL.MakeInParams("@Std_Code", SqlDbType.NVarChar, 30,objClientView.ClientGeneralView.AddressView.StdCode),
            objDAL.MakeInParams("@Phone1", SqlDbType.NVarChar, 40,objClientView.ClientGeneralView.AddressView.Phone1),
            objDAL.MakeInParams("@Phone2", SqlDbType.NVarChar,40,objClientView.ClientGeneralView.AddressView.Phone2),
            objDAL.MakeInParams("@Mobile_No", SqlDbType.NVarChar,40,objClientView.ClientGeneralView.AddressView.MobileNo),
            objDAL.MakeInParams("@Fax", SqlDbType.NVarChar, 40,objClientView.ClientGeneralView.AddressView.FaxNo),
            objDAL.MakeInParams("@Email_ID", SqlDbType.VarChar, 100,objClientView.ClientGeneralView.AddressView.EmailId),
            objDAL.MakeInParams("@SMS_Alert", SqlDbType.Bit, 0,objClientView.ClientGeneralView.AddressView.SMSAlert),
            objDAL.MakeInParams("@eMail_Alert", SqlDbType.Bit, 0,objClientView.ClientGeneralView.AddressView.eMailAlert),
            objDAL.MakeInParams("@CST_TIN_No", SqlDbType.VarChar, 50,objClientView.ClientGeneralView.CSTTINNo),
            objDAL.MakeInParams("@Service_Tax_No",SqlDbType.VarChar,50,objClientView.ClientGeneralView.ServiceTaxNo),

            objDAL.MakeInParams("@Is_Existing_Ledger", SqlDbType.Bit, 0,objClientView.ClientFinanceView.Is_ExistingLedger),
            objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int, 0,objClientView.ClientFinanceView.LedgerID),
            objDAL.MakeInParams("@Credit_Days", SqlDbType.Int, 0,objClientView.ClientFinanceView.CreditDays),
            objDAL.MakeInParams("@Credit_Limit", SqlDbType.Decimal,0,objClientView.ClientFinanceView.CreditLimit),
            objDAL.MakeInParams("@Interest_Percent", SqlDbType.Decimal, 0,objClientView.ClientFinanceView.IntrestPercent),
            objDAL.MakeInParams("@Grace_Days", SqlDbType.Int, 0,objClientView.ClientFinanceView.GraceDays),
            objDAL.MakeInParams("@Is_Service_Tax_Applicable", SqlDbType.Bit, 0,objClientView.ClientFinanceView.IsServiceTaxPayByClient),
            objDAL.MakeInParams("@Registration_Date", SqlDbType.DateTime, 0,objClientView.ClientFinanceView.RegistrationDate),
            objDAL.MakeInParams("@Business_Hrs", SqlDbType.VarChar, 100,objClientView.ClientFinanceView.BusinessHours),
            objDAL.MakeInParams("@Is_User", SqlDbType.Bit,0,objClientView.ClientFinanceView.IseCargoUser),
            objDAL.MakeInParams("@Profile_Id", SqlDbType.Int,0,objClientView.ClientFinanceView.UserProfileId),
            objDAL.MakeInParams("@Is_Mechanical_Loading", SqlDbType.Bit, 0,objClientView.ClientFinanceView.Is_LoadingTypeId),
            objDAL.MakeInParams("@Marketing_Executive_ID", SqlDbType.Int, 0,objClientView.ClientFinanceView.MarketingExcutiveId),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID),
            objDAL.MakeInParams("@Is_To_Pay_Allowed", SqlDbType.Bit, 0,objClientView.ClientBillingView.Is_To_Pay),
            objDAL.MakeInParams("@Is_Paid_Allowed", SqlDbType.Bit, 0,objClientView.ClientBillingView.Is_Paid),
            objDAL.MakeInParams("@Is_To_Be_Billed_Allowed", SqlDbType.Bit, 0,objClientView.ClientBillingView.Is_To_Be_Billed),
            objDAL.MakeInParams("@Is_FOC_Allowed", SqlDbType.Bit, 0,objClientView.ClientBillingView.Is_FOC),

            objDAL.MakeInParams("@Billing_Cycle_ID", SqlDbType.Int, 0,objClientView.ClientBillingView.BillingCycle),
            objDAL.MakeInParams("@Billing_Day_ID", SqlDbType.Int, 0,objClientView.ClientBillingView.BillingDays),
            objDAL.MakeInParams("@Billing_Cycle_Day1", SqlDbType.Int, 0,objClientView.ClientBillingView.BillingCycleDay1),
            objDAL.MakeInParams("@Billing_Cycle_Day2", SqlDbType.Int, 0,objClientView.ClientBillingView.BillingCycleDay2),

            objDAL.MakeInParams("@Regular_Client_Id",SqlDbType.Int,0,objClientView.ClientGeneralView.Regular_Client_Id ),
            objDAL.MakeInParams("@BillingDetailsXML",SqlDbType.Xml,0,objClientView.ClientBillingView.BillingDetailsXML),
            objDAL.MakeInParams("@MinimumBalance", SqlDbType.Decimal, 0,objClientView.ClientFinanceView.MinimumBalance),
            objDAL.MakeInParams("@ClientCategoryID",SqlDbType.Int,0,objClientView.ClientGeneralView.ClientCategoryID ),
            objDAL.MakeInParams("@Delivery_Type_Id",SqlDbType.Int,0,objClientView.ClientGeneralView.DeliveryTypeID),
            objDAL.MakeInParams("@Is_Casual_Taxable", SqlDbType.Int, 0,objClientView.ClientGeneralView.Is_Casual_Taxable),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar,1000,objClientView.ClientGeneralView.Remarks),
            objDAL.MakeInParams("@Landmark1ID", SqlDbType.Int, 0,objClientView.ClientGeneralView.Landmark1ID),
            objDAL.MakeInParams("@Landmark2ID", SqlDbType.Int, 0,objClientView.ClientGeneralView.Landmark2ID),
            objDAL.MakeInParams("@Is_CreditParty", SqlDbType.Bit, 0,objClientView.ClientFinanceView.Is_CreditParty),
            objDAL.MakeInParams("@Is_OutwardBilling", SqlDbType.Int, 0,objClientView.ClientGeneralView.Is_OutwardBilling),
            objDAL.MakeInParams("@Is_InwardBilling", SqlDbType.Int, 0,objClientView.ClientGeneralView.Is_InwardBilling),
            objDAL.MakeInParams("@IsPrintFrtOnLR", SqlDbType.Bit, 0,objClientView.ClientFinanceView.IsPrintFrtOnLR)

            };

            objDAL.RunProc("dbo.EC_Master_Client_Save", objSqlParam);


            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                objClientView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";

                if (objClientView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Master/Sales/FrmClient.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objClientView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
            }

            return objMessage;
        }

    }
}
