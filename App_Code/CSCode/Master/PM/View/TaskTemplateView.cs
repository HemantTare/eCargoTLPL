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
using Raj.EC.ControlsView;

/// <summary>
/// Summary description for TaskTemplateView
/// </summary>
namespace Raj.EF.MasterView
{
    public interface ITaskTemplateView : IView
    {

        // boolean variables

            Boolean Is_Custom { get;set;}
            Boolean Is_Days_Selected { get;set;}

        // Integer Variables 

            int Template_ID { get;set;}

            int Vehicle_Manufacturer_ID { get;set;}
            int Vehicle_Model_ID { get;set;}

            int Repair_Service_Category_ID{ get;set;}
            int Repair_Service_ID { get;set;}
            int To_Be_Worked_At_ID { get;set;}
            int CompanyWorkShop { get;set;} 
            int Vendor_Id { get;set;}
            int Branch_Id { get;set;}
            int Task_Completion_ID { get;set;}
            int Task_Defination_ID { get;set;}            
            int DueOn_Value { get;set;}
            int DueOn_Days { get;set;}
            int Month_Value { get;set;}
            int Alert_Before_Value { get;set;}
            int Alert_Before_Days { get;set;}
            int CustomAlert_On_Value { get;set;}
            int Last_Permormed_On { get;set;} 
            int Schedule_By { get;set;}
            int Vehicle_Id { get;set;}
            int Mode { get;}

        //double Variables

            Decimal Cost { get; set; }

        // String Variables

            string TaskTemplateName {get;set;}
            string Description {get;set;}
            string To_Be_Worked_At_Type { get;set;}                
            string Form_Type  { get;set;}
            string CustomAlertOnDetailsXML { get; }

        // DataSet Declaration { set;}        

            DataTable Bind_ddl_Repair_Service { set;}

            DataTable Bind_ddl_Task_Template { set;}
            DataTable Bind_ddl_Repair_Service_Category { set;}

            DataTable Bind_rblst_Schedule_By { set;}
            DataTable Bind_ddl_Completion { set;} 

        //DataTable Bind_ddl_To_Be_Worked_At{ set;}

            DataTable Bind_ddl_VehicleManufacturer { set;}
            DataTable Bind_ddl_VehicleModel { set;}

        //void Property For DDLSearch Control(string text, string value);

            void SetVendorId(string Vendor_Name, string Vendor_Id);
            void SetLocationId(string Location_Name, string Location_Id);            

      //DateTime

            DateTime Last_Permormed_Date { get;set;}

        //dataset

            DataSet SessiondgCustomAlertOnDetailsGrid { set;get;}
    }
}
