<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Bill_Register_Reach.aspx.cs" Inherits="Reports_Sales_Billing_Frm_Bill_Register_Reach" %>
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
    <asp:ScriptManager ID="scm_Truck_Unloading" runat="server"></asp:ScriptManager>

    <table runat="server" id="Table1" class="TABLE">
     <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Bill  Register"></asp:Label>
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
                        <td style="width: 10%; " class="TD1" id="Division" runat="server">
                            <asp:Label ID="lbl_division" runat="server" CssClass="LABEL"></asp:Label></td>
                        <td style="width: 24%;"><uc5:WucDivisions ID="WucDivisions1" runat="server" /></td>
                        <td style="width: 9%;" class="TD1">
                            Client Name:</td>
                        <td style="width: 24%;"><asp:TextBox ID="Txt_Consignor_name" runat="server" CssClass="TEXTBOX"></asp:TextBox></td>
                        <td style="width: 9%;" class="TD1">
                            Booking Type:</td>
                        <td style="width: 24%;">
                            <asp:DropDownList ID="ddl_booking_type" runat="server" CssClass="DROPDOWN">
                                <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                <asp:ListItem Value="1">Parcel</asp:ListItem>
                                <asp:ListItem Value="2">FTL</asp:ListItem>
                                <asp:ListItem Value="3">ODC</asp:ListItem>
                                <asp:ListItem Value="4">Super ODC\Axle</asp:ListItem>
                                <asp:ListItem Value="5">Container</asp:ListItem>
                                <asp:ListItem Value="6">Spl Tempo Load</asp:ListItem>
                            </asp:DropDownList></td>
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
              <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" OnLoad="Wuc_Export_To_Excel1_Load" />
            </td>

            <td style="width:11%;">
              <a href="javascript:input_screen_action('view');">View Input</a>
            </td>

            <td style="width:11%;">
              <a href="javascript:input_screen_action('hide');">Hide Input</a>
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
                         <asp:TemplateColumn HeaderText="Bill No">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Bill_no_for_print")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>
                        <asp:BoundColumn DataField="Bill_Date" HeaderText="Bill Date"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Client_name" HeaderText="Client Name"></asp:BoundColumn>
                        <asp:BoundColumn DataField="gc_no_for_print" HeaderText="gc_caption No"></asp:BoundColumn> 
                      
                        <asp:BoundColumn DataField="Gc_Date" HeaderText="gc_caption Date"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="from" HeaderText="From"></asp:BoundColumn>
                          <asp:BoundColumn DataField="to" HeaderText="To"></asp:BoundColumn>  
                        <asp:TemplateColumn HeaderText="Bill Amount">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Bill Amount")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_bill_amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>              
                        <asp:BoundColumn DataField="Mr_no_for_print" HeaderText="MR No"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Mr_Date" HeaderText="MR Date"></asp:BoundColumn> 

                        <asp:TemplateColumn HeaderText="Recd Amount">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Recd Amount")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Recd_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>                    
                      
                        
                         <asp:TemplateColumn HeaderText="Deduction">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Deduction")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Deduction" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
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
