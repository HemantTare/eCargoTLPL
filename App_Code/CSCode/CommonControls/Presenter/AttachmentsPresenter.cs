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
/// Summary description for AttachmentsPresenter
/// </summary>
/// 


namespace Raj.EC.ControlsPresenter
{
    public class AttachmentsPresenter
    {
        private IAttachmentsView objIAttachmentsView;
        private AttachmentsModel objAttachmentsModel;

        DataSet objDS = new DataSet();

        public AttachmentsPresenter(IAttachmentsView AttachmentsView, bool isPostBack)
        {
            objIAttachmentsView = AttachmentsView;
            objAttachmentsModel = new AttachmentsModel(objIAttachmentsView);

            if (isPostBack == false)
            {
                objIAttachmentsView.SessionAttachmentsGrid = objAttachmentsModel.GetValuesForImageDatagrid();
            }
        }
    }
}
