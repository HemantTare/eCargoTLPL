<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Rpt_MR_Register_Reach.aspx.cs" Inherits="Reports_Delivery_Frm_Rpt_MR_Register_Reach" %>

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
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="MR Register"></asp:Label>
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
                <div class="DIV" style="height: 510px; width:976px;">
                  <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                      OnItemDataBound="dg_Grid_ItemDataBound" PageSize="9" AllowCustomPaging="True">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                      <Columns>
                       <asp:TemplateColumn HeaderText="MR No">
                           <ItemTemplate>                    
                          <%# DataBinder.Eval(Container.DataItem, "MR_No_For_Print")%>
                          </ItemTemplate>
                             <FooterTemplate>
                              <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                          </FooterTemplate>
                          </asp:TemplateColumn>
                            <asp:BoundColumn DataField="mr_date" HeaderText="MR Date"></asp:BoundColumn> 
                            
                             <asp:BoundColumn DataField="Bill_No_For_Print" HeaderText="Bill No"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Bill_Date" HeaderText="Bill Date"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="gccaption No" HeaderText="Gc No"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="gccaption Date" HeaderText="gccaption Date"></asp:BoundColumn> 
                       
                          
                        <asp:TemplateColumn HeaderText="Total Gc Amount">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Sub_Total")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Total_Gc_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>                    
                      
                        
                         <asp:TemplateColumn HeaderText="MR Amount">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Total_MR_Amount")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Total_MR_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
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
                         
                         <asp:TemplateColumn HeaderText="Advance Amount">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Advance_Amount")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Advance_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                         <asp:TemplateColumn HeaderText="Cash Amount">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Cash_Amount")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Cash_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                        
                          <asp:TemplateColumn HeaderText="Cheque Amount">
                          <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Cheque_Amount")%>
                          </ItemTemplate>
                          <FooterTemplate>
                          <asp:Label ID="lbl_Cheque_Amount" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                        </FooterTemplate>
                             <ItemStyle HorizontalAlign="Right" />
                             <FooterStyle HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                         <asp:BoundColumn DataField="cheque_no" HeaderText="Cheque No"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Deposit To" HeaderText="Deposit To"></asp:BoundColumn>                                                  
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

