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
using Raj.CRM.MasterView;

namespace Raj.CRM.MasterModel
{
    public class EscalationMatrixModel : IModel
    {
        private IEscalationMatrixView objIEscalationMatrixView;
        private DataSet objDS;
        private DAL objDAL = new DAL();
        private int _userId = Convert.ToInt32(UserManager.getUserParam().UserId);
 
        public EscalationMatrixModel(IEscalationMatrixView escalationMatrixView)
        {
            objIEscalationMatrixView = escalationMatrixView;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={objDAL.MakeInParams("@ComplaintNatureId", SqlDbType.Int, 0, objIEscalationMatrixView.ComplaintNatureId)};
            objDAL.RunProc("EC_CRM_Mst_EscalationMatrix_Readvalues", objSqlParam, ref objDS);
            return objDS;
        }
        public DataSet FillValues()
        {
            objDAL.RunProc("EC_CRM_Mst_EscalationMatrix_FillValues",  ref objDS);
            return objDS;
        }
        public DataSet FillProfileValues()
        {
            objDAL.RunProc("EC_CRM_Mst_EscalationMatrix_FillProfileValues", ref objDS);
            return objDS;
        }

        public DataSet FillUserValues()
        {
            SqlParameter[] objSqlParam ={ 
                                          objDAL.MakeInParams("@ComplaintNatureId",SqlDbType.Int,0,objIEscalationMatrixView.ComplaintNatureId)
                                          };
            objDAL.RunProc("EC_CRM_Trn_IncludeExclude_FillUserValues", objSqlParam, ref objDS);
            return objDS;
        }
        public Message Save()
        {
            string _strXML;
            string _UserDetailXml;
            objDS = objIEscalationMatrixView.SessionEscalationMatrixGrid;
            _strXML = objDS.GetXml();
            DataSet objDS1=objIEscalationMatrixView.SessionUserDetails;
            objDS1.Tables[0].TableName = "IncludeExcludeUserDetails";
            _UserDetailXml=objDS1.GetXml();
            Message objMsg = new Message();
            SqlParameter[] objSqlParam ={objDAL.MakeOutParams("@Error_Code",SqlDbType.Int,0),
                                    objDAL.MakeOutParams("@Error_Desc",SqlDbType.VarChar,4000),
                                    objDAL.MakeInParams("@ComplaintNatureID",SqlDbType.Int,0,objIEscalationMatrixView.ComplaintNatureId),
                                    objDAL.MakeInParams("@EscalationMatrix",SqlDbType.Xml,5000,_strXML),
                                    objDAL.MakeInParams("@EscalationMatrixDetails",SqlDbType.Xml,5000, _UserDetailXml),                  
                                  };

            objDAL.RunProc("EC_CRM_Mst_EscalationMatrix_Save", objSqlParam);

            objMsg.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMsg.message = Convert.ToString(objSqlParam[1].Value.ToString());

            if (objMsg.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";

                int MenuItemId = Raj.EC.Common.GetMenuItemId();
                string Mode = HttpContext.Current.Request.QueryString["Mode"];
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("CRM/Masters/FrmEscalationMatrix.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
            }
            return objMsg;
        }

    }
}

