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
/// Summary description for ReceivablePayablePresenter
/// </summary>
namespace Raj.EC.FinancePresenter
{
    public class ReceivablePayablePresenter : Presenter
    {
        private IReceivablePayableView objIReceivablePayableView;
        private ReceivablePayableModel objReceivablePayableModel;
        private DataSet objds;

        public ReceivablePayablePresenter(IReceivablePayableView receivablePayableView, bool IsPostBack)
        {
            objIReceivablePayableView = receivablePayableView;
            objReceivablePayableModel = new ReceivablePayableModel(objIReceivablePayableView);

            base.Init(objIReceivablePayableView, objReceivablePayableModel);

            if (IsPostBack == false)
            {
                  initValues();
               
            }
        }

        public void initValues()
        {

            objds = objReceivablePayableModel.ReadValues();
            objIReceivablePayableView.SessionReceivablePayableGrid = objds;
            objIReceivablePayableView.BindGrid = objds.Tables[0];
            
        }

        public void Save()
        {
            base.DBSave();
        }
      
    }
}
