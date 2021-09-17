// JScript File


function ValidateUI_WucClientGeneral()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucClient1_WucClientGeneralDetails1_lbl_Errors');
   var ddl_Branch = document.getElementById('WucClient1_WucClientGeneralDetails1_ddl_Branch_txtBoxddl_Branch');
   var txt_ClientCode = document.getElementById('WucClient1_WucClientGeneralDetails1_txt_ClientCode');
   var txt_ClientName = document.getElementById('WucClient1_WucClientGeneralDetails1_txt_ClientName');
   var ddl_ClientGroup = document.getElementById('WucClient1_WucClientGeneralDetails1_ddl_ClientGroup');
   var txt_ContactPerson = document.getElementById('WucClient1_WucClientGeneralDetails1_txt_ContactPerson');
   var txt_CSTTINNo = document.getElementById('WucClient1_WucClientGeneralDetails1_txt_CSTTINNo');
   var txt_ServiceTaxNo = document.getElementById('WucClient1_WucClientGeneralDetails1_txt_ServiceTaxNo');
   var hdnGSTStateCode=document.getElementById('WucClient1_WucClientGeneralDetails1_WucAddress1_hdnGSTStateCode');
   var ddl_Name = document.getElementById('WucClient1_WucClientGeneralDetails1_WucAddress1_ddl_City_txtBoxddl_City');

   var Chk_IsServiceTaxPayable = document.getElementById('WucClient1_WucClientFinanceDetails1_chk_ServiceTaxPayableByClient');
   var GSTStateCode = hdnGSTStateCode.value;
    
   var Chk_Is_Casual_Taxable = document.getElementById('WucClient1_WucClientGeneralDetails1_chk_Is_Casual_Taxable').checked;

 

//  var objResource=new Resource('WucClient1_WucClientGeneralDetails1_hdf_ResourecString');
  
   lbl_Errors.innerText ="";
   
  if (ddl_Branch.value == '')
  {
     lbl_Errors.innerText = "Please Select Branch";// objResource.GetMsg("Msg_ddl_Branch");
     ddl_Branch.focus();
  }
  else if (txt_ClientCode.value == '')
  {
     lbl_Errors.innerText = "Please Enter Client Code";//objResource.GetMsg("Msg_txt_ClientCode");
     txt_ClientCode.focus();
  }
  else if (txt_ClientName.value == '')
  {
     lbl_Errors.innerText = "Please Enter Client Name";//objResource.GetMsg("Msg_txt_ClientName");
     txt_ClientName.focus();
  }
//  else if(txt_ClientName.value.length < 2)
//  {
//    lbl_Errors.innerText= "Client Name should be greater than 5 characters";//objResource.GetMsg("Msg_ClientNameValidation");
//    txt_ClientName.focus();    
//  }  
//  else if (parseInt(ddl_ClientGroup.value) == 0)
//  {
//     lbl_Errors.innerText = "Please Select Client Group";// objResource.GetMsg("Msg_ddl_ClientGroup");
//     ddl_ClientGroup.focus();
//  }
   else if (txt_ContactPerson.value == '' && control_is_mandatory(txt_ContactPerson) == true)
  {
     lbl_Errors.innerText = "Please Enter Contact Person";//objResource.GetMsg("Msg_Txt_ContactPerson");
     txt_ContactPerson.focus();
  }   
  else if (ValidateWucAddress(lbl_Errors) == false)
  {}
//  else if (txt_CSTTINNo.value == '' && control_is_mandatory(txt_CSTTINNo) == true)
//  {
//     lbl_Errors.innerText = "Please Enter CST/TIN No";//objResource.GetMsg("Msg_Txt_CSTTINNo");
//     txt_CSTTINNo.focus();
//  }
//  else if (txt_ServiceTaxNo.value == '' && control_is_mandatory(txt_ServiceTaxNo) == true)
//  {
//     lbl_Errors.innerText = "Please Enter Service Tax No";//objResource.GetMsg("Msg_Txt_ServiceTaxNo");
//     txt_ServiceTaxNo.focus();
  //  }

  else if (Chk_IsServiceTaxPayable.checked == true && txt_CSTTINNo.value.length != 15) 
  {
      lbl_Errors.innerText = "Please Enter 15 Digits GST No.";
      txt_CSTTINNo.focus();
  }
  else if (Chk_IsServiceTaxPayable.checked == true && ValidateGST(txt_CSTTINNo, GSTStateCode,Chk_Is_Casual_Taxable, lbl_Errors) == false) 
  {
      //lbl_Errors.innerText = "Please Enter Valid GST No.";
      txt_CSTTINNo.focus();
  }  
  else if (txt_CSTTINNo.value.trim().length > 0 && ValidateGST(txt_CSTTINNo, GSTStateCode,Chk_Is_Casual_Taxable, lbl_Errors) == false) 
  {
      //lbl_Errors.innerText = "Please Enter Valid GST No.";
      txt_CSTTINNo.focus();
  } 
  else
      ATS = true;

return ATS;
}



function ValidateUI_WucClientFinance()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucClient1_WucClientFinanceDetails1_lbl_Errors');
   var txt_CreditDays = document.getElementById('WucClient1_WucClientFinanceDetails1_txt_CreditDays');
   var rbtn_UseExistingLedger = document.getElementById('WucClient1_WucClientFinanceDetails1_rbtn_UseExistingLedger_0');
   var txt_CreditLimit = document.getElementById('WucClient1_WucClientFinanceDetails1_txt_CreditLimit');
   var txt_InterestPercentage = document.getElementById('WucClient1_WucClientFinanceDetails1_txt_InterestPercentage');
   var txt_MinimumBalance = document.getElementById('WucClient1_WucClientFinanceDetails1_txt_MinimumBalance');
   var txt_GraceDays = document.getElementById('WucClient1_WucClientFinanceDetails1_txt_GraceDays');
   var txt_BusinessHrs = document.getElementById('WucClient1_WucClientFinanceDetails1_txt_BusinessHrs');
   var ddl_UserProfile = document.getElementById('WucClient1_WucClientFinanceDetails1_ddl_UserProfile');
   var ddl_Ledger=document.getElementById('WucClient1_WucClientFinanceDetails1_ddl_Ledger_txtBoxddl_Ledger');
   var ddl_MarketingExecutive=document.getElementById('WucClient1_WucClientFinanceDetails1_ddl_MarketingExecutive_txtBoxddl_MarketingExecutive');
   var chk_CreateCargoUser = document.getElementById('WucClient1_WucClientFinanceDetails1_chk_CreateeCargouser'); 
    

//   var objResource=new Resource('WucClient1_WucClientFinanceDetails1_hdf_ResourecString');
//  
   lbl_Errors.innerText ="";
   
  if (rbtn_UseExistingLedger.checked == true && ddl_Ledger.value == '')
  {
     lbl_Errors.innerText = "Please Select Ledger";// objResource.GetMsg("Msg_ddl_Ledger");
     ddl_Ledger.focus();
  }
  else if (txt_CreditDays.value == '' && control_is_mandatory(txt_CreditDays) == true)
  {
     lbl_Errors.innerText = "Please Enter Credit Days";// objResource.GetMsg("Msg_txt_CreditDays");
//     txt_CreditDays.focus();
  }
  else if(txt_CreditLimit.value == '' && control_is_mandatory(txt_CreditLimit) == true)
  {
    lbl_Errors.innerText= "Please Enter Credit Limit";//objResource.GetMsg("Msg_txt_CreditLimit");
//    txt_CreditLimit.focus();    
  }  
//  else if(txt_MinimumBalance.value == '' && control_is_mandatory(txt_MinimumBalance) == true)
//  {
//    lbl_Errors.innerText= "Please Enter Minimum Balance"; 
//  }
  else if (txt_InterestPercentage.value == '' && control_is_mandatory(txt_InterestPercentage) == true)
  {
     lbl_Errors.innerText = "Please Enter Intrest Percent";//objResource.GetMsg("Msg_txt_IntrestPercent");
     txt_InterestPercentage.focus();
  }
   else if (txt_GraceDays.value == '' && control_is_mandatory(txt_GraceDays) == true)
  {
     lbl_Errors.innerText = "Please Enter Grace Days";//objResource.GetMsg("Msg_txt_GraceDays");
     txt_GraceDays.focus();
  } 
  else if (txt_BusinessHrs.value == '' && control_is_mandatory(txt_BusinessHrs) == true)
  {
     lbl_Errors.innerText = "Please Enter Business Hours";// objResource.GetMsg("Msg_Txt_BusinessHours");
     txt_BusinessHrs.focus();
  } 
  else if (ddl_MarketingExecutive.value == '')
  {
     lbl_Errors.innerText = "Please Select Marketing Executive";//objResource.GetMsg("Msg_DDL_MarketingExcutive");
     ddl_MarketingExecutive.focus();
  }
  else if(chk_CreateCargoUser.checked == true)
  {   
          if(ddl_UserProfile.value==0)
          {
            lbl_Errors.innerText='Please Select UserProfile';
            ddl_UserProfile.focus();
          }
          else
          {
            ATS = true;
          }
  }
  else
      ATS = true;

return ATS;
}



function Hide_Control()
{
    
   var chk_CreateeCargouser = document.getElementById('WucClient1_WucClientFinanceDetails1_chk_CreateeCargouser');
   var td_lblUserProfile = document.getElementById('WucClient1_WucClientFinanceDetails1_td_lblUserProfile');
   var td_chkUserProfile = document.getElementById('WucClient1_WucClientFinanceDetails1_td_chkUserProfile');
   var td_MDUserProfile = document.getElementById('WucClient1_WucClientFinanceDetails1_td_MDUserProfile');
   var ddlUserProfile = document.getElementById('WucClient1_WucClientFinanceDetails1_ddl_UserProfile');
   
   if(chk_CreateeCargouser.checked == true)
   {
    td_lblUserProfile.style.display = 'inline';
    td_chkUserProfile.style.display = 'inline';
    td_MDUserProfile.style.display = 'inline';
   }
   else
   {
    td_lblUserProfile.style.display = 'none';
    td_chkUserProfile.style.display = 'none';
    td_MDUserProfile.style.display = 'none';
    ddlUserProfile.value = '0';
   
   }
}
