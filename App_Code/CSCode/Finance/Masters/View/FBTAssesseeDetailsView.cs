using System;
using System.Data;

/// <summary>
/// Author        : Ankit Champaneriya 
/// Created On    : 17/10/2008
/// Summary description for FBTAssesseeDetailsView
/// </summary>
namespace Raj.EC.FinanceView
{
    public interface IFBTAssesseeDetailsView : ClassLibraryMVP.General.IView
    {
        DataSet SessionAssesseeType { set;get;}
        DataSet SessionAssesseeDetails { set;get;}
        int Assessee_Type_Id { get;}
        void ClearVariables();
	}
}
