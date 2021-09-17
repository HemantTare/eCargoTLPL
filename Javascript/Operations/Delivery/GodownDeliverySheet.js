// JScript File
  function setfousonGrid(gridname)
  {  
//   var grid = document.getElementById('WucGDC1_dg_GDC');
//   var gridCheckbox1 = document.getElementById('WucGDC1_dg_GDC_Checkbox1');
//   var checkbox;
//   
//   checkbox = grid.rows[0].cells[0].getElementsByTagName('input'); 
//    checkbox[0].focus();  
      
  }
  
    function Check_All(chk,gridname)
    {              
        var grid = document.getElementById(gridname);
        var i,j=0;
        var sum_DelArt=0,sum_DelWt=0;
        var checkbox,Delivered_Art,Delivered_ActWt;

        var lbl_totalDelArt = document.getElementById('WucGDC1_lbl_totalDelArt');
        var lbl_totalDelWt = document.getElementById('WucGDC1_lbl_totalDelWt');
        var lbl_total_GC = document.getElementById('WucGDC1_lbl_Total_GC');

        var hdn_totalDelArt= document.getElementById('WucGDC1_hdn_totalDelArt');
        var hdn_totalDelWt= document.getElementById('WucGDC1_hdn_totalDelWt');
        var hdn_total_GC = document.getElementById('WucGDC1_hdn_Total_GC');
        
        var max = (grid.rows.length - 1);
        for(i=1;i<grid.rows.length;i++)
        {            
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            txt_Delivered_Art = grid.rows[i].cells[5].getElementsByTagName('input');
            txt_Delivered_ActWt = grid.rows[i].cells[16].getElementsByTagName('input');
            
            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            }
                      
            if(chk.checked == true)
            {               
                if(txt_Delivered_Art[0].type =='text')
                {
                    sum_DelArt = sum_DelArt + val(txt_Delivered_Art[0].value);
                }
                if(txt_Delivered_ActWt[0].type =='text')
                {
                    sum_DelWt = sum_DelWt + val(txt_Delivered_ActWt[0].value);
                }
            }
            
        }
        
        if(chk.checked == true)
        {
            lbl_totalDelArt.innerHTML  = sum_DelArt;
            lbl_totalDelWt.innerHTML  = sum_DelWt;
            lbl_total_GC.innerHTML = max;
                    
            hdn_total_GC.value = max;
            hdn_totalDelArt.value = sum_DelArt;
            hdn_totalDelWt.value = sum_DelWt;
        }
        else
        {
            lbl_totalDelArt.innerHTML  = 0;
            lbl_totalDelWt.innerHTML  = 0;
            lbl_total_GC.innerHTML = 0;
                    
            hdn_total_GC.value = 0;
            hdn_totalDelArt.value = 0;
            hdn_totalDelWt.value = 0;
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
            var grid = document.getElementById('WucGDC1_dg_GDC');
        }
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;
        var Delivered_Art,Delivered_ActWt;

        var lbl_total_DelArt = document.getElementById('WucGDC1_lbl_totalDelArt');
        var lbl_total_DelWt = document.getElementById('WucGDC1_lbl_totalDelWt');
        var lbl_total_GC = document.getElementById('WucGDC1_lbl_Total_GC');

        var hdn_total_DelArt= document.getElementById('WucGDC1_hdn_totalDelArt');
        var hdn_total_DelWt= document.getElementById('WucGDC1_hdn_totalDelWt');
        var hdn_total_GC = document.getElementById('WucGDC1_hdn_Total_GC');
 
        txt_Balance_Art= row.cells[4].getElementsByTagName('input');
        txt_Balance_ActWt  = row.cells[15].getElementsByTagName('input');
        txt_Delivered_Art = row.cells[5].getElementsByTagName('input');
        txt_Delivered_ActWt = row.cells[16].getElementsByTagName('input');

        if(val(txt_Delivered_Art[0].value) > val(txt_Balance_Art[0].value))
        {
            txt_Delivered_Art[0].value = val(txt_Balance_Art[0].value);
        }
        if(val(txt_Delivered_ActWt[0].value) > val(txt_Balance_ActWt[0].value))
        {
            txt_Delivered_ActWt[0].value = val(txt_Balance_ActWt[0].value);
        }
        if(callfrom == 1)
        {
            if(chk.checked == true)
            {
               lbl_total_GC.innerHTML = val(lbl_total_GC.innerHTML) + 1;
               lbl_total_DelArt.innerHTML = val(lbl_total_DelArt.innerHTML) + val(txt_Delivered_Art[0].value);
               lbl_total_DelWt.innerHTML = val(lbl_total_DelWt.innerHTML) + val(txt_Delivered_ActWt[0].value);

               hdn_total_GC.value = val(hdn_total_GC.value) + 1;
               hdn_total_DelArt.value = val(hdn_total_DelArt.value) + val(txt_Delivered_Art[0].value);
               hdn_total_DelWt.value = val(hdn_total_DelWt.value) + val(txt_Delivered_ActWt[0].value);
            }
            else
            {
               lbl_total_GC.innerHTML = val(lbl_total_GC.innerHTML) - 1;
               lbl_total_DelArt.innerHTML = val(lbl_total_DelArt.innerHTML) - val(txt_Delivered_Art[0].value);
               lbl_total_DelWt.innerHTML = val(lbl_total_DelWt.innerHTML) - val(txt_Delivered_ActWt[0].value);
               
               hdn_total_GC.value = val(hdn_total_GC.value) - 1;
               hdn_total_DelArt.value = val(hdn_total_DelArt.value) - val(txt_Delivered_Art[0].value);
               hdn_total_DelWt.value = val(hdn_total_DelWt.value) - val(txt_Delivered_ActWt[0].value);
            }
        }
                       
        if(callfrom == 2)       //onblur of  Delivered Article
        {
            if(chk.checked == true)
            {
                hdn_total_DelArt.value = val(hdn_total_DelArt.value) + val(txt_Delivered_Art[0].value);
                lbl_total_DelArt.innerHTML = val(hdn_total_DelArt.value);
            }               
        }
        if(callfrom == 3)       //onblur of  Delivered Weight
        {
            if(chk.checked == true)
            {
                hdn_total_DelWt.value = val(hdn_total_DelWt.value) + val(txt_Delivered_ActWt[0].value);
                lbl_total_DelWt.innerHTML = val(hdn_total_DelWt.value);
            }               
        }
        if(callfrom == 4 && chk.checked == true)       //onfocus of  Delivered Article
        {
            hdn_total_DelArt.value = val(hdn_total_DelArt.value) - val(txt_Delivered_Art[0].value);
            lbl_total_DelArt.innerHTML = val(hdn_total_DelArt.value);
        }
        if(callfrom == 5 && chk.checked == true)        //onfocus of  Delivered Weight
        {
            hdn_total_DelWt.value = val(hdn_total_DelWt.value) - val(txt_Delivered_ActWt[0].value);
            lbl_total_DelWt.innerHTML = val(hdn_total_DelWt.value);      
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
        
        var txt_GodownSupervisor = document.getElementById('WucGDC1_ddl_GodownSupervisor_txtBoxddl_GodownSupervisor');
        var hdn_Total_GC = document.getElementById('WucGDC1_hdn_Total_GC');
        var hdn_GCCaption = document.getElementById('WucGDC1_hdn_GCCaption');
        var txt_DeliveredTo = document.getElementById('WucGDC1_txt_DeliveredTo');
        var txt_DeliveredToMobile = document.getElementById('WucGDC1_txt_DeliveredToMobile');
        var ddl_PhotoIDType = document.getElementById('WucGDC1_ddl_PhotoIDType');
        var txt_PhotoIDNo = document.getElementById('WucGDC1_txt_PhotoIDNo');
        var ddl_VehicleType = document.getElementById('WucGDC1_ddl_VehicleType');
        var txt_Number_Part1 = document.getElementById('WucGDC1_txt_Number_Part1');
        var txt_Number_Part4 = document.getElementById('WucGDC1_txt_Number_Part4');
        
        var lbl_Error_Client = document.getElementById('WucGDC1_lbl_Error_Client');
        lbl_Error_Client.innerHTML ="";
                
//        var objResource=new Resource('WucGDC1_hdf_ResourecString');
         
        if(val(hdn_Total_GC.value) == 0)
        {
            lbl_Error_Client.innerHTML = "Please Select Atleast One " + hdn_GCCaption.value;
        }           
        else if(txt_GodownSupervisor.value == '')
        {
            lbl_Error_Client.innerHTML = "Please Select Supervisor";//objResource.GetMsg("Msg_txt_Supervisor");
            txt_GodownSupervisor.focus();
        }
        else if(txt_DeliveredTo.value == '')
        {
            lbl_Error_Client.innerHTML = "Please Enter Delivered To.";//objResource.GetMsg("Msg_txt_Supervisor");
            txt_DeliveredTo.focus();
        }
        else if(txt_DeliveredToMobile.value == '')
        {
            lbl_Error_Client.innerHTML = "Please Enter Delivered To Mobile No.";//objResource.GetMsg("Msg_txt_Supervisor");
            txt_DeliveredToMobile.focus();
        }
        else if(txt_DeliveredToMobile.value.length < 10)
        {
            lbl_Error_Client.innerHTML = "Please Enter Valid Mobile No.";//objResource.GetMsg("Msg_txt_Supervisor");
            txt_DeliveredToMobile.focus();
        }
        else if(val(ddl_PhotoIDType.value) < 0)
        {
            lbl_Error_Client.innerHTML = "Please Select PhotoID Type";//objResource.GetMsg("Msg_txt_Supervisor");
            ddl_PhotoIDType.focus();
        }
        else if(txt_PhotoIDNo.value == '')
        {
            lbl_Error_Client.innerHTML = "Please Enter PhotoID No.";//objResource.GetMsg("Msg_txt_Supervisor");
            txt_PhotoIDNo.focus();
        }
        else if(val(ddl_VehicleType.value) > 0 && txt_Number_Part1.value.length < 2)
        {
            lbl_Error_Client.innerHTML = "Truck Number Should Not Be less than 2 characters";//objResource.GetMsg("Msg_txt_Supervisor");
            txt_Number_Part1.focus();
        }
        else if(val(ddl_VehicleType.value) > 0 && txt_Number_Part4.value.length < 1)
        {
            lbl_Error_Client.innerHTML = "Truck Number Should Should have atleast 1 Digit";//objResource.GetMsg("Msg_txt_Supervisor");
            txt_Number_Part4.focus();
        }
        else
        {
            ATS = true;
        }
        return ATS;
     }    
     
    function Open_Details_Window(Path)
    {
        window.open(Path,'GDC','width=700,height=350,top=200,left=250,menubar=no,resizable=no,scrollbars=no')
        return false;
    }
     
    function Open_Details_Window(Path,ddl_DeliveryStatus,GC_Id,Article_Id,txt_GCAmount)
    { 
    
      if (ddl_DeliveryStatus.value == 0)
      {
              alert('Please Select Delivery Status');
              return false;
      }
      else if (ddl_DeliveryStatus.value == 1)
      {
              alert('Not Valid For Cash Entry');
              return false;
      }
      else if (ddl_DeliveryStatus.value == 2 && val(txt_GCAmount) < 500)
      {
              alert('Cheque Can Not Be Accepted For Freight Less Than Rs. 500.');
              return false;
      }     
      else if (ddl_DeliveryStatus.value == 5)
      {
              alert('Not Valid For Mobile Pay');
              return false;
      }
      else if (ddl_DeliveryStatus.value == 6)
      {
              alert('Not Valid For Swipe Card');
              return false;
      }
      else
      { 
        var Path = Path + "&DeliveryStatusID=" + ddl_DeliveryStatus.value + "&GC_Id=" + GC_Id.value + "&Article_Id=" + Article_Id.value; 
        window.open(Path,'GDC','width=600,height=350,top=200,left=250,menubar=no,resizable=no,scrollbars=no')
        return false;
      }
    }
    
    function Open_Dly_Receipt(Path,ddl_DeliveryStatus)
    {
      
      if (ddl_DeliveryStatus.value == 1 || ddl_DeliveryStatus.value == 2)
      {  
        window.open(Path,'GCDlyReceipt','width=950,height=900,top=200,left=50,menubar=no,resizable=no,scrollbars=no')
        return false;
      }
    }