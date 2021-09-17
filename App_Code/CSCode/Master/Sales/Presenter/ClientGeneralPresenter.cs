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
using ClassLibraryMVP.General;
/// <summary>
/// Summary description for ClientGeneralPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class ClientGeneralPresenter : Presenter
    {
        private IClientGeneralView objIClientGeneralView;
        private ClientGeneralModel objClientGeneralModel;
        private DataSet objDS;
        Common objCommon = new Common();

        public ClientGeneralPresenter(IClientGeneralView ClientGeneralView, bool isPostback)
        {
            objIClientGeneralView = ClientGeneralView;
            objClientGeneralModel = new ClientGeneralModel(objIClientGeneralView);
            base.Init(objIClientGeneralView, objClientGeneralModel);

            if (!isPostback)
            {
                initValues();
            }
        }

        private void fillValues()
        {
            objDS = objClientGeneralModel.FillValues();
            objIClientGeneralView.BindClientGroup = objDS.Tables[0];
            objIClientGeneralView.BindClientCategory = objDS.Tables[1];
            objIClientGeneralView.BindDeliveryType = objDS.Tables[2];
        }

        public bool Isregularclient()
        {
            bool Is_DuplicateName = objClientGeneralModel.IsCheck_Duplicate();
            return Is_DuplicateName;
        }
        public bool IsregularclientGeneral_Discount()
        {
            bool is_valid = objClientGeneralModel.IsValidateGeneralDiscount();
            return is_valid;
        }


        public void fillDlyArea(int CityID)
        { 
            DataTable dt = new DataTable();
            dt = objCommon.GetDeliveryArea(0, true, true, true, CityID);
            objIClientGeneralView.BindDeliveryArea = dt; 
        }


        private void initValues()
        {
            fillValues();

            if (objIClientGeneralView.keyID > 0)
            {
                objDS = objClientGeneralModel.ReadValues();

                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    objIClientGeneralView.ClientCode = objDR["Client_Code"].ToString();
                    objIClientGeneralView.ClientName = objDR["Client_Name"].ToString();
                    objIClientGeneralView.ClientGroupID = Util.String2Int(objDR["Client_Group_ID"].ToString());
                    objIClientGeneralView.ClientCategoryID = Util.String2Int(objDR["Category_ID"].ToString());
                    objIClientGeneralView.ContactPersonName = objDR["Contact_Person"].ToString();
                    objIClientGeneralView.CSTTINNo = objDR["CST_TIN_No"].ToString();
                    objIClientGeneralView.ServiceTaxNo = objDR["Service_Tax_No"].ToString();
                    objIClientGeneralView.DeliveryAreaId = Convert.ToInt32(objDR["DeliveryAreaID"].ToString());

                    objIClientGeneralView.AddressView.AddressLine1 = objDR["Address1"].ToString();
                    objIClientGeneralView.AddressView.AddressLine2 = objDR["Address2"].ToString();
                    objIClientGeneralView.AddressView.CityId = Util.String2Int(objDR["City_ID"].ToString());
                    objIClientGeneralView.AddressView.PinCode = objDR["Pin_Code"].ToString();
                    objIClientGeneralView.AddressView.StdCode = objDR["Std_Code"].ToString();
                    objIClientGeneralView.AddressView.Phone1 = objDR["Phone1"].ToString();
                    objIClientGeneralView.AddressView.Phone2 = objDR["Phone2"].ToString();
                    objIClientGeneralView.AddressView.MobileNo = objDR["Mobile_No"].ToString();
                    objIClientGeneralView.AddressView.FaxNo = objDR["Fax"].ToString();
                    objIClientGeneralView.AddressView.EmailId = objDR["Email_ID"].ToString();
                    objIClientGeneralView.AddressView.SMSAlert = Util.String2Bool(objDR["SMS_Alert"].ToString());
                    objIClientGeneralView.AddressView.eMailAlert = Util.String2Bool(objDR["eMail_Alert"].ToString());
                    objIClientGeneralView.SetBranchId(objDR["Branch_Name"].ToString(), objDR["Branch_ID"].ToString());
                    objIClientGeneralView.DeliveryTypeID = Util.String2Int(objDR["Delivery_Type_ID"].ToString());
                    objIClientGeneralView.Is_Casual_Taxable = Util.String2Bool(objDR["Is_Casual_Taxable"].ToString());

                    objIClientGeneralView.CreatedBy = objDR["Created_ByName"].ToString();
                    objIClientGeneralView.UpdatedBy = objDR["Updated_ByName"].ToString();

                    objIClientGeneralView.Remarks = objDR["Remarks"].ToString();

                    objIClientGeneralView.Landmark1ID = Convert.ToInt32(objDR["Landmark1ID"].ToString());
                    objIClientGeneralView.Landmark2ID = Convert.ToInt32(objDR["Landmark2ID"].ToString());

                    objIClientGeneralView.Is_OutwardBilling = Util.String2Bool(objDR["Is_OutwardBilling"].ToString());
                    objIClientGeneralView.Is_InwardBilling = Util.String2Bool(objDR["Is_InwardBilling"].ToString());
                }
            }
        }
        public bool CopyRegClient()
        {
            objDS = new DataSet();
            objDS = objClientGeneralModel.CopyRegClient();
            bool isregclient;

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];
                objIClientGeneralView.ClientName = objDR["Client_Name"].ToString();
                objIClientGeneralView.ContactPersonName = objDR["Contact_Person"].ToString();
                objIClientGeneralView.CSTTINNo = objDR["CST_TIN_No"].ToString();
                objIClientGeneralView.ServiceTaxNo = objDR["Service_Tax_No"].ToString();
                objIClientGeneralView.IsServiceTaxPay = Convert.ToBoolean(objDR["Is_Service_Tax_Applicable"].ToString());

                objIClientGeneralView.AddressView.AddressLine1 = objDR["Address1"].ToString();
                objIClientGeneralView.AddressView.AddressLine2 = objDR["Address2"].ToString();
                objIClientGeneralView.AddressView.CityId = Util.String2Int(objDR["City_ID"].ToString());
                objIClientGeneralView.AddressView.PinCode = objDR["Pin_Code"].ToString();
                objIClientGeneralView.AddressView.StdCode = objDR["Std_Code"].ToString();
                objIClientGeneralView.AddressView.Phone1 = objDR["Phone1"].ToString();
                objIClientGeneralView.AddressView.Phone2 = objDR["Phone2"].ToString();
                objIClientGeneralView.AddressView.MobileNo = objDR["Mobile_No"].ToString();
                objIClientGeneralView.AddressView.FaxNo = objDR["Fax"].ToString();
                objIClientGeneralView.AddressView.EmailId = objDR["Email_ID"].ToString();
                objIClientGeneralView.AddressView.SetCityId(objDR["City_name"].ToString(), objDR["City_ID"].ToString());

                fillDlyArea(Util.String2Int(objDR["City_ID"].ToString()));
                objIClientGeneralView.DeliveryAreaId = Util.String2Int(objDR["DeliveryAreaID"].ToString());
                objIClientGeneralView.Is_Casual_Taxable = Util.String2Bool(objDR["Is_Casual_Taxable"].ToString());

                isregclient = true;
                
            }
            else
                isregclient = false;

            return isregclient;
        }
    }
}
