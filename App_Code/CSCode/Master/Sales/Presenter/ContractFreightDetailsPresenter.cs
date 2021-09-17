using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for ContractFreightDetailsPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class ContractFreightDetailsPresenter : ClassLibraryMVP.General.Presenter
    {
        private IContractFreightDetailsView objIContractFreightDetailsView;
        private ContractFreightDetailsModel objContractFreightDetailsModel;
        private DataSet objDS;

        public ContractFreightDetailsPresenter(IContractFreightDetailsView contractFreightDetailsView, bool isPostBack)
        {
            objIContractFreightDetailsView = contractFreightDetailsView;
            objContractFreightDetailsModel = new ContractFreightDetailsModel(objIContractFreightDetailsView);
            base.Init(objIContractFreightDetailsView, objContractFreightDetailsModel);
            if (!isPostBack)
            {
                objDS = objContractFreightDetailsModel.FillValues();
                objIContractFreightDetailsView.Bind_ddl_UnitOfFreight = objDS.Tables["UnitOfFreight"];
                objIContractFreightDetailsView.Bind_ddl_FreightBasis = objDS.Tables["FreightBasis"];
                objIContractFreightDetailsView.Bind_ddl_SubUnit = objDS.Tables["SubUnit"];
                FillUnitFreightDropdown();
                FillSubUnitFreightDropdown();
               // FillLocation();
                SetOtherChargesDetails();
                initValues();
                FillGrid();
            }

        }
        public void Save()
        {
            objContractFreightDetailsModel.Save();
            //base.DBSave();
        }
        private void initValues()
        {

            if (objIContractFreightDetailsView.keyID > 0)
            {
                objDS = objContractFreightDetailsModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIContractFreightDetailsView.UnitOfFreightID = Util.String2Int(DR["Freight_Unit_ID"].ToString());
                    FillUnitFreightDropdown();
                    objIContractFreightDetailsView.SubUnitID = Util.String2Int(DR["Freight_Sub_Unit_ID"].ToString());
                    FillSubUnitFreightDropdown();
                    objIContractFreightDetailsView.FreightBasisID = Util.String2Int(DR["Freight_Basis_ID"].ToString());
                    objIContractFreightDetailsView.CFTFactor = Util.String2Decimal(DR["Cft_Factor"].ToString());

                }
            }
        }
      
        public void FillGrid()
        {
            // FillLocation();
            DataSet ds = new DataSet();
            ds = objContractFreightDetailsModel.FillGrid();
            objIContractFreightDetailsView.SessionFreightDetailsGrid = ds;
            objIContractFreightDetailsView.Bind_dg_FreightDetails = ds;
        }

        public void SetOtherChargesDetails()
        {
            // FillLocation();
            DataSet ds = new DataSet();
            ds = objContractFreightDetailsModel.GetOtherChargesDetails();
            objIContractFreightDetailsView.SessionOtherChargesFreightRateDetails = ds;
        }

        public void FillUnitFreightDropdown()
        {
            DataSet ds = new DataSet();
            ds = objContractFreightDetailsModel.FillUnitFreightDropdown();

            if (ds.Tables.Count != 0)
                objIContractFreightDetailsView.Bind_ddl_UnitFreight = ds.Tables[0];
        }

        public void FillSubUnitFreightDropdown()
        {
            DataSet ds = new DataSet();
            ds = objContractFreightDetailsModel.FillSubUnitFreightDropdown();

            if (ds.Tables.Count != 0)
                objIContractFreightDetailsView.Bind_ddl_SubUnitFreight = ds.Tables[0];
        }

        //public void FillLocation()
        //{
        //    DataSet ds = new DataSet();
        //    ds = objContractFreightDetailsModel.FillServiceLocation();
        //    objIContractFreightDetailsView.SessionFromToLocation = ds.Tables[0];
        //}

    }
}