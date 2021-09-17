using System;
using System.Data;

using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;

using ClassLibraryMVP;

/// <summary>
/// Author        : Ankit Champaneriya 
/// Created On    : 16/10/2008
/// Summary description for TdsNatureOfPaymentPresenter
/// </summary>
/// 

namespace Raj.EC.FinancePresenter
{

    public class TdsNatureOfPaymentPresenter : ClassLibraryMVP.General.Presenter
    {
        private ITdsNatureOfPaymentView objITdsNatureOfPaymentView;
        private TdsNatureOfPaymentModel objTdsNatureOfPaymentModel;
        DataSet objDS;

        public TdsNatureOfPaymentPresenter(ITdsNatureOfPaymentView tdsNatureOfPaymentView, bool IsPostBack)
        {
            objITdsNatureOfPaymentView = tdsNatureOfPaymentView;
            objTdsNatureOfPaymentModel = new TdsNatureOfPaymentModel(objITdsNatureOfPaymentView);

            base.Init(objITdsNatureOfPaymentView, objTdsNatureOfPaymentModel);

            if (!IsPostBack)
            {
                initValues();
                //FillOnDeducteeTypeChange();
            }
        }
        public void initValues()
        {

            objITdsNatureOfPaymentView.Session_Nature_Payment = objTdsNatureOfPaymentModel.Fill_Nature_Payment();
            objITdsNatureOfPaymentView.Nature_Payment = objITdsNatureOfPaymentView.Session_Nature_Payment;
            objITdsNatureOfPaymentView.Section = objITdsNatureOfPaymentView.Session_Nature_Payment.Tables[0].Rows[0]["TDS_Rate_Section"].ToString();
            objITdsNatureOfPaymentView.Payment_Code = objITdsNatureOfPaymentView.Session_Nature_Payment.Tables[0].Rows[0]["TDS_Rate_Challan_Code"].ToString();
            //objITdsNatureOfPaymentView.Session_Deductee_Type = objTdsNatureOfPaymentModel.FillDeducteeType();
            objITdsNatureOfPaymentView.Session_Deductee_Type = objTdsNatureOfPaymentModel.Fill_Deductee_Type();
            //if (objITdsNatureOfPaymentView.Nature_Payment_ID != -1)
            //{
                fill_Nature_Payment_Grid();

            //}
            //else
            //{
            //    DataSet ds = new DataSet();
            //    DataTable dt = new DataTable("Table");
            //    dt.Columns.Add("TDS_Deductee_Type_Id");
            //    dt.Columns.Add("TDS_Deductee_Type_Name");
            //    dt.Columns.Add("Nature_Payment_Id");
            //    dt.Columns.Add("Applicable_Form");
            //    dt.Columns.Add("Exemption_Limit");
            //    dt.Columns.Add("Rate");
            //    dt.Columns.Add("Display");
            //    ds.Tables.Add(dt);
            //    objITdsNatureOfPaymentView.Session_Payment_Details = ds;
            //    objITdsNatureOfPaymentView.Bind_Payment_Grid = objITdsNatureOfPaymentView.Session_Payment_Details;
            //}

        }
        public void FillOnDeducteeTypeChange()
        {
            objITdsNatureOfPaymentView.Session_Deductee_Type = objTdsNatureOfPaymentModel.Fill_Deductee_Type();
            //objITdsNatureOfPaymentView.Bind_Deductee_Type = objITdsNatureOfPaymentView.Session_Deductee_Type;
            //return objDS;
        }

        public void fill_Nature_Payment_Grid()
        {
            objITdsNatureOfPaymentView.Session_Payment_Details = objTdsNatureOfPaymentModel.Fill_Payment_Details_Grid();
            objITdsNatureOfPaymentView.Bind_Payment_Grid = objITdsNatureOfPaymentView.Session_Payment_Details;
        }

        public void Save()
        {
            base.DBSave();
            //objTdsNatureOfPaymentModel.Save();
        }
    }
}
