// JScript File 
var Consignee_old = '' 
var old_billing_party = ''
var old_billing_location = ''
var old_contract_client = ''
var IsNewConsignee = false;
var Search_Type;
var SearchFor;
var keyCode;
var LoginMainId = 0;
var Consignor_Details = new Array;
var Consignee_Details = new Array;
var Agency_Details = new Array;
var ContractFreightDetails = new Array;
var IsFromPageLoad = false;
var commodityDetailsApproxWeight;
var commodityDetailsRate;
var commodityDetailsAmount;
var commodityDetailsToBe;
var ispopupclientwindow = false;
var TempAOCPercent = 0;

function NewGC_AllSearch(e,txtbox,lstBox,Search_Type,length)
{

if (ispopupclientwindow == false)
    this.keyCode = e.keyCode;
    
    if (txtbox.value == '')
    {
        Clear_listbox(lstBox);
        hidecontrol(document.getElementById(lstBox));
        return;
    } 
    var chk_ConsnSearchByCode = document.getElementById('chk_ConsigneeSearchByCode');
    var hdn_ToLocationId = document.getElementById('hdn_ToLocationId');
    var ToLocationId = document.getElementById('hdn_ToLocationId').value;
   
  
   
    var txtvalue = txtbox.value.toUpperCase();
    
    var SearchByCode = false;
    var defaults = '0';
     
    LoginMainId = '0';
          
    if(txtbox.value.length >= length)
    {
        if (keyCode != 37 && keyCode != 38 && keyCode != 39 && keyCode != 40)
        {
            if(Search_Type == 'Consignee' && (Consignee_old != txtvalue || ispopupclientwindow == true))
            {
              if(Search_Type == 'Consignee')
              {
                Raj.EC.OperationModel.NewGCSearch.Get_Search(txtbox.value,SearchByCode,SearchFor,ToLocationId,defaults,handleResults);                
             
              } 
 
            }
        }
    }
    if (keyCode == 38 || keyCode == 40)
        listboxupdown(txtbox,lstBox);
}


function on_keydown(e,txtbox,lstBox)
{
    this.keyCode = e.keyCode;
    var lstBox = document.getElementById(lstBox);
    var ret = true;
    
    if (keyCode == 9)
    {
        if(lstBox.selectedIndex > -1)
        txtbox.value = lstBox.options[lstBox.selectedIndex].text;
    } 
    else if (keyCode == 13)
        ret = false;
        
    return ret;
}

//Start added on 30-12-13
function hotKey(e,txtbox,Search_Type)
{
    var ret = true;
    if (keyCode == 113)
    {
        if (txtbox.value.length > 1)
        {
              if (Search_Type == 'Consignee')
              {
                New_Consignor_Consignee(0,0);
              }
        }  
      txtbox.focus();
      
    }
    return ret;
}
//Start added on 30-12-13

function handleResults(results)
{ 
  if (Search_Type == 'Consignee')
  {var list_control = document.getElementById('lst_Consignees');}
   
 
  var tot = results.value.Rows.length -1;
  var count = 0;
  
  for (var count = list_control.options.length-1; count >-1; count--)
  {
    list_control.options[count] = null;
  }

  for (count = 0;count <= tot;count ++)
  {  
    if(Search_Type == 'Consignee')
    { 
        Consignee_Details[count] = new Array(3);
        for (var count1 = 0; count1 <= 3; count1++) 
        { 
          Consignee_Details[count][count1] = results.value.Rows[count][results.value.Columns[count1].Name]; 
        }
    }
    
    list_control.options[count] = new Option(results.value.Rows[count][results.value.Columns[0].Name],results.value.Rows[count][results.value.Columns[1].Name]); 
  }

if (list_control.options.length == 0)
  hidecontrol(list_control);
else
  showcontrol(list_control);
}
//*******************************************************************

function On_Focus(txtbox,lstBox,Search_Type)
{
    this.Search_Type = Search_Type;
    
    initControl1(txtbox,lstBox);
    txtcontrol = document.getElementById(txtbox);
    
    if(Search_Type == 'Consignee')
    {
        initControl2(txtbox,lstBox);
        SearchFor = 'Client';
        Consignee_old = txtcontrol.value.toUpperCase();
    }
    
    var lstcontrol = document.getElementById(lstBox);
    if (txtcontrol.value != '') showcontrol(lstcontrol);  
}

//*******************************************************************

function Client_LostFocus(txtbox,list_control)
{  
   
   var hdn_IsRegularConsignee = document.getElementById('hdn_IsRegularConsignee'); 

    var txtbox_value = txtbox.value.toUpperCase();
    var list_control = document.getElementById(list_control); 
    var list_control_index = list_control.selectedIndex;
    var list_control_value;
 
    hidecontrol(list_control);

    if (list_control_index != -1)
        list_control_value = list_control.options[list_control_index].value;
    else
        list_control_value = '0';

    if(Search_Type == 'Consignee' )
    {
        if (Consignee_old != txtbox_value || ispopupclientwindow == true)
        {
            SetIsRegularClient(list_control_index);
            SetClientName(list_control_index);
            Consignee_old = txtbox_value;
            ispopupclientwindow = false;
            Raj.EC.OperationModel.NewGCSearch.Get_AllDetails(val(hdn_IsRegularConsignee.value),list_control_value,SearchFor,handleClientsDetails);
 
        }
    }
}
//*******************************************************************

function SetClientName(arrindex)
{
 
  var txt_ConsigneeName = document.getElementById('txt_ConsigneeName');

   if (Search_Type == 'Consignee')
   {  
        if(arrindex < 0 || Consignee_Details[arrindex][3] != '')
         txt_ConsigneeName.value =  Consignee_Details[arrindex][3];
   }
} 

function SetIsRegularClient(arrindex)
{
  
  var hdn_IsRegularConsignee = document.getElementById('hdn_IsRegularConsignee');

   if (Search_Type == 'Consignee')
   {  
        if(arrindex < 0 || Consignee_Details[arrindex][2] == false)
            hdn_IsRegularConsignee.value = '0';
        else
            hdn_IsRegularConsignee.value = '1';
   }
} 
 
function handleClientsDetails(Results)
{
    
    var txt_ConsigneeName = document.getElementById('txt_ConsigneeName');
    var hdn_ConsigneeId  = document.getElementById('hdn_ConsigneeId');
////////    var hdn_Consignee_CSTTINNo  = document.getElementById('hdn_Consignee_CSTTINNo');
////////    var hdn_ConsigneeDDAddressLine1  = document.getElementById('hdn_ConsigneeDDAddressLine1');
     var txt_Address1  = document.getElementById('txt_Address1');
     var txt_Address2  = document.getElementById('txt_Address2');
     var txt_Pincode  = document.getElementById('txt_Pincode');
     var txt_StdCode  = document.getElementById('txt_StdCode');
     var txt_Phone  = document.getElementById('txt_Phone');
     var txt_Mobile  = document.getElementById('txt_Mobile');
//////     var txt_CSTTinNo  = document.getElementById('txt_CSTTinNo');
//////     var txt_ServiceTaxNo  = document.getElementById('txt_ServiceTaxNo');
     
     var vtxt_Address1  = document.getElementById('txt_Address1').value;
     var vtxt_Address2  = document.getElementById('txt_Address2').value;
     var vtxt_Pincode  = document.getElementById('txt_Pincode').value;
     var vtxt_StdCode  = document.getElementById('txt_StdCode').value;
     var vtxt_Phone  = document.getElementById('txt_Phone').value;
     var vtxt_Mobile  = document.getElementById('txt_Mobile').value;
////////     var vtxt_CSTTinNo  = document.getElementById('txt_CSTTinNo').value;
////////     var vtxt_ServiceTaxNo  = document.getElementById('txt_ServiceTaxNo').value;
////////    var hdn_ConsigneeDDAddressLine2  = document.getElementById('hdn_ConsigneeDDAddressLine2');
////////    var lbl_ConsigneeDetailsValue   = document.getElementById('lbl_ConsigneeDetailsValue');
////////    var txt_ConsigneeDetailsValue   = document.getElementById('txt_ConsigneeDetailsValue');
////////    var hdn_ConsigneeDetailsValue   = document.getElementById('hdn_ConsigneeDetailsValue');
////////    var hdn_ConsigneeDeliveryAreaID   = document.getElementById('hdn_ConsigneeDeliveryAreaID');
////////    var lbl_ConsigneeDeliveryAreaName   = document.getElementById('lbl_ConsigneeDeliveryAreaName');
////////    var hdn_IsServiceTaxApplicableForConsignee = document.getElementById('hdn_IsServiceTaxApplicableForConsignee');
    var hdn_IsRegularConsignee = document.getElementById('hdn_IsRegularConsignee');
////////    var lbl_consignee_Client_Code = document.getElementById('lbl_consignee_Client_Code');
////////    var lbl_ConsigneePhoneNumbers = document.getElementById('lbl_ConsigneePhoneNumbers');
////////    var lbl_ConsigneeMobileNumbers = document.getElementById('lbl_ConsigneeMobileNumbers');

    var Rows = Results.value.Rows[0];

    if (Search_Type == 'Consignee')
    {  
        if(Results.value.Rows.length > 0)
        {
            IsNewConsignee = true;
            hdn_ConsigneeId.value = Rows['Client_ID'];
//////  lbl_ConsigneeDetailsValue.innerHTML = Rows['Add_Details'];
//////  txt_ConsigneeDetailsValue.value = Rows['Add_Details'];
//////  hdn_ConsigneeDetailsValue.value = Rows['Add_Details'];
//////  lbl_consignee_Client_Code.innerHTML = Rows['Client_Code'];
//////  lbl_ConsigneePhoneNumbers.innerHTML = Rows['PhoneNumbers'];
//////  lbl_ConsigneeMobileNumbers.innerHTML = Rows['Mobile_No'];
            
//////             if(Rows['Is_Service_Tax_Applicable'] == false)
//////                hdn_IsServiceTaxApplicableForConsignee.value = '0';
//////            else
//////                hdn_IsServiceTaxApplicableForConsignee.value = '1';

            if(Rows['Is_Regular_Client'] == false)
                hdn_IsRegularConsignee.value = '0';
            else
                hdn_IsRegularConsignee.value = '1';
         
////         txt_Address1.innerHTML = Rows['Address1'];
////         txt_Address2.innerHTML = Rows['Address2'];
////         txt_Pincode.innerHTML = Rows['Pincode'];
////         txt_StdCode.innerHTML = Rows['Std_Code'];
////         txt_Phone.innerHTML = Rows['PhoneNumbers'];
////         txt_Mobile.innerHTML = Rows['Mobile_No'];
//////         txt_CSTTinNo.innerHTML = Rows['CST_TIN_No'];
//////         txt_ServiceTaxNo.innerHTML = Rows['Service_Tax_No'];


         vtxt_Address1 = Rows['Address1'];
         vtxt_Address2 = Rows['Address2'];
         vtxt_Pincode = Rows['Pin_Code'];
         vtxt_StdCode = Rows['Std_Code'];
         vtxt_Phone = Rows['PhoneNumbers'];
         vtxt_Mobile = Rows['Mobile_No'];
//////         vtxt_CSTTinNo.innerHTML = Rows['CST_TIN_No'];
//////         vtxt_ServiceTaxNo.innerHTML = Rows['Service_Tax_No'];

        txt_Address1.value = vtxt_Address1;
        txt_Address2.value = vtxt_Address2;
        txt_Pincode.value = vtxt_Pincode;
  
        txt_StdCode.value = vtxt_StdCode;
        txt_Phone.value = vtxt_Phone;
        txt_Mobile.value = vtxt_Mobile;
        
//////  hdn_ConsigneeDDAddressLine1.value = Rows['Address1'];
//////  hdn_ConsigneeDDAddressLine2.value = Rows['Address2'];
//////  hdn_Consignee_CSTTINNo.value = Rows['CST_TIN_No'];
//////  
//////  hdn_ConsigneeDeliveryAreaID.value = Rows['DeliveryAreaID']; 
//////  lbl_ConsigneeDeliveryAreaName.innerHTML = Rows['DeliveryAreaName']; 
//////            GetEncreptedConseeId();
        }
        else
        {
            hdn_ConsigneeId.value = '0';
////////            lbl_ConsigneeDetailsValue.innerHTML = '';
////////            txt_ConsigneeDetailsValue.value = '';
////////            hdn_ConsigneeDetailsValue.value = '';
////////            lbl_consignee_Client_Code.innerHTML = '';
////////            hdn_IsServiceTaxApplicableForConsignee.value = '0';
            hdn_IsRegularConsignee.value = '0';
////////            hdn_ConsigneeDDAddressLine1.value = '';
////////            hdn_ConsigneeDDAddressLine2.value = '';
        }
         
    }
       
 } 
 
//***********************************************************************************************
function SetClientDetails(Client_Id,Is_Consignor)
{
    if(Is_Consignor == 1)
        Search_Type = 'Consignor';
    else
        Search_Type = 'Consignee';

    Raj.EC.OperationModel.NewGCSearch.Get_AllDetails(1,Client_Id,'Client',handleClientsDetails);
}
 
///------------------------------ Save  ------------------------------//

function Allow_To_Save()
{
 
    var ATS = false;
    
    var hdn_ConsigneeId =  document.getElementById('hdn_ConsigneeId'); 
    var txt_ConsigneeName =  document.getElementById('txt_ConsigneeName');
   
    if (val(hdn_ConsigneeId.value) <= 0)
    {
        SetErrorMsg("Please Select Consignee.");
        txt_ConsigneeName.focus();
    }  
    else
    {
        ATS = true;
    }
    return ATS;
}

//----------------------------------- On Page Load ---------------------------------------------------------//
 
 
function GetEncreptedConseeId()
{
    document.getElementById('btn_ConseeId').click();
}
//*******************************************************************
function listboxonfocus(txt)
{
  document.getElementById(txt).focus();
}
////*******************************************************************
function txtbox_onfocus(txtbox)
{
    txtbox.style.backgroundColor = "yellow";
    txtbox.select();
}
////*******************************************************************
function txtbox_onlostfocus(txtbox)
{
    txtbox.value = txtbox.value.toUpperCase();
    txtbox.style.backgroundColor = "white";
}
//*******************************************************************

function listboxupdown(txt_Control,lst_Control)
{
  var lst_Control = document.getElementById(lst_Control);

  if(keyCode == 38)
  {
    if(lst_Control.selectedIndex == 0 || lst_Control.selectedIndex == -1)
        lst_Control.selectedIndex = lst_Control.options.length -1;
    else
        lst_Control.selectedIndex = lst_Control.selectedIndex - 1;
  }
  else if(keyCode == 40)
  {
      if(lst_Control.selectedIndex == (lst_Control.options.length - 1))
        lst_Control.selectedIndex = 0;
      else
        lst_Control.selectedIndex = lst_Control.selectedIndex + 1;
  } 
  txt_Control.value =  lst_Control.options[lst_Control.selectedIndex].text;
}
//*******************************************************************************

function initControl1(txtControl,lstControl)
{
    var txtCnt = document.getElementById(txtControl);
    var ItemPos = getElementPosition(txtCnt);
    var x = ItemPos.x;
    var y = ItemPos.y;
    var lstCnt = document.getElementById(lstControl);

    lstCnt.style.left =x + 'px';
    lstCnt.style.top =y+17 + 'px';

    lstCnt.style.visibility  = 'hidden';
}
//*******************************************************************************

function initControl2(txtControl,lstControl)
{ 
    var txtCnt = document.getElementById(txtControl);
    var ItemPos = getElementPosition(txtCnt);
    var x = ItemPos.x;
    var y = ItemPos.y;
    var lstCnt = document.getElementById(lstControl);

    lstCnt.style.width = 550 + 'px';
    lstCnt.style.left = 350 + 'px';
    lstCnt.style.top =y+17 + 'px';

    lstCnt.style.visibility  = 'hidden';
}
//********************************************************************************

function Clear_listbox(lstbox)
{
  var selectObject = document.getElementById(lstbox);
  for(var count = selectObject.options.length - 1; count >- 1; count--)
  {
	  selectObject.options[count] = null;
  }
}
function showcontrol(list_control)
{
    list_control.style.visibility= 'visible';
    if(list_control.selectedIndex == -1)
        list_control.selectedIndex = 0;
        
}
function hidecontrol(list_control)
{
    list_control.style.visibility = 'hidden'; 
}

 
 
 
//***************************** On Save Button Click **************************************************
function SetErrorMsg(msg)
{
    document.getElementById('lbl_Errors').innerHTML = msg;
    document.getElementById('lbl_Errors1').innerHTML = msg;
}

 
function Allow_To_Exit()
{
    var ATE = false;

    if (confirm("Do you want to Exit...")==false)
        ATE=false;
    else
    {
        window.close();
        ATE=true;
    }
    return ATE;
}

  

// --------------------------------------------------------
function UpdateFromRegularClient(clientname)
{
    ispopupclientwindow = true;

    if(Search_Type == 'Consignee')
    {
        var txt_ConsigneeName = document.getElementById('txt_ConsigneeName');
        var lst_Consignees = document.getElementById('lst_Consignees');

        txt_ConsigneeName.focus();
        NewGC_AllSearch(event,txt_ConsigneeName,lst_Consignees,Search_Type,2,5);
    }
}  


function On_PageUnLoad()
{
 initControl('txt_ConsigneeName','lst_Consignees');
}
 
 
 function updateconsigneegrid()
 { 
  window.opener.update_consigneegrid();
 } 