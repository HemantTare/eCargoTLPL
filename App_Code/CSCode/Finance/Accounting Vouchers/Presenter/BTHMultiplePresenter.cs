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
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;
/// <summary>
/// Summary description for BTHMultiplePresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class BTHMultiplePresenter : Presenter
    {
        private IBTHMultipleView _BTHMultipleView;
        private BTHMultipleModel _BTHMultipleModel;
        private DataSet _ds = new DataSet();

        public BTHMultiplePresenter(IBTHMultipleView BTHMultipleView, bool isPostback)
        {
            _BTHMultipleView = BTHMultipleView;
            _BTHMultipleModel = new BTHMultipleModel(_BTHMultipleView);

            base.Init(_BTHMultipleView, _BTHMultipleModel);

            if (!isPostback)
            {
                _BTHMultipleView.BTHVoucherDate = DateTime.Now;
                initvalues();
            }
        }

        public void initvalues()
        {
            _ds = _BTHMultipleModel.ReadValues();
            _BTHMultipleView.SessionLHPODetailsGrid = _ds.Tables[0];

            if (_BTHMultipleView.keyID > 0)
            {
                DataRow DR1 = _ds.Tables[1].Rows[0];

                _BTHMultipleView.BTHVoucherNo = DR1["BTH_Voucher_No_For_Print"].ToString();
                _BTHMultipleView.BTHVoucherDate = Convert.ToDateTime(DR1["BTH_Date"]);
                _BTHMultipleView.ReferenceNo = DR1["Reference_No"].ToString();

                _BTHMultipleView.SetOwnerID(DR1["Vendor_Name"].ToString(), DR1["Vendor_ID"].ToString());
                _BTHMultipleView.Total_No_Of_LHPO = _ds.Tables[0].Rows.Count;
                _BTHMultipleView.TotalBalanceToBePaid = Util.String2Decimal(DR1["Total_Balance_To_Be_Paid_Amount"].ToString());
                _BTHMultipleView.TotalOtherAmount = Util.String2Decimal(DR1["Total_Other_Amount"].ToString());
                _BTHMultipleView.TotalPayableAmount = Util.String2Decimal(DR1["Total_Payable_Amount"].ToString());
                _BTHMultipleView.TotalTDSAmount = Math.Ceiling(Convert.ToDecimal(_BTHMultipleView.SessionLHPODetailsGrid.Compute("Sum(TDS_On_OtherCharge)", "")));
                _BTHMultipleView.MRCashChequeDetailsView.CashAmount = Util.String2Decimal(DR1["Cash_Amount"].ToString());
                _BTHMultipleView.MRCashChequeDetailsView.ChequeAmount = Util.String2Decimal(DR1["Cheque_Amount"].ToString());
                _BTHMultipleView.Remark = DR1["Remarks"].ToString();

                FillTDSDetails(_ds);
            }
        }

        public void Fill_LHPONoDetails()
        {
            _ds = _BTHMultipleModel.ReadValues();
            _BTHMultipleView.SessionLHPODetailsGrid = _ds.Tables[0];
            FillTDSDetails(_ds);
        }

        public void FillTDSDetails(DataSet ds)
        { 
            DataRow dr;

            if(_BTHMultipleView.keyID <= 0)
                dr = ds.Tables[1].Rows[0];
            else
                dr = ds.Tables[2].Rows[0];

            _BTHMultipleView.Tax_Rate = Convert.ToDecimal(dr["Tax_Rate"]);
            _BTHMultipleView.Surcharge_Rate = Convert.ToDecimal(dr["Surcharge_Rate"]);
            _BTHMultipleView.Add_Surcharge_Rate = Convert.ToDecimal(dr["Add_Surcharge_Rate"]);
            _BTHMultipleView.Add_Edu_Cess_Rate = Convert.ToDecimal(dr["Add_Edu_Cess_Rate"]);
        }

        public void Save()
        {
            //_BTHMultipleModel.Save();
            base.DBSave();
        }
        

    }
}
