// JScript File
var Consignor_old = ''
var Consignee_old = ''
var old_from_Location = ''
var old_to_Location = ''
var old_book_brch = ''
var old_arr_from_brch = ''
var old_agency_brch = ''
var old_agency_ledger = ''
var old_billing_party = ''
var old_Wholeseler = ''
var old_billing_location = ''
var old_contract_client = ''
var old_packingtype = ''
var old_item = ''
var old_size = ''
var lstBox = ''
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
var chk_Is_Service_Tax_App_For_Commodity;
var hdfn_ItemRatePerKg;
var ispopupclientwindow = false;
var TempAOCPercent = 0;
var commodityDetailsBharaiAmt;

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
    var chk_ConsrSearchByCode = document.getElementById('WucGCNew1_chk_ConsignorSearchByCode');
    var chk_ConsnSearchByCode = document.getElementById('WucGCNew1_chk_ConsigneeSearchByCode');
    var hdn_BookingBranchId = document.getElementById('WucGCNew1_hdn_BookingBranchId');
    var ddl_BillHry = document.getElementById('WucGCNew1_ddl_BillingHierarchy');
    var MenuItemId = document.getElementById('WucGCNew1_hdn_MenuItemId').value;

//Start added on 11-12-13
    var FromLocationId = document.getElementById('WucGCNew1_hdn_FromLocationId').value;
    var ToLocationId = document.getElementById('WucGCNew1_hdn_ToLocationId').value;
 
//End added on 11-12-13
    var txtvalue = txtbox.value.toUpperCase();
    
    var SearchByCode = false;
    var defaults = '0';
    if (Search_Type == 'Consignor')
    {SearchByCode = chk_ConsrSearchByCode.checked;}
    else if(Search_Type == 'Consignee')
    {SearchByCode = chk_ConsnSearchByCode.checked;}

    if (val(MenuItemId) != 229 && Search_Type == 'FromLoc')
       LoginMainId = hdn_BookingBranchId.value;
    else
       LoginMainId = '0';
        
    if (Search_Type == 'BillingLocation')
         defaults = ddl_BillHry.value;
          
    if(txtbox.value.length >= length)
    {
        if (keyCode != 37 && keyCode != 38 && keyCode != 39 && keyCode != 40)
        {
            if(Search_Type == 'Consignor' && (Consignor_old != txtvalue || ispopupclientwindow == true) ||
                Search_Type == 'Consignee' && (Consignee_old != txtvalue || ispopupclientwindow == true)||
                Search_Type == 'FromLoc' && old_from_Location != txtvalue ||
                Search_Type == 'ToLoc' && old_to_Location != txtvalue ||
                Search_Type == 'BookingBranch' && old_book_brch != txtvalue ||
                Search_Type == 'ArrivedFromBranch' && old_arr_from_brch != txtvalue ||
                Search_Type == 'Agency' && old_agency_brch != txtvalue ||
                Search_Type == 'Ledger' && old_agency_ledger != txtvalue ||
                Search_Type == 'BillingParty' && old_billing_party != txtvalue ||
                Search_Type == 'BillingLocation' && old_billing_location != txtvalue ||
                Search_Type == 'Wholeseler' && old_Wholeseler != txtvalue ||
                Search_Type == 'ContractClient' && old_contract_client != txtvalue)
            {
 
              if (Search_Type == 'Consignor')
              {
                Raj.EC.OperationModel.NewGCSearch.Get_Search(txtbox.value,SearchByCode,SearchFor,val(FromLocationId),defaults,handleResults);
              }
              else if(Search_Type == 'Consignee')
              {
                Raj.EC.OperationModel.NewGCSearch.Get_Search(txtbox.value,SearchByCode,SearchFor,val(ToLocationId),defaults,handleResults);                
              }
               else if(Search_Type == 'Wholeseler')
              {
                Raj.EC.OperationModel.NewGCSearch.Get_Search(txtbox.value,SearchByCode,SearchFor,val(hdn_BookingBranchId.value),defaults,handleResults);                
              }   
              else 
              {
                Raj.EC.OperationModel.NewGCSearch.Get_Search(txtbox.value,SearchByCode,SearchFor,val(LoginMainId),defaults,handleResults);
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

function on_PackingItemSizekeydown(e,txtbox,lstBox)
{
    this.keyCode = e.keyCode;
    var lstBox = document.getElementById(lstBox);
    var txtbox = document.getElementById(txtbox);
    var ret = true;
    
    if (keyCode == 9)
    {
        if(lstBox.selectedIndex > -1){
        txtbox.value = lstBox.options[lstBox.selectedIndex].text;
        lstBox.value = lstBox.options[lstBox.selectedIndex].value;
        }
    } 
    else if (keyCode == 13)
        ret = false;
        
    return ret;
}

function On_PackingLostFocus(txtbox,list_control,hdn_control)
{
    var txtbox_value = document.getElementById(txtbox).value.toUpperCase();
    var list_control = document.getElementById(list_control); 
    var list_control_index = list_control.selectedIndex;
    var list_control_value;
    var list_control_text;
    hidecontrol(list_control);

    if (list_control_index != -1){
        list_control_value = list_control.options[list_control_index].value;
        list_control_text = list_control.options[list_control_index].text;
        }
    else{
        list_control_value = '0';
        list_control_text = '';
        }
     document.getElementById(hdn_control).value = list_control_value;
     document.getElementById(txtbox).value = list_control_text;
}

function On_ItemLostFocus(txtPackage,ddlItemID,ddlSizeID,txtApproxWeight,txtRate,txtAmount,hdfn_ToBe,chk_Is_Service_Tax_App_For_Commodity,hdfn_ItemRatePerKg,hdnBkgTypeID,txtbox,list_control,hdfn_BharaiAmt)
{
    var txtbox_value = document.getElementById(txtbox).value.toUpperCase();
    var list_control = document.getElementById(list_control); 
    var list_control_index = list_control.selectedIndex;
    var list_control_value;var list_control_text;
    hidecontrol(list_control);

    if (list_control_index != -1){
        list_control_value = list_control.options[list_control_index].value;
        list_control_text = list_control.options[list_control_index].text;
        }
    else{
        list_control_value = '0';
        list_control_text = '';
        }
        
     document.getElementById(ddlItemID).value = list_control_value;
     document.getElementById(txtbox).value = list_control_text;
     
     CalculateApproxWeightAndAmount(txtPackage,ddlItemID,ddlSizeID,txtApproxWeight,txtRate,txtAmount,hdfn_ToBe,chk_Is_Service_Tax_App_For_Commodity,hdfn_ItemRatePerKg,hdnBkgTypeID,hdfn_BharaiAmt);
}

function On_SizeLostFocus(txtPackage,ddlItemID,ddlSizeID,txtApproxWeight,txtRate,txtAmount,hdfn_ToBe,chk_Is_Service_Tax_App_For_Commodity,hdfn_ItemRatePerKg,hdnBkgTypeID,txtbox,list_control,hdfn_BharaiAmt)
{
    var txtbox_value = document.getElementById(txtbox).value.toUpperCase();
    var list_control = document.getElementById(list_control); 
    var list_control_index = list_control.selectedIndex;
    var list_control_value;var list_control_text;
    hidecontrol(list_control);

    if (list_control_index != -1){
        list_control_value = list_control.options[list_control_index].value;
        list_control_text = list_control.options[list_control_index].text;
        }
    else{
        list_control_value = '0';
        list_control_text = '';
        }
        
     document.getElementById(ddlSizeID).value = list_control_value;
     document.getElementById(txtbox).value = list_control_text;
     
     CalculateApproxWeightAndAmount(txtPackage,ddlItemID,ddlSizeID,txtApproxWeight,txtRate,txtAmount,hdfn_ToBe,chk_Is_Service_Tax_App_For_Commodity,hdfn_ItemRatePerKg,hdnBkgTypeID,hdfn_BharaiAmt);
}
//Start added on 30-12-13
function hotKey(e,txtbox,Search_Type)
{
    var ret = true;
    if (keyCode == 113)
    {
        if (txtbox.value.length > 1)
        {
              if (Search_Type == 'Consignor')
              {
                New_Consignor_Consignee(0,1);
              }
               else if (Search_Type == 'Consignee')
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
  if (Search_Type == 'Consignor')
  {var list_control = document.getElementById('WucGCNew1_lst_Consignors');}
  else if (Search_Type == 'Consignee')
  {var list_control = document.getElementById('WucGCNew1_lst_Consignees');}
  else if (Search_Type == 'FromLoc')
  {var list_control = document.getElementById('WucGCNew1_lst_FromLoc');}
  else if (Search_Type == 'ToLoc')
  {var list_control = document.getElementById('WucGCNew1_lst_ToLoc');}
  else if (Search_Type == 'BookingBranch')
  {var list_control = document.getElementById('WucGCNew1_lst_BookBranch');}
  else if (Search_Type == 'ArrivedFromBranch')
  {var list_control = document.getElementById('WucGCNew1_lst_ArrivedFromBranch');}
  else if (Search_Type == 'Agency')
  {var list_control = document.getElementById('WucGCNew1_lst_Agency');}
  else if (Search_Type == 'Ledger')
  {var list_control = document.getElementById('WucGCNew1_lst_Ledger');}
  else if (Search_Type == 'BillingParty')
  {var list_control = document.getElementById('WucGCNew1_lst_BillParty');}
  else if (Search_Type == 'BillingLocation')
  {var list_control = document.getElementById('WucGCNew1_lst_BillLocation');}
  else if (Search_Type == 'ContractClient')
  {var list_control = document.getElementById('WucGCNew1_lst_ContractClient');}
  else if (Search_Type == 'Wholeseler')
  {var list_control = document.getElementById('WucGCNew1_lst_Wholeseler');}
 
  var tot = results.value.Rows.length -1;
  var count = 0;
  
  for (var count = list_control.options.length-1; count >-1; count--)
  {
    list_control.options[count] = null;
  }

  for (count = 0;count <= tot;count ++)
  {  
    if(Search_Type == 'Consignor')
    {
//        Consignor_Details[count] = new Array(2);
        Consignor_Details[count] = new Array(3);
        for (var count1 = 0; count1 <= 3; count1++) 
        { 
          Consignor_Details[count][count1] = results.value.Rows[count][results.value.Columns[count1].Name]; 
        }
    }
    if(Search_Type == 'Consignee')
    {
//        Consignee_Details[count] = new Array(2);
        Consignee_Details[count] = new Array(3);
        for (var count1 = 0; count1 <= 3; count1++) 
        { 
          Consignee_Details[count][count1] = results.value.Rows[count][results.value.Columns[count1].Name]; 
        }
    }
    if(SearchFor == 'Agency')
    {
        Agency_Details[count] = new Array(3);
        for (var count1 = 0; count1 <= 3; count1++) 
        { 
          Agency_Details[count][count1] = results.value.Rows[count][results.value.Columns[count1].Name]; 
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
    
    if(Search_Type == 'Consignor')
    {
        SearchFor = 'Client';
        Consignor_old = txtcontrol.value.toUpperCase();
    }
    else if(Search_Type == 'Consignee')
    {
        initControl2(txtbox,lstBox);
        SearchFor = 'Client';
        Consignee_old = txtcontrol.value.toUpperCase();
    }
    else if(Search_Type == 'FromLoc')
    {
        SearchFor = 'Location';
        old_from_Location = txtcontrol.value.toUpperCase();
    }
    else if(Search_Type == 'ToLoc')
    {
        SearchFor = 'Location';
        old_to_Location = txtcontrol.value.toUpperCase();
    }
    else if(Search_Type == 'BookingBranch')
    {
        SearchFor = 'Branch';
        old_book_brch = txtcontrol.value.toUpperCase();
    }
    else if(Search_Type == 'ArrivedFromBranch')
    {
        SearchFor = 'Branch';
        old_arr_from_brch = txtcontrol.value.toUpperCase();
    }
    else if(Search_Type == 'Agency')
    {
        SearchFor = 'Agency';
        old_agency_brch = txtcontrol.value.toUpperCase();
    }
    else if(Search_Type == 'Ledger')
    {
        SearchFor = 'Ledger';
        old_agency_ledger = txtcontrol.value.toUpperCase();
    }
    else if(Search_Type == 'BillingParty')
    {
        SearchFor = 'BillingParty';
        old_billing_party = txtcontrol.value.toUpperCase();
    }
    else if(Search_Type == 'Wholeseler')
    {
        SearchFor = 'Wholeseler';
        old_Wholeseler = txtcontrol.value.toUpperCase();
    }
    else if(Search_Type == 'BillingLocation')
    {
        SearchFor = 'BillingLocation';
        old_billing_location = txtcontrol.value.toUpperCase();
    }
    else if(Search_Type == 'ContractClient')
    {
        SearchFor = 'ContractClient';
        old_contract_client = txtcontrol.value.toUpperCase();
    }
    else if(Search_Type == 'PackingType')
    {
        SearchFor = 'PackingType';
        old_packingtype = txtcontrol.value.toUpperCase();
    }
    else if(Search_Type == 'Item')
    {
        SearchFor = 'Item';
        old_item = txtcontrol.value.toUpperCase();
    }    
    else if(Search_Type == 'Size')
    {
        SearchFor = 'Size';
        old_size = txtcontrol.value.toUpperCase();
    }  
    var lstcontrol = document.getElementById(lstBox);
    //    if (txtcontrol.value != '') showcontrol(lstcontrol);
    
    //modified on 13-08-2016
    if (txtcontrol.value != '' && txtcontrol.readOnly == false) {
        showcontrol(lstcontrol);
    }
}

//*******************************************************************

function OnPackingTypeItemsSize(e,txtbox,lstBox,Search_Type,length)
{
   lstBox = lstBox;
   txtbox = document.getElementById(txtbox);
   this.keyCode = e.keyCode;
    if (txtbox.value == '')
    {
        Clear_listbox(lstBox);
        hidecontrol(document.getElementById(lstBox));
        return;
    }
    
    var fromBranchID = window.parent.GetBkgBranchIDForCommodity();
    
   
    var txtvalue = txtbox.value.toUpperCase();
    
    if(txtbox.value.length >= length)
    {
        if (keyCode != 37 && keyCode != 38 && keyCode != 39 && keyCode != 40)
        {
            if((Search_Type == 'PackingType' && old_packingtype != txtvalue) || (Search_Type == 'Item' && old_item != txtvalue)  || (Search_Type == 'Size' && old_size != txtvalue))
            { 
              if(Search_Type == 'PackingType' || Search_Type == 'Item' || Search_Type == 'Size')
              {
                Raj.EC.OperationModel.NewGCSearch.Get_Search(txtbox.value,false,Search_Type,val(fromBranchID),'0',function(results){
                  if (Search_Type == 'PackingType')
                   {var list_control = document.getElementById(lstBox);}
                  else if (Search_Type == 'Item')
                   {var list_control = document.getElementById(lstBox);}
                  else if (Search_Type == 'Size')
                   {var list_control = document.getElementById(lstBox);}
                   
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
                });                
              } 
            }
        }
    }
    if (keyCode == 38 || keyCode == 40)
        listboxupdown(txtbox,lstBox);
}

//*******************************************************************

function Client_LostFocus(txtbox,list_control)
{  
   var hdn_IsRegularConsignor = document.getElementById('WucGCNew1_hdn_IsRegularConsignor');
   var hdn_IsRegularConsignee = document.getElementById('WucGCNew1_hdn_IsRegularConsignee'); 
   var GCId = document.getElementById('WucGCNew1_hdn_GCId').value; 
   var TotalArticles = document.getElementById('WucGCNew1_hdn_TotalArticles').value; 
   var hdn_ConsignorId = document.getElementById('WucGCNew1_hdn_ConsignorId');
   var hdn_ConsigneeId  = document.getElementById('WucGCNew1_hdn_ConsigneeId');
   var txt_Consignee = document.getElementById('WucGCNew1_txt_ConsigneeName');
   
    var txtbox_value = txtbox.value.toUpperCase();
    var list_control = document.getElementById(list_control); 
    var list_control_index = list_control.selectedIndex;
    var list_control_value;

    hidecontrol(list_control);

    if (list_control_index != -1)
        list_control_value = list_control.options[list_control_index].value;
    else
        list_control_value = '0';

    if(Search_Type == 'Consignor')
    {
        if (Consignor_old != txtbox_value || ispopupclientwindow == true)
        {
            SetIsRegularClient(list_control_index);
            
//            if (list_control_index == -1)
//            {
//              SetClientName(0);
//            }
//            else
//            {
//              SetClientName(list_control_index);
//            }
            
            SetClientName(list_control_index);
            
            Consignor_old = txtbox_value;
            ispopupclientwindow = false;
            Raj.EC.OperationModel.NewGCSearch.Get_AllDetails(val(hdn_IsRegularConsignor.value),list_control_value,SearchFor,handleClientsDetails);
        }        
    }
    else if(Search_Type == 'Consignee')
    {
        if (Consignee_old != txtbox_value || ispopupclientwindow == true)
        {
            SetIsRegularClient(list_control_index);
            SetClientName(list_control_index);
            Consignee_old = txtbox_value;
            ispopupclientwindow = false;
            Raj.EC.OperationModel.NewGCSearch.Get_AllDetails(val(hdn_IsRegularConsignee.value),list_control_value,SearchFor,handleClientsDetails);
             //Start added on 23-12-13
//             if (hdn_ConsigneeId.value > 0)
//            {
//              document.frames("IframeCommodity1").document.forms("form1").elements("dg_Commodity_ctl02_txt_NewArticles").focus();
//            }
            //end added on 23-12-13
        }
        if(hdn_ConsigneeId.value > 0 && txt_Consignee.value != ''){
            if(GCId > 0 && TotalArticles > 0)
                document.getElementById('WucGCNew1_txt_Invoice_No').focus(); 
            else if(GCId > 0 && TotalArticles <= 0)
                document.frames("IframeCommodity1").document.forms("form1").elements("dg_Commodity_ctl02_txt_NewArticles").focus();
         }
    }
}
//*******************************************************************

function SetClientName(arrindex)
{
  var txt_ConsignorName = document.getElementById('WucGCNew1_txt_ConsignorName');
  var txt_ConsigneeName = document.getElementById('WucGCNew1_txt_ConsigneeName');
  var txt_ToLocation = document.getElementById('WucGCNew1_txt_To_Location');
  var txt_FromLocation = document.getElementById('WucGCNew1_txt_From_Location');
  
  // Commented By Hemant on 23 Oct 2017
//   if (Search_Type == 'Consignor')
//   {
//        if(arrindex < 0 || Consignor_Details[arrindex][3] != '')
//         txt_ConsignorName.value =  Consignor_Details[arrindex][3];
//          
//   }
//   else if (Search_Type == 'Consignee')
//   {  
//        if(arrindex < 0 || Consignee_Details[arrindex][3] != '')
//         txt_ConsigneeName.value =  Consignee_Details[arrindex][3];
  //   }

  if (Search_Type == 'Consignor') 
  {
      if (arrindex >= 0) 
      {
          if (Consignor_Details[arrindex][3] != '')
              txt_ConsignorName.value = Consignor_Details[arrindex][3];
              txt_FromLocation.readOnly = true;
      }
      else
      {
            txt_FromLocation.readOnly = false;
      }
  }
  else if (Search_Type == 'Consignee') 
  {
      if (arrindex >= 0)
      {
          if (Consignee_Details[arrindex][3] != '')
              txt_ConsigneeName.value = Consignee_Details[arrindex][3];
              txt_ToLocation.readOnly = true;
      }
      else 
      {
          txt_ToLocation.readOnly = false;
      }
  }
} 

function SetIsRegularClient(arrindex)
{
  var hdn_IsRegularConsignor = document.getElementById('WucGCNew1_hdn_IsRegularConsignor');
  var hdn_IsRegularConsignee = document.getElementById('WucGCNew1_hdn_IsRegularConsignee');

   if (Search_Type == 'Consignor')
   {
        if(arrindex < 0 || Consignor_Details[arrindex][2] == false)
            hdn_IsRegularConsignor.value = '0';
        else
            hdn_IsRegularConsignor.value = '1';
   }
   else if (Search_Type == 'Consignee')
   {  
        if(arrindex < 0 || Consignee_Details[arrindex][2] == false)
            hdn_IsRegularConsignee.value = '0';
        else
            hdn_IsRegularConsignee.value = '1';
   }
} 
 
function handleClientsDetails(Results)
{
    var txt_ConsignorName = document.getElementById('WucGCNew1_txt_ConsignorName');
    var hdn_ConsignorId = document.getElementById('WucGCNew1_hdn_ConsignorId');
    var lbl_ConsignorDetailsValue = document.getElementById('WucGCNew1_lbl_ConsignorDetailsValue');
    var txt_ConsignorDetailsValue = document.getElementById('WucGCNew1_txt_ConsignorDetailsValue');
    var hdn_ConsignorDetailsValue = document.getElementById('WucGCNew1_hdn_ConsignorDetailsValue');
    var hdn_IsServiceTaxApplicableForConsignor = document.getElementById('WucGCNew1_hdn_IsServiceTaxApplicableForConsignor');
    var hdn_IsRegularConsignor = document.getElementById('WucGCNew1_hdn_IsRegularConsignor');
    var lbl_consignor_Client_Code = document.getElementById('WucGCNew1_lbl_consignor_Client_Code');
    var lbl_ConsignorPhoneNumbers = document.getElementById('WucGCNew1_lbl_ConsignorPhoneNumbers');
    var lbl_ConsignorMobileNumbers = document.getElementById('WucGCNew1_lbl_ConsignorMobileNumbers');

    var txt_ConsigneeName = document.getElementById('WucGCNew1_txt_ConsigneeName');
    var hdn_ConsigneeId  = document.getElementById('WucGCNew1_hdn_ConsigneeId');
    var hdn_Consignee_CSTTINNo  = document.getElementById('WucGCNew1_hdn_Consignee_CSTTINNo');
    var hdn_ConsigneeDDAddressLine1  = document.getElementById('WucGCNew1_hdn_ConsigneeDDAddressLine1');
    var hdn_ConsigneeDDAddressLine2  = document.getElementById('WucGCNew1_hdn_ConsigneeDDAddressLine2');
    var lbl_ConsigneeDetailsValue   = document.getElementById('WucGCNew1_lbl_ConsigneeDetailsValue');
    var txt_ConsigneeDetailsValue   = document.getElementById('WucGCNew1_txt_ConsigneeDetailsValue');
    var hdn_ConsigneeDetailsValue   = document.getElementById('WucGCNew1_hdn_ConsigneeDetailsValue');
    var hdn_ConsigneeDeliveryAreaID   = document.getElementById('WucGCNew1_hdn_ConsigneeDeliveryAreaID');
    var lbl_ConsigneeDeliveryAreaName   = document.getElementById('WucGCNew1_lbl_ConsigneeDeliveryAreaName');
    var hdn_IsServiceTaxApplicableForConsignee = document.getElementById('WucGCNew1_hdn_IsServiceTaxApplicableForConsignee');
    var hdn_IsRegularConsignee = document.getElementById('WucGCNew1_hdn_IsRegularConsignee');
    var lbl_consignee_Client_Code = document.getElementById('WucGCNew1_lbl_consignee_Client_Code');
    var lbl_ConsigneePhoneNumbers = document.getElementById('WucGCNew1_lbl_ConsigneePhoneNumbers');
    var lbl_ConsigneeMobileNumbers = document.getElementById('WucGCNew1_lbl_ConsigneeMobileNumbers');
    var hdn_Consignee_Delivery_Type_Id = document.getElementById('WucGCNew1_hdn_Consignee_Delivery_Type_Id');
    var hdn_Consignee_Is_ToPay_Allowed = document.getElementById('WucGCNew1_hdn_Consignee_Is_ToPay_Allowed');
    var ddl_Dly_Type =  document.getElementById('WucGCNew1_ddl_Dly_Type');
    var hdn_Dly_Type =  document.getElementById('WucGCNew1_hdn_Dly_Type');

    var txt_ToLocation = document.getElementById('WucGCNew1_txt_To_Location');
    var hdn_ConsigneeStateId = document.getElementById('WucGCNew1_hdn_ConsigneeStateId');
    var hdn_ConsignorStateId = document.getElementById('WucGCNew1_hdn_ConsignorStateId');

    var Rows = Results.value.Rows[0];
    if(Search_Type == 'Consignor')
    {
    
        if(Results.value.Rows.length > 0)
        {
            
            hdn_ConsignorId.value  = Rows['Client_ID'];
            lbl_ConsignorDetailsValue.innerHTML = Rows['Add_Details'];
            txt_ConsignorDetailsValue.value = Rows['Add_Details'];
            hdn_ConsignorDetailsValue.value = Rows['Add_Details'];
            lbl_consignor_Client_Code.innerHTML = Rows['Client_Code'];
            lbl_ConsignorPhoneNumbers.innerHTML = Rows['PhoneNumbers'];
            lbl_ConsignorMobileNumbers.innerHTML = Rows['Mobile_No'];
            hdn_ConsignorStateId.value = Rows['State_ID'];
            
            if(Rows['Is_Service_Tax_Applicable'] == false)
                hdn_IsServiceTaxApplicableForConsignor.value = '0';
            else
                hdn_IsServiceTaxApplicableForConsignor.value = '1';

            if(Rows['Is_Regular_Client'] == false)
                hdn_IsRegularConsignor.value = '0';
            else
                hdn_IsRegularConsignor.value = '1';

            GetEncreptedConsrId();
        }
        else
        {
            hdn_ConsignorId.value = '0';
            lbl_ConsignorDetailsValue.innerHTML = '';
            txt_ConsignorDetailsValue.value = '';
            hdn_ConsignorDetailsValue.value = '';
            lbl_consignor_Client_Code.innerHTML = '';
            hdn_IsServiceTaxApplicableForConsignor.value = '0';
            hdn_IsRegularConsignor.value = '0';
            hdn_ConsignorStateId.value = 0;
        }
        
        //added by shiv on 2/11/18
          if(hdn_ConsignorId.value <= 0)
              txt_ConsignorName.focus();
          else
            txt_ConsigneeName.focus();
    }
    else if (Search_Type == 'Consignee')
    {  
        if(Results.value.Rows.length > 0)
        {
            IsNewConsignee = true;
            hdn_ConsigneeId.value = Rows['Client_ID'];
            lbl_ConsigneeDetailsValue.innerHTML = Rows['Add_Details'];
            txt_ConsigneeDetailsValue.value = Rows['Add_Details'];
            hdn_ConsigneeDetailsValue.value = Rows['Add_Details'];
            lbl_consignee_Client_Code.innerHTML = Rows['Client_Code'];
            lbl_ConsigneePhoneNumbers.innerHTML = Rows['PhoneNumbers'];
            lbl_ConsigneeMobileNumbers.innerHTML = Rows['Mobile_No'];
            
            hdn_Consignee_Delivery_Type_Id.value = Rows['Consignee_Delivery_Type_Id'];
            ddl_Dly_Type.value = Rows['Consignee_Delivery_Type_Id'];
            hdn_Dly_Type.value = Rows['Consignee_Delivery_Type_Id']; 
            
            //ddl_Dly_Type.disabled = true;
            //Change From true to false On 04 Sep 2020
            ddl_Dly_Type.disabled = false;
             
            
            hdn_Consignee_Is_ToPay_Allowed.value = Rows['Consignee_Is_To_Pay_Allowed'];
            hdn_ConsigneeStateId.value = Rows['State_ID'];
            
             if(Rows['Is_Service_Tax_Applicable'] == false)
                hdn_IsServiceTaxApplicableForConsignee.value = '0';
            else
                hdn_IsServiceTaxApplicableForConsignee.value = '1';

            if(Rows['Is_Regular_Client'] == false)
                hdn_IsRegularConsignee.value = '0';
            else
                hdn_IsRegularConsignee.value = '1';

            hdn_ConsigneeDDAddressLine1.value = Rows['Address1'];
            hdn_ConsigneeDDAddressLine2.value = Rows['Address2'];
            hdn_Consignee_CSTTINNo.value = Rows['CST_TIN_No'];
            
            hdn_ConsigneeDeliveryAreaID.value = Rows['DeliveryAreaID']; 
            lbl_ConsigneeDeliveryAreaName.innerHTML = Rows['DeliveryAreaName'];
            GetEncreptedConseeId();

            txt_ToLocation.readOnly = true;
        }
        else
        {
            hdn_ConsigneeId.value = '0';
            lbl_ConsigneeDetailsValue.innerHTML = '';
            txt_ConsigneeDetailsValue.value = '';
            hdn_ConsigneeDetailsValue.value = '';
            lbl_consignee_Client_Code.innerHTML = '';
            hdn_IsServiceTaxApplicableForConsignee.value = '0';
            hdn_IsRegularConsignee.value = '0';
            hdn_ConsigneeDDAddressLine1.value = '';
            hdn_ConsigneeDDAddressLine2.value = '';
            hdn_ConsigneeStateId.value = '0';
            txt_ToLocation.readOnly = false;
        }
        
        On_ConsigneeChange();
        
        //added by shiv on 2/11/18
        if(hdn_ConsigneeId.value > 0 && txt_ConsigneeName.value != '')
           document.frames("IframeCommodity1").document.forms("form1").elements("dg_Commodity_ctl02_txt_NewArticles").focus();
        else
          txt_ConsigneeName.focus();
    }
    
      OnPageLoad_ConsignorConsignee();
      Set_ServiceTaxPayableBy();
      

      GetRateContract();
      
      CalculateDiscount(); 
      //commneted by shiv on 2/11/18
 //     if(Search_Type == 'Consignor')
//      {
//          if(hdn_ConsignorId.value <= 0)
//            txt_ConsignorName.focus();
////          else
////            txt_ConsigneeName.focus();
 //     }
 //     if(Search_Type == 'Consignee')
//      {      
//          if(hdn_ConsigneeId.value > 0 && txt_ConsigneeName.value != '')
//             document.frames("IframeCommodity1").document.forms("form1").elements("dg_Commodity_ctl02_txt_NewArticles").focus();
//          else
//             txt_ConsigneeName.focus();
//      }
 } 
 
//*****************************************************************************************

function On_ConsigneeChange()
{
    var Cnsgnee_CSTTINNo = document.getElementById('WucGCNew1_hdn_Consignee_CSTTINNo').value;
    var clientCode = document.getElementById('WucGCNew1_hdn_ClientCode').value;
    var cstno = 0;
    if(Cnsgnee_CSTTINNo != '')
        cstno = 1;

    if(clientCode == 'excel')
        Raj.EC.OperationModel.NewGCSearch.Get_AllDetails(cstno,val(0),'RoadPermitType',handleRoadPermitType);
}

function handleRoadPermitType(Results)
{
    var ddl_Road_Permit = document.getElementById('WucGCNew1_ddl_Road_Permit');
    var RoadPermitTypeId = document.getElementById('WucGCNew1_hdn_RoadPermitTypeId').value;
    var ContractId = document.getElementById('WucGCNew1_hdn_ContractId').value;
    var GCId = document.getElementById('WucGCNew1_hdn_GCId').value;

    var tot = Results.value.Rows.length -1;
    var count = 0;
  
    for (var count = ddl_Road_Permit.options.length-1; count >-1; count--)
    {
        ddl_Road_Permit.options[count] = null;
    }
    for (count = 0;count <= tot;count ++)
    {
        ddl_Road_Permit.options[count] = new Option(Results.value.Rows[count][Results.value.Columns[0].Name],Results.value.Rows[count][Results.value.Columns[1].Name]); 
    }
    if(RoadPermitTypeId > 0)
        ddl_Road_Permit.value = RoadPermitTypeId;
        
    EnableDisable_On_RoadPermit_Change();
    if(GCId <= 0)
        Get_Standard_FreightRate();
    
    if(ContractId > 0)
    {
       Fill_Contract_Change(IsFromPageLoad);
    }
    else
    {
        Calculate_Freight('0');
        Calculate_GrandTotal();
    }
 }
 
//*****************************************************************************************
function Location_LostFocus(txtbox,list_control)
{
    var txtbox_value = txtbox.value.toUpperCase();
    var list_control = document.getElementById(list_control); 
    var list_control_index = list_control.selectedIndex;
    var list_control_value;
    var ContractId = document.getElementById('WucGCNew1_hdn_ContractId').value;
    var hdn_To_Location = document.getElementById('WucGCNew1_hdn_To_Location');
    var txt_ConsignorName = document.getElementById('WucGCNew1_txt_ConsignorName');
    var ddl_Dly_Type = document.getElementById('WucGCNew1_ddl_Dly_Type');
    
    hdn_To_Location.value = txtbox.value;

    hidecontrol(list_control);

    if (list_control_index != -1)
       list_control_value =  list_control.options[list_control_index].value;
    else
       list_control_value = '0';

    if(Search_Type == 'FromLoc')
    {
        if (old_from_Location != txtbox_value)
        {
            old_from_Location = txtbox_value;
            document.getElementById('WucGCNew1_hdn_FromLocationId').value = list_control_value;
            Get_Standard_FreightRate();
            if(ContractId > 0)
                Fill_Contract_Change(IsFromPageLoad);
        }
    }
    else if(Search_Type == 'ToLoc')
    {
        if (old_to_Location != txtbox_value)
        {
            document.getElementById('WucGCNew1_hdn_ToLocationId').value = list_control_value;
            Get_Standard_FreightRate();

            Raj.EC.OperationModel.NewGCSearch.Get_AllDetails(0,list_control_value,SearchFor,handleToLocation);
            //ddl_Dly_Type.focus(); 
        }
    }
    
     GetRateContract();
}

 //************************************************************************
 
function handleToLocation(Results)
{
    var txt_ToLocation = document.getElementById('WucGCNew1_txt_To_Location');
    var hdn_ToLocationId = document.getElementById('WucGCNew1_hdn_ToLocationId');
    var hdn_DeliveryBranchId = document.getElementById('WucGCNew1_hdn_DeliveryBranchId');
    var hdn_DeliveryBranchName = document.getElementById('WucGCNew1_hdn_DeliveryBranchName');
    var lbl_Dly_Branch_Value = document.getElementById('WucGCNew1_lbl_Dly_Branch_Value');
    var ddl_Dly_Type =  document.getElementById('WucGCNew1_ddl_Dly_Type');
    var hdn_Dly_Type =  document.getElementById('WucGCNew1_hdn_Dly_Type');
    var chk_Is_Oct_Appl = document.getElementById('WucGCNew1_chk_Is_Oct_Appl');
    var hdn_Is_Oct_Appl = document.getElementById('WucGCNew1_hdn_Is_Oct_Appl');
    var chk_IsODA = document.getElementById('WucGCNew1_chk_IsODA');    
    var chk_IsToPayBkgApplicable = document.getElementById('WucGCNew1_chk_IsToPayBkgApplicable');
    var hdn_Standard_DDCharge_Rate = document.getElementById('WucGCNew1_hdn_Standard_DDCharge_Rate');
    var hdn_ODAChargesUpTo500Kg = document.getElementById('WucGCNew1_hdn_ODAChargesUpTo500Kg');
    var hdn_ODAChargesAbove500Kg = document.getElementById('WucGCNew1_hdn_ODAChargesAbove500Kg');
    var hdn_Consignee_Dly_Type_Id = document.getElementById('WucGCNew1_hdn_Consignee_Dly_Type_Id');
    var ContractId = document.getElementById('WucGCNew1_hdn_ContractId').value;
    var txt_DDCharge = document.getElementById('WucGCNew1_txt_DDCharge');
    var hdn_DDCharge = document.getElementById('WucGCNew1_hdn_DDCharge');
    
    var isCalculated = 0;
    var Rows = Results.value.Rows[0];
     
     if(Results.value.Rows.length > 0)
     {
        if (old_to_Location != txt_ToLocation.value.toUpperCase())
	    {
	       old_to_Location = txt_ToLocation.value.toUpperCase();
           hdn_ToLocationId.value = Rows['Service_Location_ID'];
           hdn_DeliveryBranchId.value = Rows['del_branch_id'];
           lbl_Dly_Branch_Value.innerHTML = Rows['del_branch_name'];
           hdn_DeliveryBranchName.value = Rows['del_branch_name'];
           chk_Is_Oct_Appl.checked = Rows['Is_Octroi'];
           ddl_Dly_Type.value = Rows['Delivery_Type_ID'];
           hdn_Dly_Type.value = Rows['Delivery_Type_ID'];
           
           if(Rows['Is_Octroi'] == true)
                hdn_Is_Oct_Appl.value = '1';
           else
                hdn_Is_Oct_Appl.value = '0';
                
           chk_IsODA.checked = Rows['Is_ODA'];
           chk_IsToPayBkgApplicable.checked = Rows['Is_To_Pay_Booking'];
           hdn_Standard_DDCharge_Rate.value = Rows['Door_Delivery_Charges'];
           hdn_ODAChargesUpTo500Kg.value = Rows['Oda_charges_upto_500_Kg'];
           hdn_ODAChargesAbove500Kg.value = Rows['Oda_charges_above_500_Kg'];
           if (val(ddl_Dly_Type.value) == 2) //' for door
             txt_DDCharge.disabled = false;
           else{
               txt_DDCharge.disabled = true;
               txt_DDCharge.value = '0';
               hdn_DDCharge.value = '0';
             }
        }
     }
     else
     {
       hdn_ToLocationId.value = '0';
       hdn_DeliveryBranchId.value = '0';
       lbl_Dly_Branch_Value.innerHTML = '';
       hdn_DeliveryBranchName.value = '';
       chk_Is_Oct_Appl.checked = false;
       chk_IsODA.checked = false;
       chk_IsToPayBkgApplicable.checked = false;
       hdn_Standard_DDCharge_Rate.value = '0';
       hdn_ODAChargesUpTo500Kg.value = '0';
       hdn_ODAChargesAbove500Kg.value = '0';
       if (val(ddl_Dly_Type.value) == 2) //' for door
         txt_DDCharge.disabled = false;
       else{
             txt_DDCharge.disabled = true;
             txt_DDCharge.value = '0';
             hdn_DDCharge.value = '0';
           }
     }
     if(ContractId > 0)
     {
        Fill_Contract_Change(IsFromPageLoad);
     }
     else
     {
         Calculate_Freight('0');
         CalculateDiscount();
         isCalculated = 1
     }

     if (isCalculated == 0)
       CalculateDiscount();
 }
//*******************************************************************

function Branch_LostFocus(txtbox,list_control)
{
    var txtbox_value = txtbox.value.toUpperCase();
    var list_control = document.getElementById(list_control); 
    var list_control_index = list_control.selectedIndex;
    var list_control_value;

    hidecontrol(list_control);

    if (list_control_index != -1)
       list_control_value =  list_control.options[list_control_index].value;
    else
       list_control_value = '0';
    
    if(Search_Type == 'BookingBranch')
    {
        if (old_book_brch != txtbox_value) 
        {
            old_book_brch = txtbox_value;
            document.getElementById('WucGCNew1_hdn_BookingBranchId').value = list_control_value;
            document.getElementById('WucGCNew1_txt_From_Location').value = '';
            document.getElementById('WucGCNew1_hdn_FromLocationId').value = '0';
        }
    }
    else if(Search_Type == 'ArrivedFromBranch')
    {
        if(old_arr_from_brch != txtbox_value)
            document.getElementById('WucGCNew1_hdn_ArrivedFromBranchId').value = list_control_value;
    }
    else if(Search_Type == 'Ledger')
    {
        if(old_agency_ledger != txtbox_value)
            document.getElementById('WucGCNew1_hdn_LedgerId').value = list_control_value;
    }
}
//*******************************************************************

function Agency_LostFocus(txtbox,list_control)
{
    var txtbox_value = txtbox.value.toUpperCase();
    var list_control = document.getElementById(list_control); 
    var arrindex = list_control.selectedIndex;

    hidecontrol(list_control);

    if (arrindex >= 0 && old_agency_brch != txtbox_value) 
    {
        document.getElementById('WucGCNew1_hdn_AgencyId').value = Agency_Details[arrindex][1];
        document.getElementById('WucGCNew1_txt_Ledger').value = Agency_Details[arrindex][2];
        document.getElementById('WucGCNew1_hdn_LedgerName').value = Agency_Details[arrindex][2];
        document.getElementById('WucGCNew1_hdn_LedgerId').value  = Agency_Details[arrindex][3];
    }
    else if(arrindex <= 0 && old_agency_brch != txtbox_value)
    {
        document.getElementById('WucGCNew1_hdn_AgencyId').value = '0';
        document.getElementById('WucGCNew1_txt_Ledger').value = '';
        document.getElementById('WucGCNew1_hdn_LedgerName').value = '';
        document.getElementById('WucGCNew1_hdn_LedgerId').value  = '0';
    }
    
    if (Search_Type == 'agency') 
    {
        if (old_agency_brch != txtbox_value)
            old_agency_brch = txtbox_value;
    }  
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
    
    if(Search_Type == 'BillingLocation')
    {
        if (old_billing_location != txtbox_value) 
            document.getElementById('WucGCNew1_hdn_BillingLocationId').value = list_control_value;
    }
    else if(Search_Type == 'BillingParty')
    {
        if (old_billing_party != txtbox_value) 
            Raj.EC.OperationModel.NewGCSearch.Get_AllDetails(0,list_control_value,SearchFor,handleBillingParty);
    }
    
    
    
}
 //************************************************************************

function handleBillingParty(Results)
{
    var txt_BillingParty  = document.getElementById('WucGCNew1_txt_BillingParty');
    var hdn_BillingPartyId  = document.getElementById('WucGCNew1_hdn_BillingPartyId');
    
    var ddl_BillingHierarchy  = document.getElementById('WucGCNew1_ddl_BillingHierarchy');
    var txt_BillingLocation  = document.getElementById('WucGCNew1_txt_BillingLocation');
    var hdn_BillingLocationId  = document.getElementById('WucGCNew1_hdn_BillingLocationId');

    var hdn_BillingParty = document.getElementById('WucGCNew1_hdn_BillingParty');
    var hdn_BillingHierarchy = document.getElementById('WucGCNew1_hdn_BillingHierarchy');
    var hdn_BillingLocation = document.getElementById('WucGCNew1_hdn_BillingLocation');
    var hdn_BillingRemark = document.getElementById('WucGCNew1_hdn_BillingRemark');

    var hdn_BillingParty_LedgerId = document.getElementById('WucGCNew1_hdn_BillingParty_LedgerId');
    var hdn_Billing_Party_CreditLimit = document.getElementById('WucGCNew1_hdn_Billing_Party_CreditLimit');
    var hdn_Billing_Party_ClosingBalance = document.getElementById('WucGCNew1_hdn_Billing_Party_ClosingBalance');
    var hdn_Billing_Party_MinimumBalance = document.getElementById('WucGCNew1_hdn_Billing_Party_MinimumBalance');
    var hdn_Billing_Party_Ledger_Closing_Balance = document.getElementById('WucGCNew1_hdn_Billing_Party_Ledger_Closing_Balance');
    var hdn_IsServiceTaxApplForBillParty = document.getElementById('WucGCNew1_hdn_IsServiceTaxApplForBillParty');
    var chk_Is_Multiple_Location_Billing_Allowed = document.getElementById('WucGCNew1_chk_Is_Multiple_Location_Billing_Allowed');
    var ClientCode = document.getElementById('WucGCNew1_hdn_ClientCode').value;


    var Rows = Results.value.Rows[0];

     if(Results.value.Rows.length > 0)
     {
        if (old_billing_party != txt_BillingParty.value.toUpperCase())
	    {
	       old_billing_party = txt_BillingParty.value.toUpperCase();
	       hdn_BillingPartyId.value = Rows['Client_ID'];
           hdn_BillingParty_LedgerId.value  = Rows['Ledger_Id'];
           hdn_Billing_Party_CreditLimit.value  = Rows['Credit_Limit'];
           hdn_Billing_Party_ClosingBalance.value  = Rows['Closing_Balance'];  
           hdn_Billing_Party_MinimumBalance.value  = Rows['MinimumBalance'];  
           hdn_Billing_Party_Ledger_Closing_Balance.value  = Rows['Ledger_Closing_Balance'];  
           
           if(Rows['Is_Service_Tax_Applicable'] == true)
                hdn_IsServiceTaxApplForBillParty.value = '1'; 
           else
                hdn_IsServiceTaxApplForBillParty.value = '0'; 

           if(Rows['Billing_Branch_Id'] > 0)
           {
                Clear_listbox('WucGCNew1_lst_BillLocation');
                hdn_BillingLocationId.value = Rows['Billing_Branch_Id']; 
                txt_BillingLocation.value = Rows['Billing_Branch_Name'];
                hdn_BillingLocation.value = Rows['Billing_Branch_Name'];
                ddl_BillingHierarchy.value = 'BO';
                hdn_BillingHierarchy.value = 'BO';
           }
           else
           {
                hdn_BillingLocationId.value = '0';
                txt_BillingLocation.value = '';
                hdn_BillingLocation.value = '';

                if(chk_Is_Multiple_Location_Billing_Allowed.checked == true)
                {
                    ddl_BillingHierarchy.value = '0';
                    hdn_BillingHierarchy.value = '0';
                }
                
                if(ClientCode == 'nandwana')
                {
                    ddl_BillingHierarchy.value = 'BO';
                    hdn_BillingHierarchy.value = 'BO';
                }
           }
        }
     }
     else
     {
       hdn_BillingPartyId.value  = '0';
       hdn_BillingParty_LedgerId.value  = '0';
       hdn_Billing_Party_CreditLimit.value  = '0';
       hdn_Billing_Party_ClosingBalance.value  = '0';
       hdn_Billing_Party_MinimumBalance.value  = '0';
       hdn_Billing_Party_Ledger_Closing_Balance.value  = '0';
       hdn_IsServiceTaxApplForBillParty.value = '0';
       hdn_BillingLocationId.value = '0';
       txt_BillingLocation.value = '';
       hdn_BillingLocation.value = '';
       if(chk_Is_Multiple_Location_Billing_Allowed.checked == true)
       {
            ddl_BillingHierarchy.value = '0';
            hdn_BillingHierarchy.value = '0';
       }
     }
     On_BillingHierarchy_Load();
     Set_ServiceTaxPayableBy();
     
     GetRateContract();
     
     CalculateDiscount();
 }

    
//*******************************************************************

function Wholeseler_LostFocus(txtbox,list_control)
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
    
    if(Search_Type == 'Wholeseler')
    {
        if (old_Wholeseler != txtbox_value) 
            Raj.EC.OperationModel.NewGCSearch.Get_AllDetails(0,list_control_value,SearchFor,handleWholeseler);
    }
}

 //************************************************************************

function handleWholeseler(Results)
{
    var txt_Wholeseler  = document.getElementById('WucGCNew1_txt_Wholeseler');
    var hdn_WholeselerId  = document.getElementById('WucGCNew1_hdn_WholeselerId');

    var hdn_Wholeseler = document.getElementById('WucGCNew1_hdn_Wholeseler');

    var Rows = Results.value.Rows[0];

     if(Results.value.Rows.length > 0)
     {
        if (old_Wholeseler != txt_Wholeseler.value.toUpperCase())
	    {
	       old_Wholeseler = txt_Wholeseler.value.toUpperCase();
	       hdn_WholeselerId.value = Rows['Wholeseler_ID'];
        }
     }
     else
     {
        hdn_WholeselerId.value  = '0';
        txt_Wholeseler.value = '';
     }

 }
 
 //************************************************************************
 
function ContractClient_LostFocus(txtbox,list_control)
{
    var txtbox_value = txtbox.value.toUpperCase();
    var list_control = document.getElementById(list_control); 
    var list_control_index = list_control.selectedIndex;
    var list_control_value;

    hidecontrol(list_control);

    if (list_control_index != -1)
       list_control_value =  list_control.options[list_control_index].value;
    else
       list_control_value = '0';
   
    if(Search_Type == 'ContractClient')
    {
        if (old_contract_client != txtbox_value) 
        {
            old_contract_client = txtbox_value;
            document.getElementById('WucGCNew1_hdn_ContractualClientId').value = list_control_value;

            Raj.EC.OperationModel.NewGCSearch.Get_AllDetails(0,list_control_value,SearchFor,handleContractClient);
        }
    }  
} 

//************************************************************************
function handleContractClient(Results)
{
    var hdn_Is_Paid_Allowed  = document.getElementById('WucGCNew1_hdn_Is_Paid_Allowed');
    var hdn_Is_To_Pay_Allowed  = document.getElementById('WucGCNew1_hdn_Is_To_Pay_Allowed');
    var hdn_Is_FOC_Allowed  = document.getElementById('WucGCNew1_hdn_Is_FOC_Allowed');
    var hdn_Is_To_Be_Billed_Allowed  = document.getElementById('WucGCNew1_hdn_Is_To_Be_Billed_Allowed');
    var ContractualClientId  = document.getElementById('WucGCNew1_hdn_ContractualClientId').value;
    var booking_date = WucGCNew1_wuc_BookingDate.GetSelectedDate();

    var Rows = Results.value.Rows[0];
     
     if(Results.value.Rows.length > 0)
     {
       hdn_Is_Paid_Allowed.value = Rows['Is_Paid_Allowed'];
       hdn_Is_To_Pay_Allowed.value = Rows['Is_To_Pay_Allowed'];
       hdn_Is_FOC_Allowed.value = Rows['Is_FOC_Allowed'];
       hdn_Is_To_Be_Billed_Allowed.value = Rows['Is_To_Be_Billed_Docket_Allowed'];
     }
     else
     {
       hdn_Is_Paid_Allowed.value = '0';
       hdn_Is_To_Pay_Allowed.value = '0';
       hdn_Is_FOC_Allowed.value = '0';
       hdn_Is_To_Be_Billed_Allowed.value = '0';
     }
    
    Raj.EC.OperationModel.NewGCSearch.Get_Contract_ClientBranch(1,val(ContractualClientId),0,booking_date,FillContractBranch);
} 
 
//**************************************************************************************
function FillContractBranch(Results)
{
    var ddl_ContractBranch  = document.getElementById('WucGCNew1_ddl_ContractBranch');

    var tot = Results.value.Rows.length -1;
    var count = 0;
  
    for(var count = ddl_ContractBranch.options.length-1; count > -1; count--)
    {
        ddl_ContractBranch.options[count] = null;
    }
    var optn1 = document.createElement("OPTION");
    optn1.text = String('Select One');
    optn1.value =  String('0');
    ddl_ContractBranch.options.add(optn1);

    for (count = 0;count <= tot;count ++)
    {
        ddl_ContractBranch.options[count + 1] = new Option(Results.value.Rows[count][Results.value.Columns[0].Name],Results.value.Rows[count][Results.value.Columns[1].Name]); 
    }
    
    On_ContractBranch_Change();
 } 
//*******************************************************************
function On_ContractBranch_Change()
{
    var booking_date = WucGCNew1_wuc_BookingDate.GetSelectedDate();
    var ContractualClientId = document.getElementById('WucGCNew1_hdn_ContractualClientId').value;
    var ContractBranchId = document.getElementById('WucGCNew1_ddl_ContractBranch').value;
    
    document.getElementById('WucGCNew1_hdn_ContractBranchId').value = val(ContractBranchId);

     Raj.EC.OperationModel.NewGCSearch.Get_Contract_ClientBranch(2,val(ContractualClientId),val(ContractBranchId),booking_date,FillContract);
}
    
//************************************************************************

function FillContract(Results)
{
    var ddl_Contract  = document.getElementById('WucGCNew1_ddl_Contract');

    var tot = Results.value.Rows.length -1;
    var count = 0;
  
    for(var count = ddl_Contract.options.length-1; count > -1; count--)
    {
        ddl_Contract.options[count] = null;
    }
    var optn1 = document.createElement("OPTION");
    optn1.text = String('Select One');
    optn1.value =  String('0');
    ddl_Contract.options.add(optn1);

    for (count = 0;count <= tot;count ++)
    {
        ddl_Contract.options[count + 1] = new Option(Results.value.Rows[count][Results.value.Columns[0].Name],Results.value.Rows[count][Results.value.Columns[1].Name]); 
    }
    
    Fill_Contract_Change(IsFromPageLoad);
 } 
 //*******************************************************************
function On_Contract_Change()
{
    IsFromPageLoad = false;
    Fill_Contract_Change(IsFromPageLoad);
}

function Fill_Contract_Change(IsFromPageLoad)
{
    var Contract_Id = document.getElementById('WucGCNew1_ddl_Contract').value;
    var hdn_ContractId = document.getElementById('WucGCNew1_hdn_ContractId');
    var VehicleType_Id = document.getElementById('WucGCNew1_ddl_VehicleType').value;
    var FromLocationId = document.getElementById('WucGCNew1_hdn_FromLocationId').value;
    var ToLocationId = document.getElementById('WucGCNew1_hdn_ToLocationId').value;
    var TransitDays = document.getElementById('WucGCNew1_hdn_TransitDays').value;
    var DistanceInKm = document.getElementById('WucGCNew1_hdn_Distance_In_Km').value;
    var TotalArticle = document.getElementById('WucGCNew1_hdn_TotalArticles').value;
    var ChargeWt = document.getElementById('WucGCNew1_hdn_ChargeWeight').value;
    var TotalCft = document.getElementById('WucGCNew1_hdn_TotalCFT').value;
    var CommodityId = document.getElementById('WucGCNew1_hdn_FirstCommodityId').value;
    var ItemId = document.getElementById('WucGCNew1_hdn_FirstItemId').value;
    var ArticleId = document.getElementById('WucGCNew1_hdn_FirstPackingTypeId').value;
    var hdn_IsContractApplied = document.getElementById('WucGCNew1_hdn_IsContractApplied');
    hdn_IsContractApplied.value = "0";
    
    hdn_ContractId.value = Contract_Id;
    
    ContractFreightDetails.clear();
    if(Contract_Id > 0)
        Raj.EC.OperationModel.NewGCSearch.Get_ContractDetails(val(Contract_Id),val(VehicleType_Id),val(FromLocationId),val(ToLocationId),val(CommodityId),val(ItemId),val(ArticleId),val(TransitDays),val(DistanceInKm),val(TotalArticle),val(ChargeWt),val(TotalCft),handleContract);
    else
       CallAllContractValue(IsFromPageLoad);
}
//************************************************************************

function handleContract(results)
{
    var hdn_IsContractApplied = document.getElementById('WucGCNew1_hdn_IsContractApplied');
//    var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');

    if(results.value != null && results.value.Rows.length > 0)
        hdn_IsContractApplied.value = "1";
    else
        hdn_IsContractApplied.value = "0";

    if(hdn_IsContractApplied.value == "1")
    {
        ContractFreightDetails[0] = new Array(31);
        for (var count1 = 0; count1 <= 31; count1++) 
        {
            ContractFreightDetails[0][count1] = results.value.Rows[0][results.value.Columns[count1].Name]; 
        }
    }
        CallAllContractValue(IsFromPageLoad);
 } 
function CallAllContractValue(IsFromPageLoad)
{
    var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');
    
    var hdn_Mode = document.getElementById('WucGCNew1_hdn_Mode'); 
    if(chk_IsAttached.checked == false)
    {
//////        SetContractValue(IsFromPageLoad);
    
        if (val(hdn_Mode.value) != 4)
        {
          SetContractValue(IsFromPageLoad);
        }   
        Calculate_Freight('0');
        Calculate_LocalCharge();
        Calculate_LoadingCharge();
        Calculate_FOV();
        Calculate_DDCharge();
        Calculate_GrandTotal();
    }
}
 //************************************************************************

function SetContractValue(IsFromPageLoad)
{
    var hdn_IsContractApplied = document.getElementById('WucGCNew1_hdn_IsContractApplied');
    var hdn_FreightRate = document.getElementById('WucGCNew1_hdn_FreightRate');
    var txt_FreightRate = document.getElementById('WucGCNew1_txt_FreightRate');
    var txt_StationaryCharge = document.getElementById('WucGCNew1_txt_StationaryCharge');
    var hdn_StationaryCharge = document.getElementById('WucGCNew1_hdn_StationaryCharge');
    var txt_ToPayCharge = document.getElementById('WucGCNew1_txt_ToPayCharge');
    var hdn_ToPayCharge = document.getElementById('WucGCNew1_hdn_ToPayCharge');
    var txt_DDCharge = document.getElementById('WucGCNew1_txt_DDCharge');
    var hdn_DDCharge = document.getElementById('WucGCNew1_hdn_DDCharge');
    var txt_VolumetricToKgFactor = document.getElementById('WucGCNew1_txt_VolumetricToKgFactor');
    var hdn_VolumetricToKgFactor = document.getElementById('WucGCNew1_hdn_VolumetricToKgFactor');

    var hdn_Standard_FreightRate = document.getElementById('WucGCNew1_hdn_Standard_FreightRate');
    var hdn_RateCard_BiltiCharges = document.getElementById('WucGCNew1_hdn_RateCard_BiltiCharges');
    var hdn_RateCard_ToPayCharges = document.getElementById('WucGCNew1_hdn_RateCard_ToPayCharges');
    var hdn_RateCard_DDCharge = document.getElementById('WucGCNew1_hdn_RateCard_DDCharge');
    var hdn_RateCard_CFTFactor = document.getElementById('WucGCNew1_hdn_RateCard_CFTFactor');

    var hdn_Contract_UnitOfFreightId = document.getElementById('WucGCNew1_hdn_Contract_UnitOfFreightId');
    var hdn_Contract_FreightBasisId = document.getElementById('WucGCNew1_hdn_Contract_FreightBasisId');
    var hdn_Contract_FreightSubUnitId = document.getElementById('WucGCNew1_hdn_Contract_FreightSubUnitId');
    var chk_Is_ToPay_Charge_Require = document.getElementById('WucGCNew1_chk_Is_ToPay_Charge_Require');       

    var txt_BillingParty = document.getElementById('WucGCNew1_txt_BillingParty');
    var hdn_BillingPartyId = document.getElementById('WucGCNew1_hdn_BillingPartyId');
    var ddl_BillingHierarchy = document.getElementById('WucGCNew1_ddl_BillingHierarchy');
    var txt_BillingLocation = document.getElementById('WucGCNew1_txt_BillingLocation');
    var hdn_BillingLocationId = document.getElementById('WucGCNew1_hdn_BillingLocationId');
    var hdn_IsServiceTaxApplForBillParty = document.getElementById('WucGCNew1_hdn_IsServiceTaxApplForBillParty');

    var hdn_BillingParty = document.getElementById('WucGCNew1_hdn_BillingParty');
    var hdn_BillingHierarchy = document.getElementById('WucGCNew1_hdn_BillingHierarchy');
    var hdn_BillingLocation = document.getElementById('WucGCNew1_hdn_BillingLocation');
    var hdn_BillingRemark = document.getElementById('WucGCNew1_hdn_BillingRemark');

    var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');
    var ddl_Consignment_Type = document.getElementById('WucGCNew1_ddl_Consignment_Type');
    var ddl_GCRisk = document.getElementById('WucGCNew1_ddl_GCRisk');
    var hdn_defaultConsignmentType = document.getElementById('WucGCNew1_hdn_defaultConsignmentType');
    var hdn_Default_GCRisk = document.getElementById('WucGCNew1_hdn_Default_GCRisk');
    var lbl_Consignment_Type = document.getElementById('WucGCNew1_lbl_Consignment_Type');
    var ddl_Consignment_Type = document.getElementById('WucGCNew1_ddl_Consignment_Type');
    var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');
    var GCId  = document.getElementById('WucGCNew1_hdn_GCId').value;

    if(hdn_IsContractApplied.value == "1" && chk_IsAttached.checked == false)
    {
        hdn_FreightRate.value = ContractFreightDetails[0][20];
        txt_FreightRate.value = ContractFreightDetails[0][20];
        txt_StationaryCharge.value = ContractFreightDetails[0][3];
        hdn_StationaryCharge.value = ContractFreightDetails[0][3];
        txt_DDCharge.value = ContractFreightDetails[0][10];
        hdn_DDCharge.value = ContractFreightDetails[0][10];
        txt_VolumetricToKgFactor.value = ContractFreightDetails[0][19];
        hdn_VolumetricToKgFactor.value = ContractFreightDetails[0][19];
        
        if(val(ddl_PaymentType.value) == 1 && chk_Is_ToPay_Charge_Require.checked == true)
        {
            txt_ToPayCharge.value = ContractFreightDetails[0][5];
            hdn_ToPayCharge.value = ContractFreightDetails[0][5];
        }
        else
        {
            txt_ToPayCharge.value = '0';
            hdn_ToPayCharge.value = '0';
        }
        hdn_Contract_UnitOfFreightId.value = ContractFreightDetails[0][21];
        hdn_Contract_FreightBasisId.value = ContractFreightDetails[0][22];
        hdn_Contract_FreightSubUnitId.value = ContractFreightDetails[0][23];
        
        if(IsFromPageLoad == false)
        {
            hdn_BillingPartyId.value = ContractFreightDetails[0][24];
            txt_BillingParty.value = ContractFreightDetails[0][25];
            hdn_BillingParty.value = ContractFreightDetails[0][25];
            ddl_BillingHierarchy.value = ContractFreightDetails[0][26];
            hdn_BillingHierarchy.value = ContractFreightDetails[0][26];
            hdn_BillingLocationId.value = ContractFreightDetails[0][27];
            txt_BillingLocation.value = ContractFreightDetails[0][28];
            hdn_BillingLocation.value = ContractFreightDetails[0][28];
        }
        ddl_GCRisk.value = ContractFreightDetails[0][29];
        ddl_Consignment_Type.value = ContractFreightDetails[0][30];
        if(ContractFreightDetails[0][31] == true)
            hdn_IsServiceTaxApplForBillParty.value = '1'; 
        else
            hdn_IsServiceTaxApplForBillParty.value = '0';

        lbl_Consignment_Type.style.display = 'inline';
        ddl_Consignment_Type.style.display = 'inline';
    }
    else if(hdn_IsContractApplied.value == "0")
    {
        hdn_FreightRate.value = hdn_Standard_FreightRate.value;
        txt_FreightRate.value = hdn_Standard_FreightRate.value;
        txt_StationaryCharge.value = hdn_RateCard_BiltiCharges.value;
        hdn_StationaryCharge.value = hdn_RateCard_BiltiCharges.value;
        txt_ToPayCharge.value = hdn_RateCard_ToPayCharges.value;
        hdn_ToPayCharge.value = hdn_RateCard_ToPayCharges.value;
        txt_DDCharge.value = hdn_RateCard_DDCharge.value;
        hdn_DDCharge.value = hdn_RateCard_DDCharge.value;
        txt_VolumetricToKgFactor.value = hdn_RateCard_CFTFactor.value;
        hdn_VolumetricToKgFactor.value = hdn_RateCard_CFTFactor.value;

        hdn_Contract_UnitOfFreightId.value = '0';
        hdn_Contract_FreightBasisId.value = '0';
        hdn_Contract_FreightSubUnitId.value = '0';
        
        hdn_BillingPartyId.value = '0';
        txt_BillingParty.value = '';
        hdn_BillingParty.value = '';
        ddl_BillingHierarchy.value = '0';
        hdn_BillingHierarchy.value = '0';
        hdn_BillingLocationId.value = '0';
        txt_BillingLocation.value = '';
        hdn_BillingLocation.value = '';
        ddl_GCRisk.value = hdn_Default_GCRisk.value;
        ddl_Consignment_Type.value = hdn_defaultConsignmentType.value;
        //hdn_IsServiceTaxApplForBillParty.value = '0';

        lbl_Consignment_Type.style.display = 'none';
        ddl_Consignment_Type.style.display = 'none';
    }
    Set_ServiceTaxPayableBy();
    Enable_Disable_controls_On_ContractChange();
 } 
 
function Enable_Disable_controls_On_ContractChange()
{
    var hdn_IsContractApplied = document.getElementById('WucGCNew1_hdn_IsContractApplied');
    var txt_FreightRate = document.getElementById('WucGCNew1_txt_FreightRate');
    var txt_StationaryCharge = document.getElementById('WucGCNew1_txt_StationaryCharge');
    var txt_ToPayCharge = document.getElementById('WucGCNew1_txt_ToPayCharge');
    var txt_DDCharge = document.getElementById('WucGCNew1_txt_DDCharge');

    var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');
    var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');
    var ddl_DeliveryType = document.getElementById('WucGCNew1_ddl_Dly_Type');
    var hdn_Contract_UnitOfFreightId = document.getElementById('WucGCNew1_hdn_Contract_UnitOfFreightId');
    var hdn_Contract_FreightBasisId = document.getElementById('WucGCNew1_hdn_Contract_FreightBasisId');
    var hdn_Contract_FreightSubUnitId = document.getElementById('WucGCNew1_hdn_Contract_FreightSubUnitId');

    var ddl_FreightBasis = document.getElementById('WucGCNew1_ddl_FreightBasis');  
    var ddl_LengthChargeHead = document.getElementById('WucGCNew1_ddl_LengthChargeHead');
    var ddl_VolumetricFreightUnit = document.getElementById('WucGCNew1_ddl_VolumetricFreightUnit');

    var txt_Freight = document.getElementById('WucGCNew1_txt_Freight');
    var txt_LocalCharge = document.getElementById('WucGCNew1_txt_LocalCharge');
    var txt_LoadingCharge = document.getElementById('WucGCNew1_txt_LoadingCharge');
    var txt_FOVRiskCharge = document.getElementById('WucGCNew1_txt_FOVRiskCharge');
    var lbl_OtherCharges = document.getElementById('WucGCNew1_lbl_OtherCharges');
    var lnk_OtherCharges = document.getElementById('WucGCNew1_lnk_OtherCharges');
//    var txt_ReBookGCAmount = document.getElementById('WucGCNew1_txt_ReBookGCAmount');        
//    var hdn_ReBookGCAmount = document.getElementById('WucGCNew1_hdn_ReBookGCAmount');
    var txt_NFormCharge = document.getElementById('WucGCNew1_txt_NFormCharge');        
    var txt_LengthCharge = document.getElementById('WucGCNew1_txt_LengthCharge');
    var txt_UnloadingCharge = document.getElementById('WucGCNew1_txt_UnloadingCharge'); 
    var txt_VolumetricToKgFactor = document.getElementById('WucGCNew1_txt_VolumetricToKgFactor');       
    var hdn_FreightBasisId = document.getElementById('WucGCNew1_hdn_FreightBasisId');
    var hdn_defaultFreightBasisId = document.getElementById('WucGCNew1_hdn_defaultFreightBasisId');

    if(hdn_IsContractApplied.value == "1")  // if contract is applied then all the controls are disables  
    {
        txt_Freight.disabled = true;
        txt_FreightRate.disabled = true;
        if (chk_IsAttached.checked == false)
        {
            txt_LocalCharge.disabled = true;
            if(val(ddl_DeliveryType.value) != 2)
            {
                txt_DDCharge.disabled = true;
            }
            lnk_OtherCharges.style.display = 'none';
            lbl_OtherCharges.style.display = 'inline';
        }
        txt_LoadingCharge.disabled = true;
        txt_NFormCharge.disabled = true;
        txt_ToPayCharge.disabled = true;
//        txt_LengthCharge.disabled = true;
        ddl_LengthChargeHead.disabled = true;
        txt_UnloadingCharge.disabled = true;

        if(hdn_Contract_UnitOfFreightId.value == 1) // for vehicle
        {
            ddl_FreightBasis.value = 3; // fixed
            hdn_FreightBasisId.value = 3;
        }
        else if(hdn_Contract_UnitOfFreightId.value == 2)// for weight
        {
            ddl_FreightBasis.value = 1; // Weight
            hdn_FreightBasisId.value = 1;
        }
        else if(hdn_Contract_UnitOfFreightId.value == 3)// for CFT
        {
            ddl_FreightBasis.value = 4; // Volumetric for cft
            hdn_FreightBasisId.value = 4;

            txt_VolumetricToKgFactor.disabled = true;
            ddl_VolumetricFreightUnit.disabled = true; // set cft if selected contract on CFT
            ddl_VolumetricFreightUnit.value = 1;                
        }
        else if(hdn_Contract_UnitOfFreightId.value == 4 )// for Article
        {
            ddl_FreightBasis.value = 2; // Article
            hdn_FreightBasisId.value = 2;
        }

        ddl_FreightBasis.disabled = true;
    }
    else // if contract is Not applied then all the controls are Enable
    {
        if (chk_IsAttached.checked == false && val(ddl_PaymentType.value) != 5)
        {
            txt_Freight.disabled = false;
            txt_FreightRate.disabled = false;
            txt_LocalCharge.disabled = false;
            txt_LoadingCharge.disabled = false;
            txt_NFormCharge.disabled = false;
            txt_DDCharge.disabled = true;
            lnk_OtherCharges.style.display = 'inline';
            lbl_OtherCharges.style.display = 'none';
            
            if (val(ddl_PaymentType.value) == 1)     // to pay
            {
                txt_ToPayCharge.disabled = false;
            }
            if (val(ddl_DeliveryType.value) == 2) //' for door
            {
                txt_DDCharge.disabled = false;
            }
//            txt_LengthCharge.disabled = false;
            txt_VolumetricToKgFactor.disabled = false;
            txt_UnloadingCharge.disabled = false;

            ddl_LengthChargeHead.disabled = false;
            ddl_FreightBasis.disabled = false;
            ddl_VolumetricFreightUnit.disabled = false;
            ddl_FreightBasis.value = hdn_defaultFreightBasisId.value;
            hdn_FreightBasisId.value = hdn_defaultFreightBasisId.value;
        }
    }
    On_BillingHierarchy_Load();
    EnableDisable_On_FreightBasis_Change();
 } 
//*****************************************************************************************
function GC_Picker_OnSelectionChanged(picker)
{
    picker.AssociatedCalendar.SetSelectedDate(picker.GetSelectedDate());
    On_PickerChange(picker.GetSelectedDate());
    On_PickerChangeForServiceType();
    CalculateDiscount();
}
//*******************************************************************
function GC_Calendar_OnSelectionChanged(calendar)
{
    calendar.AssociatedPicker.SetSelectedDate(calendar.GetSelectedDate());
    On_PickerChange(calendar.GetSelectedDate());
    On_PickerChangeForServiceType();
    CalculateDiscount();
}

//******************************** Vehicle Type Change********************************************
function On_VehicleType_Change()
{
    Get_Standard_FreightRate();
}

//********************************Picker Change********************************************
function On_PickerChange(Date)
{
    Raj.EC.OperationModel.NewGCSearch.Get_ServiceTaxOnPickerChange(Date,handlePicker);
}

//*****************************************************************************************

function handlePicker(Results)
{
    var lbl_ServiceTax  = document.getElementById('WucGCNew1_lbl_ServiceTax');
    var hdn_Standard_ServiceTaxPercent  = document.getElementById('WucGCNew1_hdn_Standard_ServiceTaxPercent');

    var Rows = Results.value.Rows[0];
     
     if(Results.value.Rows.length > 0)
     {
       lbl_ServiceTax.innerHTML  = "GST (" + Rows['Service_Tax_Percent'] + "%)";
       hdn_Standard_ServiceTaxPercent.value  = Rows['Service_Tax_Percent'];       
     }
     Set_Exp_Del_Date();
     Calculate_GrandTotal();
 }
 
//********************************Picker Change********************************************
function On_PickerChangeForServiceType()
{
    var booking_date;
    booking_date = WucGCNew1_wuc_BookingDate.GetSelectedDate();
    booking_date = new Date(booking_date.getFullYear(), booking_date.getMonth(),booking_date.getDate());
    
    var ServiceTypeId = document.getElementById('WucGCNew1_ddl_Service_Type').value;

    Raj.EC.OperationModel.NewGCSearch.Is_STAbatmentRequired(val(ServiceTypeId),booking_date,IsSTAbatmentRequired);
}
//*****************************************************************************************

function IsSTAbatmentRequired(Results)
{
    var chk_Is_ST_Abatment_Req  = document.getElementById('WucGCNew1_chk_Is_ST_Abatment_Required');
    var hdn_AbatePercentage  = document.getElementById('WucGCNew1_hdn_AbatePercentage');

    chk_Is_ST_Abatment_Req.checked  = Results.value.Rows[0]['Is_ST_Abatment']; 
    hdn_AbatePercentage.value  = Results.value.Rows[0]['AbatePercentage'];  
    Calculate_GrandTotal();
 }
 
//************************************************************************************************
 
function On_DeliveryType_Change()
{
    var ddl_DeliveryType= document.getElementById('WucGCNew1_ddl_Dly_Type');  
    var ddl_DeliveryAgainst= document.getElementById('WucGCNew1_ddl_Delivery_Against');  
    var hdn_Dly_Type= document.getElementById('WucGCNew1_hdn_Dly_Type');  

    document.getElementById('WucGCNew1_hdn_Dly_Type').value = ddl_DeliveryType.value; 
    EnableDisable_On_DeliveryType_Change();
    
    if(val(ddl_DeliveryType.value) == 2) //' for door
        ddl_DeliveryAgainst.value = 1; 
    else  // for godown
        ddl_DeliveryAgainst.value = 2;  // for  Godown against consinee copy 
        
    Calculate_DDCharge();
    Calculate_GrandTotal();
}

function EnableDisable_On_DeliveryType_Change()
{
    var ddl_DeliveryType= document.getElementById('WucGCNew1_ddl_Dly_Type');
    var pnl_Change_Consignee_Address= document.getElementById('WucGCNew1_pnl_Change_Consignee_Address');
    var txt_DDCharge = document.getElementById('WucGCNew1_txt_DDCharge');
    var ddl_DeliveryAgainst= document.getElementById('WucGCNew1_ddl_Delivery_Against');
    var hdn_IsContractApplied = document.getElementById('WucGCNew1_hdn_IsContractApplied');
    var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');

    if(val(ddl_DeliveryType.value) == 2) //' for door
    {
        ddl_DeliveryAgainst.value = 1; 
        pnl_Change_Consignee_Address.style.visibility = 'visible';
        if(hdn_IsContractApplied.value == '1' && chk_IsAttached.checked == false)
            txt_DDCharge.disabled = true;
        else
            txt_DDCharge.disabled = false;
    }
    else  // for godown
    {
        ddl_DeliveryAgainst.value = 2;
        pnl_Change_Consignee_Address.style.visibility = 'hidden'; 
        txt_DDCharge.disabled = true;                         
    }
}
 ////*******************************************************************

function On_IsInsured_Click()
{
    var chk_IsInsured = document.getElementById('WucGCNew1_chk_IsInsured');
    var hdn_Mode = document.getElementById('WucGCNew1_hdn_Mode');

    if (chk_IsInsured.checked == true)
    {
        var Path='FrnNewGCInsuranceDetails.aspx?Mode=' + hdn_Mode.value ;

        w = screen.availWidth;
        h = screen.availHeight;
        var popW = 620, popH = 300;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;

        window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');
    }

     Calculate_FOV();
     Calculate_GrandTotal();
     On_CashAmount_Change();
     Set_Payment_Details();
}    
//******************************************************************************************
function SetCommodityDetails(Article,Weight,Rate,Amount,Is_ServiceTaxForCommodity,ItemValueForFOV,TotalBharaiAmt)
{    
    document.getElementById('WucGCNew1_hdn_TotalArticles').value = Article;
    document.getElementById('WucGCNew1_hdn_TotalWeight').value = Weight;
    document.getElementById('WucGCNew1_hdn_TotalRate').value = Rate;
    document.getElementById('WucGCNew1_hdn_TotalAmount').value = Amount;
    document.getElementById('WucGCNew1_hdn_Freight').value = Amount;
    document.getElementById('WucGCNew1_txt_ActualWeight').value = Weight;
    document.getElementById('WucGCNew1_hdn_Is_Service_Tax_Applicable_For_Commodity').value = Is_ServiceTaxForCommodity;
    document.getElementById('WucGCNew1_hdn_ItemValueForFOV').value = ItemValueForFOV;
    document.getElementById('WucGCNew1_hdn_TotalBharaiAmt').value = TotalBharaiAmt;
    
    
    var hdn_ConsgnyDlyAreaID   = document.getElementById('WucGCNew1_hdn_ConsigneeDeliveryAreaID');

    DisableToLocation();
    Calculate_Freight('0');
    Calculate_LoadingCharge();
    CalculateDiscount();
    Calculate_FOV();    

    Raj.EC.OperationModel.NewGCSearch.Get_DDCharges(Article,Weight,hdn_ConsgnyDlyAreaID.value,handleDDCharge);
}

function handleDDCharge(Results)
{    
     var Rows = Results.value.Rows[0];
     if(Results.value.Rows.length > 0)
     {
        document.getElementById('WucGCNew1_txt_DDCharge').value = Rows['DDChargeValue'];
        document.getElementById('WucGCNew1_hdn_DDCharge').value = Rows['DDChargeValue'];
        document.getElementById('WucGCNew1_hdn_Standard_DDCharge').value = Rows['DDChargeValue'];
        Calculate_GrandTotal();
     }
}

function DisableToLocation()
{
  var hdn_TotalArticles = document.getElementById('WucGCNew1_hdn_TotalArticles');
  var txt_To_Location = document.getElementById('WucGCNew1_txt_To_Location');
  var txt_ConsigneeName = document.getElementById('WucGCNew1_txt_ConsigneeName');  
  var ddl_Dly_Type = document.getElementById('WucGCNew1_ddl_Dly_Type');  
  var ddl_Booking_Type = document.getElementById('WucGCNew1_ddl_Booking_Type');
  var hdn_ConsigneeId = document.getElementById('WucGCNew1_hdn_ConsigneeId'); 
  
  var txt_FromLocation = document.getElementById('WucGCNew1_txt_From_Location');
  var hdn_ConsignorId = document.getElementById('WucGCNew1_hdn_ConsignorId');
  
  var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');
  var txt_BillingParty = document.getElementById('WucGCNew1_txt_BillingParty');
  var txt_ConsignorName = document.getElementById('WucGCNew1_txt_ConsignorName');
  
  if (val(hdn_TotalArticles.value) > 0)
    {
//      txt_To_Location.disabled = true;
//      txt_ConsigneeName.disabled = true;
      //Modified on 13-08-2016
      txt_To_Location.readOnly = true;
      txt_ConsigneeName.readOnly = true;
      
     //Change From true to false On 04 Sep 2020
     //ddl_Dly_Type.disabled = true; 
      ddl_Dly_Type.disabled = false; 
            
      ddl_Booking_Type.disabled = true;
      txt_FromLocation.readOnly = true; 
      
      ddl_PaymentType.disabled = true;
      txt_BillingParty.readOnly = true; 
      txt_ConsignorName.readOnly = true;
    }
  else
    {
//      txt_To_Location.disabled = false;
//      txt_ConsigneeName.disabled = false;

        //Modified on 13-08-2016
        if (val(hdn_ConsigneeId.value) <= 0) 
        {
            txt_To_Location.readOnly = false;
        }
        
        if (val(hdn_ConsignorId.value) <= 0) 
        {
            txt_FromLocation.readOnly = false;
        }
        
        txt_ConsigneeName.readOnly = false;
      ddl_Dly_Type.disabled = false;
      ddl_Booking_Type.disabled = false;
      
      ddl_PaymentType.disabled = false;
      txt_BillingParty.readOnly = false;
      txt_ConsignorName.readOnly = false; 
    }
    //Change From true to false On 04 Sep 2020
     //ddl_Dly_Type.disabled = true; 
      ddl_Dly_Type.disabled = false; 
}
//***********************************************************************************************
function SetInvoiceDetails(InvoiceAmount)
{  
    document.getElementById('WucGCNew1_hdn_TotalInvoiceAmount').value = InvoiceAmount;
    
    Calculate_FOV();
    Calculate_GrandTotal();
}
//***********************************************************************************************
function CheckInvoiceAmount(InvoiceAmount)
{  
//    if (InvoiceAmount >= 100000)
//    {alert('Please Check Invoice Value');}

    var hdn_ConsignorStateId = document.getElementById('WucGCNew1_hdn_ConsignorStateId');
    var hdn_ConsigneeStateId = document.getElementById('WucGCNew1_hdn_ConsigneeStateId');
    var hdn_Is_ServiceTax_ForCommodity = document.getElementById('WucGCNew1_hdn_Is_Service_Tax_Applicable_For_Commodity');

    var hdn_IsServiceTaxApplicableForConsignor = document.getElementById('WucGCNew1_hdn_IsServiceTaxApplicableForConsignor');
    var hdn_IsServiceTaxApplicableForConsignee = document.getElementById('WucGCNew1_hdn_IsServiceTaxApplicableForConsignee');

//    if ((hdn_IsServiceTaxApplicableForConsignor.value == '1' || hdn_IsServiceTaxApplicableForConsignee.value == '1') && (InvoiceAmount >= 50000 && val(hdn_ConsignorStateId.value) != val(hdn_ConsigneeStateId.value) && hdn_Is_ServiceTax_ForCommodity.value == '1'))
    if ((hdn_IsServiceTaxApplicableForConsignor.value == '1' || hdn_IsServiceTaxApplicableForConsignee.value == '1') && (InvoiceAmount >= 100000 && val(hdn_ConsignorStateId.value) == 1 && val(hdn_ConsigneeStateId.value) == 1 && hdn_Is_ServiceTax_ForCommodity.value == '1')) 
    {
        alert('Please Enter e-Way Bill No. As Per Maharashtra Intra-State Rule OR Check Invoice Value');
    }
    else if ((hdn_IsServiceTaxApplicableForConsignor.value == '1' || hdn_IsServiceTaxApplicableForConsignee.value == '1') && (InvoiceAmount >= 50000 && val(hdn_ConsignorStateId.value) == 2 && val(hdn_ConsigneeStateId.value) == 2 && hdn_Is_ServiceTax_ForCommodity.value == '1')) 
    {
        alert('Please Enter e-Way Bill No. As Per Gujrat Intra-State Rule OR Check Invoice Value');
    }
    else if ((hdn_IsServiceTaxApplicableForConsignor.value == '1' || hdn_IsServiceTaxApplicableForConsignee.value == '1') && (InvoiceAmount >= 50000 && val(hdn_ConsignorStateId.value) != val(hdn_ConsigneeStateId.value) && hdn_Is_ServiceTax_ForCommodity.value == '1')) 
    {
        alert('Please Enter e-Way Bill No. As Per Inter-State Rule OR Check Invoice Value');
    }
    else if (InvoiceAmount >= 100000) 
    {
        alert('Please Check Invoice Value');
    }

}
    
//***********************************************************************************************
function SetTotalOtherCharges(TotalOtherCharges)
{  
    document.getElementById('WucGCNew1_hdn_OtherCharge').value = val(TotalOtherCharges);
    document.getElementById('WucGCNew1_lbl_OtherChargesValue').innerHTML = val(TotalOtherCharges);

    Calculate_GrandTotal();
} 
//***********************************************************************************************
function SetConsigneeDDAddress(DDAdd1,DDAdd2)
{
    document.getElementById('WucGCNew1_hdn_ConsigneeDDAddressLine1').value = DDAdd1;
    document.getElementById('WucGCNew1_hdn_ConsigneeDDAddressLine2').value = DDAdd2;
}
//***********************************************************************************************
function SetIsInsuranceDetailsfilled(IsInsuranceDetailsFilled)
{
    document.getElementById('WucGCNew1_hdn_Is_InsuranceDetails_Filled').value = IsInsuranceDetailsFilled;
}
//***********************************************************************************************
function SetIsServiceTaxForBillingParty(Is_ServiceTaxForBillingParty)
{
     document.getElementById('WucGCNew1_hdn_IsServiceTaxApplForBillParty').value = Is_ServiceTaxForBillingParty;

     Set_ServiceTaxPayableBy();
     Calculate_GrandTotal();
}
//***********************************************************************************************
function SetIsContainerDetailsfilled(isContainerfilled)
{
    document.getElementById('WucGCNew1_hdn_Is_ContainerDetails_Filled').value = isContainerfilled;
}
//***********************************************************************************************
function SetLocationDetails(LocationId,LocationName,IsFromLocation)
{
    if(IsFromLocation == 1)
    {
        document.getElementById('WucGCNew1_hdn_FromLocationId').value = LocationId;
        document.getElementById('WucGCNew1_txt_From_Location').value = LocationName;
    }
    else
    {
        document.getElementById('WucGCNew1_hdn_ToLocationId').value = LocationId;
        document.getElementById('WucGCNew1_txt_To_Location').value = LocationName;
        Raj.EC.OperationModel.NewGCSearch.Get_AllDetails(0,LocationId,'Location',handleToLocation);
    }
    Get_Standard_FreightRate();
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
//********************************LengthChargeSearch*********************************************
function On_ddlLengthChargeChange(ddllengthcharge)
{
    Raj.EC.OperationModel.NewGCSearch.Get_AllDetails(0,val(ddllengthcharge.value),'LengthCharge',handleLengthCharge);
}

function handleLengthCharge(Results)
{
    EnableDisable_On_LengthCharge_Change();
    var Rows = Results.value.Rows[0];
     
     if(Results.value.Rows.length > 0)
     {
       document.getElementById('WucGCNew1_txt_LengthCharge').value  = Rows['Charge_Amount'];
       document.getElementById('WucGCNew1_hdn_LengthCharge').value  = Rows['Charge_Amount'];
       document.getElementById('WucGCNew1_hdn_Standard_LengthCharge').value  = Rows['Charge_Amount'];       
     }
     else
     {
       document.getElementById('WucGCNew1_txt_LengthCharge').value  = '0';
       document.getElementById('WucGCNew1_hdn_LengthCharge').value  = '0';
       document.getElementById('WucGCNew1_hdn_Standard_LengthCharge').value  = '0';
     }
     Calculate_GrandTotal();
 }
 
function EnableDisable_On_LengthCharge_Change()
{
    var txt_LengthCharge  = document.getElementById('WucGCNew1_txt_LengthCharge');
    var ddl_LengthChargeHead  = document.getElementById('WucGCNew1_ddl_LengthChargeHead');
     
//     if(val(ddl_LengthChargeHead.value) > 0)
//       txt_LengthCharge.disabled = false;
//     else
//       txt_LengthCharge.disabled = true;
 } 

//********************************BookingTypeChange*********************************************
function On_BookingTypeChange()
{
    var ddl_Booking_Type  = document.getElementById('WucGCNew1_ddl_Booking_Type');
    var ddl_Dly_Type = document.getElementById('WucGCNew1_ddl_Dly_Type');
    var ddl_GCRisk = document.getElementById('WucGCNew1_ddl_GCRisk');
    var hdn_Dly_Type = document.getElementById('WucGCNew1_hdn_Dly_Type');
    var hdn_Booking_Type = document.getElementById('WucGCNew1_hdn_Booking_Type');
    var clientCode = document.getElementById('WucGCNew1_hdn_ClientCode').value;
    
    document.getElementById('WucGCNew1_hdn_Booking_Type').value = ddl_Booking_Type.value; 
    
    if(clientCode == 'excel')
        Raj.EC.OperationModel.NewGCSearch.Get_AllDetails(0,val(ddl_Booking_Type.value),'BookingType',handleBookingType);
    else
    {
        EnableDisable_On_BookingType_Change();
        if(val(ddl_Booking_Type.value) == 1)  // If Sundry
        {
            ddl_Dly_Type.value = 1;
            hdn_Dly_Type.value = 1;
            On_DeliveryType_Change();  
        }
    } 
    
    if(val(ddl_Booking_Type.value) == 2) // 2 FTL
    {
    
            var opts = ddl_GCRisk.options.length;
            for (var i=0; i<opts; i++)
            {
                if (ddl_GCRisk.options[i].value == "1")
                {
                ddl_GCRisk.options[i].selected = true;
                break;
                }
            }  
        document.getElementById('WucGCNew1_txt_Discount').value = 0;
        document.getElementById('WucGCNew1_hdn_Discount').value = 0;
        document.getElementById('WucGCNew1_hdn_DiscountId').value = 0;

        document.getElementById('WucGCNew1_txt_LocalCharge').value = 0;
        document.getElementById('WucGCNew1_hdn_LocalCharge').value = 0;
        
        document.getElementById('WucGCNew1_txt_LoadingCharge').value = 0;
        document.getElementById('WucGCNew1_hdn_LoadingCharge').value = 0;
        
        document.getElementById('WucGCNew1_txt_StationaryCharge').value = 0;
        document.getElementById('WucGCNew1_hdn_StationaryCharge').value = 0;
        
        document.getElementById('WucGCNew1_txt_FOVRiskCharge').value = 0;
        document.getElementById('WucGCNew1_hdn_FOVRiskCharge').value = 0;
        
        document.getElementById('WucGCNew1_txt_ODACharge').value = 0;
        document.getElementById('WucGCNew1_hdn_ODACharge').value = 0;
        
        document.getElementById('WucGCNew1_txt_LengthCharge').value = 0;
        document.getElementById('WucGCNew1_hdn_LengthCharge').value = 0;
        
        document.getElementById('WucGCNew1_txt_AOC').value = 0;
        document.getElementById('WucGCNew1_hdn_AOC').value = 0;
//        document.getElementById('WucGCNew1_hdn_AOCPercent').value = 0;
        TempAOCPercent = document.getElementById('WucGCNew1_hdn_AOCPercent').value;
        document.getElementById('WucGCNew1_hdn_AOCPercent').value = 0;
         
        
    }
    else //1 Sundry
    {
        document.getElementById('WucGCNew1_hdn_AOCPercent').value = TempAOCPercent;
        IsFromPageLoad = true;
        Fill_Contract_Change(IsFromPageLoad);
        Calculate_Freight('0');
        Calculate_LocalCharge();
        Calculate_LoadingCharge();
        CalculateDiscount();
        Calculate_FOV();
        Calculate_DDCharge();
           
            var opts = ddl_GCRisk.options.length;
            for (var i=0; i<opts; i++)
            {
                if (ddl_GCRisk.options[i].value == "2")
                {
                ddl_GCRisk.options[i].selected = true;
                break;
                }
            } 
    }
    
    GetRateContract();
    
    Calculate_GrandTotal();
    
}

function handleBookingType(Results)
{
    var ddl_booking_Sub_Type  = document.getElementById('WucGCNew1_ddl_booking_Sub_Type');
    var ddl_Booking_Type = document.getElementById('WucGCNew1_ddl_Booking_Type');
    var ddl_Dly_Type = document.getElementById('WucGCNew1_ddl_Dly_Type');
    var hdn_Dly_Type = document.getElementById('WucGCNew1_hdn_Dly_Type');

    var tot = Results.value.Rows.length -1;
    var count = 0;
  
    for (var count = ddl_booking_Sub_Type.options.length-1; count >-1; count--)
    {
        ddl_booking_Sub_Type.options[count] = null;
    }
    for (count = 0;count <= tot;count ++)
    {
        ddl_booking_Sub_Type.options[count] = new Option(Results.value.Rows[count][Results.value.Columns[0].Name],Results.value.Rows[count][Results.value.Columns[1].Name]); 
    }  
    EnableDisable_On_BookingType_Change();
    
    if(val(ddl_Booking_Type.value) == 1)  // If Sundry
    {
        ddl_Dly_Type.value = 1;
        hdn_Dly_Type.value = 1;
        On_DeliveryType_Change();
    }
    Calculate_GrandTotal();
 }
 
function EnableDisable_On_BookingType_Change()
{
    var ddl_Booking_Type = document.getElementById('WucGCNew1_ddl_Booking_Type');
    var td_ContainerDetails = document.getElementById('WucGCNew1_td_ContainerDetails');
    var td_vehicleType = document.getElementById('WucGCNew1_td_vehicleType');
    var ddl_VehicleType = document.getElementById('WucGCNew1_ddl_VehicleType');
    var td_vehicleno = document.getElementById('WucGCNew1_td_vehicleno');
    var txt_VehicleNo = document.getElementById('WucGCNew1_txt_VehicleNo');
    var td_fesibilityrsno = document.getElementById('WucGCNew1_td_fesibilityrsno');
    var txt_FeasibilityAndRouteSurveyNo = document.getElementById('WucGCNew1_txt_FeasibilityAndRouteSurveyNo');
    var tr_onBookingTypechange = document.getElementById('WucGCNew1_tr_onBookingTypechange');
    var hdn_Container_Det_ReqFor_BkgType = document.getElementById('WucGCNew1_hdn_Container_Details_RequiredFor_BookingType');
    var clientCode = document.getElementById('WucGCNew1_hdn_ClientCode').value;

    var arr = hdn_Container_Det_ReqFor_BkgType.value.split(",");

    td_ContainerDetails.style.display = 'none'; 

    if (arr[0] == 'all')
        td_ContainerDetails.style.display = 'inline';
    else
    {
        for(i = 0;i < arr.length;i++)
        {
            if(arr[i] == ddl_Booking_Type.value)
            {
                td_ContainerDetails.style.display = 'inline';
                break;
            } 
        }
    }
        
        tr_onBookingTypechange.style.display = 'none';
        td_vehicleType.style.display = 'none';
        ddl_VehicleType.style.display = 'none';
        td_vehicleno.style.display = 'none';
        txt_VehicleNo.style.display = 'none';
        td_fesibilityrsno.style.display = 'none';
        txt_FeasibilityAndRouteSurveyNo.style.display = 'none';
        
     if(clientCode != 'nandwana' && val(ddl_Booking_Type.value) == 2)
     {
        tr_onBookingTypechange.style.display = 'inline';
        td_vehicleType.style.display = 'inline';
        ddl_VehicleType.style.display = 'inline';
        td_vehicleno.style.display = 'inline';
        txt_VehicleNo.style.display = 'inline';
     }
     else if(clientCode != 'nandwana' && (val(ddl_Booking_Type.value) == 3 || val(ddl_Booking_Type.value) == 4))
     {
        tr_onBookingTypechange.style.display = 'inline';
        td_vehicleType.style.display = 'inline';
        ddl_VehicleType.style.display = 'inline';
        td_vehicleno.style.display = 'inline';
        txt_VehicleNo.style.display = 'inline';
        td_fesibilityrsno.style.display = 'inline';
        txt_FeasibilityAndRouteSurveyNo.style.display = 'inline';
     }
     else if(clientCode != 'nandwana' && val(ddl_Booking_Type.value) == 5)
     {
        tr_onBookingTypechange.style.display = 'inline';
        td_fesibilityrsno.style.display = 'inline';
        txt_FeasibilityAndRouteSurveyNo.style.display = 'inline';
     }
 } 
 //*********************************FreightSearch****************************************
function On_Freight_Change()
{ 
    var txt_Freight  = document.getElementById('WucGCNew1_txt_Freight');
    var hdn_Freight  = document.getElementById('WucGCNew1_hdn_Freight');

    txt_Freight.value = val(txt_Freight.value);
    hdn_Freight.value = val(txt_Freight.value);
   
    Calculate_Freight('CallFromFreight');
    CalculateDiscount();
    Calculate_GrandTotal();
}
 //******************************** Discount Change ***********************************
 
function On_Discount_Change()
{
   var hdn_Discount= document.getElementById('WucGCNew1_hdn_Discount');  
   var txt_Discount= document.getElementById('WucGCNew1_txt_Discount'); 

   txt_Discount.value = val(txt_Discount.value);
   hdn_Discount.value = val(txt_Discount.value);
   
   Calculate_GrandTotal();
} 
 //******************************** LocalChargeSearch ***********************************
 
function On_LocalCharge_Change()
{
   var hdn_LocalCharge= document.getElementById('WucGCNew1_hdn_LocalCharge');  
   var txt_LocalCharge= document.getElementById('WucGCNew1_txt_LocalCharge'); 

   txt_LocalCharge.value = val(txt_LocalCharge.value);
   hdn_LocalCharge.value = val(txt_LocalCharge.value);
   
   Calculate_GrandTotal();
}

   
 //******************************LodingChargeSearch*************************************
 
function On_LoadingCharge_Change()
{
    var txt_LoadingCharge = document.getElementById('WucGCNew1_txt_LoadingCharge');
    var hdn_LoadingCharge = document.getElementById('WucGCNew1_hdn_LoadingCharge');
    var hdn_Standard_HamaliCharge = document.getElementById('WucGCNew1_hdn_Standard_HamaliCharge');
    var MenuItemId = document.getElementById('WucGCNew1_hdn_MenuItemId').value;
    var ddl_Booking_Type  = document.getElementById('WucGCNew1_ddl_Booking_Type');
    
    var fromBranchID = window.parent.GetBkgBranchIDForCommodity();
    
    txt_LoadingCharge.value = val(txt_LoadingCharge.value);

    if (val(fromBranchID) != 53)
    {
        if(val(MenuItemId)!= 200 && val(txt_LoadingCharge.value) < val(hdn_Standard_HamaliCharge.value) && (val(ddl_Booking_Type.value) == 1))
        txt_LoadingCharge.value = val(hdn_Standard_HamaliCharge.value)
    }
    else
    {
        if(val(MenuItemId)!= 200 && val(txt_LoadingCharge.value) > val(hdn_Standard_HamaliCharge.value) && val(hdn_Standard_HamaliCharge.value) != 0 && (val(ddl_Booking_Type.value) == 1))
        txt_LoadingCharge.value = val(hdn_Standard_HamaliCharge.value)
    }
    
    hdn_LoadingCharge.value = val(txt_LoadingCharge.value);
    Calculate_GrandTotal();
}
    
 //******************************StationaryChargeSearch*************************************
 
function On_StationaryCharge_Change()
{
    var hdn_StationaryCharge = document.getElementById('WucGCNew1_hdn_StationaryCharge'); 
    var txt_StationaryCharge= document.getElementById('WucGCNew1_txt_StationaryCharge');
    var hdn_RateCard_BiltiCharges = document.getElementById('WucGCNew1_hdn_RateCard_BiltiCharges');
    var hdn_RateCard_MaxBiltiCharges = document.getElementById('WucGCNew1_hdn_RateCard_MaxBiltiCharges');  
    var MenuItemId = document.getElementById('WucGCNew1_hdn_MenuItemId').value;

    txt_StationaryCharge.value = val(txt_StationaryCharge.value);

    if(val(MenuItemId) != 200)
    {
        if (val(txt_StationaryCharge.value) < val(hdn_RateCard_BiltiCharges.value))
            txt_StationaryCharge.value = val(hdn_RateCard_BiltiCharges.value);

        if (val(hdn_RateCard_MaxBiltiCharges.value) > 0 && val(txt_StationaryCharge.value) > val(hdn_RateCard_MaxBiltiCharges.value))
            txt_StationaryCharge.value= val(hdn_RateCard_MaxBiltiCharges.value);
    }
    
    hdn_StationaryCharge.value = val(txt_StationaryCharge.value);
    Calculate_GrandTotal();
}  
    
 //*******************************FOVRiskChargeSearch************************************
 
function On_FOVRiskCharge_Change()
{
    var hdn_Standard_FOV = document.getElementById('WucGCNew1_hdn_Standard_FOV');
    var txt_FOVRiskCharge = document.getElementById('WucGCNew1_txt_FOVRiskCharge');
    var hdn_FOVRiskCharge = document.getElementById('WucGCNew1_hdn_FOVRiskCharge');
    var MenuItemId = document.getElementById('WucGCNew1_hdn_MenuItemId').value;

    txt_FOVRiskCharge.value = Math.round(val(txt_FOVRiskCharge.value));
    
    if(val(MenuItemId) != 200 && val(txt_FOVRiskCharge.value) < val(hdn_Standard_FOV.value))
        txt_FOVRiskCharge.value = Math.round(val(hdn_Standard_FOV.value));
     
    hdn_FOVRiskCharge.value = val(txt_FOVRiskCharge.value);
    Calculate_GrandTotal();
}
 //*******************************ODA Charge chnge************************************
 
function On_ODA_Change()
{
    var hdn_ODACharge = document.getElementById('WucGCNew1_hdn_ODACharge');
    var txt_ODACharge = document.getElementById('WucGCNew1_txt_ODACharge');

    txt_ODACharge.value = val(txt_ODACharge.value);
    hdn_ODACharge.value = val(txt_ODACharge.value);
    
    Calculate_GrandTotal();
}
 //***************************ToPayChargeSearch****************************************
 
function On_ToPayCharge_Change()
{   
    var hdn_ToPayCharge = document.getElementById('WucGCNew1_hdn_ToPayCharge');  
    var txt_ToPayCharge = document.getElementById('WucGCNew1_txt_ToPayCharge');  
    var chk_Is_ToPay_Charge_Require = document.getElementById('WucGCNew1_chk_Is_ToPay_Charge_Require'); 
    var hdn_RateCard_ToPayCharges = document.getElementById('WucGCNew1_hdn_RateCard_ToPayCharges');
    var MenuItemId = document.getElementById('WucGCNew1_hdn_MenuItemId').value;

    txt_ToPayCharge.value = val(txt_ToPayCharge.value);

    if(val(MenuItemId) != 200 && val(txt_ToPayCharge.value) < val(hdn_RateCard_ToPayCharges.value))
         txt_ToPayCharge.value = val(hdn_RateCard_ToPayCharges.value);

    if (chk_Is_ToPay_Charge_Require.checked == false)
        txt_ToPayCharge.value = val (0);
    
    hdn_ToPayCharge.value = val(txt_ToPayCharge.value);
    Calculate_GrandTotal();
}
    
//**************************Calculate_DDCharge*****************************************
 
function Calculate_DDCharge()
{
    var hdn_DDCharge= document.getElementById('WucGCNew1_hdn_DDCharge');  
    var txt_DDCharge= document.getElementById('WucGCNew1_txt_DDCharge');
    var hdn_RateCard_DDCharge_Rate = document.getElementById('WucGCNew1_hdn_RateCard_DDCharge_Rate');
    var hdn_Standard_DDCharge = document.getElementById('WucGCNew1_hdn_Standard_DDCharge');

    var chk_IsODA = document.getElementById('WucGCNew1_chk_IsODA');
    var txt_ChargeWeight = document.getElementById('WucGCNew1_txt_ChargeWeight');
    var ddl_DeliveryType = document.getElementById('WucGCNew1_ddl_Dly_Type');
    var hdn_Odachargesupto500Kg = document.getElementById('WucGCNew1_hdn_Odachargesupto500Kg');
    var hdn_Odachargesabove500Kg = document.getElementById('WucGCNew1_hdn_Odachargesabove500Kg');
    var hdn_CompanyParameter_Standard_FreightRatePer = document.getElementById('WucGCNew1_hdn_CompanyParameter_Standard_FreightRatePer');
    var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');
    var hdn_IsContractApplied  = document.getElementById('WucGCNew1_hdn_IsContractApplied');
    var GCId  = document.getElementById('WucGCNew1_hdn_GCId').value;
    var TempDDChargeRate = 0;

    if(hdn_IsContractApplied.value == "1")
        TempDDChargeRate = ContractFreightDetails[0][10];
    else
        TempDDChargeRate = val(hdn_RateCard_DDCharge_Rate.value);
        
    var DDCharge = 0;
    txt_DDCharge.value = val(txt_DDCharge.value);
    
   if(chk_IsAttached.checked == false && val(ddl_DeliveryType.value) == 2) // for door and oda
   {
        if(chk_IsODA.checked == true)
        {
           if (val(txt_ChargeWeight.value)<= 500)
                DDCharge = val(hdn_Odachargesupto500Kg.value);
           else if(val(txt_ChargeWeight.value) > 500)
                DDCharge  = val(hdn_Odachargesabove500Kg.value);
        }
        else
        {
            DDCharge = val(txt_ChargeWeight.value) * val(TempDDChargeRate) / val(hdn_CompanyParameter_Standard_FreightRatePer.value);
        }
   }
   else
    DDCharge = 0;
  
    txt_DDCharge.value = val(DDCharge);
    hdn_DDCharge.value = val(DDCharge);
    hdn_Standard_DDCharge.value = val(DDCharge);

    Calculate_GrandTotal();
}
//**************************DDCharge*****************************************
function On_DDCharge_Change()
{
    var hdn_DDCharge = document.getElementById('WucGCNew1_hdn_DDCharge');  
    var txt_DDCharge = document.getElementById('WucGCNew1_txt_DDCharge');
    var hdn_Standard_DDCharge = document.getElementById('WucGCNew1_hdn_Standard_DDCharge');
    var MenuItemId = document.getElementById('WucGCNew1_hdn_MenuItemId').value;

    txt_DDCharge.value = val(txt_DDCharge.value);

    if (val(MenuItemId) != 200 && val(MenuItemId) != 194 && val(txt_DDCharge.value) < val(hdn_Standard_DDCharge.value))
         txt_DDCharge.value = val(hdn_Standard_DDCharge.value);
    
    txt_DDCharge.value = val(txt_DDCharge.value);
    hdn_DDCharge.value = val(txt_DDCharge.value);
    
    Calculate_GrandTotal();
}
   
 //***************************IBA(DACC)ChargeSearch****************************************
function On_DACCCharge_Change()
{
    var hdn_DACCCharge = document.getElementById('WucGCNew1_hdn_DACCCharge');  
    var txt_DACCCharge = document.getElementById('WucGCNew1_txt_DACCCharge');  
    var hdn_RateCard_DACCCharges = document.getElementById('WucGCNew1_hdn_RateCard_DACCCharges');
    var MenuItemId = document.getElementById('WucGCNew1_hdn_MenuItemId').value;

    txt_DACCCharge.value = val(txt_DACCCharge.value);

    if(val(MenuItemId) != 200 && val(txt_DACCCharge.value)< val(hdn_RateCard_DACCCharges.value))
        txt_DACCCharge.value = val(hdn_RateCard_DACCCharges.value);          

    hdn_DACCCharge.value = val(txt_DACCCharge.value);        
    Calculate_GrandTotal();
}
//*************************NFormChargeSearch******************************************

function On_NFormCharge_Change()
{
    var hdn_NFormCharge = document.getElementById('WucGCNew1_hdn_NFormCharge');
    var txt_NFormCharge = document.getElementById('WucGCNew1_txt_NFormCharge');

    txt_NFormCharge.value = val(txt_NFormCharge.value);
    hdn_NFormCharge.value = val(txt_NFormCharge.value);

    Calculate_GrandTotal();
}    
//***************************RebbokChargeSearch****************************************
function On_ReBookGCAmount_Change()
{
    var txt_ReBookGCAmount = document.getElementById('WucGCNew1_txt_ReBookGCAmount');        
    var hdn_ReBookGCAmount = document.getElementById('WucGCNew1_hdn_ReBookGCAmount');        
//        var hdn_ReBookGC_SubTotal = document.getElementById('wucGC1_hdn_ReBookGC_SubTotal');

    txt_ReBookGCAmount.value = val(txt_ReBookGCAmount.value);

//        if (val(txt_ReBookGCAmount.value) < val(hdn_ReBookGC_SubTotal.value))
//        {
//            txt_ReBookGCAmount.value = val(hdn_ReBookGC_SubTotal.value);
//        }
    hdn_ReBookGCAmount.value = val(txt_ReBookGCAmount.value);
    Calculate_GrandTotal();
}
//**************************LengthChargeSearch*****************************************
function On_LengthCharge_Change()
{
    var txt_LengthCharge = document.getElementById('WucGCNew1_txt_LengthCharge');        
    var hdn_LengthCharge = document.getElementById('WucGCNew1_hdn_LengthCharge');                
    var hdn_Standard_LengthCharge = document.getElementById('WucGCNew1_hdn_Standard_LengthCharge');
    var MenuItemId = document.getElementById('WucGCNew1_hdn_MenuItemId').value;

    txt_LengthCharge.value = val(txt_LengthCharge.value);

    if(val(MenuItemId) != 200 && val(txt_LengthCharge.value) < val(hdn_Standard_LengthCharge.value))
        txt_LengthCharge.value = val(hdn_Standard_LengthCharge.value);
    
    hdn_LengthCharge.value = val(txt_LengthCharge.value);
    Calculate_GrandTotal();
}
//**************************AOC Change*****************************************
function On_AOC_Change()
{
    var txt_AOC = document.getElementById('WucGCNew1_txt_AOC');        
    var hdn_AOC = document.getElementById('WucGCNew1_hdn_AOC');                

    txt_AOC.value = val(txt_AOC.value);
    hdn_AOC.value = val(txt_AOC.value);
    Calculate_GrandTotal();
}
//***************************UnloadingChargeSearch****************************************
function On_UnloadingCharge_Change()
{
    var txt_UnloadingCharge = document.getElementById('WucGCNew1_txt_UnloadingCharge');
    var hdn_UnloadingCharge = document.getElementById('WucGCNew1_hdn_UnloadingCharge');

    txt_UnloadingCharge.value = val(txt_UnloadingCharge.value);
    hdn_UnloadingCharge.value = val(txt_UnloadingCharge.value);

    Calculate_GrandTotal();
}
//*******************************************************************
function PaymentType_Change_Confirmation()
{  
   var hdn_OldPaymentType  = document.getElementById('WucGCNew1_hdn_OldPaymentType');
   var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');

   GetRateContract();

   if (ddl_PaymentType.value == 5)
   {
        if (confirm("Are you Sure you want to change payment type to FOC?")==false)
        {
            ddl_PaymentType.value = hdn_OldPaymentType.value; 
            return ;
        }
        else
            On_PaymentType_Change();
    }
    else
    {
        On_PaymentType_Change();
    }
    
    Only_On_PaymentType_Change();

    
    
}

//*******************************************************************

function On_PaymentType_Change()
{
   var hdn_Mode = document.getElementById('WucGCNew1_hdn_Mode');
   
   if (val(hdn_Mode.value) == 4)
    {
        SetValue_On_PaymentType_Change();
        EnableDisable_On_PaymentType_Change();
    }
     
   if (val(hdn_Mode.value) != 4)
    {
        SetValue_On_PaymentType_Change();
        EnableDisable_On_PaymentType_Change();
        Set_ServiceTaxPayableBy();
        CalculateDiscount();
        Calculate_GrandTotal();
    }
}
    
function SetValue_On_PaymentType_Change()
{
    var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');        
    var hdn_OldPaymentType  = document.getElementById('WucGCNew1_hdn_OldPaymentType');   
    var ddl_LengthChargeHead = document.getElementById('WucGCNew1_ddl_LengthChargeHead');
    var txt_FreightRate = document.getElementById('WucGCNew1_txt_FreightRate');
    var hdn_FreightRate = document.getElementById('WucGCNew1_hdn_FreightRate');
    
    var txt_Freight = document.getElementById('WucGCNew1_txt_Freight');
    var hdn_Freight = document.getElementById('WucGCNew1_hdn_Freight');
    var txt_Discount = document.getElementById('WucGCNew1_txt_Discount');
    var hdn_Discount = document.getElementById('WucGCNew1_hdn_Discount');
    var hdn_DiscountId = document.getElementById('WucGCNew1_hdn_DiscountId');
    var txt_LocalCharge = document.getElementById('WucGCNew1_txt_LocalCharge');
    var hdn_LocalCharge = document.getElementById('WucGCNew1_hdn_LocalCharge');
    var txt_LoadingCharge = document.getElementById('WucGCNew1_txt_LoadingCharge');
    var hdn_LoadingCharge = document.getElementById('WucGCNew1_hdn_LoadingCharge');
    var txt_StationaryCharge = document.getElementById('WucGCNew1_txt_StationaryCharge');
    var hdn_StationaryCharge = document.getElementById('WucGCNew1_hdn_StationaryCharge');
    var txt_FOVRiskCharge = document.getElementById('WucGCNew1_txt_FOVRiskCharge');
    var hdn_FOVRiskCharge = document.getElementById('WucGCNew1_hdn_FOVRiskCharge');
    var txt_ODACharge = document.getElementById('WucGCNew1_txt_ODACharge');
    var hdn_ODACharge = document.getElementById('WucGCNew1_hdn_ODACharge');
    var txt_ToPayCharge = document.getElementById('WucGCNew1_txt_ToPayCharge');
    var hdn_ToPayCharge = document.getElementById('WucGCNew1_hdn_ToPayCharge');
    var txt_DDCharge = document.getElementById('WucGCNew1_txt_DDCharge');
    var hdn_DDCharge = document.getElementById('WucGCNew1_hdn_DDCharge');
    var lbl_OtherChargesValue = document.getElementById('WucGCNew1_lbl_OtherChargesValue');
    var hdn_OtherCharge = document.getElementById('WucGCNew1_hdn_OtherCharge');
    var txt_ReBookGCAmount = document.getElementById('WucGCNew1_txt_ReBookGCAmount');        
    var hdn_ReBookGCAmount = document.getElementById('WucGCNew1_hdn_ReBookGCAmount');
    var txt_NFormCharge = document.getElementById('WucGCNew1_txt_NFormCharge');        
    var hdn_NFormCharge = document.getElementById('WucGCNew1_hdn_NFormCharge');
    var txt_LengthCharge = document.getElementById('WucGCNew1_txt_LengthCharge');
    var hdn_LengthCharge = document.getElementById('WucGCNew1_hdn_LengthCharge');        
    var txt_UnloadingCharge = document.getElementById('WucGCNew1_txt_UnloadingCharge');   
    var hdn_UnloadingCharge = document.getElementById('WucGCNew1_hdn_UnloadingCharge');  
    var lbl_ReBookOctroiAmountValue = document.getElementById('WucGCNew1_lbl_ReBookOctroiAmountValue');
    var hdn_ReBookGC_OctroiAmount = document.getElementById('WucGCNew1_hdn_ReBookGC_OctroiAmount'); 
    var hdn_TotalGCAmount = document.getElementById('WucGCNew1_hdn_TotalGCAmount');        

    var txt_Advance = document.getElementById('WucGCNew1_txt_Advance');        
    var hdn_Advance = document.getElementById('WucGCNew1_hdn_Advance');        
    var txt_CashAmount = document.getElementById('WucGCNew1_txt_CashAmount');
    var hdn_CashAmount = document.getElementById('WucGCNew1_hdn_CashAmount');        
    var txt_ChequeAmount = document.getElementById('WucGCNew1_txt_ChequeAmount');
    var hdn_ChequeAmount = document.getElementById('WucGCNew1_hdn_ChequeAmount');
    var txt_ChequeNo = document.getElementById('WucGCNew1_txt_ChequeNo');
    var txt_BankName = document.getElementById('WucGCNew1_txt_BankName');
    
    var hdn_Standard_FreightRate = document.getElementById('WucGCNew1_hdn_Standard_FreightRate');
    var hdn_RateCard_LocalCharge = document.getElementById('WucGCNew1_hdn_RateCard_LocalCharge');
    var hdn_Standard_HamaliCharge = document.getElementById('WucGCNew1_hdn_Standard_HamaliCharge');
    var hdn_RateCard_BiltiCharges = document.getElementById('WucGCNew1_hdn_RateCard_BiltiCharges');
    var hdn_RateCard_ToPayCharges = document.getElementById('WucGCNew1_hdn_RateCard_ToPayCharges');
    var hdn_Standard_DDCharge = document.getElementById('WucGCNew1_hdn_Standard_DDCharge');
    var hdn_Standard_FOV = document.getElementById('WucGCNew1_hdn_Standard_FOV');
    var lnk_OtherCharges = document.getElementById('WucGCNew1_lnk_OtherCharges');
    var lbl_OtherCharges = document.getElementById('WucGCNew1_lbl_OtherCharges');
    var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');
    
    var txt_BillingParty = document.getElementById('WucGCNew1_txt_BillingParty');
    var hdn_BillingPartyId = document.getElementById('WucGCNew1_hdn_BillingPartyId');
    var ddl_BillingHierarchy = document.getElementById('WucGCNew1_ddl_BillingHierarchy');
    var txt_BillingLocation = document.getElementById('WucGCNew1_txt_BillingLocation');
    var hdn_BillingLocationId = document.getElementById('WucGCNew1_hdn_BillingLocationId');
    var txt_BillingRemark = document.getElementById('WucGCNew1_txt_BillingRemark');
    var hdn_IsContractApplied = document.getElementById('WucGCNew1_hdn_IsContractApplied');
    var hdn_IsServiceTaxApplForBillParty = document.getElementById('WucGCNew1_hdn_IsServiceTaxApplForBillParty');

    var hdn_BillingParty = document.getElementById('WucGCNew1_hdn_BillingParty');
    var hdn_BillingHierarchy = document.getElementById('WucGCNew1_hdn_BillingHierarchy');
    var hdn_BillingLocation = document.getElementById('WucGCNew1_hdn_BillingLocation');
    var hdn_BillingRemark = document.getElementById('WucGCNew1_hdn_BillingRemark');
    var ddl_Booking_Type = document.getElementById('WucGCNew1_ddl_Booking_Type');
  
    var chk_Is_ToPay_Charge_Require = document.getElementById('WucGCNew1_chk_Is_ToPay_Charge_Require'); 
    
    var txt_Paid_Freight_Pending_Person_Name =  document.getElementById('WucGCNew1_txt_Paid_Freight_Pending_Person_Name');
    var txt_Paid_Freight_Pending_Person_Mobile =  document.getElementById('WucGCNew1_txt_Paid_Freight_Pending_Person_Mobile');
    
    var hdn_RateContractId = document.getElementById('WucGCNew1_hdn_RateContractId');
    
    hdn_OldPaymentType.value = ddl_PaymentType.value;

    if (val(ddl_PaymentType.value)== 5 || chk_IsAttached.checked == true)     // foc
    {
        ddl_LengthChargeHead.value = 0;
        txt_FreightRate.value = val(0);
        txt_Freight.value = val(0);
        txt_Discount.value = val(0);
        if(chk_IsAttached.checked == false)
        {
            txt_LocalCharge.value = val(0);
            lbl_OtherChargesValue.innerHTML = val(0);
            txt_DDCharge.value = val(0);

            hdn_LocalCharge.value = val(0);
            hdn_DDCharge.value = val(0);
            hdn_OtherCharge.value = val(0);

            txt_LocalCharge.disabled = true;
            txt_DDCharge.disabled = true;
        }
        txt_LoadingCharge.value = val(0);
        txt_StationaryCharge.value = val(0);
        txt_FOVRiskCharge.value = val(0);
        txt_ToPayCharge.value = val(0);
        txt_NFormCharge.value = val(0);
        txt_ODACharge.value = val(0);
        txt_LengthCharge.value = val(0);
        txt_UnloadingCharge.value = val(0);
        txt_ReBookGCAmount.value = val(0);
        lbl_ReBookOctroiAmountValue.innerHTML = val(0);

        txt_Advance.value = val(0);
        txt_CashAmount.value = val(0);
        txt_ChequeAmount.value = val(0);
        txt_ChequeNo.value = val(0); 
        txt_BankName.value = ""

        hdn_Freight.value = val(0);
        hdn_Discount.value = val(0);
        hdn_DiscountId.value = val(0);
        hdn_FreightRate.value = val(0);
        hdn_LoadingCharge.value = val(0);
        hdn_StationaryCharge.value = val(0);
        hdn_FOVRiskCharge.value = val(0);
        hdn_ToPayCharge.value = val(0);
        hdn_NFormCharge.value = val(0);
        hdn_ODACharge.value = val(0);
        hdn_LengthCharge.value = val(0);
        hdn_UnloadingCharge.value = val(0); 
        hdn_ReBookGCAmount.value = val(0);
        hdn_ReBookGC_OctroiAmount.value = val(0);

        hdn_Advance.value = val(0);
        hdn_CashAmount.value = val(0);
        hdn_ChequeAmount.value = val(0); 

        ddl_LengthChargeHead.disabled = true;
        txt_FreightRate.disabled = true;
        txt_Freight.disabled = true;
        txt_LoadingCharge.disabled = true;
        txt_ToPayCharge.disabled = true;
        txt_NFormCharge.disabled = true;
//        txt_LengthCharge.disabled = true;
        txt_UnloadingCharge.disabled = true;
        txt_ReBookGCAmount.disabled = true;
        
        txt_Advance.disabled = true;
        txt_CashAmount.disabled = true;
        txt_ChequeAmount.disabled = true;
        txt_ChequeNo.disabled = true;
        txt_BankName.disabled = true;
    }
    else
    {
        ddl_LengthChargeHead.disabled = false;
        txt_FreightRate.disabled = false;
        txt_Freight.disabled = false;
        txt_LocalCharge.disabled = false;
        txt_LoadingCharge.disabled = false;

        if(val(ddl_PaymentType.value) != 1)
            txt_ToPayCharge.disabled = false;
        else
            txt_ToPayCharge.disabled = true;
        
        txt_NFormCharge.disabled = false;
//        txt_LengthCharge.disabled = false;
        txt_UnloadingCharge.disabled = false;
        txt_ReBookGCAmount.disabled = false;

        txt_Advance.disabled = false;
        txt_CashAmount.disabled = false;
        txt_ChequeAmount.disabled = false;
        txt_BankName.disabled = false;
        txt_ChequeNo.disabled = false;
        
//        if(val(txt_FreightRate.value) < val(hdn_Standard_FreightRate.value))
//        {
//            txt_FreightRate.value = val (hdn_Standard_FreightRate.value);
//            hdn_FreightRate.value = val (hdn_Standard_FreightRate.value);
//        }
        txt_FreightRate.value = val(hdn_FreightRate.value);

        if(val(txt_LocalCharge.value) < val(hdn_RateCard_LocalCharge.value))
        {
            txt_LocalCharge.value = val (hdn_RateCard_LocalCharge.value);
            hdn_LocalCharge.value = val (hdn_RateCard_LocalCharge.value);
        }
        if (val(txt_LoadingCharge.value) < val(hdn_Standard_HamaliCharge.value))
        {
            txt_LoadingCharge.value = val (hdn_Standard_HamaliCharge.value);
            hdn_LoadingCharge.value = val (hdn_Standard_HamaliCharge.value);
        }
        if (val(txt_StationaryCharge.value) < val(hdn_RateCard_BiltiCharges.value)  && (val(ddl_Booking_Type.value) == 1))
        {
            txt_StationaryCharge.value = val(hdn_RateCard_BiltiCharges.value);
            hdn_StationaryCharge.value = val(hdn_RateCard_BiltiCharges.value);
        }
        if (val(txt_ToPayCharge.value) < val(hdn_RateCard_ToPayCharges.value))
        {
            txt_ToPayCharge.value = val(hdn_RateCard_ToPayCharges.value);
            hdn_ToPayCharge.value = val(hdn_RateCard_ToPayCharges.value);
        }
        if (chk_Is_ToPay_Charge_Require.checked == false)
        {
            txt_ToPayCharge.value = val (0);
            hdn_ToPayCharge.value = val (0);
        }
        if(val(txt_DDCharge.value) < val(hdn_Standard_DDCharge.value))
        {
            txt_DDCharge.value = val(hdn_Standard_DDCharge.value);
            hdn_DDCharge.value = val(hdn_Standard_DDCharge.value);
        }
        if(val(txt_FOVRiskCharge.value) < val(hdn_Standard_FOV.value) && val(hdn_RateContractId.value) <= 0 )
        {
            txt_FOVRiskCharge.value = Math.round(val(hdn_Standard_FOV.value));
            hdn_FOVRiskCharge.value = Math.round(val(hdn_Standard_FOV.value));
        }
    }
   
    if (val(ddl_PaymentType.value) == 1 && chk_IsAttached.checked == false)    // topay
    {
        if (val(hdn_TotalGCAmount.value) < val(txt_Advance.value))
        {
            txt_Advance.value=  val(0);
            hdn_Advance.value=  val(0);
        }

        txt_CashAmount.value=  val(0);
        hdn_CashAmount.value=  val(0);

        if (val(txt_Advance.value) < val(txt_ChequeAmount.value))
        {
            txt_ChequeAmount.value = val(0);
            hdn_ChequeAmount.value = val(0);

            txt_ChequeNo.value="";
            txt_BankName.value="";
        }
        if (val(txt_ToPayCharge.value) < val(hdn_RateCard_ToPayCharges.value))
        {
            txt_ToPayCharge.value = val(hdn_RateCard_ToPayCharges.value);
            hdn_ToPayCharge.value = val(hdn_RateCard_ToPayCharges.value);
        }
    }
    else  
    {
        txt_ToPayCharge.value = val(0);
        hdn_ToPayCharge.value = val(0);
    }

    if(hdn_IsContractApplied.value == "1" && val(ddl_PaymentType.value)== 3 && chk_IsAttached.checked == false)
    {
        hdn_BillingPartyId.value = ContractFreightDetails[0][24];
        txt_BillingParty.value = ContractFreightDetails[0][25];
        hdn_BillingParty.value = ContractFreightDetails[0][25];
        ddl_BillingHierarchy.value = ContractFreightDetails[0][26];
        hdn_BillingHierarchy.value = ContractFreightDetails[0][26];
        hdn_BillingLocationId.value = ContractFreightDetails[0][27];
        txt_BillingLocation.value = ContractFreightDetails[0][28];
        hdn_BillingLocation.value = ContractFreightDetails[0][28];
        if(ContractFreightDetails[0][31] == true)
            hdn_IsServiceTaxApplForBillParty.value = '1'; 
        else
            hdn_IsServiceTaxApplForBillParty.value = '0';
    }
    
    if (val(ddl_PaymentType.value) != 4 && chk_IsAttached.checked == false)    // topay
    {
        txt_Paid_Freight_Pending_Person_Name.value = '';
        txt_Paid_Freight_Pending_Person_Mobile.value = '';
    }
    else
    {
        txt_CashAmount.value=  val(0);
        hdn_CashAmount.value=  val(0);
        txt_ChequeAmount.value = val(0);
        hdn_ChequeAmount.value = val(0);
        txt_ChequeNo.value="";
        txt_BankName.value="";
    
    }
    
    EnableDisable_On_LengthCharge_Change();
    On_BillingHierarchy_Load();
    Calculate_GrandTotal();
}

function Only_On_PaymentType_Change()
{
    var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');
    var hdn_defaultPod = document.getElementById('WucGCNew1_hdn_defaultPod');
    var GCId = document.getElementById('WucGCNew1_hdn_GCId').value;
    var chk_PodRequire = document.getElementById('WucGCNew1_chk_PodRequire');

    if(val(ddl_PaymentType.value) == 3 ) // TBB
    {
       chk_PodRequire.checked = true;
    }
    else
    {
        if(GCId <= 0)
        {
            if(hdn_defaultPod.value == '1')
                chk_PodRequire.checked = true;
        }
    }
}

function EnableDisable_On_PaymentType_Change()
{ 
    var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');
    var chk_IsMultipleBilling = document.getElementById('WucGCNew1_chk_IsMultipleBilling');
    var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');

    var txt_ODACharge = document.getElementById('WucGCNew1_txt_ODACharge');
    var txt_LengthCharge = document.getElementById('WucGCNew1_txt_LengthCharge');

    var tr_cashamountchqamount = document.getElementById('WucGCNew1_tr_cashamountchqamount');
    var tr_PaymentDetails = document.getElementById('WucGCNew1_tr_PaymentDetails');
    var tr_cheque_Details = document.getElementById('WucGCNew1_tr_cheque_Details');
    var tr_cheque_DetailsBank = document.getElementById('WucGCNew1_tr_cheque_DetailsBank');
    var tr_multiple_billing_details = document.getElementById('WucGCNew1_tr_multiple_billing_details');
    var hdn_Is_Multiple_Party_Billing_Allowed = document.getElementById('WucGCNew1_hdn_Is_Multiple_Party_Billing_Allowed');
    var chk_Is_Multiple_Location_Billing_Allowed = document.getElementById('WucGCNew1_chk_Is_Multiple_Location_Billing_Allowed');
    var tr_billing_details = document.getElementById('WucGCNew1_tr_billing_details');
//    var chk_PodRequire = document.getElementById('WucGCNew1_chk_PodRequire');

    var txt_BillingParty = document.getElementById('WucGCNew1_txt_BillingParty');
    var hdn_BillingPartyId = document.getElementById('WucGCNew1_hdn_BillingPartyId');
    var ddl_BillingHierarchy = document.getElementById('WucGCNew1_ddl_BillingHierarchy');
    var txt_BillingLocation = document.getElementById('WucGCNew1_txt_BillingLocation');
    var hdn_BillingLocationId = document.getElementById('WucGCNew1_hdn_BillingLocationId');
    var txt_BillingRemark = document.getElementById('WucGCNew1_txt_BillingRemark');

    var hdn_BillingParty = document.getElementById('WucGCNew1_hdn_BillingParty');
    var hdn_BillingHierarchy = document.getElementById('WucGCNew1_hdn_BillingHierarchy');
    var hdn_BillingLocation = document.getElementById('WucGCNew1_hdn_BillingLocation');
    var hdn_BillingRemark = document.getElementById('WucGCNew1_hdn_BillingRemark');

    var txt_Advance = document.getElementById('WucGCNew1_txt_Advance');
    var hdn_Advance = document.getElementById('WucGCNew1_hdn_Advance');
    var txt_CashAmount = document.getElementById('WucGCNew1_txt_CashAmount');
    var hdn_CashAmount = document.getElementById('WucGCNew1_hdn_CashAmount');
    var txt_ChequeAmount = document.getElementById('WucGCNew1_txt_ChequeAmount');
    var hdn_ChequeAmount = document.getElementById('WucGCNew1_hdn_ChequeAmount');
    var txt_ChequeNo = document.getElementById('WucGCNew1_txt_ChequeNo');
    var txt_BankName = document.getElementById('WucGCNew1_txt_BankName');
    var lbl_Advance = document.getElementById('WucGCNew1_lbl_Advance');
    var txt_ToPayCharge = document.getElementById('WucGCNew1_txt_ToPayCharge');
    var hdn_ToPayCharge = document.getElementById('WucGCNew1_hdn_ToPayCharge');
    var lnk_OtherCharges = document.getElementById('WucGCNew1_lnk_OtherCharges');
    var lbl_OtherCharges = document.getElementById('WucGCNew1_lbl_OtherCharges');
//    var hdn_defaultPod = document.getElementById('WucGCNew1_hdn_defaultPod');
//    var GCId = document.getElementById('WucGCNew1_hdn_GCId').value;
    var IsContractApplied = document.getElementById('WucGCNew1_hdn_IsContractApplied').value;

    var tr_Paid_Freight_Pending_Reason = document.getElementById('WucGCNew1_tr_Paid_Freight_Pending_Reason');
    var tr_Paid_Freight_Pending_Person_Details = document.getElementById('WucGCNew1_tr_Paid_Freight_Pending_Person_Details');

    lnk_OtherCharges.style.display = 'inline';
    lbl_OtherCharges.style.display = 'none';
    tr_multiple_billing_details.style.display = 'none';
    chk_IsMultipleBilling.disabled = true; 


    if(val(ddl_PaymentType.value) == 3 ) // TBB
    {
        if(val(hdn_Is_Multiple_Party_Billing_Allowed.value) == 1)
        {
            tr_multiple_billing_details.style.display = 'inline';
            chk_IsMultipleBilling.disabled = false; 
        }
        
//        tr_PaymentDetails.style.display = 'none';
        tr_cheque_Details.style.display = 'none';
        tr_cheque_DetailsBank.style.display = 'none';
        tr_Paid_Freight_Pending_Reason.style.display = 'none';
        tr_Paid_Freight_Pending_Person_Details.style.display = 'none';
        
        if(chk_IsMultipleBilling.checked == false)
            tr_billing_details.style.display = 'inline';
        else
            tr_billing_details.style.display = 'none';
        
        txt_Advance.value = val(0);
        hdn_Advance.value = val(0);
        txt_CashAmount.value = val(0);
        hdn_CashAmount.value = val(0);
        txt_ChequeAmount.value = val(0);
        hdn_ChequeAmount.value = val(0);
        txt_ChequeNo.value = val(0);
        txt_BankName.value = '';
        
        tr_Paid_Freight_Pending_Reason.style.display = 'none';
        tr_Paid_Freight_Pending_Person_Details.style.display = 'none';
    
        if(chk_Is_Multiple_Location_Billing_Allowed.checked == false)
            ddl_BillingHierarchy.value = 'BO';
    }
    else
    {
        chk_IsMultipleBilling.disabled = true;
        chk_IsMultipleBilling.checked = false;
//        tr_PaymentDetails.style.display = 'inline';
        tr_cheque_Details.style.display = 'inline';
        tr_cheque_DetailsBank.style.display = 'inline';

        tr_multiple_billing_details.style.display = 'none';
        tr_billing_details.style.display = 'none';

        txt_BillingParty.value = '';
        hdn_BillingParty.value = '';
        hdn_BillingPartyId.value = val(0);
        ddl_BillingHierarchy.value = '0';
        hdn_BillingHierarchy.value = '0';
        txt_BillingLocation.value = '';
        hdn_BillingLocation.value = '';
        hdn_BillingLocationId.value = val(0);
        txt_BillingRemark.value = '';
        hdn_BillingRemark.value = '';
    }

    if(val(ddl_PaymentType.value) == 1 && chk_IsAttached.checked == false) // topay
    {
        txt_Advance.disabled = false;
        txt_ToPayCharge.disabled = false;
        txt_Advance.style.visibility = 'visible';
        lbl_Advance.style.visibility = 'visible';
        
        tr_cheque_Details.style.display = 'none';
        tr_cheque_DetailsBank.style.display = 'none';
        
    }
    else
    {
        txt_Advance.value = val(0);
        hdn_Advance.value = val(0);
        txt_ToPayCharge.value = val(0);
        hdn_ToPayCharge.value = val(0);
        
        txt_Advance.style.visibility = 'hidden';
        lbl_Advance.style.visibility = 'hidden';
        txt_Advance.disabled = true;
        txt_ToPayCharge.disabled = true;
    }
    
    if(val(ddl_PaymentType.value) == 5)// || chk_IsAttached.checked == true
    {
        lnk_OtherCharges.style.display = 'none';
        lbl_OtherCharges.style.display = 'inline';
        
        txt_ODACharge.disabled = true;
        txt_LengthCharge.disabled = true;
    }
    else
    {
        txt_ODACharge.disabled = false;
        txt_LengthCharge.disabled = false;
    }

    tr_cashamountchqamount.style.display = 'none';
    tr_Paid_Freight_Pending_Reason.style.display = 'none';
    tr_Paid_Freight_Pending_Person_Details.style.display = 'none';

    if(val(ddl_PaymentType.value) == 2)
        tr_cashamountchqamount.style.display = 'inline';


    if(val(ddl_PaymentType.value) == 4)
    {
        tr_cheque_Details.style.display = 'none';
        tr_cheque_DetailsBank.style.display = 'none';
    
        tr_Paid_Freight_Pending_Reason.style.display = 'inline';
        tr_Paid_Freight_Pending_Person_Details.style.display = 'inline';
    }
            
    On_BillingHierarchy_Load();
    
    if(IsContractApplied == "1")    
        Enable_Disable_controls_On_ContractChange();
        
}
  
//*******************************************************************
function Set_Payment_Details()
{
    var hdn_ChequeAmount = document.getElementById('WucGCNew1_hdn_ChequeAmount');  
    var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');
    var txt_BankName = document.getElementById('WucGCNew1_txt_BankName');
    var txt_ChequeNo =  document.getElementById('WucGCNew1_txt_ChequeNo');  

    if (val(ddl_PaymentType.value) == 1)
    {
        document.getElementById('WucGCNew1_txt_Advance').disabled = false;
        document.getElementById('WucGCNew1_txt_CashAmount').disabled = false;
        document.getElementById('WucGCNew1_txt_ChequeAmount').disabled = false;
        txt_ChequeNo.disabled = true;
        txt_BankName.disabled = true;
    }
    else
    {
        document.getElementById('WucGCNew1_txt_Advance').disabled = true;
        document.getElementById('WucGCNew1_txt_CashAmount').disabled = false;
        document.getElementById('WucGCNew1_txt_ChequeAmount').disabled = false;
        txt_ChequeNo.disabled = true;
        txt_BankName.disabled = false;
    }
   if (val(hdn_ChequeAmount.value) > 0)
   {
        document.getElementById('WucGCNew1_tr_cheque_Details').style.display = 'inline';
        document.getElementById('WucGCNew1_tr_cheque_DetailsBank').style.display = 'inline';
        txt_ChequeNo.disabled = false;
        txt_BankName.disabled = false;
   }
   else
   {
        txt_ChequeNo.value = '';
        txt_BankName.value = '';
        txt_ChequeNo.disabled = true;
        txt_BankName.disabled = true;
        document.getElementById('WucGCNew1_tr_cheque_Details').style.display = 'none';
        document.getElementById('WucGCNew1_tr_cheque_DetailsBank').style.display = 'none';
   }
} 

 //*******************************************************************

function On_AdvanceAmount_Change()
{
    var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');
    var lbl_TotalGCAmountValue = document.getElementById('WucGCNew1_lbl_TotalGCAmountValue');
    var hdn_TotalGCAmount = document.getElementById('WucGCNew1_hdn_TotalGCAmount');
    var txt_Advance = document.getElementById('WucGCNew1_txt_Advance');
    var hdn_Advance = document.getElementById('WucGCNew1_hdn_Advance');
    var txt_CashAmount = document.getElementById('WucGCNew1_txt_CashAmount');
    var hdn_CashAmount = document.getElementById('WucGCNew1_hdn_CashAmount');
    var txt_ChequeAmount = document.getElementById('WucGCNew1_txt_ChequeAmount');
    var hdn_ChequeAmount = document.getElementById('WucGCNew1_hdn_ChequeAmount');
    var txt_ChequeNo  =  document.getElementById('WucGCNew1_txt_ChequeNo');
    var txt_BankName= document.getElementById('WucGCNew1_txt_BankName');

    var Amount=  val(0);
    
    txt_Advance.value = val(txt_Advance.value); 

    if (val(ddl_PaymentType.value) == 2) // paid
        Amount = val(hdn_TotalGCAmount.value); 
    else if(val(ddl_PaymentType.value) == 1) // to pay
        Amount = val(txt_Advance.value); 

    if (val(txt_Advance.value) > val(hdn_TotalGCAmount.value))
        txt_Advance.value = val(hdn_TotalGCAmount.value);

    if (val(txt_Advance.value) < val(txt_ChequeAmount.value))
    {
        txt_ChequeAmount.value = val(0);
        hdn_ChequeAmount.value = val(0);
        txt_ChequeNo.value = "";
        txt_BankName.value = "";
    }
    else
    {
        txt_CashAmount.value = val(txt_Advance.value) - val(txt_ChequeAmount.value);                
    }
    hdn_Advance.value = val(txt_Advance.value);
    hdn_CashAmount.value = val(txt_CashAmount.value);
    hdn_ChequeAmount.value = val(txt_ChequeAmount.value);
}
//*******************************************************************

function On_CashAmount_Change()
{
    var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');
    var hdn_TotalGCAmount = document.getElementById('WucGCNew1_hdn_TotalGCAmount');
    var txt_Advance = document.getElementById('WucGCNew1_txt_Advance');
    var hdn_Advance = document.getElementById('WucGCNew1_hdn_Advance');
    var txt_CashAmount = document.getElementById('WucGCNew1_txt_CashAmount');
    var hdn_CashAmount = document.getElementById('WucGCNew1_hdn_CashAmount');
    var txt_ChequeAmount = document.getElementById('WucGCNew1_txt_ChequeAmount');
    var hdn_ChequeAmount = document.getElementById('WucGCNew1_hdn_ChequeAmount');

    var Amount = val(0);
    txt_CashAmount.value = val(txt_CashAmount.value); 

    if(val(ddl_PaymentType.value) == 2) // paid
        Amount = val(hdn_TotalGCAmount.value); 
    else if(val(ddl_PaymentType.value) == 1 ) // to pay
        Amount = val(txt_Advance.value); 
    
    if (Amount < val(txt_CashAmount.value))
        txt_CashAmount.value = Amount;
    else if (Amount < val(txt_ChequeAmount.value))
        txt_ChequeAmount.value = Amount ;

    if (val(txt_ChequeAmount.value) <= 0 && val(txt_CashAmount.value) <= 0)
        txt_CashAmount.value = val(Amount);

    txt_ChequeAmount.value = Amount - val(txt_CashAmount.value);
    hdn_ChequeAmount.value = val(txt_ChequeAmount.value);
    hdn_CashAmount.value = val(txt_CashAmount.value);
}
//*******************************************************************

function On_ChequeAmount_Change()
{
    var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');       
    var hdn_TotalGCAmount = document.getElementById('WucGCNew1_hdn_TotalGCAmount');       
    var txt_Advance = document.getElementById('WucGCNew1_txt_Advance');
    var txt_CashAmount = document.getElementById('WucGCNew1_txt_CashAmount');
    var hdn_CashAmount = document.getElementById('WucGCNew1_hdn_CashAmount');       
    var hdn_ChequeAmount = document.getElementById('WucGCNew1_hdn_ChequeAmount');
    var txt_ChequeAmount = document.getElementById('WucGCNew1_txt_ChequeAmount'); 
    
    var Amount=  val(0);
    txt_ChequeAmount.value = val(txt_ChequeAmount.value); 
    
    if (val(ddl_PaymentType.value) == 2) // paid
        Amount = val(hdn_TotalGCAmount.value); 
    else if(val(ddl_PaymentType.value) == 1) // to pay
        Amount = val(txt_Advance.value);

    if (Amount < val(txt_CashAmount.value))
        txt_CashAmount.value = val(Amount);
    else if (Amount < val(txt_ChequeAmount.value))
        txt_ChequeAmount.value = Amount ;

    txt_CashAmount.value =  val(Amount) - val(txt_ChequeAmount.value);
    hdn_CashAmount.value = val(txt_CashAmount.value); 
    hdn_ChequeAmount.value = val(txt_ChequeAmount.value);
}
//*******************************************************************
function ddl_UnitOfMeasurment_Change()
{
    Convert_InTo_Feet();
    Calculate_CFT_CBM();
    Get_BasisFreight();
}
//*******************************************************************
function On_Measurment_Unit_Change()
{
    var txt_UnitOfMeasurmentHeight = document.getElementById('WucGCNew1_txt_UnitOfMeasurmentHeight');
    var txt_UnitOfMeasurmentWidth = document.getElementById('WucGCNew1_txt_UnitOfMeasurmentWidth');
    var txt_UnitOfMeasurmentLength = document.getElementById('WucGCNew1_txt_UnitOfMeasurmentLength'); 
  
    txt_UnitOfMeasurmentHeight.value = val(txt_UnitOfMeasurmentHeight.value);
    txt_UnitOfMeasurmentLength.value = val(txt_UnitOfMeasurmentLength.value);
    txt_UnitOfMeasurmentWidth.value = val(txt_UnitOfMeasurmentWidth.value);
  
    document.getElementById('WucGCNew1_hdn_UnitOfMeasurmentHeight').value  = val(txt_UnitOfMeasurmentHeight.value);
    document.getElementById('WucGCNew1_hdn_UnitOfMeasurmentWidth').value  = val(txt_UnitOfMeasurmentLength.value);
    document.getElementById('WucGCNew1_hdn_UnitOfMeasurmentLength').value  = val(txt_UnitOfMeasurmentWidth.value);
   
    ddl_UnitOfMeasurment_Change();
    On_VolumetricToKgFactor_Change();
    Calculate_ChargeWeight();
    Calculate_Freight('0');
    Calculate_GrandTotal();
}
//*******************************************************************
function Convert_InTo_Feet()
{
    var ddl_UnitOfMeasurment = document.getElementById('WucGCNew1_ddl_UnitOfMeasurment');
    var txt_UnitOfMeasurmentLength = document.getElementById('WucGCNew1_txt_UnitOfMeasurmentLength');
    var txt_UnitOfMeasurmentWidth = document.getElementById('WucGCNew1_txt_UnitOfMeasurmentWidth');
    var txt_UnitOfMeasurmentHeight = document.getElementById('WucGCNew1_txt_UnitOfMeasurmentHeight');
    
    var lbl_LengthInFeetValue = document.getElementById('WucGCNew1_lbl_LengthInFeetValue');
    var lbl_WidthInFeetValue = document.getElementById('WucGCNew1_lbl_WidthInFeetValue');
    var lbl_HeightInFeetValue = document.getElementById('WucGCNew1_lbl_HeightInFeetValue');
    
    var Convirsion_Factor = val(0);

    if (ddl_UnitOfMeasurment.value == 1)
        Convirsion_Factor = 0.083;
    else if (ddl_UnitOfMeasurment.value == 2)
        Convirsion_Factor = 1;
    else if (ddl_UnitOfMeasurment.value == 3)
        Convirsion_Factor = 3.29;
    else if (ddl_UnitOfMeasurment.value == 4)
        Convirsion_Factor = 0.032;
    
    lbl_LengthInFeetValue.innerHTML = val(txt_UnitOfMeasurmentLength.value) * Convirsion_Factor;
    lbl_WidthInFeetValue.innerHTML = val(txt_UnitOfMeasurmentWidth.value) * Convirsion_Factor;
    lbl_HeightInFeetValue.innerHTML = val(txt_UnitOfMeasurmentHeight.value) * Convirsion_Factor;
     
    lbl_LengthInFeetValue.innerHTML = roundNumber(lbl_LengthInFeetValue.innerHTML,0);
    lbl_WidthInFeetValue.innerHTML = roundNumber(lbl_WidthInFeetValue.innerHTML,0);
    lbl_HeightInFeetValue.innerHTML = roundNumber(lbl_HeightInFeetValue.innerHTML,0);
    
    document.getElementById('WucGCNew1_hdn_LengthInFeet').value = roundNumber(lbl_LengthInFeetValue.innerHTML,0);
    document.getElementById('WucGCNew1_hdn_WidthInFeet').value = roundNumber(lbl_WidthInFeetValue.innerHTML,0);
    document.getElementById('WucGCNew1_hdn_HeightInFeet').value = roundNumber(lbl_HeightInFeetValue.innerHTML,0);
}
////*******************************************************************

function ddl_GCRisk_Change()
{
    var ddl_GCRisk = document.getElementById('WucGCNew1_ddl_GCRisk');       
    var txt_FOVRiskCharge = document.getElementById('WucGCNew1_txt_FOVRiskCharge');
    var hdn_FOVRiskCharge = document.getElementById('WucGCNew1_hdn_FOVRiskCharge');        
    var chk_IsInsured = document.getElementById('WucGCNew1_chk_IsInsured');

    txt_FOVRiskCharge.value = val(0);
    hdn_FOVRiskCharge.value = val(0);

    if ((ddl_GCRisk.value == 2 ||ddl_GCRisk.value == 3) || (ddl_GCRisk.value == 1 && chk_IsInsured.checked == false ))    // for carrier risk (2) and none (3)
    {
        Calculate_FOV();
    }
    Calculate_GrandTotal();
}
//*******************************************************************
function ddl_FreightBasis_Change()
{        
    var ddl_FreightBasis = document.getElementById('WucGCNew1_ddl_FreightBasis');
    var ddl_VolumetricFreightUnit = document.getElementById('WucGCNew1_ddl_VolumetricFreightUnit');
    var hdn_FreightBasisId = document.getElementById('WucGCNew1_hdn_FreightBasisId');

    hdn_FreightBasisId.value = ddl_FreightBasis.value;
    
    On_FreightBasis_Change();
    Calculate_ChargeWeight();
    Calculate_LoadingCharge();
    Calculate_Freight('0');
    Calculate_GrandTotal();
     
    if (val(ddl_FreightBasis.value) == 4 ) // for Volumetric  
    {
        ddl_VolumetricFreightUnit.value = 3; // kg
        ddl_VolumetricFreightUnit_Change();
    }
}
//*************************************************************************************
function On_FreightBasis_Change()
{
    var ddl_FreightBasis = document.getElementById('WucGCNew1_ddl_FreightBasis');
    var lbl_TotalCBMValue = document.getElementById('WucGCNew1_lbl_TotalCBMValue');
    var lbl_TotalCFTValue = document.getElementById('WucGCNew1_lbl_TotalCFTValue');
    var txt_VolumetricToKgFactor = document.getElementById('WucGCNew1_txt_VolumetricToKgFactor');
    var hdn_VolumetricToKgFactor = document.getElementById('WucGCNew1_hdn_VolumetricToKgFactor');
    var ddl_VolumetricFreightUnit= document.getElementById('WucGCNew1_ddl_VolumetricFreightUnit');
    var hdn_RateCard_CFTFactor = document.getElementById('WucGCNew1_hdn_RateCard_CFTFactor');   
    var hdn_Standard_FreightRate= document.getElementById('WucGCNew1_hdn_Standard_FreightRate'); 
    var txt_FreightRate = document.getElementById('WucGCNew1_txt_FreightRate'); 
    var hdn_FreightRate= document.getElementById('WucGCNew1_hdn_FreightRate');

    EnableDisable_On_FreightBasis_Change();
    
    if (ddl_FreightBasis.value == 4) // for Volumetric  
    {
        if (ddl_VolumetricFreightUnit.value == 3)
        {
            txt_VolumetricToKgFactor.value = val(hdn_RateCard_CFTFactor.value);
            hdn_VolumetricToKgFactor.value = val(hdn_RateCard_CFTFactor.value);                
        }
        else
        {
            txt_VolumetricToKgFactor.value = val(0);
            hdn_VolumetricToKgFactor.value = val(0);
        }
        Calculate_CFT_CBM();
    }
    else
    {
        lbl_TotalCBMValue.innerHTML = val(0);
        lbl_TotalCFTValue.innerHTML = val(0);
        txt_VolumetricToKgFactor.value = val(0);
    }
    if (ddl_FreightBasis.value != 3) // for other than Fixed  
    {
        txt_FreightRate.value = val(hdn_Standard_FreightRate.value);
        hdn_FreightRate.value =  val(txt_FreightRate.value );
        Calculate_ChargeWeight();
    }
    Get_BasisFreight();
}

function EnableDisable_On_FreightBasis_Change()
{  
    var ddl_FreightBasis = document.getElementById('WucGCNew1_ddl_FreightBasis');
    var ddl_VolumetricFreightUnit= document.getElementById('WucGCNew1_ddl_VolumetricFreightUnit');
    var tr_Volumetric = document.getElementById('WucGCNew1_tr_Volumetric');
    var tr_VolumetrickgFactor = document.getElementById('WucGCNew1_tr_VolumetrickgFactor');
    var lbl_mandatory_UnitOfMeasurmentWidth = document.getElementById('WucGCNew1_lbl_mandatory_UnitOfMeasurmentWidth');
    var lbl_mandatory_UnitOfMeasurmentLength = document.getElementById('WucGCNew1_lbl_mandatory_UnitOfMeasurmentLength');
    var lbl_mandatory_UnitOfMeasurmentHeight = document.getElementById('WucGCNew1_lbl_mandatory_UnitOfMeasurmentHeight');

    if (val(ddl_FreightBasis.value) == 4) // for Volumetric  
    {
        tr_Volumetric.style.display = 'inline';
        tr_VolumetrickgFactor.style.display = 'inline';
        lbl_mandatory_UnitOfMeasurmentLength.style.visibility = 'visible';
        lbl_mandatory_UnitOfMeasurmentWidth.style.visibility = 'visible';
        lbl_mandatory_UnitOfMeasurmentHeight.style.visibility = 'visible';

        if (val(ddl_VolumetricFreightUnit.value) != 3)
            tr_VolumetrickgFactor.style.display = 'none';
    }
    else
    {
        tr_Volumetric.style.display = 'none';
        tr_VolumetrickgFactor.style.display = 'none';

        lbl_mandatory_UnitOfMeasurmentLength.style.visibility = 'hidden';
        lbl_mandatory_UnitOfMeasurmentWidth.style.visibility = 'hidden';
        lbl_mandatory_UnitOfMeasurmentHeight.style.visibility = 'hidden';
    }
}
//*******************************************************************

function Calculate_FOV()
{   

    var ddl_GCRisk = document.getElementById('WucGCNew1_ddl_GCRisk');
    var hdn_ItemValueForFOV = document.getElementById('WucGCNew1_hdn_ItemValueForFOV');
    var hdn_TotalInvoiceAmount = document.getElementById('WucGCNew1_hdn_TotalInvoiceAmount');
    var chk_IsInsured = document.getElementById('WucGCNew1_chk_IsInsured');
    var chk_Is_FOV_Calculated_As_Per_Standard = document.getElementById('WucGCNew1_chk_Is_FOV_Calculated_As_Per_Standard');
    var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');
    var txt_FOVRiskCharge = document.getElementById('WucGCNew1_txt_FOVRiskCharge');
    var hdn_FOVRiskCharge = document.getElementById('WucGCNew1_hdn_FOVRiskCharge');
    var hdn_Standard_FOV = document.getElementById('WucGCNew1_hdn_Standard_FOV');
    var hdn_RateCard_FOVPercentage = document.getElementById('WucGCNew1_hdn_RateCard_FOVPercentage');
    var hdn_RateCard_Fov_Charge_Discount_Percent = document.getElementById('WucGCNew1_hdn_RateCard_Fov_Charge_Discount_Percent'); 
    var hdn_RateCard_FOV   = document.getElementById('WucGCNew1_hdn_RateCard_FOV');
    var hdn_RateCard_FOVRate = document.getElementById('WucGCNew1_hdn_RateCard_FOVRate');
    var hdn_RateCard_Invoice_Rate = document.getElementById('WucGCNew1_hdn_RateCard_Invoice_Rate');
    var hdn_RateCard_Invoice_Per_How_Many_Rs = document.getElementById('WucGCNew1_hdn_RateCard_Invoice_Per_How_Many_Rs');
    var ddl_Booking_Type  = document.getElementById('WucGCNew1_ddl_Booking_Type');
    
//    var txt_ChargeWeight = document.getElementById('WucGCNew1_txt_ChargeWeight');
    var hdn_ChargeWeight = document.getElementById('WucGCNew1_hdn_ChargeWeight');
    
    var txt_ActualWeight = document.getElementById('WucGCNew1_txt_ActualWeight');
    var MenuItemId = document.getElementById('WucGCNew1_hdn_MenuItemId').value;
    var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');
    var hdn_IsContractApplied  = document.getElementById('WucGCNew1_hdn_IsContractApplied');
    var TempMinimumFOV = 0;
    var TempInvoice_Per_How_Many_Rs = 0;
    var TempFOVRate = 0;
    var TempInvoice_Rate = 0;
    var TempFOVPercentage = 0; 
    var TempFOVExemptUpTo_RateContract = 0;
  
  
    var hdn_RateContractId = document.getElementById('WucGCNew1_hdn_RateContractId');
    var hdn_FOVPercentage_RateContract = document.getElementById('WucGCNew1_hdn_FOVPercentage_RateContract');
    var hdn_FOVExemptUpTo_RateContract = document.getElementById('WucGCNew1_hdn_FOVExemptUpTo_RateContract');
    
    
    
    if(hdn_IsContractApplied.value == "1")
    {
        TempMinimumFOV = '0';
        TempFOVPercentage = ContractFreightDetails[0][4];
        TempFOVRate = ContractFreightDetails[0][16];
        TempInvoice_Rate = ContractFreightDetails[0][17];
        TempInvoice_Per_How_Many_Rs = ContractFreightDetails[0][18];
    }
    else if(val(hdn_RateContractId.value) > 0)
    {
    
        TempMinimumFOV = '0';
        TempFOVPercentage = val(hdn_FOVPercentage_RateContract.value);
        TempFOVRate = '0';
        TempInvoice_Rate = '0';
        TempInvoice_Per_How_Many_Rs = '0';
        TempFOVExemptUpTo_RateContract = val(hdn_FOVExemptUpTo_RateContract.value);
    }
    else
    {
        TempMinimumFOV = val(hdn_RateCard_FOV.value);
        TempFOVPercentage = val(hdn_RateCard_FOVPercentage.value);
        TempFOVRate = val(hdn_RateCard_FOVRate.value);
        TempInvoice_Rate = val(hdn_RateCard_Invoice_Rate.value);
        TempInvoice_Per_How_Many_Rs = val(hdn_RateCard_Invoice_Per_How_Many_Rs.value);
    }


    var FOV = val(0);
    var Standard_FOV = val(0);
    var Discounted_FOV = 0;
    var Discount = 0;
    var Invoice_Difference = 0;
    var Mod = 0;
    
    txt_FOVRiskCharge.value = Math.round(val(txt_FOVRiskCharge.value));

    // 1 for owner risk    
    
    if (MenuItemId != 200 && (val(ddl_GCRisk.value) == 2 || val(ddl_GCRisk.value) == 3) || (val(ddl_GCRisk.value) == 1 && chk_IsInsured.checked == false))    // for carrier risk (2) and none (3)
    {
        FOV = val(hdn_TotalInvoiceAmount.value) * val(TempFOVPercentage)/100;
        Standard_FOV = val(hdn_TotalInvoiceAmount.value) * val(TempFOVPercentage)/100;
        
        if (chk_Is_FOV_Calculated_As_Per_Standard.checked == true)
        {
            var ItemRatePerKgOnInvoiceValue; 
            var ItemRatePerKgOnItemValue;
            
            ItemRatePerKgOnInvoiceValue = val(hdn_TotalInvoiceAmount.value) / val(txt_ActualWeight.value);
            ItemRatePerKgOnItemValue = val(hdn_ItemValueForFOV.value) / val(txt_ActualWeight.value);
            
            //if (val(hdn_TotalInvoiceAmount.value) > 0)
            if (ItemRatePerKgOnInvoiceValue > ItemRatePerKgOnItemValue)
            {            
                FOV = (val(hdn_TotalInvoiceAmount.value) - val(TempFOVExemptUpTo_RateContract) ) * val(TempFOVPercentage) / 100;
                Standard_FOV = val(hdn_TotalInvoiceAmount.value) * val(TempFOVPercentage)/100;  
            }
            else
            {        
//                FOV = (val(txt_ActualWeight.value) * 400) * val(TempFOVPercentage) / 100;
//                Standard_FOV = (val(txt_ActualWeight.value) * 400) * val(TempFOVPercentage)/100;  
                FOV = ((hdn_ItemValueForFOV.value) - val(TempFOVExemptUpTo_RateContract)) * val(TempFOVPercentage) / 100;
                Standard_FOV = (hdn_ItemValueForFOV.value) * val(TempFOVPercentage)/100;  
              
            }
        }
        else
        {
            System_Invoice = val(txt_ActualWeight.value) * val(TempInvoice_Rate);
            Invoice_Difference = Math.round(val(hdn_TotalInvoiceAmount.value) - System_Invoice);
            Mod = val(Invoice_Difference) % 1000;

            if (val(hdn_TotalInvoiceAmount.value) > val(System_Invoice))
            {
                if (Mod > 0)
                {
                    var Difference = 1000 - Mod;
                    Invoice_Difference = Invoice_Difference + Difference;
                }

                if(val(TempInvoice_Per_How_Many_Rs) > 0)
                {
                   FOV = (val(Invoice_Difference)) * val(TempFOVRate) / val(TempInvoice_Per_How_Many_Rs);
                   Standard_FOV = (val(Invoice_Difference)) * val(TempFOVRate) / val(TempInvoice_Per_How_Many_Rs);
                }
            }
            else
            {
                FOV = 0;
                Standard_FOV = 0;
            }
         }
         
         if (val(FOV) < val(TempMinimumFOV))
         {
            FOV = val(TempMinimumFOV);
            txt_FOVRiskCharge.value= val(FOV);
         }
         else
            txt_FOVRiskCharge.value = val(FOV);
     }
     else
     {
       txt_FOVRiskCharge.value = val(0);
       hdn_FOVRiskCharge.value = val(0);
     }
     
    Discount  =  FOV * val(hdn_RateCard_Fov_Charge_Discount_Percent.value) / 100;             
    Discounted_FOV = val(FOV) - val(Discount);

    if(val(txt_FOVRiskCharge.value) < val(FOV))
    { 
      if(val(txt_FOVRiskCharge.value) < val(Discounted_FOV))
         txt_FOVRiskCharge.value = val(FOV);
    }
    
    if(val(ddl_Booking_Type.value) == 2) // 2 FTL
    {
       txt_FOVRiskCharge.value =  0;
       hdn_FOVRiskCharge.value =  0;
    }
    else
    { 
        txt_FOVRiskCharge.value =  Math.round(val(txt_FOVRiskCharge.value));
        hdn_FOVRiskCharge.value =  Math.round(val(txt_FOVRiskCharge.value));
        hdn_Standard_FOV.value =  val(Standard_FOV);
    }
   if (ddl_PaymentType.value == 5 || chk_IsAttached.checked == true)
   {
        txt_FOVRiskCharge.value = val(0);
        hdn_FOVRiskCharge.value = val(0);
   }
}
//*******************************************************************
function Get_BasisFreight()
{ 
    Calculate_Freight('0');
    Calculate_LoadingCharge();
    Calculate_FOV();
//  Calculate_DDODA_Charge();
    Calculate_GrandTotal();
 }
//*******************************************************************
function Calculate_CFT_CBM()
{
    var FreightBasisId = document.getElementById('WucGCNew1_ddl_FreightBasis').value;
    var hdn_TotalCFT = document.getElementById('WucGCNew1_hdn_TotalCFT');
    var hdn_TotalCBM = document.getElementById('WucGCNew1_hdn_TotalCBM');
    var LengthInFeet = document.getElementById('WucGCNew1_hdn_LengthInFeet').value;
    var WidthInFeet = document.getElementById('WucGCNew1_hdn_WidthInFeet').value;
    var HeightInFeet = document.getElementById('WucGCNew1_hdn_HeightInFeet').value;

    hdn_TotalCFT.value = val(0);
    hdn_TotalCBM.value = val(0);

    if (val(FreightBasisId) == 4)
    {
//            Convert_InTo_Feet();    
        hdn_TotalCFT.value = val(LengthInFeet) * val(WidthInFeet) * val(HeightInFeet);
        hdn_TotalCBM.value = val(hdn_TotalCFT.value) / parseFloat('34.328125');
    }
    
    hdn_TotalCFT.value = roundNumber(val(hdn_TotalCFT.value),2);
    hdn_TotalCBM.value = roundNumber(val(hdn_TotalCBM.value),2);
    
    document.getElementById('WucGCNew1_lbl_TotalCFTValue').innerHTML = hdn_TotalCFT.value;
    document.getElementById('WucGCNew1_lbl_TotalCBMValue').innerHTML = hdn_TotalCBM.value;
}
//*******************************************************************************
function Get_Freight_Rate()
{
    var ddl_FreightBasis = document.getElementById('WucGCNew1_ddl_FreightBasis');
    var ddl_VolumetricFreightUnit = document.getElementById('WucGCNew1_ddl_VolumetricFreightUnit');
    var txt_Freight = document.getElementById('WucGCNew1_txt_Freight');
    var hdn_ChargeWeight = document.getElementById('WucGCNew1_hdn_ChargeWeight');
    var txt_FreightRate = document.getElementById('WucGCNew1_txt_FreightRate');
    var hdn_FreightRate = document.getElementById('WucGCNew1_hdn_FreightRate');
    var hdn_Standard_FreightRate= document.getElementById('WucGCNew1_hdn_Standard_FreightRate'); 
    var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');
    var hdn_TotalCFT = document.getElementById('WucGCNew1_hdn_TotalCFT');
    var hdn_TotalCBM = document.getElementById('WucGCNew1_hdn_TotalCBM');
    var hdn_TotalArticles = document.getElementById('WucGCNew1_hdn_TotalArticles');
    var hdn_CompanyParameter_Standard_FreightRatePer = document.getElementById('WucGCNew1_hdn_CompanyParameter_Standard_FreightRatePer');
    var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');

    var Freight_value = val(0);
    var Standard_Freight_value = val(0);
    var Temp_Freight_Rate = val(0);
    var Discounted_Freight = 0;
    var Discount = 0;
    
    Freight_value = val(txt_Freight.value);

    if (ddl_PaymentType.value != 5 && chk_IsAttached.checked == false )     // foc
    {
        if (ddl_FreightBasis.value == 1 )  // for wt
        {
            Temp_Freight_Rate = val(Freight_value) * val(hdn_CompanyParameter_Standard_FreightRatePer.value) 
                                / val(hdn_ChargeWeight.value);
        }
        else if (ddl_FreightBasis.value == 2 )// for article
        {
            Temp_Freight_Rate = val(Freight_value) / val(hdn_TotalArticles.value) ;
        }
        else if (ddl_FreightBasis.value == 3 )// for Fixed
        {
            Temp_Freight_Rate = val(Freight_value);
        }
        else if (ddl_FreightBasis.value == 4 && ddl_VolumetricFreightUnit.value== 1) // for cft
        {
            Calculate_CFT_CBM();
            Temp_Freight_Rate = val(Freight_value) / val(hdn_TotalCFT.value); 
        }
        else if (ddl_FreightBasis.value == 4 && ddl_VolumetricFreightUnit.value == 2) // for cbm
        {
            Calculate_CFT_CBM();
            Temp_Freight_Rate = val(Freight_value) / val(hdn_TotalCBM.value); 
        }
        else if (ddl_FreightBasis.value == 4 && ddl_VolumetricFreightUnit.value == 3) // for kg
        {
            Temp_Freight_Rate = val(Freight_value) * val(hdn_CompanyParameter_Standard_FreightRatePer.value) 
                                / val(hdn_ChargeWeight.value);
        }
        
        txt_FreightRate.value = val(Temp_Freight_Rate);
        hdn_FreightRate.value = val(Temp_Freight_Rate);
        
        if(val(hdn_FreightRate.value) < val(hdn_Standard_FreightRate.value))
        {
            On_Freight_Rate_Change();
        }
    }
    else
    {
        txt_FreightRate.value = val(0);
        hdn_FreightRate.value = val(0);
    }  
}  
//*******************************************************************
function ddl_VolumetricFreightUnit_Change()
{
    On_FreightBasis_Change();
    Calculate_ChargeWeight();
    Calculate_Freight('0');
    Calculate_GrandTotal();
    EnableDisable_On_FreightBasis_Change();
}
//*******************************************************************
function On_Freight_Rate_Change()
{
    var MenuItemId = document.getElementById('WucGCNew1_hdn_MenuItemId').value;
    var txt_FreightRate = document.getElementById('WucGCNew1_txt_FreightRate');
    var hdn_Standard_FreightRate= document.getElementById('WucGCNew1_hdn_Standard_FreightRate'); 
    var hdn_FreightRate= document.getElementById('WucGCNew1_hdn_FreightRate');
    var ddl_FreightBasis = document.getElementById('WucGCNew1_ddl_FreightBasis');
    var hdn_RateCard_Freight_Charge_Discount_Percent = document.getElementById('WucGCNew1_hdn_RateCard_Freight_Charge_Discount_Percent'); 
//    var hdn_Is_Validate_Freight_On_Article = document.getElementById('WucGCNew1_hdn_Is_Validate_Freight_On_Article'); 
    var Discounted_Freight_Rate = 0 ;
    var Discount_Rate = 0 ;

     txt_FreightRate.value = val(txt_FreightRate.value);
     Discount_Rate = val(hdn_Standard_FreightRate.value) * val(hdn_RateCard_Freight_Charge_Discount_Percent.value) / 100 ; 
     Discounted_Freight_Rate = val(hdn_Standard_FreightRate.value) - val(Discount_Rate); 

    if(val(MenuItemId) != 200 && val(txt_FreightRate.value) < val(hdn_Standard_FreightRate.value))
    {
        if (val(txt_FreightRate.value) < val(Discounted_Freight_Rate)) 
        {
            if(val(txt_FreightRate.value) <= 0 || val(ddl_FreightBasis.value) != 2)
            {
                txt_FreightRate.value = val(hdn_Standard_FreightRate.value );  
            }
//            else if (ddl_FreightBasis.value == 2 && hdn_Is_Validate_Freight_On_Article.value == 'True' )
//            {
//                txt_FreightRate.value = val(hdn_Standard_FreightRate.value );  
//            }
        }
    } 
    txt_FreightRate.value = val(txt_FreightRate.value);
    hdn_FreightRate.value = val(txt_FreightRate.value);

    Calculate_Freight('0');
    Calculate_GrandTotal();
}    
//*******************************************************************
function On_Actual_Weight_Change()
{
    var txt_ActualWeight = document.getElementById('WucGCNew1_txt_ActualWeight');
    var hdn_TotalWeight = document.getElementById('WucGCNew1_hdn_TotalWeight');

    txt_ActualWeight.value = val(txt_ActualWeight.value);

    if(val(txt_ActualWeight.value) < val(hdn_TotalWeight.value))
        txt_ActualWeight.value = val(hdn_TotalWeight.value);

    Calculate_ChargeWeight();
    Calculate_Freight('0'); 
    Calculate_GrandTotal();
}    
 //*******************************************************************
function On_ChargeWeight_Change()
{
    var txt_ChargeWeight = document.getElementById('WucGCNew1_txt_ChargeWeight');
    var txt_ActualWeight = document.getElementById('WucGCNew1_txt_ActualWeight');
    var ContractId = document.getElementById('WucGCNew1_hdn_ContractId').value;

    txt_ChargeWeight.value = val(txt_ChargeWeight.value); 
    
    if (val(txt_ChargeWeight.value) < val(txt_ActualWeight.value))
         txt_ChargeWeight.value = val(txt_ActualWeight.value);

    Validate_ChargeWeight();
    if(ContractId > 0)
    {
        Fill_Contract_Change(IsFromPageLoad);
    }
    else
    {
        Calculate_Freight('0');
        Calculate_LoadingCharge();
        Calculate_DDCharge();
        Calculate_FOV();
        Calculate_GrandTotal();
    }
}
//*******************************************************************

function Calculate_LocalCharge()
{
    var txt_LocalCharge = document.getElementById('WucGCNew1_txt_LocalCharge');
    var hdn_LocalCharge = document.getElementById('WucGCNew1_hdn_LocalCharge');
    var hdn_RateCard_LocalCharge = document.getElementById('WucGCNew1_hdn_RateCard_LocalCharge');
    var txt_ChargeWeight = document.getElementById('WucGCNew1_txt_ChargeWeight');
    var hdn_CompanyParameter_Standard_FreightRatePer = document.getElementById('WucGCNew1_hdn_CompanyParameter_Standard_FreightRatePer');
    var chk_IsAttached  = document.getElementById('WucGCNew1_chk_IsAttached');
    var hdn_IsContractApplied  = document.getElementById('WucGCNew1_hdn_IsContractApplied');
    var GCId  = document.getElementById('WucGCNew1_hdn_GCId').value;

    var TempLocalChargeRate = 0;
    var TempLocalCharge = 0;
    
    if(chk_IsAttached.checked == false && val(GCId) <= 0)
    {
        if(hdn_IsContractApplied.value == "1")
            TempLocalChargeRate = ContractFreightDetails[0][7];
        else
            TempLocalChargeRate = val(hdn_RateCard_LocalCharge.value);
        txt_LocalCharge.value = val(txt_LocalCharge.value); 

        TempLocalCharge = val(txt_ChargeWeight.value) * val(TempLocalChargeRate) / val(hdn_CompanyParameter_Standard_FreightRatePer.value);

        if (val(txt_LocalCharge.value) < TempLocalCharge && hdn_IsContractApplied.value == "1")
           txt_LocalCharge.value = val(TempLocalCharge);
        else
           txt_LocalCharge.value = val(TempLocalCharge);
    }
    else if (chk_IsAttached.checked == true && val(GCId) <= 0)
    {
        txt_LocalCharge.value = val(0);
    }
    hdn_LocalCharge.value = val(txt_LocalCharge.value); 
 }
 
//*******************************************************************
function Calculate_LoadingCharge()
{

    var hdn_RateCard_HamaliPerKg = document.getElementById('WucGCNew1_hdn_RateCard_HamaliPerKg');
    var hdn_RateCard_HamaliPerArticles = document.getElementById('WucGCNew1_hdn_RateCard_HamaliPerArticles');
    var hdn_RateCard_HamaliCharge = document.getElementById('WucGCNew1_hdn_RateCard_HamaliCharge');
    var txt_LoadingCharge = document.getElementById('WucGCNew1_txt_LoadingCharge');
    var hdn_LoadingCharge = document.getElementById('WucGCNew1_hdn_LoadingCharge');
    var hdn_Standard_HamaliCharge = document.getElementById('WucGCNew1_hdn_Standard_HamaliCharge');
    var txt_ChargeWeight = document.getElementById('WucGCNew1_txt_ChargeWeight');
    var hdn_CompanyParameter_Standard_FreightRatePer = document.getElementById('WucGCNew1_hdn_CompanyParameter_Standard_FreightRatePer');
//        var hdn_RateCard_Hamali_Charge_Discount_Percent = document.getElementById('WucGCNew1_hdn_RateCard_Hamali_Charge_Discount_Percent'); 
    var ddl_FreightBasis = document.getElementById('WucGCNew1_ddl_FreightBasis');
    var hdn_TotalArticles  = document.getElementById('WucGCNew1_hdn_TotalArticles');
    var hdn_TotalWeight = document.getElementById('WucGCNew1_hdn_TotalWeight');
    var MenuItemId  = document.getElementById('WucGCNew1_hdn_MenuItemId').value;
    var chk_IsAttached  = document.getElementById('WucGCNew1_chk_IsAttached');
    var hdn_IsContractApplied  = document.getElementById('WucGCNew1_hdn_IsContractApplied');
    var hdn_TotalBharaiAmt  = document.getElementById('WucGCNew1_hdn_TotalBharaiAmt');

    var hdn_Mode = document.getElementById('WucGCNew1_hdn_Mode');

    var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');

    var TempHamaliCharge = 0;
    var TempHamaliPerArticles = 0;
    var TempHamaliPerKg = 0;

    var fromBranchID = window.parent.GetBkgBranchIDForCommodity();
    
    TempHamaliCharge = val(hdn_RateCard_HamaliCharge.value);
    TempHamaliPerArticles = val(hdn_RateCard_HamaliPerArticles.value);
    TempHamaliPerKg = val(hdn_RateCard_HamaliPerKg.value);

    txt_LoadingCharge.value = val(txt_LoadingCharge.value); 

    var hdn_RateContractId = document.getElementById('WucGCNew1_hdn_RateContractId');
    var hdn_HamaliPerKg_RateContract = document.getElementById('WucGCNew1_hdn_HamaliPerKg_RateContract');

    if (hdn_RateContractId.value > 0)
    {
        txt_LoadingCharge.value = val(hdn_HamaliPerKg_RateContract.value) * val(hdn_TotalWeight.value);
    }
    else
    {
        txt_LoadingCharge.value = val(TempHamaliPerKg) * val(hdn_TotalWeight.value);
    }
    
    
    //if(MenuItemId != 200 && val(txt_LoadingCharge.value) < val(TempHamaliCharge))
    if (MenuItemId != 200)
    {
        if (fromBranchID == 53) 
        {
            txt_LoadingCharge.value = val(hdn_TotalBharaiAmt.value);
            
        }
        else if (val(txt_LoadingCharge.value) < val(TempHamaliCharge) && hdn_RateContractId.value <= 0)
        {

            txt_LoadingCharge.value = val(TempHamaliCharge);
        }
    }
    
    if (chk_IsAttached.checked == true || val(ddl_PaymentType.value) == 5)
    {
        txt_LoadingCharge.value = val(0);
    }

    hdn_LoadingCharge.value = val(txt_LoadingCharge.value); 
    hdn_Standard_HamaliCharge.value = Math.round(val(txt_LoadingCharge.value));
 }
    
//*******************************************************************

function Calculate_ChargeWeight()
{
    var ddl_FreightBasis = document.getElementById('WucGCNew1_ddl_FreightBasis');
    var ddl_VolumetricFreightUnit = document.getElementById('WucGCNew1_ddl_VolumetricFreightUnit');
    var txt_ActualWeight = document.getElementById('WucGCNew1_txt_ActualWeight');
    var txt_ChargeWeight = document.getElementById('WucGCNew1_txt_ChargeWeight');
    var hdn_ChargeWeight = document.getElementById('WucGCNew1_hdn_ChargeWeight');
    var hdn_TotalWeight = document.getElementById('WucGCNew1_hdn_TotalWeight');
    var hdn_TotalCFT = document.getElementById('WucGCNew1_hdn_TotalCFT');
    var txt_VolumetricToKgFactor = document.getElementById('WucGCNew1_txt_VolumetricToKgFactor');
    var hdn_RateCard_MinimumChargeWeight = document.getElementById('WucGCNew1_hdn_RateCard_MinimumChargeWeight');
    var MenuItemId  = document.getElementById('WucGCNew1_hdn_MenuItemId').value;

    txt_ChargeWeight.value = val(txt_ActualWeight.value);            
    hdn_ChargeWeight.value = val(txt_ActualWeight.value);

    if(val(ddl_FreightBasis.value) == 4 && val(ddl_VolumetricFreightUnit.value) == 3)
    {
        txt_ChargeWeight.value = val(hdn_TotalCFT.value) * val(txt_VolumetricToKgFactor.value);
        txt_ChargeWeight.value = val(txt_ChargeWeight.value);
    }

    var Mod = val(txt_ChargeWeight.value) % 5;

    if (Mod > 0)
    {
        var Difference = 5 - Mod;
        txt_ChargeWeight.value = val(txt_ChargeWeight.value) + Difference;
        txt_ChargeWeight.value = val(txt_ChargeWeight.value);
    }

    if (val(MenuItemId) != 200 && val(txt_ChargeWeight.value) < val(hdn_RateCard_MinimumChargeWeight.value))
        txt_ChargeWeight.value = val(hdn_RateCard_MinimumChargeWeight.value);

    hdn_ChargeWeight.value = roundNumber(val(txt_ChargeWeight.value),0);
    txt_ChargeWeight.value = roundNumber(val(hdn_ChargeWeight.value),0);
}

 //***********************************************************************************
function On_VolumetricToKgFactor_Change()
{
     var hdn_RateCard_CFTFactor = document.getElementById('WucGCNew1_hdn_RateCard_CFTFactor');
     var txt_VolumetricToKgFactor = document.getElementById('WucGCNew1_txt_VolumetricToKgFactor');
     var hdn_VolumetricToKgFactor = document.getElementById('WucGCNew1_hdn_VolumetricToKgFactor');
     var MenuItemId  = document.getElementById('WucGCNew1_hdn_MenuItemId').value;

     txt_VolumetricToKgFactor.value =  val(txt_VolumetricToKgFactor.value);

     if(val(MenuItemId) != 200 && val(txt_VolumetricToKgFactor.value) < val(hdn_RateCard_CFTFactor.value))  
      {
         txt_VolumetricToKgFactor.value = val(hdn_RateCard_CFTFactor.value);
      }
      hdn_VolumetricToKgFactor.value = val(txt_VolumetricToKgFactor.value);
      
      Calculate_ChargeWeight();
      Calculate_Freight('0');
      Calculate_GrandTotal();
}
    
 //*******************************************************************************
 
function On_RoadPermit_Change()
{
    var ContractId = document.getElementById('WucGCNew1_hdn_ContractId').value;

    EnableDisable_On_RoadPermit_Change();
    Get_Standard_FreightRate();

    if(ContractId > 0)
    {
       Fill_Contract_Change(IsFromPageLoad);
    }
    else
    {
        Calculate_Freight('0');
        Calculate_GrandTotal();
    }
}
   
function EnableDisable_On_RoadPermit_Change()
{
    var ddl_Road_Permit = document.getElementById('WucGCNew1_ddl_Road_Permit');
    var td_RoadPermitSrNo = document.getElementById('WucGCNew1_td_RoadPermitSrNo');
    var txt_Road_Permit_SrNo = document.getElementById('WucGCNew1_txt_Road_Permit_SrNo');

    if(val(ddl_Road_Permit.value)== 2)
    {
         td_RoadPermitSrNo.style.visibility = 'visible';
         txt_Road_Permit_SrNo.style.visibility = 'visible';
    }
    else
    {
        txt_Road_Permit_SrNo.value = '';
        td_RoadPermitSrNo.style.visibility = 'hidden';
        txt_Road_Permit_SrNo.style.visibility = 'hidden';
    }
}
 //*******************************************************************************
function Get_Standard_FreightRate()
{
    var FromLocationId = document.getElementById('WucGCNew1_hdn_FromLocationId').value;
    var ToLocationId = document.getElementById('WucGCNew1_hdn_ToLocationId').value;
    var DivisionId = document.getElementById('WucGCNew1_hdn_DivisionId').value;
    var RoadPermitId = document.getElementById('WucGCNew1_ddl_Road_Permit').value;
    var VehicleTypeId = document.getElementById('WucGCNew1_ddl_VehicleType').value;
    
    if(val(FromLocationId) > 0 && val(ToLocationId) > 0)
        Raj.EC.OperationModel.NewGCSearch.Get_StdFreightRate(val(FromLocationId),val(ToLocationId),val(DivisionId),val(RoadPermitId),val(VehicleTypeId),handleStandardFreightRate);
}
 //*******************************************************************************
 
function handleStandardFreightRate(Results)
{
    var rows = Results.value.Rows[0];
    
    var ContractId = document.getElementById('WucGCNew1_hdn_ContractId').value;

    document.getElementById('WucGCNew1_hdn_FreightRate').value = val(rows['Normal_Rate']);
    document.getElementById('WucGCNew1_hdn_Standard_FreightRate').value = val(rows['Normal_Rate']);
    document.getElementById('WucGCNew1_txt_FreightRate').value = val(rows['Normal_Rate']);
    document.getElementById('WucGCNew1_hdn_TransitDays').value = val(rows['Transit_Days']);
    document.getElementById('WucGCNew1_hdn_Distance_In_Km').value = val(rows['Distance_In_Km']);

    Set_Exp_Del_Date();
    if(ContractId > 0)
    {
        Fill_Contract_Change(IsFromPageLoad);
    }
    else
    {
        Calculate_Freight('0');
        Calculate_GrandTotal();
    }
}
 //*******************************************************************************

function CalculateApproxWeightAndAmount(txtPackage,ddlItemID,ddlSizeID,
txtApproxWeight,txtRate,txtAmount,hdfn_ToBe,
chk_Is_Service_Tax_App_For_Commodity,hdfn_ItemRatePerKg,hdnBkgTypeID,hdfn_BharaiAmt)
{ 



var fromBranchID = window.parent.GetBkgBranchIDForCommodity();
var toBranchID = window.parent.GetDlyBranchIDForCommodity();
var fromSerLocationID = window.parent.GetFromSerLocationIDForCommodity();
var toSerLocationID = window.parent.GetSerLocationIDForCommodity();
var Dly_TypeID = window.parent.GetDly_TypeIDForCommodity();
var Bkg_TypeID = window.parent.GetBkg_TypeIDForCommodity(); 
var Payment_TypeID = window.parent.GetPayment_TypeIDForCommodity(); 
var hdn_Bkg_TypeID = document.getElementById('hdn_Bkg_TypeID');

hdn_Bkg_TypeID.value = Bkg_TypeID;

var booking_date = window.parent.GetBookingDate(); 
booking_date = new Date(booking_date.getFullYear(), booking_date.getMonth(),booking_date.getDate())

var hdn_ConsigneeDeliveryAreaID = window.parent.GetDlyAreaID();
 
var txtPackage = document.getElementById(txtPackage);
var ddlItemID = document.getElementById(ddlItemID);
var ddlSizeID = document.getElementById(ddlSizeID);  


if (Bkg_TypeID == 2 || fromBranchID == 50 || fromBranchID == 53 || fromBranchID == 78 )
{ 
 document.getElementById(txtApproxWeight).disabled = false;
 document.getElementById(txtAmount).disabled = false;
}
else
{
 document.getElementById(txtApproxWeight).disabled = true;
 document.getElementById(txtAmount).disabled = true;
}

 
commodityDetailsApproxWeight = document.getElementById(txtApproxWeight);
commodityDetailsRate = document.getElementById(txtRate);
commodityDetailsAmount= document.getElementById(txtAmount);
commodityDetailsToBe = document.getElementById(hdfn_ToBe);

commodityDetailsBharaiAmt = document.getElementById(hdfn_BharaiAmt);


this.chk_Is_Service_Tax_App_For_Commodity = document.getElementById(chk_Is_Service_Tax_App_For_Commodity);
this.hdfn_ItemRatePerKg = document.getElementById(hdfn_ItemRatePerKg);

var RateContractID= window.parent.GetRateContract_IDForCommodity();



if (RateContractID == '0')
{
    Raj.EC.OperationModel.NewGCSearch.CalculateApproxWeightAndRate
    (val(txtPackage.value),fromBranchID,toSerLocationID,
    ddlItemID.value,ddlSizeID.value,booking_date,hdn_ConsigneeDeliveryAreaID.value,val(Dly_TypeID),Payment_TypeID,handleApproxWeightAndAmount);
}
else
{

    Raj.EC.OperationModel.NewGCSearch.CalculateApproxWeightAndRate_RateContract
    (val(RateContractID),val(txtPackage.value),fromSerLocationID,toSerLocationID,
    ddlSizeID.value,ddlItemID.value,fromBranchID,booking_date,Payment_TypeID,handleApproxWeightAndAmount_RateContract);

}

}

function setFocusonAdd(ddlSizeID,lbtnAddCommodity)
{   

//document.onkeydown = checkKeycode

//  var keycode;
//  if (window.event) keycode = window.event.keyCode;
//  else if (e) keycode = e.which; 
//  alert(window.event.keyCode);
  var keycode = window.event.keyCode;

  if (keycode == 13)
    {
        var ddlSizeID = document.getElementById(ddlSizeID);
        var lbtnAddCommodity = document.getElementById(lbtnAddCommodity); 
         
        lbtnAddCommodity.focus();
    }
}
 //*******************************************************************************
function handleApproxWeightAndAmount(Results)
{  
  if (Results.value != null)
  {  
    var rows = Results.value.Rows[0];
    commodityDetailsApproxWeight.value = val(rows['ApproxWieght']);
    commodityDetailsRate.value = val(rows['Rate']);
    commodityDetailsAmount.value = val(rows['Amount']);
    commodityDetailsToBe.value = rows['ToBe'];
    chk_Is_Service_Tax_App_For_Commodity.checked = rows['Is_Service_Tax_Applicable'];
    hdfn_ItemRatePerKg.value = rows['ItemRatePerKg'];

    commodityDetailsBharaiAmt.value = rows['BharaiAmt'];
    
    var strMessage;
    strMessage = rows['StrMessage'];
    
    var lbl_CommodityErrorMsg = document.getElementById('lbl_CommodityErrorMsg');
    lbl_CommodityErrorMsg.innerHTML = strMessage;  
  }
} 
 //*******************************************************************************
function handleApproxWeightAndAmount_RateContract(Results)
{  
  if (Results.value != null)
  {  
    var rows = Results.value.Rows[0];
    commodityDetailsApproxWeight.value = val(rows['ApproxWieght']);
    commodityDetailsRate.value = val(rows['Rate']);
    commodityDetailsAmount.value = val(rows['Amount']);
    commodityDetailsToBe.value = rows['ToBe'];
    chk_Is_Service_Tax_App_For_Commodity.checked = rows['Is_Service_Tax_Applicable'];
    hdfn_ItemRatePerKg.value = rows['ItemRatePerKg'];

    commodityDetailsBharaiAmt.value = rows['BharaiAmt'];
    
    var strMessage;
    strMessage = rows['StrMessage'];
    
    var lbl_CommodityErrorMsg = document.getElementById('lbl_CommodityErrorMsg');
    lbl_CommodityErrorMsg.innerHTML = strMessage;  
  }
} 
 //*******************************************************************************
function GetBkgBranchID()
{
var hdn_BookingBranchId = document.getElementById('WucGCNew1_hdn_BookingBranchId');
var fromBranchID = hdn_BookingBranchId.value;
return fromBranchID;
}
 //*******************************************************************************
function GetDlyBranchID()
{
var hdn_DeliveryBranchId = document.getElementById('WucGCNew1_hdn_DeliveryBranchId');
var toBranchID = hdn_DeliveryBranchId.value;
return toBranchID;
}
 //*******************************************************************************
function GetSerLocationID()
{
var hdn_ToLocationId = document.getElementById('WucGCNew1_hdn_ToLocationId');
var toLocationID = hdn_ToLocationId.value;
return toLocationID;
}
//*******************************************************************************
function GetFromSerLocationID()
{
var hdn_FromLocationId = document.getElementById('WucGCNew1_hdn_FromLocationId');
var fromLocationID = hdn_FromLocationId.value;
return fromLocationID;
}
 //*******************************************************************************
function GetBookingDate()
{ 
var booking_date = WucGCNew1_wuc_BookingDate.GetSelectedDate(); 
return booking_date;
}
 //*******************************************************************************
function GetDlyAreaID()
{ 
var hdn_ConsigneeDeliveryAreaID = document.getElementById('WucGCNew1_hdn_ConsigneeDeliveryAreaID'); 
return hdn_ConsigneeDeliveryAreaID;
} 
 //*******************************************************************************
function GetDly_TypeID()
{
var ddl_Dly_Type = document.getElementById('WucGCNew1_ddl_Dly_Type');
var Dly_TypeID = ddl_Dly_Type.value;
return Dly_TypeID;
} 
 //*******************************************************************************
function GetBkg_TypeID()
{
var ddl_Bkg_Type = document.getElementById('WucGCNew1_ddl_Booking_Type');
var Bkg_TypeID = ddl_Bkg_Type.value;
return Bkg_TypeID;
}

 //*******************************************************************************
function GetRateContract_ID()
{
var hdn_RateContractId = document.getElementById('WucGCNew1_hdn_RateContractId');
var RateContractId = hdn_RateContractId.value;
return RateContractId;
}

 //*******************************************************************************
function GetPayment_TypeID()
{
var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');
var PaymentTypeId = ddl_PaymentType.value;
return PaymentTypeId;
}


//*******************************************************************************
function CalculateDiscount()
{
var hdn_ConsignorId = document.getElementById('WucGCNew1_hdn_ConsignorId');
var hdn_ConsigneeId = document.getElementById('WucGCNew1_hdn_ConsigneeId');  
var hdn_IsRegularConsignor = document.getElementById('WucGCNew1_hdn_IsRegularConsignor');
var hdn_IsRegularConsignee = document.getElementById('WucGCNew1_hdn_IsRegularConsignee');
var hdn_BillingPartyId = document.getElementById('WucGCNew1_hdn_BillingPartyId');
var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');
var fromBranchID = GetBkgBranchID();
var toBranchID = GetDlyBranchID();
var hdn_TotalArticles  = document.getElementById('WucGCNew1_hdn_TotalArticles');
var hdn_Freight  = document.getElementById('WucGCNew1_hdn_Freight');
var hdn_ConsigneeDeliveryAreaID  = document.getElementById('WucGCNew1_hdn_ConsigneeDeliveryAreaID');
 
var booking_date = WucGCNew1_wuc_BookingDate.GetSelectedDate();
booking_date = new Date(booking_date.getFullYear(), booking_date.getMonth(),booking_date.getDate())

var consignorID = val(hdn_ConsignorId.value);
var consigneeID = val(hdn_ConsigneeId.value);

////if (hdn_IsRegularConsignor.value == '1')
////  consignorID = 0

////if (hdn_IsRegularConsignee.value == '1')
////  consigneeID = 0 


var txt_Discount = document.getElementById('WucGCNew1_txt_Discount');
var hdn_Discount = document.getElementById('WucGCNew1_hdn_Discount');
var hdn_DiscountId = document.getElementById('WucGCNew1_hdn_DiscountId');

    
var hdn_RateContractId = document.getElementById('WucGCNew1_hdn_RateContractId');

if (val(hdn_RateContractId.value) <= 0)
{
    Raj.EC.OperationModel.NewGCSearch.Get_GC_Discount
    (booking_date,val(consignorID),val(consigneeID),val(hdn_BillingPartyId.value),
    val(fromBranchID),val(toBranchID),val(ddl_PaymentType.value),
    val(hdn_TotalArticles.value),val(hdn_Freight.value),val(hdn_ConsigneeDeliveryAreaID.value)
    ,val(hdn_IsRegularConsignor.value)
    ,val(hdn_IsRegularConsignee.value)
    ,handleDiscount);
}
else
{
    txt_Discount.value = '0';
    hdn_Discount.value = '0';
    hdn_DiscountId.value = '0'; 
    Calculate_GrandTotal();

}


}
//*******************************************************************************
function handleDiscount(Results)
{
  if (Results.value != null)
  {
    var txt_Discount = document.getElementById('WucGCNew1_txt_Discount');
    var hdn_Discount = document.getElementById('WucGCNew1_hdn_Discount');
    var hdn_DiscountId = document.getElementById('WucGCNew1_hdn_DiscountId');
    var rows = Results.value.Rows[0];

    txt_Discount.value = val(rows['Discount']);
    hdn_Discount.value = val(rows['Discount']);
    hdn_DiscountId.value = val(rows['DiscountId']); 
    Calculate_GrandTotal();
  }
}
 //*******************************************************************************


function GetRateContract()
{
    var hdn_FromLocationId =  document.getElementById('WucGCNew1_hdn_FromLocationId');
    var hdn_ToLocationId =  document.getElementById('WucGCNew1_hdn_ToLocationId');
    
    var hdn_ConsignorId = document.getElementById('WucGCNew1_hdn_ConsignorId');
    var hdn_IsRegularConsignor = document.getElementById('WucGCNew1_hdn_IsRegularConsignor');

    var hdn_ConsigneeId = document.getElementById('WucGCNew1_hdn_ConsigneeId');  
    var hdn_IsRegularConsignee = document.getElementById('WucGCNew1_hdn_IsRegularConsignee');

    var hdn_BillingPartyId = document.getElementById('WucGCNew1_hdn_BillingPartyId');

    var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');

    var booking_date = WucGCNew1_wuc_BookingDate.GetSelectedDate();
    booking_date = new Date(booking_date.getFullYear(), booking_date.getMonth(),booking_date.getDate());

    Raj.EC.OperationModel.NewGCSearch.Get_RateContract
    (booking_date,val(hdn_FromLocationId.value),val(hdn_ToLocationId.value),val(hdn_ConsignorId.value),val(hdn_IsRegularConsignor.value),
    val(hdn_ConsigneeId.value),val(hdn_IsRegularConsignee.value),val(hdn_BillingPartyId.value),val(ddl_PaymentType.value),handleRateContract);

}

//*******************************************************************************
function handleRateContract(Results)
{
    var hdn_RateContractId = document.getElementById('WucGCNew1_hdn_RateContractId');
    var hdn_StationaryCharge_RateContract = document.getElementById('WucGCNew1_hdn_StationaryCharge_RateContract');
    var hdn_HamaliPerKg_RateContract = document.getElementById('WucGCNew1_hdn_HamaliPerKg_RateContract');
    var hdn_AOCPercentage_RateContract = document.getElementById('WucGCNew1_hdn_AOCPercentage_RateContract');
    var hdn_FOVPercentage_RateContract = document.getElementById('WucGCNew1_hdn_FOVPercentage_RateContract');
    var hdn_FOVExemptUpTo_RateContract = document.getElementById('WucGCNew1_hdn_FOVExemptUpTo_RateContract');

    if (Results.value != null)
    {
        var rows = Results.value.Rows[0];
    
        hdn_RateContractId.value = val(rows['RateContractID']); 
        hdn_StationaryCharge_RateContract.value = val(rows['StationaryCharge']); 
        hdn_HamaliPerKg_RateContract.value = val(rows['HamaliPerKg']);  
        hdn_AOCPercentage_RateContract.value = val(rows['AOCPercent']);  
        hdn_FOVPercentage_RateContract.value = val(rows['FOVPercent']);  
        hdn_FOVExemptUpTo_RateContract.value = val(rows['FOVExemptUpTo']);  
    }
    else
    {
        hdn_RateContractId.value = '0'; 
        hdn_StationaryCharge_RateContract.value = '0';
        hdn_HamaliPerKg_RateContract.value = '0';
        hdn_AOCPercentage_RateContract.value = '0';
        hdn_FOVPercentage_RateContract.value = '0';
        hdn_FOVExemptUpTo_RateContract.value = '0';
    }
    
    EnableDisable_On_RateContract();
}


function EnableDisable_On_RateContract()
{
    var hdn_RateContractId = document.getElementById('WucGCNew1_hdn_RateContractId');

    var txt_Freight = document.getElementById('WucGCNew1_txt_Freight');
    var txt_LocalCharge = document.getElementById('WucGCNew1_txt_LocalCharge');
    var txt_LoadingCharge = document.getElementById('WucGCNew1_txt_LoadingCharge');
    var txt_StationaryCharge = document.getElementById('WucGCNew1_txt_StationaryCharge');
    var txt_FOVRiskCharge = document.getElementById('WucGCNew1_txt_FOVRiskCharge');
    var txt_DDCharge = document.getElementById('WucGCNew1_txt_DDCharge');
    var txt_ODACharge = document.getElementById('WucGCNew1_txt_ODACharge');
    var txt_LengthCharge = document.getElementById('WucGCNew1_txt_LengthCharge');
    var txt_AOC = document.getElementById('WucGCNew1_txt_AOC');
    
    var ddl_BookingType = document.getElementById('WucGCNew1_ddl_Booking_Type');
    
    
    if (val(hdn_RateContractId.value) > 0 && ddl_BookingType.value == "1")
    {
        txt_Freight.disabled = true;
        txt_LocalCharge.disabled = true;
        txt_LoadingCharge.disabled = true;
        txt_StationaryCharge.disabled = true;
        txt_FOVRiskCharge.disabled = true;
        txt_DDCharge.disabled = true;
        txt_ODACharge.disabled = true;
        txt_LengthCharge.disabled = true;
        txt_AOC.disabled = true;
    }
    else if (val(hdn_RateContractId.value) > 0 && ddl_BookingType.value == "2")
    {
        txt_Freight.disabled = false;
        txt_LocalCharge.disabled = false;
        txt_LoadingCharge.disabled = false;
        txt_StationaryCharge.disabled = false;
        txt_FOVRiskCharge.disabled = true;
//        txt_DDCharge.disabled = false;
        txt_ODACharge.disabled = false;
        txt_LengthCharge.disabled = false;
        txt_AOC.disabled = true;
    }
    else
    {
        txt_Freight.disabled = false;
        txt_LocalCharge.disabled = false;
        txt_LoadingCharge.disabled = false;
        txt_StationaryCharge.disabled = false;
        txt_FOVRiskCharge.disabled = true;
//        txt_DDCharge.disabled = false;
        txt_ODACharge.disabled = false;
        txt_LengthCharge.disabled = false;
        txt_AOC.disabled = true;
    }
}

function Set_Exp_Del_Date()
{
    var booking_date = WucGCNew1_wuc_BookingDate.GetSelectedDate();
    var hdn_TransitDays = document.getElementById('WucGCNew1_hdn_TransitDays');
    
    var Transit_Day = val(hdn_TransitDays.value);

    var month = booking_date.getMonth();
    var date = booking_date.getDate();
    var dly_date = new Date(booking_date.getFullYear(), month,date);

    date  = date + Transit_Day;
    dly_date.setDate(date);

    var dtstr = dly_date.getDate()  + '-' + (dly_date.getMonth()+1) + '-' + dly_date.getFullYear();
    
    document.getElementById('WucGCNew1_lbl_ExpDel_Date_Value').innerHTML = dtstr;    
    document.getElementById('WucGCNew1_hdn_dly_date').value = dtstr;
}
 //*******************************************************************
 
function Validate_ChargeWeight()
{
    var ChargeWeight ;
    var ddl_FreightBasis = document.getElementById('WucGCNew1_ddl_FreightBasis');
    var ddl_VolumetricFreightUnit = document.getElementById('WucGCNew1_ddl_VolumetricFreightUnit');
    var txt_ActualWeight = document.getElementById('WucGCNew1_txt_ActualWeight');
    var txt_ChargeWeight = document.getElementById('WucGCNew1_txt_ChargeWeight');
    var hdn_ChargeWeight = document.getElementById('WucGCNew1_hdn_ChargeWeight');
    var hdn_TotalWeight = document.getElementById('WucGCNew1_hdn_TotalWeight');
    var hdn_TotalCFT = document.getElementById('WucGCNew1_hdn_TotalCFT');
    var txt_VolumetricToKgFactor = document.getElementById('WucGCNew1_txt_VolumetricToKgFactor');
    var hdn_RateCard_MinimumChargeWeight = document.getElementById('WucGCNew1_hdn_RateCard_MinimumChargeWeight');
    var MenuItemId  = document.getElementById('WucGCNew1_hdn_MenuItemId').value;

    ChargeWeight = 0;
    
    if (val(txt_ActualWeight.value) < val(hdn_TotalWeight.value))
        txt_ActualWeight.value = val(hdn_TotalWeight.value);

    if (val(txt_ChargeWeight.value) < val(txt_ActualWeight.value))
    {
        txt_ChargeWeight.value= val(txt_ActualWeight.value);
        hdn_ChargeWeight.value= val(txt_ActualWeight.value);
    }
    
     hdn_ChargeWeight.value = val(txt_ChargeWeight.value);

    if (val(ddl_FreightBasis.value) == 4 && val(ddl_VolumetricFreightUnit.value) == 3)
    {
        ChargeWeight = val(hdn_TotalCFT.value) * val(txt_VolumetricToKgFactor.value);
        ChargeWeight = val(txt_ChargeWeight.value);
    }

    var Mod = val(ChargeWeight) % 5;
    
    if (Mod > 0)
    {
        var Difference = 5-Mod;
        ChargeWeight = val(ChargeWeight) + Difference;
        ChargeWeight = val(ChargeWeight);
    }
    
    if(val(MenuItemId) != 200 && val(ChargeWeight) < val(hdn_RateCard_MinimumChargeWeight.value))
        ChargeWeight = val(hdn_RateCard_MinimumChargeWeight.value);
    
    if (val(txt_ChargeWeight.value) < val(ChargeWeight))
        txt_ChargeWeight.value = ChargeWeight ;
    
    txt_ChargeWeight.value = roundNumber(val(txt_ChargeWeight.value),0);        
    hdn_ChargeWeight.value= val(txt_ChargeWeight.value);           
}
//*********************************************************************************
 
   function Calculate_Freight(Call_From)
    { 
        var txt_Freight = document.getElementById('WucGCNew1_txt_Freight');
        var hdn_Freight = document.getElementById('WucGCNew1_hdn_Freight');
        txt_Freight.value = hdn_Freight.value;
        return;

        var MenuItemId = document.getElementById('WucGCNew1_hdn_MenuItemId').value;
        var ddl_FreightBasis = document.getElementById('WucGCNew1_ddl_FreightBasis');
        var ddl_VolumetricFreightUnit = document.getElementById('WucGCNew1_ddl_VolumetricFreightUnit');
        var hdn_TotalCFT = document.getElementById('WucGCNew1_hdn_TotalCFT');
        var hdn_TotalCBM = document.getElementById('WucGCNew1_hdn_TotalCBM');
        var txt_ChargeWeight = document.getElementById('WucGCNew1_txt_ChargeWeight');
        var hdn_TotalArticles = document.getElementById('WucGCNew1_hdn_TotalArticles');
        var hdn_TotalWeight = document.getElementById('WucGCNew1_hdn_TotalWeight');
        var txt_FreightRate = document.getElementById('WucGCNew1_txt_FreightRate');
        var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');
        var hdn_CompanyParameter_Standard_FreightRatePer= document.getElementById('WucGCNew1_hdn_CompanyParameter_Standard_FreightRatePer');
        var hdn_Standard_FreightRate = document.getElementById('WucGCNew1_hdn_Standard_FreightRate');
        var hdn_Standard_FreightAmount = document.getElementById('WucGCNew1_hdn_Standard_FreightAmount');
        var hdn_RateCard_Freight_Charge_Discount_Percent = document.getElementById('WucGCNew1_hdn_RateCard_Freight_Charge_Discount_Percent'); 
        var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');
        var hdn_IsContractApplied  = document.getElementById('WucGCNew1_hdn_IsContractApplied');
        var TempFreightRate = 0;

        if(hdn_IsContractApplied.value == "1")
            TempFreightRate = ContractFreightDetails[0][20];
        else
            TempFreightRate = val(hdn_Standard_FreightRate.value);
        
        
        var Freight_value = 0;
        var Standard_Freight_value = 0;
        var Discounted_Freight = 0;
        var Discount = 0;

        if (ddl_PaymentType.value != 5 && chk_IsAttached.checked == false)     // foc
        {
            if (val(ddl_FreightBasis.value) == 1)  // for wt
            {
                if(val(txt_ChargeWeight.value) < val(hdn_TotalWeight.value))
                {
                    txt_ChargeWeight.value = val(hdn_TotalWeight.value);
                    Calculate_ChargeWeight();
                }
                Freight_value = val(txt_ChargeWeight.value) / val(hdn_CompanyParameter_Standard_FreightRatePer.value)
                                 * val(txt_FreightRate.value);

                Standard_Freight_value = val(txt_ChargeWeight.value) / val(hdn_CompanyParameter_Standard_FreightRatePer.value)
                                     * val(TempFreightRate);               
            }
            else if(val(ddl_FreightBasis.value) == 2 )// for article
            {
                Freight_value = val(hdn_TotalArticles.value) * val(txt_FreightRate.value);
                Standard_Freight_value = val(hdn_TotalArticles.value) * val(TempFreightRate);
            }
            else if(val(ddl_FreightBasis.value) == 3 )// for Fixed
            {
                Freight_value = val(txt_FreightRate.value);
                Standard_Freight_value = val(TempFreightRate);
            }
            else if (val(ddl_FreightBasis.value) == 4 && val(ddl_VolumetricFreightUnit.value) == 1) // for cft
            {
                Calculate_CFT_CBM();
                Freight_value = val(hdn_TotalCFT.value) * val(txt_FreightRate.value);                
                Standard_Freight_value = val(hdn_TotalCFT.value) * val(TempFreightRate);                                
            }
            else if(val(ddl_FreightBasis.value) == 4 && val(ddl_VolumetricFreightUnit.value) == 2) // for cbm
            {
                Calculate_CFT_CBM();
                Freight_value = val(hdn_TotalCBM.value) * val(txt_FreightRate.value);               
                Standard_Freight_value = val(hdn_TotalCBM.value) * val(TempFreightRate);               
            }
            else if(val(ddl_FreightBasis.value) == 4 && val(ddl_VolumetricFreightUnit.value) == 3) // for kg
            {
                Validate_ChargeWeight();
                Freight_value = val(txt_ChargeWeight.value) / val(hdn_CompanyParameter_Standard_FreightRatePer.value)
                                * val(txt_FreightRate.value);

                Standard_Freight_value = val(txt_ChargeWeight.value) / val(hdn_CompanyParameter_Standard_FreightRatePer.value)
                                    * val(TempFreightRate);
            }
            
            Discount  =  Standard_Freight_value * val(hdn_RateCard_Freight_Charge_Discount_Percent.value) / 100 ; 
            Discounted_Freight = val(Standard_Freight_value) - val(Discount);
            
            if(val(MenuItemId) != 200)
            {
                if(Call_From == 'CallFromFreight')
                {
                    if(val(txt_Freight.value) < val(Standard_Freight_value) && val(txt_Freight.value) < val(Discounted_Freight))
                    {
                       txt_Freight.value = val(Standard_Freight_value);
                    } 
                    Get_Freight_Rate();
                }
                else
                    txt_Freight.value = val(Freight_value);
            }
            else
            {
                if(Call_From != 'CallFromFreight')
                {
                    txt_Freight.value = val(Freight_value);
                }
            }
        }
        else
        {
            Freight_value = val(0);
            Standard_Freight_value = val(0);
        }
        
        txt_Freight.value = roundNumber(txt_Freight.value,0);
        hdn_Freight.value = roundNumber(txt_Freight.value,0); 
        hdn_Standard_FreightAmount.value = roundNumber(Standard_Freight_value,2); 
    }
//**************************************************************************************
 
    function Calculate_GrandTotal()
    {
        var hdn_Standard_ServiceTaxPercent = document.getElementById('WucGCNew1_hdn_Standard_ServiceTaxPercent');
        var ddl_ServiceTaxPayableBy = document.getElementById('WucGCNew1_ddl_ServiceTaxPayableBy');  
        var hdn_Freight  = document.getElementById('WucGCNew1_hdn_Freight');
        var hdn_Discount = document.getElementById('WucGCNew1_hdn_Discount');
        var hdn_LocalCharge= document.getElementById('WucGCNew1_hdn_LocalCharge');
        var hdn_LoadingCharge= document.getElementById('WucGCNew1_hdn_LoadingCharge');
        var hdn_StationaryCharge = document.getElementById('WucGCNew1_hdn_StationaryCharge');
        var hdn_FOVRiskCharge = document.getElementById('WucGCNew1_hdn_FOVRiskCharge');
        var hdn_ODACharge = document.getElementById('WucGCNew1_hdn_ODACharge');
        var hdn_DDCharge = document.getElementById('WucGCNew1_hdn_DDCharge');
        var hdn_LengthCharge = document.getElementById('WucGCNew1_hdn_LengthCharge');
        var hdn_AOC = document.getElementById('WucGCNew1_hdn_AOC');
        var hdn_AOCPercent = document.getElementById('WucGCNew1_hdn_AOCPercent');
        var hdn_RoundOff = document.getElementById('WucGCNew1_hdn_RoundOff');
 
        var lbl_SubTotalValue = document.getElementById('WucGCNew1_lbl_SubTotalValue');
        var lbl_AbatmentValue = document.getElementById('WucGCNew1_lbl_AbatmentValue');
        var lbl_TaxableAmountValue = document.getElementById('WucGCNew1_lbl_TaxableAmountValue'); 
        var lbl_ServiceTaxValue = document.getElementById('WucGCNew1_lbl_ServiceTaxValue');
        var lbl_RoundOff = document.getElementById('WucGCNew1_lbl_RoundOff');
        var lbl_TotalGCAmountValue = document.getElementById('WucGCNew1_lbl_TotalGCAmountValue');
        
        var txt_AOC = document.getElementById('WucGCNew1_txt_AOC'); 
        var txt_CashAmount = document.getElementById('WucGCNew1_txt_CashAmount'); 
        var txt_ChequeAmount = document.getElementById('WucGCNew1_txt_ChequeAmount');  
        var txt_BankName= document.getElementById('WucGCNew1_txt_BankName');
        var txt_ChequeNo  =  document.getElementById('WucGCNew1_txt_ChequeNo');  

        var ddl_BookingType = document.getElementById('WucGCNew1_ddl_Booking_Type');
        var ddl_PaymentType= document.getElementById('WucGCNew1_ddl_PaymentType');
        var ddl_Dly_Type =  document.getElementById('WucGCNew1_ddl_Dly_Type');
        var hdn_SubTotal= document.getElementById('WucGCNew1_hdn_SubTotal');
        var hdn_Abatment= document.getElementById('WucGCNew1_hdn_Abatment');
        var hdn_TaxableAmount= document.getElementById('WucGCNew1_hdn_TaxableAmount');
        var hdn_TotalGCAmount= document.getElementById('WucGCNew1_hdn_TotalGCAmount');
        var hdn_ServiceTax= document.getElementById('WucGCNew1_hdn_ServiceTax');
        var hdn_Actual_ServiceTax = document.getElementById('WucGCNew1_hdn_Actual_ServiceTax');

        var txt_Advance = document.getElementById('WucGCNew1_txt_Advance');        
        var hdn_Advance = document.getElementById('WucGCNew1_hdn_Advance'); 
        var hdn_CashAmount= document.getElementById('WucGCNew1_hdn_CashAmount');
        var hdn_ChequeAmount= document.getElementById('WucGCNew1_hdn_ChequeAmount');
        var MenuItemId = document.getElementById('WucGCNew1_hdn_MenuItemId').value;         
        var hdn_Is_ServiceTax_ForCommodity = document.getElementById('WucGCNew1_hdn_Is_Service_Tax_Applicable_For_Commodity');
        var ddl_Service_Type = document.getElementById('WucGCNew1_ddl_Service_Type');
        var chk_IsSTAbatment_Req = document.getElementById('WucGCNew1_chk_Is_ST_Abatment_Required');

        var hdn_AbatePercentage  = document.getElementById('WucGCNew1_hdn_AbatePercentage');

        var IsServiceTaxApplicableForConsignor = document.getElementById('WucGCNew1_hdn_IsServiceTaxApplicableForConsignor').value;
        var IsServiceTaxApplicableForConsignee = document.getElementById('WucGCNew1_hdn_IsServiceTaxApplicableForConsignee').value;
        var IsServiceTaxApplForBillParty = document.getElementById('WucGCNew1_hdn_IsServiceTaxApplForBillParty').value;
        var ConsigneeStateId = document.getElementById('WucGCNew1_hdn_ConsigneeStateId').value;
        var ConsignorStateId = document.getElementById('WucGCNew1_hdn_ConsignorStateId').value;
    
        if (isNaN(hdn_Freight.value)) hdn_Freight.value = val(0);
        if (isNaN(hdn_Discount.value)) hdn_Discount.value = val(0);
        if (isNaN(hdn_LocalCharge.value)) hdn_LocalCharge.value = val(0);
        if (isNaN(hdn_LoadingCharge.value)) hdn_LoadingCharge.value = val(0);
        if (isNaN(hdn_StationaryCharge.value)) hdn_StationaryCharge.value = val(0);
        if (isNaN(hdn_FOVRiskCharge.value)) hdn_FOVRiskCharge.value = val(0);
        if (isNaN(hdn_ODACharge.value)) hdn_ODACharge.value = val(0);
        if (isNaN(hdn_DDCharge.value)) hdn_DDCharge.value = val(0);
        if (isNaN(hdn_LengthCharge.value)) hdn_LengthCharge.value = val(0);
        if (isNaN(hdn_AOCPercent.value)) hdn_AOCPercent.value = val(0);
        if (isNaN(hdn_AbatePercentage.value)) hdn_AbatePercentage.value = val(0);
        

        var hdn_RateContractId = document.getElementById('WucGCNew1_hdn_RateContractId');
        var hdn_StationaryCharge_RateContract = document.getElementById('WucGCNew1_hdn_StationaryCharge_RateContract');
        var hdn_AOCPercentage_RateContract = document.getElementById('WucGCNew1_hdn_AOCPercentage_RateContract');
        var txt_StationaryCharge = document.getElementById('WucGCNew1_txt_StationaryCharge');


        if (val(hdn_RateContractId.value) > 0)
        {
            txt_StationaryCharge.value = hdn_StationaryCharge_RateContract.value;
            hdn_StationaryCharge.value = hdn_StationaryCharge_RateContract.value;
        }
        
        var SubTotal  = val(hdn_Freight.value) - val(hdn_Discount.value) + val(hdn_LocalCharge.value)
                        + val(hdn_LoadingCharge.value) + val(hdn_StationaryCharge.value)
                        + val(hdn_FOVRiskCharge.value) + val(hdn_ODACharge.value) + val(hdn_DDCharge.value)
                        + val(hdn_LengthCharge.value);
                        
        var hdn_Mode = document.getElementById('WucGCNew1_hdn_Mode');
        
        
        if (val(hdn_Mode.value) != 4) 
        {
            if (val(hdn_RateContractId.value) <= 0)
            {
                hdn_AOC.value = val(SubTotal) * (val(hdn_AOCPercent.value)/100);
                hdn_AOC.value = Math.round(val(hdn_AOC.value));
            }
            else
            {
                hdn_AOC.value = val(SubTotal) * (val(hdn_AOCPercentage_RateContract.value)/100);
                hdn_AOC.value = Math.round(val(hdn_AOC.value));
            }
        }
        
        txt_AOC.value = hdn_AOC.value;
        
        SubTotal = val(SubTotal)  + val(hdn_AOC.value);
        
        SubTotal = Math.round(val(SubTotal));

        if(val(ddl_PaymentType.value) == 5)
        {
            SubTotal = val(0);
        }
        
        hdn_SubTotal.value = val(SubTotal);

//        var Tax_Abate = val(SubTotal) * 0.75;
        var Tax_Abate = val(SubTotal) * val(hdn_AbatePercentage.value);
        if (isNaN(Tax_Abate)) Tax_Abate = val(0);
        if (val(SubTotal) < 750 && ddl_BookingType.value == 1) Tax_Abate = val(0);
        if (val(SubTotal) < 1500 && ddl_BookingType.value != 1) Tax_Abate = val(0);
        if (ddl_ServiceTaxPayableBy.value != 3) Tax_Abate = val(0); // for transporter

        if (val(ddl_Service_Type.value) == 2 && chk_IsSTAbatment_Req.checked == false) //31 oct 09
        {
            Tax_Abate =  val(0);
        }
        if (isNaN(Tax_Abate)) Tax_Abate = val(0);
        hdn_Abatment.value = Math.round(val(Tax_Abate));

        var Amt_Taxable = val(SubTotal) - val(Tax_Abate);
        if (isNaN(Amt_Taxable)) Amt_Taxable = val(0);

        if (val(SubTotal) < 750 && ddl_BookingType.value == 1) Amt_Taxable = val(0); // for sundry
        if (val(SubTotal) < 1500 && ddl_BookingType.value != 1) Amt_Taxable = val(0); // for FTL

        if (val(ddl_Service_Type.value) == 2 && chk_IsSTAbatment_Req.checked == false) //31 oct 09
        {
            Amt_Taxable =  val(SubTotal) - val(Tax_Abate);
        }
        if (isNaN(Amt_Taxable)) Amt_Taxable = val(0);
        hdn_TaxableAmount.value = Math.round(val(Amt_Taxable)); 

        var ServiceTaxPercent = val(hdn_Standard_ServiceTaxPercent.value);
        var ServiceTax =(ServiceTaxPercent /100) * val(Amt_Taxable);

        if (val(SubTotal) < 750 && ddl_BookingType.value == 1) ServiceTax = val(0); // for sundry
        if (val(SubTotal) < 1500 && ddl_BookingType.value != 1) ServiceTax = val(0); // for FTL

        if (val(ddl_Service_Type.value) == 2 && chk_IsSTAbatment_Req.checked == false) //31 oct 09
        {
            ServiceTax =  (ServiceTaxPercent /100) * val(Amt_Taxable);
        }
        ServiceTax = Math.round(val(ServiceTax));
        
        if (isNaN(ServiceTax)) ServiceTax = val(0);
        hdn_ServiceTax.value = val(ServiceTax); 

        var GrandTotal = val(0);        
        if (hdn_Is_ServiceTax_ForCommodity.value == '0')
        {
          ServiceTax =  val(0);
          hdn_Abatment.value = val(0);
          hdn_TaxableAmount.value = val(0);
          hdn_ServiceTax.value = ServiceTax;
        }
//-----------------------------------------------------------
        var booking_date;
        var applicableDate = new Date("December 01, 2017 00:00:00")
        booking_date = WucGCNew1_wuc_BookingDate.GetSelectedDate();
        booking_date = new Date(booking_date.getFullYear(), booking_date.getMonth(),booking_date.getDate())
    
        if(booking_date >= applicableDate)
        {
          if (val(ConsigneeStateId) == val(ConsignorStateId))
          {
            if(val(ddl_PaymentType.value) == 1 && IsServiceTaxApplicableForConsignee == '0')
            {
              ServiceTax =  val(0);
              hdn_Abatment.value = val(0);
              hdn_TaxableAmount.value = val(0);
              hdn_ServiceTax.value = ServiceTax;
            }

            else if(val(ddl_PaymentType.value) == 2 && IsServiceTaxApplicableForConsignor == '0')
            {
              ServiceTax =  val(0);
              hdn_Abatment.value = val(0);
              hdn_TaxableAmount.value = val(0);
              hdn_ServiceTax.value = ServiceTax;
            }
            
            else if(val(ddl_PaymentType.value) == 4 && IsServiceTaxApplicableForConsignor == '0')
            {
              ServiceTax =  val(0);
              hdn_Abatment.value = val(0);
              hdn_TaxableAmount.value = val(0);
              hdn_ServiceTax.value = ServiceTax;
            }
            
            else if(val(ddl_PaymentType.value) == 3 && IsServiceTaxApplForBillParty == '0')
            {
              ServiceTax =  val(0);
              hdn_Abatment.value = val(0);
              hdn_TaxableAmount.value = val(0);
              hdn_ServiceTax.value = ServiceTax;
            }
          }
        }
        
//-----------------------------------------------------------

        if (val(ddl_ServiceTaxPayableBy.value) != 3) // service tax paid by client 
        {
            GrandTotal = val(SubTotal);
            hdn_Actual_ServiceTax.value = 0;
        }
        else
        {
            GrandTotal= val(SubTotal) + val(ServiceTax);
            hdn_Actual_ServiceTax.value = val(ServiceTax);
        }
        
        if (val(ddl_Service_Type.value) == 2 && chk_IsSTAbatment_Req.checked == false) //31 oct 09
        {
            GrandTotal= val(SubTotal) + val(ServiceTax);
            hdn_Actual_ServiceTax.value = val(ServiceTax);
        }

        lbl_RoundOff.innerHTML = "0";
        hdn_RoundOff.value = "0";
  
 
        var RoundOff = val(GrandTotal) % 10;
        RoundOff = Math.round(val(RoundOff));
        if (ddl_BookingType.value == 1)  //1	Sundry
        {
            if (val(GrandTotal) >740 && val(GrandTotal) < 750)
            {
                if (val(RoundOff)!= 0)
                 {
                    GrandTotal = val(GrandTotal) - val(RoundOff);
                    lbl_RoundOff.innerHTML = "-" + val(RoundOff);
                 } 
            }
            else
            {  
                if (val(RoundOff)!= 0 && val(RoundOff) > 4)
                {
                    GrandTotal = val(GrandTotal) + val(10 - RoundOff);
                    lbl_RoundOff.innerHTML = val(10 - RoundOff);
                }
                else if (val(RoundOff)!= 0)
                {
                    GrandTotal = val(GrandTotal) - val(RoundOff);
                    lbl_RoundOff.innerHTML = "-" + val(RoundOff);
                } 
             }
              hdn_RoundOff.value = lbl_RoundOff.innerHTML;  
        }
        else  //if (ddl_BookingType.value == 2) 2	FTL
        {
        
            if (val(GrandTotal) > 1490 && val(GrandTotal) < 1500)
            {
                if (val(RoundOff)!= 0)
                 {
                    GrandTotal = val(GrandTotal) - val(RoundOff);
                    lbl_RoundOff.innerHTML = "-" + val(RoundOff);
                 } 
            }
            else
            {    
                if (val(RoundOff)!= 0 && val(RoundOff) > 4)
                {
                    GrandTotal = val(GrandTotal) + val(10 - RoundOff);
                    lbl_RoundOff.innerHTML = val(10 - RoundOff);
                }
                else if (val(RoundOff)!= 0)
                {
                    GrandTotal = val(GrandTotal) - val(RoundOff);
                    lbl_RoundOff.innerHTML = "-" + val(RoundOff);
                }
            }
            hdn_RoundOff.value = lbl_RoundOff.innerHTML;  
        }

 
        hdn_TotalGCAmount.value = Math.round(val(GrandTotal));
        lbl_SubTotalValue.innerHTML = val(hdn_SubTotal.value);
        lbl_AbatmentValue.innerHTML = val(hdn_Abatment.value);
        lbl_TaxableAmountValue.innerHTML = val(hdn_TaxableAmount.value);
////////        lbl_ServiceTaxValue.innerHTML = val(hdn_ServiceTax.value);
        lbl_TotalGCAmountValue.innerHTML =  val(hdn_TotalGCAmount.value); 

        var hdn_Mode = document.getElementById('WucGCNew1_hdn_Mode');
        if (val(hdn_Mode.value) != 4) 
        {
            lbl_ServiceTaxValue.innerHTML = val(hdn_ServiceTax.value); 
        }
        
        if(val(ddl_PaymentType.value) == 2 || val(ddl_PaymentType.value) == 4)
        {
            txt_Advance.value=  val(0);
            hdn_Advance.value=  val(0);

            if (val(hdn_TotalGCAmount.value) < val(txt_ChequeAmount.value))
            {
                txt_ChequeAmount.value = val(0);
                hdn_ChequeAmount.value = val(0);
                txt_ChequeNo.value = "";
                txt_BankName.value = "";
            }
            txt_CashAmount.value = val(hdn_TotalGCAmount.value) - val(txt_ChequeAmount.value);
            hdn_CashAmount.value = val(txt_CashAmount.value);  
        }

        Set_Payment_Details();
        
        EnableDisable_On_RateContract();
    }
     
function Disable_On_MenuItemBasis()
{
    var tr_Opening_Details = document.getElementById('WucGCNew1_tr_Opening_Details');
    var tr_Agency_Details = document.getElementById('WucGCNew1_tr_Agency_Details');
    var tr_Copy_GC_Details = document.getElementById('WucGCNew1_tr_Copy_GC_Details');
    var lnk_View_Details = document.getElementById('WucGCNew1_lnk_View_Details');

    var MenuItemId = document.getElementById('WucGCNew1_hdn_MenuItemId').value;
    var tr_ReBookCharge = document.getElementById('WucGCNew1_tr_ReBookCharge');
    var txt_ReBookGCAmount = document.getElementById('WucGCNew1_txt_ReBookGCAmount');
    var hdn_ReBookGCAmount = document.getElementById('WucGCNew1_hdn_ReBookGCAmount');
    var hdn_ReBookGC_OctroiAmount = document.getElementById('WucGCNew1_hdn_ReBookGC_OctroiAmount');
    var tr_ReBookOctroiAmount = document.getElementById('WucGCNew1_tr_ReBookOctroiAmount');
    var tr_DACC = document.getElementById('WucGCNew1_tr_DACC');
    var lnk_Get_Next_No = document.getElementById('WucGCNew1_lnk_Get_Next_No');
    var td_chk_IsReBook = document.getElementById('WucGCNew1_td_chk_IsReBook');
    var ddl_FreightBasis = document.getElementById('WucGCNew1_ddl_FreightBasis');
    var tr_Contractual_Client_Details = document.getElementById('WucGCNew1_tr_Contractual_Client_Details');
    var tr_Contract_Branch_Details = document.getElementById('WucGCNew1_tr_Contract_Branch_Details');
    var tr_Contract_Details = document.getElementById('WucGCNew1_tr_Contract_Details');
    var ddl_GC_No = document.getElementById('WucGCNew1_ddl_GC_No');
    var lbl_GC_No = document.getElementById('WucGCNew1_lbl_GC_No');
    var lbl_pickup_reqId = document.getElementById('WucGCNew1_lbl_pickup_reqId');
    var ddl_pick_request = document.getElementById('WucGCNew1_ddl_pick_request');

    var ClientCode = document.getElementById('WucGCNew1_hdn_ClientCode').value;

    tr_Opening_Details.style.display = 'none';
    tr_Agency_Details.style.display = 'none';
    lnk_View_Details.style.display = 'none';
    
    tr_ReBookCharge.style.display = 'none';
    tr_ReBookOctroiAmount.style.display = 'none';
    tr_DACC.style.display = 'none';
    td_chk_IsReBook.style.display = 'none';
    ddl_GC_No.style.display = 'none';
    lbl_GC_No.style.display = 'none';

    if(val(MenuItemId) == 200) // Opening gc
    {
        tr_Opening_Details.style.display = 'inline';
        tr_Copy_GC_Details.style.display = 'none';
        lnk_Get_Next_No.style.display = 'none';
        tr_Contractual_Client_Details.style.display = 'none';
        tr_Contract_Branch_Details.style.display = 'none';
        tr_Contract_Details.style.display = 'none';
        lbl_pickup_reqId.style.display = 'none';
        ddl_pick_request.style.display = 'none';

        if(ClientCode == 'nandwana')
        {
            ddl_GC_No.style.display = 'inline';
            lbl_GC_No.style.display = 'inline';
        }
    }
    else if(val(MenuItemId) == 229) // Agency gc
    {
        ddl_FreightBasis.disabled = true;
        tr_Agency_Details.style.display = 'inline';
        tr_Copy_GC_Details.style.display = 'none';
        tr_Contractual_Client_Details.style.display = 'none';
        tr_Contract_Branch_Details.style.display = 'none';
        tr_Contract_Details.style.display = 'none';
        lbl_pickup_reqId.style.display = 'none';
        ddl_pick_request.style.display = 'none';
    }
    else if(val(MenuItemId) == 188) // IBA gc
    {
        tr_DACC.style.display = 'inline';
    }
    else if(val(MenuItemId) == 184) // Rebook gc
    {
        txt_ReBookGCAmount.value = '0';
        hdn_ReBookGCAmount.value = '0';
        hdn_ReBookGC_OctroiAmount.value = '0';
        td_chk_IsReBook.style.display = 'inline';
        tr_ReBookCharge.style.display = 'inline';
        tr_ReBookOctroiAmount.style.display = 'inline';
        tr_Contractual_Client_Details.style.display = 'none';
        tr_Contract_Branch_Details.style.display = 'none';
        tr_Contract_Details.style.display = 'none';
        lbl_pickup_reqId.style.display = 'none';
        ddl_pick_request.style.display = 'none';
    }
    else if(val(MenuItemId) == 194) // gc Rectification
    {
        tr_Copy_GC_Details.style.display = 'none';
        lnk_View_Details.style.display = 'inline';
        Call_For_GC_Rectification();
    }
}

function Disable_Controls_On_ClientWise()
{
    var tr_UnitOfMeasurment = document.getElementById('WucGCNew1_tr_UnitOfMeasurment');
    var tr_Instruction = document.getElementById('WucGCNew1_tr_Instruction');
    var tr_InstructionRemarks = document.getElementById('WucGCNew1_tr_InstructionRemarks');
    var tr_Enclosure = document.getElementById('WucGCNew1_tr_Enclosure');
    var tr_EmployeeSupervisor = document.getElementById('WucGCNew1_tr_EmployeeSupervisor');
    var tr_lengthCharge = document.getElementById('WucGCNew1_tr_lengthCharge');
    var tr_lengthChargeValue = document.getElementById('WucGCNew1_tr_lengthChargeValue');
    var tr_NFormChargeValue = document.getElementById('WucGCNew1_tr_NFormChargeValue');
    var tr_UnloadingChargeValue = document.getElementById('WucGCNew1_tr_UnloadingChargeValue');

    var tr_ToPayCharge = document.getElementById('WucGCNew1_tr_ToPayCharge');
    var tr_onBookingTypechange = document.getElementById('WucGCNew1_tr_onBookingTypechange');
    var tr_ConsignmentType = document.getElementById('WucGCNew1_tr_ConsignmentType');
    var tr_STMNo = document.getElementById('WucGCNew1_tr_STMNo');
    
    var lbl_CustomerRefNo = document.getElementById('WucGCNew1_lbl_CustomerRefNo');
    var txt_CustomerRefNo = document.getElementById('WucGCNew1_txt_CustomerRefNo');
    var lbl_DelWay_Type = document.getElementById('WucGCNew1_lbl_DelWay_Type');
    var ddl_DelWay_Type = document.getElementById('WucGCNew1_ddl_DelWay_Type');
    var ddl_booking_Sub_Type = document.getElementById('WucGCNew1_ddl_booking_Sub_Type');
    var lbl_Booking_Sub_Type = document.getElementById('WucGCNew1_lbl_Booking_Sub_Type');
    var chk_SignedByConsignor = document.getElementById('WucGCNew1_chk_SignedByConsignor');
    var tr_CnsrsearchByCode = document.getElementById('WucGCNew1_tr_CnsrsearchByCode');
    var tr_CsneesearchByCode = document.getElementById('WucGCNew1_tr_CsneesearchByCode');
    var tbl_Commodity = document.getElementById('WucGCNew1_tbl_Commodity');
    var tr_Commodity = document.getElementById('WucGCNew1_tr_Commodity');
    var tr_OCRemarks = document.getElementById('WucGCNew1_tr_OCRemarks');
    var lbl_Private_Mark = document.getElementById('WucGCNew1_lbl_Private_Mark');
    var td_PrivateMark = document.getElementById('WucGCNew1_td_PrivateMark');
    var tr_Copy_GC_Details = document.getElementById('WucGCNew1_tr_Copy_GC_Details');
    var lbl_BillingHierarchy = document.getElementById('WucGCNew1_lbl_BillingHierarchy');
    var ddl_BillingHierarchy = document.getElementById('WucGCNew1_ddl_BillingHierarchy');
    var tr_Contractual_Client_Details = document.getElementById('WucGCNew1_tr_Contractual_Client_Details');
    var tr_Contract_Branch_Details = document.getElementById('WucGCNew1_tr_Contract_Branch_Details');
    var tr_Contract_Details = document.getElementById('WucGCNew1_tr_Contract_Details');
    var lbl_Service_Type = document.getElementById('WucGCNew1_lbl_Service_Type');
    var ddl_Service_Type = document.getElementById('WucGCNew1_ddl_Service_Type');

    var ClientCode = document.getElementById('WucGCNew1_hdn_ClientCode').value;

    lbl_DelWay_Type.style.display = 'none';
    ddl_DelWay_Type.style.display = 'none';
    tr_NFormChargeValue.style.display = 'none';
    tr_UnloadingChargeValue.style.display = 'none';
    ddl_booking_Sub_Type.style.display = 'none';
    lbl_Booking_Sub_Type.style.display = 'none';
    tr_lengthCharge.style.display = 'none';
//    tr_lengthChargeValue.style.display = 'none';
    tr_OCRemarks.style.display = 'inline';
    lbl_Service_Type.style.display = 'none';
    ddl_Service_Type.style.display = 'none';

    if(ClientCode == 'nandwana')
    {
        tr_UnitOfMeasurment.style.display = 'none';
        tr_Instruction.style.display = 'none';
        tr_InstructionRemarks.style.display = 'none';
        tr_Enclosure.style.display = 'none';
        tr_EmployeeSupervisor.style.display = 'none';
        tr_ToPayCharge.style.display = 'none';
        tr_onBookingTypechange.style.display = 'none';
        tr_ConsignmentType.style.display = 'none';
        tr_STMNo.style.display = 'none';
        lbl_BillingHierarchy.style.display = 'none';
        ddl_BillingHierarchy.style.display = 'none';
        
        lbl_CustomerRefNo.style.display = 'none';
        txt_CustomerRefNo.style.display = 'none';
        chk_SignedByConsignor.style.display = 'none';
        chk_SignedByConsignor.value = '';
//        tr_CnsrsearchByCode.style.display = 'none';
//        tr_CsneesearchByCode.style.display = 'none';
//        tr_Commodity.style.display = 'inline';
        tbl_Commodity.style.display = 'none';
        tr_Copy_GC_Details.style.display = 'none';
        tr_Contractual_Client_Details.style.display = 'none';
        tr_Contract_Branch_Details.style.display = 'none';
        tr_Contract_Details.style.display = 'none';
    }
    else if(ClientCode == 'reach')
    {
        tr_NFormChargeValue.style.display = 'none';
        tr_UnloadingChargeValue.style.display = 'none';
        tr_Copy_GC_Details.style.display = 'inline';
        tr_OCRemarks.style.display = 'none';
        tr_Commodity.style.display = 'none';
        lbl_Service_Type.style.display = 'inline';
        ddl_Service_Type.style.display = 'inline';
    }
    else if(ClientCode == 'excel')
    {
        lbl_DelWay_Type.style.display = 'inline';
        ddl_DelWay_Type.style.display = 'inline';
        lbl_Private_Mark.style.display = 'inline';
        td_PrivateMark.style.display = 'inline';
        ddl_booking_Sub_Type.style.display = 'inline';
        lbl_Booking_Sub_Type.style.display = 'inline';
        tr_lengthCharge.style.display = 'inline';
        tr_lengthChargeValue.style.display = 'inline';
        tr_OCRemarks.style.display = 'none';
        tr_Commodity.style.display = 'none';
        tr_Copy_GC_Details.style.display = 'inline';
    }
    
    var tr_WholeselerBroker = document.getElementById('WucGCNew1_tr_WholeselerBroker');
    var hdn_BookingBranchId = document.getElementById('WucGCNew1_hdn_BookingBranchId');
    var lbl_WholeselerBroker = document.getElementById('WucGCNew1_lbl_WholeselerBroker');
    
    if (hdn_BookingBranchId.value != '53')
    {
        tr_WholeselerBroker.style.display = 'none';
    }
    else
    {
        tr_WholeselerBroker.style.display = 'inline';
        lbl_WholeselerBroker.innerHTML = "APMC Broker :";
    }    
}
  //***************************************************************************************
 
    function On_BillingHierarchy_Change()
    {
        var ddl_BillingHierarchy= document.getElementById('WucGCNew1_ddl_BillingHierarchy');  
        var lbl_BillingLocation= document.getElementById('WucGCNew1_lbl_BillingLocation');  
        var txt_BillingLocation = document.getElementById('WucGCNew1_txt_BillingLocation');
        var hdn_BillingLocationId = document.getElementById('WucGCNew1_hdn_BillingLocationId');
        var hdn_BillingHierarchy= document.getElementById('WucGCNew1_hdn_BillingHierarchy');  

        txt_BillingLocation.value = '';
        hdn_BillingLocationId.value = '0';
        hdn_BillingHierarchy.value = ddl_BillingHierarchy.value;
        
        On_BillingHierarchy_Load();
    }
    
    function On_BillingHierarchy_Load()
    {
        var ddl_BillingHierarchy= document.getElementById('WucGCNew1_ddl_BillingHierarchy');  
        var td_Bill_Location= document.getElementById('WucGCNew1_td_Bill_Location');
        var lbl_BillingLocation= document.getElementById('WucGCNew1_lbl_BillingLocation');  

        if(ddl_BillingHierarchy.value == 'RO')
            lbl_BillingLocation.innerHTML = 'Region :'; 
        else if(ddl_BillingHierarchy.value == 'AO')
            lbl_BillingLocation.innerHTML = 'Area :'; 
        else if(ddl_BillingHierarchy.value == 'BO')
            lbl_BillingLocation.innerHTML = 'Branch :'; 

        if(ddl_BillingHierarchy.value == 'HO' || ddl_BillingHierarchy.value == '0')
        {
            lbl_BillingLocation.style.display = 'none';
            td_Bill_Location.style.display = 'none';
        }
        else
        {
            lbl_BillingLocation.style.display = 'inline';
            td_Bill_Location.style.display = 'inline';
        }
    }
    
     //*******************************************************************
 
    function OnPageLoad_ConsignorConsignee()
    {
    
        EnableDisable_On_PaymentType_Change();
    
        var tr_Consignor_Details = document.getElementById('WucGCNew1_tr_Consignor_Details');
        var tr_Consignee_Details = document.getElementById('WucGCNew1_tr_Consignee_Details');
        var chk_Is_CnrCne_Details_Shown = document.getElementById('WucGCNew1_chk_Is_Consignor_Consignee_Details_Shown');
        var Consignor_Id = document.getElementById('WucGCNew1_hdn_ConsignorId').value;
        var Consignee_Id = document.getElementById('WucGCNew1_hdn_ConsigneeId').value;
        
        var lnk_EditConsignor = document.getElementById('WucGCNew1_lnk_EditConsignor');
        var lnk_ViewConsignor = document.getElementById('WucGCNew1_lnk_ViewConsignor');
        var lnk_EditConsignee = document.getElementById('WucGCNew1_lnk_EditConsignee');
        var lnk_ViewConsignee = document.getElementById('WucGCNew1_lnk_ViewConsignee');
        
        var hdn_IsRegularConsignor = document.getElementById('WucGCNew1_hdn_IsRegularConsignor');         
        var hdn_IsRegularConsignee = document.getElementById('WucGCNew1_hdn_IsRegularConsignee');

        if(Consignor_Id > 0)
        {
            lnk_EditConsignor.style.display = 'inline';
            lnk_ViewConsignor.style.display = 'inline';
        }
        else
        {
            lnk_EditConsignor.style.display = 'none';
            lnk_ViewConsignor.style.display = 'none';
        }
        
        if(Consignee_Id > 0)
        {
            lnk_EditConsignee.style.display = 'inline';
            lnk_ViewConsignee.style.display = 'inline';
        }
        else
        {
            lnk_EditConsignee.style.display = 'none';
            lnk_ViewConsignee.style.display = 'none';
        }
        
        if(chk_Is_CnrCne_Details_Shown.checked == true)
        {
            tr_Consignor_Details.style.display = 'inline';
            tr_Consignee_Details.style.display = 'inline';
        }
        else
        {
            tr_Consignor_Details.style.display = 'none';
            tr_Consignee_Details.style.display = 'none';
        }
        
        if(hdn_IsRegularConsignor.value == "1")
            lnk_EditConsignor.style.visibility = 'visible';
        else
            lnk_EditConsignor.style.visibility = 'hidden';
        
        if(hdn_IsRegularConsignee.value == "1")
            lnk_EditConsignee.style.visibility = 'visible';
        else
            lnk_EditConsignee.style.visibility = 'hidden';
    }
    
  //*********************************Other Charge Grid On Amount Change and Check Box Checked**********************************
 
    function Check_Single(gridname)
    {
        var grid = document.getElementById(gridname);
        var Sum_Total_GC_Other_Charges = val(0);
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');

        var checkbox;
        var i;

        var lbl_TotalGCOtherCharges = document.getElementById('lbl_TotalGCOtherCharges');             
        var hdn_TotalGCOtherCharges = document.getElementById('hdn_TotalGCOtherCharges');        
        var hdn_OtherChargesCount = document.getElementById('hdn_OtherChargesCount');

        var max = grid.rows.length - 1;
        
        for(i=1;i<grid.rows.length;i++)
        {
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            Amount = grid.rows[i].cells[3].getElementsByTagName('input');

            if(checkbox[0].checked  == true)
            {
                if(Amount[0].type =='text')
                {
                    Sum_Total_GC_Other_Charges = Sum_Total_GC_Other_Charges + val(Amount[0].value);
                }
            }
        }
        hdn_OtherChargesCount.value = max;
        lbl_TotalGCOtherCharges.innerHTML = Sum_Total_GC_Other_Charges;
        hdn_TotalGCOtherCharges.value = Sum_Total_GC_Other_Charges;
    }      

  //*********************************Other Charge Grid On Select Check All**********************************
 
    function Check_All(chk,gridname)
    {
        var grid = document.getElementById(gridname);
        var Sum_Total_GC_Other_Charges = val(0);
        var checkbox;
        var i;

        var lbl_TotalGCOtherCharges = document.getElementById('lbl_TotalGCOtherCharges');             
        var hdn_TotalGCOtherCharges = document.getElementById('hdn_TotalGCOtherCharges');        
        var hdn_OtherChargesCount = document.getElementById('hdn_OtherChargesCount');
        var max = (grid.rows.length - 1);

        for(i=1;i<grid.rows.length;i++)
        {
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');             
            txt_Amount = grid.rows[i].cells[3].getElementsByTagName('input');

            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            }
            if(chk.checked == true)
            {
                if(txt_Amount[0].type =='text')
                {
                    Sum_Total_GC_Other_Charges = Sum_Total_GC_Other_Charges + val(txt_Amount[0].value);
                }
            }
        }

        if(chk.checked == true)
        {
            hdn_OtherChargesCount.value = max;
            lbl_TotalGCOtherCharges.innerHTML = Sum_Total_GC_Other_Charges;
            hdn_TotalGCOtherCharges.value = Sum_Total_GC_Other_Charges;
        }
        else
        {
            hdn_OtherChargesCount.value = val(0);
            lbl_TotalGCOtherCharges.innerHTML = val(0);
            hdn_TotalGCOtherCharges.value = val(0);
        }
    }
///----------------------------------- GC No Validation ------------------------------/////////////

function Check_GC_No()
{
    var GC_Start_No =  document.getElementById('WucGCNew1_hdn_Start_No').value;
    var GC_End_No =  document.getElementById('WucGCNew1_hdn_End_No').value;
    var Txt_GC_No =  document.getElementById('WucGCNew1_txt_GC_No');
    var ATC = false;
    
    SetErrorMsg('');
    
    if(val(Txt_GC_No.value) < val(GC_Start_No) || val(Txt_GC_No.value) > val(GC_End_No))
    {
        SetErrorMsg('GC No. should be between '+ GC_Start_No + ' and '+ GC_End_No);
        Txt_GC_No.focus();
    }
    else
    {
        ATC = true;
    }
    return ATC;
}

function Check_Valid_GC_No()
{
 
    var GC_Id = document.getElementById('WucGCNew1_hdn_GCId').value;
    var GC_No =  document.getElementById('WucGCNew1_txt_GC_No').value;
    var MenuItemId =  document.getElementById('WucGCNew1_hdn_MenuItemId').value;
    var Txt_GC_No =  document.getElementById('WucGCNew1_txt_GC_No');

    Append_Zero_In_GC_No(Txt_GC_No);

    if(GC_Id <= 0)
    {
        if(MenuItemId != 200 && val(GC_No) > 0 && Check_GC_No() == true)
        {
           Raj.EC.OperationModel.NewGCSearch.Check_Duplicate_GC_No(val(GC_Id),val(GC_No),val(MenuItemId),handleValidGC);
        }
        else if(MenuItemId == 200 && val(GC_No) > 0)
        {
           Raj.EC.OperationModel.NewGCSearch.Check_Duplicate_GC_No(val(GC_Id),val(GC_No),val(MenuItemId),handleValidGC);
        }
    }
}

function handleValidGC(Results)
{
    var Txt_GC_No =  document.getElementById('WucGCNew1_txt_GC_No');
    var hdn_GC_No =  document.getElementById('WucGCNew1_hdn_GC_No');
    var txt_Private_Mark =  document.getElementById('WucGCNew1_txt_Private_Mark');
    var hdn_Private_Mark =  document.getElementById('WucGCNew1_hdn_Private_Mark');
    var Result = Results.value.Rows[0]['Is_Duplicate'];

    SetErrorMsg('');

    if(Result == true)
    {
        SetErrorMsg('Duplicate GC No.');
        Txt_GC_No.focus();
    }
    else
    {
        txt_Private_Mark.value = Txt_GC_No.value;
        hdn_GC_No.value = Txt_GC_No.value;
        hdn_Private_Mark.value = Txt_GC_No.value;
        SetErrorMsg('');
    }
 } 
 
function Get_Next_No()
{
    var VA_Id = document.getElementById('WucGCNew1_hdn_VAId').value;
    var MenuItemId =  document.getElementById('WucGCNew1_hdn_MenuItemId').value;
    var GC_Id = document.getElementById('WucGCNew1_hdn_GCId').value;

    if(val(GC_Id) <= 0)
    {
        Raj.EC.OperationModel.NewGCSearch.Get_Next_GC_No(val(VA_Id),val(0),val(MenuItemId),handle_NextGCNo);
    }
   return false;
}

function handle_NextGCNo(Results)
{
    var Txt_GC_No =  document.getElementById('WucGCNew1_txt_GC_No');
    var txt_Private_Mark =  document.getElementById('WucGCNew1_txt_Private_Mark');
    var hdn_Private_Mark =  document.getElementById('WucGCNew1_hdn_Private_Mark');
    var hdn_End_No =  document.getElementById('WucGCNew1_hdn_End_No');
    var hdn_Start_No =  document.getElementById('WucGCNew1_hdn_Start_No');
    var hdn_GC_No =  document.getElementById('WucGCNew1_hdn_GC_No');
    var hdn_DocumentSeriesAllocationID =  document.getElementById('WucGCNew1_hdn_DocumentSeriesAllocationID');
    SetErrorMsg('');

    var Result = Results.value.Rows[0];

    if(Result['Allocation_ID'] > 0)
    {
        hdn_Start_No.value = Result['Start_No'];
        hdn_End_No.value = Result['End_No'];
        hdn_DocumentSeriesAllocationID.value = Result['Allocation_ID'];
        Txt_GC_No.value = Result['Next_No'];
        Append_Zero_In_GC_No(Txt_GC_No);

        txt_Private_Mark.value = Txt_GC_No.value;
        hdn_Private_Mark.value = Txt_GC_No.value;
        hdn_GC_No.value = Txt_GC_No.value;

        Append_Zero_In_GC_No(hdn_Start_No);
        Append_Zero_In_GC_No(hdn_End_No);
    }
    else
    {
        SetErrorMsg('Please Allocate Document Series.');
    }
 } 
////*******************************************************************
function On_Attached_GC_No_Change()
{
    var txt_Attached_GC_No = document.getElementById('WucGCNew1_txt_Attached_GC_No');
    var btn_GetAttachedGCDetails = document.getElementById('WucGCNew1_btn_GetAttachedGCDetails');
    var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');
    var hdn_AttachedGCId = document.getElementById('WucGCNew1_hdn_AttachedGCId');
    var MenuItemId =  document.getElementById('WucGCNew1_hdn_MenuItemId').value;
//    var IsGetCopyDetails =  document.getElementById('WucGCNew1_hdn_IsGetCopyDetails').value;

    SetErrorMsg('');

    if(val(txt_Attached_GC_No.value) > 0)
       Raj.EC.OperationModel.NewGCSearch.Check_Valid_Attach_GC_No(val(txt_Attached_GC_No.value),val(MenuItemId),handle_AttachedValidGC);
    else
    {
        hdn_AttachedGCId.value = '0';
        btn_GetAttachedGCDetails.disabled = true;
        chk_IsAttached.disabled = true;
    }
 } 

function handle_AttachedValidGC(Results)
{
    var txt_Attached_GC_No = document.getElementById('WucGCNew1_txt_Attached_GC_No');
    var hdn_AttachedGCId = document.getElementById('WucGCNew1_hdn_AttachedGCId');
    var btn_GetAttachedGCDetails = document.getElementById('WucGCNew1_btn_GetAttachedGCDetails');
    var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');
    var hdn_CanAttached = document.getElementById('WucGCNew1_hdn_CanAttached');

    SetErrorMsg('');
    
    var Result = Results.value.Rows[0];

    if(Result['GC_ID'] > 0)
    {
        hdn_AttachedGCId.value = Result['GC_ID'];
        btn_GetAttachedGCDetails.disabled = false;
        Append_Zero_In_GC_No(txt_Attached_GC_No);
    }
    else
    {
        hdn_AttachedGCId.value = '0';
        btn_GetAttachedGCDetails.disabled = true;
        SetErrorMsg('Invalid GC No.');
        txt_Attached_GC_No.focus();
    }
        chk_IsAttached.disabled = !Result['Allow_To_Attach'];
        hdn_CanAttached.value = Result['Allow_To_Attach'];
 } 
  
////*******************************************************************

function On_IsAttached_Click()
{
    var hdn_IsAttached = document.getElementById('WucGCNew1_hdn_IsAttached');        
    var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');

    hdn_IsAttached.value = "0";

    if(chk_IsAttached.checked == true)
    {
        hdn_IsAttached.value = "1";
        Get_Attached_GC_Details();
    }

    Enable_Disable_On_IsAttached();
}

function Enable_Disable_On_IsAttached()
{
    var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');
    var hdn_IsAttached = document.getElementById('WucGCNew1_hdn_IsAttached');        
    var ddl_Booking_Type = document.getElementById('WucGCNew1_ddl_Booking_Type'); 
    var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');         
    var ddl_FreightBasis = document.getElementById('WucGCNew1_ddl_FreightBasis');
    var txt_ChargeWeight = document.getElementById('WucGCNew1_txt_ChargeWeight'); 
    var txt_Attached_GC_No = document.getElementById('WucGCNew1_txt_Attached_GC_No');
    
    var lnk_FromLocation = document.getElementById('WucGCNew1_lnk_FromServiceLocation'); 
    var lnk_ToLocation = document.getElementById('WucGCNew1_lnk_AddToServiceLocation');         
    var lnk_NewConsignor = document.getElementById('WucGCNew1_lnk_NewConsignor'); 
    var lnk_EditConsignor = document.getElementById('WucGCNew1_lnk_EditConsignor');              
    var lnk_NewConsignee = document.getElementById('WucGCNew1_lnk_NewConsignee'); 
    var lnk_EditConsignee = document.getElementById('WucGCNew1_lnk_EditConsignee');  
    var lnk_DD_Address = document.getElementById('WucGCNew1_lnk_Change_Door_Delivery_Address'); 
    var btn_GetAttachedGCDetails = document.getElementById('WucGCNew1_btn_GetAttachedGCDetails'); 

    var txt_ContractClient = document.getElementById('WucGCNew1_txt_ContractClient');              
    var ddl_ContractBranch = document.getElementById('WucGCNew1_ddl_ContractBranch'); 
    var ddl_Contract = document.getElementById('WucGCNew1_ddl_Contract');  
    var txt_BillingParty = document.getElementById('WucGCNew1_txt_BillingParty'); 
    var ddl_BillingHierarchy = document.getElementById('WucGCNew1_ddl_BillingHierarchy'); 
    var txt_BillingLocation = document.getElementById('WucGCNew1_txt_BillingLocation');  
    var txt_BillingRemark = document.getElementById('WucGCNew1_txt_BillingRemark'); 

    On_PaymentType_Change();

    ddl_Booking_Type.disabled = chk_IsAttached.checked;
    ddl_PaymentType.disabled = chk_IsAttached.checked;
    ddl_FreightBasis.disabled = chk_IsAttached.checked;
    txt_ChargeWeight.disabled = chk_IsAttached.checked;
    txt_Attached_GC_No.disabled = chk_IsAttached.checked;
    btn_GetAttachedGCDetails.disabled = chk_IsAttached.checked;

    txt_ContractClient.disabled = chk_IsAttached.checked;
    ddl_ContractBranch.disabled = chk_IsAttached.checked;
    ddl_Contract.disabled = chk_IsAttached.checked;
    txt_BillingParty.disabled = chk_IsAttached.checked;
    ddl_BillingHierarchy.disabled = chk_IsAttached.checked;
    txt_BillingLocation.disabled = chk_IsAttached.checked;
    txt_BillingRemark.disabled = chk_IsAttached.checked;

    if(chk_IsAttached.checked == true || hdn_IsAttached.value == "1")
    {
        lnk_FromLocation.style.display = 'none'; 
        lnk_ToLocation.style.display = 'none'; 
        lnk_NewConsignor.style.visibility = 'hidden';
        lnk_NewConsignee.style.visibility = 'hidden';
        lnk_EditConsignor.style.visibility = 'hidden';
        lnk_EditConsignee.style.visibility = 'hidden'; 
    }
    else
    {
        lnk_FromLocation.style.display = 'inline';
        lnk_ToLocation.style.display = 'inline';
        lnk_DD_Address.style.visibility = 'visible'; 
        lnk_NewConsignor.style.visibility = 'visible';
        lnk_NewConsignee.style.visibility = 'visible';
        lnk_EditConsignor.style.visibility = 'visible';
        lnk_EditConsignee.style.visibility = 'visible';
    }
}

function Call_IsAttachedOnPageLoad()
{
    var hdn_IsAttached = document.getElementById('WucGCNew1_hdn_IsAttached');
    var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');
    var btn_GetAttachedGCDetails = document.getElementById('WucGCNew1_btn_GetAttachedGCDetails');
    var txt_Attached_GC_No = document.getElementById('WucGCNew1_txt_Attached_GC_No');
    var hdn_CanAttached = document.getElementById('WucGCNew1_hdn_CanAttached');
    var GCId = document.getElementById('WucGCNew1_hdn_GCId').value;

    if(val(txt_Attached_GC_No.value) <= 0)
    {
        btn_GetAttachedGCDetails.disabled = true;
        chk_IsAttached.disabled = true;
    }
    
    if(hdn_IsAttached.value == "1")
    {
        chk_IsAttached.checked = true;
        Enable_Disable_On_IsAttached();
    }
    if(hdn_CanAttached.value == "false" && val(GCId) <= 0)
    {
        chk_IsAttached.disabled = true;
    }
}

 ////************************** Call For GC Rectification ***********************************

function Call_For_GC_Rectification()
{
    var txt_From_Location = document.getElementById('WucGCNew1_txt_From_Location');
    var txt_To_Location = document.getElementById('WucGCNew1_txt_To_Location');
    var txt_ConsignorName = document.getElementById('WucGCNew1_txt_ConsignorName');
    var txt_ConsigneeName = document.getElementById('WucGCNew1_txt_ConsigneeName');
    var ddl_PaymentType = document.getElementById('WucGCNew1_ddl_PaymentType');
 
    var lnk_FromServiceLocation = document.getElementById('WucGCNew1_lnk_FromServiceLocation');
    var lnk_AddToServiceLocation = document.getElementById('WucGCNew1_lnk_AddToServiceLocation');

    var lnk_NewConsignor = document.getElementById('WucGCNew1_lnk_NewConsignor');
    var lnk_EditConsignor = document.getElementById('WucGCNew1_lnk_EditConsignor');
    var lnk_NewConsignee = document.getElementById('WucGCNew1_lnk_NewConsignee');
    var lnk_EditConsignee = document.getElementById('WucGCNew1_lnk_EditConsignee');

    var btn_Save_New = document.getElementById('WucGCNew1_btn_Save_New');
    var btn_Save_Print = document.getElementById('WucGCNew1_btn_Save_Print');
    var btn_Save_Repeat = document.getElementById('WucGCNew1_btn_Save_Repeat');

    txt_From_Location.disabled = true;
    txt_To_Location.disabled = true;
    if(val(ddl_PaymentType.value) == "1" || val(ddl_PaymentType.value) == "3" || val(ddl_PaymentType.value) == "5")
    {
        txt_ConsignorName.disabled = false;
        txt_ConsigneeName.disabled = false;
        lnk_EditConsignor.style.display = 'inline';
        lnk_EditConsignee.style.display = 'inline';
    }
    else
    {
        txt_ConsignorName.disabled = true;
        txt_ConsigneeName.disabled = true;
        lnk_EditConsignor.style.display = 'none';
        lnk_EditConsignee.style.display = 'none';
    }
    btn_Save_New.style.display = 'none';
    btn_Save_Print.style.display = 'none';
    btn_Save_Repeat.style.display = 'none';
    
    lnk_FromServiceLocation.style.display = 'none';
    lnk_AddToServiceLocation.style.display = 'none';
    lnk_NewConsignor.style.display = 'none';
    lnk_NewConsignee.style.display = 'none';
}

///------------------------------ End GC Validation ------------------------------//

///------------------------------ Save  ------------------------------//

function Allow_To_Save()
{
 
    var ATS = false;
    var MenuItemId =  document.getElementById('WucGCNew1_hdn_MenuItemId').value;
    var GC_Start_No =  document.getElementById('WucGCNew1_hdn_Start_No').value;
    var GC_End_No =  document.getElementById('WucGCNew1_hdn_End_No').value;
    var Txt_GC_No =  document.getElementById('WucGCNew1_txt_GC_No');
    var GC_No_Length =  document.getElementById('WucGCNew1_hdn_GC_No_Length').value;
    var ClientCode = document.getElementById('WucGCNew1_hdn_ClientCode').value;
    var GCCaption = document.getElementById('WucGCNew1_hdn_GCCaption').value;
    var txt_Attached_GC_No = document.getElementById('WucGCNew1_txt_Attached_GC_No');
    var hdn_AttachedGCId = document.getElementById('WucGCNew1_hdn_AttachedGCId');

    var txt_Agency_GCNo =  document.getElementById('WucGCNew1_txt_Agency_GCNo');
    var hdn_AgencyId =  document.getElementById('WucGCNew1_hdn_AgencyId');
    var hdn_LedgerId =  document.getElementById('WucGCNew1_hdn_LedgerId');
    var txt_Agency =  document.getElementById('WucGCNew1_txt_Agency');
    var txt_Ledger =  document.getElementById('WucGCNew1_txt_Ledger');
    var txt_Private_Mark = document.getElementById('WucGCNew1_txt_Private_Mark');
    var ddl_Booking_Type =  document.getElementById('WucGCNew1_ddl_Booking_Type');
    var ddl_Consignment_Type =  document.getElementById('WucGCNew1_ddl_Consignment_Type');
    var ddl_booking_Sub_Type =  document.getElementById('WucGCNew1_ddl_booking_Sub_Type');
    var ddl_Dly_Type =  document.getElementById('WucGCNew1_ddl_Dly_Type');
    var ddl_Delivery_Against =  document.getElementById('WucGCNew1_ddl_Delivery_Against');
    var hdn_BookingBranchId =  document.getElementById('WucGCNew1_hdn_BookingBranchId');
    var txt_Booking_Branch =  document.getElementById('WucGCNew1_txt_Booking_Branch');
    var hdn_FromLocationId =  document.getElementById('WucGCNew1_hdn_FromLocationId');
    var hdn_ToLocationId =  document.getElementById('WucGCNew1_hdn_ToLocationId');
    var txt_From_Location =  document.getElementById('WucGCNew1_txt_From_Location');
    var txt_To_Location =  document.getElementById('WucGCNew1_txt_To_Location');
    var ddl_Road_Permit =  document.getElementById('WucGCNew1_ddl_Road_Permit');
    var txt_Road_Permit_SrNo =  document.getElementById('WucGCNew1_txt_Road_Permit_SrNo');
    var ddl_VehicleType =  document.getElementById('WucGCNew1_ddl_VehicleType');
    var ddl_Pickup_Type =  document.getElementById('WucGCNew1_ddl_Pickup_Type');
    var hdn_ConsignorId =  document.getElementById('WucGCNew1_hdn_ConsignorId');
    var hdn_ConsigneeId =  document.getElementById('WucGCNew1_hdn_ConsigneeId');
    var txt_ConsignorName =  document.getElementById('WucGCNew1_txt_ConsignorName');
    var txt_ConsigneeName =  document.getElementById('WucGCNew1_txt_ConsigneeName');
    var ddl_PaymentType =  document.getElementById('WucGCNew1_ddl_PaymentType');
    var ddl_GCRisk =  document.getElementById('WucGCNew1_ddl_GCRisk');
    var hdn_Is_InsuranceDetails_Filled = document.getElementById('WucGCNew1_hdn_Is_InsuranceDetails_Filled');
    var hdn_Is_ContainerDetails_Filled = document.getElementById('WucGCNew1_hdn_Is_ContainerDetails_Filled');

    var chk_IsInsured =  document.getElementById('WucGCNew1_chk_IsInsured');
    var hdn_TotalArticles =  document.getElementById('WucGCNew1_hdn_TotalArticles');
    var hdn_TotalWeight =  document.getElementById('WucGCNew1_hdn_TotalWeight');
    var hdn_TotalCFT =  document.getElementById('WucGCNew1_hdn_TotalCFT');
    var hdn_TotalCBM =  document.getElementById('WucGCNew1_hdn_TotalCBM');
    var ddl_FreightBasis =  document.getElementById('WucGCNew1_ddl_FreightBasis');
    var ddl_VolumetricFreightUnit =  document.getElementById('WucGCNew1_ddl_VolumetricFreightUnit');
    var txt_MarketingExecutive =  document.getElementById('WucGCNew1_ddl_MarketingExecutive_txtBoxddl_MarketingExecutive');
    var hdn_MarketingExecutive =  document.getElementById('WucGCNew1_ddl_MarketingExecutive_hdnddl_MarketingExecutive');
    var txt_LoadingSuperVisor =  document.getElementById('WucGCNew1_ddl_LoadingSuperVisor_txtBoxddl_LoadingSuperVisor');
    var hdn_LoadingSuperVisor =  document.getElementById('WucGCNew1_ddl_LoadingSuperVisor_hdnddl_LoadingSuperVisor');
    var hdn_LS_ReqFor_BkgType =  document.getElementById('WucGCNew1_hdn_LoadingSuperVisor_RequiredFor_BookingType');

    var td_ddl_MarketingExecutive =  document.getElementById('WucGCNew1_td_ddl_MarketingExecutive');
    var ddl_Contract =  document.getElementById('WucGCNew1_ddl_Contract');
    var ContractId = document.getElementById('WucGCNew1_hdn_ContractId').value;
    var IsContractApplied = document.getElementById('WucGCNew1_hdn_IsContractApplied').value;
    var chk_Is_Contract_Required_For_TBB_GC = document.getElementById('WucGCNew1_chk_Is_Contract_Required_For_TBB_GC');

    var chk_IsMultipleBilling =  document.getElementById('WucGCNew1_chk_IsMultipleBilling');
    var txt_BillingParty =  document.getElementById('WucGCNew1_txt_BillingParty');
    var hdn_BillingPartyId =  document.getElementById('WucGCNew1_hdn_BillingPartyId');
    var ddl_BillingHierarchy =  document.getElementById('WucGCNew1_ddl_BillingHierarchy');
    var txt_BillingLocation =  document.getElementById('WucGCNew1_txt_BillingLocation');
    var hdn_BillingLocationId =  document.getElementById('WucGCNew1_hdn_BillingLocationId');
    var chk_IsToPayBkgApplicable =  document.getElementById('WucGCNew1_chk_IsToPayBkgApplicable');
    var txt_Freight =  document.getElementById('WucGCNew1_txt_Freight');
    var TotalGCAmount = document.getElementById('WucGCNew1_hdn_TotalGCAmount').value;
    var SubTotal = document.getElementById('WucGCNew1_hdn_SubTotal').value;
  
    var txt_Advance =  document.getElementById('WucGCNew1_txt_Advance');
    var Advance =  document.getElementById('WucGCNew1_hdn_Advance').value;
    var txt_CashAmount =  document.getElementById('WucGCNew1_txt_CashAmount');
    var CashAmount =  document.getElementById('WucGCNew1_hdn_CashAmount').value;
    var txt_ChequeAmount =  document.getElementById('WucGCNew1_txt_ChequeAmount');
    var ChequeAmount =  document.getElementById('WucGCNew1_hdn_ChequeAmount').value;
    var txt_ChequeNo =  document.getElementById('WucGCNew1_txt_ChequeNo');
    var txt_BankName = document.getElementById('WucGCNew1_txt_BankName');
    var chk_IsAttached = document.getElementById('WucGCNew1_chk_IsAttached');
    var GCId = document.getElementById('WucGCNew1_hdn_GCId').value;

    var txt_ContractClient =  document.getElementById('WucGCNew1_txt_ContractClient');
    var hdn_ContractualClientId =  document.getElementById('WucGCNew1_hdn_ContractualClientId');
    var hdn_Is_Paid_Allowed = document.getElementById('WucGCNew1_hdn_Is_Paid_Allowed');
    var hdn_Is_To_Pay_Allowed = document.getElementById('WucGCNew1_hdn_Is_To_Pay_Allowed');
    var hdn_Is_FOC_Allowed = document.getElementById('WucGCNew1_hdn_Is_FOC_Allowed');
    var hdn_Is_To_Be_Billed_Allowed = document.getElementById('WucGCNew1_hdn_Is_To_Be_Billed_Allowed');
    
    var hdn_Consignee_Is_ToPay_Allowed = document.getElementById('WucGCNew1_hdn_Consignee_Is_ToPay_Allowed');
    
    var MinimumBalance =  document.getElementById('WucGCNew1_hdn_Billing_Party_MinimumBalance').value;
    var Ledger_Closing_Balance =  document.getElementById('WucGCNew1_hdn_Billing_Party_Ledger_Closing_Balance').value;
    var hdn_Billing_Party_CreditLimit = document.getElementById('WucGCNew1_hdn_Billing_Party_CreditLimit'); 

    var txt_eWayBillNo =  document.getElementById('WucGCNew1_txt_eWayBillNo');

    var hdn_TotalInvoiceAmount = document.getElementById('WucGCNew1_hdn_TotalInvoiceAmount'); 
    var hdn_ConsignorStateId = document.getElementById('WucGCNew1_hdn_ConsignorStateId'); 
    var hdn_ConsigneeStateId = document.getElementById('WucGCNew1_hdn_ConsigneeStateId');

    var hdn_Is_ServiceTax_ForCommodity = document.getElementById('WucGCNew1_hdn_Is_Service_Tax_Applicable_For_Commodity');

    var hdn_IsServiceTaxApplicableForConsignor = document.getElementById('WucGCNew1_hdn_IsServiceTaxApplicableForConsignor');
    var hdn_IsServiceTaxApplicableForConsignee = document.getElementById('WucGCNew1_hdn_IsServiceTaxApplicableForConsignee');
    
    var hdn_LoginUserHierarchy = document.getElementById('WucGCNew1_hdn_LoginUserHierarchy');

    var ddl_Reason_Freight_Pending =  document.getElementById('WucGCNew1_ddl_Reason_Freight_Pending');
    var txt_Paid_Freight_Pending_Person_Name =  document.getElementById('WucGCNew1_txt_Paid_Freight_Pending_Person_Name');
    var txt_Paid_Freight_Pending_Person_Mobile =  document.getElementById('WucGCNew1_txt_Paid_Freight_Pending_Person_Mobile');

    var Validate_LS_ReqFor_BkgTypeId = 0;
    var _IdArray = hdn_LS_ReqFor_BkgType.value.split(',');
    var i = 0;
   
    if (_IdArray.length > 0)
    {           
        for (i = 0; i <= _IdArray.length - 1; i++)
        {
            if (parseInt(_IdArray[i]) == parseInt(ddl_Booking_Type.value))
            {
                Validate_LS_ReqFor_BkgTypeId = parseInt(ddl_Booking_Type.value);
                break;
            }
        }
    }
    else
    {
        Validate_LS_ReqFor_BkgTypeId = 0;
        _IdArray[0] = "all";
    }

    var hdn_year = parseFloat(document.getElementById('WucGCNew1_hdn_year').value);
    var hdn_month = parseFloat(document.getElementById('WucGCNew1_hdn_month').value) - 1;
    var hdn_date = parseFloat(document.getElementById('WucGCNew1_hdn_date').value);

    var booking_date;var todays_date;

    booking_date = WucGCNew1_wuc_BookingDate.GetSelectedDate();
    booking_date = new Date(booking_date.getFullYear(), booking_date.getMonth(),booking_date.getDate())
    todays_date = new Date(hdn_year,hdn_month,hdn_date);
    todays_date = new Date(todays_date.getFullYear(), todays_date.getMonth(),todays_date.getDate());
    
    if (Txt_GC_No.value == '')
    {
        SetErrorMsg("Please Enter "+ GCCaption +" No.");
//        Txt_GC_No.focus();
    }
//    else if (val(Txt_GC_No.value.length) < val(GC_No_Length))
//    {
//        SetErrorMsg(" "+ GCCaption +" Number should have " + GC_No_Length + " Digits Only.");
////        Txt_GC_No.focus();
//    }
    else if (GCId <= 0 && MenuItemId != 200 && (Txt_GC_No.value < GC_Start_No || Txt_GC_No.value > GC_End_No))
    {
        SetErrorMsg(" "+ GCCaption +" No.should be between (" + GC_Start_No + ") and (" + GC_End_No + ")");
//        Txt_GC_No.focus();
    }
    else if (MenuItemId == 200 && val(Txt_GC_No.value) <= 0)
    {
        SetErrorMsg("Please Enter "+ GCCaption +" No.");
//        Txt_GC_No.focus();
    }
    else if (booking_date > todays_date)
    {
        SetErrorMsg("Booking Date Shoud Be Less Than Or Equal To Todays Date.");
    }
    else if (MenuItemId != 194 && val(ddl_Booking_Type.value) == 1 && booking_date < todays_date)
    {
        SetErrorMsg("Booking Date Should Be Equal To Todays Date.");
    }
    else if (MenuItemId == 229 && txt_Agency_GCNo.value == '')
    {
        SetErrorMsg("Please Enter Agency "+ GCCaption +" No.");
        txt_Agency_GCNo.focus();
    }
    else if (MenuItemId == 229 && val(hdn_AgencyId.value) <= 0)
    {
        SetErrorMsg("Please Select Agency");
        txt_Agency.focus();
    }
    else if (MenuItemId == 229 && val(hdn_AgencyId.value) > 0 && val(hdn_LedgerId.value) <= 0)
    {
        SetErrorMsg("Please Select Agency Ledger ");
        txt_Ledger.focus();
    }    
    else if (ClientCode != 'nandwana' && txt_Private_Mark.value == '' && control_is_mandatory(txt_Private_Mark) == true)
    {
        SetErrorMsg("Please Enter Private Mark.");
        txt_Private_Mark.focus();
    }
    else if (ClientCode != 'nandwana' && MenuItemId != 200 && chk_IsAttached.checked == true && val(hdn_AttachedGCId.value) <= 0)
    {
        SetErrorMsg("Please Enter "+ GCCaption +" No. For Attached");
        txt_Attached_GC_No.focus();
    }
    else if (val(ddl_Consignment_Type.value) <= 0 && control_is_mandatory(ddl_Consignment_Type) == true)
    {
        SetErrorMsg("Please Select Consignment Type.");
        ddl_Consignment_Type.focus();
    }
    else if (val(ddl_Booking_Type.value) <= 0)
    {
        SetErrorMsg("Please Select Booking Type.");
        ddl_Booking_Type.focus();
    }
    else if (ClientCode == 'nandwana' && val(ddl_Booking_Type.value) == 5 && val(hdn_Is_ContainerDetails_Filled.value) == 0)
    {
        SetErrorMsg("Please Mention Container Details.");
    }
    else if (ClientCode != 'nandwana' && val(ddl_booking_Sub_Type.value) <= 0 && control_is_mandatory(ddl_booking_Sub_Type) == true)
    {
        SetErrorMsg("Please Select Booking Sub Type.");
        ddl_booking_Sub_Type.focus();
    }
    else if(val(ddl_Dly_Type.value) <= 0)
    {
        SetErrorMsg("Please Select Delivery Type.");
        ddl_Dly_Type.focus();
    }
    else if(ClientCode != 'nandwana' && val(ddl_Delivery_Against.value) <= 0)
    {
        SetErrorMsg("Please Select Delivery Against.");
        ddl_Delivery_Against.focus();
    }
    else if (MenuItemId == 200 && val(hdn_BookingBranchId.value) == 0)
    {
        SetErrorMsg("Please Select Booking Branch.");
        txt_Booking_Branch.focus();
    }
    else if(val(hdn_FromLocationId.value) <= 0)
    {
        SetErrorMsg("Please Select From Location.");
        txt_From_Location.focus();
    }
    else if(val(hdn_ToLocationId.value) <= 0)
    {
        SetErrorMsg("Please Select To Location.");
        txt_To_Location.focus();
    }
    else if(ClientCode != 'nandwana' && val(ddl_Road_Permit.value) <= 0 && control_is_mandatory(ddl_Road_Permit) == true)
    {
        SetErrorMsg("Please Select Road Permit Type.");
        ddl_Road_Permit.focus();
    }
    else if(ClientCode != 'nandwana' && val(ddl_Road_Permit.value) == 2 && txt_Road_Permit_SrNo.value == '' && control_is_mandatory(txt_Road_Permit_SrNo) == true)
    {
        SetErrorMsg("Please Enter Road Permit Sr. No.");
        txt_Road_Permit_SrNo.focus();
    }
    else if(ClientCode != 'nandwana' && val(ddl_VehicleType.value) <= 0)
    {
        SetErrorMsg("Please Select Vehicle Type.");
        ddl_VehicleType.focus();
    }
    else if(val(ddl_Pickup_Type.value) <= 0)
    {
        SetErrorMsg("Please Select Pickup Type.");
        ddl_Pickup_Type.focus();
    }
    else if (val(hdn_ConsignorId.value) <= 0)
    {
        SetErrorMsg("Please Select Consignor.");
        txt_ConsignorName.focus();
    }
    else if (val(hdn_ConsigneeId.value) <= 0)
    {
        SetErrorMsg("Please Select Consignee.");
        txt_ConsigneeName.focus();
    }
    else if (val(ddl_PaymentType.value) <= 0)
    {
        SetErrorMsg("Please Select Payment Type.");
        ddl_PaymentType.focus();
    }
    else if (val(ddl_PaymentType.value) == 1 && chk_IsToPayBkgApplicable.checked == false)
    {
        SetErrorMsg("To Pay Booking is not allowed for selected To Location");
        ddl_PaymentType.focus();
    }
    else if (val(ddl_PaymentType.value) == 3 && val(hdn_BillingPartyId.value) <= 0 && chk_IsMultipleBilling.checked == false)
    {
        SetErrorMsg("Please Select Billing Party.");
        txt_BillingParty.focus();
    }
    else if(val(ddl_PaymentType.value) == 3 && val(hdn_BillingPartyId.value) > 0 && chk_IsMultipleBilling.checked == false && ddl_BillingHierarchy.value == "0")
    {
        SetErrorMsg("Please Select Billing Hierarchy");
        ddl_BillingHierarchy.focus();
    }
    else if (ClientCode != 'nandwana' && val(ddl_PaymentType.value) == 3 && val(hdn_BillingPartyId.value) > 0 && chk_IsMultipleBilling.checked == false && ddl_BillingHierarchy.value != "0" && ddl_BillingHierarchy.value != "HO" && val(hdn_BillingLocationId.value) <= 0)
    {
        if (ddl_BillingHierarchy.value == "BO")
        {
            SetErrorMsg("Please Select Billing Branch");
            txt_BillingLocation.focus();
        }
        else if (ddl_BillingHierarchy.value == "AO")
        {
            SetErrorMsg("Please Select Billing Area");
            txt_BillingLocation.focus();
        }
        else if (ddl_BillingHierarchy.value == "RO")
        {
            SetErrorMsg("Please Select Billing Region");
            txt_BillingLocation.focus();
        }
    }
    else if (ClientCode == 'nandwana' && val(ddl_PaymentType.value) == 3 && val(hdn_BillingPartyId.value) > 0 && val(hdn_BillingLocationId.value) <= 0)
    {
        if (ddl_BillingHierarchy.value == "BO")
        {
            SetErrorMsg("Please Select Billing Branch");
            txt_BillingLocation.focus();
        }
        else if (ddl_BillingHierarchy.value == "AO")
        {
            SetErrorMsg("Please Select Billing Area");
            txt_BillingLocation.focus();
        }
        else if (ddl_BillingHierarchy.value == "RO")
        {
            SetErrorMsg("Please Select Billing Region");
            txt_BillingLocation.focus();
        }
    }
    else if (val(ddl_GCRisk.value) <= 0)
    {
        SetErrorMsg("Please Select "+ GCCaption +" Risk.");
        ddl_GCRisk.focus();
    }
//    else if (val(ddl_GCRisk.value) == 2 && chk_IsInsured.checked == false) // for career risk
//    {
//        SetErrorMsg("Please Mention Insurance details.");
//    }  
//    else if (val(ddl_GCRisk.value) == 2 && chk_IsInsured.checked == true && val(hdn_Is_InsuranceDetails_Filled.value) == 0) // for career risk
//    {
//        SetErrorMsg("Please Mention Valid Insurance details.");
//    }   
    else if (val(hdn_TotalArticles.value) <= 0)
    {
        SetErrorMsg("Please Mention Commodity Details.");
    }
    else if (val(ddl_PaymentType.value) == 3 && val(hdn_Billing_Party_CreditLimit.value) > 0 && (val(Ledger_Closing_Balance) + val(hdn_Billing_Party_CreditLimit.value)) <= 0) 
    {
        SetErrorMsg("Aapka Credit Limmit Khatam Huva. Om Turanth Me Payment Bhejo.");
    }
    else if (val(ddl_PaymentType.value) == 3 && val(MinimumBalance) > 0 && val(Ledger_Closing_Balance) < val(MinimumBalance)) 
    {
        SetErrorMsg("Aapka Balance Khatam Huva. Om Turanth Me Payment Bhejo.");
    }
    else if (val(hdn_TotalWeight.value) <= 0)
    {
        SetErrorMsg("Total Actual Weight Should be Greater than Zero.");
    }
    else if (val(ddl_FreightBasis.value) == 4 && val(ddl_VolumetricFreightUnit.value) == 1 && val(hdn_TotalCFT.value) <= 0)
    {
        SetErrorMsg("Please Enter Valid Length, Width and Height.");
    }
    else if (val(ddl_FreightBasis.value) == 4 && val(ddl_VolumetricFreightUnit.value) == 2 && val(hdn_TotalCBM.value) <= 0)
    {
        SetErrorMsg("Please Enter Valid Length, Width and Height.");
    }
    else if ((val(ddl_PaymentType.value) != 5 && chk_IsAttached.checked == false) && val(txt_Freight.value) <= 0)
    {
        SetErrorMsg("Freight Amount Should Be Greater Than Zero.");
        txt_Freight.focus();
    }
    else if ((val(ddl_PaymentType.value) != 5 && chk_IsAttached.checked == false) && val(TotalGCAmount) <= 0)
    {
        SetErrorMsg("Total "+ GCCaption +" Amount Should Be Greater Than Zero.");
    }
    else if (val(ChequeAmount) > 0 && val(txt_ChequeNo.value) <= 0)
    {
        SetErrorMsg("Please Enter Cheque No.");
        txt_ChequeNo.focus();
    }
    else if (val(ChequeAmount) > 0 && (txt_ChequeNo.value == '' || txt_ChequeNo.value.length < 5))
    {
        SetErrorMsg("Cheque No Should have Atleast 5 Digits.");
        txt_ChequeNo.focus();
    }
    else if (val(ChequeAmount) > 0 && txt_BankName.value == '')
    {
        SetErrorMsg("Please Enter Bank Name.");
        txt_BankName.focus();
    }
    else if (val(ddl_PaymentType.value) == 2 && (val(CashAmount) + val(ChequeAmount)) != val(TotalGCAmount)) // paid
    {
        SetErrorMsg("Cash + Cheque Amount Should Be Equal To "+ GCCaption +" Amount.");
        txt_CashAmount.focus();
    }
    else if (val(ddl_PaymentType.value) == 1 && (val(CashAmount) + val(ChequeAmount)) != val(Advance))
    {
        SetErrorMsg("Cash + Cheque Amount Should Be Equal To Advance Amount.");
        txt_CashAmount.focus();
    }
    
    else if (val(ddl_PaymentType.value) == 2 && (val(ChequeAmount) > 0 && val(ChequeAmount) < 500) ) 
    {
        SetErrorMsg("Cheque Can Not Be Accepted For Freight Less Than Rs. 500.");
        txt_CashAmount.focus();
    }
    
    else if (val(ddl_PaymentType.value) == 1 && val(Advance) > val(SubTotal))
    {
        SetErrorMsg("Advance Amount Should Not Be Greater Than Sub Total.");
        txt_Advance.focus();
    }
    else if (val(hdn_LoadingSuperVisor.value) <= 0 && _IdArray[0] == "all")
    {
        SetErrorMsg("Please Select Loading Supervisor.");
        txt_LoadingSuperVisor.focus();
    }
    else if (val(hdn_LoadingSuperVisor.value) <= 0 && _IdArray[0] != "all" && val(ddl_Booking_Type.value) == Validate_LS_ReqFor_BkgTypeId)
    {
        SetErrorMsg("Please Select Loading Supervisor.");
        txt_LoadingSuperVisor.focus();
    }
//    else if (val(hdn_MarketingExecutive.value) <= 0 && control_is_mandatory(td_ddl_MarketingExecutive) == true)
//    {
//        SetErrorMsg("Please Select Marketing Executive.");
//        txt_MarketingExecutive.focus();
//    }
    else if (MenuItemId != 200 && val(ContractId) > 0 && val(IsContractApplied) == 0 && chk_IsAttached.checked == false)
    {
        SetErrorMsg("Contractual Rates are not Applicable.");
        ddl_Contract.focus();
    }
    else if (MenuItemId != 200 && val(ContractId) <= 0 && chk_Is_Contract_Required_For_TBB_GC.checked == true && val(ddl_PaymentType.value) == 3)
    {
        SetErrorMsg("Contract Applied In Case Of TBB "+ GCCaption +" .");
        ddl_Contract.focus();
    }
    else if (val(hdn_ContractualClientId.value) > 0 && val(ddl_PaymentType.value) == 1 && hdn_Is_To_Pay_Allowed.value != 'true')
    {
        SetErrorMsg("To Pay Booking Is Not Allow For Selected Contractual Client");
        txt_ContractClient.focus();
    }
    else if (val(hdn_ContractualClientId.value) > 0 && (val(ddl_PaymentType.value) == 2 || val(ddl_PaymentType.value) == 4) && hdn_Is_Paid_Allowed.value != 'true')
    {
        SetErrorMsg("Paid Booking Is Not Allow For Selected Contractual Client");
        txt_ContractClient.focus();
    }
    else if (val(hdn_ContractualClientId.value) > 0 && val(ddl_PaymentType.value) == 3 && hdn_Is_To_Be_Billed_Allowed.value != 'true')
    {
        SetErrorMsg("To Be Billed Booking Is Not Allow For Selected Contractual Client");
        txt_ContractClient.focus();
    }
    else if (val(hdn_ContractualClientId.value) > 0 && val(ddl_PaymentType.value) == 5 && hdn_Is_FOC_Allowed.value != 'true')
    {
        SetErrorMsg("FOC Booking Is Not Allow For Selected Contractual Client");
        txt_ContractClient.focus();
    }
    else if (val(ddl_PaymentType.value) == 1 && hdn_Consignee_Is_ToPay_Allowed.value == 'false')
    {
        SetErrorMsg("To Pay Is Not Allow For Selected Consignee");
        ddl_PaymentType.focus();
    }
    else if (txt_eWayBillNo.value.length > 0 && txt_eWayBillNo.value.length < 12) 
    {
        SetErrorMsg("Invalid e-Way Bill No.");
        txt_eWayBillNo.focus();
    }
    else if (txt_eWayBillNo.value == "000000000000" || txt_eWayBillNo.value == "111111111111" || txt_eWayBillNo.value == "222222222222" || txt_eWayBillNo.value == "333333333333" || txt_eWayBillNo.value == "444444444444" || txt_eWayBillNo.value == "555555555555" || txt_eWayBillNo.value == "666666666666" || txt_eWayBillNo.value == "777777777777" || txt_eWayBillNo.value == "888888888888" || txt_eWayBillNo.value == "999999999999") {
        SetErrorMsg("Invalid e-Way Bill No.");
        txt_eWayBillNo.focus();
    }
    else if ((hdn_IsServiceTaxApplicableForConsignor.value == '1' || hdn_IsServiceTaxApplicableForConsignee.value == '1') && (val(hdn_TotalInvoiceAmount.value) >= 100000 && val(hdn_ConsignorStateId.value) == 1 && val(hdn_ConsigneeStateId.value) == 1 && txt_eWayBillNo.value.length < 12 && hdn_Is_ServiceTax_ForCommodity.value == '1')) 
    {
        SetErrorMsg("Enter Proper e-Way Bill No.  As Per Maharashtra Intra-State Rule");
        txt_eWayBillNo.focus();
    } 
    else if ((hdn_IsServiceTaxApplicableForConsignor.value == '1' || hdn_IsServiceTaxApplicableForConsignee.value == '1') && (val(hdn_TotalInvoiceAmount.value) >= 50000 && val(hdn_ConsignorStateId.value) == 2 && val(hdn_ConsigneeStateId.value) == 2 && txt_eWayBillNo.value.length < 12 && hdn_Is_ServiceTax_ForCommodity.value == '1')) 
    {
        SetErrorMsg("Enter Proper e-Way Bill No.  As Per Gujrat Intra-State Rule");
        txt_eWayBillNo.focus();
    } 

//    else if ((hdn_IsServiceTaxApplicableForConsignor.value == '1' || hdn_IsServiceTaxApplicableForConsignee.value == '1') && (val(hdn_TotalInvoiceAmount.value) >= 50000 && val(hdn_ConsignorStateId.value) != val(hdn_ConsigneeStateId.value) && txt_eWayBillNo.value.length < 12 && hdn_Is_ServiceTax_ForCommodity.value == '1')) 
    else if ((hdn_IsServiceTaxApplicableForConsignor.value == '1' || hdn_IsServiceTaxApplicableForConsignee.value == '1') && (val(hdn_TotalInvoiceAmount.value) >= 50000 && val(hdn_ConsignorStateId.value) != val(hdn_ConsigneeStateId.value) && txt_eWayBillNo.value.length < 12 && hdn_Is_ServiceTax_ForCommodity.value == '1')) 
    {
        SetErrorMsg("Enter Proper e-Way Bill No.  As Per Inter-State Rule");
        txt_eWayBillNo.focus();
    }
//    else if ((hdn_IsServiceTaxApplicableForConsignor.value == '1' || hdn_IsServiceTaxApplicableForConsignee.value == '1') && (val(hdn_TotalInvoiceAmount.value) >= 50000 && val(hdn_ConsignorStateId.value) == 2 && val(hdn_ConsigneeStateId.value) == 2 && txt_eWayBillNo.value.length < 12 && hdn_Is_ServiceTax_ForCommodity.value == '1')) 
//    {
//        SetErrorMsg("e-Way Bill No. is Mandatory for Intra State Transaction in Gujrat.");
//        txt_eWayBillNo.focus();
//    }    

    else if (hdn_LoginUserHierarchy.value == 'BO' && val(ddl_PaymentType.value) == 5 ) 
    {
        SetErrorMsg("You Are Not Authorised To Save FOC LR");
        ddl_PaymentType.focus();
    } 
    
    else if (val(ddl_PaymentType.value) == 4 && val(ddl_Reason_Freight_Pending.value) <= 0)
    {
        SetErrorMsg("Select Proper Reason");
        ddl_Reason_Freight_Pending.focus();
    }
    
    else if (val(ddl_PaymentType.value) == 4 && txt_Paid_Freight_Pending_Person_Name.value.length <= 0)
    {
        SetErrorMsg("Enter Person Name");
        txt_Paid_Freight_Pending_Person_Name.focus();
    }

    else if (val(ddl_PaymentType.value) == 4 && txt_Paid_Freight_Pending_Person_Mobile.value.length < 10)
    {
        SetErrorMsg("Enter Person Mobile No.");
        txt_Paid_Freight_Pending_Person_Mobile.focus();
    }
    else
    {
        ATS = true;
    }
    return ATS;
}

//----------------------------------- On Page Load ---------------------------------------------------------//
 //***************************************************************************
    
 function On_PageUnLoad()
 {
   var GCId = document.getElementById('WucGCNew1_hdn_GCId').value;
   var lnk_Get_Next_No = document.getElementById('WucGCNew1_lnk_Get_Next_No');
   var ContractId = document.getElementById('WucGCNew1_hdn_ContractId').value;
   var ddl_Dly_Type =  document.getElementById('WucGCNew1_ddl_Dly_Type');
   
    initControl('WucGCNew1_txt_From_Location','WucGCNew1_lst_FromLoc');
    initControl('WucGCNew1_txt_To_Location','WucGCNew1_lst_ToLoc');
    initControl('WucGCNew1_txt_ConsignorName','WucGCNew1_lst_Consignors');
    initControl('WucGCNew1_txt_ConsigneeName','WucGCNew1_lst_Consignees');
    initControl('WucGCNew1_txt_Booking_Branch','WucGCNew1_lst_BookBranch');
    initControl('WucGCNew1_txt_ArrivedFrom_Branch','WucGCNew1_lst_ArrivedFromBranch');
    initControl('WucGCNew1_txt_Agency','WucGCNew1_lst_Agency');
    initControl('WucGCNew1_txt_Ledger','WucGCNew1_lst_Ledger');
    initControl('WucGCNew1_txt_BillingParty','WucGCNew1_lst_BillParty');
     initControl1('WucGCNew1_txt_Wholeseler','WucGCNew1_lst_Wholeseler');
    initControl('WucGCNew1_txt_BillingLocation','WucGCNew1_lst_BillLocation');
    initControl('WucGCNew1_txt_ContractClient','WucGCNew1_lst_ContractClient');

    EnableDisable_On_BookingType_Change();
    EnableDisable_On_RoadPermit_Change();
    Disable_Controls_On_ClientWise();
    On_BillingHierarchy_Load();
    EnableDisable_On_FreightBasis_Change();
    EnableDisable_On_DeliveryType_Change();
    OnPageLoad_ConsignorConsignee();
    Disable_On_MenuItemBasis();
    Call_IsAttachedOnPageLoad();
  
    
    if(ContractId > 0)
    {
      IsFromPageLoad = true;
      Fill_Contract_Change(IsFromPageLoad);
    }

    if(GCId <= '0')
    {
        if(ContractId < 0)
        {
            Calculate_Freight('0');
            Calculate_LoadingCharge();
            Calculate_DDCharge();
            Calculate_GrandTotal();
        }
    }
    else
    {
        lnk_Get_Next_No.style.display = 'none';
    }
    On_PickerChangeForServiceType();
    On_ConsigneeChange();
    On_PaymentType_Change();
    EnableDisable_On_LengthCharge_Change();
    Set_Payment_Details();
    
    //Change From true to false On 04 Sep 2020
     //ddl_Dly_Type.disabled = true; 
      ddl_Dly_Type.disabled = false; 
 }
//************************************************************************
function Get_Attached_GC_Details()
{
    document.getElementById('WucGCNew1_btn_GetAttachedGCDetails').click();
}
function GetEncreptedConsrId()
{
    document.getElementById('WucGCNew1_btn_ConsrId').click();
}
function GetEncreptedConseeId()
{
    document.getElementById('WucGCNew1_btn_ConseeId').click();
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
  //  txt_Control.value =  lst_Control.options[lst_Control.selectedIndex].text;

  //modified on 13-08-2016
  if (txt_Control.readOnly == false) {
      txt_Control.value = lst_Control.options[lst_Control.selectedIndex].text;
  }
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

//************************************************************************************************
function On_Instruction_Change()
{  
    var ddl_Instruction = document.getElementById('WucGCNew1_ddl_Instruction');
    var txt_InstructionRemark = document.getElementById('WucGCNew1_txt_InstructionRemark');

    if (ddl_Instruction.value > 0)
        txt_InstructionRemark.value = ddl_Instruction.options[ddl_Instruction.selectedIndex].text;
    else
        txt_InstructionRemark.value = ''; 
}    
//*************************************************************************************************
 
function Change_Consignee_Address()
{
    var Mode = document.getElementById('WucGCNew1_hdn_Mode').value;
    var ConsigneeId = document.getElementById('WucGCNew1_hdn_ConsigneeId').value;
    var ConsigneeName = document.getElementById('WucGCNew1_txt_ConsigneeName').value;
    var ConsigneeDDAddressLine1= document.getElementById('WucGCNew1_hdn_ConsigneeDDAddressLine1').value;  
    var ConsigneeDDAddressLine2 = document.getElementById('WucGCNew1_hdn_ConsigneeDDAddressLine2').value;

    if(val(ConsigneeId)> 0)
    {
        var Path='FrmNewGCConsigneeDDAddress.aspx?Mode=' + Mode + '&ConsigneeName='+ ConsigneeName + '&DDAddressLine1='+ ConsigneeDDAddressLine1 + '&DDAddressLine2='+ ConsigneeDDAddressLine2 + '&isnewconsignee='+ IsNewConsignee;
        w = screen.availWidth;
        h = screen.availHeight;
        var popW = 600, popH = 300;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
        
        window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');
    }
    else
    {
        alert("Please Select Consignee...");
        txt_ConsigneeName.focus();
    }
    return false;
}    
//***************************************************************************************************
 
function RequiredForms()
{  
    var hdn_DeliveryBaranchId= document.getElementById('WucGCNew1_hdn_DeliveryBranchId');          
    var txt_To_Location  = document.getElementById('WucGCNew1_txt_To_Location');

    if (val(hdn_DeliveryBaranchId.value) > 0)
    {
        var Path='FrmNewRequiredForms.aspx?DeliveryBaranchId=' + hdn_DeliveryBaranchId.value ;

        w = screen.availWidth;
        h = screen.availHeight;
        var popW = 600, popH = 350;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;

        window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');      
    }
    else
    {
        alert("Please Select To Location..");
        txt_To_Location.focus();
    }
}

 //*******************************************************************
 
function ContainerDetails()
{
    var hdn_Mode = document.getElementById('WucGCNew1_hdn_Mode');    
    var Path='FrmNewGCContainerDetails.aspx?Mode=' + hdn_Mode.value ;

    w = screen.availWidth;
    h = screen.availHeight;
    var popW = 500, popH = 300;
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
    window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');    

    return false;  
}
//*******************************************************************

function On_OtherCharges_Click()
{  
    var hdn_Mode = document.getElementById('WucGCNew1_hdn_Mode');
    var Path='FrmNewGCOtherCharge.aspx?Mode=' +  hdn_Mode.value  ;

    w = screen.availWidth;
    h = screen.availHeight;
    var popW = 650, popH = 400;
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
    
    window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');
return false;
} 
 
//*******************************************************************
   
function On_IsMultipleBilling_Click()
{
    var chk_IsMultipleBilling = document.getElementById('WucGCNew1_chk_IsMultipleBilling');
    var hdn_Mode = document.getElementById('WucGCNew1_hdn_Mode');
    var chk_Is_Multiple_Location_Billing_Allowed = document.getElementById('WucGCNew1_chk_Is_Multiple_Location_Billing_Allowed');
    var tr_billing_details = document.getElementById('WucGCNew1_tr_billing_details');
    
    if(chk_IsMultipleBilling.checked == true)
    {
        var Path='FrmNewGCBillingDetails.aspx?Mode=' + hdn_Mode.value + '&IsMultipleLocBillingAllow=' + chk_Is_Multiple_Location_Billing_Allowed.checked ;
        w = screen.availWidth;
        h = screen.availHeight;
        var popW = 800, popH = 500;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
        
        window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');
        tr_billing_details.style.display = 'none';         
    }
    else
    {
        tr_billing_details.style.display = 'inline';         
    }
}
//*******************************************************************
 
    function New_Consignor_Consignee(Is_Add,Is_Consignor)
    {
        var hdn_ConsignorId = document.getElementById('WucGCNew1_hdn_ConsignorId');
        var hdn_ConsigneeId = document.getElementById('WucGCNew1_hdn_ConsigneeId');        
        var hdn_EncreptedConsignorId = document.getElementById('WucGCNew1_hdn_EncreptedConsignorId');
        var hdn_EncreptedConsigneeId = document.getElementById('WucGCNew1_hdn_EncreptedConsigneeId');        
        var hdn_IsRegularConsignor = document.getElementById('WucGCNew1_hdn_IsRegularConsignor');
        var hdn_IsRegularConsignee = document.getElementById('WucGCNew1_hdn_IsRegularConsignee');        
        var txt_ConsignorName = document.getElementById('WucGCNew1_txt_ConsignorName');
        var txt_ConsigneeName = document.getElementById('WucGCNew1_txt_ConsigneeName');
        var branchid;

        var hdn_FromLocationId = document.getElementById('WucGCNew1_hdn_FromLocationId');
        var hdn_ToLocationId = document.getElementById('WucGCNew1_hdn_ToLocationId');        

        if (Is_Consignor == 1)
          branchid = GetBkgBranchID();
        else
          branchid = GetDlyBranchID();

        var Allow_Popup = 0;

        if (Is_Add == 0)
        {
           if (Is_Consignor == 1)
           {
              var Path='../../../Master/Sales/FrmRegularClientGC.aspx?Menu_Item_Id=MwA2AA==&Mode=MQA=&Call_From=GC' + '&Is_Consignor=' + Is_Consignor + '&BranchID=' + branchid + '&txt_ConsignorName=' + txt_ConsignorName.value + '&ServiceLocationID=' + hdn_FromLocationId.value;                   
           }
           else
           {
              var Path='../../../Master/Sales/FrmRegularClientGC.aspx?Menu_Item_Id=MwA2AA==&Mode=MQA=&Call_From=GC' + '&Is_Consignor=' + Is_Consignor + '&BranchID=' + branchid + '&txt_ConsigneeName=' + txt_ConsigneeName.value + '&ServiceLocationID=' + hdn_ToLocationId.value;                   
           }
            Allow_Popup = 1 ;
        }
        else
        {
            if (Is_Consignor == 1)
            {
                if(val(hdn_ConsignorId.value) > 0)
                {
                    if (val(hdn_IsRegularConsignor.value) == 1)
                    {
                        var Path='../../../Master/Sales/FrmRegularClientGC.aspx?Menu_Item_Id=MwA2AA==&Mode=MgA=&Id=' + hdn_EncreptedConsignorId.value + '&Call_From=GC' + '&Is_Consignor=1' ;                
                        Allow_Popup = 1 ;
                    }
                    else
                    {
                        alert("Please Select Reqular Consignor..");
                        txt_ConsignorName.focus();
                        Allow_Popup = 0 ;
                    }
                }
                else
                {
                    alert("Please Select Consignor..");
                    txt_ConsignorName.focus();
                    Allow_Popup = 0 ;
                }
            }
            else
            {
                if (val(hdn_ConsigneeId.value) > 0)
                {
                    if(val(hdn_IsRegularConsignee.value) == 1)
                    {
                        var Path='../../../Master/Sales/FrmRegularClientGC.aspx?Menu_Item_Id=MwA2AA==&Mode=MgA=&Id=' + hdn_EncreptedConsigneeId.value + '&Call_From=GC' + '&Is_Consignor=0';                
                        Allow_Popup = 1 ;
                    }
                    else 
                    {
                        alert("Please Select Reqular Consignee..");    
                        txt_ConsigneeName.focus();
                        Allow_Popup = 0 ;
                    }
                }
                else
                {
                    alert("Please Select Consignee..");    
                    txt_ConsigneeName.focus();
                    Allow_Popup = 0;
                }
            }
        }

        if (Allow_Popup == 1)
        {
            w = screen.availWidth;
            h = screen.availHeight;
            var popW = 900, popH = 600;
            var leftPos = (w-popW)/2;
            var topPos = (h-popH)/2;

            window.open(Path,'PopUp','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');      
        }
        return false;
    }

//*******************************************************************
 
  function View_Consignor_Consignee(Is_Consignor)
  {
        var hdn_EncreptedConsignorId = document.getElementById('WucGCNew1_hdn_EncreptedConsignorId');
        var hdn_EncreptedConsigneeId = document.getElementById('WucGCNew1_hdn_EncreptedConsigneeId');
        var hdn_ConsignorId = document.getElementById('WucGCNew1_hdn_ConsignorId');
        var hdn_ConsigneeId = document.getElementById('WucGCNew1_hdn_ConsigneeId');
        var txt_ConsignorName = document.getElementById('WucGCNew1_txt_ConsignorName');
        var txt_ConsigneeName = document.getElementById('WucGCNew1_txt_ConsigneeName');
        var hdn_IsRegularConsignee = document.getElementById('WucGCNew1_hdn_IsRegularConsignee');        
        var hdn_IsRegularConsignor = document.getElementById('WucGCNew1_hdn_IsRegularConsignor');

        var Allow_Popup = 0;
   
        if(Is_Consignor == 1)
        {
            if(val(hdn_ConsignorId.value) > 0)
            {
                if(hdn_IsRegularConsignor.value == '1')
                    var Path='../../../Master/Sales/FrmRegularClientGC.aspx?Menu_Item_Id=MwA2AA==&Mode=NAA=&Id=' + hdn_EncreptedConsignorId.value + '&Call_From=GC' + '&Is_Consignor=1'  ;                
                else
                    var Path='../../../Master/Sales/FrmClient.aspx?Menu_Item_Id=MwA2AA==&Mode=NAA=&Id=' + hdn_EncreptedConsignorId.value + '&Call_From=GC' + '&Is_Consignor=1'  ;                

                Allow_Popup = 1 ;
            }
            else
            {
                alert("Please Select Consignor..");
                txt_ConsignorName.focus();
            }
        }
        else
        {
            if(val(hdn_ConsigneeId.value) > 0)
            {
                if(hdn_IsRegularConsignee.value == '1')
                    var Path='../../../Master/Sales/FrmRegularClientGC.aspx?Menu_Item_Id=MwA2AA==&Mode=NAA=&Id=' + hdn_EncreptedConsigneeId.value + '&Call_From=GC' + '&Is_Consignor=0';                
                else 
                    var Path='../../../Master/Sales/FrmClient.aspx?Menu_Item_Id=MwA2AA==&Mode=NAA=&Id=' + hdn_EncreptedConsigneeId.value + '&Call_From=GC' + '&Is_Consignor=0'  ;                

                Allow_Popup = 1 ;
            }
            else
            {
                alert("Please Select Consignee..");
                txt_ConsigneeName.focus(); 
            }
        }

        if(Allow_Popup == 1)
        {
            w = screen.availWidth; h = screen.availHeight;
            var popW = 900, popH = 600;
            var leftPos = (w-popW)/2;
            var topPos = (h-popH)/2;

            window.open(Path,'PopUp','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');      
        }
        return false;
  }

//*******************************************************************

  function Open_PopPage(Is_Add_From_Location,CallFrom)
  { 
//        var hdn_Can_Add_Commodity = document.getElementById('WucGCNew1_hdn_Can_Add_Commodity');
//        var hdn_Can_Add_Item = document.getElementById('WucGCNew1_hdn_Can_Add_Item');
//        var hdn_Can_Add_Location = document.getElementById('WucGCNew1_hdn_Can_Add_Location');
//
//        if (hdn_Can_Add_Commodity.value == "True")
//        {
            var Path = '';
            
            if(CallFrom == 'AddCommodity')
               Path = '../../../Master/General/FrmCommodity.aspx?Menu_Item_Id=MQAzAA==&Mode=MQA=&Call_From=GC';
            else if(CallFrom == 'AddItem')
               Path='../../../Master/General/FrmItem.aspx?Menu_Item_Id=MQA2AA==&Mode=MQA=&Call_From=GC';
            else if(CallFrom == 'AddLocation')
               Path='../../../Master/Branch/FrmODALocation.aspx?Menu_Item_Id=MwA0AA==&Mode=MQA=&Call_From=GC&Is_From_Location=' + Is_Add_From_Location +'&BookingBranchId='+ document.getElementById('WucGCNew1_hdn_BookingBranchId').value;                   

            w = screen.availWidth;h = screen.availHeight;
            var popW = 900,popH = 600;
            var leftPos = (w-popW)/2;
            var topPos = (h-popH)/2;
            window.open(Path,'PopUp','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');    
//        }
//        else
//        {
//            alert("You Are Not Authorised To Perform This Operation...");
//        }           
        return false;  
   }   
//*******************************************************************
function View_GC_Details()
{
    var hdn_Rectify_GCId = document.getElementById('WucGCNew1_hdn_Rectify_GCId');

    var Path='FrmGCNew.aspx?Menu_Item_Id=MQA5ADQA&Mode=NAA=&Id='+ hdn_Rectify_GCId.value;
    w = screen.availWidth;
    h = screen.availHeight;
    var popW = (w-100);
    var popH = h-40;
    var leftPos = (w-popW)/2;
    var topPos = 0;
    window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',, menubar=no, resizable=no,scrollbars=yes,color=blue');      

   return false;
}
//*******************************************************************
function View_PickUp_Details()
{
    var hdnEncrypted_pickuprequestid = document.getElementById('WucGCNew1_hdnEncrypted_pickuprequestid');
    var lnk_Pickup_Req = document.getElementById('WucGCNew1_lnk_Pickup_Req');
    var ddl_pick_request = document.getElementById('WucGCNew1_ddl_pick_request');
    if (ddl_pick_request.value > 0)
    {
        lnk_Pickup_Req.style.visibility = 'visible'; 
        var Path='../../../CRM/Queries/frm_Customer_Pickup_Request.aspx?Menu_Item_Id=MQAyADUA&Mode=NAA=&Id='+ hdnEncrypted_pickuprequestid.value;
        
        w = screen.availWidth;h = screen.availHeight;
        var popW = 900,popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
        window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',, menubar=no, resizable=no,scrollbars=yes,color=blue');      
    }
    else
    {
        var Path='';
        lnk_Pickup_Req.style.visibility = 'hidden'; 
    }

   return false;
}
//*******************************************************************

function On_View()
{    
    var hdn_Mode = document.getElementById('WucGCNew1_hdn_Mode');
    var btn_Close = document.getElementById('WucGCNew1_btn_Close');
    var btn_Save_New = document.getElementById('WucGCNew1_btn_Save_New');
    var btn_Save_Exit = document.getElementById('WucGCNew1_btn_Save_Exit');
    var btn_Save_Print = document.getElementById('WucGCNew1_btn_Save_Print');
    var btn_Save_Repeat = document.getElementById('WucGCNew1_btn_Save_Repeat');

    var lnk_NewConsignor = document.getElementById('WucGCNew1_lnk_NewConsignor'); 
    var lnk_EditConsignor = document.getElementById('WucGCNew1_lnk_EditConsignor');
    var lnk_NewConsignee = document.getElementById('WucGCNew1_lnk_NewConsignee'); 
    var lnk_EditConsignee = document.getElementById('WucGCNew1_lnk_EditConsignee'); 
    var tr_AddLocation = document.getElementById('WucGCNew1_tr_AddLocation'); 
    var TD_Calender = document.getElementById('WucGCNew1_TD_Calender'); 

    var chk_IsMultipleBilling = document.getElementById('WucGCNew1_chk_IsMultipleBilling');
    var chk_IsInsured = document.getElementById('WucGCNew1_chk_IsInsured');

    var Enable = true; 
        
    if (val(hdn_Mode.value)== 4)
    {
        for(i = 0;i < document.forms[0].elements.length; i++) 
        {
            elm = document.forms[0].elements[i];
            if(elm.id!='')
               elm.disabled = Enable;
        }
        
        if(chk_IsInsured.checked == true)
            chk_IsInsured.disabled = !Enable;
            
        if(chk_IsMultipleBilling.checked == true)
            chk_IsMultipleBilling.disabled = !Enable;

        lnk_NewConsignor.style.visibility = 'hidden';
        lnk_NewConsignee.style.visibility = 'hidden';
        lnk_EditConsignee.style.visibility = 'hidden';
        lnk_EditConsignor.style.visibility = 'hidden';
        tr_AddLocation.style.display = 'none';
        TD_Calender.style.display = 'none';
        
        btn_Close.disabled = false;
        btn_Save_New.style.display = 'none';
        btn_Save_Exit.style.display = 'none';
        btn_Save_Print.style.display = 'none';
        btn_Save_Repeat.style.display = 'none';
    }  
 }
//***************************** On Save Button Click **************************************************
function SetErrorMsg(msg)
{
    document.getElementById('WucGCNew1_lbl_Errors').innerHTML = msg;
    document.getElementById('WucGCNew1_lbl_Errors1').innerHTML = msg;
}

///----------------------------------- GC No Validation ------------------------------/////////////

function Append_Zero_In_GC_No(Txt_GC_No)
{
   var GC_No_Length =  document.getElementById('WucGCNew1_hdn_GC_No_Length').value;

   var i = Txt_GC_No.value.length;
   var tempgcno = '';
   
    for(i;i < val(GC_No_Length);i++)
    {
        tempgcno = tempgcno + '0';
    }
    Txt_GC_No.value = tempgcno + Txt_GC_No.value;
}

// --------------------------------------------------------
function On_Change_Agency_No(txt_Agency_GCNo)
{
    document.getElementById('WucGCNew1_hdn_Private_Mark').value = txt_Agency_GCNo.value;
    document.getElementById('WucGCNew1_txt_Private_Mark').value = txt_Agency_GCNo.value;
}
function On_Private_Mark_Change(txt_PrivateMark)
{
   document.getElementById('WucGCNew1_hdn_Private_Mark').value = txt_PrivateMark.value;
}
// --------------------------------------------------------

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

function Set_ServiceTaxPayableBy()
{
    var ddl_ServiceTaxPayableBy = document.getElementById('WucGCNew1_ddl_ServiceTaxPayableBy');
    var PaymentTypeId = document.getElementById('WucGCNew1_ddl_PaymentType').value;
    var IsServiceTaxApplicableForConsignor = document.getElementById('WucGCNew1_hdn_IsServiceTaxApplicableForConsignor').value;
    var IsServiceTaxApplicableForConsignee = document.getElementById('WucGCNew1_hdn_IsServiceTaxApplicableForConsignee').value;
    var IsServiceTaxApplForBillParty = document.getElementById('WucGCNew1_hdn_IsServiceTaxApplForBillParty').value;
    var hdn_Mode = document.getElementById('WucGCNew1_hdn_Mode'); 

if (val(hdn_Mode.value) != 4)
  {
    if(IsServiceTaxApplicableForConsignor == "1" && (val(PaymentTypeId) == 2 || val(PaymentTypeId) == 4))
        ddl_ServiceTaxPayableBy.value = "1"; // consignor
    else if(IsServiceTaxApplicableForConsignee == '1' && val(PaymentTypeId) == 1)
        ddl_ServiceTaxPayableBy.value = "2"; // consignee
    else if(IsServiceTaxApplForBillParty == '1' && val(PaymentTypeId) == 3)
        ddl_ServiceTaxPayableBy.value = "1"; // consignor
    else
        ddl_ServiceTaxPayableBy.value = "3"; // transporter 

      document.getElementById('WucGCNew1_hdn_ServiceTaxPayableBy').value = ddl_ServiceTaxPayableBy.value;
   }
 }   
  

// --------------------------------------------------------
function UpdateFromRegularClient(clientname)
{
 
ispopupclientwindow = true;

if(Search_Type == 'Consignor')
{
    var txt_ConsignorName = document.getElementById('WucGCNew1_txt_ConsignorName');
    var lst_Consignors = document.getElementById('WucGCNew1_lst_Consignors');

    txt_ConsignorName.focus();

    NewGC_AllSearch(event,txt_ConsignorName,lst_Consignors,Search_Type,2,5);
}
else if(Search_Type == 'Consignee')
{
    var txt_ConsigneeName = document.getElementById('WucGCNew1_txt_ConsigneeName');
    var lst_Consignees = document.getElementById('WucGCNew1_lst_Consignees');

    txt_ConsigneeName.focus();

    NewGC_AllSearch(event,txt_ConsigneeName,lst_Consignees,Search_Type,2,5);
}

}   


// --------------------------------------------------------

function Print()
{ 
    var hdnissaveandnewandprint = document.getElementById('WucGCNew1_hdnissaveandnewandprint');
    var hdndocumentID = document.getElementById('WucGCNew1_hdndocumentID');
    var hdnprinturl = document.getElementById('WucGCNew1_hdnprinturl');
    
    var Path=hdnprinturl.value ;

    w = screen.availWidth;
    h = screen.availHeight;
    var popW = 1000, popH = 1000;
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;

    if (hdnissaveandnewandprint.value == '1')
        window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');

}

// ---------------------------------------------------------
//Added : Hemant 24 Dec 2020
function eWayBillLossFocus()
{
  
    var txt_Wholeseler =  document.getElementById('WucGCNew1_txt_Wholeseler');
    var tr_WholeselerBroker = document.getElementById('WucGCNew1_tr_WholeselerBroker');
    var txt_Freight = document.getElementById('WucGCNew1_txt_Freight');
     
    if (tr_WholeselerBroker.style.display == 'inline')
    {
        txt_Wholeseler.focus();
    }
    else
    {
        txt_Freight.focus();
    }    
    
}


// ---------------------------------------------------------
//Added : Hemant 13 Jul 2020
function CheckDuplicateeWayBillNo()
{
 
    var GC_Id = document.getElementById('WucGCNew1_hdn_GCId').value;
    var txt_eWayBillNo =  document.getElementById('WucGCNew1_txt_eWayBillNo');
    
        
    if(txt_eWayBillNo.value != '')
    {

        Raj.EC.OperationModel.NewGCSearch.CheckDuplicateeWayBillNo(GC_Id,txt_eWayBillNo.value,handleValideWayBill);

    }
    
}

// ---------------------------------------------------------
//Added : Hemant 13 Jul 2020
function handleValideWayBill(Results)
{
    var txt_eWayBillNo =  document.getElementById('WucGCNew1_txt_eWayBillNo');
    
    var Result = Results.value.Rows[0]['Is_Duplicate'];
    
    if(Result == true)
    {
        alert("Duplicate eWay Bill No. Same No. Entered In Another LR.");
        txt_eWayBillNo.focus();
    }
}