
// JScript File

    function Check_All(chk,gridname)
    {
        var grid = document.getElementById(gridname);
        var checkbox,Freight,Weight,Articles,GCAmount,SerTaxAmt;
        var i,j=0;
        var sum_TBA=0,sum_TBW=0,sum_LA=0,sum_LW=0,sum_RA=0,sum_RW=0,sum_DLA=0,sum_DLV=0;
        var sum_GD=0,sum_UCR=0,sum_SCh=0,sum_UCC=0;
        var Actual_Art,Actual_Wt,Loaded_Art,Loaded_Wt,Rec_Art,Rec_Wt,Damage_Article,Damage_Value,CommValve;

        var lbl_Total_Booking_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Booking_Articles');  
        var lbl_Total_Booking_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Booking_Articles_Wt');  
        var lbl_Total_Loaded_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Loaded_Articles');  
        var lbl_Total_Loaded_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Loaded_Articles_Wt');  
        var lbl_Total_Received_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Received_Articles');  
        var lbl_Total_Received_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Received_Articles_Wt');  
        var lbl_Total_Damage_Leakage_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Damage_Leakage_Articles');  
        var lbl_Total_Damage_Leakage_Value=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Damage_Leakage_Value');  
        var lbl_Total_GC = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_GC');
        
        var lbl_To_Pay_Value = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_To_Pay_Value');
        var lbl_UpcountryReceivableValue = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_UpcountryReceivableValue');
        var lbl_Total_Receivable_Value = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Receivable_Value');
        var lbl_Total_Delivery_Commision = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Delivery_Commision');
        var lbl_UpcountryCrossingCostValue = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_UpcountryCrossingCostValue');
        var txt_Lorry_Hire = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_txt_Lorry_Hire');
        var txt_Others_Payables = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_txt_Others_Payables');
        var lbl_Total_Payable_Value = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Payable_Value');

        var hdn_Total_Booking_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Booking_Articles');  
        var hdn_Total_Booking_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Booking_Articles_Wt');  
        var hdn_Total_Loaded_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Loaded_Articles');  
        var hdn_Total_Loaded_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Loaded_Articles_Wt');  
        var hdn_Total_Received_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Received_Articles');  
        var hdn_Total_Received_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Received_Articles_Wt');  
        var hdn_Total_Damage_Leakage_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Damage_Leakage_Articles');  
        var hdn_Total_Damage_Leakage_Value=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Damage_Leakage_Value');  
        var hdn_Total_GC = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_GC');
        
        var hdn_To_Pay = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_To_Pay');
        var hdn_UpcountryReceivable = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_UpcountryReceivable');
        var hdn_Total_Receivable_Value = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Receivable_Value');
        var hdn_Total_Delivery_Commision = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Delivery_Commision');
        var hdn_UpcountryCrossingCost = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_UpcountryCrossingCost');
        var hdn_Total_Payable_Value = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Payable_Value');

        var max = (grid.rows.length - 1);
        for(i=1;i<grid.rows.length;i++)
        {
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            Actual_Art= grid.rows[i].cells[7].getElementsByTagName('input');
            Actual_Wt= grid.rows[i].cells[8].getElementsByTagName('input');
            Loaded_Art  = grid.rows[i].cells[11].getElementsByTagName('input');
            Loaded_Wt = grid.rows[i].cells[12].getElementsByTagName('input');
            Rec_Art  = grid.rows[i].cells[13].getElementsByTagName('input');
            Rec_Wt = grid.rows[i].cells[14].getElementsByTagName('input');
            Damage_Article= grid.rows[i].cells[16].getElementsByTagName('input');
            Damage_Value= grid.rows[i].cells[17].getElementsByTagName('input');
            CommValve= grid.rows[i].cells[18].getElementsByTagName('input');
                    
            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            }

            if(chk.checked == true)
            {
                if(Actual_Art[0].type =='text')
                {
                    sum_TBA = sum_TBA + val(Actual_Art[0].value);
                }
                if(Actual_Wt[0].type =='text')
                {
                    sum_TBW = sum_TBW + val(Actual_Wt[0].value);
                }               
                if(Loaded_Art[0].type =='text')
                {
                    sum_LA = sum_LA + val(Loaded_Art[0].value);
                }
                if(Loaded_Wt[0].type =='text')
                {
                    sum_LW = sum_LW + val(Loaded_Wt[0].value);
                }
                if(Rec_Art[0].type =='text')
                {
                    sum_RA = sum_RA + val(Rec_Art[0].value);
                }
                if(Rec_Wt[0].type =='text')
                {
                    sum_RW = sum_RW + val(Rec_Wt[0].value);
                }
                if(Damage_Article[0].type =='text')
                {
                    sum_DLA = sum_DLA + val(Damage_Article[0].value);
                }
                if(Damage_Value[0].type =='text')
                {
                    sum_DLV = sum_DLV + val(Damage_Value[0].value);
                }
                
                if(CommValve[0].type =='hidden')
                {
                    sum_GD = sum_GD + val(CommValve[0].value);
                }
                if(CommValve[1].type =='hidden')
                {
                    sum_UCR = sum_UCR + val(CommValve[1].value);
                }
                if(CommValve[2].type =='hidden')
                {
                    sum_SCh = sum_SCh + val(CommValve[2].value);
                }
                if(CommValve[3].type =='hidden')
                {
                    sum_UCC = sum_UCC + val(CommValve[3].value);
                }
            }
        }
        
        if(chk.checked == true)
        {
            lbl_Total_Booking_Articles.innerHTML = sum_TBA;
            lbl_Total_Booking_Articles_Wt.innerHTML = sum_TBW;
            lbl_Total_Loaded_Articles.innerHTML = sum_LA;
            lbl_Total_Loaded_Articles_Wt.innerHTML  = sum_LW;
            lbl_Total_Received_Articles.innerHTML  = sum_RA;
            lbl_Total_Received_Articles_Wt.innerHTML  = sum_RW;
            lbl_Total_Damage_Leakage_Articles.innerHTML  = sum_DLA;
            lbl_Total_Damage_Leakage_Value.innerHTML  = sum_DLV;
            lbl_Total_GC.innerHTML = max;

            lbl_To_Pay_Value.innerHTML  = sum_GD;
            lbl_UpcountryReceivableValue.innerHTML  = sum_UCR;
            lbl_Total_Receivable_Value.innerHTML  = val(sum_GD) + val(sum_UCR);
            lbl_Total_Delivery_Commision.innerHTML  = sum_SCh;
            lbl_UpcountryCrossingCostValue.innerHTML  = sum_UCC;
            lbl_Total_Payable_Value.innerHTML = val(sum_SCh) + val(sum_UCC) + val(txt_Lorry_Hire.value) + val(txt_Others_Payables.value);
            
            hdn_Total_GC.value = max;
            hdn_Total_Booking_Articles.value = sum_TBA;
            hdn_Total_Booking_Articles_Wt.value = sum_TBW;
            hdn_Total_Loaded_Articles.value = sum_LA;
            hdn_Total_Loaded_Articles_Wt.value  = sum_LW;
            hdn_Total_Received_Articles.value = sum_RA;
            hdn_Total_Received_Articles_Wt.value = sum_RW;
            hdn_Total_Damage_Leakage_Articles.value = sum_DLA;
            hdn_Total_Damage_Leakage_Value.value = sum_DLV;
            
            hdn_To_Pay.value = sum_GD;
            hdn_UpcountryReceivable.value  = sum_UCR;
            hdn_Total_Receivable_Value.value = val(sum_GD) + val(sum_UCR);
            hdn_Total_Delivery_Commision.value = sum_SCh;
            hdn_UpcountryCrossingCost.value = sum_UCC;
            hdn_Total_Payable_Value.value = val(sum_SCh) + val(sum_UCC) + val(txt_Lorry_Hire.value) + val(txt_Others_Payables.value);
        }
        else
        {
            lbl_Total_Booking_Articles.innerHTML = 0;
            lbl_Total_Booking_Articles_Wt.innerHTML = 0;
            lbl_Total_Loaded_Articles.innerHTML = 0;
            lbl_Total_Loaded_Articles_Wt.innerHTML  = 0;
            lbl_Total_Received_Articles.innerHTML  = 0;
            lbl_Total_Received_Articles_Wt.innerHTML  = 0;
            lbl_Total_Damage_Leakage_Articles.innerHTML  = 0;
            lbl_Total_Damage_Leakage_Value.innerHTML  = 0;
            lbl_Total_GC.innerHTML = 0;
            
            lbl_To_Pay_Value.innerHTML  = 0;
            lbl_UpcountryReceivableValue.innerHTML  = 0;
            lbl_Total_Receivable_Value.innerHTML  = 0;
            lbl_Total_Delivery_Commision.innerHTML  = 0;
            lbl_UpcountryCrossingCostValue.innerHTML  = 0;
            lbl_Total_Payable_Value.innerHTML = 0;

            hdn_Total_GC.value = 0;
            hdn_Total_Booking_Articles.value = 0;
            hdn_Total_Booking_Articles_Wt.value = 0;
            hdn_Total_Loaded_Articles.value = 0;
            hdn_Total_Loaded_Articles_Wt.value  = 0;
            hdn_Total_Received_Articles.value = 0;
            hdn_Total_Received_Articles_Wt.value = 0;
            hdn_Total_Damage_Leakage_Articles.value = 0;
            hdn_Total_Damage_Leakage_Value.value = 0;
            
            hdn_To_Pay.value = 0;
            hdn_UpcountryReceivable.value  = 0;
            hdn_Total_Receivable_Value.value = 0;
            hdn_Total_Delivery_Commision.value = 0;
            hdn_UpcountryCrossingCost.value = 0;
            hdn_Total_Payable_Value.value = 0;
        }
        Calculate_Total_Receivable();
    }
        
    function Check_Single(chk,gridname,callfrom)
    {
        //callfrom means From checkbox else from Textbox
        if(callfrom == 1)
        {
            var grid = document.getElementById(gridname);
        }
        else
        {
            var grid = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_dg_OtherAgencyGCDetails');
        }
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;

        var Actual_Art,Actual_Wt,Loaded_Art,Loaded_Wt,Rec_Art,Rec_Wt,Dam_Article,Dam_Value;
        var CommValve,Goods_Dly_Rec,Upcountry_Rec,Service_charge_Payable,Upcountry_Crossing_Cost_Payable;

        var lbl_Total_Booking_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Booking_Articles');  
        var lbl_Total_Booking_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Booking_Articles_Wt');  
        var lbl_Total_Loaded_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Loaded_Articles');  
        var lbl_Total_Loaded_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Loaded_Articles_Wt');  
        var lbl_Total_Received_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Received_Articles');  
        var lbl_Total_Received_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Received_Articles_Wt');  
        var lbl_Total_Damage_Leakage_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Damage_Leakage_Articles');  
        var lbl_Total_Damage_Leakage_Value=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Damage_Leakage_Value');  
        var lbl_Total_GC = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_GC');
        
        var lbl_To_Pay_Value = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_To_Pay_Value');
        var lbl_UpcountryReceivableValue = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_UpcountryReceivableValue');
        var lbl_Total_Receivable_Value = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Receivable_Value');
        var lbl_Total_Delivery_Commision = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Delivery_Commision');
        var lbl_UpcountryCrossingCostValue = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_UpcountryCrossingCostValue');
        var txt_Lorry_Hire = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_txt_Lorry_Hire');
        var txt_Others_Payables = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_txt_Others_Payables');
        var lbl_Total_Payable_Value = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Payable_Value');

        var hdn_Total_Booking_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Booking_Articles');  
        var hdn_Total_Booking_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Booking_Articles_Wt');  
        var hdn_Total_Loaded_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Loaded_Articles');  
        var hdn_Total_Loaded_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Loaded_Articles_Wt');  
        var hdn_Total_Received_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Received_Articles');  
        var hdn_Total_Received_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Received_Articles_Wt');  
        var hdn_Total_Damage_Leakage_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Damage_Leakage_Articles');  
        var hdn_Total_Damage_Leakage_Value=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Damage_Leakage_Value');  
        var hdn_Total_GC = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_GC');
            
        var hdn_To_Pay = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_To_Pay');
        var hdn_UpcountryReceivable = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_UpcountryReceivable');
        var hdn_Total_Receivable_Value = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Receivable_Value');
        var hdn_Total_Delivery_Commision = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Delivery_Commision');
        var hdn_UpcountryCrossingCost = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_UpcountryCrossingCost');
        var hdn_Total_Payable_Value = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Payable_Value');
    
        Actual_Art= row.cells[7].getElementsByTagName('input');
        Actual_Wt= row.cells[8].getElementsByTagName('input');
        Bal_Article= row.cells[9].getElementsByTagName('input');
        Bal_Act_Wt= row.cells[10].getElementsByTagName('input');
        Loaded_Art  = row.cells[11].getElementsByTagName('input');
        Loaded_Wt = row.cells[12].getElementsByTagName('input');
        Rec_Art  = row.cells[13].getElementsByTagName('input');
        Rec_Wt = row.cells[14].getElementsByTagName('input');
        Dam_Article= row.cells[16].getElementsByTagName('input');
        Dam_Value= row.cells[17].getElementsByTagName('input');
        CommValve= row.cells[18].getElementsByTagName('input');
            
        Goods_Dly_Rec = CommValve[0].value;
        Upcountry_Rec = CommValve[1].value;
        Service_charge_Payable = CommValve[2].value;
        Upcountry_Crossing_Cost_Payable = CommValve[3].value;
            
            if(val(Loaded_Art[0].value) > val(Bal_Article[0].value))
            {
                Loaded_Art[0].value = val(Bal_Article[0].value);
            }
            if(val(Loaded_Wt[0].value) > val(Bal_Act_Wt[0].value))
            {
                Loaded_Wt[0].value = val(Bal_Act_Wt[0].value);
            }
            if(val(Rec_Art[0].value) > val(Loaded_Art[0].value))
            {
                Rec_Art[0].value = val(Loaded_Art[0].value);
            }
            if(val(Rec_Wt[0].value) > val(Loaded_Wt[0].value))
            {
                Rec_Wt[0].value = val(Loaded_Wt[0].value);
            }
            if(val(Dam_Article[0].value) > val(Rec_Art[0].value))
            {
                Dam_Article[0].value = val(Rec_Art[0].value);
            }

            if(callfrom == 1)
            {
                if(chk.checked == true)
                {
                   lbl_Total_GC.innerHTML = val(lbl_Total_GC.innerHTML) + 1;
                   lbl_Total_Booking_Articles.innerHTML = val(lbl_Total_Booking_Articles.innerHTML) + val(Actual_Art[0].value);
                   lbl_Total_Booking_Articles_Wt.innerHTML = val(lbl_Total_Booking_Articles_Wt.innerHTML) + val(Actual_Wt[0].value);
                   lbl_Total_Loaded_Articles.innerHTML = val(lbl_Total_Loaded_Articles.innerHTML) + val(Loaded_Art[0].value);
                   lbl_Total_Loaded_Articles_Wt.innerHTML = val(lbl_Total_Loaded_Articles_Wt.innerHTML) + val(Loaded_Wt[0].value);
                   lbl_Total_Received_Articles.innerHTML = val(lbl_Total_Received_Articles.innerHTML) + val(Rec_Art[0].value);
                   lbl_Total_Received_Articles_Wt.innerHTML = val(lbl_Total_Received_Articles_Wt.innerHTML) + val(Rec_Wt[0].value);
                   lbl_Total_Damage_Leakage_Articles.innerHTML = val(lbl_Total_Damage_Leakage_Articles.innerHTML) + val(Dam_Article[0].value);
                   lbl_Total_Damage_Leakage_Value.innerHTML = val(lbl_Total_Damage_Leakage_Value.innerHTML) + val(Dam_Value[0].value);
               
                   lbl_To_Pay_Value.innerHTML = val(lbl_To_Pay_Value.innerHTML) + val(Goods_Dly_Rec);
                   lbl_UpcountryReceivableValue.innerHTML = val(lbl_UpcountryReceivableValue.innerHTML) + val(Upcountry_Rec);
                   lbl_Total_Receivable_Value.innerHTML = val(lbl_To_Pay_Value.innerHTML) + val(lbl_UpcountryReceivableValue.innerHTML);
                   lbl_Total_Delivery_Commision.innerHTML = val(lbl_Total_Delivery_Commision.innerHTML) + val(Service_charge_Payable);
                   lbl_UpcountryCrossingCostValue.innerHTML = val(lbl_UpcountryCrossingCostValue.innerHTML) + val(Upcountry_Crossing_Cost_Payable);
                   lbl_Total_Payable_Value.innerHTML = val(lbl_Total_Delivery_Commision.innerHTML) + val(lbl_UpcountryCrossingCostValue.innerHTML) + val(txt_Lorry_Hire.value) + val(txt_Others_Payables.value);
    
                   hdn_Total_GC.value = val(hdn_Total_GC.value) + 1;
                   hdn_Total_Booking_Articles.value = val(hdn_Total_Booking_Articles.value) + val(Actual_Art[0].value);
                   hdn_Total_Booking_Articles_Wt.value = val(hdn_Total_Booking_Articles_Wt.value) + val(Actual_Wt[0].value);
                   hdn_Total_Loaded_Articles.value = val(hdn_Total_Loaded_Articles.value) + val(Loaded_Art[0].value);
                   hdn_Total_Loaded_Articles_Wt.value = val(hdn_Total_Loaded_Articles_Wt.value) + val(Loaded_Wt[0].value);
                   hdn_Total_Received_Articles.value = val(hdn_Total_Received_Articles.value) + val(Rec_Art[0].value);
                   hdn_Total_Received_Articles_Wt.value = val(hdn_Total_Received_Articles_Wt.value) + val(Rec_Wt[0].value);
                   hdn_Total_Damage_Leakage_Articles.value = val(hdn_Total_Damage_Leakage_Articles.value) + val(Dam_Article[0].value);
                   hdn_Total_Damage_Leakage_Value.value = val(hdn_Total_Damage_Leakage_Value.value) + val(Dam_Value[0].value);
                   
                   hdn_To_Pay.value = val(hdn_To_Pay.value) + val(Goods_Dly_Rec);
                   hdn_UpcountryReceivable.value = val(hdn_UpcountryReceivable.value) + val(Upcountry_Rec);
                   hdn_Total_Receivable_Value.value = val(hdn_To_Pay.value) + val(hdn_UpcountryReceivable.value);
                   hdn_Total_Delivery_Commision.value = val(hdn_Total_Delivery_Commision.value) + val(Service_charge_Payable);
                   hdn_UpcountryCrossingCost.value = val(hdn_UpcountryCrossingCost.value) + val(Upcountry_Crossing_Cost_Payable);
                   hdn_Total_Payable_Value.value = val(hdn_Total_Delivery_Commision.value) + val(hdn_UpcountryCrossingCost.value) + val(txt_Lorry_Hire.value) + val(txt_Others_Payables.value);
                }
                else
                {
                   lbl_Total_GC.innerHTML = val(lbl_Total_GC.innerHTML) - 1;
                   lbl_Total_Booking_Articles.innerHTML = val(lbl_Total_Booking_Articles.innerHTML) - val(Actual_Art[0].value);
                   lbl_Total_Booking_Articles_Wt.innerHTML = val(lbl_Total_Booking_Articles_Wt.innerHTML) - val(Actual_Wt[0].value);
                   lbl_Total_Loaded_Articles.innerHTML = val(lbl_Total_Loaded_Articles.innerHTML) - val(Loaded_Art[0].value);
                   lbl_Total_Loaded_Articles_Wt.innerHTML = val(lbl_Total_Loaded_Articles_Wt.innerHTML) - val(Loaded_Wt[0].value);
                   lbl_Total_Received_Articles.innerHTML = val(lbl_Total_Received_Articles.innerHTML) - val(Rec_Art[0].value);
                   lbl_Total_Received_Articles_Wt.innerHTML = val(lbl_Total_Received_Articles_Wt.innerHTML) - val(Rec_Wt[0].value);
                   lbl_Total_Damage_Leakage_Articles.innerHTML = val(lbl_Total_Damage_Leakage_Articles.innerHTML) - val(Dam_Article[0].value);
                   lbl_Total_Damage_Leakage_Value.innerHTML = val(lbl_Total_Damage_Leakage_Value.innerHTML) - val(Dam_Value[0].value);
                   
                   lbl_To_Pay_Value.innerHTML = val(lbl_To_Pay_Value.innerHTML) - val(Goods_Dly_Rec);
                   lbl_UpcountryReceivableValue.innerHTML = val(lbl_UpcountryReceivableValue.innerHTML) - val(Upcountry_Rec);
                   lbl_Total_Receivable_Value.innerHTML = val(lbl_To_Pay_Value.innerHTML) + val(lbl_UpcountryReceivableValue.innerHTML);
                   lbl_Total_Delivery_Commision.innerHTML = val(lbl_Total_Delivery_Commision.innerHTML) - val(Service_charge_Payable);
                   lbl_UpcountryCrossingCostValue.innerHTML = val(lbl_UpcountryCrossingCostValue.innerHTML) - val(Upcountry_Crossing_Cost_Payable);
                   lbl_Total_Payable_Value.innerHTML = val(lbl_Total_Delivery_Commision.innerHTML) + val(lbl_UpcountryCrossingCostValue.innerHTML) + val(txt_Lorry_Hire.value) + val(txt_Others_Payables.value);

                   hdn_Total_GC.value = val(hdn_Total_GC.value) - 1;
                   hdn_Total_Booking_Articles.value = val(hdn_Total_Booking_Articles.value) - val(Actual_Art[0].value);
                   hdn_Total_Booking_Articles_Wt.value = val(hdn_Total_Booking_Articles_Wt.value) - val(Actual_Wt[0].value);
                   hdn_Total_Loaded_Articles.value = val(hdn_Total_Loaded_Articles.value) - val(Loaded_Art[0].value);
                   hdn_Total_Loaded_Articles_Wt.value = val(hdn_Total_Loaded_Articles_Wt.value) - val(Loaded_Wt[0].value);
                   hdn_Total_Received_Articles.value = val(hdn_Total_Received_Articles.value) - val(Rec_Art[0].value);
                   hdn_Total_Received_Articles_Wt.value = val(hdn_Total_Received_Articles_Wt.value) - val(Rec_Wt[0].value);
                   hdn_Total_Damage_Leakage_Articles.value = val(hdn_Total_Damage_Leakage_Articles.value) - val(Dam_Article[0].value);
                   hdn_Total_Damage_Leakage_Value.value = val(hdn_Total_Damage_Leakage_Value.value) - val(Dam_Value[0].value);
                   
                   hdn_To_Pay.value = val(hdn_To_Pay.value) - val(Goods_Dly_Rec);
                   hdn_UpcountryReceivable.value = val(hdn_UpcountryReceivable.value) - val(Upcountry_Rec);
                   hdn_Total_Receivable_Value.value = val(hdn_To_Pay.value) + val(hdn_UpcountryReceivable.value);
                   hdn_Total_Delivery_Commision.value = val(hdn_Total_Delivery_Commision.value) - val(Service_charge_Payable);
                   hdn_UpcountryCrossingCost.value = val(hdn_UpcountryCrossingCost.value) - val(Upcountry_Crossing_Cost_Payable);
                   hdn_Total_Payable_Value.value = val(hdn_Total_Delivery_Commision.value) + val(hdn_UpcountryCrossingCost.value) + val(txt_Lorry_Hire.value) + val(txt_Others_Payables.value);
                }
            }
            
            if(callfrom == 2)       //onblur of  Loded Article
            {
                if(chk.checked == true)
                {
                    hdn_Total_Loaded_Articles.value = val(hdn_Total_Loaded_Articles.value) + val(Loaded_Art[0].value);
                    lbl_Total_Loaded_Articles.innerHTML = val(hdn_Total_Loaded_Articles.value);
                }
            }
            if(callfrom == 3)       //onblur of  Loded Weight
            {
                if(chk.checked == true)
                {
                    hdn_Total_Loaded_Articles_Wt.value = val(hdn_Total_Loaded_Articles_Wt.value) + val(Loaded_Wt[0].value);
                    lbl_Total_Loaded_Articles_Wt.innerHTML = val(hdn_Total_Loaded_Articles_Wt.value);
                }
            }
            if(callfrom == 4 && chk.checked == true)       //onfocus of  Loded Article
            {
                hdn_Total_Loaded_Articles.value = val(hdn_Total_Loaded_Articles.value) - val(Loaded_Art[0].value);
                lbl_Total_Loaded_Articles.innerHTML = val(hdn_Total_Loaded_Articles.value);
            }
            if(callfrom == 5 && chk.checked == true)        //onfocus of  Loded Weight
            {
                hdn_Total_Loaded_Articles_Wt.value = val(hdn_Total_Loaded_Articles_Wt.value) - val(Loaded_Wt[0].value);
                lbl_Total_Loaded_Articles_Wt.innerHTML = val(hdn_Total_Loaded_Articles_Wt.value);      
            }
            
            if(callfrom == 6)       //onblur of  Receive Article
            {
                if(chk.checked == true)
                {
                    hdn_Total_Received_Articles.value = val(hdn_Total_Received_Articles.value) + val(Rec_Art[0].value);
                    lbl_Total_Received_Articles.innerHTML = val(hdn_Total_Received_Articles.value);
                }
            }
            if(callfrom == 7)       //onblur of  Receive Weight
            {
                if(chk.checked == true)
                {
                    hdn_Total_Received_Articles_Wt.value = val(hdn_Total_Received_Articles_Wt.value) + val(Rec_Wt[0].value);
                    lbl_Total_Received_Articles_Wt.innerHTML = val(hdn_Total_Received_Articles_Wt.value);
                }
            }
            if(callfrom == 8 && chk.checked == true)       //onfocus of  Receive Article
            {
                hdn_Total_Received_Articles.value = val(hdn_Total_Received_Articles.value) - val(Rec_Art[0].value);
                lbl_Total_Received_Articles.innerHTML = val(hdn_Total_Received_Articles.value);
            }
            if(callfrom == 9 && chk.checked == true)        //onfocus of  Receive Weight
            {
                hdn_Total_Received_Articles_Wt.value = val(hdn_Total_Received_Articles_Wt.value) - val(Rec_Wt[0].value);
                lbl_Total_Received_Articles_Wt.innerHTML = val(hdn_Total_Received_Articles_Wt.value);      
            }
             if(callfrom == 10)       //onblur of  Damage Article
            {
                if(chk.checked == true)
                {
                    hdn_Total_Damage_Leakage_Articles.value = val(hdn_Total_Damage_Leakage_Articles.value) + val(Dam_Article[0].value);
                    lbl_Total_Damage_Leakage_Articles.innerHTML = val(hdn_Total_Damage_Leakage_Articles.value);
                }
            }
            if(callfrom == 11)       //onblur of  Damage value
            {
                if(chk.checked == true)
                {
                    hdn_Total_Damage_Leakage_Value.value = val(hdn_Total_Damage_Leakage_Value.value) + val(Dam_Value[0].value);
                    lbl_Total_Damage_Leakage_Value.innerHTML = val(hdn_Total_Damage_Leakage_Value.value);
                }
            }
            if(callfrom == 12 && chk.checked == true)       //onfocus of  Damage Article
            {
                hdn_Total_Damage_Leakage_Articles.value = val(hdn_Total_Damage_Leakage_Articles.value) - val(Dam_Article[0].value);
                lbl_Total_Damage_Leakage_Articles.innerHTML = val(hdn_Total_Damage_Leakage_Articles.value);
            }
            if(callfrom == 13 && chk.checked == true)        //onfocus of  Damage value
            {
                hdn_Total_Damage_Leakage_Value.value = val(hdn_Total_Damage_Leakage_Value.value) - val(Dam_Value[0].value);
                lbl_Total_Damage_Leakage_Value.innerHTML = val(hdn_Total_Damage_Leakage_Value.value);      
            }
            
            if((grid.rows.length-1) == val(hdn_Total_GC.value))
            {
                checkall[0].checked = true;
            }
            else
            {
                checkall[0].checked = false;
            }
    }
    
        function Calculate_Total_Receivable()
        {            
            var txt_Lorry_Hire = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_txt_Lorry_Hire');
            var txt_Others_Payables = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_txt_Others_Payables');
            var lbl_Total_Payable_Value = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Payable_Value');
            var hdn_Total_Delivery_Commision = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Delivery_Commision');
            var hdn_UpcountryCrossingCost = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_UpcountryCrossingCost');
            var hdn_Total_Payable_Value = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Payable_Value');

            hdn_Total_Payable_Value.value =  val(hdn_Total_Delivery_Commision.value) + val(hdn_UpcountryCrossingCost.value) +
                                                    val(txt_Lorry_Hire.value) + val(txt_Others_Payables.value);

            lbl_Total_Payable_Value.innerHTML  = val(hdn_Total_Payable_Value.value);
        }
      
        function Allow_To_Save()
        {   
            var ATS = false;
            var objResource=new Resource('WucAUSOtherAgencyUnloadingDetails1_hdf_ResourecString');
            var lbl_Errors = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Errors');
            var txt_VehicleNo = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_txt_VehicleNo');  
            var ddl_Agency = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_ddl_Agency');
            var txt_LHPO_No_For_Print = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_txt_LHPO_No_For_Print');
            var hdn_Total_GC = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_GC');
            var ddl_Reason_For_Late_Uploading= document.getElementById('WucAUSOtherAgencyUnloadingDetails1_ddl_Reason_For_Late_Uploading');
            var ddl_Supervisor = document.getElementById('WucAUSOtherAgencyUnloadingDetails1_ddl_Supervisor_txtBoxddl_Supervisor');
            
            if (val(Trim(ddl_Agency.value)) <= 0)
            {
                lbl_Errors.innerText = "Please Select Agency";
                ddl_Agency.focus();
            }   
            else if (Trim(txt_VehicleNo.value) == '')
            {  
                lbl_Errors.innerText = "Please Enter Vehicle No.";
                txt_VehicleNo.focus();
            }
            else if ( Trim(txt_LHPO_No_For_Print.value) == '')
            {  
                lbl_Errors.innerText = "Please Enter LHC No.";
                txt_LHPO_No_For_Print.focus(); 
            }
            else if ( val(hdn_Total_GC.value) <= 0)
            {
                lbl_Errors.innerText = "Please Select Atleast One GC";
            }
            else if (ddl_Supervisor.value == '')
            {
                lbl_Errors.innerText = "Please Select Unloaded by";
                ddl_Supervisor.focus();
            }
//            else if ( Trim(ddl_Reason_For_Late_Uploading.value) == '')            
//            {
//                lbl_Errors.innerText = objResource.GetMsg("Msg_ReasonFroLateUnloading");    
//                WucAUSOtherAgency1_TB_AUSOtherAgency.SelectTabById('zero'); 
//            } 
            else
                ATS = true;

            return ATS;
        }



    function Enable_Disable(ddl_Received_Condintion, txt_Damaged_Leakage_Articles, txt_Damaged_Leakage_Value)
    {
        var ddl_Received_Condintion =document.getElementById(ddl_Received_Condintion);  
        var txt_Damaged_Leakage_Articles=document.getElementById(txt_Damaged_Leakage_Articles);  
        var txt_Damaged_Leakage_Value=document.getElementById(txt_Damaged_Leakage_Value);
          
        var lbl_Total_Damage_Leakage_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Damage_Leakage_Articles');  
        var lbl_Total_Damage_Leakage_Value=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Damage_Leakage_Value');  
        var hdn_Total_Damage_Leakage_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Damage_Leakage_Articles');  
        var hdn_Total_Damage_Leakage_Value=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Damage_Leakage_Value');  

        if (ddl_Received_Condintion.value != 1)    
        {
            txt_Damaged_Leakage_Articles.disabled = false;
            txt_Damaged_Leakage_Value.disabled = false;
        }
        else
        {
            txt_Damaged_Leakage_Articles.disabled = true;
            txt_Damaged_Leakage_Value.disabled = true;
            
            hdn_Total_Damage_Leakage_Articles.value =val(hdn_Total_Damage_Leakage_Articles.value) - val(txt_Damaged_Leakage_Articles.value) ;
            hdn_Total_Damage_Leakage_Value.value =val(hdn_Total_Damage_Leakage_Value.value) - val(txt_Damaged_Leakage_Value.value) ;

            txt_Damaged_Leakage_Articles.value="0";
            txt_Damaged_Leakage_Value.value="0";
            
            lbl_Total_Damage_Leakage_Articles.innerHTML = hdn_Total_Damage_Leakage_Articles.value;
            lbl_Total_Damage_Leakage_Value.innerHTML = hdn_Total_Damage_Leakage_Value.value;
        }
    }

//      function Calculate_Summary(lbl_Balance_Articles,lbl_Booking_Article,txt_Loaded_Articles  , lbl_Loaded_Actual_Wt  ,
//                                txt_Recieved_Article, txt_Recieved_Articles_Wt,
//                                txt_Damaged_Leakage_Articles, txt_Damaged_Leakage_Value,
//                                Is_OnBlur)
//      {
//        
//        var lbl_Balance_Articles =document.getElementById(lbl_Balance_Articles);        
//        var txt_Loaded_Articles =document.getElementById(txt_Loaded_Articles);
//        var lbl_Booking_Article=document.getElementById(lbl_Booking_Article);         
//        var lbl_Loaded_Actual_Wt=document.getElementById(lbl_Loaded_Actual_Wt);
//        var txt_Recieved_Article=document.getElementById(txt_Recieved_Article);
//        var txt_Recieved_Articles_Wt=document.getElementById(txt_Recieved_Articles_Wt);        
//        var txt_Damaged_Leakage_Articles=document.getElementById(txt_Damaged_Leakage_Articles);
//        var txt_Damaged_Leakage_Value=document.getElementById(txt_Damaged_Leakage_Value);

//        var lbl_Total_Booking_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Booking_Articles');  
//        var lbl_Total_Booking_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Booking_Articles_Wt');  
//        var lbl_Total_Loaded_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Loaded_Articles');  
//        var lbl_Total_Loaded_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Loaded_Articles_Wt');  
//        var lbl_Total_Received_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Received_Articles');  
//        var lbl_Total_Received_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Received_Articles_Wt');  
//        var lbl_Total_Damage_Leakage_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Damage_Leakage_Articles');  
//        var lbl_Total_Damage_Leakage_Value=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_lbl_Total_Damage_Leakage_Value');  
//        
//        var hdn_Total_Booking_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Booking_Articles');  
//        var hdn_Total_Booking_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Booking_Articles_Wt');  
//        var hdn_Total_Loaded_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Loaded_Articles');  
//        var hdn_Total_Loaded_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Loaded_Articles_Wt');  
//        var hdn_Total_Received_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Received_Articles');  
//        var hdn_Total_Received_Articles_Wt=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Received_Articles_Wt');  
//        var hdn_Total_Damage_Leakage_Articles=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Damage_Leakage_Articles');  
//        var hdn_Total_Damage_Leakage_Value=document.getElementById('WucAUSOtherAgencyUnloadingDetails1_hdn_Total_Damage_Leakage_Value');  
//        
//        if ( Is_OnBlur == '0' )
//          {  
//                
//                hdn_Total_Loaded_Articles.value = val(hdn_Total_Loaded_Articles.value) - val(txt_Loaded_Articles.value);
//                hdn_Total_Received_Articles.value = val(hdn_Total_Received_Articles.value) - val(txt_Recieved_Article.value);
//                hdn_Total_Received_Articles_Wt.value = val(hdn_Total_Received_Articles_Wt.value) - val(txt_Recieved_Articles_Wt.value);
//                hdn_Total_Damage_Leakage_Articles.value  = val(hdn_Total_Damage_Leakage_Articles.value) - val(txt_Damaged_Leakage_Articles.value);
//                hdn_Total_Damage_Leakage_Value.value  = val(hdn_Total_Damage_Leakage_Value.value) - val(txt_Damaged_Leakage_Value.value);            
//            }        
//        else       
//            {
//                if ( val( lbl_Balance_Articles.innerHTML ) < val( txt_Loaded_Articles.value)  )
//                 {
//                    txt_Loaded_Articles.value= val( lbl_Balance_Articles.innerHTML);
//                 }             
//                 if ( val( txt_Loaded_Articles.value) < val( txt_Recieved_Article.value))
//                 {
//                    txt_Recieved_Article.value= val( txt_Loaded_Articles.value);
//                 }             
//                 if ( val( lbl_Loaded_Actual_Wt.innerHTML) < val( txt_Recieved_Articles_Wt.value))
//                 {
//                    txt_Recieved_Articles_Wt.value= val( lbl_Loaded_Actual_Wt.innerHTML);
//                 }
//                 if ( val( txt_Recieved_Article.value) < val( txt_Damaged_Leakage_Articles.value))
//                 {
//                    txt_Damaged_Leakage_Articles.value= val( txt_Recieved_Article.value);
//                 }             
//                hdn_Total_Loaded_Articles.value = val(hdn_Total_Loaded_Articles.value) + val(txt_Loaded_Articles.value);             
//                lbl_Total_Loaded_Articles.innerHTML= val(hdn_Total_Loaded_Articles.value);
//                              
//                hdn_Total_Received_Articles.value = val(hdn_Total_Received_Articles.value) + val(txt_Recieved_Article.value);
//                lbl_Total_Received_Articles.innerHTML= val(hdn_Total_Received_Articles.value);
//                
//                hdn_Total_Received_Articles_Wt.value = val(hdn_Total_Received_Articles_Wt.value) + val(txt_Recieved_Articles_Wt.value);
//                lbl_Total_Received_Articles_Wt.innerHTML=val(hdn_Total_Received_Articles_Wt.value);

//                hdn_Total_Damage_Leakage_Articles.value  = val(hdn_Total_Damage_Leakage_Articles.value) + val(txt_Damaged_Leakage_Articles.value);
//                lbl_Total_Damage_Leakage_Articles.innerHTML=val(hdn_Total_Damage_Leakage_Articles.value); 

//                hdn_Total_Damage_Leakage_Value.value  = val(hdn_Total_Damage_Leakage_Value.value) + val(txt_Damaged_Leakage_Value.value);
//                lbl_Total_Damage_Leakage_Value.innerHTML=val(hdn_Total_Damage_Leakage_Value.value); 
//            }
//         }    
   