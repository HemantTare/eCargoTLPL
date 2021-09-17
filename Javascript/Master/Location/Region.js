// JScript File
function validateUI()
{
  
var ATS = false;
var txt_RegionCode = document.getElementById('WucRegion1_txt_RegionCode');
var txt_RegionName = document.getElementById('WucRegion1_txt_RegionName');
var ddl_Country= document.getElementById('WucRegion1_ddl_Country');
var ddl_CashLedger=document.getElementById('WucRegion1_ddl_CashLedger');
var ddl_BankLedger=document.getElementById('WucRegion1_ddl_BankLedger');
var lbl_Errors =document.getElementById('Wucregion1_lbl_Errors');
//var objResource=new Resource('WucRegion1_hdf_ResourecString');

lbl_Errors.innerText='';

if(txt_RegionCode.value == '')
 {
     lbl_Errors.innerText = "Please Enter Region Code";// objResource.GetMsg("Msg_RegionCode");
     txt_RegionCode.focus();
 }
 else if(txt_RegionName.value == '')
 {
     lbl_Errors.innerText = "Please Enter Region Name";//objResource.GetMsg("Msg_RegionName");
     txt_RegionName.focus();
 }
 else if(ddl_Country.value == '0')
 {
     lbl_Errors.innerText ="Please Select Country";//objResource.GetMsg("Msg_ddlCountry");
     ddl_Country.focus();
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



