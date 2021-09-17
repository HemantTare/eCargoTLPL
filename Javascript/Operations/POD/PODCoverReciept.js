// JScript File

 function Check_All(chk,gridname)   //currently not in use
    {              
        var grid = document.getElementById(gridname);
        var i,j=0; var checkbox;
     
        var hdn_TotalNoofGC = document.getElementById('WucPODCoverReciept1_hdn_TotalNoofGC');
        
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
            hdn_TotalNoofGC.value = max;
        }
        else
        {         
            hdn_TotalNoofGC.value = 0;
        }
    }
    
     function Check_Single(chk,gridname)  //currently not in use
    {
       
        var grid = document.getElementById(gridname);       
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;
       
        var hdn_TotalNoofGC = document.getElementById('WucPODCoverReciept1_hdn_TotalNoofGC');
     
        if(chk.checked == true)
        {
           hdn_TotalNoofGC.value = val(hdn_TotalNoofGC.value) + 1;
        }
        else
        {         
           hdn_TotalNoofGC.value = val(hdn_TotalNoofGC.value) - 1;
        }
        
        if((grid.rows.length-1) == val(hdn_TotalNoofGC.value))
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
        
        var ddl_CoverNo = document.getElementById('WucPODCoverReciept1_ddl_CoverNo');       
        var hdn_TotalNoofGC = document.getElementById('WucPODCoverReciept1_hdn_TotalNoofGC');
        var lbl_Errors = document.getElementById('WucPODCoverReciept1_lbl_Errors');
        
        lbl_Errors.innerHTML ="";
                
        if(val(ddl_CoverNo.value) <= 0)
        {
            lbl_Errors.innerText = "Please Select Cover No";//objResource.GetMsg("Msg_CoverNoResource");
            ddl_CoverNo.focus();
        } 
        else if(!ValidateWucPODDetails(lbl_Errors)) 
        {}  
        else if(val(hdn_TotalNoofGC.value) <= 0)
        {
            lbl_Errors.innerHTML = "Please Select At Least One Record";//objResource.GetMsg("Msg_dg_PODCoverRecieptResource");
        }           
        else
        {
            ATS = true;
        }
        return ATS;
     }