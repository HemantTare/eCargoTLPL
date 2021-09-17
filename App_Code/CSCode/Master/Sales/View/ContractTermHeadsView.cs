using System;
using System.Data;

using ClassLibraryMVP.General;

/// <summary>
/// Author:		Ankit
/// Create date: 13-10-2008
/// Summary description for ContractTermHeadsView
/// </summary>
/// 
namespace Raj.EC.MasterView
{
    public interface IContractTermHeadsView : IView
    {
        string TermHead { get;set;}
        string Description { get;set;}
    }
}