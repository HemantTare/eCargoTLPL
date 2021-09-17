// JScript File
function validateUI()
{
 var lbl_Errors = document.getElementById('WucReasonForFault1_lbl_Errors');
 var txt_ReasonForFault  =  document.getElementById('WucReasonForFault1_txt_ReasonForFault');
 var ATS = false;
 //alert('a');

 if (Trim(txt_ReasonForFault.value)=='')
 {
  lbl_Errors.innerText = "Please Enter Reason For Fault";
  txt_ReasonForFault.focus();
  
 }
 else 
    {ATS = true;}
    if (ATS){return true;}
else
{
lbl_Errors.style.visibility = 'visible';
return false;
}
}

