// JScript File

function ValidateUI()
{
var ATS = false;


  var lbl_Errors = document.getElementById('WucContainerType1_lbl_Errors');

  var txt_ContainerType = document.getElementById('WucContainerType1_txt_ContainerType');
//  var objResource=new Resource('WucDepartment1_hdf_ResourecString');

  lbl_Errors.innerText='';
  

if (txt_ContainerType.value == '')
  {
  lbl_Errors.innerText = "Please Enter Container Type Name";
  txt_ContainerType.focus();
  }
  
 else
 
  ATS = true;
  return ATS;

  
}