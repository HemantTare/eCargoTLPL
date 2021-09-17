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
using Raj.EC.ControlsModel;
using Raj.EC.ControlsView;

/// <summary>
/// Summary description for PODSentByPresenter
/// </summary>
/// 
namespace Raj.EC.ControlsPresenter
{
    public class PODSentByPresenter:Presenter
    {

        private IPODSentByView objIPODSentByView;
        private PODSentByModel objPODSentByModel;
        private DataSet objDS;

        public PODSentByPresenter(IPODSentByView PODSentByView, bool isPostBack)
        {
            objIPODSentByView = PODSentByView;
            objPODSentByModel = new PODSentByModel(objIPODSentByView);
            base.Init(objIPODSentByView, objPODSentByModel);

            if (!isPostBack)
            {
                initvalues();
            }

        }


        public void initvalues()
        {
            if (objIPODSentByView.IsDllSentByAlreadyBinded == false)
                objIPODSentByView.Bind_ddl_SentBy = objPODSentByModel.FillSentBy();
            
        }
    }
}
