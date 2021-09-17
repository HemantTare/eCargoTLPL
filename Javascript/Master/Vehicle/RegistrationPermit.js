// JScript File
function  ValidateUI_RegistrationPermit()
{
   var ATS=false;

   var ddl_PermitType=document.getElementById('WucVehicle1_WucRegistrationPermit1_ddl_PermitType');
   var txt_RegPermitNo=document.getElementById('WucVehicle1_WucRegistrationPermit1_txt_RegPermitNo');
   var txt_DocumentNo=document.getElementById('WucVehicle1_WucRegistrationPermit1_txt_DocumentNo');
   var lbl_Errors=document.getElementById('WucVehicle1_WucRegistrationPermit1_lbl_Errors');

   lbl_Errors.innerText='';

   if(ddl_PermitType.value==0 || ddl_PermitType.options.length <= 0)
     {
      lbl_Errors.innerText="Please Select Permit Type";
      ddl_PermitType.focus();
     }
   else if(txt_RegPermitNo.value=='')
     {
      lbl_Errors.innerText="Please Enter Permit Number";
      txt_RegPermitNo.focus();
     }
  
   else
   {
    ATS=true;
   }
   
   return ATS;
}

