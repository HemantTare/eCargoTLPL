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
using Raj.EC.OperationView; 


/// <summary>
/// Summary description for OctroiRecoveryFormView
/// </summary>
namespace Raj.EC.OperationView
{
    public interface IOctroiRecoveryFormView : IView
    {
        DataTable SessionBindOctroiRecoveryFormGrid { set;get;}
        String OctroiRecoveryFormDetailsXML { get;}
        String GetGCNoXML { get;set;}
        DataTable BindOctroiRecoveryFormGrid { set;}

        void ClearVariables();// added Ankit
	}
}
