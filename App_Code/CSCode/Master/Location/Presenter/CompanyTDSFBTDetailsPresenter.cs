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
using Raj.EC.MasterView;
using Raj.EC.MasterModel;


/// <summary>
/// Summary description for CompanyTDSFBTDetailsPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class CompanyTDSFBTDetailsPresenter : Presenter
    {
        private ICompanyTDSFBTDetailsView objICompanyTDSFBTDetailsView;
        private CompanyTDSFBTDetailsModel objCompanyTDSFBTDetailsModel;
        private DataSet objDS;

        public CompanyTDSFBTDetailsPresenter(ICompanyTDSFBTDetailsView companyTDSFBTDetailsView, bool IsPostBack)
        {
            objICompanyTDSFBTDetailsView = companyTDSFBTDetailsView;
            objCompanyTDSFBTDetailsModel = new CompanyTDSFBTDetailsModel(objICompanyTDSFBTDetailsView);

            base.Init(objICompanyTDSFBTDetailsView, objCompanyTDSFBTDetailsModel);

            if (!IsPostBack)
            {
                FillValues();
                initValues();
            }
        }

         public void Save()
        {
            //base.DBSave();
            objCompanyTDSFBTDetailsModel.Save();
        }
        public void FillValues()
        {
            objDS = objCompanyTDSFBTDetailsModel.FillValues();
            objICompanyTDSFBTDetailsView.BindPersonResponsible = objDS.Tables["dbo.EC_Master_Employee"];
            objICompanyTDSFBTDetailsView.BindAssesseeType = objDS.Tables["dbo.FA_Mst_FBT_Assessee_Type"];
            objICompanyTDSFBTDetailsView.BindAssesseeCategory = objDS.Tables["dbo.FA_Mst_FBT_Assessee_Category"];
            objICompanyTDSFBTDetailsView.BindDeducteeType = objDS.Tables["dbo.FA_Mst_Ledger_TDS_Deductee_Type"];
           
        }


        private void initValues()
        {


           
                objDS = objCompanyTDSFBTDetailsModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objICompanyTDSFBTDetailsView.TaxAssessmentNumber = DR["TAN_No"].ToString();
                    objICompanyTDSFBTDetailsView.IncomeTaxCircle = DR["Income_Tax_Circle"].ToString();
                    objICompanyTDSFBTDetailsView.Designation = DR["Designation"].ToString();
                    objICompanyTDSFBTDetailsView.DeductorType = DR["Deductor_Type"].ToString();
                    objICompanyTDSFBTDetailsView.PersonResponsible = Util.String2Int(DR["Employee_Id"].ToString());
                    objICompanyTDSFBTDetailsView.IsAllowSelectionFBTCategory = Util.String2Bool(DR["Allow_FBT_Category_Selection"].ToString());
                    objICompanyTDSFBTDetailsView.PanNo = DR["Pan_No"].ToString();
                    objICompanyTDSFBTDetailsView.AssesseeType = Util.String2Int(DR["Assessee_Type_Id"].ToString());
                    objICompanyTDSFBTDetailsView.IsSurchargeApplicable = Util.String2Bool(DR["Is_Surcharge_Applicable"].ToString());
                    objICompanyTDSFBTDetailsView.AssesseeCategory = Util.String2Int(DR["Assessee_Category_Id"].ToString());

                }
            }

        }
       
	}
