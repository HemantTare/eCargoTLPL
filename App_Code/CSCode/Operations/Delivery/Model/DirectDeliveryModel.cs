
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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;


using Raj.EC.OperationView;
using Raj.EC.OperationModel;

using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;



/// <summary>
/// Summary description for DirectDeliveryModel
/// </summary>


namespace Raj.EC.OperationModel
{
    public class DirectDeliveryModel : IModel
    {

        private IDirectDeliveryView objIDirectDeliveryView;
        private DataSet _ds;
        private DAL _objDAL = new DAL();
        private int _userId = Convert.ToInt32(UserManager.getUserParam().UserId);


        public DirectDeliveryModel(IDirectDeliveryView DirectDeliveryView)
        {
            objIDirectDeliveryView = DirectDeliveryView;
        }

        public DataSet ReadValues()
        {

            SqlParameter[] objSqlParam ={ _objDAL.MakeInParams("@DDC_ID", SqlDbType.Int, 0, objIDirectDeliveryView.keyID) };


            _objDAL.RunProc("EC_Opr_Direct_Delivery_ReadValues", objSqlParam, ref _ds);

            return _ds;


        }
        public void Get_VehicleDetails()
        {
            SqlParameter[] objSqlParam ={  
                                    _objDAL.MakeOutParams("@Vehicle_Category_ID", SqlDbType.VarChar , 20 ),
                                    _objDAL.MakeOutParams("@Vehicle_Category", SqlDbType.VarChar , 20 ),    
                                     _objDAL.MakeOutParams("@NoofMinuteDifferenceForLate", SqlDbType.Int , 0 ),   
                                    _objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objIDirectDeliveryView.VehicleSearchView.VehicleID  )};


            _objDAL.RunProc("EC_Opr_AUS_Get_Vehicle_Details", objSqlParam, ref _ds);

            objIDirectDeliveryView.Vehicle_Category_Id = Util.String2Int(objSqlParam[0].Value.ToString());
            objIDirectDeliveryView.Vehicle_Category = objSqlParam[1].Value.ToString();
            objIDirectDeliveryView.NoofMinuteDifferenceForLate = Util.String2Int(objSqlParam[2].Value.ToString());

        }

      
        public DataSet Get_LHPO()
        {
            SqlParameter[] objSqlParam ={
                                    _objDAL.MakeInParams("@GC_ID", SqlDbType.Int  , 0, objIDirectDeliveryView.GC_Id  ),
                                    _objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objIDirectDeliveryView.VehicleSearchView.VehicleID  )};


            _objDAL.RunProc("EC_Opr_Direct_Delivery_Get_LHPO", objSqlParam, ref _ds);

            return _ds;

        }

        //[AjaxPro.AjaxMethod]
        //public static DataTable GetLedger(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        //{
        //    DAL _objDAL = new DAL();
        //    DataSet _ds = null;

        //    SearchFor = SearchFor + "%";
        //    SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
        //    _objDAL.RunProc("EC_FA_Opr_Ledger_Search", sqlPara, ref _ds);

        //    DataTable dt = _ds.Tables[0];
        //    return dt;
        //}
        //[AjaxPro.AjaxMethod]
        //public static DataTable GetCreditToBranch(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        //{
        //    DAL _objDAL = new DAL();
        //    DataSet _ds = null;
        //    int _mainID = UserManager.getUserParam().MainId;

        //    SearchFor = SearchFor + "%";
        //    SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
        //      _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0, _mainID)};
        //    _objDAL.RunProc("EC_Opr_MemoToBranch_Search", sqlPara, ref _ds);

        //    DataTable dt = _ds.Tables[0];
        //    return dt;
        //}


        public DataSet Get_LHPO_Details()
        {
            SqlParameter[] objSqlParam ={
                                    _objDAL.MakeOutParams("@Schedule_Arrival_Delivery_Date", SqlDbType.VarChar  , 20 ) ,
                                    _objDAL.MakeOutParams("@Schedule_Arrival_Delivery_Time", SqlDbType.VarChar  , 20 ) ,
                                    _objDAL.MakeInParams("@GC_ID", SqlDbType.Int  , 0, objIDirectDeliveryView.GC_Id  ),                                    
                                    _objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objIDirectDeliveryView.VehicleSearchView.VehicleID  ),
                                    _objDAL.MakeInParams("@LHPO_Id", SqlDbType.Int, 0, objIDirectDeliveryView.LHPO_Id   ),
                                    _objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, UserManager.getUserParam().MainId  )};


            _objDAL.RunProc("EC_Opr_Direct_Delivery_Get_LHPO_Details", objSqlParam, ref _ds);


            objIDirectDeliveryView.ScheduledArivalDate = objSqlParam[0].Value.ToString();
            objIDirectDeliveryView.ScheduledArivalTime = objSqlParam[1].Value.ToString();


            return _ds;

        }

        public DataSet Get_GC_Details()
        {
            SqlParameter[] objSqlParam ={
                                    _objDAL.MakeInParams("@GC_ID", SqlDbType.Int  , 0, objIDirectDeliveryView.GC_Id   )};

            _objDAL.RunProc("EC_Opr_Direct_Delivery_Get_GC_Details", objSqlParam, ref _ds);


            return _ds;

        }



        public Message Save()
        {
            Message objMessage = new Message();


            SqlParameter[] objSqlParam = {_objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                _objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,UserManager.getUserParam().DivisionId ),
                _objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0, UserManager.getUserParam().YearCode ),
                _objDAL.MakeInParams("@DDC_Branch_ID",SqlDbType.Int,0, UserManager.getUserParam().MainId  ),
                _objDAL.MakeInParams("@DDC_ID",SqlDbType.Int,0,objIDirectDeliveryView.keyID ),
                _objDAL.MakeInParams("@DDC_No",SqlDbType.Int,0,0 ),
                _objDAL.MakeInParams("@DDC_No_For_Print",SqlDbType.VarChar,20,objIDirectDeliveryView.DDC_No_For_Print ),
                _objDAL.MakeInParams("@DDC_Date",SqlDbType.DateTime,0,objIDirectDeliveryView.DDC_Date  ),
                _objDAL.MakeInParams("@DDC_Type_ID",SqlDbType.Int,0, 0),//objIDirectDeliveryView.DDC_Type_ID),              
                _objDAL.MakeInParams("@GC_ID",SqlDbType.Int,0, objIDirectDeliveryView.GC_Id ),
                _objDAL.MakeInParams("@Article_ID",SqlDbType.Int,0,objIDirectDeliveryView.Previous_Article_ID ),
                _objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objIDirectDeliveryView.VehicleSearchView.VehicleID  ),
                _objDAL.MakeInParams("@LHPO_ID",SqlDbType.Int,0, objIDirectDeliveryView.LHPO_Id ),
                _objDAL.MakeInParams("@Memo_ID",SqlDbType.Int,0,objIDirectDeliveryView.Memo_Id  ),
                _objDAL.MakeInParams("@Total_DDC_Articles",SqlDbType.Int,0,objIDirectDeliveryView.Loaded_Articles   ),
                _objDAL.MakeInParams("@Total_DDC_Actual_Wt",SqlDbType.Decimal,0,objIDirectDeliveryView.Loaded_Articles_Weight  ),
                _objDAL.MakeInParams("@Reason_ID",SqlDbType.Int,0,objIDirectDeliveryView.Reason_For_Late_Uploading  ),
                _objDAL.MakeInParams("@Balance_Articles",SqlDbType.Int,0, objIDirectDeliveryView.Loaded_Articles  ),
                _objDAL.MakeInParams("@Balance_Articles_Wt",SqlDbType.Int,0, objIDirectDeliveryView.Loaded_Articles_Weight  ),
                _objDAL.MakeInParams("@Delivered_Articles",SqlDbType.Int,0, objIDirectDeliveryView.Delivered_Articles  ),
                _objDAL.MakeInParams("@Delivered_Actual_Wt",SqlDbType.Decimal,0,objIDirectDeliveryView.Delivered_Articles_Weight  ),
                _objDAL.MakeInParams("@Damaged_Articles",SqlDbType.Int,0,objIDirectDeliveryView.Damage_Leakage_Articles  ),
                _objDAL.MakeInParams("@Damaged_Value",SqlDbType.Decimal,0,objIDirectDeliveryView.Damage_Leakage_Articles_Value  ),
                _objDAL.MakeInParams("@Received_Condintion_ID",SqlDbType.Int,0,objIDirectDeliveryView.Delivery_Condition  ),
                _objDAL.MakeInParams("@Delivery_Time",SqlDbType.VarChar  ,0,objIDirectDeliveryView.Delivery_Time  ),
                _objDAL.MakeInParams("@Delivery_Date",SqlDbType.DateTime,0,objIDirectDeliveryView.ActualDeliveryDate     ),
                _objDAL.MakeInParams("@Delivery_Taken_By",SqlDbType.VarChar,0,objIDirectDeliveryView.Delivery_Taken_By  ),
                _objDAL.MakeInParams("@Delivery_Status_ID",SqlDbType.Int,0,200),//objIDirectDeliveryView.Delivery_Status_Id  ),
                _objDAL.MakeInParams("@Is_POD_Received",SqlDbType.Bit,0,objIDirectDeliveryView.Is_PODReceived  ),
                _objDAL.MakeInParams("@Previous_Article_ID",SqlDbType.Int,0,objIDirectDeliveryView.Previous_Article_ID),
                _objDAL.MakeInParams("@Previous_Status_ID",SqlDbType.Int,0,objIDirectDeliveryView.Previous_Status_ID  ),
                _objDAL.MakeInParams("@Previous_Document_ID",SqlDbType.Int,0,objIDirectDeliveryView.Previous_Document_ID ),
                _objDAL.MakeInParams("@Previous_Document_No_For_Print",SqlDbType.VarChar   ,0,objIDirectDeliveryView.Previous_Document_No_For_Print  ),
                _objDAL.MakeInParams("@Previous_Document_Date",SqlDbType.DateTime  ,0,objIDirectDeliveryView.Previous_Document_Date),                             
                _objDAL.MakeInParams("@Remarks",SqlDbType.VarChar,0, objIDirectDeliveryView.Remarks  ),                
                _objDAL.MakeInParams("@Created_By",SqlDbType.Int,0, UserManager.getUserParam().UserId  ),
                _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, UserManager.getUserParam().HierarchyCode),                        
                _objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0, Common.GetMenuItemId ()),
                _objDAL.MakeInParams("@Delivery_To_ID",SqlDbType.Int,0,objIDirectDeliveryView.DeliveryToID ),
                _objDAL.MakeInParams("@Cne_Copy_Status_ID",SqlDbType.Int,0,objIDirectDeliveryView.ConsigneeCopyID),
                _objDAL.MakeInParams("@Delivery_Against_ID",SqlDbType.Int,0,objIDirectDeliveryView.DeliveryAgainstID),
                _objDAL.MakeInParams("@IsFreightReceived",SqlDbType.Bit,0,objIDirectDeliveryView.IsFreightReceived  ),

                _objDAL.MakeInParams("@ReceivedBy",SqlDbType.Int,0,objIDirectDeliveryView.ReceivedBy),
                _objDAL.MakeInParams("@Debit_To_Ledger_ID",SqlDbType.Int,0,objIDirectDeliveryView.Debit_To_Ledger_ID),
                _objDAL.MakeInParams("@billing_branch_id",SqlDbType.Int,0,objIDirectDeliveryView.Debit_To_Branch_ID),
                _objDAL.MakeInParams("@Cash_Amount",SqlDbType.Decimal,0,objIDirectDeliveryView.MRCashChequeDetailsView.CashAmount),
                _objDAL.MakeInParams("@Cash_Ledger_ID",SqlDbType.Int,0,objIDirectDeliveryView.MRCashChequeDetailsView.CashLedgerID),
                _objDAL.MakeInParams("@Cheque_Amount",SqlDbType.Decimal,0,objIDirectDeliveryView.MRCashChequeDetailsView.ChequeAmount), 
                _objDAL.MakeInParams("@TDS",SqlDbType.Decimal,0,objIDirectDeliveryView.TDS),

                _objDAL.MakeInParams("@MRChequeDetailsXML",SqlDbType.Xml,0,objIDirectDeliveryView.MRCashChequeDetailsView.MRChequeDetailsXML)
                       
                };


            _objDAL.RunProc("EC_Opr_Direct_Delivery_Save", objSqlParam);


            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";

                if (objIDirectDeliveryView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Delivery/FrmDirectDelivery.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objIDirectDeliveryView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
            }
            return objMessage;
        }

        public DataSet Fill_Values()
        {

            _objDAL.RunProc("EC_Opr_Direct_Delivery_FillValues", ref _ds);

            return _ds;
        }

    }
}




