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

using Raj.EF.MasterView;

/// <summary>
/// Summary description for DriverModel
/// </summary>
/// 

namespace Raj.EF.MasterModel
{
    public class DriverModel : IModel
    {
        private IDriverView objIDriverView;
        private DAL objDAL = new DAL();
        //private int UserID = 1;
        private int _userID = UserManager.getUserParam().UserId;

        public DriverModel(IDriverView DriverView)
        {
            objIDriverView = DriverView;
        }

        public DataSet ReadValues()
        {
            //Do nothing here
            //Filling work for product master done on their respective Usercontrols
            DataSet DS = null;
            return DS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Driver_ID", SqlDbType.Int, 0,objIDriverView.DriverDetailsView.keyID),
            objDAL.MakeInParams("@Driver_Name", SqlDbType.VarChar, 50,objIDriverView.DriverDetailsView.DriverName),
            objDAL.MakeInParams("@Driver_Code", SqlDbType.VarChar, 10,objIDriverView.DriverDetailsView.DriverCode),
            objDAL.MakeInParams("@Address1", SqlDbType.VarChar, 100,objIDriverView.DriverDetailsView.AddressView.AddressLine1),
            objDAL.MakeInParams("@Address2", SqlDbType.VarChar, 100,objIDriverView.DriverDetailsView.AddressView.AddressLine2),
            objDAL.MakeInParams("@Reference_Name", SqlDbType.VarChar, 100,objIDriverView.DriverDetailsView.ReferenceName),
            objDAL.MakeInParams("@Reference_Phone", SqlDbType.VarChar, 25,objIDriverView.DriverDetailsView.ReferencePhone),
            objDAL.MakeInParams("@Reference_Mobile", SqlDbType.VarChar, 25,objIDriverView.DriverDetailsView.ReferenceMobile),
            objDAL.MakeInParams("@Driver_Image", SqlDbType.VarChar, 250,objIDriverView.DriverDetailsView.DriverImage),
            objDAL.MakeInParams("@Qualification", SqlDbType.VarChar, 100,objIDriverView.DriverDetailsView.Qualification),
            objDAL.MakeInParams("@Native_Address1", SqlDbType.VarChar, 100,objIDriverView.DriverDetailsView.NativeAddress1),
            objDAL.MakeInParams("@Native_Address2", SqlDbType.VarChar, 100,objIDriverView.DriverDetailsView.NativeAddress2),
            objDAL.MakeInParams("@Policy_No", SqlDbType.VarChar, 50, objIDriverView.DriverInsuranceDependentView.PolicyNumber ),
            objDAL.MakeInParams("@Nominee_Name", SqlDbType.VarChar, 50, objIDriverView.DriverInsuranceDependentView.NomineeName ),
            objDAL.MakeInParams("@Pin_Code", SqlDbType.NVarChar, 15,objIDriverView.DriverDetailsView.AddressView.PinCode),
            objDAL.MakeInParams("@Phone_No", SqlDbType.NVarChar, 20,objIDriverView.DriverDetailsView.DriverMobile2 ),
            objDAL.MakeInParams("@Mobile_No", SqlDbType.NVarChar, 25,objIDriverView.DriverDetailsView.DriverMobile1 ),
            objDAL.MakeInParams("@Native_Contact_No", SqlDbType.NVarChar, 20,objIDriverView.DriverDetailsView.NativeContactNo),
            objDAL.MakeInParams("@Driver_License_No", SqlDbType.NVarChar, 50,objIDriverView.DriverDetailsView.DriverLicenseNo),
            objDAL.MakeInParams("@City_ID", SqlDbType.Int, 0, objIDriverView.DriverDetailsView.AddressView.CityId),
            objDAL.MakeInParams("@License_Issue_City_ID", SqlDbType.Int, 0, objIDriverView.DriverDetailsView.LicenseIssueCityID),
            objDAL.MakeInParams("@License_Category_ID", SqlDbType.Int, 0, objIDriverView.DriverDetailsView.LicenseCategoryID),
            objDAL.MakeInParams("@Driver_Category_ID", SqlDbType.Int, 0, objIDriverView.DriverDetailsView.DriverCategoryID),
            objDAL.MakeInParams("@Religion_ID", SqlDbType.Int, 0, objIDriverView.DriverDetailsView.ReligionID),
            objDAL.MakeInParams("@User_ID", SqlDbType.Int, 0, _userID),
            objDAL.MakeInParams("@Insurance_Company_ID", SqlDbType.Int , 0,objIDriverView.DriverInsuranceDependentView.InsuranceCompanyID ), 
            objDAL.MakeInParams("@Insurance_Branch_ID", SqlDbType.Int , 0,objIDriverView.DriverInsuranceDependentView.InsuranceBranchID ), 
            objDAL.MakeInParams("@Insurance_Agent_ID", SqlDbType.Int , 0,objIDriverView.DriverInsuranceDependentView.InsuranceAgentID ), 
            objDAL.MakeInParams("@Nominee_Relation_ID",SqlDbType.Int,0,objIDriverView.DriverInsuranceDependentView.NomineeRelationID),
            objDAL.MakeInParams("@Attachment_Form_ID",SqlDbType.Int,0,objIDriverView.AttachmentsView.AttachmentFormId),
            objDAL.MakeInParams("@Opening_Balance", SqlDbType.Decimal, 0, objIDriverView.DriverDetailsView.OpeningBalance),
            objDAL.MakeInParams("@Insurance_Premium", SqlDbType.Decimal, 0, objIDriverView.DriverInsuranceDependentView.InsurancePremium ),
            objDAL.MakeInParams("@Sum_Assured",SqlDbType.Decimal,0,objIDriverView.DriverInsuranceDependentView.SumAssured),
            objDAL.MakeInParams("@License_Expiry_Date", SqlDbType.DateTime, 0,objIDriverView.DriverDetailsView.LicenseExpiryDate),
            objDAL.MakeInParams("@Birth_Date", SqlDbType.DateTime, 0,objIDriverView.DriverDetailsView.BirthDate),
            objDAL.MakeInParams("@Insurance_Expiry_Date", SqlDbType.DateTime, 0,objIDriverView.DriverInsuranceDependentView.InsuranceExpiryDate), 
            objDAL.MakeInParams("@Is_Cleaner", SqlDbType.Bit, 0, objIDriverView.DriverDetailsView.IsCleaner),
            objDAL.MakeInParams("@Is_Reliable", SqlDbType.Bit, 0, objIDriverView.DriverDetailsView.IsReliable),
            objDAL.MakeInParams("@Is_Married", SqlDbType.Bit, 0, objIDriverView.DriverDetailsView.IsMarried),
            objDAL.MakeInParams("@Is_Company_Driver", SqlDbType.Bit, 0, objIDriverView.DriverDetailsView.IsCompanyDriver),
            objDAL.MakeInParams("@Driver_Dependent_Details",SqlDbType.Xml,0,objIDriverView.DriverInsuranceDependentView.Driver_Dependent_Details.ToLower()),
            objDAL.MakeInParams("@Attachments_XML",SqlDbType.Xml,0,objIDriverView.AttachmentsView.AttachmentsXML),
            objDAL.MakeInParams("@Driver_Type_ID",SqlDbType.Int ,0,objIDriverView.DriverDetailsView.Driver_Type_ID ),
            objDAL.MakeInParams("@Blood_Group",SqlDbType.VarChar,5,objIDriverView.DriverDetailsView.BloodGroup) ,
            objDAL.MakeInParams("@IsLicenseAuthenticated",SqlDbType.Bit,1,objIDriverView.DriverDetailsView.IsLicenseAuthenticated),
            objDAL.MakeInParams("@LicenseAuthenticatedBy",SqlDbType.VarChar,100,objIDriverView.DriverDetailsView.LicenseAuthenticatedBy),
            objDAL.MakeInParams("@IseCargoUser", SqlDbType.Bit, 0, objIDriverView.DriverDetailsView.IseCargoUser),

            objDAL.MakeInParams("@Nick_Name", SqlDbType.VarChar, 50,objIDriverView.DriverDetailsView.DriverNickName),
            objDAL.MakeInParams("@AadharNo", SqlDbType.VarChar, 20,objIDriverView.DriverDetailsView.AadharNo),
            objDAL.MakeInParams("@HistoryRemark", SqlDbType.VarChar, 1000,objIDriverView.DriverDetailsView.HistoryRemarks),
            objDAL.MakeInParams("@Reference_Name2", SqlDbType.VarChar, 100,objIDriverView.DriverDetailsView.ReferenceName2),
            objDAL.MakeInParams("@Reference_Phone2", SqlDbType.VarChar, 25,objIDriverView.DriverDetailsView.ReferencePhone2),
            objDAL.MakeInParams("@Reference_Mobile2", SqlDbType.VarChar, 25,objIDriverView.DriverDetailsView.ReferenceMobile2),
            objDAL.MakeInParams("@ReferenceDate", SqlDbType.DateTime, 0,objIDriverView.DriverDetailsView.ReferenceDate ),
            objDAL.MakeInParams("@ReferenceDate2", SqlDbType.DateTime, 0,objIDriverView.DriverDetailsView.ReferenceDate2 ),
            objDAL.MakeInParams("@License_Issue_State_ID", SqlDbType.Int, 0, objIDriverView.DriverDetailsView.LicenseIssueStateID )


            };

            objDAL.RunProc("rstil7.EF_Master_Driver_Save", objSqlParam);

            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {
                objIDriverView.ClearVariables();
                string _Msg;
                _Msg = "Saved SuccessFully";
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg));
            }
            return objMessage;
        }
    }
} 