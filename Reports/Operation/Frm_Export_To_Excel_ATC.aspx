<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Export_To_Excel_ATC.aspx.cs"
    Inherits="Reports_Operation_Frm_Export_To_Excel_ATC" %>

<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Export To Excel</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_ExportToExcel" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Export To Excel"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" class="TABLE">
            <tr>
                <td style="width: 20%; text-align: right">
                </td>
                <td style="width: 80%; text-align: left">
                </td>
            </tr>
            <tr>
                <td style="width: 20%; text-align: right;">
                    <asp:Label ID="lbl_DocumentType" runat="server" Text="Document Type :"></asp:Label>
                </td>
                <td style="width: 80%; text-align: left;">
                     
                            <asp:DropDownList ID="ddl_DocumentType" runat="server" CssClass="DROPDOWN" AutoPostBack="True" Width="200px" OnSelectedIndexChanged="ddl_DocumentType_SelectedIndexChanged" >
                            </asp:DropDownList>
                       
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 17px; text-align: right">
                </td>
                <td style="width: 80%; height: 17px; text-align: left">
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 17px; text-align: center">
                    <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 17px; text-align: right">
                </td>
                <td style="width: 80%; height: 17px; text-align: left">
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 17px; text-align: right">
                </td>
                <td style="width: 80%; height: 17px; text-align: left">
                    </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: left">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center" colspan="2" id="td_ExpToExl"  runat="server" >
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label></td>
            </tr>
        </table>
    </form>

    <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>

</body>
</html>
