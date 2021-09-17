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
/// Summary description for DayBookPresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class DayBookPresenter : Presenter
    {
        private IDayBookView objIDayBookView;
        private DayBookModel objDayBookModel;
        private DataSet objDS;

        public DayBookPresenter(IDayBookView dayBookView, bool IsPostBack)
        {
            objIDayBookView = dayBookView;
            objDayBookModel = new DayBookModel(objIDayBookView);

            base.Init(objIDayBookView, objDayBookModel);
            if (!IsPostBack)
            {
                GetData();
            }

         }
        
        public void GetData()
        {
            objDS = objDayBookModel.ReadValues();
            objIDayBookView.BindDayBookGrid = objDS;
        }

	}
}
