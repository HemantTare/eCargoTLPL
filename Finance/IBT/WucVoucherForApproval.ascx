<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVoucherForApproval.ascx.cs" Inherits="Finance_IBT_WucVoucherForApproval" %>
<script src="../../Javascript/Common.js" type="text/javascript"></script>
<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="4">
            &nbsp;<asp:Label ID="lbl_heading_name" runat="server" CssClass="HEADINGLABEL" Font-Bold="True"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="4">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
                        <asp:Label ID="lbl_Voucher_type" runat="server" CssClass="LABEL" Font-Bold="True"
                            ForeColor="Red" Text="VOUCHER"></asp:Label>
                        No:</td>
        <td style="width: 25%">
            <asp:Label ID="lbl_Voucher_No" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label></td>
        <td class="TD1" style="width: 25%">
                        Voucher Date:</td>
        <td style="width: 25%">
                        <asp:Label ID="lbl_Voucher_Date" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="4">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
                        Ref. No.:</td>
        <td style="width: 25%">
            <asp:Label ID="lbl_Ref_No" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label></td>
        <td style="width: 25%">
        </td>
        <td style="width: 25%">
        </td>
    </tr>
    <tr>
        <td colspan="4">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="4">
        <asp:datagrid id="dg_Voucher" 
            runat="server" 
            AllowPaging="True"
            AllowSorting="True"
            AutoGenerateColumns="False" 
            ShowFooter="True" 
            DataKeyField="Sr_No" 
            CellPadding  = "3"
            CssClass="GRID" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" OnPageIndexChanged="dg_Voucher_PageIndexChanged">
            <PagerStyle Mode="NumericPages"/>
            <HeaderStyle CssClass="GRIDHEADERCSS" />
            <Columns>
                <asp:TemplateColumn  HeaderText="Sr. No" HeaderStyle-Width="10%">
                    <ItemTemplate>
                       <%#(DataBinder.Eval(Container.DataItem, "Sr_No"))%>
                    </ItemTemplate>
                </asp:TemplateColumn>
              
                <asp:TemplateColumn HeaderText="Cr/Dr" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <%#Parse_CrDr(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Credit")))%>
                    </ItemTemplate>
                </asp:TemplateColumn>
                
                <asp:TemplateColumn HeaderText="Particulars" HeaderStyle-Width="50%">
                    <ItemTemplate>
                        <%#(DataBinder.Eval(Container.DataItem, "Ledger_Name"))%>
                    </ItemTemplate>
                </asp:TemplateColumn>
                
                
                <asp:TemplateColumn HeaderText="Debit" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="15%">
                    <ItemTemplate>
                       <%#Parse_Amt(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Debit")))%>  
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" />
                </asp:TemplateColumn>
                
                
                <asp:TemplateColumn HeaderText="Credit" HeaderStyle-HorizontalAlign="Right" HeaderStyle-Width="15%">
                    <ItemTemplate>
                         <%#Parse_Amt(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Credit")))%> 
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" />
                </asp:TemplateColumn>
                
            </Columns>
        </asp:DataGrid></td>
            
    </tr>
    <tr>
        <td colspan="4">
          
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 10%">
                    </td>
                    <td style="width: 50%" class="TD1">
                        <asp:Label ID="Label1" runat="server" CssClass="LABEL" Font-Bold="True" ForeColor="Black">Total : </asp:Label>&nbsp;
                    </td>
                    <td class="TD1" style="width: 15%">
                        <asp:Label ID="lbl_Debit_Total" runat="server" CssClass="TEXTBOXNOS" Font-Bold="True"
                            ForeColor="Black" Width="100px"></asp:Label>&nbsp;</td>
                    <td class="TD1" style="width: 15%">
                        <asp:Label ID="lbl_Credit_Total" runat="server" CssClass="TEXTBOXNOS" Font-Bold="True" ForeColor="Black" Width="100px"></asp:Label>&nbsp;</td>
                </tr>
            </table>
        
        </td>
    </tr>
    <tr>
        <td style="width: 25%">
        </td>
        <td style="width: 25%">
        </td>
        <td style="width: 25%">
        </td>
        <td style="width: 25%">
        </td>
    </tr>
    <tr>
        <td  colspan="4">
        
                  <table cellpadding="0" cellspacing="0"  style="width: 100%" border="0">
                        <tr>
                            <td class="TD1" style="vertical-align: top; width: 10%">
                                            Narration :</td>
                            <td style="width: 90%">
                                            <asp:TextBox ID="txt_Narration" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                                TextMode="MultiLine" Height="60px" ReadOnly="True"></asp:TextBox></td>
                        </tr>
                   </table>
        </td>
    </tr>
    <tr>
        <td colspan="4">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="4">
            <asp:Button ID="btn_Accept" runat="server" CssClass="BUTTON" Text="Acceptable" OnClick="btn_Accept_Click" />
            &nbsp; &nbsp;<asp:Button ID="btn_Unacceptable" runat="server" CssClass="BUTTON" Text="Unacceptable" OnClick="btn_Unacceptable_Click" /></td>
    </tr>
    <tr>
        <td style="width: 25%">
        </td>
        <td style="width: 25%">
        </td>
        <td style="width: 25%">
        </td>
        <td style="width: 25%">
        </td>
    </tr>
</table>
&nbsp; &nbsp;

