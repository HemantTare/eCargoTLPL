// JScript File



function ValidateUI()
{
var ATS = false;
var lbl_Errors = document.getElementById('WucFreightCopy1_lbl_Errors');
var ddl_City = document.getElementById('WucFreightCopy1_ddl_City');
var ddl_State = document.getElementById('WucFreightCopy1_ddl_State');
var ddl_CopyFromCity = document.getElementById('WucFreightCopy1_ddl_CopyFromCity');
var txt_Rate = document.getElementById('WucFreightCopy1_txt_Rate');

lbl_Errors.innerText='';
 
 if(ddl_City.value == '0')
 {
     lbl_Errors.innerText = "Please Select Copy To City";
     ddl_City.focus();
 }
 else if(ddl_State.value == '0')
 {
     lbl_Errors.innerText = "Please Select To State";
     ddl_State.focus();
 }
 else if(ddl_CopyFromCity.value == '0')
 {
     lbl_Errors.innerText = "Please Select Copy From City";
     ddl_CopyFromCity.focus();
 }
 else if(txt_Rate.value == '')
 {
     lbl_Errors.innerText = "Please Enter Rate to add or subtract";
     txt_Rate.focus();
 }
 else 
 ATS = true;

 return ATS;

}