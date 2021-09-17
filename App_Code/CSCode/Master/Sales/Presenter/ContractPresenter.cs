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
/// Summary description for ContractPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{

    public class ContractPresenter : Presenter
    {
        private IContractView objIContractView;
        private ContractModel objContractModel;

        public ContractPresenter(IContractView contractView, bool isPostback)
        {
            objIContractView = contractView;
            objContractModel = new ContractModel(objIContractView);
            base.Init(objIContractView, objContractModel);
        }

        public void Save()
        {
            base.DBSave();
            //objClientModel.Save();
        }
    }
}