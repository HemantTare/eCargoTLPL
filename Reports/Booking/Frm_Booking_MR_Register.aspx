<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Booking_MR_Register.aspx.cs" Inherits="Reports_Booking_Frm_Booking_MR_Register" %>
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
       <asp:ScriptManager ID="scm_Truck_Unloading" runat="server"></asp:ScriptManager>

    <table runat="server" id="Table1" class="TABLE">
     <tr>
        <td class="TDGRADIENT" style="width: 100%">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Booking MR Register"></asp:Label>
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
            <td style="width: 50%">
                <uc6:WucFilter ID="WucFilter1" runat="Server" />
            </td>
        </tr>
      
        <tr>
            <td style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 10%; " class="TD1">
                        <asp:label ID="lbl_division" runat="server" CssClass="LABEL" Text="label"/></td>
                        <td style="width: 24%;">
                           <uc5:WucDivisions ID="WucDivisions1" runat="server" />
                        </td>
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
         <asp:UpdatePanel ID="Upd_Pnl_BookingRegister" UpdateMode="Conditional" runat="server">
              <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
              </Triggers>
              <ContentTemplate>
                 <%--<asp:Panel ID="pnl_BookingRegister" runat="server" ScrollBars="Auto" Height="500px">--%>
                     <div class="DIV" style="height: 510px; width:976px;">
                     <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged" AllowCustomPaging="true" 
                      OnItemDataBound="dg_Grid_ItemDataBound" PageSize="9">

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
                        <asp:BoundColumn DataField="Payment Type" HeaderText="Payment Type"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Consignee" HeaderText="Consignee"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Address1" HeaderText="Address1"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Address2" HeaderText="Address2"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="City" HeaderText="City"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Pincode" HeaderText="Pincode"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Email" HeaderText="Email ID"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Mobile" HeaderText="Mobile No"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Phone" HeaderText="Phone No"></asp:BoundColumn> 
                          
                        <asp:TemplateColumn HeaderText="Articles" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Total Articles")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>                    
                      
                        
                         <asp:TemplateColumn HeaderText="Charged Weight" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Charged Wt")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Charged_Weight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        
                         <asp:TemplateColumn HeaderText="gc_caption Amount" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "gc_caption Amount")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Gc_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                         
                         <asp:TemplateColumn HeaderText="Octroi Amount" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Octroi Amount")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Octroi_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        
                         <asp:TemplateColumn HeaderText="Service Tax" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Service Tax")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Service_Tax" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                         <asp:BoundColumn DataField="Arrival Date" HeaderText="Arrival Date"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="MR No" HeaderText="MR No"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="MR Date" HeaderText="MR Date"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="MR Branch" HeaderText="MR Branch"></asp:BoundColumn>  
                        
                        
                         <asp:TemplateColumn HeaderText="Demurrage Days" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Demurrage Days")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Demurrage_Days" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        
                         <asp:TemplateColumn HeaderText="Demurrage Charges" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Demurrage Charges")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Demurrage_Charges" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        
                         <asp:TemplateColumn HeaderText="Octroi Form Charge" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Octroi Form Charge")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Octroi_Form_Charge" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                        
                         <asp:TemplateColumn HeaderText="Octroi Service Charge" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Octroi Service Charge")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Octroi_Service_Charge" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="Detention Charges" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Detention Charges")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Detention_Charges" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                        </asp:TemplateColumn>                       
                         </Columns>
                  </asp:DataGrid>
                     </div>
        <%--       </asp:Panel>--%>
              </ContentTemplate>
          </asp:UpdatePanel>
        </td>
      </tr>
  </table>
    </form>
</body>
</html>
