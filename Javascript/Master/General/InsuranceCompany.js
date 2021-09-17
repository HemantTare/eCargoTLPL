// JScript File

function validateUI()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucInsuranceCompany1_lbl_Errors');
   var txt_Insurance_Company_Name = document.getElementById('WucInsuranceCompany1_txt_Insurance_Company_Name');
//    var objResource=new Resource('WucInsuranceCompany1_hdf_ResourecString');
  
  lbl_Errors.innerText ="";
  
  if (Trim(txt_Insurance_Company_Name.value) == '')
  {
      lbl_Errors.innerText = "Please Enter Insurance Company Name";//objResource.GetMsg("Msg_txt_Insurance_Company_Name");
      txt_Insurance_Company_Name.focus();
  }
  else
      ATS = true;

return ATS;
}
