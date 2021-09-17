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
using ClassLibraryMVP;
using Raj.EC.GeneralView;
using Raj.EC.GeneralModel;

/// <summary>
/// Summary description for ReasonPresenter
/// </summary>
namespace Raj.EC.GeneralPresenter
{
    public class  ReasonPresenter : Presenter
    {
        private IReasonView objIReasonView;
        private ReasonModel objReasonModel;
        private DataSet objDS;

        public ReasonPresenter(IReasonView reasonView, bool IsPostBack)
        {
            objIReasonView = reasonView;
            objReasonModel = new ReasonModel(objIReasonView);

            base.Init(objIReasonView, objReasonModel);

            if (!IsPostBack)
            {

                initValues();
            }
        }

        public void FillReasonProcess()
        {
            objIReasonView.BindChkListReasonProcess = objReasonModel.GetProcessValues();   
        }
        private void initValues()
        {
            FillReasonProcess(); 

            if (objIReasonView.keyID >0)
            {

                objDS = objReasonModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIReasonView.Reason = DR["Reason"].ToString();
                    objIReasonView.Description = DR["Description"].ToString();
                    


                }
                objDS = objReasonModel.ReadValues1();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIReasonView.BindChkListReasonProcess = objDS;
                    
                }

            }



        }
        public void Save()
        {
            base.DBSave();
            //objReasonModel.Save();
        }
    }
}
