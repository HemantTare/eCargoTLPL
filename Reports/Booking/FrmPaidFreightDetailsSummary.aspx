<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPaidFreightDetailsSummary.aspx.cs" Inherits="Reports_Booking_FrmPaidFreightDetailsSummary" %>

<%@ Register Src="../../CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters" TagPrefix="uc3" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/WucFilter.ascx" TagName="WucFilter" TagPrefix="uc6" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
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
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Paid Freight Details / Summary</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_Paid_Freight" runat="server"></asp:ScriptManager>

    <div>
        <table class="TABLE" style="width: 100%">
            <tr>
                <td class="TDGRADIENT" colspan="6">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Paid Freight Details / Summary"></asp:Label></td>
            </tr>
        </table>
    
    </div>
        <table id = "tbl_input_screen" style="width: 100%" class = "TABLE">
            <tr>
                <td colspan="6">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
          
            <tr>
                <td colspan="6">
                    <uc3:Wuc_GC_Parameters ID="Wuc_GC_Parameters1" runat="server" />
                </td>
            </tr>
              <tr>
                <td colspan="6">
                    <uc6:WucFilter ID="WucFilter1" runat="Server" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%" class="TD1">
                 <asp:label ID="lbl_division" runat="server" CssClass="LABEL" Text="label"/></td>
                <td style="width: 24%">
                    <uc5:WucDivisions ID="WucDivisions1" runat="server" />
                </td>
                <td style="width: 21%">
                <asp:RadioButtonList ID="rbl_Detailed_Summary" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="0" Selected = "true">Detailed</asp:ListItem>
                        <asp:ListItem Value="1">Summary</asp:ListItem>
                    </asp:RadioButtonList></td>
                <td style="width: 15%">
                </td>
                <td style="width: 13%">
                </td>
                <td style="width: 20%">
                </td>
            </tr>
        </table>
        <table class = "TABLE" style="width: 100%">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_View" runat="server" CssClass="BUTTON" Text="View" OnClick="btn_View_Click" /></td>
                <td style="width: 10%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 11%">
                    <a href="javascript:input_screen_action('view');">View Input</a></td>
                <td style="width: 11%">
                    <a href="javascript:input_screen_action('hide');">Hide Input</a></td>
                <td colspan="2">
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
         <table class="TABLE">
          <tr>
                <td>
            <div class="DIV" style="height: 510px; width:976px;">
        <table>
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_PaidFreightDetails" UpdateMode="Conditional" runat="server">
                    <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="dg_Grid_Details" />
                    </Triggers>
                    <ContentTemplate>
                        <%--<asp:Panel ID="pnl_PaidFreightDetails" runat="server" Height="500px" ScrollBars="Auto">--%>
                            <asp:DataGrid ID="dg_Grid_Details" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" AllowCustomPaging="true"
                                ShowFooter="True" CssClass="GRID" OnItemDataBound="dg_Grid_Details_ItemDataBound" OnPageIndexChanged="dg_Grid_Details_PageIndexChanged">
                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                <Columns>
                                    <asp:BoundColumn DataField="Booking Region" HeaderText="Booking Region"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Booking Area" HeaderText="Booking Area"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Booking Branch" HeaderText="Booking Branch"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="gc_caption No" HeaderText="gc_caption No"></asp:BoundColumn>                                                              
                                    <asp:BoundColumn DataField="gc_caption Date" HeaderText="gc_caption Date"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Conr Name" HeaderText="Conr Name"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Bkg Type" HeaderText="Bkg Type"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Dly Type" HeaderText="Dly Type"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Pmt Type" HeaderText="Pmt Type"></asp:BoundColumn>
                                    
                                    <asp:TemplateColumn HeaderText="Cheque No">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"Cheque No")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Cheque_No" runat="server" CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Cash Amount">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"Cash Amount")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Cash_Amount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Cheque Amount">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"Cheque Amount")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Cheque_Amount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="gc_caption Amount">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "gc_caption Amount")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_GC_Amount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Basic Amount">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"Basic Amount")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Basic_Amount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Local Charges">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"Local Charges")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Local_Charges" runat="server" CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="FOV">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"FOV")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_FOV" runat="server" CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Hamali Charges">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"Hamali Charges")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Hamali_Charges" runat="server" CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Other Charges">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"Other Charges")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Other_Charges" runat="server" CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Bilti Charges">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"Bilti Charges")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Bilti_Charges" runat="server" CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="DD Charges">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"DD Charges")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_DD_Charges" runat="server" CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Sub Total">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"Sub Total")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Sub_Total" runat="server" CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Service Tax Amount">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"Service Tax Amount")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Service_Tax_Amount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="Tax Payable By" HeaderText="Tax Payable By"></asp:BoundColumn>
                                </Columns>
                                <PagerStyle Mode="NumericPages" CssClass="GRIDPAGERCSS" HorizontalAlign="Left" />
                            </asp:DataGrid>
                        <%--</asp:Panel>--%>
                    </ContentTemplate>
                  </asp:UpdatePanel>
                </td>
            </tr>
             <tr>
                <td>
                <asp:UpdatePanel ID="Upd_Pnl_PaidFreightSummary" UpdateMode="Conditional" runat="server">
                    <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="dg_Grid_Summary" />
                    </Triggers>
                    <ContentTemplate>
                       <%-- <asp:Panel ID="pnl_PaidFreightSummary" runat="server" Height="500px" ScrollBars="Auto">--%>
                       <asp:DataGrid ID="dg_Grid_Summary" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" AllowCustomPaging="true"
                            ShowFooter="True" CssClass="GRID" OnItemDataBound="dg_Grid_Summary_ItemDataBound" OnPageIndexChanged="dg_Grid_Summary_PageIndexChanged">
                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                            <HeaderStyle CssClass="GRIDHEADERCSS" />
                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <Columns>
                                <asp:BoundColumn DataField="Booking Region" HeaderText="Booking Region"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Booking Area" HeaderText="Booking Area"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Booking Branch">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Booking Branch")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_Booking_Branch" runat="server" CssClass="LABEL" Font-Bold="true" />
                                </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                               <asp:TemplateColumn HeaderText="Total gc_caption">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Total gc_caption")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_Total_GC" runat="server" CssClass="LABEL" Font-Bold="true" />
                                </FooterTemplate>
                                   <ItemStyle HorizontalAlign="Right" />
                                   <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Cash Amount">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"Cash Amount")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_Cash_Amount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Cheque Amount">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"Cheque Amount")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_Cheque_Amount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="gc_caption Amount">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "gc_caption Amount")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_GC_Amount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Basic Amount">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"Basic Amount")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_Basic_Amount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Local Charges">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"Local Charges")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_Local_Charges" runat="server" CssClass="LABEL" Font-Bold="true" />
                                </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="FOV">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"FOV")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_FOV" runat="server" CssClass="LABEL" Font-Bold="true" />
                                </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Hamali Charges">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"Hamali Charges")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_Hamali_Charges" runat="server" CssClass="LABEL" Font-Bold="true" />
                                </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Other Charges">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"Other Charges")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_Other_Charges" runat="server" CssClass="LABEL" Font-Bold="true" />
                                </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Bilti Charges">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"Bilti Charges")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_Bilti_Charges" runat="server" CssClass="LABEL" Font-Bold="true" />
                                </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DD Charges">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"DD Charges")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_DD_Charges" runat="server" CssClass="LABEL" Font-Bold="true" />
                                </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Sub Total">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"Sub Total")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_Sub_Total" runat="server" CssClass="LABEL" Font-Bold="true" />
                                </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Service Tax Amount">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"Service Tax Amount")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_Service_Tax_Amount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Mode="NumericPages" CssClass="GRIDPAGERCSS" HorizontalAlign="Left" />
                        </asp:DataGrid>
                       <%--</asp:Panel>--%>
                    </ContentTemplate>
                  </asp:UpdatePanel>
                </td>
            </tr>
        </table>
       </div>
       </td>
            </tr>
        </table>
    </form>
</body>
</html>
