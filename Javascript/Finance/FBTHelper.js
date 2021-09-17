// JScript File
function validateUI()
{
   var ATS = false;

  var lbl_Errors =  document.getElementById('WucFBTHelper1_lbl_Errors');  
  var ddl_FBTLedger=document.getElementById('WucFBTHelper1_ddl_FBTLedger');
    var ddl_CashBankAc=document.getElementById('WucFBTHelper1_ddl_CashBankAc');

 
  lbl_Errors.innerText =""; 

  if (ddl_FBTLedger.value == '0')
  {
   lbl_Errors.innerText ="Please Select FBT Ledger";
   ddl_FBTLedger.focus();   
  }
  else if(ddl_CashBankAc.value == '0')
  {
   lbl_Errors.innerText ="Please Select Cash Bank Account";
   ddl_CashBankAc.focus();   
  }
   else 
    {ATS = true;}
    
if (ATS){return true;}
else
{
lbl_Errors.style.visibility = 'visible';
return false;
}

}

