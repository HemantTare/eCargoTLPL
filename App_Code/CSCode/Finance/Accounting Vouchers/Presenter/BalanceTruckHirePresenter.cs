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
/// Summary description for BalanceTruckHirePresenter
/// </summary>
/// 

namespace Raj.EC.FinancePresenter
{
    public class BalanceTruckHirePresenter:Presenter
    {
        private IBalanceTruckHireView _BalanceTruckHireView;
        private BalanceTruckHireModel _BalanceTruckHireModel;
        private DataSet _ds = new DataSet();

        public BalanceTruckHirePresenter(IBalanceTruckHireView BalanceTruckHireView,bool isPostback)
        {
            _BalanceTruckHireView = BalanceTruckHireView;
            _BalanceTruckHireModel = new BalanceTruckHireModel(_BalanceTruckHireView);

            base.Init(_BalanceTruckHireView, _BalanceTruckHireModel);

            if (!isPostback)
            {
                _BalanceTruckHireView.BTHVoucherDate = DateTime.Now;
                initvalues();
            }
        }

        public void initvalues()
        {
            if (_BalanceTruckHireView.keyID > 0)
            {
                DataSet ds = new DataSet();
                DataSet ds1 = new DataSet();
                ds = _BalanceTruckHireModel.ReadValues();
                DataRow DR = ds.Tables[0].Rows[0];
                DataRow DR1 = ds.Tables[1].Rows[0];

                _BalanceTruckHireView.BTHVoucherNo = DR["BTH_Voucher_No_For_Print"].ToString();
                _BalanceTruckHireView.BTHVoucherDate = Convert.ToDateTime(DR["BTH_Date"]);
                _BalanceTruckHireView.ReferenceNo = DR["Reference_No"].ToString();
               
                _BalanceTruckHireView.SetOwnerID(DR["Vendor_Name"].ToString(),DR["Vendor_ID"].ToString());
                _BalanceTruckHireView.VehicleSearchView.VehicleID = Util.String2Int(DR1["Vehicle_ID"].ToString());
                _BalanceTruckHireView.LHPO_Date = Convert.ToDateTime(DR1["LHPO_Date"]);
                
                _BalanceTruckHireView.LHPO_DDl_Fill();
                Fill_LHPONoDDL();
                _BalanceTruckHireView.LHPONo_ID = Util.String2Int(DR1["LHPO_ID"].ToString());

                ds1 = _BalanceTruckHireModel.Get_LHPO_Details();
                _BalanceTruckHireView.Fill_LHPO_Detail(ds1);

                _BalanceTruckHireView.TotalPayableAmount = Util.String2Decimal(DR["Total_Payable_Amount"].ToString());
                _BalanceTruckHireView.MRCashChequeDetailsView.CashAmount = Util.String2Decimal(DR["Cash_Amount"].ToString());
                _BalanceTruckHireView.MRCashChequeDetailsView.ChequeAmount = Util.String2Decimal(DR["Cheque_Amount"].ToString());
                _BalanceTruckHireView.Remark = DR["Remarks"].ToString();            
            }
        }

        public void Fill_LHPONoDDL()
        {
            _BalanceTruckHireView.Bind_ddlLHPONo = _BalanceTruckHireModel.Fill_LHPO_Dll();        
        }

        public DataSet Set_Lhpo_Detail()
        {
            DataSet ds = new DataSet();
            ds = _BalanceTruckHireModel.Get_LHPO_Details();

            return ds;
        
        }

        public void Save()
        {
            base.DBSave();
        }

    }
}