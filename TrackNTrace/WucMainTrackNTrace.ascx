<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucMainTrackNTrace.ascx.cs"
    Inherits="TrackNTrace_WucMainTrackNTrace" %>
<%@ Register Src="WucTrackNTraceFinanceDetails.ascx" TagName="WucTrackNTraceFinanceDetails"
    TagPrefix="uc17" %>
<%@ Register Src="WucTrackNTraceBillingDetails.ascx" TagName="WucTrackNTraceBillingDetails"
    TagPrefix="uc16" %>
<%@ Register Src="WucTrackNTraceCreditMemo.ascx" TagName="WucTrackNTraceCreditMemo"
    TagPrefix="uc15" %>
<%@ Register Src="WucTrackNTraceAttachedLHPODetails.ascx" TagName="WucTrackNTraceAttachedLHPODetails"
    TagPrefix="uc10" %>
<%@ Register Src="WucTrackNTraceGeneralDetails.ascx" TagName="WucTrackNTraceGeneralDetails"
    TagPrefix="uc1" %>
<%@ Register Src="WucTrackNTraceLHPODetails.ascx" TagName="WucTrackNTraceLHPODetails"
    TagPrefix="uc2" %>
<%@ Register Src="WucTrackNTraceDeliveryDetails.ascx" TagName="WucTrackNTraceDeliveryDetails"
    TagPrefix="uc3" %>
<%@ Register Src="WucTrackNTraceAUSDetails.ascx" TagName="WucTrackNTraceAUSDetails"
    TagPrefix="uc4" %>
<%@ Register Src="WucTrackNTraceCrossingDetails.ascx" TagName="WucTrackNTraceCrossingDetails"
    TagPrefix="uc5" %>
<%@ Register Src="WucTrackNTraceMemoDetails.ascx" TagName="WucTrackNTraceMemoDetails"
    TagPrefix="uc6" %>
<%@ Register Src="WucTrackNTraceStatusDetails.ascx" TagName="WucTrackNTraceStatusDetails"
    TagPrefix="uc7" %>
<%@ Register Src="WucTrackNTraceBookingMR.ascx" TagName="WucTrackNTraceBookingMR"
    TagPrefix="uc8" %>
<%@ Register Src="WucTrackNTraceDeliveryMR.ascx" TagName="WucTrackNTraceDeliveryMR"
    TagPrefix="uc9" %>
<%@ Register Src="WucPODTrackNTraceMovementDetails.ascx" TagName="WucPODTrackNTraceMovementDetails"
    TagPrefix="uc11" %>
<%@ Register Src="WucPODTrackNTraceCoverGenerationDetails.ascx" TagName="WucPODTrackNTraceCoverGenerationDetails"
    TagPrefix="uc12" %>
<%@ Register Src="WucPODTrackNTraceCoverReceivedDetails.ascx" TagName="WucPODTrackNTraceCoverReceivedDetails"
    TagPrefix="uc13" %>
<%@ Register Src="WucPODTrackNTraceCoverDeliveryDetails.ascx" TagName="WucPODTrackNTraceCoverDeliveryDetails"
    TagPrefix="uc14" %>
<%@ Register Src="WucTrackNTracePDSDeliveryDetails.ascx" TagName="WucTrackNTracePDSDeliveryDetails"
    TagPrefix="uc18" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:ScriptManager runat="server" ID="scm_TrackNTrace">
</asp:ScriptManager>

<script type="text/javascript">
function set_onshowclick()
{
    var hdn_check_for_show = document.getElementById('<%=hdn_check_for_show.ClientID%>');

    hdn_check_for_show.value = '1';
}

function disable_deliverytype()
{
    var tr_del_type = document.getElementById('tr_del_type');
    var DDL_Select_No = document.getElementById('<%=DDL_Select_No.ClientID%>');
    if(DDL_Select_No.value == '4')
    {
        tr_del_type.style.display = 'inline';
    }
    else
    {
        tr_del_type.style.display = 'none';
    }
}

function disable_GcNo()
{
    var tr_gc_no = document.getElementById('<%=tr_gc_no.ClientID%>');
    var DDL_Select_No = document.getElementById('<%=DDL_Select_No.ClientID%>');

    if(DDL_Select_No.value == '0' || DDL_Select_No.value == '15')
    {
        tr_gc_no.style.display = 'inline';
    }
    else
    {
        tr_gc_no.style.display = 'none';
    }
}

function SendStatusSMS() 
{
    var txt_GC_No = document.getElementById('<%=txt_GC_No.ClientID%>');
    var txt_Status  = document.getElementById('<%=lbl_Status.ClientID%>');
    var Path = '';
    Path = "../TrackNTrace/FrmGCStatusSMS.aspx?GC_No=" + txt_GC_No.value +"&Msg=UwBNAFMAWQBlAHMA";
    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = w - 600;
    var popH = h - 180;
    var leftPos = (w - popW) / 2;
    var topPos = 0;
    window.open(Path, 'GCStatusSMS', 'width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', directories=no,titlebar=no,toolbar=no,menubar=no,resizable=no,scrollbars=no,statusbar=no');
    return false;
}

</script>

<table class="TABLE" border="0px">
    <tr>
        <td align="center" style="width: 50%" colspan="3">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Status" ForeColor="MidnightBlue" Font-Bold="True" runat="server"
                        Font-Size="Medium"></asp:Label>&nbsp;&nbsp;
                    <asp:Button ID="btn_SMS" runat="server" Text="Send SMS" CssClass="SMALLBUTTON" Visible="false" BackColor="Yellow" ForeColor="Red" Font-Bold="true"  />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Show" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_GC_No" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Cancelled_Msg" ForeColor="Red" Font-Bold="True" runat="server"
                        Font-Size="X-Large"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Show" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_GC_No" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Short" runat="server" Font-Bold="True" Font-Size="X-Large" ForeColor="Red"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Show" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_GC_No" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td align="right" style="width: 50%" colspan="3">
            <table style="width: 100%">
                <tr>
                    <td align="center" style="width: 40%">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="DDL_Select_No" onchange="disable_deliverytype();disable_GcNo()"
                                    runat="server" CssClass="DROPDOWN">
                                    <asp:ListItem Selected="True" Value="0">GC No</asp:ListItem>
                                    <%-- <asp:ListItem Value="8">Private Mark</asp:ListItem>--%>
                                    <asp:ListItem Value="1">Manifest No</asp:ListItem>
                                    <asp:ListItem Value="2">LHPO No</asp:ListItem>
                                    <asp:ListItem Value="3">AUS No</asp:ListItem>
                                    <asp:ListItem Value="4">Delivery No</asp:ListItem>
                                    <asp:ListItem Value="5">Booking MR No</asp:ListItem>
                                    <asp:ListItem Value="6">Delivery MR No</asp:ListItem>
                                    <asp:ListItem Value="7">Credit Memo No</asp:ListItem>
                                    <asp:ListItem Value="8">Bill No</asp:ListItem>
                                    <asp:ListItem Value="16">Cover No</asp:ListItem>
                                    <asp:ListItem Value="17">Cover Receipt No</asp:ListItem>
                                    <asp:ListItem Value="18">Cover Delivered No</asp:ListItem>
                                    <asp:ListItem Value="19">PDS No</asp:ListItem>
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdn_check_for_show" Value="0" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Show" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="center" style="width: 40%">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_GC_No" runat="server" CssClass="TEXTBOX" MaxLength="15"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Show" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td align="center" style="width: 18%">
                        <asp:Button ID="btn_Show" runat="server" Text="Show" CssClass="SMALLBUTTON" OnClientClick="set_onshowclick()"
                            OnClick="btn_Show_Click" />
                    </td>
                    <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txt_GC_No"
                            runat="server" Text="*"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr id="tr_del_type" style="display: none">
                    <td align="center" style="width: 40%">
                    </td>
                    <td align="left" style="width: 40%">
                        <asp:DropDownList ID="ddl_Delivery_type" runat="server" Width="100%" CssClass="DROPDOWN">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 20%">
                        &nbsp;</td>
                </tr>
                <tr id="tr_gc_no" runat="server">
                    <td align="center" style="width: 40%">
                    </td>
                    <td align="left" style="width: 60%" colspan="2">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="ddl_GC_No" runat="server" AutoPostBack="true" Width="70%" CssClass="DROPDOWN"
                                                OnSelectedIndexChanged="ddl_GC_No_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="btn_Show" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 100%" colspan="6">
                                <ComponentArt:TabStrip ID="TB_TrackNTrace" SiteMapXmlFile="~/XML/TrackNTrace.xml"
                                    EnableViewState="false" MultiPageId="MP_TrackNTrace" runat="server">
                                </ComponentArt:TabStrip>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 100%" colspan="6">
                                <ComponentArt:MultiPage ID="MP_TrackNTrace" CssClass="MULTIPAGE" runat="server" Style="left: 0px;
                                    top: 1px">
                                    <ComponentArt:PageView runat="server">
                                        <%--General Details--%>
                                        <uc1:WucTrackNTraceGeneralDetails ID="WucTrackNTraceGeneralDetails1" runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--Crossing Details--%>
                                        <uc5:WucTrackNTraceCrossingDetails ID="WucTrackNTraceCrossingDetails1" runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--Memo Details--%>
                                        <uc6:WucTrackNTraceMemoDetails ID="WucTrackNTraceMemoDetails1" runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--LHPO Details--%>
                                        <uc2:WucTrackNTraceLHPODetails ID="WucTrackNTraceLHPODetails1" runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--AUS Details--%>
                                        <uc4:WucTrackNTraceAUSDetails ID="WucTrackNTraceAUSDetails1" runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--Delivery Details--%>
                                        <uc3:WucTrackNTraceDeliveryDetails ID="WucTrackNTraceDeliveryDetails1" runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--GC Status--%>
                                        <uc7:WucTrackNTraceStatusDetails ID="WucTrackNTraceStatusDetails1" runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--Booking MR--%>
                                        <uc8:WucTrackNTraceBookingMR ID="WucTrackNTraceBookingMR1" runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--Delivery MR--%>
                                        <uc9:WucTrackNTraceDeliveryMR ID="WucTrackNTraceDeliveryMR1" runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--Attached LHPO--%>
                                        <uc10:WucTrackNTraceAttachedLHPODetails ID="WucTrackNTraceAttachedLHPODetails1" runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--POD Movement Details--%>
                                        <uc11:WucPODTrackNTraceMovementDetails ID="WucPODTrackNTraceMovementDetails" runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--POD Cover Generation Details--%>
                                        <uc12:WucPODTrackNTraceCoverGenerationDetails ID="WucPODTrackNTraceCoverGenerationDetails"
                                            runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--POD Cover Received Details--%>
                                        <uc13:WucPODTrackNTraceCoverReceivedDetails ID="WucPODTrackNTraceCoverReceivedDetails"
                                            runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--POD Cover Delivery Details--%>
                                        <uc14:WucPODTrackNTraceCoverDeliveryDetails ID="WucPODTrackNTraceCoverDeliveryDetails"
                                            runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--Credit Memo--%>
                                        <uc15:WucTrackNTraceCreditMemo ID="WucTrackNTraceCreditMemo1" runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--Transport Bill--%>
                                        <uc16:WucTrackNTraceBillingDetails ID="WucTrackNTraceBillingDetails1" runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--Voucher Details--%>
                                        <uc17:WucTrackNTraceFinanceDetails ID="WucTrackNTraceFinanceDetails1" runat="server" />
                                    </ComponentArt:PageView>
                                    <ComponentArt:PageView runat="server">
                                        <%--Voucher Details--%>
                                        <uc18:WucTrackNTracePDSDeliveryDetails ID="WucTrackNTracePDSDeliveryDetails1" runat="server" />
                                    </ComponentArt:PageView>
                                </ComponentArt:MultiPage>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Show" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_GC_No" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>

<script language="javascript" type="text/javascript">
 disable_deliverytype();
</script>

