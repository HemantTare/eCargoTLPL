<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Real_Time_Loading_Unloading_Excel.aspx.cs" Inherits="Reports_Operation_Frm_Real_Time_Loading_Unloading_Excel" %>
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
     <asp:ScriptManager ID="scm_Route_Load_Performance" runat="server"></asp:ScriptManager>

    <table runat="server" id="Table1" class="TABLE">
     <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Real Time Loading Unloading"></asp:Label>
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
                        <td style="width: 10%;" class="TD1"></td>
                        <td style="width: 23%;">
                            <asp:RadioButtonList ID="rbtn_type" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" Selected = "true">Unloading</asp:ListItem>
                                <asp:ListItem Value="1">Direct Delivery</asp:ListItem>
                            </asp:RadioButtonList></td>
                       <td style="width: 10%;" class="TD1"></td>
                        <td style="width: 23%;">
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
        <td style="height: 524px">
          <asp:UpdatePanel ID="Upd_Pnl_Real_Time" UpdateMode="Conditional" runat="server">
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
              <ContentTemplate>
          <%--      <asp:Panel ID="pnl_BookingRegister" runat="server" ScrollBars="Auto" Height="500px">--%>
                     <div class="DIV" style="height: 510px; width:976px;">
                  <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                      PageSize="9" AllowCustomPaging="True">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                      <Columns>
                    
                          <asp:TemplateColumn HeaderText="Memo No">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Memo_No_For_Print")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_Memo_No" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>                       
                                     
                          <asp:BoundColumn DataField="Memo_Date" HeaderText="Memo Date"></asp:BoundColumn>
                   
                   <asp:BoundColumn DataField="Branch_Name" HeaderText="Memo Branch"></asp:BoundColumn>
                   <asp:BoundColumn DataField="Lhpo_no_for_print" HeaderText="LHPO No"></asp:BoundColumn>
                   <asp:BoundColumn DataField="lhpo_date" HeaderText="LHPO Date"></asp:BoundColumn>
                   <asp:BoundColumn DataField="vehicle_no" HeaderText="Vehicle No"></asp:BoundColumn>
                   <asp:BoundColumn DataField="GAS/Direct Delivery No" HeaderText="GAS/Direct Delivery No"></asp:BoundColumn>
                   <asp:BoundColumn DataField="GAS/Direct Delivery Date" HeaderText="GAS/Direct Delivery Date"></asp:BoundColumn>
                   <asp:BoundColumn DataField="Total Transit Time" HeaderText="Total Transit Time"></asp:BoundColumn>
                   <asp:BoundColumn DataField="Standard Transit Time" HeaderText="Standard Transit Time"></asp:BoundColumn>
                   <asp:BoundColumn DataField="delay" HeaderText="Delay"></asp:BoundColumn>
                   <asp:BoundColumn DataField="Reason" HeaderText="Reason"></asp:BoundColumn>      
                                
                         </Columns>
                  </asp:DataGrid>
                    </div>
               <%--</asp:Panel>--%>
              </ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>
  </table>
    </form>
</body>
</html>

