// JScript File
  
    function Check_All(chk,gridname)
    {              
        var grid = document.getElementById(gridname);
        var i,j=0;
        var sum_totalGCAmt=0;
        var checkbox;
 
        var lbl_total_GC = document.getElementById('WucTruckBharai1_lbl_Total_GC');  
        var hdn_total_GC = document.getElementById('WucTruckBharai1_hdn_Total_GC'); 
        
        var max = (grid.rows.length - 1);
        for(i=1;i<grid.rows.length;i++)
        {            
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');  
            
            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            } 
            
        }
        
        if(chk.checked == true)
        {
           lbl_total_GC.innerHTML = max;
           hdn_total_GC.value = max;  
        }
        else
        {
            lbl_total_GC.innerHTML = 0;
            hdn_total_GC.value = 0;
        }
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
            var grid = document.getElementById('WucTruckBharai1_dg_TruckBharai');
        }
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement; 
 
        var lbl_total_GC = document.getElementById('WucTruckBharai1_lbl_Total_GC');  
        var hdn_total_GC = document.getElementById('WucTruckBharai1_hdn_Total_GC'); 
 
        if(callfrom == 1)
        {
            if(chk.checked == true)
            {
               lbl_total_GC.innerHTML = val(lbl_total_GC.innerHTML) + 1;
               hdn_total_GC.value = val(hdn_total_GC.value) + 1; 
            }
            else
            {
               lbl_total_GC.innerHTML = val(lbl_total_GC.innerHTML) - 1;
               hdn_total_GC.value = val(hdn_total_GC.value) - 1;
            }
        }  
        if((grid.rows.length-1) == val(hdn_total_GC.value))
        {
            checkall[0].checked = true;
        }
        else
        {
            checkall[0].checked = false;
        }
    }
   
   
   function Check_AllSelectedMemo(chk,gridname)
    {         
        var grid = document.getElementById(gridname);
        var i,j=0;
        var sum_Hamali_Charges=0,sum_Hamali_Paid=0,sum_DelArt=0,sum_DelWt=0,sum_totalGCAmt=0;
        var checkbox;

        var lbl_totalHamali_Charges = document.getElementById('WucTruckBharai1_lbl_Total_Hamali_Charges');
        var lbl_totalHamali_Paid = document.getElementById('WucTruckBharai1_lbl_Total_Hamali_Paid'); 
        var lbl_total_SelectedMemo = document.getElementById('WucTruckBharai1_lbl_Total_SelectedMemo');

        var hdn_totalHamali_Charges= document.getElementById('WucTruckBharai1_hdn_Total_Hamali_Charges');
        var hdn_totalHamali_Paid= document.getElementById('WucTruckBharai1_hdn_Total_Hamali_Paid'); 
        var hdn_total_SelectedMemo = document.getElementById('WucTruckBharai1_hdn_Total_SelectedMemo');
        
        var max = (grid.rows.length - 1);
        for(i=1;i<grid.rows.length;i++)
        {            
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input'); 
            
            txt_Hamali_Charges= grid.rows[i].cells[8].innerText;//.getElementsByTagName('input');
            txt_Hamali_Paid  = grid.rows[i].cells[9].getElementsByTagName('input');
            
            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            }
                      
            if(chk.checked == true)
            {
                txt_Hamali_Paid[0].disabled = false;
                 
                sum_Hamali_Charges = sum_Hamali_Charges + val(txt_Hamali_Charges);
                             
                if(txt_Hamali_Paid[0].type =='text')
                {
                    sum_Hamali_Paid = sum_Hamali_Paid + val(txt_Hamali_Paid[0].value);
                }  
            }
            else
            {
                txt_Hamali_Paid[0].disabled = true; 
                txt_Hamali_Paid[0].value=0;   
            }
            
            
        }
        
        if(chk.checked == true)
        {
            lbl_totalHamali_Charges.innerHTML  = sum_Hamali_Charges;
            lbl_totalHamali_Paid.innerHTML  = sum_Hamali_Paid; 
            lbl_total_SelectedMemo.innerHTML = max; 
                    
            hdn_total_SelectedMemo.value = max;
            hdn_totalHamali_Charges.value = sum_Hamali_Charges;
            hdn_totalHamali_Paid.value = sum_Hamali_Paid; 
        }
        else
        {  
            lbl_totalHamali_Charges.innerHTML  = 0;
            lbl_totalHamali_Paid.innerHTML  = 0; 
            lbl_total_SelectedMemo.innerHTML = 0; 
                    
            hdn_total_SelectedMemo.value = 0;
            hdn_totalHamali_Charges.value = 0;
            hdn_totalHamali_Paid.value = 0;
            
            txt_Hamali_Paid[0].value=0;   
        }
    } 
        
    function Check_SingleSelectedMemo(chk,gridname,callfrom)
    {  
 
        //callfrom means From checkbox else from Textbox
        if(callfrom == 1)
        {
            var grid = document.getElementById(gridname);
        }
        else
        {
            var grid = document.getElementById('WucTruckBharai1_dg_TruckBharai');
        }
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;

        var lbl_total_Hamali_Charges = document.getElementById('WucTruckBharai1_lbl_Total_Hamali_Charges');
        var lbl_total_Hamali_Paid = document.getElementById('WucTruckBharai1_lbl_Total_Hamali_Paid'); 
        var lbl_total_SelectedMemo = document.getElementById('WucTruckBharai1_lbl_Total_SelectedMemo'); 

        var hdn_total_Hamali_Charges= document.getElementById('WucTruckBharai1_hdn_Total_Hamali_Charges');
        var hdn_total_Hamali_Paid= document.getElementById('WucTruckBharai1_hdn_Total_Hamali_Paid'); 
        var hdn_total_SelectedMemo = document.getElementById('WucTruckBharai1_hdn_Total_SelectedMemo');  
        
        txt_Hamali_Charges = row.cells[8].innerText;//row.cells[8].getElementsByTagName('input');
        txt_Hamali_Paid  = row.cells[9].getElementsByTagName('input');  
 
 
        if(callfrom == 1)
        {
            if(chk.checked == true)
            {
               txt_Hamali_Paid[0].disabled = false;
               lbl_total_SelectedMemo.innerHTML = val(lbl_total_SelectedMemo.innerHTML) + 1;
               lbl_total_Hamali_Charges.innerHTML = val(lbl_total_Hamali_Charges.innerHTML) + val(txt_Hamali_Charges);
               lbl_total_Hamali_Paid.innerHTML = val(lbl_total_Hamali_Paid.innerHTML) + val(txt_Hamali_Paid[0].value);
                
               hdn_total_SelectedMemo.value = val(hdn_total_SelectedMemo.value) + 1;
               hdn_total_Hamali_Charges.value = val(hdn_total_Hamali_Charges.value) + val(txt_Hamali_Charges);
               hdn_total_Hamali_Paid.value = val(hdn_total_Hamali_Paid.value) + val(txt_Hamali_Paid[0].value); 
            }
            else
            {
               txt_Hamali_Paid[0].disabled = true;
               lbl_total_SelectedMemo.innerHTML = val(lbl_total_SelectedMemo.innerHTML) - 1;
               lbl_total_Hamali_Charges.innerHTML = val(lbl_total_Hamali_Charges.innerHTML) - val(txt_Hamali_Charges);
               lbl_total_Hamali_Paid.innerHTML = val(lbl_total_Hamali_Paid.innerHTML) - val(txt_Hamali_Paid[0].value); 
               
               hdn_total_SelectedMemo.value = val(hdn_total_SelectedMemo.value) - 1;
               hdn_total_Hamali_Charges.value = val(hdn_total_Hamali_Charges.value) - val(txt_Hamali_Charges);
               hdn_total_Hamali_Paid.value = val(hdn_total_Hamali_Paid.value) - val(txt_Hamali_Paid[0].value);
               
               txt_Hamali_Paid[0].value=0;  
            }
        } 

        if((grid.rows.length-1) == val(hdn_total_SelectedMemo.value))
        {
            checkall[0].checked = true;
        }
        else
        {
            checkall[0].checked = false;
        }
    }
    function Check_SingleSelectedMemoonTextboxChange()
    {   
        
        var total_Hamali_Paid = 0; 
        var total_Hamali_Charges = 0; 
        var lbl_total_Hamali_Charges = document.getElementById('WucTruckBharai1_lbl_Total_Hamali_Charges');
        var hdn_total_Hamali_Charges= document.getElementById('WucTruckBharai1_hdn_Total_Hamali_Charges');
        var lbl_total_Hamali_Paid = document.getElementById('WucTruckBharai1_lbl_Total_Hamali_Paid'); 
        var hdn_total_Hamali_Paid = document.getElementById('WucTruckBharai1_hdn_total_Hamali_Paid'); 
        var add_this_record;

        for(i = 0; i < document.forms[0].elements.length; i++) 
        {

        elm = document.forms[0].elements[i];
        if (elm.name != undefined)
            {
                var elm_id = document.getElementById(elm.id);
                var elm_name = elm.name;
                var arr = elm_name.split("$");
                
               
                if (arr[3] =="Chk_Attach")
                    {
                    if (elm_id.checked == true)
                        {
                           if (elm_id.value == '') elm_id.value = 0;
                            add_this_record = true; 
                       }
                       else
                       {
                           add_this_record = false; 
                       }
                    }
               
                else if (arr[3] =="hdn_Hamali_Charges")
                    {
                        if (add_this_record == true)
                            {
                                if (elm_id.value == '') elm_id.value = 0; 
                                total_Hamali_Charges =  parseFloat(total_Hamali_Charges) + parseFloat(elm_id.value);
                            }
                        else  
                            add_this_record = false;
                    }
               
                else if (arr[3] =="txt_Hamali_Paid")
                    {
                        if (add_this_record == true)
                            {
                                if (elm_id.value == '') elm_id.value = 0;
                                elm_id.disabled = false;
                                total_Hamali_Paid =  parseFloat(total_Hamali_Paid) + parseFloat(elm_id.value);
                            }
                        else 
                            elm_id.disabled = true;
                            add_this_record = false;
                    }
            }        

        }

        lbl_total_Hamali_Charges.innerHTML =  Math.round(total_Hamali_Charges);
        hdn_total_Hamali_Charges.value =  Math.round(total_Hamali_Charges); 
        lbl_total_Hamali_Paid.innerHTML =  Math.round(total_Hamali_Paid);
        hdn_total_Hamali_Paid.value =  Math.round(total_Hamali_Paid); 
    
    }
    

    function Allow_To_Save()
    {
        var ATS = false; 
        var txt_LoadedBy = document.getElementById('WucTruckBharai1_ddl_LoadedBy_txtBoxddl_LoadedBy');
        var hdn_Total_SelectedMemo = document.getElementById('WucTruckBharai1_hdn_Total_SelectedMemo');
        var hdn_GCCaption = document.getElementById('WucTruckBharai1_hdn_GCCaption');
        var hdn_Mode = document.getElementById('WucTruckBharai1_hdn_Mode');
        
        var lbl_Error_Client = document.getElementById('WucTruckBharai1_lbl_Error_Client');
        lbl_Error_Client.innerHTML ="";
         
        if (val(hdn_Total_SelectedMemo.value) == 0)
        {
            lbl_Error_Client.innerHTML = "Please Select All" + hdn_GCCaption.value;
        }       
        else if(txt_LoadedBy.value == '')
        {
            lbl_Error_Client.innerHTML = "Please Select Loaded By"; 
            txt_LoadedBy.focus();
        }               
        else
        {
            ATS = true;
        }
        return ATS;
     }    
 
 