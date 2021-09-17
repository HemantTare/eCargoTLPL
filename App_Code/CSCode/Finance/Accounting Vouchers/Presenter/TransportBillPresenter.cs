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

/// <summary>
/// Summary description for TransportBillPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class TransportBillPresenter : Presenter
    {
        private ITransportBillView objITransportBillView;
        private TransportBillModel objTransportBillModel; 

        private DataSet objDS;

        public TransportBillPresenter(ITransportBillView TransportBillView, bool IsPostBack)
        {
            objITransportBillView = TransportBillView;
            objTransportBillModel = new TransportBillModel(objITransportBillView);
            base.Init(objITransportBillView, objTransportBillModel);

            

            if (!IsPostBack)
            {
                objITransportBillView.BillDate = DateTime.Now.Date;
                initValues();
            }
        }

        public void fillValues()
        {
            objDS = objTransportBillModel.FillValues();
            objITransportBillView.BindBillType = objDS.Tables[0];
            objITransportBillView.BindBillFor = objDS.Tables[1];
        }

        public void FillGCDetails(int from)
        {

            objDS = objTransportBillModel.FillGCDetails();
            objITransportBillView.Bind_dg_Voucher = objDS.Tables[3];
            objITransportBillView.SessionBillGrid = objDS.Tables[0];
            objITransportBillView.SessionBillOtherChargeGrid = objDS.Tables[1];
            if (from != 2)  //from == 2  radio button
            {
                if (objDS.Tables[2].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[2].Rows[0];

                    objITransportBillView.ContactPerson = objDR["Contact_Person"].ToString();
                    objITransportBillView.BillingName = objDR["Billing_Name"].ToString();
                    objITransportBillView.BillingAddress = objDR["Billing_Address"].ToString();
                    objITransportBillView.ContactNo = objDR["Contact_No"].ToString();
                    objITransportBillView.Email = objDR["Email"].ToString();

                }
                else
                {
                    objITransportBillView.ContactPerson = "";
                    objITransportBillView.BillingName = "";
                    objITransportBillView.BillingAddress = "";
                    objITransportBillView.ContactNo = "";
                    objITransportBillView.Email = "";
                }
            }

        }

        private void initValues()
        {
            fillValues();

            if (objITransportBillView.keyID > 0)
            {
                objDS = objTransportBillModel.ReadValues();

                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];

                    objITransportBillView.BillNo = objDR["Bill_No_For_Print"].ToString();
                    objITransportBillView.BillDate = Convert.ToDateTime(objDR["Bill_Date"].ToString());

                    objITransportBillView.BillTypeID = Util.String2Int(objDR["Bill_Type_ID"].ToString());
                    objITransportBillView.SetClientId(objDR["Client_Name"].ToString(), objDR["Client_Id"].ToString());
                    objITransportBillView.ReferenceNo = objDR["Bill_Ref_No"].ToString();
                    objITransportBillView.Remarks = objDR["Remarks"].ToString();

                    objITransportBillView.Total_No_Of_GC = Util.String2Int(objDR["Total_GC"].ToString());
                    //objITransportBillView.TotalSubTotal = objDR["Bill_Sub_Total"].ToString();

                    objITransportBillView.TotalOtherCharge = objDR["Bill_Other_Charges_Total"].ToString();
                    objITransportBillView.TotalServiceTax = objDR["Bill_Service_Tax_Amount"].ToString();
                    objITransportBillView.TotalOctAmount = objDR["Bill_Octroi_Amount"].ToString();
                    objITransportBillView.TotalGCAmount = objDR["Bill_Total_Amount"].ToString();
                    objITransportBillView.Less_Amount = Util.String2Decimal(objDR["Less_Amount"].ToString());
                    objITransportBillView.Bill_ForID = Convert.ToInt32(objDR["transport_bill_type_id"]);
                    objITransportBillView.TotalOctroiFormCharge = objDR["Total_Oct_Form_Charges"].ToString();
                    objITransportBillView.TotalOctroiServiceCharge = objDR["Total_Oct_Service_Charges"].ToString();
                    objITransportBillView.Total_Additional_Charges = Util.String2Decimal(objDR["Total_Additional_Charges"].ToString());

                    objITransportBillView.TotalLRSerTax = objDR["Total_GCService_Tax_Amount"].ToString();
                    objITransportBillView.TotalRound_Off = objDR["Total_Round_Off"].ToString();
                    objITransportBillView.TotalLRTotal = objDR["SumTotal_GC_Amount"].ToString();

                    objITransportBillView.TotalSubTotal = objDR["Total_Actual_Sub_Total"].ToString();
                }
            }

            FillGCDetails(1);
            if (objITransportBillView.keyID <= 0)
            {
                ReadLedgerValues();
            }
        }

        private void ReadLedgerValues()
        {
            objDS = objTransportBillModel.ReadLedgerValues();

            objITransportBillView.Bind_dg_Voucher = objDS.Tables["LedgerDetails"];  
        }

        public void Save()
        {
            base.DBSave();
        }

        //public void Fill_Values()
        //{
        //    objITransportBillView.BindBillFor = objTransportBillModel.Fill_Bill_For();
        //}
    }
}

