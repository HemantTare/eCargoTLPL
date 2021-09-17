<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVoucherBillByBill.aspx.cs" Inherits="Finance_VoucherView_FrmVoucherBillByBill" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Voucher Bill By Bill</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    <table class="TABLE" style="width:100%">
            <tr>
                <td class = "TDGRADIENT" colspan="6">
                    <asp:Label ID = "lbl_heading" runat = "server" Text = "BILL-WISE DETAILS" CssClass="HEADINGLABEL"></asp:Label>
                </td>
            </tr>
            <tr>
            <td style="width:20%" class="TD1">
               Bill-Wise Details For :
            </td>
                <td colspan="5" style="width:30%">
                 <asp:Label ID="lbl_LedgerName" runat="server" Font-Bold="true"></asp:Label></td>
            </tr>
            
            
            <tr>
            <td style="width:20%" class="TD1">
               Upto :
            </td>
                <td colspan="5" style="width:30%">
                 <asp:Label ID="lbl_Upto" runat="server" Font-Bold="true"></asp:Label></td>
            </tr>
             
            <tr>
               <td colspan="6">
                 <fieldset id="fld_Grid">
                 <legend>Bill Details :</legend>
                 <table cellpadding="3" cellspacing="3" border="0" width="100%">
                  <tr>
                   <td>
                    <asp:DataGrid ID = "dg_BillByBill" runat = "server"
                        CssClass = "Grid"
                        AutoGenerateColumns="False" >
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <PagerStyle Mode="NumericPages" CssClass="GRIDVIEWPAGERCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="Ref Type">
                                <HeaderStyle width="15%" />
                                
                                <ItemTemplate >
                                    <%#Eval("Ref_Type") %>
                                </ItemTemplate>
                    
                            </asp:TemplateColumn>
  
                            <asp:TemplateColumn HeaderText="Name">
                                <HeaderStyle Width="20%"  />
                                
                                <ItemTemplate >
                                    <%#Eval("Ref_No")%>
                                </ItemTemplate>
                                                           
                                
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Credit Days">
                                <HeaderStyle width="10%" HorizontalAlign = "Right" />
                                <ItemStyle HorizontalAlign = "Right" />
                                
                                <ItemTemplate >
                                    <%#Eval("Credit_Days") %>
                                </ItemTemplate>
                                             
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Amount">
                                <HeaderStyle width="15%" HorizontalAlign = "Right" />
                                <ItemStyle HorizontalAlign = "Right" />
                                <ItemTemplate>
                                    <%#Math.Abs(Convert.ToDecimal(Eval("Amount")))%>
                                </ItemTemplate>
                                                  
                                
                            </asp:TemplateColumn>
                            
                            <asp:TemplateColumn HeaderText="TDS Ledger">
                                <HeaderStyle width="30%" />
                                
                                <ItemTemplate >
                                    <%#Eval("TDS_Ledger_Name") %>
                                </ItemTemplate>
                  
                                
                            </asp:TemplateColumn>
                            
                            
                            <asp:TemplateColumn HeaderText="Cr/Dr">
                                <HeaderStyle width="10%" />
                                
                                <ItemTemplate >
                                    <%#convertToDrCr(Eval("Amount"))%>
                                </ItemTemplate>
                                                          
                            </asp:TemplateColumn>
                 
                            
                        </Columns>
                    </asp:DataGrid>
                     </td>
                 </tr>
             </table>
                    </fieldset>                       
                </td>
            </tr>
            <tr>
                <td style="height: 15px; text-align: center" colspan="5" >
                    </td>
            </tr>
            <tr>
                <td colspan = "5">
                    &nbsp;</td>
            </tr>            
            
        </table>
  
    </div>
    </form>
</body>
</html>
