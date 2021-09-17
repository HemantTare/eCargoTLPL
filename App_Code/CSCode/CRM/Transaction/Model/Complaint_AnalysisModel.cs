
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
using Raj.CRM.TransactionView;


/// <summary>
/// Summary description for Complaint_AnalysisModel
/// </summary>

namespace Raj.CRM.TransactionModel
{
    public class Complaint_AnalysisModel : IModel
    {

        private DataSet _ds;
        private IComplaint_AnalysisView _Complaint_AnalysisView;
        private DAL _objDAL = new DAL();
        
        public Complaint_AnalysisModel(IComplaint_AnalysisView Complaint_AnalysisView)
        {
            _Complaint_AnalysisView = Complaint_AnalysisView;
        }
        
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {_objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                        _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),                                        
                                        _objDAL.MakeInParams("@Ticket_Id", SqlDbType.Int, 0,_Complaint_AnalysisView.Ticket_ID  ),                                                                             
                                        _objDAL.MakeInParams("@Person_Responsible", SqlDbType.VarChar , 0,_Complaint_AnalysisView.Person_Responsible  ),
                                        _objDAL.MakeInParams("@Action_Taken", SqlDbType.VarChar , 0,_Complaint_AnalysisView.Action_Taken  ),                                        
                                        _objDAL.MakeInParams("@Complaint_Analysis_Xml", SqlDbType.Xml , 0,_Complaint_AnalysisView.Complaint_Analysis_Xml    ),                                                                                
                                        _objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, Convert.ToInt32(UserManager.getUserParam().UserId))};
            
            _objDAL.RunProc("EC_CRM_Trn_ComplaintAnalysisSave", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Ticket Analysed SuccessFully";

                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
            return objMessage;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { _objDAL.MakeInParams("@Ticket_ID", SqlDbType.VarChar, 100, Convert.ToString( _Complaint_AnalysisView.Ticket_ID   ))};

            _objDAL.RunProc("EC_CRM_Trn_Ticket_Details_Get", objSqlParam, ref _ds);
            return _ds;
        }

        public DataSet Read_Complaint_Analysis_Details()
        {
            SqlParameter[] objSqlParam = { _objDAL.MakeInParams("@Ticket_Id", SqlDbType.VarChar, 100, Convert.ToString(_Complaint_AnalysisView.Ticket_ID  ))                                         };

            _objDAL.RunProc("EC_CRM_Trn_Complaint_Analysis_Read_Value", objSqlParam, ref _ds);
            return _ds;
        }

        public DataSet Get_Region_For_Fault()
        {
            _objDAL.RunProc("EC_CRM_Trn_Fill_Region_For_Fault", ref _ds);
            return _ds;
        }
    }
}




