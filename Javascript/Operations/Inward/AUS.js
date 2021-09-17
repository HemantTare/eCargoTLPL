

// JScript File

     
     function ValidateUI_WucAUSUnloadingDetails()
        {   

            var ATS = false;
           //var objResource=new Resource('WucAUS1_WucAUSUnloadingDetails1_hdf_ResourecString');

            var lbl_Errors = document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_Errors');

            var hdn_Vehicle_Id = document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Vehicle_Id');  

            var ddl_LHPO = document.getElementById('WucAUS1_WucAUSUnloadingDetails1_ddl_LHPO');
            var ddl_TAS = document.getElementById('WucAUS1_WucAUSUnloadingDetails1_ddl_TAS');
            var hdn_IsTAS = document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_IsTAS');
            var hdn_Supervisor_Id = document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Supervisor_Id');


            var hdn_Total_Received_Articles = document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Received_Articles');
            
            var hdn_Total_Additional_Freight = document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Additional_Freight');

            var ddl_Supervisor = document.getElementById('WucAUS1_WucAUSUnloadingDetails1_ddl_Supervisor_txtBoxddl_Supervisor');


            var ddl_Reason_For_Late_Uploading= document.getElementById('WucAUS1_WucAUSUnloadingDetails1_ddl_Reason_For_Late_Uploading');
            var ddl_Reason_For_Late_Arrival= document.getElementById('WucAUS1_WucAUSUnloadingDetails1_ddl_Reason_For_Late_Arrival');

            if ( val(hdn_Vehicle_Id.value) <= 0)
            {  
                lbl_Errors.innerText = "Please Select Vehicle.";//objResource.GetMsg("Msg_VehicleNo");
                WucAUS1_TB_AUS.SelectTabById('zero'); 
            }
            //Added by: Anita on :24 Jan 09
//            else if(val(ddl_TAS.value)==0 && hdn_IsTAS.value == 'True')
//            {
//                lbl_Errors.innerText = "Please Select TAS No."
//                WucAUS1_TB_AUS.SelectTabById('zero'); 
//            }
            else if (val(ddl_LHPO.value) == 0)
            {
                lbl_Errors.innerText = "Please Select LHPO No"; //objResource.GetMsg("Msg_LHPO");     
                WucAUS1_TB_AUS.SelectTabById('zero'); 
            }   
            else if ( val(hdn_Total_Received_Articles.value) <=0)
            {
                lbl_Errors.innerText = "Total Received Articles Should Be Greater Than Zero."; //objResource.GetMsg("Msg_TotalReceivedArticles");       
                WucAUS1_TB_AUS.SelectTabById('zero'); 
            }
//            else if ( val(ddl_Reason_For_Late_Arrival.value) == 0)
//            {
//                lbl_Errors.innerText = "Please Select Reason For Late Arrival."; //objResource.GetMsg("Msg_ReasonForLateArrival");       
//                WucAUS1_TB_AUS.SelectTabById('zero'); 
//            } 
//            else if ( val(ddl_Reason_For_Late_Uploading.value) == 0)            
//            {
//                lbl_Errors.innerText = "Please Select Reason For Late Unloading."; //objResource.GetMsg("Msg_ReasonFroLateUnloading");    
//                WucAUS1_TB_AUS.SelectTabById('zero'); 
//            } 
            else if (ddl_Supervisor.value == '' ) // val(hdn_Supervisor_Id.value) <=0)
            {
                lbl_Errors.innerText = "Please Select Supervisor";
                WucAUS1_TB_AUS.SelectTabById('zero');       
            }  
            else
                ATS = true;

            return ATS;
        }



    function Enable_Disable(   ddl_Received_Condintion, txt_Damaged_Leakage_Articles, txt_Damaged_Leakage_Value)
    {
        var ddl_Received_Condintion =document.getElementById(ddl_Received_Condintion);  
        var txt_Damaged_Leakage_Articles=document.getElementById(txt_Damaged_Leakage_Articles);  
        var txt_Damaged_Leakage_Value=document.getElementById(txt_Damaged_Leakage_Value);  
        
        if ( ddl_Received_Condintion.value != 1)    
        {    
            txt_Damaged_Leakage_Articles.disabled = false;
            txt_Damaged_Leakage_Value.disabled = false;              
        }
        else
        {
            txt_Damaged_Leakage_Articles.disabled = true;
            txt_Damaged_Leakage_Value.disabled = true; 

            txt_Damaged_Leakage_Articles.value="0";
            txt_Damaged_Leakage_Value.value="0";    
        }    
    }




      function Calculate_Summary(lbl_Loaded_Articles  , lbl_Loaded_Actual_Wt  ,
                                txt_Recieved_Article, txt_Recieved_Articles_Wt,
                                txt_Damaged_Leakage_Articles, txt_Damaged_Leakage_Value,
                                txt_Additional_Freight,Is_OnBlur)
      {
        
        var lbl_Loaded_Articles =document.getElementById(lbl_Loaded_Articles);  
        var lbl_Loaded_Actual_Wt=document.getElementById(lbl_Loaded_Actual_Wt);  
         
        var txt_Recieved_Article=document.getElementById(txt_Recieved_Article);  
        var txt_Recieved_Articles_Wt=document.getElementById(txt_Recieved_Articles_Wt);  
        
        var txt_Damaged_Leakage_Articles=document.getElementById(txt_Damaged_Leakage_Articles);  
        var txt_Damaged_Leakage_Value=document.getElementById(txt_Damaged_Leakage_Value);  
        
        var txt_Additional_Freight = document.getElementById(txt_Additional_Freight);  

 
        var lbl_Total_Booking_Articles=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_Total_Booking_Articles');  
        var lbl_Total_Booking_Articles_Wt=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_Total_Booking_Articles_Wt');  

        var lbl_Total_Loaded_Articles=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_Total_Loaded_Articles');  
        var lbl_Total_Loaded_Articles_Wt=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_Total_Loaded_Articles_Wt');  

        var lbl_Total_Received_Articles=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_Total_Received_Articles');  
        var lbl_Total_Received_Articles_Wt=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_Total_Received_Articles_Wt');  

        var lbl_Total_Damage_Leakage_Articles=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_Total_Damage_Leakage_Articles');  
        var lbl_Total_Damage_Leakage_Value=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_Total_Damage_Leakage_Value');  
        
        var lbl_Total_Additional_Freight =document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_Total_Additional_Freight');  
        
       
        var hdn_Total_Booking_Articles=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Booking_Articles');  
        var hdn_Total_Booking_Articles_Wt=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Booking_Articles_Wt');  
        
        var hdn_Total_Loaded_Articles=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Loaded_Articles');  
        var hdn_Total_Loaded_Articles_Wt=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Loaded_Articles_Wt');  
        
        var hdn_Total_Received_Articles=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Received_Articles');  
        var hdn_Total_Received_Articles_Wt=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Received_Articles_Wt');  
        
        var hdn_Total_Damage_Leakage_Articles=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Damage_Leakage_Articles');  
        var hdn_Total_Damage_Leakage_Value=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Damage_Leakage_Value');  
        
        var hdn_Total_Additional_Freight = document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Additional_Freight');  
            
        var lbl_TotalShortArticlesValue=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_TotalShortArticlesValue');  
        var lbl_Total_Damage_Leakage_Articles=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_Total_Damage_Leakage_Articles');  
        var lbl_Total_Damage_Leakage_Value=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_Total_Damage_Leakage_Value');  
        
        var hdn_Total_Short_Articles=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Short_Articles');  
         
        
        if ( Is_OnBlur == '0' )
            {  
                hdn_Total_Received_Articles.value = val(hdn_Total_Received_Articles.value) - val(txt_Recieved_Article.value);

                hdn_Total_Received_Articles_Wt.value = val(hdn_Total_Received_Articles_Wt.value) - val(txt_Recieved_Articles_Wt.value);

                hdn_Total_Damage_Leakage_Articles.value  = val(hdn_Total_Damage_Leakage_Articles.value) - val(txt_Damaged_Leakage_Articles.value);

                hdn_Total_Damage_Leakage_Value.value  = val(hdn_Total_Damage_Leakage_Value.value) - val(txt_Damaged_Leakage_Value.value);            
                
                hdn_Total_Additional_Freight.value = val(hdn_Total_Additional_Freight.value) - val( txt_Additional_Freight.value); 

            }        
        else       
            {
                 if ( val( lbl_Loaded_Articles.innerHTML) < val( txt_Recieved_Article.value))
                 {
                    txt_Recieved_Article.value= val( lbl_Loaded_Articles.innerHTML);
                 }
             
                 if ( val( lbl_Loaded_Actual_Wt.innerHTML) < val( txt_Recieved_Articles_Wt.value))
                 {
                    txt_Recieved_Articles_Wt.value= val( lbl_Loaded_Actual_Wt.innerHTML);
                 }             
                 
                 if ( val( txt_Recieved_Article.value) < val( txt_Damaged_Leakage_Articles.value))
                 {
                    txt_Damaged_Leakage_Articles.value= val( txt_Recieved_Article.value);
                 }          
             
                hdn_Total_Received_Articles.value = val(hdn_Total_Received_Articles.value) + val(txt_Recieved_Article.value);
                lbl_Total_Received_Articles.innerHTML= val(hdn_Total_Received_Articles.value);

                hdn_Total_Short_Articles.value=val(hdn_Total_Loaded_Articles.value) - val(hdn_Total_Received_Articles.value);            
                lbl_TotalShortArticlesValue.innerHTML=val(hdn_Total_Short_Articles.value);

                hdn_Total_Received_Articles_Wt.value = val(hdn_Total_Received_Articles_Wt.value) + val(txt_Recieved_Articles_Wt.value);
                lbl_Total_Received_Articles_Wt.innerHTML=val(hdn_Total_Received_Articles_Wt.value);

                hdn_Total_Damage_Leakage_Articles.value  = val(hdn_Total_Damage_Leakage_Articles.value) + val(txt_Damaged_Leakage_Articles.value);
                lbl_Total_Damage_Leakage_Articles.innerHTML=val(hdn_Total_Damage_Leakage_Articles.value); 

                hdn_Total_Damage_Leakage_Value.value  = val(hdn_Total_Damage_Leakage_Value.value) + val(txt_Damaged_Leakage_Value.value);
                lbl_Total_Damage_Leakage_Value.innerHTML=val(hdn_Total_Damage_Leakage_Value.value); 
                
                hdn_Total_Additional_Freight.value = val(hdn_Total_Additional_Freight.value) + val( txt_Additional_Freight.value);
                lbl_Total_Additional_Freight.innerHTML = val( hdn_Total_Additional_Freight.value);  
            
                Calculate_Total_Receivable();    
                Calculate_Total_Payable();
            }
            
      }
      
      
  
   function Calculate_Total_Receivable()
      {      
      
 
        var hdn_Total_Delivery_Commision=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Delivery_Commision');  
        var hdn_Total_Additional_Freight=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Additional_Freight');  

        var txt_Lorry_Hire=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_txt_Lorry_Hire');  
        var txt_Others_Recevable=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_txt_Others_Recevable');  

        var lbl_Total_Recevable_Value=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_Total_Recevable_Value');  
        var hdn_Total_Recevable_Value=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Recevable_Value');  
        
        var lbl_UpcountryCrossingCostValue=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_UpcountryCrossingCostValue'); 
        var hdn_UpcountryCrossingCost= document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_UpcountryCrossingCost'); 
        
        var hdn_Total_Additional_Freight = document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Additional_Freight');  
        
        txt_Lorry_Hire.value = val( txt_Lorry_Hire.value) ;

         txt_Others_Recevable.value = val( txt_Others_Recevable.value) ;
          
        
        lbl_Total_Recevable_Value.innerHTML =  val( hdn_Total_Delivery_Commision.value) + 
                                                val( hdn_Total_Additional_Freight.value) +
                                                val( txt_Lorry_Hire.value) +
                                                val( txt_Others_Recevable.value)+
                                                val( hdn_UpcountryCrossingCost.value)
                                                  
       lbl_Total_Recevable_Value.innerHTML = val(lbl_Total_Recevable_Value.innerHTML );
       
       hdn_Total_Recevable_Value.value  = val(lbl_Total_Recevable_Value.innerHTML );
       
      }
      
      
      
      
   function Calculate_Total_Payable()
      { 
        var hdn_To_Pay = document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_To_Pay');  
        var txt_Others_Payables = document.getElementById('WucAUS1_WucAUSUnloadingDetails1_txt_Others_Payables');  

        var lbl_Total_Payable_Value = document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_Total_Payable_Value');  
                
        var hdn_Total_Payable_Value = document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_Total_Payable_Value'); 
        
        var lbl_UpcountryReceivableValue=document.getElementById('WucAUS1_WucAUSUnloadingDetails1_lbl_UpcountryReceivableValue'); 
        var hdn_UpcountryReceivable= document.getElementById('WucAUS1_WucAUSUnloadingDetails1_hdn_UpcountryReceivable'); 
        
        
        
        hdn_To_Pay.value = val( hdn_To_Pay.value);
        
        txt_Others_Payables.value = val( txt_Others_Payables.value);
        
        lbl_Total_Payable_Value.innerHTML =  val( hdn_To_Pay.value) + val( txt_Others_Payables.value) + val(hdn_UpcountryReceivable.value);                                                 
       
       lbl_Total_Payable_Value.innerHTML = val(lbl_Total_Payable_Value.innerHTML );
       hdn_Total_Payable_Value.value = val(lbl_Total_Payable_Value.innerHTML );
       
      }