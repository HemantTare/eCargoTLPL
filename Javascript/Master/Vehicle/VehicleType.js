

// JScript File
function ValidateUI()
{
   var ATS = false;
   var lbl_Errors=document.getElementById('WucVehicleType1_lbl_Errors');
   var txt_Vehicle_Type_Name = document.getElementById('WucVehicleType1_txt_Vehicle_Type_Name');
   
//   var objResource=new Resource('WucVehicleType1_hdf_ResourecString');
   
     lbl_Errors.innerText ="";
  
  if (Trim(txt_Vehicle_Type_Name.value) == '')
  {
      lbl_Errors.innerText = "Please Enter Vehicle Type Name";//objResource.GetMsg("MsgVehicleTypeName");
      txt_Vehicle_Type_Name.focus();
  }
  else
      ATS = true;

return ATS;
}

