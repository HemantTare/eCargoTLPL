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
using ClassLibraryMVP;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for CompanyCaptionPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class CompanyCaptionPresenter : Presenter
    {
        private ICompanyCaptionView objICompanyCaptionView;
        private CompanyCaptionModel objCompanyCaptionModel;
        private DataSet objDS;

        public CompanyCaptionPresenter(ICompanyCaptionView companyCaptionView, bool IsPostBack)
        {
            objICompanyCaptionView = companyCaptionView;
            objCompanyCaptionModel = new CompanyCaptionModel(objICompanyCaptionView);

            base.Init(objICompanyCaptionView, objCompanyCaptionModel);

            if (!IsPostBack)
            {
                
                initValues();
            }
        }
        public void Save()
        {
            base.DBSave();
            
        }

        private void initValues()
        {
            objDS = objCompanyCaptionModel.ReadValues();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow DR = objDS.Tables[0].Rows[0];
                objICompanyCaptionView.GCCaption = DR["GC_Caption"].ToString();
                objICompanyCaptionView.LHPOCaption = DR["LHPO_Caption"].ToString();
                objICompanyCaptionView.IsMemoSeriesRequired = Util.String2Bool(DR["Is_Memo_Series_Required"].ToString());
                objICompanyCaptionView.IsLHPOSeriesRequired = Util.String2Bool(DR["Is_LHPO_Series_Required"].ToString());
                objICompanyCaptionView.IsAlsRequired = Util.String2Bool(DR["Is_ALS_Required"].ToString());
                objICompanyCaptionView.IsTasRequired = Util.String2Bool(DR["Is_TAS_Required"].ToString());
                objICompanyCaptionView.MinDiffTASandAUS = Util.String2Int(DR["Minutes_Diff_Between_TAS_And_AUS"].ToString());

            }
        }
	}
}
