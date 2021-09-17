// JScript File

function Check_All(chk,gridname)
    {              
        var grid = document.getElementById(gridname);
        var i,j=0; var checkbox;
     
        var max = (grid.rows.length - 1);
        for(i=1;i<grid.rows.length;i++)
        {            
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            
            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            } 
        }
               
    }
    
    
    function Allow_To_Save()
    {
        var ATS = false;
        
        var lbl_Errors = document.getElementById('WucPODDeliverySheet1_lbl_Errors');
        
        lbl_Errors.innerHTML ="";
         
        if(!ValidateWucPODDetails(lbl_Errors)) 
        {}   
        else
        {
            ATS = true;
        }
        return ATS;
     }
     