// JScript File

function Allow_to_Save_RateCardParameter()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucBranchRateParameters1_lbl_Errors');
   var ddl_ToBranch = document.getElementById('WucBranchRateParameters1_ddl_ToBranch_txtBoxddl_ToBranch');
   var txt_MinChrgeWt = document.getElementById('WucBranchRateParameters1_txt_MinChrgeWt');
   var txt_BiltyCharges = document.getElementById('WucBranchRateParameters1_txt_BiltyCharges');
   var txt_FOVPercent = document.getElementById('WucBranchRateParameters1_txt_FOVPercent');
   var txt_MinFOV = document.getElementById('WucBranchRateParameters1_txt_MinFOV');
   var txt_HamaliPercent = document.getElementById('WucBranchRateParameters1_txt_HamaliPercent');
   var txt_MinHamali = document.getElementById('WucBranchRateParameters1_txt_MinHamali');
   var txt_DoorDeliveryCharges=document.getElementById('WucBranchRateParameters1_txt_DoorDeliveryCharges');
   var txt_ToPayCharges=document.getElementById('WucBranchRateParameters1_txt_ToPayCharges');
   var txt_DACCCharges = document.getElementById('WucBranchRateParameters1_txt_DACCCharges');
   var txt_CFTFactor = document.getElementById('WucBranchRateParameters1_txt_CFTFactor');
   var txt_DemurrageDays = document.getElementById('WucBranchRateParameters1_txt_DemurrageDays');
   var txt_DemurrageRate = document.getElementById('WucBranchRateParameters1_txt_DemurrageRate');
   var txt_OctroiFormCharges = document.getElementById('WucBranchRateParameters1_txt_OctroiFormCharges');
   var txt_OctroiServiceCharges = document.getElementById('WucBranchRateParameters1_txt_OctroiServiceCharges');
   var txt_GICharges=document.getElementById('WucBranchRateParameters1_txt_GICharges');
   var txt_DeliveryCommission = document.getElementById('WucBranchRateParameters1_txt_DeliveryCommission');
   var txt_FirstNoticeDays = document.getElementById('WucBranchRateParameters1_txt_FirstNoticeDays');
   var txt_SecondNoticeDays = document.getElementById('WucBranchRateParameters1_txt_SecondNoticeDays');
   var txt_ThirdNoticeDays = document.getElementById('WucBranchRateParameters1_txt_ThirdNoticeDays');
   var txt_CashLimit = document.getElementById('WucBranchRateParameters1_txt_CashLimit');
   var txt_BankLimit = document.getElementById('WucBranchRateParameters1_txt_BankLimit');
   var hdn_KeyId = document.getElementById('WucBranchRateParameters1_hdn_KeyId');

    var txt_Freight = document.getElementById('WucBranchRateParameters1_txt_Freight');
    var txt_HamaliofBooking= document.getElementById('WucBranchRateParameters1_txt_HamaliofBooking');
    var txt_FovofBooking= document.getElementById('WucBranchRateParameters1_txt_FovofBooking');
    var txt_TpCharge= document.getElementById('WucBranchRateParameters1_txt_TpCharge');
    var txt_Ddcharge= document.getElementById('WucBranchRateParameters1_txt_Ddcharge');
    var txt_Octroiformchargepercent= document.getElementById('WucBranchRateParameters1_txt_Octroiformchargepercent');
    var txt_Octroiservicechargepercent= document.getElementById('WucBranchRateParameters1_txt_Octroiservicechargepercent');
    var txt_GichargesofDel= document.getElementById('WucBranchRateParameters1_txt_GichargesofDel');
    var txt_HamaliofDel= document.getElementById('WucBranchRateParameters1_txt_HamaliofDel');
    var txt_Demurrage= document.getElementById('WucBranchRateParameters1_txt_Demurrage');


  var objResource=new Resource('WucBranchRateParameters1_hdf_ResourecString');
  
   lbl_Errors.innerText ="";
   
  if(parseInt(hdn_KeyId.value) <= 0 && ddl_ToBranch.value == '' && (control_is_mandatory(ddl_ToBranch) == true))
  {
     lbl_Errors.innerText = "Please Select Branch" ;//objResource.GetMsg("Msg_ddl_ToBranch");
     ddl_ToBranch.focus();
  }
  else if (parseFloat(txt_MinChrgeWt.value) < 0 || txt_MinChrgeWt.value =='' && (control_is_mandatory(txt_MinChrgeWt) == true))
  {
     lbl_Errors.innerText = "Please Enter Minimum Charge Weight";// objResource.GetMsg("Msg_txt_MinchWt");
     txt_MinChrgeWt.focus();
  }
  else if(parseFloat(txt_BiltyCharges.value) < 0 || txt_BiltyCharges.value =='' && (control_is_mandatory(txt_BiltyCharges) == true))
  {
    lbl_Errors.innerText= objResource.GetMsg("Msg_txt_Biltycharge");
    txt_BiltyCharges.focus();
  }  
  else if(parseFloat(txt_FOVPercent.value) < 0 || txt_FOVPercent.value=='' && (control_is_mandatory(txt_FOVPercent) == true))
  {
     lbl_Errors.innerText = "Please Enter FOV Percent";//objResource.GetMsg("Msg_txt_fovPercent");
     txt_FOVPercent.focus();
  }
  else if(parseFloat(txt_MinFOV.value) < 0 || txt_MinFOV.value =='' && (control_is_mandatory(txt_MinFOV) == true))
  {
     lbl_Errors.innerText = "Please Enter Min  FOV";//objResource.GetMsg("Msg_txt_Minfov");
     txt_MinFOV.focus();
  } 
  else if(parseFloat(txt_HamaliPercent.value) < 0 || txt_HamaliPercent.value=='' && (control_is_mandatory(txt_HamaliPercent) == true))
  {
     lbl_Errors.innerText = "Please Enter Hamali"; //objResource.GetMsg("Msg_txt_hamalipercent");
     txt_HamaliPercent.focus();
  }
  else if(parseFloat(txt_MinHamali.value) < 0 || txt_MinHamali.value=='' && (control_is_mandatory(txt_MinHamali) == true))
  {
     lbl_Errors.innerText = "Please Enter Min Hamali";//objResource.GetMsg("Msg_txt_Minhamali");
     txt_MinHamali.focus();
  }
  else if(parseFloat(txt_DoorDeliveryCharges.value)<0 || txt_DoorDeliveryCharges.value=='' && (control_is_mandatory(txt_BiltyCharges) == true))
  {
        lbl_Errors.innerText = "Please Enter DoorDeliveryCharges";// objResource.GetMsg("MSG_txt_DoorDeliveryCharges");
        txt_DoorDeliveryCharges.focus();
  }
  else if(parseFloat(txt_ToPayCharges.value) < 0 || txt_ToPayCharges.value=='' && (control_is_mandatory(txt_DoorDeliveryCharges) == true))
  {  
     lbl_Errors.innerHTML = "Please Enter To Pay Charges";//objResource.GetMsg("Msg_txt_TopayCharges");
     txt_ToPayCharges.focus();
  } 
  else if(parseFloat(txt_DACCCharges.value) < 0 || txt_DACCCharges.value=='' && (control_is_mandatory(txt_DACCCharges) == true))
  {
     lbl_Errors.innerText = "Please Enter DACC Charges";//objResource.GetMsg("Msg_txt_daccCharges");
     txt_DACCCharges.focus();
  }
  else if(parseInt(txt_CFTFactor.value) < 0 || txt_CFTFactor.value=='' && (control_is_mandatory(txt_CFTFactor) == true))
  {
     lbl_Errors.innerText = "Please Enter CFT Factor";//objResource.GetMsg("Msg_txt_CftFactor");
     txt_CFTFactor.focus();
  }
  else if(parseInt(txt_DemurrageDays.value) < 0 || txt_DemurrageDays.value=='' && (control_is_mandatory(txt_DemurrageDays) == true))
  {
     lbl_Errors.innerText = "Please Enter Demurrage Days"; //objResource.GetMsg("Msg_txt_DemDays");
     txt_DemurrageDays.focus();
  } 
  else if(parseFloat(txt_DemurrageRate.value) < 0 || txt_DemurrageRate.value=='' && (control_is_mandatory(txt_DemurrageRate) == true))
  {
     lbl_Errors.innerText = "Please Enter Demurrage Rate";//objResource.GetMsg("Msg_txt_DemRate");
     txt_DemurrageRate.focus();
  }
  else if(parseFloat(txt_OctroiFormCharges.value) < 0 || txt_OctroiFormCharges.value=='' && (control_is_mandatory(txt_OctroiFormCharges) == true))
  {
     lbl_Errors.innerText = "Please Enter Octroi Form Charges";//objResource.GetMsg("Msg_txt_OctroiFormCharge");
     txt_OctroiFormCharges.focus();
  }
  else if(parseFloat(txt_OctroiServiceCharges.value) < 0 ||txt_OctroiServiceCharges.value=='' && (control_is_mandatory(txt_OctroiServiceCharges) == true))
  {  
     lbl_Errors.innerHTML = "Please Enter Octroi Service Charges";//objResource.GetMsg("Msg_txt_OctroiserCharge");
     txt_OctroiServiceCharges.focus();
  } 
  else if(parseFloat(txt_GICharges.value) < 0 || txt_GICharges.value=='' && (control_is_mandatory(txt_GICharges) == true))
  {
     lbl_Errors.innerText = objResource.GetMsg("Msg_txt_GICharge");
     txt_GICharges.focus();
  }  
  else if(parseFloat(txt_DeliveryCommission.value) < 0 || txt_DeliveryCommission.value=='' && (control_is_mandatory(txt_DeliveryCommission) == true))
  {
     lbl_Errors.innerText = "Please Enter Delivery Commission"; //objResource.GetMsg("Msg_txt_DelCommission");
     txt_DeliveryCommission.focus();
  }
  else if(parseInt(txt_FirstNoticeDays.value) < 0 || txt_FirstNoticeDays.value=='' && (control_is_mandatory(txt_FirstNoticeDays) == true))
  {
     lbl_Errors.innerText = "Please Enter First Notice Days"; //objResource.GetMsg("Msg_txt_FND");
     txt_FirstNoticeDays.focus();
  } 
  else if(parseInt(txt_SecondNoticeDays.value) < 0 || txt_SecondNoticeDays.value=='' && (control_is_mandatory(txt_SecondNoticeDays) == true))
  {
     lbl_Errors.innerText = "Please Enter Second Notice Days";//objResource.GetMsg("Msg_txt_SND");
     txt_SecondNoticeDays.focus();
  }
  else if(parseInt(txt_ThirdNoticeDays.value) < 0 || txt_ThirdNoticeDays.value=='' && (control_is_mandatory(txt_ThirdNoticeDays) == true))
  {
     lbl_Errors.innerText = "Please Enter Third Notice Days";//objResource.GetMsg("Msg_txt_TND");
     txt_ThirdNoticeDays.focus();
  }
  else if(parseFloat(txt_CashLimit.value) < 0 || txt_CashLimit.value=='' && (control_is_mandatory(txt_CashLimit) == true))
  {  
     lbl_Errors.innerHTML = "Please Enter Cash Limit"; //objResource.GetMsg("Msg_txt_CashLimit");
     txt_CashLimit.focus();
  } 
  else if(parseFloat(txt_BankLimit.value) < 0 || txt_BankLimit.value=='' && (control_is_mandatory(txt_BankLimit) == true))
  {
     lbl_Errors.innerText = "Please Enter Bank Limit"; //objResource.GetMsg("Msg_txt_BankLimit");
     txt_BankLimit.focus();
  }
  else if(parseFloat(txt_Freight.value) > 100 && (control_is_mandatory(txt_Freight) == true))
  {
     WucBranchRateParameters1_TabStrip1.SelectTabById('one');
     lbl_Errors.innerText = "Percentage Should Not be Greater than 100";//objResource.GetMsg("Msg_txt_Percentage");
     txt_Freight.disabled=false;
     txt_Freight.focus();
  }
  else if(parseFloat(txt_HamaliofBooking.value) > 100  && (control_is_mandatory(txt_HamaliofBooking) == true))
  {
      WucBranchRateParameters1_TabStrip1.SelectTabById('one');
     lbl_Errors.innerText = "Percentage Should Not be Greater than 100";//objResource.GetMsg("Msg_txt_Percentage");
     txt_HamaliofBooking.disabled=false;
     txt_HamaliofBooking.focus();
  }  
  else if(parseFloat(txt_FovofBooking.value) > 100 && (control_is_mandatory(txt_FovofBooking) == true))
  {
     WucBranchRateParameters1_TabStrip1.SelectTabById('one');
     lbl_Errors.innerText = "Percentage Should Not be Greater than 100"; //objResource.GetMsg("Msg_txt_Percentage");  
     txt_FovofBooking.disabled=false;
     txt_FovofBooking.focus();
  }
  else if(parseFloat(txt_TpCharge.value) > 100 && (control_is_mandatory(txt_TpCharge) == true))
  {
     WucBranchRateParameters1_TabStrip1.SelectTabById('one');
     lbl_Errors.innerText = "Percentage Should Not be Greater than 100"; //objResource.GetMsg("Msg_txt_Percentage");     
     txt_TpCharge.disabled=false;
     txt_TpCharge.focus();
  }  
  else if(parseFloat(txt_Ddcharge.value) > 100 && (control_is_mandatory(txt_Ddcharge) == true))
  {
     WucBranchRateParameters1_TabStrip1.SelectTabById('one');
     lbl_Errors.innerText = "Percentage Should Not be Greater than 100";//objResource.GetMsg("Msg_txt_Percentage");   
     txt_Ddcharge.disabled=false;
     txt_Ddcharge.focus();
  }   
   else if(parseFloat(txt_Octroiformchargepercent.value) > 100 && (control_is_mandatory(txt_Octroiformchargepercent) == true))
  {
     WucBranchRateParameters1_TabStrip1.SelectTabById('one');
     lbl_Errors.innerText = "Percentage Should Not be Greater than 100";//objResource.GetMsg("Msg_txt_Percentage");    
     txt_Octroiformchargepercent.disabled=false;
     txt_Octroiformchargepercent.focus();
  }   
   else if(parseFloat(txt_Octroiservicechargepercent.value) > 100 && (control_is_mandatory(txt_Octroiservicechargepercent) == true))
  {
      WucBranchRateParameters1_TabStrip1.SelectTabById('one');
     lbl_Errors.innerText = "Percentage Should Not be Greater than 100";//objResource.GetMsg("Msg_txt_Percentage");   
     txt_Octroiservicechargepercent.disabled=false;
     txt_Octroiservicechargepercent.focus();
  }      
   else if(parseFloat(txt_GichargesofDel.value) > 100 && (control_is_mandatory(txt_GichargesofDel) == true))
  {
     WucBranchRateParameters1_TabStrip1.SelectTabById('one');
     lbl_Errors.innerText = "Percentage Should Not be Greater than 100";// objResource.GetMsg("Msg_txt_Percentage");     
     txt_GichargesofDel.disabled=false;
     txt_GichargesofDel.focus();
  }    
   else if(parseFloat(txt_HamaliofDel.value) > 100 && (control_is_mandatory(txt_HamaliofDel) == true)) 
  {
     WucBranchRateParameters1_TabStrip1.SelectTabById('one');
     lbl_Errors.innerText = "Percentage Should Not be Greater than 100";//objResource.GetMsg("Msg_txt_Percentage");    
     txt_HamaliofDel.disabled=false;
     txt_HamaliofDel.focus();
  }   
   else if(parseFloat(txt_Demurrage.value) > 100 && (control_is_mandatory(txt_Demurrage) == true))
  {
     WucBranchRateParameters1_TabStrip1.SelectTabById('one');
     lbl_Errors.innerText = "Percentage Should Not be Greater than 100";// objResource.GetMsg("Msg_txt_Percentage"); 
     txt_Demurrage.disabled=false;
     txt_Demurrage.focus();
  }  
  else
      ATS = true;

return ATS;
}