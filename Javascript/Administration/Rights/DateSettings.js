// JScript File

function ValidateUI()
{
   
    var ATS=false;
    var txt_ProcessName=document.getElementById("WucDateSettings1_txt_ProcessName");
    var txt_Code=document.getElementById("WucDateSettings1_Txt_Code");
    var lbl_Errors=document.getElementById("WucDateSettings1_lbl_Errors");
    
    lbl_Errors.innerText=" ";
        
     if(txt_ProcessName.value=='')
    {
        lbl_Errors.innerHTML= "Please Enter Process Name";
        txt_ProcessName.focus();
    }
    else if(txt_Code.value=='')
    {
        lbl_Errors.innerHTML= "Please Enter Code";
        txt_Code.focus();
    }
      else
     {
  
      ATS=true;
    }
  
   return ATS;
}