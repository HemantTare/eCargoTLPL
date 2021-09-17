<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmRptLossClientMonthWise.aspx.cs"
    Inherits="Reports_Booking_FrmRptLossClientMonthWise" %>

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>

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
<head runat="server">
    <title>Loss Client Month Wise</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Loss Client Month Wise"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 50%; height: 41px;">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 10%; height: 41px;" class="TD1">
                                <asp:Label ID="lblCity" runat="server" CssClass="LABEL" Text="City :" /></td>
                            
                            <td style="width: 24%; height: 41px;">
                                &nbsp;<asp:DropDownList ID="ddl_City" runat="server">
                                </asp:DropDownList></td>

                            <td style="width: 10%; height: 41px;" class="TD1">
                                <asp:Label ID="lblMonth" runat="server" CssClass="LABEL" Text="Month :" /></td>
                            
                            <td style="width: 24%; height: 41px;">
                                &nbsp;<asp:DropDownList ID="ddl_Month" runat="server">
                                </asp:DropDownList></td>                            
                            <td colspan="4" style="height: 41px">
                                <asp:Label ID="lblYear" runat="server" CssClass="LABEL" Text="Year :" />&nbsp;<asp:DropDownList ID="ddl_Year" runat="server">
                                </asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%">
                    <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" />
                </td>
                <td style="width: 10%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('view');">View Input</a>
                </td>
                <td style="width: 10%">
                    <a href="javascript:input_screen_action('hide');">Hide Input</a>
                </td>
                <td style="width: 50%">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
        
         <table class="TABLE">
            <tr>
                <td>
                    <div class="DIV" style="height: 510px; width: 976px;">
                        <table>
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="Upd_Pnl_PaidFreightDetails" UpdateMode="Conditional" runat="server">
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                                        </Triggers>
                                        <ContentTemplate>
                                            <asp:DataGrid ID="dg_Grid" runat="server" AllowPaging="True" AllowSorting="True"
                                                AutoGenerateColumns="False" AllowCustomPaging="true" ShowFooter="True" CssClass="GRID"
                                                OnItemDataBound="dg_Grid_ItemDataBound" OnPageIndexChanged="dg_Grid_PageIndexChanged" PageSize="25">
                                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                <FooterStyle CssClass="GRIDFOOTERCSS" />

                                                <Columns>
                                                    <asp:BoundColumn DataField="Mobile_No" HeaderText="MobileNo"></asp:BoundColumn> 

                                                    <asp:BoundColumn DataField="Client_Name" HeaderText="Client Name"></asp:BoundColumn> 
                                                    <asp:BoundColumn DataField="DeliveryArea" HeaderText="Delivery Area"></asp:BoundColumn> 
                                                    <asp:BoundColumn DataField="TotArt" HeaderText="Total Art"></asp:BoundColumn> 
                                                    <asp:BoundColumn DataField="LRCount" HeaderText="Total LR"></asp:BoundColumn> 
                                                    <asp:BoundColumn DataField="Average" HeaderText="Average"></asp:BoundColumn> 
                                                    <asp:BoundColumn DataField="CurrentMonthLR" HeaderText="CurrentMonth"></asp:BoundColumn> 
                                                                
                                                </Columns>

                                                <PagerStyle Mode="NumericPages" CssClass="GRIDPAGERCSS" HorizontalAlign="Left" />
                                            </asp:DataGrid>
                                            <%--</asp:Panel>--%>
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
