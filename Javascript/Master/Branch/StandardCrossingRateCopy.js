

// JScript File

function ValidateUI()
{
var ATS = false;
var lbl_Errors = document.getElementById('WucStandardCrossingRateCopy1_lbl_Errors');

var ddl_Area = document.getElementById('WucStandardCrossingRateCopy1_ddl_Area');
var ddl_Branch=document.getElementById('WucStandardCrossingRateCopy1_ddl_Branch');
var ddl_CopyFromBranch=document.getElementById('WucStandardCrossingRateCopy1_ddl_CopyFromBranch');
var txt_HireRate = document.getElementById('WucStandardCrossingRateCopy1_txt_HireRate');
var txt_Hamali = document.getElementById('WucStandardCrossingRateCopy1_txt_Hamali');



lbl_Errors.innerText='';
 
 if(ddl_Branch.value == '0')
 {
     lbl_Errors.innerText = "Please select Copy To Branch";
     ddl_Branch.focus();
 }
 else if(ddl_Area.value == '0')
 {
     lbl_Errors.innerText = "Please select To Area";
     ddl_Area.focus();
 }
 else if(ddl_CopyFromBranch.value == '0')
 {
     lbl_Errors.innerText = "Please Select Copy From Branch";
     ddl_CopyFromBranch.focus();
 }
 else if(Trim(txt_Hamali.value) == '')
 {
     lbl_Errors.innerText = "Please Enter Hamali Rate";
     txt_Hamali.focus();
 }
 else if(Trim(txt_HireRate.value) == '')
 {
     lbl_Errors.innerText = "Please Enter Hire Rate";
     txt_HireRate.focus();
 }
 else 
 ATS = true;

 return ATS;

}