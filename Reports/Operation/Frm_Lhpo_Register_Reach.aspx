<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Lhpo_Register_Reach.aspx.cs" Inherits="Reports_Operation_Frm_Lhpo_Register_Reach" %>

<%@ Register Src="~/CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc2" %>
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
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="LHS Register"></asp:Label>
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
                                <asp:ListItem Value="5">Market</asp:ListItem>
                            </asp:DropDownList></td>
                        <td style="width: 9%;" class="TD1">
                            Broker Name:</td>
                        <td style="width: 24%;">
                            <asp:TextBox ID="Txt_Consignor_name" runat="server" CssClass="TEXTBOX"></asp:TextBox></td>
                        <td style="width: 9%;" class="TD1">
                        </td>
                        <td style="width: 24%;">
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
        <td style="height: 243px">
          <asp:UpdatePanel ID="Upd_Pnl_Vehicle_Monitor" UpdateMode="Conditional" runat="server">
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
               <ContentTemplate>
          <%--   <asp:Panel ID="pnl_VehicleMonitor" runat="server" ScrollBars="Auto" Height="495px">--%>
           <div class="DIV" style="height: 510px; width:976px;">
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
                      <asp:BoundColumn DataField="Vehicle_No" HeaderText="Vehicle No"></asp:BoundColumn> 
                          <asp:BoundColumn DataField="Dest To" HeaderText="Dest To"></asp:BoundColumn>
                       <asp:TemplateColumn HeaderText="LHS Amt">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "LHS Amt")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_LHS_Amt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                          <asp:TemplateColumn HeaderText="TDS on LHS">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "TDS on LHS")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_TDS_on_LHS" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                          <asp:TemplateColumn HeaderText="Charity">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Charity")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Charity" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                          <asp:BoundColumn HeaderText="CN No/s" DataField="CN No/s"></asp:BoundColumn>
                          
                           <asp:TemplateColumn HeaderText="Total ATH">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Total ATH")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Total_ATH" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="ATH Amt">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "ATH Amt")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_ATH_Amt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="ATH Amount Paid">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "ATH Amount Paid")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_ATH_Amount_Paid" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                          
                          <asp:BoundColumn HeaderText="ATH Payment Office" DataField="ATH Payment Office"></asp:BoundColumn>
                           <asp:TemplateColumn HeaderText="Munishana Deduction">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Munishana Deduction")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Munishana_Deduction" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>

                          <asp:BoundColumn HeaderText="ATH Paid Date" DataField="ATH Paid Date"></asp:BoundColumn>
                          <asp:BoundColumn HeaderText="BTH Branch" DataField="Balance Payable At"></asp:BoundColumn>
                           <asp:TemplateColumn HeaderText="Total BTH Amt">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Total BTH Amt")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Total_BTH_Amt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="Other Charges Paid">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Other Charges Paid")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Other_Charges_Paid" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="TDS on Other Charges">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "TDS on Other Charges")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_TDS_on_Other_Charges" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>  
                          
                            <asp:TemplateColumn HeaderText="Other Charges Deducted">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Other Charges Deducted")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Other_Charges_Deducted" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>                        
                          <asp:BoundColumn HeaderText="Balance Paid Date" DataField="Balance Paid Date"></asp:BoundColumn>
                          <asp:BoundColumn HeaderText="Ledger Name" DataField="Ledger Name"></asp:BoundColumn>

                          <asp:BoundColumn HeaderText="Broker Name" DataField="Broker Name"></asp:BoundColumn>
                      </Columns>
                  </asp:DataGrid>
                    </div>
              <%-- </asp:Panel>--%>
            </ContentTemplate></asp:UpdatePanel></td>
      </tr>
  </table>
 </form>
</body>
</html>

