using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for FreightBranchCopyPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class FreightBranchCopyPresenter: ClassLibraryMVP.General.Presenter
    {

        private IFreightBranchCopyView objIFreightBranchCopyView;
        private FreightBranchCopyModel objFreightBranchCopyModel;
        private DataSet objDS;

        public FreightBranchCopyPresenter(IFreightBranchCopyView freightBranchCopyView, bool isPostBack)
        {
            objIFreightBranchCopyView = freightBranchCopyView;
            objFreightBranchCopyModel = new FreightBranchCopyModel(objIFreightBranchCopyView);
            base.Init(objIFreightBranchCopyView, objFreightBranchCopyModel);

            if (!isPostBack)
            {
                objDS = objFreightBranchCopyModel.FillValues();
                objIFreightBranchCopyView.Bind_ddl_Area = objDS.Tables["AreaMaster"];
                objIFreightBranchCopyView.Bind_ddl_CopyFromBranchID = objDS.Tables["BranchMaster"];
                objIFreightBranchCopyView.Bind_ddl_FromBranchID = objDS.Tables["BranchMaster"];
                objIFreightBranchCopyView.Bind_ddl_Commodity = objDS.Tables["CommodityMaster"];
            }
        }
        public int Save()
        {
            //objFreightBranchCopyModel.Save();
            //base.DBSave();
            
            Message objmsg = new Message();
            if (objIFreightBranchCopyView.validateUI())
            {
                objmsg = objFreightBranchCopyModel.Save();
            }
            if (objmsg.message != "")
            {
               objIFreightBranchCopyView.errorMessage = objmsg.message;
            }

            return objmsg.messageID;
        }
 
    }
}