
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
 using Raj.EC.ControlsView;

 
/// <summary>
/// Summary description for EmployeeView
/// </summary>

namespace Raj.EC.MasterView
{
    public interface IEmployeeView : ClassLibraryMVP.General.IView
    {
        int Department_Id { get;set;}
        int UserProfile_Id { get;set;}

        int Branch_Id { get;set;}
        int Area_Id { get;set;}
        int Region_Id { get;set;}
         

        Boolean Is_eCargoUser { get;set;}
        Boolean Is_HO { get;set;}

        String HierarchyCode { get;} 
        String EmployeeCode { get;set;} 
        String FirstName { get;set;}
        String MiddleName { get;set;}
        String LastName { get;set;}

        String Qualification { get;set;}
        String Email { get;set;}

        String PreviousJobProfile { get;set;}

        String Applicable_Divisions_Details_Xml { get;}

        DataSet Session_Applicable_Divisions_Details { get;set;}
         

        DataTable BindDepartment { set;}
        DataTable BindUserProfile { set;}
        DataTable BindDivision { set;}       

        //common adress control   
        IHierarchiWithIdView HierarchiWithIdView  { get;}
        Boolean Is_Active { get;set;}
        Boolean Is_Account_Locked { get;set;}
        //======================================ADDED FOR PAYROLL on 07-03-09 by Tausif ===============================================
        bool Consider_Payroll_Yes { get;set;}
        bool Consider_Payroll_Not { set; }
        DateTime Birth_Date { get;set;}
        DateTime DOJ { get;set;}
        decimal Basic_Rate { get;set;}
        int Probation_Period { get;set;}
        DateTime Confirmation_Date { get;set;}
        int Weekly_Off { get;set;}
        //==========================================================================================================

    }
}