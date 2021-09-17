// JScript File
function ValidateUI()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucDriverCategory1_lbl_Errors');
   var txt_Driver_Cateogry_Name = document.getElementById('WucDriverCategory1_txt_Driver_category_Name');
  
//    var objResource=new Resource('WucDriverCategory1_hdf_ResourecString');
    lbl_Errors.innerText ="";

  
  if (txt_Driver_Cateogry_Name.value == '')
  {
      lbl_Errors.innerText = "Please Enter Driver Cateogry";//objResource.GetMsg("Msg_DriverCategoryName");
      txt_Driver_Cateogry_Name.focus();
  }
  else
      ATS = true;

return ATS;
}



