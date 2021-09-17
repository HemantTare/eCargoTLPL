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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using ClassLibraryMVP.UI;
using ClassLibraryMVP;
using Raj.EC.FinanceView;
using Raj.EC;

/// <summary>
/// Summary description for WrongDeliveryModel
/// </summary>
/// 
namespace Raj.EC.FinanceModel
{
    public class WrongDeliveryModel : IModel
    {
        private DataSet _ds;
        private IWrongDeliveryView objIWrongDeliveryView;
        private DAL _objDAL = new DAL();
        private DataSet objDS;

        private int _userID = UserManager.getUserParam().UserId;
        private int _yearCode = UserManager.getUserParam().YearCode;
        private int _branchID = UserManager.getUserParam().MainId;

        public WrongDeliveryModel(IWrongDeliveryView WrongDeliveryView)
        {
            objIWrongDeliveryView = WrongDeliveryView;
        }

        public DataSet FillValues()
        { 
            _objDAL.RunProc("EC_Opr_WrongDly_FillValues", ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {
            _objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0),
            _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            _objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,_yearCode), 
            _objDAL.MakeInParams("@WrongDelivery_Id", SqlDbType.Int, 0,objIWrongDeliveryView.keyID),
            _objDAL.MakeInParams("@GC_ID", SqlDbType.Int,0,objIWrongDeliveryView.GC_ID),
            _objDAL.MakeInParams("@GCNo", SqlDbType.VarChar, 10,objIWrongDeliveryView.GCNo),
            _objDAL.MakeInParams("@Booking_Branch_ID",SqlDbType.Int,0,objIWrongDeliveryView.Booking_Branch_ID),
            _objDAL.MakeInParams("@Delivery_Branch_Id", SqlDbType.Int, 0,objIWrongDeliveryView.Delivery_Branch_Id),
            _objDAL.MakeInParams("@InformedBy", SqlDbType.VarChar, 100,objIWrongDeliveryView.InformedBy),
            _objDAL.MakeInParams("@InformedContactNo", SqlDbType.VarChar, 100,objIWrongDeliveryView.InformedContactNo),
            _objDAL.MakeInParams("@CollectedBy", SqlDbType.VarChar, 100,objIWrongDeliveryView.CollectedBy),
            _objDAL.MakeInParams("@CollectedContactNo", SqlDbType.VarChar, 100,objIWrongDeliveryView.CollectedContactNo),
            _objDAL.MakeInParams("@VehicleID",SqlDbType.Int,0,objIWrongDeliveryView.VehicleID),
            _objDAL.MakeInParams("@ParcelCondition",SqlDbType.Int,0,objIWrongDeliveryView.Received_Condition_ID), 
            _objDAL.MakeInParams("@IsCheque",SqlDbType.Bit,0,objIWrongDeliveryView.IsCheque), 
            _objDAL.MakeInParams("@ChequeNo",SqlDbType.Int,8,objIWrongDeliveryView.ChequeNo), 
            _objDAL.MakeInParams("@ChequeDate",SqlDbType.DateTime,0,objIWrongDeliveryView.ChequeDate), 
                _objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID)
            };

            _objDAL.RunProc("EC_Opr_WrongDelivery_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);


            if (objMessage.messageID == 0)
            {

                string _Msg;
                _Msg = "Saved SuccessFully";

                if (objIWrongDeliveryView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                 
            }

            return objMessage;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] sqlpara = { _objDAL.MakeInParams("@GC_No",SqlDbType.VarChar,20,objIWrongDeliveryView.GCNo),
                                    _objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,UserManager.getUserParam().YearCode),
                                    _objDAL.MakeInParams("@Main_Id",SqlDbType.Int,0,UserManager.getUserParam().MainId)};

            _objDAL.RunProc("EC_Opr_WrongDly_Get_GC_Details", sqlpara, ref _ds);

            return _ds;
        }
    }
}