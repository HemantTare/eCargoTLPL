// JScript File
//mandatory : added :Ankit : 02-01-09 6.30 pm


function ValidateUI_WucContractGeneral()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucContract1_WucContractGeneral1_lbl_Errors');
   var txt_ContractName = document.getElementById('WucContract1_WucContractGeneral1_txt_ContractName');
   var ddl_Branch = document.getElementById('WucContract1_WucContractGeneral1_ddl_Branch_txtBoxddl_Branch');
   var ddl_ClientName= document.getElementById('WucContract1_WucContractGeneral1_ddl_ClientName_txtBoxddl_ClientName');
   var txt_POMaxLimit = document.getElementById('WucContract1_WucContractGeneral1_txt_POMaxLimit');
   var txt_Weight = document.getElementById('WucContract1_WucContractGeneral1_txt_Weight');
   var txt_Freight = document.getElementById('WucContract1_WucContractGeneral1_txt_Freight');
   var txt_Days = document.getElementById('WucContract1_WucContractGeneral1_txt_Days');
   var txt_Amount = document.getElementById('WucContract1_WucContractGeneral1_txt_Amount');
   var ddl_BillingClient = document.getElementById('WucContract1_WucContractGeneral1_ddl_BillingClientName_txtBoxddl_BillingClientName');
   //var ddl_BillingBranch = document.getElementById('WucContract1_WucContractGeneral1_ddl_BillingBranchName_txtBoxddl_BillingBranchName');


//  var objResource=new Resource('WucContract1_WucContractGeneral1_hdf_ResourecString');
  
   lbl_Errors.innerText ="";
  
  if(txt_ContractName.value == '')
  {
     lbl_Errors.innerText = "Select Contract Name.";
     txt_ContractName.focus();
  } 
  else if (ddl_Branch.value == '')
  {
     lbl_Errors.innerText = "Please Select Branch";// objResource.GetMsg("Msg_Branch");
     ddl_Branch.focus();
  }
  else if (ddl_ClientName.value == '')
  {
     lbl_Errors.innerText =  "Please Select Client Name";// objResource.GetMsg("Msg_ClientName");
     ddl_ClientName.focus();
  }
  else if (txt_POMaxLimit.value == '' && control_is_mandatory(txt_POMaxLimit) == true)
  {
     lbl_Errors.innerText = "Please Enter PO Max Limit";//objResource.GetMsg("Msg_POMaxLimit");
     txt_POMaxLimit.focus();
  }
  else if (ddl_BillingClient.value == '')
  {
     lbl_Errors.innerText = "Please Select Billing Party";// objResource.GetMsg("Msg_BillingClientName");
     ddl_BillingClient.focus();
  }
//  else if (ddl_BillingBranch.value == '')
//  {
//     lbl_Errors.innerText = "Please Select Billing Branch";//objResource.GetMsg("Msg_BillingBranchName");
//     ddl_BillingBranch.focus();
//  }
  else if(get_hierarchy_id() <= 0)
  {
    lbl_Errors.innerText = "Please Select Billing Hierarchy";
  }
  else if(get_hierarchy_id() != 0 && get_hierarchy_id() != 'HO' && get_location_id() <= 0)
  {
      if(get_hierarchy_id() == 'BO')
      {
        lbl_Errors.innerText = "Please Select Billing Branch";
      }
      
      if(get_hierarchy_id() == 'AO')
      {
        lbl_Errors.innerText = "Please Select Billing Area";
      }
      
      if(get_hierarchy_id() == 'RO')
      {
        lbl_Errors.innerText = "Please Select Billing Region";
      }
  }
  else if (txt_Weight.value == '' && control_is_mandatory(txt_Weight) == true) //added : Ankit champaneriya : 02-01-09 6.30 pm
  {
     lbl_Errors.innerText ="Please Enter Weight";// objResource.GetMsg("Msg_Weight");
     txt_Weight.focus();
  }
  else if(txt_Freight.value=='' && control_is_mandatory(txt_Freight) == true)
  {
    lbl_Errors.innerText=  "Please Enter Freight";//objResource.GetMsg("Msg_Freight");
    txt_Freight.focus();    
  }  
  else if (txt_Days.value == '' && control_is_mandatory(txt_Days) == true)
  {
     lbl_Errors.innerText = "Please Enter Days";// objResource.GetMsg("Msg_Days");
     txt_Days.focus();
  }  
  else if (txt_Amount.value == '' && control_is_mandatory(txt_Amount) == true)
  {
     lbl_Errors.innerText = "Please Enter Amount";//objResource.GetMsg("Msg_Amount");
     txt_Amount.focus();
  }   
  else
      ATS = true;

return ATS;
}