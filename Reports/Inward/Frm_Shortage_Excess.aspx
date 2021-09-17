<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Shortage_Excess.aspx.cs" Inherits="Reports_Inward_Frm_Shortage_Excess" %>
<%@ Register Src="../../CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters" TagPrefix="uc3" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc2" %>
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
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Shortage and Excess"></asp:Label>
        </td>
      </tr>
    </table>
          
    <table runat="server" id="tbl_input_screen" class="TABLE">
 
      <tr>
        <td style="width:100%; height: 23px;">
            <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
        </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 10%; " class="TD1">
                        <asp:label ID="lbl_division" runat="server" CssClass="LABEL" Text="label"/></td>
                        <td style="width: 24%;"><uc5:WucDivisions ID="WucDivisions1" runat="server" /></td>
                        <td style="width: 9%;" class="TD1">Status:</td>
                        <td style="width: 24%;">
                            <asp:DropDownList ID="ddl_Type" runat="server" CssClass="DROPDOWN">
                                <asp:ListItem Value="0">Shortage</asp:ListItem>
                                <asp:ListItem Value="1">Excess</asp:ListItem>
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
        <td>
          <asp:UpdatePanel ID="Upd_Pnl_Vehicle_Monitor" UpdateMode="Conditional" runat="server">
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid_Short" />
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid_Excess" />

              </Triggers>
               <ContentTemplate>
                <div class="DIV" style="height: 510px; width:976px;">
                  <asp:DataGrid ID="dg_Grid_Short"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_Short_PageIndexChanged"
                      OnItemDataBound="dg_Grid_Short_ItemDataBound" PageSize="9" AllowCustomPaging="True" >

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
                        <asp:BoundColumn DataField="Type" HeaderText="Type"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Booking Branch" HeaderText="Booking Branch"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Short Excess Branch" HeaderText="Short Excess Branch"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Unloading Date" HeaderText="Unloading Date"></asp:BoundColumn> 
                        <asp:TemplateColumn HeaderText="Actual Article">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Actual Article")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Actual_article" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>                  
                         <asp:TemplateColumn HeaderText="Recieved Articles">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Recieved Articles")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Recieved_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                            <asp:TemplateColumn HeaderText="Actual Weight">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Actual Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Actual_weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                         <asp:TemplateColumn HeaderText="Received Weight">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Received Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Received_weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="Sub Total">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Sub Total")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_sub_total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                         <asp:TemplateColumn HeaderText="Total Invoice Value">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Total Invoice Value")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Total_Invoice_Value" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                         
                          <asp:BoundColumn DataField="Unloading Branch" HeaderText="Unloading Branch"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Vehicle No" HeaderText="Vehicle No"></asp:BoundColumn>
                          <asp:BoundColumn DataField="AUS No" HeaderText="AUS No"></asp:BoundColumn>  
                     </Columns>
                  </asp:DataGrid>
                  
                  
                  
                  <asp:DataGrid ID="dg_Grid_Excess"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_Excess_PageIndexChanged"
                      OnItemDataBound="dg_Grid_Excess_ItemDataBound" PageSize="9" AllowCustomPaging="True">

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
                        <asp:BoundColumn DataField="Type" HeaderText="Type"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Excess Branch" HeaderText="Excess Branch"></asp:BoundColumn>
                         <asp:BoundColumn DataField="Unloading Date" HeaderText="Unloading Date"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Vehicle No" HeaderText="Vehicle No"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Damaged Articles">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Damaged Articles")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Damaged_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>                    
                      
                        
                         <asp:TemplateColumn HeaderText="Damaged Value">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Damaged Value")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Damaged_Value" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="Excess Articles">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Excess Articles")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Excess_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                         <asp:TemplateColumn HeaderText="Excess Articles Weight">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Excess Articles Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Excess_Articles_Wt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                         <asp:TemplateColumn HeaderText="Marking On Packaget">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Marking On Package")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Marking_On_Package" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                          <asp:BoundColumn DataField="Remarks" HeaderText="Remarks"></asp:BoundColumn> 
                          
                     </Columns>
                  </asp:DataGrid>
                  </div>
</ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>
  </table>
    </form>
</body>
</html>
