// JScript File

function ValidateUI()
{
var ATS = false;
var lbl_Errors = document.getElementById('WucFreightBranchCopy1_lbl_Errors');

var ddl_Area = document.getElementById('WucFreightBranchCopy1_ddl_Area');
var ddl_Branch=document.getElementById('WucFreightBranchCopy1_ddl_Branch');
var ddl_CopyFromBranch=document.getElementById('WucFreightBranchCopy1_ddl_CopyFromBranch');
var txt_Rate=document.getElementById('WucFreightBranchCopy1_txt_FreightRate');
var hdn_Mode=document.getElementById('WucFreightBranchCopy1_hdn_Mode');
var ddl_Commodity=document.getElementById('WucFreightBranchCopy1_ddl_Commodity');


 
lbl_Errors.innerText='';
 
 if(ddl_Branch.value == '0')
 {
     lbl_Errors.innerText = "Please Select Copy To Branch";
     ddl_Branch.focus();
 }
 else if(ddl_Area.value == '0')
 {
     lbl_Errors.innerText = "Please Select To Area";
     ddl_Area.focus();
 }
 else if(ddl_CopyFromBranch.value == '0')
 {
     lbl_Errors.innerText = "Please Select Copy From Branch";
     ddl_CopyFromBranch.focus();
 }
 else if(Trim(txt_Rate.value) == '')
 {
     lbl_Errors.innerText = "Please Enter Rate To add or subtract";
     txt_Rate.focus();
 }
else if(hdn_Mode.value == "2" && ddl_Commodity.value == '0')
 {
     lbl_Errors.innerText = "Please Select Commodity";
     ddl_Commodity.focus();
 }
 else 
 ATS = true;

 return ATS;

}