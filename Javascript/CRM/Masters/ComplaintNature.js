// JScript File

function validateUI()
{
 var lbl_Errors = document.getElementById('WucComplaintNature1_lbl_Errors');
 var txt_ComplaintNatureName  =  document.getElementById('WucComplaintNature1_txt_ComplaintNatureName');
 var ATS = false;

 if (Trim(txt_ComplaintNatureName.value)=='')
 {
  lbl_Errors.innerText = "Please Enter Complaint Nature Name";
  txt_ComplaintNatureName.focus();  
 }
 else 
 {
  ATS = true;
 }
    if(ATS)
    {
        return true;
    }
    else
    {
        lbl_Errors.style.visibility = 'visible';
        return false;
    }
}

