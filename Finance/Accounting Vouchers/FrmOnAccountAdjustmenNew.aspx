<%@ Page AutoEventWireup="true" CodeFile="FrmOnAccountAdjustmenNew.aspx.cs" Inherits="Finance_Accounting_Vouchers_FrmOnAccountAdjustmenNew"
    Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>

<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/txtsearch_common.js"></script>

<script type="text/javascript">



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

var Search_Type;
var lst_control_id;
function Search_txtSearch(e,txtbox,lstBox,SearchType,length)
{    


    Search_Type = SearchType;
    lst_control_id = lstBox;
    if (txtbox.value == '')
    {
        Clear_listbox(lstBox);
        hidecontrol(document.getElementById(lstBox));
        return;
    }
    var hdn_ClientId = document.getElementById("hdn_ClientId");
    var txtvalue = txtbox.value.toUpperCase();
    if(txtvalue.length >= length)
    {
        if (keyCode != 37 && keyCode != 38 && keyCode != 39 && keyCode != 40)
        {
            if(oldvalue != txtvalue)
            {
                if(SearchType == 'Client')
                
                    Raj.EF.CallBackFunction.CallBack.RegularClient_txtSearch(txtvalue,handleResults);

            }
        }
    }
    if (keyCode == 38 || keyCode == 40)
        listboxupdown(txtbox,lstBox);
}

function handleResults(results)
{  
  var list_control = document.getElementById(lst_control_id);
  
  var tot = results.value.Rows.length -1;
  var count = 0;
  
  for (var count = list_control.options.length-1; count >-1; count--)
  {
    list_control.options[count] = null;
  }

  for (count = 0;count <= tot;count ++)
  { 
    list_control.options[count] = new Option(results.value.Rows[count][results.value.Columns[0].Name],results.value.Rows[count][results.value.Columns[1].Name]); 
  }
  
    if (list_control.options.length == 0)
      hidecontrol(list_control);
    else
      showcontrol(list_control);

}

function On_txtLostFocus(txtbox,list_control,hdn_control)
{
    var txtbox_value = document.getElementById(txtbox).value.toUpperCase();
    var listcontrol = document.getElementById(list_control); 
    var list_control_index = listcontrol.selectedIndex;
    var list_control_value;
    var list_control_text;
    
    hidecontrol(listcontrol);
    if (oldvalue != txtbox_value)
    {
    
        if (list_control_index != -1){
            list_control_value = listcontrol.options[list_control_index].value;
            list_control_text = listcontrol.options[list_control_index].text;
        }
        else{
            list_control_value = '0';
            list_control_text = '';
        }
    
        document.getElementById(hdn_control).value = list_control_value;
        document.getElementById(txtbox).value = list_control_text;
    }

     if(Search_Type == 'Client')
     {
         if (oldvalue != txtbox_value)
            update_PartyDetails();
     }

}
    
 
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>On Account Adjustment</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="On Account Adjustment"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 34%; height: 15px;" colspan="2" class="TD1">
                                Select Ledger :</td>
                            <td style="width: 33%; height: 15px;" colspan="2">
                                <asp:UpdatePanel ID="UpdatePanelParty" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txt_Client" runat="server" autocomplete="off" CssClass="TEXTBOX"
                                            EnableViewState="False" MaxLength="50" onblur="On_txtLostFocus('txt_Client','lst_Client','hdn_ClientId'); txtbox_onlostfocus(this);"
                                            onfocus="On_Focus('txt_Client','lst_Client');txtbox_onfocus(this);" onkeydown="return on_keydown(event,'txt_Client','lst_Client');"
                                            onkeyup="Search_txtSearch(event,this,'lst_Client','Client',2);" Width="90%"></asp:TextBox>
                                        <asp:ListBox ID="lst_Client" runat="server" onfocus="listboxonfocus('txt_Client')"
                                            Style="z-index: 1000; position: absolute" TabIndex="21" Width="90%"></asp:ListBox>
                                        <asp:HiddenField ID="hdn_ClientId" runat="server" Value="0" />
                                        <asp:HiddenField ID="hdn_LedgerId" runat="server" Value="0" />
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txt_Client" />
                                        <asp:AsyncPostBackTrigger ControlID="btn_hidden" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 9%; height: 15px;">
                            </td>
                            <td style="width: 24%; height: 15px;">
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr id="tr_References" runat="server">
                <td style="width: 30%; vertical-align: top;">
                    <div id="Adjusted" class="DIV" style="height: 400px; vertical-align:top">
                        <fieldset id="fld_AdjAmount" runat="server">
                            <legend>Payment Details:</legend>
                            <table style="width: 100%">
                                <tr valign="Top">
                                    <td style="height: 300px">
                                        <asp:UpdatePanel ID="Up_dgAdjusted" runat="server">
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
                                                        <asp:BoundColumn DataField="Voucher_Date" HeaderText="Date">
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
                                                <asp:AsyncPostBackTrigger ControlID="dg_Adjusted" />
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
                                <tr valign="top">
                                    <td style="height: 300px">
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
                <td style="width: 30%; vertical-align: top; height: 50px;" class="TD1">
                    <asp:Label ID="lbl_TotalBillAmunt" runat="server" Text="0.00" Font-Bold="true"></asp:Label>&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;&nbsp&nbsp;
                    <asp:Label ID="lbl_TotalToBeAdjusted" runat="server" Text="0.00" Font-Bold="true"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:HiddenField ID="hdn_TotalToBeAdjusted" Value="0" runat="server" />
                </td>
                <td style="width: 70%; vertical-align: top; height: 50px;" align="left">
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
                <td style="width: 30%; vertical-align: top; height: 15px;">
                    &nbsp
                </td>
                <td style="width: 70%; vertical-align: top; height: 15px;" align="left">
                    <asp:UpdatePanel ID="UpdatePanel" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_SaveAdjustment" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btn_hidden" runat="server" OnClick="btn_hidden_Click" Style="display: none"
                                Text="" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_hidden" />
                        </Triggers>
                    </asp:UpdatePanel>
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr id="tr_ReferencesSave" runat="server">
                <td style="width: 30%; vertical-align: top;">
                    &nbsp
                    <asp:Button ID="btn_SaveAdjustment" runat="server" AccessKey="S" CssClass="BUTTON"
                        Text="Save" OnClick="btn_SaveAdjustment_Click" /></td>
                <td style="width: 70%; vertical-align: top;" align="left">
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" />&nbsp;</td>
            </tr>
        </table>
    </form>

    <script type="text/javascript">
    
    function update_PartyDetails()
    {
    document.getElementById('<%=btn_hidden.ClientID%>').style.display = "none";
    document.getElementById('<%=btn_hidden.ClientID%>').style.visibility = "hidden";
    document.getElementById('<%=btn_hidden.ClientID%>').click();
    }
    </script>

</body>
</html>
