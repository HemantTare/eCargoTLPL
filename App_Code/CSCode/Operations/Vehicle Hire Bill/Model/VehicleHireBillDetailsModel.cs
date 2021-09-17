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

using Raj.EC.OperationView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;


/// <summary>
/// Summary description for VehicleHireBillDetailsModel
/// </summary>
namespace Raj.EC.OperationPresenter
{

    public class VehicleHireBillDetailsModel : IModel
    {
        private IVehicleHireBillDetailsView objIVehicleHireBillDetailsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;
        private int _yearCODE = UserManager.getUserParam().YearCode;
        private int _divisionID = UserManager.getUserParam().DivisionId;
        private int _menuitemID = Raj.EC.Common.GetMenuItemId();
        private int _branchID = UserManager.getUserParam().MainId;
        private string _hierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _keyID;

        public VehicleHireBillDetailsModel(IVehicleHireBillDetailsView vehicleHireBillDetailsView)
        {
            objIVehicleHireBillDetailsView = vehicleHireBillDetailsView;          

        }
        

        public DataSet ReadValues()
        {

            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@HireBillID", SqlDbType.Int, 0, objIVehicleHireBillDetailsView.keyID)
                                           };
            objDAL.RunProc("[dbo].[EC_Opr_VehicleHireBillDetails_ReadValues]", objSqlParam, ref objDS);
            return objDS;

        }
        public DataSet FillValues()
        {
            objDAL.RunProc("EC_Opr_VehicleHireBillDetails_FillValues", ref objDS);
            Raj.EC.Common.SetTableName(new string[] {"BrokerName", "FreightType" }, objDS);
            return objDS;

        }

        public DataSet GetVehicleInformationOnVehicleChanged()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, objIVehicleHireBillDetailsView.VehicleID)
                                        
                                           };
            objDAL.RunProc("[dbo].[EC_Opr_VehicleHireBillDetails_GetValuesOnVehicleChanged]", objSqlParam, ref objDS);
            return objDS;

        }
        public DataSet GetTDSercent()
        {
            SqlParameter[] objSqlParam = {  
                                            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,_divisionID),
                                            objDAL.MakeInParams("@Vendor_Id", SqlDbType.Int, 0, objIVehicleHireBillDetailsView.BrokerID),
                                            objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,objIVehicleHireBillDetailsView.VehicleID),                           
                                            objDAL.MakeInParams("@VehicleHireDate",SqlDbType.DateTime,0,objIVehicleHireBillDetailsView.VehicleHireBillDate),
                                            objDAL.MakeInParams("@Amount",SqlDbType.Decimal,0,0),
                                            objDAL.MakeOutParams("@TDSAmount",SqlDbType.Decimal,0)                                         
                                                                                    
                                            
                                           };
            objDAL.RunProc("[dbo].[EC_Opr_VehicleHireBillDetails_CalcTds]", objSqlParam, ref objDS);
            return objDS;

        }
        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                            objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,_divisionID),
                                            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,_yearCODE),
                                            objDAL.MakeInParams("@MenuItemId",SqlDbType.Int,0,_menuitemID),
                                            objDAL.MakeInParams("@HierarchyCode",SqlDbType.VarChar,2,_hierarchyCode),
                                            objDAL.MakeInParams("@DocumentSeriesAllocationId",SqlDbType.Int,0,objIVehicleHireBillDetailsView.Document_Series_Allocation_ID),
                                            objDAL.MakeInParams("@Hire_Bill_Branch_ID",SqlDbType.Int,0,_branchID),
                                            objDAL.MakeInParams("@HireBillId",SqlDbType.Int,0, objIVehicleHireBillDetailsView.keyID),
                                            objDAL.MakeInParams("@HireBillDate",SqlDbType.DateTime,0,objIVehicleHireBillDetailsView.VehicleHireBillDate),
                                            objDAL.MakeInParams("@HireBillNo",SqlDbType.Int,0,objIVehicleHireBillDetailsView.Next_No),
                                            objDAL.MakeInParams("@HireBillNoForPrint",SqlDbType.VarChar,20,objIVehicleHireBillDetailsView.VehicleHireBillNo),
                                            objDAL.MakeInParams("@RefNo",SqlDbType.VarChar,20,objIVehicleHireBillDetailsView.RefNo),
                                            objDAL.MakeInParams("@VehicleID",SqlDbType.Int,0,objIVehicleHireBillDetailsView.VehicleID),
                                            objDAL.MakeInParams("@FromLocationID",SqlDbType.Int,0,objIVehicleHireBillDetailsView.FromLocationID),
                                            objDAL.MakeInParams("@ToLocationID",SqlDbType.Int,0,objIVehicleHireBillDetailsView.ToLocationID),
                                            objDAL.MakeInParams("@BrokerID",SqlDbType.Int,0,objIVehicleHireBillDetailsView.BrokerID),                                                                       
                                            objDAL.MakeInParams("@Driver1Id",SqlDbType.Int,0,objIVehicleHireBillDetailsView.Driver1ID),
                                            objDAL.MakeInParams("@Driver2Id",SqlDbType.Int,0,objIVehicleHireBillDetailsView.Driver2ID),
                                            objDAL.MakeInParams("@CleanerID",SqlDbType.Int,0,objIVehicleHireBillDetailsView.CleanerID),
                                            objDAL.MakeInParams("@LHPOFreightBasisID",SqlDbType.Int,0,objIVehicleHireBillDetailsView.FreightTypeID),
                                            objDAL.MakeInParams("@MinWtGuarantee",SqlDbType.Decimal,0,objIVehicleHireBillDetailsView.WtGuarantee),
                                            objDAL.MakeInParams("@Rate",SqlDbType.Decimal,0,objIVehicleHireBillDetailsView.RateKg),
                                            objDAL.MakeInParams("@ActualKms",SqlDbType.Decimal,0,objIVehicleHireBillDetailsView.ActualKms),
                                            objDAL.MakeInParams("@WtKmsArticlesPayable",SqlDbType.Decimal,0,objIVehicleHireBillDetailsView.ActualWtPayableValue),
                                            objDAL.MakeInParams("@TruckHireCharge",SqlDbType.Decimal,0,objIVehicleHireBillDetailsView.TruckHireCharge),
                                            objDAL.MakeInParams("@TDSPercent",SqlDbType.Decimal,0,objIVehicleHireBillDetailsView.TDSPercentage),
                                            objDAL.MakeInParams("@TDSAmount",SqlDbType.Decimal,0,objIVehicleHireBillDetailsView.TDSAmount),
                                            objDAL.MakeInParams("@TotalTruckHirePayable",SqlDbType.Decimal,0,objIVehicleHireBillDetailsView.TotalTruckHireCharge),
                                            objDAL.MakeInParams("@AdvanceReceived",SqlDbType.Decimal,0,objIVehicleHireBillDetailsView.AdvanceReceived),
                                            objDAL.MakeInParams("@BrokeragePayable",SqlDbType.Decimal,0,objIVehicleHireBillDetailsView.BrokeragePayable),
                                            objDAL.MakeInParams("@CollectionCharges",SqlDbType.Decimal,0,objIVehicleHireBillDetailsView.CollectionChargePayable),
                                            objDAL.MakeInParams("@Vehicle_Departure_Time",SqlDbType.VarChar,0,objIVehicleHireBillDetailsView.VehicleDepartureTime),
                                            objDAL.MakeInParams("@Transit_Days",SqlDbType.Int,0,objIVehicleHireBillDetailsView.TransitDays),
                                            objDAL.MakeInParams("@Commited_Delivery_Date",SqlDbType.DateTime,0,objIVehicleHireBillDetailsView.CommittedDelDate),
                                            objDAL.MakeInParams("@Remark",SqlDbType.NVarChar,0,objIVehicleHireBillDetailsView.Remark),                            
                                            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,_userID),
                                            };


            objDAL.RunProc("[dbo].[EC_Opr_VehicleHireBillDetails_Save]", objSqlParam);


            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);


            if (objMessage.messageID == 0)
            {
               
                string _Msg;
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));

            }

            return objMessage;
        }
       
    }
}
