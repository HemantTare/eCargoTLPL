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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationView;

/// <summary>
/// Summary description for ALSModel
/// </summary>
namespace Raj.EC.OperationModel
{
    public class ALSModel : IModel
    {
        private IALSView objIALSView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        private int _branchID = UserManager.getUserParam().MainId;
        private bool _isacctransferreq = CompanyManager.getCompanyParam().IsAccountTransferRequired;

        public ALSModel(IALSView ALSView)
        {
            objIALSView = ALSView;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Opr_ALS_FillValues", ref objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0, _branchID),
            objDAL.MakeInParams("@ALS_Id", SqlDbType.Int, 0, objIALSView.keyID) ,
            objDAL.MakeInParams("@IsAccTransferReq", SqlDbType.Bit, 0, _isacctransferreq) ,
            objDAL.MakeInParams("@GetGCXML", SqlDbType.Xml, 0, objIALSView.GetGCNoXML) ,
            objDAL.MakeInParams("@ALS_Date", SqlDbType.DateTime, 0, objIALSView.ALSDate)};

            objDAL.RunProc("dbo.EC_Opr_ALS_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }             

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeOutParams("@Print_Doc_ID", SqlDbType.Int, 0),                
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int, 0,UserManager.getUserParam().DivisionId),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int, 0,UserManager.getUserParam().YearCode),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, UserManager.getUserParam().HierarchyCode),
            objDAL.MakeInParams("@Menu_Item_ID", SqlDbType.Int, 0,Raj.EC.Common.GetMenuItemId()),
            objDAL.MakeInParams("@ALS_Id", SqlDbType.Int, 0,objIALSView.keyID),
            objDAL.MakeInParams("@ALS_Date", SqlDbType.DateTime,0,objIALSView.ALSDate),
            objDAL.MakeInParams("@ALS_Branch_Id", SqlDbType.Int,0,_branchID),
            objDAL.MakeInParams("@Vehicle_Category_ID", SqlDbType.Int, 0,objIALSView.VehicleCotegoryID),
            objDAL.MakeInParams("@Vehicle_ID", SqlDbType.Int, 0,objIALSView.VehicleID),
            objDAL.MakeInParams("@Supervisor_Id", SqlDbType.Int, 0,objIALSView.SupervisorID),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, UserManager.getUserParam().UserId),
            objDAL.MakeInParams("@Remarks", SqlDbType.VarChar, 250,objIALSView.Remarks),
            objDAL.MakeInParams("@ALSDetailsXML",SqlDbType.Xml,0,objIALSView.ALSDetailsXML)};

            objDAL.RunProc("dbo.EC_Opr_ALS_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                objIALSView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";
                if (objIALSView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Outward/FrmALS.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objIALSView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                else if (objIALSView.Flag == "SaveAndPrint")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    int Document_ID = Convert.ToInt32(objSqlParam[2].Value);
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Reports/Direct_Printing/FrmCommonReportViewer.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode + "&Document_ID=" + ClassLibraryMVP.Util.EncryptInteger(Document_ID)));
                }
            }

            return objMessage;
        }
    }
}

