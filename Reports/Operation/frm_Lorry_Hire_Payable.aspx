<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_Lorry_Hire_Payable.aspx.cs" Inherits="Reports_Operation_frm_Lorry_Hire_Payable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc6" %>
<%@ Register Src="~/CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc2" %>

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
<head id="Head1" runat="server">
    <title>Untitled Page</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="scm_Vehicle_Monitor" runat="server"></asp:ScriptManager>

    <table runat="server" id="Table1" class="TABLE">
     <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Lorry Hire Payable"></asp:Label>
        </td>
      </tr>
    </table>
          
    <table runat="server" id="tbl_input_screen" class="TABLE">
      <tr>
        <td style="width:100%">
            <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
        </td>
         </tr>
        <tr>
            <td style="width: 100%">
                <table style="width: 100%">
                    <tr>
                       <td style="width: 10%; " class="TD1">
                          <asp:Label ID="lbl_division" runat="server" Text="Label"></asp:Label></td>
                        <td style="width: 24%; height: 39px;"><uc5:WucDivisions ID="WucDivisions1" runat="server" /></td>
                        <td style="width: 9%; height: 39px;" class="TD1">Vehicle No:</td>
                        <td style="width: 24%; height: 39px;"><asp:TextBox ID="Txt_Vehicle_No" runat="server" CssClass="TEXTBOX"></asp:TextBox></td>
                        <td style="width: 9%; height: 39px;" class="TD1">
                            Vehicle Type:</td>
                        <td style="width: 24%; height: 39px;">
                            <asp:DropDownList ID="ddl_Truck" runat="server" CssClass="DROPDOWN">
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Truck</asp:ListItem>
                                <asp:ListItem Value="2">Tempo</asp:ListItem>
                                <asp:ListItem Value="3">Matador</asp:ListItem>
                                <asp:ListItem Value="5">Axle</asp:ListItem>
                                <asp:ListItem Value="6">cbxcvbxcvb44</asp:ListItem>
                            </asp:DropDownList></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 10%;" class="TD1">
                            Vehicle Category:</td>
                        <td style="width: 24%;">
                            &nbsp;<asp:DropDownList ID="ddl_Veh_Cat" runat="server" CssClass="DROPDOWN">
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Own</asp:ListItem>
                                <asp:ListItem Value="2">Managed</asp:ListItem>
                                <asp:ListItem Value="3">Attached</asp:ListItem>
                                <asp:ListItem Value="4">Market</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="width: 9%;" class="TD1">
                            Broker Name:</td>
                        <td style="width: 24%;">
                            <asp:TextBox ID="Txt_Consignor_name" runat="server" CssClass="TEXTBOX"></asp:TextBox></td>
                        <td style="width: 9%;" class="TD1">
                            As On Date:</td>
                        <td style="width: 24%;">
                            &nbsp;<uc6:WucDatePicker ID="WucDatePicker1" runat="server" />
                        </td>
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
              </td>
            
      </tr>
    </table>
       
    <table class="TABLE">
      <tr>
        <td>
         <%-- <asp:UpdatePanel ID="Upd_Pnl_Vehicle_Monitor" UpdateMode="Conditional" runat="server">--%>
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
               <ContentTemplate>
             <%--  <asp:Panel ID="pnl_VehicleMonitor" runat="server" ScrollBars="Auto" Height="495px">--%>
                  <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                      OnItemDataBound="dg_Grid_ItemDataBound" PageSize="9" AllowCustomPaging="True">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                      <Columns>
                         <asp:TemplateColumn HeaderText="lhpo_caption No">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "lhpo_caption No")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>
                          <asp:BoundColumn DataField="lhpo_caption date" HeaderText="lhpo_caption Date"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="Vehicle No" HeaderText="Vehicle No"></asp:BoundColumn> 
                         <asp:BoundColumn DataField="LHS From" HeaderText="LHS From"></asp:BoundColumn>
                          <asp:BoundColumn DataField="LHS To" HeaderText="LHS To"></asp:BoundColumn>
                       <asp:TemplateColumn HeaderText="Total Lorry Hire">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Total Lorry Hire")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Total_Truck_Hire" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                        <%--  <asp:TemplateColumn HeaderText="Advance Amount">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Advance Amount")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Advance_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>--%>
                          <%--    <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>--%>
                          <asp:TemplateColumn HeaderText="Advance Amount Payable">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Advance Amount Payable")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Advance_Amount_As_On" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                            <asp:BoundColumn DataField="ATH Payment Office" HeaderText="ATH Payment Office"></asp:BoundColumn>
                        <%--  <asp:TemplateColumn HeaderText="Balance Amount">
                           <ItemTemplate>  --%>
                       <%--   <%# DataBinder.Eval(Container.DataItem, "Balance Amount")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Balance_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>--%>
                            <asp:TemplateColumn HeaderText="Balance Amount Payable">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Balance Amount Payable")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Balance_Amount_As_On" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                          <asp:BoundColumn DataField="BTH Payment Office" HeaderText="BTH Payment Office"></asp:BoundColumn>
                           <asp:BoundColumn HeaderText="Broker Name" DataField="Broker Name"></asp:BoundColumn>
                      </Columns>
                  </asp:DataGrid>
              <%-- </asp:Panel>--%>
            </ContentTemplate>&nbsp;<%--</asp:UpdatePanel>--%></td>
      </tr>
  </table>
 </form>
</body>
</html>
