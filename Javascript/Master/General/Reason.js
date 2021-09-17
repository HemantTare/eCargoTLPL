// JScript File
function validateUI()
{
var ATS = false;


  var lbl_Errors = document.getElementById('WucReason1_lbl_Errors');

  var txt_Reason = document.getElementById('WucReason1_txt_Reason');

  lbl_Errors.innerText='';
  

if (txt_Reason.value == '')
  {
  lbl_Errors.innerText = "Please Enter Reason";
  txt_Reason.focus();
  }
 
else if (!isNaN(txt_Reason.value.slice(0,1)) )
  {
  lbl_Errors.innerText = "First Character Should be Alphabet in Reason Name";
  txt_Reason.focus();
  }
 
else
  ATS = true;
  return ATS;

  
}

