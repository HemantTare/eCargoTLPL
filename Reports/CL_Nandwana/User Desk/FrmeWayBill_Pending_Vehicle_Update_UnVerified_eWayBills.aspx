<%@ Page AutoEventWireup="true" CodeFile="FrmeWayBill_Pending_Vehicle_Update_UnVerified_eWayBills.aspx.cs"
    Inherits="Reports_CL_Nandwana_UserDesk_FrmeWayBill_Pending_Vehicle_Update_UnVerified_eWayBills"
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

function viewwindow_general(GC_ID)
{
 
        var Path='../../../Operations/Booking/NewGC/FrmGCNew.aspx?Menu_Item_Id=MwAwAA==&Mode=NAA=&Id=' + GC_ID;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 1000;
        var popH = 800;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUpUnVerifiedeWaybillGC', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_eWayBillVerify()
{
 
        var Path='../../../Reports/Booking/frm_eWayBillVerification.aspx';
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 1000;
        var popH = 800;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUpUneWayBillVerification', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>UnVerified eWay Bills</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="UnVerified eWay Bills"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 24%; height: 10px;" colspan="6" align="center">
                                <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                                    Text="Close Window" /></td>
                        </tr>
                        <tr>
                            <td class="TD1" colspan="6" style="text-align: left">
                                <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_DeliveryStockList" UpdateMode="Conditional" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_DeliveryStockList" runat="server" Height="290px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Details" runat="server" ShowFooter="False" AllowPaging="false"
                                    CssClass="GRID" AllowSorting="True" AllowCustomPaging="false" AutoGenerateColumns="False"
                                    OnPageIndexChanged="dg_Details_PageIndexChanged" PageSize="100" OnItemDataBound="dg_Details_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="LR Date" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "GC_Date")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="LR No." HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_GC_No" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "GC_No") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="eWay Bill No." HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "eWayBillNo")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" Font-Bold="true" ForeColor="Red" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Unverified Reason" HeaderStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "UnVerifiedReason")%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk_Verify" runat="server" CssClass="LABEL" Text='Verify' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
