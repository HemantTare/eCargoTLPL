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
using Raj.EC.FinanceView;
using Raj.EC.FinanceModel;

/// <summary>
/// Summary description for AgeingPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class AgeingPresenter : Presenter
    {
        private IAgeingView objIAgeingView;
        private AgeingModel objAgeingModel;
        private DataSet objds;

        public AgeingPresenter(IAgeingView ageingView, bool IsPostBack)
        {
            objIAgeingView = ageingView;
            objAgeingModel = new AgeingModel(objIAgeingView);

            base.Init(objIAgeingView, objAgeingModel);

            if (IsPostBack == false)
            {
                initValues();
                objIAgeingView.SessionAgeingGrid = BindAgeingGridHeader();
            }
        }

        private void initValues()
        {
            if (objIAgeingView.keyID > 0)
            {
                objds = objAgeingModel.ReadValues();

            }
           
        }
        public DataSet BindAgeingGridHeader()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("Table");
            dt.Columns.Add("Sr_No");
            dt.Columns.Add("From_Days");
            dt.Columns.Add("To_Days");
            ds.Tables.Add(dt);
            return ds;
        }

        public void Save()
        {
            base.DBSave();
        }
    }
}
