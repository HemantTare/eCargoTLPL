using System;
using System.Data;


/// <summary>
/// Author        : Ankit
/// Created On    : 11/10/2008
/// Description   : This Page is For General Weight range
///
/// Summary description for WeightRageView
/// </summary>
/// 

namespace Raj.EC.MasterView
{
    public interface IWeightRageView : ClassLibraryMVP.General.IView
    {
        int From_Unit { get;set;}
        int To_Unit { get;set;}
    }
}