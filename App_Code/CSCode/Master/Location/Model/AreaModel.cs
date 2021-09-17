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
/// Summary description for AreaModel
/// </summary>
namespace Raj.EC.MasterModel
{
    class AreaModel : IModel
    {
        private IAreaView objIAreaView;
        private DAL objDAL = new DAL();
        private DataSet objDS;

        private int _userID = UserManager.getUserParam().UserId;

        public AreaModel(IAreaView areaView)
        {
            objIAreaView = areaView;
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
               objDAL.MakeInParams("@AreaId",SqlDbType.Int,0,objIAreaView.AreaGeneralDetailsView.keyID),
               objDAL.MakeInParams("@AreaCode",SqlDbType.VarChar,5,objIAreaView.AreaGeneralDetailsView.AreaCode),
               objDAL.MakeInParams("@AreaName",SqlDbType.VarChar,50,objIAreaView.AreaGeneralDetailsView.AreaName),
               objDAL.MakeInParams("@ContactPerson",SqlDbType.VarChar,50,objIAreaView.AreaGeneralDetailsView.ContactPerson),
               objDAL.MakeInParams("@AddressLine1", SqlDbType.VarChar,100,objIAreaView.AreaGeneralDetailsView.AddressView.AddressLine1),
               objDAL.MakeInParams("@AddressLine2", SqlDbType.VarChar,100,objIAreaView.AreaGeneralDetailsView.AddressView.AddressLine2),
               objDAL.MakeInParams("@CityId", SqlDbType.Int,0,objIAreaView.AreaGeneralDetailsView.AddressView.CityId),
               objDAL.MakeInParams("@PinCode", SqlDbType.NVarChar,15,objIAreaView.AreaGeneralDetailsView.AddressView.PinCode),
               objDAL.MakeInParams("@StdCode", SqlDbType.NVarChar,15,objIAreaView.AreaGeneralDetailsView.AddressView.StdCode),
               objDAL.MakeInParams("@Phone1", SqlDbType.NVarChar,20,objIAreaView.AreaGeneralDetailsView.AddressView.Phone1),
               objDAL.MakeInParams("@Phone2", SqlDbType.NVarChar,20,objIAreaView.AreaGeneralDetailsView.AddressView.Phone2),
               objDAL.MakeInParams("@Fax", SqlDbType.NVarChar,20,objIAreaView.AreaGeneralDetailsView.AddressView.FaxNo),
               objDAL.MakeInParams("@EmailId", SqlDbType.VarChar,100,objIAreaView.AreaGeneralDetailsView.AddressView.EmailId),
               objDAL.MakeInParams("@StartedOn", SqlDbType.DateTime,8,objIAreaView.AreaGeneralDetailsView.StartedOn),
               objDAL.MakeInParams("@UserId",SqlDbType.Int,0,_userID),
               objDAL.MakeInParams("@CashLimit",SqlDbType.Decimal,0,objIAreaView.AreaDepartmentView.CashLimit),
               objDAL.MakeInParams("@BankLimit",SqlDbType.Decimal,0,objIAreaView.AreaDepartmentView.BankLimit),
               objDAL.MakeInParams("@SessionChkListDivisionDetails",SqlDbType.Xml,0,objIAreaView.AreaGeneralDetailsView.SessionChkListDivisionDetails),
               objDAL.MakeInParams("@SessionChkListDepartmentDetails",SqlDbType.Xml,0,objIAreaView.AreaDepartmentView.SessionChkListDepartmentDetails),
               objDAL.MakeInParams("@CashLedgerId",SqlDbType.Int,0,objIAreaView.AreaDepartmentView.CashLedgerId),
               objDAL.MakeInParams("@BankLedgerId",SqlDbType.Int,0,objIAreaView.AreaDepartmentView.BankLedgerId)
            };
            objDAL.RunProc("EC_Mst_Area_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            return objMessage;
        }

	}
}
