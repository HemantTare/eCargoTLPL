// JScript File
function DisableControlOnChecked()
{
 var txt_Tds_Rate_Percent=document.getElementById("WucVendor1_txt_Tds_Rate_Percent");
 var txt_Tds_Exemption_Limit=document.getElementById("WucVendor1_txt_Tds_Exemption_Limit");
 var ddl_Tds_Name=document.getElementById("WucVendor1_ddl_Tds_Name");
 var Is_Tds=document.getElementById("WucVendor1_Chk_Is_Tds");
 
 var lbl_Tds_Name=document.getElementById("WucVendor1_lbl_Tds_Name");
 var lbl_Tds_Rate_Percent=document.getElementById("WucVendor1_lbl_Tds_Rate_Percent");
  var lbl_Tds_Exemption_Limit=document.getElementById("WucVendor1_lbl_Tds_Exemption_Limit");
 
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
function validateUI()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucVendor1_lbl_Errors');
   var txt_Vendor_Name = document.getElementById('WucVendor1_txt_Vendor_Name');
   var txt_Reference_Name=document.getElementById('WucVendor1_txt_Reference_Name');
   var txt_Reference_Phone=document.getElementById('WucVendor1_txt_Reference_Phone');
   var txt_Reference_Mobile=document.getElementById('WucVendor1_txt_Reference_Mobile');
   var txt_Tds_Rate_Percent=document.getElementById("WucVendor1_txt_Tds_Rate_Percent");
   var txt_Tds_Exemption_Limit=document.getElementById("WucVendor1_txt_Tds_Exemption_Limit");
   var txt_Pan_No=document.getElementById("WucVendor1_txt_Pan_No");
   var Is_Tds=document.getElementById("WucVendor1_Chk_Is_Tds");
   var ddl_Vendor_Type=document.getElementById("WucVendor1_ddl_Vendor_Type");
  
  lbl_Errors.innerText ="";
  
  if (Trim(txt_Vendor_Name.value) == '')
  {
      lbl_Errors.innerText = "Please Enter Vendor Name";
      txt_Vendor_Name.focus();
  } 
    else if(txt_Vendor_Name.value.length < 2)
  {
     lbl_Errors.innerText=" Vendor Name Should be Atleast Two Characters.";
     txt_Vendor_Name.focus();
  }
    else if (val(ddl_Vendor_Type.value) <= 0)  //edited by Ankit : 14-11-08 : 5.00 pm
 {
    lbl_Errors.innerText = "Please Select Vendor Type";
    ddl_Vendor_Type.focus();
 }
  else if(!ValidateWucAddress(lbl_Errors)){}
  
  else if(Trim(txt_Reference_Name.value) == '' && control_is_mandatory(txt_Reference_Name) == true )
  {
      lbl_Errors.innerText="Please Enter Reference Name";
      txt_Reference_Name.focus();
  }
   else if(Trim(txt_Reference_Phone.value) == '' && control_is_mandatory(txt_Reference_Phone) == true )
  {
      lbl_Errors.innerText="Please Enter Reference Phone";
      txt_Reference_Phone.focus();
  }
   else if(Trim(txt_Reference_Mobile.value) == '' && control_is_mandatory(txt_Reference_Mobile) == true )
  {
      lbl_Errors.innerText="Please Enter Reference Mobile";
      txt_Reference_Mobile.focus();
  } 
   else if(Trim(txt_Pan_No.value) == ''&& control_is_mandatory(txt_Pan_No) == true)
  {
      lbl_Errors.innerText="Please Enter PAN No";
      txt_Pan_No.focus();
  } 

 else if(!ValidateWucTDS(lbl_Errors)){}
 
  else
      ATS = true;

return ATS;
}


