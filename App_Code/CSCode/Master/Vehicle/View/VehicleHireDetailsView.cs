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
/// Summary description for VehicleHireDetailsView
/// </summary>
/// 

namespace Raj.EF.MasterView
{
    public interface IVehicleHireDetailsView : IView
    {
        string MaintainedBy_Caption { set;get;}

        int HireTypeID { set;get;}
        int RegionID { get;}
        int AreaID { get;}
        int BranchID { get;}

        int MaintainedID { set;get;}
        int MaintainedByID { set;get;}

        decimal HireAmount { get;set;}

        bool MultipleTripADay { set;get;}

        DataTable Bind_ddl_HireType { set; }
        DataTable Bind_ddl_MaintainedBy { set; }        
    }
}