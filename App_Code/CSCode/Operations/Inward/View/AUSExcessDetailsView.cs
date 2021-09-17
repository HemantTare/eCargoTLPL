
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
    public interface IAUSExcessDetailsView : IView
    {
        int TotalExcessArticles { get;set;}
        int CommodityId { get;set;}

        String Excess_Details_Xml { get;}

        DataTable SessionBindPackingTypeDropdown { set;}
        DataTable SessionBindCommodityDropdown { set;}
        DataTable SessionBindItemDropdown { set;}

        DataTable SessionBindExcessGrid { set;}
        
    }
}

