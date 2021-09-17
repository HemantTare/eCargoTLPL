

// JScript File


function validateUI()
{
    var ATS = false;
    var lbl_Errors = document.getElementById('WucTaskTemplate1_lbl_Errors');
 
    var rblst_Odometer = document.getElementById('WucTaskTemplate1_rblst_Schedule_By_0');
    var rblst_Time = document.getElementById('WucTaskTemplate1_rblst_Schedule_By_1');
    var rblst_Odometer_Time= document.getElementById('WucTaskTemplate1_rblst_Schedule_By_2');
    var rbl_Is_Custome_Days=document.getElementById('wucTaskTemplate1_rbl_Is_Custome_Days');
    var rbl_Is_Custome_Months=document.getElementById('wucTaskTemplate1_rbl_Is_Custome_Months');

    var txt_Due_Every=document.getElementById('wucTaskTemplate1_txt_Due_Every');
    var txt_Alert_Before=document.getElementById('wucTaskTemplate1_txt_Alert_Before');
    var txt_Kms=document.getElementById('wucTaskTemplate1_txt_Kms');
    var txt_Days_Months=document.getElementById('wucTaskTemplate1_txt_Days_Months');
    var txt_Odometer_Time_Kms=document.getElementById('wucTaskTemplate1_txt_Odometer_Time_Kms');
    var txt_Odometer_Time_Days_Months=document.getElementById('wucTaskTemplate1_txt_Odometer_Time_Days_Months');
    var txt_Cost=document.getElementById('wucTaskTemplate1_txt_Cost');

    var txt_TaskTemplateName = document.getElementById('WucTaskTemplate1_txt_TaskTemplateName');
    var txt_Description=document.getElementById('wucTaskTemplate1_txt_Description'); 
    var txt_Odometer_Time_Days_Months=document.getElementById('wucTaskTemplate1_txt_Odometer_Time_Days_Months');
    var txt_Days_Months=document.getElementById('wucTaskTemplate1_txt_Days_Months');

    var ddl_Repair_Service_Category=document.getElementById('wucTaskTemplate1_ddl_Repair_Service_Category');
    var ddl_Repair_Service=document.getElementById('wucTaskTemplate1_ddl_Repair_Service');
    var ddl_Completion=document.getElementById('wucTaskTemplate1_ddl_Completion');
    var ddl_To_Be_Worked_At=document.getElementById('wucTaskTemplate1_ddl_To_Be_Worked_At');
    var ddl_Task_Template=document.getElementById('wucTaskTemplate1_ddl_Task_Template');

    var hdn_Form_Type = document.getElementById('WucTaskTemplate1_hdn_Form_Type');

    var hdn_Branch_Id = document.getElementById('WucTaskTemplate1_hdn_Branch_Id'); 
    var hdn_Vendor_Id = document.getElementById('WucTaskTemplate1_hdn_Vendor_Id'); 
  
    var lbl_To_Be_Worked_At = document.getElementById('WucTaskTemplate1_lbl_To_Be_Worked_At'); 
   
    var Chk_Is_Custom = document.getElementById('WucTaskTemplate1_Chk_Is_Custom');

    var ddl_VehicleManufacturer=document.getElementById('wucTaskTemplate1_ddl_VehicleManufacturer');
    var ddl_VehicleModel=document.getElementById('wucTaskTemplate1_ddl_VehicleModel');

    var rblWorkShop=document.getElementById('wucTaskTemplate1_rblWorkShop');
 
    lbl_Errors.innerText ="";

    if (txt_TaskTemplateName.value== '')
    {
        lbl_Errors.innerText = "Please Enter TaskTemplate Name ";
        txt_TaskTemplateName.focus();
        return ATS;
    }
    
    else if (ddl_VehicleManufacturer.value == 0 || ddl_VehicleManufacturer.options.length <= 0)
    {
        lbl_Errors.innerText = "Please Select Vehicle Manufacturer ";
        ddl_VehicleManufacturer.focus(); 
        return ATS;  
    }
    
    else if (ddl_VehicleModel.value == 0 || ddl_VehicleModel.options.length <= 0)
    {
        lbl_Errors.innerText = "Please Select Vehicle Model ";
        ddl_VehicleModel.focus();   
        return ATS;
    }
    
    else  if ((rblst_Odometer.checked == true || rblst_Time.checked == true ) && Chk_Is_Custom.checked == false) 
    {
        if (txt_Due_Every.value== '')
        {
            lbl_Errors.innerText = "Please Enter Due Every ";
            txt_Due_Every.focus();
            return ATS;
        }
        else if (txt_Alert_Before.value== '')
        {
            lbl_Errors.innerText = "Please Enter Alert Before Due Every ";
            txt_Alert_Before.focus();
            return ATS;
        }
        
        else if ( parseFloat(txt_Due_Every.value)*30 < parseFloat( txt_Alert_Before.value) && rblst_Time.checked == true)
        {
            lbl_Errors.innerText = "Alert Before Should Not Exceeds The No Of Days of Month Specified ";
            txt_Alert_Before.focus();
            return ATS;
        }
        else if ( parseFloat(txt_Due_Every.value) < parseFloat( txt_Alert_Before.value) && rblst_Odometer.checked == true)
        {
            lbl_Errors.innerText = "Alert Before Should Not Exceeds Due Every ";
            txt_Alert_Before.focus();
            return ATS;
        }
    }
    else if (rblst_Odometer_Time.checked == true  && Chk_Is_Custom.checked == false) 
    {
        if (txt_Kms.value== '')
        {
            lbl_Errors.innerText = "Please Enter Due Every ";
            txt_Kms.focus();
            return ATS;
        }
        else   if (txt_Days_Months.value== '')
        {
            lbl_Errors.innerText = "Please Enter Days/Months ";
            txt_Days_Months.focus();
            return ATS;
        }
        else   if (txt_Odometer_Time_Kms.value== '')
        {
            lbl_Errors.innerText = "Please Enter Alert Before Odometer ";
            txt_Odometer_Time_Kms.focus();
            return ATS;
        }
        else  if (txt_Odometer_Time_Days_Months.value== '')
        {
            lbl_Errors.innerText = "Please Enter Alert Before Odometer Days/Months ";
            txt_Odometer_Time_Days_Months.focus();
            return ATS;
        }
        
         else if ( parseFloat(txt_Kms.value)< parseFloat( txt_Odometer_Time_Kms.value))
        {
            lbl_Errors.innerText = "Alert Before Should Not Exceeds Due Every ";
            txt_Odometer_Time_Kms.focus();
            return ATS;
        }
          else if (rbl_Is_Custome_Days.checked == true &&  parseFloat(txt_Days_Months.value)< parseFloat( txt_Odometer_Time_Days_Months.value))
        {
            lbl_Errors.innerText = "Alert Before Days Should Not Exceeds Due Every ";
            txt_Odometer_Time_Days_Months.focus();
            return ATS;
        }
        
         if (rbl_Is_Custome_Months.checked == true && (parseInt   (txt_Odometer_Time_Days_Months.value) > (parseInt (txt_Days_Months.value) * 30))     )
        {
            lbl_Errors.innerText="Alert Before Days Exceeds The No Of Days of Month Specified";
            txt_Odometer_Time_Days_Months.focus();
                   return ATS;
        }
    }

    if (ddl_Repair_Service_Category.value == 0 || ddl_Repair_Service_Category.options.length <= 0)
    {
        lbl_Errors.innerText = "Please Select Repair Service Category ";
        ddl_Repair_Service_Category.focus();
        return ATS;   
    }
    else if (ddl_Repair_Service.value == 0 || ddl_Repair_Service.options.length <= 0)
    {
        lbl_Errors.innerText = "Please Select Repair Service ";
        ddl_Repair_Service.focus();   
        return ATS;
    }
    else if (ddl_Completion.value == 0 || ddl_Completion.options.length <= 0)
    {
        lbl_Errors.innerText = "Please Select Completion ";
        ddl_Completion.focus();   
        return ATS;
    }   
    else if (txt_Cost.value=='' || parseFloat(txt_Cost.value)==0  )
    {
        lbl_Errors.innerText="Please Enter Cost";
        txt_Cost.focus();
        return ATS;
    }
//    else if (txt_Description.value=='')
//    {        
//        lbl_Errors.innerText="Please Enter Description";
//        lbl_Errors.style.visibilty = 'visible';
//        txt_Description.focus();
//    }
    else if ( lbl_To_Be_Worked_At.innerText =='Preffered Vendor:')
    {
        if ( hdn_Vendor_Id.value =='' || parseFloat(hdn_Vendor_Id.value)==0)
            {
                ATS = false;
                lbl_Errors.innerText="Please select Vendor";                
            } 
            else
            {
            ATS = true;
            }
    }
    
    else if ( lbl_To_Be_Worked_At.innerText =='Location:')
    {
        if (  hdn_Branch_Id.value =='' || parseFloat(hdn_Branch_Id.value)==0)
            {
                ATS = false;
                lbl_Errors.innerText="Please select Location";                
            } 
            else
            {
                ATS = true;
            }
    }
    
    else
        ATS = true;

    return ATS;
}

function Is_Custome_Checked()
{
    var tr_Is_Custome_Details_True = document.getElementById('WucTaskTemplate1_tr_Is_Custome_Details_True');
    var tr_Is_Custome_Details_False = document.getElementById('WucTaskTemplate1_tr_Is_Custome_Details_False');
    
    var tr_Odometer_Time0 = document.getElementById('WucTaskTemplate1_tr_Odometer_Time0');    
    var tr_Odometer_Time1 = document.getElementById('WucTaskTemplate1_tr_Odometer_Time1');
    var tr_Odometer_Time2 = document.getElementById('WucTaskTemplate1_tr_Odometer_Time2');    
    
    var tr_Custom_Odometer_Time = document.getElementById('WucTaskTemplate1_tr_Custom_Odometer_Time');    
    var tr_Custom_Alert_Before = document.getElementById('WucTaskTemplate1_tr_Custom_Alert_Before');    
 
    var Chk_Is_Custom = document.getElementById('WucTaskTemplate1_Chk_Is_Custom');

    var rbl_Odometer = document.getElementById('WucTaskTemplate1_rbl_Odometer');
    var rbl_Is_Custome_Odometer = document.getElementById('WucTaskTemplate1_rbl_Is_Custome_Odometer');
        
    var hdn_Form_Type = document.getElementById('WucTaskTemplate1_hdn_Form_Type');

      if ( hdn_Form_Type.value == 'Template' && Chk_Is_Custom.checked == true)
        {   
            tr_Odometer_Time0.style.display = 'none'      
            tr_Odometer_Time1.style.display = 'inline'    
            tr_Odometer_Time2.style.display = 'inline'    
            tr_Custom_Odometer_Time.style.display = 'inline'     
            tr_Custom_Alert_Before.style.display = 'inline'      
        }
      else if ( hdn_Form_Type.value == 'VehiclePM' && Chk_Is_Custom.checked == true)
        {   
            tr_Odometer_Time0.style.display = 'none'      
            tr_Odometer_Time1.style.display = 'none'      
            tr_Odometer_Time2.style.display = 'none'      
            tr_Custom_Odometer_Time.style.display = 'inline'     
            tr_Custom_Alert_Before.style.display = 'inline'     
        }
        
        else if ( hdn_Form_Type.value == 'Template' && Chk_Is_Custom.checked == false)
        {   
             tr_Odometer_Time0.style.display = 'inline'   
            tr_Odometer_Time1.style.display = 'none'      
            tr_Odometer_Time2.style.display = 'none'      
            tr_Custom_Odometer_Time.style.display = 'none'      
            tr_Custom_Alert_Before.style.display =  'none'      
        }
        else if ( hdn_Form_Type.value == 'VehiclePM' && Chk_Is_Custom.checked == false)
        {   
            tr_Odometer_Time0.style.display = 'inline'      
            tr_Odometer_Time1.style.display = 'none'      
            tr_Odometer_Time2.style.display = 'none'      
            tr_Custom_Odometer_Time.style.display = 'none'     
            tr_Custom_Alert_Before.style.display =  'none'      
        }
}


function Show_Odometer_Time_Detail()
{
    var tr_Odometer_Time0 = document.getElementById('WucTaskTemplate1_tr_Odometer_Time0');
    var tr_Odometer_Time1 = document.getElementById('WucTaskTemplate1_tr_Odometer_Time1');
    var tr_Odometer_Time2 = document.getElementById('WucTaskTemplate1_tr_Odometer_Time2');
    var tr_Custom_Odometer_Time = document.getElementById('WucTaskTemplate1_tr_Custom_Odometer_Time');    

    var tr_Custom_Alert_Before = document.getElementById('WucTaskTemplate1_tr_Custom_Alert_Before');     

    var rblst_Odometer = document.getElementById('WucTaskTemplate1_rblst_Schedule_By_0');
    var rblst_Time = document.getElementById('WucTaskTemplate1_rblst_Schedule_By_1');
    var rblst_Odometer_Time= document.getElementById('WucTaskTemplate1_rblst_Schedule_By_2');
 
    var hdn_Form_Type = document.getElementById('WucTaskTemplate1_hdn_Form_Type');
    var Chk_Is_Custom = document.getElementById('WucTaskTemplate1_Chk_Is_Custom');
    
    if ( hdn_Form_Type.value == 'VehiclePM' && Chk_Is_Custom.checked == true)
        {        
            tr_Odometer_Time0.style.display = 'none'      
            tr_Odometer_Time1.style.display = 'none'      
            tr_Odometer_Time2.style.display = 'none' 
            tr_Custom_Odometer_Time.style.display = 'inline'     
            tr_Custom_Alert_Before.style.display =  'inline'      
        }
    else if (rblst_Odometer_Time.checked == true ) 
        {  
            tr_Odometer_Time0.style.display = 'none'      
            tr_Odometer_Time1.style.display = 'inline'      
            tr_Odometer_Time2.style.display = 'inline'       
            tr_Custom_Odometer_Time.style.display = 'none'     
            tr_Custom_Alert_Before.style.display =  'none'      
        }
    else
        {   
            tr_Odometer_Time0.style.display = 'inline'      
            tr_Odometer_Time1.style.display = 'none'      
            tr_Odometer_Time2.style.display = 'none' 
            tr_Custom_Odometer_Time.style.display = 'none'     
            tr_Custom_Alert_Before.style.display =  'none'      
        }
}



function Show_Last_Permormed_On()
{
    var tr_Last_Permormed_On_Value = document.getElementById('WucTaskTemplate1_tr_Last_Permormed_On_Value');
    var tr_Last_Permormed_On_Date = document.getElementById('WucTaskTemplate1_tr_Last_Permormed_On_Date');
    var tr_task_template = document.getElementById('WucTaskTemplate1_tr_task_template');
    var tddtp_Last_Permormed_Date = document.getElementById('WucTaskTemplate1_tddtp_Last_Permormed_Date');
    var tdtxt_Last_Permormed_On = document.getElementById('WucTaskTemplate1_tdtxt_Last_Permormed_On');
    var td_On = document.getElementById('WucTaskTemplate1_tdon');
    var tdkms = document.getElementById('WucTaskTemplate1_tdkms');

    var rblst_Odometer = document.getElementById('WucTaskTemplate1_rblst_Schedule_By_0');
    var rblst_Time = document.getElementById('WucTaskTemplate1_rblst_Schedule_By_1');
    var rblst_Odometer_Time= document.getElementById('WucTaskTemplate1_rblst_Schedule_By_2');

    var hdn_Form_Type = document.getElementById('WucTaskTemplate1_hdn_Form_Type');

    if (hdn_Form_Type.value== 'Template') 
    {  
        tr_Last_Permormed_On_Value.style.display = 'none'      
        tr_task_template.style.display = 'none'        
    }
    else
    {   
        if ( rblst_Odometer.checked == true )
            {        
                tr_Last_Permormed_On_Value.style.display = 'inline';
                tddtp_Last_Permormed_Date.style.display = 'none';
                tdtxt_Last_Permormed_On.style.display = 'inline';
                td_On.style.display = 'none';
                tdkms.style.display = 'inline';
            }
        else if (rblst_Time.checked == true )
            {
               tr_Last_Permormed_On_Value.style.display = 'inline';
               tddtp_Last_Permormed_Date.style.display = 'inline';
               tdtxt_Last_Permormed_On.style.display = 'none';
               td_On.style.display = 'none';
               tdkms.style.display = 'none';
            }
        else if (rblst_Odometer_Time.checked == true )
        {
           tddtp_Last_Permormed_Date.style.display = 'inline';
           tdtxt_Last_Permormed_On.style.display = 'inline';
           td_On.style.display = 'inline';
           tdkms.style.display = 'inline';
        }
        tr_task_template.style.display = 'none'      
    }
}