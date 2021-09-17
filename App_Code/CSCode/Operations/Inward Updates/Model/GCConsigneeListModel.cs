using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationView;
using ClassLibraryMVP;

/// <summary>
/// Author: Ankit champaneriya
/// Date  : 07-01-09
/// Summary description for GCConsigneeListModel
/// </summary>
/// 

namespace Raj.EC.OperationModel
{
    public class GCConsigneeListModel : IModel
    {
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private IGCConsigneeListView objIGCConsigneeListView;

        public GCConsigneeListModel(IGCConsigneeListView GCConsigneeView)
        {
            objIGCConsigneeListView = GCConsigneeView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={objDAL.MakeInParams("@Branch_Id", SqlDbType.Int , 0,objIGCConsigneeListView.BranchId ),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int , 0,UserManager.getUserParam().DivisionId ),
            objDAL.MakeInParams("@BranchXML", SqlDbType.Xml, 0, objIGCConsigneeListView .GetBranchXML),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int , 0, UserManager.getUserParam().YearCode  )};

            objDAL.RunProc("dbo.EC_Opr_GC_ConsigneeUpdate_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }

        ////public DataSet FillClientDetails()
        ////{
        ////    string[] splitted = objIGCConsigneeListView.ClientId.Split(new char[] { '*' });
        ////    int Client_Id = Util.String2Int(splitted[0]);
        ////    int isRegularClient = Util.String2Int(splitted[1]);
        ////    SqlParameter[] objSqlParam ={
        ////        objDAL.MakeInParams("@Client_Id", SqlDbType.Int  , 0,Client_Id ) ,
        ////        objDAL.MakeInParams("@IsRegularClient",SqlDbType.Bit,0,isRegularClient )};

        ////    objDAL.RunProc("dbo.EC_Opr_GC_ClientWise_GetConsignee_ReadValues", objSqlParam, ref objDS);

        ////    return objDS;
        ////}

        public Message Save()
        {
            DataSet ds = new DataSet();
            Message objMessage = new Message();
            ////string[] splitted = objIGCConsigneeListView.ClientId.Split(new char[] { '*' });
            ////int Client_Id = Util.String2Int(splitted[0]);
            ////int isRegularClient = Convert.ToInt32(splitted[1]);

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                objDAL.MakeInParams ("@GC_ID", SqlDbType.Int   ,10,objIGCConsigneeListView.GC_ID ),
                objDAL.MakeInParams("@Is_Regular_Client",SqlDbType.Int  ,0, objIGCConsigneeListView.isRegularClient),
                objDAL.MakeInParams("@New_Consignee_Client_ID",SqlDbType.Int  ,0,objIGCConsigneeListView.NewClientId ),
                objDAL.MakeInParams("@New_Consignee_Name",SqlDbType.VarChar   ,100, objIGCConsigneeListView.ClientName ),
                objDAL.MakeInParams("@New_Consignee_Add1",SqlDbType.VarChar   ,100, objIGCConsigneeListView.Add1 ),
                objDAL.MakeInParams("@New_Consignee_Add2",SqlDbType.VarChar   ,100, objIGCConsigneeListView.Add2 ),
                objDAL.MakeInParams("@New_Consignee_Pin_Code",SqlDbType.VarChar  ,60, objIGCConsigneeListView.pincode ),
                objDAL.MakeInParams("@New_Consignee_Std_Code",SqlDbType.VarChar   ,60, objIGCConsigneeListView.stdcode ),
                objDAL.MakeInParams("@New_Consignee_Phone1",SqlDbType.VarChar   ,80, objIGCConsigneeListView.phone  ),
                objDAL.MakeInParams("@New_Consignee_Mobile_No",SqlDbType.VarChar   ,80, objIGCConsigneeListView.mobile ),
                objDAL.MakeInParams("@New_Consignee_CST_TIN_No",SqlDbType.VarChar   ,60, objIGCConsigneeListView.csttinno  ),
                objDAL.MakeInParams("@New_Consignee_Service_Tax_No",SqlDbType.VarChar   ,60, objIGCConsigneeListView.servicetaxNo ),
                objDAL.MakeInParams("@Reason",SqlDbType.VarChar ,250, objIGCConsigneeListView.Reason  ),
                objDAL.MakeInParams("@Updated_By",SqlDbType.Int,0, UserManager.getUserParam().UserId  )};

            objDAL.RunProc("[EC_Opr_GC_Consignee_History_Save]", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                //objIGCConsigneeListView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";
                //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
            return objMessage;
        }
    }
}