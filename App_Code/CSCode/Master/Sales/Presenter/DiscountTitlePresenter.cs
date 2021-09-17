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
/// Summary description for DiscountTitlePresenter
/// </summary>
namespace Raj.EC.MasterPresenter
{
    public class DiscountTitlePresenter : Presenter
    {
        private IDiscountTitleView objIDiscountTitleView;
        private DiscountTitleModel objDiscountTitleModel;
        private DataSet objDS;
        private DataTable objDT;

        public DiscountTitlePresenter(IDiscountTitleView DiscountTitleView, bool IsPostBack)
        {
            objIDiscountTitleView = DiscountTitleView;
            objDiscountTitleModel = new DiscountTitleModel(objIDiscountTitleView);

            base.Init(objIDiscountTitleView, objDiscountTitleModel);

            if (!IsPostBack)
            {   
                initValues();
            }
        } 

        private void initValues()
        {
            if (objIDiscountTitleView.keyID > 0)
            {
                objDS= objDiscountTitleModel.ReadValues();
                if (objDS.Tables[0].Rows.Count > 0)
                {
                    DataRow DR = objDS.Tables[0].Rows[0];

                    objIDiscountTitleView.DiscountTitleName = DR["DiscountTitle"].ToString();
                    objIDiscountTitleView.Remarks = DR["Remarks"].ToString();  
                   
                } 
            } 
        }
        public void Save()
        {
            base.DBSave();
            //objDiscountTitleModel.Save();
        }
    }
}


