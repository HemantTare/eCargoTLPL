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
using Raj.EC.OperationView;

/// <summary>
/// Summary description for Trip_Settlement_2_Model
/// </summary>
namespace Raj.EC.OperationModel
{
    public class Trip_Settlement_2_Model : IModel
    {
        private ITrip_Settlement_2_View objITrip_Settlement_2_View;
        private DataSet objDS;
        private DAL objDAL = new DAL();

        public Trip_Settlement_2_Model(ITrip_Settlement_2_View ITrip_Settlement_2_View)
        {
            objITrip_Settlement_2_View = ITrip_Settlement_2_View;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_Trip_ID",SqlDbType.Int,0, objITrip_Settlement_2_View.keyID)
                                           };
            objDAL.RunProc("EF_Trn_Vehicle_Trip_2_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("EF_Trn_Vehicle_Trip_2_FillValues", ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0, UserManager.getUserParam().YearCode  ),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2, UserManager.getUserParam().HierarchyCode),                        
            objDAL.MakeInParams("@Main_ID",SqlDbType.Int,0, UserManager.getUserParam().MainId  ),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,UserManager.getUserParam().DivisionId),
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
            objDAL.MakeInParams("@Vehicle_Trip_ID",SqlDbType.Int,0,objITrip_Settlement_2_View.keyID),
            objDAL.MakeInParams("@Vehicle_Trip_Date",SqlDbType.DateTime,0,objITrip_Settlement_2_View.Vehicle_Trip_Date   ),
            objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0, objITrip_Settlement_2_View.Vehicle_ID),
            objDAL.MakeInParams("@Trip_Start_Date",SqlDbType.DateTime,0,objITrip_Settlement_2_View.Trip_Start_Date),
            objDAL.MakeInParams("@Trip_End_Date",SqlDbType.DateTime,0,objITrip_Settlement_2_View.Trip_End_Date),
            objDAL.MakeInParams("@Driver_ID",SqlDbType.Int,0,objITrip_Settlement_2_View.Driver_ID  ),
            objDAL.MakeInParams("@Total_KM_Run",SqlDbType.Int,0,objITrip_Settlement_2_View.Total_KM_Run ),
            objDAL.MakeInParams("@Total_Actual_Wt",SqlDbType.Decimal,0,objITrip_Settlement_2_View.Total_Actual_Wt ),
            objDAL.MakeInParams("@Total_Hire_Amount",SqlDbType.Decimal,0,objITrip_Settlement_2_View.Total_Hire_Amount ),
            objDAL.MakeInParams("@Total_Advance",SqlDbType.Decimal,0,objITrip_Settlement_2_View.Total_Advance ),
            objDAL.MakeInParams("@Total_Fuel_Qty",SqlDbType.Decimal,0,objITrip_Settlement_2_View.Total_Fuel_Qty ),
            objDAL.MakeInParams("@Total_Fuel_Amount",SqlDbType.Decimal,0,objITrip_Settlement_2_View.Total_Fuel_Amount ),
            objDAL.MakeInParams("@Total_Oil_Amount",SqlDbType.Decimal,0,objITrip_Settlement_2_View.Total_Oil_Amount ),
            objDAL.MakeInParams("@Total_Fuel_Oil_Cost",SqlDbType.Decimal,0,objITrip_Settlement_2_View.Total_Fuel_Oil_Cost ),
            objDAL.MakeInParams("@Total_Trip_Expense",SqlDbType.Decimal,0,objITrip_Settlement_2_View.Total_Trip_Expense ),
            objDAL.MakeInParams("@Total_Trip_Cost",SqlDbType.Decimal,0,objITrip_Settlement_2_View.Total_Trip_Cost ),
            objDAL.MakeInParams("@Driver_Closing_Balance",SqlDbType.Decimal,0,objITrip_Settlement_2_View.Driver_Closing_Balance ),
            objDAL.MakeInParams("@Remarks",SqlDbType.VarChar,500, objITrip_Settlement_2_View.Remarks ),
            objDAL.MakeInParams("@TripDetails",SqlDbType.Xml,0, objITrip_Settlement_2_View.TripHireChallansDetailsXML  ),
            objDAL.MakeInParams("@FuelDetails",SqlDbType.Xml,0, objITrip_Settlement_2_View.TripFuelDetailsXML ),
            objDAL.MakeInParams("@ExpenseDetails",SqlDbType.Xml,0, objITrip_Settlement_2_View.TripExpenseDetailsXML  ),
            objDAL.MakeInParams("@User_ID",SqlDbType.Int,0,UserManager.getUserParam().UserId )
            };

            objDAL.RunProc("EF_Trn_Vehicle_Trip_2_Save", objSqlParam);


            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
            //else
            //{
            //    Common.DisplayErrors(objMessage.messageID);
            //}
            return objMessage;
        }

        public decimal GetDriverOpeningBalance()
        {
            SqlParameter[] objSqlParam ={
                objDAL.MakeInParams("@Driver_ID", SqlDbType.Int, 0, objITrip_Settlement_2_View.Driver_ID),
                objDAL.MakeInParams("@Vehicle_Trip_Id", SqlDbType.Int, 0, objITrip_Settlement_2_View.keyID),
                objDAL.MakeInParams("@Vehicle_Trip_Date", SqlDbType.DateTime, 0, objITrip_Settlement_2_View.Vehicle_Trip_Date),
                objDAL.MakeOutParams("@Opening_Balance", SqlDbType.Decimal, 0)};
            objDAL.RunProc("EF_Trn_Trip_Settlement_2_GetDriverOpeningBalance", objSqlParam, ref objDS);

            return Util.String2Decimal(objSqlParam[3].Value.ToString()) == null ? 0 : Util.String2Decimal(objSqlParam[3].Value.ToString());
        }

        public DataSet GetVehicle_OnRoadDate()
        {
            SqlParameter[] objSqlParam ={
                objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, objITrip_Settlement_2_View.Vehicle_ID)};
            objDAL.RunProc("EF_Trn_Trip_Settlement_GetVehicle_OnRoadDate", objSqlParam, ref objDS);

            return objDS;
        }

    }
}