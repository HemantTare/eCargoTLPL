<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Branch_Vasuli_Hisab.aspx.cs"
    Inherits="Reports_Booking_Inward_Frm_Branch_Vasuli_Hisab" %>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc4" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="~/CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters"
    TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
 
function Open_Details_Window(Path)
{ 
  window.open(Path,'BkgReg','width=1000,height=800,top=0,left=0,menubar=no,resizable=yes,scrollbars=yes')
  return false;
} 
 
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title runat="server"></title>
    <link href="~/CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_booking_dest_wise" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td class="TDGRADIENT" colspan="5">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Branch Vasuli Hisab" ></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 21px;">
                </td>
                <td style="width: 20%; height: 21px;">
                </td>
                <td style="width: 20%; height: 21px;">
                </td>
                <td style="width: 20%; height: 21px;">
                </td>
                <td style="width: 20%; height: 21px;">
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="5" style="height: 20px">
                </td>
            </tr>
            <tr>
                <td colspan="5"> 
                <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                    &nbsp;</td>
            </tr>
            

            
            <tr>
                <td style="width: 20%">
                </td>
                <td style="width: 20%">
                </td>
                <td style="width: 20%">
                    <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" /></td>
                <td style="width: 20%">
                </td>
                <td style="width: 20%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 20%">
                </td>
                <td style="width: 20%">
                </td>
                <td style="width: 20%">
                </td>
                <td style="width: 20%">
                </td>
                <td style="width: 20%">
                </td>
            </tr>
        </table>
    </form>

    <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>

</body>
</html>
