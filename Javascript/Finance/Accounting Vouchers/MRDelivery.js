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
//Start added on 10-12-13
 
var TotalLRAmt_txtTotalAmount  =  document.getElementById('WucMRDelivery1_txt_Total_Amount');
//End added on 10-12-13

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
var chk_Is_Oct_Recovered_From_Consignee = document.getElementById('WucMRDelivery1_chk_Is_Oct_Recovered_From_Consignee');
var chk_Is_Octr = document.getElementById('WucMRDelivery1_chk_Is_Octr');
var hdn_Oct_Pay_Type_id = document.getElementById('WucMRDelivery1_hdn_Octr_Pay_Type_ID');
var hdn_Payment_Type_ID = document.getElementById('WucMRDelivery1_hdn_Payment_Type_ID');
var hdn_Booking_Type_ID = document.getElementById('WucMRDelivery1_hdn_Booking_Type_ID');
var txt_Advance_Amount = document.getElementById('WucMRDelivery1_txt_Advance_Amount');

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

var rbtnCreditMemoFor_Frieght=document.getElementById('WucMRDelivery1_rbtn_CreditMemoFor_0');
var rbtnCreditMemoFor_Octroi=document.getElementById('WucMRDelivery1_rbtn_CreditMemoFor_1');
var rbtnCreditMemoFor_Both=document.getElementById('WucMRDelivery1_rbtn_CreditMemoFor_2');

var Doc_Id=document.getElementById('WucMRDelivery1_hdn_DocumentID');

var txt_Octrai_Amt  =  document.getElementById('WucMRDelivery1_txt_Octr_Amount');
hdn_Octr_Amt = document.getElementById('WucMRDelivery1_hdn_OctAmount');

var txt_Total_Receivables =  document.getElementById('WucMRDelivery1_txt_TotalReceivables');
var hdn_Total_Receivables =  document.getElementById('WucMRDelivery1_hdn_Delivery_Total_Receivables');
var hdn_Service_Tax_On_Advance =  document.getElementById('WucMRDelivery1_hdn_Service_Tax_On_Advance');
var hdn_Service_Pay_By_ID =  document.getElementById('WucMRDelivery1_hdn_Service_Pay_By_ID');

var txt_DeliveryCommission = document.getElementById('WucMRDelivery1_txt_DeliveryCommission');

var pay_Type_id = val(hdn_Payment_Type_ID.value);  

hdn_Additional_Charges.value = val(txt_Addition_Charges.value);
hdn_Discount_Amount.value = val(txt_Discount_Amount.value);

var GC_Amt = val(hdn_GC_Sub_Total.value);

//Start added on 10-12-13
var TotalLRAmt_txtTotalAmount = val(TotalLRAmt_txtTotalAmount.value);
//alert('GC_Amt ' + GC_Amt); 
//alert('TotalLRAmt_txtTotalAmount ' + TotalLRAmt_txtTotalAmount);
//End added on 10-12-13

//Start added on 23-12-13
var hdn_RoundOff = document.getElementById('WucMRDelivery1_hdn_RoundOff');
var lbl_RoundOff = document.getElementById('WucMRDelivery1_lbl_RoundOff');
//End added on 23-12-13

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
var Advance_Amount = val(txt_Advance_Amount.value);
var Service_Tax_On_Advance = val(hdn_Service_Tax_On_Advance.value);
var Service_Pay_By_ID = val(hdn_Service_Pay_By_ID.value);
var DeliveryCommission = val(txt_DeliveryCommission.value);

if (pay_Type_id != 1) GC_Amt = 0;
//Start added on 10-12-13

if (pay_Type_id == 1) GC_Amt = TotalLRAmt_txtTotalAmount;

//End added on 10-12-13

var subtotal  = val(GC_Amt) + val(Octroi_Service_Charge_Amount) + 
val(Oct_Form_Charge) + val(Dem_Amt) + val(Other_Charges) +
val(Detention_Charges) + val(GI_Charges) +
val(Hamali_charges) + val(Local_Charges) + val(Additional_Charge) + val(DeliveryCommission);
 

if(pay_Type_id == 1) subtotal = subtotal - Advance_Amount;   

if(Doc_Id.value == '8' && rbtnCreditMemoFor_Frieght.checked == true)
{
   subtotal  = val(GC_Amt) + val(Dem_Amt) + val(Other_Charges) +
    val(Detention_Charges) + val(GI_Charges) +
    val(Hamali_charges) + val(Local_Charges) + val(Additional_Charge) + val(DeliveryCommission); 
}
else if(Doc_Id.value == '8' && rbtnCreditMemoFor_Octroi.checked == true)
{
   subtotal  = val(Octroi_Service_Charge_Amount) + val(Oct_Form_Charge); 
}

 subtotal = val(subtotal);

if(val(Discount_Amount) > subtotal)
{
    txt_Discount_Amount.value = 0;
    hdn_Discount_Amount.value = '0';
    Discount_Amount = 0;
}
else
{
    subtotal  = val(subtotal) - val(Discount_Amount);
}

subtotal = val(subtotal);

txt_Sub_Tot.value = subtotal;
hdn_Sub_Tot.value = subtotal; 
// alert(subtotal);

//Start added on 10-12-13
if (pay_Type_id == 1) subtotal = val(subtotal) - val(TotalLRAmt_txtTotalAmount);
// alert(subtotal);
//End added on 10-12-13

var Tax_Abate = val(subtotal) * 0.75; 
if (val(hdn_Booking_Type_ID.value) == 1 && subtotal < 750) Tax_Abate = 0;
if (val(hdn_Booking_Type_ID.value) == 2 && subtotal < 1500) Tax_Abate = 0;
if (chk_Service_Tax_Payable_By_Consignee.checked == true ) Tax_Abate = 0;
if (chk_Commodity_Service_Tax_Applicable.checked == false) Tax_Abate = 0;
Tax_Abate = Math.round(Tax_Abate);
txt_Tax_Abate.value = Tax_Abate;
hdn_Tax_Abate.value = Tax_Abate; 

//alert('Tax_Abate ' + Tax_Abate);
var Amt_Taxable = parseFloat(subtotal - Tax_Abate);
if (val(hdn_Booking_Type_ID.value) == 1 && subtotal < 750) Amt_Taxable = 0;
if (val(hdn_Booking_Type_ID.value) == 2 && subtotal < 1500) Amt_Taxable = 0;
if (chk_Service_Tax_Payable_By_Consignee.checked == true ) Amt_Taxable = 0;
if (chk_Commodity_Service_Tax_Applicable.checked == false) Amt_Taxable = 0;
Amt_Taxable = Math.round(Amt_Taxable);
txt_Amt_Taxable.value = Amt_Taxable;
hdn_Amt_Taxable.value = Amt_Taxable;
//alert('Amt_Taxable ' + Amt_Taxable);

var servicetax = (Service_Tax_Percent/100) * Amt_Taxable;
if (val(hdn_Booking_Type_ID.value) == 1 && subtotal < 750) servicetax = 0;
if (val(hdn_Booking_Type_ID.value) == 2 && subtotal < 1500) servicetax = 0;
if (chk_Service_Tax_Payable_By_Consignee.checked == true) servicetax = 0;
if (chk_Commodity_Service_Tax_Applicable.checked == false) servicetax = 0;
servicetax= Math.round(servicetax);
txt_Service_Tax_Amount.value = servicetax;
hdn_Service_Tax_Amount.value = servicetax; 
//alert('servicetax ' + servicetax);

if (pay_Type_id == 1) subtotal = val(subtotal) + val(TotalLRAmt_txtTotalAmount);


if (chk_Is_Octr_To_Be_Added_In_MR.checked == false) Octrai_Amt = 0;
if(chk_Is_Oct_Recovered_From_Consignee.checked == false) Octrai_Amt = 0;
if(val(hdn_Oct_Pay_Type_id.value) != 3) Octrai_Amt = 0;
if(chk_Is_Octr.checked == false) Octrai_Amt = 0;

if(Doc_Id.value == '8' && rbtnCreditMemoFor_Frieght.checked == true) Octrai_Amt = 0;
 

var grandtotal
if (chk_Service_Tax_Payable_By_Consignee.checked == true && chk_Is_Oct_Recovered_From_Consignee.checked == true) //service tax by client
  grandtotal =  val(subtotal) + val(Octrai_Amt) ;
else
  grandtotal =  val(subtotal) + val(servicetax) + val(Octrai_Amt) ;

if(Service_Pay_By_ID == 3)grandtotal = grandtotal + Service_Tax_On_Advance;

//Start added on 23-12-13
//Committed on 23-12-2013

    //grandtotal = val(grandtotal);

//Committed on 23-12-2013
 
lbl_RoundOff.innerHTML = "0";
hdn_RoundOff.value = "0";

var RoundOff = val(grandtotal) % 10;
RoundOff = Math.round(val(RoundOff));
if (val(RoundOff)!= 0 && val(RoundOff) > 4)
{
    grandtotal = val(grandtotal) + val(10 - RoundOff);
    lbl_RoundOff.innerHTML = val(10 - RoundOff);
}
else if (val(RoundOff)!= 0)
{
    grandtotal = val(grandtotal) - val(RoundOff);
    lbl_RoundOff.innerHTML = "-" + val(RoundOff);
}
hdn_RoundOff.value = lbl_RoundOff.innerHTML;
 
//End added on 23-12-13

if(txt_Total_Receivables.value == '' && val(hdn_Total_Receivables.value) > 0)
{
    txt_Total_Receivables.value = val(hdn_Total_Receivables.value);
}

if(val(hdn_Total_Receivables.value) != grandtotal)
{
txt_Total_Receivables.value = grandtotal;
hdn_Total_Receivables.value  = grandtotal;

if(grandtotal > (val(txt_Cash_Amount.value) + val(txt_Cheque_Amount.value)))
{
    txt_Cash_Amount.value = (grandtotal - (val(txt_Cash_Amount.value) + val(txt_Cheque_Amount.value))) + val(txt_Cash_Amount.value);
    hdn_CashAmount.value = val(txt_Cash_Amount.value);
}
else if (grandtotal == val(txt_Cheque_Amount.value))
{
    txt_Cash_Amount.value = 0;
    hdn_CashAmount.value = 0;
}
else if(grandtotal < (val(txt_Cash_Amount.value) + val(txt_Cheque_Amount.value)))
{
    if(val(txt_Cash_Amount.value) > ((val(txt_Cash_Amount.value) + val(txt_Cheque_Amount.value)) - grandtotal))
    {
        txt_Cash_Amount.value = val(txt_Cash_Amount.value) - ((val(txt_Cash_Amount.value) + val(txt_Cheque_Amount.value)) - grandtotal);
        hdn_CashAmount.value = val(txt_Cash_Amount.value)
    }

    if(val(txt_Cash_Amount.value) < ((val(txt_Cash_Amount.value) + val(txt_Cheque_Amount.value)) - grandtotal))
    {
        txt_Cheque_Amount.value = 0;
        txt_Cash_Amount.value = grandtotal;
        hdn_CashAmount.value = grandtotal;
    }
    
}

if (val(txt_Cheque_Amount.value) <= 0)
{
    txt_Cheque_Amount.value = 0;
    hdn_Cheque_Amount.value = 0;
    hdn_Total_Cheque_Amount.value = 0;
}


Cheque_Amount();
}
}


function Hide_Octr_Controls()
{
    var chk_Is_Octr = document.getElementById('WucMRDelivery1_chk_Is_Octr');
    var chk_Is_Service_Tax_App_Commodity = document.getElementById('WucMRDelivery1_chk_Is_Service_Tax_App_Commodity');
    var chk_Is_Service_Tax_by_Consignee = document.getElementById('WucMRDelivery1_chk_Is_Service_Tax_by_Consignee');
    var chk_Is_Octr_To_Be_Added_In_MR = document.getElementById('WucMRDelivery1_chk_Is_Octr_To_Be_Added_In_MR');
    var chk_Is_Oct_Recovered_From_Consignee = document.getElementById('WucMRDelivery1_chk_Is_Oct_Recovered_From_Consignee')
    var chk_Is_Mr_DlyFirstTime = document.getElementById('WucMRDelivery1_WucMRGeneralDetails1_chk_Is_Mr_DlyFirstTime');
    var CreditMemoFor_1=document.getElementById('WucMRDelivery1_rbtn_CreditMemoFor_1');
    var Doc_Id=document.getElementById('WucMRDelivery1_hdn_DocumentID');
    var tr_Octr = document.getElementById('WucMRDelivery1_tr_Octr');
    var td_txt_OctrFormType = document.getElementById('WucMRDelivery1_td_txt_OctrFormType');
    var td_lbl_Octr_Form_Type = document.getElementById('WucMRDelivery1_td_lbl_Octr_Form_Type');

    var tr_Advance_Amount = document.getElementById('WucMRDelivery1_tr_Advance_Amount');
    var hdn_Payment_Type_ID = document.getElementById('WucMRDelivery1_hdn_Payment_Type_ID');
    
    
    chk_Is_Octr.style.display = 'none';
    chk_Is_Service_Tax_App_Commodity.style.display = 'none';
    chk_Is_Service_Tax_by_Consignee.style.display = 'none';
    chk_Is_Oct_Recovered_From_Consignee.style.display = 'none';
    chk_Is_Octr_To_Be_Added_In_MR.style.display = 'none';

    tr_Advance_Amount.style.display = 'none';
    
    if  (chk_Is_Octr.checked == true)
        {
        tr_Octr.style.display = 'inline';
        td_txt_OctrFormType.style.display = 'inline';
        td_lbl_Octr_Form_Type.style.display = 'inline';
        }
    else
        {
        tr_Octr.style.display = 'none';
        td_txt_OctrFormType.style.display = 'none';
        td_lbl_Octr_Form_Type.style.display = 'none';
        }
        
     if(val(hdn_Payment_Type_ID.value) == 1)
     {
       tr_Advance_Amount.style.display = 'inline';
     }
    
}


function Disable_Control_On_Octroi()
{

var txt_Oct_Form_Ch  =  document.getElementById('WucMRDelivery1_txt_Octr_Form_Charges');
var txt_Octroi_Service_Charge_Amount  =  document.getElementById('WucMRDelivery1_txt_Octr_Service_Charges');
var txt_GI_Charges = document.getElementById('WucMRDelivery1_txt_GI_Charges');
var txt_Detention_Charges = document.getElementById('WucMRDelivery1_txt_Detention_Charges');
var txt_Hamali_charges = document.getElementById('WucMRDelivery1_txt_Hamali_Charges');
var txt_Local_Charges = document.getElementById('WucMRDelivery1_txt_Local_Charges');
var txt_Dem_Amt  =  document.getElementById('WucMRDelivery1_txt_Demurage_Charges');
var txt_Addition_Charges  =  document.getElementById('WucMRDelivery1_txt_Addition_Charges');
var txt_Discount_Amount  =  document.getElementById('WucMRDelivery1_txt_Discount_Amount');
var rbtnCreditMemoFor_Freight=document.getElementById('WucMRDelivery1_rbtn_CreditMemoFor_0');
var rbtnCreditMemoFor_Octroi=document.getElementById('WucMRDelivery1_rbtn_CreditMemoFor_1');
var rbtnCreditMemoFor_Both=document.getElementById('WucMRDelivery1_rbtn_CreditMemoFor_2');

var hdn_Sub_Tot =  document.getElementById('WucMRDelivery1_hdn_Delivery_Sub_Total');
var hdn_Tax_Abate = document.getElementById('WucMRDelivery1_hdn_Tax_Abate');
var hdn_Amt_Taxable = document.getElementById('WucMRDelivery1_hdn_Amount_Taxable');
var hdn_Service_Tax_Amount =  document.getElementById('WucMRDelivery1_hdn_Service_Tax_Amount');
var txt_Octrai_Amt  =  document.getElementById('WucMRDelivery1_txt_Octr_Amount');
var hdn_Octr_Amt = document.getElementById('WucMRDelivery1_hdn_OctAmount');

var txt_Sub_Tot  =  document.getElementById('WucMRDelivery1_txt_Sub_Total');
var txt_Tax_Abate = document.getElementById('WucMRDelivery1_txt_Tax_Abate');
var txt_Amt_Taxable = document.getElementById('WucMRDelivery1_txt_Amount_Taxable');
var txt_Service_Tax_Amount  =  document.getElementById('WucMRDelivery1_txt_Service_Tax');

var hdn_Std_Octroi_Form_Charges =  document.getElementById('WucMRDelivery1_hdn_Std_Octroi_Form_Charges');
var hdn_Std_Octroi_Service_Charges =  document.getElementById('WucMRDelivery1_hdn_Std_Octroi_Service_Charges');
var hdn_Std_GI_Charges =  document.getElementById('WucMRDelivery1_hdn_Std_GI_Charges');
var hdn_Std_Hamali_Charges =  document.getElementById('WucMRDelivery1_hdn_Std_Hamali_Charges');
var hdn_Std_Demurage_Charges =  document.getElementById('WucMRDelivery1_hdn_Std_Demurage_Charges');
var hdn_CreditMemoFor_Id =  document.getElementById('WucMRDelivery1_hdn_CreditMemoFor_Id');
var hdn_KeyID =  document.getElementById('WucMRDelivery1_hdn_KeyID');

    if(rbtnCreditMemoFor_Octroi.checked == true)
    {
        hdn_CreditMemoFor_Id.value = '2';
        txt_GI_Charges.value = '0';
        txt_Detention_Charges.value = '0';
        txt_Hamali_charges.value = '0';
        txt_Local_Charges.value = '0';
        txt_Dem_Amt.value = '0';
        txt_Addition_Charges.value = '0';
        txt_Discount_Amount.value = '0';
        txt_Sub_Tot.value = 0;
        txt_Tax_Abate.value = 0;
        txt_Amt_Taxable.value = 0;
        txt_Service_Tax_Amount.value = 0;
        if(val(hdn_KeyID.value) <= 0)
        {
            txt_Oct_Form_Ch.value = val(hdn_Std_Octroi_Form_Charges.value);
            txt_Octroi_Service_Charge_Amount.value =val(hdn_Std_Octroi_Service_Charges.value);
            txt_Octrai_Amt.value = val(hdn_Octr_Amt.value);
        }

        txt_Oct_Form_Ch.disabled = false;
        txt_Octroi_Service_Charge_Amount.disabled = false;
        txt_GI_Charges.disabled = true;
        txt_Detention_Charges.disabled = true;
        txt_Hamali_charges.disabled = true;
        txt_Local_Charges.disabled = true;
        txt_Dem_Amt.disabled = true;
        txt_Addition_Charges.disabled = true;
        txt_Discount_Amount.disabled = true;
    }
    else if(rbtnCreditMemoFor_Both.checked == true)
    {
        hdn_CreditMemoFor_Id.value = '3';
         if(val(hdn_KeyID.value) <= 0)
        {
            txt_Oct_Form_Ch.value = hdn_Std_Octroi_Form_Charges.value;
            txt_Octroi_Service_Charge_Amount.value =val(hdn_Std_Octroi_Service_Charges.value);
            txt_GI_Charges.value = hdn_Std_GI_Charges.value;
            txt_Hamali_charges.value = val(hdn_Std_Hamali_Charges.value);
            txt_Dem_Amt.value = hdn_Std_Demurage_Charges.value;

            txt_Sub_Tot.value = hdn_Sub_Tot.value;
            txt_Tax_Abate.value = hdn_Tax_Abate.value;
            txt_Amt_Taxable.value = hdn_Amt_Taxable.value;
            txt_Service_Tax_Amount.value = hdn_Service_Tax_Amount.value;
            txt_Octrai_Amt.value = val(hdn_Octr_Amt.value);
        }
        txt_Oct_Form_Ch.disabled = false;
        txt_Octroi_Service_Charge_Amount.disabled = false;
        txt_GI_Charges.disabled = false;
        txt_Detention_Charges.disabled = false;
        txt_Hamali_charges.disabled = false;
        txt_Local_Charges.disabled = false;
        txt_Dem_Amt.disabled = false;
        txt_Addition_Charges.disabled = false;
        txt_Discount_Amount.disabled = true;
    }
    else
    {
        hdn_CreditMemoFor_Id.value = '1';
//        txt_Octrai_Amt.value = '0';
        txt_Oct_Form_Ch.value = '0';
        txt_Octroi_Service_Charge_Amount.value ='0';
        if(val(hdn_KeyID.value) <= 0)
        {
            txt_GI_Charges.value = hdn_Std_GI_Charges.value;
            txt_Hamali_charges.value = val(hdn_Std_Hamali_Charges.value);
            txt_Dem_Amt.value = hdn_Std_Demurage_Charges.value;
            
            txt_Sub_Tot.value = hdn_Sub_Tot.value;
            txt_Tax_Abate.value = hdn_Tax_Abate.value;
            txt_Amt_Taxable.value = hdn_Amt_Taxable.value;
            txt_Service_Tax_Amount.value = hdn_Service_Tax_Amount.value;
        }
        txt_Oct_Form_Ch.disabled = true;
        txt_Octroi_Service_Charge_Amount.disabled = true;
        txt_GI_Charges.disabled = false;
        txt_Detention_Charges.disabled = false;
        txt_Hamali_charges.disabled = false;
        txt_Local_Charges.disabled = false;
        txt_Dem_Amt.disabled = false;
        txt_Addition_Charges.disabled = false;
        txt_Discount_Amount.disabled = true;
    }
    
        Calculate_GrandTotal();
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
var hdn_Payment_Type_Id =  document.getElementById('WucMRDelivery1_hdn_Payment_Type_ID');
var txt_Addition_Charges_Remarks  =  document.getElementById('WucMRDelivery1_txt_Add_Charge_remark');
var txt_Discount_Amount_Remarks  =  document.getElementById('WucMRDelivery1_txt_discount_remark');
var Doc_Id=document.getElementById('WucMRDelivery1_hdn_DocumentID');
var MenuItemId=document.getElementById('WucMRDelivery1_hdn_MenuItemID');
var txt_ddl_DebitTo  =  document.getElementById('WucMRDelivery1_ddl_DebitTo_txtBoxddl_DebitTo');
var txt_ddl_BillingBranch  =  document.getElementById('WucMRDelivery1_ddl_BillingBranch_txtBoxddl_BillingBranch');
var rbl_CashBank=document.getElementById('WucMRDelivery1_Rbl_Receivedby_0');
var rbl_debit_to=document.getElementById('WucMRDelivery1_Rbl_Receivedby_1');

if(validateGeneralDetails(lbl_Error) == false)
{ return false;}

else if (val(hdn_Additional_Charges.value) > 0 && Trim(txt_Addition_Charges_Remarks.value) == '')
{
lbl_Error.innerText = 'Please Enter Additional Charge Remark';
txt_Addition_Charges_Remarks.focus();
return false;
}
else if (val(hdn_Discount_Amount.value) > 0 && Trim(txt_Discount_Amount_Remarks.value) == '')
{
lbl_Error.innerText = 'Please Enter Discount Remark';
txt_Discount_Amount_Remarks.focus();
return false;
}
else if (val(hdn_Sub_Tot.value) < 0)
{
lbl_Error.innerText = 'Sub Total should be greater than Zero';
return false;
}
else if(val(hdn_Tax_Abate.value) < 0)
{
lbl_Error.innerText = 'Tax Abate should be greater than Zero';
return false;
}
else if (val(hdn_Amt_Taxable.value) < 0)
{
lbl_Error.innerText = 'Amount Taxable should be greater than Zero';
return false;
}
else if (val(hdn_Service_Tax_Amount.value) < 0)
{
lbl_Error.innerText = 'Service Tax should be greater than Zero';
return false;
}
else if (val(hdn_Octr_Amt.value) < '0')
{
lbl_Error.innerText = 'Octroi Amount should be greater than Zero';
return false;
}
else if (val(MenuItemId.value) == '108' && (hdn_Payment_Type_Id.value == '1' || hdn_Payment_Type_Id.value == '5') && val(hdn_Total_Receivables.value) <= '0')
{
lbl_Error.innerText = 'Total Receivable should be greater than Zero';
return false;
}
else if (val(MenuItemId.value) == '195' && val(hdn_Total_Receivables.value) <= '0')
{
lbl_Error.innerText = 'Total Receivable should be greater than Zero';
return false;
}
else if (rbl_debit_to.checked == true && txt_ddl_DebitTo.value == '')
{
lbl_Error.innerText = 'Please Select Debit To Ledger';
txt_ddl_DebitTo.focus();
return false;
}
else if (rbl_debit_to.checked == true && txt_ddl_BillingBranch.value == '')
{
lbl_Error.innerText = 'Please Select Debit To Branch';
txt_ddl_BillingBranch.focus();
return false;
}

if(rbl_CashBank.checked == true &&  Doc_Id.value == '3' && val(hdn_Total_Receivables.value) > 0 && validateWUCCheque(lbl_Error) == false)
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
function HideReceivedByControl()
{
 var TR_DebitTo=document.getElementById('WucMRDelivery1_TR_DebitTo');
 var TR_Cheque=document.getElementById('WucMRDelivery1_TR_Cheque');    
 var rbl_CashBank=document.getElementById('WucMRDelivery1_Rbl_Receivedby_0');
    
    TR_DebitTo.style.display='none';
    if (rbl_CashBank.checked == true )
    {
      TR_Cheque.style.display='inline';
      TR_DebitTo.style.display='none';
    }
    else 
    {
      TR_Cheque.style.display='none';
      TR_DebitTo.style.display='inline';
    }
}
