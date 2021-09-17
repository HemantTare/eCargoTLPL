<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Contractual_Client_Performance.aspx.cs" Inherits="Reports_Sales_Billing_Frm_Contractual_Client_Performance" %>
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
     <asp:ScriptManager ID="scm_Contractual_Client_Performance" runat="server"></asp:ScriptManager>

    <table runat="server" id="Table1" class="TABLE">
     <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Contractual Client Performance"></asp:Label>
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
            <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                
            </td>
        </tr>
      <tr>
        <td style="width:100%">
            <table style="width: 100%">
                    <tr>
                           <td style="width: 10%; " class="TD1" id="Division" runat="server">
                            <asp:Label ID="lbl_division" runat="server" CssClass="LABEL"></asp:Label></td>
                        <td style="width: 24%;">
                            <uc5:WucDivisions ID="WucDivisions1" runat="server" />
                            </td>
                        <td style="width: 9%;" class="TD1"></td>
                        <td style="width: 24%;">
                           <asp:RadioButtonList ID="Rbtn_type" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Value="1" Selected="True">Inward</asp:ListItem>
                                <asp:ListItem Value="0">Outward</asp:ListItem>
                            </asp:RadioButtonList></td>
                       <td style="width: 10%;" class="TD1"></td>
                        <td style="width: 23%;">
                            <asp:Button ID="btn_Fill_Client" runat="server" CssClass="BUTTON" OnClick="btn_Fill_Client_Click"
                                Text="Fill Client" /></td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:Panel ID="Pnl_Client" runat="server" BorderWidth="1px" Height="200px" ScrollBars="Vertical"
                                Width="100%" Wrap="False">
                                <asp:CheckBoxList ID="ChkBoxLst_Client" runat="server" CssClass="CHECKBOXLIST" RepeatColumns="2"
                                    RepeatDirection="Vertical">
                                </asp:CheckBoxList>
                            </asp:Panel>
                        </td>
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
          <asp:UpdatePanel ID="Upd_Pnl_Contractual_Client_Performance" UpdateMode="Conditional" runat="server">
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
              <ContentTemplate>
                <asp:Panel ID="pnl_Contractual_Client_Performance" runat="server" ScrollBars="Auto" Height="400px">
                    <%--<div class="DIV" style="height: 510px; width:976px;">--%>
                  <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                      OnItemDataBound="dg_Grid_ItemDataBound" PageSize="9" AllowCustomPaging="True">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                      <Columns>
                          <asp:TemplateColumn HeaderText="Client Name">
                              <ItemTemplate>
                                  <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Client Name") %>'></asp:Label>
                              </ItemTemplate>
                             <FooterTemplate>
                          <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                              <ItemStyle HorizontalAlign="Left" />
                              <FooterStyle HorizontalAlign="Left" />
                              <HeaderStyle HorizontalAlign="Left" />
                          </asp:TemplateColumn>
                         <asp:TemplateColumn HeaderText="Total Recievable">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Total Recievable")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Total_recievable" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                         
                         <asp:TemplateColumn HeaderText="Overdue">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Overdue")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Overdue" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>                   
                                            
                        <asp:TemplateColumn HeaderText="Weight">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                         <asp:TemplateColumn HeaderText="Freight">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Freight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>  
                        
                        <asp:TemplateColumn HeaderText="Promissed Weight">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Promissed Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Promissed_Wt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>  
                        
                        <asp:TemplateColumn HeaderText="Promissed Freight">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Promissed Freight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Promissed_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                         </Columns>
                  </asp:DataGrid>
                  <%--</div>--%>
               </asp:Panel>
              </ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>
  </table>
    </form>
</body>
</html>
