using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for ContractTermsPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class ContractTermsPresenter : ClassLibraryMVP.General.Presenter
    {
        private IContractTermsView objIContractTermsView;
        private ContractTermsModel objContractTermsModel;
        private DataSet objDS;

        public ContractTermsPresenter(IContractTermsView contractTermsView, bool isPostBack)
        {
            objIContractTermsView = contractTermsView;
            objContractTermsModel = new ContractTermsModel(objIContractTermsView);
            base.Init(objIContractTermsView, objContractTermsModel);
            DataSet ds = new DataSet();
            if (!isPostBack)
            {
                objDS = objContractTermsModel.FillValues();
                ds.Tables.Add(objDS.Tables["TermsGrid"].Copy());
                objIContractTermsView.SessionContractTermsGrid = ds;
                objIContractTermsView.SessionTermsHead = objDS.Tables["TermsMaster"];
                objIContractTermsView.Bind_dg_ContractTerms = ds;
                objIContractTermsView.Bind_ddl_TermsHead = objDS.Tables["TermsMaster"];
                initValues();

            }

        }
        public void Save()
        {
            objContractTermsModel.Save();
            //base.DBSave();
        }
        public void GetTermDescraption()
        {
            objDS=objContractTermsModel.GetTermDescription();
            objIContractTermsView.TermsDescription = objDS.Tables[0].Rows[0]["Description"].ToString();
        }
        private void initValues()
        {

            if (objIContractTermsView.keyID > 0)
            {
                objDS = objContractTermsModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIContractTermsView.SessionContractTermsGrid = objDS;
                    objIContractTermsView.Bind_dg_ContractTerms = objDS;
                }
            }
        }
    }
}