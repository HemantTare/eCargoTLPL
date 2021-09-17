using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Raj.EC.ControlsView;
 

/// <summary>
/// Summary description for PetrolPumpGeneralView
/// </summary>
///  

namespace Raj.EC.MasterView
{
    public interface IPetrolPumpGeneralView : ClassLibraryMVP.General.IView
    {      

        String PetrolPumpName { get;set;}
        String MailingName { get;set;}
        String PetrolCompany { get;set;}    
        String ContactPerson { get;set;}
        String CSTNo { get;set;}
        String TINNo { get;set;}


        //common adress control
        IAddressView AddressView { get;}
    }
}
