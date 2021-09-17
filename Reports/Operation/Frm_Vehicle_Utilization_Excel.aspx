<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Vehicle_Utilization_Excel.aspx.cs" Inherits="Reports_Operation_Frm_Vehicle_Utilization_Excel" %>

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
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Vehicle Utilization"></asp:Label>
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
                        <td style="width: 9%;" class="TD1"></td>
                        <td style="width: 24%;">
                            </td>
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
              <a href="javascript:input_screen_action('view');">View Inupt</a>
            </td>

            <td style="width:11%;">
              <a href="javascript:input_screen_action('hide');">Hide Inupt</a>
            </td>


            <td style="width:58%;">
              <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                
      </tr>
    </table>
    <table class="TABLE">
      <tr>
        <td style="height: 179px">
        <%--  <asp:UpdatePanel ID="Upd_Pnl_Vehicle_Monitor" UpdateMode="Conditional" runat="server">--%>
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                 
              </Triggers>
             <ContentTemplate>
               <%--<asp:Panel ID="pnl_VehicleMonitor" runat="server" ScrollBars="Auto" Height="495px">--%>
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
                         <asp:BoundColumn DataField="lhpo_caption Date" HeaderText="lhpo_caption Date"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="From Location" HeaderText="From Location"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="Destination" HeaderText="Destination"></asp:BoundColumn> 
                        
                          <asp:TemplateColumn HeaderText="Actual Weight">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Actual Weight")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Actual_Weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                           <asp:BoundColumn DataField="Vehicle_Type" HeaderText="Type of Lorry"></asp:BoundColumn> 
                           
                           <asp:TemplateColumn HeaderText="Lorry Freight">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Truck Hire Charge")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Truck_Hire_Charge" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>    
                          
                           <asp:TemplateColumn HeaderText="Vehicle Capacity">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Vehicle Capacity")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Vehicle_Capacity" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>                         
                          
                           <asp:TemplateColumn HeaderText="Utilization">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Utilization")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Utilization" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>                                              
                      </Columns>
                  </asp:DataGrid>
            <%-- </asp:Panel>--%>
            </ContentTemplate>
            <footerstyle cssclass="GRIDFOOTERCSS" />
            <pagerstyle cssclass="GRIDPAGERCSS" horizontalalign="Left" mode="NumericPages" />
            <alternatingitemstyle cssclass="GRIDALTERNATEROWCSS" />
            <headerstyle cssclass="GRIDHEADERCSS" />
            <itemstyle horizontalalign="Right" />
            <footerstyle horizontalalign="Right" />
            <itemstyle horizontalalign="Right" />
            <footerstyle horizontalalign="Right" />
            <itemstyle horizontalalign="Right" />
            <footerstyle horizontalalign="Right" />
            <asp:BoundColumn></asp:BoundColumn>
            <asp:BoundColumn></asp:BoundColumn><footerstyle cssclass="GRIDFOOTERCSS" />
            <pagerstyle cssclass="GRIDPAGERCSS" horizontalalign="Left" mode="NumericPages" />
            <alternatingitemstyle cssclass="GRIDALTERNATEROWCSS" />
            <headerstyle cssclass="GRIDHEADERCSS" />
            <asp:BoundColumn></asp:BoundColumn>
            <itemstyle horizontalalign="Right" /><footerstyle horizontalalign="Right" /><itemstyle horizontalalign="Right" /><footerstyle horizontalalign="Right" /><itemstyle horizontalalign="Right" /><footerstyle horizontalalign="Right" /><asp:BoundColumn></asp:BoundColumn>
            <footerstyle cssclass="GRIDFOOTERCSS" /><pagerstyle cssclass="GRIDPAGERCSS" horizontalalign="Left" mode="NumericPages" /><alternatingitemstyle cssclass="GRIDALTERNATEROWCSS" /><headerstyle cssclass="GRIDHEADERCSS" /><asp:BoundColumn></asp:BoundColumn>
            <itemstyle horizontalalign="Right" /><footerstyle horizontalalign="Right" /><itemstyle horizontalalign="Right" /><footerstyle horizontalalign="Right" /><itemstyle horizontalalign="Right" /><footerstyle horizontalalign="Right" /><asp:BoundColumn></asp:BoundColumn>
      <%-- </asp:UpdatePanel>--%></td>
      </tr>
  </table>
 </form>
</body>
</html>
