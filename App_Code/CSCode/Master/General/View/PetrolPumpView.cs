using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// Summary description for PetrolPumpView
/// </summary>
///  

namespace Raj.EC.MasterView
{
    public interface IPetrolPumpView : ClassLibraryMVP.General.IView
    {
        IPetrolPumpGeneralView PetrolPumpGeneralView { get;}
        IPetrolPumpFinanceDetailsView PetrolPumpFinanceDetailsView { get;}
    }
}
