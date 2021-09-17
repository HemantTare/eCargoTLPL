<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPDSTempoFreightCalculator.aspx.cs" Inherits="Operations_Delivery_FrmPDSTempoFreightCalculator" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
  <title>Tempo Freight Calculator</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table class="TABLE" width="100%" border="0">
            <tr>
              <td class="TDGRADIENT" colspan="6">
                <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="PDS Tempo Freight Calculator"></asp:Label>
              </td>
            </tr>
            <tr>
              <td colspan="6">
                &nbsp;</td>
            </tr>            
            <tr>
              <td colspan="6" style="width:100%">
                <asp:DataGrid ID="dgGrid" Width="100%" AutoGenerateColumns="false" CssClass="Grid" runat="server">
                    <HeaderStyle Height="15px" Font-Size="11px" Font-Names="Verdana" Font-Bold="True"
                        HorizontalAlign="center" ForeColor="Black" BorderStyle="Solid" BorderColor="#9495A2"
                        BorderWidth="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader">
                    </HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="LR No" ItemStyle-HorizontalAlign="left" HeaderStyle-Width="150px" >
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "LRNo")%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="left" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Actual Wt" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="right" >
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ActualWt")%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="right" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Delivery Area" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="left" >
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "DeliveryArea")%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="left" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Tempo Frt" HeaderStyle-Width="100px" ItemStyle-HorizontalAlign="right" >
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "TempoFrt")%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="right" />
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
              </td>
            </tr>
            <tr>
              <td colspan="6">&nbsp;</td>
            </tr> 
            <tr>
              <td>&nbsp;</td>
              <td style="width:20%" align="right">
                <asp:Label ID="lbltot" runat="server" Font-Bold="true">Total: </asp:Label>
              </td>
              <td style="width:20%" align="right" >
                <asp:Label ID="txtTotalWt" runat="server" Font-Bold="true">0</asp:Label>
              </td>
              <td align="right" style="width:30%">
                <asp:Label ID="Label1" runat="server" Font-Bold="true">Total: </asp:Label>
              </td>
              <td>&nbsp;</td>
              <td style="width:20%" align="right" >
                <asp:Label ID="txtTotalFrt" runat="server" Font-Bold="true">0.00</asp:Label>
              </td>
            </tr>
             <tr>
              <td colspan="6">&nbsp;</td>
            </tr>          
          </table>
    </div>
    </form>
</body>
</html>
