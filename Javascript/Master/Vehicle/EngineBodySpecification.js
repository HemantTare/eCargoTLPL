// JScript File

function ValidateUI_EngineBodySpecificaton()
{
   var ATS = false;
  
   var txt_Chasis_No = document.getElementById('WucVehicle1_WucEngineBodySpecification1_txt_Chasis_No');
   var txt_Engine_No = document.getElementById('WucVehicle1_WucEngineBodySpecification1_txt_Engine_No');
   var ddl_Fuel_Type = document.getElementById('WucVehicle1_WucEngineBodySpecification1_ddl_Fuel_Type');
   var txt_Gross_Veh_Wt = document.getElementById('WucVehicle1_WucEngineBodySpecification1_txt_Gross_Veh_Wt');
   var txt_Unladen_Wt = document.getElementById('WucVehicle1_WucEngineBodySpecification1_txt_Unladen_Wt');
   var lbl_Errors=document.getElementById('WucVehicle1_WucEngineBodySpecification1_lbl_Errors');
     lbl_Errors.innerText ="";
   var hdn_Vehicle_Category_ID=document.getElementById("WucVehicle1_WucVehicleInformation1_hdn_Vehicle_Category_ID");
 

  if(Trim(txt_Chasis_No.value) == '' && control_is_mandatory(txt_Chasis_No) == true)
  {
      lbl_Errors.innerText = "Please Enter Chasis No";
      txt_Chasis_No.focus();
  }
  else if (Trim(txt_Engine_No.value) == '' && control_is_mandatory(txt_Engine_No) == true)
  {
      lbl_Errors.innerText =  "Please Enter Engine No";
      txt_Engine_No.focus();
  }
  else if (ddl_Fuel_Type.value == '0' && control_is_mandatory(ddl_Fuel_Type) == true)
  {
      lbl_Errors.innerText = "Please Select Fuel Type";
      ddl_Fuel_Type.focus();
  }
  else if (Trim(txt_Gross_Veh_Wt.value) == '' && control_is_mandatory(txt_Gross_Veh_Wt) == true)
  {
      lbl_Errors.innerText = "Please Enter Gross Vehicle Weight";
      txt_Gross_Veh_Wt.focus();
  }
  else if (Trim(txt_Gross_Veh_Wt.value) == '0' &&  control_is_mandatory(txt_Gross_Veh_Wt) == true)
  {
      lbl_Errors.innerText = 'Please Enter Vehicle Weight Greater Than 0';
      txt_Gross_Veh_Wt.focus();
  }
  else if (Trim(txt_Unladen_Wt.value) == '' &&  control_is_mandatory(txt_Unladen_Wt) == true)
  {
      lbl_Errors.innerText = "Please Enter Unladen Weight";
      txt_Unladen_Wt.focus();
  }
  else if (Trim(txt_Unladen_Wt.value) == '0' &&  control_is_mandatory(txt_Unladen_Wt) == true)
  {
      lbl_Errors.innerText = 'Please Enter Unladen Weight Greater Than 0';
      txt_Unladen_Wt.focus();
  }
  else
      ATS = true;

return ATS;
}


function Calculate_Vehicle_Capacity()
{
   var txt_Gross_Veh_Wt = document.getElementById('WucVehicle1_WucEngineBodySpecification1_txt_Gross_Veh_Wt');
   var txt_Unladen_Wt = document.getElementById('WucVehicle1_WucEngineBodySpecification1_txt_Unladen_Wt');
   var lbl_Vehicle_Capacity = document.getElementById('WucVehicle1_WucEngineBodySpecification1_lbl_Vehicle_Capacity');
   var hdn_Vehicle_Capacity = document.getElementById('WucVehicle1_WucEngineBodySpecification1_hdn_Vehicle_Capacity');

    if (isNaN(txt_Gross_Veh_Wt.value))txt_Gross_Veh_Wt.value = 0;
    if (isNaN(txt_Unladen_Wt.value))txt_Unladen_Wt.value = 0;
    if (isNaN(lbl_Vehicle_Capacity.innerHTML))lbl_Vehicle_Capacity.innerHTML = 0;
    
   
    
    if (parseFloat(txt_Gross_Veh_Wt.value) > 0 && parseFloat(txt_Unladen_Wt.value) > 0)
    {
         if (parseFloat(txt_Gross_Veh_Wt.value) >= parseFloat(txt_Unladen_Wt.value))
            {
                lbl_Vehicle_Capacity.innerHTML = parseFloat(txt_Gross_Veh_Wt.value) - parseFloat(txt_Unladen_Wt.value);
            }
            else
            {
                lbl_Vehicle_Capacity.innerHTML = 0;
                txt_Unladen_Wt.value = 0;

            }
    }
    else 
    {
        lbl_Vehicle_Capacity.innerHTML = 0;
    }
    hdn_Vehicle_Capacity.value = lbl_Vehicle_Capacity.innerHTML;
}