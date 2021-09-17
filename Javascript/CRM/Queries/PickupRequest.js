// JScript File

function DDL_Commodity_Type_change()
{
    var ddl_CommodityType = document.getElementById('WucCustomerPickupRequest1_ddl_CommodityType');
    var DDL_Commodity_Type_text = ddl_CommodityType.options[ddl_CommodityType.selectedIndex].text;
    DDL_Commodity_Type_text = DDL_Commodity_Type_text.toUpperCase();
    if (DDL_Commodity_Type_text == "CHEMICALS")
      {
      var popW = 300;
      var popH = 320;
      var w = screen.availWidth;
      var h = screen.availHeight;

      var leftPos = (w-popW)/2;
      var topPos = (h-popH)/2;
      window.open('../../Operations/Booking/frm_Hazardous_Commodity.aspx', 'PopUp_Hazardous_From_CRM', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
      }
}
    
function Check_Click()
{
    var chk_Close = document.getElementById('WucCustomerPickupRequest1_chk_Close');
    var tr_Reason = document.getElementById('WucCustomerPickupRequest1_tr_Reason');
    var tr_GC = document.getElementById('WucCustomerPickupRequest1_tr_GC');
    
    if(chk_Close.checked == true)
    {
        tr_Reason.style.display ='inline';
        tr_GC.style.display ='inline';
    }
    else
    {
        tr_Reason.style.display ='none';
        tr_GC.style.display ='none';
    }

}

 function AllowToSave()
    {
    var ATS = false;
    var txt_Orgin = document.getElementById('WucCustomerPickupRequest1_txt_Orgin');
    var txt_Destination = document.getElementById('WucCustomerPickupRequest1_txt_Destination');
    var txt_Weight = document.getElementById('WucCustomerPickupRequest1_txt_Weight');
    var txt_Pkgs = document.getElementById('WucCustomerPickupRequest1_txt_Pkgs');
    var txt_Consignor = document.getElementById('WucCustomerPickupRequest1_txt_Consignor');
    var txt_ContactName = document.getElementById('WucCustomerPickupRequest1_txt_ContactName');
    var txt_MobileNo = document.getElementById('WucCustomerPickupRequest1_txt_MobileNo');
    var txt_Address = document.getElementById('WucCustomerPickupRequest1_txt_Address');
    var txt_TelephoneNo = document.getElementById('WucCustomerPickupRequest1_txt_TelephoneNo');
    var txt_EmailId = document.getElementById('WucCustomerPickupRequest1_txt_EmailId');
    var lbl_Errors = document.getElementById('WucCustomerPickupRequest1_lbl_Errors');
    
    var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
    var address = txt_EmailId.value;
  

    lbl_Errors.innerHTML = '';
    
    if(txt_Orgin.value =='' && control_is_mandatory(txt_Orgin) == true)
    {
    lbl_Errors.innerHTML = 'Please Enter PickUp Point';
    txt_Orgin.focus();
    }
    else if(txt_Destination.value ==''&& control_is_mandatory(txt_Destination) == true)
    {
    lbl_Errors.innerHTML = 'Please Enter Destination';
    txt_Destination.focus();
    }
    else if(txt_Weight.value <='0' && control_is_mandatory(txt_Weight) == true)
    {
    lbl_Errors.innerHTML = 'Please Enter Weight';
    txt_Weight.focus();
    }
    else if(txt_Pkgs.value <='0' && control_is_mandatory(txt_Pkgs) == true)
    {
    lbl_Errors.innerHTML = 'Please Enter Packages';
    txt_Pkgs.focus();
    }
    else if(txt_Consignor.value =='' && control_is_mandatory(txt_Consignor) == true)
    {
    lbl_Errors.innerHTML = 'Please Enter Consignor';
    txt_Consignor.focus();
    }
    else if(txt_ContactName.value =='' && control_is_mandatory(txt_ContactName) == true)
    {
    lbl_Errors.innerHTML = 'Please Enter Contact Name';
    txt_ContactName.focus();
    }
    else if(txt_MobileNo.value =='' && control_is_mandatory(txt_MobileNo) == true)
    {
    lbl_Errors.innerHTML = 'Please Enter Mobile No';
    txt_MobileNo.focus();
    }
    else if(txt_MobileNo.value.length < 10 && control_is_mandatory(txt_MobileNo) == true)
    {
    lbl_Errors.innerHTML = 'Invalid Mobile No';
    txt_MobileNo.focus();
    }
    else if(txt_Address.value =='' && control_is_mandatory(txt_Address) == true)
    {
    lbl_Errors.innerHTML = 'Please Enter Pickup Address';
    txt_Address.focus();
    }
    else if(txt_Address.value.length < 10 && control_is_mandatory(txt_Address) == true)
    {
    lbl_Errors.innerHTML = 'Pickup Address must be greater than 10 character';
    txt_Address.focus();
    }
    else if(txt_TelephoneNo.value =='' && control_is_mandatory(txt_TelephoneNo) == true)
    {
    lbl_Errors.innerHTML = 'Please Enter Telephone No';
    txt_TelephoneNo.focus();
    }
    else if(txt_TelephoneNo.value.length < 8 && control_is_mandatory(txt_TelephoneNo) == true)
    {
    lbl_Errors.innerHTML = 'Invalid Telephone No';
    txt_TelephoneNo.focus();
    }
    else if(txt_EmailId.value =='' && control_is_mandatory(txt_EmailId) == true)
    {
    lbl_Errors.innerHTML = 'Please Enter Email Id';
    txt_EmailId.focus();
    }
    else if(reg.test(address) == false && control_is_mandatory(txt_Address) == true)
    {
    lbl_Errors.innerHTML = 'Invalid Email Id';
    txt_EmailId.focus();
    }
    else
    ATS = true;
    
    return ATS;
    }