using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationView;

/// <summary>
/// Author : Ankit champaneriya
/// Date   : 05-01-09
/// Summary description for DlyBranchUpdateModel
/// </summary>
/// 

namespace Raj.EC.OperationModel
{
    public class DlyBranchUpdateModel : IModel
    {
        #region Class Declaration
        private IDlyBranchUpdateView objIDlyBranchUpdateView;
        private DAL objDAL = new DAL();
        private DataSet objDS = new DataSet();
        #endregion

        public DlyBranchUpdateModel(IDlyBranchUpdateView DlyBranchUpdateView)
        {
            objIDlyBranchUpdateView = DlyBranchUpdateView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={objDAL.MakeInParams("@Branch_Id", SqlDbType.Int , 0,objIDlyBranchUpdateView.BranchId ),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int , 0,UserManager.getUserParam().DivisionId),
            objDAL.MakeInParams("@BranchXML", SqlDbType.Xml, 0, objIDlyBranchUpdateView.GetBranchXML),
            objDAL.MakeInParams("@New_Dly_Branch_ID", SqlDbType.Int , 0, objIDlyBranchUpdateView.NewDlyBranchId ),
            objDAL.MakeInParams("@ServiceLocation_Id",SqlDbType.Int,0,objIDlyBranchUpdateView.ServiceLocationId),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int , 0, UserManager.getUserParam().YearCode  )};

            objDAL.RunProc("dbo.EC_Opr_DeliveryBranchUpdate_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }

        public DataSet Fill_Values()
        {
            objDAL.RunProc("dbo.EC_Opr_DeliveryBranchUpdate_FillValues", ref objDS);
            return objDS;
        }

        public DataSet ServiceLocation_FillValues()
        {
            SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Delivery_Branch_Id", SqlDbType.Int, 0, objIDlyBranchUpdateView.NewDlyBranchId) };

            objDAL.RunProc("EC_Opr_deliveryBranchUpdate_Fill_ServiceLocation", objSqlParam, ref objDS);
            return objDS;
        }

        public Message Save()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt = objIDlyBranchUpdateView.SessionDGDeliveryBranch;
            ds.Tables.Add(dt.Copy());
            string xmldata = ds.GetXml().ToString();
            //@ServiceLocation_Id added by ANKIT : 20-02-09 7.15 PM

            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
            objDAL.MakeInParams ("@TransactionDate", SqlDbType.DateTime  ,10,objIDlyBranchUpdateView.TransactionDate ),
            objDAL.MakeInParams("@Current_Branch_Id", SqlDbType.Int , 0,objIDlyBranchUpdateView.BranchId ),
            objDAL.MakeInParams ("@xmldata", SqlDbType.Xml  ,0,xmldata ),
            objDAL.MakeInParams("@New_Dly_Branch_Id",SqlDbType.Int ,0, objIDlyBranchUpdateView.NewDlyBranchId ),
            objDAL.MakeInParams("@ServiceLocation_Id",SqlDbType.Int,0,objIDlyBranchUpdateView.ServiceLocationId),
            objDAL.MakeInParams("@Reason",SqlDbType.VarChar ,250, objIDlyBranchUpdateView.Reason ),
            objDAL.MakeInParams("@Created_By",SqlDbType.Int,0, UserManager.getUserParam().UserId  )};

            objDAL.RunProc("EC_Opr_DeliveryBranchUpdate_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);
            if (objMessage.messageID == 0)
            {
                objIDlyBranchUpdateView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";
                int MenuItemId = Common.GetMenuItemId();
                string Mode = System.Web.HttpContext.Current.Request.QueryString["Mode"];
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Operations/Inward Updates/FrmDlyBranchUpdate.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));

                //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
            return objMessage;
        }
    }
}