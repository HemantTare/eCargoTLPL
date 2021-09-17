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
    public interface IRegistrationPermitView:IView 
    {
        string VehiclePermitTaxDetailsXML { get;}
        string VehicleTemporaryPermitDetailsXML { get;}

        int PermitTypeId { get;set; }
        int MainSrNo { get;set; }

        string PermitNo { get;set;}
        string PermitDocumentNo { get;set; }
        DateTime PermitValidFrom { get;set;}
        DateTime PermitValidUpTo { get;set;}

        DataTable SessionStateDropDown { get;set;}
        //DataTable SessionStateDropDown_1 { get;set;}

        DataTable Bind_ddl_Permit_Type { set;}
        DataTable Bind_DDLPermitState { set;}

        DataSet SessionPermitTaxDetails { set;}
        DataSet SessionTemparayRegistrationPermitGrid { set;}
    }
}
