using System;
using System.Data;

using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;

using ClassLibraryMVP;


/// <summary>
/// Author        : Ankit Champaneriya 
/// Created On    : 17/10/2008
/// Summary description for TDSDeducteeTypePresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{

    public class TDSDeducteeTypePresenter : ClassLibraryMVP.General.Presenter
    {

        private ITDSDeducteeTypeView objITDSDeducteeTypeView;
        private TDSDeducteeTypeModel objTDSDeducteeTypeModel;
        DataSet objDS;

        public TDSDeducteeTypePresenter(ITDSDeducteeTypeView tdsDeducteeTypeView, bool IsPostBack)
        {
            objITDSDeducteeTypeView = tdsDeducteeTypeView;
            objTDSDeducteeTypeModel = new TDSDeducteeTypeModel(objITDSDeducteeTypeView);

            base.Init(objITDSDeducteeTypeView, objTDSDeducteeTypeModel);
            if (!IsPostBack)
            {
                Fill_Deductee_Type();
                FillGrid();

            }
        }
        private void Fill_Deductee_Type()
        {
            objITDSDeducteeTypeView.Session_Deductee_Type = objTDSDeducteeTypeModel.Fill_Deductee_Type();
            objITDSDeducteeTypeView.Deductee_Type = objITDSDeducteeTypeView.Session_Deductee_Type;

            if (objITDSDeducteeTypeView.Session_Deductee_Type.Rows[0]["Deductee_Residential_Status"].ToString() == "Resident")
            {

                objITDSDeducteeTypeView.Resedential_Status = 0;
            }
            else
            {
                objITDSDeducteeTypeView.Resedential_Status = 1;
            }

            if (objITDSDeducteeTypeView.Session_Deductee_Type.Rows[0]["Deductee_Status"].ToString() == "Company")
            {
                objITDSDeducteeTypeView.Deductee_Status = 0;
            }
            else
            {
                objITDSDeducteeTypeView.Deductee_Status = 1;
            }
        }
        public void FillGrid()
        {
            //if (objITDSDeducteeTypeView.Deductee_Type_ID != -1)
            //{
                objITDSDeducteeTypeView.Session_Deductee_Details = objTDSDeducteeTypeModel.Fill_Deductee_Details_Grid();
                objITDSDeducteeTypeView.Bind_Deductee_Grid = objITDSDeducteeTypeView.Session_Deductee_Details;
            //}
            //else
            //{
            //    DataSet ds = new DataSet();
            //    DataTable dt = new DataTable("Table");
            //    dt.Columns.Add("TDS_Deductee_Type_Id");
            //    dt.Columns.Add("Applicable_Form");
            //    dt.Columns.Add("Exemption_Limit");
            //    dt.Columns.Add("Surcharge");
            //    dt.Columns.Add("Addl_Surcharge_Cess");
            //    dt.Columns.Add("Addl_Education_Cess");
            //    dt.Columns.Add("Display");
            //    ds.Tables.Add(dt);
            //    objITDSDeducteeTypeView.Session_Deductee_Details = ds;
            //    objITDSDeducteeTypeView.Bind_Deductee_Grid = objITDSDeducteeTypeView.Session_Deductee_Details;
            //}
        }

        public void Save()
        {
            base.DBSave();
            //objTDSDeducteeTypeModel.Save();
        }
    }

}
