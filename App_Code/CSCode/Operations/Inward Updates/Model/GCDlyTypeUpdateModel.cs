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
/// Summary description for GCDlyTypeUpdateModel
/// </summary>
/// 

namespace Raj.EC.OperationModel
{
    public class GCDlyTypeUpdateModel : IModel
    {
        private DAL objDAL = new DAL();
        private DataSet objDS;
        private IGCDlyTypeUpdateView objIGCDlyTypeUpdateView;

        public GCDlyTypeUpdateModel(IGCDlyTypeUpdateView GCConsigneeView)
        {
            objIGCDlyTypeUpdateView = GCConsigneeView;
        }

        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam ={objDAL.MakeInParams("@Branch_Id", SqlDbType.Int , 0,objIGCDlyTypeUpdateView.BranchId ),
            objDAL.MakeInParams("@Division_ID", SqlDbType.Int , 0,UserManager.getUserParam().DivisionId ),
            objDAL.MakeInParams("@BranchXML", SqlDbType.Xml, 0, objIGCDlyTypeUpdateView.GetBranchXML),
            objDAL.MakeInParams("@Year_Code", SqlDbType.Int , 0, UserManager.getUserParam().YearCode  )};

            objDAL.RunProc("dbo.EC_Opr_GC_DeliveryTypeUpdate_ReadValues", objSqlParam, ref objDS);

            return objDS;
        }
        public DataSet FillValues()
        {
            objDAL.RunProc("dbo.EC_Opr_DeliveryType_FillValues", ref objDS);
            return objDS;
        }
 

        public Message Save()
        {
            DataSet ds = new DataSet();
            int _menuItemId = Common.GetMenuItemId();
            Message objMessage = new Message();
            

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000), 
                objDAL.MakeInParams("@GCDlyTypeUpdateXML",SqlDbType.Xml,0,objIGCDlyTypeUpdateView.GCDlyTypeUpdateXML), 
                objDAL.MakeInParams("@Updated_By",SqlDbType.Int,0, UserManager.getUserParam().UserId  )};

            objDAL.RunProc("EC_Opr_GC_DeliveryType_History_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                //objIGCDlyTypeUpdateView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
            else if (objMessage.messageID > 0)
            {
                string Mode = System.Web.HttpContext.Current.Request.QueryString["Mode"];
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(objMessage.message) + "&Url=" +
                                               ClassLibraryMVP.Util.EncryptString("Operations/Inward Updates/FrmGCDlyTypeUpdate.aspx?Menu_Item_Id=" +
                                               ClassLibraryMVP.Util.EncryptInteger(_menuItemId) + "&Mode=" + Mode));
            } 
            return objMessage;
        }
    }
}