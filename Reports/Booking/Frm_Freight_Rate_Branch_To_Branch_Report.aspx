<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Freight_Rate_Branch_To_Branch_Report.aspx.cs" Inherits="Reports_Booking_Frm_Freight_Rate_Branch_To_Branch_Report" %>
<%@ Register Src="~/CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch_1" TagPrefix="uc6" %>
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
       <asp:ScriptManager ID="scm_Truck_Unloading" runat="server"></asp:ScriptManager>

    <table runat="server" id="Table1" class="TABLE">
     <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Freight Rate Branch To Branch"></asp:Label>
        </td>
      </tr>
    </table>
          
    <table runat="server" id="tbl_input_screen" class="TABLE">
        <tr>
            <td style="width: 100%; ">
                <table style="width: 100%">
                    <tr>
                        <td class="TD1" colspan="6">
                            <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD1" style="height: 21px" colspan="6">
                            <uc6:Wuc_Region_Area_Branch_1 ID="Wuc_Region_Area_Branch2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td class="TD1" style="height: 21px" colspan="6">
                            <uc6:WucFilter ID="WucFilter1" runat="Server" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%;" class="TD1">
                            <asp:Label ID="lbl_division" runat="server" Text="Label"></asp:Label></td>
                        <td style="width: 24%;">
                            &nbsp;<uc5:WucDivisions ID="WucDivisions1" runat="server" />
                        </td>
                        <td style="width: 9%;" class="TD1"></td>
                        <td style="width: 24%;">
                            <asp:RadioButtonList ID="rdl_type" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdl_type_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem Selected="True" Value="0">Bookingwise</asp:ListItem>
                                <asp:ListItem Value="1">Deliverywise</asp:ListItem>
                            </asp:RadioButtonList></td>
                       <td style="width: 9%;" class="TD1"></td>
                        <td style="width: 24%;"></td>
                    </tr>
                </table>
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
        <td>
         <asp:UpdatePanel ID="Upd_Pnl_BookingRegister" UpdateMode="Conditional" runat="server">
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
              <ContentTemplate>
              <asp:Panel ID="pnl_BookingRegister" runat="server" ScrollBars="Auto" Height="500px">
                   
                     <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged" AllowCustomPaging="true" 
                      OnItemDataBound="dg_Grid_ItemDataBound" PageSize="9">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                      <Columns>
                         <asp:TemplateColumn HeaderText="Sr No.">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Sr No")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_Total1" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>
                    
                          <asp:TemplateColumn HeaderText="From Branch">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "From Branch")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>
                          
                       
                        <asp:BoundColumn DataField="To Branch" HeaderText="To Branch"></asp:BoundColumn> 
                      
                          
                        <asp:TemplateColumn HeaderText="Freight Rate" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Freight Rate")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
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
