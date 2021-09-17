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
/// Summary description for CompanyTDSFBTDetailsView
/// </summary>
namespace Raj.EC.MasterView
{
    public interface ICompanyTDSFBTDetailsView : IView
    {
        string TaxAssessmentNumber{set;get;}
        string IncomeTaxCircle{set;get;}
        string DeductorType{set;get;}
        string Designation { set;get;}
        int PersonResponsible{set;get;}
        bool IsAllowSelectionFBTCategory{set;get;}
        string PanNo{set;get;}
        int AssesseeType{set;get;}
         bool IsSurchargeApplicable{set;get;}
        int AssesseeCategory{set;get;}
        DataTable BindPersonResponsible{set;}
        DataTable BindAssesseeType{set;}
        DataTable BindAssesseeCategory { set;}
        DataTable BindDeducteeType { set;} 

        


	}
}
