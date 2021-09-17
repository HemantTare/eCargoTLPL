<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_GC_Costing_View_Nandwana.aspx.cs" Inherits="Reports_Finance_frm_GC_Costing_View_Nandwana" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>LR Costing Register View</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>

    <script language="javascript" type="text/javascript">
    function CallTrackNTrace(DocType,DocNo)
    {
        var Path='../../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type='+ DocType +'&Doc_No=' + DocNo;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 600;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(Path, 'CustomPopUp23GCCosting', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    </script>

        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td>
                    <table id="tbl_GC_Details" runat="server" class="TABLE" style="background-color: #ccffff"
                        width="100%">
                        <tr style="color: white; background-color: #000080">
                            <td colspan="4">
                                <b>GC Details</b></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table runat="server" class="TABLE" width="100%">
                    <tr>
                        <td>
                            <asp:DataGrid ID="dg_Grid" runat="server" AllowCustomPaging="True" AutoGenerateColumns="False" CssClass="GRID" OnItemDataBound="dg_Grid_ItemDataBound" PageSize="15" ShowHeader="False">
                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                <PagerStyle CssClass="GRIDPAGERCSS" HorizontalAlign="Left" Mode="NumericPages" Visible="False" />
                                <Columns>
                                    <asp:BoundColumn DataField="Description" HeaderText="Description">
                                        <ItemStyle HorizontalAlign="Left" Width="90%" />
                                        <HeaderStyle HorizontalAlign="Left" Width="90%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="Cost" HeaderText="Cost">
                                        <ItemStyle HorizontalAlign="Right" Width="10%" />
                                        <HeaderStyle HorizontalAlign="Right" Width="10%" />
                                    </asp:BoundColumn>
                                </Columns>
                            </asp:DataGrid>
                        </td>
                    </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:BulletedList ID="bl_Notes" runat="server" Font-Bold="true" Font-Names="Verdana" Font-Size="small">
                        <asp:ListItem Text="The Costing is exclusive of Hub, Delivery, Stock Transfer and etc"></asp:ListItem>
                    </asp:BulletedList>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click" Text="Close Window" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
