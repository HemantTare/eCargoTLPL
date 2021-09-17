// JScript File

function validateUI()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucBank1_lbl_Errors');
   var txt_Bank = document.getElementById('WucBank1_txt_Bank');
   var txt_Comments = document.getElementById('WucBank1_txt_Comments');
//   var objResource=new Resource('WucBank1_hdf_ResourecString');
  
  lbl_Errors.innerText ="";
  
  if (Trim(txt_Bank.value) == '')
  {
      lbl_Errors.innerText = "Please Enter Bank Name";// objResource.GetMsg("Msg_txt_Bank");
     txt_Bank.focus();
  }
  else if (Trim(txt_Comments.value) == '')
  {
      lbl_Errors.innerText = "Please Enter Comments";//  objResource.GetMsg("Msg_txt_Comments");
     txt_Comments.focus();
  }
    else
  {
      ATS = true;
  }
    
return ATS;
}