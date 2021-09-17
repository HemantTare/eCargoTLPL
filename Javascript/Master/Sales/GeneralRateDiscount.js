// JScript File

function validateUI()
{
  var ATS = false;
  var lblErrors = document.getElementById('lblErrors');
  var DDLDiscountBranch = document.getElementById('DDLDiscountBranch_txtBoxDDLDiscountBranch');
  
  lblErrors.innerText ="";
  
  if (DDLDiscountBranch.value == '')
  {
     lblErrors.innerText = "Please Select Discount Branch";//objResource.GetMsg("Msg_ddl_Agency");
     DDLDiscountBranch.focus();
  }
  else
  {
      ATS = true;
  }

return ATS;
}

function ddlRateDiscountForChange()
{
  var lbl_Category = document.getElementById('ddlRateDiscountFor');
  var ddl_ClientCategory = document.getElementById('ddl_ClientCategory');
  var ddlRateDiscountFor = document.getElementById('ddlRateDiscountFor');
  var trParty = document.getElementById('trParty');
  var trPromisedQtyPerMonth = document.getElementById('trPromisedQtyPerMonth');
  
  var lbl_Category = document.getElementById('lbl_Category');
  var ddl_ClientCategory = document.getElementById('ddl_ClientCategory');
 
  if (ddlRateDiscountFor.value == "1")
  {
    trParty.style.display = 'none'; 
    trPromisedQtyPerMonth.style.display = 'none'; 
    trDeliveryArea.style.display = 'none';
    lbl_Category.style.display = 'none'; 
    ddl_ClientCategory.style.display = 'none';  
  }
  else if(ddlRateDiscountFor.value == "3")
  {
    trParty.style.display = 'none'; 
    trPromisedQtyPerMonth.style.display = 'none'; 
    trDeliveryArea.style.display = 'none'; 
    lbl_Category.style.display = 'inline'; 
    ddl_ClientCategory.style.display = 'inline'; 
  }
  else
  {
    trParty.style.display = 'inline'; 
    trPromisedQtyPerMonth.style.display = 'inline'; 
    trDeliveryArea.style.display = 'none';  
    lbl_Category.style.display = 'none'; 
    ddl_ClientCategory.style.display = 'none';  
  }
}  
  
function ddlBrchAsDlyForChange()
{
  var ddlRateDiscountFor = document.getElementById('ddlRateDiscountFor');
  var ddlBranchAs = document.getElementById('ddlBranchAs');
  var trDeliveryArea = document.getElementById('trDeliveryArea'); 


  if (ddlRateDiscountFor.value == "1" && ddlBranchAs.value == "2")
  {
    trDeliveryArea.style.display = 'none';  
  }
  else
  {
    trDeliveryArea.style.display = 'none';  
  }
}

function validateDiscount(DiscountPercent)
{ 
   if (DiscountPercent.value > 100)
   {
     DiscountPercent.value=0;
   }
}