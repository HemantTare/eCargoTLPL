using System;
using System.Data;
using System.Data.SqlClient;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;
using Raj.EC;

/// <summary>
/// Author : Ankit champaneriya
/// Date   : 05-1-09
/// Summary description for DlyBranchUpdatePresenter
/// </summary>
/// 
namespace Raj.EC.OperationPresenter
{
    public class DlyBranchUpdatePresenter : Presenter
    {
        private IDlyBranchUpdateView objIDlyBranchUpdateView;
        private DlyBranchUpdateModel objDlyBranchUpdateModel;
        private DataSet objDS;
        private DAL objDAL = new DAL();

        public DlyBranchUpdatePresenter(IDlyBranchUpdateView DlyBranchUpdateView, bool isPostBack)
        {
            objIDlyBranchUpdateView = DlyBranchUpdateView;
            objDlyBranchUpdateModel = new DlyBranchUpdateModel(objIDlyBranchUpdateView);

            base.Init(objIDlyBranchUpdateView, objDlyBranchUpdateModel);

            if (!isPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            Fill_Values();
            ServiceLocation_FillValues();
            if (objIDlyBranchUpdateView.keyID > 0)
            {
                ReadValues();
            }
        }

        public void Fill_Values()
        {
            objDS = objDlyBranchUpdateModel.Fill_Values();

            objIDlyBranchUpdateView.BindDDLDeliveryBranch = objDS.Tables[0];
        }

        public void ServiceLocation_FillValues()
        {
            objDS = objDlyBranchUpdateModel.ServiceLocation_FillValues();
            objIDlyBranchUpdateView.BindDDLServiceLocation = objDS.Tables[0];
        }
        
        public void fillgrid()
        {
            objDS = objDlyBranchUpdateModel.ReadValues();
            objIDlyBranchUpdateView.SessionDGDeliveryBranch = objDS.Tables[0];
        }

       
        public void Save()
        {
            base.DBSave();
        }
    }
}