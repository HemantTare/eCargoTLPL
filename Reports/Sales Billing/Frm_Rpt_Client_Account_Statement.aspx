<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Rpt_Client_Account_Statement.aspx.cs"
    Inherits="Reports_SalesBilling_Frm_Rpt_Client_Account_Statement" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<%@ Register Src="~/CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

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
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_booking_dest_wise" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td class="TDGRADIENT" colspan="2">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Client Account Statement"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 21px;">
                </td>
                <td style="width: 80%; height: 21px;">
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 21px;" class="TD1">
                    Client : &nbsp;</td>
                <td style="width: 80%; height: 21px;">
                    <asp:UpdatePanel ID="UpdatePanelParty" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <cc1:DDLSearch ID="ddlParty" runat="server" AllowNewText="False" CallBackAfter="2"
                                CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchBillingPartyForAccountStatement"
                                InjectJSFunction="" IsCallBack="True" PostBack="True" Text=""></cc1:DDLSearch>
                            <asp:HiddenField ID="hdn_Is_Regular_Client" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlParty" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 20px">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2" style="height: 20px;" align="center">
                    <asp:Button ID="btn_view" CssClass="BUTTON" runat="server" Text="View" OnClick="btn_view_Click" /></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 20%">
                </td>
                <td style="width: 80%">
                </td>
            </tr>
        </table>
    </form>

    <script type="text/javascript">
    
        self.parent.hideload();
    
    </script>

</body>
</html>
