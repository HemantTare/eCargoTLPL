<%@ Page AutoEventWireup="true" CodeFile="FrmOnAccountReceipt.aspx.cs" Inherits="Finance_Accounting_Vouchers_FrmOnAccountReceipt"
    Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>

<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/txtsearch_common.js"></script>

<script type="text/javascript">

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

}
    


</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>OnAccount Receipt</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="OnAccount Receipt"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 34%" colspan="2" class="TD1">
                                Received From :
                            </td>
                            <td style="width: 33%" colspan="2">
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
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td style="width: 9%">
                            </td>
                            <td style="width: 24%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" colspan="6" style="text-align: left">
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
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
                                            onblur="CalculateTotal(); txtbox_onlostfocus(this)" runat="server" Text="0.00"
                                            CssClass="TEXTBOXNOS" Width="50%"></asp:TextBox></td>
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
                            <td style="width: 30%" colspan="2">
                                &nbsp;&nbsp;
                                <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btn_Save_Receipt" />
                                    </Triggers>
                                </asp:UpdatePanel>
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
                                <asp:TextBox CssClass="TEXTBOX" ID="txt_RefNo" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"
                                    runat="server" Text="0" Width="90%" MaxLength="30"></asp:TextBox></td>
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
                                <asp:TextBox ID="txt_ReceiptBank" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"
                                    runat="server" Text="" CssClass="TEXTBOX" Width="95%"></asp:TextBox>
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
                                <asp:TextBox ID="txt_Narration" runat="server" CssClass="TEXTBOX" MaxLength="250"
                                    onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)" Height="30px"
                                    TextMode="MultiLine" Width="90%"></asp:TextBox>
                            </td>
                            <td style="width: 20%" align="center">
                                &nbsp;
                                <asp:Button ID="btn_Save_Receipt" runat="server" AccessKey="S" CssClass="BUTTON"
                                    Text="Save Receipt" OnClick="btn_Save_Receipt_Click" /></td>
                            <td style="width: 30%" align="Left" colspan="2">
                                <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                                    Text="Close Window" />&nbsp;
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

    <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>

</body>
</html>
