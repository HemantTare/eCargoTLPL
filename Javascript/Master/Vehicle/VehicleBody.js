// JScript File

function ValidateUI()
{
 var ATS=false;
 var lbl_Errors=document.getElementById('WucVehicleBody1_lbl_Errors');
  
 var txt_VehicleBody=document.getElementById('WucVehicleBody1_txt_VehicleBody');
 lbl_Errors.innerText = "";  
// var objResource=new Resource('WucVehicleBody1_hdf_ResourecString');
  
     
   
    if(txt_VehicleBody.value=='')
       {
        lbl_Errors.innerText = "Please Enter Vehicle Body";//objResource.GetMsg("MsgVehicleBody");
        txt_VehicleBody.focus();
       }
       else
       ATS=true;
       return ATS;
 
}