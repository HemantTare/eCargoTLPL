// JScript File

  
    function Check_All(chk,gridname)
    {              
        var grid = document.getElementById(gridname);
        var checkbox;
        var i,j=0; 
        var Booking_Branch_Id;
        
        var lbl_Total_GC = document.getElementById('WucLabelPrinting1_lbl_Total_GC');
  
        var hdn_Total_GC = document.getElementById('WucLabelPrinting1_hdn_Total_GC');
        
        var max = (grid.rows.length - 1);
        for(i=1;i<grid.rows.length;i++)
        {            
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');

            CommValve = grid.rows[i].cells[5].getElementsByTagName('input');    //it Contain Booking_Branch_Id,
          
            Booking_Branch_Id = CommValve[1].value; 

            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            }
             
            
        }
        if(chk.checked == true)
        {
            lbl_Total_GC.innerHTML = max;
            hdn_Total_GC.value = max; 
        }
        else
        {
            lbl_Total_GC.innerHTML = 0;
            hdn_Total_GC.value = 0; 
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
            var grid = document.getElementById('WucLabelPrinting1_dg_Memo');
        }
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;

        var Actual_Wt,Loaded_Art,Loaded_Wt,CommValve,Sub_Total,Booking_Branch_Id,Payment_Type_Id,Bal_Article,Bal_Act_Wt;

        var txt_Book_ActualWt = document.getElementById('WucLabelPrinting1_txt_Book_ActualWt');
        var txt_Book_ToPayCollection = document.getElementById('WucLabelPrinting1_txt_Book_ToPayCollection');
        var txt_Cros_ActualWt = document.getElementById('WucLabelPrinting1_txt_Cros_ActualWt');
        var txt_Cros_ToPayCollection = document.getElementById('WucLabelPrinting1_txt_Cros_ToPayCollection');
        var txt_Total_ActualWt = document.getElementById('WucLabelPrinting1_txt_Total_ActualWt');
        var txt_Total_ToPayCollection = document.getElementById('WucLabelPrinting1_txt_Total_ToPayCollection');
        var lbl_tolalLodArt = document.getElementById('WucLabelPrinting1_lbl_tolalLodArt');
        var lbl_tolalLodWt = document.getElementById('WucLabelPrinting1_lbl_tolalLodWt');
        var lbl_Total_GC = document.getElementById('WucLabelPrinting1_lbl_Total_GC');

        var hdn_Book_ActualWt= document.getElementById('WucLabelPrinting1_hdn_Book_ActualWt');
        var hdn_Book_ToPayCollection= document.getElementById('WucLabelPrinting1_hdn_Book_ToPayCollection');
        var hdn_Cros_ActualWt = document.getElementById('WucLabelPrinting1_hdn_Cros_ActualWt');
        var hdn_Cros_ToPayCollection = document.getElementById('WucLabelPrinting1_hdn_Cros_ToPayCollection');
        var hdn_Total_ActualWt= document.getElementById('WucLabelPrinting1_hdn_Total_ActualWt');
        var hdn_Total_ToPayCollection= document.getElementById('WucLabelPrinting1_hdn_Total_ToPayCollection');
        var hdn_tolalLodArt= document.getElementById('WucLabelPrinting1_hdn_tolalLodArt');
        var hdn_tolalLodWt= document.getElementById('WucLabelPrinting1_hdn_tolalLodWt');
        
        var hdn_LoginBranch_Id= document.getElementById('WucLabelPrinting1_hdn_LoginBranch_Id');
        var hdn_Total_GC = document.getElementById('WucLabelPrinting1_hdn_Total_GC');
                
            Actual_Wt= row.cells[8].getElementsByTagName('input');
            Bal_Article= row.cells[10].getElementsByTagName('input');
            Bal_Act_Wt= row.cells[12].getElementsByTagName('input');
            Loaded_Art  = row.cells[13].getElementsByTagName('input');
            Loaded_Wt = row.cells[14].getElementsByTagName('input');
            CommValve = row.cells[15].getElementsByTagName('input');    //it Contain Sub_Total,Booking_Branch_Id,
                                                                        //Payment_Type_Id
            Sub_Total = CommValve[0].value;
            Booking_Branch_Id = CommValve[1].value;
            Payment_Type_Id = CommValve[2].value;
            
        if(val(Loaded_Art[0].value) > val(Bal_Article[0].value))
        {
            Loaded_Art[0].value = val(Bal_Article[0].value);
        }
        if(val(Loaded_Wt[0].value) > val(Bal_Act_Wt[0].value))
        {
            Loaded_Wt[0].value = val(Bal_Act_Wt[0].value);
        }
        
            if(callfrom == 1 && val(hdn_LoginBranch_Id.value)== val(Booking_Branch_Id))
            {
                if(chk.checked == true)
                {
                   txt_Book_ActualWt.value = val(txt_Book_ActualWt.value) + val(Actual_Wt[0].value);
                   hdn_Book_ActualWt.value = val(hdn_Book_ActualWt.value) + val(Actual_Wt[0].value);
                }
                else
                {
                   txt_Book_ActualWt.value = val(txt_Book_ActualWt.value) - val(Actual_Wt[0].value);
                   hdn_Book_ActualWt.value = val(hdn_Book_ActualWt.value) - val(Actual_Wt[0].value);
                }
            }
            if((callfrom == 1) && (Payment_Type_Id == 1) && val(hdn_LoginBranch_Id.value)== val(Booking_Branch_Id))
            {
                if(chk.checked == true)
                {
                   txt_Book_ToPayCollection.value = val(txt_Book_ToPayCollection.value) + val(Sub_Total);
                   hdn_Book_ToPayCollection.value = val(hdn_Book_ToPayCollection.value) + val(Sub_Total);               
                }
                else
                {
                   txt_Book_ToPayCollection.value = val(txt_Book_ToPayCollection.value) - val(Sub_Total);
                   hdn_Book_ToPayCollection.value = val(hdn_Book_ToPayCollection.value) - val(Sub_Total);               
                }
            }
            
            if((callfrom == 1) && val(hdn_LoginBranch_Id.value)!= val(Booking_Branch_Id))
            {
                if(chk.checked == true)
                {
                   txt_Cros_ActualWt.value = val(txt_Cros_ActualWt.value) + val(Actual_Wt[0].value);
                   hdn_Cros_ActualWt.value = val(hdn_Cros_ActualWt.value) + val(Actual_Wt[0].value);
                }
                else
                {
                   txt_Cros_ActualWt.value = val(txt_Cros_ActualWt.value) - val(Actual_Wt[0].value);
                   hdn_Cros_ActualWt.value = val(hdn_Cros_ActualWt.value) - val(Actual_Wt[0].value);
                }
            }
            
            if((callfrom == 1) && (Payment_Type_Id == 1) && val(hdn_LoginBranch_Id.value)!= val(Booking_Branch_Id))
            {
                if(chk.checked == true)
                {
                   txt_Cros_ToPayCollection.value = val(txt_Cros_ToPayCollection.value) + val(Sub_Total);
                   hdn_Cros_ToPayCollection.value = val(hdn_Cros_ToPayCollection.value) + val(Sub_Total);               
                }
                else
                {
                   txt_Cros_ToPayCollection.value = val(txt_Cros_ToPayCollection.value) - val(Sub_Total);
                   hdn_Cros_ToPayCollection.value = val(hdn_Cros_ToPayCollection.value) - val(Sub_Total);               
                }
            }
            if(callfrom == 1)
            {
                if(chk.checked == true)
                {
                    hdn_Total_GC.value = val(hdn_Total_GC.value) + 1;
                    hdn_tolalLodArt.value = val(hdn_tolalLodArt.value) + val(Loaded_Art[0].value);
                    hdn_tolalLodWt.value = val(hdn_tolalLodWt.value) + val(Loaded_Wt[0].value);
                    
                    lbl_Total_GC.innerHTML = val(lbl_Total_GC.innerHTML) + 1;
                    lbl_tolalLodArt.innerHTML = val(hdn_tolalLodArt.value);
                    lbl_tolalLodWt.innerHTML = val(hdn_tolalLodWt.value);
                }
                else
                {
                    hdn_Total_GC.value = val(hdn_Total_GC.value) - 1;
                    hdn_tolalLodArt.value = val(hdn_tolalLodArt.value) - val(Loaded_Art[0].value);
                    hdn_tolalLodWt.value = val(hdn_tolalLodWt.value) - val(Loaded_Wt[0].value);

                    lbl_Total_GC.innerHTML = val(lbl_Total_GC.innerHTML) - 1;
                    lbl_tolalLodArt.innerHTML = val(hdn_tolalLodArt.value);
                    lbl_tolalLodWt.innerHTML = val(hdn_tolalLodWt.value);
                }
            }
            
            if(callfrom == 2)       //onblur of  Loded Article
            {
                if(chk.checked == true)
                {
                    hdn_tolalLodArt.value = val(hdn_tolalLodArt.value) + val(Loaded_Art[0].value);
                    lbl_tolalLodArt.innerHTML = val(hdn_tolalLodArt.value);
                }               
            }
            if(callfrom == 3)       //onblur of  Loded Weight
            {
                if(chk.checked == true)
                {
                    hdn_tolalLodWt.value = val(hdn_tolalLodWt.value) + val(Loaded_Wt[0].value);
                    lbl_tolalLodWt.innerHTML = val(hdn_tolalLodWt.value);
                }               
            }
            if(callfrom == 4 && chk.checked == true)       //onfocus of  Loded Article
            {
                hdn_tolalLodArt.value = val(hdn_tolalLodArt.value) - val(Loaded_Art[0].value);
                lbl_tolalLodArt.innerHTML = val(hdn_tolalLodArt.value);
            }
            if(callfrom == 5 && chk.checked == true)        //onfocus of  Loded Weight
            {
                hdn_tolalLodWt.value = val(hdn_tolalLodWt.value) - val(Loaded_Wt[0].value);
                lbl_tolalLodWt.innerHTML = val(hdn_tolalLodWt.value);      
            }

            
            hdn_Total_ActualWt.value = val(hdn_Book_ActualWt.value) + val(hdn_Cros_ActualWt.value);
            txt_Total_ActualWt.value = val(hdn_Book_ActualWt.value) + val(hdn_Cros_ActualWt.value);
            hdn_Total_ToPayCollection.value = val(txt_Book_ToPayCollection.value) + val(hdn_Cros_ToPayCollection.value);
            txt_Total_ToPayCollection.value = val(txt_Book_ToPayCollection.value) + val(hdn_Cros_ToPayCollection.value);

            if((grid.rows.length-1) == val(hdn_Total_GC.value))
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
       
        var hdn_Total_GC = document.getElementById('WucLabelPrinting1_hdn_Total_GC');
        var hdn_GCCaption = document.getElementById('WucLabelPrinting1_hdn_GCCaption');
        
        var lbl_Error_Client = document.getElementById('WucLabelPrinting1_lbl_Error_Client');
        var lbl_Error = document.getElementById('WucLabelPrinting1_lbl_Errors');
        
        lbl_Error_Client.innerHTML ="";
        lbl_Error.innerHTML ="";
                
        if(val(hdn_Total_GC.value) == 0)
        {
            lbl_Error_Client.innerHTML = "Please Select Atleast One " + hdn_GCCaption.value;
        }
        else
        {
            ATS = true;
        }
        return ATS;
     }    

 

 
 