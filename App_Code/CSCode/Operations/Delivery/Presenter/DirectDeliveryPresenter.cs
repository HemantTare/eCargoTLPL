
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
using Raj.EC.OperationView;
using Raj.EC.OperationModel;
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;
using ClassLibraryMVP;

/// <summary>
/// Summary description for DirectDeliveryPresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{
    public class DirectDeliveryPresenter : Presenter
    {
       private IDirectDeliveryView objIDirectDeliveryView;
        private DirectDeliveryModel objDirectDeliveryModel;
        private DataSet objDS;

        public DirectDeliveryPresenter(IDirectDeliveryView DirectDeliveryView, bool IsPostBack)
        {
            objIDirectDeliveryView = DirectDeliveryView;
            objDirectDeliveryModel = new DirectDeliveryModel(objIDirectDeliveryView);

            base.Init(objIDirectDeliveryView, objDirectDeliveryModel);

            if (!IsPostBack)
            {                                   
                initValues();                 
            }
        }

        public void Fill_Values()
        {
            objDS = objDirectDeliveryModel.Fill_Values();
             objIDirectDeliveryView.BindResionForLateDelivery = objDS.Tables[0];
             objIDirectDeliveryView.BindDeliveryCondition      = objDS.Tables[1];
        } 

        private void initValues()
        {
             Fill_Values();

            if (objIDirectDeliveryView.keyID > 0)
            {
                ReadValues();
            }
        }

        public void Get_VehicleDetails(object sender, EventArgs e)
        {
            objDirectDeliveryModel.Get_VehicleDetails();
        }

        public void Get_LHPO()
        {
            objIDirectDeliveryView.BindLHPO = objDirectDeliveryModel.Get_LHPO();
        }

        public void Get_LHPO_Details()
        {
            objDS  = objDirectDeliveryModel.Get_LHPO_Details ();

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                objIDirectDeliveryView.LHPO_From = objDR["LHPO_From"].ToString();
                objIDirectDeliveryView.LHPO_To = objDR["LHPO_To"].ToString();
                objIDirectDeliveryView.Memo_Id  = Util.String2Int( objDR["Memo_Id"].ToString());
                objIDirectDeliveryView.Memo_No = objDR["Memo_No_For_Print"].ToString();
                objIDirectDeliveryView.Memo_From = objDR["Memo_From_Branch"].ToString();
                objIDirectDeliveryView.Memo_To = objDR["Memo_To_Branch"].ToString();
                objIDirectDeliveryView.LHPO_Date = objDR["LHPO_Date"].ToString();
                objIDirectDeliveryView.Memo_Date = objDR["Memo_Date"].ToString();

                objIDirectDeliveryView.Loaded_Articles = Util.String2Int(objDR["Loaded_Articles"].ToString());
                objIDirectDeliveryView.Loaded_Articles_Weight = Util.String2Decimal (objDR["Loaded_Wt"].ToString());
                objIDirectDeliveryView.Delivered_Articles = Util.String2Int(objDR["Loaded_Articles"].ToString());
                objIDirectDeliveryView.Delivered_Articles_Weight = Util.String2Decimal(objDR["Loaded_Wt"].ToString());

                objIDirectDeliveryView.Short_Articles = 0;
                objIDirectDeliveryView.Damage_Leakage_Articles = 0;
                objIDirectDeliveryView.Damage_Leakage_Articles_Value = 0;

                objIDirectDeliveryView.Previous_Article_ID = Util.String2Int(objDR["Previous_Article_ID"].ToString());
                objIDirectDeliveryView.Previous_Status_ID   = Util.String2Int(objDR["Previous_Status_ID"].ToString());
                objIDirectDeliveryView.Previous_Document_ID   = Util.String2Int(objDR["Previous_Document_ID"].ToString());
                objIDirectDeliveryView.Previous_Document_No_For_Print  = objDR["Previous_Document_No_For_Print"].ToString();
                
                objIDirectDeliveryView.Previous_Document_Date  = Convert.ToDateTime  (objDR["Previous_Document_Date"].ToString());

            }
            else
            {
                objIDirectDeliveryView.LHPO_From = "";
                objIDirectDeliveryView.LHPO_To = "";
                objIDirectDeliveryView.Memo_From = "";
                objIDirectDeliveryView.Memo_To = "";
                objIDirectDeliveryView.LHPO_Date = "";
                objIDirectDeliveryView.Memo_Date = "";
                objIDirectDeliveryView.Loaded_Articles = 0;
                objIDirectDeliveryView.Loaded_Articles_Weight = 0;
                objIDirectDeliveryView.Delivered_Articles = 0;
                objIDirectDeliveryView.Delivered_Articles_Weight = 0;
                objIDirectDeliveryView.Short_Articles = 0;
                objIDirectDeliveryView.Damage_Leakage_Articles = 0;
                objIDirectDeliveryView.Damage_Leakage_Articles_Value = 0;
                objIDirectDeliveryView.Previous_Article_ID = 0;
                objIDirectDeliveryView.Previous_Status_ID = 0;
                objIDirectDeliveryView.Previous_Document_ID = 0;
                objIDirectDeliveryView.Previous_Document_No_For_Print = "";

                objIDirectDeliveryView.Previous_Document_Date = DateTime.Now ;
            }
        }

        public void Get_GCDetails( )
        {
            objDS = objDirectDeliveryModel.Get_GC_Details();            

            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];
            
                objIDirectDeliveryView.BookingDate = objDR["GC_Date"].ToString();
                objIDirectDeliveryView.Booking_Branch_Id = Util.String2Int(objDR["From_Branch_ID"].ToString());
                objIDirectDeliveryView.Booking_Branch = objDR["Booking_Branch_Name"].ToString();
                objIDirectDeliveryView.Delivery_Location = objDR["Delivery_Location"].ToString();
                objIDirectDeliveryView.Payment_Type_Id = Util.String2Int(objDR["Payment_Type_Id"].ToString());
                objIDirectDeliveryView.Payment_Type = objDR["Payment_Type"].ToString();
                objIDirectDeliveryView.Total_GC_Amount = Util.String2Decimal(objDR["Total_GC_Amount"].ToString());
                objIDirectDeliveryView.Booking_Articles = Util.String2Int(objDR["Total_Articles"].ToString());
                objIDirectDeliveryView.Booking_Articles_Weight = Util.String2Decimal(objDR["Total_Actual_Weight"].ToString());
                objIDirectDeliveryView.Is_OctroiApplicable = Util.String2Bool(objDR["Is_Octroi"].ToString());
                objIDirectDeliveryView.Is_OctroiUpdated = Util.String2Bool(objDR["Is_Octroi_Updated"].ToString());
            }
            else
            {
                objIDirectDeliveryView.Booking_Branch_Id = 0;
                objIDirectDeliveryView.Booking_Branch = "";                
                objIDirectDeliveryView.Payment_Type_Id = 0;
                objIDirectDeliveryView.Payment_Type = "";
                objIDirectDeliveryView.Total_GC_Amount = 0;
                objIDirectDeliveryView.Booking_Articles = 0;
                objIDirectDeliveryView.Booking_Articles_Weight = 0;
                objIDirectDeliveryView.Is_OctroiApplicable = false; ;
                objIDirectDeliveryView.Is_OctroiUpdated = false ;
            }
        }

        public  void ReadValues()
        {
            objDS = objDirectDeliveryModel.ReadValues();
            
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                objIDirectDeliveryView.DDC_No_For_Print = objDR["DDC_No_For_Print"].ToString(); 
                objIDirectDeliveryView.DDC_Date = Convert.ToDateTime  (objDR["DDC_Date"].ToString());
                objIDirectDeliveryView.Remarks = objDR["Remarks"].ToString();
                objIDirectDeliveryView.ReceivedBy = Util.String2Int(objDR["Recd_By"].ToString());
                objIDirectDeliveryView.MRCashChequeDetailsView.CashAmount = Util.String2Decimal(objDR["Cash_Amount"].ToString());
                objIDirectDeliveryView.MRCashChequeDetailsView.ChequeAmount = Util.String2Decimal(objDR["Cheque_Amount"].ToString());
                objIDirectDeliveryView.MRCashChequeDetailsView.CashLedgerID = Util.String2Int(objDR["Cash_Ledger_ID"].ToString());
                objIDirectDeliveryView.MRCashChequeDetailsView.Total_ChequeAmount = Util.String2Decimal(objDR["Cheque_Amount"].ToString());

                objIDirectDeliveryView.TDS = Util.String2Decimal(objDR["TDS"].ToString());


                objIDirectDeliveryView.IsFreightReceived = Util.String2Bool(objDR["IsFreightReceived"].ToString());


                if (objIDirectDeliveryView.ReceivedBy == 2)
                {
                    objIDirectDeliveryView.Set_DebitTo_LedgerID(objDR["Ledger_Name"].ToString(), objDR["Ledger_Id"].ToString());
                    objIDirectDeliveryView.Set_DebitTo_BranchID(objDR["Branch_Name"].ToString(), objDR["Branch_Id"].ToString());
                }
            }

            if (objDS.Tables[1].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[1].Rows[0];

                objIDirectDeliveryView.Memo_Id = Util.String2Int(objDR["Memo_ID"].ToString());
                objIDirectDeliveryView.VehicleSearchView.VehicleID = Util.String2Int(objDR["Vehicle_Id"].ToString());
                objIDirectDeliveryView.Vehicle_Id = Util.String2Int(objDR["Vehicle_Id"].ToString());
                objDirectDeliveryModel.Get_VehicleDetails();
                objIDirectDeliveryView.SetLHPO(objDR["LHPO_No_For_Print"].ToString(), objDR["LHPO_ID"].ToString());
                objIDirectDeliveryView.LHPO_Id =Util.String2Int(objDR["LHPO_ID"].ToString());
                objIDirectDeliveryView.SetGC(objDR["GC_No_For_Print"].ToString(), objDR["GC_ID"].ToString());
                Get_GCDetails();
                Get_LHPO_Details();                
                objIDirectDeliveryView.ActualDeliveryDate = Convert.ToDateTime(objDR["Delivery_Date"].ToString());
                objIDirectDeliveryView.Delivery_Time = objDR["Delivery_Time"].ToString();
                objIDirectDeliveryView.Previous_Article_ID = Util.String2Int(objDR["Previous_Article_ID"].ToString());
                objIDirectDeliveryView.Previous_Status_ID = Util.String2Int(objDR["Previous_Status_ID"].ToString());
                objIDirectDeliveryView.Previous_Document_ID = Util.String2Int(objDR["Previous_Document_ID"].ToString());
                objIDirectDeliveryView.Previous_Document_No_For_Print = objDR["Previous_Document_No_For_Print"].ToString();
                objIDirectDeliveryView.Previous_Document_Date = Convert.ToDateTime(objDR["Previous_Document_Date"].ToString());
                objIDirectDeliveryView.Delivered_Articles = Util.String2Int(objDR["Delivered_Articles"].ToString());
                objIDirectDeliveryView.Delivered_Articles_Weight = Util.String2Decimal(objDR["Delivered_Actual_Wt"].ToString());
                objIDirectDeliveryView.Damage_Leakage_Articles = Util.String2Int(objDR["Damaged_Articles"].ToString());
                objIDirectDeliveryView.Damage_Leakage_Articles_Value = Util.String2Decimal(objDR["Damaged_Value"].ToString());
                objIDirectDeliveryView.Delivery_Taken_By = objDR["Delivery_Taken_By"].ToString();
               // objIDirectDeliveryView.GC_Id = Util.String2Int(objDR["GC_ID"].ToString());
                objIDirectDeliveryView.Is_PODReceived = Util.String2Bool(objDR["Is_POD_Received"].ToString());
                objIDirectDeliveryView.Loaded_Articles = Util.String2Int(objDR["Balance_Articles"].ToString());
                objIDirectDeliveryView.Loaded_Articles_Weight = Util.String2Decimal  (objDR["Balance_Actual_Wt"].ToString());
                objIDirectDeliveryView.Delivery_Condition = Util.String2Int(objDR["Received_Condition_ID"].ToString());
                objIDirectDeliveryView.Reason_For_Late_Uploading = Util.String2Int(objDR["Reason_ID"].ToString());
                objIDirectDeliveryView.DeliveryToID = Util.String2Int(objDR["Delivery_To_ID"].ToString());
                objIDirectDeliveryView.DeliveryAgainstID = Util.String2Int(objDR["Delivery_Against_ID"].ToString());
                objIDirectDeliveryView.ConsigneeCopyID = Util.String2Int(objDR["Cne_Copy_Status_ID"].ToString());

            }           
        }

        public void save()
        {
            base.DBSave();
        }         
    }
}






