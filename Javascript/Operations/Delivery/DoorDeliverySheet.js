// JScript File
  function setFocusonPageLoad() 
  {
      var hdn_Mode = document.getElementById('WucDDC1_hdn_Mode');
      if (hdn_Mode.value == "1") 
      {
          var ddl_DeliveryMode = document.getElementById('WucDDC1_ddl_DeliveryMode');
          ddl_DeliveryMode.focus();
      }
  }
  function setFocusonddlArea(ddl_DeliveryStatus)
    { 
        var ddl_DeliveryStatus = document.getElementById(ddl_DeliveryStatus); 
         
        ddl_DeliveryStatus.focus();
    }
    function Check_All(chk,gridname)
    {              
        var grid = document.getElementById(gridname);
        var i,j=0;
        var sum_DelArt=0,sum_DelWt=0;
        var checkbox,txt_Delivered_Art,txt_Delivered_ActWt,txt_Status,ddl_UnDel_Reason,txt_Del_Tak_By;

        var lbl_totalDelArt = document.getElementById('WucDDC1_lbl_totalDelArt');
        var lbl_totalDelWt = document.getElementById('WucDDC1_lbl_totalDelWt');

        var hdn_totalDelArt= document.getElementById('WucDDC1_hdn_totalDelArt');
        var hdn_totalDelWt= document.getElementById('WucDDC1_hdn_totalDelWt');
        var hdn_total_GC = document.getElementById('WucDDC1_hdn_Total_GC');
        var hdnforselectall = document.getElementById('WucDDC1_hdnforselectall');

        var max = (grid.rows.length - 1);
        
        hdnforselectall.value = max;
        
        for(i=1;i<grid.rows.length;i++)
        {
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            txt_Delivered_Art = grid.rows[i].cells[5].getElementsByTagName('input');
            txt_Delivered_ActWt = grid.rows[i].cells[6].getElementsByTagName('input');
            txt_Status = grid.rows[i].cells[9].getElementsByTagName('input');
            ddl_UnDel_Reason = grid.rows[i].cells[10].getElementsByTagName('select');
            txt_Del_Tak_By = grid.rows[i].cells[11].getElementsByTagName('input');
            
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
            
            if(chk.checked == true)
            {
                txt_Status[0].value = txt_Status[2].value;
                txt_Status[3].value = txt_Status[1].value;
            }
            else
            {
                txt_Status[0].value = 'UnDelivered';
                txt_Status[3].value = '300';
            }
            
            EnableDisable_Reason(txt_Status[3].value,ddl_UnDel_Reason); // for enable disable undelivered reason
            
        }        
        
        if(chk.checked == true)
        {
            hdn_total_GC.value = max;
            lbl_totalDelArt.innerHTML  = sum_DelArt;
            lbl_totalDelWt.innerHTML  = sum_DelWt;
            hdn_totalDelArt.value = sum_DelArt;
            hdn_totalDelWt.value = sum_DelWt;
            txt_Del_Tak_By.disabled = true;
        }
        else
        {
            hdn_total_GC.value = 0;
            lbl_totalDelArt.innerHTML  = 0;
            lbl_totalDelWt.innerHTML  = 0;
            hdn_totalDelArt.value = 0;
            hdn_totalDelWt.value = 0;
            txt_Del_Tak_By.disabled = false;
        }
    }
        
    function Check_Single(chk,gridname)
    {      
        var grid = document.getElementById(gridname);
       
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;
        var txt_Delivered_Art,txt_Delivered_ActWt,txt_Status,ddl_UnDel_Reason;

        var lbl_total_DelArt = document.getElementById('WucDDC1_lbl_totalDelArt');
        var lbl_total_DelWt = document.getElementById('WucDDC1_lbl_totalDelWt');

        var hdn_total_DelArt= document.getElementById('WucDDC1_hdn_totalDelArt');
        var hdn_total_DelWt= document.getElementById('WucDDC1_hdn_totalDelWt');
        var hdn_total_GC = document.getElementById('WucDDC1_hdn_Total_GC');
        var hdnforselectall = document.getElementById('WucDDC1_hdnforselectall');

        txt_Delivered_Art = row.cells[5].getElementsByTagName('input');
        txt_Delivered_ActWt = row.cells[6].getElementsByTagName('input');
//        txt_Status = row.cells[9].getElementsByTagName('input');
//        ddl_UnDel_Reason = row.cells[10].getElementsByTagName('select');
                
        hdnforselectall.value = (grid.rows.length - 1);
        
        if(chk.checked == true)
        {
           lbl_total_DelArt.innerHTML = val(lbl_total_DelArt.innerHTML) + val(txt_Delivered_Art[0].value);
           lbl_total_DelWt.innerHTML = val(lbl_total_DelWt.innerHTML) + val(txt_Delivered_ActWt[0].value);
//           txt_Status[0].value = txt_Status[2].value;

           hdn_total_DelArt.value = val(hdn_total_DelArt.value) + val(txt_Delivered_Art[0].value);
           hdn_total_DelWt.value = val(hdn_total_DelWt.value) + val(txt_Delivered_ActWt[0].value);
//           txt_Status[3].value = txt_Status[1].value;
           hdn_total_GC.value =val(hdn_total_GC.value) + 1;
        }
        else
        {
           lbl_total_DelArt.innerHTML = val(lbl_total_DelArt.innerHTML) - val(txt_Delivered_Art[0].value);
           lbl_total_DelWt.innerHTML = val(lbl_total_DelWt.innerHTML) - val(txt_Delivered_ActWt[0].value);
//           txt_Status[0].value = 'UnDelivered';
           
           hdn_total_DelArt.value = val(hdn_total_DelArt.value) - val(txt_Delivered_Art[0].value);
           hdn_total_DelWt.value = val(hdn_total_DelWt.value) - val(txt_Delivered_ActWt[0].value);
//           txt_Status[3].value = '300';
           hdn_total_GC.value =val(hdn_total_GC.value) - 1;
        }
              
//        EnableDisable_Reason(txt_Status[3].value,ddl_UnDel_Reason); // for enable disable undelivered reason
  
        if((grid.rows.length-1) == val(hdn_total_GC.value))
        {
            checkall[0].checked = true;
        }
        else
        {
            checkall[0].checked = false;
        }
    }
    
    function EnableDisable_Reason(val,ddl_UnDel_Reason)
    {
        if(val == 300)
        {
            ddl_UnDel_Reason[0].disabled = false;
        }
        else
        {
            ddl_UnDel_Reason[0].disabled = true;
            ddl_UnDel_Reason[0].value = 0;
        }

    }
    

    function Allow_To_Save()
    { 
        CalculateBalance();
        var ATS = false;
        var ddl_PDSNo = document.getElementById('WucDDC1_ddl_PDSNo');
        var ddl_DeliveryMode = document.getElementById('WucDDC1_ddl_DeliveryMode');
        var ddl_Supervisor = document.getElementById('WucDDC1_ddl_Supervisor_txtBoxddl_Supervisor');
                  
        var hdnforselectall = document.getElementById('WucDDC1_hdnforselectall');
        var hdn_GCCaption = document.getElementById('WucDDC1_hdn_GCCaption');
        var lbl_txt_BalancedCash = document.getElementById('WucDDC1_lbl_txt_BalancedCash');
        var lbl_txt_TotalCash = document.getElementById('WucDDC1_lbl_txt_TotalCash');
        var txt_CashReceived = document.getElementById('WucDDC1_txt_CashReceived');
        var txt_DriverName = document.getElementById('WucDDC1_txt_DriverName');
        
        var lbl_Error_Client = document.getElementById('WucDDC1_lbl_Errors');
        lbl_Error_Client.innerHTML ="";
                
//        var objResource=new Resource('WucDDC1_hdf_ResourecString');
         
        if(val(ddl_PDSNo.value) <= 0)
        {
            lbl_Error_Client.innerHTML = "Please Select PDS No";// objResource.GetMsg("Msg_PDS_validation");
            ddl_PDSNo.focus();
        }
        else if(val(ddl_DeliveryMode.value) <= 0)
        {
       
            lbl_Error_Client.innerHTML = "Please Select Atleast One Delivery Mode";
            ddl_DeliveryMode.focus();
        } 
        else if((val(lbl_txt_BalancedCash.value) != 0))
        {
            lbl_Error_Client.innerHTML = "Cash Balance Should be Zero";
            lbl_txt_BalancedCash.focus();
        }
        else if (val(txt_DriverName.value.length) < 4) {

            if (val(ddl_DeliveryMode.value) == 2) {
                lbl_Error_Client.innerHTML = "Please Enter Valid Driver Name";
            }
            else if (val(ddl_DeliveryMode.value) == 3) {
                lbl_Error_Client.innerHTML = "Please Enter Valid Hand Cart No";
            }
            else if (val(ddl_DeliveryMode.value) == 4) {
                lbl_Error_Client.innerHTML = "Please Enter Valid Person Name";
            }

            txt_DriverName.focus();
        } 
        else if(ddl_Supervisor.value == '')
        {
            lbl_Error_Client.innerHTML = "Please Select Godown Supervisor";
            ddl_Supervisor.focus();
        }  
        else if(val(lbl_txt_BalancedCash.value) < 0)
        {
             lbl_Error_Client.innerHTML = "Balanced Cash Cannot be Zero";
             txt_CashReceived.focus();
        } 
        else if(val(hdnforselectall.value) <= 0)
        {
            lbl_Error_Client.innerHTML = "Please Select Atleast One " + hdn_GCCaption.value;
        }           
        else
        {
            ATS = true;
        }
        return ATS;
     }    
    
    function Open_Details_Window(Path,ddl_DeliveryStatus,GC_Id,Article_Id,hdn_DDS_Date,txt_GCAmount)
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
        var Path = Path + "&DeliveryStatusID=" + ddl_DeliveryStatus.value + "&GC_Id=" + GC_Id.value + "&Article_Id=" + Article_Id.value + "&hdn_DDS_Date=" + hdn_DDS_Date.value; 
        window.open(Path,'DDC','width=600,height=350,top=200,left=250,menubar=no,resizable=no,scrollbars=no')
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
    
//    function CalculateBalance()
//    {
//    
//        var txt_CashReceived = document.getElementById('WucDDC1_txt_CashReceived');
//        var hdn_CashReceived = document.getElementById('WucDDC1_hdn_CashReceived');
//        
//        var lbl_txt_TotalCash = document.getElementById('WucDDC1_lbl_txt_TotalCash');
//        var hdn_TotalCash = document.getElementById('WucDDC1_hdn_TotalCash');
//        
//        var lbl_txt_BalancedCash = document.getElementById('WucDDC1_lbl_txt_BalancedCash');
//        var hdn_BalancedCash = document.getElementById('WucDDC1_hdn_BalancedCash');
//       
//        lbl_txt_BalancedCash.value = lbl_txt_TotalCash.value - txt_CashReceived.value;
////        hdn_BalancedCash.value = hdn_TotalCash.value - txt_CashReceived.value;
//        alert(hdn_BalancedCash.value);
//    }