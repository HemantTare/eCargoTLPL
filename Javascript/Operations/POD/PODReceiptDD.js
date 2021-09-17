// JScript File

//Created : Ankit champaneriya
//Date : 2/12/08
//Desc : Pod Receipt DD 

    function Allow_To_Save()
    {
        var ATS = false;
        
        var lbl_Errors = document.getElementById('WucPODReceiptDD1_lbl_Errors');
       
        lbl_Errors.innerHTML ="";
                
         
        if(!ValidateWucPODDetails(lbl_Errors)) 
        {}   
        else
        {
            ATS = true;
        }
        return ATS;
     }    
    