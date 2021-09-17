// JScript File
function ValidateUI()
{
   var ATS = false;
   var lbl_Errors = document.getElementById('WucCarrierCategory1_lbl_Errors');
   var txt_Carrier = document.getElementById('WucCarrierCategory1_txt_Carrier_Category_Name');
//   var objResource=new Resource('WucCarrierCategory1_hdf_ResourecString');
  lbl_Errors.innerText ="";
  
  if (Trim(txt_Carrier.value) == '')
  {
      lbl_Errors.innerText = "Please Enter Carrier Category";//objResource.GetMsg("Msg_CarrierCategoryName");
      txt_Carrier.focus();
  }
  else
      ATS = true;

return ATS;
}

//function ValidateUI(lbl_Errors)
//{
//   var ATS = false;
//  
//   var txt_Carrier = document.getElementById('<%=txt_Carrier_Category_Name.ClientID%>');
//  
//  var objResource=new Resource('<%=hdf_ResourecString.ClientID%>');
//  
//  if (Trim(txt_Carrier.value) == '')
//  {
//      lbl_Errors.innerText = objResource.GetMsg("Msg_CarrierCategoryName");
//      txt_Carrier.focus();
//  }
//  else
//      ATS = true;

//return ATS;
//}



