// JScript File

    function Calculate_FuelAmount(txt_Qty,txt_Rate,txt_FuelAmt,txt_OilAmt,lbl_TotAmt,hdn_FuelAmt,hdn_TotAmt)
    {

        var Qty = parseFloat(txt_Qty.value);
        var Rate  = parseFloat(txt_Rate.value);
        var FuelAmt = parseFloat(txt_FuelAmt.value);
        var OilAmt  = parseFloat(txt_OilAmt.value);
        var TotAmt  = parseFloat(lbl_TotAmt.innerHTML);

        if (isNaN(Qty)) Qty = 0;
        if (isNaN(Rate)) Rate = 0;
        if (isNaN(FuelAmt)) FuelAmt = 0;
        if (isNaN(OilAmt)) OilAmt = 0;
        if (isNaN(TotAmt)) TotAmt = 0;

        if(Qty > 0 && Rate > 0)
        {
            var FuelAmount = Math.round(((parseFloat(Qty) * parseFloat(Rate))/1)*100)/100;
        }
        else
            var FuelAmount = 0;
        
        var TotAmount = Math.round(((parseFloat(FuelAmount) + parseFloat(OilAmt))/1)*100)/100;

        hdn_FuelAmt.value = FuelAmount;
        hdn_TotAmt.value = TotAmount;
        
        txt_FuelAmt.value = hdn_FuelAmt.value;
        lbl_TotAmt.innerHTML = hdn_TotAmt.value;
    }


 function Calculate_FuelRate(txt_Qty,txt_Rate,txt_FuelAmt,txt_OilAmt,lbl_TotAmt,hdn_FuelAmt,hdn_TotAmt,hdn_FuelRate)
    {

        var Qty = parseFloat(txt_Qty.value);
        var Rate  = parseFloat(txt_Rate.value);
        var FuelAmt = parseFloat(txt_FuelAmt.value);
        var OilAmt  = parseFloat(txt_OilAmt.value);
        var TotAmt  = parseFloat(lbl_TotAmt.innerHTML);

        if (isNaN(Qty)) Qty = 0;
        if (isNaN(Rate)) Rate = 0;
        if (isNaN(FuelAmt)) FuelAmt = 0;
        if (isNaN(OilAmt)) OilAmt = 0;
        if (isNaN(TotAmt)) TotAmt = 0;

        if(FuelAmt > 0 && Qty > 0)
        {
            var FuelRate = Math.round(((parseFloat(FuelAmt) / parseFloat(Qty))/1)*100)/100;
        }
        else
            var FuelRate = 0;
            
        var TotAmount = Math.round(((parseFloat(FuelAmt) + parseFloat(OilAmt))/1)*100)/100;
        
        hdn_FuelRate.value = FuelRate;
        hdn_TotAmt.value = TotAmount;
        
        txt_Rate.value = hdn_FuelRate.value;
        lbl_TotAmt.innerHTML = hdn_TotAmt.value;
        
        
    }


function Calculate_HireChallansKM(txt_StartKM,txt_EndKM,lbl_KMRun,hdn_KMRun)
{

        var StartKM = txt_StartKM.value;
        var EndKM  = txt_EndKM.value;
        var TotKMRun  = lbl_KMRun.innerHTML;

        if (isNaN(StartKM) || StartKM=="") StartKM = 0;
        if (isNaN(EndKM) || EndKM=="") EndKM = 0;
        if (isNaN(TotKMRun)) TotKMRun = 0;

        if (parseFloat(EndKM) > parseFloat(StartKM))
        {
            var TotalKMRun = parseFloat(EndKM) - parseFloat(StartKM);
        }
        else
        TotalKMRun = 0;
        
        hdn_KMRun.value = TotalKMRun;
        lbl_KMRun.innerHTML = hdn_KMRun.value;
}


function Allow_To_Save_TripSettlement()
{

  var txt_TripSettlementNo = document.getElementById('txt_TripSettlementNo');
  var DDL_Vehicle = document.getElementById('WucVehicleNo_ddl_Vehicle');
  var txt_Vehicle_Last_4_Digits=document.getElementById('WucVehicleNo_txt_Vehicle_Last_4_Digits');
  var ddl_Driver = document.getElementById('ddl_Driver_txtBoxddl_Driver');
  var lbl_Errors=document.getElementById('lbl_Errors');
//  var dtp_TripStartDate = document.getElementById('dtp_TripStartDate_Picker_selecteddates');
//  var dtp_EndDate=document.getElementById('dtp_TripEndDate_Picker_selecteddates');
    var objResource=new Resource('hdf_ResourecString');

    lbl_Errors.innerHTML = '';
    var ATS=false;
    
//     if (txt_TripSettlementNo.value =='')
//      {
//       lbl_Errors.innerHTML = objResource.GetMsg("Msg_SettlementNo");
//      }
//     else 
     if (DDL_Vehicle.options.length == 0 || DDL_Vehicle.value=='0')
      {
      lbl_Errors.innerHTML = objResource.GetMsg("Msg_VehicleID");
      txt_Vehicle_Last_4_Digits.focus();
      }
     else if(ddl_Driver.value=='')
      {
      lbl_Errors.innerHTML = objResource.GetMsg("Msg_DriverId");
      ddl_Driver.focus();
      }
//    else if(dtp_EndDate.GetSelectedDate() < dtp_StartDate.GetSelectedDate())
//      {
//      lbl_Errors.innerHTML = "Please Select Driver"; 
//      }
    else
      {
        ATS=true;
      }
      
      return ATS;
}


