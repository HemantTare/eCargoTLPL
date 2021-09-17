<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_ShortExcess_Supervisorwise.aspx.cs" Inherits="Reports_Operation_Frm_ShortExcess_Supervisorwise" %>
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
     <asp:ScriptManager ID="scm_Supervisorwise" runat="server"></asp:ScriptManager>

    <table runat="server" id="Table1" class="TABLE">
     <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Short Excess Supervisorwise"></asp:Label>
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
                        <td style="width: 21%;"><uc5:WucDivisions ID="WucDivisions1" runat="server" /></td>
                        <td style="width: 9%; " class="TD1">Type:</td>
                        <td style="width: 21%;">
                            <asp:DropDownList ID="ddl_Type" runat="server" CssClass="DROPDOWN">
                                <asp:ListItem Value="0">Both</asp:ListItem>
                                <asp:ListItem Value="1">Short/Excess</asp:ListItem>
                                <asp:ListItem Value="2">Damaged/Leakage</asp:ListItem>
                            </asp:DropDownList></td>
                       <td style="width: 9%;" class="TD1"></td>
                       <td style="width: 21%;"></td>
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
        <td >
          <asp:UpdatePanel ID="Upd_Pnl_Route_Load_Performance" UpdateMode="Conditional" runat="server">
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
                         <asp:TemplateColumn HeaderText="gc_caption No">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "gc_caption No")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>
                         <asp:BoundColumn DataField="gc_caption Date" HeaderText="gc_caption Date"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="BKG Branch" HeaderText="Booking Branch"></asp:BoundColumn>
                      <asp:BoundColumn DataField="DEL Branch" HeaderText="Delivery Branch"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="RECEIVED CONDITION" HeaderText="Recieved Condition"></asp:BoundColumn> 
                          <asp:TemplateColumn HeaderText="Articles">
                              <ItemTemplate>
                                <%# DataBinder.Eval(Container.DataItem, "ARTICLES")%>
                              </ItemTemplate>
                              <FooterTemplate>
                              <asp:Label ID="lbl_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>
                      <asp:BoundColumn DataField="LOADING BRANCH" HeaderText="Loading Branch"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="LOADING SUPERVISOR" HeaderText="Loading Supervisor"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="Memo Date" HeaderText="Memo Date"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="lhpo_caption No" HeaderText="lhpo_caption No"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="Unloading Branch" HeaderText="Unloading Branch"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="UNLOADING SUPERVISOR" HeaderText="Unloading Supervisor"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="UNLOADING DATE" HeaderText="Unloading Date"></asp:BoundColumn> 
                       <asp:BoundColumn HeaderText="AUS No" DataField="AUS NO"></asp:BoundColumn>
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
