// JScript File

function On_Cash_Amount_Change()
{
   
        var txt_CashAmount = document.getElementById('WucMRDelivery1_WucMRCashChequeDetails1_txt_CashAmount');
        var txt_ChequeAmount = document.getElementById('WucMRDelivery1_WucMRCashChequeDetails1_txt_ChequeAmount');
       
 
        var txt_Totalreceivables = document.getElementById('WucMRDelivery1_txt_TotalReceivables');
        var hdn_CashAmount = document.getElementById('WucMRDelivery1_WucMRCashChequeDetails1_hdn_CashAmount');
       
        var hdn_ChequeAmount = document.getElementById('WucMRDelivery1_WucMRCashChequeDetails1_hdn_ChequeAmount');
       var hdn_ToatlChequeAmount = document.getElementById('WucMRDelivery1_WucMRCashChequeDetails1_hdn_Total_Cheque_Amount');
        
        
        var Amount=0;
        Amount = val(txt_Totalreceivables.value);

       
       
        if ( Amount   < val(txt_CashAmount.value)  )
        {  
            txt_CashAmount.value = Amount;
        }  

        else if (Amount   < val(txt_ChequeAmount.value)  )
        {  
            txt_ChequeAmount.value = Amount ;
        }  


  
        txt_ChequeAmount.value= Amount - val( txt_CashAmount.value );   
      
        hdn_CashAmount.value = val(txt_CashAmount.value); 
        hdn_ChequeAmount.value= val( txt_ChequeAmount.value ); 
        
        if(val(txt_ChequeAmount.value) == 0)
        {
            hdn_ToatlChequeAmount.value = val(txt_ChequeAmount.value);
        }
        
        Cheque_Amount();  
}


function On_Cheque_Amount_Change()
{

       var txt_CashAmount = document.getElementById('WucMRDelivery1_WucMRCashChequeDetails1_txt_CashAmount');
        var txt_ChequeAmount = document.getElementById('WucMRDelivery1_WucMRCashChequeDetails1_txt_ChequeAmount');
       
 
        var txt_Totalreceivables = document.getElementById('WucMRDelivery1_txt_TotalReceivables');
        var hdn_CashAmount = document.getElementById('WucMRDelivery1_WucMRCashChequeDetails1_hdn_CashAmount');
       
        var hdn_ChequeAmount = document.getElementById('WucMRDelivery1_WucMRCashChequeDetails1_hdn_ChequeAmount');
        var hdn_ToatlChequeAmount = document.getElementById('WucMRDelivery1_WucMRCashChequeDetails1_hdn_Total_Cheque_Amount');
        var Amount=0;
        Amount = val(txt_Totalreceivables.value);
       

        if ( Amount   < val(txt_CashAmount.value)  )
        {  
            txt_CashAmount.value = val( Amount);
        }  

        else if (Amount   < val(txt_ChequeAmount.value)  )
        {  
            txt_ChequeAmount.value = Amount ;
        }  


        txt_CashAmount.value =  val( Amount) - val( txt_ChequeAmount.value); 
       
      
        hdn_CashAmount.value = val(txt_CashAmount.value); 
        hdn_ChequeAmount.value= val( txt_ChequeAmount.value ); 
        
        if(val(txt_ChequeAmount.value) == 0)
        {
            hdn_ToatlChequeAmount.value = val(txt_ChequeAmount.value);
        }  
        
         Cheque_Amount();  
}

function Calculate_GrandTotal()
{
var hdn_GC_Sub_Total  =  document.getElementById('WucMRDelivery1_hdn_SubTotal');
var txt_Octroi_Service_Charge_Amount  =  document.getElementById('WucMRDelivery1_txt_Octr_Service_Charges');
var txt_Oct_Form_Ch  =  document.getElementById('WucMRDelivery1_txt_Octr_Form_Charges');
var txt_Dem_Amt  =  document.getElementById('WucMRDelivery1_txt_Demurage_Charges');
var txt_Other_Charges  =  document.getElementById('WucMRDelivery1_hdn_Other_Charges');
var txt_Detention_Charges = document.getElementById('WucMRDelivery1_txt_Detention_Charges');
var txt_GI_Charges = document.getElementById('WucMRDelivery1_txt_GI_Charges');
var txt_Hamali_charges = document.getElementById('WucMRDelivery1_txt_Hamali_Charges');
var txt_Local_Charges = document.getElementById('WucMRDelivery1_txt_Local_Charges');
var txt_Tax_Abate = document.getElementById('WucMRDelivery1_txt_Tax_Abate');
var hdn_Tax_Abate = document.getElementById('WucMRDelivery1_hdn_Tax_Abate');
var txt_Amt_Taxable = document.getElementById('WucMRDelivery1_txt_Amount_Taxable');
var hdn_Amt_Taxable = document.getElementById('WucMRDelivery1_hdn_Amount_Taxable');
var txt_Sub_Tot  =  document.getElementById('WucMRDelivery1_txt_Sub_Total');
var hdn_Sub_Tot =  document.getElementById('WucMRDelivery1_hdn_Delivery_Sub_Total');
var chk_Service_Tax_Payable_By_Consignee = document.getElementById('WucMRDelivery1_chk_Is_Service_Tax_by_Consignee');
var chk_Commodity_Service_Tax_Applicable = document.getElementById('WucMRDelivery1_chk_Is_Service_Tax_App_Commodity');
var chk_Is_Octr_To_Be_Added_In_MR = document.getElementById('WucMRDelivery1_chk_Is_Octr_To_Be_Added_In_MR');
var hdn_Oct_Pay_Type_id = document.getElementById('WucMRDelivery1_hdn_Octr_Pay_Type_ID');
var hdn_Payment_Type_ID = document.getElementById('WucMRDelivery1_hdn_Payment_Type_ID');
var hdn_Booking_Type_ID = document.getElementById('WucMRDelivery1_hdn_Booking_Type_ID');

var hdn_Service_Tax_Per =  document.getElementById('WucMRDelivery1_hdn_Service_Tax_Percent');
var txt_Service_Tax_Amount  =  document.getElementById('WucMRDelivery1_txt_Service_Tax');
var hdn_Service_Tax_Amount =  document.getElementById('WucMRDelivery1_hdn_Service_Tax_Amount');
var hdn_Additional_Charges =  document.getElementById('WucMRDelivery1_hdn_Additional_Charges');
var hdn_Discount_Amount =  document.getElementById('WucMRDelivery1_hdn_Discount_Amount');
var txt_Addition_Charges  =  document.getElementById('WucMRDelivery1_txt_Addition_Charges');
var txt_Discount_Amount  =  document.getElementById('WucMRDelivery1_txt_Discount_Amount');

var txt_Cash_Amount  =  document.getElementById('WucMRDelivery1_WucMRCashChequeDetails1_txt_CashAmount');
var txt_Cheque_Amount  =  document.getElementById('WucMRDelivery1_WucMRCashChequeDetails1_txt_ChequeAmount');
var hdn_CashAmount  =  document.getElementById('WucMRDelivery1_WucMRCashChequeDetails1_hdn_CashAmount');
var hdn_Cheque_Amount  =  document.getElementById('WucMRDelivery1_WucMRCashChequeDetails1_hdn_ChequeAmount');
var hdn_Total_Cheque_Amount  =  document.getElementById('WucMRDelivery1_WucMRCashChequeDetails1_hdn_Total_Cheque_Amount');



var txt_Octrai_Amt  =  document.getElementById('WucMRDelivery1_txt_Octr_Amount');
hdn_Octr_Amt = document.getElementById('WucMRDelivery1_hdn_OctAmount');

var txt_Total_Receivables =  document.getElementById('WucMRDelivery1_txt_TotalReceivables');
var hdn_Total_Receivables =  document.getElementById('WucMRDelivery1_hdn_Delivery_Total_Receivables');

var pay_Type_id = val(hdn_Payment_Type_ID.value);
hdn_Additional_Charges.value = val(txt_Addition_Charges.value);
hdn_Discount_Amount.value = val(txt_Discount_Amount.value);

var GC_Amt = val(hdn_GC_Sub_Total.value);
var Octroi_Service_Charge_Amount = val(txt_Octroi_Service_Charge_Amount.value);
var Oct_Form_Charge = val(txt_Oct_Form_Ch.value);
var Dem_Amt = val(txt_Dem_Amt.value);
var Other_Charges = val(txt_Other_Charges.value);
var Sub_Tot = val(txt_Sub_Tot.value);
var Service_Tax_Percent = val(hdn_Service_Tax_Per.value);
var Service_Tax_Amount = val(txt_Service_Tax_Amount.value);
var Octrai_Amt  = val(txt_Octrai_Amt.value);
var Total_Receivables = val(txt_Total_Receivables.value);
var Detention_Charges = val(txt_Detention_Charges.value);
var GI_Charges = val(txt_GI_Charges.value);
var Hamali_charges = val(txt_Hamali_charges.value);
var Local_Charges = val(txt_Local_Charges.value);
var Additional_Charge = val(hdn_Additional_Charges.value);
var Discount_Amount = val(hdn_Discount_Amount.value);

if (pay_Type_id != 1) GC_Amt = 0;

var subtotal  = val(GC_Amt) + val(Octroi_Service_Charge_Amount) + 
val(Oct_Form_Charge) + val(Dem_Amt) + val(Other_Charges) +
val(Detention_Charges) + val(GI_Charges) +
val(Hamali_charges) + val(Local_Charges) + val(Additional_Charge);

if(val(Discount_Amount) > subtotal)
{
    txt_Discount_Amount.value = 0;
    hdn_Discount_Amount.value = '0';
    Discount_Amount = 0;
}
else
{
    subtotal  = subtotal - val(Discount_Amount);
}

subtotal = Math.round(subtotal);
txt_Sub_Tot.value = subtotal;
hdn_Sub_Tot.value = subtotal;
var Tax_Abate = val(subtotal) * 0.75;
if (val(hdn_Booking_Type_ID.value) == 1 && subtotal < 750) Tax_Abate = 0;
if (val(hdn_Booking_Type_ID.value) == 2 && subtotal < 1500) Tax_Abate = 0;
if (chk_Service_Tax_Payable_By_Consignee.checked == true ) Tax_Abate = 0;
if (chk_Commodity_Service_Tax_Applicable.checked == false) Tax_Abate = 0;
Tax_Abate = Math.round(Tax_Abate);
txt_Tax_Abate.value = Tax_Abate;
hdn_Tax_Abate.value = Tax_Abate;

var Amt_Taxable = parseFloat(subtotal - Tax_Abate);
if (val(hdn_Booking_Type_ID.value) == 1 && subtotal < 750) Amt_Taxable = 0;
if (val(hdn_Booking_Type_ID.value) == 2 && subtotal < 1500) Amt_Taxable = 0;
if (chk_Service_Tax_Payable_By_Consignee.checked == true ) Amt_Taxable = 0;
if (chk_Commodity_Service_Tax_Applicable.checked == false) Amt_Taxable = 0;
Amt_Taxable = Math.round(Amt_Taxable);
txt_Amt_Taxable.value = Amt_Taxable;
hdn_Amt_Taxable.value = Amt_Taxable; 

var servicetax = (Service_Tax_Percent/100) * Amt_Taxable;
if (val(hdn_Booking_Type_ID.value) == 1 && subtotal < 750) servicetax = 0;
if (val(hdn_Booking_Type_ID.value) == 2 && subtotal < 1500) servicetax = 0;
if (chk_Service_Tax_Payable_By_Consignee.checked == true) servicetax = 0;
if (chk_Commodity_Service_Tax_Applicable.checked == false) servicetax = 0;
servicetax= Math.round(servicetax)
txt_Service_Tax_Amount.value = servicetax;
hdn_Service_Tax_Amount.value = servicetax;

if (chk_Is_Octr_To_Be_Added_In_MR.checked == false) Octrai_Amt = 0;

var grandtotal
if (chk_Service_Tax_Payable_By_Consignee.checked == true) //service tax by client
  grandtotal =  val(subtotal) + val(Octrai_Amt) ;
else
  grandtotal =  val(subtotal) + val(servicetax) + val(Octrai_Amt) ;

grandtotal = Math.round(grandtotal);

txt_Total_Receivables.value = grandtotal;
hdn_Total_Receivables.value  = grandtotal;

txt_Cash_Amount.value = grandtotal;
txt_Cheque_Amount.value = 0;
hdn_CashAmount.value = 0;
hdn_Cheque_Amount.value = 0;
hdn_Total_Cheque_Amount.value = 0;

Cheque_Amount();
}
                
                
function Allow_To_Save()
{
var lbl_Error = document.getElementById('WucMRDelivery1_lbl_Errors');
var hdn_Sub_Tot =  document.getElementById('WucMRDelivery1_hdn_Delivery_Sub_Total');
var hdn_Tax_Abate = document.getElementById('WucMRDelivery1_hdn_Tax_Abate');
var hdn_Amt_Taxable = document.getElementById('WucMRDelivery1_hdn_Amount_Taxable');
var hdn_Service_Tax_Amount =  document.getElementById('WucMRDelivery1_hdn_Service_Tax_Amount');
var hdn_Octr_Amt = document.getElementById('WucMRDelivery1_hdn_OctAmount');
var hdn_Total_Receivables =  document.getElementById('WucMRDelivery1_hdn_Delivery_Total_Receivables');
var hdn_Additional_Charges =  document.getElementById('WucMRDelivery1_hdn_Additional_Charges');
var hdn_Discount_Amount =  document.getElementById('WucMRDelivery1_hdn_Discount_Amount');
var txt_Addition_Charges_Remarks  =  document.getElementById('WucMRDelivery1_txt_Add_Charge_remark');
var txt_Discount_Amount_Remarks  =  document.getElementById('WucMRDelivery1_txt_discount_remark');


if(validateGeneralDetails(lbl_Error) == false)
{ return false;}

else if (val(hdn_Additional_Charges.value) > 0 && Trim(txt_Addition_Charges_Remarks.value) == '')
{
lbl_Error.innerText = 'Please Enter Additional Charge Remark';
return false;
}
else if (val(hdn_Discount_Amount.value) > 0 && Trim(txt_Discount_Amount_Remarks.value) == '')
{
lbl_Error.innerText = 'Please Enter Discount Remark';
return false;
}
else if (val(hdn_Sub_Tot.value) < 0)
{
lbl_Error.innerText = 'Sub Total Cannot Be Negative';
return false;
}
else if(val(hdn_Tax_Abate.value) < 0)
{
lbl_Error.innerText = 'Tax Abate cannot be Negative';
return false;
}
else if (val(hdn_Amt_Taxable.value) < 0)
{
lbl_Error.innerText = 'Amount Taxable Cannot be Negative';
return false;
}
else if (val(hdn_Service_Tax_Amount.value) < 0)
{
lbl_Error.innerText = 'Service Tax Cannot be Negative';
return false;
}
else if (val(hdn_Octr_Amt.value) < 0)
{
lbl_Error.innerText = 'Octroi Amount Cannot be Negative';
return false;
}
else if (val(hdn_Total_Receivables.value) < 0)
{
lbl_Error.innerText = 'Total Receivable Cannot be Negative';
return false;
}

if(validateWUCCheque(lbl_Error) == false)
{return false;}

return true;
}


function Validate_Discount(txt_Box)
{

var Element = txt_Box;

var Std_Oct_Form_Chg  =  document.getElementById('WucMRDelivery1_hdn_Std_Octroi_Form_Charges');
var Std_Oct_Service_Chg  =  document.getElementById('WucMRDelivery1_hdn_Std_Octroi_Service_Charges');
var Std_GI_Chg =  document.getElementById('WucMRDelivery1_hdn_Std_GI_Charges');
var Std_Hamali_Chg  =  document.getElementById('WucMRDelivery1_hdn_Std_Hamali_Charges');
var Std_Demurage_Chg  =  document.getElementById('WucMRDelivery1_hdn_Std_Demurage_Charges');

var hdn_Oct_Form_Chg_Discount  =  document.getElementById('WucMRDelivery1_hdn_Oct_Form_Chg_Discount');
var hdn_Oct_Service_Chg_Discount  =  document.getElementById('WucMRDelivery1_hdn_Oct_Service_Chg_Discount');
var hdn_GI_Chg_Discount  =  document.getElementById('WucMRDelivery1_hdn_GI_Chg_Discount');
var hdn_Hamali_Chg_Discount  =  document.getElementById('WucMRDelivery1_hdn_Hamali_Chg_Discount');
var hdn_Demurage_Chg_Discount  =  document.getElementById('WucMRDelivery1_hdn_Demurage_Chg_Discount');

if(Element.id == 'WucMRDelivery1_txt_Octr_Form_Charges')
{
    var Oct_Form_Chg = val(Std_Oct_Form_Chg.value);
    var Oct_Form_Chg_Discount = val(hdn_Oct_Form_Chg_Discount.value);
    var Elm_Value = val(Element.value);
    Oct_Form_Chg = Oct_Form_Chg - ((Oct_Form_Chg_Discount/100) * Oct_Form_Chg);
    
    if(val(Elm_Value) < val(Oct_Form_Chg))
    {
       Element.value = val(Std_Oct_Form_Chg.value);
    }
     
} 
   
if(Element.id == 'WucMRDelivery1_txt_Octr_Service_Charges') 
{
    var Oct_Ser_Chg = val(Std_Oct_Service_Chg.value);
    var Oct_Service_Chg_Discount = val(hdn_Oct_Service_Chg_Discount.value);
    var Elm_Value = val(Element.value);
    Oct_Ser_Chg = Oct_Ser_Chg - ((Oct_Service_Chg_Discount/100) * Oct_Ser_Chg);
    
    if(val(Elm_Value) < val(Oct_Ser_Chg))
    {
       Element.value = val(Std_Oct_Service_Chg.value);
    }
    
}

if(Element.id == 'WucMRDelivery1_txt_GI_Charges') 
{
    var GI_Chg = val(Std_GI_Chg.value);
    var GI_Chg_Discount = val(hdn_GI_Chg_Discount.value);
    var Elm_Value = val(Element.value);
    GI_Chg = GI_Chg - ((GI_Chg_Discount/100) * GI_Chg);
    
    if(val(Elm_Value) < val(GI_Chg))
    {
       Element.value = val(Std_GI_Chg.value);
    }
}

if(Element.id == 'WucMRDelivery1_txt_Hamali_Charges') 
{
    var Hamali_Chg = val(Std_Hamali_Chg.value);
    var Hamali_Chg_Discount = val(hdn_Hamali_Chg_Discount.value);
    var Elm_Value = val(Element.value);
    Hamali_Chg = Hamali_Chg - ((Hamali_Chg_Discount/100) * Hamali_Chg);
    
    if(val(Elm_Value) < val(Hamali_Chg))
    {
       Element.value = val(Std_Hamali_Chg.value);
    }
}

if(Element.id == 'WucMRDelivery1_txt_Demurage_Charges')
{
    var Dem_Chg = val(Std_Demurage_Chg.value);
    var Demurage_Chg_Discount = val(hdn_Demurage_Chg_Discount.value);
    var Elm_Value = val(Element.value);
    Dem_Chg = Dem_Chg - ((Demurage_Chg_Discount/100) * Dem_Chg);
    
    if(val(Elm_Value) < val(Dem_Chg))
    {
       Element.value = val(Std_Demurage_Chg.value);
    }
} 

}