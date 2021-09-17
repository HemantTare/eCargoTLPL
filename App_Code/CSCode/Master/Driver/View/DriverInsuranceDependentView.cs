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
/// Summary description for Driver InsuranceDependentDetails View
/// </summary>
/// 

namespace Raj.EF.MasterView
{
    public interface IDriverInsuranceDependentView : IView
    {
        //string
        string NomineeName { set;get;}
        string PolicyNumber { set;get;}
        string Driver_Dependent_Details { get;}

        //int
        int InsuranceCompanyID {set;get;}
        int InsuranceBranchID {set;get;}
        int InsuranceAgentID {set;get;}
        int NomineeRelationID { set;get;}

        //decimal
        decimal InsurancePremium { set;get;}
        decimal SumAssured { set;get;}

        //datetime
        DateTime InsuranceExpiryDate { set;get;}

        //datatable
        DataTable BindInsuranceCompany { set;}
        DataTable BindInsuranceAgent { set;}
        DataTable BindNomineeRelation { set;}
        DataTable SessionDepRelationDropDown { set;get;}

        //dataset
        DataSet BindInsuranceBranch { set;}
        DataSet SessionDependentDetailsGrid { set;get;}
    }
}