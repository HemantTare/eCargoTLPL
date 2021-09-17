<%@ Page AutoEventWireup="true" CodeFile="FrmUserDeskCreditDebitCustomerBalancePendingBillsNew.aspx.cs"
    Inherits="Reports_CL_Nandwana_UserDesk_FrmUserDeskCreditDebitCustomerBalancePendingBillsNew"
    Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>

<script type="text/javascript" language="javascript" src="../../../Javascript/Common.js"></script>

<script type="text/javascript">
function input_screen_action(action)
{
if (action == 'view')
  {
  tbl_input_screen.style.display='inline';
  }
else
  {
  tbl_input_screen.style.display='none';
  }
}

function PaymentReceiptClicked()
{
    var trid = document.getElementById("tr_References");
    var trid1 = document.getElementById("tr_ReferencesSave");
    var trid3 = document.getElementById("tr_ReferencesTotal");

    var trid2 = document.getElementById("tr_Receipt");
    
    if (trid != null) 
    {
        trid.style.display = 'none';
    }
    
    if (trid1 != null) 
    {
        trid1.style.display = 'none';
    }
    
    if (trid2 != null) 
    {
       trid2.style.display = 'block';
    }

    if (trid3 != null) 
    {
       trid3.style.display = 'none';
    }

}

function OnAccountAdjustmentClick()
{

    var trid = document.getElementById("tr_References");
    var trid1 = document.getElementById("tr_ReferencesSave");
    var trid3 = document.getElementById("tr_ReferencesTotal");
    
    var trid2 = document.getElementById("tr_Receipt");
    
    if (trid != null) 
    {
        trid.style.display = 'block';
    }
    
    if (trid1 != null) 
    {
        trid1.style.display = 'block';
    }

    
    if (trid2 != null) 
    {
       trid2.style.display = 'none';
    }

    if (trid3 != null) 
    {
       trid3.style.display = 'block';
    }

}

function OnChequeAmountLossFocus()
{

    var txt_ChequeAmount = document.getElementById("txt_ChequeAmount");
    var lbl_BankPaymentBy = document.getElementById("lbl_BankPaymentBy");
    var ddl_BankPaymentType = document.getElementById("ddl_BankPaymentType");
    var lbl_CheckRefNo = document.getElementById("lbl_CheckRefNo");
    var txt_RefNo = document.getElementById("txt_RefNo");
    var td_ChequeDate = document.getElementById("td_ChequeDate");
    var txt_ReceiptBank = document.getElementById("txt_ReceiptBank");
    var lbl_ReceiptBank = document.getElementById("lbl_ReceiptBank");


   var lbl_Deposited_Bank = document.getElementById("lbl_Deposited_Bank");
   var ddl_BankLedger = document.getElementById("ddl_BankLedger");

    if (val(txt_ChequeAmount.value) > 0) 
    {
        lbl_BankPaymentBy.style.display = 'block';
        ddl_BankPaymentType.style.display = 'block';
        lbl_CheckRefNo.style.display = 'block';
        txt_RefNo.style.display = 'block';
        td_ChequeDate.style.display = 'block';
        txt_ReceiptBank.style.display = 'block';
        lbl_ReceiptBank.style.display = 'block';
        ddl_BankPaymentType.focus();
        
        lbl_Deposited_Bank.style.display = 'block';
        ddl_BankLedger.style.display = 'block';
        
    }
    else
    {
       lbl_BankPaymentBy.style.display = 'none';
       ddl_BankPaymentType.style.display = 'none';
       lbl_CheckRefNo.style.display = 'none';
       txt_RefNo.style.display = 'none';
       td_ChequeDate.style.display = 'none';
       txt_ReceiptBank.style.display = 'none';
       lbl_ReceiptBank.style.display = 'none';

       lbl_Deposited_Bank.style.display = 'none';
       ddl_BankLedger.style.display = 'none';
    
    }
}
    
    
function CalculateTotal()
{ 
    var txt_CashAmount = document.getElementById('<%=txt_CashAmount.ClientID %>');
    var hdn_CashAmount = document.getElementById('<%=hdn_CashAmount.ClientID %>');
    
    var txt_ChequeAmount = document.getElementById('<%=txt_ChequeAmount.ClientID %>');
    var hdn_ChequeAmount = document.getElementById('<%=hdn_ChequeAmount.ClientID %>');

       
    var lbl_TotalReceipt = document.getElementById('<%=lbl_TotalReceipt.ClientID %>');
    var hdn_TotalReceipt = document.getElementById('<%=hdn_TotalReceipt.ClientID %>');

    hdn_CashAmount.value = txt_CashAmount.value;
    hdn_ChequeAmount.value = txt_ChequeAmount.value;
        
    lbl_TotalReceipt.value = val(txt_CashAmount.value) + val(txt_ChequeAmount.value);
    
    hdn_TotalReceipt.value = lbl_TotalReceipt.value;
    
    lbl_TotalReceipt.innerHTML = hdn_TotalReceipt.value
    
        
    return;
    }



    function Check_SingleAdjusted(chk,gridname)
    {

        var grid = document.getElementById(gridname);
        
        var row = chk.parentElement.parentElement;

        txtAdjustedAmount = row.cells[4].getElementsByTagName('input');
        txtSrNo = row.cells[6].getElementsByTagName('input');

        if(chk.checked == true)
        {

            txtAdjustedAmount[0].disabled = false;

        }
        else
        {
            txtAdjustedAmount[0].value = '0';
            txtAdjustedAmount[0].disabled = true;
        }
        
        var i,j=0;
        
        var Chk_Attach, txtAdjustedAmount2;
        var txtSrNo2;
             
        var max = (grid.rows.length - 1);
        
        
        for(i=1;i<grid.rows.length;i++)
        {            

            Chk_Attach = grid.rows[i].cells[0].getElementsByTagName('input');
            txtSrNo2 = grid.rows[i].cells[6].getElementsByTagName('input');
            txtAdjustedAmount2 = grid.rows[i].cells[4].getElementsByTagName('input');
        
            if(txtSrNo[0].value != txtSrNo2[0].value)
            {
                Chk_Attach[0].checked = false;
                txtAdjustedAmount2[0].value = '0';
            }
            
        }

        
        
    }

    function jsCalculateTotalAdjusted(gridname)
    {        

        var grid = document.getElementById(gridname);
        var i,j=0;
        var sum_Adjusted=0;
        
        var Chk_Attach,txtAdjustedAmount, txtBillAmount;


        var lbl_TotalToBeAdjusted = document.getElementById('lbl_TotalToBeAdjusted');
        var hdn_TotalToBeAdjusted = document.getElementById('hdn_TotalToBeAdjusted');

              
        var max = (grid.rows.length - 1);
        
        for(i=1;i<grid.rows.length;i++)
        {            
            Chk_Attach = grid.rows[i].cells[0].getElementsByTagName('input');
            txtAdjustedAmount = grid.rows[i].cells[4].getElementsByTagName('input');
            txtBillAmount = grid.rows[i].cells[3].getElementsByTagName('input');
            
            if(Chk_Attach[0].checked == true)
            {         
            
                if(txtAdjustedAmount[0].type == 'text')
                {

                    if ( val(txtAdjustedAmount[0].value) >  val(txtBillAmount[0].value))
                    {
                        txtAdjustedAmount[0].value = "0"
                    }
                    
                    sum_Adjusted = sum_Adjusted + val(txtAdjustedAmount[0].value);

                }
            }
            
        }
        
       lbl_TotalToBeAdjusted.innerHTML  = sum_Adjusted;
       lbl_TotalToBeAdjusted.value = sum_Adjusted;
       hdn_TotalToBeAdjusted.value  = sum_Adjusted;
    } 


    function Check_SingleUnadjusted(chk,gridname)
    {


        var grid = document.getElementById(gridname);
        
        var row = chk.parentElement.parentElement;

        txtReceivedAmount = row.cells[3].getElementsByTagName('input');
        txtTDS = row.cells[4].getElementsByTagName('input');
        txtFrtDeduction = row.cells[5].getElementsByTagName('input');
        txtClaimDeduction = row.cells[6].getElementsByTagName('input');


        if(chk.checked == true)
        {

            txtReceivedAmount[0].disabled = false;
            txtTDS[0].disabled = false;
            txtFrtDeduction[0].disabled = false;
            txtClaimDeduction[0].disabled = false;

        }
        else
        {
            txtReceivedAmount[0].disabled = true;
            txtTDS[0].disabled = true;
            txtFrtDeduction[0].disabled = true;
            txtClaimDeduction[0].disabled = true;

            txtReceivedAmount[0].value = '0';
            txtTDS[0].value = '0';
            txtFrtDeduction[0].value = '0';
            txtClaimDeduction[0].value = '0';
        }

        jsCalculateTotalUnAdjusted(gridname);      
        
        
    }

    function Onblur_ReceivedAmount(txtReceivedAmount,hdn_ReceivedAmount,txtTDS,hdn_TDS,txtFrtDeduction,hdn_FrtDeduction,txtClaimDeduction,
    hdn_ClaimDeduction,txtPendingAmount,hdn_TDPercent, gridname)
    {
        var txtReceivedAmount=document.getElementById(txtReceivedAmount);
        var hdn_ReceivedAmount=document.getElementById(hdn_ReceivedAmount);

        var txtTDS=document.getElementById(txtTDS);
        var hdn_TDS=document.getElementById(hdn_TDS);
        
        var txtFrtDeduction=document.getElementById(txtFrtDeduction);
        var hdn_FrtDeduction=document.getElementById(hdn_FrtDeduction);
        
        var txtClaimDeduction=document.getElementById(txtClaimDeduction);
        var hdn_ClaimDeduction=document.getElementById(hdn_ClaimDeduction);
        
        var txtPendingAmount=document.getElementById(txtPendingAmount);
        var hdn_TDPercent=document.getElementById(hdn_TDPercent);
                                      

        hdn_ReceivedAmount.value = txtReceivedAmount.value;

        if (val(hdn_TDPercent.value)>0)
        {
            hdn_TDS.value = Math.ceil((val(hdn_ReceivedAmount.value) * (val(hdn_TDPercent.value)/100)));
        }
        else
        {
            hdn_TDS.value  = '0';
        }
        
    
        txtTDS.value = hdn_TDS.value;
        
        if (val(hdn_ReceivedAmount.value) > val(txtPendingAmount.value))
        {
            hdn_ReceivedAmount.value = '0';
            txtReceivedAmount.value = '0';
            hdn_TDS.value  = '0';
            txtTDS.value = 0;
            hdn_FrtDeduction.value = '0';
            txtFrtDeduction.value = '0';
            hdn_ClaimDeduction.value = '0';
            txtClaimDeduction.value = '0';
        
        }
        else if ((val(hdn_ReceivedAmount.value) + val(hdn_TDS.value)) > val(txtPendingAmount.value))
        {

            hdn_TDS.value  = '0';
            txtTDS.value = 0;
            hdn_FrtDeduction.value = '0';
            txtFrtDeduction.value = '0';
            hdn_ClaimDeduction.value = '0';
            txtClaimDeduction.value = '0';

        }
        else
        {
            hdn_FrtDeduction.value = val(txtPendingAmount.value) - val(hdn_ReceivedAmount.value) - val(hdn_TDS.value);
            
            txtFrtDeduction.value = hdn_FrtDeduction.value; 
            
            hdn_ClaimDeduction.value = '0';
            txtClaimDeduction.value = hdn_ClaimDeduction.value ;
        }    
        
        jsCalculateTotalUnAdjusted(gridname);        
   } 


    function Onblur_TDS(txtReceivedAmount,hdn_ReceivedAmount,txtTDS,hdn_TDS,txtFrtDeduction,hdn_FrtDeduction,txtClaimDeduction,
    hdn_ClaimDeduction,txtPendingAmount,hdn_TDPercent, gridname)
    {
        var txtReceivedAmount=document.getElementById(txtReceivedAmount);
        var hdn_ReceivedAmount=document.getElementById(hdn_ReceivedAmount);

        var txtTDS=document.getElementById(txtTDS);
        var hdn_TDS=document.getElementById(hdn_TDS);
        
        var txtFrtDeduction=document.getElementById(txtFrtDeduction);
        var hdn_FrtDeduction=document.getElementById(hdn_FrtDeduction);
        
        var txtClaimDeduction=document.getElementById(txtClaimDeduction);
        var hdn_ClaimDeduction=document.getElementById(hdn_ClaimDeduction);
        
        var txtPendingAmount=document.getElementById(txtPendingAmount);
        var hdn_TDPercent=document.getElementById(hdn_TDPercent);
                                
        hdn_ReceivedAmount.value = txtReceivedAmount.value;

        hdn_TDS.value = txtTDS.value;
        
        if ((val(txtPendingAmount.value) - val(hdn_ReceivedAmount.value) - val(hdn_TDS.value)) < 0) 
        {
            hdn_FrtDeduction.value = '0';
            txtFrtDeduction.value = '0';
        
            hdn_ClaimDeduction.value = '0';
            txtClaimDeduction.value = hdn_ClaimDeduction.value ;

        }
        else
        {        
            hdn_FrtDeduction.value = val(txtPendingAmount.value) - val(hdn_ReceivedAmount.value) - val(hdn_TDS.value);
            txtFrtDeduction.value = hdn_FrtDeduction.value; 
        
            hdn_ClaimDeduction.value = '0';
            txtClaimDeduction.value = hdn_ClaimDeduction.value ;
        } 
        
        jsCalculateTotalUnAdjusted(gridname);             
   } 

    function Onblur_FrtDeduction(txtReceivedAmount,hdn_ReceivedAmount,txtTDS,hdn_TDS,txtFrtDeduction,hdn_FrtDeduction,txtClaimDeduction,
    hdn_ClaimDeduction,txtPendingAmount,hdn_TDPercent, gridname)
    {
        var txtReceivedAmount=document.getElementById(txtReceivedAmount);
        var hdn_ReceivedAmount=document.getElementById(hdn_ReceivedAmount);

        var txtTDS=document.getElementById(txtTDS);
        var hdn_TDS=document.getElementById(hdn_TDS);
        
        var txtFrtDeduction=document.getElementById(txtFrtDeduction);
        var hdn_FrtDeduction=document.getElementById(hdn_FrtDeduction);
        
        var txtClaimDeduction=document.getElementById(txtClaimDeduction);
        var hdn_ClaimDeduction=document.getElementById(hdn_ClaimDeduction);
        
        var txtPendingAmount=document.getElementById(txtPendingAmount);
        var hdn_TDPercent=document.getElementById(hdn_TDPercent);
                                      

        hdn_ReceivedAmount.value = txtReceivedAmount.value;

        hdn_TDS.value = txtTDS.value;
        
        hdn_FrtDeduction.value = txtFrtDeduction.value;

        if ((val(txtPendingAmount.value) - val(hdn_ReceivedAmount.value) - val(hdn_TDS.value) - val(hdn_FrtDeduction.value)) >= 0)
        {
                
            hdn_ClaimDeduction.value = val(txtPendingAmount.value) - val(hdn_ReceivedAmount.value) - val(hdn_TDS.value) - val(hdn_FrtDeduction.value);
            
            if (hdn_ClaimDeduction.value > 0) 
            {
                txtClaimDeduction.value =  hdn_ClaimDeduction.value; 
            }
            else
            {
                txtClaimDeduction.value = '0';
                hdn_ClaimDeduction.value = '0';
            }
        }
        else
        {
            hdn_FrtDeduction.value = '0';
            txtFrtDeduction.value = '0';
            txtClaimDeduction.value = '0';
            hdn_ClaimDeduction.value = '0';        
        }       
        
        jsCalculateTotalUnAdjusted(gridname);      
   } 

    function Onblur_ClaimDeduction(txtReceivedAmount,hdn_ReceivedAmount,txtTDS,hdn_TDS,txtFrtDeduction,hdn_FrtDeduction,txtClaimDeduction,
    hdn_ClaimDeduction,txtPendingAmount,hdn_TDPercent, gridname)
    {
        var txtReceivedAmount=document.getElementById(txtReceivedAmount);
        var hdn_ReceivedAmount=document.getElementById(hdn_ReceivedAmount);

        var txtTDS=document.getElementById(txtTDS);
        var hdn_TDS=document.getElementById(hdn_TDS);
        
        var txtFrtDeduction=document.getElementById(txtFrtDeduction);
        var hdn_FrtDeduction=document.getElementById(hdn_FrtDeduction);
        
        var txtClaimDeduction=document.getElementById(txtClaimDeduction);
        var hdn_ClaimDeduction=document.getElementById(hdn_ClaimDeduction);
        
        var txtPendingAmount=document.getElementById(txtPendingAmount);
        var hdn_TDPercent=document.getElementById(hdn_TDPercent);
                                      

        hdn_ReceivedAmount.value = txtReceivedAmount.value;

        hdn_TDS.value = txtTDS.value;
        
        hdn_ClaimDeduction.value = txtClaimDeduction.value;

//        if ((val(txtPendingAmount.value) - val(hdn_ReceivedAmount.value) - val(hdn_TDS.value) - val(hdn_ClaimDeduction.value)) >= 0)
//        {
//                
//            hdn_FrtDeduction.value = val(txtPendingAmount.value) - val(hdn_ReceivedAmount.value) - val(hdn_TDS.value) - val(hdn_ClaimDeduction.value);
//            
//            if (hdn_FrtDeduction.value > 0) 
//            {
//                txtFrtDeduction.value =  hdn_FrtDeduction.value; 
//            }
//            else
//            {
//                txtFrtDeduction.value = '0';
//                hdn_FrtDeduction.value = '0';
//            }
//        }
//        else
//        {
//            hdn_FrtDeduction.value = '0';
//            txtFrtDeduction.value = '0';
//            txtClaimDeduction.value = '0';
//            hdn_ClaimDeduction.value = '0';        
//        }       

       jsCalculateTotalUnAdjusted(gridname);      
   
   }
   
   
    function jsCalculateTotalUnAdjusted(gridname)
    {        

        var grid = document.getElementById(gridname);
        var i,j=0;
        var sum_ReceivedAmount=0, sum_TDS=0, sum_FrtDeduction=0, sum_ClaimAmount=0;
        
        var Chk_Attach,  txtReceivedAmount, txtTDS, txtFrtDeduction, txtClaimDeduction;


        var lbl_TotalReceivedAmount = document.getElementById('lbl_TotalReceivedAmount');
        var hdn_TotalReceivedAmount = document.getElementById('hdn_TotalReceivedAmount');

        var lbl_TotalTDS = document.getElementById('lbl_TotalTDS');
        var hdn_TotalTDS = document.getElementById('hdn_TotalTDS');

        var lbl_TotalFrtDeduction = document.getElementById('lbl_TotalFrtDeduction');
        var hdn_TotalFrtDeduction = document.getElementById('hdn_TotalFrtDeduction');
        
        var lbl_TotalClaimAmount = document.getElementById('lbl_TotalClaimAmount');
        var hdn_TotalClaimAmount = document.getElementById('hdn_TotalClaimAmount');

        var lbl_TotalRecTDSFrtDeduction = document.getElementById('lbl_TotalRecTDSFrtDeduction');
        var hdn_TotalRecTDSFrtDeduction = document.getElementById('hdn_TotalRecTDSFrtDeduction');
              
                      
        var max = (grid.rows.length - 1);
        
        for(i=1;i<grid.rows.length;i++)
        {            
            Chk_Attach = grid.rows[i].cells[0].getElementsByTagName('input');
            txtReceivedAmount = grid.rows[i].cells[3].getElementsByTagName('input');
            txtTDS = grid.rows[i].cells[4].getElementsByTagName('input');

            txtFrtDeduction = grid.rows[i].cells[5].getElementsByTagName('input');
            txtClaimDeduction = grid.rows[i].cells[6].getElementsByTagName('input');
            
            if(Chk_Attach[0].checked == true)
            {         
            
                if(txtReceivedAmount[0].type == 'text')
                {
                    
                    sum_ReceivedAmount = sum_ReceivedAmount + val(txtReceivedAmount[0].value);

                }
                
                if(txtTDS[0].type == 'text')
                {
                    
                    sum_TDS = sum_TDS + val(txtTDS[0].value);

                }

                if(txtFrtDeduction[0].type == 'text')
                {
                    
                    sum_FrtDeduction = sum_FrtDeduction + val(txtFrtDeduction[0].value);

                }
                
                if(txtClaimDeduction[0].type == 'text')
                {
                    
                    sum_ClaimAmount = sum_ClaimAmount + val(txtClaimDeduction[0].value);

                }
            }
            
        }
        
       lbl_TotalReceivedAmount.innerHTML  = sum_ReceivedAmount;
       lbl_TotalReceivedAmount.value = sum_ReceivedAmount;
       hdn_TotalReceivedAmount.value  = sum_ReceivedAmount;

       lbl_TotalTDS.innerHTML  = sum_TDS;
       lbl_TotalTDS.value = sum_TDS;
       hdn_TotalTDS.value = sum_TDS;
       
       
       lbl_TotalFrtDeduction.innerHTML = sum_FrtDeduction;
       lbl_TotalFrtDeduction.value = sum_FrtDeduction;
       hdn_TotalFrtDeduction.value = sum_FrtDeduction;
        
       lbl_TotalClaimAmount.innerHTML = sum_ClaimAmount;
       lbl_TotalClaimAmount.value = sum_ClaimAmount;
       hdn_TotalClaimAmount.value = sum_ClaimAmount;
       

       hdn_TotalRecTDSFrtDeduction.value = sum_ReceivedAmount + sum_TDS + sum_FrtDeduction + sum_ClaimAmount; 
       lbl_TotalRecTDSFrtDeduction.innerHTML = hdn_TotalRecTDSFrtDeduction.value;
       lbl_TotalRecTDSFrtDeduction.value = hdn_TotalRecTDSFrtDeduction.value;


    } 
 
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending Bills</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Pending Bills"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 34%" colspan="2">
                                <asp:Label ID="lbl_ClientName" runat="server" CssClass="LABEL" Text="ClientName"
                                    Font-Bold="true" ForeColor="Magenta" /></td>
                            <td style="width: 33%" colspan="2">
                                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:RadioButtonList ID="rdl_PaymentReceiptOrAdjustment" runat="server" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0" Selected="True" Text="Payment Receipt" OnClick="PaymentReceiptClicked();"></asp:ListItem>
                                            <asp:ListItem Value="1" Text="On Account Adjustment" OnClick="OnAccountAdjustmentClick();"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btn_Save_Receipt" />
                                        <asp:AsyncPostBackTrigger ControlID="btn_SaveAdjustment" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 9%">
                                <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                                    Text="Close Window" Visible="false" /></td>
                            <td style="width: 24%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" colspan="6" style="text-align: left">
                                <asp:UpdatePanel ID="UpdatePanel" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btn_Save_Receipt" />
                                        <asp:AsyncPostBackTrigger ControlID="btn_SaveAdjustment" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr id="tr_References" runat="server">
                <td style="width: 30%; vertical-align: top;">
                    <div id="Adjusted" class="DIV" style="height: 400px;">
                        <fieldset id="fld_AdjAmount" runat="server">
                            <legend>Payment Details:</legend>
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="Up_dgAdjusted" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DataGrid Style="width: 100%" ID="dg_Adjusted" runat="server" AutoGenerateColumns="False"
                                                    BorderStyle="Solid" BorderColor="Black" ShowFooter="false" OnItemDataBound="dg_Adjusted_ItemDataBound">
                                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk_Adjusted" OnClick="Check_SingleAdjusted(this,'dg_Adjusted'); jsCalculateTotalAdjusted('dg_Adjusted');"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="Bill_Date1" HeaderText="Date">
                                                            <HeaderStyle Width="20%" />
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="Ref No">
                                                            <ItemStyle HorizontalAlign="Left" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Ref_No" runat="server" Text='<%#Eval("Ref_No")%>' CssClass="LABEL" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="18%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="On Account Amount (Cr)" HeaderStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtBillAmount" runat="server" BackColor="Transparent" BorderStyle="None"
                                                                    BorderColor="Transparent" Style="text-align: right; width: 95%" Text='<%#Eval("Amount")%>'
                                                                    ReadOnly="true"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="20%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Adjust Amount">
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtAdjustedAmount" runat="server" CssClass="TEXTBOX" Style="text-align: right;
                                                                    width: 95%" Text='<%#Eval("AdjustedAmount")%>' onkeypress="return Only_Numbers(this,event)"
                                                                    onchange="OnTxtChange()" onblur="jsCalculateTotalAdjusted('dg_Adjusted'); txtbox_onlostfocus(this);"
                                                                    onfocus="this.select(); txtbox_onfocus(this);"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Voucher_Id">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Voucher_ID" runat="server" Text='<%#Eval("Voucher_Id")%>' CssClass="HIDEGRIDCOL" />
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="SrNo">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_SrNo" runat="server" Text='<%#Eval("SrNo")%>' CssClass="HIDEGRIDCOL" />
                                                                <asp:TextBox ID="txtSrNO" runat="server" Style="text-align: right; width: 95%" Text='<%#Eval("SrNo")%>'
                                                                    CssClass="HIDEGRIDCOL" ReadOnly="true"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="dg_Adjusted" EventName="ItemCommand" />
                                                <asp:AsyncPostBackTrigger ControlID="btn_SaveAdjustment" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
                <td style="width: 70%; vertical-align: top;">
                    <div id="Unadjusted" class="DIV" style="height: 400px;">
                        <fieldset id="fld_OnAccAdj" runat="server">
                            <legend>Pending Unadjusted References:</legend>
                            <table style="width: 100%">
                                <tr>
                                    <td style="height: 214px">
                                        <asp:UpdatePanel ID="up_dgUnadjusted" runat="server">
                                            <ContentTemplate>
                                                <asp:DataGrid Style="width: 100%" ID="dg_OnAccountAdjustment" runat="server" AutoGenerateColumns="False"
                                                    BorderStyle="Solid" BorderColor="Black" ShowFooter="false" OnItemDataBound="dg_OnAccountAdjustment_ItemDataBound">
                                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderStyle-Width="5%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chk_Unadjusted" OnClick="Check_SingleUnadjusted(this,'dg_OnAccountAdjustment');"
                                                                    runat="server" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Date" HeaderStyle-Width="15%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_VoucherDate" runat="server" Text='<%#Eval("Bill_Date1")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn DataField="Ref_No" HeaderText="Ref No" HeaderStyle-Width="22%"></asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="Received Amount">
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtReceivedAmount" runat="server" CssClass="TEXTBOX" Style="width: 80%;
                                                                    text-align: right" Text='<%#Eval("ReceivedAmount")%>' onkeypress="return Only_Numbers(this,event)"
                                                                    onfocus="this.select(); txtbox_onfocus(this);" onchange="OnTxtChange()" Width="67%"></asp:TextBox>
                                                                <asp:HiddenField ID="hdn_ReceivedAmount" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ReceivedAmount") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="TDS" HeaderStyle-Width="10%">
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtTDS" runat="server" CssClass="TEXTBOX" Style="width: 80%; text-align: right"
                                                                    Text='<%#Eval("TDSAmount")%>' onkeypress="return Only_Numbers(this,event)" onfocus="this.select(); txtbox_onfocus(this);"
                                                                    onchange="OnTxtChange()" Width="67%"></asp:TextBox>
                                                                <asp:HiddenField ID="hdn_TDS" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.TDSAmount") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Frt Deduction">
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtFrtDeduction" runat="server" CssClass="TEXTBOX" Style="width: 80%;
                                                                    text-align: right" Text='<%#Eval("FrtDeduction")%>' onkeypress="return Only_Numbers(this,event)"
                                                                    onfocus="this.select(); txtbox_onfocus(this);" onchange="OnTxtChange()" Width="67%"></asp:TextBox>
                                                                <asp:HiddenField ID="hdn_FrtDeduction" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.FrtDeduction") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Claim Deduction">
                                                            <HeaderStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtClaimDeduction" runat="server" CssClass="TEXTBOX" Style="width: 80%;
                                                                    text-align: right" Text='<%#Eval("ClaimDeduction")%>' onkeypress="return Only_Numbers(this,event)"
                                                                    onfocus="this.select(); txtbox_onfocus(this);" onchange="OnTxtChange()" Width="67%"></asp:TextBox>
                                                                <asp:HiddenField ID="hdn_ClaimDeduction" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.ClaimDeduction") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Pending Amount (Dr)" ItemStyle-HorizontalAlign="Right"
                                                            FooterStyle-HorizontalAlign="right" HeaderStyle-HorizontalAlign="Right">
                                                            <ItemStyle HorizontalAlign="right" />
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtPendingAmount" runat="server" Style="width: 80%; text-align: right;"
                                                                    Text='<%#Eval("PendingAmount")%>' ReadOnly="true" BackColor="Transparent" BorderStyle="None"
                                                                    BorderColor="Transparent" Width="67%"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Voucher_ID">
                                                            <HeaderStyle CssClass="HIDEGRIDCOL" Width="0%" />
                                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_VoucherID" runat="server" Text='<%#Eval("Voucher_ID")%>' CssClass="HIDEGRIDCOL" />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="TDSPercent">
                                                            <HeaderStyle CssClass="HIDEGRIDCOL" Width="0%" />
                                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_TDSPercent" runat="server" Text='<%#Eval("TDSPercent")%>' CssClass="HIDEGRIDCOL" />
                                                                <asp:HiddenField ID="hdn_TDPercent" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.TDSPercent") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="dg_OnAccountAdjustment" />
                                                <asp:AsyncPostBackTrigger ControlID="btn_SaveAdjustment" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </div>
                </td>
            </tr>
            <tr id="tr_ReferencesTotal" runat="server">
                <td style="width: 30%; vertical-align: top;" class="TD1">
                    <asp:Label ID="lbl_TotalBillAmunt" runat="server" Text="0.00" Font-Bold="true"></asp:Label>&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;
                    <asp:Label ID="lbl_TotalToBeAdjusted" runat="server" Text="0.00" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HiddenField ID="hdn_TotalToBeAdjusted" Value="0" runat="server" />
                </td>
                <td style="width: 70%; vertical-align: top;" align="left">
                    <table width="100%">
                        <tr>
                            <td class="TD1" style="width: 30%">
                                &nbsp;
                            </td>
                            <td class="TD1" style="width: 16%">
                                <asp:Label ID="lbl_TotalReceivedAmount" runat="server" Text="0.00" Font-Bold="true"></asp:Label>
                                <asp:HiddenField ID="hdn_TotalReceivedAmount" Value="0" runat="server" />
                            </td>
                            <td class="TD1" style="width: 8%">
                                <asp:Label ID="lbl_TotalTDS" runat="server" Text="0.00" Font-Bold="true"></asp:Label>
                                <asp:HiddenField ID="hdn_TotalTDS" Value="0" runat="server" />
                            </td>
                            <td class="TD1" style="width: 12%">
                                <asp:Label ID="lbl_TotalFrtDeduction" runat="server" Text="0.00" Font-Bold="true"></asp:Label>
                                <asp:HiddenField ID="hdn_TotalFrtDeduction" Value="0" runat="server" />
                            </td>
                            <td class="TD1" style="width: 12%">
                                <asp:Label ID="lbl_TotalClaimAmount" runat="server" Text="0.00" Font-Bold="true"></asp:Label>
                                <asp:HiddenField ID="hdn_TotalClaimAmount" Value="0" runat="server" />
                            </td>
                            <td class="TD1" style="width: 12%">
                                <asp:Label ID="lbl_TotalPendingAmount" runat="server" Text="0.00" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 30%">
                                Total :
                            </td>
                            <td colspan="5" style="width: 70%">
                                <asp:Label ID="lbl_TotalRecTDSFrtDeduction" runat="server" Text="0.00" Font-Bold="true"></asp:Label>
                                <asp:HiddenField ID="hdn_TotalRecTDSFrtDeduction" Value="0" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="tr2" runat="server">
                <td style="width: 30%; vertical-align: top;">
                    &nbsp
                </td>
                <td style="width: 70%; vertical-align: top;" align="center">
                    &nbsp;
                </td>
            </tr>
            <tr id="tr_ReferencesSave" runat="server">
                <td style="width: 30%; vertical-align: top;">
                    &nbsp
                </td>
                <td style="width: 70%; vertical-align: top;" align="center">
                    <asp:Button ID="btn_SaveAdjustment" runat="server" AccessKey="S" CssClass="BUTTON"
                        Text="Save" OnClick="btn_SaveAdjustment_Click" />
                </td>
            </tr>
            <tr id="tr_Receipt" runat="server">
                <td colspan="2">
                    <table class="TABLE">
                        <tr>
                            <td colspan="7">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%" class="TD1">
                                Receipt Date:
                            </td>
                            <td style="width: 20%">
                                <uc1:WucDatePicker ID="dtpReceiptDate" runat="server"></uc1:WucDatePicker>
                            </td>
                            <td style="width: 10%">
                                &nbsp;
                            </td>
                            <td style="width: 10%">
                                &nbsp;
                            </td>
                            <td style="width: 20%" class="TD1">
                                &nbsp;
                            </td>
                            <td style="width: 20%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%" class="TD1">
                                Cash Amount :
                            </td>
                            <td style="width: 20%">
                                <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_CashAmount" onfocus="txtbox_onfocus(this)" onkeypress="return Only_Integers(this,event)"
                                            onblur="CalculateTotal(); txtbox_onlostfocus(this)" runat="server" Text="0.00" CssClass="TEXTBOXNOS" Width="50%"></asp:TextBox></td>
                                        <asp:HiddenField ID="hdn_CashAmount" Value="0" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 10%">
                                &nbsp;
                            </td>
                            <td style="width: 10%">
                                &nbsp;
                            </td>
                            <td style="width: 20%" class="TD1">
                                &nbsp;
                            </td>
                            <td style="width: 20%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%" class="TD1">
                                Bank Amount :</td>
                            <td style="width: 20%">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_ChequeAmount" runat="server" CssClass="TEXTBOXNOS" onfocus="txtbox_onfocus(this)"
                                            onkeypress="return Only_Integers(this,event)" onblur="OnChequeAmountLossFocus(); CalculateTotal(); txtbox_onlostfocus(this)"
                                            Text="0.00" Width="50%"></asp:TextBox>
                                        <asp:HiddenField ID="hdn_ChequeAmount" Value="0" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 10%" class="TD1">
                                <asp:Label ID="lbl_BankPaymentBy" runat="server" CssClass="TD1" Text="By :"></asp:Label>
                            </td>
                            <td style="width: 10%" class="TD1">
                                <asp:DropDownList ID="ddl_BankPaymentType" runat="server" CssClass="DROPDOWN" Width="80%">
                                </asp:DropDownList></td>
                            <td style="width: 10%" class="TD1">
                                <asp:Label ID="lbl_CheckRefNo" runat="server" CssClass="TD1" Text="Cheque/RefNo.:"></asp:Label>
                            </td>
                            <td style="width: 20%">
                                <asp:TextBox CssClass="TEXTBOX" ID="txt_RefNo" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)" runat="server"
                                    Text="0" Width="90%" MaxLength="30"></asp:TextBox></td>
                            <td style="width: 20%" class="TD1" id="td_ChequeDate" runat="server">
                                <uc1:WucDatePicker ID="dtpBankDate" runat="server"></uc1:WucDatePicker>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7" style="height: 15px">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%" class="TD1">
                                <asp:Label ID="lbl_ReceiptBank" runat="server" CssClass="TD1" Text="Party's Bank :"></asp:Label>
                            </td>
                            <td style="width: 30%" colspan="2">
                                <asp:TextBox ID="txt_ReceiptBank" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)" runat="server" Text=""
                                    CssClass="TEXTBOX" Width="95%"></asp:TextBox>
                            </td>
                            <td style="width: 10%">
                            </td>
                            <td style="width: 20%" class="TD1">
                                <asp:Label ID="lbl_Deposited_Bank" runat="server" CssClass="TD1" Text="Deposited In :"></asp:Label>
                            </td>
                            <td style="width: 30%" colspan="2">
                                <asp:DropDownList ID="ddl_BankLedger" runat="server" CssClass="DROPDOWN" Width="95%">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%" class="TD1">
                                <asp:Label ID="lbl_TotalReceiptH" runat="server" Text="Total :" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width: 20%">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_TotalReceipt" runat="server" Text="0" CssClass="TEXTBOXNOS" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Bold="true" Width="50%"></asp:Label>
                                        <asp:HiddenField ID="hdn_TotalReceipt" Value="0" runat="server" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txt_CashAmount" />
                                        <asp:AsyncPostBackTrigger ControlID="txt_ChequeAmount" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 10%">
                                &nbsp;
                            </td>
                            <td style="width: 10%">
                                &nbsp;
                            </td>
                            <td style="width: 20%" align="center">
                                &nbsp;</td>
                            <td style="width: 20%" align="right">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%" class="TD1">
                                <asp:Label ID="lbl_Narration" runat="server" Text="Narration :" Font-Bold="true"></asp:Label>
                            </td>
                            <td style="width: 40%" colspan="3">
                                <asp:TextBox ID="txt_Narration" runat="server" CssClass="TEXTBOX" MaxLength="250" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"
                                    Height="30px" TextMode="MultiLine" Width="90%"></asp:TextBox>
                            </td>
                            <td style="width: 20%" align="center">
                                &nbsp;
                                <asp:Button ID="btn_Save_Receipt" runat="server" AccessKey="S" CssClass="BUTTON" Text="Save Receipt"
                                    OnClick="btn_Save_Receipt_Click" /></td>
                            <td style="width: 20%" align="right">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="7">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
