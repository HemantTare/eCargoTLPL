using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.CRM.TransactionView;

/// <summary>
/// Summary description for IncludeExcludeModel
/// </summary>
namespace Raj.CRM.TransactionModel
{
    public class IncludeExcludeModel : IModel
    {
        private IIncludeExcludeView objIIncludeExcludeView;
        private DataSet objDS;
        private DAL objDAL = new DAL();
        private int _userId = UserManager.getUserParam().UserId;
            
        public IncludeExcludeModel(IIncludeExcludeView includeExcludeView)
        {
            objIIncludeExcludeView = includeExcludeView;
        }

        public DataSet FillGrid()
        {
            SqlParameter[] objSqlParam ={
                                          
                                          objDAL.MakeInParams("@ProfileId", SqlDbType.Int, 0, objIIncludeExcludeView.ProfileId)
                                        
                                          };          
                objDAL.RunProc("EC_CRM_Trn_IncludeExclude_FillGridValues", objSqlParam, ref objDS);
                       
            return objDS;
        }

        public DataSet FillLabelValues()
        {
            SqlParameter[] objSqlParam = {
                                            objDAL.MakeInParams("@ComplaintNatureId",SqlDbType.Int,0, objIIncludeExcludeView.ComplaintNatureId),
                                            objDAL.MakeInParams("@ProfileId", SqlDbType.Int,0,objIIncludeExcludeView.ProfileId)
                                          
                                           };
            objDAL.RunProc("EC_CRM_Trn_IncludeExclueUser_FillLabelValues", objSqlParam, ref objDS);

            return objDS;
        }


        public Message Save()
        {          
            Message objMsg = new Message();
            //SqlParameter[] objSqlParam ={objDAL.MakeOutParams("@Error_Code",SqlDbType.Int,0),
            //                        objDAL.MakeOutParams("@Error_Desc",SqlDbType.VarChar,4000)
            //                        //objDAL.MakeInParams("@ProfileId",SqlDbType.Int,0,objIIncludeExcludeView.EscalationMatrixView.SessionProfile),
            //                          //objDAL.MakeInParams("@ComplaintNatureId",SqlDbType.Int,0,objIIncludeExcludeView.EscalationMatrixView.ComplaintNatureId),                                         
            //                       // objDAL.MakeInParams("@IncludeExcludeUser",SqlDbType.Xml,5000,objIIncludeExcludeView.IncludeExcludeUserGridDetails)                                    
            //                      };
            ////objDAL.RunProc("EC_CRM_Trn_IncludeExclude_Save", objSqlParam);

            //objMsg.messageID = Util.String2Int(objSqlParam[0].Value.ToString());
            //objMsg.message = objSqlParam[1].Value.ToString();
            return objMsg;

        }
        public DataSet ReadValues()
        {           
            return objDS;
        }

	}
}
