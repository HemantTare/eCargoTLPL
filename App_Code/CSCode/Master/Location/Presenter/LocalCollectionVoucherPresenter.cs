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
/// Summary description for LocalCollectionVoucherPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class LocalCollectionVoucherPresenter:Presenter 
    {
        private ILocalCollectionVoucherView objILocalCollectionVoucherView;
        private LocalCollectionVoucherModel objLocalCollectionVoucherModel;
        private DataSet objDS;

        public LocalCollectionVoucherPresenter(ILocalCollectionVoucherView localCollectionVoucherView, bool isPostBack)
        {
            objILocalCollectionVoucherView = localCollectionVoucherView;
            objLocalCollectionVoucherModel = new LocalCollectionVoucherModel(objILocalCollectionVoucherView);
            base.Init(objILocalCollectionVoucherView, objLocalCollectionVoucherModel);

            if (!isPostBack)
            {
                
                initValues();
            }
        }
        public void Save()
        {
           
        }
      
        private void initValues()
        {
           
        }
    }
}