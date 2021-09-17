// JScript File
function  ValidateUI_RegistrationPermit()
{
   var ATS=false;
   var ddl_RegistrationState=document.getElementById('WucVehicle1_WucRegistrationPermit1_ddl_RegistrationState');
   var ddl_RtoAuthorizedPlace=document.getElementById('WucVehicle1_WucRegistrationPermit1_ddl_RtoAuthorizedPlace');
   var ddl_PermitType=document.getElementById('WucVehicle1_WucRegistrationPermit1_ddl_PermitType');
   var txt_TaxReceiptNo=document.getElementById('WucVehicle1_WucRegistrationPermit1_txt_TaxReceiptNo');
   var txt_TaxAmount=document.getElementById('WucVehicle1_WucRegistrationPermit1_txt_TaxAmount');
   var lbl_Errors=document.getElementById('WucVehicle1_WucRegistrationPermit1_lbl_Errors');
   
   lbl_Errors.innerText='';
   
   if(ddl_RegistrationState.value==0 || ddl_RegistrationState.options.length <= 0)
   {
     lbl_Errors.innerText="Please Select Registration State";
     ddl_RegistrationState.focus();
   }
   else if(ddl_RtoAuthorizedPlace.value==0 || ddl_RtoAuthorizedPlace.options.length <= 0)
   {
    lbl_Errors.innerText="Please Select RTO Authorized Place";
    ddl_RtoAuthorizedPlace.focus();
   }
   
   else if(txt_TaxReceiptNo.value=='')
   {
    lbl_Errors.innerText="Please Enter Tax Receipt No";
    txt_TaxReceiptNo.focus();
   }
   else if(txt_TaxAmount.value=='')
   {
    lbl_Errors.innerText="Please Enter Tax Amount";
    txt_TaxAmount.focus();
   }
   else if(ddl_PermitType.value==0 || ddl_PermitType.options.length <= 0)
   {
    lbl_Errors.innerText="Please Select Permit Type";
    ddl_PermitType.focus();
   }
   
   else
   {
    ATS=true;
   }
   
   return ATS;
}

