using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;
using Raj.FA.ReportsView;
using Raj.FA.ReportsModel;

/// <summary>
/// Summary description for TDSComputationPresenter
/// </summary>
namespace Raj.FA.ReportsPresenter
{
    public class TDSComputationPresenter : Presenter
    {
        private ITDSComputationView objITDSComputationView;
        private TDSComputationModel objTDSComputationModel;
        DataSet objDS;

        public TDSComputationPresenter(ITDSComputationView tdsComputationView, bool IsPostBack)
        {
            objITDSComputationView = tdsComputationView;
            objTDSComputationModel = new TDSComputationModel(objITDSComputationView);

            base.Init(objITDSComputationView, objTDSComputationModel);

            if (!IsPostBack)
            {
               
                FillGrid();

            }
        }
        public void FillGrid()
        {
            objITDSComputationView.BindTDSGrid = objTDSComputationModel.ReadGridValues();
            
           
        }
	}
}
