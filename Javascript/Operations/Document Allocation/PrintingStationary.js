// JScript File

function Allow_To_Save()
{
var ATS = false;
var lbl_Errors = document.getElementById('WucPrintingStationary1_lbl_Errors');
var ddl_DocumentType = document.getElementById('WucPrintingStationary1_ddl_DocumentType');
var txt_StartNo = document.getElementById('WucPrintingStationary1_txt_StartNo');
var txt_EndNo = document.getElementById('WucPrintingStationary1_txt_EndNo');

lbl_Errors.innerText='';
 
 if(ddl_DocumentType.value == '0')
 {
     lbl_Errors.innerText = "Please Select Document Type";//objResource.GetMsg("Msg_DocumentType");
     ddl_DocumentType.focus();
 }
 else if(txt_StartNo.value == '')
 {
     lbl_Errors.innerText = "Please Enter Start No";// objResource.GetMsg("Msg_StartNo");
     txt_StartNo.focus();
 }
  else if(txt_EndNo.value == '')
 {
     lbl_Errors.innerText = "Please Enter End No";//objResource.GetMsg("Msg_EndNo");
     txt_EndNo.focus();
 }
 else 
 ATS = true;

 return ATS;

}
