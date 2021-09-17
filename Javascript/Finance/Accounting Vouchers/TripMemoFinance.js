// JScript File
function Check_All(chk,gridname)
    {              
        var grid = document.getElementById(gridname);
        var checkbox;
        var i,j=0;
        var sum_GCs=0,sum_Package=0,sum_Freight=0;
        var Memos,GCs,Package,Freight;

        var lbl_Total_Memo = document.getElementById('lbl_Total_Memo');
        var lbl_Total_GC = document.getElementById('lbl_Total_GC');
        var lbl_totalpackage = document.getElementById('lbl_totalpackage');
        var lbl_totalFreight = document.getElementById('lbl_totalFreight');
       
        var hdn_Total_Memo = document.getElementById('hdn_Total_Memo');
        var hdn_Total_GC = document.getElementById('hdn_Total_GC');
        var hdn_totalpackage= document.getElementById('hdn_totalpackage');
        var hdn_totalFreight= document.getElementById('hdn_totalFreight'); 
        
        var max = (grid.rows.length - 1);
        for(i=1;i<grid.rows.length;i++)
        {            
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            GCs  = grid.rows[i].cells[7].getElementsByTagName('input');
            Package  = grid.rows[i].cells[8].getElementsByTagName('input');
            Freight = grid.rows[i].cells[9].getElementsByTagName('input');

            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            } 
            
            if(chk.checked == true)
            {
                if(GCs[0].type =='text')
                {
                    sum_GCs = sum_GCs + val(GCs[0].value);
                }
                if(Package[0].type =='text')
                {
                    sum_Package = sum_Package + val(Package[0].value);
                }
                if(Freight[0].type =='text')
                {
                    sum_Freight = sum_Freight + val(Freight[0].value);
                }
            }
            
        }
        if(chk.checked == true)
        {
            lbl_Total_Memo.innerHTML = max;
            lbl_Total_GC.innerHTML = sum_GCs;
            lbl_totalpackage.innerHTML  = sum_Package;
            lbl_totalFreight.innerHTML  = sum_Freight;
                    
            hdn_Total_Memo.value = max;
            hdn_Total_GC.value = sum_GCs;
            hdn_totalpackage.value = sum_Package;
            hdn_totalFreight.value = sum_Freight;
        }
        else
        {
            lbl_Total_Memo.innerHTML = 0;
            lbl_Total_GC.innerHTML = 0;
            lbl_totalpackage.innerHTML  = 0;
            lbl_totalFreight.innerHTML  = 0;
                    
            hdn_Total_GC.value = 0;
            hdn_Total_Memo.value = 0;
            hdn_totalpackage.value = 0;
            hdn_totalFreight.value = 0;
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
            var grid = document.getElementById('dg_Memo');
        }
       
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;

        var Memos,GCs,Package,Freight;

        var lbl_Total_Memo = document.getElementById('lbl_Total_Memo');
        var lbl_Total_GC = document.getElementById('lbl_Total_GC');
        var lbl_totalpackage = document.getElementById('lbl_totalpackage');
        var lbl_totalFreight = document.getElementById('lbl_totalFreight');
      
        var hdn_Total_Memo = document.getElementById('hdn_Total_Memo');
        var hdn_Total_GC = document.getElementById('hdn_Total_GC');
        var hdn_totalpackage= document.getElementById('hdn_totalpackage');
        var hdn_totalFreight= document.getElementById('hdn_totalFreight');  
         
//        GCs  = row.cells[6].getElementsByTagName('input');
//        Package  = row.cells[7].getElementsByTagName('input');
//        Freight = row.cells[8].getElementsByTagName('input');
             
        lbl_Total_Memo.innerHTML='';
        lbl_Total_GC.innerHTML='';
        lbl_totalpackage.innerHTML='';
        lbl_totalFreight.innerHTML='';
        
        hdn_Total_Memo.value='';     
        hdn_Total_GC.value='';     
        hdn_totalpackage.value='';     
        hdn_totalFreight.value='';  
         
        for (i = 1; i < grid.rows.length; i++)
        {
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            GCs = grid.rows[i].cells[7].getElementsByTagName('input');
            Package = grid.rows[i].cells[8].getElementsByTagName('input');
            Freight = grid.rows[i].cells[9].getElementsByTagName('input');
            
            if (checkbox[0].checked == true) 
            {
                hdn_Total_Memo.value = val(hdn_Total_Memo.value) + 1;
                hdn_Total_GC.value = val(hdn_Total_GC.value) + val(GCs[0].value);
                hdn_totalpackage.value = val(hdn_totalpackage.value) + val(Package[0].value);
                hdn_totalFreight.value = val(hdn_totalFreight.value) + val(Freight[0].value);
               
                lbl_Total_Memo.innerHTML = val(lbl_Total_Memo.innerHTML) + 1;
                lbl_Total_GC.innerHTML = val(hdn_Total_GC.value);
                lbl_totalpackage.innerHTML = val(hdn_totalpackage.value);
                lbl_totalFreight.innerHTML = val(hdn_totalFreight.value); 
            }
        }
             
             
//        if(callfrom == 1)
//        {
//            if(chk.checked == true)
//            {
//                hdn_Total_Memo.value = val(hdn_Total_Memo.value) + 1;
//                hdn_Total_GC.value = val(hdn_Total_GC.value) + val(GCs[0].value);
//                hdn_totalpackage.value = val(hdn_totalpackage.value) + val(Package[0].value);
//                hdn_totalFreight.value = val(hdn_totalFreight.value) + val(Freight[0].value);
//                
//                lbl_Total_Memo.innerHTML = val(lbl_Total_Memo.innerHTML) + 1;
//                lbl_Total_GC.innerHTML = val(hdn_Total_GC.value);
//                lbl_totalpackage.innerHTML = val(hdn_totalpackage.value);
//                lbl_totalFreight.innerHTML = val(hdn_totalFreight.value);
//            }
//            else
//            {
//                hdn_Total_Memo.value = val(hdn_Total_Memo.value) - 1;
//                hdn_Total_GC.value = val(hdn_Total_GC.value) - val(GCs[0].value);
//                hdn_totalpackage.value = val(hdn_totalpackage.value) - val(Package[0].value);
//                hdn_totalFreight.value = val(hdn_totalFreight.value) - val(Freight[0].value);

//                lbl_Total_Memo.innerHTML = val(lbl_Total_Memo.innerHTML) - 1;
//                lbl_Total_GC.innerHTML = val(hdn_Total_GC.value);
//                lbl_totalpackage.innerHTML = val(hdn_totalpackage.value);
//                lbl_totalFreight.innerHTML = val(hdn_totalFreight.value);
//            }
//        }
        
              

        if((grid.rows.length-1) == val(hdn_Total_Memo.value))
        {
            checkall[0].checked = true;
        }
        else
        {
            checkall[0].checked = false;
        }
    }   
 
    
function validateUI()
{
 
  var ATS = false;
  var lblErrors = document.getElementById('lblErrors');

  var DDLBroker = document.getElementById('DDLBroker_txtBoxDDLBroker');
  var DDLFromBranch = document.getElementById('DDLFromBranch_txtBoxDDLFromBranch');
  var DDLToBranch = document.getElementById('DDLToBranch_txtBoxDDLToBranch');
  var DDLDriver = document.getElementById('DDLDriver_txtBoxDDLDriver');
//  var ddlTDSCertificateTo = document.getElementById('ddlTDSCertificateTo');
  var DDLVehicle = document.getElementById('DDLVehicle_ddl_Vehicle'); 
  var hdn_Total_Memo = document.getElementById('hdn_Total_Memo');
  var txtAdvance = document.getElementById('txtAdvance');
  var hdnBalance = document.getElementById('hdnBalance');
  var DDLATHPayableAt = document.getElementById('WucHierarchyWithIDATH_ddl_hierarchy');
  var DDLATHLocation = document.getElementById('WucHierarchyWithIDATH_ddl_location');
  var DDLBTHPayableAt = document.getElementById('WucHierarchyWithIDBTH_ddl_hierarchy');
  var DDLBTHLocation = document.getElementById('WucHierarchyWithIDBTH_ddl_location');
  
//Start added on 11-12-13
  var hdn_VehicleCategoryIds = document.getElementById('hdn_VehicleCategoryIds');
  var VehicleCategoryIds = hdn_VehicleCategoryIds.value;
//End added on 11-12-13
  lblErrors.innerText ="";

  if (DDLVehicle.options.length == 0)
  {
      lblErrors.innerText = "Please Select Vehicle No";
  }
//  else if (DDLBroker.value == '')
//  {
//     lblErrors.innerText = "Please Select Broker";
//     DDLBroker.focus();
//  }
  else if (DDLFromBranch.value == '')
  {
     lblErrors.innerText = "Please Select From branch";
     DDLFromBranch.focus();
  }

  else if (VehicleCategoryIds == "5" && DDLBroker.value == '')
  {
     lblErrors.innerText = "Please Select To Broker";
     DDLBroker.focus();
  }
  else if (DDLFromBranch.value == DDLToBranch.value)
  {
     lblErrors.innerText = "From branch and To branch cannot be same";
     DDLToBranch.focus();
  }
//  else if (DDLDriver.value == '')
//  {
//     lblErrors.innerText = "Please Select Driver";
//     DDLDriver.focus();
//  }
//  else if(val(hdn_Total_Memo.value) == 0)
//  {
//    lblErrors.innerText = "Please Select Atleast One Record";
//  }
//  else if (val(ddlTDSCertificateTo.value) == 0)
//  {
//     lblErrors.innerText = "Please Select TDS Certificate to";
//     ddlTDSCertificateTo.focus();
//  }
  else if (val(txtAdvance.value) > 0 && (DDLATHPayableAt.value == "0"))
  {
     lblErrors.innerText = "Please Select ATH payable at";
     DDLATHPayableAt.focus();
  }
  else if (val(txtAdvance.value) > 0 && DDLATHPayableAt.value != "HO" && DDLATHLocation.value == "0")
  {
     lblErrors.innerText = "Please Select ATH payable at location";
     DDLATHLocation.focus();
  }
  else if (val(hdnBalance.value) > 0 && DDLBTHPayableAt.value == "0")
  {
     lblErrors.innerText = "Please Select BTH payable at";
     DDLBTHPayableAt.focus();
  }
  else if (val(hdnBalance.value) > 0 && DDLBTHPayableAt.value != "HO" && DDLBTHLocation.value == "0")
  {
     lblErrors.innerText = "Please Select BTH payable at location";
     DDLBTHLocation.focus();
  } 
  else
  {
      ATS = true;
  }

return ATS;
}

function TDSCertificateToChange()
{
 
  var ddlTDSCertificateTo = document.getElementById('ddlTDSCertificateTo');
  var trIsRCRecieved = document.getElementById('trIsRCRecieved');
  var trIsPanCardRecieved = document.getElementById('trIsPanCardRecieved');
  var chkIsRCRecieved = document.getElementById('chkIsRCRecieved');
  var chkIsPanCardRecieved = document.getElementById('chkIsPanCardRecieved'); 
 
  //Start added on 11-12-13
  var hdn_VehicleCategoryIds = document.getElementById('hdn_VehicleCategoryIds');
  if (hdn_VehicleCategoryIds != null)
  {
     var VehicleCategoryIds = hdn_VehicleCategoryIds.value;
     var trBrokerName = document.getElementById('trBrokerName'); 
  }
  
  if (hdn_VehicleCategoryIds != null)
  {
    if (VehicleCategoryIds == "1")
    {    
       ddlTDSCertificateTo.value = 1;  
          
     
      chkIsRCRecieved.checked = true;
      chkIsPanCardRecieved.checked = true;
      
      chkIsRCRecieved.disabled = true;
      chkIsPanCardRecieved.disabled = true;    
      ddlTDSCertificateTo.disabled = true;
      trBrokerName.style.display = 'none'; 
     }
   
    else if (VehicleCategoryIds == "5")
    {
      trBrokerName.style.display = 'inline'; 
    }
    else
    {     
      
      chkIsRCRecieved.checked = false;
      chkIsPanCardRecieved.checked = false;
      
      chkIsRCRecieved.disabled = false;
      chkIsPanCardRecieved.disabled = false;    
      ddlTDSCertificateTo.disabled = false;
      trBrokerName.style.display = 'none';
    } 
  }  //End added on 11-12-13  
   
  
   if (ddlTDSCertificateTo != null)
  {
  if (val(ddlTDSCertificateTo.value) == 0)
  {
    chkIsRCRecieved.checked = false;
    trIsRCRecieved.style.display = 'none';
    chkIsPanCardRecieved.checked = false;
    trIsPanCardRecieved.style.display = 'none';
  }
  else if (val(ddlTDSCertificateTo.value) == 1) //owner
  {
//    trIsRCRecieved.style.display = 'inline';
//    trIsPanCardRecieved.style.display = 'inline';    
 
    
  }
  else if (val(ddlTDSCertificateTo.value) == 2)//broker
  {
    chkIsRCRecieved.checked = false;
    trIsRCRecieved.style.display = 'none';
//    trIsPanCardRecieved.style.display = 'inline';
   
  }
   
  }
  CalculateTDSPercent();
}

function CalculateTDSPercent()
{
  var ddlTDSCertificateTo = document.getElementById('ddlTDSCertificateTo');
  var chkIsRCRecieved = document.getElementById('chkIsRCRecieved');
  var chkIsPanCardRecieved = document.getElementById('chkIsPanCardRecieved');
  var DDLBrokerid = document.getElementById('DDLBroker_lstBoxDDLBroker');
  
  var vendorid;
  if (DDLBrokerid != null)
  {
    vendorid = val(DDLBrokerid.value);
    
    var LoadingDate = dtp_TripMemo_Date.GetSelectedDate();
    LoadingDate = new Date(LoadingDate.getFullYear(), LoadingDate.getMonth(),LoadingDate.getDate())

    Raj.EF.CallBackFunction.CallBack.GetTDSPercent
    (LoadingDate,val(ddlTDSCertificateTo.value),chkIsRCRecieved.checked,chkIsPanCardRecieved.checked,vendorid,handleTDSPercent)
  }
}

function handleTDSPercent(Results)
{
  if (Results.value != null)
  {
    var lblTDSPercent = document.getElementById('lblTDSPercent');
    var hdnTDSPercent = document.getElementById('hdnTDSPercent');
    var lblSurChargePercent = document.getElementById('lblSurChargePercent');
    var hdnSurchargePercent = document.getElementById('hdnSurchargePercent');
    var lblAdditionalSurchargeCessPercent = document.getElementById('lblAdditionalSurchargeCessPercent');
    var hdnAdditionalSurchargeCessPercent = document.getElementById('hdnAdditionalSurchargeCessPercent');
    var lblAddistionalEducationCessPercent = document.getElementById('lblAddistionalEducationCessPercent');
    var hdnAddistionalEducationCessPercent = document.getElementById('hdnAddistionalEducationCessPercent');

    var rows = Results.value.Rows[0];

    lblTDSPercent.innerHTML = "TDS";
    lblSurChargePercent.innerHTML = "Surcharge";
    lblAdditionalSurchargeCessPercent.innerHTML = "Additional Surcharge Cess";
    lblAddistionalEducationCessPercent.innerHTML = "Additional Education Cess";

    hdnTDSPercent.value = 0;//val(rows['TDSPercent']);
    hdnSurchargePercent.value = 0;//val(rows['SurchargePercent']);
    hdnAdditionalSurchargeCessPercent.value = 0;//val(rows['AdditionalSurchargeCessPercent']);
    hdnAddistionalEducationCessPercent.value = 0;//val(rows['AdditionalEducationCessPercent']);    

    lblTDSPercent.innerHTML = lblTDSPercent.innerHTML + ' ' + hdnTDSPercent.value + "%:";
    lblSurChargePercent.innerHTML = lblSurChargePercent.innerHTML + ' ' + hdnSurchargePercent.value + "%:";
    lblAdditionalSurchargeCessPercent.innerHTML = lblAdditionalSurchargeCessPercent.innerHTML + ' ' + hdnAdditionalSurchargeCessPercent.value + "%:";
    lblAddistionalEducationCessPercent.innerHTML = lblAddistionalEducationCessPercent.innerHTML + ' ' + hdnAddistionalEducationCessPercent.value + "%:";

    CalculateBalance();
  }
}

function CalculateBalance()
{

  var txtHireAmount = document.getElementById('txtHireAmount');
  
  var hdnTDSPercent = document.getElementById('hdnTDSPercent');
  var lblTDSAmount = document.getElementById('lblTDSAmount');
  var hdnTDSAmount = document.getElementById('hdnTDSAmount');
  
  var hdnSurchargePercent = document.getElementById('hdnSurchargePercent');
  var lblSurChargeAmount = document.getElementById('lblSurChargeAmount');
  var hdnSurChargeAmount = document.getElementById('hdnSurChargeAmount');

  var hdnAdditionalSurchargeCessPercent = document.getElementById('hdnAdditionalSurchargeCessPercent');
  var lblAdditionalSurchargeCessAmount = document.getElementById('lblAdditionalSurchargeCessAmount');
  var hdnAdditionalSurchargeCessAmount = document.getElementById('hdnAdditionalSurchargeCessAmount');

  var hdnAddistionalEducationCessPercent = document.getElementById('hdnAddistionalEducationCessPercent');
  var lblAddistionalEducationCessAmount = document.getElementById('lblAddistionalEducationCessAmount');
  var hdnAddistionalEducationCessAmount = document.getElementById('hdnAddistionalEducationCessAmount');

  var lblTotalTDSAmount = document.getElementById('lblTotalTDSAmount');
  var hdnTotalTDSAmount = document.getElementById('hdnTotalTDSAmount');

  var lblTruckHirePayable = document.getElementById('lblTruckHirePayable');
  var hdnTruckHirePayable = document.getElementById('hdnTruckHirePayable');
  var txtAdvance = document.getElementById('txtAdvance');
  var lblBalance = document.getElementById('lblBalance');
  var hdnBalance = document.getElementById('hdnBalance'); 

  hdnTDSAmount.value = 0;//val(val(txtHireAmount.value) * (val(hdnTDSPercent.value)/100));
  hdnSurChargeAmount.value = 0;//val(val(hdnTDSAmount.value) * val(hdnSurchargePercent.value)/100);
  hdnAdditionalSurchargeCessAmount.value = 0;//val((val(hdnTDSAmount.value) + val(hdnSurChargeAmount.value))* val(hdnAdditionalSurchargeCessPercent.value)/100);
  hdnAddistionalEducationCessAmount.value = 0;//val((val(hdnTDSAmount.value) + val( hdnSurChargeAmount.value))* val(hdnAddistionalEducationCessPercent.value)/100);
  hdnTotalTDSAmount.value = 0;//val(val(hdnTDSAmount.value) + val(hdnSurChargeAmount.value) + val(hdnAdditionalSurchargeCessAmount.value) + val(hdnAddistionalEducationCessAmount.value));

  lblTDSAmount.innerHTML = 0;//val(hdnTDSAmount.value);
  lblSurChargeAmount.innerHTML = 0;//val(hdnSurChargeAmount.value);
  lblAdditionalSurchargeCessAmount.innerHTML = 0;//val(hdnAdditionalSurchargeCessAmount.value);
  lblAddistionalEducationCessAmount.innerHTML = 0;//val(hdnAddistionalEducationCessAmount.value);
  lblTotalTDSAmount.innerHTML = 0;//val(hdnTotalTDSAmount.value);

  lblTruckHirePayable.innerHTML = val(txtHireAmount.value);//val(val(txtHireAmount.value) - val(hdnTotalTDSAmount.value));
  hdnTruckHirePayable.value = val(txtHireAmount.value);//lblTruckHirePayable.innerHTML;
  
  if (val(txtHireAmount.value) > 0)
  {
    lblBalance.innerHTML = val(val(hdnTruckHirePayable.value) - val(txtAdvance.value));
    hdnBalance.value = lblBalance.innerHTML;
  }
  else
  {
    lblBalance.innerHTML = "0";
    hdnBalance.value = "0";
  }
}

function DVLPDateChange()
{
  CalculateTDSPercent();
}

function Add_Driver_Window()
{
var hdn_Driver_path = document.getElementById('hdnDriverpath');

var w = screen.availWidth;
var h = screen.availHeight;
var popW = (w-100);
var popH = (h-100);
var leftPos = (w-popW)/2;
var topPos = (h-popH)/2;

if(hdn_Driver_path.value == '')
  {
  alert('You Don"t Have Rights to Add Driver');
  }
else
  {
  window.open(hdn_Driver_path.value, 'DVLPDriver', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
  return false;
  }

return false;
}

function Add_Broker_Window()
{
var hdn_Driver_path = document.getElementById('hdnBrokerPath');

var w = screen.availWidth;
var h = screen.availHeight;
var popW = (w-100);
var popH = (h-100);
var leftPos = (w-popW)/2;
var topPos = (h-popH)/2;

if(hdn_Driver_path.value == '')
  {
  alert('You Don"t Have Rights to Add Boker');
  }
else
  {
  window.open(hdn_Driver_path.value, 'DVLPBroker', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
  return false;
  }

return false;
}