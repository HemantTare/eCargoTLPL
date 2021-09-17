// JScript File

function DisableControlOnChecked()
{
 var ddl_CustomerSatisfied=document.getElementById("WucTicketResolution1_ddl_CustomerSatisfied");
 var txt_Reason=document.getElementById("WucTicketResolution1_txt_Reason");
 var lbl_Reason=document.getElementById("WucTicketResolution1_lbl_Reason");
 alert('a')
 
 if (ddl_CustomerSatisfied.SelectedValue =="0")
 {
   txt_Reason.style.visibility='hidden';     
   lbl_Reason.style.visibility='hidden';
 }
 
 else
 {
   txt_Reason.style.visibility='visible';     
   lbl_Reason.style.visibility='Visible';
 }
}