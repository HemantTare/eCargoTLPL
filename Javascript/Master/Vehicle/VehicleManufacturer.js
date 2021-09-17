// JScript File
function ValidateUI()
{
   var ATS = false;
   
   var txt_Vehicle_Manufacturer_Name = document.getElementById('WucVehicleManufacturer1_txt_Vehicle_Manufacturer_Name');
  
//  var objResource=new Resource('WucVehicleManufacturer1_hdf_ResourecString');
  var lbl_Errors=document.getElementById('WucVehicleManufacturer1_lbl_Errors');
     lbl_Errors.innerText ="";
  if (txt_Vehicle_Manufacturer_Name.value == '')
  {
      lbl_Errors.innerText = "Please Enter Manufacturer Name";//objResource.GetMsg("Msg_manufacturer_Name");
      txt_Vehicle_Manufacturer_Name.focus();
  }
  else
      ATS = true;

return ATS;
}

