<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_LRStatus.aspx.cs" Inherits="Reports_CL_Nandwana_User_Desk_Frm_LRStatus" %>

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
        <td class="TDGRADIENT" style="width: 100%;">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="LR Status"></asp:Label>
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
        <td style="width:50%;">
            <uc3:Wuc_GC_Parameters ID="Wuc_GC_Parameters1" runat="server" />
        </td>
      </tr>
      <tr>
            <td style="width: 100%">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 10%; " class="TD1" id="Division" runat="server">
                            <asp:Label ID="lbl_division" runat="server" CssClass="LABEL"></asp:Label></td>
                        <td style="width: 24%;"><uc5:WucDivisions ID="WucDivisions1" runat="server" /></td>
                        <td style="width: 9%;" class="TD1">LR No:</td>
                        <td style="width: 24%;"><asp:TextBox ID="Txt_LR_No" runat="server" CssClass="TEXTBOX"></asp:TextBox></td>
                        <td style="width: 9%;" class="TD1"></td>
                        <td style="width: 24%;"></td>
                    </tr>
                </table>
            </td>
        </tr>
      <tr>
        <td style="width:50%;">
            <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
        </td>
      </tr>  
       
    
    </table>
<tr>
        <td style="width: 50%">
        </td>
    </tr>
    <table class="TABLE">
        
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
              <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label><asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
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
                <div class="DIV" style="height: 510px; width:998px;">
                  <asp:DataGrid ID="dg_Grid"  runat="server" ShowFooter="true" AllowPaging="True" CssClass="GRID" AllowSorting="True"
                      AutoGenerateColumns="false" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                      OnItemDataBound="dg_Grid_ItemDataBound" PagerStyle-HorizontalAlign="Left" 
                      PageSize="9" PagerStyle-Mode="NumericPages">

                      <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                      <HeaderStyle CssClass="GRIDHEADERCSS" />
                      <FooterStyle CssClass="GRIDFOOTERCSS" />
                      <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                      <Columns>
                        <asp:BoundColumn DataField="gc_caption No" HeaderText="gc_caption No"></asp:BoundColumn>                                                 
                        <asp:BoundColumn DataField="gc_caption Date" HeaderText="gc_caption Date"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Booking Branch" HeaderText="Booking Branch"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Delivery Branch" HeaderText="Delivery Branch"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Booking Type" HeaderText="Booking Type"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Delivery Type" HeaderText="Delivery Type"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Packing Type" HeaderText="Packing Type"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Consignor Name" HeaderText="Consignor Name"></asp:BoundColumn>
                        <asp:BoundColumn DataField="Consignee Name" HeaderText="Consignee Name"></asp:BoundColumn>                         
                         
                        <asp:BoundColumn DataField="Current Status" HeaderText="Current Status"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Total Articles" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>                    
                                <%# DataBinder.Eval(Container.DataItem, "Total Articles")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_Articles" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateColumn>                       
                        <asp:TemplateColumn HeaderText="Total Weight" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right">
                            <ItemTemplate>                    
                                <%# DataBinder.Eval(Container.DataItem, "Total Weight")%>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbl_ActualWeight" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="Current Branch" HeaderText="Current Branch"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Current Document No" HeaderText="Current Document No"></asp:BoundColumn>
                                          
                                               <%-- <asp:BoundColumn DataField="Vehicle No" HeaderText="Vehicle No" Visible="false"></asp:BoundColumn> --%>

                        <asp:BoundColumn DataField="Payment Type" HeaderText="Payment Type"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Payment Status" HeaderText="Payment Status"></asp:BoundColumn> 
                        <asp:BoundColumn DataField="Payment Ref No" HeaderText="Payment Ref No"></asp:BoundColumn> 
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
