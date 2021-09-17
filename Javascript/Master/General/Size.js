// JScript File

function validateUI()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('lbl_Errors');
   var txtSizeName = document.getElementById('txtSizeName');
   var txtApproxChargeWeight = document.getElementById('txtApproxChargeWeight');
   var ddlFunction = document.getElementById('ddlFunction');
   var txtFactorAmount = document.getElementById('txtFactorAmount');
  
  lbl_Errors.innerText ="";
  
  if (Trim(txtSizeName.value) == '')
  {
    lbl_Errors.innerText = "Please Enter Size Name";
    txtSizeName.focus();
  }
  else if (Trim(txtApproxChargeWeight.value) == '')
  {
    lbl_Errors.innerText = "Please Enter Approx. charge Weight";
    txtApproxChargeWeight.focus();
  }
  else if (parseFloat(txtApproxChargeWeight.value) <= 0)
  {
    lbl_Errors.innerText = "Approx. charge Weight should be greater than zero";
    txtApproxChargeWeight.focus();
  }
  else if ( ddlFunction.value == '' || ddlFunction.value == '0')
  {
    lbl_Errors.innerText = "Please Select Function";
    ddlFunction.focus();
  }
  else if (Trim(txtFactorAmount.value) == '')
  {
    lbl_Errors.innerText = "Please Enter Factor/Amount";
    txtFactorAmount.focus();
  }
    else
  {
      ATS = true;
  }
    
return ATS;
}