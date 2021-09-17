using System;
using ClassLibraryMVP;
using ClassLibraryMVP.General;

/// <summary>
/// Name: Ankit champaneriya
/// date : 20/11/08
/// Summary description for PackingView
/// </summary>
/// 


namespace Raj.EC.MasterView
{
    public interface IPackingView : IView
    {
        String PackingType { get;set;}
    }
}