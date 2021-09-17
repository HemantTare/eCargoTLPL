
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
using ClassLibraryMVP.DataAccess;
using ClassLibraryMVP.General;
using Raj.EC.MasterView;

 

/// <summary>
/// Summary description for ItemModel
/// </summary>


namespace Raj.EC.MasterModel
{
    public class ItemModel : IModel
    {

        private IItemView objIItemView;
        private DataSet _ds;
        private DAL _objDAL = new DAL();
        private int _userId = Convert.ToInt32(  UserManager.getUserParam().UserId );
              
         
        public ItemModel(IItemView ItemView)
        {
            objIItemView = ItemView;
        }

        public DataSet ReadValues()
        {

            SqlParameter[] objSqlParam ={ _objDAL.MakeInParams("@Item_ID", SqlDbType.Int, 0, objIItemView.keyID) };


            _objDAL.RunProc("EC_Mst_Item_ReadValues", objSqlParam, ref _ds);

            return _ds;

           
        }

        public Message Save()
        {
            Message objMessage = new Message();
             

            SqlParameter[] objSqlParam = {_objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            _objDAL.MakeOutParams("@ItemId", SqlDbType.Int, 0), 
            _objDAL.MakeInParams("@Item_ID", SqlDbType.Int, 0,objIItemView.keyID ),
            _objDAL.MakeInParams("@Item_Name", SqlDbType.VarChar, 50,objIItemView.ItemName ),            
            _objDAL.MakeInParams("@Commodity_Id", SqlDbType.VarChar, 100,objIItemView.Commodity_Id ),    
            _objDAL.MakeInParams("@Description", SqlDbType.VarChar, 150,objIItemView.Description ),            
            _objDAL.MakeInParams("@ItemRatePerKg", SqlDbType.Decimal, 0,objIItemView.ItemRatePerKg),            
            _objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userId)}; 

            _objDAL.RunProc("EC_Mst_Item_Save", objSqlParam);

          
            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            objIItemView.ItemId = Convert.IsDBNull(objSqlParam[2].Value) ? 0 : Convert.ToInt32(objSqlParam[2].Value);

            if (objMessage.messageID == 0)
            {

                //string _Msg;
                //_Msg = "Saved SuccessFully";
                //string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;
                //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + LinkUrl + "&DecryptUrl='No'");
            }          


            return objMessage;
        }

        public DataSet Fill_Values()
        {
            _objDAL.RunProc("EC_Mst_Item_FillValues", ref _ds);
            return _ds;
        }
    }
}




