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
/// Summary description for StatePresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class StatePresenter : Presenter
    {
        private IStateView objIStateView;
        private StateModel objStateModel;
        private DataSet objDS;
        private DataTable objDT;

        public StatePresenter(IStateView stateView, bool IsPostBack)
        {
            objIStateView = stateView;
            objStateModel = new StateModel(objIStateView);

            base.Init(objIStateView, objStateModel);

            if (!IsPostBack)
            {
                
                FillFormType();
                initValues();
            }
        }

        public void FillRegion()
        {
            objIStateView.BindRegion = objStateModel.GetRegionValues();
        }
        public void FillFormType()
        {
            objIStateView.BindChkListFormType = objStateModel.GetFormType();
        }
        public void FillCountryLabel()
        {
            objDS = objStateModel.GetCountryNameOnRegionSelection();
            if (objDS.Tables[0].Rows.Count > 0)
            {
              objIStateView.CountryName = objDS.Tables[0].Rows[0]["Country_Name"].ToString();
            }
        }


        private void initValues()
        {
            FillRegion();
            
            if (objIStateView.keyID > 0)
            {
                
                objDS= objStateModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIStateView.RegionId = Util.String2Int(DR["Region_Id"].ToString());
                    objIStateView.StateName = DR["State_Name"].ToString();
                    objIStateView.NsdlCode = DR["NSDL_Code"].ToString();
                    objIStateView.StateCode  = DR["State_Code"].ToString();
                    objIStateView.CountryName = DR["Country_Name"].ToString();
                   
                   
                }
                objDS = objStateModel.ReadValues1();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    objIStateView.BindChkListFormType = objDS;
                }
                
            }
            
            

        }
        public void Save()
        {
            base.DBSave();
            //objStateModel.Save();
        }
    }
}


