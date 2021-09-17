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
using Raj.EF.MasterView;
using Raj.EF.MasterModel;


/// <summary>
/// Summary description for VendorTypeSelectionPresenter
/// </summary>
namespace Raj.EF.MasterPresenter
{

    public class VendorTypeSelectionPresenter : Presenter
    {
        private IVendorTypeSelectionView objIVendorTypeSelectionView;
        private VendorTypeSelectionModel objVendorTypeSelectionModel;
        private DataSet objDS;

        public VendorTypeSelectionPresenter(IVendorTypeSelectionView vendorTypeSelectionView, bool isPostBack)
        {
            objIVendorTypeSelectionView = vendorTypeSelectionView;
            objVendorTypeSelectionModel = new VendorTypeSelectionModel(objIVendorTypeSelectionView);
            base.Init(objIVendorTypeSelectionView, objVendorTypeSelectionModel);

            if (!isPostBack)
            {

                FillValues();               
                FillOnKeyNameChanged();
              


            }
        }

        public void FillValues()
        {
            objIVendorTypeSelectionView.BindKeyName = objVendorTypeSelectionModel.GetKeyName();
            
        }

        public void FillOnKeyNameChanged()
        {
          
            objIVendorTypeSelectionView.BindChkListVendorType = objVendorTypeSelectionModel.GetVendorTypeOnKeyNameChanged();
           
        }
        public void Save()
        {

            base.DBSave();
            

        }
	}
}
