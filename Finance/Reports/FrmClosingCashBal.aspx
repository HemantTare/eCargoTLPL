<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmClosingCashBal.aspx.cs"
    Inherits="Finance_Reports_FrmClosingCashBal" %>

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
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

 function viewwindow(DetailType,BranchID,AsOnDate)     //For Delivery Details
    {
        var Path='FrmClosingCashBalDetails.aspx?DetailType=' + DetailType + '&BranchID='+ BranchID + '&AsOnDate=' + AsOnDate;

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 400;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
          window.open(Path, 'CustomPopUp_Track_And_Traceddc', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
          return false;
    }

 function viewwindowDlyStkList(DetailType,RegionID,AreaID,BranchID,AsOnDate,DivisionId,DeliveryAreaID,RegionText,AreaText,BranchText,DlyArea)     //For Delivery Details
    {
        var Path='../../Reports/Booking/FrmDeliveryAreaDetails.aspx?DetailType=' + DetailType + '&Region_Id='+ RegionID + '&Area_Id='+ AreaID + '&Branch_Id='+ BranchID + '&As_On_Date=' + AsOnDate + '&Division_Id=' + DivisionId + '&DeliveryAreaID=' + DeliveryAreaID + '&RegionText=' + RegionText + '&AreaText=' + AreaText + '&BranchText=' + BranchText + '&DlyArea=' + DlyArea;

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-50);
        var popH = 1000;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
           
                    
          window.open(Path, 'CustomPopUp_Track_And_Traceddc', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
          return false;
    }

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Closing Cash Balance</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
      .SHOWSELECTEDLINK{FONT-SIZE: 11px;FONT-FAMILY: Verdana;color:#0033ff;text-decoration:underline;}
</style>
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_ClosingCashBal" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Closing Cash Balance"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 12%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 12%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 12%; text-align: right;">
                    <asp:Label ID="lbl_SelectDate" runat="server" Text="Select Date : "></asp:Label></td>
                <td style="width: 10%">
                    <uc2:WucDatePicker ID="Dtp_AsOnDate" runat="server"></uc2:WucDatePicker>
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 10%">
                </td>
                <td style="width: 50%">
                </td>
            </tr>
        </table>
        <table class="TABLE">
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
        <table class="TABLE">
            <tr>
                <td style="text-align: left;">
                    <asp:UpdatePanel ID="Upd_ClosingCashBal" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_View" />
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV" style="height: 600px; width: 100%;">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" CssClass="GRID" AllowSorting="True"
                                    AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged" OnItemDataBound="dg_Grid_ItemDataBound"
                                    Width="50%">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="Description" HeaderText="Description">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Details" HeaderText="Details">
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="Amount" HeaderText="Amount">
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
