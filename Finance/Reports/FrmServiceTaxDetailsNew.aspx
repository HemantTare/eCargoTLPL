<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmServiceTaxDetailsNew.aspx.cs"
  Inherits="Finance_Reports_FrmServiceTaxDetailsNew" %>

<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
  TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
  TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
  TagPrefix="uc4" %>

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
        function changeScreenSize() {
            window.resizeTo(1800, 1300);
        }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <title>Service tax details</title>
  <link href="~/CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
  <form id="form1" runat="server">
    <asp:ScriptManager ID="scm_BookingRegister" runat="server">
    </asp:ScriptManager>
    <table runat="server" id="Table1" class="TABLE" onclick="rr()">
      <tr>
        <td class="TDGRADIENT" style="width: 100%">
          <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Service tax details"></asp:Label>
        </td>
      </tr>
    </table>
    <table runat="server" id="tbl_input_screen" class="TABLE">
      <tr>
        <td style="width: 50%">
          <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
        </td>
      </tr>
      <tr>
        <td style="width: 50%">
          <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
        </td>
      </tr>
    </table>
    <table class="TABLE" id="TABLE2">
      <tr>
        <td style="width: 10%">
          <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" />
        </td>
        <td style="width: 10%">
          <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
        </td>
        <td style="width: 10%">
          <a href="javascript:input_screen_action('view');">View Input</a>
        </td>
        <td style="width: 10%">
          <a href="javascript:input_screen_action('hide');">Hide Input</a>
        </td>
        <td style="width: 50%">
          <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
        </td>
      </tr>
    </table>
    <table>
      <tr>
        <td>
          <asp:UpdatePanel ID="Upd_Pnl_billingdetails" UpdateMode="Conditional" runat="server">
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="GridBillingDetails" />
            </Triggers>
            <ContentTemplate>
              <asp:Panel runat="server" ID="pnlbillingdetails" GroupingText="Billing Details">
                <asp:DataGrid ID="GridBillingDetails" runat="server" ShowFooter="true" AllowPaging="True"
                  CssClass="GRID" PagerStyle-Mode="NumericPages" AllowSorting="True" AutoGenerateColumns="false"
                  OnPageIndexChanged="GridBillingDetails_PageIndexChanged" OnItemDataBound="GridBillingDetails_ItemDataBound"
                  PagerStyle-HorizontalAlign="Left" PageSize="9">
                  <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                  <HeaderStyle CssClass="GRIDHEADERCSS" />
                  <FooterStyle CssClass="GRIDFOOTERCSS" />
                  <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                  <Columns>
                    <asp:BoundColumn DataField="Branch" HeaderText="Branch"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Ref No" HeaderText="Ref No"></asp:BoundColumn>
                    <asp:BoundColumn DataField="gc_caption Date" HeaderText="gc_caption Date"></asp:BoundColumn>
                    <asp:BoundColumn DataField="gc_caption No" HeaderText="gc_caption #"></asp:BoundColumn>
                    <asp:BoundColumn DataField="party_name" HeaderText="Party Name"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Pay_type" HeaderText="Payment Type"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Booking_Type" HeaderText="Booking Type"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Total Freight">
                      <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Total_Freight")%>
                      </ItemTemplate>
                      <FooterTemplate>
                        <asp:Label ID="lbl_TotalFreight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                      </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tax Abate">
                      <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Tax_Abate")%>
                      </ItemTemplate>
                      <FooterTemplate>
                        <asp:Label ID="lbl_TotalTaxAbate" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                      </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Amt Taxable">
                      <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Amt_Taxable")%>
                      </ItemTemplate>
                      <FooterTemplate>
                        <asp:Label ID="lbl_TotalAmtTaxable" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                      </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Service Tax">
                      <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Service_Tax")%>
                      </ItemTemplate>
                      <FooterTemplate>
                        <asp:Label ID="lbl_TotalServiceTax" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                      </FooterTemplate>
                    </asp:TemplateColumn>
                  </Columns>
                </asp:DataGrid>
              </asp:Panel>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>
      <tr>
        <td>
          <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel2" runat="server" />
        </td>
      </tr>
      <tr>
        <td>
          <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
            <Triggers>
              <asp:AsyncPostBackTrigger ControlID="GridBillingDetails" />
            </Triggers>
            <ContentTemplate>
              <asp:Panel runat="server" ID="pnlpaiddetails" GroupingText="Paid Details">
                <asp:DataGrid ID="GridPaidetails" runat="server" ShowFooter="true" AllowPaging="True"
                  CssClass="GRID" PagerStyle-Mode="NumericPages" AllowSorting="True" AutoGenerateColumns="false"
                  PagerStyle-HorizontalAlign="Left" PageSize="9" OnItemDataBound="GridPaidetails_ItemDataBound"
                  OnPageIndexChanged="GridPaidetails_PageIndexChanged">
                  <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                  <HeaderStyle CssClass="GRIDHEADERCSS" />
                  <FooterStyle CssClass="GRIDFOOTERCSS" />
                  <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                  <Columns>
                    <asp:BoundColumn DataField="Branch" HeaderText="Branch"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Ref No" HeaderText="Ref No"></asp:BoundColumn>
                    <asp:BoundColumn DataField="gc_caption Date" HeaderText="gc_caption Date"></asp:BoundColumn>
                    <asp:BoundColumn DataField="gc_caption No" HeaderText="gc_caption #"></asp:BoundColumn>
                    <asp:BoundColumn DataField="party_name" HeaderText="Party Name"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Pay_type" HeaderText="Payment Type"></asp:BoundColumn>
                    <asp:BoundColumn DataField="Booking_Type" HeaderText="Booking Type"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Total Freight">
                      <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Total_Freight")%>
                      </ItemTemplate>
                      <FooterTemplate>
                        <asp:Label ID="lbl_TotalFreight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                      </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Tax Abate">
                      <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Tax_Abate")%>
                      </ItemTemplate>
                      <FooterTemplate>
                        <asp:Label ID="lbl_TotalTaxAbate" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                      </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Amt Taxable">
                      <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Amt_Taxable")%>
                      </ItemTemplate>
                      <FooterTemplate>
                        <asp:Label ID="lbl_TotalAmtTaxable" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                      </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Service Tax">
                      <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Service_Tax")%>
                      </ItemTemplate>
                      <FooterTemplate>
                        <asp:Label ID="lbl_TotalServiceTax" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                      </FooterTemplate>
                    </asp:TemplateColumn>
                  </Columns>
                </asp:DataGrid>
              </asp:Panel>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>
    </table>
  </form>
</body>
</html>

