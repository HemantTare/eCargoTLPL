using System;
using System.Data;

/// <summary>
/// Author        : Ankit Champaneriya 
/// Created On    : 17/10/2008
/// Summary description for TDSDeducteeTypeView
/// </summary>
namespace Raj.EC.FinanceView
{
    public interface ITDSDeducteeTypeView : ClassLibraryMVP.General.IView
    {
        DataSet Session_Deductee_Details { get;set; }
        DataSet Bind_Deductee_Grid { set;}
        DataTable Deductee_Type { set; }
        DataTable Session_Deductee_Type { get;set;  }
        int Deductee_Type_ID { get;set; }
        int Resedential_Status { get;set; }
        int Deductee_Status { get;set;  }
        void ClearVariables();

	}
}
