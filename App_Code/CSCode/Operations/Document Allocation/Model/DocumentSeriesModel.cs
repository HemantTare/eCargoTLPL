using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.OperationView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for DocumentSeriesModel
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{

    public class DocumentSeriesModel : IModel
    {
        private IDocumentSeriesView objIDocumentSeriesView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;

        public DocumentSeriesModel(IDocumentSeriesView documentSeriesView)
        {
            objIDocumentSeriesView = documentSeriesView;

        }
        public DataSet FillValues()
        {
            objDAL.RunProc("EC_Opr_DocumentSeries_FillValues", ref objDS);
            Raj.EC.Common.SetTableName(new string[] { "DocumentType", "PrintedSeriesGrid" }, objDS);
            return objDS;

            return objDS;

        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Document_Series_Allocation_ID", SqlDbType.Int, 0, objIDocumentSeriesView.keyID)
                                           };
            objDAL.RunProc("EC_Opr_DocumentSeries_ReadValues", objSqlParam, ref objDS);
            return objDS;

        }
        public DataSet FillVA()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@BranchID", SqlDbType.Int, 0, objIDocumentSeriesView.BranchID)
                                           };
            objDAL.RunProc("EC_Opr_DocumentSeries_FillVAOnBranchChange", objSqlParam, ref objDS);
            return objDS;

        }

        public void GetMinMaxNumber()
        {

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Child_Min_Start_No", SqlDbType.Int, 0),                                             
                                          objDAL.MakeOutParams("@Child_Max_End_No", SqlDbType.Int, 0),     
                                          objDAL.MakeOutParams("@Parent_Start_No", SqlDbType.Int, 0),                                             
                                          objDAL.MakeOutParams("@Parent_End_No", SqlDbType.Int, 0),                            
                                            objDAL.MakeInParams("@ID",SqlDbType.Int,0,objIDocumentSeriesView.keyID),
                                            objDAL.MakeInParams("@va_ID",SqlDbType.Int,0,objIDocumentSeriesView.VAID),
                                            objDAL.MakeInParams("@CallFrom",SqlDbType.Int,0,3)
                                            
                                            };

            objDAL.RunProc("EC_Opr_Allocation_Get_Min_Max_Number", objSqlParam);
            objIDocumentSeriesView.MinStartNo = Util.String2Int(objSqlParam[0].Value.ToString());
            objIDocumentSeriesView.MaxEndNo = Util.String2Int(objSqlParam[1].Value.ToString());
            objIDocumentSeriesView.ParentStartNo = Util.String2Int(objSqlParam[2].Value.ToString());
            objIDocumentSeriesView.ParentEndNo = Util.String2Int(objSqlParam[3].Value.ToString());
        }
        public bool CheckDuplicate()
        {

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Is_duplicate", SqlDbType.Bit, 0),                                             
                                            objDAL.MakeInParams("@ID",SqlDbType.Int,0,objIDocumentSeriesView.keyID),
                                            objDAL.MakeInParams("@Document_ID",SqlDbType.Int,0,objIDocumentSeriesView.DocumentTypeID),
                                            objDAL.MakeInParams("@Previous_ID",SqlDbType.Int,0,objIDocumentSeriesView.PrintedSeriesID),
                                            objDAL.MakeInParams("@Start_Number",SqlDbType.Int,0,objIDocumentSeriesView.StartNo),
                                            objDAL.MakeInParams("@End_Number",SqlDbType.Int,0,objIDocumentSeriesView.EndNo),
                                            objDAL.MakeInParams("@Document_Allocation_Type_ID",SqlDbType.Int,0,3)
                                            
                                            };

            objDAL.RunProc("EC_Opr_Allocation_Duplicate_checking", objSqlParam);

            return Util.String2Bool(objSqlParam[0].Value.ToString());
        }
        public DataSet FillGrid()
        {
            SqlParameter[] objSqlParam = { 
                objDAL.MakeInParams("@va_id", SqlDbType.Int, 0, objIDocumentSeriesView.VAID),
                objDAL.MakeInParams("@Series_Printing_ID", SqlDbType.Int, 0, objIDocumentSeriesView.PrintedSeriesID),
                objDAL.MakeInParams("@Document_Series_Allocation_ID", SqlDbType.Int, 0, objIDocumentSeriesView.keyID) 
                                    
                                           };
            objDAL.RunProc("EC_Opr_DocumentSeries_FillGrid", objSqlParam, ref objDS);
            return objDS;

        }
        public DataSet FillGridAdd()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Document_ID", SqlDbType.Int, 0, objIDocumentSeriesView.DocumentTypeID)
                                        };
            objDAL.RunProc("EC_Opr_DocumentSeries_FillGridAdd", objSqlParam, ref objDS);
            return objDS;

        }
        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                                            objDAL.MakeInParams("@Document_Series_Allocation_ID",SqlDbType.Int,0,objIDocumentSeriesView.keyID),
                                            objDAL.MakeInParams("@Date_Of_Allocation",SqlDbType.DateTime,0,objIDocumentSeriesView.DateofAllocation),
                                            objDAL.MakeInParams("@Branch_ID",SqlDbType.Int,0,objIDocumentSeriesView.BranchID),   
                                            objDAL.MakeInParams("@Area_ID",SqlDbType.Int,0,objIDocumentSeriesView.AreaID),   
                                            objDAL.MakeInParams("@Region_ID",SqlDbType.Int,0,objIDocumentSeriesView.RegionID), 
                                            objDAL.MakeInParams("@Is_HO",SqlDbType.Bit,0,objIDocumentSeriesView.Is_HO),            
                                            objDAL.MakeInParams("@Document_ID",SqlDbType.Int,0,objIDocumentSeriesView.DocumentTypeID),
                                            objDAL.MakeInParams("@Series_Printing_ID",SqlDbType.Int,0,objIDocumentSeriesView.PrintedSeriesID),
                                            objDAL.MakeInParams("@Start_No",SqlDbType.Int,0,objIDocumentSeriesView.StartNo),
                                            objDAL.MakeInParams("@End_No",SqlDbType.Int,0,objIDocumentSeriesView.EndNo),
                                            objDAL.MakeInParams("@VA_ID",SqlDbType.Int,0,objIDocumentSeriesView.VAID),                                                                                                               
                                            objDAL.MakeInParams("@Balance",SqlDbType.Int,0,objIDocumentSeriesView.Balance),
                                            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,_userID)
                                            };


            objDAL.RunProc("EC_Opr_DocumentSeries_Save", objSqlParam);


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