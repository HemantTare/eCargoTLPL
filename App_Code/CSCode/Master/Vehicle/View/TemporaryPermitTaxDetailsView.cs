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
namespace Raj.EF.MasterView
{
    public interface  ITemporaryPermitTaxDetailsView:IView 
    {
        int State_ID {get;set; }
        string  ReceiptNo{get;set;}
        decimal TaxAmount { get;set;}
        DateTime ValidFrom { get;set;}
        DateTime ValidUpto { get;set;}

      //  DataSet Fill_DDL_State { set;}
        DataTable SessionStateDropDown { get;set;}
      
        DataSet SessionRegistrationGrid { set;}
    }
}