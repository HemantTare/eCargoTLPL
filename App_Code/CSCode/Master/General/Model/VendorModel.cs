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
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EF.MasterView;

/// <summary>
/// Summary description for VendorModel
/// </summary>
namespace Raj.EF.MasterModel
{
    class VendorModel : IModel
    {
        private IVendorView objIVendorView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        //private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;
        //
        public VendorModel(IVendorView VendorView)
        {
            objIVendorView = VendorView;
        }
        
        public DataSet FillValues()

        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@keyID", SqlDbType.Int, 0, objIVendorView.keyID) 
                                         };
            objDAL.RunProc("rstil43.EF_Mst_General_Vendor_FillValues",objSqlParam, ref objDS);
            return objDS;
        }

        public DataSet FillVendorType()
        {
            objDAL.RunProc("rstil43.EF_Mst_General_Vendor_FillVendorType", ref objDS);
            return objDS;
        }

        
        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Vendor_Id", SqlDbType.Int, 0, objIVendorView.keyID) 
                                         };
            objDAL.RunProc("rstil43.EF_Mst_General_Vendor_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }

        //public Boolean ISDuplicatePANNoCheck()   //added by ANkit : 19/11/08 : 5.30 pm
        //{
        //    bool _isDuplicate;
        //    SqlParameter[] SqlPara ={objDAL.MakeInParams("@Pan_No",SqlDbType.VarChar,0,objIVendorView.PanNo ),
        //                             objDAL.MakeInParams("@Vendor_Id",SqlDbType.Int,0,objIVendorView.keyID ),
        //                             objDAL.MakeOutParams("@Duplicate",SqlDbType.Bit,0)};

        //    objDAL.RunProc("EF_Master_PANNo_Check_Duplication", SqlPara);

        //    _isDuplicate = Convert.ToBoolean(SqlPara[2].Value.ToString());

        //    return _isDuplicate;
        //}


        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@Vendor_Name", SqlDbType.VarChar, 50,objIVendorView.VendorName), 
                                               objDAL.MakeInParams("@Vendor_Id", SqlDbType.Int, 0, objIVendorView.keyID),
                                               objDAL.MakeInParams("@Address_Line1", SqlDbType.VarChar,100,objIVendorView.AddressView.AddressLine1),
                                               objDAL.MakeInParams("@Address_Line2", SqlDbType.VarChar,100,objIVendorView.AddressView.AddressLine2),
                                               objDAL.MakeInParams("@City_Id", SqlDbType.Int,0,objIVendorView.AddressView.CityId),
                                               objDAL.MakeInParams("@Pin_Code", SqlDbType.NVarChar,15,objIVendorView.AddressView.PinCode),
                                               objDAL.MakeInParams("@Std_Code", SqlDbType.NVarChar,15,objIVendorView.AddressView.StdCode),
                                               objDAL.MakeInParams("@Phone_1", SqlDbType.NVarChar,20,objIVendorView.AddressView.Phone1),
                                               objDAL.MakeInParams("@Phone_2", SqlDbType.NVarChar,20,objIVendorView.AddressView.Phone2),
                                               objDAL.MakeInParams("@Mobile_No", SqlDbType.NVarChar,25,objIVendorView.AddressView.MobileNo),
                                               objDAL.MakeInParams("@Fax", SqlDbType.NVarChar,20,objIVendorView.AddressView.FaxNo),
                                               objDAL.MakeInParams("@Email_Id", SqlDbType.VarChar,100,objIVendorView.AddressView.EmailId),
                                               objDAL.MakeInParams("@Reference_Name",SqlDbType.VarChar,100,objIVendorView.ReferenceName),
                                               objDAL.MakeInParams("@Reference_Phone", SqlDbType.VarChar,25,objIVendorView.ReferencePhone),
                                               objDAL.MakeInParams("@Reference_Mobile", SqlDbType.VarChar,25,objIVendorView.ReferenceMobile),
                                               objDAL.MakeInParams("@Credit_Days", SqlDbType.Int,3,objIVendorView.Credit_Days),
                                               objDAL.MakeInParams("@Credit_Limit", SqlDbType.Int,6,objIVendorView.Credit_Limit),
                                               objDAL.MakeInParams("@Debit_BalLimmit", SqlDbType.Int,6,objIVendorView.Debit_BalLimmit),
                                               objDAL.MakeInParams("@Is_Tds_Applicable", SqlDbType.Bit,1,objIVendorView.TDSAppView.IsTDSApp),
                                               //objDAL.MakeInParams("@Tds_Rate_Percent",SqlDbType.Decimal,0,objIVendorView.TdsRatePercent),
                                               //objDAL.MakeInParams("@Tds_Exemption_Limit",SqlDbType.Decimal,0,objIVendorView.TdsExemptionLimit),
                                               objDAL.MakeInParams("@Pan_No",SqlDbType.VarChar,20,objIVendorView.PanNo),
                                               //objDAL.MakeInParams("@Tds_Id",SqlDbType.Int,0,objIVendorView.TdsId), 
                                               objDAL.MakeInParams("@Vendor_Type_Id",SqlDbType.Int,0,objIVendorView.VendorTypeId),                              
                                               objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, _userID),
                                               objDAL.MakeInParams("@DeducteeTypeID", SqlDbType.Int,0,objIVendorView.TDSAppView.DeducteeTypeID),
                                                objDAL.MakeInParams("@IsLowerNoDeduction", SqlDbType.Bit,0,objIVendorView.TDSAppView.IsLower),
                                                objDAL.MakeInParams("@IsIgnoreExemption", SqlDbType.Bit,1,objIVendorView.TDSAppView.IsIgnore),
                                                objDAL.MakeInParams("@SectionNo", SqlDbType.NVarChar,15,objIVendorView.TDSAppView.sectionNo),
                                                objDAL.MakeInParams("@LowerRate", SqlDbType.Decimal,0,objIVendorView.TDSAppView.LowerRate),
                                                objDAL.MakeInParams("@APMCBroker_ToCity",SqlDbType.VarChar,50,objIVendorView.APMCBroker_To_City )

                                         };


            objDAL.RunProc("rstil43.EF_Mst_General_Vendor_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                string _Msg;
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }

            return objMessage;
        }
    }
}
