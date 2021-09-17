<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDaliyChequeReceived.aspx.cs"
    Inherits="Finance_Reports_FrmDaliyChequeReceived" %>

<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
function Open_Details_Window(Path)
{ 
  window.open(Path,'BkgReg','width=1000,height=800,top=0,left=0,menubar=no,resizable=yes,scrollbars=yes')
  return false;
} 
 
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Daliy Cheque Received</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Daliy Cheque Received"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 50%">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 10%; height: 41px;" class="TD1">
                            </td>
                            <td style="width: 24%; height: 41px;">
                            </td>
                            <td colspan="4" style="height: 41px">
                                <asp:RadioButtonList ID="rdl_ReportType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Selected="True">Branch-Wise</asp:ListItem>
                                    <asp:ListItem Value="1">Date-Wise</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 50%" align="right">
                    <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" /></td>
                <td style="width: 50%;">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                </td>
            </tr>
        </table>

        <script type="text/javascript">
    
        self.parent.hideload();
    
        </script>

    </form>
</body>
</html>
