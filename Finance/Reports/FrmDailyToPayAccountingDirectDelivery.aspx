<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDailyToPayAccountingDirectDelivery.aspx.cs"
    Inherits="Finance_Reports_FrmDailyToPayAccountingDirectDelivery" %>

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%--<%@ Register Src="../../CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters"
    TagPrefix="uc3" %>--%>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">

function hideload()
{
    var loading = document.getElementById('loading');     
    loading.style.visibility = 'hidden'; 
}

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
 
function Allow_To_Save()
{ 
}
 
function viewwindow_general(Region_Id,Area_Id,Branch_Id,Dest_Branch_ID,DirectToPay,AsOnDate,RegionText,AreaText,BranchText)
{  
        var Path='../../Finance/Reports/FrmDailyToPayAccountingBranchDtls.aspx?Region_Id=' + Region_Id + '&Area_Id=' + Area_Id  + '&Branch_ID=' + Branch_Id + '&Dest_Branch_ID=' + Dest_Branch_ID + '&DirectToPay=' + DirectToPay + '&AsOnDate=' + AsOnDate + '&RegionText=' + RegionText + '&AreaText=' + AreaText + '&BranchText=' + BranchText;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
       
        window.open(Path, 'ToPayAccounting', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daily ToPay Accounting - Direct Delivery</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Daily ToPay Accounting - Direct Delivery"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="TABLE" id="tab_DailyToPayAccounting" runat="server">
            <tr>
                <td style="width: 10%; height: 15px; text-align: right">
                </td>
                <td style="width: 10%; height: 15px">
                </td>
                <td style="width: 10%; height: 15px">
                </td>
                <td style="width: 10%; height: 15px">
                </td>
                <td style="width: 50%; height: 15px">
                </td>
            </tr>
            <tr id="tr_RegionAreaBranch" runat="server">
                <td colspan="5" style="text-align: right">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right;">
                    <asp:Label ID="lbl_AsOnDate" runat="server" Text="Date :"></asp:Label></td>
                <td style="width: 10%">
                    <uc2:WucDatePicker ID="Dtp_AsOnDate" runat="server" />
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 10%; height: 15px; text-align: right">
                    &nbsp;</td>
                <td style="width: 10%; height: 15px">
                    &nbsp;</td>
                <td style="width: 10%; height: 15px">
                    &nbsp;</td>
                <td style="width: 10%; height: 15px">
                    &nbsp;</td>
                <td style="width: 50%; height: 15px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 10%">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
        <table class="TABLE" id="tab_DailyBookingStatement" runat="server">
            <tr>
                <td style="width: 16%; text-align: right;">
                    <asp:Label ID="lblRegion" runat="server" Text="Region :" Font-Bold="true"></asp:Label></td>
                <td style="width: 16%; text-align: left;">
                    <asp:Label ID="txtRegion" runat="server" Font-Bold="true"></asp:Label></td>
                <td style="width: 16%; text-align: right;">
                    <asp:Label ID="lblArea" runat="server" Text="Area :" Font-Bold="true"></asp:Label>
                </td>
                <td style="width: 16%; text-align: left;">
                    <asp:Label ID="txtArea" runat="server" Font-Bold="true"></asp:Label></td>
                <td style="width: 16%; text-align: right;">
                    <asp:Label ID="lblBranch" runat="server" Text="Branch :" Font-Bold="true"></asp:Label>
                </td>
                <td style="width: 16%; text-align: left;">
                    <asp:Label ID="txtBranch" runat="server" Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 16%; text-align: right;">
                    <asp:Label ID="lblAsOnDate" runat="server" Text="As On Date :" Font-Bold="true"></asp:Label></td>
                <td style="width: 16%; text-align: left;">
                    <asp:Label ID="txtAsonDate" runat="server" Font-Bold="true"></asp:Label></td>
                <td style="width: 16%">
                </td>
                <td style="width: 16%">
                </td>
                <td style="width: 16%">
                </td>
                <td style="width: 16%">
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="text-align: left;">
                    <asp:UpdatePanel ID="Upd_Pnl_BookingRegister" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_view" />
                            <asp:AsyncPostBackTrigger ControlID="dg_GridToPay" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV" style="height: 510px; width: 100%;">
                                <asp:Label ID="lbl_Paid" runat="server" Font-Bold="True" Visible="False" Font-Size="Large"
                                    Text="      To Pay      " BackColor="SlateGray" ForeColor="PowderBlue" Width="99%"></asp:Label>
                                <asp:DataGrid ID="dg_GridToPay" runat="server" ShowFooter="true" AllowPaging="false"
                                    AllowCustomPaging="false" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false"
                                    OnPageIndexChanged="dg_GridToPay_PageIndexChanged" OnItemDataBound="dg_GridToPay_ItemDataBound"
                                    Width="80%">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="ToPayFRT">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="ServiceTax">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="RoundOff">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Total">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                                <hr />
                                <asp:Label ID="lblToPay" runat="server" Font-Bold="True" Visible="False" Font-Size="Large"
                                    Text="      Today's Dispatch (To Pay) - Direct Delivery    " BackColor="SlateGray"
                                    ForeColor="PowderBlue" Width="99%"></asp:Label>
                                <asp:DataGrid ID="dg_GridDispatchToPay" runat="server" ShowFooter="true" AllowPaging="false"
                                    AllowCustomPaging="false" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false"
                                    OnItemDataBound="dg_GridDispatchToPay_ItemDataBound" Width="80%">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="Dest_Branch_Name" HeaderText="Destination">
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <%--                                        <asp:BoundColumn DataField="CrossingToPay" HeaderText="Crossing">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>--%>
                                        <%--<asp:BoundColumn DataField="TotalToPay" HeaderText="Total">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>--%>
                                        <asp:TemplateColumn HeaderText="DirectToPay" ItemStyle-HorizontalAlign="Right" FooterStyle-HorizontalAlign="Right"
                                            HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdn_Dest_Branch_ID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Dest_Branch_ID")%>' />
                                                <asp:LinkButton ID="lbtn_DirectToPay" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "DirectToPay") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
            <ProgressTemplate>
                <div style="position: absolute; bottom: 50%; left: 50%; font-size: 11px; font-family: Verdana;
                    z-index: 100">
                    <span id="ajaxloading">
                        <table>
                            <tr>
                                <td>
                                    <asp:Image ID="Ajax_Image_ID" runat="server" ImageUrl="~/images/ajax-loader-Squares.gif" /></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    Wait! Action in Progress...</td>
                            </tr>
                        </table>
                    </span>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
    </form>
</body>
</html>
