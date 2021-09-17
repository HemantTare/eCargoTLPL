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

using Raj.EC.MasterView;

/// <summary>
/// Summary description for BranchModel
/// </summary>
namespace Raj.EC.MasterModel
{
    public class BranchModel : IModel
    {
        private IBranchView objBranchView;
        private DAL objDAL = new DAL();
        DataSet objDS = null;
        private int _userID = UserManager.getUserParam().UserId;
        private string _HierarchyCode = UserManager.getUserParam().HierarchyCode;


        public BranchModel(IBranchView BranchView)
        {
            objBranchView = BranchView;
        }

        public DataSet ReadValues()
        {

            return objDS;
        }

        public Message Save()
        {
            Message objMessage = new Message();
            
            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Branch_Id", SqlDbType.Int, 0,objBranchView.BranchGeneralView.keyID),
            objDAL.MakeInParams("@Branch_Code", SqlDbType.VarChar, 5,objBranchView.BranchGeneralView.BranchCode),
            objDAL.MakeInParams("@Branch_Name", SqlDbType.VarChar, 50,objBranchView.BranchGeneralView.BranchName),
            objDAL.MakeInParams("@Service_Tax_Reg_No", SqlDbType.VarChar, 50,objBranchView.BranchGeneralView.STRegistrationNo),
            objDAL.MakeInParams("@Hierarchy_Code", SqlDbType.VarChar, 5,_HierarchyCode),

            objDAL.MakeInParams("@Branch_Type_ID", SqlDbType.Int, 0,objBranchView.BranchGeneralView.BranchTypeID),
            objDAL.MakeInParams("@Agency_Ledger_ID", SqlDbType.Int,0,objBranchView.BranchGeneralView.AgencyAcountID),
            objDAL.MakeInParams("@Default_Hub", SqlDbType.Int, 0,objBranchView.BranchGeneralView.DefaultHubID),
            objDAL.MakeInParams("@Memo_Destination", SqlDbType.Int, 0,objBranchView.BranchGeneralView.MemoDestinationID),
            objDAL.MakeInParams("@Delivery_At", SqlDbType.Int, 0,objBranchView.BranchGeneralView.DeliveryAtID),
            objDAL.MakeInParams("@Delivery_Type_ID", SqlDbType.Int, 0,objBranchView.BranchGeneralView.DeliveryTypeID),
            objDAL.MakeInParams("@Contact_Person", SqlDbType.VarChar, 100,objBranchView.BranchGeneralView.ContactPersonName),
            objDAL.MakeInParams("@Address_1", SqlDbType.VarChar, 100,objBranchView.BranchGeneralView.AddressView.AddressLine1),
            objDAL.MakeInParams("@Address_2", SqlDbType.VarChar,100,objBranchView.BranchGeneralView.AddressView.AddressLine2),
            objDAL.MakeInParams("@City_ID", SqlDbType.Int,0,objBranchView.BranchGeneralView.AddressView.CityId),
            objDAL.MakeInParams("@Pin_Code", SqlDbType.NVarChar, 30,objBranchView.BranchGeneralView.AddressView.PinCode),
            objDAL.MakeInParams("@Std_Code", SqlDbType.NVarChar, 30,objBranchView.BranchGeneralView.AddressView.StdCode),
            objDAL.MakeInParams("@Phone_1", SqlDbType.NVarChar, 40,objBranchView.BranchGeneralView.AddressView.Phone1),
            objDAL.MakeInParams("@Phone_2",SqlDbType.NVarChar,40,objBranchView.BranchGeneralView.AddressView.Phone2),
            objDAL.MakeInParams("@Fax", SqlDbType.NVarChar, 40,objBranchView.BranchGeneralView.AddressView.FaxNo),
            objDAL.MakeInParams("@EMail_Id", SqlDbType.VarChar, 100,objBranchView.BranchGeneralView.AddressView.EmailId),
            objDAL.MakeInParams("@Area_Id", SqlDbType.Int, 0,objBranchView.BranchGeneralView.AreaID),
            objDAL.MakeInParams("@Started_On", SqlDbType.DateTime,0,objBranchView.BranchGeneralView.StartedOn),
            objDAL.MakeInParams("@RegionId",SqlDbType.Int,0,objBranchView.BranchGeneralView.RegionId),

            objDAL.MakeInParams("@Is_Booking", SqlDbType.Bit , 1,objBranchView.BranchDeptServiceView.IsBookingAllowed),
            objDAL.MakeInParams("@Is_Delivery", SqlDbType.Bit, 1,objBranchView.BranchDeptServiceView.IsDeliveryAllowed),
            objDAL.MakeInParams("@Is_Crossing", SqlDbType.Bit , 1,objBranchView.BranchDeptServiceView.IsCrossingBranch),
            objDAL.MakeInParams("@Is_Compuetrised", SqlDbType.Bit, 1,objBranchView.BranchDeptServiceView.IsComputersiedBranch),
            objDAL.MakeInParams("@Is_Octroi", SqlDbType.Bit, 1,objBranchView.BranchDeptServiceView.IsOctroiApplicable),
            objDAL.MakeInParams("@Is_Frenchisee", SqlDbType.Bit,1,objBranchView.BranchDeptServiceView.IsFranchiseeBranch),

            objDAL.MakeInParams("@Bank_Ledger_Id", SqlDbType.Int, 0,objBranchView.BranchParametersView.DefaultBankLedgerId),
            objDAL.MakeInParams("@Cash_Ledger_Id", SqlDbType.Int, 0,objBranchView.BranchParametersView.DefaultCashLedgerId),
            objDAL.MakeInParams("@GC_Min_Stock", SqlDbType.Int, 0,objBranchView.BranchParametersView.GCMinStock),
            objDAL.MakeInParams("@CR_Min_Stock", SqlDbType.Int, 0,objBranchView.BranchParametersView.CRMinStock),
            objDAL.MakeInParams("@Memo_Min_Stock", SqlDbType.Int, 0,objBranchView.BranchParametersView.MemoMinStock),
            objDAL.MakeInParams("@LHPO_Min_Stock", SqlDbType.Int, 0,objBranchView.BranchParametersView.LHPOMinStock),
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userID),
            objDAL.MakeInParams("@BranchDivisionDetailsXML", SqlDbType.Xml, 0,objBranchView.BranchGeneralView.BranchDivisionXML),
            objDAL.MakeInParams("@BranchDepartmentDetailsXML", SqlDbType.Xml, 0,objBranchView.BranchDeptServiceView.BranchDepartmentXML),
            objDAL.MakeInParams("@BranchRequiredFormsXML",SqlDbType.Xml,0,objBranchView.BranchRequiredFormsView.BranchRequiredFormsXML),
            objDAL.MakeInParams("@BookingStartTime",SqlDbType.VarChar,0,objBranchView.BranchParametersView.BookingStartTime),
            objDAL.MakeInParams("@BookingEndTime",SqlDbType.VarChar,0,objBranchView.BranchParametersView.BookingEndTime)};


            objDAL.RunProc("dbo.EC_Master_Branch_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);



            if (objMessage.messageID == 0)
            {

               
                string _Msg;
                _Msg = "Saved SuccessFully";
                if (objBranchView.Flag == "SaveAndNew")
                {
                    int MenuItemId = Common.GetMenuItemId();
                    string Mode = HttpContext.Current.Request.QueryString["Mode"];
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + ClassLibraryMVP.Util.EncryptString("Master/Branch/FrmBranch.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + Mode));
                }
                else if (objBranchView.Flag == "SaveAndExit")
                {
                    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
                }
                
            }

            return objMessage;
        }
               
    }
}
