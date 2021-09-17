// JScript File

function Allow_to_Save()
{
    var ATS = false;

    var lbl_Errors = document.getElementById('wucEmployee1_lbl_Errors');
    var ddl_hierarchy = document.getElementById('wucEmployee1_WucHierarchyWithID1_ddl_hierarchy');
    var ddl_location = document.getElementById('wucEmployee1_WucHierarchyWithID1_ddl_location');
    
    var txt_EmployeeCode = document.getElementById('wucEmployee1_txt_EmployeeCode');
    var txt_FirstName = document.getElementById('wucEmployee1_txt_FirstName');
    var txt_MiddleName = document.getElementById('wucEmployee1_txt_MiddleName');
    var txt_LastName = document.getElementById('wucEmployee1_txt_LastName');
    var ddl_Department = document.getElementById('wucEmployee1_ddl_Department');
    var ddl_UserProfile = document.getElementById('wucEmployee1_ddl_UserProfile');
    var txt_Qualification = document.getElementById('wucEmployee1_txt_Qualification');
    var txt_Email = document.getElementById('wucEmployee1_txt_Email');
    var txt_PreviousJobProfile = document.getElementById('wucEmployee1_txt_PreviousJobProfile');
    //======================================ADDED FOR PAYROLL============================================
    var txt_BasicRate= document.getElementById('wucEmployee1_txt_BasicRate');
    var birth_date = document.getElementById('wucEmployee1_birth_date_Picker_selecteddates');
    var date_Joinning = document.getElementById('wucEmployee1_date_Joinning_Picker_selecteddates');
    var Confirmation_date = document.getElementById('wucEmployee1_Confirmation_date_Picker_selecteddates');

    var chk_Confirmation= document.getElementById('wucEmployee1_chk_Confirmation'); 
    var TabStrip1 = wucEmployee1_TabStrip1;
    var Consider_Payroll_Yes = document.getElementById('wucEmployee1_rdb_Consider_Payroll_Yes'); 
    var date = new Date();

    //==================================================================================
//    var objResource=new Resource('wucEmployee1_hdf_ResourecString');

    lbl_Errors.innerText='';
  

    if (ddl_hierarchy.value <= '0')
    {
        lbl_Errors.innerText = "Please Select Hierarchy";//objResource.GetMsg("Msg_ddl_hierarchy");
        ddl_hierarchy.focus();
    }
    else if (ddl_hierarchy.value != 'HO' && ddl_location.value <= '0')
    {
        lbl_Errors.innerText = "Please Select Location";//objResource.GetMsg("Msg_ddl_location");
        ddl_location.focus();
    }
    else if (Trim(txt_EmployeeCode.value) == ''&& control_is_mandatory(txt_EmployeeCode) == true)
    {
        lbl_Errors.innerText = "Please Enter Employee Code";// objResource.GetMsg("Msg_txt_EmpCode");
        txt_EmployeeCode.focus();
    }
    else if (Trim(txt_FirstName.value) == '')
    {
        lbl_Errors.innerText = "Please Enter First Name"; //objResource.GetMsg("Msg_txt_firstname");
        txt_FirstName.focus();
    }
    else if (Trim(txt_MiddleName.value) == '' && control_is_mandatory(txt_MiddleName) == true)
    {
        lbl_Errors.innerText = "Please Enter Middle Name";// objResource.GetMsg("Msg_txt_middlename");
        txt_MiddleName.focus();
    }
    else if(Trim(txt_LastName.value) == '')
    {
        lbl_Errors.innerText =  "Please Enter Last Name";//objResource.GetMsg("Msg_txt_lastname");
        txt_LastName.focus();
    }
    else if(ddl_Department.value <= '0')
    {
        lbl_Errors.innerText = "Please Select Department";//objResource.GetMsg("Msg_ddl_department");
        ddl_Department.focus();
    }
    else if(ddl_UserProfile.value <= '0')
    {
        lbl_Errors.innerText = "Please Select User Profile";//  objResource.GetMsg("Msg_ddl_userprofile");
        ddl_UserProfile.focus();
    }
    else if (Trim(txt_Qualification.value) == '')
    {
        lbl_Errors.innerText = "Please Enter Qualification";// objResource.GetMsg("Msg_txt_qualification");
        txt_Qualification.focus();
    }
    else if (Trim(txt_Email.value) == '' && control_is_mandatory(txt_Email) == true)  //added by Ankit champaneriya: 02-01-09
    {
        lbl_Errors.innerText =  "Please Enter Email";//  objResource.GetMsg("Msg_txt_email");
        txt_Email.focus();
    }
    else if ((!(txt_Email.value.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) != -1))&&  Trim(txt_Email.value) != '')
    {
        lbl_Errors.innerText = "Please Enter Valid Email";// objResource.GetMsg("Msg_txt_emailvalidation");
        txt_Email.focus();
    }
    else if (Trim(txt_PreviousJobProfile.value) == '' && control_is_mandatory(txt_PreviousJobProfile) == true)
    {
        lbl_Errors.innerText = "Please Enter Previous Job Details";// objResource.GetMsg("Msg_txt_previousjobdetails");
        txt_PreviousJobProfile.focus();
    } 
     //======================================================ADDED FOR PAYROLL====================================
    else if(Consider_Payroll_Yes.checked == true)
    {
        if(birth_date.value >= date_Joinning.value)
            {
                lbl_Errors.innerText = "Joinning Date Must Be Greater Than Birthdate";
                TabStrip1.SelectTabById('one');
                 return false; 
             }
        else if (Trim(txt_BasicRate.value) == "" || Trim(txt_BasicRate.value)=='0')
            {
              lbl_Errors.innerText = "Basic Rate Should Not Be Blank or Zero ";
              TabStrip1.SelectTabById('one'); 
              txt_BasicRate.focus();
               return false;
            }
        else if(date_Joinning.value > Confirmation_date.value && chk_Confirmation.checked == true)
            {
              lbl_Errors.innerText = "Confirmation Date Must Be Greater Than Joining Date";
              TabStrip1.SelectTabById('one'); 
               return false;
            }
        else if(birth_date.value >= date)
            {
               lbl_Errors.innerText = "Birthdate Must Be Less Than Today's Date";
               TabStrip1.SelectTabById('one'); 
               return false;
            }
            else
           { 
            return true;
           }
    }   
         //======================================================ADDED FOR PAYROLL====================================
   
    else
        ATS = true;
        
    return ATS;  
}


 function CheckConfirmation(chk)
{
     var  chk= document.getElementById('WucEmployee1_chk_Confirmation');
     var  pickerdoj= document.getElementById('WucEmployee1_tbl_Confirmation');
   
       if(chk.checked == true)
      {
    
       pickerdoj.style.visibility = 'visible' ;
      
        return true;
      }
     else
      {
         pickerdoj.style.visibility = 'hidden' ;
        return false;
      }
 }
 
 
function check_On_Edit(rdb_payroll)
{
 var rdb_Yes=document.getElementById('WucEmployee1_rdb_Consider_Payroll_Yes');
 var rdb_No=document.getElementById('WucEmployee1_rdb_Consider_Payroll_No');
 var TabStrip1 = document.getElementById('WucEmployee1_TabStrip1'); 
 if(rdb_payroll.value == "rdb_Consider_Payroll_Yes")
 {
     TabStrip1.Tabs('one').SetProperty('Enabled',true); 
     TabStrip1.Render();

 }
 else
 {
      TabStrip1.Tabs('one').SetProperty('Enabled',false); 
      TabStrip1.Render();
 }
}