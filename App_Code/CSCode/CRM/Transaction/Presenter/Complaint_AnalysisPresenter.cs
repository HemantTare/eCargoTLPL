
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
using Raj.CRM.TransactionView;
using Raj.CRM.TransactionModel;
using ClassLibraryMVP;

/// <summary>
/// Summary description for Complaint_AnalysisPresenter
/// </summary>
/// 
namespace Raj.CRM.TransactionPresenter
{
    public class Complaint_AnalysisPresenter : Presenter
    {
        private IComplaint_AnalysisView _Complaint_AnalysisView;
        private Complaint_AnalysisModel _Complaint_AnalysisModel;

        private   DataSet ds_Primary_Group_Ledger = new DataSet();
        private    DataSet ds_Sub_Group_Ledger = new DataSet();
        private DataSet ds_Ledger = new DataSet();
        private DataSet DS_Temp = new DataSet();
        private int Primary_Ledger_Group_Id;

        private String str;
        private String Space;
        private String Add_Space;

        private int Hierarchi_Level;
     
        private void initValues()
        {

            //Get_Details();

        }


        public void Get_Details()
        {

            if (_Complaint_AnalysisView.Ticket_ID > 0)
            {
                DataSet objDS;

                objDS = _Complaint_AnalysisModel.ReadValues();

                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow objDR = objDS.Tables[0].Rows[0];

                    // _Complaint_AnalysisView.Ticket_ID = Convert.ToInt32(objDR["Ticket_Id"].ToString());
                    _Complaint_AnalysisView.Ticket_No = Convert.ToString(objDR["Ticket_No"].ToString());

                    _Complaint_AnalysisView.GC_Docket_Id = Convert.ToInt32(objDR["GC_Docket_Id"].ToString());
                    _Complaint_AnalysisView.GC_Docket_No = Convert.ToString(objDR["GC_Docket_No"].ToString());

                    _Complaint_AnalysisView.Person_Responsible = objDR["Person_Responsible"].ToString();
                    _Complaint_AnalysisView.Action_Taken = objDR["Action_Taken"].ToString();
                }

                objDS = _Complaint_AnalysisModel.Read_Complaint_Analysis_Details();

                _Complaint_AnalysisView.Bind_Complaint_Analysis_Details = objDS;
            }
            else
            {

            }
        }
                
        public Complaint_AnalysisPresenter(IComplaint_AnalysisView Complaint_AnalysisView, bool isPostBack)
        {
            _Complaint_AnalysisView = Complaint_AnalysisView;
            _Complaint_AnalysisModel = new Complaint_AnalysisModel(_Complaint_AnalysisView);
            base.Init(_Complaint_AnalysisView, _Complaint_AnalysisModel);

            if (isPostBack == false)
            {
                 initValues();
            }
        }

        public DataSet BindLHPODetails()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable("Table");
            dt.Columns.Add("Sr_No");
            dt.Columns.Add("From_Days");
            dt.Columns.Add("To_Days");
            ds.Tables.Add(dt);
            return ds;
        }
        

        public DataSet Get_Region_For_Fault()
        {
            return _Complaint_AnalysisModel.Get_Region_For_Fault();
        }

        
        public void Save()
        {
              //base.DBSave();
            _Complaint_AnalysisModel.Save();
        }
    }
}






