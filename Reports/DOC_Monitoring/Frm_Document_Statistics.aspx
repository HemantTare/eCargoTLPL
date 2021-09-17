<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Document_Statistics.aspx.cs" Inherits="Reports_DOC_Monitoring_Frm_Document_Statistics" %>
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
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Document Statistics"></asp:Label>
        </td>
      </tr>
    </table>
          
    <table runat="server" id="tbl_input_screen" class="TABLE">
       <tr>
        <td style="width:100%;">
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
                        <td style="width: 9%;" class="TD1"></td>
                        <td style="width: 24%;"></td>
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
          <asp:UpdatePanel ID="Upd_Pnl_Document_Statistic" UpdateMode="Conditional" runat="server">
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
              <ContentTemplate>
                <asp:Panel ID="pnl_Document_Statistic" runat="server" ScrollBars="Auto" Height="225px">
                     <%--<div class="DIV" style="height: 250px; width:976px;" >--%>
                         <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                      OnItemDataBound="dg_Grid_ItemDataBound">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                      <Columns>
                         <asp:TemplateColumn HeaderText="Name">
                           <ItemTemplate>                    
                          <%--<%# DataBinder.Eval(Container.DataItem, "Name")%>--%>
                              <asp:HiddenField ID="hdn_Id" runat="server" Value = '<%# DataBinder.Eval(Container.DataItem, "ID")%>'></asp:HiddenField>
                              <asp:Label ID="lbl_Name" runat="server" CssClass="LABEL" text = '<%# DataBinder.Eval(Container.DataItem, "Name")%>'></asp:Label>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                             <ItemStyle HorizontalAlign="Left" Width="70%" />
                             <FooterStyle HorizontalAlign="Left" Width="70%" />
                             <HeaderStyle Width="70%" />
                          </asp:TemplateColumn>
                          <asp:TemplateColumn HeaderText="No of Documents">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "No Of Document")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_No_Of_documents" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" Width="15%" />
                              <FooterStyle HorizontalAlign="Right" Width="15%" />
                              <HeaderStyle HorizontalAlign="Center" Width="15%" />
                        </asp:TemplateColumn>     
                         <asp:TemplateColumn  HeaderText="Cancelled Document">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Cancelled Docs")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Cancelled_Docs" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" Width="15%" />
                             <FooterStyle HorizontalAlign="Right" Width="15%" />
                             <HeaderStyle HorizontalAlign="Center" Width="15%" />
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
