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
/// Summary description for BranchRequiredFormsPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class BranchRequiredFormsPresenter : Presenter
    {
        private IBranchRequiredFormsView objIBranchRequiredFormsView;
        private BranchRequiredFormsModel objBranchRequiredFormsModel;
        private DataSet objDS;

        public BranchRequiredFormsPresenter(IBranchRequiredFormsView BranchRequiredFormsView, bool isPostback)
        {
            objIBranchRequiredFormsView = BranchRequiredFormsView;
            objBranchRequiredFormsModel = new BranchRequiredFormsModel(objIBranchRequiredFormsView);
            base.Init(objIBranchRequiredFormsView, objBranchRequiredFormsModel);

            if (!isPostback)
            {
                fillValues();
                initValues();
            }
        }

        public void fillValues()
        {
            objDS = objBranchRequiredFormsModel.FillValues();
            objIBranchRequiredFormsView.BindRequiredForms = objDS.Tables[0];
        }

        private void initValues()
        {
            if (objIBranchRequiredFormsView.keyID > 0)
            {
                
            }
        }
    }
}