// JScript File
  function setFocusonSubTotal(lbtn_SubTotal)
    { 
        var lbtn_SubTotal = document.getElementById(lbtn_SubTotal);
        lbtn_SubTotal.focus();
    }
  function setFocusonOtherCharge(lbtn_OtherCharge)
    { 
        var lbtn_OtherCharge = document.getElementById(lbtn_OtherCharge);
        lbtn_OtherCharge.focus();
    }
    
  function Check_All(chk,gridname)
    {              
        var grid = document.getElementById(gridname);
        var i,j=0;
        var sum_gcsubtotal=0,sum_lrsertax=0,sum_Round_Off=0,sum_LRTotal=0,sum_othercharge=0,sum_servicetax=0,sum_octroiamt=0,sum_totgcamt=0,sum_totOFC=0,sum_totOSC=0;
        var checkbox,txt_Sub_Total,txt_LRSerTax,txt_Round_Off,txt_LRTotal,txt_Other_Charges,txt_Service_Tax,txt_Octroi_Amount,txt_Total_Amount,txt_Octroi_Form_Charges,txt_Octroi_Service_Charges;

        var lbl_totalsubtot = document.getElementById('WucTransportBill1_lbl_totalsubtot');
        var lbl_totalLRSerTax = document.getElementById('WucTransportBill1_lbl_totalLRSerTax');
        var lbl_totalRound_Off = document.getElementById('WucTransportBill1_lbl_totalRound_Off');
        var lbl_totalLRTotal = document.getElementById('WucTransportBill1_lbl_totalLRTotal');
        var lbl_totalothercharge = document.getElementById('WucTransportBill1_lbl_totalothercharge');
        var lbl_TotalOctroiFormCharge = document.getElementById('WucTransportBill1_lbl_TotalOctroiFormCharge');
        var lbl_TotalOctroiServiceCharge = document.getElementById('WucTransportBill1_lbl_TotalOctroiServiceCharge');
        var lbl_totalservicetax = document.getElementById('WucTransportBill1_lbl_totalservicetax');
        var lbl_totaloctamt = document.getElementById('WucTransportBill1_lbl_totaloctamt');
        var lbl_totalgcamount = document.getElementById('WucTransportBill1_lbl_totalgcamount');
        var lbl_totalgc = document.getElementById('WucTransportBill1_lbl_totalgc');

        var hdn_totalsubtot= document.getElementById('WucTransportBill1_hdn_totalsubtot');
        var hdn_totalLRSerTax= document.getElementById('WucTransportBill1_hdn_totalLRSerTax');
        var hdn_totalRound_Off= document.getElementById('WucTransportBill1_hdn_totalRound_Off');
        var hdn_totalLRTotal= document.getElementById('WucTransportBill1_hdn_totalLRTotal');
        var hdn_totalothercharge= document.getElementById('WucTransportBill1_hdn_totalothercharge');
        var hdn_TotalOctroiFormCharge = document.getElementById('WucTransportBill1_hdn_TotalOctroiFormCharge');
        var hdn_TotalOctroiServiceCharge = document.getElementById('WucTransportBill1_hdn_TotalOctroiServiceCharge');
        var hdn_totalservicetax= document.getElementById('WucTransportBill1_hdn_totalservicetax');
        var hdn_totaloctamt= document.getElementById('WucTransportBill1_hdn_totaloctamt');
        var hdn_totalgcamount= document.getElementById('WucTransportBill1_hdn_totalgcamount');
        var hdn_totalgc = document.getElementById('WucTransportBill1_hdn_totalgc');
        
        var max = (grid.rows.length - 1);
        for(i=1;i<grid.rows.length;i++)
        {            
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            txt_Sub_Total= grid.rows[i].cells[10].getElementsByTagName('input');
            txt_LRSerTax= grid.rows[i].cells[11].getElementsByTagName('input');
            txt_Round_Off= grid.rows[i].cells[12].getElementsByTagName('input');
            txt_LRTotal= grid.rows[i].cells[13].getElementsByTagName('input');
            txt_Other_Charges  = grid.rows[i].cells[14].getElementsByTagName('input');
            txt_Octroi_Form_Charges = grid.rows[i].cells[15].getElementsByTagName('input');
            txt_Octroi_Service_Charges = grid.rows[i].cells[16].getElementsByTagName('input');
            txt_Service_Tax = grid.rows[i].cells[17].getElementsByTagName('input');
            txt_Octroi_Amount = grid.rows[i].cells[18].getElementsByTagName('input');
            txt_Total_Amount = grid.rows[i].cells[19].getElementsByTagName('input');
            
//            txt_Octroi_Service_Charges = grid.rows[i].cells[13].getElementsByTagName('input');
//            txt_Service_Tax = grid.rows[i].cells[14].getElementsByTagName('input');
//            txt_Octroi_Amount = grid.rows[i].cells[15].getElementsByTagName('input');
//            txt_Total_Amount = grid.rows[i].cells[16].getElementsByTagName('input');
            
            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            }
                      
            if(chk.checked == true)
            {
                if(txt_Sub_Total[0].type =='text')
                {
                    sum_gcsubtotal = sum_gcsubtotal + val(txt_Sub_Total[0].value);
                }
                if(txt_LRSerTax[0].type =='text')
                {
                    sum_lrsertax = sum_lrsertax + val(txt_LRSerTax[0].value);
                }
                if(txt_Round_Off[0].type =='text')
                {
                    sum_Round_Off = sum_Round_Off + val(txt_Round_Off[0].value);
                }
                if(txt_LRTotal[0].type =='text')
                {
                    sum_LRTotal = sum_LRTotal + val(txt_LRTotal[0].value);
                }            
                if(txt_Other_Charges[0].type =='text')
                {
                    sum_othercharge = sum_othercharge + val(txt_Other_Charges[0].value);
                }
                if(txt_Service_Tax[0].type =='text')
                {
                    sum_servicetax = sum_servicetax + val(txt_Service_Tax[0].value);
                }
                if(txt_Octroi_Amount[0].type =='text')
                {
                    sum_octroiamt = sum_octroiamt + val(txt_Octroi_Amount[0].value);
                }
                if(txt_Total_Amount[0].type =='text')
                {
                    sum_totgcamt = sum_totgcamt + val(txt_Total_Amount[0].value);
                }
                if(txt_Octroi_Form_Charges[0].type =='text')
                {
                    sum_totOFC = sum_totOFC + val(txt_Octroi_Form_Charges[0].value);
                }
                if(txt_Octroi_Service_Charges[0].type =='text')
                {
                    sum_totOSC = sum_totOSC + val(txt_Octroi_Service_Charges[0].value);
                }
            }            
        }
        
        if(chk.checked == true)
        {
            lbl_totalsubtot.innerHTML  = roundNumber(sum_gcsubtotal,2);
            lbl_totalLRSerTax.innerHTML  = roundNumber(sum_lrsertax,2);
            lbl_totalRound_Off.innerHTML  = roundNumber(sum_Round_Off,2);
            lbl_totalLRTotal.innerHTML  = roundNumber(sum_LRTotal,2);
            lbl_totalothercharge.innerHTML  = roundNumber(sum_othercharge,2);
            lbl_totalservicetax.innerHTML  = roundNumber(sum_servicetax,2);
            lbl_totaloctamt.innerHTML  = sum_octroiamt;
            lbl_totalgcamount.innerHTML  = roundNumber(sum_totgcamt,2);
            lbl_totalgc.innerHTML = max;
            lbl_TotalOctroiFormCharge.innerHTML  = roundNumber(sum_totOFC,2);
            lbl_TotalOctroiServiceCharge.innerHTML  = roundNumber(sum_totOSC,2);
                    
            hdn_totalgc.value = max;
            hdn_totalsubtot.value = roundNumber(sum_gcsubtotal,2);
            hdn_totalLRSerTax.value = roundNumber(sum_lrsertax,2);
            hdn_totalRound_Off.value = roundNumber(sum_Round_Off,2);
            hdn_totalLRTotal.value = roundNumber(sum_LRTotal,2);
            hdn_totalothercharge.value = roundNumber(sum_othercharge,2);
            hdn_totalservicetax.value = roundNumber(sum_servicetax,2);
            hdn_totaloctamt.value = sum_octroiamt;
            hdn_totalgcamount.value = roundNumber(sum_totgcamt,2);
            hdn_TotalOctroiFormCharge.value  = roundNumber(sum_totOFC,2);
            hdn_TotalOctroiServiceCharge.value  = roundNumber(sum_totOSC,2);
        }
        else
        {
            lbl_totalgc.innerHTML = 0;
            lbl_totalsubtot.innerHTML  = 0;
            lbl_totalLRSerTax.innerHTML  = 0;
            lbl_totalRound_Off.innerHTML  = 0;
            lbl_totalLRTotal.innerHTML  = 0;
            lbl_totalothercharge.innerHTML  = 0;
            lbl_totalservicetax.innerHTML  = 0;
            lbl_totaloctamt.innerHTML  = 0;
            lbl_totalgcamount.innerHTML = 0;
            lbl_TotalOctroiFormCharge.innerHTML  = 0;
            lbl_TotalOctroiServiceCharge.innerHTML  = 0;
                    
            hdn_totalgc.value = 0;
            hdn_totalsubtot.value = 0;
            hdn_totalLRSerTax.value = 0;
            hdn_totalRound_Off.value = 0;
            hdn_totalLRTotal.value = 0;
            hdn_totalothercharge.value = 0;
            hdn_totalservicetax.value = 0;
            hdn_totaloctamt.value = 0;
            hdn_totalgcamount.value = 0;
            hdn_TotalOctroiFormCharge.value  = 0;
            hdn_TotalOctroiServiceCharge.value  = 0;
        }
    }
        
    function Check_Single(chk,gridname,callfrom)
    {
    
        if(callfrom == 1)
        {
            var grid = document.getElementById(gridname);
        }
        else
        { 
            var grid = document.getElementById('WucTransportBill1_dg_Bill');
        }
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;
        var txt_Sub_Total,txt_LRSerTax,txt_Round_Off,txt_LRTotal,txt_Other_Charges,txt_Service_Tax,txt_Octroi_Amount,txt_Total_Amount,txt_Octroi_Form_Charges,txt_Octroi_Service_Charges;;

        var lbl_totalsubtot = document.getElementById('WucTransportBill1_lbl_totalsubtot');
        var lbl_totalLRSerTax = document.getElementById('WucTransportBill1_lbl_totalLRSerTax');
        var lbl_totalRound_Off = document.getElementById('WucTransportBill1_lbl_totalRound_Off');
        var lbl_totalLRTotal = document.getElementById('WucTransportBill1_lbl_totalLRTotal');
        var lbl_totalothercharge = document.getElementById('WucTransportBill1_lbl_totalothercharge');
        var lbl_totalservicetax = document.getElementById('WucTransportBill1_lbl_totalservicetax');
        var lbl_totaloctamt = document.getElementById('WucTransportBill1_lbl_totaloctamt');
        var lbl_totalgcamount = document.getElementById('WucTransportBill1_lbl_totalgcamount');
        var lbl_totalgc = document.getElementById('WucTransportBill1_lbl_totalgc');
        var lbl_TotalOctroiFormCharge = document.getElementById('WucTransportBill1_lbl_TotalOctroiFormCharge');
        var lbl_TotalOctroiServiceCharge = document.getElementById('WucTransportBill1_lbl_TotalOctroiServiceCharge');
        var lbl_Total_Additional_Charges = document.getElementById('WucTransportBill1_lbl_Total_Additional_Charges');
        var lbl_TotalAmount = document.getElementById('WucTransportBill1_lbl_TotalAmount');

        var hdn_totalsubtot = document.getElementById('WucTransportBill1_hdn_totalsubtot');
        var hdn_totalLRSerTax = document.getElementById('WucTransportBill1_hdn_totalLRSerTax');
        var hdn_totalRound_Off = document.getElementById('WucTransportBill1_hdn_totalRound_Off');
        var hdn_totalLRTotal = document.getElementById('WucTransportBill1_hdn_totalLRTotal');
        var hdn_totalothercharge = document.getElementById('WucTransportBill1_hdn_totalothercharge');
        var hdn_totalservicetax = document.getElementById('WucTransportBill1_hdn_totalservicetax');
        var hdn_totaloctamt = document.getElementById('WucTransportBill1_hdn_totaloctamt');
        var hdn_totalgcamount= document.getElementById('WucTransportBill1_hdn_totalgcamount');
        var hdn_totalgc = document.getElementById('WucTransportBill1_hdn_totalgc');
        var hdn_TotalOctroiFormCharge = document.getElementById('WucTransportBill1_hdn_TotalOctroiFormCharge');
        var hdn_TotalOctroiServiceCharge = document.getElementById('WucTransportBill1_hdn_TotalOctroiServiceCharge');
        var hdn_Total_Additional_Charges = document.getElementById('WucTransportBill1_hdn_Total_Additional_Charges');
        var hdn_TotalAmount = document.getElementById('WucTransportBill1_hdn_TotalAmount');
 
//        txt_Sub_Total= row.cells[10].getElementsByTagName('input');
//        txt_Other_Charges  = row.cells[11].getElementsByTagName('input');
//        txt_Octroi_Form_Charges = row.cells[12].getElementsByTagName('input');
//        txt_Octroi_Service_Charges = row.cells[16].getElementsByTagName('input');
//        txt_Service_Tax = row.cells[17].getElementsByTagName('input');
//        txt_Octroi_Amount = row.cells[18].getElementsByTagName('input');
//        txt_Total_Amount = row.cells[19].getElementsByTagName('input');
        
        txt_Sub_Total = row.cells[10].getElementsByTagName('input');
        txt_LRSerTax = row.cells[11].getElementsByTagName('input');
        txt_Round_Off = row.cells[12].getElementsByTagName('input');
        txt_LRTotal = row.cells[13].getElementsByTagName('input'); 
        txt_Other_Charges  = row.cells[14].getElementsByTagName('input');
        txt_Octroi_Form_Charges = row.cells[15].getElementsByTagName('input');
        txt_Octroi_Service_Charges = row.cells[16].getElementsByTagName('input');
        txt_Service_Tax = row.cells[17].getElementsByTagName('input');
        txt_Octroi_Amount = row.cells[18].getElementsByTagName('input');
        txt_Total_Amount = row.cells[19].getElementsByTagName('input'); 
       
//        txt_Octroi_Service_Charges = row.cells[13].getElementsByTagName('input');
//        txt_Service_Tax = row.cells[14].getElementsByTagName('input');
//        txt_Octroi_Amount = row.cells[15].getElementsByTagName('input');
//        txt_Total_Amount = row.cells[16].getElementsByTagName('input');
       
        
        if(chk.checked == true)
        {
           lbl_totalgc.innerHTML = val(lbl_totalgc.innerHTML) + 1;
           lbl_totalsubtot.innerHTML = roundNumber(val(lbl_totalsubtot.innerHTML) + val(txt_Sub_Total[0].value),2);
           lbl_totalLRSerTax.innerHTML = roundNumber(val(lbl_totalLRSerTax.innerHTML) + val(txt_LRSerTax[0].value),2);
           lbl_totalRound_Off.innerHTML = roundNumber(val(lbl_totalRound_Off.innerHTML) + val(txt_Round_Off[0].value),2);
           lbl_totalLRTotal.innerHTML = roundNumber(val(lbl_totalLRTotal.innerHTML) + val(txt_LRTotal[0].value),2);
           lbl_totalothercharge.innerHTML =roundNumber(val(lbl_totalothercharge.innerHTML) + val(txt_Other_Charges[0].value),2);
           lbl_totalservicetax.innerHTML = roundNumber(val(lbl_totalservicetax.innerHTML) + val(txt_Service_Tax[0].value),2);
           lbl_totaloctamt.innerHTML = val(lbl_totaloctamt.innerHTML) + val(txt_Octroi_Amount[0].value);
           lbl_totalgcamount.innerHTML = roundNumber(val(lbl_totalgcamount.innerHTML) + val(txt_Total_Amount[0].value),2);
           lbl_TotalOctroiFormCharge.innerHTML = roundNumber(val(lbl_TotalOctroiFormCharge.innerHTML) + val(txt_Octroi_Form_Charges[0].value),2);
           lbl_TotalOctroiServiceCharge.innerHTML = roundNumber(val(lbl_TotalOctroiServiceCharge.innerHTML) + val(txt_Octroi_Service_Charges[0].value),2);

           hdn_totalgc.value = val(hdn_totalgc.value) + 1;
           hdn_totalsubtot.value = roundNumber(val(hdn_totalsubtot.value) + val(txt_Sub_Total[0].value),2);
           hdn_totalLRSerTax.value = roundNumber(val(hdn_totalLRSerTax.value) + val(txt_LRSerTax[0].value),2);
           hdn_totalRound_Off.value = roundNumber(val(hdn_totalRound_Off.value) + val(txt_Round_Off[0].value),2);
           hdn_totalLRTotal.value = roundNumber(val(hdn_totalLRTotal.value) + val(txt_LRTotal[0].value),2);          
           hdn_totalothercharge.value = roundNumber(val(hdn_totalothercharge.value) + val(txt_Other_Charges[0].value),2);
           hdn_totalservicetax.value = roundNumber(val(hdn_totalservicetax.value) + val(txt_Service_Tax[0].value),2);
           hdn_totaloctamt.value = val(hdn_totaloctamt.value) + val(txt_Octroi_Amount[0].value);
           hdn_totalgcamount.value = roundNumber(val(hdn_totalgcamount.value) + val(txt_Total_Amount[0].value),2);
           hdn_TotalOctroiFormCharge.value = roundNumber(val(hdn_TotalOctroiFormCharge.value) + val(txt_Octroi_Form_Charges[0].value),2);
           hdn_TotalOctroiServiceCharge.value = roundNumber(val(hdn_TotalOctroiServiceCharge.value) + val(txt_Octroi_Service_Charges[0].value),2);
           
        }
        else
        {
           lbl_totalgc.innerHTML = val(lbl_totalgc.innerHTML) - 1;
           lbl_totalsubtot.innerHTML = roundNumber(val(lbl_totalsubtot.innerHTML) - val(txt_Sub_Total[0].value),2);
           lbl_totalLRSerTax.innerHTML = roundNumber(val(lbl_totalLRSerTax.innerHTML) - val(txt_LRSerTax[0].value),2);
           lbl_totalRound_Off.innerHTML = roundNumber(val(lbl_totalRound_Off.innerHTML) - val(txt_Round_Off[0].value),2);
           lbl_totalLRTotal.innerHTML = roundNumber(val(lbl_totalLRTotal.innerHTML) - val(txt_LRTotal[0].value),2); 
           lbl_totalothercharge.innerHTML = roundNumber(val(lbl_totalothercharge.innerHTML) - val(txt_Other_Charges[0].value),2);
           lbl_totalservicetax.innerHTML = roundNumber(val(lbl_totalservicetax.innerHTML) - val(txt_Service_Tax[0].value),2);
           lbl_totaloctamt.innerHTML = val(lbl_totaloctamt.innerHTML) - val(txt_Octroi_Amount[0].value);
           lbl_totalgcamount.innerHTML = roundNumber(val(lbl_totalgcamount.innerHTML) - val(txt_Total_Amount[0].value),2);
           lbl_TotalOctroiFormCharge.innerHTML = roundNumber(val(lbl_TotalOctroiFormCharge.innerHTML) - val(txt_Octroi_Form_Charges[0].value),2);
           lbl_TotalOctroiServiceCharge.innerHTML = roundNumber(val(lbl_TotalOctroiServiceCharge.innerHTML) - val(txt_Octroi_Service_Charges[0].value),2);

           hdn_totalgc.value = val(hdn_totalgc.value) - 1;
           hdn_totalsubtot.value = roundNumber(val(hdn_totalsubtot.value) - val(txt_Sub_Total[0].value),2);
           hdn_totalLRSerTax.value = roundNumber(val(hdn_totalLRSerTax.value) - val(txt_LRSerTax[0].value),2);
           hdn_totalRound_Off.value = roundNumber(val(hdn_totalRound_Off.value) - val(txt_Round_Off[0].value),2);
           hdn_totalLRTotal.value = roundNumber(val(hdn_totalLRTotal.value) - val(txt_LRTotal[0].value),2); 
           hdn_totalothercharge.value = roundNumber(val(hdn_totalothercharge.value) - val(txt_Other_Charges[0].value),2);
           hdn_totalservicetax.value = roundNumber(val(hdn_totalservicetax.value) - val(txt_Service_Tax[0].value),2);
           hdn_totaloctamt.value = val(hdn_totaloctamt.value) - val(txt_Octroi_Amount[0].value);
           hdn_totalgcamount.value = roundNumber(val(hdn_totalgcamount.value) - val(txt_Total_Amount[0].value),2);
           hdn_TotalOctroiFormCharge.value = roundNumber(val(hdn_TotalOctroiFormCharge.value) - val(txt_Octroi_Form_Charges[0].value),2);
           hdn_TotalOctroiServiceCharge.value = roundNumber(val(hdn_TotalOctroiServiceCharge.value) - val(txt_Octroi_Service_Charges[0].value),2);

        }
           lbl_Total_Additional_Charges.innerHTML = val(lbl_totalgcamount.innerHTML) + val(lbl_TotalAmount.innerHTML);
           hdn_Total_Additional_Charges.value = val(hdn_totalgcamount.value) + val(hdn_TotalAmount.value);

        if((grid.rows.length-1) == val(hdn_totalgc.value))
        {
            checkall[0].checked = true;
        }
        else
        {
            checkall[0].checked = false;
        }
    }    
    

    function Allow_To_Save()
    {
        var ATS = false;
        
        var ddl_BillType = document.getElementById('WucTransportBill1_ddl_BillType');
        var txt_Client = document.getElementById('WucTransportBill1_ddl_Client_txtBoxddl_Client');
        var hdn_totalgc = document.getElementById('WucTransportBill1_hdn_totalgc');        
        var lbl_Error_Client = document.getElementById('WucTransportBill1_lbl_Error_Client');
        
        lbl_Error_Client.innerHTML ="";                
         
        if(val(hdn_totalgc.value) == 0)
        {
            lbl_Error_Client.innerHTML = "Please Select Atleast One GC";
        }
        else if (val(ddl_BillType.value) <= 0)
        {
            lbl_Error_Client.innerHTML =  "Please Select Bill Type";
            ddl_BillType.focus();
        }       
        else if(txt_Client.value == '')
        {
            lbl_Error_Client.innerHTML = "Please Select Client";
            txt_Client.focus();
        }               
        else
        {
            ATS = true;
        }
        return ATS;
     }    



 function Hide_Controls()
 {
    var txt_IBACharges = document.getElementById('txt_IBACharges');
    var lbl_IBACharges = document.getElementById('lbl_IBACharges');    
    var hdn_Is_IBA = document.getElementById('hdn_Is_IBA');
    var tr_LenghtCharge = document.getElementById('tr_LenghtCharge');
    var tr_IBACharges = document.getElementById('tr_IBACharges');
     
    
    if ( hdn_Is_IBA.value == 'True' )
    {
        txt_IBACharges.style.visibility = 'visible';
        lbl_IBACharges.style.visibility = 'visible';
        
        tr_IBACharges.style.display = "inline";
    }
    else
    {
        txt_IBACharges.style.visibility = 'hidden';
        lbl_IBACharges.style.visibility = 'hidden';
        
        tr_IBACharges.style.display = "none";
    }    
 }
 
 function HideLabel()
 {
        var td_OFC = document.getElementById('WucTransportBill1_td_OFC');
        var td_OSC = document.getElementById('WucTransportBill1_td_OSC');
        var td_SubTotal = document.getElementById('WucTransportBill1_td_SubTotal');
        var td_OtherCharge = document.getElementById('WucTransportBill1_td_OtherCharge');
        var td_ServiceTax = document.getElementById('WucTransportBill1_td_ServiceTax');
        var td_OctroiAmount = document.getElementById('WucTransportBill1_td_OctroiAmount');
        var td_GCAmount = document.getElementById('WucTransportBill1_td_GCAmount');
        
        var rbtn_TransBillFor = document.getElementById('WucTransportBill1_rbtn_TransBillFor');
        
        var rbtn_TransBillFor_Frieght = document.getElementById('WucTransportBill1_rbtn_TransBillFor_0');
        var rbtn_TransBillFor_Octroi = document.getElementById('WucTransportBill1_rbtn_TransBillFor_1');
        var rbtn_TransBillFor_Both = document.getElementById('WucTransportBill1_rbtn_TransBillFor_2');

        if(rbtn_TransBillFor_Frieght.checked == true)
        {
            td_OFC.style.display = 'none';
            td_OSC.style.display = 'none';
            td_OctroiAmount.style.display = 'none';
        }
        else
        {
            td_OFC.style.display = 'inline';
            td_OSC.style.display = 'inline';
            td_OctroiAmount.style.display = 'inline';
        }
        
        if(rbtn_TransBillFor_Octroi.checked == true)
        {
            td_SubTotal.style.display = 'none';
            td_OtherCharge.style.display = 'none';
        }
        else
        {
            td_SubTotal.style.display = 'inline';
            td_OtherCharge.style.display = 'inline';
        }
        
        if(rbtn_TransBillFor_Both.checked == true)
        {
            td_OFC.style.display = 'inline';
            td_OSC.style.display = 'inline';
            td_OctroiAmount.style.display = 'inline';
            td_SubTotal.style.display = 'inline';
            td_OtherCharge.style.display = 'inline';
        }
 
}