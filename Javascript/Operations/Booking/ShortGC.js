
    // JScript File
        
//*******************************************************************
 
    function Allow_To_Save()
    {
        return true;
    }
    
//*******************************************************************
 
    function Allow_To_Exit()
    {   
        var ATE = false;

        if (confirm("Do you want to Exit...")==false)
        {
            ATE=false;
        }
        else
        {
            window.close();
            ATE=true;
        }       
       return ATE;
    }
    
    function Allow_To_Print()
    {   
        var ATP = false;
        
        var hdn_GCId = document.getElementById('wucShortGC1_hdn_GCId');
        var btn_Print_Details = document.getElementById('wucShortGC1_btn_Print_Details');
        
        if(val(hdn_GCId.value ) <=0)
        {
            alert("Printing Details Are Not Available."); 
            ATP=false;
            return false;
        }
        else
        {
            if (confirm("Do you want to Print...")==false)
            {
                ATP=false;
                return false;
            }
            else
            {
                ATP=true;
                return false;
            }
        }
        return ATP ;
    }
    
//*******************************************************************
 
    function On_Load()
    {
        var tr_dg_CommodityGeneral = document.getElementById('wucShortGC1_tr_dg_CommodityGeneral');
        var tr_dg_CommodityNandwana = document.getElementById('wucShortGC1_tr_dg_CommodityNandwana');
        var hdn_ClientCode = document.getElementById('wucShortGC1_hdn_ClientCode');
        var hdn_GCId = document.getElementById('wucShortGC1_hdn_GCId');
        var lnk_Get_Next_No = document.getElementById('wucShortGC1_lnk_Get_Next_No');
        var hdn_Is_Opening_GC = document.getElementById('wucShortGC1_hdn_Is_Opening_GC');
        var hdn_MenuItemId = document.getElementById('wucShortGC1_hdn_MenuItemId');
        var hdn_Rectification_GC_Id = document.getElementById('wucShortGC1_hdn_Rectification_GC_Id');
        var lbl_DocumentNextCounterNo = document.getElementById('wucShortGC1_lbl_DocumentNextCounterNo');
        var txt_GC_No_For_Print = document.getElementById('wucShortGC1_txt_GC_No_For_Print');
        var tr_ToPayCharge = document.getElementById('wucShortGC1_tr_ToPayCharge');
        var hdn_Is_ToPay_Charge_Require = document.getElementById('wucShortGC1_hdn_Is_ToPay_Charge_Require');
        var txt_ChequeNo  =  document.getElementById('wucShortGC1_txt_ChequeNo');  
        var txt_ChequeAmount= document.getElementById('wucShortGC1_txt_ChequeAmount');
        var tr_cheque_Details = document.getElementById('wucShortGC1_tr_cheque_Details');
        var hdn_Is_Consignor_Consignee_Details_Shown = document.getElementById('wucShortGC1_hdn_Is_Consignor_Consignee_Details_Shown');
        var tr_Consignor_Details = document.getElementById('wucShortGC1_tr_Consignor_Details');
        var tr_Consignee_Details = document.getElementById('wucShortGC1_tr_Consignee_Details');
        var tr_Series = document.getElementById('wucShortGC1_tr_Series');
        var tr_ddl_LengthChargeHead = document.getElementById('wucShortGC1_tr_ddl_LengthChargeHead');
        var tr_LengthCharge = document.getElementById('wucShortGC1_tr_LengthCharge');
        var tr_btn_ReadContractDetails = document.getElementById('wucShortGC1_tr_btn_ReadContractDetails');

        if (hdn_ClientCode.value == 'nandwana')
        {
            tr_dg_CommodityGeneral.style.display = 'none';
            tr_dg_CommodityNandwana.style.display = 'inline'; 
        }
        else
        {
            tr_dg_CommodityGeneral.style.display = 'inline';
            tr_dg_CommodityNandwana.style.display = 'none'; 
        }
        tr_btn_ReadContractDetails.style.display = 'none';
        Visible_Controls();
        On_FreightBasis_Change_Edit();
        On_DeliveryType_Change_Edit();
        On_BookingType_Change_Edit();     
        Convert_InTo_Feet();
        Hide_Consingor_Consignee_Details();
        On_ChequeAmount_Change();
        Set_Payment_Details();        
        On_GCRisk_Change_Edit();
        Enable_Disable_Freight_Details_Controls();
        On_PaymentType_Change_Edit();
        Calculate_GrandTotal_Edit();
        Set_Control_Attributes();
        ddl_LengthChargeHead_Change();
        Hide_Controls_For_Opening_GC();
        Hide_Controls_For_Short_GC();        
        Disable_Control_As_Company_GC_Parameter();

        if (val(hdn_GCId.value) > 0)
        {
            lnk_Get_Next_No.style.visibility = 'hidden';
            txt_GC_No_For_Print.disabled = true;
        }
        else if (( hdn_Is_Opening_GC.value != 'True' || hdn_MenuItemId.value != 200 ) &&  hdn_Rectification_GC_Id.value <= 0 )
        {
           lnk_Get_Next_No.style.visibility = 'visible';
           lbl_DocumentNextCounterNo.style.visibility = 'visible';
        }
        
        If_Rectification();

        if ( val(txt_ChequeAmount.value) >0)
            tr_cheque_Details.style.display = 'inline';
        else
            tr_cheque_Details.style.display = 'none';
               
        Enable_Disable_Freight_Details_Controls();
        
        tr_Series.style.display = 'none';
        tr_ddl_LengthChargeHead.style.display = 'none';
        tr_LengthCharge.style.display = 'none';        
        
        if (hdn_Is_ToPay_Charge_Require.value != 'True')
        {        
            tr_ToPayCharge.style.display = 'none';
        }        
        On_View();

        if( hdn_Is_Consignor_Consignee_Details_Shown.value == 'True')
        {
            tr_Consignor_Details.style.display = 'inline';
            tr_Consignee_Details.style.display = 'inline';
        }
        else
        {
            tr_Consignor_Details.style.display = 'none';
            tr_Consignee_Details.style.display = 'none';
        }
    }
    
    function If_Rectification()
    {
       var hdn_MenuItemId = document.getElementById('wucShortGC1_hdn_MenuItemId');
       var hdn_Rectification_GC_Id = document.getElementById('wucShortGC1_hdn_Rectification_GC_Id');
       var ddl_FromLocation = document.getElementById('wucShortGC1_ddl_FromLocation_txtBoxddl_FromLocation');
       var ddl_ToLocation = document.getElementById('wucShortGC1_ddl_ToLocation_txtBoxddl_ToLocation');
       var ddl_Consignor = document.getElementById('wucShortGC1_ddl_Consignor_txtBoxddl_Consignor');
       var ddl_Consignee = document.getElementById('wucShortGC1_ddl_Consignee_txtBoxddl_Consignee');
       var lnk_View_Details = document.getElementById('wucShortGC1_lnk_View_Details');
       
       if (hdn_MenuItemId.value == 194 )//|| hdn_Rectification_GC_Id.value > 0 )
       {            
            ddl_FromLocation.disabled = true;
            ddl_ToLocation.disabled = true;
            ddl_Consignor.disabled = true; 
            ddl_Consignee.disabled = true;
            lnk_View_Details.style.visibility = 'visible'; 
       }
       else
       {
         lnk_View_Details.style.visibility = 'hidden'; 
       }
    } 
    
    function Hide_Controls_For_Opening_GC()
    {    
        var tr_OpeningGC = document.getElementById('wucShortGC1_tr_OpeningGC');
        var hdn_Is_Opening_GC = document.getElementById('wucShortGC1_hdn_Is_Opening_GC');
        var hdn_MenuItemId = document.getElementById('wucShortGC1_hdn_MenuItemId');
        var tr_Contractual_Client_Details = document.getElementById('wucShortGC1_tr_Contractual_Client_Details');
        var tr_Contract_Branch_Details = document.getElementById('wucShortGC1_tr_Contract_Branch_Details');
        var tr_Contract_Details = document.getElementById('wucShortGC1_tr_Contract_Details');
        var lnk_Get_Next_No = document.getElementById('wucShortGC1_lnk_Get_Next_No');
        var lbl_DocumentNextCounterNo = document.getElementById('wucShortGC1_lbl_DocumentNextCounterNo');
        
        if (( hdn_Is_Opening_GC.value == 'True' || hdn_MenuItemId.value == 200 ) )//&&  hdn_Rectification_GC_Id.value <= 0 )
        {
            tr_OpeningGC.style.display = 'inline';
            tr_Contractual_Client_Details.style.display = 'none'; 
            tr_Contract_Branch_Details.style.display = 'none'; 
            tr_Contract_Details.style.display = 'none';    
            lnk_Get_Next_No.style.visibility = 'hidden';        
            lbl_DocumentNextCounterNo.style.visibility =  'hidden';   
        }
        else        
        {
            tr_OpeningGC.style.display = 'none';
            tr_Contractual_Client_Details.style.display = 'inline'; 
            tr_Contract_Branch_Details.style.display = 'inline';
            tr_Contract_Details.style.display = 'inline';
            lnk_Get_Next_No.style.visibility = 'visible';   
            lbl_DocumentNextCounterNo.style.visibility = 'visible';               
        }       
    }
    
    function Hide_Controls_For_Short_GC()
    {    
        var tr_tbl_Unitofmeasurement = document.getElementById('wucShortGC1_tr_tbl_Unitofmeasurement');
        var tr_ConversionInFeet = document.getElementById('wucShortGC1_tr_ConversionInFeet');
        var tr_UnitOfMeasurment = document.getElementById('wucShortGC1_tr_UnitOfMeasurment');
        var hdn_MenuItemId = document.getElementById('wucShortGC1_hdn_MenuItemId');
        var tr_Contractual_Client_Details = document.getElementById('wucShortGC1_tr_Contractual_Client_Details');
        var tr_Contract_Branch_Details = document.getElementById('wucShortGC1_tr_Contract_Branch_Details');
        var tr_Contract_Details = document.getElementById('wucShortGC1_tr_Contract_Details');
        var lnk_Get_Next_No = document.getElementById('wucShortGC1_lnk_Get_Next_No');
        var lbl_DocumentNextCounterNo = document.getElementById('wucShortGC1_lbl_DocumentNextCounterNo');
        var tr_Instruction= document.getElementById('wucShortGC1_tr_Instruction');
        var tr_InstructionRemark = document.getElementById('wucShortGC1_tr_InstructionRemark');
        var tr_Enclosure = document.getElementById('wucShortGC1_tr_Enclosure');
        var tr_LoadingSuperVisor = document.getElementById('wucShortGC1_tr_LoadingSuperVisor');
        var txt_ActualWeight = document.getElementById('wucShortGC1_txt_ActualWeight');
        
        if (hdn_MenuItemId.value == 213 || hdn_MenuItemId.value == 200 || hdn_MenuItemId.value == 194)//&&  hdn_Rectification_GC_Id.value <= 0 ) shiv
        {
            tr_Contractual_Client_Details.style.display = 'none'; 
            tr_Contract_Branch_Details.style.display = 'none'; 
            tr_Contract_Details.style.display = 'none';    
            lnk_Get_Next_No.style.visibility = 'hidden';        
            lbl_DocumentNextCounterNo.style.visibility =  'hidden';
            tr_tbl_Unitofmeasurement.style.display = 'none';     
            tr_ConversionInFeet.style.display = 'none';     
            tr_UnitOfMeasurment.style.display = 'none';            
            tr_Instruction.style.display = 'none';    
            tr_InstructionRemark.style.display = 'none';    
            tr_Enclosure.style.display = 'none';    
            tr_LoadingSuperVisor.style.display = 'none';    
            txt_ActualWeight.style.visibility =  'hidden';            
        }
        else        
        {
            tr_Contractual_Client_Details.style.display = 'inline'; 
            tr_Contract_Branch_Details.style.display = 'inline';
            tr_Contract_Details.style.display = 'inline';            
            lnk_Get_Next_No.style.visibility = 'visible';   
            lbl_DocumentNextCounterNo.style.visibility = 'visible';            
            tr_tbl_Unitofmeasurement.style.display = 'inline';     
            tr_ConversionInFeet.style.display = 'inline';     
            tr_UnitOfMeasurment.style.display = 'inline';            
            tr_Instruction.style.display = 'inline';    
            tr_InstructionRemark.style.display = 'inline';    
            tr_Enclosure.style.display = 'inline';    
            tr_LoadingSuperVisor.style.display = 'inline';
            txt_ActualWeight.style.visibility = 'visible';    
        }       
    }
    
//*******************************************************************
 
    function Disable_Control_As_Company_GC_Parameter()
    {
        var chk_PodRequire = document.getElementById('wucShortGC1_chk_PodRequire');        
        var hdn_Is_POD_Disabled = document.getElementById('wucShortGC1_hdn_Is_POD_Disabled');
        
        if (hdn_Is_POD_Disabled.value == 'True')
        {
            chk_PodRequire.disabled = true
        }
        else
        {
            chk_PodRequire.disabled = false;
        }    
    }
 
    function Set_Control_Attributes()
    {
        var ddl_ServiceTaxPayableBy = document.getElementById('wucShortGC1_ddl_ServiceTaxPayableBy');
        ddl_ServiceTaxPayableBy.disabled = true;

        var hdn_GCId = document.getElementById('wucShortGC1_hdn_GCId');
        var txt_GC_No_For_Print = document.getElementById('wucShortGC1_txt_GC_No_For_Print');
        var ddl_GC_No = document.getElementById('wucShortGC1_ddl_GC_No');
        var hdn_Is_GCNumberEditable = document.getElementById('wucShortGC1_hdn_Is_GCNumberEditable');
        var hdn_Is_Opening_GC = document.getElementById('wucShortGC1_hdn_Is_Opening_GC');
        var hdn_MenuItemId = document.getElementById('wucShortGC1_hdn_MenuItemId');        
        var tr_InsurenceDetails  = document.getElementById('wucShortGC1_tr_InsurenceDetails');
        tr_InsurenceDetails.style.display = 'none';   
        var tbl_InsurenceDetails  = document.getElementById('wucShortGC1_tbl_InsurenceDetails');
        tbl_InsurenceDetails.style.display = 'none';   
        var txt_ChequeAmount = document.getElementById('wucShortGC1_txt_ChequeAmount');
        var hdn_BankName= document.getElementById('wucShortGC1_hdn_BankName');
        var txt_BankName= document.getElementById('wucShortGC1_txt_BankName');
        var txt_ChequeNo  =  document.getElementById('wucShortGC1_txt_ChequeNo');  
        var hdn_ChequeNo= document.getElementById('wucShortGC1_hdn_ChequeNo');

        if ( hdn_Is_GCNumberEditable.value == "True" || hdn_Is_Opening_GC.value == 'True' || hdn_MenuItemId.value == 200)
        {
            txt_GC_No_For_Print.disabled = false;
        }
        else
        {
            txt_GC_No_For_Print.disabled = true;
        }        
       
        if (val(hdn_GCId.value) > 0)
        {
            txt_GC_No_For_Print.disabled = true;
        }
        if ((hdn_Is_Opening_GC.value == 'True' || hdn_MenuItemId.value == 200) && val(hdn_GCId.value) > 0)
        {
           ddl_GC_No.disabled = true;
        }
            
        if (val(txt_ChequeAmount.value) <= 0)
        {  
            txt_BankName.value = '';
            txt_ChequeNo.value = '';
            hdn_ChequeNo.value = '';
            txt_BankName.disabled = true;
            txt_ChequeNo.disabled = true;
        }
        else
        {
            txt_BankName.disabled = false;
            txt_ChequeNo.disabled = false;
        }        
    }
    
//*******************************************************************
 
    function Set_Links_Rights()
    {
        var hdn_Can_Add_Location = document.getElementById('wucShortGC1_hdn_Can_Add_Location');
        var hdn_Can_Add_Consignor = document.getElementById('wucShortGC1_hdn_Can_Add_Consignor');
        var hdn_Can_Edit_Consignor = document.getElementById('wucShortGC1_hdn_Can_Edit_Consignor');
        var hdn_Can_View_Consignor = document.getElementById('wucShortGC1_hdn_Can_View_Consignor');
        var hdn_Can_Add_Consignee = document.getElementById('wucShortGC1_hdn_Can_Add_Consignee');
        var hdn_Can_Edit_Consignee = document.getElementById('wucShortGC1_hdn_Can_Edit_Consignee');
        var hdn_Can_View_Consignee = document.getElementById('wucShortGC1_hdn_Can_View_Consignee');
        var hdn_Can_Add_Commodity = document.getElementById('wucShortGC1_hdn_Can_Add_Commodity');
        var hdn_Can_Add_Item = document.getElementById('wucShortGC1_hdn_Can_Add_Item');
        var lnk_AddFromServiceLocation = document.getElementById('wucShortGC1_lnk_AddFromServiceLocation');
        var lnk_AddToServiceLocation = document.getElementById('wucShortGC1_lnk_AddToServiceLocation');
        var tr_AddLocation = document.getElementById('wucShortGC1_tr_AddLocation');
        var lnk_NewConsignor = document.getElementById('wucShortGC1_lnk_NewConsignor');
        var lnk_NewConsignee = document.getElementById('wucShortGC1_lnk_NewConsignee');
        var lnk_EditConsignor = document.getElementById('wucShortGC1_lnk_EditConsignor');
        var lnk_ViewConsignor = document.getElementById('wucShortGC1_lnk_ViewConsignor');
        var lnk_EditConsignee = document.getElementById('wucShortGC1_lnk_EditConsignee');
        var lnk_ViewConsignee = document.getElementById('wucShortGC1_lnk_ViewConsignee');
        var lnk_AddCommodity = document.getElementById('wucShortGC1_lnk_AddCommodity');
        var lnk_AddItem = document.getElementById('wucShortGC1_lnk_AddItem');
        var tr_Add_Commodity_Item = document.getElementById('wucShortGC1_tr_Add_Commodity_Item');                    
        var hdn_Mode = document.getElementById('wucShortGC1_hdn_Mode'); 
                
        if ( hdn_Can_Add_Location.value == "True" && val( hdn_Mode.value ) != 4)
        {
            lnk_AddFromServiceLocation.style.visibility = 'visible'; 
        }
        else
        {
            lnk_AddFromServiceLocation.style.visibility = 'hidden'; 
        }
        
        if ( hdn_Can_Add_Consignor.value == "True" && val( hdn_Mode.value ) != 4)
        {
            lnk_NewConsignor.style.visibility = 'visible'; 
            lnk_NewConsignee.style.visibility = 'visible';
        }
        else
        {
            lnk_NewConsignor.style.visibility = 'hidden';             
            lnk_NewConsignee.style.visibility = 'hidden';
        }
        
        if ( hdn_Can_Edit_Consignor.value == "True" && val( hdn_Mode.value ) != 4)
        {            
            lnk_EditConsignor.style.visibility = 'visible';  
            lnk_EditConsignee.style.visibility = 'visible';              
        }
        else
        {            
            lnk_EditConsignor.style.visibility = 'hidden'; 
            lnk_EditConsignee.style.visibility = 'hidden'; 
        }        
        
        if ( hdn_Can_View_Consignor.value == "True" && val( hdn_Mode.value ) != 4)
        {            
            lnk_ViewConsignor.style.visibility = 'visible';  
            lnk_ViewConsignee.style.visibility = 'visible';              
        }
        else
        {            
            lnk_ViewConsignor.style.visibility = 'hidden'; 
            lnk_ViewConsignee.style.visibility = 'hidden'; 
        }        
        
        if ( hdn_Can_Add_Commodity.value == "True" && val( hdn_Mode.value ) != 4)
        {
            lnk_AddCommodity.style.visibility = 'visible'; 
        }
        else
        {
            lnk_AddCommodity.style.visibility = 'hidden'; 
        }        
        
        if ( hdn_Can_Add_Item.value == "True" && val( hdn_Mode.value ) != 4)
        {
            lnk_AddItem.style.visibility = 'visible'; 
        }
        else
        {
            lnk_AddItem.style.visibility = 'hidden'; 
        }
    }
    
//*******************************************************************
 
    function Hide_Consingor_Consignee_Details()
    {
        var tr_Consingor_Consignee_Details = document.getElementById('wucShortGC1_tr_Consingor_Consignee_Details');
        var tbl_Consingor_Consignee_Details = document.getElementById('wucShortGC1_tbl_Consingor_Consignee_Details');
        var tr_Hidden_Fields = document.getElementById('wucShortGC1_tr_Hidden_Fields');
        var tr_Hidden_Fields1 = document.getElementById('wucShortGC1_tr_Hidden_Fields1');
        var tr_Hidden_Consignor_Details = document.getElementById('wucShortGC1_tr_Hidden_Consignor_Details');
        var tr_Hidden_Consignee_Details = document.getElementById('wucShortGC1_tr_Hidden_Consignee_Details');
        
        tr_Consingor_Consignee_Details.style.display = 'none';   
        tbl_Consingor_Consignee_Details.style.display = 'none';
        tr_Hidden_Fields.style.display = 'none';
        tr_Hidden_Fields1.style.display = 'none';
        tr_Hidden_Consignor_Details.style.display = 'none';
        tr_Hidden_Consignee_Details.style.display = 'none';
    }

////*******************************************************************

    function Visible_Controls()
    {
        var tr_DACC = document.getElementById('wucShortGC1_tr_DACC');
        tr_DACC.style.visibility = 'hidden';
        tr_DACC.style.display = 'none';
    }

    function View_GC_Details(Call_From)
    {
        var txt_ReBooked_GC_No_For_Print = document.getElementById('WucReBookGC1_txt_GC_No_For_Print');        
        var lbl_Errors = document.getElementById('WucReBookGC1_lbl_Errors');        
        var hdn_ReBook_GC_ID = document.getElementById('WucReBookGC1_hdn_ReBook_GC_ID');        
        var hdn_Encrepted_Rectification_GC_Id = document.getElementById('wucShortGC1_hdn_Encrepted_Rectification_GC_Id');
        var hdn_Rectification_GC_ID = document.getElementById('wucShortGC1_hdn_Rectification_GC_ID');
        var txt_Rectification_GC_No_For_Print = document.getElementById('wucShortGC1_txt_GC_No_For_Print');
        
        var hdn_Encrepted_Rectification_GC_Id = document.getElementById('wucShortGC1_hdn_Encrepted_Rectification_GC_Id');        
        var GC_No_For_Print = '';

        if (Call_From == 'From ReBook')
        {
            GC_No_For_Print = txt_ReBooked_GC_No_For_Print.value;
        }
        else
        {
            GC_No_For_Print = txt_Rectification_GC_No_For_Print.value;
        }
        
        if ( GC_No_For_Print == '' )
        {         
            lbl_Errors.innerHTML = "Please Enter GC No.";
        }
        else
        {        
            var Path= '';
                          
            if (Call_From == 'From ReBook')//MQA
            {
                Path='FrmShortGC.aspx?Menu_Item_Id=MQA4ADQA&Mode=MQA=&ReBook_GC_ID='+ val(hdn_ReBook_GC_ID.value) + 
                                     '&Rectification_GC_ID=0&ReBook_GC_No_For_Print=' + GC_No_For_Print + 
                                     '&Rectification_GC_No_For_Print=0&Id='+ val(hdn_ReBook_GC_ID.value) ;
            }
            else
            {            
                Path='FrmShortGC.aspx?Menu_Item_Id=MQA5ADQA&Mode=NAA=&ReBook_GC_ID=0&Rectification_GC_ID='+ val(hdn_Rectification_GC_ID.value) + 
                                        '&ReBook_GC_No_For_Print=0&Rectification_GC_No_For_Print='+ GC_No_For_Print +
                                        '&Id='+ hdn_Encrepted_Rectification_GC_Id.value ;                                     
            }  
           
            w = screen.availWidth;
            h = screen.availHeight;
            var popW = (w-100);
            var popH = h-40;
            var leftPos = (w-popW)/2;
            var topPos = 0;

            window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',, menubar=no, resizable=no,scrollbars=yes,color=blue');      
        }        
       return false;
    }    
//*******************************************************************
 
    function On_View()
    {    
        var hdn_Mode = document.getElementById('wucShortGC1_hdn_Mode'); 
        var lnk_RequireForms = document.getElementById('wucShortGC1_lnk_RequireForms');
        var lnk_Get_Next_No = document.getElementById('wucShortGC1_lnk_Get_Next_No'); 
        var lnk_AddFromServiceLocation = document.getElementById('wucShortGC1_lnk_AddFromServiceLocation'); 
        var lnk_AddToServiceLocation = document.getElementById('wucShortGC1_lnk_AddToServiceLocation'); 
        var lnk_NewConsignor = document.getElementById('wucShortGC1_lnk_NewConsignor'); 
        var lnk_EditConsignor = document.getElementById('wucShortGC1_lnk_EditConsignor');     
        var lnk_NewConsignee = document.getElementById('wucShortGC1_lnk_NewConsignee'); 
        var lnk_EditConsignee = document.getElementById('wucShortGC1_lnk_EditConsignee'); 
        var lnk_AddCommodity  = document.getElementById('wucShortGC1_lnk_AddCommodity');              
        var lnk_AddItem = document.getElementById('wucShortGC1_lnk_AddItem'); 
        var lnk_Change_Door_Delivery_Address = document.getElementById('wucShortGC1_lnk_Change_Door_Delivery_Address'); 
        var dg_Commodity = document.getElementById('wucShortGC1_dg_Commodity');
        var dg_Invoice = document.getElementById('wucShortGC1_dg_Invoice');
        var chk_IsInsured = document.getElementById('wucShortGC1_chk_IsInsured');
        var lnk_View_Details = document.getElementById('wucShortGC1_lnk_View_Details');

        var Enable = true;
            
        if (val( hdn_Mode.value ) == 4)
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
                }
            }
      
            lnk_Get_Next_No.style.visibility = 'hidden'; 
            lnk_View_Details.style.visibility = 'hidden';             
            
            lnk_AddFromServiceLocation.style.visibility = 'hidden'; 
            lnk_AddToServiceLocation.style.visibility = 'hidden'; 
            
            lnk_NewConsignor.style.visibility = 'hidden';      
            lnk_NewConsignee.style.visibility = 'hidden';      
            
            lnk_EditConsignee.style.visibility = 'hidden';     
            lnk_EditConsignor.style.visibility = 'hidden';  
            
            lnk_AddCommodity.style.visibility = 'hidden';  
            lnk_AddItem.style.visibility = 'hidden';               
            
            dg_Commodity.disabled = Enable;
            dg_Invoice.disabled = Enable;
                   
            if ( chk_IsInsured.checked == true )
            {
                chk_IsInsured.disabled = !Enable;
            }
        }  
    }

//*******************************************************************
    function On_IsInsured_Click()
    {
        var chk_IsInsured = document.getElementById('wucShortGC1_chk_IsInsured');
        var hdn_Mode = document.getElementById('wucShortGC1_hdn_Mode');        
        var ddl_GCRisk = document.getElementById('wucShortGC1_ddl_GCRisk');
        
        if (chk_IsInsured.checked == true )     
        {                        
            var Path='FrmGCInsurenceDetails.aspx?Mode=' + hdn_Mode.value + '&GCRiskId=' + ddl_GCRisk.value   ;
            w = screen.availWidth;
            h = screen.availHeight;
            var popW = 700, popH = 300;
            var leftPos = (w-popW)/2;
            var topPos = (h-popH)/2;
            
            window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');
        }
         Calculate_FOV();
         Calculate_GrandTotal();
         On_CashAmount_Change();
         Set_Payment_Details();
    }    

//*******************************************************************
    
    function roundNumber(num, dec) 
    {
      if (isNaN(num) || num == 0 || num == '') num = '0.00';
	    var result = Math.round(num*Math.pow(10,dec))/Math.pow(10,dec);
	    return 		parseFloat(result).toFixed(2);
    }
    
//*******************************************************************
    
    function On_BookingType_Change()
    {   
        var ddl_BookingType = document.getElementById('wucShortGC1_ddl_BookingType');         
        var ddl_DeliveryType= document.getElementById('wucShortGC1_ddl_DeliveryType');          
        var hdn_Container_Details_RequiredFor_BookingType = document.getElementById('wucShortGC1_hdn_Container_Details_RequiredFor_BookingType');
        var tr_ContainerDetails = document.getElementById('wucShortGC1_tr_ContainerDetails');
        var arr = hdn_Container_Details_RequiredFor_BookingType.value.split(",");
                
        tr_ContainerDetails.style.display = 'none'; 
        
        if ( arr[0] == 'all')
        {
            tr_ContainerDetails.style.display = 'inline';
        }
        else
        {
            for(i = 0; i < arr.length; i++) 
            {
                if(arr[i] == ddl_BookingType.value)
                {
                    tr_ContainerDetails.style.display = 'inline';
                    break;
                } 
            }        
        }
             
        if (ddl_BookingType.value == 1)  // If Sundry
        {
            ddl_DeliveryType.value = 1;
            On_DeliveryType_Change();
        }
    }
 
//*******************************************************************
    
    function On_BookingType_Change_Edit()
    {    
        var hdn_Container_Details_RequiredFor_BookingType = document.getElementById('wucShortGC1_hdn_Container_Details_RequiredFor_BookingType');
        var tr_ContainerDetails = document.getElementById('wucShortGC1_tr_ContainerDetails'); 
        var ddl_BookingType = document.getElementById('wucShortGC1_ddl_BookingType'); 

        var arr = hdn_Container_Details_RequiredFor_BookingType.value.split(",");

        tr_ContainerDetails.style.display = 'none'; 
        
        if ( arr[0] == 'all')
        {
            tr_ContainerDetails.style.display = 'inline';
        }
        else
        {
            for(i = 0; i < arr.length; i++) 
            {
                if(arr[i] == ddl_BookingType.value)
                {
                    tr_ContainerDetails.style.display = 'inline';
                    break;
                } 
            }
        }
    }        
//*******************************************************************
 
    function On_VolumetricToKgFactor_Change()
    {                      
         var hdn_Applicable_Standard_CFTFactor = document.getElementById('wucShortGC1_hdn_Applicable_Standard_CFTFactor');
         var txt_VolumetricToKgFactor = document.getElementById('wucShortGC1_txt_VolumetricToKgFactor');
         var hdn_VolumetricToKgFactor = document.getElementById('wucShortGC1_hdn_VolumetricToKgFactor');

         txt_VolumetricToKgFactor.value =  val(txt_VolumetricToKgFactor.value);
         
         if ( val(txt_VolumetricToKgFactor.value) < val( hdn_Applicable_Standard_CFTFactor.value))  
          {
              txt_VolumetricToKgFactor.value = val( hdn_Applicable_Standard_CFTFactor.value);
          }         
          
          hdn_VolumetricToKgFactor.value = val(txt_VolumetricToKgFactor.value);
          
          Calculate_ChargeWeight();
          Calculate_Freight('0');
          Calculate_GrandTotal();
          On_CashAmount_Change();
          Set_Payment_Details();
    }
    
//*******************************************************************
 
    function ddl_VehicleType_Change()
    {    
        var hdn_Contract_UnitOfFreightId = document.getElementById('wucShortGC1_hdn_Contract_UnitOfFreightId');
        var hdn_Contract_FreightBasisId = document.getElementById('wucShortGC1_hdn_Contract_FreightBasisId');
        var hdn_Contract_FreightSubUnitId = document.getElementById('wucShortGC1_hdn_Contract_FreightSubUnitId');
        var hdn_IsContractApplied = document.getElementById('wucShortGC1_hdn_IsContractApplied');
        var ddl_Contract = document.getElementById('wucShortGC1_ddl_Contract');
        var ddl_PaymentType = document.getElementById('wucShortGC1_ddl_PaymentType');

        if (( hdn_Contract_UnitOfFreightId.value == 1 ||  // for vehicle 
            hdn_Contract_FreightSubUnitId.value ==  5 ||  // for vehicle  
            hdn_Contract_FreightBasisId.value ==  2 ||  // // for Kilo Meter
            hdn_Contract_FreightBasisId.value ==  3 ||  // // for Transit Days
            hdn_Contract_UnitOfFreightId.value == 5)   // for Km
                && ddl_Contract.value > 0 )
        {
            hdn_IsContractApplied.value = "0";
            Get_ContractCharges();
            Enable_Disable_Freight_Details_Controls();
        }
       
        if (ddl_PaymentType.value != 5) // foc
        {
            Enable_Disable_Freight_Details_Controls();   
        }
         Get_BasisFreight();
    }
    
//*******************************************************************

    function Enable_Disable_Freight_Details_Controls()
    {         
        var txt_FreightRate = document.getElementById('wucShortGC1_txt_FreightRate');
        var txt_Freight = document.getElementById('wucShortGC1_txt_Freight');
        var txt_LocalCharge = document.getElementById('wucShortGC1_txt_LocalCharge');
        var txt_LoadingCharge = document.getElementById('wucShortGC1_txt_LoadingCharge');
        var txt_StationaryCharge = document.getElementById('wucShortGC1_txt_StationaryCharge');
        var txt_ToPayCharge = document.getElementById('wucShortGC1_txt_ToPayCharge');
        var txt_DDCharge = document.getElementById('wucShortGC1_txt_DDCharge');
        var lbl_OtherChargesValue = document.getElementById('wucShortGC1_lbl_OtherChargesValue');
        var txt_FOVRiskCharge = document.getElementById('wucShortGC1_txt_FOVRiskCharge');        
        var txt_ReBookGCAmount = document.getElementById('wucShortGC1_txt_ReBookGCAmount');
        var hdn_ReBookGCAmount = document.getElementById('wucShortGC1_hdn_ReBookGCAmount');
        var txt_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_txt_ReBookGC_OctroiAmount');
        var hdn_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_hdn_ReBookGC_OctroiAmount');
        var txt_LengthCharge = document.getElementById('wucShortGC1_txt_LengthCharge');  
        var hdn_LengthCharge = document.getElementById('wucShortGC1_hdn_LengthCharge');          
        var txt_VolumetricToKgFactor = document.getElementById('wucShortGC1_txt_VolumetricToKgFactor');       
        var hdn_IsContractApplied = document.getElementById('wucShortGC1_hdn_IsContractApplied');
        var hdn_Contract_UnitOfFreightId = document.getElementById('wucShortGC1_hdn_Contract_UnitOfFreightId');
        var ddl_FreightBasis = document.getElementById('wucShortGC1_ddl_FreightBasis');        
        var ddl_PaymentType= document.getElementById('wucShortGC1_ddl_PaymentType');  
        var ddl_DeliveryType= document.getElementById('wucShortGC1_ddl_DeliveryType');  
        var ddl_VolumetricFreightUnit = document.getElementById('wucShortGC1_ddl_VolumetricFreightUnit');
        var ddl_LengthChargeHead = document.getElementById('wucShortGC1_ddl_LengthChargeHead');
        var hdn_LengthChargeHeadId = document.getElementById('wucShortGC1_hdn_LengthChargeHeadId');
        var txt_UnloadingCharge = document.getElementById('wucShortGC1_txt_UnloadingCharge');
        var hdn_NFormCharge = document.getElementById('wucShortGC1_hdn_NFormCharge');
        var txt_NFormCharge = document.getElementById('wucShortGC1_txt_NFormCharge');
        
        if ( hdn_IsContractApplied.value == "1")  // if contract is applied then all the controls are disables  
        {                                                 
            txt_FreightRate.disabled = true;
            txt_Freight.disabled = true;
            txt_LocalCharge.disabled = true;
            txt_LoadingCharge.disabled = true;
            txt_StationaryCharge.disabled = true;
            txt_NFormCharge.disabled = true;
            txt_ToPayCharge.disabled = true;
            txt_DDCharge.disabled = true;
            txt_FOVRiskCharge.disabled = true;
            txt_LengthCharge.disabled = true;
            ddl_LengthChargeHead.disabled = true;
            
            if ( hdn_Contract_UnitOfFreightId.value == 3) // for cft
            {
                txt_VolumetricToKgFactor.disabled = true;
            }            
            ddl_FreightBasis.disabled = true;
            
            if ( hdn_Contract_UnitOfFreightId.value == 1 ) // for vehicle
            {
                ddl_FreightBasis.value=3; // fixed
            }
            else if ( hdn_Contract_UnitOfFreightId.value == 2 )// for weight
            {
                ddl_FreightBasis.value=1; // Weight
            }
            else if ( hdn_Contract_UnitOfFreightId.value == 3 )// for CFT
            {
                ddl_FreightBasis.value=4; // Volumetric for cft
                
                ddl_VolumetricFreightUnit.disabled = true; // set cft if selected contract on CFT
                ddl_VolumetricFreightUnit.value = 1;                
            }
            else if ( hdn_Contract_UnitOfFreightId.value == 4 )// for Article
            {
                ddl_FreightBasis.value=2; // Article
            }
        }
        else                            // if contract is Not applied then all the controls are Enable
        {            
            if ( ddl_PaymentType.value != 5)
            {            
                txt_FreightRate.disabled = false;
                txt_Freight.disabled = false;
                txt_LocalCharge.disabled = false;
                txt_LoadingCharge.disabled = false;
                txt_StationaryCharge.disabled = false;                
                txt_NFormCharge.disabled = false;
                
                if (ddl_PaymentType.value != 1)     // to pay
                {
                    txt_ToPayCharge.disabled = true;
                }
                
                if (ddl_DeliveryType.value == 2) //' for door
                {  
                    txt_DDCharge.disabled = false;            
                }
                txt_FOVRiskCharge.disabled = false;
                txt_LengthCharge.disabled = false;
                ddl_LengthChargeHead.disabled = false;               
                txt_VolumetricToKgFactor.disabled = false;            
                ddl_FreightBasis.disabled = false;
                ddl_VolumetricFreightUnit.disabled = false;                
                txt_UnloadingCharge.disabled = false;
            }
        }
    }
    
//*******************************************************************
 
    function Get_ContractCharges()
    {
        var btn_ReadContractDetails = document.getElementById('wucShortGC1_btn_ReadContractDetails');        
        btn_ReadContractDetails.click();
    }

//*******************************************************************
    function ddl_LengthChargeHead_Change()
    {
        var ddl_LengthChargeHead = document.getElementById('wucShortGC1_ddl_LengthChargeHead');
        var txt_LengthCharge = document.getElementById('wucShortGC1_txt_LengthCharge');
        var hdn_LengthCharge = document.getElementById('wucShortGC1_hdn_LengthCharge');
     
          if(ddl_LengthChargeHead.value == 0)
          {
            txt_LengthCharge.disabled = true;
            txt_LengthCharge.value =val(0);
            hdn_LengthCharge.value =val(0); 
          }
    }
        
//*******************************************************************
 
    function ddl_FreightBasis_Change()
    {        
        var ddl_FreightBasis = document.getElementById('wucShortGC1_ddl_FreightBasis');
        var ddl_VolumetricFreightUnit= document.getElementById('wucShortGC1_ddl_VolumetricFreightUnit');
        
        On_FreightBasis_Change();   
        Calculate_ChargeWeight();
        Calculate_LoadingCharge_On_ChargeWeight_Change();
        Calculate_Freight('0');
        Calculate_GrandTotal();
        On_CashAmount_Change();
        Set_Payment_Details();
//        Calculate_ChargeWeight();
//        Calculate_LoadingCharge_On_ChargeWeight_Change();
//        Calculate_Freight('0');
//        Calculate_GrandTotal(); 
//        On_CashAmount_Change();        
//        Set_Payment_Details();
          
        if (ddl_FreightBasis.value == 4 ) // for Volumetric  
        {
            ddl_VolumetricFreightUnit.value = 3; // kg
            ddl_VolumetricFreightUnit_Change();
        }
    }
        
//*******************************************************************
 
    function ddl_VolumetricFreightUnit_Change()
    {    
        On_FreightBasis_Change();   
        Calculate_ChargeWeight();
        Calculate_Freight('0');
        Calculate_GrandTotal();
        On_CashAmount_Change();        
        Set_Payment_Details();
        
//        Calculate_ChargeWeight();
//        Calculate_Freight('0');
//        Calculate_GrandTotal(); 
//        On_CashAmount_Change();//        
//        Set_Payment_Details();          
    }    
     
//*******************************************************************
  
    function Enable_Disable_All_Controls(Enable)
    {
        var dg_Commodity = document.getElementById('wucShortGC1_dg_Commodity');
        var dg_Invoice = document.getElementById('wucShortGC1_dg_Invoice');
        var txt_GC_No_For_Print = document.getElementById('wucShortGC1_txt_GC_No_For_Print');
        dg_Invoice.disabled = Enable;        
        
        for(i = 0; i < document.forms[0].elements.length; i++) 
        {        
            elm = document.forms[0].elements[i];

            if(elm.id!='')
            {
                var elm_id = document.getElementById(elm.id);

                var elm_name = elm.name;
                var arr = elm_name.split("$");                                     
                
                if (arr[1] != 'dg_Commodity' && elm.type != 'lable')
                {                    
                   elm.disabled = Enable;
                }                
            }       
        }        
        txt_GC_No_For_Print.disabled = Enable;
        Set_Control_Attributes();
    }

//*******************************************************************

    function PaymentType_Change_Confirmation()
    {  
       var hdn_OldPaymentType  = document.getElementById('wucShortGC1_hdn_OldPaymentType');
       var ddl_PaymentType = document.getElementById('wucShortGC1_ddl_PaymentType');
    
       if (ddl_PaymentType.value == 5)
       {
            if (confirm("Are you Sure.")==false)
            {
                ddl_PaymentType.value = hdn_OldPaymentType.value; 
                return ;
            }
            else
            {
                On_PaymentType_Change();
            }
        }
        else
        {
            On_PaymentType_Change();
        }
    }

//*******************************************************************
    
    function Set_Applicable_Rates()
    {    
//        var hdn_Applicable_MinimunFreightRate= document.getElementById('wucShortGC1_hdn_Applicable_MinimunFreightRate');
//        var hdn_Applicable_SpecialFreightRate= document.getElementById('wucShortGC1_hdn_Applicable_SpecialFreightRate');
//        var hdn_Applicable_FreightRate= document.getElementById('wucShortGC1_hdn_Applicable_FreightRate');
        var hdn_Applicable_Standard_BiltiCharges= document.getElementById('wucShortGC1_hdn_Applicable_Standard_BiltiCharges');
        var hdn_Applicable_Standard_DDCharge= document.getElementById('wucShortGC1_hdn_Applicable_Standard_DDCharge');
        var hdn_Applicable_Standard_DDCharge_Rate = document.getElementById('wucShortGC1_hdn_Applicable_Standard_DDCharge_Rate');
        var hdn_Applicable_Standard_FOV= document.getElementById('wucShortGC1_hdn_Applicable_Standard_FOV');
        var hdn_Applicable_Standard_FOVPercentage= document.getElementById('wucShortGC1_hdn_Applicable_Standard_FOVPercentage');
//        var hdn_Applicable_Standard_FreightAmount= document.getElementById('wucShortGC1_hdn_Applicable_Standard_FreightAmount');
        var hdn_Applicable_Standard_FreightRate= document.getElementById('wucShortGC1_hdn_Applicable_Standard_FreightRate');
        var hdn_Applicable_Standard_HamaliCharge= document.getElementById('wucShortGC1_hdn_Applicable_Standard_HamaliCharge');
        var hdn_Applicable_Standard_LocalCharge= document.getElementById('wucShortGC1_hdn_Applicable_Standard_LocalCharge');
        var hdn_Applicable_Standard_ServiceTaxAmount= document.getElementById('wucShortGC1_hdn_Applicable_Standard_ServiceTaxAmount');
        var hdn_Applicable_Standard_ToPayCharges= document.getElementById('wucShortGC1_hdn_Applicable_Standard_ToPayCharges');
        var hdn_Applicable_Standard_ServiceTaxPercent= document.getElementById('wucShortGC1_hdn_Applicable_Standard_ServiceTaxPercent');
        var hdn_Applicable_Standard_MinimumFOV= document.getElementById('wucShortGC1_hdn_Applicable_Standard_MinimumFOV');
        var hdn_Applicable_Standard_MinimumChargeWeight= document.getElementById('wucShortGC1_hdn_Applicable_Standard_MinimumChargeWeight');
        var hdn_Applicable_Standard_MinimumHamaliPerKg= document.getElementById('wucShortGC1_hdn_Applicable_StandardMinimumHamaliPerKg');
        var hdn_Applicable_Standard_MinFOV= document.getElementById('wucShortGC1_hdn_Applicable_Standard_MinFOV');
        var hdn_Applicable_Standard_HamaliPerKg= document.getElementById('wucShortGC1_hdn_Applicable_Standard_HamaliPerKg');
        var hdn_Applicable_Standard_CFTFactor= document.getElementById('wucShortGC1_hdn_Applicable_Standard_CFTFactor');
        var hdn_Applicable_Standard_DACCCharges= document.getElementById('wucShortGC1_hdn_Applicable_Standard_DACCCharges');
        
        var hdn_IsContractApplied = document.getElementById('wucShortGC1_hdn_IsContractApplied');    
        var hdn_Contractual_BiltiCharges  = document.getElementById('wucShortGC1_hdn_Contractual_BiltiCharges');
        var hdn_Contractual_DDCharge_Rate = document.getElementById('wucShortGC1_hdn_Contractual_DDCharge_Rate');
        var hdn_Contractual_DDCharge = document.getElementById('wucShortGC1_hdn_Contractual_DDCharge');
        var hdn_Contractual_FOV = document.getElementById('wucShortGC1_hdn_Contractual_FOV');
        var hdn_Contractual_FOVPercentage = document.getElementById('wucShortGC1_hdn_Contractual_FOVPercentage');
        var hdn_Contractual_FreightAmount = document.getElementById('wucShortGC1_hdn_Contractual_FreightAmount');
        var hdn_Contractual_FreightRate = document.getElementById('wucShortGC1_hdn_Contractual_FreightRate');
        var hdn_Contractual_HamaliCharge = document.getElementById('wucShortGC1_hdn_Contractual_HamaliCharge');
        var hdn_Contractual_LocalCharge_Rate = document.getElementById('wucShortGC1_hdn_Contractual_LocalCharge_Rate');
        var hdn_Contractual_LocalCharge = document.getElementById('wucShortGC1_hdn_Contractual_LocalCharge');
        var hdn_Contractual_ServiceTaxAmount = document.getElementById('wucShortGC1_hdn_Contractual_ServiceTaxAmount');
        var hdn_Contractual_ToPayCharges = document.getElementById('wucShortGC1_hdn_Contractual_ToPayCharges');
        var hdn_Contractual_ServiceTaxPercent = document.getElementById('wucShortGC1_hdn_Contractual_ServiceTaxPercent');
        var hdn_Contractual_MinimumFOV = document.getElementById('wucShortGC1_hdn_Contractual_MinimumFOV');
        var hdn_Contractual_MinimumChargeWeight = document.getElementById('wucShortGC1_hdn_Contractual_MinimumChargeWeight');
        var hdn_Contractual_MinimumHamaliPerKg = document.getElementById('wucShortGC1_hdn_Contractual_MinimumHamaliPerKg');
        var hdn_Contractual_HamaliPerKg = document.getElementById('wucShortGC1_hdn_Contractual_HamaliPerKg');
        var hdn_Contractual_CFTFactor = document.getElementById('wucShortGC1_hdn_Contractual_CFTFactor');
        var hdn_Contractual_DACCCharges = document.getElementById('wucShortGC1_hdn_Contractual_DACCCharges');
        var hdn_Contractual_Octroi_Form_Charge = document.getElementById('wucShortGC1_hdn_Contractual_Octroi_Form_Charge');
        var hdn_Contractual_Octroi_Service_Charge = document.getElementById('wucShortGC1_hdn_Contractual_Octroi_Service_Charge');
        var hdn_Contractual_GI_Charges = document.getElementById('wucShortGC1_hdn_Contractual_GI_Charges');
        var hdn_Contractual_Demurrage_Days = document.getElementById('wucShortGC1_hdn_Contractual_Demurrage_Days');
        var hdn_Contractual_Demurrage_Rate = document.getElementById('wucShortGC1_hdn_Contractual_Demurrage_Rate');
        
        var hdn_MinimunFreightRate= document.getElementById('wucShortGC1_hdn_MinimunFreightRate');
//        var hdn_Applicable_SpecialFreightRate= document.getElementById('wucShortGC1_hdn_SpecialFreightRate');
        var hdn_FreightRate= document.getElementById('wucShortGC1_hdn_FreightRate');
        var hdn_Standard_BiltiCharges= document.getElementById('wucShortGC1_hdn_Standard_BiltiCharges');
        var hdn_Standard_DDCharge= document.getElementById('wucShortGC1_hdn_Standard_DDCharge');
        var hdn_Standard_DDCharge_Rate = document.getElementById('wucShortGC1_hdn_Standard_DDCharge_Rate');
        var hdn_Standard_FOV= document.getElementById('wucShortGC1_hdn_Standard_FOV');
        var hdn_Standard_FOVPercentage= document.getElementById('wucShortGC1_hdn_Standard_FOVPercentage');
        var hdn_Standard_FreightAmount= document.getElementById('wucShortGC1_hdn_Standard_FreightAmount');
        var hdn_Standard_FreightRate= document.getElementById('wucShortGC1_hdn_Standard_FreightRate');
        var hdn_Standard_HamaliCharge= document.getElementById('wucShortGC1_hdn_Standard_HamaliCharge');
        var hdn_Standard_LocalCharge= document.getElementById('wucShortGC1_hdn_Standard_LocalCharge');
        var hdn_Standard_ServiceTaxAmount= document.getElementById('wucShortGC1_hdn_Standard_ServiceTaxAmount');
        var hdn_Standard_ToPayCharges= document.getElementById('wucShortGC1_hdn_Standard_ToPayCharges');
        var hdn_Standard_ServiceTaxPercent= document.getElementById('wucShortGC1_hdn_Standard_ServiceTaxPercent');

        var hdn_Standard_MinimumFOV= document.getElementById('wucShortGC1_hdn_Standard_MinimumFOV');
        var hdn_Standard_MinimumChargeWeight= document.getElementById('wucShortGC1_hdn_Standard_MinimumChargeWeight');
        var hdn_Standard_MinimumHamaliPerKg= document.getElementById('wucShortGC1_hdn_StandardMinimumHamaliPerKg');
        var hdn_Standard_MinFOV= document.getElementById('wucShortGC1_hdn_Standard_MinFOV');
        var hdn_Standard_HamaliPerKg= document.getElementById('wucShortGC1_hdn_Standard_HamaliPerKg');
        var hdn_Standard_CFTFactor= document.getElementById('wucShortGC1_hdn_Standard_CFTFactor');
        var hdn_Standard_DACCCharges= document.getElementById('wucShortGC1_hdn_Standard_DACCCharges');
                
        if ( hdn_IsContractApplied.value != 1)
        {
            hdn_Applicable_Standard_BiltiCharges.value =  val( hdn_Standard_BiltiCharges.value ) ; 
            hdn_Applicable_Standard_DDCharge.value =  val( hdn_Standard_DDCharge.value ) ; 
            hdn_Applicable_Standard_DDCharge_Rate.value =  val( hdn_Standard_DDCharge_Rate.value ) ; 
            hdn_Applicable_Standard_FOV.value =  val( hdn_Standard_FOV.value ) ; 
            hdn_Applicable_Standard_FOVPercentage.value =  val( hdn_Standard_FOVPercentage.value ) ; 
            hdn_Applicable_Standard_FreightRate.value  =  val( hdn_Standard_FreightRate.value ) ;
            hdn_Applicable_Standard_HamaliCharge.value =  val( hdn_Standard_HamaliCharge.value ) ; 
            hdn_Applicable_Standard_LocalCharge.value =  val( hdn_Standard_LocalCharge.value ) ; 
            hdn_Applicable_Standard_ToPayCharges.value =  val( hdn_Standard_ToPayCharges.value ) ; 
            hdn_Applicable_Standard_ServiceTaxPercent.value =  val( hdn_Standard_ServiceTaxPercent.value ) ; 
            hdn_Applicable_Standard_MinimumChargeWeight.value =  val( hdn_Standard_MinimumChargeWeight.value ) ; 
            hdn_Applicable_Standard_HamaliPerKg.value =  val( hdn_Standard_HamaliPerKg.value ) ; 
            hdn_Applicable_Standard_CFTFactor.value =   val( hdn_Standard_CFTFactor.value ) ; 
            hdn_Applicable_Standard_DACCCharges.value =  val( hdn_Standard_DACCCharges.value ) ; 
        }
        else
        {
            hdn_Applicable_Standard_BiltiCharges.value =  val( hdn_Contractual_BiltiCharges.value ); 
            hdn_Applicable_Standard_DDCharge.value =  val(  hdn_Contractual_DDCharge.value ); 
            hdn_Applicable_Standard_DDCharge_Rate.value =  val(  hdn_Contractual_DDCharge_Rate.value ); 
            hdn_Applicable_Standard_FOV.value =  val(  hdn_Contractual_FOV.value ); 
            hdn_Applicable_Standard_FOVPercentage.value =  val(  hdn_Contractual_FOVPercentage.value ); 
            hdn_Applicable_Standard_FreightRate.value =  val(  hdn_Contractual_FreightRate.value ); 
            hdn_Applicable_Standard_HamaliCharge.value =  val(  hdn_Contractual_HamaliCharge.value ); 
            hdn_Applicable_Standard_LocalCharge.value =  val(  hdn_Contractual_LocalCharge.value ); 
            hdn_Applicable_Standard_ServiceTaxAmount.value =  val(  hdn_Contractual_ServiceTaxAmount.value ); 
            hdn_Applicable_Standard_ToPayCharges.value =  val(  hdn_Contractual_ToPayCharges.value ); 
            hdn_Applicable_Standard_ServiceTaxPercent.value =  val(  hdn_Contractual_ServiceTaxPercent.value ); 
            hdn_Applicable_Standard_MinimumFOV.value =  val(  hdn_Contractual_MinimumFOV.value ); 
            hdn_Applicable_Standard_MinimumChargeWeight.value =  val(  hdn_Contractual_MinimumChargeWeight.value ); 
            hdn_Applicable_Standard_MinimumHamaliPerKg.value =  val(  hdn_Contractual_HamaliPerKg.value ); 
            hdn_Applicable_Standard_MinFOV.value =  val(  hdn_Contractual_MinimumFOV.value ); 
            hdn_Applicable_Standard_HamaliPerKg.value =  val(  hdn_Contractual_HamaliPerKg.value ); 
            hdn_Applicable_Standard_CFTFactor.value =  val(  hdn_Contractual_CFTFactor.value ); 
            hdn_Applicable_Standard_DACCCharges.value =  val(  hdn_Contractual_DACCCharges.value ); 
        }
    }
    
//*******************************************************************
    
    function On_PaymentType_Change()
    {   
        var hdn_OldPaymentType  = document.getElementById('wucShortGC1_hdn_OldPaymentType');        
        var tr_cheque_Details = document.getElementById('wucShortGC1_tr_cheque_Details');        
        var ddl_PaymentType = document.getElementById('wucShortGC1_ddl_PaymentType');
        var txt_FreightRate = document.getElementById('wucShortGC1_txt_FreightRate');
        var txt_Freight = document.getElementById('wucShortGC1_txt_Freight');
        var hdn_Freight = document.getElementById('wucShortGC1_hdn_Freight');
        var txt_LocalCharge = document.getElementById('wucShortGC1_txt_LocalCharge');
        var hdn_LocalCharge = document.getElementById('wucShortGC1_hdn_LocalCharge');
        var txt_LoadingCharge = document.getElementById('wucShortGC1_txt_LoadingCharge');
        var hdn_LoadingCharge = document.getElementById('wucShortGC1_hdn_LoadingCharge');
        var txt_StationaryCharge = document.getElementById('wucShortGC1_txt_StationaryCharge');
        var hdn_StationaryCharge = document.getElementById('wucShortGC1_hdn_StationaryCharge');
        var txt_ToPayCharge = document.getElementById('wucShortGC1_txt_ToPayCharge');
        var hdn_ToPayCharge = document.getElementById('wucShortGC1_hdn_ToPayCharge');
        var txt_DDCharge = document.getElementById('wucShortGC1_txt_DDCharge');
        var hdn_DDCharge = document.getElementById('wucShortGC1_hdn_DDCharge');
        var lbl_OtherChargesValue = document.getElementById('wucShortGC1_lbl_OtherChargesValue');
        var hdn_OtherCharge = document.getElementById('wucShortGC1_hdn_OtherCharge');
        var txt_FOVRiskCharge = document.getElementById('wucShortGC1_txt_FOVRiskCharge');
        var hdn_FOVRiskCharge = document.getElementById('wucShortGC1_hdn_FOVRiskCharge');        
        var txt_ReBookGCAmount = document.getElementById('wucShortGC1_txt_ReBookGCAmount');        
        var hdn_ReBookGCAmount = document.getElementById('wucShortGC1_hdn_ReBookGCAmount');
        var txt_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_txt_ReBookGC_OctroiAmount');
        var hdn_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_hdn_ReBookGC_OctroiAmount');
        var txt_Advance = document.getElementById('wucShortGC1_txt_Advance');
        var hdn_Advance = document.getElementById('wucShortGC1_hdn_Advance');        
        var txt_CashAmount = document.getElementById('wucShortGC1_txt_CashAmount');
        var hdn_CashAmount = document.getElementById('wucShortGC1_hdn_CashAmount');        
        var txt_ChequeAmount = document.getElementById('wucShortGC1_txt_ChequeAmount');
        var hdn_ChequeAmount = document.getElementById('wucShortGC1_hdn_ChequeAmount');
        var txt_ChequeNo = document.getElementById('wucShortGC1_txt_ChequeNo');
        var hdn_ChequeNo = document.getElementById('wucShortGC1_hdn_ChequeNo');
        var txt_BankName = document.getElementById('wucShortGC1_txt_BankName');
        var hdn_BankName = document.getElementById('wucShortGC1_hdn_BankName');        
        var lbl_ChequeDate = document.getElementById('wucShortGC1_lbl_ChequeDate');
        var lbl_Bank = document.getElementById('wucShortGC1_lbl_Bank');
        var lbl_Advance = document.getElementById('wucShortGC1_lbl_Advance');
        var lbl_CashAmount = document.getElementById('wucShortGC1_lbl_CashAmount');
        var lbl_ChequeAmount = document.getElementById('wucShortGC1_lbl_ChequeAmount');
        var lbl_ChequeNo= document.getElementById('wucShortGC1_lbl_ChequeNo');        

        var hdn_IsServiceTaxApplicableForConsignor = document.getElementById('wucShortGC1_hdn_IsServiceTaxApplicableForConsignor');
        var hdn_IsServiceTaxApplicableForConsignee= document.getElementById('wucShortGC1_hdn_IsServiceTaxApplicableForConsignee');

        var ddl_ServiceTaxPayableBy = document.getElementById('wucShortGC1_ddl_ServiceTaxPayableBy');
        var hdn_FreightRate= document.getElementById('wucShortGC1_hdn_FreightRate');
   
        var wuc_PolicyExpDate = wucShortGC1_wuc_PolicyExpDate;
   
//        var hdn_Applicable_MinimunFreightRate= document.getElementById('wucShortGC1_hdn_Applicable_MinimunFreightRate');
//        var hdn_Applicable_SpecialFreightRate= document.getElementById('wucShortGC1_hdn_Applicable_SpecialFreightRate');
//        var hdn_Applicable_FreightRate= document.getElementById('wucShortGC1_hdn_Applicable_FreightRate');
        var hdn_Applicable_Standard_BiltiCharges= document.getElementById('wucShortGC1_hdn_Applicable_Standard_BiltiCharges');
        
        var hdn_Applicable_Standard_DDCharge= document.getElementById('wucShortGC1_hdn_Applicable_Standard_DDCharge');
        var hdn_Applicable_Standard_DDCharge_Rate = document.getElementById('wucShortGC1_hdn_Applicable_Standard_DDCharge_Rate');
        
        var hdn_Applicable_Standard_FOV= document.getElementById('wucShortGC1_hdn_Applicable_Standard_FOV');
        var hdn_Applicable_Standard_FOVPercentage= document.getElementById('wucShortGC1_hdn_Applicable_Standard_FOVPercentage');
        var hdn_Applicable_Standard_FreightAmount= document.getElementById('wucShortGC1_hdn_Applicable_Standard_FreightAmount');
        var hdn_Applicable_Standard_FreightRate= document.getElementById('wucShortGC1_hdn_Applicable_Standard_FreightRate');
        var hdn_Applicable_Standard_HamaliCharge= document.getElementById('wucShortGC1_hdn_Applicable_Standard_HamaliCharge');
        var hdn_Applicable_Standard_LocalCharge= document.getElementById('wucShortGC1_hdn_Applicable_Standard_LocalCharge');
        var hdn_Applicable_Standard_ServiceTaxAmount= document.getElementById('wucShortGC1_hdn_Applicable_Standard_ServiceTaxAmount');
        var hdn_Applicable_Standard_ToPayCharges= document.getElementById('wucShortGC1_hdn_Applicable_Standard_ToPayCharges');
        var hdn_Applicable_Standard_ServiceTaxPercent= document.getElementById('wucShortGC1_hdn_Applicable_Standard_ServiceTaxPercent');

        var hdn_Applicable_Standard_MinimumFOV= document.getElementById('wucShortGC1_hdn_Applicable_Standard_MinimumFOV');
        var hdn_Applicable_Standard_MinimumChargeWeight= document.getElementById('wucShortGC1_hdn_Applicable_Standard_MinimumChargeWeight');
        var hdn_Applicable_Standard_MinimumHamaliPerKg= document.getElementById('wucShortGC1_hdn_Applicable_StandardMinimumHamaliPerKg');
        var hdn_Applicable_Standard_MinFOV= document.getElementById('wucShortGC1_hdn_Applicable_Standard_MinFOV');
        var hdn_Applicable_Standard_HamaliPerKg= document.getElementById('wucShortGC1_hdn_Applicable_Standard_HamaliPerKg');
        var hdn_Applicable_Standard_CFTFactor= document.getElementById('wucShortGC1_hdn_Applicable_Standard_CFTFactor');
        var hdn_Applicable_Standard_DACCCharges= document.getElementById('wucShortGC1_hdn_Applicable_Standard_DACCCharges');
        
        var hdn_IsContractApplied = document.getElementById('wucShortGC1_hdn_IsContractApplied');    
        var txt_ReBookGCAmount = document.getElementById('wucShortGC1_txt_ReBookGCAmount');        
        var hdn_ReBookGCAmount = document.getElementById('wucShortGC1_hdn_ReBookGCAmount');                
        var txt_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_txt_ReBookGC_OctroiAmount');
        var hdn_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_hdn_ReBookGC_OctroiAmount');    
        var lbl_TotalGCAmountValue = document.getElementById('wucShortGC1_lbl_TotalGCAmountValue');        
        var lnk_OtherCharges  = document.getElementById('wucShortGC1_lnk_OtherCharges'); 
        var lbl_OtherCharges = document.getElementById('wucShortGC1_lbl_OtherCharges');        
        var txt_LengthCharge = document.getElementById('wucShortGC1_txt_LengthCharge');
        var hdn_LengthCharge = document.getElementById('wucShortGC1_hdn_LengthCharge');        
        var ddl_LengthChargeHead = document.getElementById('wucShortGC1_ddl_LengthChargeHead');
        var hdn_LengthChargeHeadId = document.getElementById('wucShortGC1_hdn_LengthChargeHeadId');        
        var tr_cheque_Details = document.getElementById('wucShortGC1_tr_cheque_Details');
        var txt_UnloadingCharge = document.getElementById('wucShortGC1_txt_UnloadingCharge');        
        var hdn_Is_ToPay_Charge_Require = document.getElementById('wucShortGC1_hdn_Is_ToPay_Charge_Require');          
        var hdn_NFormCharge = document.getElementById('wucShortGC1_hdn_NFormCharge');
        var txt_NFormCharge = document.getElementById('wucShortGC1_txt_NFormCharge');

        txt_Advance.disabled = false;
        txt_Advance.style.visibility = 'visible';
        lbl_Advance.style.visibility = 'visible';  

        hdn_OldPaymentType.value = ddl_PaymentType.value;       
       
        if (ddl_PaymentType.value == 5 )// foc
        {
            txt_FreightRate.value = val(0);            
            txt_Freight.value = val(0);            
            hdn_Freight.value=val(0);
            hdn_Applicable_Standard_FreightAmount.value  = val(0); 
            txt_LocalCharge.value = val(0);
            hdn_LocalCharge.value = val(0);
            txt_LoadingCharge.value = val(0);
            hdn_LoadingCharge.value = val(0); 
            txt_StationaryCharge.value = val(0);
            hdn_StationaryCharge.value = val(0);
            txt_ToPayCharge.value = val(0);
            hdn_ToPayCharge.value = val(0);
            txt_DDCharge.value = val(0);
            hdn_DDCharge.value = val(0);
            lbl_OtherChargesValue.innerHTML = val(0);
            hdn_OtherCharge.value = val(0);
            txt_FOVRiskCharge.value = val(0);
            hdn_FOVRiskCharge.value = val(0);
            txt_LengthCharge.value = val(0);
            hdn_LengthCharge.value = val(0);  
            txt_UnloadingCharge.value = val(0);
            ddl_LengthChargeHead.value = 0;
            hdn_LengthChargeHeadId.value = 0;
            txt_CashAmount.value = val(0);
            hdn_CashAmount.value = val(0); 
            txt_ChequeAmount.value = val(0);
            hdn_ChequeAmount.value = val(0); 
            txt_ChequeNo.value = val(0); 
            hdn_ChequeNo.value = val(0);  
            txt_Advance.value=val(0);      
            hdn_Advance.value=val(0);      

            txt_FreightRate.disabled = true;
            txt_Freight.disabled = true;
            txt_LocalCharge.disabled = true;
            txt_LoadingCharge.disabled = true;
            txt_StationaryCharge.disabled = true;
            txt_NFormCharge.disabled = true;
            txt_ToPayCharge.disabled = true;
            txt_DDCharge.disabled = true;
            txt_FOVRiskCharge.disabled = true;
            txt_LengthCharge.disabled = true;
            ddl_LengthChargeHead.disabled = true;
            txt_UnloadingCharge.disabled = true;
            txt_ChequeNo.disabled = true;
            txt_Advance.disabled = true;
            txt_CashAmount.disabled = true;
            txt_ChequeAmount.disabled = true;
            txt_BankName.disabled = true;
            wuc_PolicyExpDate.Disabled = false;             
             
            txt_ChequeNo.value="";
            hdn_ChequeNo.value="";
            txt_BankName.value="";
            hdn_BankName.value="";
            
            txt_Advance.style.visibility = 'hidden';
            lbl_Advance.style.visibility = 'hidden';
            lnk_OtherCharges.style.visibility = 'hidden';
            lbl_OtherCharges.style.visibility = 'visible';
            lnk_OtherCharges.style.display = 'none'; 
            lbl_OtherCharges.style.display = 'inline';            
        }
        else  
        {
            if ( hdn_IsContractApplied.value != "1")  // if contract is applied then all the controls are disables 
            {
                Set_Applicable_Rates();
                txt_FreightRate.disabled = false;
                txt_Freight.disabled = false;                
                txt_LocalCharge.disabled = false;
                txt_LoadingCharge.disabled = false;
                txt_StationaryCharge.disabled = false;                
                txt_NFormCharge.disabled = false;
                
                if (ddl_PaymentType.value != 1 && hdn_IsContractApplied.value == "0")  // if contract is applied then all the controls are disables  
                {                                                 
                    txt_ToPayCharge.disabled = false;
                }
                else
                {
                    txt_ToPayCharge.disabled = true;             
                }            
                txt_FOVRiskCharge.disabled = false;                
                txt_LengthCharge.disabled = false;
                ddl_LengthChargeHead.disabled = false;
                
                txt_Advance.style.visibility = 'visible';
                lbl_Advance.style.visibility = 'visible';
                txt_Advance.disabled = false;
                
                txt_CashAmount.disabled = false;
                txt_ChequeAmount.disabled = false;
                txt_BankName.disabled = false;                
                txt_ChequeNo.disabled = false;
                      
                wuc_PolicyExpDate.Disabled = true;             
                lnk_OtherCharges.style.visibility = 'visible';         
                lbl_OtherCharges.style.visibility = 'hidden';
                
                lnk_OtherCharges.style.display = 'inline'; 
                lbl_OtherCharges.style.display = 'none';                
            
                if ( val ( txt_FreightRate.value ) < val (hdn_Applicable_Standard_FreightRate.value))
                {
                    txt_FreightRate.value = val (hdn_Applicable_Standard_FreightRate.value);
                    hdn_FreightRate.value = val (hdn_Applicable_Standard_FreightRate.value);
                }                
                txt_FreightRate.value = val( hdn_FreightRate.value);                  
                
                if ( val ( txt_LocalCharge.value ) < val (hdn_Applicable_Standard_LocalCharge.value))
                {
                    txt_LocalCharge.value = val (hdn_Applicable_Standard_LocalCharge.value);
                    hdn_LocalCharge.value = val (hdn_Applicable_Standard_LocalCharge.value);
                }                
                if ( val ( txt_LoadingCharge.value ) < val (hdn_Applicable_Standard_HamaliCharge.value))
                {
                    txt_LoadingCharge.value = val (hdn_Applicable_Standard_HamaliCharge.value);
                    hdn_LoadingCharge.value = val (hdn_Applicable_Standard_HamaliCharge.value);
                }                
                if ( val ( txt_StationaryCharge.value ) < val (hdn_Applicable_Standard_BiltiCharges.value))
                {
                    txt_StationaryCharge.value = val (hdn_Applicable_Standard_BiltiCharges.value);
                    hdn_StationaryCharge.value = val (hdn_Applicable_Standard_BiltiCharges.value);
                }                                
                if ( val ( txt_ToPayCharge.value ) < val (hdn_Applicable_Standard_ToPayCharges.value))
                {
                    txt_ToPayCharge.value = val (hdn_Applicable_Standard_ToPayCharges.value);
                    hdn_ToPayCharge.value = val (hdn_Applicable_Standard_ToPayCharges.value);
                }                
                if (hdn_Is_ToPay_Charge_Require.value != 'True')
                {
                    txt_ToPayCharge.value = val (0);
                    hdn_ToPayCharge.value = val (0);
                }                        
                if ( val ( txt_DDCharge.value ) < val (hdn_Applicable_Standard_DDCharge.value))
                {
                    txt_DDCharge.value = val (hdn_Applicable_Standard_DDCharge.value);
                    hdn_DDCharge.value = val (hdn_Applicable_Standard_DDCharge.value);
                }                
                if ( val ( txt_FOVRiskCharge.value ) < val (hdn_Applicable_Standard_FOV.value))
                {
                    txt_FOVRiskCharge.value = val (hdn_Applicable_Standard_FOV.value);
                    hdn_FOVRiskCharge.value = val (hdn_Applicable_Standard_FOV.value);
                }
            }
            else
            {
                if ( val ( txt_FreightRate.value ) < val (hdn_Applicable_Standard_FreightRate.value))
                {
                    txt_FreightRate.value = val (hdn_Applicable_Standard_FreightRate.value);
                    hdn_FreightRate.value = val (hdn_Applicable_Standard_FreightRate.value);
                }                
                txt_FreightRate.value = val( hdn_FreightRate.value);                  
                
                if ( val ( txt_LocalCharge.value ) < val (hdn_Applicable_Standard_LocalCharge.value))
                {
                    txt_LocalCharge.value = val (hdn_Applicable_Standard_LocalCharge.value);
                    hdn_LocalCharge.value = val (hdn_Applicable_Standard_LocalCharge.value);
                }                
                if ( val ( txt_LoadingCharge.value ) < val (hdn_Applicable_Standard_HamaliCharge.value))
                {
                    txt_LoadingCharge.value = val (hdn_Applicable_Standard_HamaliCharge.value);
                    hdn_LoadingCharge.value = val (hdn_Applicable_Standard_HamaliCharge.value);
                }                
                if ( val ( txt_StationaryCharge.value ) < val (hdn_Applicable_Standard_BiltiCharges.value))
                {
                    txt_StationaryCharge.value = val (hdn_Applicable_Standard_BiltiCharges.value);
                    hdn_StationaryCharge.value = val (hdn_Applicable_Standard_BiltiCharges.value);
                }
                if ( val ( txt_ToPayCharge.value ) < val (hdn_Applicable_Standard_ToPayCharges.value))
                {
                    txt_ToPayCharge.value = val (hdn_Applicable_Standard_ToPayCharges.value);
                    hdn_ToPayCharge.value = val (hdn_Applicable_Standard_ToPayCharges.value);
                }                
                if (hdn_Is_ToPay_Charge_Require.value != 'True')
                {
                    txt_ToPayCharge.value = val (0);
                    hdn_ToPayCharge.value = val (0);
                }
                if ( val ( txt_DDCharge.value ) < val (hdn_Applicable_Standard_DDCharge.value))
                {
                    txt_DDCharge.value = val (hdn_Applicable_Standard_DDCharge.value);
                    hdn_DDCharge.value = val (hdn_Applicable_Standard_DDCharge.value);
                }                
                if ( val ( txt_FOVRiskCharge.value ) < val (hdn_Applicable_Standard_FOV.value))
                {
                    txt_FOVRiskCharge.value = val (hdn_Applicable_Standard_FOV.value);
                    hdn_FOVRiskCharge.value = val (hdn_Applicable_Standard_FOV.value);
                }
           }        
        }      
        if (ddl_PaymentType.value == 1)    // topay
        {                        
            if (val(lbl_TotalGCAmountValue.innerHTML) < val (txt_Advance.value) )
            {
                txt_Advance.value=  val(0);
                hdn_Advance.value=  val(0);
            }
                        
            txt_CashAmount.value=  val(0);
            hdn_CashAmount.value=  val(0);
            
            if ( val (txt_Advance.value) <  val(txt_ChequeAmount.value) )
            {
                txt_ChequeAmount.value = val(0);        
                hdn_ChequeAmount.value  =   val(txt_ChequeAmount.value);
                        
                txt_ChequeNo.value="";
                hdn_ChequeNo.value="";                
                txt_BankName.value="";
                hdn_BankName.value="";  
            }
            
            txt_Advance.style.visibility = 'hidden';
            lbl_Advance.style.visibility = 'hidden';  

            txt_Advance.disabled = false;
                             
            txt_ToPayCharge.value = val( hdn_Applicable_Standard_ToPayCharges.value);
            hdn_ToPayCharge.value = val( hdn_Applicable_Standard_ToPayCharges.value);
            if (hdn_Is_ToPay_Charge_Require.value != 'True')
            {
                txt_ToPayCharge.value = val (0);
                hdn_ToPayCharge.value = val (0);
            }            
            txt_ToPayCharge.disabled = false;
        }            
        else  
        {
            txt_Advance.value = val(0);
            hdn_Advance.value = val(0);
            txt_ToPayCharge.value=val(0);
            hdn_ToPayCharge.value = val(0);

            txt_Advance.style.visibility = 'visible';
            lbl_Advance.style.visibility = 'visible';  

            txt_Advance.disabled = true;
            txt_ToPayCharge.disabled = true;
        }
        
        if ( hdn_IsServiceTaxApplicableForConsignor.value == "1" && ( ddl_PaymentType.value == "2"  ||  ddl_PaymentType.value == "4" ))
        {
            ddl_ServiceTaxPayableBy.value = "1"; // consignor
        }
        else if ( hdn_IsServiceTaxApplicableForConsignee.value == "1" && ddl_PaymentType.value == "1")
        {
            ddl_ServiceTaxPayableBy.value = "2"; // consignee
        }
        else if ( hdn_IsServiceTaxApplicableForConsignor.value == "1" && ddl_PaymentType.value == "3") // TBB
        {
            ddl_ServiceTaxPayableBy.value = "1"; // consignor
        }
        else if ( hdn_IsServiceTaxApplicableForConsignor.value == "0" && ddl_PaymentType.value == "3") // TBB
        {
            ddl_ServiceTaxPayableBy.value = "3"; // transporter
        }
        else
        {
            ddl_ServiceTaxPayableBy.value = "3"; // transporter 
        }        
        
        Get_BasisFreight();
        
        var ddl_PaymentType = document.getElementById('wucShortGC1_ddl_PaymentType');        
        var ddl_ContractualClient = document.getElementById('wucShortGC1_ddl_ContractualClient_txtBoxddl_ContractualClient');
        var ddl_ContractBranch = document.getElementById('wucShortGC1_ddl_ContractBranch');
        var ddl_Contract = document.getElementById('wucShortGC1_ddl_Contract');
        var hdn_ContractId = document.getElementById('wucShortGC1_hdn_ContractId');
        var hdn_IsContractApplied = document.getElementById('wucShortGC1_hdn_IsContractApplied');
        var ddl_BillingParty = document.getElementById('wucShortGC1_ddl_BillingParty_txtBoxddl_BillingParty');        
        var ddl_BillingBranch = document.getElementById('wucShortGC1_ddl_BillingBranch_txtBoxddl_BillingBranch');        
        var txt_BillingRemark = document.getElementById('wucShortGC1_txt_BillingRemark');                
        var hdn_BillingPartyId = document.getElementById('wucShortGC1_hdn_BillingPartyId');
        var hdn_BillingBranchId = document.getElementById('wucShortGC1_hdn_BillingBranchId');
        var hdn_ContractualClientId = document.getElementById('wucShortGC1_hdn_ContractualClientId');
        var tr_PaymentDetails = document.getElementById('wucShortGC1_tr_PaymentDetails');
        var tr_billing_details = document.getElementById('wucShortGC1_tr_billing_details');
        var i;           
        
        if (ddl_PaymentType.value == 5 )   // FOC
        {
            ddl_ContractualClient.disabled = true;
            ddl_ContractBranch.disabled = true;
            ddl_Contract.disabled = true;
            ddl_BillingParty.disabled = true; 
            
            ddl_BillingBranch.disabled = true;
            txt_BillingRemark.disabled = true;
                        
            ddl_BillingBranch.value ='';
            ddl_BillingParty.value ='';                 
            
            tr_billing_details.style.display = 'none';           
        }
        else       
        {        
            ddl_ContractualClient.disabled = false;
            ddl_ContractBranch.disabled = false;
            ddl_Contract.disabled = false;
            ddl_BillingParty.disabled = false; 

            ddl_BillingBranch.disabled = false;
            txt_BillingRemark.disabled = false;        
            tr_billing_details.style.display = 'inline';
            hdn_IsContractApplied.value = val(0);

            if ( ddl_Contract.value > 0 )
            {
                Get_ContractCharges();            
                Enable_Disable_Freight_Details_Controls();      
            }
            else
            {
                hdn_ContractId.value = 0;            
            }
        }
        
        if (ddl_PaymentType.value != 5 ) // foc
        {
            Enable_Disable_Freight_Details_Controls();   
        }
        
        if (ddl_PaymentType.value == 3 )//|| ddl_PaymentType.value == 4 )     // TBB and Short term billing
        {   
            lbl_ChequeDate.style.visibility = 'hidden';
            lbl_Bank.style.visibility = 'hidden';
            lbl_Advance.style.visibility = 'hidden';
            lbl_CashAmount.style.visibility = 'hidden';
            lbl_ChequeAmount.style.visibility = 'hidden';
            lbl_ChequeNo.style.visibility = 'hidden';
                    
            txt_Advance.style.visibility = 'hidden';
            lbl_Advance.style.visibility = 'hidden';  
                        
            txt_Advance.value=val(0);      
            hdn_Advance.value=val(0);      
            
            txt_Advance.style.visibility = 'hidden';
            txt_BankName.style.visibility = 'hidden';
            txt_CashAmount.style.visibility = 'hidden';
            txt_ChequeAmount.style.visibility = 'hidden';
            txt_ChequeNo.style.visibility = 'hidden';
            tr_PaymentDetails.style.display = 'none';
            tr_cheque_Details.style.visibility = 'hidden';
            txt_Advance.value = val(0);
            txt_BankName.value = '';
            txt_CashAmount.value = val(0);
            txt_ChequeAmount.value = val(0);
            txt_ChequeNo.value = val(0);
            
            hdn_Advance.value = val(0);
            hdn_BankName.value = '';  
            
            hdn_CashAmount.value = val(0);
            hdn_ChequeAmount.value = val(0);
            hdn_ChequeNo.value = val(0);    
            
            txt_ChequeNo.value="";
            hdn_ChequeNo.value="";
            
            txt_BankName.value="";
            hdn_BankName.value="";
            
            ddl_ContractualClient.disabled = false;
            ddl_ContractBranch.disabled = false;
            ddl_Contract.disabled = false;
            ddl_BillingParty.disabled = false; 
            
            ddl_BillingBranch.disabled = false;
            txt_BillingRemark.disabled = false;              
            tr_billing_details.style.display = 'inline';
        }
        else
        {   
            lbl_ChequeDate.style.visibility = 'visible';
            lbl_Bank.style.visibility = 'visible';
            lbl_Advance.style.visibility = 'visible';
            lbl_CashAmount.style.visibility = 'visible';
            lbl_ChequeAmount.style.visibility = 'visible';
            lbl_ChequeNo.style.visibility = 'visible';
        
            txt_Advance.style.visibility = 'visible';
            txt_BankName.style.visibility = 'visible';
            txt_CashAmount.style.visibility = 'visible';
            txt_ChequeAmount.style.visibility = 'visible';
            txt_ChequeNo.style.visibility = 'visible';
            tr_cheque_Details.style.visibility = 'visible';
            tr_PaymentDetails.style.display = 'inline';
            tr_PaymentDetails.style.display = 'inline';
            ddl_BillingParty.disabled = true;             
            ddl_BillingBranch.disabled = true;
            txt_BillingRemark.disabled = true;                        
            ddl_BillingBranch.value ='';
            ddl_BillingParty.value ='';                 
        
            hdn_BillingPartyId.value=val(0);
            hdn_BillingBranchId.value=val(0);
            tr_billing_details.style.display = 'none';
        }
        
        if (ddl_PaymentType.value == 1)    // topay
        {    
            txt_Advance.disabled = false;
            txt_ToPayCharge.disabled = false;            
            txt_Advance.style.visibility = 'visible';
            lbl_Advance.style.visibility = 'visible';  
        }            
        else  
        {     
            txt_Advance.style.visibility = 'hidden';
            lbl_Advance.style.visibility = 'hidden';            
            txt_Advance.value=val(0);
            hdn_Advance.value=val(0);            
             txt_Advance.disabled = true;    
             txt_ToPayCharge.disabled = true;   
             txt_ToPayCharge.value=val(0);      
             hdn_ToPayCharge.value=val(0);
        }      
    }
        
//*******************************************************************
    function Get_BasisFreight()
    {
        Calculate_Freight('0');
        Calculate_LoadingCharge();
        Calculate_FOV();
        Calculate_DDODA_Charge();
        Calculate_GrandTotal();
        On_CashAmount_Change();
        Set_Payment_Details();
     }
    
//*******************************************************************
 
    function On_PaymentType_Change_Edit()
    {   
        var tr_cheque_Details = document.getElementById('wucShortGC1_tr_cheque_Details');
        var txt_Advance = document.getElementById('wucShortGC1_txt_Advance');        
        var ddl_PaymentType = document.getElementById('wucShortGC1_ddl_PaymentType');
        var txt_FreightRate = document.getElementById('wucShortGC1_txt_FreightRate');
        var txt_Freight = document.getElementById('wucShortGC1_txt_Freight');
        var hdn_Freight = document.getElementById('wucShortGC1_hdn_Freight');
        var txt_LocalCharge = document.getElementById('wucShortGC1_txt_LocalCharge');
        var hdn_LocalCharge = document.getElementById('wucShortGC1_hdn_LocalCharge');
        var txt_LoadingCharge = document.getElementById('wucShortGC1_txt_LoadingCharge');
        var hdn_LoadingCharge = document.getElementById('wucShortGC1_hdn_LoadingCharge');
        var txt_StationaryCharge = document.getElementById('wucShortGC1_txt_StationaryCharge');
        var hdn_StationaryCharge = document.getElementById('wucShortGC1_hdn_StationaryCharge');
        var txt_ToPayCharge = document.getElementById('wucShortGC1_txt_ToPayCharge');
        var hdn_ToPayCharge = document.getElementById('wucShortGC1_hdn_ToPayCharge');
        var txt_DDCharge = document.getElementById('wucShortGC1_txt_DDCharge');
        var hdn_DDCharge = document.getElementById('wucShortGC1_hdn_DDCharge');
        var lbl_OtherChargesValue = document.getElementById('wucShortGC1_lbl_OtherChargesValue');
        var hdn_OtherCharge = document.getElementById('wucShortGC1_hdn_OtherCharge');
        var txt_FOVRiskCharge = document.getElementById('wucShortGC1_txt_FOVRiskCharge');
        var hdn_FOVRiskCharge = document.getElementById('wucShortGC1_hdn_FOVRiskCharge');
        var txt_ReBookGCAmount = document.getElementById('wucShortGC1_txt_ReBookGCAmount');        
        var hdn_ReBookGCAmount = document.getElementById('wucShortGC1_hdn_ReBookGCAmount');        
        var txt_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_txt_ReBookGC_OctroiAmount');
        var hdn_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_hdn_ReBookGC_OctroiAmount');        
        var hdn_Advance = document.getElementById('wucShortGC1_hdn_Advance');
        var txt_CashAmount = document.getElementById('wucShortGC1_txt_CashAmount');
        var hdn_CashAmount = document.getElementById('wucShortGC1_hdn_CashAmount');
        var txt_ChequeAmount = document.getElementById('wucShortGC1_txt_ChequeAmount');
        var hdn_ChequeAmount = document.getElementById('wucShortGC1_hdn_ChequeAmount');
        var txt_BankName = document.getElementById('wucShortGC1_txt_BankName');
        var hdn_BankName = document.getElementById('wucShortGC1_hdn_BankName');       
        var txt_ChequeNo = document.getElementById('wucShortGC1_txt_ChequeNo');
        var hdn_ChequeNo = document.getElementById('wucShortGC1_hdn_ChequeNo');        
        var wuc_PolicyExpDate = document.getElementById('wucShortGC1_wuc_PolicyExpDate');
        var hdn_IsServiceTaxApplicableForConsignor = document.getElementById('wucShortGC1_hdn_IsServiceTaxApplicableForConsignor');
        var hdn_IsServiceTaxApplicableForConsignee= document.getElementById('wucShortGC1_hdn_IsServiceTaxApplicableForConsignee');
        var ddl_ServiceTaxPayableBy = document.getElementById('wucShortGC1_ddl_ServiceTaxPayableBy');
        var hdn_FreightRate= document.getElementById('wucShortGC1_hdn_FreightRate');
        var wuc_PolicyExpDate = wucShortGC1_wuc_PolicyExpDate; 
        var hdn_IsContractApplied = document.getElementById('wucShortGC1_hdn_IsContractApplied'); 
        var hdn_Applicable_Standard_FreightAmount = document.getElementById('wucShortGC1_hdn_Applicable_Standard_FreightAmount');
            
        var lbl_ChequeDate = document.getElementById('wucShortGC1_lbl_ChequeDate');
        var lbl_Bank = document.getElementById('wucShortGC1_lbl_Bank');
        var lbl_Advance = document.getElementById('wucShortGC1_lbl_Advance');
        var lbl_CashAmount = document.getElementById('wucShortGC1_lbl_CashAmount');
        var lbl_ChequeAmount = document.getElementById('wucShortGC1_lbl_ChequeAmount');
        var lbl_ChequeNo= document.getElementById('wucShortGC1_lbl_ChequeNo');        
        var lnk_OtherCharges = document.getElementById('wucShortGC1_lnk_OtherCharges');
        var lbl_OtherCharges = document.getElementById('wucShortGC1_lbl_OtherCharges');        
        var ddl_LengthChargeHead = document.getElementById('wucShortGC1_ddl_LengthChargeHead');
        var hdn_LengthChargeHeadId = document.getElementById('wucShortGC1_hdn_LengthChargeHeadId');        
        var txt_LengthCharge = document.getElementById('wucShortGC1_txt_LengthCharge');  
        var hdn_LengthCharge = document.getElementById('wucShortGC1_hdn_LengthCharge');   
        var txt_UnloadingCharge = document.getElementById('wucShortGC1_txt_UnloadingCharge');       
        var hdn_NFormCharge = document.getElementById('wucShortGC1_hdn_NFormCharge');
        var txt_NFormCharge = document.getElementById('wucShortGC1_txt_NFormCharge');

        txt_Advance.disabled = false;
        
        txt_Advance.style.visibility = 'hidden';
        lbl_Advance.style.visibility = 'hidden';            

         if (ddl_PaymentType.value == 5 ) // foc
        {
            txt_FreightRate.value = val(0);
            txt_Freight.value = val(0);
            hdn_Freight.value=val(0);
            
            hdn_Applicable_Standard_FreightAmount.value  = val(0);
            txt_LocalCharge.value = val(0);
            hdn_LocalCharge.value = val(0);
            txt_LoadingCharge.value = val(0);
            hdn_LoadingCharge.value = val(0);
            txt_StationaryCharge.value = val(0);
            hdn_StationaryCharge.value = val(0);
            txt_ToPayCharge.value = val(0);
            hdn_ToPayCharge.value = val(0);
            txt_DDCharge.value = val(0);
            hdn_DDCharge.value = val(0);
            lbl_OtherChargesValue.innerHTML = val(0);
            hdn_OtherCharge.value = val(0);
            txt_FOVRiskCharge.value = val(0);
            hdn_FOVRiskCharge.value = val(0);
            txt_LengthCharge.value = val(0);
            hdn_LengthCharge.value = val(0);
            hdn_LengthChargeHeadId.value =0;
            ddl_LengthChargeHead.value = 0;            
            ddl_LengthChargeHead.disabled = true;            
            txt_FreightRate.disabled = true;
            txt_Freight.disabled = true;            
            txt_LocalCharge.disabled = true;
            txt_LoadingCharge.disabled = true;
            txt_StationaryCharge.disabled = true;            
            txt_NFormCharge.disabled = true;                
            txt_ToPayCharge.disabled = true;
            txt_DDCharge.disabled = true;
            txt_FOVRiskCharge.disabled = true;
            txt_LengthCharge.disabled = true;
            ddl_LengthChargeHead.disabled = true;            
            txt_UnloadingCharge.disabled = true;
            txt_UnloadingCharge.value = val(0);            
            txt_Advance.value = val(0);
            hdn_Advance.value = val(0);              
            txt_CashAmount.value = val(0);
            hdn_CashAmount.value = val(0);             
            txt_ChequeAmount.value = val(0);
            hdn_ChequeAmount.value = val(0); 
            txt_ChequeNo.value = val(0); 
            hdn_ChequeNo.value = val(0);  
            txt_ChequeNo.disabled = true;
            txt_Advance.style.visibility = 'visible';
            lbl_Advance.style.visibility = 'visible';
            txt_Advance.disabled = true;
            txt_CashAmount.disabled = true;
            txt_ChequeAmount.disabled = true;            
            txt_BankName.value = "";
            hdn_BankName.value = ""; 
            txt_BankName.disabled = true;
            wuc_PolicyExpDate.Disabled = false;           
            txt_ChequeNo.value="";
            hdn_ChequeNo.value="";            
            txt_BankName.value="";
            hdn_BankName.value="";              
            lnk_OtherCharges.style.visibility = 'hidden';
            lbl_OtherCharges.style.visibility = 'visible';            
            lnk_OtherCharges.style.display = 'none'; 
            lbl_OtherCharges.style.display = 'inline';
        }            
        else  
        {                    
             if ( hdn_IsContractApplied.value != "1")  // if contract is applied then all the controls are disables 
                {
                    txt_FreightRate.disabled = false;
                    txt_Freight.disabled = false;                    
                    txt_LocalCharge.disabled = false;
                    txt_LoadingCharge.disabled = false;
                    txt_StationaryCharge.disabled = false;                    
                    txt_NFormCharge.disabled = false;                    
                    txt_ToPayCharge.disabled = false;                     
                   if (ddl_PaymentType.value != 1 && hdn_IsContractApplied.value == "0")  // if contract is applied then all the controls are disables  
                    {                                                 
                        txt_ToPayCharge.disabled = false;
                    }
                    else
                    {
                        txt_ToPayCharge.disabled = true;
                    }  
                    txt_FOVRiskCharge.disabled = false;                    
                    txt_LengthCharge.disabled = false;
                    ddl_LengthChargeHead.disabled = false;                    
                    txt_Advance.style.visibility = 'hidden';
                    lbl_Advance.style.visibility = 'hidden';            
                    txt_Advance.disabled = false;
                    txt_CashAmount.disabled = false;
                    txt_ChequeAmount.disabled = false;
                    txt_ChequeNo.disabled = false;
                    txt_BankName.disabled = false;                    
                    wuc_PolicyExpDate.Disabled = true;                    
                    lnk_OtherCharges.style.visibility = 'visible';
                    lbl_OtherCharges.style.visibility = 'hidden';                    
                    lnk_OtherCharges.style.display = 'inline'; 
                    lbl_OtherCharges.style.display = 'none';                    
                }
                txt_FreightRate.value =   val( hdn_FreightRate.value);                         
        }       
   
        if (ddl_PaymentType.value == 1  )      // topay
        {    
            txt_Advance.disabled = false;
            txt_ToPayCharge.disabled = false;
            txt_Advance.style.visibility = 'visible';
            lbl_Advance.style.visibility = 'visible';  
        }            
        
        if (ddl_PaymentType.value != 1  ) 
        {     
            txt_Advance.style.visibility = 'hidden';
            lbl_Advance.style.visibility = 'hidden';  
            txt_Advance.disabled = true;    
            txt_ToPayCharge.disabled = true;   
            txt_ToPayCharge.value=val(0);      
            hdn_ToPayCharge.value=val(0);
        }
        
        if( hdn_IsContractApplied.value == 0 && ddl_PaymentType.value == 1  ) 
        {
            txt_Advance.disabled = false;            
        }
        else
        {
           txt_Advance.disabled = true;        
        }      
                
        if ( hdn_IsServiceTaxApplicableForConsignor.value == "1" && ( ddl_PaymentType.value == "2"  ||  ddl_PaymentType.value == "4" ) )
        {
            ddl_ServiceTaxPayableBy.value = "1"; // consignor
        }
        else if ( hdn_IsServiceTaxApplicableForConsignee.value == "1" && ddl_PaymentType.value == "1")
        {
            ddl_ServiceTaxPayableBy.value = "2"; // consignee
        }
        else if ( hdn_IsServiceTaxApplicableForConsignor.value == "1" && ddl_PaymentType.value == "3") // TBB
        {
            ddl_ServiceTaxPayableBy.value = "1"; // consignor
        }
        else if ( hdn_IsServiceTaxApplicableForConsignor.value == "0" && ddl_PaymentType.value == "3") // TBB
        {
            ddl_ServiceTaxPayableBy.value = "3"; // transporter
        }
        else
        {
            ddl_ServiceTaxPayableBy.value = "3"; // transporter 
        }
       
        var ddl_ContractualClient = document.getElementById('wucShortGC1_ddl_ContractualClient_txtBoxddl_ContractualClient');
        var ddl_ContractBranch = document.getElementById('wucShortGC1_ddl_ContractBranch');
        var ddl_Contract = document.getElementById('wucShortGC1_ddl_Contract');        
        var ddl_BillingParty = document.getElementById('wucShortGC1_ddl_BillingParty_txtBoxddl_BillingParty');
        var ddl_BillingBranch = document.getElementById('wucShortGC1_ddl_BillingBranch_txtBoxddl_BillingBranch');        
        var txt_BillingRemark = document.getElementById('wucShortGC1_txt_BillingRemark');        
        var hdn_BillingPartyId = document.getElementById('wucShortGC1_hdn_BillingPartyId');
        var hdn_BillingBranchId = document.getElementById('wucShortGC1_hdn_BillingBranchId');
        var hdn_ContractualClientId = document.getElementById('wucShortGC1_hdn_ContractualClientId');
        var i;
        
        var tr_PaymentDetails = document.getElementById('wucShortGC1_tr_PaymentDetails');
        var tr_billing_details = document.getElementById('wucShortGC1_tr_billing_details');        
        var hdn_ContractId = document.getElementById('wucShortGC1_hdn_ContractId');
        var hdn_ContractBranchId  = document.getElementById('wucShortGC1_hdn_ContractBranchId');
        
        if ((ddl_PaymentType.value == 5 ) && ddl_PaymentType.value != 3)     // FOC
         {                
            ddl_ContractualClient.disabled = true;
            ddl_ContractBranch.disabled = true;
            ddl_Contract.disabled = true;
            ddl_BillingParty.disabled = true;             
            ddl_BillingBranch.disabled = true;                       
            ddl_BillingBranch.value ='';
            ddl_BillingParty.value ='';
            hdn_BillingPartyId.value=val(0);
            hdn_BillingBranchId.value=val(0);
            txt_BillingRemark.disabled = true;
            tr_billing_details.style.display = 'none';
        }
        else
        {
            ddl_ContractualClient.disabled = false;
            ddl_ContractBranch.disabled = false;
            ddl_Contract.disabled = false;
            ddl_BillingParty.disabled = false;
            ddl_BillingBranch.disabled = false;
            txt_BillingRemark.disabled = false;
            tr_billing_details.style.display = 'inline';
        }
        
         if (ddl_PaymentType.value == 3 )//|| ddl_PaymentType.value == 4 )     // TBB and Short term billing
        {
        
            lbl_ChequeDate.style.visibility = 'hidden';
            lbl_Bank.style.visibility = 'hidden';
            lbl_Advance.style.visibility = 'hidden';
            lbl_CashAmount.style.visibility = 'hidden';
            lbl_ChequeAmount.style.visibility = 'hidden';
            lbl_ChequeNo.style.visibility = 'hidden';
        
            txt_Advance.style.visibility = 'hidden';
            txt_BankName.style.visibility = 'hidden';
            txt_CashAmount.style.visibility = 'hidden';
            txt_ChequeAmount.style.visibility = 'hidden';
            txt_ChequeNo.style.visibility = 'hidden';
            tr_cheque_Details.style.visibility = 'hidden';
            tr_PaymentDetails.style.display = 'none'; 
            
            txt_Advance.value = val(0);
            txt_BankName.value = '';
            txt_CashAmount.value = val(0);
            txt_ChequeAmount.value = val(0);
            txt_ChequeNo.value = val(0);            
            hdn_Advance.value = val(0);
            hdn_BankName.value = '';  
            
            hdn_CashAmount.value = val(0);
            hdn_ChequeAmount.value = val(0);
            hdn_ChequeNo.value = val(0);                    
            txt_ChequeNo.value="";
            hdn_ChequeNo.value="";            
            txt_BankName.value="";
            hdn_BankName.value="";  
        }
        else if ( ddl_PaymentType.value != 5 )
        {   
            lbl_ChequeDate.style.visibility = 'visible';
            lbl_Bank.style.visibility = 'visible';
            lbl_Advance.style.visibility = 'visible';
            lbl_CashAmount.style.visibility = 'visible';
            lbl_ChequeAmount.style.visibility = 'visible';
            lbl_ChequeNo.style.visibility = 'visible';
        
            txt_Advance.style.visibility = 'visible';
            txt_BankName.style.visibility = 'visible';
            txt_CashAmount.style.visibility = 'visible';
            txt_ChequeAmount.style.visibility = 'visible';
            txt_ChequeNo.style.visibility = 'visible';
            tr_cheque_Details.style.visibility = 'visible';
            tr_PaymentDetails.style.display = 'inline';             
            ddl_BillingParty.disabled = true;             
            ddl_BillingBranch.disabled = true;            
            txt_BillingRemark.disabled = true;
            tr_billing_details.style.display = 'none';
        }

        if (ddl_PaymentType.value == 1 )      // topay
        {    
            txt_Advance.disabled = false;
            txt_ToPayCharge.disabled = false;
            
            txt_Advance.style.visibility = 'visible';
            lbl_Advance.style.visibility = 'visible';  
        }
        
        if (ddl_PaymentType.value != 1  ) 
        {     
            txt_Advance.style.visibility = 'hidden';
            lbl_Advance.style.visibility = 'hidden';  

             txt_Advance.disabled = true;    
             txt_Advance.value=val(0);      
             hdn_Advance.value=val(0);      
             
             txt_ToPayCharge.disabled = true;   
             txt_ToPayCharge.value=val(0);      
             hdn_ToPayCharge.value=val(0);
        }
        
        if(  ddl_PaymentType.value == 1  ) 
        {
            txt_Advance.disabled = false;            
        }
        else 
        {
           txt_Advance.disabled = true;        
        }      
    }
//*******************************************************************

    function On_CashAmount_Change()
    {           
        var ddl_PaymentType = document.getElementById('wucShortGC1_ddl_PaymentType');
        var lbl_TotalGCAmountValue = document.getElementById('wucShortGC1_lbl_TotalGCAmountValue');        
        var txt_CashAmount = document.getElementById('wucShortGC1_txt_CashAmount');
        var hdn_CashAmount = document.getElementById('wucShortGC1_hdn_CashAmount');        
        var txt_Advance = document.getElementById('wucShortGC1_txt_Advance');
        var hdn_Advance = document.getElementById('wucShortGC1_hdn_Advance');        
        var txt_ChequeAmount = document.getElementById('wucShortGC1_txt_ChequeAmount');
        var hdn_ChequeAmount = document.getElementById('wucShortGC1_hdn_ChequeAmount');
        var hdn_BankName = document.getElementById('wucShortGC1_hdn_BankName');
        var txt_BankName = document.getElementById('wucShortGC1_txt_BankName');
        var txt_ChequeNo = document.getElementById('wucShortGC1_txt_ChequeNo');  
        var hdn_ChequeNo = document.getElementById('wucShortGC1_hdn_ChequeNo');
        
        var Amount = val(0);

        txt_CashAmount.value = val(txt_CashAmount.value); 
        
        if ( ddl_PaymentType.value == 2 ) // paid
        {
            Amount=val(lbl_TotalGCAmountValue.innerHTML); 
        }        
        else if( ddl_PaymentType.value == 1 ) // to pay
        {   
            Amount=val(txt_Advance.value); 
        }
        
        if ( Amount < val(txt_CashAmount.value)  )
        {  
            txt_CashAmount.value = Amount;
        }
        else if (Amount < val(txt_ChequeAmount.value)  )
        {  
            txt_ChequeAmount.value = Amount ;
        }  

        if ( val(txt_ChequeAmount.value) <= 0 && val(txt_CashAmount.value) <= 0)
        {
            txt_CashAmount.value = val(Amount);
        }
        txt_ChequeAmount.value = Amount - val( txt_CashAmount.value );   
        hdn_ChequeAmount.value = val( txt_ChequeAmount.value );   
        
        if ( val(txt_ChequeAmount.value) <= 0)
        {  
            txt_BankName.value = '';
                     
            txt_ChequeNo.value = '';
            hdn_ChequeNo.value = '';
            
            txt_BankName.disabled = true;
            txt_ChequeNo.disabled = true;
        }
        else
        {
            txt_BankName.disabled = false;
            txt_ChequeNo.disabled = false;        
        }
        hdn_CashAmount.value = val(txt_CashAmount.value);         
    }
     
//*******************************************************************
     function Set_Cheque_Details()
     {
        var txt_ChequeAmount = document.getElementById('wucShortGC1_txt_ChequeAmount');
        var txt_ChequeNo = document.getElementById('wucShortGC1_txt_ChequeNo');
 
        if(val(txt_ChequeAmount.value) > 0)
        {
            txt_ChequeNo.focus();
        }
     }
 
    function On_ChequeAmount_Change()
    {   
        var ddl_PaymentType = document.getElementById('wucShortGC1_ddl_PaymentType');       
        var lbl_TotalGCAmountValue = document.getElementById('wucShortGC1_lbl_TotalGCAmountValue');        
        var txt_Advance = document.getElementById('wucShortGC1_txt_Advance');
        var hdn_Advance = document.getElementById('wucShortGC1_hdn_Advance');
        var txt_CashAmount = document.getElementById('wucShortGC1_txt_CashAmount');
        var hdn_CashAmount = document.getElementById('wucShortGC1_hdn_CashAmount');
        var hdn_ChequeAmount = document.getElementById('wucShortGC1_hdn_ChequeAmount');
        var txt_ChequeAmount = document.getElementById('wucShortGC1_txt_ChequeAmount');
        var txt_BankName = document.getElementById('wucShortGC1_txt_BankName');
        var txt_ChequeNo = document.getElementById('wucShortGC1_txt_ChequeNo');
        var hdn_ChequeNo = document.getElementById('wucShortGC1_hdn_ChequeNo');
        
        var Amount=  val(0);

        txt_ChequeAmount.value = val(txt_ChequeAmount.value); 
        
        if ( ddl_PaymentType.value == 2 ) // paid
        {
            Amount=val(lbl_TotalGCAmountValue.innerHTML); 
        }
        else if( ddl_PaymentType.value == 1 ) // to pay
        {           
            Amount=val(txt_Advance.value);             
        }

        if ( Amount   < val(txt_CashAmount.value))
        {  
            txt_CashAmount.value = val( Amount);
        }
        else if (Amount < val(txt_ChequeAmount.value))
        {  
            txt_ChequeAmount.value = Amount ;
        }  
        txt_CashAmount.value =  val( Amount) - val( txt_ChequeAmount.value); 
      
        hdn_CashAmount.value = val(txt_CashAmount.value); 
        hdn_ChequeAmount.value= val( txt_ChequeAmount.value );   
        
        if ( val(txt_ChequeAmount.value) <= 0)
        {  
            txt_BankName.value = '';                     
            txt_ChequeNo.value = '';
            hdn_ChequeNo.value = '';            
            txt_BankName.disabled = true;
            txt_ChequeNo.disabled = true;            
        }
        else
        {
            txt_BankName.disabled = false;
            txt_ChequeNo.disabled = false;        
        }        
    }    
//*******************************************************************
 
    function On_AdvanceAmount_Change()
    {        
        var ddl_PaymentType = document.getElementById('wucShortGC1_ddl_PaymentType');
        var lbl_SubTotalValue = document.getElementById('wucShortGC1_lbl_SubTotalValue');
        var hdn_SubTotal = document.getElementById('wucShortGC1_hdn_SubTotal');
        var lbl_TotalGCAmountValue = document.getElementById('wucShortGC1_lbl_TotalGCAmountValue');
        var txt_Advance = document.getElementById('wucShortGC1_txt_Advance');
        var hdn_Advance = document.getElementById('wucShortGC1_hdn_Advance');
        var txt_CashAmount = document.getElementById('wucShortGC1_txt_CashAmount');
        var hdn_CashAmount = document.getElementById('wucShortGC1_hdn_CashAmount');
        var txt_ChequeAmount = document.getElementById('wucShortGC1_txt_ChequeAmount');
        var hdn_ChequeAmount = document.getElementById('wucShortGC1_hdn_ChequeAmount');
        var txt_ChequeNo  =  document.getElementById('wucShortGC1_txt_ChequeNo');  
        var hdn_ChequeNo= document.getElementById('wucShortGC1_hdn_ChequeNo');
        var hdn_BankName= document.getElementById('wucShortGC1_hdn_BankName');
        var txt_BankName= document.getElementById('wucShortGC1_txt_BankName');

        var Amount=  val(0);
        
        txt_Advance.value = val(txt_Advance.value); 

        if ( ddl_PaymentType.value == 2 ) // paid
        {
            Amount=val(lbl_TotalGCAmountValue.innerHTML); 
        }
        else if( ddl_PaymentType.value == 1 ) // to pay
        {
            Amount=val(txt_Advance.value); 
        }

        if ( val(lbl_TotalGCAmountValue.innerHTML)< val(txt_Advance.value)  )
        {  
            txt_Advance.value = val(lbl_TotalGCAmountValue.innerHTML);
        }  

        if ( val (txt_Advance.value) <  val(txt_ChequeAmount.value) )
            {
                txt_ChequeAmount.value = val(0);        
                hdn_ChequeAmount.value = val(txt_ChequeAmount.value);
                txt_ChequeNo.value="";
                hdn_ChequeNo.value="";
                txt_BankName.value="";
                hdn_BankName.value="";  
            }
            else
            {
                txt_CashAmount.value = val(txt_Advance.value ) - val(txt_ChequeAmount.value);                
            }        
        hdn_Advance.value = val(txt_Advance.value);
        hdn_CashAmount.value = val(txt_CashAmount.value); 
        hdn_ChequeAmount.value= val( txt_ChequeAmount.value );           
    }
    
//*******************************************************************
 
    function ddl_GCRisk_Change()
    {                
        var ddl_GCRisk = document.getElementById('wucShortGC1_ddl_GCRisk');
        var txt_FOVRiskCharge = document.getElementById('wucShortGC1_txt_FOVRiskCharge');
        var hdn_FOVRiskCharge = document.getElementById('wucShortGC1_hdn_FOVRiskCharge');
        var chk_IsInsured = document.getElementById('wucShortGC1_chk_IsInsured');
        
        On_GCRisk_Change();
        
        txt_FOVRiskCharge.value=val(0) ;   
        hdn_FOVRiskCharge.value=val(0) ;    

        if ((ddl_GCRisk.value == 2 ||ddl_GCRisk.value == 3 ) || (ddl_GCRisk.value == 1 && chk_IsInsured.checked == false ))    // for carrier risk (2) and none (3)
        {
            Calculate_FOV();
        }
        else
        {
           txt_FOVRiskCharge.value=val(0) ;
           hdn_FOVRiskCharge.value=val(0) ;
        }
        Calculate_GrandTotal();
        On_CashAmount_Change();
        Set_Payment_Details();
    }    
//*******************************************************************

    function Calculate_FOV()
    {   
        var ddl_GCRisk = document.getElementById('wucShortGC1_ddl_GCRisk');
        var lbl_TotalInvoiceAmount = document.getElementById('wucShortGC1_lbl_TotalInvoiceAmount');
        var hdn_FOVPercentage = document.getElementById('wucShortGC1_hdn_FOVPercentage');
        var hdn_MinimumFOV = document.getElementById('wucShortGC1_hdn_MinimumFOV');
        var txt_FOVRiskCharge = document.getElementById('wucShortGC1_txt_FOVRiskCharge');
        var hdn_FOVRiskCharge = document.getElementById('wucShortGC1_hdn_FOVRiskCharge');
        var hdn_Applicable_Standard_FOVPercentage   = document.getElementById('wucShortGC1_hdn_Applicable_Standard_FOVPercentage');
        var hdn_Applicable_Standard_MinimumFOV   = document.getElementById('wucShortGC1_hdn_Applicable_Standard_MinimumFOV');
        var hdn_Standard_FOV   = document.getElementById('wucShortGC1_hdn_Standard_FOV');
        var hdn_Standard_FOVPercentage = document.getElementById('wucShortGC1_hdn_Standard_FOVPercentage');
        var ddl_PaymentType = document.getElementById('wucShortGC1_ddl_PaymentType');  
        var hdn_Is_Opening_GC = document.getElementById('wucShortGC1_hdn_Is_Opening_GC');
        var hdn_MenuItemId = document.getElementById('wucShortGC1_hdn_MenuItemId');
        var hdn_Rectification_GC_Id = document.getElementById('wucShortGC1_hdn_Rectification_GC_Id');       
        var hdn_Is_FOV_Calculated_As_Per_Standard = document.getElementById('wucShortGC1_hdn_Is_FOV_Calculated_As_Per_Standard');
        var hdn_Applicable_Standard_FOVRate = document.getElementById('wucShortGC1_hdn_Applicable_Standard_FOVRate');
        var hdn_Applicable_Standard_Invoice_Rate = document.getElementById('wucShortGC1_hdn_Applicable_Standard_Invoice_Rate');
        var hdn_Applicable_Standard_Invoice_Per_How_Many_Rs = document.getElementById('wucShortGC1_hdn_Applicable_Standard_Invoice_Per_How_Many_Rs');
        var txt_ChargeWeight = document.getElementById('wucShortGC1_txt_ChargeWeight');     
                
        var FOV = val(0);
        var System_Invoice = 0;
        
        var hdn_Fov_Charge_Discount_Percent = document.getElementById('wucShortGC1_hdn_Fov_Charge_Discount_Percent'); 
        var chk_IsInsured = document.getElementById('wucShortGC1_chk_IsInsured');
        
        var Discounted_FOV = 0 ;        
        var Discount = 0 ;
        var Invoice_Difference = 0;
        var Mod = 0;

        // 1 for owner risk    
        
        if (( hdn_Is_Opening_GC.value != 'True' || hdn_MenuItemId.value != 200 ) &&  hdn_Rectification_GC_Id.value <= 0 )
        if ((ddl_GCRisk.value == 2 ||ddl_GCRisk.value == 3) || ( ddl_GCRisk.value == 1 && chk_IsInsured.checked == false ))    // for carrier risk (2) and none (3)
        {
             FOV = val(lbl_TotalInvoiceAmount.innerHTML)*val(hdn_Applicable_Standard_FOVPercentage.value)/100;
             
            if (hdn_Is_FOV_Calculated_As_Per_Standard.value == 'True')
            {
                FOV = val(lbl_TotalInvoiceAmount.innerHTML) * val(hdn_Applicable_Standard_FOVPercentage.value) / 100;
            }
            else
            {
                System_Invoice = val(txt_ChargeWeight.value) * val(hdn_Applicable_Standard_Invoice_Rate.value);

                Invoice_Difference = val(lbl_TotalInvoiceAmount.innerHTML) - System_Invoice;
                Invoice_Difference = Math.round(Invoice_Difference);
                Mod = val(Invoice_Difference) % 1000;

                if (val(lbl_TotalInvoiceAmount.innerHTML) > val(System_Invoice))
                {
                    if (Mod > 0)
                    {
                        var Difference = 1000 - Mod;
                        Invoice_Difference = Invoice_Difference + Difference;
                    }
                    
                    if(val(hdn_Applicable_Standard_Invoice_Per_How_Many_Rs.value)<=0)
                    {
                        FOV = 0;
                    }
                    else
                    {
                       FOV = (val(Invoice_Difference)) * val(hdn_Applicable_Standard_FOVRate.value)
                             / val(hdn_Applicable_Standard_Invoice_Per_How_Many_Rs.value);
                    }
                }
                else
                {
                    FOV = 0;
                }
             }
             
             if (  val( FOV )< val( hdn_Applicable_Standard_MinimumFOV.value))
             {
                FOV= val( hdn_Applicable_Standard_MinimumFOV.value);
                txt_FOVRiskCharge.value= val(FOV);
             }
             else
             {
                txt_FOVRiskCharge.value = val(FOV);
             }
        } 
        else
        {
           txt_FOVRiskCharge.value=val(0) ;   
           hdn_FOVRiskCharge.value=val(0) ;  
        }     
        txt_FOVRiskCharge.value=  val( txt_FOVRiskCharge.value );
        hdn_FOVRiskCharge.value=  val( txt_FOVRiskCharge.value );  

       if (ddl_PaymentType.value == 5 )
       {
            txt_FOVRiskCharge.value = val(0);
            hdn_FOVRiskCharge.value = val(0);
       }
    }
    
//*******************************************************************

    function On_GCRisk_Change()
    {       
        var ddl_GCRisk = document.getElementById('wucShortGC1_ddl_GCRisk');
        var txt_InsuranceCompany = document.getElementById('wucShortGC1_txt_InsuranceCompany');
        var txt_PolicyNo = document.getElementById('wucShortGC1_txt_PolicyNo');
        var txt_PolicyAmount = document.getElementById('wucShortGC1_txt_PolicyAmount');
        var txt_RiskAmount = document.getElementById('wucShortGC1_txt_RiskAmount');

        if (ddl_GCRisk.value == 2 ||ddl_GCRisk.value == 3)    // for carrier risk (2) and none (3)
        {
            txt_InsuranceCompany.value = val(0);
            txt_PolicyNo.value = val(0);
            txt_PolicyAmount.value = val(0);
            txt_RiskAmount.value = val(0);            
            txt_InsuranceCompany.disabled = true;
            txt_PolicyNo.disabled = true;
            txt_PolicyAmount.disabled = true;
            txt_RiskAmount.disabled = true;
        }
        else  // for owne risk
        {
            txt_InsuranceCompany.value = "";
            txt_PolicyNo.value = "";
            txt_PolicyAmount.value = "";
            txt_RiskAmount.value = "";
            txt_InsuranceCompany.disabled = false;
            txt_PolicyNo.disabled = false;
            txt_PolicyAmount.disabled = false;
            txt_RiskAmount.disabled = false; 
        }
    }
//*******************************************************************

    function On_GCRisk_Change_Edit()
    {       
        var ddl_GCRisk = document.getElementById('wucShortGC1_ddl_GCRisk');
        var txt_InsuranceCompany = document.getElementById('wucShortGC1_txt_InsuranceCompany');
        var txt_PolicyNo = document.getElementById('wucShortGC1_txt_PolicyNo');
        var txt_PolicyAmount = document.getElementById('wucShortGC1_txt_PolicyAmount');
        var txt_RiskAmount = document.getElementById('wucShortGC1_txt_RiskAmount');
        
        if (ddl_GCRisk.value == 2 ||ddl_GCRisk.value == 3  )    // for carrier risk (2) and none (3)
        {            
            txt_InsuranceCompany.disabled = true;
            txt_PolicyNo.disabled = true;
            txt_PolicyAmount.disabled = true;
            txt_RiskAmount.disabled = true;
        }            
        else  // for owne risk
        {
            txt_InsuranceCompany.disabled = false;
            txt_PolicyNo.disabled = false;
            txt_PolicyAmount.disabled = false;
            txt_RiskAmount.disabled = false;           
        }   
        
    }
//*******************************************************************

    function Calculate_CFT_CBM()
    {   
        var ddl_FreightBasis = document.getElementById('wucShortGC1_ddl_FreightBasis');
        var txt_TotalCBM = document.getElementById('wucShortGC1_txt_TotalCBM');
        var txt_TotalCFT = document.getElementById('wucShortGC1_txt_TotalCFT');
        var txt_LengthInFeet = document.getElementById('wucShortGC1_txt_LengthInFeet');
        var txt_WidthInFeet = document.getElementById('wucShortGC1_txt_WidthInFeet');
        var txt_HeightInFeet = document.getElementById('wucShortGC1_txt_HeightInFeet');
        var hdn_TotalCFT = document.getElementById('wucShortGC1_hdn_TotalCFT');
        var hdn_TotalCBM = document.getElementById('wucShortGC1_hdn_TotalCBM');

        txt_TotalCBM.value = val(0);
        txt_TotalCFT.value = val(0);
        hdn_TotalCFT.value = val(0);
        hdn_TotalCBM.value = val(0);
          
        if (ddl_FreightBasis.value == 4)
        {        
            Convert_InTo_Feet();
        
            txt_TotalCFT.value = val(txt_LengthInFeet.value) * val(txt_WidthInFeet.value) * 
                                val(txt_HeightInFeet.value);
                                
            txt_TotalCBM.value = val(txt_TotalCFT.value) /  parseFloat('34.328125');

            txt_TotalCFT.value = txt_TotalCFT.value ;          
            txt_TotalCBM.value = txt_TotalCBM.value ;
            hdn_TotalCFT.value = txt_TotalCFT.value ;          
            hdn_TotalCBM.value = txt_TotalCBM.value ;  
        }
        else
        {
            txt_TotalCBM.value = val(0);
            txt_TotalCFT.value = val(0);
            hdn_TotalCFT.value = val(0);
            hdn_TotalCBM.value = val(0);         
        }
        
        txt_TotalCFT.value= roundNumber(val(txt_TotalCFT.value ), 2);
        hdn_TotalCFT.value= roundNumber(val(txt_TotalCFT.value ), 2);          
        txt_TotalCBM.value= roundNumber(val(txt_TotalCBM.value ), 2);
        hdn_TotalCBM.value= roundNumber(val(txt_TotalCBM.value ), 2);  
    }

//*****************************

    function On_FreightBasis_Change()
    {  
        var ddl_FreightBasis = document.getElementById('wucShortGC1_ddl_FreightBasis');
        var tr_Volumetric = document.getElementById('wucShortGC1_tr_Volumetric');
        var lbl_mandatory_UnitOfMeasurmentWidth = document.getElementById('wucShortGC1_lbl_mandatory_UnitOfMeasurmentWidth');
        var lbl_mandatory_UnitOfMeasurmentLength = document.getElementById('wucShortGC1_lbl_mandatory_UnitOfMeasurmentLength');
        var lbl_mandatory_UnitOfMeasurmentHeight = document.getElementById('wucShortGC1_lbl_mandatory_UnitOfMeasurmentHeight');
        var tr_VolumetrickgFactor = document.getElementById('wucShortGC1_tr_VolumetrickgFactor');
        var txt_TotalCBM = document.getElementById('wucShortGC1_txt_TotalCBM');
        var txt_TotalCFT = document.getElementById('wucShortGC1_txt_TotalCFT');
        var txt_VolumetricToKgFactor = document.getElementById('wucShortGC1_txt_VolumetricToKgFactor');
        var hdn_VolumetricToKgFactor = document.getElementById('wucShortGC1_hdn_VolumetricToKgFactor');
        var ddl_VolumetricFreightUnit= document.getElementById('wucShortGC1_ddl_VolumetricFreightUnit');
        var ddl_VehicleType = document.getElementById('wucShortGC1_ddl_VehicleType');
        var hdn_Contract_UnitOfFreightId = document.getElementById('wucShortGC1_hdn_Contract_UnitOfFreightId');
        var hdn_Contract_FreightBasisId = document.getElementById('wucShortGC1_hdn_Contract_FreightBasisId');
        var hdn_Contract_FreightSubUnitId = document.getElementById('wucShortGC1_hdn_Contract_FreightSubUnitId');
        var hdn_IsContractApplied = document.getElementById('wucShortGC1_hdn_IsContractApplied');
        var ddl_Contract = document.getElementById('wucShortGC1_ddl_Contract');
        var ddl_PaymentType = document.getElementById('wucShortGC1_ddl_PaymentType');
        var hdn_Applicable_Standard_FreightRate= document.getElementById('wucShortGC1_hdn_Applicable_Standard_FreightRate');          
        var hdn_Applicable_Standard_CFTFactor = document.getElementById('wucShortGC1_hdn_Applicable_Standard_CFTFactor');   
        var hdn_ContractId = document.getElementById('wucShortGC1_hdn_ContractId');
        var hdn_Standard_FreightRate= document.getElementById('wucShortGC1_hdn_Standard_FreightRate'); 
        var hdn_Contractual_FreightRate = document.getElementById('wucShortGC1_hdn_Contractual_FreightRate'); 
        
        var txt_FreightRate = document.getElementById('wucShortGC1_txt_FreightRate'); 
        var hdn_FreightRate= document.getElementById('wucShortGC1_hdn_FreightRate');

        if (ddl_FreightBasis.value == 1 &&  hdn_Contract_UnitOfFreightId.value == 2 && ddl_Contract.value > 0  ) // for weight
        {
            hdn_IsContractApplied.value = val(0);
            Get_ContractCharges();            
            Enable_Disable_Freight_Details_Controls();               
        }
        
        if (ddl_FreightBasis.value == 2 && hdn_Contract_UnitOfFreightId.value == 4 && ddl_Contract.value > 0   ) // for Articles
        {
            hdn_IsContractApplied.value = val(0);
            Get_ContractCharges();
            Enable_Disable_Freight_Details_Controls();
        }
        
        // FreightBasis = 4 for Volumetric  
        // VolumetricFreightUnit = 1 for CFT
        // hdn_Contract_Unit_Of_Freight_Id = 3 for cft   
        
        if (ddl_FreightBasis.value == 4 && ddl_VolumetricFreightUnit.value == 1
            &&  hdn_Contract_UnitOfFreightId.value == 3  && ddl_Contract.value > 0  ) 
        {
            hdn_IsContractApplied.value = val(0);
            Get_ContractCharges();
            Enable_Disable_Freight_Details_Controls();
        }
        
        if ( ddl_FreightBasis.value == 3  ) // for Fixed
        {
            hdn_Applicable_Standard_FreightRate.value=1;        
        }

        if (ddl_FreightBasis.value == 4 ) // for Volumetric  
        {
            tr_Volumetric.style.visibility = 'visible';                        
            tr_Volumetric.style.display = 'inline';               
            
            lbl_mandatory_UnitOfMeasurmentLength.style.visibility = 'visible';
            lbl_mandatory_UnitOfMeasurmentWidth.style.visibility = 'visible';
            lbl_mandatory_UnitOfMeasurmentHeight.style.visibility = 'visible';
            
            if (  ddl_VolumetricFreightUnit.value == 3 )
            {                
                tr_VolumetrickgFactor.style.visibility = 'visible';
                tr_VolumetrickgFactor.style.display = 'inline';  
                
                lbl_mandatory_UnitOfMeasurmentLength.style.visibility = 'visible';
                lbl_mandatory_UnitOfMeasurmentWidth.style.visibility = 'visible';
                lbl_mandatory_UnitOfMeasurmentHeight.style.visibility = 'visible';
            
                txt_VolumetricToKgFactor.value = val( hdn_Applicable_Standard_CFTFactor.value);
                hdn_VolumetricToKgFactor.value = val( hdn_Applicable_Standard_CFTFactor.value);                
            }
            else
            {
                tr_VolumetrickgFactor.style.visibility = 'hidden';                
                tr_VolumetrickgFactor.style.display = 'none';  
                txt_VolumetricToKgFactor.value = val(0);
                hdn_VolumetricToKgFactor.value = val(0);
            }

            Calculate_CFT_CBM();
        }
        else
        {
            tr_Volumetric.style.visibility = 'hidden';            
            tr_VolumetrickgFactor.style.visibility = 'hidden';
            
            lbl_mandatory_UnitOfMeasurmentLength.style.visibility = 'hidden';
            lbl_mandatory_UnitOfMeasurmentWidth.style.visibility = 'hidden';            
            lbl_mandatory_UnitOfMeasurmentHeight.style.visibility = 'hidden';
            
            txt_TotalCBM.value = val(0);
            txt_TotalCFT.value = val(0);
            txt_VolumetricToKgFactor.value = val(0);
            
            tr_Volumetric.style.display = 'none';  
            tr_VolumetrickgFactor.style.display = 'none';   
        }
                
        if (ddl_FreightBasis.value != 3  ) // for other than Fixed  
        {
            if ( val(hdn_ContractId.value) > 0 && hdn_IsContractApplied.value == "1" )
            {
                hdn_Applicable_Standard_FreightRate.value = val ( hdn_Contractual_FreightRate.value); 
            }
            else
            {
                hdn_Applicable_Standard_FreightRate.value = val(hdn_Standard_FreightRate.value);
            }            
           Calculate_ChargeWeight();
        }
        
        if (ddl_PaymentType.value != 5 )  // foc
        {
            Enable_Disable_Freight_Details_Controls();   
        }
        Get_BasisFreight();        
    }
    
//*******************************************************************
 
    function On_FreightBasis_Change_Edit()
    {   
        var ddl_FreightBasis = document.getElementById('wucShortGC1_ddl_FreightBasis');
        var tr_Volumetric = document.getElementById('wucShortGC1_tr_Volumetric');
        var tr_VolumetrickgFactor = document.getElementById('wucShortGC1_tr_VolumetrickgFactor');
        var lbl_mandatory_UnitOfMeasurmentWidth = document.getElementById('wucShortGC1_lbl_mandatory_UnitOfMeasurmentWidth');
        var lbl_mandatory_UnitOfMeasurmentLength = document.getElementById('wucShortGC1_lbl_mandatory_UnitOfMeasurmentLength');
        var lbl_mandatory_UnitOfMeasurmentHeight = document.getElementById('wucShortGC1_lbl_mandatory_UnitOfMeasurmentHeight');
        var txt_TotalCBM = document.getElementById('wucShortGC1_txt_TotalCBM');
        var txt_TotalCFT = document.getElementById('wucShortGC1_txt_TotalCFT');
        var txt_VolumetricToKgFactor = document.getElementById('wucShortGC1_txt_VolumetricToKgFactor');
        var ddl_VolumetricFreightUnit= document.getElementById('wucShortGC1_ddl_VolumetricFreightUnit');
 
        if (ddl_FreightBasis.value == 4 )
        {
            tr_Volumetric.style.visibility = 'visible';            
            tr_Volumetric.style.display = 'inline';  
            lbl_mandatory_UnitOfMeasurmentLength.style.visibility = 'visible';
            lbl_mandatory_UnitOfMeasurmentWidth.style.visibility = 'visible';            
            lbl_mandatory_UnitOfMeasurmentHeight.style.visibility = 'visible';
            
            if (ddl_VolumetricFreightUnit.value == 3 )
            {
                 tr_VolumetrickgFactor.style.visibility = 'visible';                 
                 tr_VolumetrickgFactor.style.display = 'inline';  
                 
                 lbl_mandatory_UnitOfMeasurmentLength.style.visibility = 'visible';
                 lbl_mandatory_UnitOfMeasurmentWidth.style.visibility = 'visible';            
                 lbl_mandatory_UnitOfMeasurmentHeight.style.visibility = 'visible';
            }
            else
            {
                tr_VolumetrickgFactor.style.visibility = 'hidden';                
                tr_VolumetrickgFactor.style.display = 'none';  
                
                lbl_mandatory_UnitOfMeasurmentLength.style.visibility = 'hidden';
                lbl_mandatory_UnitOfMeasurmentWidth.style.visibility = 'hidden'; 
                lbl_mandatory_UnitOfMeasurmentHeight.style.visibility = 'hidden';
                
                txt_VolumetricToKgFactor.value = val(0);
            }
            Calculate_CFT_CBM();
        }
        else
        {
            tr_Volumetric.style.visibility = 'hidden';
            tr_VolumetrickgFactor.style.visibility = 'hidden';
            lbl_mandatory_UnitOfMeasurmentLength.style.visibility = 'hidden';
            lbl_mandatory_UnitOfMeasurmentWidth.style.visibility = 'hidden';            
            lbl_mandatory_UnitOfMeasurmentHeight.style.visibility = 'hidden';            
             
            txt_TotalCBM.value = val(0);
            txt_TotalCFT.value = val(0);
            txt_VolumetricToKgFactor.value = val(0);
            tr_Volumetric.style.display = 'none';  
            tr_VolumetrickgFactor.style.display = 'none';  
        }
    }

//*******************************************************************

    function ddl_UnitOfMeasurment_Change()
    {    
        Convert_InTo_Feet();
        Calculate_CFT_CBM();        
        Get_BasisFreight();
    }    
//*******************************************************************
    function Convert_InTo_Feet()
    {   
        var ddl_UnitOfMeasurment = document.getElementById('wucShortGC1_ddl_UnitOfMeasurment');
        var txt_UnitOfMeasurmentLength = document.getElementById('wucShortGC1_txt_UnitOfMeasurmentLength');
        var txt_UnitOfMeasurmentWidth = document.getElementById('wucShortGC1_txt_UnitOfMeasurmentWidth');
        var txt_UnitOfMeasurmentHeight = document.getElementById('wucShortGC1_txt_UnitOfMeasurmentHeight');
        var txt_LengthInFeet = document.getElementById('wucShortGC1_txt_LengthInFeet');
        var txt_WidthInFeet = document.getElementById('wucShortGC1_txt_WidthInFeet');
        var txt_HeightInFeet = document.getElementById('wucShortGC1_txt_HeightInFeet');
        var hdn_HeightInFeet = document.getElementById('wucShortGC1_hdn_HeightInFeet');
        var hdn_WidthInFeet = document.getElementById('wucShortGC1_hdn_WidthInFeet');
        var hdn_LengthInFeet = document.getElementById('wucShortGC1_hdn_LengthInFeet');
       
        var Convirsion_Factor =   val(0);
       
        txt_HeightInFeet.value = val(txt_UnitOfMeasurmentHeight.value) * Convirsion_Factor ;
        txt_WidthInFeet.value = val(txt_UnitOfMeasurmentWidth.value) * Convirsion_Factor ;
        txt_LengthInFeet.value = val(txt_UnitOfMeasurmentLength.value) * Convirsion_Factor ;
        txt_HeightInFeet.value = val( txt_HeightInFeet.value  );
        txt_WidthInFeet.value = val( txt_WidthInFeet.value  );
        txt_LengthInFeet.value = val( txt_LengthInFeet.value  );
        txt_HeightInFeet.value = roundNumber ( txt_HeightInFeet.value , 0 );
        txt_WidthInFeet.value = roundNumber ( txt_WidthInFeet.value , 0 );
        txt_LengthInFeet.value = roundNumber ( txt_LengthInFeet.value , 0 );

        hdn_HeightInFeet.value = roundNumber ( txt_HeightInFeet.value , 0 );
        hdn_WidthInFeet.value = roundNumber ( txt_WidthInFeet.value , 0 );
        hdn_LengthInFeet.value = roundNumber ( txt_LengthInFeet.value , 0 );
        
        if (ddl_UnitOfMeasurment.value == 1)    
        {
            Convirsion_Factor = 0.083;
        }    
        else if (ddl_UnitOfMeasurment.value == 2)    
        {
            Convirsion_Factor = 1;
        }  
        else if (ddl_UnitOfMeasurment.value == 3)    
        {
            Convirsion_Factor = 3.29;
        }    
        else if (ddl_UnitOfMeasurment.value == 4)    
        {
            Convirsion_Factor = 0.032;
        }      
        
         txt_HeightInFeet.value = val( txt_UnitOfMeasurmentHeight.value ) * Convirsion_Factor ;
         txt_WidthInFeet.value = val( txt_UnitOfMeasurmentWidth.value ) * Convirsion_Factor ;
         txt_LengthInFeet.value = val( txt_UnitOfMeasurmentLength.value ) * Convirsion_Factor ;
         txt_HeightInFeet.value = roundNumber ( txt_HeightInFeet.value , 0 );
         txt_WidthInFeet.value = roundNumber ( txt_WidthInFeet.value , 0 );
         txt_LengthInFeet.value = roundNumber ( txt_LengthInFeet.value , 0 );

         hdn_HeightInFeet.value = roundNumber ( txt_HeightInFeet.value , 0 );
         hdn_WidthInFeet.value = roundNumber ( txt_WidthInFeet.value , 0 );
         hdn_LengthInFeet.value = roundNumber ( txt_LengthInFeet.value , 0 );
    }

//*******************************************************************

    function  On_Freight_Rate_Change()
    {
        var txt_FreightRate = document.getElementById('wucShortGC1_txt_FreightRate');
        var hdn_Standard_FreightRate= document.getElementById('wucShortGC1_hdn_Standard_FreightRate'); 
        var hdn_FreightRate= document.getElementById('wucShortGC1_hdn_FreightRate');
        var hdn_Applicable_Standard_FreightRate= document.getElementById('wucShortGC1_hdn_Applicable_Standard_FreightRate'); 
        var hdn_Is_Opening_GC = document.getElementById('wucShortGC1_hdn_Is_Opening_GC');
        var hdn_MenuItemId = document.getElementById('wucShortGC1_hdn_MenuItemId');
        var hdn_Rectification_GC_Id = document.getElementById('wucShortGC1_hdn_Rectification_GC_Id');
       
        txt_FreightRate.value = val( txt_FreightRate.value );
    
        var hdn_Freight_Charge_Discount_Percent = document.getElementById('wucShortGC1_hdn_Freight_Charge_Discount_Percent'); 
        var hdn_Is_Validate_Freight_On_Article = document.getElementById('wucShortGC1_hdn_Is_Validate_Freight_On_Article'); 
        var ddl_FreightBasis = document.getElementById('wucShortGC1_ddl_FreightBasis');
        var Discounted_Freight_Rate = 0 ;
        var Discount_Rate = 0 ;
        
         Discount_Rate = val(hdn_Applicable_Standard_FreightRate.value) * val(hdn_Freight_Charge_Discount_Percent.value) / 100 ; 
         Discounted_Freight_Rate = val(hdn_Applicable_Standard_FreightRate.value) - val(Discount_Rate); 

        if (( hdn_Is_Opening_GC.value != 'True' || hdn_MenuItemId.value != 200 ) &&  hdn_Rectification_GC_Id.value <= 0 )
        if ( val(txt_FreightRate.value ) < val( hdn_Applicable_Standard_FreightRate.value ))
        {                
            if ( val(txt_FreightRate.value) == val(Discounted_Freight_Rate) ) //|| val(Discounted_Freight)  == 0)
            {                    
                //txt_Freight.value = val(Discounted_Freight);
                //txt_Freight.value = val(Freight_value);
            }   
          else if ( val(txt_FreightRate.value) < val(Discounted_Freight_Rate) )  
            {
                // FreightBasis = 2 : Articles
                if ( val(txt_FreightRate.value) <= 0 )
                {
                    txt_FreightRate.value = val(hdn_Applicable_Standard_FreightRate.value );  
                }
                else if (ddl_FreightBasis.value == 2 && hdn_Is_Validate_Freight_On_Article.value != 'True' )
                {
                }
                else if (ddl_FreightBasis.value == 2 && hdn_Is_Validate_Freight_On_Article.value == 'True' )
                {
                    txt_FreightRate.value = val(hdn_Applicable_Standard_FreightRate.value );  
                }
                else if (ddl_FreightBasis.value != 2 )
                {
                    txt_FreightRate.value = val(hdn_Applicable_Standard_FreightRate.value );  
                }
            }
            else
            {
            }
        }         
        hdn_FreightRate.value = val(txt_FreightRate.value);

        Calculate_Freight('0');
        Calculate_GrandTotal();
        On_CashAmount_Change();
        Set_Payment_Details();        
    }
    
//*******************************************************************
 
    function On_Freight_Change()
    {    
        Calculate_Freight('From Freight');
        Calculate_GrandTotal();
        On_CashAmount_Change();        
        Set_Payment_Details();    
    }    
        
//*******************************************************************
 
    function On_Actual_Weight_Change()
    {
        var txt_ActualWeight = document.getElementById('wucShortGC1_txt_ActualWeight');        
        txt_ActualWeight.value = val(txt_ActualWeight.value);
        
        Calculate_ChargeWeight();
        Calculate_Freight('0'); 
        Calculate_GrandTotal();
        On_CashAmount_Change();        
        Set_Payment_Details();
    }
        
//*******************************************************************
    function On_ChargeWeight_Change()
    {
        var hdn_Contract_UnitOfFreightId = document.getElementById('wucShortGC1_hdn_Contract_UnitOfFreightId');
        var ddl_Contract = document.getElementById('wucShortGC1_ddl_Contract');
        var hdn_IsContractApplied = document.getElementById('wucShortGC1_hdn_IsContractApplied');
        var txt_ChargeWeight = document.getElementById('wucShortGC1_txt_ChargeWeight');
        
        txt_ChargeWeight.value = val(txt_ChargeWeight.value); 

        Validate_ChargeWeight();
        
        if ( ( hdn_Contract_UnitOfFreightId.value == 2 || 
                hdn_Contract_UnitOfFreightId.value == 5 ) 
                && ddl_Contract.value > 0  ) // for weight
        {
            hdn_IsContractApplied.value = val(0);
            Get_ContractCharges();
            Enable_Disable_Freight_Details_Controls();
        }
      
        Calculate_DDODA_Charge();
        Calculate_Freight('0');
        Calculate_LoadingCharge_On_ChargeWeight_Change();        
        Calculate_FOV();        
        Calculate_GrandTotal();
        On_CashAmount_Change();        
        Set_Payment_Details();
    }
    
//*******************************************************************
 
    function On_Measurment_Unit_Change()
    {
//        var hdn_Contract_UnitOfFreightId = document.getElementById('wucShortGC1_hdn_Contract_UnitOfFreightId');
//        var ddl_Contract = document.getElementById('wucShortGC1_ddl_Contract');
        var txt_UnitOfMeasurmentHeight = document.getElementById('wucShortGC1_txt_UnitOfMeasurmentHeight');
        var txt_UnitOfMeasurmentWidth = document.getElementById('wucShortGC1_txt_UnitOfMeasurmentWidth');
        var txt_UnitOfMeasurmentLength = document.getElementById('wucShortGC1_txt_UnitOfMeasurmentLength');
        var hdn_UnitOfMeasurmentHeight = document.getElementById('wucShortGC1_hdn_UnitOfMeasurmentHeight');
        var hdn_UnitOfMeasurmentWidth = document.getElementById('wucShortGC1_hdn_UnitOfMeasurmentWidth');
        var hdn_UnitOfMeasurmentLength = document.getElementById('wucShortGC1_hdn_UnitOfMeasurmentLength');
      
        txt_UnitOfMeasurmentHeight.value = val(txt_UnitOfMeasurmentHeight.value);
        txt_UnitOfMeasurmentLength.value = val(txt_UnitOfMeasurmentLength.value);
        txt_UnitOfMeasurmentWidth.value = val(txt_UnitOfMeasurmentWidth.value);      
        hdn_UnitOfMeasurmentHeight.value  = val(txt_UnitOfMeasurmentHeight.value);
        hdn_UnitOfMeasurmentLength.value  = val(txt_UnitOfMeasurmentLength.value);
        hdn_UnitOfMeasurmentWidth.value  = val(txt_UnitOfMeasurmentWidth.value);
        
        ddl_UnitOfMeasurment_Change();
        On_VolumetricToKgFactor_Change();
        Calculate_ChargeWeight();
        Calculate_Freight('0');
        Calculate_GrandTotal();
        On_CashAmount_Change();        
        Set_Payment_Details();
    }
    
    function On_Private_Mark_Change()
    {
        var txt_Private_Mark = document.getElementById('wucShortGC1_txt_Private_Mark');
        var hdn_Private_Mark = document.getElementById('wucShortGC1_hdn_Private_Mark');
        
        hdn_Private_Mark.value = txt_Private_Mark.value;
    }    
    
//*******************************************************************
 
   function Calculate_Freight(Call_From)
    {
        var ddl_FreightBasis = document.getElementById('wucShortGC1_ddl_FreightBasis');
        var ddl_VolumetricFreightUnit = document.getElementById('wucShortGC1_ddl_VolumetricFreightUnit');
        var txt_Freight = document.getElementById('wucShortGC1_txt_Freight');
        var txt_TotalCFT = document.getElementById('wucShortGC1_txt_TotalCFT');
        var txt_TotalCBM = document.getElementById('wucShortGC1_txt_TotalCBM');
        var txt_ChargeWeight = document.getElementById('wucShortGC1_txt_ChargeWeight');
        var lbl_TotalArticles = document.getElementById('wucShortGC1_lbl_TotalArticles');
        var lbl_TotalWeight = document.getElementById('wucShortGC1_lbl_TotalWeight');
        var hdn_TotalWeight = document.getElementById('wucShortGC1_hdn_TotalWeight');
        var txt_FreightRate = document.getElementById('wucShortGC1_txt_FreightRate');
        var hdn_Standard_FreightRate= document.getElementById('wucShortGC1_hdn_Standard_FreightRate'); 
        var ddl_PaymentType = document.getElementById('wucShortGC1_ddl_PaymentType');
        var hdn_Freight = document.getElementById('wucShortGC1_hdn_Freight');
        var hdn_CompanyParameter_Standard_FreightRatePer= document.getElementById('wucShortGC1_hdn_CompanyParameter_Standard_FreightRatePer');
        var hdn_Applicable_Standard_FreightRate = document.getElementById('wucShortGC1_hdn_Applicable_Standard_FreightRate');
        var hdn_Applicable_Standard_FreightAmount = document.getElementById('wucShortGC1_hdn_Applicable_Standard_FreightAmount');
        
        var hdn_Applicable_Standard_FreightRate_For_Calculation = document.getElementById('wucShortGC1_hdn_Applicable_Standard_FreightRate_For_Calculation'); 
        
        var Freight_value =  val(0);
        var Standard_Freight_value =  val(0);
        var hdn_Freight_Charge_Discount_Percent = document.getElementById('wucShortGC1_hdn_Freight_Charge_Discount_Percent'); 
        var Discounted_Freight = 0 ;
        var Discount = 0 ;
                          
        var hdn_Is_Opening_GC = document.getElementById('wucShortGC1_hdn_Is_Opening_GC');
        var hdn_MenuItemId = document.getElementById('wucShortGC1_hdn_MenuItemId');
        
        var hdn_Rectification_GC_Id = document.getElementById('wucShortGC1_hdn_Rectification_GC_Id');

        if (ddl_PaymentType.value != 5 ) // foc
        {
            if (ddl_FreightBasis.value == 4 && ddl_VolumetricFreightUnit.value== 1) // for cft
            {
                Calculate_CFT_CBM();
                Freight_value= val(txt_TotalCFT.value) * val(txt_FreightRate.value);                

                Standard_Freight_value = val(txt_TotalCFT.value) * val(hdn_Applicable_Standard_FreightRate.value);                                
            }
            else if (ddl_FreightBasis.value == 4 && ddl_VolumetricFreightUnit.value == 2) // for cbm
            {
                Calculate_CFT_CBM();
                Freight_value  = val(txt_TotalCBM.value) * val(txt_FreightRate.value);               
                Standard_Freight_value   = val(txt_TotalCBM.value) * val(hdn_Applicable_Standard_FreightRate.value);               
            }
            else if (ddl_FreightBasis.value == 4 && ddl_VolumetricFreightUnit.value == 3) // for kg
            {
                Validate_ChargeWeight();

                Freight_value = val(txt_ChargeWeight.value) / val ( hdn_CompanyParameter_Standard_FreightRatePer.value)
                                * val(txt_FreightRate.value);

                Standard_Freight_value   = val(txt_ChargeWeight.value) / val ( hdn_CompanyParameter_Standard_FreightRatePer.value)
                                    * val(hdn_Applicable_Standard_FreightRate.value);               
            }
            else if (ddl_FreightBasis.value == 1 )  // for wt
            {
                if (  val(txt_ChargeWeight.value) < val(hdn_TotalWeight.value))
                {
                    txt_ChargeWeight.value =  val( hdn_TotalWeight.value );//lbl_TotalWeight.innerHTML );
                    Calculate_ChargeWeight();
                }

                Freight_value = val(txt_ChargeWeight.value)  / val ( hdn_CompanyParameter_Standard_FreightRatePer.value)
                                 * val(txt_FreightRate.value);

                Standard_Freight_value   = val(txt_ChargeWeight.value) / val ( hdn_CompanyParameter_Standard_FreightRatePer.value)
                                     * val(hdn_Applicable_Standard_FreightRate.value);               
            }
            else if (ddl_FreightBasis.value == 2 )// for article
            {
                Calculate_ChargeWeight();
                Freight_value = val(lbl_TotalArticles.innerHTML) * val(txt_FreightRate.value);        


                Standard_Freight_value   = val(lbl_TotalArticles.innerHTML) * val(hdn_Applicable_Standard_FreightRate.value);               
            }
            else if (ddl_FreightBasis.value == 3 )// for Fixed
            {                             
                txt_Freight.value = val(txt_FreightRate.value);

                Freight_value = val(txt_FreightRate.value);
                Standard_Freight_value   =  val(hdn_Applicable_Standard_FreightRate.value);               
            }
            else
            {
                Freight_value  = val(txt_ChargeWeight.value)  / val ( hdn_CompanyParameter_Standard_FreightRatePer.value)
                                 * val(txt_FreightRate.value);                

                Standard_Freight_value   = val(txt_ChargeWeight.value)  / val ( hdn_CompanyParameter_Standard_FreightRatePer.value)
                                         * val(hdn_Applicable_Standard_FreightRate.value);               
            }
            
            Discount  =  Freight_value * val(hdn_Freight_Charge_Discount_Percent.value) / 100 ; 
            
            Discounted_Freight = val( Freight_value )  - val ( Discount ); 
            
            if (( hdn_Is_Opening_GC.value != 'True' || hdn_MenuItemId.value != 200 ) &&  hdn_Rectification_GC_Id.value <= 0 )
            {
                if (Call_From == 'From Freight')
                {
                    if ( val(txt_Freight.value) < val( Standard_Freight_value ))
                    {                
                        if ( val(txt_Freight.value) == val(Discounted_Freight) || val(Discounted_Freight)  == 0)
                        {                    
                        }   
                      else if ( val(txt_Freight.value) < val(Discounted_Freight) )
                        {                    
                            txt_Freight.value = val(Standard_Freight_value);
                        }   
                        else
                        {
                        }
                    }                     
                    Get_Freight_Rate();
                }
                else //if (( hdn_Is_Opening_GC.value != 'True' || hdn_MenuItemId.value != 200 ) &&  hdn_Rectification_GC_Id.value <= 0 )
                {
                    txt_Freight.value = val(Freight_value);
                }
            }
            else
            {
                if (Call_From == 'From Freight')
                {
                
                }
                else
                {
                   txt_Freight.value = val(Freight_value);
                }
            }            
        }
        else
        {
            txt_Freight.value = val(0);
            hdn_Applicable_Standard_FreightAmount.value  = val(0); 
        }

        txt_Freight.value= roundNumber ( txt_Freight.value , 0 );
        hdn_Freight.value= roundNumber ( txt_Freight.value , 0 ); 
        hdn_Applicable_Standard_FreightAmount.value  = roundNumber(Standard_Freight_value,2); 
    }     
//*******************************************************************
 
  function Get_Freight_Rate()
  {
        var ddl_FreightBasis = document.getElementById('wucShortGC1_ddl_FreightBasis');
        var ddl_VolumetricFreightUnit = document.getElementById('wucShortGC1_ddl_VolumetricFreightUnit');
        var txt_Freight = document.getElementById('wucShortGC1_txt_Freight');
        var txt_TotalCFT = document.getElementById('wucShortGC1_txt_TotalCFT');
        var txt_TotalCBM = document.getElementById('wucShortGC1_txt_TotalCBM');
        var txt_ChargeWeight = document.getElementById('wucShortGC1_txt_ChargeWeight');
        var lbl_TotalArticles = document.getElementById('wucShortGC1_lbl_TotalArticles');
        var lbl_TotalWeight = document.getElementById('wucShortGC1_lbl_TotalWeight');
        var hdn_TotalWeight = document.getElementById('wucShortGC1_hdn_TotalWeight');
        var txt_FreightRate = document.getElementById('wucShortGC1_txt_FreightRate');
        var hdn_FreightRate = document.getElementById('wucShortGC1_hdn_FreightRate');
        var hdn_Standard_FreightRate= document.getElementById('wucShortGC1_hdn_Standard_FreightRate'); 
        var ddl_PaymentType = document.getElementById('wucShortGC1_ddl_PaymentType');
        var hdn_Freight = document.getElementById('wucShortGC1_hdn_Freight');
        var hdn_CompanyParameter_Standard_FreightRatePer= document.getElementById('wucShortGC1_hdn_CompanyParameter_Standard_FreightRatePer');
        var hdn_Applicable_Standard_FreightRate = document.getElementById('wucShortGC1_hdn_Applicable_Standard_FreightRate');
        var hdn_Applicable_Standard_FreightRate_For_Calculation = document.getElementById('wucShortGC1_hdn_Applicable_Standard_FreightRate_For_Calculation'); 
        
        var Freight_value =  val(0);
        var Standard_Freight_value =  val(0);

        var Temp_Freight_Rate =  val(0);

        var hdn_Freight_Charge_Discount_Percent = document.getElementById('wucShortGC1_hdn_Freight_Charge_Discount_Percent'); 
        
        var Discounted_Freight = 0 ;
        
        var Discount = 0 ;
         
        Freight_value =  val(txt_Freight.value );
        
        if (ddl_PaymentType.value != 5 )     // foc
        {
            if (ddl_FreightBasis.value == 4 && ddl_VolumetricFreightUnit.value== 1) // for cft
            {
                Calculate_CFT_CBM();
                
                Temp_Freight_Rate = val(Freight_value) / val(txt_TotalCFT.value); 
            }
            else if (ddl_FreightBasis.value == 4 && ddl_VolumetricFreightUnit.value == 2) // for cbm
            {
                Calculate_CFT_CBM();
                
                Temp_Freight_Rate = val(Freight_value) / val(txt_TotalCBM.value); 
            }
            else if (ddl_FreightBasis.value == 4 && ddl_VolumetricFreightUnit.value == 3) // for kg
            {                
                Temp_Freight_Rate = val(Freight_value) * val(hdn_CompanyParameter_Standard_FreightRatePer.value) 
                                    / val(txt_ChargeWeight.value);
            }
            else if (ddl_FreightBasis.value == 1 )  // for wt
            {
                Temp_Freight_Rate = val(Freight_value) * val(hdn_CompanyParameter_Standard_FreightRatePer.value) 
                                    / val(txt_ChargeWeight.value);
            }
            else if (ddl_FreightBasis.value == 2 )// for article
            {                
                Temp_Freight_Rate = val(Freight_value) / val(lbl_TotalArticles.innerHTML) ;
            }
            else if (ddl_FreightBasis.value == 3 )// for Fixed
            {   
                Temp_Freight_Rate = val(Freight_value) ;                
            }
            else
            {                
                Temp_Freight_Rate = val(Freight_value) * val ( hdn_CompanyParameter_Standard_FreightRatePer.value) 
                                    / val(txt_ChargeWeight.value)  ;
            }
            
            txt_FreightRate.value= val(Temp_Freight_Rate );
            hdn_FreightRate.value= val(Temp_Freight_Rate );
            
            if ( val(txt_FreightRate.value ) < val( hdn_Applicable_Standard_FreightRate.value ))
            {   
                On_Freight_Rate_Change();
            }
        }
        else
        {
            txt_FreightRate.value = val(0);
            hdn_FreightRate.value = val(0);
        }        
  }
//*******************************************************************
 
    function Validate_ChargeWeight()
        {                  
            var ChargeWeight ;
            var ddl_FreightBasis = document.getElementById('wucShortGC1_ddl_FreightBasis');
            var ddl_VolumetricFreightUnit = document.getElementById('wucShortGC1_ddl_VolumetricFreightUnit');
            var lbl_TotalWeight = document.getElementById('wucShortGC1_lbl_TotalWeight');        
            var hdn_TotalWeight = document.getElementById('wucShortGC1_hdn_TotalWeight');
            
            var txt_ActualWeight = document.getElementById('wucShortGC1_txt_ActualWeight');
            var txt_ChargeWeight = document.getElementById('wucShortGC1_txt_ChargeWeight');
            var txt_TotalCFT = document.getElementById('wucShortGC1_txt_TotalCFT');
            var txt_VolumetricToKgFactor = document.getElementById('wucShortGC1_txt_VolumetricToKgFactor');
            
            var hdn_Applicable_Standard_MinimumChargeWeight = document.getElementById('wucShortGC1_hdn_Applicable_Standard_MinimumChargeWeight');
            
            var hdn_ChargeWeight = document.getElementById('wucShortGC1_hdn_ChargeWeight');
      
            ChargeWeight = 0;
            
            if (  val(txt_ActualWeight.value) < val( hdn_TotalWeight.value)) //  lbl_TotalWeight.innerHTML))
            {
                txt_ActualWeight.value = val(hdn_TotalWeight.value);
            }            
            if (  val(txt_ChargeWeight.value) < val(txt_ActualWeight.value))
            {
                txt_ChargeWeight.value= val(txt_ActualWeight.value);
                hdn_ChargeWeight.value= val( txt_ActualWeight.value);
            }            
             hdn_ChargeWeight.value = val( txt_ChargeWeight.value);
                 
            if (ddl_FreightBasis.value == 4 && ddl_VolumetricFreightUnit.value == 3)
            {
                ChargeWeight = val(txt_TotalCFT.value) * val(txt_VolumetricToKgFactor.value);
                ChargeWeight = val(txt_ChargeWeight.value);
            }

            var Mod = val(ChargeWeight) % 5;
            
            if (Mod > 0)
            {
                var Difference = 5-Mod;
                ChargeWeight = val(ChargeWeight) + Difference;
                ChargeWeight = val(ChargeWeight);
            }
            if (val( ChargeWeight)< val(hdn_Applicable_Standard_MinimumChargeWeight.value))
            {
                ChargeWeight = val(hdn_Applicable_Standard_MinimumChargeWeight.value);
            }
            if (val(txt_ChargeWeight.value ) < val( ChargeWeight) )
            {
                txt_ChargeWeight.value = ChargeWeight ;
            }
            
            txt_ChargeWeight.value = roundNumber (  val( txt_ChargeWeight.value ), 0 );
            hdn_ChargeWeight.value= val( txt_ChargeWeight.value );
        }

//*******************************************************************
 
        function Calculate_ChargeWeight()
        {
            var ddl_FreightBasis = document.getElementById('wucShortGC1_ddl_FreightBasis');
            var ddl_VolumetricFreightUnit = document.getElementById('wucShortGC1_ddl_VolumetricFreightUnit');
            var lbl_TotalWeight = document.getElementById('wucShortGC1_lbl_TotalWeight');        
            var hdn_TotalWeight = document.getElementById('wucShortGC1_hdn_TotalWeight');
            var txt_ActualWeight = document.getElementById('wucShortGC1_txt_ActualWeight');
            var txt_ChargeWeight = document.getElementById('wucShortGC1_txt_ChargeWeight');
            var txt_TotalCFT = document.getElementById('wucShortGC1_txt_TotalCFT');
            var txt_VolumetricToKgFactor = document.getElementById('wucShortGC1_txt_VolumetricToKgFactor');
            
            var hdn_Applicable_Standard_MinimumChargeWeight = document.getElementById('wucShortGC1_hdn_Applicable_Standard_MinimumChargeWeight');
            
            var hdn_ChargeWeight = document.getElementById('wucShortGC1_hdn_ChargeWeight');
      
            if (  val(txt_ActualWeight.value) < val( hdn_TotalWeight.value)) //  lbl_TotalWeight.innerHTML))
            {
                txt_ActualWeight.value = val(hdn_TotalWeight.value);
            }            
            //if (  val(txt_ChargeWeight.value) < val(txt_ActualWeight.value)) by dinesh on 270109
            {
                txt_ChargeWeight.value= val(txt_ActualWeight.value);
                hdn_ChargeWeight.value= val( txt_ActualWeight.value);
            }
                                     
            hdn_ChargeWeight.value = val( txt_ChargeWeight.value);
                 
            if (ddl_FreightBasis.value == 4 && ddl_VolumetricFreightUnit.value == 3)
            {
                txt_ChargeWeight.value = val(txt_TotalCFT.value) * val(txt_VolumetricToKgFactor.value);
                txt_ChargeWeight.value = val(txt_ChargeWeight.value);
            }

            var Mod = val(txt_ChargeWeight.value) % 5;
            
            if (Mod > 0)
            {
                var Difference = 5-Mod;
                txt_ChargeWeight.value = val(txt_ChargeWeight.value) + Difference;
                txt_ChargeWeight.value = val(txt_ChargeWeight.value);
            }
                    
            if (val( txt_ChargeWeight.value)< val(hdn_Applicable_Standard_MinimumChargeWeight.value))
            {
                txt_ChargeWeight.value = val(hdn_Applicable_Standard_MinimumChargeWeight.value);
            }
            
            txt_ChargeWeight.value= roundNumber (  val( txt_ChargeWeight.value ), 0 );        
            hdn_ChargeWeight.value= val( txt_ChargeWeight.value );            
        }
//*******************************************************************

    function On_BankName_Change()
    {   
        var hdn_BankName= document.getElementById('wucShortGC1_hdn_BankName');
        var txt_BankName= document.getElementById('wucShortGC1_txt_BankName');

        hdn_BankName.value =   txt_BankName.value;        
    }    
//******************************************************************* 
    function On_LocalCharge_Change()
    {        
       var hdn_LocalCharge= document.getElementById('wucShortGC1_hdn_LocalCharge');  
       var txt_LocalCharge= document.getElementById('wucShortGC1_txt_LocalCharge'); 
            
       txt_LocalCharge.value = val(txt_LocalCharge.value);
       
       hdn_LocalCharge.value = val( txt_LocalCharge.value);        
       
       Calculate_GrandTotal();
       On_CashAmount_Change();
        
       Set_Payment_Details();
    }        
//*******************************************************************
 
    function On_ReBookGCAmount_Change()
    {       
        var txt_ReBookGCAmount = document.getElementById('wucShortGC1_txt_ReBookGCAmount');        
        var hdn_ReBookGCAmount = document.getElementById('wucShortGC1_hdn_ReBookGCAmount');
        var txt_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_txt_ReBookGC_OctroiAmount');
        var hdn_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_hdn_ReBookGC_OctroiAmount');
        var hdn_ReBookGC_SubTotal = document.getElementById('wucShortGC1_hdn_ReBookGC_SubTotal');
        
        txt_ReBookGCAmount.value = val(txt_ReBookGCAmount.value);
        
        if ( val(hdn_ReBookGC_SubTotal.value) > val( txt_ReBookGCAmount.value))
        {
            txt_ReBookGCAmount.value = val(hdn_ReBookGC_SubTotal.value);
        }
                
        hdn_ReBookGCAmount.value = val( txt_ReBookGCAmount.value);        
        
        Calculate_GrandTotal();
        On_CashAmount_Change();
        
        Set_Payment_Details();
    }     
//*******************************************************************
 
    function On_LengthCharge_Change()
    {       
        var txt_LengthCharge = document.getElementById('wucShortGC1_txt_LengthCharge');        
        var hdn_LengthCharge = document.getElementById('wucShortGC1_hdn_LengthCharge');
                
        var hdn_Applicable_Standard_LengthCharge = document.getElementById('wucShortGC1_hdn_Applicable_Standard_LengthCharge');
        
        txt_LengthCharge.value = val(txt_LengthCharge.value);
        
        if ( val(hdn_Applicable_Standard_LengthCharge.value) > val( txt_LengthCharge.value))
        {
            txt_LengthCharge.value = val(hdn_Applicable_Standard_LengthCharge.value);
        }
                
        hdn_LengthCharge.value = val( txt_LengthCharge.value);        
        
        Calculate_GrandTotal();
        On_CashAmount_Change();
        
        Set_Payment_Details();
    } 
    
//******************************************************************* 

    function On_UnloadingCharge_Change()
    {       
        var txt_UnloadingCharge = document.getElementById('wucShortGC1_txt_UnloadingCharge');
        var hdn_UnloadingCharge = document.getElementById('wucShortGC1_hdn_UnloadingCharge');
                
        txt_UnloadingCharge.value = val(txt_UnloadingCharge.value);
        
        hdn_UnloadingCharge.value = val( txt_UnloadingCharge.value);        
        
        Calculate_GrandTotal();
        On_CashAmount_Change();
        
        Set_Payment_Details();
    }        
//******************************************************************* 
    function On_LoadingCharge_Change()
    {   
        var hdn_Standard_HamaliCharge= document.getElementById('wucShortGC1_hdn_Standard_HamaliCharge');  
        var txt_LoadingCharge= document.getElementById('wucShortGC1_txt_LoadingCharge');
        var hdn_LoadingCharge= document.getElementById('wucShortGC1_hdn_LoadingCharge');
        var hdn_Applicable_Standard_HamaliCharge = document.getElementById('wucShortGC1_hdn_Applicable_Standard_HamaliCharge');  

        txt_LoadingCharge.value = val(txt_LoadingCharge.value);
        Calculate_LoadingCharge();

        hdn_LoadingCharge.value = val( txt_LoadingCharge.value);
        Calculate_GrandTotal();
        On_CashAmount_Change();

        Set_Payment_Details();        
    }    
    
//******************************************************************* 

    function On_StationaryCharge_Change()
    {
        var hdn_StationaryCharge= document.getElementById('wucShortGC1_hdn_StationaryCharge');  
        var hdn_MaxStationaryCharge = document.getElementById('wucShortGC1_hdn_MaxStationaryCharge'); 
        var txt_StationaryCharge= document.getElementById('wucShortGC1_txt_StationaryCharge');
        var hdn_Applicable_Standard_BiltiCharges = document.getElementById('wucShortGC1_hdn_Applicable_Standard_BiltiCharges');
                        
        txt_StationaryCharge.value = val(txt_StationaryCharge.value);
            
        if ( val ( hdn_Applicable_Standard_BiltiCharges.value ) > val( txt_StationaryCharge.value ))
        {
            txt_StationaryCharge.value= val( hdn_Applicable_Standard_BiltiCharges.value );
        } 
        if (val(hdn_MaxStationaryCharge.value) > 0 && val(txt_StationaryCharge.value ) > val(hdn_MaxStationaryCharge.value))
        {
            txt_StationaryCharge.value= val(hdn_MaxStationaryCharge.value );
        } 
        hdn_StationaryCharge.value = val( txt_StationaryCharge.value);
            
        Calculate_GrandTotal();
        On_CashAmount_Change();
        Set_Payment_Details();       
    }    
//******************************************************************* 
    function On_FOVRiskCharge_Change()
    {
        var ddl_GCRisk = document.getElementById('wucShortGC1_ddl_GCRisk');
        var lbl_TotalInvoiceAmount = document.getElementById('wucShortGC1_lbl_TotalInvoiceAmount');
        var hdn_FOVPercentage = document.getElementById('wucShortGC1_hdn_FOVPercentage');
        var hdn_MinimumFOV = document.getElementById('wucShortGC1_hdn_MinimumFOV');
        var txt_FOVRiskCharge = document.getElementById('wucShortGC1_txt_FOVRiskCharge');
        var hdn_FOVRiskCharge = document.getElementById('wucShortGC1_hdn_FOVRiskCharge');
        var hdn_Applicable_Standard_FOVPercentage   = document.getElementById('wucShortGC1_hdn_Applicable_Standard_FOVPercentage');
        var hdn_Applicable_Standard_MinimumFOV   = document.getElementById('wucShortGC1_hdn_Applicable_Standard_MinimumFOV');
        var hdn_Standard_FOV   = document.getElementById('wucShortGC1_hdn_Standard_FOV');
        var hdn_Standard_FOVPercentage = document.getElementById('wucShortGC1_hdn_Standard_FOVPercentage');
        var ddl_PaymentType = document.getElementById('wucShortGC1_ddl_PaymentType');  
        var txt_FOVRiskCharge= document.getElementById('wucShortGC1_txt_FOVRiskCharge');  
        var hdn_MinimumFOV= document.getElementById('wucShortGC1_hdn_MinimumFOV');  

        var hdn_Applicable_Standard_MinimumFOV= document.getElementById('wucShortGC1_hdn_Applicable_Standard_MinimumFOV');  
        var hdn_FOVRiskCharge= document.getElementById('wucShortGC1_hdn_FOVRiskCharge');
        
        var ddl_GCRisk = document.getElementById('wucShortGC1_ddl_GCRisk');
        var FOV ;
        FOV=  val(0);
        
        var hdn_Fov_Charge_Discount_Percent = document.getElementById('wucShortGC1_hdn_Fov_Charge_Discount_Percent'); 
        var chk_IsInsured = document.getElementById('wucShortGC1_chk_IsInsured');
                
        var hdn_Is_FOV_Calculated_As_Per_Standard = document.getElementById('wucShortGC1_hdn_Is_FOV_Calculated_As_Per_Standard');
        
        var hdn_Applicable_Standard_FOVRate = document.getElementById('wucShortGC1_hdn_Applicable_Standard_FOVRate');
        var hdn_Applicable_Standard_Invoice_Rate = document.getElementById('wucShortGC1_hdn_Applicable_Standard_Invoice_Rate');
        var hdn_Applicable_Standard_Invoice_Per_How_Many_Rs = document.getElementById('wucShortGC1_hdn_Applicable_Standard_Invoice_Per_How_Many_Rs');
        
        var txt_ChargeWeight = document.getElementById('wucShortGC1_txt_ChargeWeight');
                
        var Discounted_FOV = 0 ;        
        var Discount = 0 ;

        var Invoice_Difference = 0;
        var Mod = 0;
        
        txt_FOVRiskCharge.value = val(txt_FOVRiskCharge.value);
        
        if ((ddl_GCRisk.value == 2 ||ddl_GCRisk.value == 3) || ( ddl_GCRisk.value == 1 && chk_IsInsured.checked == false ))    // for carrier risk (2) and none (3)
        {
            FOV = val(lbl_TotalInvoiceAmount.innerHTML)*val(hdn_Applicable_Standard_FOVPercentage.value)/100;

            if (hdn_Is_FOV_Calculated_As_Per_Standard.value == 'True')
            {
                FOV = val(lbl_TotalInvoiceAmount.innerHTML) * val(hdn_Applicable_Standard_FOVPercentage.value) / 100;
            }
            else
            {
                System_Invoice = val(txt_ChargeWeight.value) * val(hdn_Applicable_Standard_Invoice_Rate.value);
                Invoice_Difference = val(lbl_TotalInvoiceAmount.innerHTML) - System_Invoice;
                Invoice_Difference = Math.round(Invoice_Difference);
                Mod = val(Invoice_Difference) % 1000;

                if (val(lbl_TotalInvoiceAmount.innerHTML) > val(System_Invoice))
                {                
                    if (Mod > 0)
                    {
                        var Difference = 1000 - Mod;
                        Invoice_Difference = Invoice_Difference + Difference;
                    }                   
                
                    if(val(hdn_Applicable_Standard_Invoice_Per_How_Many_Rs.value)<=0)
                    {
                        FOV = 0;
                    }
                    else
                    {
                       FOV = (val(Invoice_Difference)) * val(hdn_Applicable_Standard_FOVRate.value)
                             / val(hdn_Applicable_Standard_Invoice_Per_How_Many_Rs.value);
                    }                         
                }
                else
                {
                    FOV = 0;
                    //Standard_FOV = 0;
                }
             }
             
             if (  val( FOV )< val( hdn_Applicable_Standard_MinimumFOV.value))
             {
                FOV= val( hdn_Applicable_Standard_MinimumFOV.value);
             }
         }
         else
         {
           txt_FOVRiskCharge.value=val(0) ;   
           hdn_FOVRiskCharge.value=val(0) ;           
         }
         
        Discount  =  FOV * val(hdn_Fov_Charge_Discount_Percent.value) / 100 ;             
        Discounted_FOV = val( FOV)  - val ( Discount );             

        if ( val(txt_FOVRiskCharge.value) < val( FOV ) )
        { 
          if ( val(txt_FOVRiskCharge.value) == val(Discounted_FOV) ||  val(Discounted_FOV) == 0)
            {                    
            }   
          else if ( val(txt_FOVRiskCharge.value) < val(Discounted_FOV) )
            {                    
                txt_FOVRiskCharge.value = val(FOV);
            }   
            else
            {
            }
        }         
                
        txt_FOVRiskCharge.value= val( txt_FOVRiskCharge.value );
        hdn_FOVRiskCharge.value= val( txt_FOVRiskCharge.value );  
        
        hdn_FOVRiskCharge.value = val( txt_FOVRiskCharge.value);        
        
        Calculate_GrandTotal();
        On_CashAmount_Change();        
        Set_Payment_Details();
    }
    
//*******************************************************************
 
    function On_ToPayCharge_Change()
    {   
        var hdn_ToPayCharge= document.getElementById('wucShortGC1_hdn_ToPayCharge');  
        var txt_ToPayCharge= document.getElementById('wucShortGC1_txt_ToPayCharge');  
        var hdn_Is_ToPay_Charge_Require = document.getElementById('wucShortGC1_hdn_Is_ToPay_Charge_Require');
        var hdn_Applicable_Standard_ToPayCharges = document.getElementById('wucShortGC1_hdn_Applicable_Standard_ToPayCharges');
      
        var hdn_ToPay_Charge_Discount_Percent = document.getElementById('wucShortGC1_hdn_ToPay_Charge_Discount_Percent'); 

        var Discounted_ToPay_Charge = 0 ;        
        var Discount = 0 ;

        txt_ToPayCharge.value = val(txt_ToPayCharge.value);
        
        Discount  =  val( hdn_Applicable_Standard_ToPayCharges.value ) * val(hdn_ToPay_Charge_Discount_Percent.value) / 100 ;             
        Discounted_ToPay_Charge = val( hdn_Applicable_Standard_ToPayCharges.value )  - val ( Discount );             

        if ( val(txt_ToPayCharge.value) < val( hdn_Applicable_Standard_ToPayCharges.value ) )
        {           
            if ( val(txt_ToPayCharge.value) == val(Discounted_ToPay_Charge) ||  val(Discounted_ToPay_Charge) == 0 )
            {                    
            }   
            else if ( val(txt_ToPayCharge.value) < val(Discounted_ToPay_Charge) )
            {                    
                txt_ToPayCharge.value = val(hdn_Applicable_Standard_ToPayCharges.value);
            }   
            else
            {
                txt_ToPayCharge.value = val( hdn_Applicable_Standard_ToPayCharges.value );
            }
        }         
        

        if (hdn_Is_ToPay_Charge_Require.value != 'True')
        {
            txt_ToPayCharge.value = val (0);
            hdn_ToPayCharge.value = val (0);
        }               
        
        hdn_ToPayCharge.value = val( txt_ToPayCharge.value);        
        
        Calculate_GrandTotal();
        On_CashAmount_Change();
        
        Set_Payment_Details();        
    }
    
//*******************************************************************
 
    function On_DeliveryType_Change()
    {
        var ddl_DeliveryType= document.getElementById('wucShortGC1_ddl_DeliveryType');  
        var pnl_Change_Consignee_Address= document.getElementById('wucShortGC1_pnl_Change_Consignee_Address');
        var ddl_DeliveryAgainst= document.getElementById('wucShortGC1_ddl_DeliveryAgainst');
        var hdn_Applicable_Standard_DDCharge= document.getElementById('wucShortGC1_hdn_Applicable_Standard_DDCharge');
        var hdn_Applicable_Standard_DDCharge_Rate = document.getElementById('wucShortGC1_hdn_Applicable_Standard_DDCharge_Rate');
        var hdn_Standard_DDCharge =  document.getElementById('wucShortGC1_hdn_Standard_DDCharge');
        var txt_DDCharge = document.getElementById('wucShortGC1_txt_DDCharge');
        var hdn_DDCharge = document.getElementById('wucShortGC1_hdn_DDCharge');
        var hdn_IsContractApplied = document.getElementById('wucShortGC1_hdn_IsContractApplied');
        var tr_lbl_ConsigneeDDAddressLine1 = document.getElementById('wucShortGC1_tr_lbl_ConsigneeDDAddressLine1');
        var tr_lbl_ConsigneeDDAddressLine2 = document.getElementById('wucShortGC1_tr_lbl_ConsigneeDDAddressLine2');

        if (ddl_DeliveryType.value == 2) //' for door
        {  
            pnl_Change_Consignee_Address.style.visibility = 'visible';     
            Calculate_DDODA_Charge(); 
       
            if ( hdn_IsContractApplied.value == "0")  // if contract is applied then all the controls are disables  
            {                                                 
                txt_DDCharge.disabled = false;
            }
        }
        else  // for godown
        {   
            pnl_Change_Consignee_Address.style.visibility = 'hidden'; 
            
            // for godown delivery DD Charges should be Zero
            
            txt_DDCharge.value=val(0);
            hdn_DDCharge.value=val(0);
            
            txt_DDCharge.disabled = true;                
        }
    }
    
//*******************************************************************
 
  function On_DeliveryType_Change_Edit()
    {
        var ddl_DeliveryType= document.getElementById('wucShortGC1_ddl_DeliveryType');  
        var pnl_Change_Consignee_Address= document.getElementById('wucShortGC1_pnl_Change_Consignee_Address');
        var ddl_DeliveryAgainst= document.getElementById('wucShortGC1_ddl_DeliveryAgainst');  
        var hdn_Applicable_Standard_DDCharge= document.getElementById('wucShortGC1_hdn_Applicable_Standard_DDCharge');
        var hdn_Applicable_Standard_DDCharge_Rate = document.getElementById('wucShortGC1_hdn_Applicable_Standard_DDCharge_Rate');
        var hdn_Standard_DDCharge =  document.getElementById('wucShortGC1_hdn_Standard_DDCharge');  
        var txt_DDCharge = document.getElementById('wucShortGC1_txt_DDCharge');
        var hdn_DDCharge = document.getElementById('wucShortGC1_hdn_DDCharge');
        var hdn_IsContractApplied = document.getElementById('wucShortGC1_hdn_IsContractApplied');
        var tr_lbl_ConsigneeDDAddressLine1 = document.getElementById('wucShortGC1_tr_lbl_ConsigneeDDAddressLine1');
        var tr_lbl_ConsigneeDDAddressLine2 = document.getElementById('wucShortGC1_tr_lbl_ConsigneeDDAddressLine2');
        
        if (ddl_DeliveryType.value == 2) //' for door
        {  
            pnl_Change_Consignee_Address.style.visibility = 'visible';            
        
            if (hdn_IsContractApplied.value == "0" )  // if contract is applied then all the controls are disables  
            {
                txt_DDCharge.disabled = false;
            }
        }
        else  // for godown
        {   
            pnl_Change_Consignee_Address.style.visibility = 'hidden';             
            txt_DDCharge.disabled = true;    
        }
    }
    
//*******************************************************************
 
    function On_DDCharge_Change()
    {        
        var hdn_DDCharge= document.getElementById('wucShortGC1_hdn_DDCharge');  
        var txt_DDCharge= document.getElementById('wucShortGC1_txt_DDCharge');
        var hdn_Applicable_Standard_DDCharge= document.getElementById('wucShortGC1_hdn_Applicable_Standard_DDCharge');
        var hdn_Applicable_Standard_DDCharge_Rate = document.getElementById('wucShortGC1_hdn_Applicable_Standard_DDCharge_Rate');
        var hdn_IsODA = document.getElementById('wucShortGC1_hdn_IsODA');
        var txt_ChargeWeight= document.getElementById('wucShortGC1_txt_ChargeWeight');
        var ddl_DeliveryType= document.getElementById('wucShortGC1_ddl_DeliveryType');
        var hdn_Odachargesupto500Kg= document.getElementById('wucShortGC1_hdn_Odachargesupto500Kg');
        var hdn_Odachargesabove500Kg= document.getElementById('wucShortGC1_hdn_Odachargesabove500Kg');
        var hdn_CompanyParameter_Standard_FreightRatePer= document.getElementById('wucShortGC1_hdn_CompanyParameter_Standard_FreightRatePer');
        var hdn_DD_Charge_Discount_Percent = document.getElementById('wucShortGC1_hdn_DD_Charge_Discount_Percent'); 
        
        var Discounted_DDCharge = 0 ;        
        var Discount = 0 ;       
        
        txt_DDCharge.value = val(txt_DDCharge.value);
        
       if ( hdn_IsODA.value == "True"  && ddl_DeliveryType.value == 2) // for door and oda
       {
           txt_DDCharge.disabled = false;
       
           if ( val(txt_ChargeWeight.value ) <= 500)
                DDCharge = val( hdn_Odachargesupto500Kg.value);
           else if (val(txt_ChargeWeight.value ) > 500)
                DDCharge  = val( hdn_Odachargesabove500Kg.value);
                
            Discount  =  DDCharge * val(hdn_DD_Charge_Discount_Percent.value) / 100 ;             
            Discounted_DDCharge = val( DDCharge)  - val ( Discount );             

            if ( val(txt_DDCharge.value) < val( DDCharge ) )
            {                                          
              if ( val(txt_DDCharge.value) == val(Discounted_DDCharge) || val(Discounted_DDCharge) == 0 )
                {                    
                }   
              else if ( val(txt_DDCharge.value) < val(Discounted_DDCharge) )
                {                    
                    txt_DDCharge.value = val(DDCharge);
                }   
                else
                {
                    txt_DDCharge.value = val(DDCharge);
                }
            }         
       }   
      else if (ddl_DeliveryType.value == 2) // for door deliery
         {
            DDCharge = val(txt_ChargeWeight.value ) * val (hdn_Applicable_Standard_DDCharge_Rate.value) / val( hdn_CompanyParameter_Standard_FreightRatePer.value) ;
            Discount  =  DDCharge * val(hdn_DD_Charge_Discount_Percent.value) / 100;
            Discounted_DDCharge = val(DDCharge) - val(Discount);

            if (val(txt_DDCharge.value) < val( DDCharge))
            {
                if (val(txt_DDCharge.value) < val(Discounted_DDCharge))
                {
                    txt_DDCharge.value = val(DDCharge);
                }
            }
       }   
       else
        {
            txt_DDCharge.value = val(0);
        }         
       hdn_DDCharge.value = val( txt_DDCharge.value);     
       
       Calculate_GrandTotal();
       On_CashAmount_Change();        
       Set_Payment_Details();       
    }
    
//*******************************************************************
 
    function On_DACCCharge_Change()
    {   
        var hdn_DACCCharge = document.getElementById('wucShortGC1_hdn_DACCCharge');  
        var txt_DACCCharge = document.getElementById('wucShortGC1_txt_DACCCharge');  
        var hdn_Applicable_Standard_DACCCharges = document.getElementById('wucShortGC1_hdn_Applicable_Standard_DACCCharges');

        txt_DACCCharge.value = val(txt_DACCCharge.value);

        if(val(txt_DACCCharge.value) < val(hdn_Applicable_Standard_DACCCharges.value))
        {                                                    
            txt_DACCCharge.value = val(hdn_Applicable_Standard_DACCCharges.value);          
        }         

        hdn_DACCCharge.value = val( txt_DACCCharge.value);        

        Calculate_GrandTotal();
        On_CashAmount_Change();
        Set_Payment_Details();        
    }    
 
 //*******************************************************************
    
    function On_NFormCharge_Change()
    {   
        var hdn_NFormCharge = document.getElementById('wucShortGC1_hdn_NFormCharge');
        var txt_NFormCharge = document.getElementById('wucShortGC1_txt_NFormCharge');

        txt_NFormCharge.value = val(txt_NFormCharge.value);
        hdn_NFormCharge.value = val(txt_NFormCharge.value);

        Calculate_GrandTotal();
        On_CashAmount_Change();
        Set_Payment_Details();
    }
    
//*******************************************************************
  
    function On_OtherCharge_Change()
    {  
       var hdn_OtherCharge= document.getElementById('wucShortGC1_hdn_OtherCharge');  
       var lbl_OtherChargesValue= document.getElementById('wucShortGC1_lbl_OtherChargesValue');
        
       lbl_OtherChargesValue.innerHTML = val(lbl_OtherChargesValue.innerHTML);
       hdn_OtherCharge.value = val( lbl_OtherChargesValue.innerHTML);               
       
       Calculate_GrandTotal();
       On_CashAmount_Change();        
       Set_Payment_Details();       
    }    
//******************************************************************* 
    function Calculate_GrandTotal_Edit()
    {
        var hdn_Applicable_Standard_ServiceTaxPercent = document.getElementById('wucShortGC1_hdn_Applicable_Standard_ServiceTaxPercent');
        var lbl_SubTotalValue = document.getElementById('wucShortGC1_lbl_SubTotalValue');
        var txt_ReBookGCAmount = document.getElementById('wucShortGC1_txt_ReBookGCAmount');
        var hdn_ReBookGCAmount = document.getElementById('wucShortGC1_hdn_ReBookGCAmount');
        var txt_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_txt_ReBookGC_OctroiAmount');
        var hdn_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_hdn_ReBookGC_OctroiAmount');    
        var hdn_SubTotal= document.getElementById('wucShortGC1_hdn_SubTotal');
        var txt_Freight =  document.getElementById('wucShortGC1_txt_Freight');  
        var txt_LocalCharge =  document.getElementById('wucShortGC1_txt_LocalCharge');  
        var txt_LoadingCharge = document.getElementById('wucShortGC1_txt_LoadingCharge');
        var txt_StationaryCharge = document.getElementById('wucShortGC1_txt_StationaryCharge');
        var txt_FOVRiskCharge = document.getElementById('wucShortGC1_txt_FOVRiskCharge');
        var txt_ReBookGCAmount = document.getElementById('wucShortGC1_txt_ReBookGCAmount');
        var hdn_ReBookGCAmount = document.getElementById('wucShortGC1_hdn_ReBookGCAmount');
        var txt_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_txt_ReBookGC_OctroiAmount');
        var hdn_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_hdn_ReBookGC_OctroiAmount');
        var txt_ToPayCharge = document.getElementById('wucShortGC1_txt_ToPayCharge');
        var hdn_ToPayCharge = document.getElementById('wucShortGC1_hdn_ToPayCharge');
        var txt_DDCharge = document.getElementById('wucShortGC1_txt_DDCharge');
        var lbl_OtherChargesValue = document.getElementById('wucShortGC1_lbl_OtherChargesValue');
        var lbl_SubTotalValue = document.getElementById('wucShortGC1_lbl_SubTotalValue');
        var lbl_AbatmentValue = document.getElementById('wucShortGC1_lbl_AbatmentValue');
        var lbl_TaxableAmountValue = document.getElementById('wucShortGC1_lbl_TaxableAmountValue'); 
        var lbl_ServiceTaxValue = document.getElementById('wucShortGC1_lbl_ServiceTaxValue'); 
        var lbl_TotalGCAmountValue = document.getElementById('wucShortGC1_lbl_TotalGCAmountValue');
        var txt_Advance = document.getElementById('wucShortGC1_txt_Advance'); 
        var txt_CashAmount = document.getElementById('wucShortGC1_txt_CashAmount'); 
        var txt_ChequeAmount = document.getElementById('wucShortGC1_txt_ChequeAmount');  
        var ddl_ServiceTaxPayableBy = document.getElementById('wucShortGC1_ddl_ServiceTaxPayableBy');  
        var ddl_BookingType = document.getElementById('wucShortGC1_ddl_BookingType');
        var lbl_AbatmentValue= document.getElementById('wucShortGC1_lbl_AbatmentValue');
        var hdn_Abatment= document.getElementById('wucShortGC1_hdn_Abatment');
        var lbl_TaxableAmountValue= document.getElementById('wucShortGC1_lbl_TaxableAmountValue');
        var hdn_TaxableAmount= document.getElementById('wucShortGC1_hdn_TaxableAmount');
        var lbl_TotalGCAmountValue= document.getElementById('wucShortGC1_lbl_TotalGCAmountValue');
        var hdn_TotalGCAmount= document.getElementById('wucShortGC1_hdn_TotalGCAmount');
        var ddl_PaymentType= document.getElementById('wucShortGC1_ddl_PaymentType');
        var hdn_ServiceTax= document.getElementById('wucShortGC1_hdn_ServiceTax');
        var hdn_Advance= document.getElementById('wucShortGC1_hdn_Advance');
        var hdn_CashAmount= document.getElementById('wucShortGC1_hdn_CashAmount');
        var hdn_ChequeAmount= document.getElementById('wucShortGC1_hdn_ChequeAmount');
        var hdn_ChequeNo= document.getElementById('wucShortGC1_hdn_ChequeNo');
        var hdn_ChequeAmount= document.getElementById('wucShortGC1_hdn_ChequeAmount');
        var hdn_BankName= document.getElementById('wucShortGC1_hdn_BankName');
        var txt_BankName= document.getElementById('wucShortGC1_txt_BankName');
        var txt_ChequeNo  =  document.getElementById('wucShortGC1_txt_ChequeNo');  
        var txt_DACCCharge = document.getElementById('wucShortGC1_txt_DACCCharge');  
        var hdn_DACCCharge = document.getElementById('wucShortGC1_hdn_DACCCharge');  
        var chk_IsDACC = document.getElementById('wucShortGC1_chk_IsDACC');  
        var ddl_LengthChargeHead = document.getElementById('wucShortGC1_ddl_LengthChargeHead');
        var hdn_LengthChargeHeadId = document.getElementById('wucShortGC1_hdn_LengthChargeHeadId');
        var txt_LengthCharge = document.getElementById('wucShortGC1_txt_LengthCharge');  
        var hdn_LengthCharge = document.getElementById('wucShortGC1_hdn_LengthCharge');  
        var txt_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_txt_ReBookGC_OctroiAmount');  
        var hdn_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_hdn_ReBookGC_OctroiAmount');  
        var txt_UnloadingCharge = document.getElementById('wucShortGC1_txt_UnloadingCharge');
        var hdn_UnloadingCharge = document.getElementById('wucShortGC1_hdn_UnloadingCharge');
        var hdn_MenuItemId = document.getElementById('wucShortGC1_hdn_MenuItemId');
        var hdn_NFormCharge = document.getElementById('wucShortGC1_hdn_NFormCharge');
        var txt_NFormCharge = document.getElementById('wucShortGC1_txt_NFormCharge');
        var hdn_Is_ToPay_Charge_Require = document.getElementById('wucShortGC1_hdn_Is_ToPay_Charge_Require');
        
        hdn_LengthChargeHeadId.value = ddl_LengthChargeHead.value;
        
        if ( val(hdn_LengthChargeHeadId.value ) == 0 )
        {
            txt_LengthCharge.value = val(0); 
            hdn_LengthCharge.value = val ( txt_LengthCharge.value );
            
            hdn_LengthChargeHeadId.value =0;
            ddl_LengthChargeHead.value = 0; 
        } 

        if (isNaN(txt_Freight.value)) txt_Freight.value =   val(0);
        if (isNaN(txt_LocalCharge.value)) txt_LocalCharge.value =   val(0);
        if (isNaN(txt_LoadingCharge.value)) txt_LoadingCharge.value =   val(0);
        if (isNaN(txt_StationaryCharge.value)) txt_StationaryCharge.value =   val(0);
        if (isNaN(txt_FOVRiskCharge.value)) txt_FOVRiskCharge.value =   val(0);
        if (isNaN(txt_ToPayCharge.value)) txt_ToPayCharge.value =   val(0);
        if (isNaN(txt_Freight.value)) txt_Freight.value =   val(0);
        if (isNaN(txt_DACCCharge.value)) txt_DACCCharge.value =   val(0);
        if (isNaN(txt_LengthCharge.value)) txt_LengthCharge.value =   val(0);
        if (isNaN(txt_UnloadingCharge.value)) txt_UnloadingCharge.value =   val(0);
        if (isNaN(txt_NFormCharge.value)) txt_NFormCharge.value =  val(0);
        
        txt_DACCCharge.value = val(0);
        hdn_DACCCharge.value = val(0);
                
        if ( ddl_PaymentType.value != 1 || hdn_Is_ToPay_Charge_Require.value != 'True') // for To pay
        {
            txt_ToPayCharge.value = val(0);
            hdn_ToPayCharge.value = val(0);            
        } 
        var SubTotal  = val(txt_Freight.value) +  val(txt_LocalCharge.value) 
                        + val(txt_LoadingCharge.value)   + val(txt_StationaryCharge.value)
                        + val(txt_FOVRiskCharge.value) + val(txt_ToPayCharge.value)
                        + val(txt_DDCharge.value) + val(lbl_OtherChargesValue.innerHTML)
                        + val(txt_DACCCharge.value)  
                        + val(txt_LengthCharge.value) + val(txt_UnloadingCharge.value)
                        + val(txt_NFormCharge.value);
                                              
       SubTotal  = Math.round( val( SubTotal ));
        
       if (isNaN(SubTotal )) SubTotal  =   val(0);

       if (ddl_PaymentType.value == 5 )
        {
            SubTotal  =   val(0);             
        }

        lbl_SubTotalValue.innerHTML =  val( SubTotal );
        hdn_SubTotal.value =  val( SubTotal );
        lbl_SubTotalValue.innerHTML= roundNumber (  val( lbl_SubTotalValue.innerHTML ), 0 );
        hdn_SubTotal.value= roundNumber (  val( lbl_SubTotalValue.innerHTML ), 0 );  

        var Tax_Abate =  val(SubTotal ) * 0.75;
        if (isNaN(Tax_Abate)) Tax_Abate =   val(0);

        if ( val( SubTotal ) < 750 && ddl_BookingType.value == 1) Tax_Abate =   val(0);
        if ( val( SubTotal ) < 1500 && ddl_BookingType.value != 1) Tax_Abate =   val(0);
        if (ddl_ServiceTaxPayableBy.value  != 3) Tax_Abate =   val(0); // for transporter
        if (isNaN(Tax_Abate)) Tax_Abate =   val(0);

        lbl_AbatmentValue.innerHTML = Math.round( val( Tax_Abate));
        hdn_Abatment.value = Math.round( val( Tax_Abate));

        var Amt_Taxable =  val(SubTotal ) - val(  Tax_Abate);

        if (isNaN(Amt_Taxable)) Amt_Taxable =   val(0);

        if ( val( SubTotal ) < 750 && ddl_BookingType.value == 1) Amt_Taxable =   val(0); // for sundry
        if ( val( SubTotal ) < 1500 && ddl_BookingType.value != 1) Amt_Taxable =   val(0); // for FTL

        if (isNaN(Amt_Taxable)) Amt_Taxable =   val(0);

        lbl_TaxableAmountValue.innerHTML = Math.round( val( Amt_Taxable));
        hdn_TaxableAmount.value = Math.round( val( Amt_Taxable)); 

        var ServiceTaxPercent =  val(hdn_Applicable_Standard_ServiceTaxPercent.value); // 12.36; //g_servicetaxpercent;

        var ServiceTax = ( ServiceTaxPercent /100) *  val( Amt_Taxable);

        if ( val( SubTotal  ) < 750 && ddl_BookingType.value == 1) ServiceTax =   val(0); // for sundry
        if ( val( SubTotal ) < 1500 && ddl_BookingType.value != 1) ServiceTax  =   val(0); // for FTL

        ServiceTax  = Math.round( val( ServiceTax ))
        if (isNaN(ServiceTax )) ServiceTax  =   val(0);

        lbl_ServiceTaxValue.innerHTML =  val( ServiceTax );
        hdn_ServiceTax.value =  val( ServiceTax ); 

        var GrandTotal =  val(0);

        var hdn_Is_Service_Tax_Applicable_For_Commodity = document.getElementById('wucShortGC1_hdn_Is_Service_Tax_Applicable_For_Commodity');
              
        if ( hdn_Is_Service_Tax_Applicable_For_Commodity.value == "True"  )  
        {

        }
        else
        {
            ServiceTax =  val(0);
            lbl_ServiceTaxValue.innerHTML =   val(0);    // if service tax not applicable for commodity then service tax = 0
            hdn_ServiceTax.value = val(0);
            
            lbl_TaxableAmountValue.innerHTML = val(0);            
            hdn_TaxableAmount.value = val(0);
            
            lbl_AbatmentValue.innerHTML = val(0);
            hdn_Abatment.value = val(0);
        }
        
        if (ddl_ServiceTaxPayableBy.value  != 3) // service tax paid by client 
            GrandTotal =   val(SubTotal );
        else
            GrandTotal=  val(SubTotal ) + val(ServiceTax );         
        
        GrandTotal = Math.round( val( GrandTotal) );
        
        if (isNaN(GrandTotal )) GrandTotal =   val(0);
        
        lbl_TotalGCAmountValue.innerHTML =  roundNumber ( val( GrandTotal ),0); 
        hdn_TotalGCAmount.value =   roundNumber ( val( GrandTotal ),0);        
    }    
//*******************************************************************
 
    function Calculate_GrandTotal()
    {
        var hdn_Applicable_Standard_ServiceTaxPercent = document.getElementById('wucShortGC1_hdn_Applicable_Standard_ServiceTaxPercent');
        var lbl_SubTotalValue = document.getElementById('wucShortGC1_lbl_SubTotalValue');        
        var txt_ReBookGCAmount = document.getElementById('wucShortGC1_txt_ReBookGCAmount');
        var hdn_ReBookGCAmount = document.getElementById('wucShortGC1_hdn_ReBookGCAmount');        
        var txt_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_txt_ReBookGC_OctroiAmount');
        var hdn_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_hdn_ReBookGC_OctroiAmount');    
        var hdn_SubTotal= document.getElementById('wucShortGC1_hdn_SubTotal');
        var txt_Freight =  document.getElementById('wucShortGC1_txt_Freight');  
        var txt_LocalCharge =  document.getElementById('wucShortGC1_txt_LocalCharge');  
        var txt_LoadingCharge = document.getElementById('wucShortGC1_txt_LoadingCharge');
        var txt_StationaryCharge = document.getElementById('wucShortGC1_txt_StationaryCharge');
        var txt_FOVRiskCharge = document.getElementById('wucShortGC1_txt_FOVRiskCharge');
        var txt_ReBookGCAmount = document.getElementById('wucShortGC1_txt_ReBookGCAmount');        
        var hdn_ReBookGCAmount = document.getElementById('wucShortGC1_hdn_ReBookGCAmount'); 
        var txt_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_txt_ReBookGC_OctroiAmount');
        var hdn_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_hdn_ReBookGC_OctroiAmount');    
        var txt_ToPayCharge = document.getElementById('wucShortGC1_txt_ToPayCharge');
        var hdn_ToPayCharge = document.getElementById('wucShortGC1_hdn_ToPayCharge');        
        var txt_DDCharge = document.getElementById('wucShortGC1_txt_DDCharge');
        var lbl_OtherChargesValue = document.getElementById('wucShortGC1_lbl_OtherChargesValue');
        var lbl_SubTotalValue = document.getElementById('wucShortGC1_lbl_SubTotalValue');
        var lbl_AbatmentValue = document.getElementById('wucShortGC1_lbl_AbatmentValue');
        var lbl_TaxableAmountValue = document.getElementById('wucShortGC1_lbl_TaxableAmountValue'); 
        var lbl_ServiceTaxValue = document.getElementById('wucShortGC1_lbl_ServiceTaxValue'); 
        var lbl_TotalGCAmountValue = document.getElementById('wucShortGC1_lbl_TotalGCAmountValue'); 
        var txt_Advance = document.getElementById('wucShortGC1_txt_Advance'); 
        var txt_CashAmount = document.getElementById('wucShortGC1_txt_CashAmount'); 
        var txt_ChequeAmount = document.getElementById('wucShortGC1_txt_ChequeAmount');  
        var ddl_ServiceTaxPayableBy = document.getElementById('wucShortGC1_ddl_ServiceTaxPayableBy');  
        var ddl_BookingType = document.getElementById('wucShortGC1_ddl_BookingType');
        var lbl_AbatmentValue= document.getElementById('wucShortGC1_lbl_AbatmentValue');
        var hdn_Abatment= document.getElementById('wucShortGC1_hdn_Abatment');
        var lbl_TaxableAmountValue= document.getElementById('wucShortGC1_lbl_TaxableAmountValue');
        var hdn_TaxableAmount= document.getElementById('wucShortGC1_hdn_TaxableAmount');
        var lbl_TotalGCAmountValue= document.getElementById('wucShortGC1_lbl_TotalGCAmountValue');
        var hdn_TotalGCAmount= document.getElementById('wucShortGC1_hdn_TotalGCAmount');
        var ddl_PaymentType= document.getElementById('wucShortGC1_ddl_PaymentType');
        var hdn_ServiceTax= document.getElementById('wucShortGC1_hdn_ServiceTax');
        var hdn_Advance= document.getElementById('wucShortGC1_hdn_Advance');
        var hdn_CashAmount= document.getElementById('wucShortGC1_hdn_CashAmount');
        var hdn_ChequeAmount= document.getElementById('wucShortGC1_hdn_ChequeAmount');
        var hdn_ChequeNo= document.getElementById('wucShortGC1_hdn_ChequeNo');
        var hdn_ChequeAmount= document.getElementById('wucShortGC1_hdn_ChequeAmount'); 
        var hdn_BankName= document.getElementById('wucShortGC1_hdn_BankName');
        var txt_BankName= document.getElementById('wucShortGC1_txt_BankName');
        var txt_ChequeNo  =  document.getElementById('wucShortGC1_txt_ChequeNo');  
        var txt_DACCCharge = document.getElementById('wucShortGC1_txt_DACCCharge');  
        var hdn_DACCCharge = document.getElementById('wucShortGC1_hdn_DACCCharge');  
        var txt_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_txt_ReBookGC_OctroiAmount');  
        var hdn_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_hdn_ReBookGC_OctroiAmount');          
        var chk_IsDACC = document.getElementById('wucShortGC1_chk_IsDACC');  
        var txt_LengthCharge = document.getElementById('wucShortGC1_txt_LengthCharge');  
        var hdn_LengthCharge = document.getElementById('wucShortGC1_hdn_LengthCharge');          
        var ddl_LengthChargeHead = document.getElementById('wucShortGC1_ddl_LengthChargeHead');        
        var hdn_LengthChargeHeadId = document.getElementById('wucShortGC1_hdn_LengthChargeHeadId');                
        
        var arr = ddl_LengthChargeHead.value.split("~");                                     
          
        var txt_UnloadingCharge = document.getElementById('wucShortGC1_txt_UnloadingCharge');
        var hdn_UnloadingCharge = document.getElementById('wucShortGC1_hdn_UnloadingCharge');        
        var tr_cheque_Details = document.getElementById('wucShortGC1_tr_cheque_Details');        
        var hdn_MenuItemId = document.getElementById('wucShortGC1_hdn_MenuItemId');                 
        var hdn_NFormCharge = document.getElementById('wucShortGC1_hdn_NFormCharge');
        var txt_NFormCharge = document.getElementById('wucShortGC1_txt_NFormCharge');
                
        var hdn_Is_ToPay_Charge_Require = document.getElementById('wucShortGC1_hdn_Is_ToPay_Charge_Require');
          
        hdn_LengthChargeHeadId.value = ddl_LengthChargeHead.value; //arr[0];
                
        
        if ( val(hdn_LengthChargeHeadId.value ) == 0 )
        {
            txt_LengthCharge.value = val(0); 
            hdn_LengthCharge.value = val ( txt_LengthCharge.value );
        } 

        if (isNaN(txt_Freight.value)) txt_Freight.value =   val(0);
        if (isNaN(txt_LocalCharge.value)) txt_LocalCharge.value =   val(0);
        if (isNaN(txt_LoadingCharge.value)) txt_LoadingCharge.value =   val(0);
        if (isNaN(txt_StationaryCharge.value)) txt_StationaryCharge.value =   val(0);
        if (isNaN(txt_FOVRiskCharge.value)) txt_FOVRiskCharge.value =   val(0);
        if (isNaN(txt_ToPayCharge.value)) txt_ToPayCharge.value =   val(0);
        if (isNaN(txt_Freight.value)) txt_Freight.value =   val(0);
        if (isNaN(txt_DACCCharge.value)) txt_DACCCharge.value =   val(0);        
        if (isNaN(txt_LengthCharge.value)) txt_LengthCharge.value =   val(0);
        
        if (isNaN(txt_UnloadingCharge.value)) txt_UnloadingCharge.value =   val(0);
        if (isNaN(txt_NFormCharge.value)) txt_NFormCharge.value =  val(0);
          
        txt_DACCCharge.value = val(0);
        hdn_DACCCharge.value = val(0);
                
        if ( ddl_PaymentType.value != 1  || hdn_Is_ToPay_Charge_Require.value != 'True')// for To pay
        {
            txt_ToPayCharge.value = val(0);
            hdn_ToPayCharge.value = val(0);            
        } 

        var SubTotal  = val(txt_Freight.value) +  val(txt_LocalCharge.value) 
                        + val(txt_LoadingCharge.value)   + val(txt_StationaryCharge.value)
                        + val(txt_FOVRiskCharge.value) + val(txt_ToPayCharge.value)
                        + val(txt_DDCharge.value) + val(lbl_OtherChargesValue.innerHTML)
                        + val(txt_DACCCharge.value)  
                        + val(txt_LengthCharge.value) + val(txt_UnloadingCharge.value)
                        + val(txt_NFormCharge.value);

                                              
        SubTotal  = Math.round( val( SubTotal ));
        
        if (isNaN(SubTotal )) SubTotal  =   val(0);

       if (ddl_PaymentType.value == 5 )
        {
            SubTotal  =   val(0);             
        }

        lbl_SubTotalValue.innerHTML =  val( SubTotal );
        hdn_SubTotal.value =  val( SubTotal );
        var Tax_Abate =  val(SubTotal ) * 0.75;

        if (isNaN(Tax_Abate)) Tax_Abate =   val(0);

        if ( val( SubTotal ) < 750 && ddl_BookingType.value == 1) Tax_Abate =   val(0);
        if ( val( SubTotal ) < 1500 && ddl_BookingType.value != 1) Tax_Abate =   val(0);

        if (ddl_ServiceTaxPayableBy.value  != 3) Tax_Abate =   val(0); // for transporter
        if (isNaN(Tax_Abate)) Tax_Abate =   val(0);

        lbl_AbatmentValue.innerHTML = Math.round( val( Tax_Abate));
        hdn_Abatment.value = Math.round( val( Tax_Abate));

        var Amt_Taxable =  val(SubTotal ) - val(  Tax_Abate);

        if (isNaN(Amt_Taxable)) Amt_Taxable =   val(0);

        if ( val( SubTotal ) < 750 && ddl_BookingType.value == 1) Amt_Taxable =   val(0); // for sundry
        if ( val( SubTotal ) < 1500 && ddl_BookingType.value != 1) Amt_Taxable =   val(0); // for FTL

        if (isNaN(Amt_Taxable)) Amt_Taxable =   val(0);

        lbl_TaxableAmountValue.innerHTML = Math.round( val( Amt_Taxable));
        hdn_TaxableAmount.value = Math.round( val( Amt_Taxable)); 
        
        var ServiceTaxPercent =  val(hdn_Applicable_Standard_ServiceTaxPercent.value); // 12.36; //g_servicetaxpercent;
        var ServiceTax = ( ServiceTaxPercent /100) *  val( Amt_Taxable);

        if ( val( SubTotal  ) < 750 && ddl_BookingType.value == 1) ServiceTax =   val(0); // for sundry
        if ( val( SubTotal ) < 1500 && ddl_BookingType.value != 1) ServiceTax  =   val(0); // for FTL

        ServiceTax  = Math.round( val( ServiceTax ))
        if (isNaN(ServiceTax )) ServiceTax  =   val(0);

        lbl_ServiceTaxValue.innerHTML =  val( ServiceTax );
        hdn_ServiceTax.value =  val( ServiceTax ); 

        var GrandTotal
        GrandTotal =  val(0);
        
        var hdn_Is_Service_Tax_Applicable_For_Commodity = document.getElementById('wucShortGC1_hdn_Is_Service_Tax_Applicable_For_Commodity');
              
        if ( hdn_Is_Service_Tax_Applicable_For_Commodity.value == "True"  )  
        {

        }
        else
        {
            ServiceTax =  val(0);
            lbl_ServiceTaxValue.innerHTML =   val(0);    // if service tax not applicable for commodity then service tax = 0
            hdn_ServiceTax.value = val(0);
            
            lbl_TaxableAmountValue.innerHTML = val(0);            
            hdn_TaxableAmount.value = val(0);
            
            lbl_AbatmentValue.innerHTML = val(0);
            hdn_Abatment.value = val(0);             
        }

        if (ddl_ServiceTaxPayableBy.value  != 3) // service tax paid by client 
            GrandTotal =   val(SubTotal );
        else
            GrandTotal=  val(SubTotal ) + val(ServiceTax );
        
        GrandTotal = Math.round( val( GrandTotal) );
        
        if (isNaN(GrandTotal )) GrandTotal =   val(0);
        lbl_TotalGCAmountValue.innerHTML =  val( GrandTotal ); 
        hdn_TotalGCAmount.value =  val( GrandTotal ); 

        if ( ddl_PaymentType.value == 1)
        {
            if ( val(lbl_TotalGCAmountValue.innerHTML) < val (txt_Advance.value) ) // || val(txt_ChequeAmount.value) <= 0 )
            {
                txt_Advance.value=  val(0);
                hdn_Advance.value=  val(0);
                
                txt_CashAmount.value=  val(0);
                hdn_CashAmount.value=  val(0);
                   
                txt_ChequeAmount.value = val(0);        
                hdn_ChequeAmount.value  =   val(txt_ChequeAmount.value);
                        
                txt_ChequeNo.value="";
                hdn_ChequeNo.value="";                
                txt_BankName.value="";
                hdn_BankName.value="";  
                
            }
            else
            {
                txt_CashAmount.value = val(txt_Advance.value ) - val(txt_ChequeAmount.value);
            }            
            
            if ( val (txt_Advance.value) <  val(txt_ChequeAmount.value) )
            {
                txt_ChequeAmount.value = val(0);        
                hdn_ChequeAmount.value  =   val(txt_ChequeAmount.value);
                        
                txt_ChequeNo.value="";
                hdn_ChequeNo.value="";                
                txt_BankName.value="";
                hdn_BankName.value="";                                  
            }
            
            txt_Advance.disabled = true;
            
            txt_CashAmount.disabled = false;
            txt_ChequeAmount.disabled = false;
            
            txt_ChequeNo.disabled = false;
            txt_BankName.disabled = false;
        }
        else if ( ddl_PaymentType.value == 2 ||  ddl_PaymentType.value == 4 )
        {
            txt_Advance.value=  val(0);
            hdn_Advance.value=  val(0);
            
            if ( val (lbl_TotalGCAmountValue.innerHTML) <  val(txt_ChequeAmount.value) )
            {
                txt_ChequeAmount.value =   val(0);
                hdn_ChequeAmount.value  =   val(txt_ChequeAmount.value);
                
                txt_ChequeNo.value="";
                hdn_ChequeNo.value="";                
                txt_BankName.value="";
                hdn_BankName.value="";                  
            }
            
            txt_CashAmount.value = val(lbl_TotalGCAmountValue.innerHTML) - val(txt_ChequeAmount.value);
            hdn_CashAmount.value = val(lbl_TotalGCAmountValue.innerHTML) - val(txt_ChequeAmount.value);  
        }          
    }    
//*******************************************************************
 
    function Set_Payment_Details()
    {
        var hdn_Applicable_Standard_ServiceTaxPercent = document.getElementById('wucShortGC1_hdn_Applicable_Standard_ServiceTaxPercent');

//        var lbl_SubTotalValue = document.getElementById('wucShortGC1_lbl_SubTotalValue');
        var txt_ReBookGCAmount = document.getElementById('wucShortGC1_txt_ReBookGCAmount');
        var hdn_ReBookGCAmount = document.getElementById('wucShortGC1_hdn_ReBookGCAmount');        
        var hdn_SubTotal= document.getElementById('wucShortGC1_hdn_SubTotal');
        var txt_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_txt_ReBookGC_OctroiAmount');
        var hdn_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_hdn_ReBookGC_OctroiAmount');    
        var txt_Freight =  document.getElementById('wucShortGC1_txt_Freight');  
        var txt_LocalCharge =  document.getElementById('wucShortGC1_txt_LocalCharge');  
        var txt_LoadingCharge = document.getElementById('wucShortGC1_txt_LoadingCharge');
        var txt_StationaryCharge = document.getElementById('wucShortGC1_txt_StationaryCharge');
        var txt_FOVRiskCharge = document.getElementById('wucShortGC1_txt_FOVRiskCharge');
        var txt_ReBookGCAmount = document.getElementById('wucShortGC1_txt_ReBookGCAmount');        
        var hdn_ReBookGCAmount = document.getElementById('wucShortGC1_hdn_ReBookGCAmount'); 
        var txt_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_txt_ReBookGC_OctroiAmount');
        var hdn_ReBookGC_OctroiAmount = document.getElementById('wucShortGC1_hdn_ReBookGC_OctroiAmount');    
        var txt_ToPayCharge = document.getElementById('wucShortGC1_txt_ToPayCharge');
        var hdn_ToPayCharge = document.getElementById('wucShortGC1_hdn_ToPayCharge');        
        var txt_DDCharge = document.getElementById('wucShortGC1_txt_DDCharge');
        var lbl_OtherChargesValue = document.getElementById('wucShortGC1_lbl_OtherChargesValue');
        var lbl_SubTotalValue = document.getElementById('wucShortGC1_lbl_SubTotalValue');
        var lbl_AbatmentValue = document.getElementById('wucShortGC1_lbl_AbatmentValue');
        var lbl_TaxableAmountValue = document.getElementById('wucShortGC1_lbl_TaxableAmountValue'); 
        var lbl_ServiceTaxValue = document.getElementById('wucShortGC1_lbl_ServiceTaxValue'); 
        var lbl_TotalGCAmountValue = document.getElementById('wucShortGC1_lbl_TotalGCAmountValue'); 
        var txt_Advance = document.getElementById('wucShortGC1_txt_Advance'); 
        var txt_CashAmount = document.getElementById('wucShortGC1_txt_CashAmount'); 
        var txt_ChequeAmount = document.getElementById('wucShortGC1_txt_ChequeAmount');  
        var ddl_ServiceTaxPayableBy = document.getElementById('wucShortGC1_ddl_ServiceTaxPayableBy');  
        var ddl_BookingType = document.getElementById('wucShortGC1_ddl_BookingType');
        var lbl_AbatmentValue= document.getElementById('wucShortGC1_lbl_AbatmentValue');
        var hdn_Abatment= document.getElementById('wucShortGC1_hdn_Abatment');
        var lbl_TaxableAmountValue= document.getElementById('wucShortGC1_lbl_TaxableAmountValue');
        var hdn_TaxableAmount= document.getElementById('wucShortGC1_hdn_TaxableAmount');
        var lbl_TotalGCAmountValue= document.getElementById('wucShortGC1_lbl_TotalGCAmountValue');
        var hdn_TotalGCAmount= document.getElementById('wucShortGC1_hdn_TotalGCAmount');
        var ddl_PaymentType= document.getElementById('wucShortGC1_ddl_PaymentType');
        var hdn_ServiceTax= document.getElementById('wucShortGC1_hdn_ServiceTax');
        var hdn_Advance= document.getElementById('wucShortGC1_hdn_Advance');
        var hdn_CashAmount= document.getElementById('wucShortGC1_hdn_CashAmount');
        var hdn_ChequeAmount= document.getElementById('wucShortGC1_hdn_ChequeAmount');
        var hdn_ChequeNo= document.getElementById('wucShortGC1_hdn_ChequeNo');
        var hdn_ChequeAmount= document.getElementById('wucShortGC1_hdn_ChequeAmount'); 
        var hdn_BankName= document.getElementById('wucShortGC1_hdn_BankName');
        var txt_BankName= document.getElementById('wucShortGC1_txt_BankName');
        var txt_ChequeNo  =  document.getElementById('wucShortGC1_txt_ChequeNo');  
        var txt_DACCCharge = document.getElementById('wucShortGC1_txt_DACCCharge');  
        var hdn_DACCCharge = document.getElementById('wucShortGC1_hdn_DACCCharge');          
        var chk_IsDACC = document.getElementById('wucShortGC1_chk_IsDACC');  
        var tr_cheque_Details = document.getElementById('wucShortGC1_tr_cheque_Details');
    
        if ( ddl_PaymentType.value == 1 )//&& val(txt_Advance.value) > 0)
        {            
            txt_Advance.disabled = false;            
            txt_CashAmount.disabled = false;
            txt_ChequeAmount.disabled = false;
            
            txt_ChequeNo.disabled = true;
            txt_BankName.disabled = true;
        }
        else //if ( ddl_PaymentType.value == 2 ||  ddl_PaymentType.value == 4 )
        {
            txt_Advance.disabled = true;
            
            txt_CashAmount.disabled = false;
            txt_ChequeAmount.disabled = false;
            
            txt_ChequeNo.disabled = true;
            txt_BankName.disabled = false;
        }

        if ( val ( txt_ChequeAmount.value ) > 0)
        {
            txt_ChequeNo.disabled = false;
            txt_BankName.disabled = false;         
        }
        else
        {
            txt_ChequeNo.value = '';
            txt_BankName.value = '';
            
            txt_ChequeNo.disabled = true;
            txt_BankName.disabled = true;         
        }
             
        var txt_ChequeAmount= document.getElementById('wucShortGC1_txt_ChequeAmount');
        
        if ( val(txt_ChequeAmount.value) >0)
            tr_cheque_Details.style.display = 'inline';
        else
            tr_cheque_Details.style.display = 'none';
    } 
         
//*******************************************************************
 
    function Change_Consignee_Address()
    {
        var hdn_ConsigneeId= document.getElementById('wucShortGC1_hdn_ConsigneeId');  
        var ddl_DeliveryType= document.getElementById('wucShortGC1_ddl_DeliveryType');
            
        var hdn_Mode = document.getElementById('wucShortGC1_hdn_Mode');
        var hdn_MenuItemId = document.getElementById('wucShortGC1_hdn_MenuItemId'); 
    
        if (val( hdn_ConsigneeId.value) > 0)
        {
            if (ddl_DeliveryType.value == 2)    
            {         
                var Path='FrmConsigneeDoorDeliveryAddress.aspx?Mode=' + hdn_Mode.value ;
                
                w = screen.availWidth;
                h = screen.availHeight;
                var popW = 600, popH = 400;
                var leftPos = (w-popW)/2;
                var topPos = (h-popH)/2;
                
                window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');                
             }
        }
        else
        {
            alert("Please Select Consignee...");    
        }        
        return false;
    }
     
//*******************************************************************
 
    function Require_Forms()
    {  
        var hdn_DeliveryBaranchId= document.getElementById('wucShortGC1_hdn_DeliveryBaranchId');
        var hdn_Mode = document.getElementById('wucShortGC1_hdn_Mode');
//        var hdn_MenuItemId = document.getElementById('wucShortGC1_hdn_MenuItemId'); 
    
        if (val( hdn_DeliveryBaranchId.value) > 0 )
        {        
            var Path='FrmRequireForms.aspx?Mode=' + hdn_Mode.value ;  
                  
            w = screen.availWidth;
            h = screen.availHeight;
            var popW = 600, popH = 400;
            var leftPos = (w-popW)/2;
            var topPos = (h-popH)/2;
            
            window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');     
        }
        else
        {
            alert("Please Select Delevery Location...");    
        }
    }     
     
//*******************************************************************
 
    function New_Consignor_Consignee(Is_Add,Is_Consignor)
    {      
        var hdn_EncreptedConsignorId = document.getElementById('wucShortGC1_hdn_EncreptedConsignorId');
        var hdn_EncreptedConsigneeId = document.getElementById('wucShortGC1_hdn_EncreptedConsigneeId');
        var hdn_IsRegularConsignee = document.getElementById('wucShortGC1_hdn_IsRegularConsignee');        
        var hdn_IsRegularConsignor = document.getElementById('wucShortGC1_hdn_IsRegularConsignor');
        var hdn_RegularClientCaption = document.getElementById('wucShortGC1_hdn_RegularClientCaption');

        var Allow_Popup = 0;
        
        if (Is_Add == 0)
        {
            if(Is_Consignor == 1)
            {
                var Path='../../Master/Sales/FrmRegularClient.aspx?Menu_Item_Id=MwA2AA==&Mode=MQA=&Call_From=GC' + '&Is_Consignor=1' ;                   
                Allow_Popup = 1 ;                     
            }
            else
            {             
                var Path='../../Master/Sales/FrmRegularClient.aspx?Menu_Item_Id=MwA2AA==&Mode=MQA=&Call_From=GC' + '&Is_Consignor=0' ;                   
                Allow_Popup = 1 ;                                  
            }
        }
        else
        {
            if (Is_Consignor == 1)
            {
                if (hdn_EncreptedConsignorId.value != 'MAA=')
                {
                    if ( hdn_IsRegularConsignor.value == '1')
                    {
                        var Path='../../Master/Sales/FrmRegularClient.aspx?Menu_Item_Id=MwA2AA==&Mode=MgA=&Id=' + hdn_EncreptedConsignorId.value + '&Call_From=GC' + '&Is_Consignor=1'  ;                
                        Allow_Popup = 1 ;
                    }
                    else
                    {
                        alert("Please Select " + hdn_RegularClientCaption.value +" Consignor...");    
                        Allow_Popup = 0 ;
                    }
                }
                else
                {
                    alert("Please Select Consignor...");    
                    Allow_Popup = 0 ;
                }
            }
            else
            {
                if (hdn_EncreptedConsigneeId.value != 'MAA=')
                {                    
                    if (hdn_IsRegularConsignee.value == '1')
                    {
                        var Path='../../Master/Sales/FrmRegularClient.aspx?Menu_Item_Id=MwA2AA==&Mode=MgA=&Id=' + hdn_EncreptedConsigneeId.value + '&Call_From=GC' + '&Is_Consignor=0';                
                        Allow_Popup = 1 ;
                    }
                    else 
                    {
                        alert("Please Select " + hdn_RegularClientCaption.value +" Consignee...");    
                        Allow_Popup = 0 ;
                    }
                }
                else
                {
                    alert("Please Select Consignee...");    
                    Allow_Popup = 0 ;
                }
            }
        }
       
        if (Allow_Popup == 1)
        {        
            w = screen.availWidth;
            h = screen.availHeight;
            var popW = 900, popH = 600;
            var leftPos = (w-popW)/2;
            var topPos = (h-popH)/2;
            
            window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');
        }
        return false;
    }     
      
//*******************************************************************
 
  function View_Consignor_Consignee(Is_Add,Is_Consignor)
    {      
        var hdn_EncreptedConsignorId = document.getElementById('wucShortGC1_hdn_EncreptedConsignorId');
        var hdn_EncreptedConsigneeId = document.getElementById('wucShortGC1_hdn_EncreptedConsigneeId');
        var hdn_IsRegularConsignee = document.getElementById('wucShortGC1_hdn_IsRegularConsignee');        
        var hdn_IsRegularConsignor = document.getElementById('wucShortGC1_hdn_IsRegularConsignor');
        var Allow_Popup = 0;        
        var hdn_Can_View_Consignor = document.getElementById('wucShortGC1_hdn_Can_View_Consignor');
        var hdn_Can_View_Consignee = document.getElementById('wucShortGC1_hdn_Can_View_Consignee');
        var hdn_Mode = document.getElementById('wucShortGC1_hdn_Mode'); 
                
        if ( hdn_Can_View_Consignor.value == "True" && val( hdn_Mode.value ) != 4)
        {
        }
        else
        {
            alert("You Are Not Authorised To Perform This Operation...");
            Allow_Popup = 0 ;
            return false;
        }                
        
        if ( Is_Add == 0  )
        {
            if ( Is_Consignor  == 1  )
            {
                var Path='../../Master/Sales/FrmRegularClient.aspx?Menu_Item_Id=MwA2AA==&Mode=MQA=&Call_From=GC' + '&Is_Consignor=1' ;                   
                Allow_Popup = 1 ;                     
            }
            else
            {             
                var Path='../../Master/Sales/FrmRegularClient.aspx?Menu_Item_Id=MwA2AA==&Mode=MQA=&Call_From=GC' + '&Is_Consignor=0' ;                   
                Allow_Popup = 1 ;                                  
            }
        }
        else        
        {
            if ( Is_Consignor  == 1  )
            {
                if ( hdn_EncreptedConsignorId.value != 'MAA=')
                {
                    if ( hdn_IsRegularConsignor.value == '1' )
                    {
                        var Path='../../Master/Sales/FrmRegularClient.aspx?Menu_Item_Id=MwA2AA==&Mode=NAA=&Id=' + hdn_EncreptedConsignorId.value + '&Call_From=GC' + '&Is_Consignor=1'  ;                
                        Allow_Popup = 1 ;
                    }
                    else
                    {
                        var Path='../../Master/Sales/FrmClient.aspx?Menu_Item_Id=MwA2AA==&Mode=NAA=&Id=' + hdn_EncreptedConsignorId.value + '&Call_From=GC' + '&Is_Consignor=1'  ;                
                        Allow_Popup = 1 ;
                    }
                }
                else
                {
                    alert("Please Select Consignor...");    
                    Allow_Popup = 0 ;
                }
            }
            else
            {
                if ( hdn_EncreptedConsigneeId.value != 'MAA=')
                {                    
                    if ( hdn_IsRegularConsignee.value == '1' )
                    {
                        var Path='../../Master/Sales/FrmRegularClient.aspx?Menu_Item_Id=MwA2AA==&Mode=NAA=&Id=' + hdn_EncreptedConsigneeId.value + '&Call_From=GC' + '&Is_Consignor=0';                
                        Allow_Popup = 1 ;
                    }
                    else 
                    {
                         var Path='../../Master/Sales/FrmClient.aspx?Menu_Item_Id=MwA2AA==&Mode=NAA=&Id=' + hdn_EncreptedConsigneeId.value + '&Call_From=GC' + '&Is_Consignor=0'  ;                
                        Allow_Popup = 1 ;
                    }
                }
                else
                {
                    alert("Please Select Consignee...");    
                    Allow_Popup = 0 ;
                }
            }
        }
       
        if ( Allow_Popup == 1 )
        {        
            w = screen.availWidth;
            h = screen.availHeight;
            var popW = 900, popH = 600;
            var leftPos = (w-popW)/2;
            var topPos = (h-popH)/2;
            
            window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');      
        }
        return false;
    }
     
      
//*******************************************************************
 
  function Add_Commodity()
  {          
        var Allow_Popup = 0;
        var hdn_Can_Add_Commodity = document.getElementById('wucShortGC1_hdn_Can_Add_Commodity');
        var hdn_Mode = document.getElementById('wucShortGC1_hdn_Mode'); 
                
        if ( hdn_Can_Add_Commodity.value == "True" && val(hdn_Mode.value) != 4)
        {
        }
        else
        {
            alert("You Are Not Authorised To Perform This Operation...");
            Allow_Popup = 0 ;
	        return false;
        }

   
        var Path='../../Master/General/FrmCommodity.aspx?Menu_Item_Id=MQAzAA==&Mode=MQA=&Call_From=GC' ;
        Allow_Popup = 1 ;                     
        
        if ( Allow_Popup == 1 )
        {
            w = screen.availWidth;
            h = screen.availHeight;
            var popW = 900, popH = 600;
            var leftPos = (w-popW)/2;
            var topPos = (h-popH)/2;
            
            window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');    
        }
        return false;  
  }
 
//*******************************************************************
 
  function ContainerDetails()
  {          
        var hdn_Mode = document.getElementById('wucShortGC1_hdn_Mode');    
        var Path='FrmGCContainerDetails.aspx?Mode=' + hdn_Mode.value;
        
        w = screen.availWidth;
        h = screen.availHeight;
        var popW = 500, popH = 300;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
        
        window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');    
        return false;  
  }  
      
//*******************************************************************
 
  function Add_Item()
  {          
       var Allow_Popup = 0;
          
        var hdn_Can_Add_Item = document.getElementById('wucShortGC1_hdn_Can_Add_Item');
        var hdn_Mode = document.getElementById('wucShortGC1_hdn_Mode'); 
                
        if ( hdn_Can_Add_Item.value == "True" && val( hdn_Mode.value ) != 4)
        {
        }
        else
        {
            alert("You Are Not Authorised To Perform This Operation...");
            Allow_Popup = 0 ;
	        return false;
        }
   
        var Path='../../Master/General/FrmItem.aspx?Menu_Item_Id=MQA2AA==&Mode=MQA=&Call_From=GC' ;
        Allow_Popup = 1 ;                     
       
        if ( Allow_Popup == 1 )
        {
            w = screen.availWidth;
            h = screen.availHeight;
            var popW = 900, popH = 600;
            var leftPos = (w-popW)/2;
            var topPos = (h-popH)/2;
            
            window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');    
        }
        return false;
  }
      
//*******************************************************************
 
    function Add_Service_Location(Is_Add,Is_Add_From_Location)
    {          
        var hdn_Encrepted_FromLocationId = document.getElementById('wucShortGC1_hdn_Encrepted_FromLocationId');
        var hdn_Encrepted_ToLocationId = document.getElementById('wucShortGC1_hdn_Encrepted_ToLocationId');
        
        var Allow_Popup = 0;
                
        var hdn_Can_Add_Location = document.getElementById('wucShortGC1_hdn_Can_Add_Location');
        var hdn_Mode = document.getElementById('wucShortGC1_hdn_Mode'); 
                
        if ( hdn_Can_Add_Location.value == "True" && val( hdn_Mode.value ) != 4)
        {
        }
        else
        {
            alert("You Are Not Authorised To Perform This Operation...");
            Allow_Popup = 0 ;
            return false;
        }
        
        if ( Is_Add == 0  )
        {
            if ( Is_Add_From_Location == 0  )
            {
                var Path='../../Master/Branch/FrmODALocation.aspx?Menu_Item_Id=MwA0AA==&Mode=MQA=&Call_From=GC' + '&Is_From_Location=1' ;                   
                Allow_Popup = 1 ;                     
            }
            else
            {             
                var Path='../../Master/Branch/FrmODALocation.aspx?Menu_Item_Id=MwA0AA==&Mode=MQA=&Call_From=GC' + '&Is_From_Location=0' ;                   
                Allow_Popup = 1 ;                                  
            }
        }
       
        if ( Allow_Popup == 1 )
        {
            w = screen.availWidth;
            h = screen.availHeight;
            var popW = 900, popH = 600;
            var leftPos = (w-popW)/2;
            var topPos = (h-popH)/2;
            
            window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');    
        }
        return false;
    }
             
//*******************************************************************
 
    function Calculate_LoadingCharge()
    {
        var hdn_HamaliPerKg= document.getElementById('wucShortGC1_hdn_HamaliPerKg');
        var hdn_Applicable_Standard_HamaliPerKg = document.getElementById('wucShortGC1_hdn_Applicable_Standard_HamaliPerKg');
        var hdn_Applicable_Standard_HamaliPerArticles = document.getElementById('wucShortGC1_hdn_Applicable_Standard_HamaliPerArticles');

        var txt_LoadingCharge = document.getElementById('wucShortGC1_txt_LoadingCharge');
        var hdn_LoadingCharge = document.getElementById('wucShortGC1_hdn_LoadingCharge');
        var txt_ChargeWeight = document.getElementById('wucShortGC1_txt_ChargeWeight');
        var hdn_CompanyParameter_Standard_FreightRatePer= document.getElementById('wucShortGC1_hdn_CompanyParameter_Standard_FreightRatePer');

        var hdn_Standard_HamaliCharge= document.getElementById('wucShortGC1_hdn_Standard_HamaliCharge');
        var hdn_Applicable_Standard_HamaliCharge = document.getElementById('wucShortGC1_hdn_Applicable_Standard_HamaliCharge');

        var ddl_PaymentType = document.getElementById('wucShortGC1_ddl_PaymentType');  
        var hdn_Standard_HamaliPerKg = document.getElementById('wucShortGC1_hdn_Standard_HamaliPerKg');  
 
        var charge_weight = val(txt_ChargeWeight.value);

        var hdn_Hamali_Charge_Discount_Percent = document.getElementById('wucShortGC1_hdn_Hamali_Charge_Discount_Percent'); 
        
        var hdn_Is_Opening_GC = document.getElementById('wucShortGC1_hdn_Is_Opening_GC');
        var hdn_MenuItemId = document.getElementById('wucShortGC1_hdn_MenuItemId');
        var hdn_Rectification_GC_Id = document.getElementById('wucShortGC1_hdn_Rectification_GC_Id');
        
        var Discounted_Hamali = 0 ;        
        var Discount = 0 ;
        if (isNaN(charge_weight )) charge_weight  = val(0);

        var ddl_FreightBasis = document.getElementById('wucShortGC1_ddl_FreightBasis');
        
        var lbl_TotalArticles  = document.getElementById('wucShortGC1_ddl_FreightBasis');
        
        var Temp_LoadingCharge = 0;
        
        if ( ddl_FreightBasis.value == 2)  // for Articles
        {
            Temp_LoadingCharge = val(hdn_Applicable_Standard_HamaliPerArticles.value) *  val(lbl_TotalArticles.innerHTML)  ;
        }
        else
        {        
            Temp_LoadingCharge = val(hdn_Applicable_Standard_HamaliPerKg.value) *  val( charge_weight  ) /
                             val(hdn_CompanyParameter_Standard_FreightRatePer.value) 
        }                    
         
         if ( val(Temp_LoadingCharge) < val( hdn_Applicable_Standard_HamaliCharge.value))
         {
            Temp_LoadingCharge = val( hdn_Applicable_Standard_HamaliCharge.value);         
         } 
        
        Discount  =  Temp_LoadingCharge * val(hdn_Hamali_Charge_Discount_Percent.value) / 100 ; 
            
        Discounted_Hamali = val( Temp_LoadingCharge )  - val ( Discount ); 
        
        if (( hdn_Is_Opening_GC.value != 'True' || hdn_MenuItemId.value != 200 ) &&  hdn_Rectification_GC_Id.value <= 0 )
        if ( val( txt_LoadingCharge.value ) < val( Temp_LoadingCharge ))
        {                
          
          if ( val( txt_LoadingCharge.value ) == val( Discounted_Hamali ) )
            {                    
                txt_LoadingCharge.value = val(Discounted_Hamali);
                hdn_LoadingCharge.value = val(txt_LoadingCharge.value );
            }   
          else if ( val( txt_LoadingCharge.value ) < val( Discounted_Hamali ) )
            {                    
                txt_LoadingCharge.value = val( Temp_LoadingCharge );
                hdn_LoadingCharge.value = val(txt_LoadingCharge.value );
            }           
        } 
           if (ddl_PaymentType.value == 5 )
            {
                txt_LoadingCharge.value = val(0);
                hdn_LoadingCharge.value = val(txt_LoadingCharge.value );
            }
           
            txt_LoadingCharge.value = Math.round(val(txt_LoadingCharge.value));
            hdn_LoadingCharge.value = Math.round(val(txt_LoadingCharge.value));            
        }
    
//*******************************************************************
 
    function Calculate_LoadingCharge_On_ChargeWeight_Change()              
    {        
        var hdn_HamaliPerKg= document.getElementById('wucShortGC1_hdn_HamaliPerKg');
        var hdn_Applicable_Standard_HamaliPerKg = document.getElementById('wucShortGC1_hdn_Applicable_Standard_HamaliPerKg');
        var hdn_Applicable_Standard_HamaliPerArticles = document.getElementById('wucShortGC1_hdn_Applicable_Standard_HamaliPerArticles');
        var txt_LoadingCharge= document.getElementById('wucShortGC1_txt_LoadingCharge');
        var hdn_LoadingCharge = document.getElementById('wucShortGC1_hdn_LoadingCharge');
        var txt_ChargeWeight= document.getElementById('wucShortGC1_txt_ChargeWeight');
        var hdn_CompanyParameter_Standard_FreightRatePer= document.getElementById('wucShortGC1_hdn_CompanyParameter_Standard_FreightRatePer');
        var hdn_Standard_HamaliCharge= document.getElementById('wucShortGC1_hdn_Standard_HamaliCharge');
        var hdn_Applicable_Standard_HamaliCharge = document.getElementById('wucShortGC1_hdn_Applicable_Standard_HamaliCharge');
        var ddl_PaymentType = document.getElementById('wucShortGC1_ddl_PaymentType');  
        var hdn_Standard_HamaliPerKg = document.getElementById('wucShortGC1_hdn_Standard_HamaliPerKg');
        var hdn_Is_Opening_GC = document.getElementById('wucShortGC1_hdn_Is_Opening_GC');
        var hdn_MenuItemId = document.getElementById('wucShortGC1_hdn_MenuItemId');        
        var hdn_Rectification_GC_Id = document.getElementById('wucShortGC1_hdn_Rectification_GC_Id');
       
        txt_ChargeWeight.value =  val(txt_ChargeWeight.value);        
        var charge_weight = val(txt_ChargeWeight.value);
        
        if (isNaN(charge_weight )) charge_weight  = val(0);
                             
        var ddl_FreightBasis = document.getElementById('wucShortGC1_ddl_FreightBasis');
        var lbl_TotalArticles = document.getElementById('wucShortGC1_lbl_TotalArticles');
        
        var Temp_LoadingCharge = 0;
        
        if ( ddl_FreightBasis.value == 2)  // for Articles
        {
            Temp_LoadingCharge = val(hdn_Applicable_Standard_HamaliPerArticles.value) *   val(lbl_TotalArticles.innerHTML);
        }
        else
        {        
            Temp_LoadingCharge = val(hdn_Applicable_Standard_HamaliPerKg.value) *  val( charge_weight  ) /
                             val(hdn_CompanyParameter_Standard_FreightRatePer.value) 
        }                                                                   
        
        if (( hdn_Is_Opening_GC.value != 'True' || hdn_MenuItemId.value != 200 ) &&  hdn_Rectification_GC_Id.value <= 0 )
            if ( val( Temp_LoadingCharge ) < val(hdn_Applicable_Standard_HamaliCharge.value) )
            {
                Temp_LoadingCharge  = val(hdn_Applicable_Standard_HamaliCharge.value) ;
                
                txt_LoadingCharge.value = Math.round( val( Temp_LoadingCharge   ));
                hdn_LoadingCharge.value = val(txt_LoadingCharge.value );
            }
        
        if (( hdn_Is_Opening_GC.value != 'True' || hdn_MenuItemId.value != 200 ) &&  hdn_Rectification_GC_Id.value <= 0 )
        {
            txt_LoadingCharge.value = Math.round( val( Temp_LoadingCharge));
            hdn_LoadingCharge.value = val(txt_LoadingCharge.value );
        }
        
       if (ddl_PaymentType.value == 5  )
        {
            txt_LoadingCharge.value = val(0);
            hdn_LoadingCharge.value = val(txt_LoadingCharge.value );
        }        
        txt_LoadingCharge.value = Math.round(val(txt_LoadingCharge.value));
        hdn_LoadingCharge.value = Math.round(val(txt_LoadingCharge.value));
    }

    
//*******************************************************************
 
    function Calculate_LoacalCharge_On_ChargeWeight_Change()
    {
        var hdn_Applicable_Standard_LocalCharge = document.getElementById('wucShortGC1_hdn_Applicable_Standard_LocalCharge');
        var txt_LocalCharge = document.getElementById('wucShortGC1_txt_LocalCharge');
        var hdn_LocalCharge= document.getElementById('wucShortGC1_hdn_LocalCharge');
        var txt_ChargeWeight= document.getElementById('wucShortGC1_txt_ChargeWeight');
        var hdn_CompanyParameter_Standard_FreightRatePer= document.getElementById('wucShortGC1_hdn_CompanyParameter_Standard_FreightRatePer');
        var hdn_Standard_LocalCharge= document.getElementById('wucShortGC1_hdn_Standard_LocalCharge');
        var ddl_PaymentType = document.getElementById('wucShortGC1_ddl_PaymentType');      
        var hdn_Is_Opening_GC = document.getElementById('wucShortGC1_hdn_Is_Opening_GC');
        var hdn_MenuItemId = document.getElementById('wucShortGC1_hdn_MenuItemId');
        var hdn_Rectification_GC_Id = document.getElementById('wucShortGC1_hdn_Rectification_GC_Id');
        
        txt_ChargeWeight.value =  val(txt_ChargeWeight.value);
        
        var charge_weight = val(txt_ChargeWeight.value);
        
        if (isNaN(charge_weight )) charge_weight  = val(0);
      
        var Temp_LocalCharge = val (hdn_Applicable_Standard_LocalCharge.value)  *  val( charge_weight  ) /
                             val(hdn_CompanyParameter_Standard_FreightRatePer.value) 
 
        if (( hdn_Is_Opening_GC.value != 'True' || hdn_MenuItemId.value != 200 ) &&  hdn_Rectification_GC_Id.value <= 0 )
            if ( val(txt_LocalCharge.value) < val( Temp_LocalCharge )   )
            {   
                txt_LocalCharge.value = Math.round( val( Temp_LocalCharge   ));
                hdn_LocalCharge.value = val(txt_LocalCharge.value );
            }
        
        if (( hdn_Is_Opening_GC.value != 'True' || hdn_MenuItemId.value != 200 ) &&  hdn_Rectification_GC_Id.value <= 0 )
        {
            txt_LocalCharge.value = Math.round( val( Temp_LocalCharge));
            hdn_LocalCharge.value = val(txt_LocalCharge.value );
        }
        
       if (ddl_PaymentType.value == 5  )
        {
            txt_LocalCharge.value = val(0);
            hdn_LocalCharge.value = val(0);
        }
    }

//*******************************************************************
    
    function Calculate_DDODA_Charge()
    { 
           var chk_IsOctroiApplicable= document.getElementById('wucShortGC1_chk_IsOctroiApplicable');                 
           var txt_DDCharge= document.getElementById('wucShortGC1_txt_DDCharge');
           var hdn_DDCharge = document.getElementById('wucShortGC1_hdn_DDCharge');          
           var txt_ChargeWeight= document.getElementById('wucShortGC1_txt_ChargeWeight');          
           var hdn_Odachargesupto500Kg= document.getElementById('wucShortGC1_hdn_Odachargesupto500Kg');          
           var hdn_Odachargesabove500Kg= document.getElementById('wucShortGC1_hdn_Odachargesabove500Kg');          
           var hdn_Applicable_Standard_DDCharge= document.getElementById('wucShortGC1_hdn_Applicable_Standard_DDCharge');           
           var hdn_Applicable_Standard_DDCharge_Rate = document.getElementById('wucShortGC1_hdn_Applicable_Standard_DDCharge_Rate');          
           var hdn_IsODA = document.getElementById('wucShortGC1_hdn_IsODA');            
           var ddl_DeliveryType = document.getElementById('wucShortGC1_ddl_DeliveryType');           
           var ddl_PaymentType= document.getElementById('wucShortGC1_ddl_PaymentType');             
           var hdn_CompanyParameter_Standard_FreightRatePer= document.getElementById('wucShortGC1_hdn_CompanyParameter_Standard_FreightRatePer');           
           var hdn_Is_Opening_GC = document.getElementById('wucShortGC1_hdn_Is_Opening_GC');
           var hdn_MenuItemId = document.getElementById('wucShortGC1_hdn_MenuItemId');           
           var hdn_Rectification_GC_Id = document.getElementById('wucShortGC1_hdn_Rectification_GC_Id'); 

           txt_DDCharge.value = val(0);           
           hdn_DDCharge.value = val(txt_DDCharge.value );
           
           if (( hdn_Is_Opening_GC.value != 'True' || hdn_MenuItemId.value != 200 ) &&  hdn_Rectification_GC_Id.value <= 0 )
           if ( hdn_IsODA.value == "True"  && ddl_DeliveryType.value == 2) // for door and oda
           {
                txt_DDCharge.disabled = false;
           
               if ( val(txt_ChargeWeight.value ) <= 500)
                    txt_DDCharge.value = val( hdn_Odachargesupto500Kg.value);
               else if (val(txt_ChargeWeight.value ) > 500)
                    txt_DDCharge.value = val( hdn_Odachargesabove500Kg.value);
                    
               hdn_DDCharge.value = val(txt_DDCharge.value );
           }
           else
           {
           
            if (ddl_DeliveryType.value == 2 && ddl_PaymentType.value != 5  )
            {
                txt_DDCharge.value = val(txt_ChargeWeight.value ) * val (hdn_Applicable_Standard_DDCharge_Rate.value)
                                        / val( hdn_CompanyParameter_Standard_FreightRatePer.value) ;
                 
                txt_DDCharge.value  = val(txt_DDCharge.value );
                hdn_DDCharge.value = val(txt_DDCharge.value );
            }
            else
            {
                txt_DDCharge.value = val(0);
                hdn_DDCharge.value = val(txt_DDCharge.value );
            }            
        }
        
       if (ddl_PaymentType.value == 5)
       {
            txt_DDCharge.value = val(0);
            hdn_DDCharge.value = val(txt_DDCharge.value );
       }        
       txt_DDCharge.value = val(txt_DDCharge.value);
       hdn_DDCharge.value = val(txt_DDCharge.value);    
   }

//*******************************************************************
 
    function On_Instruction_Change()
    {  
        var ddl_Instruction = document.getElementById('wucShortGC1_ddl_Instruction');
        var txt_InstructionRemark = document.getElementById('wucShortGC1_txt_InstructionRemark');

        if (ddl_Instruction.value > 0)
        {
            txt_InstructionRemark.value = ddl_Instruction.options[ddl_Instruction.selectedIndex].text;
        }
        else
        {
            txt_InstructionRemark.value = ''; 
        }    
    }    
      
//*******************************************************************

    function Other_Charges()
    {  
        var hdn_Mode = document.getElementById('wucShortGC1_hdn_Mode');
        var Path='FrmGCOtherCharges.aspx?Mode=' +  hdn_Mode.value  ;

        w = screen.availWidth;
        h = screen.availHeight;
        var popW = 600, popH = 500;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
        
        window.open(Path,'','width=' + popW + ',height=' + popH + ',top=' + topPos + ',left=' + leftPos + ',color=blue');

    return false;
  } 

//*******************************************************************
 
    function Check_All(chk,gridname)
    {              
        var grid = document.getElementById(gridname);
        var Sum_Total_GC_Other_Charges = val(0);
        var checkbox,Amount;
        var i,j=0;
        var lbl_TotalGCOtherCharges = document.getElementById('wucGCOtherCharges1_lbl_TotalGCOtherCharges');
        var hdn_TotalGCOtherCharges = document.getElementById('wucGCOtherCharges1_hdn_TotalGCOtherCharges');
        var hdn_OtherChargesCount = document.getElementById('wucGCOtherCharges1_hdn_OtherChargesCount');
        
        var max = (grid.rows.length - 1);
        
        for(i=1;i<grid.rows.length-1;i++)
        {
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');                       
            Amount = grid.rows[i].cells[3].getElementsByTagName('input');
            
            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            }
            if(chk.checked == true)
            {
                if(Amount[0].type =='text')
                {
                    Sum_Total_GC_Other_Charges = Sum_Total_GC_Other_Charges + val(Amount[0].value);
                }
            }
        }
        
        if(chk.checked == true)
        {
            hdn_OtherChargesCount.value = max;            
            lbl_TotalGCOtherCharges.innerHTML = Sum_Total_GC_Other_Charges  ;                    
            hdn_TotalGCOtherCharges.value = Sum_Total_GC_Other_Charges  ;
        }
        else
        {
            hdn_OtherChargesCount.value =  val(0);            
            lbl_TotalGCOtherCharges.innerHTML   =  val(0);                    
            hdn_TotalGCOtherCharges.value =  val(0);
        }
    }  
        
//*******************************************************************
 
    function Calculate_Other_Charges_Total(gridname)
    {
        var grid = document.getElementById(gridname);
        var Sum_Total_GC_Other_Charges = val(0);
        var checkbox,Amount;
        var i,j=0;
        var lbl_TotalGCOtherCharges = document.getElementById('wucGCOtherCharges1_lbl_TotalGCOtherCharges');             
        var hdn_TotalGCOtherCharges = document.getElementById('wucGCOtherCharges1_hdn_TotalGCOtherCharges');        
        var hdn_OtherChargesCount = document.getElementById('wucGCOtherCharges1_hdn_OtherChargesCount');

        var max = (grid.rows.length - 1);
        
        for(i=1;i<grid.rows.length-1;i++)
        {
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            Amount = grid.rows[i].cells[3].getElementsByTagName('input');
            
            if(checkbox[0].checked  == true)
            {
                if(Amount[0].type =='text')
                {
                    Sum_Total_GC_Other_Charges = Sum_Total_GC_Other_Charges + val(Amount[0].value);
                }
            }
        }        
        hdn_OtherChargesCount.value = max;
        lbl_TotalGCOtherCharges.innerHTML = Sum_Total_GC_Other_Charges;
        hdn_TotalGCOtherCharges.value = Sum_Total_GC_Other_Charges;
    }
        
//*******************************************************************
 
    function Calculate_Summary(chk,txt_Amount)
    {
        var lbl_TotalGCOtherCharges = document.getElementById('wucGCOtherCharges1_lbl_TotalGCOtherCharges');
        var hdn_TotalGCOtherCharges = document.getElementById('wucGCOtherCharges1_hdn_TotalGCOtherCharges');
        var hdn_OtherChargesCount = document.getElementById('wucGCOtherCharges1_hdn_OtherChargesCount');
        var chk =document.getElementById(chk);
        var txt_Amount =document.getElementById(txt_Amount);

        if (chk.checked == true)
        {
           hdn_OtherChargesCount.value = val(hdn_OtherChargesCount.value) + 1;
           lbl_TotalGCOtherCharges.innerHTML = val(lbl_TotalGCOtherCharges.innerHTML)  + val(txt_Amount.value);           
           hdn_TotalGCOtherCharges.value  = val(hdn_TotalGCOtherCharges.value)  + val(txt_Amount.value);
        }
        else
        {
           hdn_OtherChargesCount.value = val(hdn_OtherChargesCount.value) - 1;           
           hdn_TotalGCOtherCharges.value  = val(hdn_TotalGCOtherCharges.value)  - val(txt_Amount.value);           
           lbl_TotalGCOtherCharges.innerHTML = val(lbl_TotalGCOtherCharges.innerHTML)  - val(txt_Amount.value);
        }

        if (val(lbl_TotalGCOtherCharges.innerHTML)<=0)
        {
            lbl_TotalGCOtherCharges.innerHTML = val(0);
        }
        if ( val (hdn_TotalGCOtherCharges.value ) <=0 )
        {
            hdn_TotalGCOtherCharges.value = val(0);
        }
    }