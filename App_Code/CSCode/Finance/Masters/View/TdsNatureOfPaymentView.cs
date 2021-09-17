using System;
using System.Data;

/// <summary>
/// Author        : Ankit Champaneriya 
/// Created On    : 16/10/2008
/// Summary description for TdsNatureOfPaymentView
/// </summary>
/// 

namespace Raj.EC.FinanceView
{
    public interface ITdsNatureOfPaymentView : ClassLibraryMVP.General.IView
    {
        DataSet Session_Payment_Details { get;set; }
        DataSet Bind_Payment_Grid { set;}        
        DataSet Nature_Payment { set; }
        string Section { get;set; }
        string Payment_Code { get;set; }
        int Nature_Payment_ID { get;set; }
        DataTable Session_Deductee_Type { get;set; }
        DataSet Session_Nature_Payment { get;set; }
        void ClearVariables();

	}
}
