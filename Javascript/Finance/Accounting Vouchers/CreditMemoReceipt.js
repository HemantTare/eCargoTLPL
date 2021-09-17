// JScript File

function Txt_Enable()
{ 
  var Tot_Amount = 0;
  var Total_CredirMemo=0;
  var lbl_TotalValue = document.getElementById('WucCreditMemoReceipt1_lbl_TotalValue');
  var hdn_Total = document.getElementById('WucCreditMemoReceipt1_hdn_Total');
  var hdn_TotalCreditMemo= document.getElementById('WucCreditMemoReceipt1_hdn_TotalCreditMemo');
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
                             Total_CredirMemo =  val(Total_CredirMemo) + 1;
                       }
                    }
               
                else 
                    if (arr[3] =="txt_AmountReceived")
                    {
                        if (add_this_record == true)
                            {
                                if (elm_id.value == '') elm_id.value = 0;
                                elm_id.disabled = false;
                                Tot_Amount =  val(Tot_Amount) + val(elm_id.value);
                            }
                        else 
                            elm_id.disabled = true;
                            add_this_record = false;
                    }
            }        

    }
    
    lbl_TotalValue.innerHTML =  Math.round(Tot_Amount*100)/100 ;
    hdn_Total.value =  Math.round(Tot_Amount*100)/100 ;
     hdn_TotalCreditMemo.value =  Total_CredirMemo ;
}
function Allow_To_Save()
{
     var ddl_PartyName = document.getElementById('WucCreditMemoReceipt1_ddl_PartyName_txtBoxddl_PartyName');
     var lbl_Error = document.getElementById('WucCreditMemoReceipt1_lbl_Error');
     var txt_Remark=   document.getElementById('WucCreditMemoReceipt1_txt_Remark');          
     var lbl_TotalValue = document.getElementById('WucCreditMemoReceipt1_lbl_TotalValue');
     var hdn_Total = document.getElementById('WucCreditMemoReceipt1_hdn_Total');
     var hdn_TotalCreditMemo= document.getElementById('WucCreditMemoReceipt1_hdn_TotalCreditMemo');
     var txt_CashAmount=   document.getElementById('WucCreditMemoReceipt1_txt_CashAmount');          
     var txt_ChequeAmount=   document.getElementById('WucCreditMemoReceipt1_txt_ChequeAmount');  
     var txt_ChequeNo=   document.getElementById('WucCreditMemoReceipt1_txt_ChequeNo');  
     
     
     var ATS = false;
   
           
       if (ddl_PartyName.value == '')       
       {
            lbl_Error.innerText='Please Select Party Name';
            ddl_PartyName.focus();            
       }
       else if (val(hdn_TotalCreditMemo.value)<=0)
        {
             lbl_Error.innerText='Please Select Atleast One Credit Memo';             
        }
        else if (val(hdn_Total.value)!=(val(txt_CashAmount.value)+val(txt_ChequeAmount.value)))
        {
            lbl_Error.innerText='Cash And Cheque Amount Should Be Match With Total Amount';
            txt_CashAmount.focus();          
        }
        else if (val(txt_ChequeAmount.value)>0 && CheckCheque() == false)
        {
            
         }
        else if (txt_Remark.value=='')
        {
             lbl_Error.innerText='Please Enter Remark';
             txt_Remark.focus();         
        }    
        else
                ATS = true;
        
         return ATS;

}

function CheckCheque()
{
     var txt_ChequeNo=   document.getElementById('WucCreditMemoReceipt1_txt_ChequeNo');  
     var lbl_Error = document.getElementById('WucCreditMemoReceipt1_lbl_Error');
     
           if(txt_ChequeNo.value=='')
           {
                lbl_Error.innerText='Please Enter Cheque No.';
                txt_ChequeNo.focus();
                return false;
           }
           else if (txt_ChequeNo.value.length<6)
           {
                lbl_Error.innerText='Cheque No Should Be Greater Than Six';
                txt_ChequeNo.focus();
                return false;
           }
 
        return true;
}