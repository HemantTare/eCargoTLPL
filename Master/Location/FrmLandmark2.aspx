<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmLandmark2.aspx.cs" Inherits="Master_Location_FrmLandmark2"
    EnableViewState="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript">


</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Landmark 2 Master</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="3" style="height: 16px">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Landmark 2"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 79%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Branch</td>
                    <td style="width: 79%">
                        <cc1:DDLSearch ID="DDLBranch" runat="server" AllowNewText="False" IsCallBack="True"
                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" CallBackAfter="2"
                            PostBack="True" InjectJSFunction="" Text="" OnTxtChange="DDLBranch_TxtChange" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 79%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 21px;">
                        Delivery Area</td>
                    <td style="width: 79%; height: 21px;">
                        <asp:DropDownList ID="ddlDeliveryArea" CssClass="DROPDOWN" runat="server" Width="50%" AutoPostBack="true"
                         OnSelectedIndexChanged="ddlDeliveryArea_SelectedIndexChanged" /></td>
                    <td class="TDMANDATORY" style="width: 1%; height: 21px;">
                        *</td>
                    <td class="TD1" style="width: 1%; height: 21px;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 79%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Landmark 1</td>
                    <td style="width: 79%">
                        <asp:DropDownList ID="ddlLandmark1" CssClass="DROPDOWN" runat="server" Width="50%" /></td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 79%">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Landmark&nbsp;</td>
                    <td style="width: 79%">
                        <asp:TextBox ID="txtLandmark2" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            Width="50%" MaxLength="50"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                    </td>
                    <td style="width: 79%">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClick="btnSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="3">
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
