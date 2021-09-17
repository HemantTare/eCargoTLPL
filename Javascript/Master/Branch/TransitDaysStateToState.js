// JScript File

function ValidateUI()
{
var ATS = false;
var lbl_Errors = document.getElementById('WucTransitDaysStateToState1_lbl_Errors');

var ddl_FromState = document.getElementById('WucTransitDaysStateToState1_ddl_FromState');
var ddl_ToState = document.getElementById('WucTransitDaysStateToState1_ddl_ToState');
var ddl_VehicleType = document.getElementById('WucTransitDaysStateToState1_ddl_VehicleType');
var txt_TransitDays = document.getElementById('WucTransitDaysStateToState1_txt_TransitDays');
var txt_DistanceInKM = document.getElementById('WucTransitDaysStateToState1_txt_DistanceInKM');



lbl_Errors.innerText='';
 
 if(ddl_FromState.value == '0')
 {
     lbl_Errors.innerText = "Please Select From State";
     ddl_FromState.focus();
 }
 else if(ddl_ToState.value == '0')
 {
     lbl_Errors.innerText = "Please Select To State";
     ddl_ToState.focus();
 }
 else if(ddl_VehicleType.value == '0')
 {
     lbl_Errors.innerText = "Please Select Vehicle Type";
     ddl_VehicleType.focus();
 }
 else if(Trim(txt_TransitDays.value) == '')
 {
     lbl_Errors.innerText = "Please Enter Transit Days";
     txt_TransitDays.focus();
 }
 else if(Trim(txt_DistanceInKM.value) == '')
 {
     lbl_Errors.innerText = "Please Enter Distance In KM";
     txt_DistanceInKM.focus();
 }
 else 
 ATS = true;

 return ATS;

}