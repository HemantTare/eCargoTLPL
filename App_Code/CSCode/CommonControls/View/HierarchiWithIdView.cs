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
/// Summary description for HierarchiWithIdView
/// </summary>

namespace Raj.EC.ControlsView
{


    public interface IHierarchiWithIdView
    {

        int RegionID { get;set;}
        int AreaID { get;set;}
        int BranchID { get;set;}
        int MainId { get;set;}

        Boolean Is_Ho { get;set;}
        String HierarchyCode { set; get;}
        //String validateHierarchyWithIdUI(string msg);

    }
}