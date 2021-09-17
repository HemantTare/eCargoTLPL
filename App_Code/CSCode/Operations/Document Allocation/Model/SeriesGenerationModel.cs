using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.OperationView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
 
/// <summary>
/// Summary description for SeriesGenerationModel
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{ 
    public class SeriesGenerationModel:IModel 
    {
        private ISeriesGenerationView objISeriesGenerationView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;
        public SeriesGenerationModel(ISeriesGenerationView seriesGenerationView)
        {
            objISeriesGenerationView = seriesGenerationView;

        }
        public DataSet FillValues()
        {
            objDAL.RunProc("EC_Opr_SeriesGeneration_FillValues", ref objDS);          
            return objDS;

        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Series_Generation_ID", SqlDbType.Int, 0, objISeriesGenerationView.keyID)
                                           };
            objDAL.RunProc("EC_Opr_SeriesGeneration_ReadValues", objSqlParam, ref objDS);
            return objDS;

        }
        public void GetMinMaxNumber()
        {

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Child_Min_Start_No", SqlDbType.Int, 0),                                             
                                          objDAL.MakeOutParams("@Child_Max_End_No", SqlDbType.Int, 0),     
                                          objDAL.MakeOutParams("@Parent_Start_No", SqlDbType.Int, 0),                                             
                                          objDAL.MakeOutParams("@Parent_End_No", SqlDbType.Int, 0),                                                                           
                                            objDAL.MakeInParams("@ID",SqlDbType.Int,0,objISeriesGenerationView.keyID),
                                            objDAL.MakeInParams("@CallFrom",SqlDbType.Int,0,1)
                                            
                                            };

            objDAL.RunProc("EC_Opr_Allocation_Get_Min_Max_Number", objSqlParam);
            objISeriesGenerationView.MinStartNo = Util.String2Int(objSqlParam[0].Value.ToString());
            objISeriesGenerationView.MaxEndNo = Util.String2Int(objSqlParam[1].Value.ToString());
        }
        public bool CheckDuplicate()
        {

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Is_duplicate", SqlDbType.Bit, 0),                                             
                                            objDAL.MakeInParams("@ID",SqlDbType.Int,0,objISeriesGenerationView.keyID),
                                            objDAL.MakeInParams("@Document_ID",SqlDbType.Int,0,objISeriesGenerationView.DocumentTypeID),
                                            objDAL.MakeInParams("@Previous_ID",SqlDbType.Int,0,0),
                                            objDAL.MakeInParams("@Start_Number",SqlDbType.Int,0,objISeriesGenerationView.StartNo),
                                            objDAL.MakeInParams("@End_Number",SqlDbType.Int,0,objISeriesGenerationView.EndNo),
                                            objDAL.MakeInParams("@Document_Allocation_Type_ID",SqlDbType.Int,0,1)
                                            
                                            };

            objDAL.RunProc("EC_Opr_Allocation_Duplicate_checking", objSqlParam);

            return Util.String2Bool(objSqlParam[0].Value.ToString());
        }

        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                                            objDAL.MakeInParams("@Series_Generation_ID",SqlDbType.Int,0,objISeriesGenerationView.keyID),
                                            objDAL.MakeInParams("@Document_ID",SqlDbType.Int,0,objISeriesGenerationView.DocumentTypeID),
                                            objDAL.MakeInParams("@Generation_Date",SqlDbType.DateTime,0,objISeriesGenerationView.GeneratedDate),
                                            objDAL.MakeInParams("@Start_No",SqlDbType.Int,0,objISeriesGenerationView.StartNo),
                                            objDAL.MakeInParams("@End_No",SqlDbType.Int,0,objISeriesGenerationView.EndNo),
                                            objDAL.MakeInParams("@Balance",SqlDbType.Int,0,objISeriesGenerationView.Balance),
                                            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,_userID)
                                            };

            objDAL.RunProc("EC_Opr_SeriesGeneration_Save", objSqlParam);
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