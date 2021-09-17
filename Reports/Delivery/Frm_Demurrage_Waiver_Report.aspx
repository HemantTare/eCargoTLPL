<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Demurrage_Waiver_Report.aspx.cs" Inherits="Reports_Delivery_Frm_Demurrage_Waiver_Report" %>
<%@ Register Src="../../CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc1" %>

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

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Delivery Stock List View</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server"></asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Demurrage Waiver Report"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                   <uc2:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
             <tr>
            <td style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 10%; " class="TD1">
                           <asp:label ID="lbl_division" runat="server" CssClass="LABEL" Text="label"/></td>
                        <td style="width: 24%;"><uc5:WucDivisions ID="WucDivisions1" runat="server" /></td>
                        <td style="width: 9%;" class="TD1"></td>
                        <td style="width: 24%;"></td>
                       <td style="width: 9%;" class="TD1"></td>
                        <td style="width: 24%;"></td>
                    </tr>
                </table>
            </td>
        </tr>
                        <tr>
                               <td style="width:100%">
            <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
        </td>
                     
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_view" runat="server" CssClass="BUTTON" OnClick="btn_view_Click"
                        Text="View" />
                </td>
                <td style="width: 10%">
                    <uc1:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('view');">View Inupt</a>
                </td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('hide');">Hide Inupt</a>
                </td>
                <td style="width: 50%">
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label>
                               </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                 <asp:UpdatePanel ID="Upd_Pnl_Demurrage" UpdateMode="Conditional" runat="server">
                  <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                  </Triggers>
                  <ContentTemplate>
                    <asp:Panel ID="pnl_Demurrage" runat="server" Height="500px" ScrollBars="Auto">
                        <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="true" AllowPaging="True" CssClass="GRID" AllowSorting="True" AllowCustomPaging="true"
                            AutoGenerateColumns="false" OnPageIndexChanged="dg_Grid_PageIndexChanged" OnItemDataBound="dg_Grid_ItemDataBound">
                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                            <HeaderStyle CssClass="GRIDHEADERCSS" />
                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                            <Columns>
                               <asp:TemplateColumn HeaderText="gc_caption No">
                                <ItemTemplate>                    
                                <%# DataBinder.Eval(Container.DataItem, "gc_caption No")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_Total" Text="Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                                 </asp:TemplateColumn>
                          
                            <asp:BoundColumn DataField="Unloaded Date" HeaderText="Unloaded On"></asp:BoundColumn> 
                            <asp:BoundColumn DataField="Delivery Date" HeaderText="Delivered On"></asp:BoundColumn> 
                          
                          
                            <asp:TemplateColumn HeaderText="No of Days(Transit)" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                <ItemTemplate>                    
                                <%# DataBinder.Eval(Container.DataItem, "No of Days")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_No_of_Days"  runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Demurrage Days" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" >
                                <ItemTemplate>                    
                                <%# DataBinder.Eval(Container.DataItem, "Demurrage Days")%>
                                </ItemTemplate>
                                </asp:TemplateColumn>
                             <asp:TemplateColumn HeaderText="Amount Waived" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" >
                                <ItemTemplate>                    
                                <%# DataBinder.Eval(Container.DataItem, "Amount Waived")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_Demurrage_Rate_Kg_Per_Day" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Waived By">
                                <ItemTemplate>                    
                                <%# DataBinder.Eval(Container.DataItem, "Waived By")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbl_Amount_Waived_By" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
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

