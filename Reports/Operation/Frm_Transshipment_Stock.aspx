<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Transshipment_Stock.aspx.cs" Inherits="Reports_Operation_Frm_Transshipment_Stock" %>
<%@ Register Src="../../CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc3" %>
<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc1" %>
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

<%--Author : Harshal Sapre
    Date   : 14-01-09--%>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Delivery Stock List View</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_CrossingStockList" runat="server"></asp:ScriptManager>

        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Transshipment Stock"></asp:Label>
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
                    <uc6:WucFilter ID="WucFilter1" runat="Server" />
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                          <td style="width: 10%; " class="TD1">
                          <asp:Label ID="lbl_division" runat="server" Text="Label"></asp:Label></td>
                            <td style="width: 24%">
                                <uc5:WucDivisions ID="WucDivisions1" runat="server" />
                            </td>
                            <td class="TD1" style="width: 9%">
                                As On Date:</td>
                            <td style="width: 24%">
                                <uc3:WucDatePicker ID="WucDatePicker1" runat="server" />
                            </td>
                            <td style="width: 9%">
                            </td>
                            <td style="width: 24%">
                            </td>
                        </tr>
                    </table>
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
                    <a href="javascript:input_screen_action('view');">View Input</a>
                </td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('hide');">Hide Input</a>
                </td>
                <td style="width: 50%">
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label>
                  <%--  <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>--%>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                  <asp:UpdatePanel ID="Upd_Pnl_CrossingStockList" UpdateMode="Conditional" runat="server">
                  <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                  </Triggers>
                  <ContentTemplate>
                      <%-- <asp:Panel ID="pnl_CrossingStockList" runat="server" Height="500px" ScrollBars="Auto">--%> 
                            <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True" 
                                AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged" OnItemDataBound="dg_Grid_ItemDataBound" AllowCustomPaging="True">
                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="CN No">
                                    <ItemTemplate>                    
                                    <%# DataBinder.Eval(Container.DataItem, "CN No")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Total" Text="Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                       
                                    </asp:TemplateColumn>
                                <asp:BoundColumn DataField="CN Date" HeaderText="CN Date"></asp:BoundColumn> 
                              
                                <asp:BoundColumn DataField="AUS Date" HeaderText="AUS Date"></asp:BoundColumn> 
                              
                                <asp:TemplateColumn HeaderText="No of days">
                                    <ItemTemplate>                    
                                    <%# DataBinder.Eval(Container.DataItem, "No of days")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_No_of_days" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Total Packages" >
                                    <ItemTemplate>                    
                                    <%# DataBinder.Eval(Container.DataItem, "Total Articles")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Total_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Avaliable Packages">
                                    <ItemTemplate>                    
                                    <%# DataBinder.Eval(Container.DataItem, "Total Received Articles")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Total_Received_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                             
                                <asp:BoundColumn DataField="Remarks" HeaderText="Remarks"></asp:BoundColumn> 
                                <asp:BoundColumn DataField="Consignee Name" HeaderText="Consignee Name"></asp:BoundColumn> 
                                <asp:BoundColumn DataField="dest" HeaderText="Destination"></asp:BoundColumn> 
                                <asp:TemplateColumn HeaderText="Amount">
                                    <ItemTemplate>                    
                                    <%# DataBinder.Eval(Container.DataItem, "Total GC Amount")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_Total_GC_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Reason" HeaderText="Reason"></asp:BoundColumn> 
                                </Columns>

                            </asp:DataGrid>
                      <%--  </asp:Panel>--%>
                  </ContentTemplate>
                  </asp:UpdatePanel>

                </td>
            </tr>
        </table>
    </form>
</body>
</html>
