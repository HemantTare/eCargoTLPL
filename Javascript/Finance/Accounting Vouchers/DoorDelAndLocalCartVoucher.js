// JScript File



function Cheque()
{
    var lbl_Cheque = document.getElementById('WucDoorDelAndLocalCartVoucher1_lbl_Cheque');
    var txt_Cheque=document.getElementById('WucDoorDelAndLocalCartVoucher1_txt_Cheque');
    var lbl_ChequeInFavourOf = document.getElementById('WucDoorDelAndLocalCartVoucher1_lbl_ChequeInFavourOf');
    var txt_ChequeInFavourOf = document.getElementById('WucDoorDelAndLocalCartVoucher1_txt_ChequeInFavourOf');
    var lbl_ddl_Ledgers=document.getElementById('WucDoorDelAndLocalCartVoucher1_lbl_ddlLedgers');
    var ddl_GetLedgers = document.getElementById('WucDoorDelAndLocalCartVoucher1_ddl_GetLedgers_txtBoxddl_GetLedgers');
    var td_Ledger=document.getElementById('WucDoorDelAndLocalCartVoucher1_ddl_Ledger');
    var RadioButtonList1=document.getElementById('WucDoorDelAndLocalCartVoucher1_RadioButtonList1_0');
    var RadioButtonList2 = document.getElementById('WucDoorDelAndLocalCartVoucher1_RadioButtonList1_2');
    var tr_Cheque=document.getElementById('WucDoorDelAndLocalCartVoucher1_tr_Cheque');
    tr_Cheque.style.display="none";
    td_Ledger.style.visibility='hidden';
    if(RadioButtonList1.checked == true)
    {
         tr_Cheque.style.display="";    
    }
    else if (RadioButtonList2.checked == true)
    {
         td_Ledger.style.visibility='visible';     
    }   
    else
    {
    
    }

}

//***********************************************************************************************************

function get_details()
{

var lbl_Client_Error = document.getElementById('WucDoorDelAndLocalCartVoucher1_lbl_Client_Error');
var txt_GCNo = document.getElementById('WucDoorDelAndLocalCartVoucher1_txt_GCNo');
var hdn_GCCaption = document.getElementById('WucDoorDelAndLocalCartVoucher1_hdn_GCCaption');


if (txt_GCNo.value == '')
  {
   
    lbl_Client_Error.innerHTML = hdn_GCCaption.value +" Number(s) Cannot be blank";
    txt_GCNo.focus();
    lbl_Client_Error.style.visibility='visible';
    return false;
  }
else
  {
    lbl_Client_Error.style.visibility='hidden';
    return true;
  }  
}

//***********************************************************************************************************
function valid_for_Docket(f)
{
if (window.event.keyCode == 37 || window.event.keyCode == 39 || window.event.keyCode == 9 || window.event.keyCode == 16) return;
f.value = f.value.replace(/[^\,|\d]/g,"");
}


function Txt_Enable()
{ 
  var Tot_Spent = 0;
  var Total_GC=0;
  var lbl_Total_Spent = document.getElementById('WucDoorDelAndLocalCartVoucher1_lbl_Total_Spent');
  var hdn_Total_Spent_Add = document.getElementById('WucDoorDelAndLocalCartVoucher1_hdn_Total_Spent_Add');
  var lbl_TotalGC= document.getElementById('WucDoorDelAndLocalCartVoucher1_lbl_TotalGC');
  var add_this_record;
  
   for(i = 0; i < document.forms[0].elements.length; i++) 
    {
        
        elm = document.forms[0].elements[i];
        if (elm.name != undefined)
            {
                var elm_id = document.getElementById(elm.id);
                var elm_name = elm.name;
                var arr = elm_name.split("$");
                
               
                if (arr[3] =="chk_Attach")
                    {
                    if (elm_id.checked == true)
                        {
                           if (elm_id.value == '') elm_id.value = 0;
                            add_this_record = true;
                             Total_GC =  parseFloat(Total_GC) + 1;
                       }
                    }
               
                else if (arr[3] =="txt_Amount_Spent_Add")
                    {
                        if (add_this_record == true)
                            {
                                if (elm_id.value == '') elm_id.value = 0;
                                elm_id.disabled = false;
                                Tot_Spent =  parseFloat(Tot_Spent) + parseFloat(elm_id.value);
                            }
                        else 
                            elm_id.disabled = true;
                            add_this_record = false;
                    }
            }        

    }
    
    lbl_Total_Spent.innerHTML =  Math.round(Tot_Spent*100)/100 ;
    hdn_Total_Spent_Add.value =  Math.round(Tot_Spent*100)/100 ;
    lbl_TotalGC.innerHTML =  Total_GC ;
}
function Allow_To_Save()
{
     var radio_1=document.getElementById('WucDoorDelAndLocalCartVoucher1_RadioButtonList1_0');
     var radio_2=document.getElementById('WucDoorDelAndLocalCartVoucher1_RadioButtonList1_2');    
     var txt_Cheque=document.getElementById('WucDoorDelAndLocalCartVoucher1_txt_Cheque');
     var txt_ChequeInFavourOf = document.getElementById('WucDoorDelAndLocalCartVoucher1_txt_ChequeInFavourOf');
     var ddl_GetLedgers = document.getElementById('WucDoorDelAndLocalCartVoucher1_ddl_GetLedgers_txtBoxddl_GetLedgers');
     var lbl_Client_Error = document.getElementById('WucDoorDelAndLocalCartVoucher1_lbl_Error');
     var txt_Remark=   document.getElementById('WucDoorDelAndLocalCartVoucher1_txt_Remark');
     var lbl_TotalGC= document.getElementById('WucDoorDelAndLocalCartVoucher1_lbl_TotalGC');
     var hdn_GCCaption = document.getElementById('WucDoorDelAndLocalCartVoucher1_hdn_GCCaption');

     var ATS = false;
   
       
        if(radio_1.checked==true)
        {
            if (CheckCheque() == false)
            {
               return ATS;         
            }
        }   
        if(radio_2.checked==true)
        {           
            if (CheckLedger() == false)
            {
               return ATS;         
            }
        }
        if (parseFloat(lbl_TotalGC.innerText)<=0)
        {
             lbl_Client_Error.innerText='Please Select Atleast One '+hdn_GCCaption.value;             
        }
        else if (txt_Remark.value=='')
        {
             lbl_Client_Error.innerText='Please Enter Remark';
             txt_Remark.focus();
         
        }    
        else
                ATS = true;
        
         return ATS;

}

function CheckCheque()
{
     var txt_Cheque=document.getElementById('WucDoorDelAndLocalCartVoucher1_txt_Cheque');
     var txt_ChequeInFavourOf = document.getElementById('WucDoorDelAndLocalCartVoucher1_txt_ChequeInFavourOf');
     var ddl_GetLedgers = document.getElementById('WucDoorDelAndLocalCartVoucher1_ddl_GetLedgers_txtBoxddl_GetLedgers');
     var lbl_Client_Error = document.getElementById('WucDoorDelAndLocalCartVoucher1_lbl_Error');
     var txt_Remark=   document.getElementById('WucDoorDelAndLocalCartVoucher1_txt_Remark');
     
     
           if(txt_Cheque.value=='')
           {
                lbl_Client_Error.innerText='Please Enter Cheque No.';
                txt_Cheque.focus();
                return false;
           }
           else if (txt_ChequeInFavourOf.value=='')
           {
                lbl_Client_Error.innerText='Please Enter Cheque In Favaour Of.';
                txt_ChequeInFavourOf.focus();
                return false;
           }
 
        return true;
}
function CheckLedger()
{
     var txt_Cheque=document.getElementById('WucDoorDelAndLocalCartVoucher1_txt_Cheque');
     var txt_ChequeInFavourOf = document.getElementById('WucDoorDelAndLocalCartVoucher1_txt_ChequeInFavourOf');
     var ddl_GetLedgers = document.getElementById('WucDoorDelAndLocalCartVoucher1_ddl_GetLedgers_txtBoxddl_GetLedgers');
     var lbl_Client_Error = document.getElementById('WucDoorDelAndLocalCartVoucher1_lbl_Error');
     var txt_Remark=   document.getElementById('WucDoorDelAndLocalCartVoucher1_txt_Remark');
     


       if (ddl_GetLedgers.value == '')
       {
            lbl_Client_Error.innerText='Please Select Ledger';
            ddl_GetLedgers.focus();
            return false;
       }
       
    return true;                
}
function CheckAmountSpent(txt_Amount_Spent_Add,chk_Attach)
{
    var lbl_Client_Error = document.getElementById('WucDoorDelAndLocalCartVoucher1_lbl_Error');
    var txt_Amount_Spent_Add= document.getElementById(txt_Amount_Spent_Add);
    var chk_Attach= document.getElementById(chk_Attach);
    
     lbl_Client_Error.innerText='';
    if(chk_Attach.checked==true)
    {
            if(parseFloat(txt_Amount_Spent_Add.value)<=0)
            {
                lbl_Client_Error.innerText='Amount Spent Should be Greater then Zero';                
            }   
    }
    Txt_Enable();
}