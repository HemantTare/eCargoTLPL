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
/// Summary description for InsuranceCompanyBranchPresenter
/// </summary>
namespace Raj.EF.MasterPresenter
{
    public class InsuranceCompanyBranchPresenter : Presenter
    {
        private IInsuranceCompanyBranchView objIInsuranceCompanyBranchView;
        private InsuranceCompanyBranchModel objInsuranceCompanyBranchModel;
        private DataSet objDS;

        public InsuranceCompanyBranchPresenter(IInsuranceCompanyBranchView insuranceCompanyBranchView, bool isPostBack)
        {
            objIInsuranceCompanyBranchView = insuranceCompanyBranchView;
            objInsuranceCompanyBranchModel = new InsuranceCompanyBranchModel(objIInsuranceCompanyBranchView);
            base.Init(objIInsuranceCompanyBranchView, objInsuranceCompanyBranchModel);

            if (!isPostBack)
            {
                FillInsuranceCompany();
                initValues();
            }

        }
        public void FillInsuranceCompany()
        {

            objIInsuranceCompanyBranchView.BindInsuranceCompany = objInsuranceCompanyBranchModel.FillValues();

        }
        
        

        private void initValues()
        {
            if (objIInsuranceCompanyBranchView.keyID > 0)
            {
                objDS = objInsuranceCompanyBranchModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIInsuranceCompanyBranchView.BranchName = objDS.Tables[0].Rows[0]["Branch_Name"].ToString();
                    objIInsuranceCompanyBranchView.ContactPerson = objDS.Tables[0].Rows[0]["Contact_Person"].ToString();
                    objIInsuranceCompanyBranchView.AddressView.AddressLine1 = objDS.Tables[0].Rows[0]["Address_1"].ToString();
                    objIInsuranceCompanyBranchView.AddressView.AddressLine2 = objDS.Tables[0].Rows[0]["Address_2"].ToString();
                    objIInsuranceCompanyBranchView.AddressView.CityId = Util.String2Int(objDS.Tables[0].Rows[0]["City_ID"].ToString());
                   // objIInsuranceCompanyBranchView.AddressView.SetLables=objDS.Tables[1].Rows[0]["C
                    objIInsuranceCompanyBranchView.AddressView.PinCode = objDS.Tables[0].Rows[0]["Pin_Code"].ToString();
                    objIInsuranceCompanyBranchView.AddressView.StdCode = objDS.Tables[0].Rows[0]["Std_Code"].ToString();
                    objIInsuranceCompanyBranchView.AddressView.Phone1 = objDS.Tables[0].Rows[0]["Phone_1"].ToString();
                    objIInsuranceCompanyBranchView.AddressView.Phone2 = objDS.Tables[0].Rows[0]["Phone_2"].ToString();
                    objIInsuranceCompanyBranchView.AddressView.MobileNo = objDS.Tables[0].Rows[0]["Mobile_No"].ToString();
                    objIInsuranceCompanyBranchView.AddressView.FaxNo = objDS.Tables[0].Rows[0]["Fax"].ToString();
                    objIInsuranceCompanyBranchView.AddressView.EmailId = objDS.Tables[0].Rows[0]["Email_ID"].ToString();
                    objIInsuranceCompanyBranchView.InsuranceCompanyId= Util.String2Int(objDS.Tables[0].Rows[0]["Insurance_Company_Id"].ToString());
                   
                }
            }
        }

        public void Save()
        {

            base.DBSave();
            //objInsuranceCompanyBranchModel.Save();
        }
    }
}
