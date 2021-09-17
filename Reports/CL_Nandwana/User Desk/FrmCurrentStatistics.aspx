<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmCurrentStatistics.aspx.cs"
    Inherits="Reports_CL_Nandwana_User_Desk_FrmCurrentStatistics" %>

<%@ Register Src="../../../CommonControls/WucFilter.ascx" TagName="WucFilter" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
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
function viewwindow_general(DocType,DocNo)
    {
    var Path='../../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type='+ DocType +'&Doc_No=' + DocNo ;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = 500;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
          window.open(Path, 'CustomPopUpGC6767', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
          return false;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Current Statistics</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <table class="TABLE" style="width: 100%">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Current Statistics"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                </td>
            </tr>
            <tr>
                <td>
                    <%--<div class="DIV" style="height: 100%; width: 98%;">--%>
                    <asp:DataGrid ID="dg_GridCurrentStatistics" runat="server" ShowFooter="true" AllowPaging="True"
                        AllowCustomPaging="true" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false"
                        PagerStyle-HorizontalAlign="Left" PageSize="15" Width="100%" OnItemDataBound="dg_GridCurrentStatistics_ItemDataBound">
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" BackColor="Blue" ForeColor="White" />
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" HorizontalAlign="Left" />
                        <Columns>
                            <asp:BoundColumn DataField="BkgDly" >
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="Description" HeaderText="Description">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TotalLR" HeaderText="Total LR">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TotalArticles" HeaderText="Total&lt;br/&gt;Articles">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="TotalFrt" HeaderText="Total Frt">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                    <%--</div>--%>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label><asp:Button
                        ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
