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
/// Summary description for DoorDelAndLocalCartVoucherPresenter
/// </summary>
public class DoorDelAndLocalCartVoucherPresenter : ClassLibraryMVP.General.Presenter
{

    private IDoorDelAndLocalCartVoucherView objIDoorDelAndLocalCartVoucherView;
    private DoorDelAndLocalCartVoucherModel objDoorDelAndLocalCartVoucherModel;
    DataSet objDS;

    public DoorDelAndLocalCartVoucherPresenter(IDoorDelAndLocalCartVoucherView doorDelAndLocalCartVoucherView, bool IsPostBack)
    {
        objIDoorDelAndLocalCartVoucherView = doorDelAndLocalCartVoucherView;
        objDoorDelAndLocalCartVoucherModel = new DoorDelAndLocalCartVoucherModel(objIDoorDelAndLocalCartVoucherView);

        base.Init(objIDoorDelAndLocalCartVoucherView, objDoorDelAndLocalCartVoucherModel);

        if (!IsPostBack)
        {            
            initValues();
            FillGrid();
        }
    }

    public void Save()
    {
        //objDoorDelAndLocalCartVoucherModel.Save();
        base.DBSave();
    }
    public void FillGrid()
    {
        objDS=objDoorDelAndLocalCartVoucherModel.FillGrid();
        objIDoorDelAndLocalCartVoucherView.SessionVoucherGrid = objDS.Tables[0];
        objIDoorDelAndLocalCartVoucherView.Bind_dg_Voucher = objIDoorDelAndLocalCartVoucherView.SessionVoucherGrid;
    }
    private void initValues()
    {
        if (objIDoorDelAndLocalCartVoucherView.keyID > 0)
        {
            objDS = objDoorDelAndLocalCartVoucherModel.ReadValues();
            if (objDS.Tables[0].Rows.Count > 0)
            {               

                DataRow DR = objDS.Tables[0].Rows[0];
                objIDoorDelAndLocalCartVoucherView.VoucherDate = Convert.ToDateTime(DR["VoucherDate"]);
                objIDoorDelAndLocalCartVoucherView.VoucherNo = DR["NoForPrint"].ToString();
                objIDoorDelAndLocalCartVoucherView.TotalGC = Util.String2Int(DR["Total_GC"].ToString());
                objIDoorDelAndLocalCartVoucherView.TotalAmount = Util.String2Decimal(DR["Total_Amount"].ToString());
                objIDoorDelAndLocalCartVoucherView.ChequeDate = Convert.ToDateTime(DR["Cheque_Date"]);
                objIDoorDelAndLocalCartVoucherView.ChequeNo = DR["Cheque_No"].ToString();
                objIDoorDelAndLocalCartVoucherView.Remark = DR["Remark"].ToString();
                objIDoorDelAndLocalCartVoucherView.RefNo = DR["Ref_No"].ToString();
                objIDoorDelAndLocalCartVoucherView.ChequeInFavour = DR["Cheque_In_Favour"].ToString();    
                objIDoorDelAndLocalCartVoucherView.IsCash =Util.String2Bool(DR["Is_Cash"].ToString());
                objIDoorDelAndLocalCartVoucherView.IsCheque =Util.String2Bool(DR["Is_Cheque"].ToString());                
                objIDoorDelAndLocalCartVoucherView.SetLedgerID(DR["Ledger_Name"].ToString(), DR["Ledger_ID"].ToString());
                objIDoorDelAndLocalCartVoucherView.CreditToLedgerID = Util.String2Int(DR["Ledger_ID"].ToString());
                if (objIDoorDelAndLocalCartVoucherView.CreditToLedgerID > 0)
                {
                    objIDoorDelAndLocalCartVoucherView.IsCreditTo = true;                    
                }
            }
        }
    }

}
