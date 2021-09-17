<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Leakage_Breakage_Report.aspx.cs" Inherits="Reports_Inward_Frm_Leakage_Breakage_Report" %>
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
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Leakage Breakage Material"></asp:Label>
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
                        <td style="width: 24%;"><uc5:WucDivisions ID="WucDivisions1" runat="server" /></td>
                        <td style="width: 10%; " class="TD1">Type:</td>
                        <td style="width: 23%;">
                            <asp:DropDownList ID="ddl_Type" runat="server" CssClass="DROPDOWN">
                                <asp:ListItem Value="0">Both</asp:ListItem>
                                <asp:ListItem Value="2">Damaged</asp:ListItem>
                                <asp:ListItem Value="3">Leakage</asp:ListItem>
                            </asp:DropDownList></td>
                       <td style="width: 10%;" class="TD1"></td>
                       <td style="width: 23%;"></td>
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
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
               <ContentTemplate>
                  <div class="DIV" style="height: 510px; width:976px;">
                  <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                      OnItemDataBound="dg_Grid_ItemDataBound" PageSize="9" AllowCustomPaging="True">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                      <Columns>
                      <asp:BoundColumn DataField="Unloading Region" HeaderText="Unloading Region"></asp:BoundColumn>
                      <asp:BoundColumn DataField="Unloading Area" HeaderText="Unloading Area"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="Unloading Branch" HeaderText="Unloading Branch"></asp:BoundColumn>
                   
                         <asp:TemplateColumn HeaderText="gc_caption No">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "gc_caption No")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>
                        
                      <asp:BoundColumn DataField="Booking Branch" HeaderText="Booking Branch"></asp:BoundColumn>
                      <asp:BoundColumn DataField="Delivery Branch" HeaderText="Delivery Branch"></asp:BoundColumn> 
                      
                          <asp:TemplateColumn HeaderText="Charged Weight">
                              <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Charged Weight")%>
                              </ItemTemplate>
                              <FooterTemplate>
                              <asp:Label ID="lbl_Weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>                          
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                          
                          <asp:TemplateColumn HeaderText="Total Articles">
                              <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Total Articles")%>
                              </ItemTemplate>
                              <FooterTemplate>
                              <asp:Label ID="lbl_Total_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                          <asp:TemplateColumn HeaderText="Freight">
                              <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Frieght")%>
                              </ItemTemplate>
                              <FooterTemplate>
                              <asp:Label ID="lbl_Frieght" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                          <asp:TemplateColumn HeaderText="Invoice Value">
                              <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Invoice Value")%>
                              </ItemTemplate>
                              <FooterTemplate>
                              <asp:Label ID="lbl_V_Val" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                          
                           <asp:TemplateColumn HeaderText="Received Weight">
                              <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "Received Weight")%>
                              </ItemTemplate>
                              <FooterTemplate>
                              <asp:Label ID="lbl_Rec_Wght" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                               <ItemStyle HorizontalAlign="Right" />
                               <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                      <asp:BoundColumn DataField="Received Condition" HeaderText="Received Condition"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="Arrival Date" HeaderText="Arrival Date"></asp:BoundColumn> 
                      
                       
                      <asp:BoundColumn DataField="Remarks" HeaderText="Remarks"></asp:BoundColumn> 
                          <asp:TemplateColumn HeaderText="Damaged Articles">
                              <ItemTemplate>
                                  <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Damaged Articles") %>'></asp:Label>
                              </ItemTemplate>
                               <FooterTemplate>
                              <asp:Label ID="lbl_damaged_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                           <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                          <asp:TemplateColumn HeaderText="Damaged Value">
                              <ItemTemplate>
                                  <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Damaged Value") %>'></asp:Label>
                              </ItemTemplate>
                                 <FooterTemplate>
                              <asp:Label ID="lbl_damaged_Value" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                           <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
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
