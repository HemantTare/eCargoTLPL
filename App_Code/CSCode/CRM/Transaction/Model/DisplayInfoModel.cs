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
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.CRM.TransactionsView;
//using Raj.eCargo.Init;
using ClassLibraryMVP;
namespace Raj.CRM.TransactionsModel
{
    class DisplayInfoModel
    {
        private IDisplayInfoView objIDisplayInfoView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private int _userID = (int)UserManager.getUserParam().UserId;
        private int _yearCode = (int)UserManager.getUserParam().YearCode;
        private string _hierarchyCode = (string)UserManager.getUserParam().HierarchyCode;
        private int _mainId = (int)UserManager.getUserParam().MainId;
        //private bool _isVT = Convert.ToBoolean(UserManager.getUserParam().Is_VT);

        public DisplayInfoModel(IDisplayInfoView DisplayInfoView)
        {
            objIDisplayInfoView = DisplayInfoView;
        }

   
        public DataSet FillTicketInfo()
        {
            SqlParameter[] objParam ={
                                      objDAL.MakeInParams("@GcDoc_No",SqlDbType.Int,0,Util.String2Int(objIDisplayInfoView.keyID)),
                                      objDAL.MakeInParams("@Is_VT",SqlDbType.Bit,0,1)
                                     };
            objDAL.RunProc("EC_CRM_Trn_TicketInfo", objParam, ref objDS);
            return objDS;
        }



        public DataSet FillProfileUserInfo()
        {
            DataTable Dt = new DataTable();
            Dt.Columns.Add("Profile_Name");

            string[] Prof = objIDisplayInfoView.keyID.Split(',');
            DataRow Dr;
            for (int i = 0; i < Prof.Length; i++)
            {
                Dr= Dt.NewRow();
                Dr["Profile_Name"] = Prof[i].Trim();
                Dt.Rows.Add(Dr);
            }
            Dt.AcceptChanges();
            DataSet Ds = new DataSet();
            Ds.Tables.Add(Dt);

            if (Ds.Tables[0].Rows.Count != 0)
            {
                Ds.Tables[0].TableName = "Table";
                Ds.AcceptChanges();
                SqlParameter[] objParam ={
                                          objDAL.MakeInParams("@ProfileXml",SqlDbType.Xml,0,Ds.GetXml()),
                                          objDAL.MakeInParams("@Type",SqlDbType.VarChar,50,objIDisplayInfoView.Type),
                                          objDAL.MakeInParams("@Ticket_Id",SqlDbType.VarChar,50,objIDisplayInfoView.Ticket_Id)
                                        };
                objDAL.RunProc("EC_CRM_Trn_UserInfo", objParam, ref objDS);
            }
            return objDS;
        }

        public DataSet FillUserInfo()
        {
           SqlParameter[] objParam ={
                                       objDAL.MakeInParams("@Id",SqlDbType.VarChar,50,(string)objIDisplayInfoView.keyID),
                                       objDAL.MakeInParams("@Ticket_Id",SqlDbType.Int,0,objIDisplayInfoView.Ticket_Id),
                                       objDAL.MakeInParams("@Type",SqlDbType.VarChar,50,objIDisplayInfoView.Type)
                                    };
            objDAL.RunProc("EC_CRM_Trn_UserInfo", objParam, ref objDS);
            return objDS;
        }
    }
}
