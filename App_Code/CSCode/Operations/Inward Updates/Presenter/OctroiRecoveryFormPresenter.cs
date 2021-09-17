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

/// <summary>
/// Summary description for OctroiRecoveryFormPresenter
/// </summary>
namespace Raj.EC.OperationPresenter
{
    public class OctroiRecoveryFormPresenter : Presenter
    {
        private IOctroiRecoveryFormView objIOctroiRecoveryFormView;
        private OctroiRecoveryFormModel objOctroiRecoveryFormModel;
        private DataSet objDS;

        public OctroiRecoveryFormPresenter(IOctroiRecoveryFormView octroiRecoveryFormView, bool IsPostBack)
        {
            objIOctroiRecoveryFormView = octroiRecoveryFormView;
            objOctroiRecoveryFormModel = new OctroiRecoveryFormModel(objIOctroiRecoveryFormView);
            base.Init(objIOctroiRecoveryFormView, objOctroiRecoveryFormModel);

            if (!IsPostBack)
            {
                
                
                initValues();
            }
        }

        

        public void fillgrid()
        {
            objDS = objOctroiRecoveryFormModel.ReadValues();
            objIOctroiRecoveryFormView.SessionBindOctroiRecoveryFormGrid = objDS.Tables[0];
            if (objDS.Tables[0].Rows.Count > 0)
            {

            objIOctroiRecoveryFormView.BindOctroiRecoveryFormGrid = objIOctroiRecoveryFormView.SessionBindOctroiRecoveryFormGrid;
            }
                
           
        }

        private void initValues()
        {
            
        }
        public void save()
        {
            base.DBSave();
            //objOctroiRecoveryFormModel.Save();
        }
    }
}
