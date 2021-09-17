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

namespace Raj.EF.MasterPresenter
{
   public class TemporaryPermitTaxDetailsPresenter:Presenter 
   {
       private ITemporaryPermitTaxDetailsView objITemporaryPermitTaxDetailsView;
       private TemporaryPermitTaxDetailsModel objTemporaryPermitTaxDetailsModel;
       private DataSet objDS;
       public TemporaryPermitTaxDetailsPresenter(ITemporaryPermitTaxDetailsView TemporaryPermitTaxDetailsView,bool IsPostBack)
       {
           objITemporaryPermitTaxDetailsView = TemporaryPermitTaxDetailsView;
           objTemporaryPermitTaxDetailsModel = new TemporaryPermitTaxDetailsModel(objITemporaryPermitTaxDetailsView);
           base.Init(objITemporaryPermitTaxDetailsView, objTemporaryPermitTaxDetailsModel);
           if (!IsPostBack)
           {
               Fill_State();
           }
       }
       public void Fill_State()
       {
           objDS = objTemporaryPermitTaxDetailsModel.Fill_State();

           objITemporaryPermitTaxDetailsView.SessionStateDropDown = objDS.Tables[1];
          // objITemporaryPermitTaxDetailsView.Fill_DDL_State = objTemporaryPermitTaxDetailsModel.Fill_State();
       }
   }
}
