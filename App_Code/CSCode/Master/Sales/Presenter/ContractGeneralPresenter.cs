using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for ContractGeneralPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class ContractGeneralPresenter : ClassLibraryMVP.General.Presenter
    {
        private IContractGeneralView objIContractGeneralView;
        private ContractGeneralModel objContractGeneralModel;
        private DataSet objDS,objDS1;

        public ContractGeneralPresenter(IContractGeneralView contractGeneralView, bool isPostBack)
        {
            objIContractGeneralView = contractGeneralView;
            objContractGeneralModel = new ContractGeneralModel(objIContractGeneralView);
            base.Init(objIContractGeneralView, objContractGeneralModel);

            if (!isPostBack)
            {
                objDS = objContractGeneralModel.FillValues();
                GetGCRiskType();
                initValues();
            }
        }

        public void Save()
        {
            objContractGeneralModel.Save();
        }
        public void GetClientOnBranchChanged()
        {
            objDS = objContractGeneralModel.FillClientOnBranchChanged();
        }
        public void GetGCRiskType()
        {
           objDS1=objContractGeneralModel.FillGCRiskType();
           objIContractGeneralView.BindGCRiskType=objDS1.Tables[0];
           objIContractGeneralView.BindConsignmentType = objDS1.Tables[1];

        }
        
        //Added :Anita On: 05 Feb 09
        public bool IsDuplicateContractName()
        {
            bool IsDuplicateContractName = objContractGeneralModel.IsContractNameDuplicate();
            return IsDuplicateContractName;
        }
        private void initValues()
        {
            if (objIContractGeneralView.keyID > 0)
            {
                objDS = objContractGeneralModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];                    
                    objIContractGeneralView.ContractDate =Convert.ToDateTime(DR["Contract_Date"]);
                    objIContractGeneralView.ContractNo = DR["Contract_No"].ToString();
                    objIContractGeneralView.ContractName = DR["Contract_Name"].ToString();
                    objIContractGeneralView.Days =Util.String2Int(DR["Credit_Period"].ToString());
                    objIContractGeneralView.Freight =Util.String2Decimal(DR["Promissed_Freight_Per_Month"].ToString());
                    objIContractGeneralView.PODate =Convert.ToDateTime(DR["Client_PO_Date"]);
                    objIContractGeneralView.ClientPONo = DR["Client_PO_No"].ToString();
                    objIContractGeneralView.Weight =Util.String2Decimal(DR["Promissed_Wt_Per_Month"].ToString());
                    objIContractGeneralView.ValidFromDate =Convert.ToDateTime(DR["Valid_From"]);
                    objIContractGeneralView.ValidUptoDate =Convert.ToDateTime(DR["Valid_UpTo"]);
                    objIContractGeneralView.POMaxLimit =Util.String2Decimal(DR["PO_Max_Limmit"].ToString());
                    objIContractGeneralView.Amount =Util.String2Decimal(DR["Credit_Limit"].ToString());
                    objIContractGeneralView.Remark = DR["Remarks"].ToString();
                    objIContractGeneralView.GCRiskId=Util.String2Int(DR["GC_risk_type_id"].ToString());
                    objIContractGeneralView.ConsignmentTypeId = Util.String2Int(DR["Consignment_Type_Id"].ToString());
                    objIContractGeneralView.SetBranchID(DR["Branch_Name"].ToString(), DR["Contract_Branch_ID"].ToString());
                    objIContractGeneralView.SetClientID(DR["Client_Name"].ToString(), DR["Client_ID"].ToString());
                    //objIContractGeneralView.SetBillingBranchID(DR["Billing_Branch_Name"].ToString(), DR["Billing_Branch_ID"].ToString());
                    objIContractGeneralView.BillingHierarchy = DR["Billing_Hierarchy"].ToString();
                    objIContractGeneralView.BillingBranchID = Util.String2Int(DR["Billing_Branch_ID"].ToString());
                    objIContractGeneralView.SetBillingClientID(DR["Billing_Client_Name"].ToString(), DR["Billing_Client_ID"].ToString());
                }
            }
        }
    }
}