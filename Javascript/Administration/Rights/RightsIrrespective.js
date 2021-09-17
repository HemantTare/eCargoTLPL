
/// Author        : Ankit champaneriya
/// Created On    : 21/11/2008
/// Description   : This is the Form  For Master Rights Irrespective


//checks all DataGrid CheckBoxes 

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
             
             case 'Can_Cancel':  
                if(arr[3]=='chk_CanCancel')
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

