
using System;
using System.Data;
using ClassLibraryMVP.General;

namespace Raj.EC.OperationView
{
    /// <summary>
    /// Name : Ankit champaneriya
    /// Date : 25-10-08
    /// Description : Excess Details view
    /// </summary>
    /// 
    public interface IExcessDetailsView : IView
    {
        int Total { get;set;}
        DataTable SessionBindPackingTypeDropdown { set;}
        DataTable SessionBindCommodityDropdown { set;}
        DataTable SessionBindItemDropdown { set;}

        DataTable SessionBindExcessGrid { set;}
        
    }
}

