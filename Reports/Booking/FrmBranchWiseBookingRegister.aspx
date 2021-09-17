<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBranchWiseBookingRegister.aspx.cs"
    Inherits="Reports_Booking_FrmBranchWiseBookingRegister" %>

<%@ Register Src="../../CommonControls/Wuc_GC_Parameters.ascx" TagName="Wuc_GC_Parameters"
    TagPrefix="uc3" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<%-- Author : Ankit champaneriya
     Date   : 03-01-09
     Desc   : BookingRegister --%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Booking Register</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table class="TABLE">
                <tr>
                    <td class="TDGRADIENT" style="width: 100%">
                        <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Branch wise Booking Register"></asp:Label></td>
                </tr>
                <tr>
                    <td>
                        <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc3:Wuc_GC_Parameters ID="Wuc_GC_Parameters1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                    </td>
                </tr>
                <%--<tr>
          <td>
            <table class="TABLE">
              <tr>
                <td>&nbsp;</td>
                <td>
                  <asp:RadioButtonList ID="rdl_BookingRegister" runat="server" RepeatDirection="Horizontal">
                    <asp:ListItem Value="0" Selected="True">Bkg BranchWise</asp:ListItem>
                    <asp:ListItem Value="1">Dly BranchWise</asp:ListItem>
                  </asp:RadioButtonList>
                </td>
              </tr>
            </table>
          </td>
        </tr>--%>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="rdl_BookingRegister" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Value="0" Selected="True">BranchWise Booking Register</asp:ListItem>
                            <asp:ListItem Value="1">DeliveryWise Booking Register</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <script type="text/javascript">
    self.parent.hideload();
    </script>

</body>
</html>
