// JScript File

 
    function Check_All(chk,gridname)
    {              
        var grid = document.getElementById(gridname);
        var checkbox,Freight,Weight,Articles,GCAmount,SerTaxAmt;
        var i,j=0;
        var sum_LodArt=0,sum_LodWt=0;
        var Loaded_Wt,Loaded_Art;

        var lbl_tolalLodArt = document.getElementById('WucALS1_lbl_tolalLodArt');
        var lbl_tolalLodWt = document.getElementById('WucALS1_lbl_tolalLodWt');
        var lbl_Total_GC = document.getElementById('WucALS1_lbl_Total_GC');

        var hdn_tolalLodArt= document.getElementById('WucALS1_hdn_tolalLodArt');
        var hdn_tolalLodWt= document.getElementById('WucALS1_hdn_tolalLodWt');        
        var hdn_Total_GC = document.getElementById('WucALS1_hdn_Total_GC');
        
        var max = (grid.rows.length - 1);
        for(i=1;i<grid.rows.length;i++)
        {            
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            Loaded_Art  = grid.rows[i].cells[13].getElementsByTagName('input');
            Loaded_Wt = grid.rows[i].cells[14].getElementsByTagName('input');

            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            } 
            
            if(chk.checked == true)
            {
                if(Loaded_Art[0].type =='text')
                {
                    sum_LodArt = sum_LodArt + val(Loaded_Art[0].value);
                }
                if(Loaded_Wt[0].type =='text')
                {
                    sum_LodWt = sum_LodWt + val(Loaded_Wt[0].value);
                }
            }
            
        }
        if(chk.checked == true)
        {
            lbl_tolalLodArt.innerHTML  = sum_LodArt;
            lbl_tolalLodWt.innerHTML  = sum_LodWt;
            lbl_Total_GC.innerHTML = max;
                    
            hdn_Total_GC.value = max;
            hdn_tolalLodArt.value = sum_LodArt;
            hdn_tolalLodWt.value = sum_LodWt;
        }
        else
        {
            lbl_tolalLodArt.innerHTML  = 0;
            lbl_tolalLodWt.innerHTML  = 0;
            lbl_Total_GC.innerHTML = 0;
                    
            hdn_Total_GC.value = 0;
            hdn_tolalLodArt.value = 0;
            hdn_tolalLodWt.value = 0;
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
            var grid = document.getElementById('WucALS1_dg_ALS');
        }
       
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;

        var Loaded_Art,Loaded_Wt;

        var lbl_tolalLodArt = document.getElementById('WucALS1_lbl_tolalLodArt');
        var lbl_tolalLodWt = document.getElementById('WucALS1_lbl_tolalLodWt');
        var lbl_Total_GC = document.getElementById('WucALS1_lbl_Total_GC');

        var hdn_tolalLodArt= document.getElementById('WucALS1_hdn_tolalLodArt');
        var hdn_tolalLodWt= document.getElementById('WucALS1_hdn_tolalLodWt');        
        var hdn_Total_GC = document.getElementById('WucALS1_hdn_Total_GC');
                
        Bal_Article= row.cells[11].getElementsByTagName('input');
        Bal_Act_Wt= row.cells[12].getElementsByTagName('input');
        Loaded_Art  = row.cells[13].getElementsByTagName('input');
        Loaded_Wt = row.cells[14].getElementsByTagName('input');
            
        if(val(Loaded_Art[0].value) > val(Bal_Article[0].value))
        {
            Loaded_Art[0].value = val(Bal_Article[0].value);
        }
        if(val(Loaded_Wt[0].value) > val(Bal_Act_Wt[0].value))
        {
            Loaded_Wt[0].value = val(Bal_Act_Wt[0].value);
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
        
        var txt_Supervisior = document.getElementById('WucALS1_ddl_Supervisior_txtBoxddl_Supervisior');
        var ddl_VehicleCotegory = document.getElementById('WucALS1_ddl_VehicleCotegory');
        var ddl_Vehicle = document.getElementById('WucALS1_WucVehicleSearch1_ddl_Vehicle');
        var txt_Vehicle_Last_4_Digits = document.getElementById('WucALS1_WucVehicleSearch1_txt_Vehicle_Last_4_Digits');
        var hdn_Total_GC = document.getElementById('WucALS1_hdn_Total_GC');
        var hdn_GCCaption = document.getElementById('WucALS1_hdn_GCCaption');
        
        var lbl_Error_Client = document.getElementById('WucALS1_lbl_Error_Client');
        var lbl_Error = document.getElementById('WucALS1_lbl_Errors');
        
        lbl_Error_Client.innerHTML ="";
        lbl_Error.innerHTML ="";                
         
        if(val(ddl_VehicleCotegory.value) <= 0)
        {
            lbl_Error_Client.innerHTML = "Please Select Vehicle Cotegory";
            ddl_VehicleCotegory.focus();
        }
        else if(val(ddl_Vehicle.value) <= 0)
        {
            lbl_Error_Client.innerHTML = "Please Select Vehicle";
            txt_Vehicle_Last_4_Digits.focus();
        }
        else if(val(hdn_Total_GC.value) == 0)
        {
            lbl_Error_Client.innerHTML = "Please Select Atleast One " + hdn_GCCaption.value;
        }
        else if(txt_Supervisior.value == '')
        {
            lbl_Error_Client.innerHTML = "Please Select Supervisior";
            txt_Supervisior.focus();
        }
        else
        {
            ATS = true;
        }
        return ATS;
     }  