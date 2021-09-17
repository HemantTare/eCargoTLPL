
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
/// Summary description for CommodityView
/// </summary>

namespace Raj.EC.MasterView
{
    public interface ICommodityView : ClassLibraryMVP.General.IView
    {
        int Commodity_Type_Id { get;set;}

        int CommodityId { get;set;}

        String CommodityName { get;set;}
        Boolean Is_Restricted { get;set;}
        Boolean Is_Perishable { get;set;}
        Boolean Is_Service_Tax_Applicable { get;set;}
        Boolean Is_CST_Applicable { get;set;}

        DataTable BindCommodityType { set;}
        
    }
}