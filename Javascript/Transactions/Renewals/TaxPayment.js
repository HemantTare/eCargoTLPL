// JScript File




function Enabled_Disabled_Controls_On_Cheque()
{    
    var txt_Cheque_No=document.getElementById('txt_Cheque_No');
    var ddl_Bank_Name=document.getElementById('ddl_Bank_Name');
    var tr_Cheque_Details=document.getElementById('tr_Cheque_Details');
    var tr_Bank_Name=document.getElementById('tr_Bank_Name');
    var radio_2=document.getElementById('rdl_Paid_By_1');
    var radio_1=document.getElementById('rdl_Paid_By_0');    
          //tr_Cheque_Details.style.visibility='visible';
          //tr_Bank_Name.style.visibility='visible';
          
        if(radio_2.checked==true)
        {
           tr_Cheque_Details.style.display = '';
           tr_Bank_Name.style.display = '';      
           radio_2.value='2';  
        }   
        else
        {
            txt_Cheque_No.value='';
            ddl_Bank_Name.selectedIndex=0;                       
            tr_Cheque_Details.style.display = 'none';
            tr_Bank_Name.style.display = 'none';
            radio_1.value='1';  
        }
         
}