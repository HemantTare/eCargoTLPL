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
using Raj.EF.MasterView;
using Raj.EF.MasterModel;


/// <summary>
/// Summary description for VendorPresenter
/// </summary>
namespace Raj.EF.MasterPresenter
{
    public class VendorPresenter : Presenter
    {
        private IVendorView objVendorView;
        private VendorModel objVendorModel;
        private DataSet objDS;

        public VendorPresenter(IVendorView VendorView, bool isPostBack)
        {
            objVendorView = VendorView;
            objVendorModel = new VendorModel(objVendorView);
            base.Init(objVendorView, objVendorModel);

            if (!isPostBack)
            {
                //FillTds();
                FillVendorType();
                initValues();
            }

        }
        //public void FillTds()
        //{

        //    objVendorView.BindTds = objVendorModel.FillValues();

        //}
        public void FillVendorType()
        {
            objVendorView.BindVendorType = objVendorModel.FillVendorType();
        }


        //public Boolean Duplicate_PANNo_Check()  //added by ANkit : 19/11/08 : 5.30 pm
        //{
        //    Boolean _IsDuplicate;
        //    _IsDuplicate = objVendorModel.ISDuplicatePANNoCheck();
        //    return _IsDuplicate;
        //}

        private void initValues()
        {
            if (objVendorView.keyID > 0)
            {
                objDS = objVendorModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objVendorView.VendorName = objDS.Tables[0].Rows[0]["Vendor_Name"].ToString();
                    objVendorView.AddressView.AddressLine1 = objDS.Tables[0].Rows[0]["Address_1"].ToString();
                    objVendorView.AddressView.AddressLine2 = objDS.Tables[0].Rows[0]["Address_2"].ToString();
                    objVendorView.AddressView.CityId = Convert.ToInt32(objDS.Tables[0].Rows[0]["City_ID"]);
                    objVendorView.AddressView.PinCode = objDS.Tables[0].Rows[0]["Pin_Code"].ToString();
                    objVendorView.AddressView.StdCode = objDS.Tables[0].Rows[0]["Std_Code"].ToString();
                    objVendorView.AddressView.Phone1 = objDS.Tables[0].Rows[0]["Phone_1"].ToString();
                    objVendorView.AddressView.Phone2 = objDS.Tables[0].Rows[0]["Phone_2"].ToString();
                    objVendorView.AddressView.MobileNo = objDS.Tables[0].Rows[0]["Mobile_No"].ToString();
                    objVendorView.AddressView.FaxNo = objDS.Tables[0].Rows[0]["Fax"].ToString();
                    objVendorView.AddressView.EmailId = objDS.Tables[0].Rows[0]["Email_ID"].ToString();
                    objVendorView.ReferenceName = objDS.Tables[0].Rows[0]["Reference_Name"].ToString();
                    objVendorView.ReferencePhone = objDS.Tables[0].Rows[0]["Reference_phone"].ToString();
                    objVendorView.ReferenceMobile = objDS.Tables[0].Rows[0]["Reference_Mobile"].ToString();
                    objVendorView.Credit_Days = Convert.ToInt32(objDS.Tables[0].Rows[0]["Credit_Days"]);
                    objVendorView.Credit_Limit = Convert.ToInt32(objDS.Tables[0].Rows[0]["Credit_Limit"]);
                    objVendorView.Debit_BalLimmit = Convert.ToInt32(objDS.Tables[0].Rows[0]["Debit_BalLimmit"]);
                    //objVendorView.IsTdsApplicable = Util.String2Bool(objDS.Tables[0].Rows[0]["Is_TDS_Applicable"].ToString());
                    //objVendorView.TdsId=Util.String2Int(objDS.Tables[0].Rows[0]["TDS_Id"].ToString());
                    objVendorView.VendorTypeId = Convert.ToInt32(objDS.Tables[0].Rows[0]["Vendor_Type_ID"]);
                    //objVendorView.TdsExemptionLimit=Util.String2Decimal(objDS.Tables[0].Rows[0]["TDs_Exemption_Limit"].ToString());
                    objVendorView.PanNo = objDS.Tables[0].Rows[0]["Pan_No"].ToString();
                    objVendorView.APMCBroker_To_City = objDS.Tables[0].Rows[0]["APMCBroker_ToCity"].ToString();

                }
            }
        }

        public void Save()
        {

            base.DBSave();
            //objVendorModel.Save();
        }
    }
}


