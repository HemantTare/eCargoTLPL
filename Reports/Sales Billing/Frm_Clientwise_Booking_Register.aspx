<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Clientwise_Booking_Register.aspx.cs" Inherits="Reports_Sales_Billing_Frm_Clientwise_Booking_Register" %>
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
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="ClientWise Booking Register"></asp:Label>
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
                        <td style="width: 9%;" class="TD1">Consignor Name:</td>
                        <td style="width: 24%;"><asp:TextBox ID="Txt_Consignor_name" runat="server" CssClass="TEXTBOX"></asp:TextBox></td>
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
                          <asp:BoundColumn DataField="Invoice No" HeaderText="Invoice No"></asp:BoundColumn>
                        <asp:BoundColumn DataField="gc_caption Date" HeaderText="gc_caption Date"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Booking Branch" HeaderText="Booking Branch"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Delivery Branch" HeaderText="Delivery Branch"></asp:BoundColumn> 
                      
                        <asp:BoundColumn DataField="Booking Type" HeaderText="Booking Type"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Delivery Type" HeaderText="Delivery Type"></asp:BoundColumn>
                          <asp:BoundColumn DataField="Payment Type" HeaderText="Payment Type"></asp:BoundColumn>  
                        <asp:BoundColumn DataField="Consignee Name" HeaderText="Consignee Name"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Consignor Name" HeaderText="Consignor Name"></asp:BoundColumn> 
                       
                        <asp:TemplateColumn HeaderText="Articles">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Total Articles")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>                    
                      
                        
                         <asp:TemplateColumn HeaderText="Charged Weight">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Charged Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Charged_Weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="Freight Amt">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Freight Amt")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Freight_Amt" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                         <asp:TemplateColumn HeaderText="gc_caption Amount">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "gc_caption Amount")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Gc_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                         
                         <asp:TemplateColumn HeaderText="Sub Total">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Sub Total")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_sub_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                         <asp:TemplateColumn HeaderText="Service Tax">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Service Tax Amount")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Service_Tax" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="Other Total Charges">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Other Total Charges")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Other_Total_Charges" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="FOV Charges">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "FOV Charges")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_FOV" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                        <asp:TemplateColumn HeaderText="Invoice Value">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Total Invoice Value")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_total_Invoice_Value" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                           <asp:TemplateColumn HeaderText="Per KG Weight">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Per Kg Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Per_Kg_Weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                               <ItemStyle HorizontalAlign="Right" />
                               <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                           <asp:TemplateColumn HeaderText="Actual Weight">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Total Actual Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Total_Actual_Weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                               <ItemStyle HorizontalAlign="Right" />
                               <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                           <asp:TemplateColumn HeaderText="DDC Articles">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Total DDC Articles")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Total_DDC_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                               <ItemStyle HorizontalAlign="Right" />
                               <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                         <asp:BoundColumn DataField="Committed Del Date" HeaderText="Committed Del Date"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="MR No" HeaderText="MR No"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="MR Date" HeaderText="MR Date"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Actual Delivery Date &amp; Time" HeaderText="Actual Delivery Date &amp; Time"></asp:BoundColumn>  
                        <asp:BoundColumn DataField="Customer Ref No" HeaderText="Customer Ref No"></asp:BoundColumn>
                          <asp:BoundColumn DataField="Delivery Taken By" HeaderText="Delivery Taken By"></asp:BoundColumn>                       
                           <asp:BoundColumn DataField="Remarks" HeaderText="Remarks"></asp:BoundColumn>                       
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
