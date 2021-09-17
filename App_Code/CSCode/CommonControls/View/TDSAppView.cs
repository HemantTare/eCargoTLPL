using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using ClassLibraryMVP.General;

namespace Raj.EC.ControlsView
{
    public interface ITDSAppView:IView
    {
        bool IsTDSApp { get;set;}
        bool IsLower { get;set;}
        bool IsIgnore { get;set;}
        int DeducteeTypeID { get;set;}
        string DeducteeTypeName { get;}
        DataSet BindDeducteeType { set;}
        string sectionNo { get;set;}
        decimal LowerRate { get;set;}
        int Call_From { get;set;}
        int ID { get;set;}

        //void Enable_Disable_Controls(bool Enalble_Disable);

    }
}
