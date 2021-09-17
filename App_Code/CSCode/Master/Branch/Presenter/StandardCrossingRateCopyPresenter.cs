using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;
/// <summary>
/// Summary description for StandardCrossingRateCopyPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class StandardCrossingRateCopyPresenter : ClassLibraryMVP.General.Presenter
    {

        private IStandardCrossingRateCopyView objIStandardCrossingRateCopyView;
        private StandardCrossingRateCopyModel objStandardCrossingRateCopyModel;
        private DataSet objDS;

        public StandardCrossingRateCopyPresenter(IStandardCrossingRateCopyView standardCrossingRateCopyView, bool isPostBack)
        {
            objIStandardCrossingRateCopyView = standardCrossingRateCopyView;
            objStandardCrossingRateCopyModel = new StandardCrossingRateCopyModel(objIStandardCrossingRateCopyView);
            base.Init(objIStandardCrossingRateCopyView, objStandardCrossingRateCopyModel);

            if (!isPostBack)
            {
                objDS = objStandardCrossingRateCopyModel.FillValues();
                objIStandardCrossingRateCopyView.Bind_ddl_Area  = objDS.Tables["AreaMaster"];
                objIStandardCrossingRateCopyView.Bind_ddl_CopyFromBranchID = objDS.Tables["BranchMaster"];
                objIStandardCrossingRateCopyView.Bind_ddl_FromBranchID = objDS.Tables["BranchMaster"];
            }
        }
        public int Save()
        {
           // objStandardCrossingRateCopyModel.Save();
            //base.DBSave();

            Message objMsg = new Message();
            if (objIStandardCrossingRateCopyView.validateUI())
            {
                objMsg = objStandardCrossingRateCopyModel.Save();

            }
            if (objMsg.message != "")
            {
                objIStandardCrossingRateCopyView.errorMessage = objMsg.message;
            }

            return objMsg.messageID;
        }
 
    }
}