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
/// Summary description for FBTComputationLedgerWisePresenter
/// </summary>
namespace Raj.FA.ReportsPresenter
{
    public class FBTComputationLedgerWisePresenter : Presenter
    {
        private IFBTComputationLedgerWiseView objIFBTComputationLedgerWiseView;
        private FBTComputationLedgerWiseModel objFBTComputationLedgerWiseModel;
        DataSet objDS;
        int tableNo;

        public FBTComputationLedgerWisePresenter(IFBTComputationLedgerWiseView fbtComputationLedgerWiseView, bool IsPostBack)
        {
            objIFBTComputationLedgerWiseView = fbtComputationLedgerWiseView;
            objFBTComputationLedgerWiseModel = new FBTComputationLedgerWiseModel(objIFBTComputationLedgerWiseView);

            base.Init(objIFBTComputationLedgerWiseView, objFBTComputationLedgerWiseModel);

            if (!IsPostBack)
            {
                FillLabelValues();
                GetValues();
               
            }
        }
        public void FillLabelValues()
        {
            objDS = objFBTComputationLedgerWiseModel.FillLabelValues();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow ObjDR = objDS.Tables[0].Rows[0];
                objIFBTComputationLedgerWiseView.FBTCategoryName = ObjDR["FBT_Category_Name"].ToString();
                objIFBTComputationLedgerWiseView.FBTSectionName = ObjDR["FBT_Section"].ToString();

            }
        }
        public void GetValues()
        {

            DataSet ds = new DataSet();
            ds = objFBTComputationLedgerWiseModel.GetValues();
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataRow objDR = ds.Tables[1].Rows[0];
                objIFBTComputationLedgerWiseView.TotExpenditureAmount = objDR["Total_Expenditure_Amount"].ToString();
                objIFBTComputationLedgerWiseView.TotExpenditureAmount = objDR["Total_Expenditure_Amount"].ToString();
                objIFBTComputationLedgerWiseView.TotAmountRecovered = objDR["Total_Amount_Recovered"].ToString();
                objIFBTComputationLedgerWiseView.TotNetExpenditure = objDR["Total_Net_Expenditure"].ToString();
                objIFBTComputationLedgerWiseView.TotValueOfFringeBenefit = objDR["Total_FBT_Value"].ToString();
                objIFBTComputationLedgerWiseView.TotalFBT = objDR["Total_FBT_Amount"].ToString();
                objIFBTComputationLedgerWiseView.TotEducationCess = objDR["Total_Add_Surcharge_Value"].ToString();
                objIFBTComputationLedgerWiseView.TotAddlEducationCess = objDR["Total_Add_Education_Value"].ToString();
                objIFBTComputationLedgerWiseView.TotalTax = objDR["Total_Total_Tax"].ToString();

            }

            objIFBTComputationLedgerWiseView.BindFBTLedgerWiseGrid = ds.Tables[0];
        }
                  

    }
}
