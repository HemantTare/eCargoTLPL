// JScript File
  
    function Check_All(chk,gridname)
    {              
        var grid = document.getElementById(gridname);
        var i,j=0;
        var sum_BalArt=0,sum_BalActWt=0,sum_DelArt=0,sum_DelWt=0,sum_totalGCAmt=0;
        var checkbox,Balance_Art,Balance_ActWt,Delivered_Art,Delivered_ActWt;

        var lbl_totalBalArt = document.getElementById('WucPDS1_lbl_BalArt');
        var lbl_totalBalActWt = document.getElementById('WucPDS1_lbl_BalActWt');
        var lbl_totalDelArt = document.getElementById('WucPDS1_lbl_totalDelArt');
        var lbl_totalDelWt = document.getElementById('WucPDS1_lbl_totalDelWt');
        var lbl_total_GC = document.getElementById('WucPDS1_lbl_Total_GC');
        var lbl_totalGCAmt = document.getElementById('WucPDS1_lbl_totalGCAmt');

        var hdn_totalBalArt= document.getElementById('WucPDS1_hdn_BalArt');
        var hdn_totalBalActWt= document.getElementById('WucPDS1_hdn_BalActWt');
        var hdn_totalDelArt= document.getElementById('WucPDS1_hdn_totalDelArt');
        var hdn_totalDelWt= document.getElementById('WucPDS1_hdn_totalDelWt');
        var hdn_total_GC = document.getElementById('WucPDS1_hdn_Total_GC');
        var hdn_totalGCAmt = document.getElementById('WucPDS1_hdn_totalGCAmt');
        
        var max = (grid.rows.length - 1);
        for(i=1;i<grid.rows.length;i++)
        {            
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            //txt_Balance_Art= grid.rows[i].cells[10].getElementsByTagName('input');
            //txt_Balance_ActWt  = grid.rows[i].cells[11].getElementsByTagName('input');
            //txt_Delivered_Art = grid.rows[i].cells[12].getElementsByTagName('input');
            //txt_Delivered_ActWt = grid.rows[i].cells[13].getElementsByTagName('input');
            
            txt_Balance_Art= grid.rows[i].cells[12].getElementsByTagName('input');
            txt_Balance_ActWt  = grid.rows[i].cells[13].getElementsByTagName('input');
            txt_Delivered_Art = grid.rows[i].cells[14].getElementsByTagName('input');
            txt_Delivered_ActWt = grid.rows[i].cells[15].getElementsByTagName('input');
            txt_Total_GC_Amount = grid.rows[i].cells[17].getElementsByTagName('input');
            
            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            }
                      
            if(chk.checked == true)
            {
                if(txt_Balance_Art[0].type =='text')
                {
                    sum_BalArt = sum_BalArt + val(txt_Balance_Art[0].value);
                }            
                if(txt_Balance_ActWt[0].type =='text')
                {
                    sum_BalActWt = sum_BalActWt + val(txt_Balance_ActWt[0].value);
                }
                if(txt_Delivered_Art[0].type =='text')
                {
                    sum_DelArt = sum_DelArt + val(txt_Delivered_Art[0].value);
                }
                if(txt_Delivered_ActWt[0].type =='text')
                {
                    sum_DelWt = sum_DelWt + val(txt_Delivered_ActWt[0].value);
                }
                if(txt_Total_GC_Amount[0].type =='text')
                {
                    sum_totalGCAmt = sum_totalGCAmt + val(txt_Total_GC_Amount[0].value);
                }
            }
            
        }
        
        if(chk.checked == true)
        {
            lbl_totalBalArt.innerHTML  = sum_BalArt;
            lbl_totalBalActWt.innerHTML  = sum_BalActWt;
            lbl_totalDelArt.innerHTML  = sum_DelArt;
            lbl_totalDelWt.innerHTML  = sum_DelWt;
            lbl_total_GC.innerHTML = max;
            lbl_totalGCAmt.innerHTML = sum_totalGCAmt;
                    
            hdn_total_GC.value = max;
            hdn_totalBalArt.value = sum_BalArt;
            hdn_totalBalActWt.value = sum_BalActWt;
            hdn_totalDelArt.value = sum_DelArt;
            hdn_totalDelWt.value = sum_DelWt;
            hdn_totalGCAmt.value = sum_totalGCAmt;
        }
        else
        {
            lbl_totalBalArt.innerHTML  = 0;
            lbl_totalBalActWt.innerHTML  = 0;
            lbl_totalDelArt.innerHTML  = 0;
            lbl_totalDelWt.innerHTML  = 0;
            lbl_total_GC.innerHTML = 0;
            lbl_totalGCAmt.innerHTML = 0;
                    
            hdn_total_GC.value = 0;
            hdn_totalBalArt.value = 0;
            hdn_totalBalActWt.value = 0;
            hdn_totalDelArt.value = 0;
            hdn_totalDelWt.value = 0;
            hdn_totalGCAmt.value = 0;
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
            var grid = document.getElementById('WucPDS1_dg_PDS');
        }
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;
        var Balance_Art,Balance_ActWt,Delivered_Art,Delivered_ActWt;

        var lbl_total_BalArt = document.getElementById('WucPDS1_lbl_BalArt');
        var lbl_total_BalActWt = document.getElementById('WucPDS1_lbl_BalActWt');
        var lbl_total_DelArt = document.getElementById('WucPDS1_lbl_totalDelArt');
        var lbl_total_DelWt = document.getElementById('WucPDS1_lbl_totalDelWt');
        var lbl_total_GC = document.getElementById('WucPDS1_lbl_Total_GC');
        var lbl_totalGCAmt = document.getElementById('WucPDS1_lbl_totalGCAmt');

        var hdn_total_BalArt= document.getElementById('WucPDS1_hdn_BalArt');
        var hdn_total_BalActWt= document.getElementById('WucPDS1_hdn_BalActWt');
        var hdn_total_DelArt= document.getElementById('WucPDS1_hdn_totalDelArt');
        var hdn_total_DelWt= document.getElementById('WucPDS1_hdn_totalDelWt');
        var hdn_total_GC = document.getElementById('WucPDS1_hdn_Total_GC');
        var hdn_totalGCAmt = document.getElementById('WucPDS1_hdn_totalGCAmt');
// 
//        txt_Balance_Art= row.cells[10].getElementsByTagName('input');
//        txt_Balance_ActWt  = row.cells[11].getElementsByTagName('input');
//        txt_Delivered_Art = row.cells[12].getElementsByTagName('input');
//        txt_Delivered_ActWt = row.cells[13].getElementsByTagName('input');
        txt_Balance_Art= row.cells[12].getElementsByTagName('input');
        txt_Balance_ActWt  = row.cells[13].getElementsByTagName('input');
        txt_Delivered_Art = row.cells[14].getElementsByTagName('input');
        txt_Delivered_ActWt = row.cells[15].getElementsByTagName('input');
        txt_Total_GC_Amount = row.cells[17].getElementsByTagName('input');


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
               lbl_total_BalArt.innerHTML = val(lbl_total_BalArt.innerHTML) + val(txt_Balance_Art[0].value);
               lbl_total_BalActWt.innerHTML = val(lbl_total_BalActWt.innerHTML) + val(txt_Balance_ActWt[0].value);
               lbl_total_DelArt.innerHTML = val(lbl_total_DelArt.innerHTML) + val(txt_Delivered_Art[0].value);
               lbl_total_DelWt.innerHTML = val(lbl_total_DelWt.innerHTML) + val(txt_Delivered_ActWt[0].value);
               lbl_totalGCAmt.innerHTML = val(lbl_totalGCAmt.innerHTML) + val(txt_Total_GC_Amount[0].value);

               hdn_total_GC.value = val(hdn_total_GC.value) + 1;
               hdn_total_BalArt.value = val(hdn_total_BalArt.value) + val(txt_Balance_Art[0].value);
               hdn_total_BalActWt.value = val(hdn_total_BalActWt.value) + val(txt_Balance_ActWt[0].value);
               hdn_total_DelArt.value = val(hdn_total_DelArt.value) + val(txt_Delivered_Art[0].value);
               hdn_total_DelWt.value = val(hdn_total_DelWt.value) + val(txt_Delivered_ActWt[0].value);
               hdn_totalGCAmt.value = val(hdn_totalGCAmt.value) + val(txt_Total_GC_Amount[0].value);
            }
            else
            {
               lbl_total_GC.innerHTML = val(lbl_total_GC.innerHTML) - 1;
               lbl_total_BalArt.innerHTML = val(lbl_total_BalArt.innerHTML) - val(txt_Balance_Art[0].value);
               lbl_total_BalActWt.innerHTML = val(lbl_total_BalActWt.innerHTML) - val(txt_Balance_ActWt[0].value);
               lbl_total_DelArt.innerHTML = val(lbl_total_DelArt.innerHTML) - val(txt_Delivered_Art[0].value);
               lbl_total_DelWt.innerHTML = val(lbl_total_DelWt.innerHTML) - val(txt_Delivered_ActWt[0].value);
               lbl_totalGCAmt.innerHTML = val(lbl_totalGCAmt.innerHTML) - val(txt_Total_GC_Amount[0].value);
               
               hdn_total_GC.value = val(hdn_total_GC.value) - 1;
               hdn_total_BalArt.value = val(hdn_total_BalArt.value) - val(txt_Balance_Art[0].value);
               hdn_total_BalActWt.value = val(hdn_total_BalActWt.value) - val(txt_Balance_ActWt[0].value);
               hdn_total_DelArt.value = val(hdn_total_DelArt.value) - val(txt_Delivered_Art[0].value);
               hdn_total_DelWt.value = val(hdn_total_DelWt.value) - val(txt_Delivered_ActWt[0].value);
               hdn_totalGCAmt.value = val(hdn_totalGCAmt.value) - val(txt_Total_GC_Amount[0].value);
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
        
        var ddl_DeliveryMode = document.getElementById('WucPDS1_ddl_DeliveryMode');
        var txt_GodownSupervisor = document.getElementById('WucPDS1_ddl_GodownSupervisor_txtBoxddl_GodownSupervisor');
        var hdn_Total_GC = document.getElementById('WucPDS1_hdn_Total_GC');
        var hdn_GCCaption = document.getElementById('WucPDS1_hdn_GCCaption');
//        var hdn_VehicleID = document.getElementById('WucPDS1_hdn_VehicleID');
        
        var lbl_Error_Client = document.getElementById('WucPDS1_lbl_Error_Client');
        lbl_Error_Client.innerHTML ="";
         
        if(val(hdn_Total_GC.value) == 0)
        {
            lbl_Error_Client.innerHTML = "Please Select Atleast One " + hdn_GCCaption.value;
        }
        else if (val(ddl_DeliveryMode.value) <= 0)
        {
            lbl_Error_Client.innerHTML =  "Please Select Delivery Mode";//objResource.GetMsg("Msg_ddl_DeliveryMode");
            ddl_DeliveryMode.focus();
        }
//        else if (val(ddl_DeliveryMode.value) == 2 && val(hdn_VehicleID.value) <= 0) 
//        {
//            lbl_Error_Client.innerHTML = "Please Select Vehicle No."; //objResource.GetMsg("Msg_ddl_DeliveryMode");
//            ddl_DeliveryMode.focus();
//        }        
        else if(txt_GodownSupervisor.value == '')
        {
            lbl_Error_Client.innerHTML = "Please Select Godown Supervisor";//objResource.GetMsg("Msg_txt_Supervisor");
            txt_GodownSupervisor.focus();
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
    
    var hdn_LoginBranch_Id= document.getElementById('WucPDS1_hdn_LoginBranch_Id'); 
    var Memo_Date = WucPDS1_dtp_PDS_Date.GetSelectedDate();
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

function FreightCalculator()
{
    var Vehicleid = document.getElementById('WucPDS1_hdn_VehicleID');
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-300);
    var popH = (h-300);
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
    
    var Url = "FrmPDSTempoFreightCalculator.aspx?VehicleID=" + Vehicleid.value;                                    
    window.open(Url, 'FreightCalculator', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
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
      var dg_PDS = window.opener.document.getElementById("WucPDS1_dg_PDS");
      var chk_AddLR = window.opener.document.getElementById("WucPDS1_chk_AddLR");
      var lblSelectedLRs = document.getElementById('lblSelectedLRs');
      var txt_get_item =  window.opener.document.getElementById("WucPDS1_WucSelectedItems1_txt_get_item");
      var btn_get_data = window.opener.document.getElementById("WucPDS1_WucSelectedItems1_btn_get_data");
      if (txt_get_item.value == "")
        txt_get_item.value = lblSelectedLRs.innerHTML;
      else
        txt_get_item.value = txt_get_item.value + "," + lblSelectedLRs.innerHTML;
      
      self.close();
      btn_get_data.click(); 
    }
}