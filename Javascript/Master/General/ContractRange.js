// JScript File


function ValidateUI()
{
    var ATS = false;

    var Lbl_Error = document.getElementById('WucContractRange1_lbl_Errors');
    var txt_FromKg = document.getElementById('WucContractRange1_txt_FromKg');
    var txt_ToKg = document.getElementById('WucContractRange1_txt_ToKg');
     
//    var objResource = new Resource('WucContractRange1_hdf_ResourceString');
 
    Lbl_Error.innerText =" ";
    
     if(Trim(txt_FromKg.value) == '')
     {
         Lbl_Error.innerText = "Please Enter From Kg";//objResource.GetMsg("Msg_txt_FromKg"); 
         txt_FromKg.focus();
     }
     else if(Trim(txt_ToKg.value) == '')
     {
         Lbl_Error.innerText = "Please Enter To Kg";//objResource.GetMsg("Msg_txt_ToKg"); 
         txt_ToKg.focus();
     }
     else if (parseInt(txt_ToKg.value) <= parseInt(txt_FromKg.value))
      {
        Lbl_Error.innerText= "Please Enter the valid range";//objResource.GetMsg("Msg_Range");   
        txt_FromKg.focus();
      }
      else 
     ATS = true;
            return ATS;
}

