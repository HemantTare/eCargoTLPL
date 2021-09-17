// JScript File



function Allow_To_Save()
{
var ATS = false;
var lbl_Errors = document.getElementById('WucDocumentSeries1_lbl_Errors');
var ddl_DocumentType = document.getElementById('WucDocumentSeries1_ddl_DocumentType');
var txt_StartNo = document.getElementById('WucDocumentSeries1_txt_StartNo');
var txt_EndNo = document.getElementById('WucDocumentSeries1_txt_EndNo');
 var ddl_Branch = document.getElementById('WucDocumentSeries1_ddl_Branch_txtBoxddl_Branch');

lbl_Errors.innerText='';

 if(val(ddl_DocumentType.value) == 0)
  {
     lbl_Errors.innerText = "Please Select Document ";
     ddl_DocumentType.focus();
  }
 else if (!Is_Multiple_hierarchy() && ddl_Branch.value == '')
  {
     lbl_Errors.innerText = "Please Select Branch";
     ddl_Branch.focus();
  }
// else if (val(ddl_DocumentType.value) == 6 && LocationId < 0)
//  {
//     lbl_Errors.innerText = "Please Select Location";
//     ddl_Branch.focus();
//  }
 else if(txt_StartNo.value == '')
 {
     lbl_Errors.innerText = "Please Enter Start No";
     txt_StartNo.focus();
 }
  else if(txt_EndNo.value == '')
 {
     lbl_Errors.innerText = "Please Enter End No";
     txt_EndNo.focus();
 }
 else 
 ATS = true;

 return ATS;

}


function Is_Multiple_hierarchy()
{
  var ddl_DocumentType = document.getElementById('WucDocumentSeries1_ddl_DocumentType');
    var val1 = false;
    if (val(ddl_DocumentType.value) == 6 || val(ddl_DocumentType.value) == 7)
        val1 = true;

    return val1;
}
    