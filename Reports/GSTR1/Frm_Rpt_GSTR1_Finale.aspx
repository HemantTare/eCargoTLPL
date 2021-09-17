<%@ Page AutoEventWireup="true" CodeFile="Frm_Rpt_GSTR1_Finale.aspx.cs" Inherits="Reports_GSTR1_Frm_Rpt_GSTR1_Finale"
    Language="C#" %>

<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
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


</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>GSTR 1 - Finale</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="GSTR 1 - Finale"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                </td>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 100%" colspan="2" align="center">
                    <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Text="Note : Prepaire These Finale Sheets Only After Varification Of All The GSTIN In B2B Sheet."
                        Font-Bold="true" ForeColor="Red" />
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                </td>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 100%" colspan="2">
                    State :
                    <asp:DropDownList ID="ddl_State" runat="server" AutoPostBack="true" CssClass="DROPDOWN" Width="20%">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                </td>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 50%;" colspan="2" align="center">
                    <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server"></uc2:Wuc_From_To_Datepicker>
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                </td>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 100%" colspan="2" align="center">
                    <asp:Button ID="btn_ExportToExcel_B2B" runat="server" CssClass="BUTTON" OnClick="btn_ExportToExcelB2B_Click"
                        BackColor="#99ff66" Font-Bold="true" Text="Export B2B" />
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                </td>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 50%; height: 15px;" align="center">
                    <asp:Button ID="btn_ExportToExcel_B2C" runat="server" CssClass="BUTTON" OnClick="btn_ExportToExcelB2C_Click"
                        BackColor="#ffcc66" Font-Bold="true" Text="Export B2C" /></td>
                <td style="width: 50%; height: 15px;" align="center">
                    <asp:Button ID="btn_ExportToExcel_Exempt" runat="server" CssClass="BUTTON" OnClick="btn_ExportToExcelExempt_Click"
                        BackColor="#66ffff" Font-Bold="true" Text="Export Exempt" /></td>
            </tr>
            <tr>
                <td style="width: 50%">
                </td>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 50%; height: 15px;" align="center">
                    <asp:Button ID="btn_ExportToExcel_HSN" runat="server" CssClass="BUTTON" OnClick="btn_ExportToExcelHSN_Click"
                        BackColor="#ffff99" Font-Bold="true" Text="Export HSN" /></td>
                <td style="width: 50%; height: 15px;" align="center">
                    <asp:Button ID="btn_ExportToExcel_Series" runat="server" CssClass="BUTTON" OnClick="btn_ExportToExcelSeries_Click"
                        BackColor="#ff99ff" Font-Bold="true" Text="Export Document Series" /></td>
            </tr>
            <tr>
                <td style="width: 50%">
                </td>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                </td>
                <td style="width: 50%">
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
