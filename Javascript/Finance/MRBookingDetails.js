// JScript File



function On_Cash_Amount_Change()
{

var txt_CashAmount = document.getElementById('WucMRBookingDetails1_WucMRCashChequeDetails1_txt_CashAmount');
var txt_ChequeAmount = document.getElementById('WucMRBookingDetails1_WucMRCashChequeDetails1_txt_ChequeAmount');
var txt_Totalreceivables = document.getElementById('WucMRBookingDetails1_txt_TotalReceivables');
var hdn_CashAmount = document.getElementById('WucMRBookingDetails1_WucMRCashChequeDetails1_hdn_CashAmount');
var hdn_ChequeAmount = document.getElementById('WucMRBookingDetails1_WucMRCashChequeDetails1_hdn_ChequeAmount');
var hdn_ToatlChequeAmount = document.getElementById('WucMRBookingDetails1_WucMRCashChequeDetails1_hdn_Total_Cheque_Amount');


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



txt_ChequeAmount.value=Amount - val( txt_CashAmount.value );   

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

var txt_CashAmount = document.getElementById('WucMRBookingDetails1_WucMRCashChequeDetails1_txt_CashAmount');
var txt_ChequeAmount = document.getElementById('WucMRBookingDetails1_WucMRCashChequeDetails1_txt_ChequeAmount');
var txt_Totalreceivables = document.getElementById('WucMRBookingDetails1_txt_TotalReceivables');
var hdn_CashAmount = document.getElementById('WucMRBookingDetails1_WucMRCashChequeDetails1_hdn_CashAmount');
var hdn_ChequeAmount = document.getElementById('WucMRBookingDetails1_WucMRCashChequeDetails1_hdn_ChequeAmount');
var hdn_ToatlChequeAmount = document.getElementById('WucMRBookingDetails1_WucMRCashChequeDetails1_hdn_Total_Cheque_Amount');

var Amount = val(txt_Totalreceivables.value);

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

