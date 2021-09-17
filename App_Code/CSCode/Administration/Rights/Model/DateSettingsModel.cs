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
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.AdminView;

/// <summary>
/// Summary description for DateSettingsModel
/// </summary>
namespace Raj.EC.AdminModel
{
    class DateSettingsModel : IModel
    {
        private IDateSettingsView objIDateSettingsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
       
        public DateSettingsModel(IDateSettingsView dateSettingsView)
        {
            objIDateSettingsView = dateSettingsView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] SqlPara ={
            objDAL.MakeInParams("@ProcessId",SqlDbType.Int,0,objIDateSettingsView.keyID)};
            objDAL.RunProc("EC_Adm_Datesettings_ReadValues", SqlPara, ref objDS);
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@ErrorCode", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@ErrorDesc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@ProcessId",SqlDbType.Int,0, objIDateSettingsView.keyID),
                                               objDAL.MakeInParams("@ProcessName", SqlDbType.VarChar,100,objIDateSettingsView.ProcessName), 
                                               objDAL.MakeInParams("@Code", SqlDbType.VarChar,50, objIDateSettingsView.Code),
                                               objDAL.MakeInParams("@MinHrs", SqlDbType.Int, 0, objIDateSettingsView.MinHrs),
                                               objDAL.MakeInParams("@MaxHrs", SqlDbType.Int,0,  objIDateSettingsView.MaxHrs)
                                              
                                         };


            objDAL.RunProc("EC_Adm_Datesettings_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            //if (objMessage.messageID == 0)
            //{

            //    string _Msg;
            //    _Msg = "Saved SuccessFully";
            //    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            //}

            //return objMessage;

            

            return objMessage;
        }

	}
}
