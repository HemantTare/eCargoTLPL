// JScript File
function validateUI()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucVendorTypeSelection1_lbl_Errors');
   var ddl_KeyName = document.getElementById('WucVendorTypeSelection1_txt_Family_Relation_Name');
//   var objResource=new Resource('WucVendorTypeSelection1_hdf_ResourecString');
  lbl_Errors.innerText ="";
  
  if (ddl_KeyName.value=='0')
  {
      lbl_Errors.innerText = "Please Select Key Name";//objResource.GetMsg("Msg_ddl_KeyName");
      ddl_KeyName.focus();
  }
  else
      ATS = true;

return ATS;
}
