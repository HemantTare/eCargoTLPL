// JScript File
function validateUI()
{
var ATS = false;


  var lbl_Errors = document.getElementById('WucNonEmployeeUsers1_lbl_Errors');
  var txt_NonEmpUserName = document.getElementById('WucNonEmployeeUsers1_txt_NonEmpUserName');
  var ddl_Profile=document.getElementById('WucNonEmployeeUsers1_ddl_Profile');

  lbl_Errors.innerText='';
  

if (txt_NonEmpUserName.value == '')
  {
  lbl_Errors.innerText = "Please Enter User Name";
  txt_NonEmpUserName.focus();
  }
 
else if (!isNaN(txt_NonEmpUserName.value.slice(0,1)) )
  {
  lbl_Errors.innerText = "First Character Should be Alphabet in User Name";
  txt_NonEmpUserName.focus();
  }
  else if ( ddl_Profile.value == '' || ddl_Profile.value == '0')
  {
      lbl_Errors.innerText = "Please Select Profile";
  }
 
else
  ATS = true;
  return ATS;

  
}

