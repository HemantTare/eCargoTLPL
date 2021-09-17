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
    
 function validateUI()
{
  
var ATS = false;
var txt_StateName = document.getElementById('WucState1_txt_StateName');
var txt_NSDLCode = document.getElementById('WucState1_txt_NsdlCode');
var ddl_Region= document.getElementById('WucState1_ddl_Region');
var lbl_Errors =document.getElementById('WucState1_lbl_Errors');
//var objResource=new Resource('WucState1_hdf_ResourecString');

lbl_Errors.innerText='';

if(txt_StateName.value == '')
 {
     lbl_Errors.innerText = "Please Enter State Name";//  objResource.GetMsg("Msg_State");
     txt_StateName.focus();
 }
 else if(ddl_Region.value == '0')
 {
     lbl_Errors.innerText = "Please Select Region Name";///objResource.GetMsg("Msg_ddlRegion");
     ddl_Region.focus();
 }
 else if(txt_NSDLCode.value.trim().length != 2 || txt_NSDLCode.value <= 0 )
 {
     lbl_Errors.innerText = "Enter Valid State GST Code.";///objResource.GetMsg("Msg_ddlRegion");
     txt_NSDLCode.focus();
 }
 else 
 ATS = true;

 return ATS;

  
}


