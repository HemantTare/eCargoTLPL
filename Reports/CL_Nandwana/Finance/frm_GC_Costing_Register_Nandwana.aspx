<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_GC_Costing_Register_Nandwana.aspx.cs" Inherits="Reports_Finance_frm_GC_Costing_Register_Nandwana" %>
<%@ Register Src="../../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Src="../../../CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters" TagPrefix="uc2" %>
<%@ Register Src="../../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc5" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>

<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript"  src="../../../Javascript/ddlsearch.js"></script>
<script type="text/javascript"  src="../../../Javascript/Common.js"></script>

<script type="text/javascript">
function viewwindow_general(GC_ID)
{
    var Path='../../../Reports/CL_Nandwana/Finance/frm_GC_Costing_View_Nandwana.aspx?&GC_ID=' + GC_ID ;

    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = 950;
    var popH = 650;
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
                
    window.open(Path, 'CustomPopUp_GC_Costing_View', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
    return false;
}

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
<head id="Head1" runat="server">
    <title>GC Costing Register</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
           <asp:ScriptManager ID="scm_Truck_Unloading" runat="server"></asp:ScriptManager>

           <table class = "TABLE">
            <tr>
                <td colspan="6" class = "TDGRADIENT">
                    <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="GC Costing Register"/>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <uc2:Wuc_GC_Parameters ID="Wuc_GC_Parameters1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <uc5:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                </td>
            </tr>
               <tr>
                   <td colspan="6">
                       <table style="width: 100%">
                           <tr>
                               <td colspan="1" style="text-align: right" width="10%">
                                   Party Type:</td>
                               <td colspan="1" style="text-align: left" width="20%">
                                   <asp:DropDownList ID="ddl_Party_Type" runat="server" Width="100%"  OnSelectedIndexChanged="ddl_Party_Type_SelectedIndexChanged" AutoPostBack="True">
                                       <asp:ListItem Value="0">All</asp:ListItem>
                                       <asp:ListItem Value="1">Consignor</asp:ListItem>
                                       <asp:ListItem Value="2">Consignee</asp:ListItem>
                                       <asp:ListItem Value="3">Billing Party</asp:ListItem>
                                   </asp:DropDownList></td>
                               <td colspan="3" style="text-align: left" width="70%">
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                       <ContentTemplate>
                                        <asp:Label ID="lbl_Search" runat="server" Text="Search:"></asp:Label>
                                           <cc1:ddlsearch id="ddl_Party" runat="server" callbackfunction="Raj.EF.CallBackFunction.CallBack.GetSearchPartyType"
                                               iscallback="True"></cc1:ddlsearch>
                                       </ContentTemplate>
                                       <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="ddl_Party_Type"/> 
                                       </Triggers> 
                                   </asp:UpdatePanel>
                               </td>
                           </tr>
                       </table>
                   </td>
               </tr>
        </table>
    
        <table class="TABLE">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_View" runat="server" CssClass="BUTTON" OnClick="btn_view_Click" Text="View" />
                </td>
                <td style="width: 10%">
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click" Text="Close Window" />
                </td>
                <td style="width: 11%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" /></td>
                <td style="width: 11%">
                    <a href="javascript:input_screen_action('view');">View Input</a>
                </td>
                <td style="width: 11%">
                    <a href="javascript:input_screen_action('hide');">Hide Input</a>
                </td>
                <td style="width: 58%">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                       <ContentTemplate>
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label>
                     </ContentTemplate>
                   <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_Party_Type"/> 
                   </Triggers> 
               </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <div class="DIV" style="height: 510px; width: 986px;">
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_GC_Costing" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Panel ID="pnl_GC_Costing" runat="server" >
                                <asp:DataGrid ID="dg_Grid" runat="server"  AllowPaging="true"
                                    ShowFooter="true" AllowSorting="True" AutoGenerateColumns="False" CssClass="GRID" OnItemDataBound="dg_Grid_ItemDataBound"
                                    OnPageIndexChanged="dg_Grid_PageIndexChanged" PagerStyle-HorizontalAlign="Left" PageSize="9">
                                    
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS"  Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="gc_caption No">
                                            <ItemTemplate>
                                                <asp:LinkButton ID = "lnk_GC_No" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container, "DataItem.CN No") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                                          <FooterTemplate>
                                              <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                          </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="7%" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="CN Date" HeaderText="gc_caption Date">
                                            <ItemStyle HorizontalAlign="Center" Width="8%" />
                                            <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                        </asp:BoundColumn>                                   
                                        <asp:BoundColumn DataField="Booking Branch" HeaderText="Bkg Branch">
                                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                                            <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Booking Location" HeaderText="Bkg Location">
                                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                                            <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Delivery Branch" HeaderText="Dly Branch">
                                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                                            <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Delivery Location" HeaderText="Dly Location">
                                            <ItemStyle HorizontalAlign="Left" Width="12%" />
                                            <HeaderStyle HorizontalAlign="Center" Width="12%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Consignor Name" HeaderText="Consignor Name">
                                            <ItemStyle HorizontalAlign="Left" Width="18%" />
                                            <HeaderStyle HorizontalAlign="Center" Width="18%" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Consignee Name" HeaderText="Consignee Name">
                                            <ItemStyle HorizontalAlign="Left" Width="18%" />
                                            <HeaderStyle HorizontalAlign="Center" Width="18%" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Booking Income">
                                          <ItemTemplate>                    
                                          <%# DataBinder.Eval(Container.DataItem, "booking income")%>
                                          </ItemTemplate>
                                          <FooterTemplate>
                                              <asp:Label ID="lbl_booking_Income" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                          </FooterTemplate>
                                      </asp:TemplateColumn>                          
                                        <asp:TemplateColumn HeaderText="Delivery Income">
                                          <ItemTemplate>                    
                                          <%# DataBinder.Eval(Container.DataItem, "Delivery income")%>
                                          </ItemTemplate>
                                          <FooterTemplate>
                                              <asp:Label ID="lbl_Delivery_Income" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                          </FooterTemplate>
                                      </asp:TemplateColumn>
                                       <asp:TemplateColumn HeaderText="Claims non Recoverable A/c">
                                          <ItemTemplate>                    
                                          <%# DataBinder.Eval(Container.DataItem, "total discount amount")%>
                                          </ItemTemplate>
                                          <FooterTemplate>
                                              <asp:Label ID="lbl_total_discount_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                          </FooterTemplate>
                                      </asp:TemplateColumn>
<%--                                        <asp:BoundColumn DataField="Total_Income" HeaderText="Total Income">
                                            <ItemStyle HorizontalAlign="Right" Width="8%" />
                                            <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                        </asp:BoundColumn>--%>
                                       <asp:TemplateColumn HeaderText="Total Income">
                                          <ItemTemplate>                    
                                          <%# DataBinder.Eval(Container.DataItem, "Total Income")%>
                                          </ItemTemplate>
                                          <FooterTemplate>
                                              <asp:Label ID="lbl_Total_Income" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                          </FooterTemplate>
                                      </asp:TemplateColumn>                                       
<%--                                        <asp:BoundColumn DataField="Total_Expense" HeaderText="Total Expense">
                                            <ItemStyle HorizontalAlign="Right" Width="8%" />
                                            <HeaderStyle HorizontalAlign="Center" Width="8%" />
                                        </asp:BoundColumn>--%>
                                         <asp:TemplateColumn HeaderText="Crossing Cost">
                                          <ItemTemplate>                    
                                          <%# DataBinder.Eval(Container.DataItem, "Direct Cost")%>
                                          </ItemTemplate>
                                          <FooterTemplate>
                                              <asp:Label ID="lbl_Crossing_Cost" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                          </FooterTemplate>
                                      </asp:TemplateColumn>   
                                         <asp:TemplateColumn HeaderText="Hamali Cost">
                                          <ItemTemplate>                    
                                          <%# DataBinder.Eval(Container.DataItem, "Hamali Cost")%>
                                          </ItemTemplate>
                                          <FooterTemplate>
                                              <asp:Label ID="lbl_Hamali_Cost" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                          </FooterTemplate>
                                      </asp:TemplateColumn>    
                                      <asp:TemplateColumn HeaderText="Incidental Expense">
                                          <ItemTemplate>                    
                                          <%# DataBinder.Eval(Container.DataItem, "Incidental cost")%>
                                          </ItemTemplate>
                                          <FooterTemplate>
                                              <asp:Label ID="lbl_Incidental_Expense" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                          </FooterTemplate>
                                      </asp:TemplateColumn>  
                                         <asp:TemplateColumn HeaderText="Actual Expenses Of Local Collection">
                                          <ItemTemplate>                    
                                          <%# DataBinder.Eval(Container.DataItem, "Local Cartage Cost")%>
                                          </ItemTemplate>
                                          <FooterTemplate>
                                              <asp:Label ID="lbl_Cartage_Cost" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                          </FooterTemplate>
                                      </asp:TemplateColumn>
                                       <asp:TemplateColumn HeaderText="Actual Door Delivery Expenses">
                                          <ItemTemplate>                    
                                          <%# DataBinder.Eval(Container.DataItem, "door delivery expense")%>
                                          </ItemTemplate>
                                          <FooterTemplate>
                                              <asp:Label ID="lbl_door_delivery_Cost" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                          </FooterTemplate>
                                      </asp:TemplateColumn>
                                       <asp:TemplateColumn HeaderText="BTH Other charges">
                                          <ItemTemplate>                    
                                          <%# DataBinder.Eval(Container.DataItem, "BTH Other charges")%>
                                          </ItemTemplate>
                                          <FooterTemplate>
                                              <asp:Label ID="lbl_BTH_Other_Charges" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                          </FooterTemplate>
                                      </asp:TemplateColumn>
                                       <asp:TemplateColumn HeaderText="Total Expense">
                                          <ItemTemplate>                    
                                          <%# DataBinder.Eval(Container.DataItem, "Total Expense")%>
                                          </ItemTemplate>
                                          <FooterTemplate>
                                              <asp:Label ID="lbl_Total_Expense" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                          </FooterTemplate>
                                      </asp:TemplateColumn>                                        
<%--                                        <asp:BoundColumn DataField="Profit_Loss" HeaderText="Profit / Loss">
                                            <ItemStyle HorizontalAlign="Right" Width="9%" />
                                            <HeaderStyle HorizontalAlign="Center" Width="9%" />
                                        </asp:BoundColumn>--%>
                                       <asp:TemplateColumn HeaderText="Profit/Loss">
                                          <ItemTemplate>                    
                                          <%# DataBinder.Eval(Container.DataItem, "Profit Loss")%>
                                          </ItemTemplate>
                                          <FooterTemplate>
                                              <asp:Label ID="lbl_Profit_Loss" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                                          </FooterTemplate>
                                      </asp:TemplateColumn>                                        
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_Party_Type"/> 
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        </div>
    </form>
</body>
</html>
