
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
using Raj.EC.MasterView;
using Raj.EC.MasterModel;
using ClassLibraryMVP;

/// <summary>
/// Summary description for CommodityPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{
    public class CommodityPresenter : Presenter
    {
       private ICommodityView objICommodityView;
        private CommodityModel objCommodityModel;
        private DataSet objDS;

        public CommodityPresenter(ICommodityView CommodityView, bool IsPostBack)
        {
            objICommodityView = CommodityView;
            objCommodityModel = new CommodityModel(objICommodityView);

            base.Init(objICommodityView, objCommodityModel);

            if (!IsPostBack)
            {                                   
                initValues();                 
            }
        }

        public void Fill_Values()
        {
            objDS = objCommodityModel.Fill_Values();

            objICommodityView.BindCommodityType = objDS.Tables[0];
            


        }

        private void initValues()
        {
             Fill_Values();

            if (objICommodityView.keyID > 0)
            {
                ReadValues();
            }
        }

        public  void ReadValues()
        {
            objDS = objCommodityModel.ReadValues();
            
            if (objDS.Tables[0].Rows.Count > 0)
            {
                objICommodityView.CommodityName = Convert.ToString(objDS.Tables[0].Rows[0]["Commodity_Name"]);


                objICommodityView.Is_Restricted = Convert.ToBoolean(objDS.Tables[0].Rows[0]["Is_Restricted"]);
                objICommodityView.Is_Service_Tax_Applicable  = Convert.ToBoolean(objDS.Tables[0].Rows[0]["Is_Service_Tax_Applicable"]);
                objICommodityView.Is_CST_Applicable  = Convert.ToBoolean(objDS.Tables[0].Rows[0]["Is_CST_Applicable"]);
                objICommodityView.Is_Perishable = Convert.ToBoolean(objDS.Tables[0].Rows[0]["Is_Perishable"]);
                objICommodityView.Commodity_Type_Id = Convert.ToInt32  (objDS.Tables[0].Rows[0]["Commodity_Type_ID"]);
                 
            }
        }
        public void save()
        {
            
           // base.DBSave();
           objCommodityModel.Save();
        }


         
    }
}






