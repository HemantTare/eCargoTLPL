using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls; 
using Raj.eCargo.Operation.Muster.Model;
using Raj.eCargo.Operation.Muster.View;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using System.Data.SqlClient;

/// <summary>
/// Summary description for Operation_Muster_Daily_Entry_Model
/// </summary>
/// 
namespace Raj.eCargo.Operation.Muster.Model
{
    public class OperationMusterDailyEntryModel:IModel
    {
      private IOperationMusterDailyEntryView _objMusterEntryDialyView;
      private DAL _objDAL = new DAL();
      private DataSet _ds;
    
	    public OperationMusterDailyEntryModel(IOperationMusterDailyEntryView IMusterDialyView)
	    {
		     _objMusterEntryDialyView = IMusterDialyView;
	    }

        public DataSet ReadValues()
        {
            return _ds;
        }
        public DataSet GetValues(string HCode, int Main_Id, int Division_Id)
        {
       
            SqlParameter[] sqlPara =
                {
                           _objDAL.MakeInParams("@Hierarchy_Code",SqlDbType.VarChar,10,HCode),
                           _objDAL.MakeInParams("@Main_Id", SqlDbType.Int, 0,Main_Id),
                           _objDAL.MakeInParams("@Payroll_Div_ID",SqlDbType.Int,0,Division_Id),
                           
                           
                };
            _objDAL.RunProc("dbo.Payroll_Trans_Fill_Muster_Daily", sqlPara, ref _ds);
            return _ds;
        }
        public Message Save()
        {
            Message objMessage = new Message();
            string check = _objMusterEntryDialyView.SessionMusterDaily.GetXml();

            SqlParameter[] sqlPara = { 
                                            _objDAL.MakeOutParams("@Error_Code", SqlDbType.Int,0 ),   
                                            _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar,4000),
                                            _objDAL.MakeInParams("@XML",SqlDbType.Xml,0,_objMusterEntryDialyView.SessionCalculatedDS.GetXml())
                                           // _objDAL.MakeInParams("@YearMonth",SqlDbType.Int,0,200907)

                                       };
            _objDAL.RunProc("Payroll_Trans_Fill_Muster_Daily_Save", sqlPara);
            objMessage.messageID = Convert.ToInt32(sqlPara[0].Value.ToString());
            objMessage.message = Convert.ToString(sqlPara[1].Value);
            return objMessage;
        }
    }
}


