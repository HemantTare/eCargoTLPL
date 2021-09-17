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
/// Summary description for CompanyParametersPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class CompanyParametersPresenter : Presenter
    {
        private ICompanyParametersView objICompanyParametersView;
        private CompanyParametersModel objCompanyParametersModel;
        private DataSet objDS;

        public CompanyParametersPresenter(ICompanyParametersView companyParametersView, bool IsPostBack)
        {
            objICompanyParametersView = companyParametersView;
            objCompanyParametersModel = new CompanyParametersModel(objICompanyParametersView);

            base.Init(objICompanyParametersView, objCompanyParametersModel);

            if (!IsPostBack)
            {
                FillValues();
                initValues();
            }
        }

        public void Save()
        {
            //base.DBSave();
            objCompanyParametersModel.Save();
        }
        public void FillValues()
        {
            objDS = objCompanyParametersModel.FillValues();
            objICompanyParametersView.BindStdBasicFreightUnit = objDS;           

        }
        private void initValues()
        {


            
                objDS = objCompanyParametersModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objICompanyParametersView.CompanyId = Util.String2Int(DR["CompanyId"].ToString());
                    objICompanyParametersView.IsActivateDivision = Util.String2Bool(DR["Is_Activate_Divisions"].ToString());
                    objICompanyParametersView.IsAccTransferRequired = Util.String2Bool(DR["Is_Account_Transfer_Required"].ToString());
                    objICompanyParametersView.IsActivateCoLoaderBusiness = Util.String2Bool(DR["Is_Co_Loader_Business"].ToString());
                    objICompanyParametersView.StdBasicFreightUnit = Util.String2Int(DR["Standard_Basic_Freight_Unit_ID"].ToString());
                    objICompanyParametersView.StdFreightRateForSundry = Util.String2Decimal(DR["Standard_Freight_Rate_Per"].ToString());
                    objICompanyParametersView.IsBookOwnTruckHire = Util.String2Bool(DR["Is_Book_Own_Truck_Hire"].ToString());
                    objICompanyParametersView.IsMarketTruckLedgerAccTruckWise = Util.String2Bool(DR["Is_Market_Truck_Ledger_Account_Truck_Wise"].ToString());
                    objICompanyParametersView.IsAttachedTruckLedgerAccTruckWise = Util.String2Bool(DR["Is_Attached_Truck_Ledger_Account_Truck_Wise"].ToString());
                    objICompanyParametersView.IsManagedTruckLedgerAccTruckWise = Util.String2Bool(DR["is_Managed_Truck_Ledger_Account_Truck_Wise"].ToString());
                    objICompanyParametersView.IsPartLoadingRequired = Util.String2Bool(DR["Is_Part_Loading_Required"].ToString());
                    objICompanyParametersView.MinDiffMEMOandTAS = Util.String2Int(DR["Minutes_Diff_Between_MEMO_And_TAS"].ToString());
                    objICompanyParametersView.IsGCNumberEditable = Util.String2Bool(DR["Is_GC_Number_Editable"].ToString());
                    objICompanyParametersView.IsContractRequiredForTBBGC = Util.String2Bool(DR["Is_Contract_Required_For_TBB_GC"].ToString());


                }
            }
        }
	}
