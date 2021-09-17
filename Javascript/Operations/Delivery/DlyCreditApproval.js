// JScript File
var Consignor_old = ''
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
 
function NewGC_AllSearch(e,txtbox,lstBox,Search_Type,length)
{
    this.keyCode = e.keyCode;
    LoginMainId = '0';
    var defaults = '0';
    if (txtbox.value == '')
    {
        Clear_listbox(lstBox);
        hidecontrol(document.getElementById(lstBox));
        return;
    }

//    var ddl_BillHry = document.getElementById('ddl_BillingHierarchy');
    var txtvalue = txtbox.value.toUpperCase();
    var SearchByCode = false;
        
    if (Search_Type == 'BillingLocation')
         defaults = 'BO';
        
    if(txtbox.value.length >= length)
    {
        if (keyCode != 37 && keyCode != 38 && keyCode != 39 && keyCode != 40)
        {
            if(Search_Type == 'BillingParty' && old_billing_party != txtvalue ||
                Search_Type == 'BillingLocation' && old_billing_location != txtvalue)
            {
            
               
                Raj.EC.OperationModel.NewGCSearch.Get_Search(txtbox.value,SearchByCode,SearchFor,val(LoginMainId),defaults,handleResults);
            }
        }
    }
    if (keyCode == 38 || keyCode == 40)
        listboxupdown(txtbox,lstBox);
}
 
//*******************************************************************
 
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

function handleResults(results)
{
   if (Search_Type == 'BillingParty')
  {var list_control = document.getElementById('lst_BillParty');}
  else if (Search_Type == 'BillingLocation')
  {var list_control = document.getElementById('lst_BillLocation');} 
 
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
 //*******************************************************************
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
function On_Focus(txtbox,lstBox,Search_Type)
{
     this.Search_Type = Search_Type;
    
    initControl1(txtbox,lstBox);
    txtcontrol = document.getElementById(txtbox);
    
    if(Search_Type == 'BillingParty')
    {
        SearchFor = 'BillingParty';
        old_billing_party = txtcontrol.value.toUpperCase();
    }   
    
    var lstcontrol = document.getElementById(lstBox);
    if (txtcontrol.value != '') showcontrol(lstcontrol);  
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
//*******************************************************************

function Billing_LostFocus(txtbox,list_control)
{
 
    var txtbox_value = txtbox.value.toUpperCase();
    var list_control = document.getElementById(list_control); 
    var list_control_index = list_control.selectedIndex;
    var list_control_value ;

    hidecontrol(list_control);

    if (list_control_index != -1)
       list_control_value =  list_control.options[list_control_index].value;
    else
       list_control_value = '0';
    
    if(Search_Type == 'BillingParty')
    {
        if (old_billing_party != txtbox_value) 
            Raj.EC.OperationModel.NewGCSearch.Get_AllDetails(0,list_control_value,SearchFor,handleBillingParty);
    }
}

//*****************************************************************************************

function handleBillingParty(Results)
{
    var txt_BillingParty  = document.getElementById('txt_BillingParty');
    var hdn_BillingPartyId  = document.getElementById('hdn_BillingPartyId');
 
    var Rows = Results.value.Rows[0];

     if(Results.value.Rows.length > 0)
     {
        if (old_billing_party != txt_BillingParty.value.toUpperCase())
	    {
	       old_billing_party = txt_BillingParty.value.toUpperCase();
	       hdn_BillingPartyId.value = Rows['Client_ID'];
        }
     }
     else
     {
       hdn_BillingPartyId.value  = '0';
     }
 }
 
 function Allow_To_Save()
 { 
    var ATS = false;
    var Rbl_Receivedby = document.getElementById("Rbl_Receivedby_0");
    var txt_BillingParty = document.getElementById("txt_BillingParty");
    var hdn_BillingPartyId = document.getElementById("hdn_BillingPartyId");
    var txtMobilePayment = document.getElementById("txtMobilePayment");
    var txtReason = document.getElementById("txtReason");
    var chkApproved = document.getElementById("chkApproved");
    var lbl_Errors = document.getElementById("lbl_Errors");

  if (chkApproved.checked == false && txtReason.value == '')
  {
     lbl_Errors.innerHTML = 'Please Enter Reason For Non Approval.';
     txtReason.focus();
  }
  else if ((chkApproved.checked == true && Rbl_Receivedby.checked) && hdn_BillingPartyId.value <= 0)
  {
    lbl_Errors.innerHTML = 'Please Select Billing Party.';
    txt_BillingParty.focus();
  } 
  else if ((chkApproved.checked == true && Rbl_Receivedby.checked == false) && txtMobilePayment.value == '')
  {
    lbl_Errors.innerHTML = 'Please Enter MPayment Transaction ID.';
    txtMobilePayment.focus();
  }
  else 
    ATS = true;

    return ATS;
}    