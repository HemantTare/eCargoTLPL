// JScript File

function getValues()
{
var checkBoxList = document.getElementById("WucArea1_WucAreaGeneralDetails1_chk_ListDivision");
for(var i=0;i<checkBoxList.length;i++)
{
if (checkBoxList[i].checked == true)
{
  return true
}
else
{
 alert("Please Select Atleast One Checkbox");
 break;
}
return false;
}
}

function validateUI_AreaGeneralDetails()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucArea1_WucAreaGeneralDetails1_lbl_Errors');
   var txt_AreaCode = document.getElementById('WucArea1_WucAreaGeneralDetails1_txt_AreaCode');
   var txt_AreaName = document.getElementById('WucArea1_WucAreaGeneralDetails1_txt_AreaName');
   var txt_ContactPerson=document.getElementById('WucArea1_WucAreaGeneralDetails1_txt_ContactPerson');
    var ddl_Name=document.getElementById('WucArea1_WucAreaGeneralDetails1_WucAddress1_ddl_City_txtBoxddl_City');

//  var objResource=new Resource('WucArea1_WucAreaGeneralDetails1_hdf_ResourecString');
//  
   lbl_Errors.innerText ="";
   
   if (txt_AreaCode.value == '')
  {
     lbl_Errors.innerText = "Please Enter Area Code";// objResource.GetMsg("Msg_AreaCode");
     txt_AreaCode.focus();
  }
  else if (txt_AreaName.value == '')
  {
     lbl_Errors.innerText = "Please Enter Area Name";//objResource.GetMsg("Msg_AreaName");
     txt_AreaName.focus();
  }
  else if (txt_ContactPerson.value == '')
  {
     lbl_Errors.innerText = "Please Enter Contact Person";// objResource.GetMsg("Msg_ContactPerson");
     txt_ContactPerson.focus();
  }
  else if (ValidateWucAddress(lbl_Errors) == false)
  {
  }
//  else if (getValues()==false)
//     {
//       lbl_Errors.innerText = "Select Atleast One Division" ; 
//     }
  else
      ATS = true;

return ATS;
}


function validateUI_AreaDepartment()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucArea1_WucAreaDepartment1_lbl_Errors');
   var txt_CashLimit = document.getElementById('WucArea1_WucAreaDepartment1_txt_CashLimit');
   var txt_BankLimit = document.getElementById('WucArea1_WucAreaDepartment1_txt_BankLimit');
   var ddl_CashLedger=document.getElementById('WucArea1_WucAreaDepartment1_ddl_CashLedger');
   var ddl_BankLedger=document.getElementById('WucArea1_WucAreaDepartment1_ddl_BankLedger');
  
  
//  var objResource=new Resource('WucArea1_WucAreaDepartment1_hdf_ResourecString');
//  
   lbl_Errors.innerText ="";
   
   if ( txt_CashLimit.value == '' && control_is_mandatory(txt_CashLimit) == true)
  {
     lbl_Errors.innerText = "Please Enter Cash Limit";// objResource.GetMsg("Msg_CashLimit");
      txt_CashLimit.focus();
  }
  else if (txt_BankLimit.value == '' && control_is_mandatory(txt_BankLimit) == true)
  {
     lbl_Errors.innerText =  "Please Enter Bank Limit";///objResource.GetMsg("Msg_BankLimit");
     txt_BankLimit.focus();
  }
   else if (ddl_CashLedger.value == '0'  && control_is_mandatory(ddl_CashLedger) == true)
 {
     lbl_Errors.innerText ="Please Select Cash Ledger";
     ddl_CashLedger.focus();
 }
 else if (ddl_BankLedger.value == '0'  && control_is_mandatory(ddl_BankLedger) == true)
 {
     lbl_Errors.innerText ="Please Select Bank Ledger";
     ddl_BankLedger.focus();
 }
 else
      ATS = true;

return ATS;
}
