// JScript File

function Allow_To_Save()
{
var ATS = false;
var lbl_Errors = document.getElementById('WucActiveSeries1_lbl_Errors');
var ddl_DocumentType = document.getElementById('WucActiveSeries1_ddl_DocumentType');
var txt_StartNo = document.getElementById('WucActiveSeries1_txt_StartNo');
var txt_EndNo = document.getElementById('WucActiveSeries1_txt_EndNo');
 var ddl_Branch = document.getElementById('WucActiveSeries1_ddl_Branch_txtBoxddl_Branch');

lbl_Errors.innerText='';
 if(ddl_DocumentType.value == '0')
 {
     lbl_Errors.innerText = "Please Select Document ";//objResource.GetMsg("Msg_DocumentType");
     ddl_DocumentType.focus();
 }  
// else if (ddl_Branch.value == '')
// {
//     lbl_Errors.innerText = "Please Enter Branch Name";// objResource.GetMsg("Msg_Branch");
//     ddl_Branch.focus();
// } 
 else 
 ATS = true;

 return ATS;
}
