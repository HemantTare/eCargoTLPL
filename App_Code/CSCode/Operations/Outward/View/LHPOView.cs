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
/// Summary description for LHPOView
/// </summary>
/// 
namespace Raj.EC.OperationView
{
    public interface ILHPOView : IView
    {
        ILHPOHireDetailsView LHPOHireDetailsView { get;}
        ILHPOAlertsBranchesView LHPOAlertsBranchesView { get;}
        ILHPOIncentivesPenaltiesView LHPOIncentivesPenaltiesView { get;}
        ILHPOAttachedBranchView LHPOAttachedBranchView { get;}
        string Flag { get;}
        void ClearVariables();
        DataSet SessionATHDetailsGrid { get;}
    }
}
