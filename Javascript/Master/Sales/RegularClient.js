// JScript File
function validateUI()
{
  
var ATS = false;
var txt_RegularClientName = document.getElementById('WucRegularClient1_txt_RegularClientName');
var txt_ContactPerson= document.getElementById('WucRegularClient1_txt_ContactPerson');
var txt_CstNo = document.getElementById('WucRegularClient1_txt_CstNo');
var txt_ServiceTaxNo = document.getElementById('WucRegularClient1_txt_ServiceTaxNo');
var ddl_Name=document.getElementById('WucRegularClient1_WucAddress1_ddl_City_txtBoxddl_City');
var hdnGSTStateCode=document.getElementById('WucRegularClient1_WucAddress1_hdnGSTStateCode');
var lbl_Errors =document.getElementById('WucRegularClient1_lbl_Errors');
var objResource=new Resource('WucRegularClient1_hdf_ResourecString');

var Chk_IsServiceTaxPayable = document.getElementById('WucRegularClient1_Chk_IsServiceTaxPayable');
var Chk_Is_Casual_Taxable = document.getElementById('WucRegularClient1_chk_Is_Casual_Taxable').checked;
var GSTStateCode = hdnGSTStateCode.value;
lbl_Errors.innerText='';

if(txt_RegularClientName.value == '')
 {
     lbl_Errors.innerText = objResource.GetMsg("Msg_ClientName");
     txt_RegularClientName.focus();
 }
 else if(txt_ContactPerson.value == '' && control_is_mandatory(txt_ContactPerson) == true)
 {
     lbl_Errors.innerText ="Please Enter Contact Person";
     txt_ContactPerson.focus();
 }
 else if (ValidateWucAddress(lbl_Errors) == false)
  {
  }
 
// else if (txt_CstNo.value == '' && control_is_mandatory(txt_CstNo) == true)
// {
//    lbl_Errors.innerText="Please Enter CST /TIN No";
//    txt_CstNo.focus();
  // }

  else if (Chk_IsServiceTaxPayable.checked == true && txt_CstNo.value.length != 15) {
      lbl_Errors.innerText = "Please Enter 15 Digits GST No.";
      txt_CstNo.focus();
  }
  else if (Chk_IsServiceTaxPayable.checked == true && ValidateGST(txt_CstNo, GSTStateCode,Chk_Is_Casual_Taxable,lbl_Errors) == false) {
      //lbl_Errors.innerText = "Please Enter Valid GST No.";
      txt_CstNo.focus();
  }
  else if (txt_CstNo.value.trim().length > 0 && ValidateGST(txt_CstNo, GSTStateCode,Chk_Is_Casual_Taxable,lbl_Errors) == false) {
      //lbl_Errors.innerText = "Please Enter Valid GST No.";
      txt_CstNo.focus();
  }
// else if (txt_ServiceTaxNo.value == '' && control_is_mandatory(txt_ServiceTaxNo) == true)
// {
//    lbl_Errors.innerText = "Please Enter Service Tax No";
//    txt_ServiceTaxNo.focus();
// }
  
  else 
 ATS = true;

 return ATS;

  
}


function Is_Contractual_Client()
{    
    var hdn_Mode = document.getElementById('WucRegularClient1_hdn_Mode'); 
    var hdn_ContractualClientId = document.getElementById('WucRegularClient1_hdn_ContractualClientId'); 
    
    var lbl_Errors  = document.getElementById('WucRegularClient1_lbl_Errors'); 
    
    var Enable ;    
    
    Enable = true;
    
    lbl_Errors.innerText ="";
        
    if (val( hdn_Mode.value ) == 4 || val(hdn_ContractualClientId.value) > 0  )
    {           
        for(i = 0; i < document.forms[0].elements.length; i++) 
        {        
            elm = document.forms[0].elements[i];

            if(elm.id!='')
            {
                var elm_id = document.getElementById(elm.id);
                var elm_name = elm.name;
                var arr = elm_name.split("$");                                     
                
                if (elm.type != 'lable')
                {                    
                   elm.disabled = Enable;
                }   
                else
                {
                    //arr[1] != 'dg_Commodity' && 
                }                            
            }       
        }       
  
       
    }  
    
    
    if (val(hdn_ContractualClientId.value) > 0  )
    {           
        lbl_Errors.innerText = "Converted In To Client Master.";//objResource.GetMsg("Msg_ServiceTaxNo");
    }
}


function validateUIForRegularClientGC()
{
var ATS = false;
var txtClientName = document.getElementById('txtClientName');
var txtMobileNo= document.getElementById('txtMobileNo');
var ChkIsServiceTaxPayableByClient= document.getElementById('ChkIsServiceTaxPayableByClient');
var txtCSTNo= document.getElementById('txtCSTNo');
var hdnGSTStateCode=document.getElementById('hdnGSTStateCode');
var Chk_Is_Casual_Taxable = false;

var GSTStateCode = hdnGSTStateCode.value;

var lblErrors =document.getElementById('lblErrors');
lblErrors.innerText='';

if(txtClientName.value == '')
 {
     lblErrors.innerText = "Please Enter Client Name";
     txtClientName.focus();
 }
 else if(txtMobileNo.value == '')
 {
     lblErrors.innerText = "Please Enter Mobile No";
     txtMobileNo.focus();
 }
 else if(ChkIsServiceTaxPayableByClient.checked == true && txtCSTNo.value.length != 15)
 {
     lblErrors.innerText = "Please Enter 15 Digits GST No";
     txtCSTNo.focus();
 }
 else if (ChkIsServiceTaxPayableByClient.checked == true && ValidateGST(txtCSTNo, GSTStateCode,Chk_Is_Casual_Taxable,lblErrors) == false) 
 {
     //lblErrors.innerText = "Please Enter Valid GST No.";
     txtCSTNo.focus();
 }
 else if (txtCSTNo.value.trim().length > 0 && ValidateGST(txtCSTNo, GSTStateCode,Chk_Is_Casual_Taxable,lblErrors) == false) 
 {
     //lblErrors.innerText = "Please Enter Valid GST No.";
     txtCSTNo.focus();
 }
else 
  ATS = true;

 return ATS;
}
