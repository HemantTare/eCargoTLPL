<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_TBB_Pending_Gc_Details_Excel.aspx.cs" Inherits="Reports_Booking_Frm_TBB_Pending_Gc_Details_Excel" %>
<%@ Register Src="~/CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc3" %>
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
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="TBB Pending Gc Details"></asp:Label>
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
            <td style="width: 100%; height: 23px;">
                <uc6:WucFilter ID="WucFilter1" runat="Server" />
            </td>
        </tr>
        <tr>
            <td style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 10%; " class="TD1">
                         <asp:label ID="lbl_division" runat="server" CssClass="LABEL" Text="label"/></td>
                        <td style="width: 24%;"><uc5:WucDivisions ID="WucDivisions1" runat="server" /></td>
                        <td style="width: 9%;" class="TD1">
                            Client Name:</td>
                        <td style="width: 24%;"><asp:TextBox ID="Txt_Consignor_name" runat="server" CssClass="TEXTBOX"></asp:TextBox></td>
                        <td style="width: 9%;" class="TD1"></td>
                        <td style="width: 24%;">
                            </td>
                    </tr>
                    <tr>
                        <td class="TD1" style="width: 10%">
                            As On Date:</td>
                        <td style="width: 24%">
                            <uc3:WucDatePicker id="WucDatePicker1" runat="server">
                            </uc3:WucDatePicker>
                        </td>
                        <td class="TD1" style="width: 9%">
                        </td>
                        <td style="width: 24%">
                        </td>
                        <td class="TD1" style="width: 9%">
                        </td>
                        <td style="width: 24%">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
      <tr>
        <td style="width:100%">
            &nbsp;</td>
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
                         <asp:TemplateColumn HeaderText="gc_caption No">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "gc_caption No")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>
                        <asp:BoundColumn DataField="gc_caption Date" HeaderText="gc_caption Date"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Client_Name" HeaderText="Client Name"></asp:BoundColumn>
                        <asp:BoundColumn DataField="From Location" HeaderText="From Location"></asp:BoundColumn> 
                      
                        <asp:BoundColumn DataField="To Location" HeaderText="To Location"></asp:BoundColumn> 
                        
                       
                        <asp:TemplateColumn HeaderText="Delay">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Delay")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Delay" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>                    
                      
                         <asp:TemplateColumn HeaderText="Actual Weight">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Total_Actual_Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Actual_Weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                         <asp:TemplateColumn HeaderText="Charged Weight">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Charged_Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Charged_Weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>  
                          <asp:TemplateColumn HeaderText="Total Articles">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Total_Articles")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Total_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>                             
                        <asp:BoundColumn DataField="billing Branch" HeaderText="Billing Branch"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Status" HeaderText="POD Status"></asp:BoundColumn> 
                                          
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
