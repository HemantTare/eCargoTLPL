// JScript File

function validateUI()
{
   var ATS = false;

  var lbl_Errors =  document.getElementById('WucTDSHelper1_lbl_Errors');  
  var ddl_TdsledgerAc=document.getElementById('WucTDSHelper1_ddl_TdsledgerAc');
  var ddl_CashBankAc=document.getElementById('WucTDSHelper1_ddl_CashBankAc');
  lbl_Errors.innerText ="";
 
  
  if (ddl_CashBankAc.value == '0')
  {
   lbl_Errors.innerText ="Please Select Cash Bank Account";
   ddl_CashBankAc.focus();   
  }
  else if (ddl_TdsledgerAc.value == '0')
  {
  lbl_Errors.innerText ="Please Select TDS Ledger Account";
  ddl_TdsledgerAc.focus();
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

