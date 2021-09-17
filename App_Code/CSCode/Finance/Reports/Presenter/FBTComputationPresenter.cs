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
using Raj.FA.ReportsView;
using Raj.FA.ReportsModel;

/// <summary>
/// Summary description for FBTComputationPresenter
/// </summary>
namespace Raj.FA.ReportsPresenter
{
    public class FBTComputationPresenter : Presenter
    {
        private IFBTComputationView objIFBTComputationView;
        private FBTComputationModel objFBTComputationModel;
        DataSet objDS;
        int tableNo;

        public FBTComputationPresenter(IFBTComputationView fbtComputationView, bool IsPostBack)
        {
            objIFBTComputationView = fbtComputationView;
            objFBTComputationModel = new FBTComputationModel(objIFBTComputationView);

            base.Init(objIFBTComputationView, objFBTComputationModel);

            if (!IsPostBack)
            {
                GetValues();
                    
            }
        }
             public void GetValues()
            {
               
                    DataSet ds = new DataSet();
                    ds = objFBTComputationModel.GetValues();
                    if (ds.Tables[1].Rows.Count > 0)
                    {
                        DataRow objDR = ds.Tables[1].Rows[0];
                        objIFBTComputationView.TotExpenditureAmount = objDR["Total_Expenditure_Amount"].ToString();
                        objIFBTComputationView.TotAmountRecovered = objDR["Total_Amount_Recovered"].ToString();
                        objIFBTComputationView.TotNetExpenditure = objDR["Total_Net_Expenditure"].ToString();
                        objIFBTComputationView.TotValueOfFringeBenefit = objDR["Total_FBT_Value"].ToString();
                        objIFBTComputationView.FringeBenefitPercent = objDR["FBT_Rate"].ToString();
                        objIFBTComputationView.FringeBenefitAmount = objDR["FBT_Value"].ToString();
                        objIFBTComputationView.SurchargePercent = objDR["Surcharge_Rate"].ToString();
                        objIFBTComputationView.SurchargeAmount = objDR["Surcharge_Value"].ToString();
                        objIFBTComputationView.EducationCessPercent = objDR["Add_Surcharge_Rate"].ToString();
                        objIFBTComputationView.EducationCessAmount = objDR["Add_Surcharge_Value"].ToString();
                        objIFBTComputationView.AddlEducationCessPercent = objDR["Add_Education_Cess_Rate"].ToString();
                        objIFBTComputationView.AddlEducationCessAmount = objDR["Add_Education_Cess_Value"].ToString();
                        objIFBTComputationView.TotalTaxPayable = objDR["Total_Tax_Payable"].ToString();
                        
                    
                    }

                    objIFBTComputationView.BindFBTGrid = ds.Tables[0];

                }
       

      
	}
}

