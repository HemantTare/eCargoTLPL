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
/// Summary description for ClientBillingPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class ClientBillingPresenter : Presenter
    {
        private IClientBillingView objIClientBillingView;
        private ClientBillingModel objClientBillingModel;
        private DataSet objDS;

        public ClientBillingPresenter(IClientBillingView ClientBillingView, bool isPostback)
        {
            objIClientBillingView = ClientBillingView;
            objClientBillingModel = new ClientBillingModel(objIClientBillingView);
            base.Init(objIClientBillingView, objClientBillingModel);

            if (!isPostback)
            {
                initValues();
            }
        }

        private void fillValues()
        {
            objDS = objClientBillingModel.FillValues();
            objIClientBillingView.BindPaymentMode = objDS.Tables[0];
            objIClientBillingView.BindBillingCycle = objDS.Tables[1];
            objIClientBillingView.BindBillingDays = objDS.Tables[2];
        }
       
        private void initValues()
        {
            fillValues();

            objDS = objClientBillingModel.ReadValues();
            objIClientBillingView.SessionBindBillingGrid = objDS.Tables[1];

            if (objIClientBillingView.keyID > 0)
            {
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];
                    objIClientBillingView.Is_To_Pay = Util.String2Bool(objDR["Is_To_Pay_Allowed"].ToString());
                    //////objIClientBillingView.Is_Paid = Util.String2Bool(objDR["Is_Paid_Allowed"].ToString());
                    //////objIClientBillingView.Is_To_Be_Billed = Util.String2Bool(objDR["Is_To_Be_Billed_Docket_Allowed"].ToString());
                    //////objIClientBillingView.Is_FOC = Util.String2Bool(objDR["Is_FOC_Allowed"].ToString());
                    objIClientBillingView.BillingCycle = Util.String2Int(objDR["Billing_Cycle_ID"].ToString());
                    objIClientBillingView.BillingDays = Util.String2Int(objDR["Billing_Day_ID"].ToString());
                    objIClientBillingView.BillingCycleDay1 = Util.String2Int(objDR["Billing_Cycle_Day1"].ToString());
                    objIClientBillingView.BillingCycleDay2 = Util.String2Int(objDR["Billing_Cycle_Day2"].ToString());
                }
            }
        }
    }
}