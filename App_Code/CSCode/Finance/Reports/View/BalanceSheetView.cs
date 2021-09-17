using System;
using System.Data;

using ClassLibraryMVP.General;

/// <summary>
/// Name : Ankit champaneriya
/// Date : 23-10-08
/// Summary description for Balance_Sheet_View
/// </summary>


namespace Raj.EC.FinanceView
{
    public interface IBalanceSheetView : IView
    {
        string CompanyName { set;}
        DateTime Start_Date { get;set; }
        DateTime End_Date { get;set;}
        string Main { get;}
        int Cat_Id { get;}
        Boolean Is_Consol { get;set;}
        String Hierarchy_Code { get;set;}
        int Main_Id { get;set;}
        Boolean Is_Details { get;set;}

        //   DataSet Bind_Balance_Sheet_Grid { set;}

        DataTable BindLiabliltyGrid { set;}
        DataTable BindAssetsGrid { set;}
        DataSet BS_DS { get; set;}
    }
}