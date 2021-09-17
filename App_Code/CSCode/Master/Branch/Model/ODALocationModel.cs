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
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
using Raj.EC.MasterView;

/// <summary>
/// Summary description for ODALocationModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class ODALocationModel : IModel
    {
        private IODALocationView objIODALocationView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        //private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;

        public ODALocationModel(IODALocationView odaLocationView)
        {
            objIODALocationView = odaLocationView;
        }
        public DataSet GetBranchValues()
        {

            objDAL.RunProc("[dbo].[EC_Mst_ODALocation_FillValues]", ref objDS);
            return objDS;
        }
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@ServiceLocationId", SqlDbType.Int, 0, objIODALocationView.keyID)
                                         };
            objDAL.RunProc("[dbo].[EC_Mst_ODALocation_ReadValues]", objSqlParam, ref objDS);
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeOutParams("@LocationId", SqlDbType.Int, 0), 
                                               objDAL.MakeInParams("@ServiceLocationId",SqlDbType.Int,0, objIODALocationView.keyID),
                                               objDAL.MakeInParams("@LocationName", SqlDbType.VarChar, 50,objIODALocationView.LocationName), 
                                                objDAL.MakeInParams("@Delivery_Type_ID",SqlDbType.Int,0,objIODALocationView.DeliveryTypeID),
                                               objDAL.MakeInParams("@BranchId",SqlDbType.Int,0,objIODALocationView.BranchId),
                                               objDAL.MakeInParams("@DistFromBranch", SqlDbType.Int,0,objIODALocationView.DistanceFromBranch),
                                               objDAL.MakeInParams("@PrimaryPinCode", SqlDbType.NVarChar,15, objIODALocationView.PrimaryPinCode),
                                               objDAL.MakeInParams("@SecodaryPinCode",SqlDbType.NVarChar,15,objIODALocationView.SecondryPinCode),
                                               objDAL.MakeInParams("@IsBooking", SqlDbType.Bit,0,objIODALocationView.IsBooking),
                                               objDAL.MakeInParams("@IsODALocation",SqlDbType.Bit,0,objIODALocationView.IsODALocation),
                                               objDAL.MakeInParams("@IsOctroiApplicable",SqlDbType.Bit,0,objIODALocationView.IsOctroiApplicable),
                                               objDAL.MakeInParams("@ODAChargesUpto",SqlDbType.Decimal,0,objIODALocationView.ODAChargeUpto),
                                               objDAL.MakeInParams("@ODAChargesAbove",SqlDbType.Decimal,0,objIODALocationView.ODAChargeAbove),
                                               objDAL.MakeInParams("@UserId",SqlDbType.Int,0,_userID),
                                               objDAL.MakeInParams("@City_ID",SqlDbType.Int,0,objIODALocationView.CityId) 
                                         };


            objDAL.RunProc("[dbo].[EC_Mst_ODALocation_Save]", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);



            objIODALocationView.LocationId = Convert.ToInt32(objSqlParam[2].Value);

            if (objMessage.messageID == 0)
            {
                //string _Msg;
                //_Msg = "Saved SuccessFully";
                //string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;
                //System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + LinkUrl + "&DecryptUrl='No'");
            }
            else
            {
                Common.DisplayErrors(objMessage.messageID);
            }


            return objMessage;
        }
       
	}
}
