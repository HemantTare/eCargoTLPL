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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.MasterView;

/// <summary>
/// Summary description for StandardBookingRateModel
/// </summary>
namespace Raj.EC.MasterModel
{
    public class StandardBookingRateModel : IModel
    {
        private IStandardBookingRateView objIStandardBookingRateView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        public StandardBookingRateModel(IStandardBookingRateView StandardBookingRateView)
        {
            objIStandardBookingRateView = StandardBookingRateView;
        }

        public DataSet Fill_Crossing_Rate(int FromBranch_ID,int ToBranch_Id)
        {
            SqlParameter[] objSqlParam ={ 
                objDAL.MakeInParams("@FromBranch_ID", SqlDbType.Int, 0, FromBranch_ID),
             objDAL.MakeInParams("@ToBranch_Id", SqlDbType.Int, 0, ToBranch_Id)};

            objDAL.RunProc("dbo.EC_Master_Standard_booking_Rate_Fill_CrossingRate",objSqlParam, ref objDS);
            return objDS;
        }
       
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Standard_Rates_ID", SqlDbType.Int, 0, objIStandardBookingRateView.keyID) };
            objDAL.RunProc("dbo.EC_Master_Standard_booking_Rate_ReadValues", objSqlParam, ref objDS);
            Raj.EC.Common.SetPrimaryKeys(new string[] { "To_Branch_ID" }, objDS.Tables[0]); 

            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                   objDAL.MakeOutParams("@ERROR_DESC", SqlDbType.VarChar, 4000),                                    
                                   objDAL.MakeInParams("@Standard_Booking_Rates_ID", SqlDbType.Int, 0,objIStandardBookingRateView.keyID),
                                   objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,UserManager.getUserParam().DivisionId),   
                                   objDAL.MakeInParams("@Applicable_From", SqlDbType.DateTime, 0, objIStandardBookingRateView.ApplicableFromDate),
                                   objDAL.MakeInParams("@From_Branch_ID", SqlDbType.Int, 0, objIStandardBookingRateView.BookingBranchId),
                                   objDAL.MakeInParams("@To_Branch_ID", SqlDbType.Int, 0, objIStandardBookingRateView.DeliveryBranchId), 
                                   objDAL.MakeInParams("@Profit_Ratio_Percent", SqlDbType.Decimal, 0,objIStandardBookingRateView.ProfitRatio),
                                   objDAL.MakeInParams("@Standard_Booking_Rate", SqlDbType.Decimal, 0,objIStandardBookingRateView.BookingRate), 
                                   objDAL.MakeInParams("@StandardRateDetailsXML", SqlDbType.Xml, 0,objIStandardBookingRateView.CrossingPointXML),
                                   objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, UserManager.getUserParam().UserId)};


            objDAL.RunProc("EC_Master_Standard_booking_Rate_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                objIStandardBookingRateView.ClearVariables();
            }

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";
                string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + LinkUrl + "&DecryptUrl='No'");
            }

            return objMessage;
        }
    }
}