// JScript File


    function Check_All(chk,gridname)
    {              
        var grid = document.getElementById(gridname);
        var i,j=0; var checkbox;

        var lbl_total_GC = document.getElementById('WucPODCoverGeneration1_lbl_Total_GC');
        var hdn_total_GC = document.getElementById('WucPODCoverGeneration1_hdn_Total_GC');
        
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
        
    function Check_Single(chk,gridname)
    {
       
        var grid = document.getElementById(gridname);       
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;

        var lbl_total_GC = document.getElementById('WucPODCoverGeneration1_lbl_Total_GC');
        var hdn_total_GC = document.getElementById('WucPODCoverGeneration1_hdn_Total_GC');
     
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
        
        if((grid.rows.length-1) == val(hdn_total_GC.value))
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
        
        //var ddl_SentBy = document.getElementById('WucPODCoverGeneration1_ddl_SentBy');
        var txt_CourierName = document.getElementById('WucPODCoverGeneration1_txt_CourierName');
        var txt_CourierDocketNo = document.getElementById('WucPODCoverGeneration1_txt_CourierDocketNo');
        var ddl_hierarchy = document.getElementById('WucPODCoverGeneration1_WucHierarchyWithID1_ddl_hierarchy');
        var ddl_location = document.getElementById('WucPODCoverGeneration1_WucHierarchyWithID1_ddl_location');
        //var ddl_Vehicle = document.getElementById('WucPODCoverGeneration1_WucVehicleSearch1_ddl_Vehicle');
        //var txt_Vehicle_Last_4_Digits = document.getElementById('WucPODCoverGeneration1_WucVehicleSearch1_txt_Vehicle_Last_4_Digits');
        //var txt_Employee = document.getElementById('WucPODCoverGeneration1_ddl_Employee_txtBoxddl_Employee');
        var hdn_Total_GC = document.getElementById('WucPODCoverGeneration1_hdn_Total_GC');        
        var lbl_Error_Client = document.getElementById('WucPODCoverGeneration1_lbl_Error_Client');
        var hdn_GCError = document.getElementById('WucPODCoverGeneration1_hdn_GCError');
        
        lbl_Error_Client.innerHTML ="";
         
        if(ddl_hierarchy.value == '0')
        {
            lbl_Error_Client.innerText = "Please Select Hierarchy";//objResource.GetMsg("Msg_ddl_hierarchy");
            ddl_hierarchy.focus();
        }
        else if(ddl_hierarchy.value != 'HO' && val(ddl_location.value) <= 0)
        {
            lbl_Error_Client.innerText = "Please Select Location";// objResource.GetMsg("Msg_ddl_location");
            ddl_location.focus();
        }        
//        else if(val(ddl_SentBy.value) <= 0)
//        {
//            lbl_Error_Client.innerHTML =  objResource.GetMsg("Msg_ddl_SentBy");
//            ddl_SentBy.focus();
//        }       
//        else if(val(ddl_SentBy.value) == 1 && txt_CourierName.value == '')
//        {
//            lbl_Error_Client.innerHTML = objResource.GetMsg("Msg_txt_CourierName");
//            txt_CourierName.focus();
//        }  
//        else if(val(ddl_SentBy.value) == 1 && txt_CourierDocketNo.value == '')
//        {
//            lbl_Error_Client.innerHTML = objResource.GetMsg("Msg_txt_CourierNo");
//            txt_CourierDocketNo.focus();
//        }
//        else if(val(ddl_SentBy.value) == 2 && txt_Employee.value == '')
//        {
//            lbl_Error_Client.innerHTML = objResource.GetMsg("Msg_txt_Employee");
//            txt_Employee.focus();
//        }   
//        else if(val(ddl_SentBy.value) == 3 && val(ddl_Vehicle.value) <= 0)
//        {
//            lbl_Error_Client.innerHTML = objResource.GetMsg("Msg_txt_Vehicle");
//            txt_Vehicle_Last_4_Digits.focus();
//        }
        else if(!ValidateWucPODDetails(lbl_Error_Client)) 
        {}     
        else if(val(hdn_Total_GC.value) <= 0)
        {
            lbl_Error_Client.innerHTML = hdn_GCError.value ; 
        }           
        else
        {
            ATS = true;
        }
        return ATS;
     }    

function enabledisable_sentType()
{
    var ddl_SentBy = document.getElementById('WucPODCoverGeneration1_ddl_SentBy');
    var lbl_CourierText = document.getElementById('WucPODCoverGeneration1_lbl_CourierText');
    var tr_txt_courier = document.getElementById('tr_txt_courier');
    var tr_ddl_employee = document.getElementById('tr_ddl_employee');
    var tr_ddl_vehicle = document.getElementById('tr_ddl_vehicle');
    var td_lbl_courierno = document.getElementById('td_lbl_courierno');
    var td_txt_courierno = document.getElementById('td_txt_courierno');

    if(parseInt(ddl_SentBy.value) == 1)
    {
        lbl_CourierText.innerText = 'Courier Name :';
        tr_txt_courier.style.display = 'inline';
        td_lbl_courierno.style.display = 'inline';
        td_txt_courierno.style.display = 'inline';
        tr_ddl_employee.style.display = 'none';
        tr_ddl_vehicle.style.display = 'none';
    }
    else if(parseInt(ddl_SentBy.value) == 2)
    {
        lbl_CourierText.innerText = 'Employee Name :';
        tr_ddl_employee.style.display = 'inline';
        tr_txt_courier.style.display = 'none';
        td_lbl_courierno.style.display = 'none';
        td_txt_courierno.style.display = 'none';
        tr_ddl_vehicle.style.display = 'none';
    }
    else if(parseInt(ddl_SentBy.value) == 3)
    {
        lbl_CourierText.innerText = 'Vehicle :';
        tr_ddl_vehicle.style.display = 'inline';
        tr_ddl_employee.style.display = 'none';
        tr_txt_courier.style.display = 'none';
        td_lbl_courierno.style.display = 'none';
        td_txt_courierno.style.display = 'none';
    }
    else
    {
        lbl_CourierText.innerText = 'Courier Name :';
        tr_txt_courier.style.display = 'inline';
        td_lbl_courierno.style.display = 'inline';
        td_txt_courierno.style.display = 'inline';
        tr_ddl_employee.style.display = 'none';
        tr_ddl_vehicle.style.display = 'none';
    }  
}