<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Branchwise_Booking_Register_Nandwana.aspx.cs" Inherits="Reports_CL_Nandwana_Booking_Frm_Branchwise_Booking_Register_Nandwana" %>
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
function TABLE2_onclick() {

}

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Branch Wise Booking Register View</title>
    <link href="~/CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    

    <form id="form1" runat="server">
<asp:ScriptManager ID="scm_BookingRegister" runat="server"></asp:ScriptManager>
    <table runat="server" id="Table1" class="TABLE" onclick="rr()">
     <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Branch wise Booking Register"></asp:Label>
        </td>
      </tr>
    </table>          
    <table runat="server" id="tbl_input_screen" class="TABLE"> 
      <tr>
        <td style="width:50%">
            <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
        </td>
      </tr>
      <tr>
        <td style="width:50%">
            <uc3:Wuc_GC_Parameters ID="Wuc_GC_Parameters1" runat="server" />
        </td>
      </tr>
      <tr>
        <td style="width:50%">
            <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
            </td>
      </tr>
    <tr>
            <td style="width: 50%">
            <table style="width: 100%">
                <tr>
                    <td style="width: 10%; " class="TD1">
                     <asp:label ID="lbl_division" runat="server" CssClass="LABEL" Text="Label"/></td>
                    <td style="width: 24%;"><uc5:WucDivisions ID="WucDivisions1" runat="server" /></td>
                    <td style="width: 10%;" class="TD1">Client Name:</td>
                       <td style="width: 24%;">
                           <asp:TextBox ID="Txt_Client" runat="server" CssClass="TEXTBOX"></asp:TextBox></td>
                    <td colspan="2" >
                        &nbsp;</td>
                </tr>
            </table>
        </td>
    </tr>   
    <tr>
     <td style="width: 50%">
            <table style="width: 100%">
            <tr>
              <td class="TD1" style="width: 20%">
                 </td>
                <td style="width: 80%">
                     <asp:RadioButtonList ID="rbl_Detailed_Summary" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbl_Detailed_Summary_SelectedIndexChanged">
                        <asp:ListItem Value="0" Selected = "true">Detailed</asp:ListItem>
                        <asp:ListItem Value="1">Summary</asp:ListItem>
                    </asp:RadioButtonList> </td>               
                </tr>
                </table>
                </td>
    </tr>       
    </table>
    <table class="TABLE" id="TABLE2" onclick="return TABLE2_onclick()">
      <tr>
            <td style="width:10%">
              <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" />
            </td>
            <td style="width:10%">
              <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
            </td>

            <td style="width:10%">
              <a href="javascript:input_screen_action('view');">View Input</a>
            </td>

            <td style="width:10%">
              <a href="javascript:input_screen_action('hide');">Hide Input</a>
            </td>     
                   
            <td style="width:50%">
              <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
               <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" />
              </td>
      </tr>
    </table>


    <table class="TABLE">
        <tr>
            <td>
                <div class="DIV" style="height: 510px; width: 976px;">
                    <table>
                        <tr>
                            <td>
            <asp:UpdatePanel ID="Upd_Pnl_BookingRegister" UpdateMode="Conditional" runat="server">
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
              <ContentTemplate>                
                  <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="true"
                      AllowPaging="True"  CssClass="GRID" PagerStyle-Mode="NumericPages" AllowSorting="True"
                      AutoGenerateColumns="false" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                      OnItemDataBound="dg_Grid_ItemDataBound" PagerStyle-HorizontalAlign="Left" PageSize="9">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                      <Columns>
                       <asp:TemplateColumn HeaderText="CN No">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "CN No")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                     <asp:BoundColumn DataField="CN Date" HeaderText="CN Date"></asp:BoundColumn>
                     <asp:BoundColumn DataField="Consignor" HeaderText="Consignor"></asp:BoundColumn> 
                          <asp:BoundColumn DataField="Consignee" HeaderText="Consignee"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="From Branch" HeaderText="From Branch"></asp:BoundColumn>
                          <asp:BoundColumn DataField="To Location" HeaderText="To Location"></asp:BoundColumn> 
                          <asp:BoundColumn DataField="Booking Type" HeaderText="Booking Type"></asp:BoundColumn>
                          <asp:BoundColumn DataField="Item" HeaderText="Item"></asp:BoundColumn> 
                          <asp:TemplateColumn HeaderText="Articles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Articles")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>   
                                            
                      <asp:TemplateColumn HeaderText="Charged Weight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" >
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Charged Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_ChargedWeight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Actual Weight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Actual Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_ActualWeight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                   
                      <asp:TemplateColumn HeaderText="Freight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Freight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_Freight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                       <asp:BoundColumn DataField="Vehicle No" HeaderText="Vehicle No"></asp:BoundColumn>
                    
                      <asp:BoundColumn DataField="LHS NO" HeaderText="LHS NO"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="LHS Date" HeaderText="LHS Date"></asp:BoundColumn> 
                     
                      <asp:TemplateColumn HeaderText="Lorry Hire Charge" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Lorry Hire Charge")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_Lorry_Hire_Charge" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:BoundColumn DataField="Door Delivery Date" HeaderText="Door Delivery Date"></asp:BoundColumn>
                      <asp:BoundColumn DataField="POD Status" HeaderText="POD Status"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Billing Party Name" HeaderText="Billing Party"></asp:BoundColumn> 
                       <asp:BoundColumn DataField="Billing Location" HeaderText="Billing Location"></asp:BoundColumn> 
                      <asp:BoundColumn DataField="Bill No" HeaderText="Bill No"></asp:BoundColumn>
                      <asp:BoundColumn DataField="Bill Date" HeaderText="Bill Date"></asp:BoundColumn>
                      <asp:BoundColumn DataField="Booking MR No" HeaderText="Booking MR No"></asp:BoundColumn>
                      <asp:BoundColumn DataField="Booking MR Date" HeaderText="Booking MR Date"></asp:BoundColumn>
                      <asp:BoundColumn DataField="Delivery MR No" HeaderText="Delivery MR No"></asp:BoundColumn>
                      <asp:BoundColumn DataField="Delivery MR Date" HeaderText="Delivery MR Date"></asp:BoundColumn>
                      
                      <%--<asp:BoundColumn DataField="From Location" HeaderText="From Location"></asp:BoundColumn>--%>
                       <asp:TemplateColumn HeaderText="All Other Charges" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "All Other Charges")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_AllOtherCharges" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                       <asp:TemplateColumn HeaderText="Service Tax Amount" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Service Tax Amount")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_ServiceTaxAmount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                       <asp:TemplateColumn HeaderText="Total Freight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Total Freight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_TotalFreight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      </Columns>
                  </asp:DataGrid>           
              </ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>
      <tr>
        <td>
          <asp:UpdatePanel ID="Upd_Pnl_BookingREgisterSummary" UpdateMode="Conditional" runat="server">
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_GridSummary" />
              </Triggers>
              <ContentTemplate>                
                  <asp:DataGrid ID="dg_GridSummary"  runat="server" ShowFooter="true"
                      AllowPaging="True"  CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="false" OnPageIndexChanged="dg_GridSummary_PageIndexChanged"
                      OnItemDataBound="dg_GridSummary_ItemDataBound" PagerStyle-HorizontalAlign="Left" PageSize="9">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                      <Columns>
                       <asp:TemplateColumn HeaderText="CN No">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "CN No")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                     <asp:BoundColumn DataField="CN Date" HeaderText="CN Date"></asp:BoundColumn>
                           <asp:BoundColumn DataField="Consignor" HeaderText="Consignor"></asp:BoundColumn> 
                          <asp:BoundColumn DataField="Consignee" HeaderText="Consignee"></asp:BoundColumn>  
                           <asp:BoundColumn DataField="Billing Party Name" HeaderText="Billing Party"></asp:BoundColumn>                    
                          <asp:BoundColumn DataField="From Branch" HeaderText="From Branch"></asp:BoundColumn> 
                          <asp:BoundColumn DataField="To Location" HeaderText="To Location"></asp:BoundColumn> 
                          <asp:BoundColumn DataField="Booking Type" HeaderText="Booking Type"></asp:BoundColumn>
                          <asp:BoundColumn DataField="Item" HeaderText="Item"></asp:BoundColumn> 
                          
                          <asp:TemplateColumn HeaderText="Articles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Articles")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>   
                                            
                      <asp:TemplateColumn HeaderText="Charged Weight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right" >
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Charged Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_ChargedWeight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Actual Weight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                          <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "Actual Weight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_ActualWeight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                     
                       <asp:TemplateColumn HeaderText="Total Freight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                       <ItemTemplate >                    
                          <%# DataBinder.Eval(Container.DataItem, "Total Freight")%>
                          </ItemTemplate>
                          <FooterTemplate>
                              <asp:Label ID="lbl_TotalFreight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                      </asp:TemplateColumn>
                      </Columns>
                  </asp:DataGrid>            
              </ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>                   
  </table>
  </div>
  </td>
  </tr>
  </table>
    </form>
</body>
</html>

