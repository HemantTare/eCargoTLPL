// JScript File

function ValidateUI_WucBranchGeneral()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucBranch1_WucBranchGeneralDetails1_lbl_Errors');
   var Txt_BranchCode = document.getElementById('WucBranch1_WucBranchGeneralDetails1_Txt_BranchCode');

   var Txt_BranchName = document.getElementById('WucBranch1_WucBranchGeneralDetails1_Txt_BranchName');
   var DDL_BranchType = document.getElementById('WucBranch1_WucBranchGeneralDetails1_DDL_BranchType');
   var DDL_Agency = document.getElementById('WucBranch1_WucBranchGeneralDetails1_DDL_Agency_txtBoxDDL_Agency');
   var DDL_MemoDestination = document.getElementById('WucBranch1_WucBranchGeneralDetails1_DDL_MemoDestination_txtBoxDDL_MemoDestination');
   var Txt_ContactPerson = document.getElementById('WucBranch1_WucBranchGeneralDetails1_Txt_ContactPerson');
   var DDL_Area = document.getElementById('WucBranch1_WucBranchGeneralDetails1_DDL_Area_txtBoxDDL_Area');
   var ddl_Name=document.getElementById('WucBranch1_WucBranchGeneralDetails1_WucAddress1_ddl_City_txtBoxddl_City');

//  var objResource=new Resource('WucBranch1_WucBranchGeneralDetails1_hdf_ResourecString');
  
   lbl_Errors.innerText ="";
   
  if (Trim(Txt_BranchCode.value) == '')
  {
     lbl_Errors.innerText = "Please Enter Branch Code";//objResource.GetMsg("Msg_txt_BranchCode");
     Txt_BranchCode.focus();
  }
  else if (Trim(Txt_BranchName.value) == '')
  {
     lbl_Errors.innerText = "Please Enter Branch Name";//objResource.GetMsg("Msg_txt_BranchName");
     Txt_BranchName.focus();
  }
//  else if(Txt_BranchName.value.length < 5)
//  {
//    lbl_Errors.innerText= "Branch Name should be greater than 5 characters"; // objResource.GetMsg("Msg_BranchnameValidation");
//    Txt_BranchName.focus();    
//  }  
  else if (parseInt(DDL_BranchType.value) == 0)
  {
     lbl_Errors.innerText = "Please Select Branch Type"; // objResource.GetMsg("Msg_ddl_BranchType");
     DDL_BranchType.focus();
  }
  else if (parseInt(DDL_BranchType.value) == 2 && DDL_Agency.value == '')
  {
     lbl_Errors.innerText = "Please Select Agency Account";//objResource.GetMsg("Msg_ddl_Agency");
     DDL_Agency.focus();
  }
   else if (Trim(DDL_MemoDestination.value) == '')
  {
     lbl_Errors.innerText = "Please Select Memo Destination";//objResource.GetMsg("Msg_ddl_MemoDestination");
     DDL_MemoDestination.focus();
  } 
  else if (Trim(Txt_ContactPerson.value) == '')
  {
     lbl_Errors.innerText = "Please Enter Contact Person";//objResource.GetMsg("Msg_Txt_ContactPerson");
     Txt_ContactPerson.focus();
  }
  else if (ValidateWucAddress(lbl_Errors) == false)
  {
  }
//  else if (Trim(DDL_Area.value) == '')
//  {
//     lbl_Errors.innerText ="Please Select Area";// objResource.GetMsg("Msg_ddl_Area");
//     DDL_Area.focus();
//  }   
  else
      ATS = true;

return ATS;
}

//function ValidateUI_WucBranchParameters()
//{
//   var ATS = false;
//   var lbl_Errors = document.getElementById('WucBranch1_WucBranchParameters1_lbl_Errors');

//   var DDL_DefaultCashLedger = document.getElementById('WucBranch1_WucBranchParameters1_DDL_DefaultCashLedger');
//  var objResource=new Resource('WucBranch1_WucBranchParameters1_hdf_ResourecString');
//  
//   lbl_Errors.innerText ="";
//   
//  else if (parseInt(DDL_DefaultCashLedger.value) == 0)
//  {
//     lbl_Errors.innerText = 'Please select Default Cash Ledger ' //objResource.GetMsg("Msg_ddl_BranchType");
//     DDL_DefaultCashLedger.focus();
//  }
// 
//  else
//      ATS = true;

//return ATS;
//}

//function EnableDisable_IsDeliveryAllowed()
//{
//    var DDL_DeliveryAt = document.getElementById('WucBranch1_WucBranchGeneralDetails1_DDL_DeliveryAt');
//    var chk_Is_DeliveryAllowed = document.getElementById('WucBranch1_WucBranchDeptServices1_chkbx_isDelivaryallowed');
//    var hdn_BranchId = document.getElementById('WucBranch1_WucBranchGeneralDetails1_hdn_BranchId');

//    var hdn_Is_DeliveryAllowed = document.getElementById('WucBranch1_WucBranchDeptServices1_hdn_IsDelivery');
//  
//       if (parseInt(DDL_DeliveryAt.value) == 0 || parseInt(DDL_DeliveryAt.value) == parseInt(hdn_BranchId.value))
//        {
//            chk_Is_DeliveryAllowed.checked = true;
//            chk_Is_DeliveryAllowed.disabled = true;
//            hdn_Is_DeliveryAllowed.value='1';
//        }
//       else
//        {
//            chk_Is_DeliveryAllowed.checked = false;
//            chk_Is_DeliveryAllowed.disabled = false;
//            hdn_Is_DeliveryAllowed.value='0';
//        }
//}

//function CheckDeliveryAllowed()
//{
//    var chk_Is_DeliveryAllowed = document.getElementById('WucBranch1_WucBranchDeptServices1_chkbx_isDelivaryallowed');
//    var hdn_Is_DeliveryAllowed = document.getElementById('WucBranch1_WucBranchDeptServices1_hdn_IsDelivery');
//  
//       if (chk_Is_DeliveryAllowed.checked == true)
//        {
//            hdn_Is_DeliveryAllowed.value='1';
//        }
//       else
//        {
//            hdn_Is_DeliveryAllowed.value='0';
//        }
//}
//function CheckCrossingAllowed()
//{
//    var chk_Is_Crossingbranch = document.getElementById('WucBranch1_WucBranchDeptServices1_chkbx_isCrossingbranch');
//    var hdn_Is_Crossingbranch = document.getElementById('WucBranch1_WucBranchDeptServices1_hdn_IsCrossing');

//       if (chk_Is_Crossingbranch.checked == true)
//        {
//            hdn_Is_Crossingbranch.value = '1';
//        }
//       else
//        {
//            hdn_Is_Crossingbranch.value = '0';
//        }

//}
//function EnableDisable_IsCrossingAllowed()
//{
//    var ddl_DefaultHub = document.getElementById('WucBranch1_WucBranchGeneralDetails1_DDL_DefaultHub');
//    var chk_Is_Crossingbranch = document.getElementById('WucBranch1_WucBranchDeptServices1_chkbx_isCrossingbranch');
//    var hdn_BranchId = document.getElementById('WucBranch1_WucBranchGeneralDetails1_hdn_BranchId');

//    var hdn_Is_Crossingbranch = document.getElementById('WucBranch1_WucBranchDeptServices1_hdn_IsCrossing');

//       if (parseInt(ddl_DefaultHub.value) == 0 || parseInt(ddl_DefaultHub.value) == parseInt(hdn_BranchId.value))
//        {
//            chk_Is_Crossingbranch.checked = true;
//            chk_Is_Crossingbranch.disabled = true;
//            hdn_Is_Crossingbranch.value = '1';
//        }
//       else
//        {
//            chk_Is_Crossingbranch.checked = false;
//            chk_Is_Crossingbranch.disabled = false;
//            hdn_Is_Crossingbranch.value = '0';
//        }

//}
