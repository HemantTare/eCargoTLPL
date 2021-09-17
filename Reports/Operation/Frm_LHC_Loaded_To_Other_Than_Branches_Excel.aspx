<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_LHC_Loaded_To_Other_Than_Branches_Excel.aspx.cs" Inherits="Reports_Operation_Frm_LHC_Loaded_To_Other_Than_Branches_Excel" %>

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
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="LHC Loaded To Other Than Branches"></asp:Label>
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
                        <td style="width: 24%;"></td>
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
              <a href="javascript:input_screen_action('view');">View Inupt</a>
            </td>

            <td style="width:11%;">
              <a href="javascript:input_screen_action('hide');">Hide Inupt</a>
            </td>


            <td style="width:58%;">
              <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
              </td>
            
      </tr>
    </table>
       
    <table class="TABLE">
      <tr>
        <td>                    
             <asp:Panel ID="pnl_LHC" runat="server" ScrollBars="Auto" Height="495px">
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
                          <asp:BoundColumn DataField="LHC From" HeaderText="LHC From"></asp:BoundColumn> 
                    
                          <asp:BoundColumn DataField="LHC To" HeaderText="LHC To"></asp:BoundColumn> 
                          <asp:BoundColumn DataField="Vehicle No" HeaderText="Vehicle No"></asp:BoundColumn> 
                          <asp:TemplateColumn HeaderText="Loaded Wt">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Loaded Wt")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_Loaded_Wt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="Total Hire Amount">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Total Hire Amount")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_Total_Hire_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="To Pay Collection Amount">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "To Pay Collection Amount")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_To_Pay_Collection_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>
                       <asp:TemplateColumn HeaderText="Debited/Credited To">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Debited/Credited To")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Debited_Credited_To" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>                           
                            
                          </asp:TemplateColumn>
                          <asp:TemplateColumn HeaderText="BTH Amount" HeaderStyle-HorizontalAlign="Center">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "BTH Amount")%>
                          </ItemTemplate>
                            <FooterTemplate>
                              <asp:Label ID="lbl_Balance_Payble_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                              <ItemStyle HorizontalAlign="Right" />
                              <FooterStyle HorizontalAlign="Right" />
                          </asp:TemplateColumn>                       
                      </Columns>
                  </asp:DataGrid>
          </asp:Panel>
            </td>
      </tr>
  </table>
 </form>
</body>
</html>