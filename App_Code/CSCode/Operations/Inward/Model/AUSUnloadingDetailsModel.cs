using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.OperationView;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;

/// <summary>
/// Name : Ankit champaneriya
/// Date : 27-10-08
/// Summary description for AUSUnloadingDetailsModel
/// </summary>
/// 

namespace Raj.EC.OperationModel
{
    public class AUSUnloadingDetailsModel : IModel
    {
        private IAUSUnloadingDetailsView objIAUSUnloadingDetailsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _divisionId = UserManager.getUserParam().DivisionId;

        public AUSUnloadingDetailsModel(IAUSUnloadingDetailsView AUSUnloadingDetailsView)
        {
            objIAUSUnloadingDetailsView = AUSUnloadingDetailsView  ;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Opr_AUS_FillValues", ref objDS);
            return objDS;
        }


        public void Get_VehicleDetails()
        {
            SqlParameter[] objSqlParam ={  
                                    objDAL.MakeOutParams("@Vehicle_Category_ID", SqlDbType.VarChar , 20 ),
                                    objDAL.MakeOutParams("@Vehicle_Category", SqlDbType.VarChar , 20 ),    
                                    objDAL.MakeOutParams("@NoofMinuteDifferenceForLate", SqlDbType.Int , 0 ),    
                                    objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objIAUSUnloadingDetailsView.VehicleSearchView.VehicleID  )};


            objDAL.RunProc("EC_Opr_AUS_Get_Vehicle_Details", objSqlParam, ref objDS);

            objIAUSUnloadingDetailsView.Vehicle_Category_Id  = Util.String2Int( objSqlParam[0].Value.ToString());
            objIAUSUnloadingDetailsView.Vehicle_Category   = objSqlParam[1].Value.ToString();
            objIAUSUnloadingDetailsView.NoofMinuteDifferenceForLate = Util.String2Int(objSqlParam[2].Value.ToString());

           /* SqlParameter[] objSqlTASParam ={  
                                    objDAL.MakeOutParams("@Vehicle_Category_ID", SqlDbType.VarChar , 20 ),
                                    objDAL.MakeOutParams("@Vehicle_Category", SqlDbType.VarChar , 20 ),    
                                    objDAL.MakeOutParams("@NoofMinuteDifferenceForLate", SqlDbType.Int , 0 ),                           
                                    objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objIAUSUnloadingDetailsView.VehicleSearchView.VehicleID  )};                         */
        }

        public DataSet Get_LHPO()
        {
            SqlParameter[] objSqlParam ={
                                    objDAL.MakeInParams("@AUS_Branch_ID", SqlDbType.Int  , 0, UserManager.getUserParam().MainId  ),                                    
                                    objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objIAUSUnloadingDetailsView.VehicleSearchView.VehicleID  ),
                                    objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionId),
                                    objDAL.MakeInParams("@AUS_Date", SqlDbType.DateTime  , 0, objIAUSUnloadingDetailsView.AUS_Date  )                                 
            };


           objDAL.RunProc("EC_opr_AUS_Get_LHPO", objSqlParam, ref objDS);

           return objDS;

        }

        //Added : Anita On:22 Jan 09
        public DataSet Get_TAS()
        {
            SqlParameter[] objSqlParam ={
                                    objDAL.MakeInParams("@AUS_Branch_ID", SqlDbType.Int  , 0, UserManager.getUserParam().MainId  ),                                    
                                    objDAL.MakeInParams("@Vehicle_Id", SqlDbType.Int, 0, objIAUSUnloadingDetailsView.VehicleSearchView.VehicleID  ),
                                    objDAL.MakeInParams("@Division_Id", SqlDbType.Int, 0, _divisionId),
                                    objDAL.MakeInParams("@AUS_Date", SqlDbType.DateTime  , 0, objIAUSUnloadingDetailsView.AUS_Date  )};

            objDAL.RunProc("EC_opr_AUS_Get_TAS", objSqlParam, ref objDS);            

            return objDS;
        }

        //Added : Anita On:22 Jan 09
        public DataSet Get_TASDetails()
        {
            SqlParameter[] objSqlParam ={   objDAL.MakeInParams("@TAS_Id", SqlDbType.Int, 0,objIAUSUnloadingDetailsView.TAS_Id)};

            objDAL.RunProc("EC_opr_AUS_Get_TAS_Details", objSqlParam, ref objDS);

            return objDS;
        }

        public DataSet Get_LHPODetails()
        {
            SqlParameter[] objSqlParam ={   objDAL.MakeOutParams("@LHPO_Date", SqlDbType.VarChar  , 20 ) ,
                                            objDAL.MakeOutParams("@Schedule_Arrival_Delivery_Date", SqlDbType.VarChar  , 20 ) ,
                                            objDAL.MakeOutParams("@Schedule_Arrival_Delivery_Time", SqlDbType.VarChar  , 20 ) ,
                                            objDAL.MakeOutParams("@Total_Booking_Articles", SqlDbType.Int   , 0 ) ,
                                            objDAL.MakeOutParams("@Total_Booking_Articles_Wt", SqlDbType.Int , 0 ) ,
                                            objDAL.MakeOutParams("@Total_Loaded_Articles", SqlDbType.Int , 0 ) ,
                                            objDAL.MakeOutParams("@Total_Loaded_Articles_Wt", SqlDbType.Int , 0 ) ,
                                            objDAL.MakeOutParams("@Total_Received_Articles", SqlDbType.Int , 0 ) ,
                                            objDAL.MakeOutParams("@Total_Received_Articles_Wt", SqlDbType.Int , 0 ) ,
                                            objDAL.MakeOutParams("@Total_Damage_Leakage_Articles", SqlDbType.Int , 20 ) ,
                                            objDAL.MakeOutParams("@Total_Damage_Leakage_Value", SqlDbType.Int , 0 ) ,
                                            objDAL.MakeOutParams("@Delivery_Commision", SqlDbType.Decimal  , 0), 
                                            objDAL.MakeOutParams("@To_Pay_Collection", SqlDbType.Decimal  , 0),
                                            objDAL.MakeOutParams("@Delivery_Receivable",SqlDbType.Decimal,0),
                                            objDAL.MakeOutParams("@Service_Charges_Payable",SqlDbType.Decimal,0),
                                            objDAL.MakeInParams("@AUS_Branch_ID", SqlDbType.Int  , 0, UserManager.getUserParam().MainId  ),                                    
                                            objDAL.MakeInParams("@LHPO_Id", SqlDbType.Int, 0, objIAUSUnloadingDetailsView.LHPO_Id  ),
                                            objDAL.MakeInParams("@TAS_Id", SqlDbType.Int, 0,objIAUSUnloadingDetailsView.TAS_Id),
                                            objDAL.MakeInParams("@menuitem_id",SqlDbType.Int, 0, objIAUSUnloadingDetailsView.MenuItemId),
                                            objDAL.MakeOutParams("@LHPOFromLocation",SqlDbType.VarChar,20),
                                            objDAL.MakeOutParams("@LHPOToLocation",SqlDbType.VarChar,20),
                                            objDAL.MakeOutParams("@BTHAmount",SqlDbType.Decimal,0),
                                            objDAL.MakeOutParams("@UpCountry_Receivable",SqlDbType.Decimal,0),
                                            objDAL.MakeOutParams("@UpCountry_Crssing_Cost_Payable",SqlDbType.Decimal,0)
                                            };


            objDAL.RunProc("EC_opr_AUS_Get_Memo_GC_Details", objSqlParam, ref objDS);

            objIAUSUnloadingDetailsView.LHPO_Date  = objSqlParam[0].Value.ToString();
            objIAUSUnloadingDetailsView.ScheduledArivalDate = objSqlParam[1].Value.ToString();
            objIAUSUnloadingDetailsView.ScheduledArivalTime = objSqlParam[2].Value.ToString();
            objIAUSUnloadingDetailsView.Total_To_Pay_Collection = Util.String2Decimal(objSqlParam[13].Value.ToString());
            objIAUSUnloadingDetailsView.Total_Delivery_Commision = Util.String2Decimal(objSqlParam[14].Value.ToString());
            objIAUSUnloadingDetailsView.LHPOFromLocation = objSqlParam[19].Value.ToString();
            objIAUSUnloadingDetailsView.LHPOToLocation = objSqlParam[20].Value.ToString();
            objIAUSUnloadingDetailsView.BTHAmount = Util.String2Decimal(objSqlParam[21].Value.ToString());
            if (objIAUSUnloadingDetailsView.BTHAmount == -1)
            {
                objIAUSUnloadingDetailsView.BTHAmount = 0;
            }
            objIAUSUnloadingDetailsView.Lorry_Hire = Util.String2Decimal(objSqlParam[21].Value.ToString());
            objIAUSUnloadingDetailsView.UpCountryReceivable = Util.String2Decimal(objSqlParam[22].Value.ToString());
            objIAUSUnloadingDetailsView.UpcountryCrossingCost = Util.String2Decimal(objSqlParam[23].Value.ToString());


            //objIAUSUnloadingDetailsView.Total_Booking_Articles  =   Util.String2Int( objSqlParam[3].Value.ToString());
            //objIAUSUnloadingDetailsView.Total_Booking_Articles_Wt = Util.String2Int(objSqlParam[4].Value.ToString());

            //objIAUSUnloadingDetailsView.Total_Loaded_Articles   =  Util.String2Int( objSqlParam[5].Value.ToString());
            //objIAUSUnloadingDetailsView.Total_Loaded_Articles_Wt  =  Util.String2Int( objSqlParam[6].Value.ToString());

            //objIAUSUnloadingDetailsView.Total_Received_Articles   =  Util.String2Int( objSqlParam[7].Value.ToString());
            //objIAUSUnloadingDetailsView.Total_Received_Articles_Wt  =  Util.String2Int( objSqlParam[8].Value.ToString());

            //objIAUSUnloadingDetailsView.Total_Damage_Leakage_Articles  =  Util.String2Int( objSqlParam[9].Value.ToString());
            //objIAUSUnloadingDetailsView.Total_Damage_Leakage_Value  =  Util.String2Int( objSqlParam[10].Value.ToString());
            
            return objDS;

        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeOutParams("@Delivery_Commision", SqlDbType.Decimal  , 0), 
                                          objDAL.MakeOutParams("@To_Pay_Collection", SqlDbType.Decimal  , 0), 
                                        objDAL.MakeInParams("@Actual_Unloading_Sheet_ID", SqlDbType.Int, 0, objIAUSUnloadingDetailsView.keyID),
                                        objDAL.MakeOutParams("@UpCountry_Receivable",SqlDbType.Decimal,0),
                                        objDAL.MakeOutParams("@UpCountry_Crssing_Cost_Payable",SqlDbType.Decimal,0),
                                        objDAL.MakeOutParams("@Delivery_Receivable",SqlDbType.Decimal,0),
                                        objDAL.MakeOutParams("@Service_Charges_Payable",SqlDbType.Decimal,0)
            };


            objDAL.RunProc("dbo.EC_Opr_AUS_ReadValues", objSqlParam, ref objDS);

            objIAUSUnloadingDetailsView.Total_Delivery_Commision = Util.String2Decimal( objSqlParam[6].Value.ToString());
            objIAUSUnloadingDetailsView.To_Pay_Collection = Util.String2Decimal(objSqlParam[5].Value.ToString());
            objIAUSUnloadingDetailsView.UpCountryReceivable = Util.String2Decimal(objSqlParam[3].Value.ToString());
            objIAUSUnloadingDetailsView.UpcountryCrossingCost = Util.String2Decimal(objSqlParam[4].Value.ToString());

            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            return objMessage;
        }
    }
}