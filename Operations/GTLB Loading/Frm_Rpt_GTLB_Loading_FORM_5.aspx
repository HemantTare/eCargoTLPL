<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Rpt_GTLB_Loading_FORM_5.aspx.cs"
    Inherits="Operations_GTLB_Loading_Frm_Rpt_GTLB_Loading_FORM_5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
function Open_FORM5_Window(Path)
{ 
  window.open(Path,'GTLBFORM5','width=1000,height=800,top=50,left=50,menubar=no,resizable=yes,scrollbars=yes');
  return false;
}    

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>FORM 5</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 5px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="FORM 5"></asp:Label>
                </td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 10%; height: 41px;" class="TD1">
                                <asp:Label ID="lblMonth" runat="server" CssClass="LABEL" Text="Month :" /></td>
                            <td style="width: 24%; height: 41px;">
                                &nbsp;<asp:DropDownList ID="ddl_Month" runat="server" Width="50%">
                                </asp:DropDownList></td>
                            <td style="width: 10%; height: 41px;" class="TD1">
                                <asp:Label ID="lblYear" runat="server" CssClass="LABEL" Text="Year :" /></td>
                            <td style="width: 24%; height: 41px;">
                                &nbsp;<asp:DropDownList ID="ddl_Year" runat="server" Width="50%">
                                </asp:DropDownList></td>
                            <td colspan="4" style="height: 41px">
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 10%">
                    &nbsp;</td>
                <td style="width: 10%">
                    &nbsp;</td>
                <td style="width: 10%">
                    &nbsp;
                </td>
                <td style="width: 10%">
                    &nbsp;
                <asp:Button ID="btn_Print" CssClass="BUTTON" runat="server" Text="Print FORM 5"
                        OnClick="btn_Print_Click" /></td>
                <td style="width: 50%">
                    &nbsp;
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label></td>
            </tr>
        </table>
    </form>

    <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>

</body>
</html>
