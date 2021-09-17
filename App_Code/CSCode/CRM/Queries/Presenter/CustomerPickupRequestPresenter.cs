using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.CRM.QueryView;
using Raj.CRM.QueryModel;
/// <summary>
/// Summary description for Customer_PickupRequestPresenter
/// </summary>
/// 
namespace Raj.CRM.QueryPresenter
{
    public class CustomerPickupRequestPresenter:Presenter
    {
        private ICustomerPickupRequestView objICustomerPickupRequestView;
        private CustomerPickupRequestModel objCustomerPickupRequestModel;
        private DataSet _objDS;

        public CustomerPickupRequestPresenter(ICustomerPickupRequestView CustomerPickupRequestView, bool isPostback)
        {
            objICustomerPickupRequestView = CustomerPickupRequestView;
            objCustomerPickupRequestModel = new CustomerPickupRequestModel(objICustomerPickupRequestView);
            base.Init(objICustomerPickupRequestView, objCustomerPickupRequestModel);

            if (!isPostback)
            {
                FillAllDropdowns();
                if (objICustomerPickupRequestView.keyID > 0)
                {
                    initValues();
                }
                else
                {
                    Fill_VA_On_Branch_Selection();
                    objICustomerPickupRequestView.PickUpDate = DateTime.Now.Date;
                }
            }
        }

        public void Fill_VA_On_Branch_Selection()
        {
            _objDS = objCustomerPickupRequestModel.Fill_VA_On_Branch_Selection();
            objICustomerPickupRequestView.BindForwardVA = _objDS.Tables[0];
        }

        private void FillAllDropdowns()
        {
            _objDS = objCustomerPickupRequestModel.FillValues();
            objICustomerPickupRequestView.BindBookingTypeMode = _objDS.Tables[0];
            objICustomerPickupRequestView.BindPackingType = _objDS.Tables[1];
            objICustomerPickupRequestView.BindCommodity = _objDS.Tables[2];
        }

        public void Save()
        {
            //objCustomerPickupRequestModel.Save();
            base.DBSave();
        }

        private void initValues()
        {
            _objDS = objCustomerPickupRequestModel.ReadValues();

            if (_objDS.Tables[0].Rows.Count > 0)
            {
                DataRow DR = _objDS.Tables[0].Rows[0];
                objICustomerPickupRequestView.PickUpNo = DR["Pickup_No"].ToString();
                objICustomerPickupRequestView.Orgin = DR["Origin"].ToString();
                objICustomerPickupRequestView.Destination = DR["Destination"].ToString();
                objICustomerPickupRequestView.PickUpDate = Convert.ToDateTime(DR["Pickup_Date"]);
                objICustomerPickupRequestView.PickUpTime = DR["PickUp_Time"].ToString();
                objICustomerPickupRequestView.Weight = Convert.ToDecimal(DR["Weight"]);
                objICustomerPickupRequestView.Packeges = Convert.ToInt32(DR["Packages"]);
                objICustomerPickupRequestView.Consigner = DR["Name_Of_Consignor"].ToString();
                objICustomerPickupRequestView.BookingTypeModeId = Convert.ToInt32(DR["Booking_Type_Id"]);
                objICustomerPickupRequestView.PackingTypeId = Convert.ToInt32(DR["Packing_Type_Id"]);
                objICustomerPickupRequestView.CommodityId = Convert.ToInt32(DR["Commodity_Id"]);
                objICustomerPickupRequestView.ContactName = DR["Contact_Name"].ToString();
                objICustomerPickupRequestView.ContactMobile = DR["Contact_Mobile_No"].ToString();
                objICustomerPickupRequestView.ContactAddress = DR["Contact_Address"].ToString();
                objICustomerPickupRequestView.ContactTelephone = DR["Contact_Telephone"].ToString();
                objICustomerPickupRequestView.ContactEmailId = DR["Contact_Email"].ToString();
                objICustomerPickupRequestView.ContactCity = DR["City"].ToString();
                objICustomerPickupRequestView.ContactState = DR["State"].ToString();
                objICustomerPickupRequestView.SetForwardBranchId(DR["Branch_ID"].ToString(), DR["Branch_Name"].ToString());
               
                Fill_VA_On_Branch_Selection();
                objICustomerPickupRequestView.VendorId = Convert.ToInt32(DR["VA_ID"]);
                objICustomerPickupRequestView.Reason = DR["Reason"].ToString();
                GetVaMobNo();
            }
        }

        public string GetVaMobNo()
        {
           return objCustomerPickupRequestModel.GetVaMobNo();
        }
    }
}