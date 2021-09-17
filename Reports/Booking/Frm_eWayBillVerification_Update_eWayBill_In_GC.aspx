<%@ Page AutoEventWireup="true" CodeFile="Frm_eWayBillVerification_Update_eWayBill_In_GC.aspx.cs"
    Inherits="Reports_Booking_Frm_eWayBillVerification_Update_eWayBill_In_GC" Language="C#" %>

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

function refreshParentPage() 
     {
        window.opener.location.href = window.opener.location.href;
        if (window.opener.progressWindow) 
        {
            window.opener.progressWindow.close();
        }
        window.close();
    }
    

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Not Verified Reason</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body onunload="refreshParentPage();">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Not Verified Reason"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 80%; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1">
                    LR No. :
                </td>
                <td style="width: 80%; height: 15px;">
                    <asp:Label ID="lbl_GCNo" runat="server" Text="" Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 80%; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1">
                    eWayBill No. :
                </td>
                <td style="width: 80%; height: 15px;">
                    <asp:Label ID="lbl_eWayBillNo" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 80%; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1">
                    New eWayBill No. :
                </td>
                <td style="width: 80%; height: 15px;">
                    <asp:TextBox ID="txt_New_eWayBillNo" runat="server" CssClass="TEXTBOX" MaxLength="12"
                        onkeypress="return Only_Numbers(this,event);" Width="20%"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 80%; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:UpdatePanel ID="UpdatePanel" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                Text=""></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="height: 20px" align="center">
                    &nbsp;<asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click" /></td>
            </tr>
        </table>
    </form>
</body>
</html>
