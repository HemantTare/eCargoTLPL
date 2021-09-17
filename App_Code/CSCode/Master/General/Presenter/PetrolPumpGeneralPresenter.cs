using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;
using ClassLibraryMVP;
//using Raj.eCargo.Init;

/// <summary> 
/// Summary description for PetrolPumpGeneralPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class PetrolPumpGeneralPresenter : ClassLibraryMVP.General.Presenter 
    {

        private IPetrolPumpGeneralView objIPetrolPumpGeneralView;
        private PetrolPumpGeneralModel objPetrolPumpGeneralModel;
        private DataSet objDS;

        public PetrolPumpGeneralPresenter(IPetrolPumpGeneralView PetrolPumpGeneralView, bool IsPostBack)
        {
            objIPetrolPumpGeneralView = PetrolPumpGeneralView;
            objPetrolPumpGeneralModel = new PetrolPumpGeneralModel(objIPetrolPumpGeneralView);

            base.Init(objIPetrolPumpGeneralView, objPetrolPumpGeneralModel);

            if (!IsPostBack)
            {                                   
                initValues();                 
            }
        }

       

        private void initValues()
        {
            

            if (objIPetrolPumpGeneralView.keyID > 0)
            {
                ReadValues();
            }
        }

        public  void ReadValues()
        {
            objDS = objPetrolPumpGeneralModel.ReadValues();
            
            if (objDS.Tables[0].Rows.Count > 0)
            {
                objIPetrolPumpGeneralView.PetrolPumpName = Convert.ToString(objDS.Tables[0].Rows[0]["Petrol_Pump_Name"]);

                objIPetrolPumpGeneralView.PetrolCompany = Convert.ToString(objDS.Tables[0].Rows[0]["Petrol_Company"]);
                objIPetrolPumpGeneralView.MailingName = Convert.ToString(objDS.Tables[0].Rows[0]["Mailing_Name"]);

                objIPetrolPumpGeneralView.TINNo = Convert.ToString(objDS.Tables[0].Rows[0]["TIN_No"]);
                objIPetrolPumpGeneralView.CSTNo = Convert.ToString(objDS.Tables[0].Rows[0]["CST_No"]);

                objIPetrolPumpGeneralView.ContactPerson = Convert.ToString(objDS.Tables[0].Rows[0]["Contact_Person"]);

                objIPetrolPumpGeneralView.AddressView.CityId = Convert.ToInt32(objDS.Tables[0].Rows[0]["City_ID"]);

                objIPetrolPumpGeneralView.AddressView.AddressLine1 = Convert.ToString(objDS.Tables[0].Rows[0]["Address_Line_1"]);
                objIPetrolPumpGeneralView.AddressView.AddressLine2   = Convert.ToString(objDS.Tables[0].Rows[0]["Address_Line_2"]);

                objIPetrolPumpGeneralView.AddressView.EmailId = Convert.ToString(objDS.Tables[0].Rows[0]["Email_ID"]);
                objIPetrolPumpGeneralView.AddressView.MobileNo = Convert.ToString(objDS.Tables[0].Rows[0]["Mobile"]);

                objIPetrolPumpGeneralView.AddressView.Phone1 = Convert.ToString(objDS.Tables[0].Rows[0]["Phone_1"]);

                objIPetrolPumpGeneralView.AddressView.Phone2= Convert.ToString(objDS.Tables[0].Rows[0]["Phone_2"]);

                objIPetrolPumpGeneralView.AddressView.PinCode = Convert.ToString(objDS.Tables[0].Rows[0]["Pin_Code"]);

                objIPetrolPumpGeneralView.AddressView.StdCode = Convert.ToString(objDS.Tables[0].Rows[0]["STD_Code"]);


                //objIPetrolPumpGeneralView.AddressView.FaxNo = Convert.ToString(objDS.Tables[0].Rows[0]["FaxNo"]);
                
                  
            }
        }
        public void save()
        {
            
            //base.DBSave();
            objPetrolPumpGeneralModel.Save();
        }


         


      

    }
}