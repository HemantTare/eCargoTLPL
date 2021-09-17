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
/// Summary description for InsuranceCompanyBranchModel
/// </summary>
namespace Raj.EF.MasterModel
{
    class InsuranceCompanyBranchModel : IModel
    {
        private IInsuranceCompanyBranchView objIInsuranceCompanyBranchView;
        private DAL objDAL = new DAL();
        private DataSet objDS;
        //private int _userID = 1;
        private int _userID = UserManager.getUserParam().UserId;

        public InsuranceCompanyBranchModel(IInsuranceCompanyBranchView insuranceCompanyBranchView)
        {
            objIInsuranceCompanyBranchView = insuranceCompanyBranchView;
        }

        public DataSet FillValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@keyID", SqlDbType.Int, 0, objIInsuranceCompanyBranchView.keyID) 
                                         };
            objDAL.RunProc("rstil43.EF_Mst_Insurance_Company_Branch_FillValues",objSqlParam, ref objDS);
            return objDS;
        }
       
        //public DataSet FillInsuranceCompanyValues()
        //{
         //   SqlParameter[] objSqlParam ={ objDAL.MakeInParams("@Insurance_Company_Id", SqlDbType.Int, 0, objIInsuranceCompanyBranchView.) };
           // objDAL.RunProc("", objSqlParam, ref objDS);
          //  return objDS;
        //}



        public DataSet ReadValues()
        {
            SqlParameter[] objSqlParam = { objDAL.MakeInParams("@Insurance_Branch_Id", SqlDbType.Int, 0, objIInsuranceCompanyBranchView.keyID) 
                                         };
            objDAL.RunProc("rstil43.EF_Mst_Insurance_Company_Branch_ReadValues", objSqlParam, ref objDS);
            return objDS;
        }
        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = { 
                                               objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
                                               objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
                                               objDAL.MakeInParams("@Branch_Name", SqlDbType.VarChar, 25,objIInsuranceCompanyBranchView.BranchName), 
                                               objDAL.MakeInParams("@Contact_Person", SqlDbType.VarChar, 50,objIInsuranceCompanyBranchView.ContactPerson), 
                                               objDAL.MakeInParams("@Insurance_Branch_Id", SqlDbType.Int, 0, objIInsuranceCompanyBranchView.keyID),
                                               objDAL.MakeInParams("@Address_Line1", SqlDbType.VarChar,100,objIInsuranceCompanyBranchView.AddressView.AddressLine1),
                                               objDAL.MakeInParams("@Address_Line2", SqlDbType.VarChar,100,objIInsuranceCompanyBranchView.AddressView.AddressLine2),
                                               objDAL.MakeInParams("@City_Id", SqlDbType.Int,0,objIInsuranceCompanyBranchView.AddressView.CityId),
                                               objDAL.MakeInParams("@Pin_Code", SqlDbType.NVarChar,15,objIInsuranceCompanyBranchView.AddressView.PinCode),
                                               objDAL.MakeInParams("@Std_Code", SqlDbType.NVarChar,15,objIInsuranceCompanyBranchView.AddressView.StdCode),
                                               objDAL.MakeInParams("@Phone_1", SqlDbType.NVarChar,20,objIInsuranceCompanyBranchView.AddressView.Phone1),
                                               objDAL.MakeInParams("@Phone_2", SqlDbType.NVarChar,20,objIInsuranceCompanyBranchView.AddressView.Phone2),
                                               objDAL.MakeInParams("@Mobile_No", SqlDbType.NVarChar,25,objIInsuranceCompanyBranchView.AddressView.MobileNo),
                                               objDAL.MakeInParams("@Fax", SqlDbType.NVarChar,20,objIInsuranceCompanyBranchView.AddressView.FaxNo),
                                               objDAL.MakeInParams("@Email_Id", SqlDbType.VarChar,100,objIInsuranceCompanyBranchView.AddressView.EmailId),
                                               objDAL.MakeInParams("@Insurance_Company_Id",SqlDbType.Int,0,objIInsuranceCompanyBranchView.InsuranceCompanyId), 
                                               objDAL.MakeInParams("@User_Id", SqlDbType.Int, 0, _userID)
                                         };


            objDAL.RunProc("rstil43.EF_Mst_Insurance_Company_Branch_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);


            //if (objMessage.messageID == 0)
            //{

            //    string _Msg;
            //    _Msg = "Saved SuccessFully";
            //    System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            //}

            return objMessage;
        }
    }
}

