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
using Raj.EF.TransactionsView;
using Raj.EC;

/// <summary>
/// Summary description for VehicleInsurancePremiumModel
/// </summary>
/// 

namespace Raj.EF.TransactionsModel
{
    public class VehicleInsurancePremiumModel:IModel
    {
        private IVehicleInsurancePremiumView objIVehicleInsurancePremiumView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        //private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;
        private int _year_Code = UserManager.getUserParam().YearCode;
        private string _hierarchy_Code = UserManager.getUserParam().HierarchyCode;
        private int _main_Id = UserManager.getUserParam().MainId;
        private int _system_Id = UserManager.getUserParam().SystemId;
        private int _menu_Item_Id = Common.GetMenuItemId();

        public VehicleInsurancePremiumModel(IVehicleInsurancePremiumView vehicleInsurancePremiumView)
        {
            objIVehicleInsurancePremiumView = vehicleInsurancePremiumView;
        }
        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@KeyId", SqlDbType.Int, 0, objIVehicleInsurancePremiumView.keyID),
                    objDAL.MakeInParams("@Menu_ItemID", SqlDbType.Int, 0, _menu_Item_Id)};
            objDAL.RunProc("rstil41.EF_Mst_Vehicle_Insurance_Premium_FillValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet fillBranchOnInsuCompanyChange()
        {
            SqlParameter[] objSqlParam = { 
            objDAL.MakeInParams("@Insurance_Company_Id", SqlDbType.Int, 0, objIVehicleInsurancePremiumView.InsuranceCompanyID),
            objDAL.MakeInParams("@Menu_ItemID", SqlDbType.Int, 0, _menu_Item_Id) ,
            objDAL.MakeInParams("@Key_ID", SqlDbType.Int, 0, objIVehicleInsurancePremiumView.keyID)};
            objDAL.RunProc("rstil41.EF_Mst_Get_Branch_On_Insurance_Company_Changed",objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { 
                objDAL.MakeInParams("@Vehicle_Insurance_ID", SqlDbType.Int, 0, objIVehicleInsurancePremiumView.keyID), 
                objDAL.MakeInParams("@IsFromVehicle", SqlDbType.Int, 0, objIVehicleInsurancePremiumView.VehicleTypeID) };
                objDAL.RunProc("rstil41.EF_Mst_Vehicle_Insurance_Premium_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet GetVehicleDetails()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_Insurance_ID", SqlDbType.Int, 0, objIVehicleInsurancePremiumView.keyID) };
            objDAL.RunProc("rstil41.EF_Mst_Vehicle_Insurance_Premium_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        public bool IsExpiryDateValid()
        {
            SqlParameter[] objSqlParam = { 
                objDAL.MakeInParams("@Vehicle_Insurance_ID", SqlDbType.Int, 0, objIVehicleInsurancePremiumView.keyID),
                objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, objIVehicleInsurancePremiumView.VehicleID),
                objDAL.MakeInParams("@Expiry_Date", SqlDbType.DateTime, 0, objIVehicleInsurancePremiumView.ExpiryDate),
                objDAL.MakeOutParams("@Is_Valid_Expiry_Date", SqlDbType.Bit,0),
                objDAL.MakeOutParams("@Min_Expiry_Date", SqlDbType.DateTime,0),
                objDAL.MakeOutParams("@Max_Expiry_Date", SqlDbType.DateTime,0)};

            objDAL.RunProc("rstil7.EF_Trn_Vehicle_Insurance_Premium_Get_Expiry_Date_Range", objSqlParam);

            bool IsValidExpiryDate = Convert.ToBoolean(objSqlParam[3].Value);
            string MinExpirydate = Convert.ToDateTime(objSqlParam[4].Value).ToString("dd MMM yyyy");
            string MaxExpirydate = Convert.ToDateTime(objSqlParam[5].Value).ToString("dd MMM yyyy");

            if (IsValidExpiryDate == false)
                objIVehicleInsurancePremiumView.errorMessage = "Please Enter Expiry Date Greater Than " + MinExpirydate + " And Less Than " + MaxExpirydate;

            return IsValidExpiryDate;
        }

        public DataSet IsValidExpiryDate()
        {
            SqlParameter[] objSqlParam = {
                objDAL.MakeInParams("@Vehicle_Insurance_Id", SqlDbType.Int, 0, objIVehicleInsurancePremiumView.keyID),
                objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, objIVehicleInsurancePremiumView.VehicleID),
                objDAL.MakeInParams("@CommenceDate", SqlDbType.DateTime, 0, objIVehicleInsurancePremiumView.CommenceDate),
                objDAL.MakeInParams("@ExpiryDate", SqlDbType.DateTime,0 ,objIVehicleInsurancePremiumView.ExpiryDate)};

            objDAL.RunProc("rstil42.EF_Trn_VehicleInsurance_GetCommenceExpiryDate", objSqlParam, ref objDS);

            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                       objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                       objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                       objDAL.MakeInParams("@Year_Code",SqlDbType.Int,0,_year_Code), 
                       objDAL.MakeInParams("@Main_Id",SqlDbType.Int,0,_main_Id ),    
                       objDAL.MakeInParams("@System_ID",SqlDbType.Int,0,_system_Id ),
                       objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 2,_hierarchy_Code),
                       objDAL.MakeInParams("@Vehicle_Insurance_ID", SqlDbType.Int , 0,objIVehicleInsurancePremiumView.keyID), 
                       objDAL.MakeInParams("@Vehicle_Insurance_Date", SqlDbType.DateTime , 0,objIVehicleInsurancePremiumView.InsuranceDate), 
                       objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int , 0,objIVehicleInsurancePremiumView.VehicleID), 
                       objDAL.MakeInParams("@Insurance_Company_ID", SqlDbType.Int , 0,objIVehicleInsurancePremiumView.InsuranceCompanyID), 
                       objDAL.MakeInParams("@Insurance_Company_Branch_ID", SqlDbType.Int, 0, objIVehicleInsurancePremiumView.IssuingBranchID),
                       objDAL.MakeInParams("@Policy_No",SqlDbType.NVarChar,25,objIVehicleInsurancePremiumView.PolicyNo),
                       objDAL.MakeInParams("@Agent_ID", SqlDbType.Int, 0,objIVehicleInsurancePremiumView.AgentID), 
                       objDAL.MakeInParams("@Commence_Date", SqlDbType.DateTime,0, objIVehicleInsurancePremiumView.CommenceDate),
                       objDAL.MakeInParams("@Expiry_Date",SqlDbType.DateTime,0,objIVehicleInsurancePremiumView.ExpiryDate),
                       objDAL.MakeInParams("@IDV", SqlDbType.Decimal, 0,objIVehicleInsurancePremiumView.IDV), 
                       objDAL.MakeInParams("@First_Party_Premium", SqlDbType.Decimal, 0, objIVehicleInsurancePremiumView.FirstPartyPremium),
                       objDAL.MakeInParams("@Third_Party_Premium", SqlDbType.Decimal, 0,objIVehicleInsurancePremiumView.ThirdPartyPremium), 
                       objDAL.MakeInParams("@Loading_Percent_TPP", SqlDbType.Decimal,0, objIVehicleInsurancePremiumView.LoadingPercentTPP),
                       objDAL.MakeInParams("@Loading_Amount_TPP", SqlDbType.Decimal,0, objIVehicleInsurancePremiumView.LoadingAmountTPP),
                       objDAL.MakeInParams("@Loading_Percent_FPP",SqlDbType.Decimal,0,objIVehicleInsurancePremiumView.LoadingPercentFPP),
                       objDAL.MakeInParams("@Loading_Amount_FPP",SqlDbType.Decimal,0,objIVehicleInsurancePremiumView.LoadingAmountFPP),
                       objDAL.MakeInParams("@NCB_Percent_FPP", SqlDbType.Decimal , 0,objIVehicleInsurancePremiumView.NCBPercentFPP), 
                       objDAL.MakeInParams("@NCB_Amount", SqlDbType.Decimal,0, objIVehicleInsurancePremiumView.NCBAmount),
                       objDAL.MakeInParams("@Net_Premium",SqlDbType.Decimal , 0,objIVehicleInsurancePremiumView.NetPremium),
                       objDAL.MakeInParams("@Service_Tax_Percentage",SqlDbType.Decimal,0,objIVehicleInsurancePremiumView.ServiceTaxPercentage),
                       objDAL.MakeInParams("@Service_Tax_Amount", SqlDbType.Decimal, 0,objIVehicleInsurancePremiumView.ServiceTaxAmount), 
                       objDAL.MakeInParams("@Net_Payable", SqlDbType.Decimal,0, objIVehicleInsurancePremiumView.NetPayable),
                       objDAL.MakeInParams("@Is_Cheque", SqlDbType.Bit,0, objIVehicleInsurancePremiumView.Is_Cheque),
                       objDAL.MakeInParams("@Cheque_No", SqlDbType.NVarChar,50, objIVehicleInsurancePremiumView.ChequeNo),
                       objDAL.MakeInParams("@Cheque_Date", SqlDbType.DateTime,0, objIVehicleInsurancePremiumView.ChequeDate),
                       objDAL.MakeInParams("@Bank_ID", SqlDbType.Int,0, objIVehicleInsurancePremiumView.Bank_ID),
                       objDAL.MakeInParams("@Document_Status_ID", SqlDbType.Int,0, 100),
                       objDAL.MakeInParams("@Insurance_Premium_Details", SqlDbType.Xml,0, objIVehicleInsurancePremiumView.InsurancePremiumDetailsXML),
                       objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, _userID)};


            objDAL.RunProc("rstil41.EF_Mst_Vehicle_Insurance_Premium_Save", objSqlParam);

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
