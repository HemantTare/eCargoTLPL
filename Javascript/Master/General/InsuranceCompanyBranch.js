// JScript File
function validateUI()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucInsuranceCompanyBranch1_lbl_Errors');
   var txt_Branch_Name = document.getElementById('WucInsuranceCompanyBranch1_txt_Branch_Name');
   var txt_Contact_Person =document.getElementById('WucInsuranceCompanyBranch1_txt_Contact_Person');
   var ddl_InsuranceBranchName =document.getElementById('WucInsuranceCompanyBranch1_ddl_Insurance_Company_Branch_Name');
//    var objResource=new Resource('WucInsuranceCompanyBranch1_hdf_ResourecString');
//    
  lbl_Errors.innerText ="";
  
  if ( ddl_InsuranceBranchName.value == '' || ddl_InsuranceBranchName.value == '0')
  {
      lbl_Errors.innerText = "Please Select Insurance Company";//objResource.GetMsg("Msg_ddl_Insurance_Company_branch");
  }
  
  else if (Trim(txt_Branch_Name.value) == '')
  {
      lbl_Errors.innerText = "Please Enter Branch Name";// objResource.GetMsg("Msg_txt_BranchName");
      txt_Branch_Name.focus();
  }
  else if (Trim(txt_Contact_Person.value)=='')
  {
    lbl_Errors.innerText= "Please Enter Contact Person";// objResource.GetMsg("Msg_txt_ContactPerson");
    txt_Contact_Person.focus();
  }
  else if (!ValidateWucAddress(lbl_Errors))
  {
      
  }
  else
      ATS = true;

return ATS;
}

function Trim(sString) 
{
    //Begining space Removes
    while (sString.substring(0,1) == ' ')
        {
             sString = sString.substring(1, sString.length);
        }
    //Ending space Removes
    while (sString.substring(sString.length-1, sString.length) == ' ')
        {
             sString = sString.substring(0,sString.length-1);
        }
     return sString;
}




