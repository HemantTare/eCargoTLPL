using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.SalesView;
using Raj.EC.SalesModel;

/// <summary>
/// Summary description for RegularClientPresenter
/// </summary>
namespace Raj.EC.SalesPresenter
{
    public class RegularClientPresenter : Presenter
    {
        private IRegularClientView objIRegularClientView;
        private RegularClientModel objRegularClientModel;
        private DataSet objDS;

        public RegularClientPresenter(IRegularClientView regularClientView, bool IsPostBack)
        {
            objIRegularClientView = regularClientView;
            objRegularClientModel = new RegularClientModel(objIRegularClientView);

            base.Init(objIRegularClientView, objRegularClientModel);

            if (!IsPostBack)
            {

                initValues();
            }
        }


        private void fillValues()
        {
            objDS = objRegularClientModel.FillValues();
            objIRegularClientView.BindClientGroup = objDS.Tables[0];
            objIRegularClientView.BindClientCategory = objDS.Tables[1];
            objIRegularClientView.BindDeliveryType = objDS.Tables[2]; 
        }

        
        private void initValues()
        {
            fillValues();

            if (objIRegularClientView.keyID > 0)
            {
                objDS = objRegularClientModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIRegularClientView.RegularClientName = DR["Client_Name"].ToString();
                    objIRegularClientView.ContactPerson = DR["Contact_Person"].ToString();
                    objIRegularClientView.AddressView.AddressLine1 = DR["Address1"].ToString();
                    objIRegularClientView.AddressView.AddressLine2 = DR["Address2"].ToString();
                    objIRegularClientView.AddressView.CityId = Util.String2Int(DR["City_ID"].ToString());
                    objIRegularClientView.AddressView.PinCode = DR["Pin_Code"].ToString();
                    objIRegularClientView.AddressView.StdCode = DR["std_Code"].ToString();
                    objIRegularClientView.AddressView.Phone1 = DR["Phone1"].ToString();
                    objIRegularClientView.AddressView.Phone2 = DR["Phone2"].ToString();
                    objIRegularClientView.AddressView.MobileNo = DR["Mobile_No"].ToString();
                    objIRegularClientView.AddressView.FaxNo = DR["Fax"].ToString();
                    objIRegularClientView.AddressView.EmailId = DR["Email_ID"].ToString();
                    objIRegularClientView.AddressView.SMSAlert = Util.String2Bool(DR["SMS_Alert"].ToString());
                    objIRegularClientView.AddressView.eMailAlert = Util.String2Bool(DR["eMail_Alert"].ToString());
                    objIRegularClientView.CSTNo = DR["CST_TIN_No"].ToString();
                    objIRegularClientView.ServiceTaxNo = DR["Service_Tax_No"].ToString();
                    objIRegularClientView.IsServiceTaxPayable = Util.String2Bool(DR["Is_Service_Tax_Applicable"].ToString());
                    objIRegularClientView.Is_Casual_Taxable = Util.String2Bool(DR["Is_Casual_Taxable"].ToString());
                   
                    objIRegularClientView.DeliveryAreaId = Convert.ToInt32(DR["DeliveryAreaID"].ToString());
                    objIRegularClientView.ContractualClientId = Util.String2Int(DR["Contractual_Client_Id"].ToString());
                    objIRegularClientView.ClientCategoryID = Util.String2Int(DR["Category_ID"].ToString());
                    objIRegularClientView.DeliveryTypeID = Util.String2Int(DR["Delivery_Type_ID"].ToString());
                    objIRegularClientView.Is_ToPay_Allowed = Util.String2Bool(DR["Is_ToPay_Allowed"].ToString());
                    objIRegularClientView.ClientGroupID = Util.String2Int(DR["Client_Group_ID"].ToString());

                    objIRegularClientView.CreatedBy = DR["Created_ByName"].ToString();
                    objIRegularClientView.UpdatedBy = DR["Updated_ByName"].ToString();

                    objIRegularClientView.Remarks = DR["Remarks"].ToString();

                    objIRegularClientView.Landmark1ID = Convert.ToInt32(DR["Landmark1ID"].ToString());
                    objIRegularClientView.Landmark2ID = Convert.ToInt32(DR["Landmark2ID"].ToString());  
                    objIRegularClientView.GSTName = DR["GSTName"].ToString();
                    objIRegularClientView.IsWithCompleteDetails = Util.String2Bool(DR["IsWithCompleteDetails"].ToString());
                }
            }
        }
        public void Save()
        {
           // base.DBSave();
            objRegularClientModel.Save();
        }
	}
}
