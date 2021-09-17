// JScript File


function ValidateUI()
{
   
    var ATS=false;
    var txt_MenuItemName=document.getElementById("WucMenuItem1_txt_MenuItemName");
    var txt_SerialNo=document.getElementById("WucMenuItem1_txt_SerialNo");
    var ddl_MenuSystemId=document.getElementById("WucMenuItem1_ddl_MenuSystemId");
    var ddl_MenuHeadId=document.getElementById("WucMenuItem1_ddl_MenuHeadId");
    var ddl_MenuGroupId=document.getElementById("WucMenuItem1_ddl_MenuGroupId");
    var txt_MenuItemLink=document.getElementById("WucMenuItem1_txt_MenuItemLink");
    var txt_Description=document.getElementById("WucMenuItem1_txt_Description");
    var txt_LinkUrl=document.getElementById("WucMenuItem1_txt_LinkUrl");
    var txt_ViewUrl=document.getElementById("WucMenuItem1_txt_ViewUrl");
    var txt_AddUrl=document.getElementById("WucMenuItem1_txt_AddUrl");
    var txt_EditUrl=document.getElementById("WucMenuItem1_txt_EditUrl");
    var txt_DeleteUrl=document.getElementById("WucMenuItem1_txt_DeleteUrl");
    var txt_QueryString=document.getElementById("WucMenuItem1_txt_QueryString");
    var lbl_Errors=document.getElementById("WucMenuItem1_lbl_Errors");
    
    var objResource = new Resource('WucMenuItem1_hdf_ResourceString');
    
    lbl_Errors.innerText=" ";
        
     if(txt_MenuItemName.value=='')
    {
        lbl_Errors.innerHTML= objResource.GetMsg("Msg_MenuItemName"); //"Please Enter Menu Item Name";
        txt_MenuItemName.focus();
    }
   
    else if(txt_SerialNo.value=='')
    {
        lbl_Errors.innerHTML= objResource.GetMsg("Msg_SerialNo"); //"Please Enter Serial Number";
        txt_SerialNo.focus();
        
    }
    
    else if (ddl_MenuSystemId.value == 0 ||ddl_MenuSystemId.options.length <= 0)
  {
      lbl_Errors.innerHTML = objResource.GetMsg("Msg_MenuSystem"); //"Please Select Menu System";
      ddl_MenuSystemId.focus();
  }
    else if (ddl_MenuHeadId.value == 0 ||ddl_MenuHeadId.options.length <= 0)
  {
      lbl_Errors.innerHTML = objResource.GetMsg("Msg_MenuHead"); //"Please Select Menu Head";
      ddl_MenuHeadId.focus();
  }
  
  
    else if (ddl_MenuGroupId.value == 0 ||ddl_MenuGroupId.options.length <= 0)
  {
      lbl_Errors.innerHTML = objResource.GetMsg("Msg_MenuGroup"); //"Please Select Menu Group";
      ddl_MenuGroupId.focus();
  }
    else if(txt_MenuItemLink.value =='')
    {
        lbl_Errors.innerHTML= objResource.GetMsg("Msg_MenuItemLink"); //"Please Enter Menu Item Link";
        txt_MenuItemLink.focus();
        
    }
    else if(txt_Description.value =='')
    {
        lbl_Errors.innerHTML= objResource.GetMsg("Msg_Description");  //"Please Enter Description";
        txt_Description.focus();
        
    }
    else if(txt_LinkUrl.value =='')
    {
        lbl_Errors.innerHTML= objResource.GetMsg("Msg_LinkUrl");  //"Please Enter Link URL";
        txt_LinkUrl.focus();
        
    }
    else if(txt_ViewUrl.value =='')
    {
        lbl_Errors.innerHTML= objResource.GetMsg("Msg_ViewUrl"); //"Please Enter View URL";
        txt_ViewUrl.focus();
        
    }
    else if(txt_AddUrl.value =='')
    {
        lbl_Errors.innerHTML= objResource.GetMsg("Msg_AddUrl"); //"Please Enter add URL";
        txt_AddUrl.focus();
        
    }
    else if(txt_EditUrl.value =='')
    {
        lbl_Errors.innerHTML= objResource.GetMsg("Msg_EditUrl");  //"Please Enter edit URL";
        txt_EditUrl.focus();
        
    }
    else if(txt_DeleteUrl.value =='')
    {
        lbl_Errors.innerHTML=  objResource.GetMsg("Msg_DeleteUrl");  //"Please Enter delete URL";
        txt_DeleteUrl.focus();        
    }
     else
     {
  
      ATS=true;
    }
  
   return ATS;
}

//---------------------------------------------------------------
//Number Check Function
//---------------------------------------------------------------
function Only_Integers(f,evt)
{
//if (window.event.keyCode == 37 || window.event.keyCode == 39 || window.event.keyCode == 9 || window.event.keyCode == 16) return;
//f.value = f.value.replace(/[^.\d]/g,"");

var charCode = (evt.which) ? evt.which : event.keyCode



if(charCode == 46)
{
    return false;
}
if (charCode > 31 && (charCode < 48 || charCode > 57))
    return false;


    return true;

}


