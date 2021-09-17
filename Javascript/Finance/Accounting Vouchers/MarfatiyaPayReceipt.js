// JScript File
  
    function Check_All(chk,gridname)
    {     
        var grid = document.getElementById(gridname);
        var i,j=0;
        var sum_Bill_Amount=0, sum_Received_Amt=0;
        var checkbox;

        var lbl_Received_Amount = document.getElementById('lbl_Received_Amount'); 
        var lbl_Bill_Amount = document.getElementById('lbl_Bill_Amount');
        var lbl_Total_Bills = document.getElementById('lbl_Total_Bills');

        var hdn_Received_Amount = document.getElementById('hdn_Received_Amount'); 
        var hdn_Bill_Amount= document.getElementById('hdn_Bill_Amount');
        var hdn_Total_Bills = document.getElementById('hdn_Total_Bills');
        
        var max = (grid.rows.length - 1);
        for(i=1;i<grid.rows.length;i++)
        {            
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input'); 
            
            txt_Bill_Amount  = grid.rows[i].cells[4].getElementsByTagName('input');
            txt_Received_Amount = grid.rows[i].cells[5].getElementsByTagName('input'); 
            
            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            }
                      
            if(chk.checked == true)
            {
                           
                if(txt_Bill_Amount[0].type =='text')
                {
                    sum_Bill_Amount = sum_Bill_Amount + val(txt_Bill_Amount[0].value);
                } 
                if(txt_Received_Amount[0].type =='text')
                {
                    sum_Received_Amt = sum_Received_Amt + val(txt_Received_Amount[0].value);
                }
            }
            
        }
        
        if(chk.checked == true)
        {
            lbl_Total_Bills.innerHTML = max;
            lbl_Bill_Amount.innerHTML  =  sum_Bill_Amount; 
            lbl_Received_Amount.innerHTML = sum_Received_Amt;
                    
            hdn_Total_Bills.value = max;
            hdn_Bill_Amount.value = sum_Bill_Amount; 
            hdn_Received_Amount.value = sum_Received_Amt;
        }
        else
        {
            lbl_Total_Bills.innerHTML = 0;
            lbl_Bill_Amount.innerHTML  = 0; 
            lbl_Received_Amount.innerHTML = 0;
                    
            hdn_Total_Bills.value = 0; 
            hdn_Bill_Amount.value = 0; 
            hdn_Received_Amount.value = 0;
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
            var grid = document.getElementById('dg_PayReceipt');
        }
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;
       

        var lbl_Total_Bills = document.getElementById('lbl_Total_Bills');
        var lbll_total_Bill_Amount = document.getElementById('lbl_Bill_Amount'); 
        var lbl_Received_Amount = document.getElementById('lbl_Received_Amount');

        var hdn_Total_Bills = document.getElementById('hdn_Total_Bills');
        var hdn_Bill_Amount= document.getElementById('hdn_Bill_Amount'); 
        var hdn_Received_Amount = document.getElementById('hdn_Received_Amount');  
         
        txt_Bill_Amount  = row.cells[4].getElementsByTagName('input'); 
        txt_Received_Amount = row.cells[5].getElementsByTagName('input'); 
         
        if(callfrom == 1)
        {
            if(chk.checked == true)
            {
               lbl_Total_Bills.innerHTML = val(lbl_Total_Bills.innerHTML) + 1;
               lbll_total_Bill_Amount.innerHTML = val(lbll_total_Bill_Amount.innerHTML) + val(txt_Bill_Amount[0].value);
               lbl_Received_Amount.innerHTML = val(lbl_Received_Amount.innerHTML) + val(txt_Received_Amount[0].value);

               hdn_Total_Bills.value = val(hdn_Total_Bills.value) + 1;
               hdn_Bill_Amount.value = val(hdn_Bill_Amount.value) + val(txt_Bill_Amount[0].value);
               hdn_Received_Amount.value = val(hdn_Received_Amount.value) + val(txt_Received_Amount[0].value);
            }
            else
            {
               lbl_Total_Bills.innerHTML = val(lbl_Total_Bills.innerHTML) - 1;
               lbll_total_Bill_Amount.innerHTML = val(lbll_total_Bill_Amount.innerHTML) - val(txt_Bill_Amount[0].value);
               lbl_Received_Amount.innerHTML = val(lbl_Received_Amount.innerHTML) - val(txt_Received_Amount[0].value);
               
               hdn_Total_Bills.value = val(hdn_Total_Bills.value) - 1;
               hdn_Bill_Amount.value = val(hdn_Bill_Amount.value) - val(txt_Bill_Amount[0].value);
               hdn_Received_Amount.value = val(hdn_Received_Amount.value) - val(txt_Received_Amount[0].value);
            }
        }
                       
        if(callfrom == 2)       //onblur of  Delivered Article
        {
            if(chk.checked == true)
            {
                hdn_Bill_Amount.value = val(hdn_Bill_Amount.value) + val(txt_Bill_Amount[0].value);
                lbll_total_Bill_Amount.innerHTML = val(hdn_Bill_Amount.value);
            }               
        }
         
        if(callfrom == 4 && chk.checked == true)       //onfocus of  Delivered Article
        {
            hdn_Bill_Amount.value = val(hdn_Bill_Amount.value) - val(txt_Bill_Amount[0].value);
            lbll_total_Bill_Amount.innerHTML = val(hdn_Bill_Amount.value);
        } 
        

        if((grid.rows.length-1) == val(hdn_Total_Bills.value))
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
        
        var hdn_Total_Bills = document.getElementById('hdn_Total_Bills');
        var hdn_GCCaption = document.getElementById('hdn_GCCaption');
        
        var lbl_Error_Client = document.getElementById('lbl_Error_Client');
        lbl_Error_Client.innerHTML ="";
         
        if(val(hdn_Total_Bills.value) == 0)
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
      var dg_PayReceipt = window.opener.document.getElementById("dg_PayReceipt");
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