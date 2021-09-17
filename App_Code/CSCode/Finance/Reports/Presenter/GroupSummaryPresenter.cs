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
/// Summary description for GroupSummaryPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class GroupSummaryPresenter : Presenter
    {
        private IGroupSummaryView objIGroupSummaryView;
        private GroupSummaryModel objGroupSummaryModel;
        private DataSet objDS;

        public GroupSummaryPresenter(IGroupSummaryView groupSummaryView, bool IsPostBack)
        {
            objIGroupSummaryView = groupSummaryView;
            objGroupSummaryModel = new GroupSummaryModel(objIGroupSummaryView);

            base.Init(objIGroupSummaryView, objGroupSummaryModel);
            if (!IsPostBack)
            {
                //objIGroupSummaryView.CompanyName = "Reach Cargo";
                initValues();
            }

        }

        public void initValues()
        {

            objDS = objGroupSummaryModel.ReadValues();
            objIGroupSummaryView.BindGroupSummaryGrid = objDS;

        }
        
	}
}
