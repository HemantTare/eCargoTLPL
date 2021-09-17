// JScript File
function ValidateUI()
{
  var ATS=false;
  var lbl_Errors=document.getElementById('WucDriverLicenseCategory1_lbl_Errors');
  var txt_DriverLicenseCategory=document.getElementById('WucDriverLicenseCategory1_txt_DriverLicenseCategory');
  
//   var objResource=new Resource('WucDriverLicenseCategory1_hdf_ResourecString');
    lbl_Errors.innerText ="";

  if(txt_DriverLicenseCategory.value=='')
   {
    lbl_Errors.innerText= "Please Enter Driver License Category";//objResource.GetMsg("Msg_Txt_DriverLicenseCategory");
    txt_DriverLicenseCategory.focus();
   }
  else 
    ATS=true;
    return ATS;
}

