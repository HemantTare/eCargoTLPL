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
/// Summary description for RegionDetailsModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class RegionDetailsModel : IModel
    {
        private IRegionDetailsView objIRegionDetailsView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        private int _userID = UserManager.getUserParam().UserId;
        private string _HierarchyCode = UserManager.getUserParam().HierarchyCode;
        private int _MainId = UserManager.getUserParam().MainId;


        public RegionDetailsModel(IRegionDetailsView regionDetailsView)
        {
            objIRegionDetailsView = regionDetailsView;
        }

        public DataSet ReadValues()
        {

            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@RegionId",SqlDbType.Int,0,objIRegionDetailsView.RegionGeneralDetailsView.keyID),                                              
                                               objDAL.MakeInParams("@ContactPerson",SqlDbType.VarChar,50,objIRegionDetailsView.RegionGeneralDetailsView.ContactPerson),
                                               objDAL.MakeInParams("@AddressLine1", SqlDbType.VarChar,100,objIRegionDetailsView.RegionGeneralDetailsView.AddressView.AddressLine1),
                                               objDAL.MakeInParams("@AddressLine2", SqlDbType.VarChar,100,objIRegionDetailsView.RegionGeneralDetailsView.AddressView.AddressLine2),
                                               objDAL.MakeInParams("@CityId", SqlDbType.Int,0,objIRegionDetailsView.RegionGeneralDetailsView.AddressView.CityId),
                                               objDAL.MakeInParams("@PinCode", SqlDbType.NVarChar,15,objIRegionDetailsView.RegionGeneralDetailsView.AddressView.PinCode),
                                               objDAL.MakeInParams("@StdCode", SqlDbType.NVarChar,15,objIRegionDetailsView.RegionGeneralDetailsView.AddressView.StdCode),
                                               objDAL.MakeInParams("@Phone1", SqlDbType.NVarChar,20,objIRegionDetailsView.RegionGeneralDetailsView.AddressView.Phone1),
                                               objDAL.MakeInParams("@Phone2", SqlDbType.NVarChar,20,objIRegionDetailsView.RegionGeneralDetailsView.AddressView.Phone2),
                                               objDAL.MakeInParams("@Fax", SqlDbType.NVarChar,20,objIRegionDetailsView.RegionGeneralDetailsView.AddressView.FaxNo),
                                               objDAL.MakeInParams("@EmailId", SqlDbType.VarChar,100,objIRegionDetailsView.RegionGeneralDetailsView.AddressView.EmailId),
                                               objDAL.MakeInParams("@StartedOn", SqlDbType.DateTime,8,objIRegionDetailsView.RegionGeneralDetailsView.StartedOn),
                                               objDAL.MakeInParams("@UserId",SqlDbType.Int,0,_userID),
                                               objDAL.MakeInParams("@HierarchyCode",SqlDbType.VarChar,2,_HierarchyCode),
                                               //objDAL.MakeInParams("@MainId",SqlDbType.Int,0,_MainId),
                                               objDAL.MakeInParams("@CashLimit",SqlDbType.Decimal,0,Util.String2Decimal( objIRegionDetailsView.RegionDepartmentView.CashLimit)),
                                               objDAL.MakeInParams("@BankLimit",SqlDbType.Decimal,0,Util.String2Decimal( objIRegionDetailsView.RegionDepartmentView.BankLimit)),
                                               objDAL.MakeInParams("@SessionChkListDivisionDetails",SqlDbType.Xml,0,objIRegionDetailsView.RegionGeneralDetailsView.SessionChkListDivisionDetails),
                                               objDAL.MakeInParams("@SessionChkListDepartmentDetails",SqlDbType.Xml,0,objIRegionDetailsView.RegionDepartmentView.SessionChkListDepartmentDetails)
         };
            objDAL.RunProc("[dbo].[EC_Mst_RegionDetails_Save]", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;

        }

    }
}

