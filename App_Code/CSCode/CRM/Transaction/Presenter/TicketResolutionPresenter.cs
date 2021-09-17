using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Raj.CRM.TransactionView;
using Raj.CRM.TransactionModel;
using ClassLibraryMVP;
using ClassLibraryMVP.General;


/// <summary>
/// Summary description for TicketResolutionPresenter
/// </summary>
namespace Raj.CRM.TransactionPresenter
{

    public class TicketResolutionPresenter : ClassLibraryMVP.General.Presenter
    {

        private ITicketResolutionView objITicketResolutionView;
        private TicketResolutionModel objTicketResolutionModel;
        DataSet objDS;  

        public TicketResolutionPresenter(ITicketResolutionView ticketResolutionView, bool IsPostBack)
        {
            objITicketResolutionView = ticketResolutionView;
            objTicketResolutionModel = new TicketResolutionModel(objITicketResolutionView);

            base.Init(objITicketResolutionView, objTicketResolutionModel);

            if (!IsPostBack)
            {

                initValues();
                ReadValues();
            }
        }

        public void initValues()
        {

            objDS = objTicketResolutionModel.FillLabelValues();            
            objITicketResolutionView.TicketNo=objDS.Tables[0].Rows[0]["Ticket_No"].ToString();
            objITicketResolutionView.GCDocketNoId = Util.String2Int(objDS.Tables[0].Rows[0]["GCDocketNo"].ToString());

        }

        public void ReadValues()
        {
            objDS = objTicketResolutionModel.ReadValues();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow DR = objDS.Tables[0].Rows[0];                
                objITicketResolutionView.HowResolved = DR["How_Resolved"].ToString();
                if (Util.String2Bool(DR["Whether_Customer_Satisfied"].ToString()))
                {
                    objITicketResolutionView.WhetherCustomerSatisfied = 1;
                    EnableControl();
                }
                else
                {
                    objITicketResolutionView.WhetherCustomerSatisfied = 0;
                    EnableControl();
                }
                objITicketResolutionView.Reason = DR["Reason"].ToString();
                ReadControl();
            }
            else
            {
                VisibleControls();
            }

        }

        public void save()
        {
            objTicketResolutionModel.Save();
            //base.DBSave();

        }

        private bool IsVisible = true;
        public void VisibleControls()
        {
           
            objITicketResolutionView.Save = IsVisible;
            

        }
        private bool ReadOnly = true;
        public void ReadControl()
        {
            objITicketResolutionView.Reason_1 = ReadOnly;
            objITicketResolutionView.HowResolve_1 = ReadOnly;
           
        }

        private bool IsEnabled = false;
        public void EnableControl()
        {
            objITicketResolutionView.CustomerSatisfied = IsEnabled;
        }
       
       
    }
}

