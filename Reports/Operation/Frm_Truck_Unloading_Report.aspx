<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Truck_Unloading_Report.aspx.cs" Inherits="Reports_Operation_Frm_Truck_Unloading_Report" %>
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
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Truck Unloading Report"></asp:Label>
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
        <td style="width:100%">
            <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch2" runat="server" />
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
                          <asp:Label ID="lbl_division" runat="server" Text="Label"></asp:Label></td>
                        <td style="width: 24%;"><uc5:WucDivisions ID="WucDivisions1" runat="server" /></td>
                        <td style="width: 9%; " class="TD1">Vehicle Type:</td>
                        <td style="width: 24%;">
                          <asp:DropDownList ID="ddl_Truck" runat="server" CssClass="DROPDOWN">
                                <asp:ListItem Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Truck</asp:ListItem>
                                <asp:ListItem Value="2">Tempo</asp:ListItem>
                                <asp:ListItem Value="3">Matador</asp:ListItem>
                                <asp:ListItem Value="5">Axle</asp:ListItem>
                                <asp:ListItem Value="6">cbxcvbxcvb44</asp:ListItem>
                            </asp:DropDownList></td>
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
        <td style="height: 524px">
          <asp:UpdatePanel ID="Upd_Pnl_Truck_Unloading" UpdateMode="Conditional" runat="server">
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
               <ContentTemplate>
               <%-- <asp:Panel ID="pnl_BookingRegister" runat="server" ScrollBars="Auto" Height="500px">--%>
                     <div class="DIV" style="height: 510px; width:976px;">
                  <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                      OnItemDataBound="dg_Grid_ItemDataBound" PageSize="9" AllowCustomPaging="True">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                      <Columns>
                      <asp:BoundColumn DataField="Unloading From AO" HeaderText="Unloading From AO"></asp:BoundColumn> 
                          <asp:BoundColumn HeaderText="Unloading From Branch/Hub" DataField="Unloading From Branch/Hub"></asp:BoundColumn>
                          <asp:TemplateColumn HeaderText="lhpo_caption No">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "lhpo_caption No")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>
                      <asp:BoundColumn DataField="lhpo_caption Date" HeaderText="lhpo_caption Date"></asp:BoundColumn> 
                          <asp:BoundColumn DataField="Truck No &amp; Type" HeaderText="Truck No &amp; Type"></asp:BoundColumn>
                      <asp:BoundColumn DataField="Loading Branch/Hub" HeaderText="Loading Branch/Hub"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="Loading A0" HeaderText="Loading A0"></asp:BoundColumn> 
                          <asp:TemplateColumn HeaderText="No of Memos">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "No of Memos")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_No_of_Memos" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                          <asp:TemplateColumn HeaderText="No of gc_caption's">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "No of gc_caption's")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_No_of_Gc" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Actual Weight">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Actual Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_ActualWeight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          <ItemStyle HorizontalAlign="Right" />
                          <FooterStyle HorizontalAlign="Right" />
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Sub Freight">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Sub Freight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_Sub_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          <ItemStyle HorizontalAlign="Right" />
                          <FooterStyle HorizontalAlign="Right" />
                      </asp:TemplateColumn>
                          <asp:TemplateColumn HeaderText="Truck Hire Charge">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Truck Hire Charge")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Truck_Hire_Charge" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                          <asp:TemplateColumn HeaderText="Advance">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Advance")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_Advance" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                          <asp:BoundColumn HeaderText="BTH Payable At" DataField="BTH Payable At"></asp:BoundColumn>
                          <asp:BoundColumn HeaderText="Last Destination Arrival Date &amp; Time" DataField="Last Destination Arrival Date &amp; Time"></asp:BoundColumn>
                          <asp:BoundColumn HeaderText="Last Destination Unloaded Date &amp; Time" DataField="Last Destination Unloaded Date &amp; Time"></asp:BoundColumn>
                          <asp:BoundColumn HeaderText="Turn-Round time" DataField="Turn-Round time"></asp:BoundColumn>
                          <asp:BoundColumn HeaderText="AUS No" DataField="AUS No"></asp:BoundColumn>
                          <asp:TemplateColumn HeaderText="Truck Capacity">
                            <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Truck Capacity")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_Truck_Capacity" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                          <asp:BoundColumn HeaderText="Manual lhpo_caption No" DataField="Manual lhpo_caption No"></asp:BoundColumn>
                          <asp:BoundColumn HeaderText="Manual TUR No" DataField="Manual TUR No"></asp:BoundColumn>
                          <asp:TemplateColumn HeaderText="Short / Excess">
                           <ItemTemplate >                    
                          <%# DataBinder.Eval(Container.DataItem, "Short/Excess")%>
                          </ItemTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                      </Columns>

                  </asp:DataGrid>
                    </div>
         <%--      </asp:Panel>--%>
              </ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>
  </table>
    </form>
</body>
</html>
