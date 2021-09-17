using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP;
using ClassLibraryMVP.General;
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for ClientPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class ClientPresenter : Presenter
    {
        private IClientView objIClientView;
        private ClientModel objClientModel;

        public ClientPresenter(IClientView ClientView, bool isPostback)
        {
            objIClientView = ClientView;
            objClientModel = new ClientModel(objIClientView);
            base.Init(objIClientView, objClientModel);                     
        }
              
        public void Save()
        {
            base.DBSave();
            //objClientModel.Save();
        }
    }
}

