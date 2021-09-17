// JScript File

function validateUI_RegionDepartment()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucRegionDetails1_WucRegionDepartment1_lbl_Errors');
   var txt_CashLimit = document.getElementById('WucRegionDetails1_WucRegionDepartment1_txt_CashLimit');
   var txt_BankLimit = document.getElementById('WucRegionDetails1_WucRegionDepartment1_txt_BankLimit');
 
//  var objResource=new Resource('WucRegionDetails1_WucRegionDepartment1_hdf_ResourecString');
// 
   lbl_Errors.innerText ="";
   
   if ( txt_CashLimit.value == '')
  {
     lbl_Errors.innerText = "Please Enter Cash Limit";// objResource.GetMsg("Msg_CashLimit");
      txt_CashLimit.focus();
  }
  else if (txt_BankLimit.value == '')
  {
     lbl_Errors.innerText = "Please Enter Bank Limit";///objResource.GetMsg("Msg_BankLimit");
     txt_BankLimit.focus();
  }
 else
      ATS = true;

return ATS;
}


