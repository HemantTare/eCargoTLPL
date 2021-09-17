// JScript File


function ValidateUI()
{
    var ATS = false;

    var Lbl_Error = document.getElementById('WucContractTermHeads1_lbl_Errors');
    var txt_FromKg = document.getElementById('WucContractTermHeads1_txt_TermHead');
    var txt_ToKg = document.getElementById('WucContractTermHeads1_txt_Description');
     
//    var objResource = new Resource('WucContractTermHeads1_hdf_ResourceString');
 
    Lbl_Error.innerText =" ";
    
     if(Trim(txt_FromKg.value) == '')
     {
         Lbl_Error.innerText =  "Please Enter Term Head";// objResource.GetMsg("Msg_txt_TermHead"); 
         txt_FromKg.focus();
     }
     else if(Trim(txt_ToKg.value) == '')
     {
         Lbl_Error.innerText = "Please Enter Description";//objResource.GetMsg("Msg_txt_Description");  
         txt_ToKg.focus();
     }
     else 
     ATS = true;
     
            return ATS;
}
