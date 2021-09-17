// JScript File

  
    function Check_All(chk,gridname)
    {              
//    var grid = document.getElementById(gridname);
//    var d = 'h';
//    return
    
        var grid = document.getElementById(gridname);
        var checkbox,Freight,Weight,Articles,GCAmount,SerTaxAmt;
        var i,j=0;
        var sum_BAW=0,sum_BTPC=0,sum_CAW=0,sum_CTPC=0,sum_TAW=0,sum_TTPC=0,sum_LodArt=0,sum_LodWt=0;
        var Actual_Wt,Loaded_Art,Loaded_Wt,CommValve,Sub_Total,Booking_Branch_Id,Payment_Type_Id;

        var txt_Book_ActualWt = document.getElementById('WucMenifest1_txt_Book_ActualWt');
        var txt_Book_ToPayCollection = document.getElementById('WucMenifest1_txt_Book_ToPayCollection');
        var txt_Cros_ActualWt = document.getElementById('WucMenifest1_txt_Cros_ActualWt');
        var txt_Cros_ToPayCollection = document.getElementById('WucMenifest1_txt_Cros_ToPayCollection');
        var txt_Total_ActualWt = document.getElementById('WucMenifest1_txt_Total_ActualWt');
        var txt_Total_ToPayCollection = document.getElementById('WucMenifest1_txt_Total_ToPayCollection');
        var lbl_tolalLodArt = document.getElementById('WucMenifest1_lbl_tolalLodArt');
        var lbl_tolalLodWt = document.getElementById('WucMenifest1_lbl_tolalLodWt');
        var lbl_Total_GC = document.getElementById('WucMenifest1_lbl_Total_GC');

        var hdn_Book_ActualWt= document.getElementById('WucMenifest1_hdn_Book_ActualWt');
        var hdn_Book_ToPayCollection= document.getElementById('WucMenifest1_hdn_Book_ToPayCollection');
        var hdn_Cros_ActualWt = document.getElementById('WucMenifest1_hdn_Cros_ActualWt');
        var hdn_Cros_ToPayCollection = document.getElementById('WucMenifest1_hdn_Cros_ToPayCollection');
        var hdn_Total_ActualWt= document.getElementById('WucMenifest1_hdn_Total_ActualWt');
        var hdn_Total_ToPayCollection= document.getElementById('WucMenifest1_hdn_Total_ToPayCollection');
        var hdn_tolalLodArt= document.getElementById('WucMenifest1_hdn_tolalLodArt');
        var hdn_tolalLodWt= document.getElementById('WucMenifest1_hdn_tolalLodWt');
        
        var hdn_LoginBranch_Id= document.getElementById('WucMenifest1_hdn_LoginBranch_Id');
        var hdn_Total_GC = document.getElementById('WucMenifest1_hdn_Total_GC');
        
        var max = (grid.rows.length - 1);
        for(i=1;i<grid.rows.length;i++)
        {            
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            Actual_Wt= grid.rows[i].cells[8].getElementsByTagName('input');
            Loaded_Art  = grid.rows[i].cells[13].getElementsByTagName('input');
            Loaded_Wt = grid.rows[i].cells[14].getElementsByTagName('input');
            CommValve = grid.rows[i].cells[15].getElementsByTagName('input');    //it Contain Sub_Total,Booking_Branch_Id,
                                                                        //Payment_Type_Id
            Sub_Total = CommValve[0].value;
            Booking_Branch_Id = CommValve[1].value;
            Payment_Type_Id = CommValve[2].value;


            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            }
            
            if(val(hdn_LoginBranch_Id.value)== val(Booking_Branch_Id))
            {
                if(chk.checked == true)
                {
                    if(Actual_Wt[0].type =='text')
                    {
                        sum_BAW = sum_BAW + val(Actual_Wt[0].value);
                    }                                     
                }
            }
            
            if((Payment_Type_Id == 1) && val(hdn_LoginBranch_Id.value)== val(Booking_Branch_Id))
            {
                if(chk.checked == true)
                {
                   if(CommValve[0].type =='hidden')
                    {
                        sum_BTPC = sum_BTPC + val(Sub_Total);
                    }                   
                }
            }
            
            if(val(hdn_LoginBranch_Id.value)!= val(Booking_Branch_Id))
            {
                if(chk.checked == true)
                {
                    if(Actual_Wt[0].type =='text')
                    {
                        sum_CAW = sum_CAW + val(Actual_Wt[0].value);
                    }                                   
                }
            }
            
            if((Payment_Type_Id == 1) && val(hdn_LoginBranch_Id.value)!= val(Booking_Branch_Id))
            {
                if(chk.checked == true)
                {
                    if(CommValve[0].type =='hidden')
                    {
                        sum_CTPC = sum_CTPC + val(Sub_Total);
                    }
                }
            }
            
            if(chk.checked == true)
            {
                if(Loaded_Art[0].type =='text')
                {
                    sum_LodArt = sum_LodArt + val(Loaded_Art[0].value);
                }
                if(Loaded_Wt[0].type =='text')
                {
                    sum_LodWt = sum_LodWt + val(Loaded_Wt[0].value);
                }
            }
            
        }
        if(chk.checked == true)
        {
            txt_Book_ActualWt.value = sum_BAW;
            txt_Book_ToPayCollection.value = sum_BTPC;
            txt_Cros_ActualWt.value = sum_CAW;
            txt_Cros_ToPayCollection.value  = sum_CTPC;
            txt_Total_ActualWt.value  = val(sum_BAW) + val(sum_CAW);
            txt_Total_ToPayCollection.value  = val(sum_BTPC) + val(sum_CTPC);
            lbl_tolalLodArt.innerHTML  = sum_LodArt;
            lbl_tolalLodWt.innerHTML  = sum_LodWt;
            lbl_Total_GC.innerHTML = max;

            hdn_Total_GC.value = max;
            hdn_Book_ActualWt.value = sum_BAW;
            hdn_Book_ToPayCollection.value = sum_BTPC;
            hdn_Cros_ActualWt.value = sum_CAW;
            hdn_Cros_ToPayCollection.value  = sum_CTPC;
            hdn_Total_ActualWt.value = val(sum_BAW) + val(sum_CAW);
            hdn_Total_ToPayCollection.value = val(sum_BTPC) + val(sum_CTPC);
            hdn_tolalLodArt.value = sum_LodArt;
            hdn_tolalLodWt.value = sum_LodWt;
        }
        else
        {
            txt_Book_ActualWt.value = 0;
            txt_Book_ToPayCollection.value = 0;
            txt_Cros_ActualWt.value = 0;
            txt_Cros_ToPayCollection.value  = 0;
            txt_Total_ActualWt.value  = 0;
            txt_Total_ToPayCollection.value  = 0;
            lbl_tolalLodArt.innerHTML  = 0;
            lbl_tolalLodWt.innerHTML  = 0;
            lbl_Total_GC.innerHTML = 0;
                    
            hdn_Total_GC.value = 0;
            hdn_Book_ActualWt.value = 0;
            hdn_Book_ToPayCollection.value = 0;
            hdn_Cros_ActualWt.value = 0;
            hdn_Cros_ToPayCollection.value  = 0;
            hdn_Total_ActualWt.value = 0;
            hdn_Total_ToPayCollection.value = 0;
            hdn_tolalLodArt.value = 0;
            hdn_tolalLodWt.value = 0;
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
            var grid = document.getElementById('WucMenifest1_dg_Memo');
        }
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;

        var Actual_Wt,Loaded_Art,Loaded_Wt,CommValve,Sub_Total,Booking_Branch_Id,Payment_Type_Id,Bal_Article,Bal_Act_Wt;

        var txt_Book_ActualWt = document.getElementById('WucMenifest1_txt_Book_ActualWt');
        var txt_Book_ToPayCollection = document.getElementById('WucMenifest1_txt_Book_ToPayCollection');
        var txt_Cros_ActualWt = document.getElementById('WucMenifest1_txt_Cros_ActualWt');
        var txt_Cros_ToPayCollection = document.getElementById('WucMenifest1_txt_Cros_ToPayCollection');
        var txt_Total_ActualWt = document.getElementById('WucMenifest1_txt_Total_ActualWt');
        var txt_Total_ToPayCollection = document.getElementById('WucMenifest1_txt_Total_ToPayCollection');
        var lbl_tolalLodArt = document.getElementById('WucMenifest1_lbl_tolalLodArt');
        var lbl_tolalLodWt = document.getElementById('WucMenifest1_lbl_tolalLodWt');
        var lbl_Total_GC = document.getElementById('WucMenifest1_lbl_Total_GC');

        var hdn_Book_ActualWt= document.getElementById('WucMenifest1_hdn_Book_ActualWt');
        var hdn_Book_ToPayCollection= document.getElementById('WucMenifest1_hdn_Book_ToPayCollection');
        var hdn_Cros_ActualWt = document.getElementById('WucMenifest1_hdn_Cros_ActualWt');
        var hdn_Cros_ToPayCollection = document.getElementById('WucMenifest1_hdn_Cros_ToPayCollection');
        var hdn_Total_ActualWt= document.getElementById('WucMenifest1_hdn_Total_ActualWt');
        var hdn_Total_ToPayCollection= document.getElementById('WucMenifest1_hdn_Total_ToPayCollection');
        var hdn_tolalLodArt= document.getElementById('WucMenifest1_hdn_tolalLodArt');
        var hdn_tolalLodWt= document.getElementById('WucMenifest1_hdn_tolalLodWt');
        
        var hdn_LoginBranch_Id= document.getElementById('WucMenifest1_hdn_LoginBranch_Id');
        var hdn_Total_GC = document.getElementById('WucMenifest1_hdn_Total_GC');
                
            Actual_Wt= row.cells[8].getElementsByTagName('input');
            Bal_Article= row.cells[10].getElementsByTagName('input');
            Bal_Act_Wt= row.cells[12].getElementsByTagName('input');
            Loaded_Art  = row.cells[13].getElementsByTagName('input');
            Loaded_Wt = row.cells[14].getElementsByTagName('input');
            CommValve = row.cells[15].getElementsByTagName('input');    //it Contain Sub_Total,Booking_Branch_Id,
                                                                        //Payment_Type_Id
            Sub_Total = CommValve[0].value;
            Booking_Branch_Id = CommValve[1].value;
            Payment_Type_Id = CommValve[2].value;
            
        if(val(Loaded_Art[0].value) > val(Bal_Article[0].value))
        {
            Loaded_Art[0].value = val(Bal_Article[0].value);
        }
        if(val(Loaded_Wt[0].value) > val(Bal_Act_Wt[0].value))
        {
            Loaded_Wt[0].value = val(Bal_Act_Wt[0].value);
        }
        
            if(callfrom == 1 && val(hdn_LoginBranch_Id.value)== val(Booking_Branch_Id))
            {
                if(chk.checked == true)
                {
                   txt_Book_ActualWt.value = val(txt_Book_ActualWt.value) + val(Actual_Wt[0].value);
                   hdn_Book_ActualWt.value = val(hdn_Book_ActualWt.value) + val(Actual_Wt[0].value);
                }
                else
                {
                   txt_Book_ActualWt.value = val(txt_Book_ActualWt.value) - val(Actual_Wt[0].value);
                   hdn_Book_ActualWt.value = val(hdn_Book_ActualWt.value) - val(Actual_Wt[0].value);
                }
            }
            if((callfrom == 1) && (Payment_Type_Id == 1) && val(hdn_LoginBranch_Id.value)== val(Booking_Branch_Id))
            {
                if(chk.checked == true)
                {
                   txt_Book_ToPayCollection.value = val(txt_Book_ToPayCollection.value) + val(Sub_Total);
                   hdn_Book_ToPayCollection.value = val(hdn_Book_ToPayCollection.value) + val(Sub_Total);               
                }
                else
                {
                   txt_Book_ToPayCollection.value = val(txt_Book_ToPayCollection.value) - val(Sub_Total);
                   hdn_Book_ToPayCollection.value = val(hdn_Book_ToPayCollection.value) - val(Sub_Total);               
                }
            }
            
            if((callfrom == 1) && val(hdn_LoginBranch_Id.value)!= val(Booking_Branch_Id))
            {
                if(chk.checked == true)
                {
                   txt_Cros_ActualWt.value = val(txt_Cros_ActualWt.value) + val(Actual_Wt[0].value);
                   hdn_Cros_ActualWt.value = val(hdn_Cros_ActualWt.value) + val(Actual_Wt[0].value);
                }
                else
                {
                   txt_Cros_ActualWt.value = val(txt_Cros_ActualWt.value) - val(Actual_Wt[0].value);
                   hdn_Cros_ActualWt.value = val(hdn_Cros_ActualWt.value) - val(Actual_Wt[0].value);
                }
            }
            
            if((callfrom == 1) && (Payment_Type_Id == 1) && val(hdn_LoginBranch_Id.value)!= val(Booking_Branch_Id))
            {
                if(chk.checked == true)
                {
                   txt_Cros_ToPayCollection.value = val(txt_Cros_ToPayCollection.value) + val(Sub_Total);
                   hdn_Cros_ToPayCollection.value = val(hdn_Cros_ToPayCollection.value) + val(Sub_Total);               
                }
                else
                {
                   txt_Cros_ToPayCollection.value = val(txt_Cros_ToPayCollection.value) - val(Sub_Total);
                   hdn_Cros_ToPayCollection.value = val(hdn_Cros_ToPayCollection.value) - val(Sub_Total);               
                }
            }
            if(callfrom == 1)
            {
                if(chk.checked == true)
                {
                    hdn_Total_GC.value = val(hdn_Total_GC.value) + 1;
                    hdn_tolalLodArt.value = val(hdn_tolalLodArt.value) + val(Loaded_Art[0].value);
                    hdn_tolalLodWt.value = val(hdn_tolalLodWt.value) + val(Loaded_Wt[0].value);
                    
                    lbl_Total_GC.innerHTML = val(lbl_Total_GC.innerHTML) + 1;
                    lbl_tolalLodArt.innerHTML = val(hdn_tolalLodArt.value);
                    lbl_tolalLodWt.innerHTML = val(hdn_tolalLodWt.value);
                }
                else
                {
                    hdn_Total_GC.value = val(hdn_Total_GC.value) - 1;
                    hdn_tolalLodArt.value = val(hdn_tolalLodArt.value) - val(Loaded_Art[0].value);
                    hdn_tolalLodWt.value = val(hdn_tolalLodWt.value) - val(Loaded_Wt[0].value);

                    lbl_Total_GC.innerHTML = val(lbl_Total_GC.innerHTML) - 1;
                    lbl_tolalLodArt.innerHTML = val(hdn_tolalLodArt.value);
                    lbl_tolalLodWt.innerHTML = val(hdn_tolalLodWt.value);
                }
            }
            
            if(callfrom == 2)       //onblur of  Loded Article
            {
                if(chk.checked == true)
                {
                    hdn_tolalLodArt.value = val(hdn_tolalLodArt.value) + val(Loaded_Art[0].value);
                    lbl_tolalLodArt.innerHTML = val(hdn_tolalLodArt.value);
                }               
            }
            if(callfrom == 3)       //onblur of  Loded Weight
            {
                if(chk.checked == true)
                {
                    hdn_tolalLodWt.value = val(hdn_tolalLodWt.value) + val(Loaded_Wt[0].value);
                    lbl_tolalLodWt.innerHTML = val(hdn_tolalLodWt.value);
                }               
            }
            if(callfrom == 4 && chk.checked == true)       //onfocus of  Loded Article
            {
                hdn_tolalLodArt.value = val(hdn_tolalLodArt.value) - val(Loaded_Art[0].value);
                lbl_tolalLodArt.innerHTML = val(hdn_tolalLodArt.value);
            }
            if(callfrom == 5 && chk.checked == true)        //onfocus of  Loded Weight
            {
                hdn_tolalLodWt.value = val(hdn_tolalLodWt.value) - val(Loaded_Wt[0].value);
                lbl_tolalLodWt.innerHTML = val(hdn_tolalLodWt.value);      
            }

            
            hdn_Total_ActualWt.value = val(hdn_Book_ActualWt.value) + val(hdn_Cros_ActualWt.value);
            txt_Total_ActualWt.value = val(hdn_Book_ActualWt.value) + val(hdn_Cros_ActualWt.value);
            hdn_Total_ToPayCollection.value = val(txt_Book_ToPayCollection.value) + val(hdn_Cros_ToPayCollection.value);
            txt_Total_ToPayCollection.value = val(txt_Book_ToPayCollection.value) + val(hdn_Cros_ToPayCollection.value);

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
        var ddl_ALSNo = document.getElementById('WucMenifest1_ddl_ALSNo');
        var hdn_ALS_Req = document.getElementById('WucMenifest1_hdn_ALS_Req');
        
        var ddl_MenifestType = document.getElementById('WucMenifest1_ddl_MenifestType');
        var ddl_MenifestTo = document.getElementById('WucMenifest1_ddl_MenifestTo_txtBoxddl_MenifestTo');
        var ddl_Loaded_By = document.getElementById('WucMenifest1_ddl_Loaded_By_txtBoxddl_Loaded_By');
        var tr_loading_supervisor = document.getElementById('WucMenifest1_tr_loading_supervisor');
        var txt_MenifestTo = document.getElementById('WucMenifest1_txt_MenifestTo');
        var ddl_VehicleCotegory = document.getElementById('WucMenifest1_ddl_VehicleCotegory');
        var ddl_Vehicle = document.getElementById('WucMenifest1_WucVehicleSearch1_ddl_Vehicle');
        var txt_Vehicle_Last_4_Digits = document.getElementById('WucMenifest1_WucVehicleSearch1_txt_Vehicle_Last_4_Digits');
        var hdn_Total_GC = document.getElementById('WucMenifest1_hdn_Total_GC');
        var hdn_GCCaption = document.getElementById('WucMenifest1_hdn_GCCaption');
        
        var lbl_Error_Client = document.getElementById('WucMenifest1_lbl_Error_Client');
        var lbl_Error = document.getElementById('WucMenifest1_lbl_Errors');
        
        lbl_Error_Client.innerHTML ="";
        lbl_Error.innerHTML ="";
                
        //var objResource=new Resource('WucMenifest1_hdf_ResourecString');
                
        if (val(ddl_MenifestType.value) == 0)
        {
            lbl_Error_Client.innerHTML = "Please Select Manifest Type"; //objResource.GetMsg("Msg_ddl_MenifestType");
            ddl_MenifestType.focus();
        }
        else if(val(ddl_MenifestType.value) == 1 && ddl_MenifestTo.value == '')
        {
            lbl_Error_Client.innerHTML = "Please Select Manifest To"; //objResource.GetMsg("Msg_ddl_MenifestTo");
            ddl_MenifestTo.focus();
        }
        else if(val(ddl_MenifestType.value) == 2 && txt_MenifestTo.value == '')
        {
            lbl_Error_Client.innerHTML = "Please Enter Manifest To" //objResource.GetMsg("Msg_txt_MenifestTo");
            txt_MenifestTo.focus();
        }
//        else if(val(ddl_VehicleCotegory.value) <= 0)
//        {
//            lbl_Error_Client.innerHTML = "Please Select Vehicle Category" //objResource.GetMsg("Msg_ddl_VehicleCotegory");
//            ddl_VehicleCotegory.focus();
//        }
        else if(val(ddl_Vehicle.value) <= 0)
        {
            lbl_Error_Client.innerHTML = "Please enter Vehicle No." ;//objResource.GetMsg("Msg_ddl_Vehicle");
            txt_Vehicle_Last_4_Digits.focus();
        }
        else if (hdn_ALS_Req.value == "True" && val(ddl_ALSNo.value) == 0)
        {
            lbl_Error_Client.innerHTML =  "Please Select ALS No";
            ddl_ALSNo.focus();
        }
        else if (ddl_Loaded_By.value == '' && control_is_mandatory(tr_loading_supervisor) == true)
        {
            lbl_Error_Client.innerHTML =  "Please Select Loaded By";
            ddl_Loaded_By.focus();
        }
        else if(val(hdn_Total_GC.value) == 0)
        {
            lbl_Error_Client.innerHTML = "Please Select Atleast One " + hdn_GCCaption.value;
        }
        else
        {
            ATS = true;
        }
        return ATS;
     }    

//    for display vehicle view

function enabledisable_memotobranch()
{
    var ddl_MenifestType = document.getElementById('WucMenifest1_ddl_MenifestType');
    var tr_ddl_memoto = document.getElementById('tr_ddl_memoto');
    var tr_txt_memoto = document.getElementById('tr_txt_memoto');

    if (parseInt(ddl_MenifestType.value) == 1)
    {
        tr_ddl_memoto.style.display = 'inline';
        tr_txt_memoto.style.display = 'none';
    }
    else
    {
       tr_ddl_memoto.style.display = 'none';
       tr_txt_memoto.style.display = 'inline';
    }  
}


function AddLR()
{
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-100);
    var popH = (h-100);
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
    
    var hdn_LoginBranch_Id= document.getElementById('WucMenifest1_hdn_LoginBranch_Id');
    var ddl_MenifestType = document.getElementById('WucMenifest1_ddl_MenifestType');
    var Memo_Date = WucMenifest1_Memo_Date.GetSelectedDate();
    var MemoYear = Memo_Date.getFullYear();
    var MemoMonth = Memo_Date.getMonth() + 1;
    var MemoDay = Memo_Date.getDate();
    
    Memo_Date = new Date(Memo_Date.getFullYear(), Memo_Date.getMonth(),Memo_Date.getDate());
    

    
    var Url = "FrmMenifestAddLR.aspx?BranchID=" +  hdn_LoginBranch_Id.value + 
                                    "&MemoTypeID=" + ddl_MenifestType.value + 
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
      var dg_Memo = window.opener.document.getElementById("WucMenifest1_dg_Memo");
      var chk_AddLR = window.opener.document.getElementById("WucMenifest1_chk_AddLR");
      var lblSelectedLRs = document.getElementById('lblSelectedLRs');
      var txt_get_item =  window.opener.document.getElementById("WucMenifest1_WucSelectedItems1_txt_get_item");
      var btn_get_data = window.opener.document.getElementById("WucMenifest1_WucSelectedItems1_btn_get_data");
      if (txt_get_item.value == "")
        txt_get_item.value = lblSelectedLRs.innerHTML;
      else
        txt_get_item.value = txt_get_item.value + "," + lblSelectedLRs.innerHTML;
      
      self.close();
      btn_get_data.click();
//      Check_All(chk_AddLR,dg_Memo)
    }
}   


var Search_Type;
var lst_control_id;
function Search_txtSearch(e,txtbox,lstBox,SearchType,length)
{    
    Search_Type = SearchType;
    lst_control_id = lstBox;
    if (txtbox.value == '')
    {
        Clear_listbox(lstBox);
        hidecontrol(document.getElementById(lstBox));
        return;
    }
    var txtvalue = txtbox.value.toUpperCase();
    if(txtvalue.length >= length)
    {
        if (keyCode != 37 && keyCode != 38 && keyCode != 39 && keyCode != 40)
        {
            if(oldvalue != txtvalue)
            {
                if(SearchType == 'MemoToBranch')
                    Raj.EF.CallBackFunction.CallBack.GetTxtSearchBranch(txtvalue,handleResults);
            }
        }
    }
    if (keyCode == 38 || keyCode == 40)
        listboxupdown(txtbox,lstBox);
}

function handleResults(results)
{
  var list_control = document.getElementById(lst_control_id);
  
  var tot = results.value.Rows.length -1;
  var count = 0;

  for (var count = list_control.options.length-1; count >-1; count--)
  {
    list_control.options[count] = null;
  }

  for (count = 0;count <= tot;count ++)
  { 
    list_control.options[count] = new Option(results.value.Rows[count][results.value.Columns[0].Name],results.value.Rows[count][results.value.Columns[1].Name]); 
  }
  
    if (list_control.options.length == 0)
      hidecontrol(list_control);
    else
      showcontrol(list_control);
}

function On_txtLostFocus(txtbox,list_control,hdn_control)
{
    var txtbox_value = document.getElementById(txtbox).value.toUpperCase();
    var listcontrol = document.getElementById(list_control); 
    var list_control_index = listcontrol.selectedIndex;
    var list_control_value;
    var list_control_text;
    
    hidecontrol(listcontrol);
    if (oldvalue != txtbox_value)
    {
    
        if (list_control_index != -1){
            list_control_value = listcontrol.options[list_control_index].value;
            list_control_text = listcontrol.options[list_control_index].text;
        }
        else{
            list_control_value = '0';
            list_control_text = '';
        }
    
        document.getElementById(hdn_control).value = list_control_value;
        document.getElementById(txtbox).value = list_control_text;
    }
    
     if(Search_Type == 'MemoToBranch')
     {
         if (oldvalue != txtbox_value)
            update_ArrivalDeliveryDateDetails();
     }    
}
