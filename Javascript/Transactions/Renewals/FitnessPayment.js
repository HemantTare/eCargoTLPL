// JScript File

function ValidateUI()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('lbl_Errors');     
   var DDL_Vehicle = document.getElementById('WucVehicleSearch1_ddl_Vehicle');
   var txt_vehicle_search = document.getElementById('WucVehicleSearch1_txt_Vehicle_Last_4_Digits');
   var txt_Fitness_Certificate_No=document.getElementById('txt_Fitness_Certificate_No');
   var ddl_RTO=document.getElementById('ddl_RTO');
   var txt_Amount=document.getElementById('txt_Amount');
   var Wuc_Issue_Date=document.getElementById('Wuc_Issue_Date_Picker_selecteddates');
    
   var objResource=new Resource('hdf_ResourecString');

   ATS = false;

  lbl_Errors.innerText ="";
  
 if (DDL_Vehicle.options.length == 0)
  {
      lbl_Errors.innerText = objResource.GetMsg("Msg_Vehicle");
      txt_vehicle_search.focus();
  } 
  else if (txt_Fitness_Certificate_No.value == '')
  {
     lbl_Errors.innerText = objResource.GetMsg("Msg_txt_Fitness_Certificate_No");
     txt_Fitness_Certificate_No.focus();   
  }
 else if (ddl_RTO.value == 0 || ddl_RTO.options.length <= 0)
  {
     lbl_Errors.innerText = objResource.GetMsg("Msg_ddl_RTO");
     ddl_RTO.focus();   
  } 
  else if (txt_Amount.value == '')
  {
     lbl_Errors.innerText = objResource.GetMsg("Msg_txt_Amount");
     txt_Amount.focus();
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
    var Wuc_Issue_Date=new Date();
    var Wuc_Valid_Upto=new Date();
    var lbl_Errors = document.getElementById('lbl_Errors');     
    var objResource=new Resource('hdf_ResourecString');

    Wuc_Issue_Date='<%=Wuc_Issue_Date.PickerClientID %>.GetSelectedDate()';
    Wuc_Valid_UpTo= '<%=Wuc_Valid_UpTo.PickerClientID %>.GetSelectedDate()';
   
    if (Wuc_Issue_Date > Wuc_Valid_UpTo)
    {
         lbl_Errors.innerText = objResource.GetMsg("Msg_IssueDate");
         return false;       
    }    
}

//function Disabled_Controls_On_Cash()
//{   
//    var txt_Cheque_No=document.getElementById('txt_Cheque_No');
//    var ddl_Bank_Name=document.getElementById('ddl_Bank_Name');
//    var tr_Cheque_Details=document.getElementById('tr_Cheque_Details');
//    var tr_Bank_Name=document.getElementById('tr_Bank_Name');
//    var radio_1=document.getElementById('rdl_Paid_By_0');
// 
//        if(radio_1.checked==true)
//        {
//          txt_Cheque_No.value='';
//          ddl_Bank_Name.selectedIndex=0;                       
//          tr_Cheque_Details.style.display = 'none';
//          tr_Bank_Name.style.display = 'none';
//        
//        }  
//         // tr_Cheque_Details.style.visibility='hidden';
//        //  tr_Bank_Name.style.visibility='hidden';  
//         
//}

function Enabled_Controls_On_Cheque()
{    
    var txt_Cheque_No=document.getElementById('txt_Cheque_No');
    var ddl_Bank_Name=document.getElementById('ddl_Bank_Name');
    var tr_Cheque_Details=document.getElementById('tr_Cheque_Details');
    var tr_Bank_Name=document.getElementById('tr_Bank_Name');
    var radio_2=document.getElementById('rdl_Paid_By_1');
    var radio_1=document.getElementById('rdl_Paid_By_0');    
          //tr_Cheque_Details.style.visibility='visible';
          //tr_Bank_Name.style.visibility='visible';
          
    if(radio_2.checked==true)
    {
       tr_Cheque_Details.style.display = '';
       tr_Bank_Name.style.display = '';      
       radio_2.value = '2';  
    }   
    else
    {
        txt_Cheque_No.value='';
        ddl_Bank_Name.selectedIndex = 0;                       
        tr_Cheque_Details.style.display = 'none';
        tr_Bank_Name.style.display = 'none';
        radio_1.value = '1';  
    }         
}
    
    
