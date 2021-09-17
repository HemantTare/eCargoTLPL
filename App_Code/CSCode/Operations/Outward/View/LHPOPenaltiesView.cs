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
/// Summary description for LHPOPenaltiesView
/// </summary>
/// 
namespace Raj.EC.OperationView
{    
        public interface ILHPOPenaltiesView : IView
        {

            DataSet Bind_dg_IncentiveDetails { set;}
            DataSet Bind_dg_PenaltyDetails { set;}
            DataSet SessionIncentiveDetailsGrid { set;get;}
            DataSet SessionPenaltyDetailsGrid { set;get;}
        }
    
}