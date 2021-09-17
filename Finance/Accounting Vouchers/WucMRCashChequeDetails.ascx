<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucMRCashChequeDetails.ascx.cs" Inherits="Finance_Accounting_Vouchers_WucMRCashChequeDetails" %>
<%@ Register Assembly="ComponentArt.Web.UI" TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript">
function Cash_Amount_Change()
{
 
        var chk_Is_AutoCalculate = document.getElementById('<%=chk_Is_AutoCalculate.ClientID %>');
        var txt_CashAmount = document.getElementById('<%=txt_CashAmount.ClientID %>');
        var txt_ChequeAmount = document.getElementById('<%=txt_ChequeAmount.ClientID %>');
        var hdn_CashAmount = document.getElementById('<%=hdn_CashAmount.ClientID %>');
        var hdn_ChequeAmount = document.getElementById('<%=hdn_ChequeAmount.ClientID %>');
        var hdn_ToatlChequeAmount = document.getElementById('<%=hdn_Total_Cheque_Amount.ClientID %>');        
        
        var Amount=0;
        Amount = GetTotalAmount();

        hdn_CashAmount.value = val(txt_CashAmount.value); 
        hdn_ChequeAmount.value= val(txt_ChequeAmount.value); 
        Cheque_Amount(); 
        
        if(chk_Is_AutoCalculate.checked == false)
            return;
        
        if ( Amount   < val(txt_CashAmount.value))
        {  
            txt_CashAmount.value = Amount;
        }
        else if (Amount   < val(txt_ChequeAmount.value))
        {  
            txt_ChequeAmount.value = Amount ;
        }
  
//        txt_ChequeAmount.value= Amount - val( txt_CashAmount.value );   
        
        if (val(txt_ChequeAmount.value) < 0)
            txt_ChequeAmount.value = 0;

        hdn_CashAmount.value = val(txt_CashAmount.value); 
        hdn_ChequeAmount.value= val( txt_ChequeAmount.value ); 

        if(val(txt_ChequeAmount.value) == 0)
        {
            hdn_ToatlChequeAmount.value = val(txt_ChequeAmount.value);
        }
        Cheque_Amount();  
}


function Cheque_Amount_Change()
{
        var chk_Is_AutoCalculate = document.getElementById('<%=chk_Is_AutoCalculate.ClientID %>');
        var txt_CashAmount = document.getElementById('<%=txt_CashAmount.ClientID %>');
        var txt_ChequeAmount = document.getElementById('<%=txt_ChequeAmount.ClientID %>');
        var hdn_CashAmount = document.getElementById('<%=hdn_CashAmount.ClientID %>');
        var hdn_ChequeAmount = document.getElementById('<%=hdn_ChequeAmount.ClientID %>');
        var hdn_ToatlChequeAmount = document.getElementById('<%=hdn_Total_Cheque_Amount.ClientID %>');
        var Amount=0;
        Amount = GetTotalAmount();       

        hdn_CashAmount.value = val(txt_CashAmount.value); 
        hdn_ChequeAmount.value= val( txt_ChequeAmount.value ); 
        Cheque_Amount(); 

        if(chk_Is_AutoCalculate.checked == false)
            return;
            
        if ( Amount   < val(txt_CashAmount.value)  )
        {  
            txt_CashAmount.value = val( Amount);
        }  
        else if (Amount   < val(txt_ChequeAmount.value)  )
        {  
            txt_ChequeAmount.value = Amount ;
        }  
//        txt_CashAmount.value =  val( Amount) - val( txt_ChequeAmount.value); 

        if (val(txt_CashAmount.value) < 0)
            txt_CashAmount.value = 0;

        hdn_CashAmount.value = val(txt_CashAmount.value); 
        hdn_ChequeAmount.value= val( txt_ChequeAmount.value ); 

        if(val(txt_ChequeAmount.value) == 0)
        {
            hdn_ToatlChequeAmount.value = val(txt_ChequeAmount.value);
        }          
         Cheque_Amount();  
}

function Cheque_Amount()
{
     var Menu_Item_ID = <%=_Menu_Item_ID %>
     var chk_Is_AutoCalculate = document.getElementById('<%=chk_Is_AutoCalculate.ClientID %>');
     var txt_ChequeAmount = document.getElementById('<%=txt_ChequeAmount.ClientID %>')
     var tr_grid = document.getElementById('<%=tr_grid.ClientID %>')
     var ddl_CashLedger = document.getElementById('<%=ddl_CashLedger.ClientID %>');
     var lbl_CashLedger = document.getElementById('<%=lbl_CashLedger.ClientID %>');
     var td_CashLedger = document.getElementById('<%=td_ddl_cash_ledger.ClientID %>');
       
       if(Menu_Item_ID == 113 || Menu_Item_ID ==114 || Menu_Item_ID == 202)
       {
        ddl_CashLedger.style.visibility = 'hidden';
        lbl_CashLedger.style.visibility = 'hidden';
        td_CashLedger.style.visibility = 'hidden';
       }
       if(Menu_Item_ID == 11131)
       {
  
          for (var i = 0; i < ddl_CashLedger.options.length; i++) 
          {
            if (ddl_CashLedger.options.length > 0) 
            {
                if(ddl_CashLedger.options[1].text.length > 0)
                {
                  ddl_CashLedger.options[1].selected = 1;
                  ddl_CashLedger.style.visibility = 'hidden';
                  lbl_CashLedger.style.visibility = 'hidden';
                  td_CashLedger.style.visibility = 'hidden';
                }
            }
          }
       }
    
    if(val(txt_ChequeAmount.value) > 0)
    {
        tr_grid.style.display = 'inline';
    }
    else
    {
        tr_grid.style.display = 'none';
    }
}
function Chk_CashLedger()
{ 
     var Menu_Item_ID = <%=_Menu_Item_ID %> 
     var ddl_CashLedger = document.getElementById('<%=ddl_CashLedger.ClientID %>');
     var lbl_CashLedger = document.getElementById('<%=lbl_CashLedger.ClientID %>');
     var td_CashLedger = document.getElementById('<%=td_ddl_cash_ledger.ClientID %>');
       
       if(Menu_Item_ID == 83)
       {
        ddl_CashLedger.style.visibility = 'hidden';
        lbl_CashLedger.style.visibility = 'hidden';
        td_CashLedger.style.visibility = 'hidden';
       } 
}


function validateWUCCheque(lbl_Error)
{ 
    var Menu_Item_ID = <%=_Menu_Item_ID %>
    var chk_Is_AutoCalculate = document.getElementById('<%=chk_Is_AutoCalculate.ClientID %>');
    var txt_CashAmount = document.getElementById('<%=txt_CashAmount.ClientID %>');
    var txt_ChequeAmount = document.getElementById('<%=txt_ChequeAmount.ClientID %>');
    
    if(Menu_Item_ID != 83)
    {
        var ddl_CashLedger = document.getElementById('<%=ddl_CashLedger.ClientID %>');
    }
    
//    if (ddl_CashLedger.value <= '0' && (Menu_Item_ID == 106 || Menu_Item_ID == 108 || Menu_Item_ID == 11131 || Menu_Item_ID == 83) && val(txt_CashAmount.value) > 0)
        if(Menu_Item_ID != 83)
        {
            if (ddl_CashLedger.value <= '0' && (Menu_Item_ID == 106 || Menu_Item_ID == 108 || Menu_Item_ID == 11131) && val(txt_CashAmount.value) > 0)
            {
                lbl_Error.innerText = 'Please select Cash Ledger';
                ddl_CashLedger.focus();
                return false;
            }
        }
        else if (val(txt_CashAmount.value) <= 0 && val(txt_ChequeAmount.value) <= 0 && chk_Is_AutoCalculate.checked == true)
        {
            lbl_Error.innerText = 'Please Enter Cash Amount Or Cheque Amount';
            return false;
        }
            return true;
}

</script>

<asp:Panel ID="Panel1" runat="server" Width="100%">
<table width="100%">
    <tr>
        <td style="width: 20%; " class="TD1">
            <asp:Label ID="lbl_CashAmount" runat="server" CssClass="LABEL" Text="Cash Amount :" ></asp:Label></td>
        <td style="width: 29%; ">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                <ContentTemplate>
            <asp:TextBox ID="txt_CashAmount" runat="server" CssClass="TEXTBOXNOS" Width="60%" onblur="Cash_Amount_Change();return valid(this)" onkeypress="return Only_Numbers(this,event)" MaxLength="9"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%; " class="TDMANDATORY">*</td>
        <td style="width: 20%; " class="TD1">
            <asp:Label ID="lbl_CashLedger" runat="server" CssClass="LABEL" Text="Cash Ledger :" ></asp:Label></td>
        <td style="width: 29%; ">
            <asp:DropDownList ID="ddl_CashLedger" runat="server" CssClass="DROPDOWN"  >
            </asp:DropDownList></td>
        <td style="width: 1%; " class="TDMANDATORY" id="td_ddl_cash_ledger" runat="server">*
        </td>
    </tr>   
    <tr>
        <td style="width: 20%" class="TD1">
            <asp:Label ID="lbl_ChequeAmount" runat="server" CssClass="LABEL" Text="Cheque Amount :"></asp:Label></td>
        <td style="width: 29%">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                <ContentTemplate>
            <asp:TextBox ID="txt_ChequeAmount" runat="server" CssClass="TEXTBOXNOS" Width="60%" onkeypress="return Only_Numbers(this,event)" onblur="Cheque_Amount_Change()" MaxLength="9"></asp:TextBox>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%" class="TDMANDATORY">*</td>
        <td style="width: 50%;display:none" colspan="3">
              <asp:CheckBox ID="chk_Is_AutoCalculate" runat="server" Checked="true"></asp:CheckBox>
        </td>       
    </tr>
    <tr id="tr_grid" runat="server">
        <td colspan="6" >
            <asp:UpdatePanel ID="up_grid" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <asp:DataGrid ID="dg_ChequeDetails" runat="server" Width="100%" CssClass="GRID" AutoGenerateColumns="False" 
                        ShowFooter="True" OnCancelCommand="dg_ChequeDetails_CancelCommand" 
                        OnDeleteCommand="dg_ChequeDetails_DeleteCommand" 
                        OnEditCommand="dg_ChequeDetails_EditCommand" OnItemCommand="dg_ChequeDetails_ItemCommand" 
                        OnItemDataBound="dg_ChequeDetails_ItemDataBound" 
                        OnUpdateCommand="dg_ChequeDetails_UpdateCommand" >
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <Columns>
                        <asp:TemplateColumn HeaderText = "Cheque Bank">
                            <HeaderStyle Width="20%" />
                            <FooterTemplate>
                                <asp:TextBox ID="txt_Chequebank" CssClass="TEXTBOX" Width="98%" MaxLength="100" runat="server" ></asp:TextBox> 
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Cheque_Bank_Name")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txt_Chequebank" CssClass="TEXTBOX" Width="98%" MaxLength="100" runat="server" ></asp:TextBox> 
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText = "Cheque Branch">
                            <HeaderStyle Width="20%" />
                            <FooterTemplate>
                                <asp:TextBox ID="txt_ChequeBranch" CssClass="TEXTBOX" Width="98%" MaxLength="50" runat="server" ></asp:TextBox> 
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Cheque_Branch_Name")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txt_ChequeBranch" CssClass="TEXTBOX" Width="98%" MaxLength="50" runat="server" ></asp:TextBox> 
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Cheque No.">
                            <HeaderStyle Width="20%" />
                            <FooterTemplate>
                                <asp:TextBox ID="txt_ChequeNo" CssClass="TEXTBOX" Width="98%" MaxLength="6" runat="server" onkeypress="return Only_Numbers(this,event)"></asp:TextBox> 
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Cheque_No")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txt_ChequeNo" CssClass="TEXTBOX" Width="98%" MaxLength="6" runat="server" onkeypress="return Only_Numbers(this,event)"></asp:TextBox> 
                            </EditItemTemplate> 
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Cheque Amount" HeaderStyle-HorizontalAlign="Right">
                            <HeaderStyle Width="15%" />
                            <ItemStyle HorizontalAlign ="Right" /> 
                            <FooterTemplate>
                                <asp:TextBox ID="txt_ChequeAmount" CssClass="TEXTBOXNOS" onblur="return valid(this)" Width="95%"
                                   runat="server" onkeypress="return Only_Numbers(this,event)" MaxLength="9"></asp:TextBox> 
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Cheque_Amount")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txt_ChequeAmount" CssClass="TEXTBOXNOS"   onblur="return valid(this)"
                                    runat="server" onkeypress="return Only_Numbers(this,event)" MaxLength="9" Width="95%"></asp:TextBox> 
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Deposit In">
                            <HeaderStyle Width="20%" />
                            <FooterTemplate>
                                <asp:DropDownList ID="ddl_DepositIn" runat="server" CssClass="DROPDOWN" Width="98%"></asp:DropDownList>
                            </FooterTemplate>
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Bank_Ledger_Name")%>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddl_DepositIn" runat="server" CssClass="DROPDOWN" Width="98%"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateColumn>

                        <asp:TemplateColumn HeaderText="Cheque Date">
                            <ItemTemplate>
                                <asp:Label ID="lbl_ChequeDate" runat="server" Font-Names="Verdana" Text='<%# DataBinder.Eval(Container.DataItem, "Cheque_Date","{0:MMM/dd/yyyy}") %>' ></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <ComponentArt:Calendar id="wuc_ChequeDate" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                                ControlType="Picker" PickerCssClass="PICKER" AllowDaySelection="True" AllowMonthSelection="True"
                                MinDate="1900-01-01"/>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <ComponentArt:Calendar id="wuc_ChequeDate" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                                ControlType="Picker" PickerCssClass="PICKER" AllowDaySelection="True" AllowMonthSelection="True"
                                MinDate="1900-01-01" SelectedDate="6-DEC-2008"/>
                            </FooterTemplate>                                
                        </asp:TemplateColumn>

                        <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit" >
                        </asp:EditCommandColumn>

                        <asp:TemplateColumn HeaderText="Delete">
                            <FooterTemplate>
                                <asp:LinkButton ID="lbtn_Add" Text="Add" Runat="server" CommandName="Add" ></asp:LinkButton>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete" ></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_ChequeDetails" />
                </Triggers>
            </asp:UpdatePanel>        
         </td>
    </tr>
    
    <tr>
        <td colspan="5">
            <asp:UpdatePanel ID="up_Error" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"></asp:Label>
                </ContentTemplate>
                </asp:UpdatePanel>    
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:HiddenField ID="hdn_CashAmount" runat="server" />
                    <asp:HiddenField ID="hdn_ChequeAmount" runat="server" Value="0"/>
                    <asp:HiddenField ID="hdn_Total_Cheque_Amount" runat="server" Value="0"/>
                    <asp:HiddenField ID="hdn_Max_Cash_Amount" runat="server" />
                    
                </ContentTemplate>
            </asp:UpdatePanel>         
        </td>
        <td style="display:none">
            <asp:CheckBox ID="Chk_IsMaxCashLimit_Required" runat="server" />
        </td>
    </tr>
</table>
</asp:Panel>

<script type="text/javascript"> 
Cheque_Amount();
Chk_CashLedger();
</script>