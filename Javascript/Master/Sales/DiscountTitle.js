// JScript File
 
 function validateUI()
{
  
var ATS = false;
var txt_DiscountTitleName = document.getElementById('WucDiscountTitle1_txt_DiscountTitleName'); 
var lbl_Errors =document.getElementById('WucDiscountTitle1_lbl_Errors');
//var objResource=new Resource('WucDiscountTitle1_hdf_ResourecString');

lbl_Errors.innerText='';

if(txt_DiscountTitleName.value == '')
 {
     lbl_Errors.innerText = "Please Enter Discount Title Name";//  objResource.GetMsg("Msg_DiscountTitle");
     txt_DiscountTitleName.focus();
 } 
 else 
 ATS = true;

 return ATS;

  
}


