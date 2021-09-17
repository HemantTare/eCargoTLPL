// JScript File

function ValidateUI()
{
   var ATS = false;
   var lbl_Errors=document.getElementById('WucVehicleModel1_lbl_Errors');
   var txt_Vehicle_Model_Name = document.getElementById('WucVehicleModel1_txt_Vehicle_Model_Name');
   var ddl_Manufacturer=document.getElementById('WucVehicleModel1_ddl_Manufacturer');
   lbl_Errors.innerText ="";
//  var objResource=new Resource('WucVehicleModel1_hdf_ResourecString');
   
  
  if (txt_Vehicle_Model_Name.value== '')
  {
      lbl_Errors.innerText = "Please Enter Vehicle Model Name";///objResource.GetMsg("MsgModelName");
      txt_Vehicle_Model_Name.focus();
      return false;
  }
  else if (ddl_Manufacturer.options[ddl_Manufacturer.options.selectedIndex].value==0)
  {
     lbl_Errors.innerText = "Please Select Manufacturer Name";//objResource.GetMsg("MsgManufacturerName");
     ddl_Manufacturer.focus();   
     return false;
  }
  else
      ATS = true;

return ATS;
}