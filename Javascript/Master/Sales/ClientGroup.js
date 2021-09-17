// JScript File

function validateUI()
{
  
var ATS = false;
var txt_ClientGroupName = document.getElementById('WucClientGroup1_txt_ClientGroupName');
var ddl_LedgerGroup= document.getElementById('WucClientGroup1_ddl_LedgerGroup');
var rdb_Use_Exsisting_Ledger= document.getElementById('WucClientGroup1_rbl_Ledgergroup_0');
var ddl_Parent_Ledger= document.getElementById('WucClientGroup1_ddl_ParentGroupName');

var lbl_Errors =document.getElementById('WucClientGroup1_lbl_Errors');

lbl_Errors.innerText='';

     if(txt_ClientGroupName.value == '')
     {
         lbl_Errors.innerText = "Please Enter Client Group Name";
         txt_ClientGroupName.focus();
     }   
     else if(rdb_Use_Exsisting_Ledger.checked == true && parseInt(ddl_LedgerGroup.value) == 0 && parseInt(ddl_Parent_Ledger.value)!=0)
     {
        lbl_Errors.innerText =  "Please Select Ledger Group Name";
        ddl_LedgerGroup.focus();
     }
     else 
     {
        ATS = true;
     }

 return ATS;

}
