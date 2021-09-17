using System;
using System.Data;
using ClassLibraryMVP.General;
using ClassLibraryMVP;


/// <summary>
/// Summary description for ShortExcessSettlementView
/// </summary>
/// 

namespace Raj.EC.OperationView
{
    public interface IShortExcessSettlementView:IView
    {
        DataTable SessionShortExcessGrid { set;get;}
        string ShortExcessXML { get;}

        void ClearVariables();
    }
}