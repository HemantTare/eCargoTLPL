<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucOnaccountAdjustment.ascx.cs" Inherits="Finance_Accounting_Vouchers_WucOnaccountAdjustment" %>


<%@ Register Assembly = "DDLSearch" Namespace = "ClassLibrary.UIControl" TagPrefix = "cc1" %>
<script src="../../Javascript/Common.js" type="text/javascript" ></script>
<script type = "text/javascript" src = "../../Javascript/ddlsearch.js"></script>
<script type="text/javascript" src="../../../Javascript/CommonReports.js"></script>
<asp:ScriptManager ID = "scm_onaccountadjustment" runat = "server"></asp:ScriptManager>

<script type="text/javascript">

function OnTxtChange()
{
    var lbl_BalanceAmount = document.getElementById('<%=lbl_BalanceAmount.ClientID %>')
    var dg_Grid = document.getElementById('<%=dg_Adjusted.ClientID %>')
    var hdn_unAdjAmount = document.getElementById('<%=hdn_unAdjAmount.ClientID %>')
    var UnAdjAmount = val(hdn_unAdjAmount.value);
    var totalAmount = 0;
    var BalanceAmount = 0;
   
    for(i = 0; i < document.forms[0].elements.length; i++) 
        {
            var elm = document.forms[0].elements[i];
            var elm_id = document.getElementById(elm.id);
            
            var elm_name = elm.name;
            
            if(elm.tagName != 'FIELDSET')
            {
                  if (elm.type == 'text') 
                    {
                       totalAmount = totalAmount + val(elm.value);
                    }
            }
        }
        
      BalanceAmount = UnAdjAmount - totalAmount;
      
      if(BalanceAmount > 0)
      {
        lbl_BalanceAmount.innerText = Math.abs(BalanceAmount) + ' Cr';
      }
      else
      {
        lbl_BalanceAmount.innerText = Math.abs(BalanceAmount) + ' Dr';
      }

}


function get_button_nullsession_clientid()
{
btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}
function Only_Numbers(f,evt)
{
//if (window.event.keyCode == 37 || window.event.keyCode == 39 || window.event.keyCode == 9 || window.event.keyCode == 16) return;
//f.value = f.value.replace(/[^.\d]/g,"");

var charCode = (evt.which) ? evt.which : event.keyCode



if(charCode == 46)
{
    var arr=f.value.split('.');

    if (arr.length > 1)
        {
            return false;
        }
    return true;
}
if (charCode > 31 && (charCode < 48 || charCode > 57))
    return false;


    return true;

}

 
 function Allow_To_Save()
 {
    return true;
 }
 

</script>

<table class="TABLE" style ="width:100%">
    <tr>
        <td class="TDGRADIENT" colspan="2" >
            <asp:Label ID="lbl_Heading" runat="server" Text="ONACCOUNT ADJUSTMENT" CssClass="HEADINGLABEL"></asp:Label>
        </td>
    </tr>
    
    <tr>
        <td  >
            <table style="width:100%">
                <tr>
                    <td style="width:25%">
                        Ledger Group :</td>
                    <td style="width:75%">
                        <asp:DropDownList ID="ddl_LedgerGroup" runat="server" CssClass = "DROPDOWN" style="width:100%" AutoPostBack = "true" OnSelectedIndexChanged="ddl_LedgerGroup_SelectedIndexChanged">
                        </asp:DropDownList></td>
                </tr>
            </table>
        </td>
        <td  >
        </td>
    </tr>
    <tr>
        <td  >
            <table style="width:100%">
            <tr>
                <td style="width:25%">
                    Ledger Name :</td>
                <td style="width:85%">
                    <%--<asp:UpdatePanel ID = "up_ddlledger" runat = "server">
                    <ContentTemplate >
                        <asp:DropDownList ID="ddl_Ledger" runat="server" CssClass = "DROPDOWN" style="width:100%" AutoPostBack = "true" OnSelectedIndexChanged="ddl_Ledger_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers >
                        <asp:AsyncPostBackTrigger ControlID = "ddl_LedgerGroup" />
                    </Triggers>
                    </asp:UpdatePanel>--%>
                  <asp:UpdatePanel ID = "up_ddlledger" runat = "server">
                    <ContentTemplate >
                        <cc1:DDLSearch id = "ddl_Ledger" runat = "server" postback = "true" OnTxtChange="ddl_Ledger_SelectedIndexChanged" IsCallBack = "true" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerOnAccount"></cc1:DDLSearch>
                  </ContentTemplate>
                    <Triggers >
                        <asp:AsyncPostBackTrigger ControlID = "ddl_LedgerGroup" />
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            </table>
        </td>
        <td style="text-align: right;"  >
            <table>
            <tr>
                <td style="width:9%; text-align: right;">
                    <asp:UpdatePanel ID = "up_autoajust" runat = "server">
                    <ContentTemplate >
                    <asp:CheckBox ID="chk_AutoAdjust" runat="server" Text = "Auto Adjust" OnCheckedChanged="chk_AutoAdjust_CheckedChanged" AutoPostBack = "true" />
                    </ContentTemplate>
                    <Triggers >
                        <asp:AsyncPostBackTrigger ControlID = "dg_OnAccountAdjustment" />
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td style="width:1%">
                
                </td>
            <td style="width:9%; text-align: right;">
                <asp:UpdatePanel ID = "up_balancamt" runat = "server">
                <ContentTemplate >
                Balance Amount :<asp:Label ID = "lbl_BalanceAmount" runat = "server" style="color:Red;font-weight:bold" Text = "0.00"></asp:Label>
                </ContentTemplate>
                <Triggers >
                    <asp:AsyncPostBackTrigger ControlID = "dg_Adjusted" />
                </Triggers>
                </asp:UpdatePanel>
            </td>
            </tr>
            
            </table>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
    </tr>

    <tr>
        <td  style="width:50%; vertical-align :top ;">
            <fieldset id="fld_OnAccAdj" runat="server"><legend>UNADJUSTED VOUCHERS:</legend>
                <table style="width:100%">
                <tr>
                    <td>
                    <asp:UpdatePanel ID = "up_dgUnadjusted" runat = "server">
                    <ContentTemplate >
            <asp:DataGrid style="width:100%" id = "dg_OnAccountAdjustment" runat = "server"
              AutoGenerateColumns="False" BorderStyle = "Solid" BorderColor = "Black"
               ShowFooter = "true" OnItemDataBound="dg_OnAccountAdjustment_ItemDataBound"  >
            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
            <FooterStyle CssClass="GRIDFOOTERCSS" />
            <HeaderStyle CssClass="GRIDHEADERCSS" />
                <Columns>
                    <asp:TemplateColumn HeaderStyle-Width = "5%" >
                        <ItemTemplate >
                            <asp:RadioButton ID = "rb_Unadjusted" runat = "server" AutoPostBack = "true" OnCheckedChanged="rb_Unadjusted_CheckedChanged"  />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="SrNo" HeaderText="Sr No." HeaderStyle-Width = "10%" ></asp:BoundColumn>
                    <%--<asp:BoundColumn DataField="Voucher_Date" HeaderText="Date">
                    </asp:BoundColumn>--%>
                    
                    <asp:TemplateColumn HeaderText="Date" HeaderStyle-Width = "22%"  >
                        <ItemTemplate >
                            <asp:Label ID = "lbl_VoucherDate" runat = "server" Text = '<%#Eval("Voucher_Date")%>' ></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:BoundColumn DataField="Ref_No" HeaderText="Ref No" HeaderStyle-Width = "22%" ></asp:BoundColumn>
                    <%--<asp:BoundColumn DataField="BillAmount" HeaderText="Amount" ItemStyle-HorizontalAlign ="Right"></asp:BoundColumn>--%>
                   <%-- <asp:BoundColumn DataField="PendingAmount" HeaderText="UnAdjusted Amount" ItemStyle-HorizontalAlign ="Right">
                        
                    </asp:BoundColumn>--%>
                    
                    <asp:TemplateColumn HeaderText="Amount" ItemStyle-HorizontalAlign ="Right" FooterStyle-HorizontalAlign = "right" HeaderStyle-Width = "22%" >
                        <ItemStyle HorizontalAlign="right" />
                        <ItemTemplate >
                            <asp:Label ID = "lblbillAmountDrCR" runat = "server" Text = '<%#convertToDrCr(Eval("BillAmount"))%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate >
                            <asp:Label ID = "lblTotalBillAmountDrCR" runat = "server" style="font-weight:bold"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Amount" ItemStyle-HorizontalAlign ="Right" FooterStyle-HorizontalAlign = "right" HeaderStyle-Width = "22%" Visible="false" >
                        <ItemStyle HorizontalAlign="right" />
                        <ItemTemplate >
                            <asp:Label ID = "lblbillAmount" runat = "server" Text = '<%#Eval("BillAmount")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate >
                            <asp:Label ID = "lblTotalBillAmount" runat = "server" style="font-weight:bold"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="UnAdjusted Amount" ItemStyle-HorizontalAlign ="Right" FooterStyle-HorizontalAlign = "right" HeaderStyle-Width = "22%" >
                        <ItemStyle HorizontalAlign="right" />
                        <ItemTemplate >
                            <asp:Label ID = "lblUnadjustedAmountDrCr" runat = "server" Text = '<%#convertToDrCr(Eval("PendingAmount"))%>'></asp:Label>
                        </ItemTemplate>
                         <FooterTemplate >
                            <asp:Label ID = "lblTotalUnadjustedAmountDrCR" runat = "server" style="font-weight:bold"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="UnAdjusted Amount" ItemStyle-HorizontalAlign ="Right" FooterStyle-HorizontalAlign = "right" HeaderStyle-Width = "22%" Visible="false"  >
                        <ItemStyle HorizontalAlign="right" />
                        <ItemTemplate >
                            <asp:Label ID = "lblUnadjustedAmount" runat = "server" Text = '<%#Eval("PendingAmount")%>'></asp:Label>
                        </ItemTemplate>
                         <FooterTemplate >
                            <asp:Label ID = "lblTotalUnadjustedAmount" runat = "server" style="font-weight:bold"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText = "LedgerID"  >
                        <HeaderStyle CssClass = "HIDEGRIDCOL" Width = "0%" />
                        <ItemStyle CssClass = "HIDEGRIDCOL" />
                        <ItemTemplate   >
                            <asp:Label ID = "lbl_LedgerID" runat = "server" Text = '<%#Eval("Ledger_ID")%>'  CssClass = "HIDEGRIDCOL"  />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText = "voucherId"  >
                         <HeaderStyle CssClass = "HIDEGRIDCOL" Width = "0%" />
                        <ItemStyle CssClass = "HIDEGRIDCOL" />
                        <ItemTemplate  >
                            <asp:Label ID = "lbl_Voucher_ID" runat = "server" Text = '<%#Eval("Voucher_Id")%>' CssClass = "HIDEGRIDCOL"   />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    
                     <asp:TemplateColumn HeaderText = "SrNo"  >
                        <HeaderStyle CssClass = "HIDEGRIDCOL" Width = "0%" />
                        <ItemStyle CssClass = "HIDEGRIDCOL" />
                        <ItemTemplate  >
                            <asp:Label ID = "lbl_SrNo" runat = "server" Text = '<%#Eval("Details_Id")%>' CssClass = "HIDEGRIDCOL"    />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
                    </ContentTemplate>
                    <Triggers >
                        <asp:AsyncPostBackTrigger ControlID = "dg_OnAccountAdjustment" />
                        
                    </Triggers>
                    </asp:UpdatePanel>
                </td> 
                </tr> 
                </table> 
                </fieldset> 
        </td>

        <td style="width:50%;vertical-align :top;">
            <fieldset id="fld_AdjAmount" runat="server"><legend>ADJUSTMENT:</legend>
                <table style="width:100%">
                <tr>
                <td>
                <asp:UpdatePanel ID = "Up_dgAdjusted" runat = "server">
                <ContentTemplate >
            <asp:DataGrid style="width:100%" id = "dg_Adjusted" runat = "server"
              AutoGenerateColumns="False" BorderStyle = "Solid" BorderColor = "Black"
              ShowFooter = "true" OnItemDataBound="dg_Adjusted_ItemDataBound" >
            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
            <FooterStyle CssClass="GRIDFOOTERCSS" />
            <HeaderStyle CssClass="GRIDHEADERCSS" />
                <Columns>
                    <asp:BoundColumn DataField="SrNo" HeaderText="Sr No.">
                        <HeaderStyle Width="5%" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="Voucher_Date" HeaderText="Date">
                        <HeaderStyle Width="20%" />
                    </asp:BoundColumn>
                    
                    <asp:TemplateColumn HeaderText = "Ref No">
                     <ItemStyle HorizontalAlign="Left" />
                        <ItemTemplate  >
                            <asp:Label ID = "lbl_Ref_No" runat = "server" Text = '<%#Eval("Ref_No")%>' CssClass = "LABEL"  />
                        </ItemTemplate>
                        <HeaderStyle Width="18%" />
                        
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Amount" >
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemTemplate >
                            <asp:Label ID = "lblBillAmountDrCR" runat = "server" Text = '<%#convertToDrCr(Eval("BillAmount"))%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate >
                            <asp:Label ID = "lblTotalBillAmountDrCR" runat = "server" style="font-weight :bold" ></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle Width="18%" />
                         
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Amount" Visible="False" >
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemTemplate >
                            <asp:Label ID = "lblBillAmount" runat = "server" Text = '<%#Eval("BillAmount")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate >
                            <asp:Label ID = "lblTotalBillAmount" runat = "server" style="font-weight :bold" ></asp:Label>
                        </FooterTemplate>
                        <HeaderStyle Width="20%" />
                         
                    </asp:TemplateColumn>
                    
                     <asp:TemplateColumn HeaderText="Pending Amount" >
                        <HeaderStyle HorizontalAlign = "Right" Width="18%" />
                        <ItemStyle HorizontalAlign="Right" />
                         <FooterStyle HorizontalAlign="Right" />
                        <ItemTemplate >
                            <asp:Label ID = "lblPendingAmountDrCR" runat = "server" Text = '<%#convertToDrCr(Eval("PendingAmount"))%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate >
                            <asp:Label ID = "lblTotalPendingAmountDrCR" runat = "server" style="font-weight :bold" ></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    
                     <asp:TemplateColumn HeaderText="Pending Amount" Visible="False" >
                        <HeaderStyle HorizontalAlign = "Right" />
                        <ItemStyle HorizontalAlign="Right" />
                         <FooterStyle HorizontalAlign="Right" />
                        <ItemTemplate >
                            <asp:Label ID = "lblPendingAmount" runat = "server" Text = '<%#Eval("PendingAmount")%>'></asp:Label>
                        </ItemTemplate>
                        <FooterTemplate >
                            <asp:Label ID = "lblTotalPendingAmount" runat = "server" style="font-weight :bold" ></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText = "Adjust Amount" >
                        <HeaderStyle HorizontalAlign = "Right" />
                        <FooterStyle HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate >
                            <asp:TextBox id = "txtAdjustedAmount" runat = "server" CssClass ="TEXTBOX" style="width:80%" onkeypress = "return Only_Numbers(this,event)" onchange="OnTxtChange()" Width="67%"></asp:TextBox>
                            <asp:Label ID="lbl_DrCR" runat="server" CssClass="LABEL" ></asp:Label>&nbsp;
                        </ItemTemplate>
                        
                    </asp:TemplateColumn>
                    
                     <asp:TemplateColumn HeaderText = "voucherId">
                        <ItemTemplate  >
                            <asp:Label ID = "lbl_Voucher_ID" runat = "server" Text = '<%#Eval("Voucher_Id")%>' CssClass = "HIDEGRIDCOL"  />
                        </ItemTemplate>
                         <ItemStyle CssClass="HIDEGRIDCOL" />
                         <HeaderStyle CssClass="HIDEGRIDCOL" />
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText = "LedgerID">
                    
                        <ItemTemplate  >
                            <asp:Label ID = "lbl_LedgerID" runat = "server" Text = '<%#Eval("Ledger_ID")%>' CssClass = "HIDEGRIDCOL"  />
                        </ItemTemplate>
                        <ItemStyle CssClass="HIDEGRIDCOL" />
                        <HeaderStyle CssClass="HIDEGRIDCOL" />
                    </asp:TemplateColumn>
                    
                     <asp:TemplateColumn HeaderText = "SrNo"  >
                        <HeaderStyle CssClass = "HIDEGRIDCOL" />
                        <ItemStyle CssClass = "HIDEGRIDCOL" />
                        <ItemTemplate  >
                            <asp:Label ID = "lbl_AdjSrNo" runat = "server" Text = '<%#Eval("Details_Id")%>' CssClass = "HIDEGRIDCOL"    />
                        </ItemTemplate>
                    </asp:TemplateColumn>                    
                    
                    
                </Columns>
            </asp:DataGrid>
            </ContentTemplate>
            <Triggers >
                <asp:AsyncPostBackTrigger ControlID = "dg_Adjusted" />
            </Triggers>
            </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            </fieldset>
        </td>

    </tr>
    <tr>
        <td style="height: 26px; text-align: center" colspan="2" >
            <asp:UpdatePanel ID = "up_approve" runat = "server" >
            <ContentTemplate >
            <asp:Button ID="btn_Approve" runat="server" Text="Approve" CssClass = "BUTTON" OnClick="btn_Approve_Click" />
               <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window" OnClick="btn_null_session_Click" />
            </ContentTemplate>
            <Triggers >
                <asp:AsyncPostBackTrigger ControlID = "dg_OnAccountAdjustment" />
            </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="2" >
            <asp:UpdatePanel ID = "up_lblError" runat = "server" >
            <ContentTemplate >
            <asp:Label ID="lbl_Error" runat="server" ForeColor="Red" Font-Bold="True"></asp:Label>
            <asp:HiddenField ID="hdn_unAdjAmount" runat="server" />
            </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>

