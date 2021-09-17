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


/// <summary>
/// Summary description for CompanyParametersView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface ICompanyParametersView : IView
    {
        int CompanyId { set;get;}
      bool IsActivateDivision{set;get;}
      bool IsAccTransferRequired{set;get;}
      bool IsActivateCoLoaderBusiness{set;get;}
      bool IsBookOwnTruckHire { set;get;}
      bool IsMarketTruckLedgerAccTruckWise { set;get;}
      bool IsAttachedTruckLedgerAccTruckWise { set;get;}
      bool IsManagedTruckLedgerAccTruckWise { set;get;}
      int StdBasicFreightUnit{set;get;}
      Decimal StdFreightRateForSundry{set;get;}
      DataSet BindStdBasicFreightUnit { set;}
        bool IsPartLoadingRequired { set;get;}
        int MinDiffMEMOandTAS { set;get;}
        bool IsGCNumberEditable { set;get;}
        bool IsContractRequiredForTBBGC { set;get;}


    }
}
