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
using Raj.EC.MasterView;
using Raj.EC.MasterModel; 

/// <summary>
/// Summary description for HoldOnReasonPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class HoldOnReasonPresenter : Presenter
    {
        private IHoldOnReasonView objIHoldOnReasonView;
        private HoldOnReasonModel objHoldOnReasonModel;
        private DataSet objDS;
        public HoldOnReasonPresenter(IHoldOnReasonView HoldOnReasonView, bool IsPostback)
        {
            objIHoldOnReasonView = HoldOnReasonView;
            objHoldOnReasonModel = new HoldOnReasonModel(objIHoldOnReasonView);
            base.Init(objIHoldOnReasonView, objHoldOnReasonModel);

            if (!IsPostback)
            {
                initValues();
            }
        }
        private void initValues()
        {
            if (objIHoldOnReasonView.keyID > 0)
            {
                objDS = objHoldOnReasonModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIHoldOnReasonView.HoldOnReason = DR["Hold_On_Reason"].ToString();
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
