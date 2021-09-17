using System;
using System.Data;

using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;
using ClassLibraryMVP;

/// <summary>
/// Author        : Ankit Champaneriya 
/// Created On    : 17/10/2008
/// Summary description for FBTAssesseeDetailsPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{

    public class FBTAssesseeDetailsPresenter : ClassLibraryMVP.General.Presenter
    {

        private IFBTAssesseeDetailsView objIFBTAssesseeDetailsView;
        private FBTAssesseeDetailsModel objFBTAssesseeDetailsModel;
        DataSet objDS;

        public FBTAssesseeDetailsPresenter(IFBTAssesseeDetailsView fbtAssesseeDetailsView, bool IsPostBack)
        {
            objIFBTAssesseeDetailsView = fbtAssesseeDetailsView;
            objFBTAssesseeDetailsModel = new FBTAssesseeDetailsModel(objIFBTAssesseeDetailsView);

            base.Init(objIFBTAssesseeDetailsView, objFBTAssesseeDetailsModel);

            if (!IsPostBack)
            {
                initValues();
                FillGridOnAssesseeTypeChange();
            }


        }

        void initValues()
        {
            objIFBTAssesseeDetailsView.SessionAssesseeType = objFBTAssesseeDetailsModel.FillValues();
        }

        public void FillGridOnAssesseeTypeChange()
        {
            objIFBTAssesseeDetailsView.SessionAssesseeDetails = objFBTAssesseeDetailsModel.FillGrid();
        }

        public void Save()
        {
            base.DBSave();
            //objFBTAssesseeDetailsModel.Save();            
        }
    }
}