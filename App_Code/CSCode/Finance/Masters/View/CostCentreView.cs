using System.Data;

using ClassLibraryMVP.General;
/// Author        : Ankit Champaneriya 
/// Created On    : 15/10/2008
/// Description   : This Page is For  Cost centre view details

/// <summary>
/// Summary description for CostCentreView
/// </summary>
/// 

namespace Raj.EC.FinanceView
{
    public interface ICostCentreView : IView
    {
        string  Cost_Centre_ID { get;set;}
        string Cost_Centre_Name { get;set;}
        string   xmlLedgerId { get;}
        DataSet Bind_DDL_Under { set;}
        DataSet Bind_CheckBoxList_Ledger { set;}

    }
}