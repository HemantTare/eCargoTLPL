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
/// Summary description for Driver InsuranceDependentPresenter
/// </summary>
/// 

namespace Raj.EF.MasterPresenter
{
    public class DriverInsuranceDependentPresenter:Presenter 
    {
        private IDriverInsuranceDependentView objIDriverInsuranceDependentView;
        private DriverInsuranceDependentModel objDriverInsuranceDependentModel;
        private DataSet _objDS;

        public DriverInsuranceDependentPresenter(IDriverInsuranceDependentView driverInsuranceDependentView,bool isPostBack)
        {
            objIDriverInsuranceDependentView = driverInsuranceDependentView;
            objDriverInsuranceDependentModel = new DriverInsuranceDependentModel(objIDriverInsuranceDependentView );
            base.Init(objIDriverInsuranceDependentView , objDriverInsuranceDependentModel);

            if (!isPostBack)
            {
                FillAllDropdownsAndGrid();

                if (objIDriverInsuranceDependentView .keyID > 0)
                {
                    initValues();
                }
                else
                {
                    objIDriverInsuranceDependentView.InsuranceExpiryDate = DateTime.Now;
                }
            }
        }

        private void FillAllDropdownsAndGrid()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ds.Tables.Add(dt);
            _objDS = objDriverInsuranceDependentModel.FillValues();
            objIDriverInsuranceDependentView.BindInsuranceCompany = _objDS.Tables[0];
            objIDriverInsuranceDependentView.BindNomineeRelation = _objDS.Tables[1];
            objIDriverInsuranceDependentView.SessionDepRelationDropDown = _objDS.Tables[1];
            objIDriverInsuranceDependentView.BindInsuranceAgent = _objDS.Tables[2];
            objIDriverInsuranceDependentView.SessionDependentDetailsGrid = objDriverInsuranceDependentModel.BindGrid();
            objIDriverInsuranceDependentView.BindInsuranceBranch = ds;
        }

        public void FillInsuranceBranchOnInsuranceCompanyChange()
        {
            objIDriverInsuranceDependentView.BindInsuranceBranch = objDriverInsuranceDependentModel.FillInsuranceBranchOnInsuranceCompanyChange();
        }

        private void initValues()
        {
            _objDS = objDriverInsuranceDependentModel.ReadValues();
            if (_objDS.Tables[0].Rows.Count > 0)
            {
                DataRow dr = _objDS.Tables[0].Rows[0];
                objIDriverInsuranceDependentView.NomineeName = dr["Nominee_Name"].ToString();
                objIDriverInsuranceDependentView.PolicyNumber = dr["Policy_No"].ToString();

                objIDriverInsuranceDependentView.InsuranceCompanyID = Util.String2Int(dr["Insurance_Company_ID"].ToString());
                FillInsuranceBranchOnInsuranceCompanyChange();
                objIDriverInsuranceDependentView.InsuranceBranchID = Util.String2Int(dr["Insurance_Branch_ID"].ToString());
                objIDriverInsuranceDependentView.InsuranceAgentID = Util.String2Int(dr["Insurance_Agent_ID"].ToString());
                objIDriverInsuranceDependentView.NomineeRelationID = Util.String2Int(dr["Nominee_Relation_ID"].ToString());

                objIDriverInsuranceDependentView.InsurancePremium = Util.String2Decimal(dr["Insurance_Premium"].ToString());
                objIDriverInsuranceDependentView.SumAssured = Util.String2Decimal(dr["Sum_Assured"].ToString());

                objIDriverInsuranceDependentView.InsuranceExpiryDate = Convert.ToDateTime(dr["Insurance_Expiry_Date"].ToString());

                objIDriverInsuranceDependentView.SessionDependentDetailsGrid = objDriverInsuranceDependentModel.BindGrid();
            }
        }
    }
}
