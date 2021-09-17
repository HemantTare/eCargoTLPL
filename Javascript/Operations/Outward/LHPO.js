// JScript File

function ValidateUI_WucLHPOHireDetails()
{
  
   var ATS = false;
   var lbl_Errors = document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_Errors');
   var ddl_Driver1 = document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_Driver1_txtBoxddl_Driver1');
   var ddl_Driver2 = document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_Driver2_txtBoxddl_Driver2');
   var ddl_FromLocation= document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_FromLocation_txtBoxddl_FromLocation');
   var ddl_ToLocation= document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_ToLocation_txtBoxddl_ToLocation');      
   var ddl_LoadingSupervisor=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_LoadingSupervisor_txtBoxddl_LoadingSupervisor');         
   var ddl_FreightType=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_FreightType');
   var ddl_VehicleCategory=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_VehicleCategory');
   var ddl_LHPOType=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_LHPOType');
   var ddl_LHPONo=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_LHPONo');
   var ddl_BrokerName=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_BrokerName');
   var ddl_TDSCertificateTo=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_TDSCertificateTo');
   var ddl_VehicleCategory=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_VehicleCategory');  
   var DDL_Vehicle = document.getElementById('WucLHPO1_WucLHPOHireDetails1_WucVehicleSearch1_ddl_Vehicle');
   var txt_vehicle_search = document.getElementById('WucLHPO1_WucLHPOHireDetails1_WucVehicleSearch1_txt_Vehicle_Last_4_Digits');
   var hdn_Total_No_of_GC = document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_Total_No_of_GC');
   var ddl_Charity=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_Charity_txtBoxddl_Charity');
   var txt_CharityAmount=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_CharityAmount');
    
   //var objResource=new Resource('WucLHPO1_WucLHPOHireDetails1_hdf_ResourceString');
  
    lbl_Errors.innerText ="";
   
//    if ( ddl_VehicleCategory.value == 0 || ddl_VehicleCategory.options.length <= 0)
//    {
//        lbl_Errors.innerText  = "Please Select Vehicle Category"; //objResource.GetMsg("Msg_VehicleCategoryResource");
//        ddl_VehicleCategory.focus();
//    }
//    else 
    if (DDL_Vehicle.options.length == 0)
    {
        lbl_Errors.innerText = "Please Select Vehicle No";//objResource.GetMsg("Msg_VehicleNoResource");
        txt_vehicle_search.disabled=false;
        txt_vehicle_search.focus();
    }
    else if ( ddl_LHPOType.value == 0 || ddl_LHPOType.options.length <= 0)
    {
        lbl_Errors.innerText  = "Please Select LHPO Type"; //objResource.GetMsg("Msg_LHPOTypeResource");
        ddl_LHPOType.focus();
    }
    else if (CheckLHPONo() == false)
    {            
            ATS = false;
            WucLHPO1_TabStrip1.SelectTabById('zero');
            return ATS;
    }
//    else if (ddl_FromLocation.value == '')
//    {
//        lbl_Errors.innerText = "Please Select From Location"; //objResource.GetMsg("Msg_FromLocationResource");
//        ddl_FromLocation.focus();
//    }
//    else if (ddl_ToLocation.value == '')
//    {
//        lbl_Errors.innerText = "Please Select To Location"; //objResource.GetMsg("Msg_ToLocationResource");
//        ddl_ToLocation.focus();
//    }    
    else if (CheckBrokerNameWithTDS() == false)
    {            
            ATS = false;
            return ATS;
    }   
    else if (ddl_Driver1.value == '')
    {
        lbl_Errors.innerText = "Please Enter Driver 1";//objResource.GetMsg("Msg_Driver1Resource");
        ddl_Driver1.focus();
    }
    else if (ddl_Driver1.value == ddl_Driver2.value)
    {
        lbl_Errors.innerText = "Driver 1 and Driver 2 Should Not be Same";//objResource.GetMsg("Msg_Driver");
        ddl_Driver2.focus();
    }
    else if (val(hdn_Total_No_of_GC.value) == 0)
    {
        lbl_Errors.innerText = "Please Select atleast One Manifest"; //objResource.GetMsg("Msg_Grid");
       // ddl_Driver1.disabled=false;
       // ddl_Driver1.focus();
    }
    else if ( ddl_FreightType.value == 0 || ddl_FreightType.options.length <= 0)
    {
        lbl_Errors.innerText  = "Please Select Frieght Type"; //objResource.GetMsg("Msg_FreightTypeResource");
        ddl_FreightType.focus();
    }
    else if (CheckTruckHireCharge() == false)
    {            
            ATS = false;
            return ATS;
    } 
    else if (ddl_VehicleCategory.value != 1 && ddl_Charity.value > 0 && txt_CharityAmount.value <=0)
    {
       lbl_Errors.innerText="Please Enter Charity Amount";
       txt_CharityAmount.focus();
    } 
    else if ( ddl_VehicleCategory.value != 1 && txt_CharityAmount.value > 0 && ddl_Charity.value  < 0)
    {
      lbl_Errors.innerText="Please Select Charity Ledger ";
      ddl_Charity.focus();
    }
   
    else if (ddl_VehicleCategory.value != 1 && validateadvanceamt() == false)
    {      
           ATS = false;
            return ATS;
     }
    else if (ddl_VehicleCategory.value != 1 && IsBalancePayableAtMandatory() == false)
    {            
            ATS = false;
            return ATS;
    }    
//    else if (ddl_LoadingSupervisor.value == '')
//    {
//        lbl_Errors.innerText = "Please Select Loading Supervisor"; //objResource.GetMsg("Msg_LoadingSupervisorResource");
//        ddl_LoadingSupervisor.focus();
//    }
    else if (CheckTotalAdvance() == false)
    {            
            ATS = false;
            return ATS;
    }    
    else
        ATS = true;

    return ATS;
}

function CheckBrokerNameWithTDS()
{
    var ddl_VehicleCategory=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_VehicleCategory');  
    var ddl_BrokerName=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_BrokerName');
    var ddl_TDSCertificateTo=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_TDSCertificateTo');
    var lbl_Errors = document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_Errors');
    //var objResource=new Resource('WucLHPO1_WucLHPOHireDetails1_hdf_ResourceString');
      
    if (val(ddl_VehicleCategory.value)== 5)
    {
        if(val(ddl_BrokerName.value)==0 && (control_is_mandatory(ddl_BrokerName) == true))
        {
            lbl_Errors.innerText = "Please Select Broker Name";//objResource.GetMsg("Msg_BrokerNameResource");
            ddl_BrokerName.focus();
            return false;
        }
        else if(val(ddl_TDSCertificateTo.value)==0 && (control_is_mandatory(ddl_TDSCertificateTo) == true))
        {
            lbl_Errors.innerText = "Please Select TDSCertificate To";//objResource.GetMsg("Msg_TDSCertificateToResource");
            ddl_TDSCertificateTo.disabled=false;
            ddl_TDSCertificateTo.focus();
            return false;
        }
    }        
    return true;
}
function CheckLHPONo()
{
    var ddl_LHPOType=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_LHPOType');
    var ddl_LHPONo=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_LHPONo');
    var lbl_Errors = document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_Errors');
   // var objResource=new Resource('WucLHPO1_WucLHPOHireDetails1_hdf_ResourceString');
    var hdn_LHPOId=document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_LHPOId');

    if(val(hdn_LHPOId.value)<=0)
    {  
        if (val(ddl_LHPOType.value)== 2)
        {
            if(val(ddl_LHPONo.value)==0 || ddl_LHPONo.options.length <= 0)
            {
                lbl_Errors.innerText = "Please Select LHPONo"; //objResource.GetMsg("Msg_DDLLHPONoResource");
                ddl_LHPONo.focus();
                return false;
            }        
        }
    }        
    return true;
}


function CheckTruckHireCharge()
{
    var ddl_FreightType=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_FreightType');
    var lbl_Errors = document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_Errors');   
    //var objResource=new Resource('WucLHPO1_WucLHPOHireDetails1_hdf_ResourceString');
    var hdn_TruckHireCharge = document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_TruckHireCharge'); 
    var txt_TruckHireCharge = document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_TruckHireCharge'); 
    var txt_RateKg=document.getElementById('WucLHPO1_WucLHPOHireDetails1_txt_RateKg');
    var ddl_VehicleCategory=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_VehicleCategory'); 
    var ddl_LHPOType=document.getElementById('WucLHPO1_WucLHPOHireDetails1_ddl_LHPOType');
 

    //1	Per Kg
    if(val(ddl_FreightType.value)==1 || val(ddl_FreightType.value)==3 || val(ddl_FreightType.value)==4 )
    {  
    if(val(ddl_VehicleCategory.value) != 1 && val(ddl_LHPOType.value)!=2)//== 2 || val(ddl_VehicleCategory.value)== 3 || val(ddl_VehicleCategory.value)== 5 )
        {
          if(val(hdn_TruckHireCharge.value)<=0)
          {
             lbl_Errors.innerText = "Truck Hire Charge Should be Greater than Zero"; //objResource.GetMsg("Msg_TruckHireCharge");    
             txt_RateKg.disabled=false;
             txt_RateKg.focus();
             return false;                        
          }    
       }
    }
    else if (val(ddl_FreightType.value)==2 && val(ddl_LHPOType.value)!=2)
    {
        if(val(txt_TruckHireCharge.value)<=0)
        {
             lbl_Errors.innerText = "Truck Hire Charge Should be Greater than Zero" ;//objResource.GetMsg("Msg_TruckHireCharge");  
             txt_TruckHireCharge.disabled=false;
             txt_TruckHireCharge.focus();
             return false;           
        }
    }   
    return true;
    
}
function IsBalancePayableAtMandatory()
{
    

    var lbl_Errors = document.getElementById('WucLHPO1_WucLHPOHireDetails1_lbl_Errors');   
    //var objResource=new Resource('WucLHPO1_WucLHPOHireDetails1_hdf_ResourceString');
    var hdn_BalanceAmount= document.getElementById('WucLHPO1_WucLHPOHireDetails1_hdn_BalanceAmount'); 
    var ddl_hierarchy= document.getElementById('WucLHPO1_WucLHPOHireDetails1_WucHierarchyWithID1_ddl_hierarchy'); 
    var ddl_location= document.getElementById('WucLHPO1_WucLHPOHireDetails1_WucHierarchyWithID1_ddl_location'); 

     if(val(hdn_BalanceAmount.value)>0)
     {
        if (ddl_hierarchy.value=='0')
        {            
             lbl_Errors.innerText = "Please Select Balance Payable At "; //objResource.GetMsg("Msg_BalancePayableAt");  
             ddl_hierarchy.disabled=false;
             ddl_hierarchy.focus();
             return false;     
        } 
        else if (ddl_hierarchy.value=='HO')
        {
            return true;
        }
        else if (ddl_hierarchy.value=='AO' || ddl_hierarchy.value=='BO' || ddl_hierarchy.value=='RO')
        {   
                if (val(ddl_location.value)<=0)
                {            
                     lbl_Errors.innerText = "Please Select Balance Payable Location" ;//objResource.GetMsg("Msg_BalancePayableLocation");  
                     ddl_location.disabled=false;
                     ddl_location.focus();
                     return false;     
                }        
        }        
            
     }
     return true;     

}



