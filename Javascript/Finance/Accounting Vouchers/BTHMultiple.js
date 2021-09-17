// JScript File

    function Check_All(chk,gridname)
    {              
        var grid = document.getElementById(gridname);
        var i,j=0;
        var sum_TotalBalance_ToBePaid=0,sum_TotalAdvanceAmount = 0;sum_TotalTDSAmount = 0;
        var checkbox,txt_Balance_To_Be_Paid,txt_Advance_Amt,ddl_AddLess;

        var txt_Total_Balance_To_Be_Paid = document.getElementById('WucBTHMultiple1_txt_Total_Balance_To_Be_Paid');
        var txt_Total_Advance_Amount = document.getElementById('WucBTHMultiple1_txt_Total_Advance_Amount');
        var txt_TotalPayableAmount = document.getElementById('WucBTHMultiple1_txt_TotalPayableAmount');
        var txt_Total_TDS_Amount = document.getElementById('WucBTHMultiple1_txt_Total_TDS_Amount');

        var hdn_Total_Balance_To_Be_Paid= document.getElementById('WucBTHMultiple1_hdn_Total_Balance_To_Be_Paid');
        var hdn_TotalLHPO = document.getElementById('WucBTHMultiple1_hdn_TotalLHPO');
        var hdn_Total_Advance_Amount = document.getElementById('WucBTHMultiple1_hdn_Total_Advance_Amount');
        var hdn_Total_Payable_Amount = document.getElementById('WucBTHMultiple1_hdn_Total_Payable_Amount');
        var hdn_Total_TDS_Amount = document.getElementById('WucBTHMultiple1_hdn_Total_TDS_Amount');

        var max = (grid.rows.length - 1);
        for(i=1;i<grid.rows.length;i++)
        {
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            txt_Balance_To_Be_Paid= grid.rows[i].cells[14].getElementsByTagName('input');
            ddl_AddLess = grid.rows[i].cells[15].getElementsByTagName('select');
            txt_Advance_Amt= grid.rows[i].cells[17].getElementsByTagName('input');
            lbl_TDS_Amount = grid.rows[i].cells[19].getElementsByTagName('span');
            
            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            }
                      
            if(chk.checked == true)
            {
                Calculate_TDS_Amount(txt_Advance_Amt,ddl_AddLess,lbl_TDS_Amount)
            
                if(txt_Balance_To_Be_Paid[0].type =='text')
                {
                    sum_TotalBalance_ToBePaid = val(sum_TotalBalance_ToBePaid) + val(txt_Balance_To_Be_Paid[0].value);
                }
                if(txt_Advance_Amt[0].type =='text')
                {
                    if(val(ddl_AddLess[0].value)== 0)
                    {
                        sum_TotalAdvanceAmount = val(sum_TotalAdvanceAmount) + 0;
                        sum_TotalTDSAmount = val(sum_TotalTDSAmount) + 0;
                    }
                    else if(val(ddl_AddLess[0].value)== 1)
                    {
                        sum_TotalAdvanceAmount = val(sum_TotalAdvanceAmount) + val(txt_Advance_Amt[0].value);
                        sum_TotalTDSAmount = val(sum_TotalTDSAmount) + val(lbl_TDS_Amount[0].innerText);
                    }
                    else if(val(ddl_AddLess[0].value)== 2)
                    {
                        sum_TotalAdvanceAmount = val(sum_TotalAdvanceAmount) - val(txt_Advance_Amt[0].value);
                        sum_TotalTDSAmount = val(sum_TotalTDSAmount) - val(lbl_TDS_Amount[0].innerText);
                    }
                }
            }            
        }
        
        if(chk.checked == true)
        {
            txt_Total_Balance_To_Be_Paid.value  = roundNumber(sum_TotalBalance_ToBePaid,2);
            txt_Total_Advance_Amount.value = roundNumber(val(sum_TotalAdvanceAmount),2);
            txt_Total_TDS_Amount.value = roundNumber(val(sum_TotalTDSAmount),2);
            txt_TotalPayableAmount.value = roundNumber(val(sum_TotalBalance_ToBePaid) + val(sum_TotalAdvanceAmount) - val(sum_TotalTDSAmount),2);
            
            hdn_TotalLHPO.value = max;
            hdn_Total_Balance_To_Be_Paid.value = roundNumber(sum_TotalBalance_ToBePaid,2);
            hdn_Total_Advance_Amount.value = roundNumber(val(sum_TotalAdvanceAmount),2);
            hdn_Total_TDS_Amount.value = roundNumber(val(sum_TotalTDSAmount),2);
            hdn_Total_Payable_Amount.value = roundNumber(val(sum_TotalBalance_ToBePaid) + val(sum_TotalAdvanceAmount) - val(sum_TotalTDSAmount),2);
        }
        else
        {
            txt_Total_Balance_To_Be_Paid.value  = 0;
            txt_Total_Advance_Amount.value = 0;
            txt_TotalPayableAmount.value = 0;
            txt_Total_TDS_Amount.value = 0;

            hdn_TotalLHPO.value = 0;
            hdn_Total_Balance_To_Be_Paid.value = 0;
            hdn_Total_Advance_Amount.value = 0;
            hdn_Total_Payable_Amount.value = 0;
            hdn_Total_TDS_Amount.value = 0;
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
            var grid = document.getElementById('WucBTHMultiple1_dg_BTH_LHPO');
        }
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;
        var txt_Balance_To_Be_Paid,txt_Other_Amount,ddl_AddLess,hdn_Previous_Value,lbl_TDS_Amount;

        var txt_Total_Balance_To_Be_Paid = document.getElementById('WucBTHMultiple1_txt_Total_Balance_To_Be_Paid');
        var txt_Total_Advance_Amount = document.getElementById('WucBTHMultiple1_txt_Total_Advance_Amount');
        var txt_TotalPayableAmount = document.getElementById('WucBTHMultiple1_txt_TotalPayableAmount');
        var txt_Total_TDS_Amount = document.getElementById('WucBTHMultiple1_txt_Total_TDS_Amount');

        var hdn_Total_Balance_To_Be_Paid= document.getElementById('WucBTHMultiple1_hdn_Total_Balance_To_Be_Paid');
        var hdn_Total_Advance_Amount = document.getElementById('WucBTHMultiple1_hdn_Total_Advance_Amount');
        var hdn_Total_Payable_Amount = document.getElementById('WucBTHMultiple1_hdn_Total_Payable_Amount');
        var hdn_TotalLHPO = document.getElementById('WucBTHMultiple1_hdn_TotalLHPO');
        var hdn_Total_TDS_Amount = document.getElementById('WucBTHMultiple1_hdn_Total_TDS_Amount');
//        var hdn_Previous_Value = document.getElementById('WucBTHMultiple1_hdn_Previous_Value');

        txt_Balance_To_Be_Paid= row.cells[14].getElementsByTagName('input');
        ddl_AddLess = row.cells[15].getElementsByTagName('select');
        txt_Other_Amount= row.cells[17].getElementsByTagName('input');
        hdn_Previous_Value = row.cells[18].getElementsByTagName('input');
        lbl_TDS_Amount = row.cells[19].getElementsByTagName('span');
        
        if(callfrom == 1)
        {
            if(chk.checked == true)
            {
                txt_Total_Balance_To_Be_Paid.value = roundNumber(val(txt_Total_Balance_To_Be_Paid.value) + val(txt_Balance_To_Be_Paid[0].value),2);
                
                Calculate_TDS_Amount(txt_Other_Amount,ddl_AddLess,lbl_TDS_Amount);
                
                if(val(ddl_AddLess[0].value) == 0)
                {
                    txt_Total_Advance_Amount.value = roundNumber(val(txt_Total_Advance_Amount.value) + 0,2);
                    hdn_Total_Advance_Amount.value = roundNumber(val(hdn_Total_Advance_Amount.value) + 0,2);
                    
                    txt_Total_TDS_Amount.value = roundNumber(val(txt_Total_TDS_Amount.value) + 0,2);
                    hdn_Total_TDS_Amount.value = roundNumber(val(hdn_Total_TDS_Amount.value) + 0,2);
                }
                else if(val(ddl_AddLess[0].value) == 1)
                {
                    txt_Total_Advance_Amount.value = roundNumber(val(txt_Total_Advance_Amount.value) + val(txt_Other_Amount[0].value),2);
                    hdn_Total_Advance_Amount.value = roundNumber(val(hdn_Total_Advance_Amount.value) + val(txt_Other_Amount[0].value),2);
                    
                    txt_Total_TDS_Amount.value = roundNumber(val(txt_Total_TDS_Amount.value) + val(lbl_TDS_Amount[0].innerText),2);
                    hdn_Total_TDS_Amount.value = roundNumber(val(hdn_Total_TDS_Amount.value) + val(lbl_TDS_Amount[0].innerText),2);

                }
                else if(val(ddl_AddLess[0].value) == 2)
                {
                    txt_Total_Advance_Amount.value = roundNumber(val(txt_Total_Advance_Amount.value) - val(txt_Other_Amount[0].value),2);
                    hdn_Total_Advance_Amount.value = roundNumber(val(hdn_Total_Advance_Amount.value) - val(txt_Other_Amount[0].value),2);
                    
                    txt_Total_TDS_Amount.value = roundNumber(val(txt_Total_TDS_Amount.value) - val(lbl_TDS_Amount[0].innerText),2);
                    hdn_Total_TDS_Amount.value = roundNumber(val(hdn_Total_TDS_Amount.value) - val(lbl_TDS_Amount[0].innerText),2);

                }
                
                txt_TotalPayableAmount.value = roundNumber(val(txt_Total_Balance_To_Be_Paid.value) + val(txt_Total_Advance_Amount.value) - val(txt_Total_TDS_Amount.value),2);

                hdn_TotalLHPO.value = val(hdn_TotalLHPO.value) + 1;
                
                hdn_Total_Balance_To_Be_Paid.value = roundNumber(val(hdn_Total_Balance_To_Be_Paid.value) + val(txt_Balance_To_Be_Paid[0].value),2);
                hdn_Total_Payable_Amount.value = roundNumber(val(hdn_Total_Balance_To_Be_Paid.value) + val(hdn_Total_Advance_Amount.value) - val(hdn_Total_TDS_Amount.value),2);
            }
            else
            {
                txt_Total_Balance_To_Be_Paid.value = roundNumber(val(txt_Total_Balance_To_Be_Paid.value) - val(txt_Balance_To_Be_Paid[0].value),2);
                
                Calculate_TDS_Amount(txt_Other_Amount,ddl_AddLess,lbl_TDS_Amount);
                
                if(val(ddl_AddLess[0].value) == 0)
                {
                    txt_Total_Advance_Amount.value = roundNumber(val(txt_Total_Advance_Amount.value) - 0,2);
                    hdn_Total_Advance_Amount.value = roundNumber(val(hdn_Total_Advance_Amount.value) - 0,2);
                    
                    txt_Total_TDS_Amount.value = roundNumber(val(txt_Total_TDS_Amount.value) - 0,2);
                    hdn_Total_TDS_Amount.value = roundNumber(val(hdn_Total_TDS_Amount.value) - 0,2);
                }
                else if(val(ddl_AddLess[0].value) == 1)
                {
                    txt_Total_Advance_Amount.value = roundNumber(val(txt_Total_Advance_Amount.value) - val(txt_Other_Amount[0].value),2);
                    hdn_Total_Advance_Amount.value = roundNumber(val(hdn_Total_Advance_Amount.value) - val(txt_Other_Amount[0].value),2);
                    
                    txt_Total_TDS_Amount.value = roundNumber(val(txt_Total_TDS_Amount.value) - val(lbl_TDS_Amount[0].innerText),2);
                    hdn_Total_TDS_Amount.value = roundNumber(val(hdn_Total_TDS_Amount.value) - val(lbl_TDS_Amount[0].innerText),2);
                }
                else if(val(ddl_AddLess[0].value) == 2)
                {
                    txt_Total_Advance_Amount.value = roundNumber(val(txt_Total_Advance_Amount.value) + val(txt_Other_Amount[0].value),2);
                    hdn_Total_Advance_Amount.value = roundNumber(val(hdn_Total_Advance_Amount.value) + val(txt_Other_Amount[0].value),2);
                    
                    txt_Total_TDS_Amount.value = roundNumber(val(txt_Total_TDS_Amount.value) + val(lbl_TDS_Amount[0].innerText),2);
                    hdn_Total_TDS_Amount.value = roundNumber(val(hdn_Total_TDS_Amount.value) + val(lbl_TDS_Amount[0].innerText),2);

                }
                txt_TotalPayableAmount.value = roundNumber(val(txt_Total_Balance_To_Be_Paid.value) + val(txt_Total_Advance_Amount.value) - val(txt_Total_TDS_Amount.value),2);

                hdn_TotalLHPO.value = val(hdn_TotalLHPO.value) - 1;
                
                hdn_Total_Balance_To_Be_Paid.value = roundNumber(val(hdn_Total_Balance_To_Be_Paid.value) - val(txt_Balance_To_Be_Paid[0].value),2);
                hdn_Total_Payable_Amount.value = roundNumber(val(hdn_Total_Balance_To_Be_Paid.value) + val(hdn_Total_Advance_Amount.value)  - val(hdn_Total_TDS_Amount.value),2);
            }
        }
        
        if(callfrom == 2)       //onblur of  Other Amount
        {
            if(chk.checked == true)
            {
                Calculate_TDS_Amount(txt_Other_Amount,ddl_AddLess,lbl_TDS_Amount);
            
                if(val(ddl_AddLess[0].value) == 0)
                {
                    hdn_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value) + 0 ;
                    txt_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value);
                    
                    hdn_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value) + 0;
                    txt_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value);
                }
                else if(val(ddl_AddLess[0].value) == 1)
                {
                    hdn_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value) + val(txt_Other_Amount[0].value);
                    txt_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value);
                    
                    hdn_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value) + val(lbl_TDS_Amount[0].innerText);
                    txt_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value);
                }
                else if(val(ddl_AddLess[0].value) == 2)
                {
                    hdn_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value) - val(txt_Other_Amount[0].value);
                    txt_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value);
                    
                    hdn_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value) - val(lbl_TDS_Amount[0].innerText);
                    txt_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value);
                }
                
                txt_TotalPayableAmount.value = val(hdn_Total_Balance_To_Be_Paid.value) +  val(hdn_Total_Advance_Amount.value)  - val(hdn_Total_TDS_Amount.value);
                hdn_Total_Payable_Amount.value = val(hdn_Total_Balance_To_Be_Paid.value) +  val(hdn_Total_Advance_Amount.value)  - val(hdn_Total_TDS_Amount.value);
            }
        }
        
        if(callfrom == 3 && chk.checked == true)   //onfocus of  Other Amount
        {
            Calculate_TDS_Amount(txt_Other_Amount,ddl_AddLess,lbl_TDS_Amount);
            
            if(val(ddl_AddLess[0].value) == 0)
            {
                hdn_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value) - 0;
                txt_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value);
                
                hdn_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value) - 0;
                txt_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value);
            }
            else if(val(ddl_AddLess[0].value) == 1)
            {
                hdn_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value) - val(txt_Other_Amount[0].value);
                txt_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value);
                
                hdn_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value) - val(lbl_TDS_Amount[0].innerText);
                txt_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value);
            }
            else if(val(ddl_AddLess[0].value) == 2)
            {
                hdn_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value) + val(txt_Other_Amount[0].value);
                txt_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value);
                
                hdn_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value) + val(lbl_TDS_Amount[0].innerText);
                txt_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value);
            }
            
            txt_TotalPayableAmount.value = val(hdn_Total_Balance_To_Be_Paid.value) + val(hdn_Total_Advance_Amount.value)  - val(hdn_Total_TDS_Amount.value);
            hdn_Total_Payable_Amount.value = val(hdn_Total_Balance_To_Be_Paid.value) + val(hdn_Total_Advance_Amount.value) - val(hdn_Total_TDS_Amount.value);
        }
        
        if(callfrom == 4 && chk.checked == true)       //onchange of IS ADD/LESS
        {
//            if(val(ddl_AddLess[0].value) != '0')
//            {
//                hdn_Previous_Value[0].value = val(ddl_AddLess[0].value);
//            }
            
            if(val(ddl_AddLess[0].value) == 0)
            {
                if(val(hdn_Previous_Value[0].value)== 1)
                {
                    hdn_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value) - val(txt_Other_Amount[0].value);
                    txt_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value);
                    
                    hdn_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value) - val(lbl_TDS_Amount[0].innerText);
                    txt_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value);
                }
                else if(val(hdn_Previous_Value[0].value)== 2)
                {
                    hdn_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value) + val(txt_Other_Amount[0].value);
                    txt_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value);

                }
                
                 Calculate_TDS_Amount(txt_Other_Amount,ddl_AddLess,lbl_TDS_Amount); 
              
            }
            else if(val(ddl_AddLess[0].value) == 1)
            {
                Calculate_TDS_Amount(txt_Other_Amount,ddl_AddLess,lbl_TDS_Amount); 
             
                if(val(hdn_Previous_Value[0].value)== 0)
                {
                    hdn_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value) + val(txt_Other_Amount[0].value);
                    txt_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value);
                    
                    hdn_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value) + val(lbl_TDS_Amount[0].innerText);
                    txt_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value);
                }
                else if(val(hdn_Previous_Value[0].value)== 2)
                {
                    hdn_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value) + val(val(txt_Other_Amount[0].value) + val(txt_Other_Amount[0].value));
                    txt_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value);
                    
                    hdn_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value) + val(lbl_TDS_Amount[0].innerText);
                    txt_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value);
                }
            }
            else if(val(ddl_AddLess[0].value) == 2)
            {
                if(val(hdn_Previous_Value[0].value)== 0)
                {
                    hdn_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value) - val(txt_Other_Amount[0].value);
                    txt_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value);
                    
                }
                else if(val(hdn_Previous_Value[0].value)== 1)
                {
                    hdn_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value) - val(val(txt_Other_Amount[0].value) + val(txt_Other_Amount[0].value));
                    txt_Total_Advance_Amount.value = val(hdn_Total_Advance_Amount.value);
                    
                    hdn_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value) - val(lbl_TDS_Amount[0].innerText);
                    txt_Total_TDS_Amount.value = val(hdn_Total_TDS_Amount.value);
                }
                
                Calculate_TDS_Amount(txt_Other_Amount,ddl_AddLess,lbl_TDS_Amount);
            }
            
                             
            txt_TotalPayableAmount.value = val(hdn_Total_Balance_To_Be_Paid.value) + val(hdn_Total_Advance_Amount.value) - val(hdn_Total_TDS_Amount.value);
            hdn_Total_Payable_Amount.value = val(hdn_Total_Balance_To_Be_Paid.value) + val(hdn_Total_Advance_Amount.value) - val(hdn_Total_TDS_Amount.value);
            
//            if(val(ddl_AddLess[0].value) != '0')
//            {
                hdn_Previous_Value[0].value = val(ddl_AddLess[0].value);
//            }
        }
        
        if(callfrom == 5)
        {
            var max = (grid.rows.length - 1);
            var sum_TotalBalance_ToBePaid=0,sum_TotalAdvanceAmount = 0;sum_TotalTDSAmount = 0;
            
            for(i = 1; i < grid.rows.length ; i++)
            {
                checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
                txt_Balance_To_Be_Paid= grid.rows[i].cells[14].getElementsByTagName('input');
                ddl_AddLess = grid.rows[i].cells[15].getElementsByTagName('select');
                txt_Advance_Amt= grid.rows[i].cells[17].getElementsByTagName('input');
                lbl_TDS_Amount = grid.rows[i].cells[19].getElementsByTagName('span');
                             
                    if(checkbox[0].checked == true)
                    {
                    
                        Calculate_TDS_Amount(txt_Advance_Amt,ddl_AddLess,lbl_TDS_Amount)
                                            
                        if(txt_Balance_To_Be_Paid[0].type =='text')
                        {
                            sum_TotalBalance_ToBePaid = val(sum_TotalBalance_ToBePaid) + val(txt_Balance_To_Be_Paid[0].value);
                        }
                        
                        if(txt_Advance_Amt[0].type =='text')
                        {
                            if(val(ddl_AddLess[0].value)== 0)
                            {
                                sum_TotalAdvanceAmount = val(sum_TotalAdvanceAmount) + 0;
                                sum_TotalTDSAmount = val(sum_TotalTDSAmount) + 0;
                            }
                            else if(val(ddl_AddLess[0].value)== 1)
                            {
                                sum_TotalAdvanceAmount = val(sum_TotalAdvanceAmount) + val(txt_Advance_Amt[0].value);
                                sum_TotalTDSAmount = val(sum_TotalTDSAmount) + val(lbl_TDS_Amount[0].innerText);
                            }
                            else if(val(ddl_AddLess[0].value)== 2)
                            {
                                sum_TotalAdvanceAmount = val(sum_TotalAdvanceAmount) - val(txt_Advance_Amt[0].value);
                                sum_TotalTDSAmount = val(sum_TotalTDSAmount) - val(lbl_TDS_Amount[0].innerText);
                            }
                        }
                    }
            }
        
            txt_Total_Balance_To_Be_Paid.value  = roundNumber(sum_TotalBalance_ToBePaid,2);
            txt_Total_Advance_Amount.value = roundNumber(val(sum_TotalAdvanceAmount),2);
            txt_Total_TDS_Amount.value = roundNumber(val(sum_TotalTDSAmount),2);
            txt_TotalPayableAmount.value = roundNumber(val(sum_TotalBalance_ToBePaid) + val(sum_TotalAdvanceAmount) - val(sum_TotalTDSAmount),2);
            
            hdn_TotalLHPO.value = max;
            hdn_Total_Balance_To_Be_Paid.value = roundNumber(sum_TotalBalance_ToBePaid,2);
            hdn_Total_Advance_Amount.value = roundNumber(val(sum_TotalAdvanceAmount),2);
            hdn_Total_TDS_Amount.value = roundNumber(val(sum_TotalTDSAmount),2);
            hdn_Total_Payable_Amount.value = roundNumber(val(sum_TotalBalance_ToBePaid) + val(sum_TotalAdvanceAmount) - val(sum_TotalTDSAmount),2);

       }

       
        if((grid.rows.length-1) == val(hdn_TotalLHPO.value))
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
        var lbl_Error = document.getElementById('WucBTHMultiple1_lbl_Errors');
        var ddl_BrokerOwner = document.getElementById('WucBTHMultiple1_ddl_OwnerBroker_hdnddl_OwnerBroker');
        var Total_Payable_Amount = document.getElementById('WucBTHMultiple1_hdn_Total_Payable_Amount');
        var hdn_TotalLHPO = document.getElementById('WucBTHMultiple1_hdn_TotalLHPO');
        var Hdn_LHPOCaption = document.getElementById('WucBTHMultiple1_Hdn_LHPOCaption');

        if(val(ddl_BrokerOwner.value) <= 0)
        {
            lbl_Error.innerText = 'Please Select Owner/Broker';
            return false;
        }        
        else if(val(hdn_TotalLHPO.value) <= 0)
        {
            lbl_Error.innerText = 'Please Select Atleast One '+ Hdn_LHPOCaption.value + ' No.';
            return false;
        }
        else if(val(Total_Payable_Amount.value) <= 0)
        {
            lbl_Error.innerText = 'Total Payable Amount Should be Greater Than Zero';
            return false;
        }

        if(validateWUCCheque(lbl_Error) == false)
        {return false;}

          return true;
    }    
    
function viewwindow(DocType,DocNo)
{
    var Path='../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type='+ DocType +'&Doc_No=' + DocNo ;

    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-100);
    var popH = (h-100);
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
                
      window.open(Path, 'CustomPopUp_Track_And_Trace', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
      return false;
}


function Calculate_TDS_Amount(txt_Other_Amount1,ddl_AddLess1,lbl_TDS_Amount1)
{
   var txt_Other_Amount = txt_Other_Amount1;
   var ddl_AddLess = ddl_AddLess1;
   var lbl_TDS_Amount = lbl_TDS_Amount1;
         
   var hdn_Tax_Rate = document.getElementById('WucBTHMultiple1_hdn_Tax_Rate');
   var hdn_Surcharge_Rate = document.getElementById('WucBTHMultiple1_hdn_Surcharge_Rate');
   var hdn_Add_Surcharge_Rate = document.getElementById('WucBTHMultiple1_hdn_Add_Surcharge_Rate');
   var hdn_Add_Edu_Cess_Rate = document.getElementById('WucBTHMultiple1_hdn_Add_Edu_Cess_Rate');

   var Amount = val(txt_Other_Amount[0].value);
   var TDS_Amount = 0;
   var Tax_Amount = 0;
   var Surcharge_Amount = 0;
   var Add_Surcharge_Amount = 0;
   var Add_Edu_Cess_Amount = 0;

   Tax_Amount = (Amount * val(hdn_Tax_Rate.value))/100;
   Surcharge_Amount = (val(Tax_Amount) * val(hdn_Surcharge_Rate.value))/100;
   Add_Surcharge_Amount = ((val(Tax_Amount) + val(Surcharge_Amount)) * val(hdn_Add_Surcharge_Rate.value))/100;
   Add_Edu_Cess_Amount = ((val(Tax_Amount) + val(Surcharge_Amount)) * val(hdn_Add_Edu_Cess_Rate.value))/100;

  
        if(val(ddl_AddLess[0].value) == 1)
        {
            TDS_Amount = Tax_Amount + Surcharge_Amount + Add_Surcharge_Amount + Add_Edu_Cess_Amount;
        }
        else
        {
            TDS_Amount = 0;
        }
    
    lbl_TDS_Amount[0].innerText = Math.ceil(TDS_Amount);
    
    
}

