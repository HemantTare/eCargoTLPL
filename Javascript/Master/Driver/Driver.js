// JScript File

function LoadImageForm()
  {
  var hdn_DriverImageName = document.getElementById('WucDriver1_WucDriverDetails1_hdn_DriverImageName');

  if (hdn_DriverImageName.value == '')
    {
      alert("Please Select Image");
    }
   else
   {
    var Path =''
    Path='../../FrmShowImage.aspx?ImageName=' + hdn_DriverImageName.value;
    
    //alert(Path);
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-400);
    var popH = (h-290);
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
    window.open(Path, 'ShowImage', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=yes,scrollbars=yes');
//    return false;
    }
  }
 
 
 function Allow_To_Upload_Image()
 {
 var Allow_To_Upload_Image = false;
 
 var FU_DriverImage = document.getElementById('WucDriver1_WucDriverDetails1_FU_DriverImage');
 
 if (FU_DriverImage.value == '')
  {
  alert("Please Select Image");
  }
 else
  {
  Allow_To_Upload_Image = true;
  }
  
  return Allow_To_Upload_Image;
 }
 function EnableDisableLicenseAuthenticatedBy()
{
  var Chk_IsLicenseAuthencticated=document.getElementById('WucDriver1_WucDriverDetails1_Chk_IsLicenseAuthencticated');
 var lbl_LicenseAuthenticatedBy=document.getElementById('WucDriver1_WucDriverDetails1_lbl_LicenseAuthenticatedBy');
  var txt_LicenseAuthenticatedBy=document.getElementById('WucDriver1_WucDriverDetails1_txt_LicenseAuthenticatedBy');
 var lbl_Mandatory_LicenseAuthenticatedby=document.getElementById('WucDriver1_WucDriverDetails1_lbl_Mandatory_LicenseAuthenticatedby');
 
  if (Chk_IsLicenseAuthencticated.checked == true)
  {
     lbl_LicenseAuthenticatedBy.style.display='inline';
     txt_LicenseAuthenticatedBy.style.display='inline';
     lbl_Mandatory_LicenseAuthenticatedby.style.display='inline';
  }
  else
  {
     lbl_LicenseAuthenticatedBy.style.display='none';
     txt_LicenseAuthenticatedBy.style.display='none';
     lbl_Mandatory_LicenseAuthenticatedby.style.display='none';
  }
}
 

//function AllowToSaveWUCDriverDetails()
//{
//var TB_Driver = WucDriver1_TB_Driver;

//var txt_Driver_Code = document.getElementById('WucDriver1_WucDriverDetails1_txt_Driver_Code');
//var Txt_Driver_Name = document.getElementById('WucDriver1_WucDriverDetails1_Txt_Driver_Name');
//var txt_Qualification = document.getElementById('WucDriver1_WucDriverDetails1_txt_Qualification');
//var txt_License_No = document.getElementById('WucDriver1_WucDriverDetails1_txt_License_No');
//var txt_Native_Address_Line1 = document.getElementById('WucDriver1_WucDriverDetails1_txt_Native_Address_Line1');
//var txt_Contact_No = document.getElementById('WucDriver1_WucDriverDetails1_txt_Contact_No');
//var txt_Refence_Name = document.getElementById('WucDriver1_WucDriverDetails1_txt_Refence_Name');
//var txt_Refence_Phone = document.getElementById('WucDriver1_WucDriverDetails1_txt_Refence_Phone');
//var txt_Refence_Mobile = document.getElementById('WucDriver1_WucDriverDetails1_txt_Refence_Mobile');
//var txt_Opening_Balance = document.getElementById('WucDriver1_WucDriverDetails1_txt_Opening_Balance');

//var ddl_Driver_Category = document.getElementById('WucDriver1_WucDriverDetails1_ddl_Driver_Category');
//var ddl_Religion = document.getElementById('WucDriver1_WucDriverDetails1_ddl_Religion');
//var ddl_License_Issue_City = document.getElementById('WucDriver1_WucDriverDetails1_ddl_License_Issue_City');
//var ddl_License_Category = document.getElementById('WucDriver1_WucDriverDetails1_ddl_License_Category');

//var lbl_Errors = document.getElementById('WucDriver1_WucDriverDetails1_lbl_Errors');

//var Opening_Balance = parseFloat(txt_Opening_Balance.value);
//if (isNaN(Opening_Balance)) Opening_Balance = 0;

//var ATS = false;

//TB_Driver.SelectTabById('zero')

//if (txt_Driver_Code.value == '')
//  {
//  lbl_Errors.innerHTML = "Please Enter Driver Code";
//  txt_Driver_Code.focus();
//  }
//else if (Txt_Driver_Name.value == '')
//  {
//  lbl_Errors.innerHTML = "Please Enter Driver Name";
//  Txt_Driver_Name.focus();
//  }
//else if (ddl_Driver_Category.value == '0' || ddl_Driver_Category.options.length == 0)
//  {
//  lbl_Errors.innerHTML = "Please Select Driver Category";
//  ddl_Driver_Category.focus();
//  }
//else if (ddl_Religion.value == '0' || ddl_Religion.options.length == 0)
//  {
//  lbl_Errors.innerHTML = "Please Select Driver Religion";
//  ddl_Religion.focus();
//  }
//else if (txt_Qualification.value == '')
//  {
//  lbl_Errors.innerHTML = "Please Enter Qualification";
//  txt_Qualification.focus();
//  }
//else if (txt_License_No.value == '')
//  {
//  lbl_Errors.innerHTML = "Please Enter License Number";
//  txt_License_No.focus();
//  }
//else if (ddl_License_Issue_City.value == '0' || ddl_License_Issue_City.options.length == 0)
//  {
//  lbl_Errors.innerHTML = "Please Select License Issue City";
//  ddl_License_Issue_City.focus();
//  }
//else if (ddl_License_Category.value == '0' || ddl_License_Category.options.length == 0)
//  {
//  lbl_Errors.innerHTML = "Please Select License Category";
//  ddl_License_Category.focus();
//  }
////else if (ValidateWucAddress(lbl_Errors) == false)
////  {
////  }
//else if (txt_Native_Address_Line1.value == '')
//  {
//  lbl_Errors.innerHTML = "Please Enter Native Address";
//  txt_Native_Address_Line1.focus();
//  }
//else if (txt_Contact_No.value == '')
//  {
//  lbl_Errors.innerHTML = "Please Enter Driver Contact Number";
//  txt_Contact_No.focus();
//  }
//else if (txt_Refence_Name.value == '')
//  {
//  lbl_Errors.innerHTML = "Please Enter Reference Name";
//  txt_Refence_Name.focus();
//  }
//else if (txt_Refence_Phone.value == '')
//  {
//  lbl_Errors.innerHTML = "Please Enter Reference Phone";
//  txt_Refence_Phone.focus();
//  }
//else if (txt_Refence_Mobile.value == '')
//  {
//  lbl_Errors.innerHTML = "Please Enter Reference Mobile Number";
//  txt_Refence_Mobile.focus();
//  }
//else if (Opening_Balance <=0)
//  {
//  lbl_Errors.innerHTML = "Please Enter Opening Balance Greater Than Zero";
//  txt_Opening_Balance.focus();
//  }
// else
//  ATS = true;
// 
// return ATS;
// }
// 
// 
// 
//function AllowToSaveWUCDriverInsuranceDependent()
//{
//var TB_Driver = WucDriver1_TB_Driver;
//var ddl_Insu_Company = document.getElementById('WucDriver1_WucDriverInsuranceDependent1_ddl_Insu_Company');
//var txt_Insu_Premium = document.getElementById('WucDriver1_WucDriverInsuranceDependent1_txt_Insu_Premium');
//var txt_Sum_assured = document.getElementById('WucDriver1_WucDriverInsuranceDependent1_txt_Sum_assured');
//var txt_Nominee_Name = document.getElementById('WucDriver1_WucDriverInsuranceDependent1_txt_Nominee_Name');
//var ddl_Nominee_Relation = document.getElementById('WucDriver1_WucDriverInsuranceDependent1_ddl_Nominee_Relation');
//var lbl_Errors = document.getElementById('WucDriver1_WucDriverInsuranceDependent1_lbl_Errors');

//var Insu_Premium = parseFloat(txt_Insu_Premium.value);
//if (isNaN(Insu_Premium)) Insu_Premium = 0;

//var Sum_assured = parseFloat(txt_Sum_assured.value);
//if (isNaN(Sum_assured)) Sum_assured = 0;

//var ATS = false;

//TB_Driver.SelectTabById('one')

//if (ddl_Insu_Company.value != "0" && Insu_Premium <= 0)
//  {
//  lbl_Errors.innerHTML = "Please Enter Insurance Premium Greater Than Zero";
//  txt_Insu_Premium.focus();
//  }
//else if (ddl_Insu_Company.value != "0" && Sum_assured <= 0)
//  {
//  lbl_Errors.innerHTML = "Please Enter Sum Assured Greater Than Zero";
//  txt_Sum_assured.focus();
//  }
//else if (ddl_Insu_Company.value != "0" && txt_Nominee_Name.value == "")
//  {
//  lbl_Errors.innerHTML = "Please Enter Nominee Name";
//  txt_Nominee_Name.focus();
//  }
//else if (ddl_Insu_Company.value != "0" && ddl_Nominee_Relation.value == "0")
//  {
//  lbl_Errors.innerHTML = "Please Select Nominee Relation";
//  ddl_Nominee_Relation.focus();
//  }
//else
//  ATS = true;

//return ATS;
//}

//function Allow_To_Save()
//{
//var ATS = false;
//if (Allow_To_Save_Driver_Details() == true)
//  {
//  if (Allow_To_Save_Driver_Insurance_Dependents() == true)
//    {
//    ATS = true;
//    }
//  }
//return ATS;
//}
