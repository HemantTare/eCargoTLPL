using System;
using System.Data;
using ClassLibraryMVP.General;

/// <summary>
/// Summary description for ClientGroupView
/// </summary>
namespace Raj.EC.SalesView
{
    public interface IClientGroupView : IView
    {
        string ClientGroupName { set;get;}
        int ParentGroupId { set;get;}
        int LedgerGroupId { set;get;}
        bool LedgerGroupForRadio { set;get;}

        DataSet BindParentGroup { set;}
        DataSet BindLedgerGroup { set;}
	}
}
