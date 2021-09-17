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
/// Summary description for CompanyCaptionView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface ICompanyCaptionView : IView
    {
        string GCCaption { set;get;}
        string LHPOCaption { set;get;}
        bool IsMemoSeriesRequired { set;get;}
        bool IsLHPOSeriesRequired { set;get;}
        bool IsAlsRequired { set;get;}
        bool IsTasRequired { set;get;}
        int MinDiffTASandAUS { set;get;}
	}
}
