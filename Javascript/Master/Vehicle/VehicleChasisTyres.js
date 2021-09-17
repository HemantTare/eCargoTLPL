// JScript File

function ValidateUI_VehicleChasisTyre()
{
var ATS = false;
var lbl_Errors = document.getElementById('WucVehicle1_WucVehicleChasisTyres1_lbl_Errors');
var txt_no_of_stephaney = document.getElementById('WucVehicle1_WucVehicleChasisTyres1_txt_no_of_stephaney');
var hdn_old_no_of_stephaney = document.getElementById('WucVehicle1_WucVehicleChasisTyres1_hdn_old_no_of_stephaney');

var no_of_stephaney = txt_no_of_stephaney.value;
var old_no_of_stephaney = hdn_old_no_of_stephaney.value;

if (isNaN(no_of_stephaney)) no_of_stephaney = 0;
if (isNaN(old_no_of_stephaney)) old_no_of_stephaney = 0;

if (parseFloat(no_of_stephaney) > 5)
  {
  lbl_Errors.innerHTML = "No. Of Stephaney(s) Must Less Than Or Equal To 5";
  txt_no_of_stephaney.focus();
  }
else if (parseFloat(no_of_stephaney) < parseFloat(old_no_of_stephaney))
  {
  lbl_Errors.innerHTML = "Please Enter No. of Stephaney(s) greater than or equal to " + old_no_of_stephaney;
  txt_no_of_stephaney.focus();
  }
else
  ATS = true;

return ATS;
}
