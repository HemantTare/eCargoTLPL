// JScript File

function Allow_To_Save()
{
    var ATS = false;
    var txt_City = document.getElementById("txt_City");
    var hdn_CityId = document.getElementById("hdn_CityId");
    var txt_Party = document.getElementById("txt_Party");
    var hdn_PartyId = document.getElementById("hdn_PartyId"); 
    
    var txt_Contact1MobileNo1 = document.getElementById("txt_Contact1MobileNo1");
    var txt_Contact1MobileNo2 = document.getElementById("txt_Contact1MobileNo2");
    
    var txt_Contatc2MobileNo1 = document.getElementById("txt_Contatc2MobileNo1");
    var txt_Contatc2MobileNo2 = document.getElementById("txt_Contatc2MobileNo2");
    
    var txt_SenderName = document.getElementById("txt_SenderName");
    var txt_SenderMobileNo = document.getElementById("txt_SenderMobileNo");
    
    var lbl_Errors = document.getElementById("lbl_Errors");
    if(parseInt(hdn_CityId.value) <= 0)
    {
        lbl_Errors.innerHTML = 'Please Select City';
        txt_City.focus();
    }
    else if(parseInt(hdn_PartyId.value) <= 0)
    {
        lbl_Errors.innerHTML = 'Please Select Client.';
        txt_Party.focus();
    }
    else if(txt_Contact1MobileNo1.value == '')
    {
        lbl_Errors.innerHTML = 'Please Enter Contact Person 1 Mobile Number';
        txt_Contact1MobileNo1.focus();
    }
    else if(txt_Contact1MobileNo1.value.trim().length < 10)
    {
        lbl_Errors.innerHTML = 'Please Enter Contact Person 1 Valid Mobile Number';
        txt_Contact1MobileNo1.focus();
    }
    else if(txt_Contact1MobileNo2.value != '' && txt_Contact1MobileNo2.value.trim().length < 10)
    {
        lbl_Errors.innerHTML = 'Please Enter Contact Person 1 Valid Mobile Number 2';
        txt_Contact1MobileNo2.focus();
    }    
    else if(txt_Contatc2MobileNo1.value != '' && txt_Contatc2MobileNo1.value.trim().length < 10)
    {
        lbl_Errors.innerHTML = 'Please Enter Contact Person 2 Valid Mobile Number';
        txt_Contatc2MobileNo1.focus();
    }
    else if(txt_Contatc2MobileNo2.value != '' && txt_Contatc2MobileNo2.value.trim().length < 10)
    {
        lbl_Errors.innerHTML = 'Please Enter Contact Person 2 Valid Mobile Number 2';
        txt_Contatc2MobileNo2.focus();
    }   
    else if(txt_SenderName.value == '')
    {
        lbl_Errors.innerHTML = 'Please Enter Sender Name';
        txt_SenderName.focus();
    }
    else if(txt_SenderMobileNo.value == '')
    {
        lbl_Errors.innerHTML = 'Please Enter Sender Mobile No';
        txt_SenderMobileNo.focus();
    }
    else if(txt_SenderMobileNo.value.trim().length < 10)
    {
        lbl_Errors.innerHTML = 'Please Enter Valid Sender Mobile No';
        txt_SenderMobileNo.focus();
    }
    else
        ATS = true;
    
    return ATS;
}

var Search_Type;
var lst_control_id;
var keyCode;
function Search_txtSearch(e,txtbox,lstBox,SearchType,length)
{    
    this.keyCode = e.keyCode;
    Search_Type = SearchType;
    lst_control_id = lstBox;
    if (txtbox.value == '')
    {
        Clear_listbox(lstBox);
        hidecontrol(document.getElementById(lstBox));
        return;
    }
    var hdncityId = document.getElementById("hdn_CityId");
    var txtvalue = txtbox.value.toUpperCase();
    if(txtvalue.length >= length)
    {
        if (keyCode != 37 && keyCode != 38 && keyCode != 39 && keyCode != 40)
        {
            if(oldvalue != txtvalue)
            {
                if(SearchType == 'City')
                    Raj.EF.CallBackFunction.CallBack.GetTxtSearchCity(txtvalue,0,handleResults);
                else if(SearchType == 'Party')
                    Raj.EF.CallBackFunction.CallBack.PTLFTLClient_txtSearch(txtvalue,hdncityId.value,handleResults);
            }
        }
    }
    if (keyCode == 38 || keyCode == 40)
        listboxupdown(txtbox,lstBox);
}

function Search_txtMultiSearch(e,txtbox,lstBox,SearchType,length)
{    
    this.keyCode = e.keyCode;
    Search_Type = SearchType;
    lst_control_id = lstBox;
    initControl3(txtbox,lstBox);
    var atxtbox = document.getElementById(txtbox);
    if (atxtbox.value == '')
    {
        Clear_listbox(lstBox);
        hidecontrol(document.getElementById(lstBox));
        return;
    }
    
    var txtvalue = atxtbox.value.toUpperCase();
    
    if(txtvalue.length >= length)
    {
        if (keyCode != 37 && keyCode != 38 && keyCode != 39 && keyCode != 40)
        {
            if(oldvalue != txtvalue)
            {
                if(SearchType == 'Driver')
                    Raj.EF.CallBackFunction.CallBack.GetTxtSearchDriver(txtvalue,handleResults);
                if(SearchType == 'Vehicle')
                    Raj.EF.CallBackFunction.CallBack.GetTxtSearchVehicle(txtvalue,handleResults);
            }
        }
    }
    
    if (keyCode == 38 || keyCode == 40)
        listboxupdown(atxtbox,lstBox);
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
    
     if(Search_Type == 'Party')
     {
         if (oldvalue != txtbox_value)
            update_PartyDetails();
     }
}

function On_txtDriverLostFocus(txtbox,list_control,hdn_control,txtboxmobile1,txtboxmobile2)
{
    var txtbox_value = document.getElementById(txtbox).value.toUpperCase();
    var listcontrol = document.getElementById(list_control); 
    var list_control_index = listcontrol.selectedIndex;
    var list_control_value;
    var list_control_text;
    
    hidecontrol(listcontrol);
    if (oldvalue != '' && oldvalue != txtbox_value)
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
    
     if (oldvalue != '' && oldvalue != txtbox_value)
        Raj.EF.CallBackFunction.CallBack.GetSearchDriverDetails(list_control_value,function (results){
          if (results.value != null)
          {  
            var rows = results.value.Rows[0];
            document.getElementById(txtboxmobile1).value = val(rows['Mobile_No_1']);
            document.getElementById(txtboxmobile2).value = val(rows['Mobile_No_2']);
          }
          else
          {
            document.getElementById(txtboxmobile1).value = '';
            document.getElementById(txtboxmobile2).value = '';
          }
        });
     
}

function On_txtVehicleLostFocus(txtbox,list_control,hdn_control,hdndriverid,txtboxdriver,txtboxmobile1,txtboxmobile2)
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
    
     if (oldvalue != txtbox_value)
        Raj.EF.CallBackFunction.CallBack.GetSearchVehicleDetails(list_control_value,function (results)
        {
          if (results.value != null)
          {
          
            var rows = results.value.Rows[0];
            if(val(rows['Driver_ID']) > 0)
            {
            document.getElementById(hdndriverid).value = val(rows['Driver_ID']);
            document.getElementById(txtboxdriver).value = rows['Driver_Name'];
            document.getElementById(txtboxmobile1).value = val(rows['Mobile_No_1']);
            document.getElementById(txtboxmobile2).value = val(rows['Mobile_No_2']);
            }
          }
          else
          {
            document.getElementById(hdndriverid).value = '0';
            document.getElementById(txtboxdriver).value = '';
            document.getElementById(txtboxmobile1).value = '';
            document.getElementById(txtboxmobile2).value = '';
          }
        });
     
}





















