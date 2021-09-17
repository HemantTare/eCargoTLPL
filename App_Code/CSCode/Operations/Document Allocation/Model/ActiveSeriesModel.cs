using System;
using System.Data;
using System.Data.SqlClient;
using Raj.EC.OperationView;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for ActiveSeriesModel
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{

    public class ActiveSeriesModel : IModel
    {
        private IActiveSeriesView objIActiveSeriesView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = UserManager.getUserParam().UserId;

        public ActiveSeriesModel(IActiveSeriesView activeSeriesView)
        {
            objIActiveSeriesView = activeSeriesView;
        }

        public DataSet FillValues()
        {
            objDAL.RunProc("EC_Opr_ActiveSeries_FillValues", ref objDS);
            Raj.EC.Common.SetTableName(new string[] { "DocumentType", "DocumentSeriesGrid" }, objDS);
            return objDS;
        }

        public DataSet ReadValues()
        {
           // SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Document_Series_Allocation_ID", SqlDbType.Int, 0, objIActiveSeriesView.keyID)
              //                             };
           // objDAL.RunProc("EC_Opr_ActiveSeries_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }
        public DataSet FillVA()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@MainID", SqlDbType.Int, 0, objIActiveSeriesView.MainID),
                                            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, objIActiveSeriesView.HierarchyCode)
                                           };
            objDAL.RunProc("EC_Opr_ActiveSeries_FillVAOnBranchChange", objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet FillGrid()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Main_ID", SqlDbType.Int, 0, objIActiveSeriesView.MainID),
                                           objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, objIActiveSeriesView.HierarchyCode),
                                           objDAL.MakeInParams("@Document_ID", SqlDbType.Int, 0, objIActiveSeriesView.DocumentTypeID),
                                           objDAL.MakeInParams("@VA_ID",SqlDbType.Int,0,objIActiveSeriesView.VAID) 
                                           };
            objDAL.RunProc("EC_Opr_ActiveSeries_FillGrid", objSqlParam, ref objDS);
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                                            objDAL.MakeInParams("@Document_Series_Allocation_ID",SqlDbType.Int,0,objIActiveSeriesView.DocumentSeriesID),                                            
                                            objDAL.MakeInParams("@Main_ID",SqlDbType.Int,0,objIActiveSeriesView.MainID),  
                                            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5, objIActiveSeriesView.HierarchyCode),
                                            objDAL.MakeInParams("@VA_ID",SqlDbType.Int,0,objIActiveSeriesView.VAID),                          
                                            objDAL.MakeInParams("@Document_ID",SqlDbType.Int,0,objIActiveSeriesView.DocumentTypeID),                                            
                                            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0,_userID)
                                            };

            objDAL.RunProc("EC_Opr_ActiveSeries_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";
                int MenuItemId = Common.GetMenuItemId();
                string Mode = System.Web.HttpContext.Current.Request.QueryString["Mode"];
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Document Allocation/FrmActiveSeries.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
            }
            return objMessage;
        }

    }
}