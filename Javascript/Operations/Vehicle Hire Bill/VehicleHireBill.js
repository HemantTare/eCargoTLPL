// JScript File
function Allow_To_Save()
{
    var ATS = false;
    var ddl_Vehicle = document.getElementById('WucVehicleHireBillDetails1_WucVehicleSearch1_ddl_Vehicle');
    var ddl_FromLocation = document.getElementById('WucVehicleHireBillDetails1_ddl_FromLocation_txtBoxddl_FromLocation');
    var ddl_ToLocation = document.getElementById('WucVehicleHireBillDetails1_ddl_ToLocation_txtBoxddl_ToLocation');
    var ddl_FreightType=document.getElementById('WucVehicleHireBillDetails1_ddl_FreightType');
    var ddl_BrokerName=document.getElementById('WucVehicleHireBillDetails1_ddl_BrokerName');
    
   var lbl_Errors=document.getElementById('WucVehicleHireBillDetails1_lbl_Errors');
   lbl_Errors.innerText="";
   
   if(val(ddl_Vehicle.value) <= 0)
   {
      lbl_Errors.innerText = "Please Select Vehicle No";
      ddl_Vehicle.focus();
   }
   else if (ddl_FromLocation.value == '')
    {
        lbl_Errors.innerText = "Please Select From Location"; 
        ddl_FromLocation.focus();
    }
    else if (ddl_ToLocation.value == '')
    {
        lbl_Errors.innerText = "Please Select To Location"; 
        ddl_ToLocation.focus();
    }    
    else if (ddl_BrokerName.value == 0 ||ddl_BrokerName.options.length <= 0)
    {
      lbl_Errors.innerText = "Please Select Broker Name"; 
        ddl_BrokerName.focus();
    }
    else if ( ddl_FreightType.value == 0 || ddl_FreightType.options.length <= 0)
    {
      lbl_Errors.innerText = "Please Select Broker Name"; 
        ddl_BrokerName.focus();
    }
     else
        ATS = true;

    return ATS;


}
function EnabledDisabledControlOnFreightType(IsExecute)
{
var ddl_FreightType=document.getElementById('WucVehicleHireBillDetails1_ddl_FreightType');
var lbl_WtGuarantee=document.getElementById('WucVehicleHireBillDetails1_lbl_WtGuarantee');
var lbl_RateKg=document.getElementById('WucVehicleHireBillDetails1_lbl_RateKg');
var lbl_ActualWtPayable=document.getElementById('WucVehicleHireBillDetails1_lbl_ActualWtPayable');
var txt_WtGuarantee=document.getElementById('WucVehicleHireBillDetails1_txt_WtGuarantee');
var txt_RateKg=document.getElementById('WucVehicleHireBillDetails1_txt_RateKg');
var lbl_ActualWtPayableValue=document.getElementById('WucVehicleHireBillDetails1_lbl_ActualWtPayableValue');
var lbl_TruckHireChargeValue=document.getElementById('WucVehicleHireBillDetails1_lbl_TruckHireChargeValue');
var txt_TruckHireCharge=document.getElementById('WucVehicleHireBillDetails1_txt_TruckHireCharge');
var lbl_ActualKms=document.getElementById('WucVehicleHireBillDetails1_lbl_ActualKms');


tr_WtGuarantee=document.getElementById('WucVehicleHireBillDetails1_tr_WtGuarantee');
tr_RateKg=document.getElementById('WucVehicleHireBillDetails1_tr_RateKg');
tr_ActualWtPayable=document.getElementById('WucVehicleHireBillDetails1_tr_ActualWtPayable');
tr_TruckHireCharge=document.getElementById('WucVehicleHireBillDetails1_tr_TruckHireCharge');
tr_txt_TruckHireCharge=document.getElementById('WucVehicleHireBillDetails1_tr_txt_TruckHireCharge');
tr_ActualKms=document.getElementById('WucVehicleHireBillDetails1_tr_ActualKms');



hdn_ActualKms=document.getElementById('WucVehicleHireBillDetails1_hdn_ActualKms');
hdn_TruckHireCharge=document.getElementById('WucVehicleHireBillDetails1_hdn_TruckHireCharge');
hdn_ActualWtPayable=document.getElementById('WucVehicleHireBillDetails1_hdn_ActualWtPayable');

hdn_TDSPer=document.getElementById('WucVehicleHireBillDetails1_hdn_TDSPer');
hdn_TDSAmount=document.getElementById('WucVehicleHireBillDetails1_hdn_TDSAmount');
hdn_TotalTruckHire=document.getElementById('WucVehicleHireBillDetails1_hdn_TotalTruckHire');

hdn_FreightType = document.getElementById('WucVehicleHireBillDetails1_hdn_FreightType');
hdn_WtGuarantee = document.getElementById('WucVehicleHireBillDetails1_hdn_WtGuarantee');
hdn_RateKg = document.getElementById('WucVehicleHireBillDetails1_hdn_RateKg');      

tr_WtGuarantee.style.display = 'none';
tr_RateKg.style.display = 'none';
tr_ActualWtPayable.style.display = 'none';
tr_TruckHireCharge.style.display = 'none';
tr_txt_TruckHireCharge.style.display = 'none';
tr_ActualKms.style.display = 'none';

if(val(ddl_FreightType.value)==1)
  {
  tr_WtGuarantee.style.display = '';
  tr_RateKg.style.display = '';
  tr_ActualWtPayable.style.display = '';
  tr_TruckHireCharge.style.display = '';
  tr_ActualKms.style.display='';
  lbl_WtGuarantee.innerText='Wt. Guarantee:';
  lbl_ActualKms.innerText='Actual Wt:';
  lbl_RateKg.innerText='Rate/Kg:';
  lbl_ActualWtPayable.innerText='Actual Wt. Payable:';
  }
else if(val(ddl_FreightType.value)==2)
  {
  tr_txt_TruckHireCharge.style.display = '';
  }
else if(val(ddl_FreightType.value)==3)
  {
  tr_WtGuarantee.style.display = '';
  tr_RateKg.style.display = '';
  tr_ActualWtPayable.style.display = '';     
  lbl_WtGuarantee.innerText='Km Guarantee:';
  lbl_RateKg.innerText='Rate/Km:';
  lbl_ActualKms.innerText='Actual Kms:';
  lbl_ActualWtPayable.innerText='Kms Payable:';     
  tr_TruckHireCharge.style.display = '';
  tr_ActualKms.style.display = '';
  }
else if(val(ddl_FreightType.value)==4)
  {
  tr_WtGuarantee.style.display = '';
  tr_RateKg.style.display = '';
  tr_ActualWtPayable.style.display = '';
  tr_ActualKms.style.display = '';
  lbl_WtGuarantee.innerText='Articles Guarantee:';
  lbl_RateKg.innerText='Rate/Articles:';
  lbl_ActualKms.innerText='Actual Articles:';
  lbl_ActualWtPayable.innerText='Articles Payable:';
  tr_TruckHireCharge.style.display = '';
  }
}
//====================================================================================================

function CalculateTruckHireCharge(callfrom)
{
ddl_FreightType=document.getElementById('WucVehicleHireBillDetails1_ddl_FreightType');
lbl_WtGuarantee=document.getElementById('WucVehicleHireBillDetails1_lbl_WtGuarantee');
lbl_RateKg=document.getElementById('WucVehicleHireBillDetails1_lbl_RateKg');
lbl_ActualKms=document.getElementById('WucVehicleHireBillDetails1_lbl_ActualKms');

lbl_ActualWtPayable=document.getElementById('WucVehicleHireBillDetails1_lbl_ActualWtPayable');
txt_WtGuarantee=document.getElementById('WucVehicleHireBillDetails1_txt_WtGuarantee');
txt_RateKg=document.getElementById('WucVehicleHireBillDetails1_txt_RateKg');
lbl_ActualWtPayableValue=document.getElementById('WucVehicleHireBillDetails1_lbl_ActualWtPayableValue');
lbl_TruckHireChargeValue=document.getElementById('WucVehicleHireBillDetails1_lbl_TruckHireChargeValue');
txt_TruckHireCharge=document.getElementById('WucVehicleHireBillDetails1_txt_TruckHireCharge');
txt_ActualKmsValue=document.getElementById('WucVehicleHireBillDetails1_txt_ActualKmsValue');

tr_WtGuarantee=document.getElementById('WucVehicleHireBillDetails1_tr_WtGuarantee');
tr_RateKg=document.getElementById('WucVehicleHireBillDetails1_tr_RateKg');
tr_ActualWtPayable=document.getElementById('WucVehicleHireBillDetails1_tr_ActualWtPayable');
tr_TruckHireCharge=document.getElementById('WucVehicleHireBillDetails1_tr_TruckHireCharge');
tr_txt_TruckHireCharge=document.getElementById('WucVehicleHireBillDetails1_tr_txt_TruckHireCharge');


hdn_ActualKms=document.getElementById('WucVehicleHireBillDetails1_hdn_ActualKms');
hdn_TruckHireCharge=document.getElementById('WucVehicleHireBillDetails1_hdn_TruckHireCharge');
hdn_ActualWtPayable=document.getElementById('WucVehicleHireBillDetails1_hdn_ActualWtPayable');


hdn_TDSAmountValue=document.getElementById('WucVehicleHireBillDetails1_hdn_TDSAmountValue');



if (callfrom ==1)
  {
  txt_WtGuarantee.value=0;
  txt_RateKg.value=0;
  txt_ActualKmsValue.value=0;
  }
  
if(val(ddl_FreightType.value)==1)//1	Per Kg
  {
  
  lbl_ActualWtPayableValue.innerText=val(txt_WtGuarantee.value); 
  hdn_ActualWtPayable.value=val(txt_WtGuarantee.value);
  if (val(txt_ActualKmsValue.value)>val(txt_WtGuarantee.value))
    {    
    hdn_ActualWtPayable.value=val(txt_ActualKmsValue.value);   
    lbl_ActualWtPayableValue.innerText=val(txt_ActualKmsValue.value); 
    }       
  hdn_TruckHireCharge.value=val(txt_RateKg.value)*val(hdn_ActualWtPayable.value);
  lbl_TruckHireChargeValue.innerText=val(hdn_TruckHireCharge.value);
  tr_txt_TruckHireCharge.style.display='none';
  //txt_ActualKmsValue.value=0;
  

  }
else if(val(ddl_FreightType.value)==2) //2	Fixed
  {
  tr_WtGuarantee.style.display = 'none';
  tr_RateKg.style.display = 'none';
  tr_ActualWtPayable.style.display = 'none';
  tr_ActualKms.style.display = 'none';
  tr_TruckHireCharge.style.display = 'none';
  txt_WtGuarantee.value=0;
  txt_RateKg.value=0;
  lbl_WtGuarantee.innerText=0;
  lbl_RateKg.innerText=0;
  hdn_ActualWtPayable.value=0;
  lbl_ActualWtPayableValue.innerText=0; 
  //hdn_ActualKms.value=0;
  txt_ActualKmsValue.value=0;
  hdn_TruckHireCharge.value=val(txt_TruckHireCharge.value);
  }
else if(val(ddl_FreightType.value)==3)//3	Per Km
  {
  
  lbl_ActualWtPayableValue.innerText=val(txt_WtGuarantee.value); 
  hdn_ActualWtPayable.value=val(txt_WtGuarantee.value);
  hdn_TruckHireCharge.value=val(txt_RateKg.value)*val(hdn_ActualWtPayable.value);
  if (val(txt_ActualKmsValue.value)>val(txt_WtGuarantee.value))
    {    
    hdn_ActualWtPayable.value=val(txt_ActualKmsValue.value);   
    lbl_ActualWtPayableValue.innerText=val(txt_ActualKmsValue.value); 
    hdn_TruckHireCharge.value=val(txt_RateKg.value)*val(hdn_ActualWtPayable.value);
    }
    lbl_TruckHireChargeValue.innerText=val(hdn_TruckHireCharge.value);    
    txt_TruckHireCharge.value=0;
    tr_txt_TruckHireCharge.style.display='none';   

  }
else if(val(ddl_FreightType.value)==4)////4	Per Article
  {
  
  lbl_ActualWtPayableValue.innerText=val(txt_WtGuarantee.value); 
  hdn_ActualWtPayable.value=val(txt_WtGuarantee.value);       
  if (val(txt_ActualKmsValue.value)>val(txt_WtGuarantee.value))
    {
    hdn_ActualWtPayable.value=val(txt_ActualKmsValue.value);   
    lbl_ActualWtPayableValue.innerText=val(txt_ActualKmsValue.value); 
    }       
  hdn_TruckHireCharge.value=val(txt_RateKg.value)*val(hdn_ActualWtPayable.value);
  lbl_TruckHireChargeValue.innerText=val(hdn_TruckHireCharge.value); 
   
  tr_txt_TruckHireCharge.style.display='none';
  txt_TruckHireCharge.value=0; 

  }

CalculateTDS();
}
//================================================================================================================
function CalculateTDS()
{
 lbl_TDSPerValue=document.getElementById('WucVehicleHireBillDetails1_lbl_TDSPerValue');
 lbl_TDSAmountValue=document.getElementById('WucVehicleHireBillDetails1_lbl_TDSAmountValue');
 lbl_TotalTruckHireValue=document.getElementById('WucVehicleHireBillDetails1_lbl_TotalTruckHireValue');
 
 hdn_TDSPer=document.getElementById('WucVehicleHireBillDetails1_hdn_TDSPer');

 hdn_TDSAmountValue=document.getElementById('WucVehicleHireBillDetails1_hdn_TDSAmountValue');
 hdn_TotalTruckHire=document.getElementById('WucVehicleHireBillDetails1_hdn_TotalTruckHire');
 hdn_TruckHireCharge=document.getElementById('WucVehicleHireBillDetails1_hdn_TruckHireCharge');
 
 hdn_TDSAmountValue.value = val(hdn_TruckHireCharge.value) * val(hdn_TDSPer.value)/100;
 lbl_TDSAmountValue.innerText=Math.round(hdn_TDSAmountValue.value);
 
 hdn_TotalTruckHire.value= val(val(hdn_TruckHireCharge.value)-val(lbl_TDSAmountValue.innerText)); 
lbl_TotalTruckHireValue.innerText=Math.round(hdn_TotalTruckHire.value);
}


