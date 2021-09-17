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
/// Summary description for SupplementaryBillPresenter
/// </summary>
/// 

namespace Raj.EC.FinancePresenter
{
    public class SupplementaryBillPresenter:Presenter
    {

        private ISupplementaryBillView ObjISupplementaryBillView;
        private SupplementaryBillModel ObjSupplementaryBillModel;
        private DataSet objds = new DataSet();

        public SupplementaryBillPresenter(ISupplementaryBillView SupplementaryBillView,bool isPostBack)
        {
            ObjISupplementaryBillView = SupplementaryBillView;
            ObjSupplementaryBillModel = new SupplementaryBillModel(ObjISupplementaryBillView);

            base.Init(ObjISupplementaryBillView,ObjSupplementaryBillModel);

            if (!isPostBack)
            {
                initvalues();
            }
        }

        public void fillValues()
        {
            objds = ObjSupplementaryBillModel.FillValues();
            ObjISupplementaryBillView.BindServiceType = objds.Tables[0];
        }

        public void initvalues()
        {
            fillValues();

            if (ObjISupplementaryBillView.keyID > 0)
            {
                objds = ObjSupplementaryBillModel.ReadValues();

                if (objds.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objds.Tables[0].Rows[0];

                    ObjISupplementaryBillView.BillNo = objDR["Bill_No_For_Print"].ToString();
                    ObjISupplementaryBillView.BillDate = Convert.ToDateTime(objDR["Bill_Date"].ToString());
                    ObjISupplementaryBillView.SetClientId(objDR["Client_Name"].ToString(), objDR["Client_Id"].ToString());
                    ObjISupplementaryBillView.ReferenceNo = objDR["Bill_Ref_No"].ToString();
                    ObjISupplementaryBillView.Remarks = objDR["Remarks"].ToString();
                    ObjISupplementaryBillView.GrandTotal = Convert.ToDecimal(objDR["Bill_Total_Amount"]);
                    ObjISupplementaryBillView.TotalOtherCharge = Convert.ToDecimal(objDR["Bill_Other_Charges_Total"]);
                    ObjISupplementaryBillView.TotalServiceTax = Convert.ToDecimal(objDR["Bill_Service_Tax_Amount"]);
                    ObjISupplementaryBillView.Service_Type_ID = Util.String2Int(objDR["Service_Type_ID"].ToString());

                    FillDetails();
                    
                }
            }

            FillGrid();
        }

        public void Save()
        {
            base.DBSave();
        }


        public void FillDetails()
        {
            objds = ObjSupplementaryBillModel.FillDetails();

            if (objds.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objds.Tables[0].Rows[0];

                ObjISupplementaryBillView.ContactPerson = objDR["Contact_Person"].ToString();
                ObjISupplementaryBillView.BillingName = objDR["Billing_Name"].ToString();
                ObjISupplementaryBillView.BillingAddress = objDR["Billing_Address"].ToString();
                ObjISupplementaryBillView.ContactNo = objDR["Contact_No"].ToString();
                ObjISupplementaryBillView.Email = objDR["Email"].ToString();

            }
            else
            {
                ObjISupplementaryBillView.ContactPerson = "";
                ObjISupplementaryBillView.BillingName = "";
                ObjISupplementaryBillView.BillingAddress = "";
                ObjISupplementaryBillView.ContactNo = "";
                ObjISupplementaryBillView.Email = "";
            }
        }

        public void FillGrid()
        {
            DataSet ds = new DataSet();
            ds = ObjSupplementaryBillModel.FillGCDetails();
            ObjISupplementaryBillView.BindGrid = ds.Tables[0];
            ObjISupplementaryBillView.SessionBillOtherChargeGrid = ds.Tables[1];
        }
        public DataSet Check_BackDatedEntry()
        {
            return ObjSupplementaryBillModel.Check_BackDatedEntry();
        }
    }
}
