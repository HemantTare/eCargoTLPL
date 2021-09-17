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
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;
using ClassLibraryMVP;

/// <summary>
/// Summary description for VoucherBillByBillPresenter
/// </summary>
public class VoucherBillByBillPresenter : ClassLibraryMVP.General.Presenter
{

    private IVoucherBillByBillView objIVoucherBillByBillView;
    private VoucherBillByBillModel objVoucherBillByBillModel;
    DataSet objDS;
    Raj.EC.Common objCommon = new Raj.EC.Common();
    public VoucherBillByBillPresenter(IVoucherBillByBillView voucherBillByBillView, bool IsPostBack)
    {
        objIVoucherBillByBillView = voucherBillByBillView;
        objVoucherBillByBillModel = new VoucherBillByBillModel(objIVoucherBillByBillView);

        base.Init(objIVoucherBillByBillView, objVoucherBillByBillModel);

        if (!IsPostBack)
        {
            initValues();         

        }
	}
    private void initValues()
    {
        if (objIVoucherBillByBillView.LedgerId > 0)
        {
            objDS = objVoucherBillByBillModel.ReadValues();

            objIVoucherBillByBillView.SessionTDSLedger = objDS.Tables[0];
            //objIVoucherBillByBillView.Bind_ddl_TDSLedger = objIVoucherBillByBillView.SessionTDSLedger;
            objIVoucherBillByBillView.IsTDSApplicable = Convert.ToBoolean(objDS.Tables[1].Rows[0]["Is_TDS_Applicable"]);
            
            
            objIVoucherBillByBillView.SessionBillByBill_New = objCommon.Get_View_Table(objIVoucherBillByBillView.SessionBillByBillDT, "Ledger_Id=" + objIVoucherBillByBillView.LedgerId).ToTable();
            Raj.EC.Common.SetPrimaryKeys(new string[] { "Ledger_Id", "Ref_No" }, objIVoucherBillByBillView.SessionBillByBill_New);
            objIVoucherBillByBillView.Bind_dg_BillByBill = objIVoucherBillByBillView.SessionBillByBill_New;
            objIVoucherBillByBillView.CreditDays = Convert.ToInt32(objDS.Tables[1].Rows[0]["Default_Credit_Period"]);
            //DefaultCrPeriod = Convert.ToInt32(objDS.Tables[1].Rows[0]["Default_Credit_Period"]);
            //if (objDS.Tables[0].Rows.Count > 0)
            //{

            //    DataRow DR = objDS.Tables[0].Rows[0];
               
            //}
        }
    }
    public DataSet SetCreditDaysAmount()
    {
         return objVoucherBillByBillModel.SetCreditDaysAmount();
    }
    public bool IsDupicateRefNo()
    {
        return objVoucherBillByBillModel.IsDuplicateRef_No();      
    }
}
