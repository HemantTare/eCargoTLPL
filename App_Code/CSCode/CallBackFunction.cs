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
using ClassLibraryMVP;
using Raj.EC;
//using Raj.EF.MasterView;

/// <summary> 
/// Summary description for CallBackFunction
/// </summary>
/// 
namespace Raj.EF.CallBackFunction
{
    public class CallBack
    {
        // Added by Apurva Shah on 31/12/2010 for Nandwana
        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchVehicleNo(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("rstil41.EF_Mst_Tyre_VehicleNo_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchLedgerName(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("rstil22.[EF_Mst_Tax_LedgerNo_Search]", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchVehicleNoWithFields(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("rstil41.EF_Mst_Vehicle_Insurance_Premium_VehicleSearch", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }
        // Upto here Apurva Shah on 31/12/2010 for Nandwana

        [AjaxPro.AjaxMethod]
        public static DataTable GetBranch(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
            _objDAL.RunProc("EC_Mst_Branch_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetTxtSearchBranch(String SearchFor)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
            _objDAL.RunProc("EC_Mst_Branch_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetTxtDriverInfo(int id)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@id", SqlDbType.BigInt,0,id) };
            _objDAL.RunProc("EC_Mst_Driver_MobileNo", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetUserForResetPassword(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) ,
                                       _objDAL.MakeInParams("@User_Type", SqlDbType.VarChar, 50, othercolumns ) };
            _objDAL.RunProc("Com_ADM_Reset_Password_User_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetAllEmployee(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor)};
            _objDAL.RunProc("EC_Mst_All_Employee_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetMemoToBranch(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            int _mainID = UserManager.getUserParam().MainId;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
              _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0, _mainID)};
            _objDAL.RunProc("EC_Opr_MemoToBranch_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetAgencyLedger(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            
            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor)
                                     };
            _objDAL.RunProc("EC_Opr_Menifest_AgencyLedger_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetShortExcessBranch(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor)};
            _objDAL.RunProc("EC_Opr_ShortExcessBranch_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod] // added Ankit : 12/12/08
        public static DataTable IBT_SearchLedgersForReverse(string SearchFor, string TableName, string KeyName, string KeyID, string othercolumns)
        {
            DataSet ds = new DataSet();
            DAL objDAL = new DAL();
            int Main_id;

            string[] splitted = othercolumns.Split(new char[] { ',' });
            string Hierarchy_Code = splitted[1];
            Main_id = Util.String2Int(splitted[0]);

            SqlParameter[] sqlParam ={ objDAL.MakeInParams("@Searchfor",SqlDbType.VarChar,20,SearchFor),
                objDAL.MakeInParams("@Main_id",SqlDbType.Int,0,UserManager.getUserParam().MainId),
                objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,5,UserManager.getUserParam().HierarchyCode),
                  objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,UserManager.getUserParam().DivisionId),
                                      };

            objDAL.RunProc("FA_Opr_IBT_SearchLedgersForReverse", sqlParam, ref ds);

            return ds.Tables[0];
        }



        [AjaxPro.AjaxMethod]
        public static DataTable GetClientOnBranch(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();    
            DataSet _ds = null;

            SearchFor = SearchFor + "%"; ;
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
                                        _objDAL.MakeInParams("@BranchID", SqlDbType.Int, 0, othercolumns)};
            _objDAL.RunProc("EC_Mst_ContractGeneral_FillClientsOnBranchChanged", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }
        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchEmployee(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            int _mainID = UserManager.getUserParam().MainId;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
            _objDAL.MakeInParams("@MainID", SqlDbType.Int, 0, _mainID)};
            _objDAL.RunProc("EC_Mst_Employee_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchEmployeeWithHierarchyWise(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            int _mainID = UserManager.getUserParam().MainId;
            string _hierarchyCode = UserManager.getUserParam().HierarchyCode;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
            _objDAL.MakeInParams("@Hirarchy_Code", SqlDbType.VarChar, 2, _hierarchyCode),
            _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0, _mainID)};

            _objDAL.RunProc("EC_Search_Employee_On_Hierarchy_Wise", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchAllClient(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("dbo.EC_Opr_GC_ClientSearch", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchAllClientBranchCity(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null; 

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor),
            _objDAL.MakeInParams("@BranchID", SqlDbType.Int, 0, Convert.ToInt32(othercolumns)) };
            _objDAL.RunProc("dbo.EC_Opr_GC_ClientBranchCitySearch", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchLocation(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor),                                       
                                        _objDAL.MakeInParams("@Branch_Id", SqlDbType.VarChar, 0, othercolumns)};
            _objDAL.RunProc("Ec_Opr_GC_Fill_Location", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }


        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchLocationForOtherAgencyBooking(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
 
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor),
                                        _objDAL.MakeInParams("@Agenct_Id", SqlDbType.Int , 0,Util.String2Int( othercolumns))};

            _objDAL.RunProc("Ec_Opr_GC_Fill_Location_For_Other_Agency", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchGC(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            string[] splitted = othercolumns.Split(new char[] { ',' });
            int Vehicle_Id = Util.String2Int(splitted[0]);
            DateTime DDC_Date = Convert.ToDateTime(splitted[1]);
            SearchFor = SearchFor + "%";

            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor),                                       
                                        _objDAL.MakeInParams("@Branch_Id", SqlDbType.Int , 0, UserManager.getUserParam().MainId ),
                                        _objDAL.MakeInParams("@Division_ID", SqlDbType.Int , 0, UserManager.getUserParam().DivisionId ),
                                        _objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0,Vehicle_Id),
                                        _objDAL.MakeInParams("@DDC_Date", SqlDbType.DateTime  , 0, DDC_Date )};

            _objDAL.RunProc("EC_Opr_Direct_Delivery_Get_GC", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }
        


        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchOtherAgencyGC(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
       {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            string[] splitted = othercolumns.Split(new char[] { ',' });
            int AgencyId = Util.String2Int(splitted[0]);
            DateTime AUS_Date = Convert.ToDateTime(splitted[1]);

            SearchFor = SearchFor + "%";
                       


            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor),                                                                              
                                       _objDAL.MakeInParams("@Agency_Id", SqlDbType.Int, 0, AgencyId ) ,
                                       _objDAL.MakeInParams("@AUS_Date", SqlDbType.DateTime  , 0, AUS_Date)};

            _objDAL.RunProc("EC_Opr_AUS_Other_Agency_Get_GC", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }
         

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchConsignorConsignee(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor),
                                        _objDAL.MakeInParams("@Is_Search_By_Code", SqlDbType.VarChar, 20, othercolumns)};

            _objDAL.RunProc("Ec_Opr_GC_Fill_Consignor_Consignee", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchBillingParty(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";

            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };

            _objDAL.RunProc("Ec_Opr_GC_Fill_Billing_Party", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchContractualClient(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };

            _objDAL.RunProc("Ec_Opr_GC_Fill_Contractual_Client", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchDriverName(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("EF_Mst_Vehicle_Information_DriverSearch", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchCleanerName(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("EF_Mst_Vehicle_Information_CleanerSearch", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetTxtSearchDriver(String SearchFor)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("EF_Mst_Vehicle_Information_DriverSearch", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchDriverDetails(String DriverId)
        {
            Common obj = new Common();
            DataSet _ds = obj.EC_Common_Pass_Query("select IsNull(Mobile_No,'') Mobile_No_1, IsNull(Phone_No,'') Mobile_No_2 from dbo.EF_Master_Driver where Driver_ID = " + DriverId);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetTxtSearchCleaner(String SearchFor)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("EF_Mst_Vehicle_Information_CleanerSearch", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetTxtSearchVehicle(String SearchFor)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("EF_Mst_Vehicle_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchVehicleDetails(String vehicleId)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, Util.String2Int(vehicleId)) };
            _objDAL.RunProc("EC_Opr_GetValuesOnVahicleChanged", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchToBranchForRateCard(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = {_objDAL.MakeInParams("@flag", SqlDbType.VarChar, 50, "ToBranch") ,
            _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor)};
            _objDAL.RunProc("dbo.EC_Master_Branch_Rate_Parameters_FillValues", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchCopyBranchForRateCard(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = {_objDAL.MakeInParams("@flag", SqlDbType.VarChar, 50, "CopyFromBranch") ,
            _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor)};
            _objDAL.RunProc("dbo.EC_Master_Branch_Rate_Parameters_FillValues", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetMemoDestForBranch(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            //String key1 = KeyName + "," + KeyID;
            //Common objCommon = new Common();
            //_ds = objCommon.Get_Values_Where(TableName, key1, "Is_Active = 1 and " + KeyName + " like'" + SearchFor.ToLower() + "%'", KeyName, true);

            if (othercolumns == "")
            {
                othercolumns = "0";
            }
            SqlParameter[] sqlParam = { _objDAL.MakeInParams("@LikeChar", SqlDbType.VarChar, 50, SearchFor.ToLower() + "%"),
                                        _objDAL.MakeInParams("@Branch_ID",SqlDbType.Int,0,othercolumns)};
            _objDAL.RunProc("EC_Mst_Branch_Get_memo_Dest", sqlParam, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetAcountAgencyForBranch(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            String key1 = KeyName + "," + KeyID;
            Common objCommon = new Common();

            _ds = objCommon.Get_Values_Where(TableName, key1, "Primary_Ledger_Group_Id = 4 and " + KeyName + " like'" + SearchFor.ToLower() + "%'", KeyName, false);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetPetrolPump(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            String key1 = KeyName + "," + KeyID;
            Common objCommon = new Common();

            _ds = objCommon.Get_Values_Where(TableName, key1, KeyName + " like'" + SearchFor.ToLower() + "%'", KeyName, true);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetAreaForBranch(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
           {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = {_objDAL.MakeInParams("@RegionId", SqlDbType.Int, 0,othercolumns) ,
                                      _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor)
                                  };
            _objDAL.RunProc("dbo.EC_Mst_Get_Area_For_Branch", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetDefaultHubForBranch(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            //String key1 = KeyName + "," + KeyID;
            //Common objCommon = new Common();

            //_ds = objCommon.Get_Values_Where(TableName, key1, "Is_Crossing =1 and Is_Active = 1 and " + KeyName + " like'" + SearchFor.ToLower() + "%'", KeyName, true);

            if (othercolumns == "")
            {
                othercolumns = "0";
            }

            SqlParameter[] sqlParam = 
                                    { 
                                       _objDAL.MakeInParams("@LikeChar", SqlDbType.VarChar, 50, SearchFor.ToLower() + "%"),
                                        _objDAL.MakeInParams("@Branch_ID",SqlDbType.Int,0,othercolumns)
                                    };
            _objDAL.RunProc("EC_Mst_Branch_Get_Default_Hub", sqlParam, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetDeliveryAtForBranch(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            //String key1 = KeyName + "," + KeyID;
            //Common objCommon = new Common();

            //_ds = objCommon.Get_Values_Where(TableName, key1, "Is_VTrans_Delivery =1 and Is_Active = 1 and " + KeyName + " like'" + SearchFor.ToLower() + "%'", KeyName, true);

            if (othercolumns == "")
            {
                othercolumns = "0";
            }

            SqlParameter[] sqlParam = 
                                    { 
                                       _objDAL.MakeInParams("@LikeChar", SqlDbType.VarChar, 50, SearchFor.ToLower() + "%"),
                                        _objDAL.MakeInParams("@Branch_ID",SqlDbType.Int,0,othercolumns)
                                    };
            _objDAL.RunProc("EC_Mst_Branch_Get_Delivery_At", sqlParam, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]  //Added by Ankit : 1/12/08 : 6.45 pm
        public static DataTable GetGCNo(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            SearchFor = SearchFor + "%";

            SqlParameter[] sqlParam = 
                                    { 
                                        _objDAL.MakeInParams("@PODReceivedDate",SqlDbType.DateTime,0,Convert.ToDateTime(othercolumns)),
                                        _objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,UserManager.getUserParam().DivisionId),
                                       _objDAL.MakeInParams("@LikeChar", SqlDbType.VarChar, 50, SearchFor)
                                    };
            _objDAL.RunProc("EC_Opr_POD_GCNoSearch", sqlParam, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchLedgerForVoucher(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            SearchFor = SearchFor + "%";

            string Ids = (String)System.Web.HttpContext.Current.Session["IDsForVoucher"];

             string[] _splitted = Ids.Split('Ö');

            string voucherType = _splitted[0];
            string crDr = _splitted[1];

            SqlParameter[] sqlPara = { 
                                       _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
                                       _objDAL.MakeInParams("@VoucherType", SqlDbType.VarChar, 50, voucherType),
                                       _objDAL.MakeInParams("@CrDr", SqlDbType.VarChar, 20, crDr),
                                       _objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,UserManager.getUserParam().DivisionId)
                                      };
            _objDAL.RunProc("EC_FA_Mst_Voucher_SearchLedger", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchLedgerForVoucherNoCrDr(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            SearchFor = SearchFor + "%"; 
    
            string voucherType="";
            voucherType = othercolumns;

            SqlParameter[] sqlPara = { 
                                       _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
                                       _objDAL.MakeInParams("@VoucherType", SqlDbType.VarChar, 50, voucherType), 
                                       _objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,UserManager.getUserParam().DivisionId)
                                      };
            _objDAL.RunProc("EC_FA_Mst_Voucher_SearchLedgerNoCrDr", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchLedgerForVehicleVendor(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            SearchFor = SearchFor + "%";

            string voucherType = "";
            voucherType = othercolumns;

            SqlParameter[] sqlPara = { 
                                       _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
                                       _objDAL.MakeInParams("@VoucherType", SqlDbType.VarChar, 50, voucherType), 
                                       _objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0,UserManager.getUserParam().DivisionId)
                                      };
            _objDAL.RunProc("EC_FA_Mst_Voucher_SearchLedgerVendor", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchCity(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 100, SearchFor),
                                       _objDAL.MakeInParams("@RegionDetail_ID",SqlDbType.Int,0,othercolumns) 
                                     };
            _objDAL.RunProc("EC_Master_CC_GetSearchCity", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetTxtSearchCity(String SearchFor, int CityId)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 100, SearchFor),
                                       _objDAL.MakeInParams("@RegionDetail_ID",SqlDbType.Int,0,CityId) 
                                     };
            _objDAL.RunProc("EC_Master_CC_GetSearchCity", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetAllSearchCity(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 100, SearchFor)  
                                     };
            _objDAL.RunProc("EC_Master_GetAllSearchCity", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetServiceLocation(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
            _objDAL.RunProc("EC_Opr_ServiceLocation_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }
        [AjaxPro.AjaxMethod]
        public static DataTable GetFromServiceLocationOfLHPO(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
                                       _objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, UserManager.getUserParam().MainId) };
            _objDAL.RunProc("EC_Opr_FromServiceLocationLHPO_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetToServiceLocationOfLHPO(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
                                       _objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, UserManager.getUserParam().MainId) };
            _objDAL.RunProc("EC_Opr_ToServiceLocationLHPO_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }
        [AjaxPro.AjaxMethod]
        public static DataTable GetDriver(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
            _objDAL.RunProc("EC_Opr_Driver_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }
        [AjaxPro.AjaxMethod]
        public static DataTable GetCleaner(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
            _objDAL.RunProc("EC_Opr_Cleaner_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }
        [AjaxPro.AjaxMethod]
        public static DataTable GetSupervisor(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            int _branchID = UserManager.getUserParam().MainId;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
                                       _objDAL.MakeInParams("@BranchID",SqlDbType.Int,0,_branchID)
                                  };
            _objDAL.RunProc("EC_Opr_Supervisor_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchBillNo(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            //string[] splitted = othercolumns.Split(new char[] { 'Ö' });

            int Ledger_Id=0;
            int Voucher_Id=0;

            //Ledger_Id = Util.String2Int(splitted[0]);
            //Voucher_Id = Util.String2Int(splitted[1]);

            if (System.Web.HttpContext.Current.Session["Bill_Voucher_Id"] != null)
            {
                Ledger_Id = Util.String2Int((string)System.Web.HttpContext.Current.Session["Bill_Ledger_Id"]);
                Voucher_Id = Util.String2Int((string)System.Web.HttpContext.Current.Session["Bill_Voucher_Id"]);
            }

            //SearchFor = SearchFor ;
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 20, SearchFor),
                                       _objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int,0,Voucher_Id),
                                       _objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,Ledger_Id),
                                       _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2,UserManager.getUserParam().HierarchyCode),
                                       _objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,UserManager.getUserParam().MainId),
                                       _objDAL.MakeInParams("@Division_Id", SqlDbType.Int,0,UserManager.getUserParam().DivisionId)
                                     };
            _objDAL.RunProc("EC_FA_Mst_Voucher_SearchBills", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
             return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetBillingClientForContract(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            String key1 = KeyName + "," + KeyID;
            Common objCommon = new Common();

            _ds = objCommon.Get_Values_Where(TableName, key1, KeyName + " like'" + SearchFor.ToLower() + "%'", KeyName, true);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetCityForClient(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            String key1 = KeyName + "," + KeyID;
            Common objCommon = new Common();

            _ds = objCommon.Get_Values_Where(TableName, key1, KeyName + " like'" + SearchFor.ToLower() + "%'", KeyName, true);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetClientForTransportBill(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            String key1 = KeyName + "," + KeyID;
            Common objCommon = new Common();

            _ds = objCommon.Get_Values_Where(TableName, key1, "Is_Active = 1 and " + KeyName + " like '" + SearchFor.ToLower() + "%'", KeyName, true);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]  //added by Ankit : 05/11/2008
        public static DataTable GetSearchVehicleCleanerName(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("rstil42.EF_Mst_Vehicle_CleanerSearch", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]   //added by Ankit : 05/11/2008
        public static DataTable GetSearchVehicleOwnerName(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            int VehicleCategoryId = Util.String2Int(othercolumns);
            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
                                       _objDAL.MakeInParams("@VehicleCategoryId", SqlDbType.Int,0,VehicleCategoryId) 
                                     };
            _objDAL.RunProc("rstil42.EF_Mst_Vehicle_OwnerSearch", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }


        [AjaxPro.AjaxMethod]  //added by Ankit : 24/11/2008
        public static DataTable GetSearchPartyLedger(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL objDAL = new DAL();
            DataSet ds = new DataSet();


            SqlParameter[] sqlParam = { 
                                        objDAL.MakeInParams("@Division_Id", SqlDbType.Int , 1,UserManager.getUserParam().DivisionId ),
                                        objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 20, SearchFor+"%")
                                      };

            objDAL.RunProc("EC_FA_Common_AdvancePayment_SearchPartyLedger", sqlParam, ref ds);

            return ds.Tables[0];
        }

        [AjaxPro.AjaxMethod] // added Ankit : 12/12/08
        public static DataTable Fill_Ledger_for_Voucher(string SearchFor, string TableName, string KeyName, string KeyID, string othercolumns)
        {
            DataSet ds = new DataSet();
            DAL objDAL = new DAL();
            int Main_id;

            string[] splitted = othercolumns.Split(new char[] { ',' });
            string Hierarchy_Code = splitted[1];
            Main_id = Util.String2Int(splitted[0]);

            SqlParameter[] sqlParam ={ objDAL.MakeInParams("@Searchfor",SqlDbType.VarChar,20,SearchFor),
                objDAL.MakeInParams("@Main_id",SqlDbType.Int,0,Main_id  ),
                objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,5,Hierarchy_Code )};

            objDAL.RunProc("dbo.FA_Fill_LedgersFor_IBT_Approved", sqlParam, ref  ds);

            return ds.Tables[0];
        }

        [AjaxPro.AjaxMethod]  //added : Ankit : 09-01-09 : 4.00 pm
        public static DataTable GetRegularClient(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) ,
                                       _objDAL.MakeInParams("@Client_Id", SqlDbType.VarChar, 10, othercolumns)};
            _objDAL.RunProc("EC_Mst_Regular_Client_FillValues", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetAttachedLHPOBranch(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            
            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
                                       _objDAL.MakeInParams("@BranchId", SqlDbType.Int , 0, UserManager.getUserParam().MainId)             
                };
            _objDAL.RunProc("EC_Opr_AttachedLHPOBranch_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }


        [AjaxPro.AjaxMethod]
        public static DataTable GetOwnerBroker(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = {_objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor)};
            _objDAL.RunProc("dbo.EC_FA_BTH_Owner_Broker_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetLedgerTransportBill(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("dbo.EC_FA_Mst_Voucher_SearchLedgerNoCrDr_TransportBill", sqlPara, ref _ds);

            return _ds.Tables[0];

        }
        [AjaxPro.AjaxMethod]
        public static DataTable GetLedgerDirectDly(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
            _objDAL.RunProc("EC_FA_Opr_Ledger_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }
        [AjaxPro.AjaxMethod]
        public static DataTable GetLedger(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("dbo.EC_FA_BTH_Fill_LedgerSearch", sqlPara, ref _ds);

            return _ds.Tables[0];
            
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetLedgerOnAccount(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL objDAL = new DAL();
            DataSet ds = new DataSet();
            

            SqlParameter[] sqlParam = { objDAL.MakeInParams("@Ledger_Group_Id", SqlDbType.Int, 0, Util.String2Int(othercolumns)),
                                        objDAL.MakeInParams("@Division_ID",SqlDbType.Int,0, UserManager.getUserParam().DivisionId),
                                        objDAL.MakeInParams("@initials", SqlDbType.VarChar, 10, SearchFor)};

            objDAL.RunProc("FA_Opr_OnAccount_Ledger", sqlParam, ref ds);

            return ds.Tables[0];
        }
        [AjaxPro.AjaxMethod]
        public static DataTable GetLedgerForCreditTo(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL objDAL = new DAL();
            DataSet ds = new DataSet();


            SqlParameter[] sqlParam = {objDAL.MakeInParams("@initials", SqlDbType.VarChar, 50, SearchFor)};

            objDAL.RunProc("EC_FA_DoorDelAndLocalCartVoucher_GetLedgers", sqlParam, ref ds);

            return ds.Tables[0];
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetIncidentalExpenseLedger(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL objDAL = new DAL();
            DataSet ds = new DataSet();


            SqlParameter[] sqlParam = { objDAL.MakeInParams("@initials", SqlDbType.VarChar,50, SearchFor) };

            objDAL.RunProc("EC_FA_Opr_Incidental_Voucher_ExpenseLedger", sqlParam, ref ds);

            return ds.Tables[0];
        }
 
        [AjaxPro.AjaxMethod]
        public static DataTable GetLedgerForOctroiUpdate(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            //int LedgerGroupId;
            

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
            _objDAL.RunProc("EC_Opr_OctroiUpdate_GetLedger", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            //LedgerGroupId = Util.String2Int(dt.Rows[0]["Ledger_Group_Id"].ToString());
            return dt;
        }


        [AjaxPro.AjaxMethod]
        public static DataTable GetLedgerCompanyDetails(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("EC_FA_Company_Details_Ledger", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }
        [AjaxPro.AjaxMethod]
        public static DataTable GetLedgerTerminatedLHC(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
            _objDAL.RunProc("EC_Opr_LHPO_GetTerminatedLHCLedger", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }
        [AjaxPro.AjaxMethod]
        public static DataTable GetATHLedger(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("EC_FA_ATH_Fill_LedgerSearch", sqlPara, ref _ds);

            return _ds.Tables[0];

        }
        [AjaxPro.AjaxMethod]
        public static DataTable GetCharityLedger(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("EC_Opr_LHPO_GetCharityLedger", sqlPara, ref _ds);

            return _ds.Tables[0];

        }
        [AjaxPro.AjaxMethod]
        public static DataTable GetRegionForBranch(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
       {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = {_objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor)};
            _objDAL.RunProc("dbo.EC_Mst_Get_Region_For_Branch", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetLedgerForOtherAgencyGC(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
                                       _objDAL.MakeInParams("@Agency_Id", SqlDbType.Int, 0,Util.String2Int(othercolumns))};
            _objDAL.RunProc("dbo.EC_Opr_AUS_Other_Agency_GC_Ledger_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetAgencyForOtherAgencyGC(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
            _objDAL.RunProc("dbo.EC_Opr_AUS_Other_Agency_GC_Agency_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetPartyRecieptVoucherCashBankLedger(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
            _objDAL.RunProc("EC_FA_PartyRecieptVoucher_CashBankLedger_Fill", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetPartyRecieptVoucherClientLedger(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
            _objDAL.RunProc("EC_FA_PartyRecieptVoucher_ClientLedger_Fill", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetPartyRecieptVoucherOtherDeductionLedger(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
            _objDAL.RunProc("EC_FA_PartyRecieptVoucher_OtherDeductionLedger_Fill", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetPartyReceiptSearchBillNo(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            int Ledger_Id = 0;
            int Voucher_Id = 0;

            if (System.Web.HttpContext.Current.Session["Bill_Voucher_Id"] != null)
            {
                Ledger_Id = Util.String2Int((string)System.Web.HttpContext.Current.Session["Bill_Ledger_Id"]);
                Voucher_Id = Util.String2Int((string)System.Web.HttpContext.Current.Session["Bill_Voucher_Id"]);
            }

            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor),
                                       _objDAL.MakeInParams("@Voucher_Id", SqlDbType.Int,0,Voucher_Id),
                                       _objDAL.MakeInParams("@Ledger_Id", SqlDbType.Int,0,Ledger_Id),
                                       _objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar,2,UserManager.getUserParam().HierarchyCode),
                                       _objDAL.MakeInParams("@Main_Id", SqlDbType.Int,0,UserManager.getUserParam().MainId),
                                       _objDAL.MakeInParams("@Division_Id", SqlDbType.Int,0,UserManager.getUserParam().DivisionId)
                                     };
            _objDAL.RunProc("EC_FA_Mst_PartyReceiptVoucher_SearchBills", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }
        [AjaxPro.AjaxMethod()]
        public static DataTable GetSearchClient(string SearchFor, string TableName, string KeyName, string KeyID, string othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            string key1 = KeyName + "," + KeyID;
            DataSet ds = null;
            Common obj_Common = new Common();
            _ds = obj_Common.Get_Values_Where(TableName, key1, KeyName + " like'" + SearchFor.ToLower() + "%'", KeyName, true);
            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetManifestTypeToBranch(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%"; ;
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
                                       _objDAL.MakeInParams("@memo_Branch_Id", SqlDbType.Int, 0, Util.String2Int(othercolumns))};
            _objDAL.RunProc("[dbo].[EC_Opr_ManifestTypeChangeToBranch_Search]", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetBrokerAndParty(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%"; ;
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
                                       _objDAL.MakeInParams("@Bill_To_ID", SqlDbType.Int, 0, Util.String2Int(othercolumns))};
            _objDAL.RunProc("[dbo].[EC_Opr_Get_BrokerAndParty_Search]", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetBrokerForLHPO(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
            _objDAL.RunProc("[dbo].[EC_Opr_LHPO_BrokerSearch]", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetBrokerForDVLP(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
            _objDAL.RunProc("[dbo].[EC_Opr_DVLP_BrokerSearch]", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetTxtBrokerForDVLP(String SearchFor)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
            _objDAL.RunProc("[dbo].[EC_Opr_DVLP_BrokerSearch]", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchPartyType(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
                                       _objDAL.MakeInParams("@Party_Type_Flg", SqlDbType.Int, 0, Util.String2Int(othercolumns))};
            _objDAL.RunProc("[dbo].[EC_Opr_PartyType_Search]", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable RegularContractualClient_Search(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor)};
            _objDAL.RunProc("[dbo].[EC_Master_RegularContractualClient_Search]", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable PTLFTLClient_Search(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
            _objDAL.MakeInParams("@City_ID", SqlDbType.VarChar, 10, othercolumns)};
            _objDAL.RunProc("[dbo].[EC_Master_PTL_FTL_Client_Search]", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable PTLFTLClient_txtSearch(String SearchFor,String CityID)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
            _objDAL.MakeInParams("@City_ID", SqlDbType.VarChar, 10, CityID)};
            _objDAL.RunProc("[dbo].[EC_Master_PTL_FTL_Client_Search]", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable RegularClient_txtSearch(String SearchFor)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor) };
            _objDAL.RunProc("[dbo].[EC_Master_Regular_Client_Search]", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetAllAreas(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            _objDAL.RunProc("dbo.EC_Mst_Get_All_Area", ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }


        [AjaxPro.AjaxMethod]
        public static DataTable GetTDSPercent(DateTime date, int TDSCertificateTo,
            bool IsRCRecieved, bool IsPANCardRecieved, int vendorID)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            date = new DateTime(date.Year, date.Month, date.Day);

            SqlParameter[] sqlPara = { 
                                _objDAL.MakeInParams("@Date", SqlDbType.DateTime, 0,date.ToString("dd MMM yyyy")),
                                _objDAL.MakeInParams("@TDSCertificateTo", SqlDbType.Int, 0,TDSCertificateTo),
                                _objDAL.MakeInParams("@IsRCRecieved", SqlDbType.Bit, 0,IsRCRecieved),
                                _objDAL.MakeInParams("@IsPANCardRecieved", SqlDbType.Bit, 0,IsPANCardRecieved),
                                _objDAL.MakeInParams("@VendorId", SqlDbType.Int, 0,vendorID)};

            try
            {
                _objDAL.RunProc("dbo.GetTDSPercent",sqlPara, ref _ds);
            }
            catch (Exception e)
            {
                int i;
            }

            DataTable dt = _ds.Tables[0];
            return dt;
        }


        [AjaxPro.AjaxMethod]
        public static DataTable GetVendor(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 50, SearchFor),
            _objDAL.MakeInParams("@VendorTypeIDs", SqlDbType.VarChar, 50, othercolumns)};
            _objDAL.RunProc("EF_Mst_Vendor_Select", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetGCNoForAUS(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            int Branch_Id = 0;
            int LHPO_Id = 0;

            if (System.Web.HttpContext.Current.Session["AUSNew_LHPO_Id"] != null)
            {
                Branch_Id = UserManager.getUserParam().MainId;
                LHPO_Id = Util.String2Int((string)System.Web.HttpContext.Current.Session["AUSNew_LHPO_Id"]);
            }


            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor),
                                      _objDAL.MakeInParams("@Branch_Id", SqlDbType.Int,0,Branch_Id), 
                                      _objDAL.MakeInParams("@LHPO_Id", SqlDbType.Int,0,LHPO_Id),
                                      _objDAL.MakeInParams("@AUS_Id", SqlDbType.Int,0,Util.String2Int(othercolumns))
                                     };
            _objDAL.RunProc("EC_Opr_Get_GCNosForAUS", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchBillingPartyForAccountStatement(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";

            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };

            _objDAL.RunProc("EC_Opr_Fill_For_Client_Account_Statement", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchToBeWorkedAt(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor),                                       
                                       _objDAL.MakeInParams("@ToBeWorkedAtType", SqlDbType.VarChar,0,othercolumns )};

            _objDAL.RunProc("[rstil37].[EF_Mst_PM_Task_Template_Fill_ToBeWorkedAt]", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }
        [AjaxPro.AjaxMethod]
        public static DataTable GetWorkOrderBranchName(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;
            //SearchFor = SearchFor ;
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor),
                                 };
            _objDAL.RunProc("[rstil41].[EF_Mst_Branch_BranchNameSearch]", sqlPara, ref _ds);
            DataTable dt = _ds.Tables[0];
            return dt;
        }
        [AjaxPro.AjaxMethod]
        public static DataTable GetSearchVendor(String SearchFor, String TableName, String KeyName, String KeyID, String othercolumns)
        {
            DAL _objDAL = new DAL();
            Raj.EC.Common objCommon = new Common();
            DataSet _ds = null;
            SearchFor = SearchFor + "%";
            _ds = objCommon.FillVendorInDDL(SearchFor, Util.String2Int(othercolumns));

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetGCDetails(int GC_No, int Month, int YearCode)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SqlParameter[] objSqlParam = {
                                    _objDAL.MakeInParams("@GC_No", SqlDbType.Int,0,GC_No),
            _objDAL.MakeInParams("@Month", SqlDbType.Int,0,Month),
            _objDAL.MakeInParams("@YearCode", SqlDbType.Int,0,YearCode)};

            _objDAL.RunProc("dbo.EC_Opr_Get_GC_Details", objSqlParam, ref _ds);

            return _ds.Tables[0];

        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetTxtSearchDriverCleaner(int IsCleaner, String SearchFor)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@IsCleaner", SqlDbType.Int  , 0, IsCleaner),
            _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("EF_Opr_DriverCleaner_Update_DriverCleanerSearch", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetTxtDriverCleanerInfo(int id, int IsCleaner)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@id", SqlDbType.BigInt, 0, id),
            _objDAL.MakeInParams("@Is_Cleaner", SqlDbType.BigInt, 0, IsCleaner)};
            _objDAL.RunProc("EF_Mst_DriverCleaner_Details", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetTxtDriverCleanerInfoFromVehicle(int VehicleId, int IsCleaner)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SqlParameter[] sqlPara = { 
            _objDAL.MakeInParams("@IsCleaner", SqlDbType.Int, 0, IsCleaner),
            _objDAL.MakeInParams("@VehicleId", SqlDbType.Int, 0, VehicleId)};
            _objDAL.RunProc("EF_Mst_DriverCleaner_Details_From_Vehicle", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetTxtSearchVarnar(String SearchFor)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SearchFor = SearchFor + "%";
            SqlParameter[] sqlPara = { _objDAL.MakeInParams("@SearchFor", SqlDbType.VarChar, 10, SearchFor) };
            _objDAL.RunProc("EC_Mst_Varnar_Search", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

        [AjaxPro.AjaxMethod]
        public static DataTable GetTxtVarnarDetails(int Varnar_Id)
        {
            DAL _objDAL = new DAL();
            DataSet _ds = null;

            SqlParameter[] sqlPara = { 
            _objDAL.MakeInParams("@Varnar_Id", SqlDbType.Int, 0, Varnar_Id)};
            _objDAL.RunProc("EC_Mst_Varnar_Details", sqlPara, ref _ds);

            DataTable dt = _ds.Tables[0];
            return dt;
        }

    }
}