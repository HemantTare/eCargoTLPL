<%@ Page AutoEventWireup="true" CodeFile="Frm_Dly_BranchWise_Pending_Stock_GC_AllUndeliveredReason.aspx.cs"
    Inherits="Reports_CL_Nandwana_UserDesk_Frm_Dly_BranchWise_Pending_Stock_GC_AllUndeliveredReason"
    Language="C#" %>

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
    <title>Undelivered Reasons</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Undelivered Reasons"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" >
                    LR No. :&nbsp;
                    <asp:Label ID="lbl_GCNo" runat="server" Text=""></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 80%; height: 15px;">
                    <asp:UpdatePanel ID="Upd_Pnl_DeliveryStockList" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_DeliveryStockList" runat="server" Height="300px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="False" AllowPaging="False" CssClass="GRID"
                                    AllowSorting="True" AllowCustomPaging="False" AutoGenerateColumns="False" PageSize="25">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="Reason" HeaderText="Reason"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="UpdatedBy" HeaderText="Updated By"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Updated_On" HeaderText="Updated On"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="height: 20px" align="center">
                    &nbsp;<asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="Close" OnClick="btn_null_session_Click" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
