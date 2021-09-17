// JScript File

function validateUI()
{
var ATS = false;


  var lbl_Errors = document.getElementById('WucDepartment1_lbl_Errors');

  var txt_DepartmentName = document.getElementById('WucDepartment1_txt_DepartmentName');
//  var objResource=new Resource('WucDepartment1_hdf_ResourecString');

  lbl_Errors.innerText='';
  

if (txt_DepartmentName.value == '')
  {
  lbl_Errors.innerText = "Please Enter Department Name";// objResource.GetMsg("Msg_DepartmentName");
  txt_DepartmentName.focus();
  }
 
else if (!isNaN(txt_DepartmentName.value.slice(0,1)) )
  {
  lbl_Errors.innerText = "First Character Should be Alphabet in Department Name";
  txt_DepartmentName.focus();
  }
 
else
  ATS = true;
  return ATS;

  
}

