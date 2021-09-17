// JScript File

function validateUI()
{
  var ATS = false;
  var lblErrors = document.getElementById('lblErrors');
  var DDLDiscountBranch = document.getElementById('DDLDiscountBranch_txtBoxDDLDiscountBranch');
  var txtDiscountPercent = document.getElementById('txtDiscountPercent');
  var hdn_DiscountPercent = document.getElementById('hdn_DiscountPercent');
  
  lblErrors.innerText ="";
  
  if (DDLDiscountBranch.value == '')
  {
     lblErrors.innerText = "Please Select Discount Branch"; 
     DDLDiscountBranch.focus();
  }
  else if (val(hdn_DiscountPercent.value) <= 0)
  {
     lblErrors.innerText = "Please Enter Discount Percentage"; 
     txtDiscountPercent.focus();
  }
  else
  {
      ATS = true;
  }

return ATS;
}
  
 
function validateDiscount(DiscountPercent)
{ 
   if (DiscountPercent.value > 999)
   {
     DiscountPercent.value=0;
   }
}