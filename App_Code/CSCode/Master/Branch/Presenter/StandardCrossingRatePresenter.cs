using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;


/// <summary>
/// Summary description for StandardCrossingRatePresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class StandardCrossingRatePresenter:Presenter 
    {
        private IStandardCrossingRateView objIStandardCrossingRateView;
        private StandardCrossingRateModel objStandardCrossingRateModel;
        private DataSet objDS;

        public StandardCrossingRatePresenter(IStandardCrossingRateView standardCrossingRateView, bool isPostBack)
        {
            objIStandardCrossingRateView = standardCrossingRateView;
            objStandardCrossingRateModel = new StandardCrossingRateModel(objIStandardCrossingRateView);
            base.Init(objIStandardCrossingRateView, objStandardCrossingRateModel);

            if (!isPostBack)
            {
                objDS = objStandardCrossingRateModel.FillValues();
                objIStandardCrossingRateView.Bind_ddl_Branch = objDS.Tables["BranchMaster"];
                objIStandardCrossingRateView.Bind_ddl_Area = objDS.Tables["AreaMaster"];
                objIStandardCrossingRateView.Bind_dg_StandardCrossingRate = objDS.Tables["StandardCrossingGrid"];

            }

        }
        public void Save()
        {
            //objStandardCrossingRateModel.Save();
            //base.DBSave();
            Message objMsg = new Message();
            if (objIStandardCrossingRateView.validateUI())
            {
                objMsg = objStandardCrossingRateModel.Save();

            }
            if (objMsg.message != "")
            {
                objIStandardCrossingRateView.errorMessage = objMsg.message;
            }
        }
        public void FillGrid()
        {
            objDS = objStandardCrossingRateModel.FillGrid();
            objIStandardCrossingRateView.Bind_dg_StandardCrossingRate = objDS.Tables[0];
        }
  
    }
}