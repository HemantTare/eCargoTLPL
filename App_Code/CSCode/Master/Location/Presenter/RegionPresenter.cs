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
/// Summary description for RegionPresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class RegionPresenter : Presenter
    {
        private IRegionView objIRegionView;
        private RegionModel objRegionModel;
        private DataSet objDS;

        public RegionPresenter(IRegionView regionView, bool IsPostBack)
        {
            objIRegionView = regionView;
            objRegionModel = new RegionModel(objIRegionView);

            base.Init(objIRegionView, objRegionModel);

            if (!IsPostBack)
            {
                
                initValues();
            }
        }

        public void FillCountry()
        {
            objIRegionView.BindCountry = objRegionModel.GetCountryValues();
        }
        private void fillValues()
        {
            
            objDS = objRegionModel.FillValues();
            objIRegionView.BindCashLedger = objDS.Tables[0];
            objIRegionView.BindBankLedger = objDS.Tables[1];
           
        }

        private  void initValues()
        {
            FillCountry();
            fillValues();
            if (objIRegionView.keyID > 0)
            {
                objDS = objRegionModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];
                    objIRegionView.RegionCode = DR["Region_Code"].ToString();
                    objIRegionView.RegionName = DR["Region_Name"].ToString();
                    objIRegionView.CountryId = Util.String2Int(DR["Country_Id"].ToString());
                    objIRegionView.CashLedgerId = Util.String2Int(DR["Cash_Ledger_Id"].ToString());
                    objIRegionView.BankLedgerId = Util.String2Int(DR["Bank_Ledger_Id"].ToString());
                }
            }
            
        }
        public void Save()
        {
            base.DBSave();
            //objRegionModel.Save();
        }
    }
}

