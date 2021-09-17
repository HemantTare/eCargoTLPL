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
    public interface  IRegistrationFitnessView :IView 
    {
        DateTime RegistrationDate { get;set;}
        string  RegistraionCertificateNo { get;set;}
        int RegistrationStateID { set;get;}
        int RegistraionRtoId { get;set;}
        decimal RegistrationFee { get;set;}
        string  FitnesCertificateNo { get;set; }
        int FitnessRtoId { get;set;}
        DateTime IssueDate { get;set; }
        DateTime ValidUpTO { get;set;}
        decimal Amount { get;set;}

        DataTable Fill_DDL_RegistrationState { set;}
        DataTable Fill_RTO { set;}
        
    }
}
