// JScript File
  
    function Check_All(chk,gridname)
    {     
         var grid = document.getElementById(gridname);
        var i,j=0;
        var sum_ToPay_LR_Total=0,sum_Delivery_Charges=0,sum_Delivery_Service_Tax=0,sum_Memo_Total=0;
        var checkbox,Balance_Art,Balance_ActWt,Delivered_Art,Delivered_ActWt;

        var lbl_Memo_Total = document.getElementById('lbl_Memo_Total');
        var lbl_Delivery_Service_Tax = document.getElementById('lbl_Delivery_Service_Tax');
        var lbl_Delivery_Charges = document.getElementById('lbl_Delivery_Charges');
        var lbl_ToPay_LR_Total = document.getElementById('lbl_ToPay_LR_Total');
        var lbl_total_GC = document.getElementById('lbl_Total_GC');

        var hdn_Memo_Total = document.getElementById('hdn_Memo_Total');
        var hdn_Delivery_Service_Tax= document.getElementById('hdn_Delivery_Service_Tax');
        var hdn_Delivery_Charges= document.getElementById('hdn_Delivery_Charges');
        var hdn_ToPay_LR_Total= document.getElementById('hdn_ToPay_LR_Total');
        var hdn_Total_GC = document.getElementById('hdn_Total_GC');
        
        var max = (grid.rows.length - 1);
        for(i=1;i<grid.rows.length;i++)
        {            
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input'); 
            
            txt_Memo_Total = grid.rows[i].cells[9].getElementsByTagName('input');
            txt_Delivery_Service_Tax = grid.rows[i].cells[8].getElementsByTagName('input');
            txt_Delivery_Charges = grid.rows[i].cells[7].getElementsByTagName('input');
            txt_ToPay_LR_Total  = grid.rows[i].cells[6].getElementsByTagName('input');
            
            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            }
                      
            if(chk.checked == true)
            {
                           
                if(txt_ToPay_LR_Total[0].type =='text')
                {
                    sum_ToPay_LR_Total = sum_ToPay_LR_Total + val(txt_ToPay_LR_Total[0].value);
                }
                if(txt_Delivery_Charges[0].type =='text')
                {
                    sum_Delivery_Charges = sum_Delivery_Charges + val(txt_Delivery_Charges[0].value);
                }
                if(txt_Delivery_Service_Tax[0].type =='text')
                {
                    sum_Delivery_Service_Tax = sum_Delivery_Service_Tax + val(txt_Delivery_Service_Tax[0].value);
                }
                if(txt_Memo_Total[0].type =='text')
                {
                    sum_Memo_Total = sum_Memo_Total + val(txt_Memo_Total[0].value);
                }
            }
            
        }
        
        if(chk.checked == true)
        {
            lbl_total_GC.innerHTML = max;
            lbl_ToPay_LR_Total.innerHTML  =  sum_ToPay_LR_Total;
            lbl_Delivery_Charges.innerHTML  =sum_Delivery_Charges;
            lbl_Delivery_Service_Tax.innerHTML  = sum_Delivery_Service_Tax;
            lbl_Memo_Total.innerHTML = sum_Memo_Total;
                    
            hdn_Total_GC.value = max;
            hdn_ToPay_LR_Total.value = sum_ToPay_LR_Total;
            hdn_Delivery_Charges.value = sum_Delivery_Charges;
            hdn_Delivery_Service_Tax.value = sum_Delivery_Service_Tax;
            hdn_Memo_Total.value = sum_Memo_Total;
        }
        else
        {
            lbl_total_GC.innerHTML = 0;
            lbl_ToPay_LR_Total.innerHTML  = 0;
            lbl_Delivery_Charges.innerHTML  = 0;
            lbl_Delivery_Service_Tax.innerHTML  = 0;
            lbl_Memo_Total.innerHTML = 0;
                    
            hdn_Total_GC.value = 0; 
            hdn_ToPay_LR_Total.value = 0;
            hdn_Delivery_Charges.value = 0;
            hdn_Delivery_Service_Tax.value = 0;
            hdn_Memo_Total.value = 0;
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
            var grid = document.getElementById('dg_Marfatiya');
        }
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;
       

        var lbl_total_GC = document.getElementById('lbl_Total_GC');
        var lbl_total_ToPay_LR_Total = document.getElementById('lbl_ToPay_LR_Total');
        var lbl_total_Delivery_Charges = document.getElementById('lbl_Delivery_Charges');
        var lbl_total_Delivery_Service_Tax = document.getElementById('lbl_Delivery_Service_Tax');
        var lbl_Memo_Total = document.getElementById('lbl_Memo_Total');

        var hdn_Total_GC = document.getElementById('hdn_Total_GC');
        var hdn_ToPay_LR_Total= document.getElementById('hdn_ToPay_LR_Total');
        var hdn_Delivery_Charges= document.getElementById('hdn_Delivery_Charges');
        var hdn_Delivery_Service_Tax= document.getElementById('hdn_Delivery_Service_Tax');
        var hdn_Memo_Total = document.getElementById('hdn_Memo_Total'); 
        
         
        txt_ToPay_LR_Total  = row.cells[6].getElementsByTagName('input');
        txt_Delivery_Charges = row.cells[7].getElementsByTagName('input');
        txt_Delivery_Service_Tax = row.cells[8].getElementsByTagName('input');
        txt_Memo_Total = row.cells[9].getElementsByTagName('input');


         
        if(callfrom == 1)
        {
            if(chk.checked == true)
            {
               lbl_total_GC.innerHTML = val(lbl_total_GC.innerHTML) + 1;
               lbl_total_ToPay_LR_Total.innerHTML = val(lbl_total_ToPay_LR_Total.innerHTML) + val(txt_ToPay_LR_Total[0].value);
               lbl_total_Delivery_Charges.innerHTML = val(lbl_total_Delivery_Charges.innerHTML) + val(txt_Delivery_Charges[0].value);
               lbl_total_Delivery_Service_Tax.innerHTML = val(lbl_total_Delivery_Service_Tax.innerHTML) + val(txt_Delivery_Service_Tax[0].value);
               lbl_Memo_Total.innerHTML = val(lbl_Memo_Total.innerHTML) + val(txt_Memo_Total[0].value);

               hdn_Total_GC.value = val(hdn_Total_GC.value) + 1;
               hdn_ToPay_LR_Total.value = val(hdn_ToPay_LR_Total.value) + val(txt_ToPay_LR_Total[0].value);
               hdn_Delivery_Charges.value = val(hdn_Delivery_Charges.value) + val(txt_Delivery_Charges[0].value);
               hdn_Delivery_Service_Tax.value = val(hdn_Delivery_Service_Tax.value) + val(txt_Delivery_Service_Tax[0].value);
               hdn_Memo_Total.value = val(hdn_Memo_Total.value) + val(txt_Memo_Total[0].value);
            }
            else
            {
               lbl_total_GC.innerHTML = val(lbl_total_GC.innerHTML) - 1;
               lbl_total_ToPay_LR_Total.innerHTML = val(lbl_total_ToPay_LR_Total.innerHTML) - val(txt_ToPay_LR_Total[0].value);
               lbl_total_Delivery_Charges.innerHTML = val(lbl_total_Delivery_Charges.innerHTML) - val(txt_Delivery_Charges[0].value);
               lbl_total_Delivery_Service_Tax.innerHTML = val(lbl_total_Delivery_Service_Tax.innerHTML) - val(txt_Delivery_Service_Tax[0].value);
               lbl_Memo_Total.innerHTML = val(lbl_Memo_Total.innerHTML) - val(txt_Memo_Total[0].value);
               
               hdn_Total_GC.value = val(hdn_Total_GC.value) - 1;
               hdn_ToPay_LR_Total.value = val(hdn_ToPay_LR_Total.value) - val(txt_ToPay_LR_Total[0].value);
               hdn_Delivery_Charges.value = val(hdn_Delivery_Charges.value) - val(txt_Delivery_Charges[0].value);
               hdn_Delivery_Service_Tax.value = val(hdn_Delivery_Service_Tax.value) - val(txt_Delivery_Service_Tax[0].value);
               hdn_Memo_Total.value = val(hdn_Memo_Total.value) - val(txt_Memo_Total[0].value);
            }
        }
                       
        if(callfrom == 2)       //onblur of  Delivered Article
        {
            if(chk.checked == true)
            {
                hdn_ToPay_LR_Total.value = val(hdn_ToPay_LR_Total.value) + val(txt_ToPay_LR_Total[0].value);
                lbl_total_ToPay_LR_Total.innerHTML = val(hdn_ToPay_LR_Total.value);
            }               
        }
        if(callfrom == 3)       //onblur of  Delivered Weight
        {
            if(chk.checked == true)
            {
                hdn_Delivery_Service_Tax.value = val(hdn_Delivery_Service_Tax.value) + val(txt_Delivery_Service_Tax[0].value);
                lbl_total_Delivery_Service_Tax.innerHTML = val(hdn_Delivery_Service_Tax.value);
            }               
        }
        if(callfrom == 4 && chk.checked == true)       //onfocus of  Delivered Article
        {
            hdn_ToPay_LR_Total.value = val(hdn_ToPay_LR_Total.value) - val(txt_ToPay_LR_Total[0].value);
            lbl_total_ToPay_LR_Total.innerHTML = val(hdn_ToPay_LR_Total.value);
        }
        if(callfrom == 5 && chk.checked == true)        //onfocus of  Delivered Weight
        {
            hdn_Delivery_Service_Tax.value = val(hdn_Delivery_Service_Tax.value) - val(txt_Delivery_Service_Tax[0].value);
            lbl_total_Delivery_Service_Tax.innerHTML = val(hdn_Delivery_Service_Tax.value);      
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
        
        var hdn_Total_GC = document.getElementById('hdn_Total_GC');
        var hdn_GCCaption = document.getElementById('hdn_GCCaption');
        
        var lbl_Error_Client = document.getElementById('lbl_Error_Client');
        lbl_Error_Client.innerHTML ="";
         
        if(val(hdn_Total_GC.value) == 0)
        {
            lbl_Error_Client.innerHTML = "Please Select Atleast One " + hdn_GCCaption.value;
        }              
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
    
    var hdn_LoginBranch_Id= document.getElementById('hdn_LoginBranch_Id'); 
    var Memo_Date = dtp_PDS_Date.GetSelectedDate();
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
      var dg_Marfatiya = window.opener.document.getElementById("dg_Marfatiya");
      var chk_AddLR = window.opener.document.getElementById("chk_AddLR");
      var lblSelectedLRs = document.getElementById('lblSelectedLRs');
      var txt_get_item =  window.opener.document.getElementById("WucSelectedItems1_txt_get_item");
      var btn_get_data = window.opener.document.getElementById("WucSelectedItems1_btn_get_data");
      if (txt_get_item.value == "")
        txt_get_item.value = lblSelectedLRs.innerHTML;
      else
        txt_get_item.value = txt_get_item.value + "," + lblSelectedLRs.innerHTML;
      
      self.close();
      btn_get_data.click(); 
    }
}