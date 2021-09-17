/// <summary>
/// Author        : Ankit champaneriya
/// Created On    : 06/10/2008
/// Description   : This is the Form  For Master User Rights
/// </summary>
/// 

// JScript File

  function SelectColoumn(chk,gridname,name) 
    { 
      for(i = 0; i < document.forms[0].elements.length; i++) 
        {
        
            elm = document.forms[0].elements[i];
            var elm_id = document.getElementById(elm.id);
            
            var elm_name = elm.name;
            var arr = elm_name.split("$");
                      
        if (arr[1] == gridname)
        {
    
          switch(name)
           {
              case 'Can_Read':  
                if(arr[3]=='chk_CanRead')
                {
                    if (elm.type == 'checkbox') 
                    {
                        elm.checked = chk.checked;   
                    }
                }
              break;
             
             case 'Can_Add':  
                if(arr[3]=='chk_CanAdd')
                {
                    if (elm.type == 'checkbox') 
                    {
                        elm.checked = chk.checked;   
                    }
                }
              break;
             
             case 'Can_Edit':  
                if(arr[3]=='chk_CanEdit')
                {
                    if (elm.type == 'checkbox') 
                    {
                        elm.checked = chk.checked;   
                    }
                }
              break;
             
             case 'Can_Delete':  
                if(arr[3]=='chk_CanDelete')
                {
                    if (elm.type == 'checkbox') 
                    {
                        elm.checked = chk.checked;   
                    }
                }
              break; 
              
              case 'Can_Verify':  
                if(arr[3]=='chk_CanVerify')
                {
                    if (elm.type == 'checkbox') 
                    {
                        elm.checked = chk.checked;   
                    }
                }
              break;
              
              case 'Can_Approve':  
                if(arr[3]=='chk_CanApprove')
                {
                    if (elm.type == 'checkbox') 
                    {
                        elm.checked = chk.checked;                              
                    }
                }
                EnabledAllApproveTextBox(chk);
              break;
              
              case 'IsInfo':  
                if(arr[3]=='chk_IsInfo')
                {
                    if (elm.type == 'checkbox') 
                    {
                        elm.checked = chk.checked;   
                    }
                }
              break;
              
              case 'Can_Audit':  
                if(arr[3]=='chk_CanAudit')
                {
                    if (elm.type == 'checkbox') 
                    {
                        elm.checked = chk.checked;   
                    }
                }
              break;
            }  
        }
    }
}

function EnabledAllApproveTextBox(chk)
{
       for(i = 0; i < document.forms[0].elements.length; i++) 
        {
            elm = document.forms[0].elements[i];
            if (elm.name != undefined)
            {
                var elm_id = document.getElementById(elm.id);
            
                var elm_name = elm.name;
                var arr = elm_name.split("$");
                
                if(arr[3]=='chk_CanApprove')
                {
                     if(chk.checked)
                     {
                        elm_id.checked=true;
                     }
                     else
                     {
                        elm_id.checked=false;
                     }
                }
                if (arr[3] =="txt_Approve_From_Value")
                {
                        if(chk.checked)
                        {
                            elm_id.disabled=false;
                        }
                        else
                        {
                            elm_id.value=0.00;
                            elm_id.disabled=true;
                        }
                }
                if (arr[3] =="txt_Approve_To_Value")
                {
                        if(chk.checked)
                        {
                            elm_id.disabled=false;
                        }
                        else
                        {
                           elm_id.value=0.00;
                           elm_id.disabled=true;
                        }
                }
                
           }
            

       }
 
}

 function EnabledApproveTextBox(chk_CanApprove,txt_Approve_From_Value,txt_Approve_To_Value)
 {  
      var chk_CanApprove=document.getElementById(chk_CanApprove);
      var txt_Approve_From_Value=document.getElementById(txt_Approve_From_Value);
      var txt_Approve_To_Value=document.getElementById(txt_Approve_To_Value);
      	
      if (chk_CanApprove.checked)
      {
            txt_Approve_From_Value.disabled=false;
            txt_Approve_To_Value.disabled=false;
      }
      else
      {
            txt_Approve_From_Value.value=0.00;
            txt_Approve_To_Value.value=0.00;
            txt_Approve_From_Value.disabled=true;
            txt_Approve_To_Value.disabled=true;
      }
    
 }

function SelectRow(chk,gridname) 
{
    var name=chk.name;
    var all = name.split("$");
    var arr1=all[2];
    var basechkname = all[3];
   
   for(i = 0; i < document.forms[0].elements.length; i++) 
   {
     elm = document.forms[0].elements[i];
     var elm_id = document.getElementById(elm.id);
     var elm_name = elm.name;
      
      var arr = elm_name.split("$");
      var arr2;
      arr2=arr[2];
      if (arr[1] == gridname)
      { 

      if(arr1==arr2)
      {
            if (elm.type == 'checkbox')  
            {
                elm.checked = chk.checked;
            }
            if (elm.type == 'text')  
            {
                  if (chk.checked)
                  {  
                    elm_id.disabled=false;
                  }
                  else
                  {
                    elm_id.value=0.00;
                    elm_id.disabled=true;
                  }  
            }
        }
        
      }
    } 
}

//checks all DataGrid CheckBoxes 
    function SelectAllNone(Temp_Char,gridname) 
    {
        for(i = 0; i < document.forms[0].elements.length; i++) 
        {
            elm = document.forms[0].elements[i];
            var elm_id = document.getElementById(elm.id);
            
            var elm_name = elm.name;
            var arr = elm_name.split("$");
            var Flag;

            if (arr[1] == gridname)
            {

                if (elm.type == 'checkbox') 
                {
                   if(Temp_Char=='A')
                   {                       
                        elm.checked = true;                        
                        Flag=1;
                   }
                   else
                   {
                       elm.checked = false;                        
                       Flag=2;
                   }                   
                }
               if (elm.type == 'text')  
               {
                  if (Flag==1)
                  {  
                        elm_id.disabled=false;
                  }
                  else
                  {
                      elm_id.value=0.00;
                      elm_id.disabled=true;
                  }  
               }
            }
        }
        
      }

function ValidateUI()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucUserRights1_lbl_Errors');
   var ddl_Hierarchy = document.getElementById('WucUserRights1_ddl_Hierarchy');
   var ddl_Profile=document.getElementById('WucUserRights1_ddl_Profile');
   var ddl_UserName=document.getElementById('WucUserRights1_ddl_UserName');
   var ddl_MenuSystem=document.getElementById('WucUserRights1_ddl_MenuSystem');
   var ddl_MenuHead=document.getElementById('WucUserRights1_ddl_MenuHead');
   var ddl_MenuGroup=document.getElementById('WucUserRights1_ddl_MenuGroup');  
   
   var objResource = new Resource('WucUserRights1_hdf_ResourceString');
   
  lbl_Errors.innerText ="";
  
  if (ddl_Hierarchy.value == 0 || ddl_Hierarchy.options.length <= 0)
  {
     lbl_Errors.innerText = objResource.GetMsg("Msg_HierarchyName"); // "Please Select Hierarchy Name";
     ddl_Hierarchy.focus();   
     return false;
  }
  if (ddl_Profile.value == 0 || ddl_Profile.options.length <= 0)
  {
     lbl_Errors.innerText =objResource.GetMsg("Msg_ProfileName"); //"Please Select Profile Name";
     ddl_Profile.focus();   
     return false;
  }
  if (ddl_UserName.value == 0 || ddl_UserName.options.length <= 0)
  {
     lbl_Errors.innerText =objResource.GetMsg("Msg_UserName");  // "Please Select User Name";
     ddl_UserName.focus();   
     return false;
  }
  if (ddl_MenuSystem.value == 0 || ddl_MenuSystem.options.length <= 0)
  {
     lbl_Errors.innerText = objResource.GetMsg("Msg_SystemName");   // "Please Select Menu System Name";
     ddl_MenuSystem.focus();   
     return false;
  }
  if (ddl_MenuHead.value == 0 || ddl_MenuHead.options.length <= 0)
  {
     lbl_Errors.innerText =objResource.GetMsg("Msg_HeadName");  // "Please Select Menu Head Name";
     ddl_MenuHead.focus();   
     return false;
  }
  if (ddl_MenuGroup.value == 0 || ddl_MenuGroup.options.length <= 0)
  {
     lbl_Errors.innerText = objResource.GetMsg("Msg_GroupName"); //"Please Select Menu Group Name";
     ddl_MenuGroup.focus();   
     return false;
  }
  else
      ATS = true;

return ATS;
}