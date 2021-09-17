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
/// Summary description for BookingParametersPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class BookingParametersPresenter : Presenter
    {
        private IBookingParametersView objIBookingParametersView;
        private BookingParametersModel objBookingParametersModel;
        private DataSet objDS;

        public BookingParametersPresenter(IBookingParametersView bookingParametersView, bool IsPostBack)
        {
            objIBookingParametersView = bookingParametersView;
            objBookingParametersModel = new BookingParametersModel(objIBookingParametersView);

            base.Init(objIBookingParametersView, objBookingParametersModel);

            if (!IsPostBack)
            {
                        
                initValues();
            }
        }

        public void Save()
        {
            
        }       
      

        private void initValues()
        {
            objDS = objBookingParametersModel.ReadValues();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow DR = objDS.Tables[0].Rows[0];
                DataRow DR1 = objDS.Tables[1].Rows[0];
                objIBookingParametersView.IsTreatBookingIncomeAdvIncome = Util.String2Bool(DR["Is_Treat_Booking_Income_As_Advance_Income"].ToString());
                objIBookingParametersView.IsToBilledAccountingGCWise = Util.String2Bool(DR["Is_To_Be_Billed_Accounting_GC_Wise"].ToString());
                objIBookingParametersView.IsBookingMoneyReceiptRequired = Util.String2Bool(DR["Is_Bookking_Money_Receipt_Required"].ToString());

                objIBookingParametersView.SetShortTermBillLedgerID(DR["Ledger_Name"].ToString(), DR["Ledger_Id"].ToString());
                objIBookingParametersView.IsDebitTodelivery = Util.String2Bool(DR1["IsDebitTodelivery"].ToString());
                objIBookingParametersView.SetPayforBookigBranchID(DR1["PayforBookig_LedgerName"].ToString(), DR1["PayforBookigBranchID"].ToString());
                objIBookingParametersView.SetPayforCrossingBranchID(DR1["PayforCrossing_LedgerName"].ToString(), DR1["PayforCrossingBranchID"].ToString());
                objIBookingParametersView.SetPayforDeliveryBranchID(DR1["PayforDelivery_LedgerName"].ToString(), DR1["PayforDeliveryBranchID"].ToString());
                objIBookingParametersView.SetDeliveryCommisionIncomeID(DR1["Delivery_Commision_Income_LedgerName"].ToString(), DR1["Delivery_Commision_Income_Id"].ToString());
                objIBookingParametersView.SetDeliveryCommisionExpenseID(DR1["Delivery_Commision_Expense_Ledger_Name"].ToString(), DR1["Delivery_Commision_Expense_Ledger_Id"].ToString());
                objIBookingParametersView.SetLHPOOtherChargesExpenseID(DR1["LHPO_Other_Charges_Expense_Ledger_Name"].ToString(), DR1["LHPO_Other_Charges_Expense_Ledger_Id"].ToString());
                objIBookingParametersView.SetLHPOOtherChargesPaybleID(DR1["LHPO_Other_Charges_Payble_Ledger_Name"].ToString(), DR1["LHPO_Other_Charges_Payble_Ledger_Id"].ToString());
                objIBookingParametersView.SetLorryPayble_ATH_BTH_ID(DR1["Lorry_Payble_ATH_BTH_Ledger_Name"].ToString(), DR1["Lorry_Payble_ATH_BTH_Ledger_Id"].ToString());
                objIBookingParametersView.SetUpcountryCostAC_ID(DR1["Upcountry_Cost_Ledger_Name"].ToString(), DR1["Upcountry_Cost_Ledger_Id"].ToString());
               
            }
             }
        }
    
}
