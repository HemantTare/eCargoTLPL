// JScript File
 function validateUI()
{
  
var ATS = false;
var txt_CityName = document.getElementById('WucCity1_txt_CityName');
var ddl_State= document.getElementById('WucCity1_ddl_State');
var lbl_Errors =document.getElementById('WucCity1_lbl_Errors');
//var objResource=new Resource('WucCity1_hdf_ResourecString');

lbl_Errors.innerText='';

if(txt_CityName.value == '')
 {
     lbl_Errors.innerText = "Please Enter City Name";// objResource.GetMsg("Msg_City");
     txt_CityName.focus();
 }
 else if(ddl_State.value == '0')
 {
     lbl_Errors.innerText = "Please Select  State";//objResource.GetMsg("Msg_ddlState");
     ddl_State.focus();
 }
 else 
 ATS = true;

 return ATS;

  
}

