using System;
using System.Data;
using System.Web.UI.WebControls;

using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;


/// <summary>
/// Summary description for TDSDeductionPresenter
/// </summary>
/// 

namespace Raj.EC.FinancePresenter
{
    public class TDSDeductionPresenter : Presenter
    {
        private ITDSDeductionView _TDSDeductionView;
        private TDSDeductionModel _TDSDeductionModel;

        private   DataSet ds_Primary_Group_Ledger = new DataSet();
        private    DataSet ds_Sub_Group_Ledger = new DataSet();
        private DataSet ds_Ledger = new DataSet();
        private DataSet DS_Temp = new DataSet();
        private int Primary_Ledger_Group_Id;

        private String str;
        private String Space;
        private String Add_Space;

        private int Hierarchi_Level;
     
        private void initValues()
        {

            if (_TDSDeductionView.keyID > 0)
            {
                DataSet objDS;

                objDS = _TDSDeductionModel.ReadValues();

                if (objDS.Tables[0].Rows.Count > 0)
                {

                    DataRow objDR = objDS.Tables[0].Rows[0];

                    //  _TDSDeductionView.TDSDeductionNo = objDR["TDSDeduction_No_For_Print"].ToString();
                    _TDSDeductionView.TDSDeduction_Date   = Convert.ToDateTime(objDR["Voucher_Date"].ToString());
                    _TDSDeductionView.TDS_Ledger_Id_View  = Convert.ToString(objDR["TDS Ledger"].ToString());
                    _TDSDeductionView.Ledger_Account_Id_View  = Convert.ToString(objDR["Ledger Account"].ToString());
                    _TDSDeductionView.Journal_No = objDR["Voucher_No"].ToString();

                    _TDSDeductionView.Ledger_Group_Id_View  = Convert.ToString(objDR["Ledger Group Name"].ToString());   
                    _TDSDeductionView.Total_Amount_Paid_Amount =   Convert.ToString  (objDR["Total_Amount_Paid_Payable"].ToString());   
                    _TDSDeductionView.Tax_Percent  =   Convert.ToString  (objDR["Tax_Rate"].ToString());   
                    _TDSDeductionView.Tax_Amount  =   Convert.ToString  (objDR["Tax_Amount"].ToString());   
                    _TDSDeductionView.Surcharge_Percent  =   Convert.ToString  (objDR["Surcharge_Rate"].ToString());   
                    _TDSDeductionView.Surcharge_Amount  =   Convert.ToString  (objDR["Surcharge_Amount"].ToString());   
                    _TDSDeductionView.Addtional_Surcharge_Percent =   Convert.ToString  (objDR["Add_Surcharge_Rate"].ToString());   
                    _TDSDeductionView.Addtional_Surcharge_Amount  =   Convert.ToString  (objDR["Add_Surcharge_Amount"].ToString());   
                    _TDSDeductionView.Addl_Ed_Cess_Percent  =   Convert.ToString  (objDR["Add_Edu_Cess_Rate"].ToString());   
                    _TDSDeductionView.Addl_Ed_Cess_Amount  =   Convert.ToString  (objDR["Add_Edu_Cess_Amount"].ToString());   
                    _TDSDeductionView.Total_TDS_Amount =     Convert.ToString  (objDR["Total_TDS"].ToString());   
                    _TDSDeductionView.Less_TDS_Deducted_Till_Date_Amount  =     Convert.ToString  (objDR["TDS_Deducted_Till_Date"].ToString());   
                    _TDSDeductionView.Net_TDS_To_Deduct_Amount  =     Convert.ToString  (objDR["Net_TDS_To_Deduct"].ToString());   
                    _TDSDeductionView.Bill_No  =     Convert.ToString  (objDR["bill_Name"].ToString());
                    _TDSDeductionView.Reference_No = objDR["Reference_No"].ToString();
                    _TDSDeductionView.Narration = objDR["Narration"].ToString();

        
                    _TDSDeductionView.Credit_Days = objDR["Credit_Days"].ToString();
                    _TDSDeductionView.Gross_Amount = objDR["Gross_Amount"].ToString();
                    _TDSDeductionView.Amount = objDR["Amount"].ToString();
                }
            }
            else
            {
              
            }
        }




        public void Generate_Voucher_No()
        {
            _TDSDeductionView.Journal_No = _TDSDeductionModel.Generate_Voucher_No();
          //  _TDSDeductionView.TDSDeductionNo = _TDSDeductionModel.FA_VT_Generate_Voucher_No();
        }

        public TDSDeductionPresenter(ITDSDeductionView TDSDeductionView, bool isPostBack)
        {
            _TDSDeductionView = TDSDeductionView;
            _TDSDeductionModel = new TDSDeductionModel(_TDSDeductionView);
            base.Init(_TDSDeductionView, _TDSDeductionModel);

            if (isPostBack == false)
            {
                Fill_Values();
                initValues();
            }
        }

        public void Fill_Values()
        {
            DataSet objDS;
            objDS = _TDSDeductionModel.Fill_Values();

            _TDSDeductionView.BindLedgerGroup  = objDS.Tables[0];

            Fill_Ledger_Account();
            Fill_TDS_Ledger();            
          
        }


         
        public void Fill_Ledger_Account()
        {
            DataSet objDS;
            objDS = _TDSDeductionModel.Fill_Ledger_Account();

            _TDSDeductionView.BindLedgerAccount  = objDS.Tables[0];
                       
        }


          
        public void Fill_TDS_Ledger()
        {
            DataSet objDS;
            objDS = _TDSDeductionModel.Fill_TDS_Ledger();

            _TDSDeductionView.BindTDSLedger  = objDS.Tables[0];

            
        }
        public void Fill_Pending_Bills()
        {
            DataSet objDS;
            objDS = _TDSDeductionModel.Fill_Pending_Bills();

            _TDSDeductionView.BindName = objDS.Tables[0];
 
        } 

        public void Get_TDS_Ledger_Details()
        {
            DataSet objDS;
            objDS = _TDSDeductionModel.Get_TDS_Ledger_Details();



            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                _TDSDeductionView.Total_Amount_Paid_Amount = Convert.ToString(objDR["Total_Amount_Payable"]);

                _TDSDeductionView.Tax_Percent = Convert.ToString(objDR["Tax_Rate"]);
                _TDSDeductionView.Tax_Amount = Convert.ToString(objDR["Tax_Amount"]);

                _TDSDeductionView.Surcharge_Percent = Convert.ToString(objDR["Surcharge_Rate"]);
                _TDSDeductionView.Surcharge_Amount = Convert.ToString(objDR["Surcharge_Amount"]);

                _TDSDeductionView.Addl_Ed_Cess_Percent = Convert.ToString(objDR["Addl_Edu_Cess_Rate"]);
                _TDSDeductionView.Addl_Ed_Cess_Amount = Convert.ToString(objDR["Addl_Edu_Cess_Amount"]);

                _TDSDeductionView.Addtional_Surcharge_Percent = Convert.ToString(objDR["Addl_Surcharge_Rate"]);
                _TDSDeductionView.Addtional_Surcharge_Amount = Convert.ToString(objDR["Addl_Surcharge_Amount"]);

                _TDSDeductionView.Total_TDS_Amount = Convert.ToString(objDR["Total_TDS_Amount"]);
                _TDSDeductionView.Less_TDS_Deducted_Till_Date_Amount = Convert.ToString  (objDR["Less_TDS_Deducted"]);
                _TDSDeductionView.Net_TDS_To_Deduct_Amount = Convert.ToString(objDR["Net_TDS_To_Deduct"]);
            }
            else
            {
                _TDSDeductionView.Total_Amount_Paid_Amount = "0";

                _TDSDeductionView.Tax_Percent = "0";
                _TDSDeductionView.Tax_Amount = "0";

                _TDSDeductionView.Surcharge_Percent = "0";
                _TDSDeductionView.Surcharge_Amount = "0";

                _TDSDeductionView.Addl_Ed_Cess_Percent = "0";
                _TDSDeductionView.Addl_Ed_Cess_Amount = "0";
                
                _TDSDeductionView.Addtional_Surcharge_Percent = "0";
                _TDSDeductionView.Addtional_Surcharge_Amount = "0";
                
                _TDSDeductionView.Total_TDS_Amount = "0";
                _TDSDeductionView.Less_TDS_Deducted_Till_Date_Amount = "0";
                _TDSDeductionView.Net_TDS_To_Deduct_Amount = "0";

            }
           
        }

        public DataSet BindLHPODetails()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("Table");
            dt.Columns.Add("Sr_No");
            dt.Columns.Add("From_Days");
            dt.Columns.Add("To_Days");
            ds.Tables.Add(dt);
            return ds;
        }

       




        public void Save()
        {
             base.DBSave();

             //_TDSDeductionModel.Save();
        }
    }
}






