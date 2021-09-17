<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDeliveryCharges.aspx.cs"
    Inherits="Master_Branch_FrmDeliveryCharges" EnableViewState="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript">


    function NewWindow()
    {
        
       var Path='FrmDeliveryChrgCopyFrom.aspx';
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-550);
        var popH = (h-450);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
          window.open(Path, 'ChrgCopyPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no,resizable=no,scrollbars=no,statusbar=no');
          return false;
    }
    

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Delivery Charge Master</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="8" style="width: 10%">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Delivery Charge"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 10%">
                    </td>
                    <td style="width: 23%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 10%">
                    </td>
                    <td style="width: 22%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 10%">
                    </td>
                    <td class="TD1" style="width: 23%; text-align: center">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 10%">
                        Branch</td>
                    <td style="width: 23%">
                        <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                &nbsp;
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLBranch" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                        <cc1:DDLSearch ID="DDLBranch" runat="server" AllowNewText="False" IsCallBack="True"
                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" CallBackAfter="2"
                            PostBack="True" InjectJSFunction="" Text="" OnTxtChange="DDLBranch_TxtChange" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 10%">
                        Delivery Area</td>
                    <td style="width: 22%">
                        <%--<asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                &nbsp;
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLBranch" EventName="TxtChange" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                        <asp:DropDownList ID="ddlDeliveryArea" CssClass="DROPDOWN" runat="server" Width="98%"
                            OnSelectedIndexChanged="ddlDeliveryArea_SelectedIndexChanged" /></td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 10%">
                        &nbsp;Applicable From:&nbsp;
                    </td>
                    <td class="TD1" style="width: 23%; text-align: center;">
                        &nbsp;<uc1:WucDatePicker ID="dtpApplicableFromDate" runat="server" />
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 10%">
                    </td>
                    <td style="width: 23%">
                        <%--<asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>--%>
                        <asp:Button ID="btn_Copy" runat="server" CssClass="BUTTON" OnClick="btn_Copy_Click"
                            Text="Copy" />
                        <%--</ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Copy" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 10%">
                    </td>
                    <td style="width: 22%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 10%">
                    </td>
                    <td class="TD1" style="width: 23%; text-align: center;">
                    </td>
                </tr>
                <%--<asp:Panel ID="pnl_Copy" runat="server" GroupingText="Copy From">--%>
                <tr>
                    <td class="TD1" style="width: 10%">
                        <%--  <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                 
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Copy" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="DDLFromBranch" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                        <asp:Label ID="lblFromBranch" runat="server" Text="From Branch:"></asp:Label>
                    </td>
                    <td style="width: 23%">
                        <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                &nbsp;
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Copy" EventName="Click" />
                                <asp:AsyncPostBackTrigger ControlID="DDLFromBranch" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                        <cc1:DDLSearch ID="DDLFromBranch" runat="server" AllowNewText="False" CallBackAfter="2"
                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" InjectJSFunction=""
                            IsCallBack="True" OnTxtChange="DDLFromBranch_TxtChange" PostBack="True" Text="" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 10%">
                        <%--<asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                &nbsp;
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLFromBranch" EventName="TxtChange" />
                                <asp:AsyncPostBackTrigger ControlID="btn_Copy" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>--%>
                        <asp:Label ID="lblFromDeliveryArea" runat="server" Text="From Delivery Area:"></asp:Label>
                    </td>
                    <td style="width: 22%">
                        <asp:DropDownList ID="FromddlDeliveryArea" runat="server" CssClass="DROPDOWN" Width="98%"
                            OnSelectedIndexChanged="FromddlDeliveryArea_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList></td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td class="TD1" style="width: 10%">
                        <asp:Label ID="lblApplicableFrom" runat="server" Text="Applicable From :"></asp:Label>
                    </td>
                    <td class="TD1" style="width: 23%; text-align: center;">
                        <%--<uc1:WucDatePicker ID="FromdtpApplicableFromDate" runat="server" />--%>
                        <asp:DropDownList ID="ddlApplicableFrom" runat="server" CssClass="DROPDOWN" Width="98%">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 10%">
                    </td>
                    <td style="width: 23%">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 10%;" align="left" colspan="3">
                        &nbsp
                    </td>
                    <td align="left" colspan="1" style="width: 10%;">
                    </td>
                    <td align="left" colspan="1" style="width: 23%; text-align: center">
                        <asp:Button ID="btn_fillCopied" runat="server" CssClass="BUTTON" Text="OK" OnClick="btn_fillCopied_Click" /></td>
                </tr>
                <%--</asp:Panel>--%>
                <tr>
                    <td class="TD1" style="width: 10%">
                        &nbsp;</td>
                    <td colspan="5" style="width: 23%">
                        <asp:DataGrid ID="dgGrid" AutoGenerateColumns="False" ShowFooter="False" CellPadding="3"
                            CssClass="Grid" runat="server">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle Height="15px" Font-Size="11px" Font-Names="Verdana" Font-Bold="True"
                                HorizontalAlign="Left" ForeColor="Black" BorderStyle="Solid" BorderColor="#9495A2"
                                BorderWidth="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader">
                            </HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="Size">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdnSizeID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "SizeID")%>' />
                                        <asp:Label ID="lblSize" CssClass="LABEL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SizeName")%>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Rate/Article">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtRatePerArticle" runat="server" CssClass="TEXTBOXNOS" onfocus="this.select()"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "RatePerArticle")%>' onkeyPress="return Only_Integers(this,event);"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                    <td colspan="1" style="width: 10%">
                    </td>
                    <td colspan="1" style="width: 23%; text-align: center">
                    </td>
                </tr>
                <tr>
                    <td colspan="8">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="8">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClick="btnSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="8">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                                <asp:HiddenField ID="hdnKeyID" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
