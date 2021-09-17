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
using ClassLibraryMVP.General;
using Raj.CRM.TransactionView ;
using ClassLibraryMVP;
//using Raj.eCargo.Init;
using Raj.CRM.TransactionsModel;

/// <summary>
/// Summary description for Complaint_AssignmentModel
/// </summary>
namespace Raj.CRM.TransactionModel
{
    public class Complaint_AssignmentModel : IModel
    {

        private DataSet _ds;
        private IComplaint_AssignmentView objComplaint_AssignmentView;
        private DAL _objDAL = new DAL();

        private int _userID = Convert.ToInt32(UserManager.getUserParam().UserId);

        private Boolean Is_VT = true;//Convert.ToBoolean(UserManager.getUserParam().Is_VT);

        public Complaint_AssignmentModel(IComplaint_AssignmentView Complaint_AssignmentView)
        {
            objComplaint_AssignmentView = Complaint_AssignmentView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { 
                                          _objDAL.MakeInParams("@Ticket_ID", SqlDbType.Int , 0, objComplaint_AssignmentView.keyID),
                                          _objDAL.MakeInParams("@User_Id", SqlDbType.Int,0, _userID)
                                         };
            _objDAL.RunProc("EC_CRM_Trn_Complaint_Assign_ReadValues", objSqlParam, ref _ds);
            return _ds;
        }

        public DataSet FillOnSearchByChanged()
        {
            SqlParameter[] objSqlParam = {  _objDAL.MakeInParams("@Search_By_Flag", SqlDbType.VarChar,10, objComplaint_AssignmentView.SearchById)};

            _objDAL.RunProc("EC_CRM_Trn_Complaint_Assignment_Fill_User_Selection_Criteria", objSqlParam, ref _ds);
            return _ds;
        }

        public DataSet FillOnAddClick()
        {
            SqlParameter[] objSqlParam = {  _objDAL.MakeInParams("@SearchByFlag", SqlDbType.VarChar,10, objComplaint_AssignmentView.SearchById),
                                            _objDAL.MakeInParams("@FilterId", SqlDbType.Int,0, objComplaint_AssignmentView.FilterById),
                                            _objDAL.MakeInParams("@Ticket_ID", SqlDbType.VarChar, 0,objComplaint_AssignmentView.keyID),
                                            _objDAL.MakeInParams("@User_Id", SqlDbType.Int,0, _userID),
                                            _objDAL.MakeInParams("@XML_User", SqlDbType.Xml , 0,objComplaint_AssignmentView.XMLAllUser)};

            _objDAL.RunProc("EC_CRM_Trn_Complaint_Assign_FillOnSearchByChanged", objSqlParam, ref _ds);
            return _ds;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {  _objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                            _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                            _objDAL.MakeInParams("@Ticket_Id", SqlDbType.Int, 0, objComplaint_AssignmentView.keyID),                                                                                
                                            _objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,(int)UserManager.getUserParam().YearCode ),                                                                                                                        
                                            _objDAL.MakeInParams("@User_Xml", SqlDbType.Xml , 0,objComplaint_AssignmentView.XML_User),
                                            _objDAL.MakeInParams("@Assigned_On", SqlDbType.DateTime, 0,DateTime.Now),                                        
                                            _objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, _userID)};
            
            _objDAL.RunProc("EC_CRM_Trn_Complaint_Assign_Users_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);
 
              if (objMessage.messageID == 0)
                {
                    Message objMessage1 = new Message();
                    AttachmentsModel objAttachmentsModel = new AttachmentsModel();
                    objMessage1 = objAttachmentsModel.Save(objComplaint_AssignmentView.keyID, _userID, false);  //parameter false added by Ankit champaneriya

                    if (objMessage1.messageID == 0)
                    {
                        string _Msg;
                        _Msg = "Ticket Assgined to Users SuccessFully";
                        System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                    }
                }

               objComplaint_AssignmentView.errorMessage = objMessage.message;         
             return objMessage;
        }
    }
}




