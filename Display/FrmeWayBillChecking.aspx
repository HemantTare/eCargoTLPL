<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmeWayBillChecking.aspx.cs"
    Inherits="Display_FrmeWayBillChecking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc3" %>
<%@ Register Src="../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>

<script type="text/javascript" src="../Javascript/Common.js"></script>

<script type="text/javascript">

function Open_Details_Window(Path)
{ 
  window.open(Path,'eWayBillCheckingDetails','width=1000,height=800,top=0,left=0,menubar=no,resizable=yes,scrollbars=yes')
  return false;
} 

function Open_Mail_Window(Path)
{ 
  window.open(Path,'eWayBillCheckingMail','width=800,height=400,top=0,left=0,menubar=no,resizable=no,scrollbars=no')
  return false;
} 


</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eWayBill Checking</title>
    <link href="../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="3" style="height: 16px">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="eWayBill Checking"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        Date :&nbsp;</td>
                    <td style="width: 30%; height: 15px;">
                        <uc1:WucDatePicker ID="dtp_Date" runat="server" />
                    </td>
                    <td class="TD1" style="width: 50%; height: 15px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        Vehicle No. :&nbsp;</td>
                    <td style="width: 30%; height: 15px;">
                        <uc3:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
                        <asp:HiddenField ID="hdn_VehicleID" runat="server" />
                        <asp:HiddenField ID="hdn_VehicleCategoryIds" runat="server" />
                        <asp:HiddenField ID="hdn_NumberPart4" runat="server" />
                    </td>
                    <td class="TD1" style="width: 50%; height: 15px;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 15px;">
                        &nbsp;</td>
                    <td align="left" colspan="2" style="height: 20px; width: 80%">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:RadioButtonList ID="rbl_ReportType" runat="server" RepeatDirection="Vertical"
                                    Font-Bold="true" ForeColor="#660099" AutoPostBack="true">
                                    <asp:ListItem Selected="True" Text="Consolidated eWayBills" Value="1"></asp:ListItem>
                                    <asp:ListItem Text="Part B Updated By Party" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="All LR Details" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Error eWayBills" Value="4"></asp:ListItem>
                                </asp:RadioButtonList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="3" style="height: 20px">
                        <asp:Button ID="btn_View" runat="server" Text="View" CssClass="BUTTON" OnClick="btn_view_Click" />&nbsp;&nbsp;
                        <asp:Button ID="btn_Mail" runat="server" Text="Send eMail" CssClass="BUTTON" OnClick="btn_Mail_Click" /></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:UpdatePanel ID="Upd_Pnl_Bank" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text=""></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
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
