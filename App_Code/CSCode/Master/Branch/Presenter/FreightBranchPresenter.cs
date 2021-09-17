using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for FreightBranchPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class FreightBranchPresenter : ClassLibraryMVP.General.Presenter
    {
        private IFreightBranchView objIFreightBranchView;
        private FreightBranchModel objFreightBranchModel;
        private DataSet objDS;

        public FreightBranchPresenter(IFreightBranchView freightBranchView, bool isPostBack)
        {
            objIFreightBranchView = freightBranchView;
            objFreightBranchModel = new FreightBranchModel(objIFreightBranchView);
            base.Init(objIFreightBranchView, objFreightBranchModel);

            if (!isPostBack)
            {
                objDS = objFreightBranchModel.FillValues();
                objIFreightBranchView.Bind_ddl_Area = objDS.Tables["AreaMaster"];
                objIFreightBranchView.Bind_ddl_Branch  = objDS.Tables["BranchMaster"];
                objIFreightBranchView.Bind_dg_FreightBranch = objDS.Tables["FreightBranchGrid"];
                objIFreightBranchView.Bind_ddl_Commodity = objDS.Tables["Commodity"];
            }
        }
        public void Save()
        {
         //   objFreightBranchModel.Save();
            //base.DBSave();

            Message objMsg = new Message();

            if (objIFreightBranchView.validateUI())
            {
                objMsg = objFreightBranchModel.Save();
            }
            if (objMsg.messageID >0)
            {
                objIFreightBranchView.errorMessage = objMsg.message;
            }
        }
        public void FillGrid()
        {
            objDS = objFreightBranchModel.FillGrid();
            objIFreightBranchView.Bind_dg_FreightBranch = objDS.Tables[0];
        }
    }
}