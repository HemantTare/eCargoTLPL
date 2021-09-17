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
using Raj.CRM.TransactionsView;
using Raj.CRM.TransactionsModel;
namespace Raj.CRM.TransactionsPresenter
{
    public class MergeTicketsPresenter : Presenter
    {
        private IMergeTicketsView objIMergeTicketsView;
        private MergeTicketsModel objMergeTicketsModel;
        private DataSet objDS;
        public MergeTicketsPresenter(IMergeTicketsView MergeTicketsView, bool IsPostBack)
        {
            objIMergeTicketsView = MergeTicketsView;
            objMergeTicketsModel = new MergeTicketsModel(objIMergeTicketsView);
            base.Init(objIMergeTicketsView, objMergeTicketsModel);

            if (!IsPostBack)
            {
                initValues();
            }
        }

        private void initValues()
        {
            fillValues();
        }

        private void fillValues()
        {
            objDS = objMergeTicketsModel.FillValues();
            objIMergeTicketsView.bind_ddl_GcDoc = objDS.Tables[0];
        }        

        public void Save()
        {
            //base.DBSave();
            objMergeTicketsModel.Save();
        }
        
        public void FillFromTicket()
        {
            objDS = objMergeTicketsModel.GetFromTicket();
            objIMergeTicketsView.bind_ddl_FromTicket = objDS.Tables[0];
        }

        public void FillToTicket()
        {
            objDS = objMergeTicketsModel.GetToTicket();
            objIMergeTicketsView.bind_ddl_ToTicket = objDS.Tables[0];
        }
    }
}
