<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBookingStockListView_Reach.aspx.cs" Inherits="Reports_Booking_FrmBookingStockListView" %>

<%@ Register Src="../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc3" %>
<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../../Javascript/CommonReports.js"></script>

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

//function get_button_nullsession_clientid()
//{
//btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
//}

</script>

<%--Author : Harshal Sapre
    Date   : 14-01-09--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Booking Stock List View</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scm_BookingStockList" runat="server"></asp:ScriptManager>

        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Booking Stock List"></asp:Label>
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
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td class="TD1" colspan="6">
                                <uc4:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 10%" class="TD1">
                            <asp:label ID="lbl_division" runat="server" CssClass="LABEL" Text="label"/></td>
                            <td style="width: 24%"><uc5:WucDivisions ID="WucDivisions1" runat="server" /></td>
                            <td class="TD1" style="width: 9%">As On Date:</td>
                            <td style="width: 24%"><uc3:WucDatePicker ID="WucDatePicker1" runat="server" /></td>
                            <td style="width: 9%"></td>
                            <td style="width: 24%"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_view" runat="server" CssClass="BUTTON" OnClick="btn_view_Click" Text="View" />
                </td>
                <td style="width: 10%">
                    <uc1:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('view');">View Input</a>
                </td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('hide');">Hide Input</a>
                </td>
                <td style="width: 50%">
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window" OnClick="btn_null_session_Click" />
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                  <asp:UpdatePanel ID="Upd_Pnl_BookingStockList" UpdateMode="Conditional" runat="server">
                  <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                  </Triggers>
                  <ContentTemplate>
                        <asp:Panel ID="pnl_BookingStockList" runat="server" Height="500px" ScrollBars="Auto">
                            <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="true" AllowPaging="True" CssClass="GRID" AllowSorting="True" AllowCustomPaging="true"
                                AutoGenerateColumns="false" OnPageIndexChanged="dg_Grid_PageIndexChanged" OnItemDataBound="dg_Grid_ItemDataBound">
                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                <Columns>
                                
                               
                                <asp:TemplateColumn HeaderText="gc_caption Date">
                                    <ItemTemplate>                    
                                    <%# DataBinder.Eval(Container.DataItem, "gc_caption Date")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Total" Text="Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                             
                               <asp:TemplateColumn HeaderText="gc_caption No">
                                    <ItemTemplate>                    
                                    <%# DataBinder.Eval(Container.DataItem, "gc_caption No")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Total_Gc" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Bkg Branch" HeaderText="Bkg Branch"></asp:BoundColumn> 
                                <asp:BoundColumn DataField="Dly Branch" HeaderText="Dly Branch"></asp:BoundColumn> 
                                <asp:BoundColumn DataField="Cnee Name" HeaderText="Cnee Name"></asp:BoundColumn> 
                                <asp:BoundColumn DataField="Cnr Name" HeaderText="Cnr Name"></asp:BoundColumn> 
                              
                                <asp:TemplateColumn HeaderText="Pay Mode">
                                    <ItemTemplate>                    
                                    <%# DataBinder.Eval(Container.DataItem, "Pay Mode")%>
                                    </ItemTemplate>
                                  
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Charged Weight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" >
                                    <ItemTemplate>                    
                                    <%# DataBinder.Eval(Container.DataItem, "Charged Weight")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_ChargedWeight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Actual Weight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>                    
                                    <%# DataBinder.Eval(Container.DataItem, "Actual Weight")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_ActualWeight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Articles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>                    
                                    <%# DataBinder.Eval(Container.DataItem, "Articles")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Basic Freight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>                    
                                    <%# DataBinder.Eval(Container.DataItem, "Basic Freight")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_BasicFreight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Total Freight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>                    
                                    <%# DataBinder.Eval(Container.DataItem, "Total Freight")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_TotalFreight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Invoice Value" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                                    <ItemTemplate>                    
                                    <%# DataBinder.Eval(Container.DataItem, "Invoice Value")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_InvoiceValue" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
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
