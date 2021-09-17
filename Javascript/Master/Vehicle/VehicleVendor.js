// JScript File
function DisableControlOnChecked()
{
 var txt_Tds_Rate_Percent=document.getElementById("WucVehicleVendor1_txt_Tds_Rate_Percent");
 var txt_Tds_Exemption_Limit=document.getElementById("WucVehicleVendor1_txt_Tds_Exemption_Limit");
 var ddl_Tds_Name=document.getElementById("WucVehicleVendor1_ddl_Tds_Name");
 var Is_Tds=document.getElementById("WucVehicleVendor1_Chk_Is_Tds");
 
 var lbl_Tds_Name=document.getElementById("WucVehicleVendor1_lbl_Tds_Name");
 var lbl_Tds_Rate_Percent=document.getElementById("WucVehicleVendor1_lbl_Tds_Rate_Percent");
  var lbl_Tds_Exemption_Limit=document.getElementById("WucVehicleVendor1_lbl_Tds_Exemption_Limit");
 
 if(Is_Tds.checked==true)
 {
   ddl_Tds_Name.style.visibility='visible';
   txt_Tds_Rate_Percent.style.visibility='visible';
   txt_Tds_Exemption_Limit.style.visibility='visible';
   
   lbl_Tds_Name.style.visibility='visible';
   lbl_Tds_Rate_Percent.style.visibility='visible';
   lbl_Tds_Exemption_Limit.style.visibility='visible';
 }
 else
 {
   ddl_Tds_Name.style.visibility='hidden';
   txt_Tds_Rate_Percent.style.visibility='hidden';
   txt_Tds_Exemption_Limit.style.visibility='hidden';
   
  lbl_Tds_Name.style.visibility='hidden';
  lbl_Tds_Rate_Percent.style.visibility='hidden';
  lbl_Tds_Exemption_Limit.style.visibility='hidden';    
 }
 
}
function ValidateUI()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucVehicleVendor1_lbl_Errors');
   var txt_Vehicle_Vendor_Name = document.getElementById('WucVehicleVendor1_txt_Vehicle_Vendor_Name');
   var txt_Reference_Name=document.getElementById('WucVehicleVendor1_txt_Reference_Name');
   var txt_Reference_Phone=document.getElementById('WucVehicleVendor1_txt_Reference_Phone');
   var txt_Reference_Mobile=document.getElementById('WucVehicleVendor1_txt_Reference_Mobile');
   var txt_Tds_Rate_Percent=document.getElementById("WucVehicleVendor1_txt_Tds_Rate_Percent");
   var txt_Tds_Exemption_Limit=document.getElementById("WucVehicleVendor1_txt_Tds_Exemption_Limit");
   var txt_Pan_No=document.getElementById("WucVehicleVendor1_txt_Pan_No");
   var Is_Tds=document.getElementById("WucVehicleVendor1_Chk_Is_Tds");
   var ddl_Vehicle_Vendor_Type=document.getElementById("WucVehicleVendor1_ddl_Vehicle_Vendor_Type");
 
  lbl_Errors.innerText ="";
  
  if (Trim(txt_Vehicle_Vendor_Name.value) == '')
  {
      lbl_Errors.innerText = "Please Enter Vehicle Vendor Name ";
      txt_Vehicle_Vendor_Name.focus();
  } 
  else if(txt_Vehicle_Vendor_Name.value.length < 2)
  {
     lbl_Errors.innerText=" Vendor Name Should be Atleast Two Characters.";
     txt_Vehicle_Vendor_Name.focus();
  }
  
  else if(!ValidateWucAddress(lbl_Errors)){}
  
  else if(Trim(txt_Reference_Name.value) == '')
  {
      lbl_Errors.innerText="Please Enter Reference Name ";
      txt_Reference_Name.focus();
  }
   else if(Trim(txt_Reference_Phone.value) == '')
  {
      lbl_Errors.innerText="Please Enter Reference Phone ";
      txt_Reference_Phone.focus();
  }
   else if(Trim(txt_Reference_Mobile.value) == '')
  {
      lbl_Errors.innerText="Please Enter Reference Mobile ";
      txt_Reference_Mobile.focus();
  } 
   else if(Is_Tds.checked == true && Trim(txt_Tds_Exemption_Limit.value) == "" )
  {
      lbl_Errors.innerText="Please Enter TDS Exemption Limit ";
      txt_Tds_Exemption_Limit.focus();
  } 
   else if(Trim(txt_Pan_No.value) == '')
  {
      lbl_Errors.innerText="Please Enter Reference Name ";
      txt_Pan_No.focus();
  } 
  else if (ddl_Vehicle_Vendor_Type.options.length <= 0)
 {
    lbl_Errors.innerText = "Please Select Vehicle Vendor Type";
    ddl_Vehicle_Vendor_Type.focus();
 }
  else
      ATS = true;

return ATS;
}


