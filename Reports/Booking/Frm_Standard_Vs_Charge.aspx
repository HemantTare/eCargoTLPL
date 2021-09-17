<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Standard_Vs_Charge.aspx.cs" Inherits="Reports_Booking_Frm_Standard_Vs_Charge" %>
<%@ Register Src="~/CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc2" %>
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
    <title>Untitled Page</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scm_Truck_Unloading" runat="server"></asp:ScriptManager>

    <table runat="server" id="Table1" class="TABLE">
     <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Standard Vs Charge"></asp:Label>
        </td>
      </tr>
    </table>
          
    <table runat="server" id="tbl_input_screen" class="TABLE">
 
      <tr style="width:100%;">
        <td>
            <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
        </td>
        
      </tr>
        <tr>
            <td style="width: 100%">
                <uc6:WucFilter ID="WucFilter1" runat="Server" />
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 10%; " class="TD1">
                        <asp:label ID="lbl_division" runat="server" CssClass="LABEL" Text="label"/></td>
                        <td style="width: 24%;"><uc5:WucDivisions ID="WucDivisions1" runat="server" /></td>
                        <td style="width: 10%;" class="TD1">From Var %</td>
                        <td style="width: 23%;"><asp:TextBox ID="Txt_From_Var" runat="server" CssClass="TEXTBOX" Wrap="False"></asp:TextBox></td>
                       <td style="width: 10%;" class="TD1">To Var %</td>
                        <td style="width: 23%;"><asp:TextBox ID="Txt_To_Var" runat="server" CssClass="TEXTBOX" Wrap="False"></asp:TextBox></td>
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

    <table class="TABLE" >
      <tr>
            <td style="width:10%;">
              <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click"  />
            </td>
            <td style="width:10%;">
              <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
            </td>

            <td style="width:11%;">
              <a href="javascript:input_screen_action('view');">View Input</a>
            </td>

            <td style="width:11%;">
              <a href="javascript:input_screen_action('hide');">Hide Input</a>
            </td>


            <td style="width:58%;">
              <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                    Text="Close Window" /></td>
      </tr>
    </table>
    
    <table class="TABLE">
      <tr>
        <td>
          <asp:UpdatePanel ID="Upd_Pnl_BookingRegister" UpdateMode="Conditional" runat="server">
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
              <ContentTemplate>
                <%--<asp:Panel ID="pnl_BookingRegister" runat="server" ScrollBars="Auto" Height="500px">--%>
                     <div class="DIV" style="height: 505px; width:976px;">
                         <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged" AllowCustomPaging="true" 
                      OnItemDataBound="dg_Grid_ItemDataBound" PageSize="9">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                      <Columns>
                        <asp:TemplateColumn HeaderText="gc_caption No">
                            <ItemTemplate>                    
                                <%# DataBinder.Eval(Container.DataItem, "gc_caption No")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="gc_caption Date" HeaderText="gc_caption Date"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Booking Region" HeaderText="Booking Region"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Booking Area" HeaderText="Booking Area"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Booking Branch" HeaderText="Booking Branch"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Delivery Branch" HeaderText="Delivery Branch"></asp:BoundColumn> 
                        <asp:TemplateColumn HeaderText="gc_caption Amt Charged" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "gc_caption Amt Charged")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_GC_Amt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateColumn>                    
                        <asp:TemplateColumn HeaderText="Charged Weight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Charged Weight")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_Charged_Weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Basic Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Basic Freight")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_BasicChrged" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Standard Freight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Standard Freight")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_BasicStnderd" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Basic Variance" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Basic Variance")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_BasicVariance" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="gc_caption Amt Standard" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "gc_caption Amt Standard")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_GCAmtStanderd" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Variance Amount" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Variance Amount")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_var_amt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Variance Percent" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                            <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Variance Percent")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_var_per" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                  </Columns>
                  </asp:DataGrid>
                  </div>
        <%--       </asp:Panel>--%>
              </ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>
  </table>
    </form>
</body>
</html>
