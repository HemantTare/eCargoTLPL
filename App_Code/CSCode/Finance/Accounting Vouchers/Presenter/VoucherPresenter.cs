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
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;
namespace Raj.EC.FinancePresenter
{
    public class VoucherPresenter : Presenter
    {
    
        private IVoucherView objIVoucherView;
        private VoucherModel objVoucherModel;
        private DataSet objDS;
        public VoucherPresenter(IVoucherView VoucherView, bool IsPostBack)
        {
            objIVoucherView = VoucherView;
            objVoucherModel = new VoucherModel(objIVoucherView);
            base.Init(objIVoucherView, objVoucherModel);

            if (!IsPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            fillValues();

            //if (objIVoucherView.keyID > 0)
            //{
               readValues();
            //}
            
        }

        private void fillValues()
        {
            //objDS = objVoucherModel.FillValues();
            //objIVoucherView.bind_ddl_Under = objDS.Tables[0];
        }

        private void readValues()
        {
            objDS = objVoucherModel.ReadValues();
            DataSet objDS_FromHelper = new DataSet();

            if (System.Web.HttpContext.Current.Session["FromTDSHelper"] != null && Convert.ToBoolean(System.Web.HttpContext.Current.Session["FromTDSHelper"]))
            {
                objDS_FromHelper = (DataSet)System.Web.HttpContext.Current.Session["TDS_FBT_Vouchers"];
                objDS_FromHelper.Tables[0].TableName = "VoucherDetails";
                objDS_FromHelper.Tables[1].TableName = "VoucherBillByBill";

                objIVoucherView.Bind_dg_Voucher = objDS_FromHelper.Tables["VoucherDetails"];
                objIVoucherView.VoucherDate = (DateTime)System.Web.HttpContext.Current.Session["Upto_Date"];
                objIVoucherView.FBTPaymentType = (string)System.Web.HttpContext.Current.Session["FBTPaymentType"];

                objIVoucherView.SessionVoucherBillByBillDT = objDS_FromHelper.Tables["VoucherBillByBill"];

                Common.SetPrimaryKeys(new string[] { "Ledger_Id" }, objDS_FromHelper.Tables["VoucherDetails"]);
             
            }
            else if (System.Web.HttpContext.Current.Session["FromFBTHelper"] != null && Convert.ToBoolean(System.Web.HttpContext.Current.Session["FromFBTHelper"]))
            {
                objDS_FromHelper = (DataSet)System.Web.HttpContext.Current.Session["TDS_FBT_Vouchers"];
                objDS_FromHelper.Tables[0].TableName = "VoucherDetails";
               
                objIVoucherView.Bind_dg_Voucher = objDS_FromHelper.Tables["VoucherDetails"];
                objIVoucherView.VoucherDate = (DateTime)System.Web.HttpContext.Current.Session["Upto_Date"];
                objIVoucherView.FBTPaymentType = (string)System.Web.HttpContext.Current.Session["FBTPaymentType"];

                Common.SetPrimaryKeys(new string[] { "Ledger_Id" }, objDS_FromHelper.Tables["VoucherDetails"]);
               
            }
            else
            {
                objIVoucherView.Bind_dg_Voucher = objDS.Tables["VoucherDetails"];
                objIVoucherView.SessionVoucherBillByBillDT = objDS.Tables["VoucherBillByBill"];
            }
            
            objIVoucherView.SessionVoucherCostCentreDT = objDS.Tables["VoucherCostCentre"];
            objIVoucherView.SessionDropDownCostCentre = objDS.Tables["MstCostCentre"];
            objIVoucherView.SessionDropDownRefType = objDS.Tables["MstRefType"];
            System.Web.HttpContext.Current.Session["FromTDSHelper"] = null;
            System.Web.HttpContext.Current.Session["FromFBTHelper"] = null;

            if (objIVoucherView.keyID > 0)
            {
                DataRow Dr = objDS.Tables["Voucher"].Rows[0];
                objIVoucherView.FBTPaymentType = Dr["FBT_Payment_Type"].ToString();
                objIVoucherView.Narration = Dr["Narration"].ToString();

                objIVoucherView.RefNo = Dr["Ref_No"].ToString();
                objIVoucherView.VoucherNo = Dr["Voucher_No"].ToString();
                objIVoucherView.VoucherDate = Convert.ToDateTime(Dr["Voucher_Date"]);

                objIVoucherView.VoucherTypeID = Convert.ToInt32(Dr["Voucher_Type_ID"]);
                objIVoucherView.VoucherTypeName = Dr["Voucher_Name"].ToString();

            }

        }


        public void Save()
        {
           base.DBSave();
          //objVoucherModel.Save();
        }



        public DataSet GetLedgerParam()
        {
           return objVoucherModel.GetLedgerParam();
        }
        public bool IsLedgerCashInHand()
        {
            return objVoucherModel.IsLedgerCashInHand();
        }
    }
}
