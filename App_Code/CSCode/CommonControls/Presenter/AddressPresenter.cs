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
using Raj.EC.ControlsModel;
using Raj.EC.ControlsView;
namespace Raj.EC.ControlsPresenter
{
     public class AddressPresenter
     {
         private IAddressView objIAddressView;
         private AddressModel objAddressModel;
         private DataSet objDS;

         public AddressPresenter(IAddressView addressView, bool isPostBack)
         {
             objIAddressView = addressView;
             objAddressModel = new AddressModel(objIAddressView);
             if (!isPostBack)
             {
                 initControl();
             }
         }
         private void initControl()
         {
             if (objIAddressView.KeyId > 0)
             {
                 objDS = objAddressModel.FillValues();
                 if (objDS.Tables[0].Rows.Count != 0)
                 {
                     objIAddressView.SetCityId(objDS.Tables[0].Rows[0]["City_Name"].ToString(), objIAddressView.KeyId.ToString());
                     objIAddressView.SetLables = objDS.Tables[0];
                 } 
               
                  
             }
         }

         public void FillLables()
         {
             objDS = objAddressModel.FillOnCityChanged();
             objIAddressView.SetLables = objDS.Tables[0];
         }

     }
 }

