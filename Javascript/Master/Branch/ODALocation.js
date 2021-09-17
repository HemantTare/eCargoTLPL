// JScript File

function validateUI()
{
  
var ATS = false;
var txt_LocationName = document.getElementById('WucODALocation1_txt_LocationName');
var ddl_ControllingBranch= document.getElementById('WucODALocation1_ddl_ControllingBranch');
var txt_PrimaryPinCode= document.getElementById('WucODALocation1_txt_PrimaryPinCode');
var Chk_IsODALocation=document.getElementById('WucODALocation1_Chk_IsODALocation');
var tr_Charges = document.getElementById('WucODALocation1_tr_Charges');
        
var txt_ChargeUpto= document.getElementById('WucODALocation1_txt_ChargeUpto');
var txt_ChargeAbove= document.getElementById('WucODALocation1_txt_ChargeAbove');
var lbl_Errors =document.getElementById('WucODALocation1_lbl_Errors');
//var objResource=new Resource('WucODALocation1_hdf_ResourecString');


lbl_Errors.innerText='';

if(txt_LocationName.value == '')
 {
     lbl_Errors.innerText =  "Please Enter Location Name";//objResource.GetMsg("Msg_LocationName");
     txt_LocationName.focus();
 }
 else if(ddl_ControllingBranch.value == '0')
 {
     lbl_Errors.innerText = "Please Select Controlling Branch";// objResource.GetMsg("Msg_ddlControllingBranch");
     ddl_ControllingBranch.focus();
 } 
  else if (txt_PrimaryPinCode.value == '')
 {
    lbl_Errors.innerText =  "Please Enter Primary Pin Code";//objResource.GetMsg("Msg_PrimaryPinCode");
     txt_PrimaryPinCode.focus();
    
 }
  else if (Chk_IsODALocation.checked==true && txt_ChargeUpto.value == '')
 {
    lbl_Errors.innerText =  "Please Enter ODA Charges Upto 500 Kg";//objResource.GetMsg("Msg_ChargesUpto");
    txt_ChargeUpto.focus();
 }
  else if (Chk_IsODALocation.checked=='true' && txt_ChargeAbove.value == '')
 {
    lbl_Errors.innerText = "Please Enter ODA Charges Above 500 Kg";//objResource.GetMsg("Msg_ChargesAbove");
    txt_ChargeAbove.focus();
 }
 else 
 ATS = true;

 return ATS;

  
}
//function showhide()
//    {
//        var Chk_IsODALocation=document.getElementById('WucODALocation1_Chk_IsODALocation');
//        var tr_Charges = document.getElementById('WucODALocation1_tr_Charges');
//        if(Chk_IsODALocation.checked==true)
//        {
//            tr_Charges.style.visibility='visible';
//        }
//        else
//            tr_Charges.style.visibility='hidden';
//        return false;
//    }