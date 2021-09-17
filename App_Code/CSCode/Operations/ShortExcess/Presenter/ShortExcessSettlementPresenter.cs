using System;
using System.Data;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using ClassLibraryMVP.DataAccess;

/// <summary>
/// Summary description for ShortExcessSettlementPresenter
/// </summary>

namespace Raj.EC.OperationPresenter
{

    public class ShortExcessSettlementPresenter : Presenter
    {
        private IShortExcessSettlementView objIShortExcessSettlementView;
        private ShortExcessSettlementModel objShortExcessSettlementModel;
        private DataSet objDS;

        public ShortExcessSettlementPresenter(IShortExcessSettlementView ShortExcessSettlementView, bool isPostBack)
        {
            objIShortExcessSettlementView = ShortExcessSettlementView;
            objShortExcessSettlementModel = new ShortExcessSettlementModel(objIShortExcessSettlementView);
            base.Init(objIShortExcessSettlementView, objShortExcessSettlementModel);
        }

        public void fillgrid()
        {
            objDS = objShortExcessSettlementModel.ReadValues();
            objIShortExcessSettlementView.SessionShortExcessGrid = objDS.Tables[0];          
        }

        public DataSet Fill_GCDetails(int flag,int Gc_Id,int Branch_Id)
        {
            objDS = objShortExcessSettlementModel.Fill_GCDetails(flag, Gc_Id, Branch_Id);
            return objDS;
        }  

        public void Save()
        {
            base.DBSave();
        }
    }
}