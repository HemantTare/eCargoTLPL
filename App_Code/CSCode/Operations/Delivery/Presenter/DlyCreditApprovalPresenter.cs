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
using Raj.EC.OperationView;
using Raj.EC.OperationModel;

namespace Raj.EC.OperationPresenter
{
    public class DlyCreditApprovalPresenter : Presenter
    {
        private IDlyCreditApprovalView objIDlyCreditApprovalView;
        private DlyCreditApprovalModel objDlyCreditApprovalModel;
        private DataSet objDS;

        public DlyCreditApprovalPresenter(IDlyCreditApprovalView DlyCreditApprovalView, bool isPostBack)
        {
            objIDlyCreditApprovalView = DlyCreditApprovalView;
            objDlyCreditApprovalModel = new DlyCreditApprovalModel(objIDlyCreditApprovalView);
            base.Init(objIDlyCreditApprovalView, objDlyCreditApprovalModel);

            //if (!isPostBack)
            //{
            //    initValues();
            //}
        }

        public void FillValues()
        {
            objDS = objDlyCreditApprovalModel.ReadValues();
            if (objDS.Tables[0].Rows.Count > 0)
            {
                DataRow objDR = objDS.Tables[0].Rows[0];

                objIDlyCreditApprovalView.Consignor = objDR["Consignor_Name"].ToString();
                objIDlyCreditApprovalView.Consignee = objDR["Consignee_Name"].ToString();
                objIDlyCreditApprovalView.PaymentMode = objDR["Payment_Type"].ToString();
                objIDlyCreditApprovalView.TotalArticles = objDR["Total_Articles"].ToString();
                objIDlyCreditApprovalView.TotalGCAmount = objDR["Total_GC_Amount"].ToString();
                objIDlyCreditApprovalView.GCID = Util.String2Int(objDR["GC_ID"].ToString());
                objIDlyCreditApprovalView.PDSID = Util.String2Int(objDR["PDS_ID"].ToString());
                objIDlyCreditApprovalView.Status = true;
            }
            else
            {
                objIDlyCreditApprovalView.Consignor = "";
                objIDlyCreditApprovalView.Consignee = "";
                objIDlyCreditApprovalView.PaymentMode = "";
                objIDlyCreditApprovalView.TotalArticles = "";
                objIDlyCreditApprovalView.TotalGCAmount = "";
                objIDlyCreditApprovalView.GCID = 0;
                objIDlyCreditApprovalView.PDSID = 0;
                objIDlyCreditApprovalView.Status = false;
            }
        }

        public void save()
        {
            base.DBSave();
        }
    }
}