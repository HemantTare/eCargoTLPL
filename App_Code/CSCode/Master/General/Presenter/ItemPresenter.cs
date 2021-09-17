
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
/// Summary description for ItemPresenter
/// </summary>
/// 
namespace Raj.EC.MasterPresenter
{
    public class ItemPresenter : Presenter
    {
       private IItemView objIItemView;
        private ItemModel objItemModel;
        private DataSet objDS;

        public ItemPresenter(IItemView ItemView, bool IsPostBack)
        {
            objIItemView = ItemView;
            objItemModel = new ItemModel(objIItemView);

            base.Init(objIItemView, objItemModel);

            if (!IsPostBack)
            {                                   
                initValues();                 
            }
        }

        public void Fill_Values()
        {
            objDS = objItemModel.Fill_Values();

            objIItemView.BindCommodity = objDS.Tables[0];
            


        }

        private void initValues()
        {
             Fill_Values();

            if (objIItemView.keyID > 0)
            {
                ReadValues();
            }
        }

        public  void ReadValues()
        {
            objDS = objItemModel.ReadValues();
            
            if (objDS.Tables[0].Rows.Count > 0)
            {
                objIItemView.ItemName = Convert.ToString(objDS.Tables[0].Rows[0]["Item_Name"]);
                objIItemView.Commodity_Id = Convert.ToInt32   (objDS.Tables[0].Rows[0]["Commodity_Id"]);
                objIItemView.Description = Convert.ToString(objDS.Tables[0].Rows[0]["Description"]);
                objIItemView.ItemRatePerKg = Convert.ToDecimal(objDS.Tables[0].Rows[0]["ItemRatePerKg"]);
            }
        }
        public void save()
        {
            
          //  base.DBSave();
          objItemModel.Save();
        }


         
    }
}






