using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.OperationView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for LHPOPenaltiesModel
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{

    public class LHPOPenaltiesModel : IModel
    {
        private ILHPOPenaltiesView objILHPOPenaltiesView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;

        public LHPOPenaltiesModel(ILHPOPenaltiesView lHPOPenaltiesView)
        {
            objILHPOPenaltiesView = lHPOPenaltiesView;

        }
        public DataSet FillValues()
        {
            objDAL.RunProc("EC_Opr_SeriesGeneration_FillValues", ref objDS);
            return objDS;

        }

        public DataSet ReadValues()
        {
            // SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Series_Generation_ID", SqlDbType.Int, 0, objISeriesGenerationView.keyID)
            //                               };
            // objDAL.RunProc("EC_Opr_SeriesGeneration_ReadValues", objSqlParam, ref objDS);
            return objDS;

        }
        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                                            objDAL.MakeInParams("@Series_Generation_ID",SqlDbType.Int,0,objILHPOPenaltiesView.keyID),
                                            //objDAL.MakeInParams("@Document_ID",SqlDbType.Int,0,objISeriesGenerationView.DocumentTypeID),
                                            //objDAL.MakeInParams("@Generation_Date",SqlDbType.DateTime,0,objISeriesGenerationView.GeneratedDate),
                                            //objDAL.MakeInParams("@Start_No",SqlDbType.Int,0,objISeriesGenerationView.StartNo),
                                            //objDAL.MakeInParams("@End_No",SqlDbType.Int,0,objISeriesGenerationView.EndNo),
                                            //objDAL.MakeInParams("@Balance",SqlDbType.Int,0,objISeriesGenerationView.Balance),
                                            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,_userID)
                                            };

            //objDAL.RunProc("EC_Opr_SeriesGeneration_Save", objSqlParam);
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