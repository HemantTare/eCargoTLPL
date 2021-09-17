
    // JScript File
    function Allow_To_Save()
    {   
        var ATS = false;

        var lbl_Errors = document.getElementById('wucDirectDelivery1_lbl_Errors');
        var hdn_Vehicle_Id = document.getElementById('wucDirectDelivery1_hdn_Vehicle_Id');  
        var hdn_GC_Id = document.getElementById('wucDirectDelivery1_hdn_GC_Id');  
        var hdn_LHPO_Id = document.getElementById('wucDirectDelivery1_hdn_LHPO_Id');  

        var lbl_LoadingArticlesValue=document.getElementById('wucDirectDelivery1_lbl_LoadingArticlesValue');  
        var txt_DeliveredArticles=document.getElementById('wucDirectDelivery1_txt_DeliveredArticles');  
        var lbl_LoadingArticleWeightValue=document.getElementById('wucDirectDelivery1_lbl_LoadingArticleWeightValue');  
        var txt_DeliveredArticlesWeight=document.getElementById('wucDirectDelivery1_txt_DeliveredArticlesWeight');  
        var txt_DamageLeakageArticle =document.getElementById('wucDirectDelivery1_txt_DamageLeakageArticle');  
        var txt_DamageLeakageValue=document.getElementById('wucDirectDelivery1_txt_DamageLeakageValue');  

        var lbl_ShortArticlesValue=document.getElementById('wucDirectDelivery1_lbl_ShortArticlesValue');  

        var hdn_DeliveredArticles=document.getElementById('wucDirectDelivery1_hdn_DeliveredArticles');  
        var hdn_DeliveredArticlesWeight=document.getElementById('wucDirectDelivery1_hdn_DeliveredArticlesWeight');  
        var hdn_DamageLeakageArticle=document.getElementById('wucDirectDelivery1_hdn_DamageLeakageArticle'); 
        var lbl_ShortArticlesValue=document.getElementById('wucDirectDelivery1_lbl_ShortArticlesValue'); 

        var hdn_ShortArticlesValue=document.getElementById('wucDirectDelivery1_hdn_ShortArticlesValue'); 
        var ddl_Reason_For_Late_Delivery= document.getElementById('wucDirectDelivery1_ddl_Reason_For_Late_Delivery');
        
        var hdn_gc_caption= document.getElementById('wucDirectDelivery1_hdn_gc_caption');
        var hdn_lhpo_caption= document.getElementById('wucDirectDelivery1_hdn_lhpo_caption');
        
        var txt_ddl_DebitTo  =  document.getElementById('wucDirectDelivery1_ddl_DebitTo_txtBoxddl_DebitTo');
        var txt_ddl_BillingBranch  =  document.getElementById('wucDirectDelivery1_ddl_BillingBranch_txtBoxddl_BillingBranch');
        var rbl_CashBank=document.getElementById('wucDirectDelivery1_Rbl_Receivedby_0');
        var rbl_debit_to=document.getElementById('wucDirectDelivery1_Rbl_Receivedby_1');
        var lbl_TotalGCAmountValue=document.getElementById('wucDirectDelivery1_lbl_TotalGCAmountValue');
       
        var chk_IsFreightReceived =document.getElementById('wucDirectDelivery1_chk_IsFreightReceived'); 
 
        var TotalGCAmountValue = val(lbl_TotalGCAmountValue.innerText); 

        if ( val(hdn_Vehicle_Id.value) <= 0)
        {  
            lbl_Errors.innerText = "Please Select Vehicle.";
        }
        else if (val(hdn_GC_Id.value) == '')
        {
            lbl_Errors.innerText = "Please Select " + hdn_gc_caption.value; 
        }   
        else if (val(hdn_LHPO_Id.value) == '')
        {
            lbl_Errors.innerText = "Please Select " + hdn_lhpo_caption.value;
        }   
        else if ( val(hdn_DeliveredArticles.value) <=0)
        {
            lbl_Errors.innerText = "Delivered Articles Should Be Greater Than Zero.";
        }
//        else if ( Trim(ddl_Reason_For_Late_Delivery.value) == '')
//        {
//            lbl_Errors.innerText = "Please Select Reason For Late Delivery.";
//        } 
        else if (chk_IsFreightReceived.checked == true && rbl_debit_to.checked == true && txt_ddl_DebitTo.value == '')
        {
            lbl_Errors.innerText = 'Please Select Debit To Ledger';
            txt_ddl_DebitTo.focus();
            return false;
        }
        else if (chk_IsFreightReceived.checked == true && rbl_debit_to.checked == true && txt_ddl_BillingBranch.value == '')
        {
            lbl_Errors.innerText = 'Please Select Debit To Branch';
            txt_ddl_BillingBranch.focus();
            return false;
        }
         
        if(chk_IsFreightReceived.checked == true &&  rbl_CashBank.checked == true && TotalGCAmountValue > 0 && validateWUCCheque(lbl_Errors) == false)
        {return false;}
        else
            ATS = true;

        return ATS;
    }


    function On_Delivery_Condition_Change()
    {
        var ddl_Delivery_Condintion =document.getElementById('wucDirectDelivery1_ddl_Delivery_Condintion'); 

        var txt_DamageLeakageArticle=document.getElementById('wucDirectDelivery1_txt_DamageLeakageArticle');  
        var txt_DamageLeakageValue=document.getElementById('wucDirectDelivery1_txt_DamageLeakageValue');

        if ( ddl_Delivery_Condintion.value != "1")    
        {    
            txt_DamageLeakageArticle.disabled = false;
            txt_DamageLeakageValue.disabled = false;
        }
        else
        {
            txt_DamageLeakageArticle.disabled = true;
            txt_DamageLeakageValue.disabled = true;        

            txt_DamageLeakageArticle.value="0";
            txt_DamageLeakageValue.value="0";    
        }
    }

    function On_Delivery_Article_Change()
    {
        var lbl_LoadingArticlesValue=document.getElementById('wucDirectDelivery1_lbl_LoadingArticlesValue');  
        var txt_DeliveredArticles=document.getElementById('wucDirectDelivery1_txt_DeliveredArticles'); 
        var lbl_LoadingArticleWeightValue=document.getElementById('wucDirectDelivery1_lbl_LoadingArticleWeightValue');  
        var txt_DeliveredArticlesWeight=document.getElementById('wucDirectDelivery1_txt_DeliveredArticlesWeight'); 
        var txt_DamageLeakageArticle =document.getElementById('wucDirectDelivery1_txt_DamageLeakageArticle');  
        var txt_DamageLeakageValue=document.getElementById('wucDirectDelivery1_txt_DamageLeakageValue');  
        var lbl_ShortArticlesValue=document.getElementById('wucDirectDelivery1_lbl_ShortArticlesValue');  
        var hdn_DeliveredArticles=document.getElementById('wucDirectDelivery1_hdn_DeliveredArticles');  
        var hdn_DeliveredArticlesWeight=document.getElementById('wucDirectDelivery1_hdn_DeliveredArticlesWeight');  
        var hdn_DamageLeakageArticle=document.getElementById('wucDirectDelivery1_hdn_DamageLeakageArticle'); 
        var lbl_ShortArticlesValue=document.getElementById('wucDirectDelivery1_lbl_ShortArticlesValue'); 

        var hdn_ShortArticlesValue=document.getElementById('wucDirectDelivery1_hdn_ShortArticlesValue'); 

        if ( val( lbl_LoadingArticlesValue.innerHTML) < val( txt_DeliveredArticles.value))
        {
            txt_DeliveredArticles.value= val( lbl_LoadingArticlesValue.innerHTML);
        }

        if ( val( lbl_LoadingArticleWeightValue.innerHTML) < val( txt_DeliveredArticlesWeight.value))
        {
            txt_DeliveredArticlesWeight.value= val( lbl_LoadingArticleWeightValue.innerHTML);
        }             

        if ( val( txt_DeliveredArticles.value) < val( txt_DamageLeakageArticle.value))
        {
            txt_DamageLeakageArticle.value= val( txt_DeliveredArticles.value);
        }          

        hdn_DeliveredArticles.value = val(txt_DeliveredArticles.value) ;
        hdn_DeliveredArticlesWeight.value = val(txt_DeliveredArticlesWeight.value);

        hdn_DamageLeakageArticle.value = val(txt_DamageLeakageArticle.value);

        lbl_ShortArticlesValue.innerHTML=val( lbl_LoadingArticlesValue.innerHTML) - val(txt_DeliveredArticles.value);

        hdn_ShortArticlesValue.value = val(lbl_ShortArticlesValue.innerHTML);
    }
    
    function On_chkIsFreightReceived()
    {
        var chk_IsFreightReceived =document.getElementById('wucDirectDelivery1_chk_IsFreightReceived'); 

        var pnl_Payment=document.getElementById('wucDirectDelivery1_pnl_Payment');  

        if (chk_IsFreightReceived.checked == true)
        {
          pnl_Payment.style.visibility = 'visible'; 
        }
        else
        {
          pnl_Payment.style.visibility = 'hidden'; 
        }
       HideReceivedByControl();
    }
    
function HideReceivedByControl()
{
 var TR_DebitTo=document.getElementById('wucDirectDelivery1_TR_DebitTo');
 var TR_Cheque=document.getElementById('wucDirectDelivery1_TR_Cheque');    
 var rbl_CashBank=document.getElementById('wucDirectDelivery1_Rbl_Receivedby_0');
 var TR_TDS=document.getElementById('wucDirectDelivery1_TR_TDS');
  
 
    
    TR_DebitTo.style.display='none';
    if (rbl_CashBank.checked == true )
    {
      TR_Cheque.style.display='inline';
      TR_DebitTo.style.display='none';
      TR_TDS.style.display='inline';
    }
    else 
    {
      TR_Cheque.style.display='none';
      TR_DebitTo.style.display='inline';
      TR_TDS.style.display='none';
    }
}

function chkPaymentType()
{
    var hdn_PaymentTypeId =document.getElementById('wucDirectDelivery1_hdn_PaymentTypeId'); 
    var chk_IsFreightReceived =document.getElementById('wucDirectDelivery1_chk_IsFreightReceived'); 
    var lbl_IsFreightReceived=document.getElementById('wucDirectDelivery1_lbl_IsFreightReceived');  

    if (hdn_PaymentTypeId.value == 1)
    {
        chk_IsFreightReceived.visibility = 'visible'; 
        lbl_IsFreightReceived.visibility = 'visible';  
    }
    else
    {
      chk_IsFreightReceived.style.visibility = 'hidden'; 
      lbl_IsFreightReceived.style.visibility = 'hidden'; 
    }
    
}