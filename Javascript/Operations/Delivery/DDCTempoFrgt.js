// JScript File
  
    function Check_All(chk,gridname)
    {              
        var grid = document.getElementById(gridname);
        var i,j=0;
        var sum_Total_No_Of_GC=0,sum_Total_DDC_Articles=0,sum_Total_GC_Amount=0,sum_TripWise_Tempo_Freight=0,sum_Tempo_Freight_ToBePaid=0,sum_Bonus=0,sum_AddTempoFrt=0;
        var checkbox;

        var lbl_Total_Records = document.getElementById('WucDDCTempoFrgt1_lbl_Total_Records');
        var lbl_Total_No_Of_GC = document.getElementById('WucDDCTempoFrgt1_lbl_Total_No_Of_GC');
        var lbl_Total_DDC_Articles = document.getElementById('WucDDCTempoFrgt1_lbl_Total_DDC_Articles');        
        var lbl_Total_GC_Amount = document.getElementById('WucDDCTempoFrgt1_lbl_Total_GC_Amount'); 
        var lbl_Tempo_Freight_ToBePaid = document.getElementById('WucDDCTempoFrgt1_lbl_Tempo_Freight_ToBePaid');
        var txtTotalTempoFrgtTBPaid = document.getElementById('WucDDCTempoFrgt1_txtTotalTempoFrgtTBPaid');
        var lbl_Bonus = document.getElementById('WucDDCTempoFrgt1_lbl_Bonus');
        var lbl_AddTempoFrt = document.getElementById('WucDDCTempoFrgt1_lbl_AddTempoFrt');
        
        var hdn_Total_Records = document.getElementById('WucDDCTempoFrgt1_hdn_Total_Records');
        var hdn_Total_No_Of_GC= document.getElementById('WucDDCTempoFrgt1_hdn_Total_No_Of_GC');
        var hdn_Total_DDC_Articles= document.getElementById('WucDDCTempoFrgt1_hdn_Total_DDC_Articles');        
        var hdn_Total_GC_Amount= document.getElementById('WucDDCTempoFrgt1_hdn_Total_GC_Amount');        
        var hdn_Tempo_Freight_ToBePaid = document.getElementById('WucDDCTempoFrgt1_hdn_Tempo_Freight_ToBePaid');
        var hdnTotalTempoFrgtTBPaid = document.getElementById('WucDDCTempoFrgt1_hdnTotalTempoFrgtTBPaid');
        var hdn_Bonus = document.getElementById('WucDDCTempoFrgt1_hdn_Bonus');
        var hdn_AddTempoFrt = document.getElementById('WucDDCTempoFrgt1_hdn_AddTempoFrt');
        
        var max = (grid.rows.length - 1);
        for(i=1;i<grid.rows.length;i++)
        {            
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
          
//            txt_Total_No_Of_GC= grid.rows[i].cells[4].getElementsByTagName('input');
//            txt_Total_DDC_Articles  = grid.rows[i].cells[5].getElementsByTagName('input');
//            txt_Total_GC_Amount = grid.rows[i].cells[6].getElementsByTagName('input');
//            txt_TripWise_Tempo_Freight = grid.rows[i].cells[7].getElementsByTagName('input');
//            txt_Tempo_Freight_ToBePaid = grid.rows[i].cells[8].getElementsByTagName('input'); 
            
            txt_Total_No_Of_GC= grid.rows[i].cells[7].getElementsByTagName('input');
            txt_Total_DDC_Articles  = grid.rows[i].cells[8].getElementsByTagName('input');
            txt_Total_GC_Amount = grid.rows[i].cells[9].getElementsByTagName('input');
            txt_Tempo_Freight_ToBePaid = grid.rows[i].cells[10].getElementsByTagName('input'); 
            txt_Bonus = grid.rows[i].cells[11].getElementsByTagName('input'); 
            txt_AddTempoFrt = grid.rows[i].cells[12].getElementsByTagName('input'); 
            
            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            }
                      
            if(chk.checked == true)
            {
                if(txt_Total_No_Of_GC[0].type =='text')
                {
                    sum_Total_No_Of_GC = sum_Total_No_Of_GC + val(txt_Total_No_Of_GC[0].value);
                }            
                if(txt_Total_DDC_Articles[0].type =='text')
                {
                    sum_Total_DDC_Articles = sum_Total_DDC_Articles + val(txt_Total_DDC_Articles[0].value);
                }
                if(txt_Total_GC_Amount[0].type =='text')
                {
                    sum_Total_GC_Amount = sum_Total_GC_Amount + val(txt_Total_GC_Amount[0].value);
                }
                if(txt_Tempo_Freight_ToBePaid[0].type =='text')
                {
                    sum_Tempo_Freight_ToBePaid = sum_Tempo_Freight_ToBePaid + val(txt_Tempo_Freight_ToBePaid[0].value);
                }
                if(txt_Bonus[0].type =='text')
                {
                    sum_Bonus = sum_Bonus + val(txt_Bonus[0].value);
                }
                if(txt_AddTempoFrt[0].type =='text')
                {
                    sum_AddTempoFrt = sum_AddTempoFrt + val(txt_AddTempoFrt[0].value);
                }
            }
            
        }
        
        if(chk.checked == true)
        {
            lbl_Total_Records.innerHTML = max;
            lbl_Total_No_Of_GC.innerHTML  = sum_Total_No_Of_GC;
            lbl_Total_DDC_Articles.innerHTML  = sum_Total_DDC_Articles;
            lbl_Total_GC_Amount.innerHTML  = sum_Total_GC_Amount;
            lbl_Tempo_Freight_ToBePaid.innerHTML = sum_Tempo_Freight_ToBePaid;
            lbl_Bonus.innerHTML = sum_Bonus;
            lbl_AddTempoFrt.innerHTML = sum_AddTempoFrt;
                    
            hdn_Total_Records.value = max;
            hdn_Total_No_Of_GC.value = sum_Total_No_Of_GC;
            hdn_Total_DDC_Articles.value = sum_Total_DDC_Articles;
            hdn_Total_GC_Amount.value = sum_Total_GC_Amount;
            hdn_Tempo_Freight_ToBePaid.value = sum_Tempo_Freight_ToBePaid;
            hdn_Bonus.value = sum_Bonus;
            hdn_AddTempoFrt.value = sum_AddTempoFrt;
                        
            txtTotalTempoFrgtTBPaid.value = sum_Tempo_Freight_ToBePaid + sum_Bonus + sum_AddTempoFrt;  
            hdnTotalTempoFrgtTBPaid.value = sum_Tempo_Freight_ToBePaid + sum_Bonus + sum_AddTempoFrt; 
        }
        else
        {
            lbl_Total_Records.innerHTML = 0;
            lbl_Total_No_Of_GC.innerHTML = 0;
            lbl_Total_DDC_Articles.innerHTML = 0;
            lbl_Total_GC_Amount.innerHTML = 0;
            lbl_Tempo_Freight_ToBePaid.innerHTML = 0;
            lbl_Bonus.innerHTML = 0;
            lbl_AddTempoFrt.innerHTML = 0;
                           
            hdn_Total_Records.value = 0;        
            hdn_Total_No_Of_GC.value = 0;
            hdn_Total_DDC_Articles.value = 0;
            hdn_Total_GC_Amount.value = 0;            
            hdn_Tempo_Freight_ToBePaid.value = 0;
            hdn_Bonus.value = 0;
            hdn_AddTempoFrt.value = 0;
            
            txtTotalTempoFrgtTBPaid.value = 0; 
            hdnTotalTempoFrgtTBPaid.value = 0;
            
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
            var grid = document.getElementById('WucDDCTempoFrgt1_dg_PDS');
        }
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;
        var Total_No_Of_GC,Total_DDC_Articles,Total_GC_Amount,TripWise_Tempo_Freight,Tempo_Freight_ToBePaid,Bonus,AddTempoFrt;

        var lbl_Total_Records = document.getElementById('WucDDCTempoFrgt1_lbl_Total_Records');
        var lbl_Total_No_Of_GC = document.getElementById('WucDDCTempoFrgt1_lbl_Total_No_Of_GC');
        var lbl_Total_DDC_Articles = document.getElementById('WucDDCTempoFrgt1_lbl_Total_DDC_Articles');
        var lbl_Total_GC_Amount = document.getElementById('WucDDCTempoFrgt1_lbl_Total_GC_Amount');
        var lbl_Tempo_Freight_ToBePaid = document.getElementById('WucDDCTempoFrgt1_lbl_Tempo_Freight_ToBePaid');
        var lbl_Bonus = document.getElementById('WucDDCTempoFrgt1_lbl_Bonus');
        var lbl_AddTempoFrt = document.getElementById('WucDDCTempoFrgt1_lbl_AddTempoFrt');
        
        var txtTotalTempoFrgtTBPaid = document.getElementById('WucDDCTempoFrgt1_txtTotalTempoFrgtTBPaid');
        var hdnTotalTempoFrgtTBPaid = document.getElementById('WucDDCTempoFrgt1_hdnTotalTempoFrgtTBPaid');

        var hdn_Total_Records = document.getElementById('WucDDCTempoFrgt1_hdn_Total_Records');
        var hdn_Total_No_Of_GC= document.getElementById('WucDDCTempoFrgt1_hdn_Total_No_Of_GC');
        var hdn_Total_DDC_Articles= document.getElementById('WucDDCTempoFrgt1_hdn_Total_DDC_Articles');
        var hdn_Total_GC_Amount= document.getElementById('WucDDCTempoFrgt1_hdn_Total_GC_Amount');
        var hdn_Tempo_Freight_ToBePaid = document.getElementById('WucDDCTempoFrgt1_hdn_Tempo_Freight_ToBePaid');
        var hdn_Bonus = document.getElementById('WucDDCTempoFrgt1_hdn_Bonus');
        var hdn_AddTempoFrt = document.getElementById('WucDDCTempoFrgt1_hdn_AddTempoFrt'); 
        
//        txt_Total_No_Of_GC= row.cells[4].getElementsByTagName('input');
//        txt_Total_DDC_Articles  = row.cells[5].getElementsByTagName('input');
//        txt_Total_GC_Amount = row.cells[6].getElementsByTagName('input');        
//        txt_TripWise_Tempo_Freight = row.cells[7].getElementsByTagName('input'); 
//        txt_Tempo_Freight_ToBePaid = row.cells[8].getElementsByTagName('input');
        
        txt_Total_No_Of_GC= row.cells[7].getElementsByTagName('input');
        txt_Total_DDC_Articles  = row.cells[8].getElementsByTagName('input');
        txt_Total_GC_Amount = row.cells[9].getElementsByTagName('input');        
        txt_Tempo_Freight_ToBePaid = row.cells[10].getElementsByTagName('input');
        txt_Bonus = row.cells[11].getElementsByTagName('input');
        txt_AddTempoFrt = row.cells[12].getElementsByTagName('input');

        
        if(callfrom == 1)
        {
            if(chk.checked == true)
            {
               lbl_Total_Records.innerHTML = val(lbl_Total_Records.innerHTML) + 1;
               lbl_Total_No_Of_GC.innerHTML = val(lbl_Total_No_Of_GC.innerHTML) + val(txt_Total_No_Of_GC[0].value);
               lbl_Total_DDC_Articles.innerHTML = val(lbl_Total_DDC_Articles.innerHTML) + val(txt_Total_DDC_Articles[0].value);
               lbl_Total_GC_Amount.innerHTML = val(lbl_Total_GC_Amount.innerHTML) + val(txt_Total_GC_Amount[0].value);
               lbl_Tempo_Freight_ToBePaid.innerHTML = val(lbl_Tempo_Freight_ToBePaid.innerHTML) + val(txt_Tempo_Freight_ToBePaid[0].value);
               lbl_Bonus.innerHTML = val(lbl_Bonus.innerHTML) + val(txt_Bonus[0].value);
               lbl_AddTempoFrt.innerHTML = val(lbl_AddTempoFrt.innerHTML) + val(txt_AddTempoFrt[0].value);
               
               hdn_Total_Records.value = val(hdn_Total_Records.value) + 1;
               hdn_Total_No_Of_GC.value = val(hdn_Total_No_Of_GC.value) + val(txt_Total_No_Of_GC[0].value);
               hdn_Total_DDC_Articles.value = val(hdn_Total_DDC_Articles.value) + val(txt_Total_DDC_Articles[0].value);
               hdn_Total_GC_Amount.value = val(hdn_Total_GC_Amount.value) + val(txt_Total_GC_Amount[0].value);
               hdn_Tempo_Freight_ToBePaid.value = val(hdn_Tempo_Freight_ToBePaid.value) + val(txt_Tempo_Freight_ToBePaid[0].value);
               hdn_Bonus.value = val(hdn_Bonus.value) + val(txt_Bonus[0].value);
               hdn_AddTempoFrt.value = val(hdn_AddTempoFrt.value) + val(txt_AddTempoFrt[0].value);
                       
            }
            else
            {
               lbl_Total_Records.innerHTML = val(lbl_Total_Records.innerHTML) - 1;
               lbl_Total_No_Of_GC.innerHTML = val(lbl_Total_No_Of_GC.innerHTML) - val(txt_Total_No_Of_GC[0].value);
               lbl_Total_DDC_Articles.innerHTML = val(lbl_Total_DDC_Articles.innerHTML) - val(txt_Total_DDC_Articles[0].value);
               lbl_Total_GC_Amount.innerHTML = val(lbl_Total_GC_Amount.innerHTML) - val(txt_Total_GC_Amount[0].value);
               lbl_Tempo_Freight_ToBePaid.innerHTML = val(lbl_Tempo_Freight_ToBePaid.innerHTML) - val(txt_Tempo_Freight_ToBePaid[0].value);
               lbl_Bonus.innerHTML = val(lbl_Bonus.innerHTML) - val(txt_Bonus[0].value);
               lbl_AddTempoFrt.innerHTML = val(lbl_AddTempoFrt.innerHTML) - val(txt_AddTempoFrt[0].value);
                              
               hdn_Total_Records.value = val(hdn_Total_Records.value) - 1;
               hdn_Total_No_Of_GC.value = val(hdn_Total_No_Of_GC.value) - val(txt_Total_No_Of_GC[0].value);
               hdn_Total_DDC_Articles.value = val(hdn_Total_DDC_Articles.value) - val(txt_Total_DDC_Articles[0].value);
               hdn_Total_GC_Amount.value = val(hdn_Total_GC_Amount.value) - val(txt_Total_GC_Amount[0].value);
               hdn_Tempo_Freight_ToBePaid.value = val(hdn_Tempo_Freight_ToBePaid.value) - val(txt_Tempo_Freight_ToBePaid[0].value);
               hdn_Bonus.value = val(hdn_Bonus.value) - val(txt_Bonus[0].value);
               hdn_AddTempoFrt.value = val(hdn_AddTempoFrt.value) - val(txt_AddTempoFrt[0].value);               
                          
            }
              
               txtTotalTempoFrgtTBPaid.value = 0;
               hdnTotalTempoFrgtTBPaid.value = 0;
               
               
               txtTotalTempoFrgtTBPaid.value = Math.abs(val(lbl_Tempo_Freight_ToBePaid.innerHTML) + val(lbl_Bonus.innerHTML) + val(lbl_AddTempoFrt.innerHTML)); 
               hdnTotalTempoFrgtTBPaid.value = Math.abs(val(hdn_Tempo_Freight_ToBePaid.value) + val(hdn_Bonus.value) + val(hdn_AddTempoFrt.value)); 
     
        } 

        if((grid.rows.length-1) == val(hdn_Total_Records.value))
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
        
        var ddl_DeliveryMode = document.getElementById('WucDDCTempoFrgt1_ddl_DeliveryMode');
        var txt_GodownSupervisor = document.getElementById('WucDDCTempoFrgt1_ddl_GodownSupervisor_txtBoxddl_GodownSupervisor');
        var hdn_Total_No_Of_GC = document.getElementById('WucDDCTempoFrgt1_hdn_Total_No_Of_GC');
        var hdn_GCCaption = document.getElementById('WucDDCTempoFrgt1_hdn_GCCaption');
        
        var lbl_Error_Client = document.getElementById('WucDDCTempoFrgt1_lbl_Error_Client');
        lbl_Error_Client.innerHTML ="";
         
        if(val(hdn_Total_No_Of_GC.value) == 0)
        {
            lbl_Error_Client.innerHTML = "Please Select Atleast One " + hdn_GCCaption.value;
        }
//        else if (val(ddl_DeliveryMode.value) <= 0)
//        {
//            lbl_Error_Client.innerHTML =  "Please Select Delivery Mode";//objResource.GetMsg("Msg_ddl_DeliveryMode");
//            ddl_DeliveryMode.focus();
//        }       
//        else if(txt_GodownSupervisor.value == '')
//        {
//            lbl_Error_Client.innerHTML = "Please Select Godown Supervisor";//objResource.GetMsg("Msg_txt_Supervisor");
//            txt_GodownSupervisor.focus();
//        }               
        else
        {
            ATS = true;
        }
        return ATS;
     }    

function AddLR()
{
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-100);
    var popH = (h-100);
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
    
    var hdn_LoginBranch_Id= document.getElementById('WucDDCTempoFrgt1_hdn_LoginBranch_Id'); 
    var Memo_Date = WucDDCTempoFrgt1_dtp_PDS_Date.GetSelectedDate();
    var MemoYear = Memo_Date.getFullYear();
    var MemoMonth = Memo_Date.getMonth() + 1;
    var MemoDay = Memo_Date.getDate();
    
    Memo_Date = new Date(Memo_Date.getFullYear(), Memo_Date.getMonth(),Memo_Date.getDate());
    

    
    var Url = "FrmPDSAddLR.aspx?BranchID=" +  hdn_LoginBranch_Id.value +  
                                    "&MemoYear=" + MemoYear + 
                                    "&MemoMonth=" + MemoMonth + 
                                    "&MemoDay=" + MemoDay + 
                                    "&MemoDate=" + Memo_Date;
                                    

    window.open(Url, 'AddLR', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
    return false;
}

function Check_All_Add_LR(chk,gridname)
{              
    var grid = document.getElementById(gridname);
    var checkbox;
    var i,j=0;

    for(i=1;i<grid.rows.length;i++)
    {            
        checkbox = grid.rows[i].cells[0].getElementsByTagName('input');

        if(checkbox[0].type = 'checkbox')
        {
            checkbox[0].checked = chk.checked;
        }
    }
}

function ValidateAddLR()
{
  var validate = false;
  var lblSelectedLRs = document.getElementById('lblSelectedLRs');
  if (lblSelectedLRs.innerHTML == "")
    {
      validate = false;
      alert("No LR(s) Found. Please select atleast one LR")
    }
  else
      validate = true;  
      
  return validate;
}

function UpdateParentWindow()
{
  if (ValidateAddLR())
    {
      var dg_PDS = window.opener.document.getElementById("WucDDCTempoFrgt1_dg_PDS");
      var chk_AddLR = window.opener.document.getElementById("WucDDCTempoFrgt1_chk_AddLR");
      var lblSelectedLRs = document.getElementById('lblSelectedLRs');
      var txt_get_item =  window.opener.document.getElementById("WucDDCTempoFrgt1_WucSelectedItems1_txt_get_item");
      var btn_get_data = window.opener.document.getElementById("WucDDCTempoFrgt1_WucSelectedItems1_btn_get_data");
      if (txt_get_item.value == "")
        txt_get_item.value = lblSelectedLRs.innerHTML;
      else
        txt_get_item.value = txt_get_item.value + "," + lblSelectedLRs.innerHTML;
      
      self.close();
      btn_get_data.click(); 
    }
}