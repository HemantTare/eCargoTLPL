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
using Raj.EC.SalesView;
using Raj.EC.SalesModel;
/// <summary>
/// Summary description for CustomiseClientPresenter
/// </summary>
/// 
namespace Raj.EC.SalesPresenter
{
    public class CustomiseClientPresenter: Presenter
    {
        private ICustomiseClientView objICustomiseClientView;
        private CustomiseClientModel objCustomiseClientModel;
        private DataSet objDS;

        public CustomiseClientPresenter(ICustomiseClientView CustomiseClientView, bool IsPostBack)
        {
            objICustomiseClientView = CustomiseClientView;
            objCustomiseClientModel = new CustomiseClientModel(objICustomiseClientView);

            base.Init(objICustomiseClientView, objCustomiseClientModel);
        }
        
        public void Save()
        {
            base.DBSave();
        }
    }
}