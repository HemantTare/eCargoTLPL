// JScript File

function validateUI_CompanyGeneralDetails()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucCompany1_WucCompanyGeneralDetails1_lbl_Errors');
   var txt_CompanyName = document.getElementById('WucCompany1_WucCompanyGeneralDetails1_txt_CompanyName');
   var txt_MailingName = document.getElementById('WucCompany1_WucCompanyGeneralDetails1_txt_MailingName');
   var ddl_Name=document.getElementById('WucCompany1_WucCompanyGeneralDetails1_WucAddress1_ddl_City_txtBoxddl_City');
   var ddl_HOLedger = document.getElementById('WucCompany1_WucCompanyGeneralDetails1_ddl_HOLedger_hdnddl_HOLedger');
   var ddl_PFALedger = document.getElementById('WucCompany1_WucCompanyGeneralDetails1_ddl_PFALedger_hdnddl_PFALedger');
   
//   var objResource=new Resource('WucCompany1_WucCompanyGeneralDetails1_hdf_ResourecString');

  lbl_Errors.innerText ="";
   
   if (txt_CompanyName.value == '')
  {
     lbl_Errors.innerText = "Please Enter Company Name";//objResource.GetMsg("Msg_CompanyName");
     txt_CompanyName.focus();
  }
  else if (txt_MailingName.value == '')
  {
     lbl_Errors.innerText = "Please Enter Mailing Name";//objResource.GetMsg("Msg_MailingName");
     txt_MailingName.focus();
  }
  else if (ValidateWucAddress(lbl_Errors) == false)
  {
  }
  else if (val(ddl_HOLedger.value) <= 0)
  {
     lbl_Errors.innerText =  "Please Select HO Ledger";//objResource.GetMsg("Msg_HOLedger");
     
  }
  else if (val(ddl_PFALedger.value) <= 0)
  {
     lbl_Errors.innerText = "Please Select PFA Ledger";/// objResource.GetMsg("Msg_PFALedger");
     
  }
  else
      ATS = true;

return ATS;
}


function  validateUI_CompanyTDSFBTDetails()
{
    var ATS = false;
   var lbl_Errors = document.getElementById('WucCompany1_WucCompanyTDSFBTDetails1_lbl_Errors');
   var txt_Tax_Assessment_Number = document.getElementById('WucCompany1_WucCompanyTDSFBTDetails1_txt_Tax_Assessment_Number');
   var ddl_Person_Responsible=document.getElementById('WucCompany1_WucCompanyTDSFBTDetails1_ddl_Person_Responsible');
   var ddl_Assessee_Type=document.getElementById('WucCompany1_WucCompanyTDSFBTDetails1_ddl_Assessee_Type');
   var ddl_Assessee_Category=document.getElementById('WucCompany1_WucCompanyTDSFBTDetails1_ddl_Assessee_Category');
   
//   var objResource=new Resource('WucCompany1_WucCompanyTDSFBTDetails1_hdf_ResourecString');
//   
   lbl_Errors.innerText ="";
   
   if (txt_Tax_Assessment_Number.value == '')
  {
     lbl_Errors.innerText = "Please Enter Tax Assessment Number";//objResource.GetMsg("Msg_TaxAssessmentNo");
     txt_Tax_Assessment_Number.focus();
  }
  else if(ddl_Person_Responsible.value == '0')
 {
     lbl_Errors.innerText = "Please select Name of the  Person Responsible";//objResource.GetMsg("Msg_ddlEmployee");
     ddl_Person_Responsible.focus();
 }
 else if(ddl_Assessee_Type.value == '0')
 {
     lbl_Errors.innerText = "Please select Assessee Type";//objResource.GetMsg("Msg_ddlAssesseeType");
     ddl_Assessee_Type.focus();
 }
 else if(ddl_Assessee_Category.value == '0')
 {
     lbl_Errors.innerText = "Please select Assessee Category";//objResource.GetMsg("Msg_ddlAssesseeCategory");
     ddl_Assessee_Category.focus();
 }
  else
      ATS = true;

return ATS;

}

function EnableExpenseLedgerOnChecked()
{
   
   var Chk_IsTreatAdvanceForOwnTruckAsExpense=document.getElementById('WucCompany1_WucCompanyTripHireParameters1_Chk_IsTreatAdvanceForOwnTruckAsExpense');
   var tr_ExpenseLedger=document.getElementById('WucCompany1_WucCompanyTripHireParameters1_tr_ExpenseLedger');


      if(Chk_IsTreatAdvanceForOwnTruckAsExpense.checked == true)
            {
             tr_ExpenseLedger.style.display='inline';                         
            }
            else
            {               
             tr_ExpenseLedger.style.display='none';       
            }
}

function  validateUI_CompanyTripHireParametersDetails()
{
    var ATS = false;
    var lbl_Errors = document.getElementById('WucCompany1_WucCompanyTripHireParameters1_lbl_Errors');
    var Chk_IsTreatAdvanceForOwnTruckAsExpense=document.getElementById('WucCompany1_WucCompanyTripHireParameters1_Chk_IsTreatAdvanceForOwnTruckAsExpense');
    var ddl_TripExpenseLedger=document.getElementById('WucCompany1_WucCompanyTripHireParameters1_ddl_TripExpenseLedger_txtBoxddl_TripExpenseLedger');
   
    
    if(Chk_IsTreatAdvanceForOwnTruckAsExpense.checked == true )
       { 
       //alert('b');
          if (ddl_TripExpenseLedger.value < 0)
          {
           //alert('d');
           lbl_Errors.innerText="Please Select Trip Expense Ledger Name";
           ddl_TripExpenseLedger.focus();
           ATS=false;
          }
       }
       else
      ATS = true;

return ATS;
          
   }




