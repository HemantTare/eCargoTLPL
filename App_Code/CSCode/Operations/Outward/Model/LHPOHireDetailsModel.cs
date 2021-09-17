using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.OperationView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for LHPOHireDetailsModel
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{

    public class LHPOHireDetailsModel:IModel 
    {
        private ILHPOHireDetailsView objILHPOHireDetailsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;
        private int _branchID = UserManager.getUserParam().MainId;
        private int _DivisionID = UserManager.getUserParam().DivisionId;
        int _keyID;
        bool _isAdd;
        public LHPOHireDetailsModel(ILHPOHireDetailsView lHPOHireDetailsView)
        {
            _isAdd = true;
            objILHPOHireDetailsView = lHPOHireDetailsView;
            _keyID = objILHPOHireDetailsView.keyID;
            if (objILHPOHireDetailsView.LHPOTypeID == 2 && objILHPOHireDetailsView.keyID <= 0)
            {
                _keyID = objILHPOHireDetailsView.LHPONo;
                _isAdd = true;
            }
            if (objILHPOHireDetailsView.keyID > 0)
            {
                _isAdd = false;
            }
        }
        public DataSet FillValues()
        {
            objDAL.RunProc("EC_Opr_LHPOHireDetails_FillValues", ref objDS);
            Raj.EC.Common.SetTableName(new string[] { "VehicleCategory", "LhpoType", "BrokerName", "FreightType" }, objDS);
            return objDS;

        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@LHPO_ID", SqlDbType.Int, 0,_keyID),
                                           objDAL.MakeInParams("@Lhpo_Type_ID", SqlDbType.Int, 0, objILHPOHireDetailsView.LHPOTypeID)
                                           };
             objDAL.RunProc("EC_Opr_LHPOHireDetails_ReadValues", objSqlParam, ref objDS);
            return objDS;

        }

        public DataSet GetVehicleInformationOnVehicleChanged()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, objILHPOHireDetailsView.VehicleID),
                                         objDAL.MakeInParams("@Vehicle_Category_ID", SqlDbType.Int, 0, objILHPOHireDetailsView.VehicleCategoryID) 
                                           };
             objDAL.RunProc("EC_Opr_LHPOHireDetails_GetValuesOnVahicleChanged", objSqlParam, ref objDS);
            return objDS;

        }
        public DataSet FillGrid()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0, objILHPOHireDetailsView.VehicleID),
                                           objDAL.MakeInParams("@LHPO_Date", SqlDbType.DateTime, 0, objILHPOHireDetailsView.LHPODate),
                                           objDAL.MakeInParams("@Attached_LHPODate", SqlDbType.DateTime, 0, objILHPOHireDetailsView.AttachedLHPODate), 
                                           objDAL.MakeInParams("@LHPO_ID", SqlDbType.Int, 0, _keyID),                                            
                                           objDAL.MakeInParams("@Branch_ID", SqlDbType.Int, 0, _branchID),                                            
                                           objDAL.MakeInParams("@Is_Add", SqlDbType.Bit, 0, _isAdd),
                                           objDAL.MakeInParams("@DivisionId",SqlDbType.Int,0,_DivisionID)
                                            
                                           };
            objDAL.RunProc("EC_Opr_LHPOHireDetails_FillGrid", objSqlParam, ref objDS);
            Raj.EC.Common.SetTableName(new string[] { "MEMOGRID"}, objDS);
            
            return objDS;

        }
        public DataSet GetKMS()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeOutParams("@Distance_In_Km", SqlDbType.Decimal, 0),
                                           objDAL.MakeOutParams("@Transit_Days", SqlDbType.Int, 0),      
                                           objDAL.MakeOutParams("@ToLocation_BranchId", SqlDbType.Int, 0),
                                           objDAL.MakeInParams("@from_Service_Location_ID", SqlDbType.Int, 0, objILHPOHireDetailsView.FromLocationID),
                                           objDAL.MakeInParams("@VehicleID", SqlDbType.Int, 0, objILHPOHireDetailsView.VehicleID), 
                                           objDAL.MakeInParams("@To_Service_Location_ID", SqlDbType.Int, 0, objILHPOHireDetailsView.ToLocationID)                                           
                                            
                                           };
            objDAL.RunProc("EC_Opr_LHPOHireDetails_GetKMS", objSqlParam, ref objDS);
            if (objSqlParam[0].Value == DBNull.Value)
            {
                objSqlParam[0].Value = 0;
            }
            objILHPOHireDetailsView.ActualKms = Util.String2Decimal(objSqlParam[0].Value.ToString());
            if (objSqlParam[1].Value == DBNull.Value)
            {
                objSqlParam[1].Value = 0;
            }
            objILHPOHireDetailsView.TransitDays = Util.String2Int(objSqlParam[1].Value.ToString());
            if (objSqlParam[2].Value == DBNull.Value)
            {
                objSqlParam[2].Value = 0;
            }
            objILHPOHireDetailsView.ToLocationBranchId = Util.String2Int(objSqlParam[2].Value.ToString());
            objILHPOHireDetailsView.CommitedDelDate = objILHPOHireDetailsView.LHPODate.AddDays(Convert.ToDouble(objILHPOHireDetailsView.TransitDays));
            return objDS;

        }
        //public DataSet GetTDSPercentage()
        //{
        //    SqlParameter[] objSqlParam = {  objDAL.MakeOutParams("@TDSPercentage", SqlDbType.Decimal, 0),
        //                                    objDAL.MakeOutParams("@Addl_Surcharge_Cess",SqlDbType.Decimal,0),
        //                                    objDAL.MakeOutParams("@Addl_Education_Cess",SqlDbType.Decimal,0),
        //                                    objDAL.MakeOutParams("@Surcharge",SqlDbType.Decimal,0),
        //                                    objDAL.MakeOutParams("@ExemptionLimit",SqlDbType.Decimal,0),
        //                                    objDAL.MakeInParams("@LHPODate",SqlDbType.DateTime,0,objILHPOHireDetailsView.LHPODate),
        //                                    objDAL.MakeInParams("@TDSCertificateToID", SqlDbType.Bit, 0, objILHPOHireDetailsView.TDSCertificateToID),
        //                                    objDAL.MakeInParams("@VehicleID", SqlDbType.Int, 0, objILHPOHireDetailsView.VehicleID),
        //                                    objDAL.MakeInParams("@Broker_ID", SqlDbType.Int, 0, objILHPOHireDetailsView.BrokerID)                                           
                                            
        //                                   };
        //    objDAL.RunProc("EC_Opr_LHPOHireDetails_GetTDSPercentageOnBrokerChange", objSqlParam, ref objDS);
        //    if (objSqlParam[0].Value == DBNull.Value)
        //    {
        //        objSqlParam[0].Value = 0;
        //    }
        //    objILHPOHireDetailsView.TDSPercentage =Convert.ToDecimal(objSqlParam[0].Value);
        //    if (objSqlParam[1].Value == DBNull.Value)
        //    {
        //        objSqlParam[1].Value = 0;
        //    }
        //    objILHPOHireDetailsView.AddlSurchargeCess = Convert.ToDecimal(objSqlParam[1].Value);
        //    if (objSqlParam[2].Value == DBNull.Value)
        //    {
        //        objSqlParam[2].Value = 0;
        //    }
        //    objILHPOHireDetailsView.AddlEducationCess = Convert.ToDecimal(objSqlParam[2].Value);
        //    if (objSqlParam[3].Value == DBNull.Value)
        //    {
        //        objSqlParam[3].Value = 0;
        //    }
        //    objILHPOHireDetailsView.Surcharge = Convert.ToDecimal(objSqlParam[3].Value);
        //    if (objSqlParam[4].Value == DBNull.Value)
        //    {
        //        objSqlParam[4].Value = 0;
        //    }
        //    objILHPOHireDetailsView.ExemptionLimit = Convert.ToDecimal(objSqlParam[4].Value);

        //    return objDS;

        //}
        public DataSet GetTDSercent()
        {
            SqlParameter[] objSqlParam = {  
                                            objDAL.MakeInParams("@Division_Id",SqlDbType.Int,0,_DivisionID),
                                            objDAL.MakeInParams("@Vendor_Id", SqlDbType.Int, 0, objILHPOHireDetailsView.BrokerID),
                                            objDAL.MakeInParams("@Vehicle_ID",SqlDbType.Int,0,objILHPOHireDetailsView.VehicleID),                           
                                            objDAL.MakeInParams("@LHPO_Date",SqlDbType.DateTime,0,objILHPOHireDetailsView.LHPODate),
                                            objDAL.MakeInParams("@Amount",SqlDbType.Decimal,0,0),
                                            objDAL.MakeOutParams("@TDSAmount",SqlDbType.Decimal,0)                                            
                                           };
            objDAL.RunProc("[dbo].[EC_FA_Opr_CalcTdsForLHPO]", objSqlParam, ref objDS);
            return objDS;
            
        }

        public DataSet GetDVLPValuesForLHPO()
        {
            SqlParameter[] objSqlParam = {  
                                            objDAL.MakeInParams("@VehicleID",SqlDbType.Int,0,objILHPOHireDetailsView.VehicleID),                           
                                            objDAL.MakeInParams("@TripMemoDate",SqlDbType.DateTime,0,objILHPOHireDetailsView.LHPODate),                                           
                                           };
            objDAL.RunProc("[dbo].[EC_Opr_GetDVLPValuesForLHPO]", objSqlParam, ref objDS);
            return objDS;

        }

        public DataSet FillAttachedLHPONo()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Lhpo_Type_ID", SqlDbType.Int, 0, objILHPOHireDetailsView.LHPOTypeID),
                                           objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objILHPOHireDetailsView.VehicleID),
                                            objDAL.MakeInParams("@Login_Branch_ID", SqlDbType.Int, 0, _branchID)};
            objDAL.RunProc("EC_Opr_LHPOHireDetails_FillAttachedLHPOOnLHPOTypeChange", objSqlParam, ref objDS);            
            return objDS;
        }

        public DataSet FillLHPOParameters()
        {
            objDAL.RunProc("EC_Opr_Fill_LHPO_Parameters", ref objDS);
            return objDS;
        }

        public DataSet GetFromLocationBranchId()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Location_ID", SqlDbType.Int, 0, objILHPOHireDetailsView.FromLocationID),
                                           };
            objDAL.RunProc("EC_Opr_LHPO_Get_Location_BranchId", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet GetToLocationBranchId()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Location_ID", SqlDbType.Int, 0, objILHPOHireDetailsView.ToLocationID),
                                           };
            objDAL.RunProc("EC_Opr_LHPO_Get_Location_BranchId", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();            
            return objMessage;
        }

    }
}