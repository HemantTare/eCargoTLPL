using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web;
using System.Web.Security;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationView;
/// <summary>
/// Summary description for ReserveGCModel
/// </summary>
namespace Raj.EC.OperationModel
{
    public class ReserveGCModel : IModel
    {
        private IReserveGCView objIReserveGCView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _ConsignorId;
        private Boolean _IsConsRegularClient;

        private int _userID = UserManager.getUserParam().UserId;
        private int _branchID = UserManager.getUserParam().MainId;
        private int _yearCode = UserManager.getUserParam().YearCode;
        private int _DivisionId = UserManager.getUserParam().DivisionId;

        public ReserveGCModel(IReserveGCView ReserveGCView)
        {
            objIReserveGCView = ReserveGCView;
        }

        public DataSet FillValues()
        {
            //@Login_Branch_ID
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Login_Branch_ID", SqlDbType.Int, 0, _branchID) };
            objDAL.RunProc("dbo.EC_Opr_Reserve_GC_FillValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            return objDS;
        }


        public DataSet Get_Company_GC_Parameter()
        {
            objDAL.RunProc("Get_Company_GC_Parameter", ref objDS);
            return objDS;
        }


        public void SetValues()
        {
            char[] char1 = new char[1];
            char1[0] = '*';
            string[] value = new string[100];
            value = objIReserveGCView.ConsignorId.Split(char1);

            _ConsignorId = Util.String2Int(value[0]);

            if (_ConsignorId == -1)
            {
                _IsConsRegularClient = false;
                _ConsignorId = 0;
            }
            else
            {
                if (Util.String2Int(value[1]) == 0)
                {
                    _IsConsRegularClient = false;
                }
                else
                {
                    _IsConsRegularClient = true;
                }
            }
        }

        public Message Save()
        {
            Message objMessage = new Message();
            SetValues();
         
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeOutParams("@Existing_GC_Number", SqlDbType.Int, 0),
            objDAL.MakeInParams("@Document_Series_Allocation_ID", SqlDbType.Int, 0,objIReserveGCView.DocumentSeriesAllocationId),
            objDAL.MakeInParams("@GC_No_From", SqlDbType.Int, 0,objIReserveGCView.GCNoFrom),
            objDAL.MakeInParams("@GC_No_To", SqlDbType.Int, 0,objIReserveGCView.GCNoTo),
            //objDAL.MakeInParams("@GC_No_To_For_Print", SqlDbType.VarChar , 10,objIReserveGCView.GCNoToForPrint),
            objDAL.MakeInParams("@Start_GC_No", SqlDbType.Int , 10,objIReserveGCView.StartGCNo),
            objDAL.MakeInParams("@End_GC_No", SqlDbType.Int , 10,objIReserveGCView.EndGCNo),
            objDAL.MakeInParams("@Reason_ID", SqlDbType.Int , 10,objIReserveGCView.ReservedReasonId),
            objDAL.MakeInParams("@VA_Id",SqlDbType.Int ,0,objIReserveGCView.VAId),
            objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0,_branchID),
            objDAL.MakeInParams("@Document_ID", SqlDbType.Int, 0,objIReserveGCView.GCTypeId),
            objDAL.MakeInParams("@Consignor_ID", SqlDbType.Int , 0,_ConsignorId),
            objDAL.MakeInParams("@Consignor_Name",SqlDbType.VarChar,100,objIReserveGCView.ConsignorName ),
            objDAL.MakeInParams("@Is_Consignor_Regular_Client", SqlDbType.Bit, 0,_IsConsRegularClient),
            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,_yearCode),
            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,_DivisionId),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0,_userID)};

            objDAL.RunProc("dbo.EC_Opr_Reserve_GC_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);
            int Existing_GC_No = Convert.ToInt32(objSqlParam[2].Value);

            if (Existing_GC_No == 0)
            {
                if (objMessage.messageID == 0)
                {
                    string _Msg;
                    _Msg = "Saved SuccessFully";

                    //objIReserveGCView.errorMessage = "Saved SuccessFully";

                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Booking/FrmReserveGC.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));

                }
            }
            // Added by Anita On:14/01/08
            //**********************************
            else
            {
                objIReserveGCView.errorMessage = "GC No " + Existing_GC_No.ToString() + " Already Exists.";
            }
            //***********************************
            return objMessage;
            
        }
    }
}