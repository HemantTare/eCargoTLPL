using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.OperationView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for PrintingStationaryModel
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{

    public class PrintingStationaryModel : IModel
    {
        private IPrintingStationaryView objIPrintingStationaryView;
        private DAL objDAL = new DAL();
        private DataSet objDS;        
        private int _userID=UserManager.getUserParam().UserId;
        public PrintingStationaryModel(IPrintingStationaryView printingStationaryView)
        {
            objIPrintingStationaryView = printingStationaryView;

        }
        public DataSet FillValues()
        {
            objDAL.RunProc("EC_Opr_PrintingStationary_FillValues", ref objDS);
            Raj.EC.Common.SetTableName(new string[] {"DocumentType","GeneratedSeriesGrid" }, objDS);
            return objDS;

        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Series_Printing_ID", SqlDbType.Int, 0, objIPrintingStationaryView.keyID)
                                           };
            objDAL.RunProc("EC_Opr_PrintingStationary_ReadValues", objSqlParam, ref objDS);
            return objDS;

        }
        public DataSet FillGrid()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Series_Generation_ID", SqlDbType.Int, 0, objIPrintingStationaryView.SeriesGenerationID),
                                           objDAL.MakeInParams("@Series_Printing_ID", SqlDbType.Int, 0, objIPrintingStationaryView.keyID) 
                                    
                                           };
            objDAL.RunProc("EC_Opr_PrintingStationary_FillGrid", objSqlParam, ref objDS);
            return objDS;

        }
        public DataSet FillGridAdd()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Document_ID", SqlDbType.Int, 0, objIPrintingStationaryView.DocumentTypeID)
                                        };
            objDAL.RunProc("EC_Opr_PrintingStationary_FillGridAdd", objSqlParam, ref objDS);
            return objDS;

        }
        
        public bool CheckDuplicate()
        {

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Is_duplicate", SqlDbType.Bit, 0),                                             
                                            objDAL.MakeInParams("@ID",SqlDbType.Int,0,objIPrintingStationaryView.keyID),
                                            objDAL.MakeInParams("@Document_ID",SqlDbType.Int,0,objIPrintingStationaryView.DocumentTypeID),
                                            objDAL.MakeInParams("@Previous_ID",SqlDbType.Int,0,objIPrintingStationaryView.SeriesGenerationID),
                                            objDAL.MakeInParams("@Start_Number",SqlDbType.Int,0,objIPrintingStationaryView.StartNo),
                                            objDAL.MakeInParams("@End_Number",SqlDbType.Int,0,objIPrintingStationaryView.EndNo),
                                            objDAL.MakeInParams("@Document_Allocation_Type_ID",SqlDbType.Int,0,2)
                                            
                                            };

            objDAL.RunProc("EC_Opr_Allocation_Duplicate_checking", objSqlParam);

            return Util.String2Bool(objSqlParam[0].Value.ToString());
        }
        public void GetMinMaxNumber()
        {

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Child_Min_Start_No", SqlDbType.Int, 0),                                             
                                          objDAL.MakeOutParams("@Child_Max_End_No", SqlDbType.Int, 0),     
                                          objDAL.MakeOutParams("@Parent_Start_No", SqlDbType.Int, 0),                                             
                                          objDAL.MakeOutParams("@Parent_End_No", SqlDbType.Int, 0),                                                
                                            objDAL.MakeInParams("@ID",SqlDbType.Int,0,objIPrintingStationaryView.keyID),
                                            objDAL.MakeInParams("@CallFrom",SqlDbType.Int,0,2)
                                            
                                            };

            objDAL.RunProc("EC_Opr_Allocation_Get_Min_Max_Number", objSqlParam);
            objIPrintingStationaryView.MinStartNo = Util.String2Int(objSqlParam[0].Value.ToString());
            objIPrintingStationaryView.MaxEndNo = Util.String2Int(objSqlParam[1].Value.ToString());
            objIPrintingStationaryView.ParentStartNo = Util.String2Int(objSqlParam[2].Value.ToString());
            objIPrintingStationaryView.ParentEndNo = Util.String2Int(objSqlParam[3].Value.ToString());
        }
        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                                            objDAL.MakeInParams("@Series_Printing_ID",SqlDbType.Int,0,objIPrintingStationaryView.keyID),
                                            objDAL.MakeInParams("@Document_ID",SqlDbType.Int,0,objIPrintingStationaryView.DocumentTypeID),
                                            objDAL.MakeInParams("@Date_Of_Printing",SqlDbType.DateTime,0,objIPrintingStationaryView.DateofPrinting),
                                            objDAL.MakeInParams("@Series_Generation_ID",SqlDbType.Int,0,objIPrintingStationaryView.SeriesGenerationID),
                                            objDAL.MakeInParams("@Start_No",SqlDbType.Int,0,objIPrintingStationaryView.StartNo),
                                            objDAL.MakeInParams("@End_No",SqlDbType.Int,0,objIPrintingStationaryView.EndNo),
                                            objDAL.MakeInParams("@Balance",SqlDbType.Int,0,objIPrintingStationaryView.Balance),
                                            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,_userID)
                                            };


            objDAL.RunProc("EC_Opr_PrintingStationary_Save", objSqlParam);


            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);


            if (objMessage.messageID == 0)
            {
                objIPrintingStationaryView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
            return objMessage;
        }


    }
}