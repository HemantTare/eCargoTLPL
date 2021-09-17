// JScript File

function validateUI()
{
 
  var ATS = false;
  var lblErrors = document.getElementById('lblErrors');

  var DDLBroker = document.getElementById('txtBroker');
  var DDLFromBranch = document.getElementById('txt_FromBranch');
  var DDLToBranch = document.getElementById('txt_ToBranch');
  var DDLDriver = document.getElementById('txtDriver');
  var ddlTDSCertificateTo = document.getElementById('ddlTDSCertificateTo');
  var DDLVehicle = document.getElementById('DDLVehicle_ddl_Vehicle');
  var txtAdvance = document.getElementById('txtAdvance');
  var hdnBalance = document.getElementById('hdnBalance');
  var DDLATHPayableAt = document.getElementById('WucHierarchyWithIDATH_ddl_hierarchy');
  var DDLATHLocation = document.getElementById('WucHierarchyWithIDATH_ddl_location');
  var DDLBTHPayableAt = document.getElementById('WucHierarchyWithIDBTH_ddl_hierarchy');
  var DDLBTHLocation = document.getElementById('WucHierarchyWithIDBTH_ddl_location');

  var txt_MobileNo1 = document.getElementById('txt_MobileNo1');
 
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
  else if (DDLDriver.value == '')
  {
     lblErrors.innerText = "Please Select Driver";
     DDLDriver.focus();
  }
  else if (txt_MobileNo1.value == '')
  {
     lblErrors.innerText = "Please Enter Driver Mobile No. 1";
     txt_MobileNo1.focus();
  }
  else if (val(ddlTDSCertificateTo.value) == 0)
  {
     lblErrors.innerText = "Please Select TDS Certificate to";
     ddlTDSCertificateTo.focus();
  }
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
      if(trBrokerName != null)
       trBrokerName.style.display = 'none'; 
     }
   
    else if (VehicleCategoryIds == "5")
    {
      trBrokerName.style.display = ''; 
    }
    else
    {     
      
      chkIsRCRecieved.checked = false;
      chkIsPanCardRecieved.checked = false;
      
      chkIsRCRecieved.disabled = false;
      chkIsPanCardRecieved.disabled = false;    
      ddlTDSCertificateTo.disabled = false;
      if(trBrokerName != null)
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
    trIsRCRecieved.style.display = '';
    trIsPanCardRecieved.style.display = '';
  }
  else if (val(ddlTDSCertificateTo.value) == 2)//broker
  {
    chkIsRCRecieved.checked = false;
    trIsRCRecieved.style.display = 'none';
    trIsPanCardRecieved.style.display = '';   
  }
   
  }
  CalculateTDSPercent();
}

function CalculateTDSPercent()
{
  var ddlTDSCertificateTo = document.getElementById('ddlTDSCertificateTo');
  var chkIsRCRecieved = document.getElementById('chkIsRCRecieved');
  var chkIsPanCardRecieved = document.getElementById('chkIsPanCardRecieved');
  var DDLBrokerid = document.getElementById('hdnBroker');
  
  var vendorid;
  if (DDLBrokerid != null)
  {
    vendorid = val(DDLBrokerid.value);
    
    var LoadingDate = dtpLoadingDate_Picker.GetSelectedDate();
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

    hdnTDSPercent.value = val(rows['TDSPercent']);
    hdnSurchargePercent.value = val(rows['SurchargePercent']);
    hdnAdditionalSurchargeCessPercent.value = val(rows['AdditionalSurchargeCessPercent']);
    hdnAddistionalEducationCessPercent.value = val(rows['AdditionalEducationCessPercent']);    

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

  hdnTDSAmount.value = val(val(txtHireAmount.value) * (val(hdnTDSPercent.value)/100));
  hdnSurChargeAmount.value= val(val(hdnTDSAmount.value) * val(hdnSurchargePercent.value)/100);
  hdnAdditionalSurchargeCessAmount.value= val((val(hdnTDSAmount.value) + val(hdnSurChargeAmount.value))* val(hdnAdditionalSurchargeCessPercent.value)/100);
  hdnAddistionalEducationCessAmount.value= val((val(hdnTDSAmount.value) + val( hdnSurChargeAmount.value))* val(hdnAddistionalEducationCessPercent.value)/100);
  hdnTotalTDSAmount.value = val(val(hdnTDSAmount.value) + val(hdnSurChargeAmount.value) + val(hdnAdditionalSurchargeCessAmount.value) + val(hdnAddistionalEducationCessAmount.value));

  lblTDSAmount.innerHTML = val(hdnTDSAmount.value);
  lblSurChargeAmount.innerHTML = val(hdnSurChargeAmount.value);
  lblAdditionalSurchargeCessAmount.innerHTML = val(hdnAdditionalSurchargeCessAmount.value);
  lblAddistionalEducationCessAmount.innerHTML = val(hdnAddistionalEducationCessAmount.value);
  lblTotalTDSAmount.innerHTML = val(hdnTotalTDSAmount.value);

  lblTruckHirePayable.innerHTML = val(val(txtHireAmount.value) - val(hdnTotalTDSAmount.value));
  hdnTruckHirePayable.value = lblTruckHirePayable.innerHTML;

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
}

function Add_Driver_Window_New()
{
var hdn_Driver_path = document.getElementById('hdnDriverpath');
var DriverName = document.getElementById('DDLDriver_txtBoxDDLDriver');

var w = screen.availWidth;
var h = screen.availHeight;
var popW = (w-100);
var popH = (h-400);
var leftPos = (w-popW)/2;
var topPos = (h-popH)/2;

if(hdn_Driver_path.value == '')
  {
  alert('You Don"t Have Rights to Add Driver');
  }
else
  {
  window.open(hdn_Driver_path.value + '&DriverName=' + DriverName.value, 'DVLPDriver', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
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

var Search_Type;
var lst_control_id;
function Search_Branch(e,txtbox,lstBox,SearchType,length)
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
                Raj.EF.CallBackFunction.CallBack.GetTxtSearchBranch(txtvalue,handleResults);
            }
        }
    }
    if (keyCode == 38 || keyCode == 40)
        listboxupdown(txtbox,lstBox);
}

function Search_Driver(e,txtbox,lstBox,SearchType,length)
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
                Raj.EF.CallBackFunction.CallBack.GetTxtSearchDriver(txtvalue,handleResults);
            }
        }
    }
    if (keyCode == 38 || keyCode == 40)
        listboxupdown(txtbox,lstBox);
}

function Search_Cleaner(e,txtbox,lstBox,SearchType,length)
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
                Raj.EF.CallBackFunction.CallBack.GetTxtSearchCleaner(txtvalue,handleResults);
            }
        }
    }
    if (keyCode == 38 || keyCode == 40)
        listboxupdown(txtbox,lstBox);
}

function Search_Broker(e,txtbox,lstBox,length)
{
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
                Raj.EF.CallBackFunction.CallBack.GetTxtBrokerForDVLP(txtvalue,handleResults);
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


function On_BranchLostFocus(txtbox,list_control,hdn_control)
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
 
     if(Search_Type == 'FromBranch')
     {
        update_ATDBTH();
     }
}

function On_DriverLostFocus(txtbox,list_control,hdn_control,SearchType)
{
    Search_Type = SearchType;
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
      
     if(Search_Type == 'Driver')
     {
        Raj.EF.CallBackFunction.CallBack.GetTxtDriverInfo(list_control_value,handleDriverInfo);
     }
     if(Search_Type == 'Cleaner')
     {
        Raj.EF.CallBackFunction.CallBack.GetTxtDriverInfo(list_control_value,handleDriverInfo);
     }
   }
}

function handleDriverInfo(results)
{  
    var mob1 = results.value.Rows[0]["Mobile_No_1"];
    var mob2 = results.value.Rows[0]["Mobile_No_2"];
    
     if(Search_Type == 'Driver')
     {
        document.getElementById("txt_MobileNo1").value = mob1;
        document.getElementById("txt_MobileNo2").value = mob2;
     }     
     if(Search_Type == 'Cleaner')
     {
        document.getElementById("txt_MobileNo1CL").value = mob1;
        document.getElementById("txt_MobileNo2CL").value = mob2;
     }
}