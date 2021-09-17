
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
/// Summary description for CommodityModel
/// </summary>


namespace Raj.EC.MasterModel
{
    public class CommodityModel : IModel
    {

         private ICommodityView objICommodityView;
        private DataSet _ds;
        private DAL _objDAL = new DAL();
        private int _userId = Convert.ToInt32(  UserManager.getUserParam().UserId );
              
         
        public CommodityModel(ICommodityView CommodityView)
        {
            objICommodityView = CommodityView;
        }

        public DataSet ReadValues()
        {

            SqlParameter[] objSqlParam ={ _objDAL.MakeInParams("@Commodity_ID", SqlDbType.Int, 0, objICommodityView.keyID) };


            _objDAL.RunProc("EC_Mst_Commodity_ReadValues", objSqlParam, ref _ds);

            return _ds;

           
        }

        public Message Save()
        {
            Message objMessage = new Message();
             

            SqlParameter[] objSqlParam = {_objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            _objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            _objDAL.MakeOutParams("@CommodityId", SqlDbType.Int, 0), 
            _objDAL.MakeInParams("@Commodity_ID", SqlDbType.Int, 0,objICommodityView.keyID ),
            _objDAL.MakeInParams("@Commodity_Name", SqlDbType.VarChar, 50,objICommodityView.CommodityName ),            
            _objDAL.MakeInParams("@Commodity_Type_Id", SqlDbType.Int,0,objICommodityView.Commodity_Type_Id ),
            _objDAL.MakeInParams("@Is_Service_Tax_Applicable", SqlDbType.Bit, 0, objICommodityView.Is_Service_Tax_Applicable  ),
            _objDAL.MakeInParams("@Is_Restricted", SqlDbType.Bit, 0, objICommodityView.Is_Restricted ),
            _objDAL.MakeInParams("@Is_CST_Applicable", SqlDbType.Bit, 0,objICommodityView.Is_CST_Applicable ),
            _objDAL.MakeInParams("@Is_Perishable", SqlDbType.Bit, 0, objICommodityView.Is_Perishable ),
            _objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userId)};

            _objDAL.RunProc("EC_Mst_Commodity_Save", objSqlParam);

            //_objDAL.MakeInParams("@TDS_Lower_Rate", SqlDbType.Bit, 0, objIPetrolPumpView.PetrolPumpFinanceDetailsView.TDS_Lower_Rate),

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            objICommodityView.CommodityId = Convert.IsDBNull(objSqlParam[2].Value) ? 0 : Convert.ToInt32(objSqlParam[2].Value);

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
                        
            _objDAL.RunProc("EC_Mst_Commodity_FillValues",  ref _ds);            
            
            return _ds;
        }


       


         

       
    }
}




