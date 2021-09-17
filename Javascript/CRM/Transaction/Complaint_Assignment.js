// JScript File



  function Allow_To_Save()
    {
        var ATS = false;

     
        var lbl_Errors = document.getElementById('wuc_Complaint_Assignment1_lbl_Errors');
      
        var ddl_Ticket = document.getElementById('wuc_Complaint_Assignment1_ddl_Ticket');

      
        lbl_Errors.innerHTML ="";
        
//         if(ddl_Ticket.value == 0)
//        {
//            lbl_Errors.innerHTML += "Please Select Ticket  ";
//            ddl_Ticket.focus();
//        }         
////        else if (hdn_Profile_Location_List_Count.value == 0 || hdn_Profile_Location_List_Count.value == '' )
////        {
////            lbl_Errors.innerHTML +=  "Please select User Selection Criteria.<br>";           
////        } 
////         else if (hdn_User_Count.value == 0 || hdn_User_Count.value == '' )
////        {
////            lbl_Errors.innerHTML +=  "Please select User Selection Criteria.<br>";           
////        } 
//            
//        else
        {
            ATS = true;
        }
        return ATS;
     }    

//    function Check_All(chk,gridname)
//    {              
//        var grid = document.getElementById(gridname);
//        var checkbox;
//        var i,j=0;
//        
//        var max = (grid.rows.length - 1);
//        for(i=1;i<grid.rows.length-1;i++)
//        {
//            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
//            
//            if(checkbox[0].type == 'checkbox')
//            {
//                checkbox[0].checked = chk.checked;
//            }
//        }
//    }
     
