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
using ClassLibraryMVP.DataAccess ;
using ClassLibraryMVP.General;
using Raj.EC.MasterView;

using Raj.EC.Security; 

 
 


namespace  Raj.EC.MasterModel
{
    /// <summary>
    /// Summary description for PetrolPumpModel
    /// </summary>
    public class PetrolPumpModel : ClassLibraryMVP.General.IModel    
    {
      
        private IPetrolPumpView objIPetrolPumpView;
        private DAL objDAL = new DAL();
 

        private int _userId = Convert.ToInt32(UserManager.getUserParam().UserId);

        public PetrolPumpModel(IPetrolPumpView PetrolPumpView)
        {
            objIPetrolPumpView = PetrolPumpView;
        }


        public DataSet ReadValues()
        {
          
            DataSet DS = null;
            return DS;
        }

        public Message Save()
        {
            Message objMessage = new Message();

 

            SqlParameter[] objSqlParam = {objDAL.MakeOutParams("@Error_Code", SqlDbType.Int, 0), 
            objDAL.MakeOutParams("@Error_Desc", SqlDbType.VarChar, 4000),
            objDAL.MakeInParams("@Petrol_Pump_ID", SqlDbType.Int, 0,objIPetrolPumpView.PetrolPumpGeneralView.keyID),
            objDAL.MakeInParams("@Petrol_Pump_Name", SqlDbType.VarChar, 50,objIPetrolPumpView.PetrolPumpGeneralView.PetrolPumpName),            
            objDAL.MakeInParams("@Mailing_Name", SqlDbType.VarChar, 100,objIPetrolPumpView.PetrolPumpGeneralView.MailingName ),
            objDAL.MakeInParams("@Petrol_Company", SqlDbType.VarChar, 100,objIPetrolPumpView.PetrolPumpGeneralView.PetrolCompany ),                        
            objDAL.MakeInParams("@Contact_Person", SqlDbType.VarChar, 100,objIPetrolPumpView.PetrolPumpGeneralView.ContactPerson ),
            objDAL.MakeInParams("@Address_Line_1", SqlDbType.VarChar, 25,objIPetrolPumpView.PetrolPumpGeneralView.AddressView.AddressLine1 ),
            objDAL.MakeInParams("@Address_Line_2", SqlDbType.VarChar, 25,objIPetrolPumpView.PetrolPumpGeneralView.AddressView.AddressLine2 ),
            objDAL.MakeInParams("@Pin_Code", SqlDbType.VarChar, 100,objIPetrolPumpView.PetrolPumpGeneralView.AddressView.PinCode ),                                    
            objDAL.MakeInParams("@Mobile", SqlDbType.NVarChar, 25,objIPetrolPumpView.PetrolPumpGeneralView.AddressView.MobileNo),
            objDAL.MakeInParams("@Email_ID", SqlDbType.NVarChar, 20,objIPetrolPumpView.PetrolPumpGeneralView.AddressView.EmailId ),            
            objDAL.MakeInParams("@City_ID", SqlDbType.Int, 0, objIPetrolPumpView.PetrolPumpGeneralView.AddressView.CityId),
            objDAL.MakeInParams("@STD_Code", SqlDbType.NVarChar, 30, objIPetrolPumpView.PetrolPumpGeneralView.AddressView.StdCode  ),
            objDAL.MakeInParams("@Phone_1", SqlDbType.NVarChar, 0, objIPetrolPumpView.PetrolPumpGeneralView.AddressView.Phone1 ),
            objDAL.MakeInParams("@Phone_2", SqlDbType.NVarChar, 0, objIPetrolPumpView.PetrolPumpGeneralView.AddressView.Phone2 ),                        
            objDAL.MakeInParams("@CST_No", SqlDbType.NVarChar, 50,objIPetrolPumpView.PetrolPumpGeneralView.CSTNo ),
            objDAL.MakeInParams("@TIN_No", SqlDbType.NVarChar, 0,objIPetrolPumpView.PetrolPumpGeneralView.TINNo ), 
            objDAL.MakeInParams("@Ledger_Group_ID", SqlDbType.Int , 0,objIPetrolPumpView.PetrolPumpFinanceDetailsView.LedgerGroupId ), 
            objDAL.MakeInParams("@Ledger_ID", SqlDbType.Int , 0,objIPetrolPumpView.PetrolPumpFinanceDetailsView.LedgerId  ), 
            objDAL.MakeInParams("@Credit_Days",SqlDbType.Int,0,objIPetrolPumpView.PetrolPumpFinanceDetailsView.CreditDays ),
            objDAL.MakeInParams("@Credit_Limit",SqlDbType.Decimal  ,0,objIPetrolPumpView.PetrolPumpFinanceDetailsView.CreditLimit ),
            objDAL.MakeInParams("@Is_Service_Tax_Applicable", SqlDbType.Bit, 0, objIPetrolPumpView.PetrolPumpFinanceDetailsView.Is_Service_Tax_Applicable ),
            objDAL.MakeInParams("@Is_Service_Tax_Exempted", SqlDbType.Bit, 0, objIPetrolPumpView.PetrolPumpFinanceDetailsView.Is_Exempted ),
            objDAL.MakeInParams("@Notification_Details",SqlDbType.VarChar  ,0,objIPetrolPumpView.PetrolPumpFinanceDetailsView.Notification_Detail ),
            objDAL.MakeInParams("@Is_TDS_Applicable", SqlDbType.Bit, 0,objIPetrolPumpView.PetrolPumpFinanceDetailsView.TDSAppView.IsTDSApp ),
            objDAL.MakeInParams("@TDS_Deductee_Type_ID", SqlDbType.Int , 0,objIPetrolPumpView.PetrolPumpFinanceDetailsView.TDSAppView.DeducteeTypeID),
            objDAL.MakeInParams("@TDS_Deductee_Type_Name", SqlDbType.VarChar , 0,objIPetrolPumpView.PetrolPumpFinanceDetailsView.TDSAppView.DeducteeTypeName ),
            objDAL.MakeInParams("@Is_Lower_Deduction", SqlDbType.Bit, 0,objIPetrolPumpView.PetrolPumpFinanceDetailsView.TDSAppView.IsLower ), 
            objDAL.MakeInParams("@Section_Number", SqlDbType.VarChar , 0, objIPetrolPumpView.PetrolPumpFinanceDetailsView.TDSAppView.sectionNo  ),            
            objDAL.MakeInParams("@TDS_Lower_Rate", SqlDbType.VarChar , 0, objIPetrolPumpView.PetrolPumpFinanceDetailsView.TDSAppView.LowerRate ),            
            objDAL.MakeInParams("@Use_Existing_Ledger", SqlDbType.Bit  , 0, objIPetrolPumpView.PetrolPumpFinanceDetailsView.Use_Existing_Ledger  ),                                           
            objDAL.MakeInParams("@Ignore_Surcharge_Exemption_Limit", SqlDbType.Bit, 0, objIPetrolPumpView.PetrolPumpFinanceDetailsView.TDSAppView.IsIgnore),           
            objDAL.MakeInParams("@Created_By", SqlDbType.Int, 0, _userId),
            objDAL.MakeInParams("@Fax", SqlDbType.VarChar , 0, objIPetrolPumpView.PetrolPumpGeneralView.AddressView.FaxNo  ),            
            objDAL.MakeInParams("@Applicable_Divisions_Details_Xml",SqlDbType.Xml,0,objIPetrolPumpView.PetrolPumpFinanceDetailsView.Applicable_Divisions_Details_Xml )};

            objDAL.RunProc("EC_Mst_Petrol_Pump_Save", objSqlParam);

        
            objMessage.messageID = Convert.ToInt32(objSqlParam[0].Value);
            objMessage.message  = Convert.ToString(objSqlParam[1].Value);

            if (objMessage.messageID == 0)
            {

                string _Msg;
                _Msg = "Saved SuccessFully";
                string LinkUrl = ClassLibraryMVP.Security.Rights.GetObject().GetLinkDetails(Common.GetMenuItemId()).LinkUrl;
                System.Web.HttpContext.Current.Response.Redirect("~/RedirectPage.aspx?Msg=" + ClassLibraryMVP.Util.EncryptString(_Msg) + "&Url=" + LinkUrl + "&DecryptUrl='No'");
            }


            return objMessage;
        }
    }
}