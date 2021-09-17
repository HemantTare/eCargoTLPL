// JScript File

 function CheckAllDataGridCheckBoxes(chk,gridname) 
    {
        for(i = 0; i < document.forms[0].elements.length; i++) 
        {
            elm = document.forms[0].elements[i];
         if (elm.name!= undefined) 
          { 
            var elm_id = document.getElementById(elm.id);
            
            var elm_name = elm.name;
            var arr = elm_name.split("$");
            chk_count =0;
          
           
            if (arr[1] == gridname)
            {

                if (elm.type == 'checkbox') 
                {
                elm.checked = chk.checked;
                chk_count++ ;   
                }
            }
          } 
        }
    }
    
  
function CheckSelection()
{
var checked = 0;
var lbl_Errors=document.getElementById("WucIncludeExcludeUser1_lbl_Errors");
lbl_Errors.innerText="";
for(i = 0; i < document.forms[0].elements.length; i++)
{
if(document.forms[0].elements[i].name.indexOf("Chk_Attach")>-1)
{
if(document.forms[0].elements[i].checked == true)
{
var checked = 1;
}
}
}
if(checked == 0)
{
//alert("Please Select atleast one checkbox");
lbl_Errors.innerText="Please Select Atleast One User";
return false;
}
}


