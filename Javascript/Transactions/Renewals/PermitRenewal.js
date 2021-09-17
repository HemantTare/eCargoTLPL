// JScript File


function ValidateUI()
{
alert('a');
   var ATS = false;
   var lbl_Errors = document.getElementById('lbl_Errors');     
   var DDL_Vehicle = document.getElementById('WucVehicleSearch1_ddl_Vehicle');
   var txt_vehicle_search = document.getElementById('WucVehicleSearch1_txt_Vehicle_Last_4_Digits');
    var ddl_Permit_Type=document.getElementById('ddl_Permit_Type');  
   var txt_Permit_No=document.getElementById('txt_Permit_No');
   var txt_Document_No=document.getElementById('txt_Document_No');  
       
   ATS = false;
  lbl_Errors.innerText ="";
  
 if (DDL_Vehicle.options.length == 0)
  {
      lbl_Errors.innerText = "Please Select Vehicle No";
      txt_vehicle_search.focus();
  } 
  else if (ddl_Permit_Type.value == 0 || ddl_Permit_Type.options.length <= 0)
  {
     lbl_Errors.innerText = "Please Select Permit Type";
     ddl_Permit_Type.focus();   
  }
  else if (txt_Permit_No.value == '')
  {
     lbl_Errors.innerText = "Please Enter Permit No";
     txt_Permit_No.focus();   
  } 
  else if (txt_Document_No.value == '')
  {
     lbl_Errors.innerText = "Please Enter Document No";
     txt_Document_No.focus();
  }
 else if (Check_Date()== false)
  {
        return ATS;
  }
  else
      ATS = true;

return ATS;
}


function Check_Date()
{   
    var Wuc_Valid_From=new Date();
    var Wuc_Valid_Upto=new Date();
    var lbl_Errors = document.getElementById('lbl_Errors'); 
    
    Wuc_Valid_From='<%=Wuc_Valid_From.PickerClientID %>.GetSelectedDate()';
    Wuc_Valid_Upto= '<%=Wuc_Valid_Upto.PickerClientID %>.GetSelectedDate()';

    if (Wuc_Valid_From>Wuc_Valid_Upto)
    {
         lbl_Errors.innerText = "Valid From Date Should be Less Then Valid Upto Date";
         return false;
    }
}
