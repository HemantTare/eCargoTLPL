// JScript File
function ValidateUI()
{
   var ATS = false;
   
   var txt_Fuel_Type_Name = document.getElementById('WucFuelType1_txt_Fuel_Type_Name');
   var lbl_Errors=document.getElementById('WucFuelType1_lbl_Errors');
     lbl_Errors.innerText ="";
//  var objResource=new Resource('WucFuelType1_hdf_ResourecString');
  
  if (txt_Fuel_Type_Name.value == '')
  {
      lbl_Errors.innerText = "Please Enter Fuel Type";// objResource.GetMsg("Msg_FuelTypeName");
      txt_Fuel_Type_Name.focus();
  }
  else
      ATS = true;

return ATS;
}

