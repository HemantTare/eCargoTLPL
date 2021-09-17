  
    function Check_All(chk,gridname)
    {              
        var grid = document.getElementById(gridname);
        var checkbox,Freight,Weight,Articles,GCAmount,SerTaxAmt;
        var i,j=0;
        var sum_gcs=0,sum_freight=0,sum_weight=0,sum_articles=0,sum_tot_gc_amt=0,sum_tot_ser=0;
        
        var txt_Total_GC = document.getElementById('WucAccountTransfer1_txt_Total_GC');
        var txt_Total_Articles = document.getElementById('WucAccountTransfer1_txt_Total_Articles');
        var txt_Total_Basic_Freight = document.getElementById('WucAccountTransfer1_txt_Total_Basic_Freight');
        var txt_Total_Wt = document.getElementById('WucAccountTransfer1_txt_Total_Wt');
        var txt_Total_Ser_Tax = document.getElementById('WucAccountTransfer1_txt_Total_Ser_Tax');
        var txt_Total_GC_Amt = document.getElementById('WucAccountTransfer1_txt_Total_GC_Amt');
                
        var hdn_Total_GC= document.getElementById('WucAccountTransfer1_hdn_Total_GC');
        var hdn_Total_Articles= document.getElementById('WucAccountTransfer1_hdn_Total_Articles');
        var hdn_Tot_Basic_Fret = document.getElementById('WucAccountTransfer1_hdn_Tot_Basic_Fret');
        var hdn_Total_Wt = document.getElementById('WucAccountTransfer1_hdn_Total_Wt');
        var hdn_Total_Ser_Tax= document.getElementById('WucAccountTransfer1_hdn_Total_Ser_Tax');
        var hdn_Total_GC_Amt= document.getElementById('WucAccountTransfer1_hdn_Total_GC_Amt');
        
        var max = (grid.rows.length - 1);
        for(i=1;i<grid.rows.length;i++)
        {
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            Articles = grid.rows[i].cells[6].getElementsByTagName('input');
            Weight = grid.rows[i].cells[7].getElementsByTagName('input');
            Freight = grid.rows[i].cells[8].getElementsByTagName('input');
            SerTaxAmt = grid.rows[i].cells[10].getElementsByTagName('input');
            GCAmount = grid.rows[i].cells[11].getElementsByTagName('input');
            
            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            }
            if(chk.checked == true)
            {
                if(Freight[0].type =='text')
                {
                    sum_freight = sum_freight + val(Freight[0].value);
                }
                if(Weight[0].type =='text')
                {
                    sum_weight = sum_weight + val(Weight[0].value);
                }
                if(Articles[0].type =='text')
                {
                    sum_articles = sum_articles + val(Articles[0].value);
                }
                if(SerTaxAmt[0].type =='text')
                {
                    sum_tot_ser = sum_tot_ser + val(SerTaxAmt[0].value);
                }
                if(GCAmount[0].type =='text')
                {
                    sum_tot_gc_amt = sum_tot_gc_amt + val(GCAmount[0].value);
                }
            }
        }
        if(chk.checked == true)
        {
            txt_Total_GC.value = max;
            txt_Total_Articles.value = sum_articles;
            txt_Total_Basic_Freight.value = sum_freight;
            txt_Total_Wt.value  = sum_weight;
            txt_Total_Ser_Tax.value  = sum_tot_ser;
            txt_Total_GC_Amt.value  = sum_tot_gc_amt;
                    
            hdn_Total_GC.value = max;
            hdn_Total_Articles.value = sum_articles;
            hdn_Tot_Basic_Fret.value = sum_freight;
            hdn_Total_Wt.value  = sum_weight;
            hdn_Total_Ser_Tax.value = sum_tot_ser;
            hdn_Total_GC_Amt.value = sum_tot_gc_amt;
        }
        else
        {
            txt_Total_GC.value = 0;
            txt_Total_Articles.value = 0;
            txt_Total_Basic_Freight.value = 0;
            txt_Total_Wt.value  = 0;
            txt_Total_Ser_Tax.value  = 0;
            txt_Total_GC_Amt.value  = 0;
                    
            hdn_Total_GC.value = 0;
            hdn_Tot_Basic_Fret.value = 0;
            hdn_Total_Wt.value  = 0;
            hdn_Total_Articles.value = 0;
            hdn_Total_Ser_Tax.value = 0;
            hdn_Total_GC_Amt.value = 0;
        }
    }
        
    function Check_Single(chk,gridname)
    {
        var grid = document.getElementById(gridname);
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;
//        var sum_gcs=0,sum_freight=0,sum_weight=0,sum_articles=0,sum_tot_amt=0,sum_tot_tax=0;
        
        var txt_Total_GC = document.getElementById('WucAccountTransfer1_txt_Total_GC');
        var txt_Total_Articles = document.getElementById('WucAccountTransfer1_txt_Total_Articles');
        var txt_Total_Basic_Freight = document.getElementById('WucAccountTransfer1_txt_Total_Basic_Freight');
        var txt_Total_Wt  = document.getElementById('WucAccountTransfer1_txt_Total_Wt');
        var txt_Total_Ser_Tax = document.getElementById('WucAccountTransfer1_txt_Total_Ser_Tax');
        var txt_Total_GC_Amt = document.getElementById('WucAccountTransfer1_txt_Total_GC_Amt');
                
        var hdn_Total_GC = document.getElementById('WucAccountTransfer1_hdn_Total_GC');
        var hdn_Tot_Basic_Fret = document.getElementById('WucAccountTransfer1_hdn_Tot_Basic_Fret');
        var hdn_Total_Wt  = document.getElementById('WucAccountTransfer1_hdn_Total_Wt');
        var hdn_Total_Articles = document.getElementById('WucAccountTransfer1_hdn_Total_Articles');
        var hdn_Total_Ser_Tax= document.getElementById('WucAccountTransfer1_hdn_Total_Ser_Tax');
        var hdn_Total_GC_Amt= document.getElementById('WucAccountTransfer1_hdn_Total_GC_Amt');
        
            Articles= row.cells[6].getElementsByTagName('input');
            Weight  = row.cells[7].getElementsByTagName('input');
            Freight = row.cells[8].getElementsByTagName('input');
            SerTaxAmt = row.cells[10].getElementsByTagName('input');
            GCAmount = row.cells[11].getElementsByTagName('input');
            
            if (chk.checked == true)
            {
               txt_Total_GC.value = val(txt_Total_GC.value) + 1;
               txt_Total_Articles.value = val(txt_Total_Articles.value) + val(Articles[0].value);
               txt_Total_Basic_Freight.value = val(txt_Total_Basic_Freight.value) + val(Freight[0].value);
               txt_Total_Wt.value  = val(txt_Total_Wt.value)  + val(Weight[0].value);
               txt_Total_Ser_Tax.value  = val(txt_Total_Ser_Tax.value)  + val(SerTaxAmt[0].value);
               txt_Total_GC_Amt.value  = val(txt_Total_GC_Amt.value)  + val(GCAmount[0].value);
               
               hdn_Total_GC.value = val(hdn_Total_GC.value) + 1;
               hdn_Total_Articles.value = val(hdn_Total_Articles.value) + val(Articles[0].value);
               hdn_Tot_Basic_Fret.value = val(hdn_Tot_Basic_Fret.value) + val(Freight[0].value);
               hdn_Total_Wt.value  = val(hdn_Total_Wt.value)  + val(Weight[0].value);
               hdn_Total_Ser_Tax.value  = val(hdn_Total_Ser_Tax.value)  + val(SerTaxAmt[0].value);
               hdn_Total_GC_Amt.value  = val(hdn_Total_GC_Amt.value)  + val(GCAmount[0].value);
            }
            else
            {
               txt_Total_GC.value = val(txt_Total_GC.value) - 1;
               txt_Total_Articles.value = val(txt_Total_Articles.value) - val(Articles[0].value);
               txt_Total_Basic_Freight.value = val(txt_Total_Basic_Freight.value) - val(Freight[0].value);
               txt_Total_Wt.value  = val(txt_Total_Wt.value)  - val(Weight[0].value);
               txt_Total_Ser_Tax.value  = val(txt_Total_Ser_Tax.value)  - val(SerTaxAmt[0].value);
               txt_Total_GC_Amt.value  = val(txt_Total_GC_Amt.value)  - val(GCAmount[0].value);
               
               hdn_Total_GC.value = val(hdn_Total_GC.value) - 1;
               hdn_Total_Articles.value = val(hdn_Total_Articles.value) - val(Articles[0].value);
               hdn_Tot_Basic_Fret.value = val(hdn_Tot_Basic_Fret.value) - val(Freight[0].value);
               hdn_Total_Wt.value  = val(hdn_Total_Wt.value)  - val(Weight[0].value);
               hdn_Total_Ser_Tax.value  = val(hdn_Total_Ser_Tax.value)  - val(SerTaxAmt[0].value);
               hdn_Total_GC_Amt.value  = val(hdn_Total_GC_Amt.value)  - val(GCAmount[0].value);
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
        

    function Allow_To_Save()
    {
        var ATS = false;

        var ddl_Associate_Name = document.getElementById('WucAccountTransfer1_ddl_Associate_Name');
        var hdn_Total_GC = document.getElementById('WucAccountTransfer1_hdn_Total_GC');
        var lbl_Error_Client = document.getElementById('WucAccountTransfer1_lbl_Error_Client');
        var AT_Date = WucAccountTransfer1_AT_Date.GetSelectedDate();
        AT_Date = new Date(AT_Date.getFullYear(), AT_Date.getMonth(),AT_Date.getDate())
        var Todays_Date = new Date ();
        Todays_Date = new Date(Todays_Date.getFullYear(), Todays_Date.getMonth(),Todays_Date.getDate())
        lbl_Error_Client.innerHTML ="";
                
         var objResource=new Resource('WucAccountTransfer1_hdf_ResourecString');
         
//        if (val(ddl_Associate_Name.value) == 0)
//        {
//            lbl_Error_Client.innerHTML =  objResource.GetMsg("Msg_ddl_Associates");
//        }
//        else 
//        if(AT_Date > Todays_Date)
//        {
//            lbl_Error_Client.innerHTML = objResource.GetMsg("Msg_dtp_ATDate");
//        }
        if(val(hdn_Total_GC.value) == 0)
        {
            lbl_Error_Client.innerHTML = objResource.GetMsg("Msg_grid_Validation");
        }
        else
        {
            ATS = true;
        }
        return ATS;
     }    
