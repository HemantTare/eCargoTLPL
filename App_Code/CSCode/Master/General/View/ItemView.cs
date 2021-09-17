
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


using Raj.EC.MasterView;
 

 
/// <summary>
/// Summary description for ItemView
/// </summary>

namespace Raj.EC.MasterView
{
    public interface IItemView : ClassLibraryMVP.General.IView
    {
        int Commodity_Id { get;set;}
        int ItemId { get;set;}
        

        String ItemName { get;set;}
        String Description { get;set;}
        decimal ItemRatePerKg { get;set;}


        DataTable BindCommodity { set;}
        
    }
}