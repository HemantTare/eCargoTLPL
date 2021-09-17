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
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

/// <summary>
/// Summary description for OctroiUpdatePresenter
/// </summary>
namespace Raj.EC.OperationPresenter
{
    public class OctroiUpdatePresenter : Presenter
    {
        private IOctroiUpdateView objIOctroiUpdateView;
        private OctroiUpdateModel objOctroiUpdateModel;
        private DataSet objDS;

        public OctroiUpdatePresenter(IOctroiUpdateView octroiUpdateView, bool IsPostBack)
        {
            objIOctroiUpdateView = octroiUpdateView;
            objOctroiUpdateModel = new OctroiUpdateModel(objIOctroiUpdateView);
            base.Init(objIOctroiUpdateView, objOctroiUpdateModel);

            if (!IsPostBack)
            {
                objIOctroiUpdateView.TransactionDate = DateTime.Now.Date;
                objIOctroiUpdateView.BillDate = DateTime.Now.Date;
                FillValues();
                if (objIOctroiUpdateView.keyID > 0)
                {
                    initValues();
                }
            }
        }

        public void FillValues()
        {
            objDS = objOctroiUpdateModel.FillValues();
            objIOctroiUpdateView.SessionOctroiFormType = objDS.Tables[0];
            //objIOctroiUpdateView.BindOctroiFormType = objIOctroiUpdateView.SessionOctroiFormType;
            objIOctroiUpdateView.SessionOctroiPaidBy = objDS.Tables[1];
           // objIOctroiUpdateView.BindOctroiPaidBy = objIOctroiUpdateView.SessionOctroiPaidBy;
        }

        public DataSet GetLedgerGroupId()
        {
            objDS = objOctroiUpdateModel.GetLedgerGroupId();
            return objDS;
        }
        public void fillgrid()
        {
            objDS = objOctroiUpdateModel.ReadValues();
            objIOctroiUpdateView.SessionBindOctroiUpdateGrid = objDS.Tables[0];
            objIOctroiUpdateView.BindOctroiUpdateGrid = objIOctroiUpdateView.SessionBindOctroiUpdateGrid;

            if (objIOctroiUpdateView.keyID > 0)
            {
                if (objDS.Tables[1].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];

                    objIOctroiUpdateView.SetLedgerId(objDR["Ledger_Name"].ToString(), objDR["Ledger_Id"].ToString());
                    objIOctroiUpdateView.Total_No_Of_GC= Util.String2Int(objDR["Total_GC"].ToString());
                    objIOctroiUpdateView.Total_Amount=Util.String2Int(objDR["Total_Amount"].ToString());
                    objIOctroiUpdateView.Remarks = objDR["Remark"].ToString();
                    
                }
            }
        }

        private void initValues()
        {
            objDS = objOctroiUpdateModel.ReadValues();
            objIOctroiUpdateView.SessionBindOctroiUpdateGrid = objDS.Tables[0];
            objIOctroiUpdateView.BindOctroiUpdateGrid = objIOctroiUpdateView.SessionBindOctroiUpdateGrid;
            if (objIOctroiUpdateView.keyID > 0)
            {
                if (objDS.Tables[1].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];

                    objIOctroiUpdateView.TransactionNo = objDR["Octroi_Update_No_For_Print"].ToString();
                    objIOctroiUpdateView.TransactionDate = Convert.ToDateTime(objDR["Octroi_Update_Date"].ToString());
                    objIOctroiUpdateView.BillNo = objDR["Bill_No"].ToString();
                    objIOctroiUpdateView.BillDate = Convert.ToDateTime(objDR["Bill_Date"].ToString());
                    objIOctroiUpdateView.SetLedgerId(objDR["Ledger_Name"].ToString(), objDR["Ledger_Id"].ToString());
                    objIOctroiUpdateView.Total_No_Of_GC = Util.String2Int(objDR["Total_GC"].ToString());
                    objIOctroiUpdateView.Total_Amount = Util.String2Decimal(objDR["Total_Octroi_Amount"].ToString());
                    objIOctroiUpdateView.OtherChargeAmount = Util.String2Decimal(objDR["Total_Other_Charge_Amount"].ToString());
                    objIOctroiUpdateView.Remarks = objDR["Remark"].ToString();
                    objIOctroiUpdateView.Grand_Total = Util.String2Decimal(objDR["Total_Amount"].ToString());
                    GetLedgerGroupId();
                    objIOctroiUpdateView.LedgerGroupId = Util.String2Int(objDS.Tables[0].Rows[0]["Ledger_Group_Id"].ToString());
                    if (objIOctroiUpdateView.LedgerGroupId == 19)
                    {
                        objIOctroiUpdateView.ChequeNo = objDR["Chq_No"].ToString();
                        objIOctroiUpdateView.ChequeDate = Convert.ToDateTime(objDR["Chq_Date"].ToString());
                        objIOctroiUpdateView.NameOfBank = objDR["Bank_Name"].ToString();
                    }


                }
            }
        }
        public void save()
        {
            base.DBSave();
            //objOctroiUpdateModel.Save();
        }
    }
}
