using System;
using System.Data;

using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

namespace Raj.EC.OperationPresenter
{
    public class FrghtDisVoucherPresenter : Presenter
    {
        private IFrghtDisVoucherView objIFrghtDisVoucherView;
        private FrghtDisVoucherModel objFrghtDisVoucherModel;
        private DataSet objDS;
        int _vehicleid;

        public int VehicleID
        {
            set { _vehicleid = value; }
            get { return _vehicleid; }
        }

        public FrghtDisVoucherPresenter(IFrghtDisVoucherView FrghtDisVoucherView, bool isPostBack)
        {
            objIFrghtDisVoucherView = FrghtDisVoucherView;
            objFrghtDisVoucherModel = new FrghtDisVoucherModel(objIFrghtDisVoucherView);
            base.Init(objIFrghtDisVoucherView, objFrghtDisVoucherModel);

            if (!isPostBack)
            {
                objIFrghtDisVoucherView.VoucherDate = DateTime.Now.Date;
                
                FillValues();
                initValues();
            }
        } 

        public void FillValues()
        {
            objDS = objFrghtDisVoucherModel.FillValues();  
            objIFrghtDisVoucherView.SessionBindDDLUndelReason = objDS.Tables[0];  
        } 

        public void FillGrid()
        {
            objDS = objFrghtDisVoucherModel.ReadValues();
            objIFrghtDisVoucherView.SessionBindFDVGrid = objDS.Tables[0];

            if (objDS.Tables[0].Rows.Count > 0)
            {
            } 

            if (objIFrghtDisVoucherView.keyID > 0)
            {
                if (objDS.Tables[1].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];
                  
                    objIFrghtDisVoucherView.Total_No_Of_GC = Util.String2Int(objDR["Total_No_Of_GC"].ToString());
                    objIFrghtDisVoucherView.Total_Total_GC_Amount = Util.String2Int(objDR["Total_GC_Amount"].ToString());
                    objIFrghtDisVoucherView.Total_DiscountAmt = Util.String2Decimal(objDR["Total_DiscountAmt"].ToString());
                }
            }
        }

        private void initValues()
        {
            objDS = objFrghtDisVoucherModel.ReadValues();
            objIFrghtDisVoucherView.SessionBindFDVGrid = objDS.Tables[0];

            if (objDS.Tables[0].Rows.Count > 0)
            {
                
            }
            else
            {   
            }
           

            if (objIFrghtDisVoucherView.keyID > 0)
            {
                if (objDS.Tables[1].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[1].Rows[0];

                    objIFrghtDisVoucherView.VoucherDate = Convert.ToDateTime(objDR["Voucher_Date"].ToString());
                    objIFrghtDisVoucherView.VoucherNo = objDR["Voucher_No_For_Print"].ToString();
                    objIFrghtDisVoucherView.Total_Total_GC_Amount = Util.String2Decimal(objDR["Total_GC_Amount"].ToString());
                    objIFrghtDisVoucherView.Total_No_Of_GC = Util.String2Int(objDR["Total_No_Of_GC"].ToString());
                    objIFrghtDisVoucherView.Total_DiscountAmt = Util.String2Decimal(objDR["Total_Discount"].ToString());
                    objIFrghtDisVoucherView.Remarks = objDR["Remarks"].ToString();
                }
            }
        }

        public void save()
        {
            base.DBSave();
        }
    }
}