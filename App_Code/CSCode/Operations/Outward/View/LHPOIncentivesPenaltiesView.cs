using System;
using System.Data;
using ClassLibraryMVP.General;
using Raj.EC.OperationView;
using ClassLibraryMVP;
using ClassLibraryMVP.DataAccess;
/// <summary>
/// Summary description for LHPOIncentivesPenaltiesView
/// </summary>

namespace Raj.EC.OperationView
{
    /// <summary>
    /// Name : Ankit champaneriya
    /// Date : 03-11-08
    /// Description : LHPO Details view
    /// </summary>
    /// 
 
    public interface ILHPOIncentivesPenaltiesView : IView 
    {
        DataTable SessionBindLHPOIncentiveGrid { set;}
        DataTable SessionBindLHPOPenaltiesGrid { set;}
        string  IncentiveDetailsXML {get;}
        string PenaltyDetailsXML { get;}
    }
}