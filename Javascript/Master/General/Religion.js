// JScript File
function validateUI()
{
 var ATS=false;
 var lbl_Errors=document.getElementById('WucReligion1_lbl_Errors');
 var txt_Religion=document.getElementById('WucReligion1_txt_Religion');
 //var objResource=new Resource('WucReligion1_hdf_ResourecString');
 lbl_Errors.innerText='';
    if(txt_Religion.value=='')
       {
        lbl_Errors.innerText= "Please Enter Religion";//objResource.GetMsg("Msg_txt_Religion");
        txt_Religion.focus();
       }
       else
       ATS=true;
       return ATS;
 
}

