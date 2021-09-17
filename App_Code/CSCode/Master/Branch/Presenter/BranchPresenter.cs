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
using Raj.EC.MasterView;
using Raj.EC.MasterModel;

/// <summary>
/// Summary description for BranchPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class BranchPresenter : Presenter
    {
        private IBranchView objIBranchView;
        private BranchModel objBranchModel;

        public BranchPresenter(IBranchView BranchView, bool isPostback)
        {
            objIBranchView = BranchView;
            objBranchModel = new BranchModel(objIBranchView);
            base.Init(objIBranchView, objBranchModel);         
        }
             

        public void Save()
        {
            base.DBSave();
            //objBranchModel.Save();
        }
    }
}
