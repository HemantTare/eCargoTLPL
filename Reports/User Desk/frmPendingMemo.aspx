<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmPendingMemo.aspx.cs" Inherits="Reports_User_Desk_frmPendingMemo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pending Invoice/Trip Memo Against Loading Plan</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:ScriptManager ID="scm_Pendingmemo" runat="server"></asp:ScriptManager>
    <table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Pending Invoice/Trip Memo Against Loading Plan"></asp:Label>
        </td>
      </tr>
      <tr>
        <td style="width: 100%">
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
        </td>
    </tr>
      <tr>
        <td style="width: 100%">
          <asp:UpdatePanel ID="Upd_Pnl_pendingmemo" UpdateMode="Conditional" runat="server">
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
              <ContentTemplate>
                <div class="DIV" style="height: 250px;width:98%;">
                  <asp:DataGrid ID="dg_Grid" runat="server" CssClass="GRID" AutoGenerateColumns="false" AllowSorting="True"
                      Style="border-top-style: none;" Width="97%" OnItemDataBound="dg_Grid_ItemDataBound">
                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <Columns>
                      <asp:TemplateColumn HeaderText="Vehicle No" HeaderStyle-Width="100px"  HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true">
                        <ItemTemplate>
                            <asp:TextBox ID="txtVehicleNo" Text='<%# DataBinder.Eval(Container.DataItem, "Vehicle_No") %>'
                                runat="server" CssClass="TEXTBOXNOS" BackColor="Transparent" BorderStyle="None"
                                BorderColor="Transparent" Style="text-align: left" Font-Size="11px" Font-Names="Verdana"
                                ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle/>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="To Branch" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTo_Branch" Text='<%# DataBinder.Eval(Container.DataItem, "To_Branch") %>'
                                runat="server" CssClass="TEXTBOXNOS" BackColor="Transparent" BorderStyle="None"
                                BorderColor="Transparent" Style="text-align: left" Font-Size="11px" Font-Names="Verdana"
                                ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle/>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Invoice No" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true">
                        <ItemTemplate>
                            <asp:TextBox ID="txtInvoiceNo" Text='<%# DataBinder.Eval(Container.DataItem, "InvoiceNo") %>'
                                runat="server" CssClass="TEXTBOXNOS" BackColor="Transparent" BorderStyle="None"
                                BorderColor="Transparent" Style="text-align: left" Font-Size="11px" Font-Names="Verdana"
                                ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle/>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Trip Memo No" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="true">
                        <ItemTemplate>
                            <asp:TextBox ID="txtTripNo" Text='<%# DataBinder.Eval(Container.DataItem, "TripMemoNo") %>'
                                runat="server" CssClass="TEXTBOXNOS" BackColor="Transparent" BorderStyle="None"
                                BorderColor="Transparent" Style="text-align: left" Font-Size="11px" Font-Names="Verdana"
                                ReadOnly="True"></asp:TextBox>
                        </ItemTemplate>
                        <ItemStyle/>
                    </asp:TemplateColumn>                    
                      </Columns>
                  </asp:DataGrid>
               </div>
              </ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>
  </table>
    </div>
    </form>
</body>
</html>
