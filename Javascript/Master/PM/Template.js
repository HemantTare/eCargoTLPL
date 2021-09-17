// JScript File

function validateUI()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucTemplate1_lbl_Errors');
   var txt_TemplateName = document.getElementById('WucTemplate1_txt_TemplateName');
   var txt_Description=document.getElementById('wucTemplate1_txt_Description');
  
  lbl_Errors.innerText ="";
  
  if (txt_TemplateName.value== '')
  {
      lbl_Errors.innerText = "Please Enter Template Name ";
      txt_TemplateName.focus();
  }
  else if (txt_Description.value=='')
  {
     lbl_Errors.innerText="Please Enter Description";
     txt_Description.focus();
  }
  else
      ATS = true;

return ATS;
}


