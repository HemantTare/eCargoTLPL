using System;
using System.Data;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

/// <summary>
/// Summary description for AUSExcessDetailPresenter
/// </summary>
namespace Raj.EC.OperationPresenter
{
    public class AUSExcessDetailsPresenter : Presenter
    {
        private IAUSExcessDetailsView objIAUSExcessDetailsView;
        private AUSExcessDetailsModel objAUSExcessDetailsModel;
        private DataSet objDS;

        public AUSExcessDetailsPresenter(IAUSExcessDetailsView AUSExcessDetailsView, bool isPostback)
        {
            objIAUSExcessDetailsView = AUSExcessDetailsView;
            objAUSExcessDetailsModel = new AUSExcessDetailsModel(objIAUSExcessDetailsView);
            base.Init(objIAUSExcessDetailsView, objAUSExcessDetailsModel);

            if (!isPostback)
            {
                initValues();
            }
        }

        private void fillValues()
        {
            objDS = objAUSExcessDetailsModel.FillValues();
            objIAUSExcessDetailsView.SessionBindPackingTypeDropdown = objDS.Tables[0];
            objIAUSExcessDetailsView.SessionBindCommodityDropdown = objDS.Tables[1];
            objIAUSExcessDetailsView.SessionBindItemDropdown = objDS.Tables[2];
        }

        private void initValues()
        {
            fillValues();

            objDS = objAUSExcessDetailsModel.ReadValues();
            objIAUSExcessDetailsView.SessionBindExcessGrid = objDS.Tables[0];

           
        }
        public void FillItemOnCommodityChanged()
        {
            objDS = objAUSExcessDetailsModel.FillItemOnCommodityChanged();
            objIAUSExcessDetailsView.SessionBindItemDropdown = objDS.Tables[0];
            
            
        }
    }
}