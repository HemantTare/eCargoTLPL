using System;
using System.Data;

/// <summary>
/// Author        : Ankit Champaneriya 
/// Created On    : 17/10/2008
/// Summary description for FBTCategoriesView
/// </summary>
/// 

namespace Raj.EC.FinanceView
{
    public interface IFBTCategoriesView : ClassLibraryMVP.General.IView
    {
        int FBTCategoryId { get;set;}
        string FBTSection { get;set;}

        DataTable BindFBTCateroies { set;}

        DataSet BindCategoryDetailsGrid { set;}
        DataSet SessionCategoryDetailsGrid { get;set;}
        DataTable SessionFBTAssesseeCategory { get;set;}
        DataTable BindAssesseCategory { set;}
        void ClearVariables();

    }
}
