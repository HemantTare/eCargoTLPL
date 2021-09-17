<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmMRBookingDeliveryReport.aspx.cs" Inherits="Reports_Booking_FrmMRBookingDeliveryReport" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch" TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker" TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc4" %>

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
    <title>MR Booking/Delivery</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

</head>
<body>
   <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_MRBookingDelivery" runat="server"></asp:ScriptManager>

    <div>
        <table class="TABLE" style="width: 100%">
            <tr>
                <td class="TDGRADIENT" colspan="6">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="MR Booking/Delivery"></asp:Label></td>
            </tr>
        </table>
    
    </div>
        <table id = "tbl_input_screen" style="width: 100%" class = "TABLE">
            <tr>
                <td id="td_lblBranch" runat="server" style="width: 20%">
                    <asp:Label ID="lbl_Branch" runat="server" Text="Branch:" />
                </td>
                <td id="td_BranchData" runat="server" style="width: 29%">
                    <asp:DropDownList ID="ddl_Branch" CssClass="DROPDOWN" AutoPostBack="false"
                        runat="server" />
                </td>
                <td style="width:20%" />
                 <td style="width:29%" />
            </tr>
            <tr>
                <td id="td_lbMRType" runat="server" style="width: 20%">
                    <asp:Label ID="lbl_MRType" runat="server" Text="MR Type:" />
                </td>
                <td id="td_MRTypeData" runat="server" style="width: 29%">
                    <asp:DropDownList ID="ddl_MRtype" CssClass="DROPDOWN" AutoPostBack="false"
                        runat="server" />
                </td>
                 <td style="width:20%; text-align:right;" >
                 <asp:Label ID="lbl_Approval" runat="server" Text="Approval:" />
                 </td>
                 <td style="width:29%">
                 <asp:DropDownList ID="ddl_Approval" runat="server">
                 <asp:ListItem Value="1" Text="Approved"></asp:ListItem>
                 <asp:ListItem Value="0" Text="Not Approved"></asp:ListItem>
                 <asp:ListItem Value="-1" Text="All"></asp:ListItem>
                 </asp:DropDownList>
                 </td>
            </tr>
            
             <tr>
                <td colspan="6">
                    <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                </td>
            </tr>
           
    </table>
       <table class = "TABLE" style="width: 100%">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_View" runat="server" CssClass="BUTTON" Text="View" OnClick="btn_view_Click" /></td>
                <td style="width: 10%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 11%">
                    <a href="javascript:input_screen_action('view');">View Input</a></td>
                <td style="width: 11%">
                    <a href="javascript:input_screen_action('hide');">Hide Input</a></td>
                <td colspan="2">
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>          
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_PaidFreightDetails" UpdateMode="Conditional" runat="server">
                    <Triggers>
                      <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                    </Triggers>
                    <ContentTemplate>
                         <div class="DIV" style="height: 510px; width: 998px;">
                            <asp:DataGrid ID="dg_Grid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" AllowCustomPaging="true"
                                ShowFooter="True" CssClass="GRID" OnItemDataBound="dg_Grid_ItemDataBound" OnPageIndexChanged="dg_Grid_PageIndexChanged">
                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                <Columns>
                                    <asp:BoundColumn DataField="MR No" HeaderText="MR No"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="MR Date" HeaderText="MR Date"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="MR Type" HeaderText="MR Type"></asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="MR Amount">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem,"MR Amount")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_MRAmount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="gc_caption No" HeaderText="gc_caption No"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="gc_caption Date" HeaderText="gc_caption Date"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="BKG Branch" HeaderText="BKG Branch"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="DEL Branch" HeaderText="DEL Branch"></asp:BoundColumn>                              
                                    <asp:TemplateColumn HeaderText="gc_caption Amount">
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "gc_caption Amount")%>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbl_gccaptionAmount" runat="server" CssClass="LABEL" Font-Bold="true" />
                                    </FooterTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>                                                            
                                    <asp:BoundColumn DataField="Consignor" HeaderText="Consignor"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Consignee" HeaderText="Consignee"></asp:BoundColumn>
                                    <asp:BoundColumn DataField="Approved By" HeaderText="Approved By"></asp:BoundColumn>
                               </Columns>
                                <PagerStyle Mode="NumericPages" CssClass="GRIDPAGERCSS" HorizontalAlign="Left" />
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
