<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVehicleMaintenance.aspx.cs"
    Inherits="Operations_PM_FrmVehicleMaintenance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" language="javascript" src="../../JavaScript/ddlsearch.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vehicle Maintenance</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">

function CalculateLabourCost()
{

var txt_LabourCost = document.getElementById('<%=txt_LabourCost.ClientID %>');
var txt_LabourDiscount = document.getElementById('<%=txt_LabourDiscount.ClientID %>');
var txt_TotalLabourCast = document.getElementById('<%=txt_TotalLabourCast.ClientID %>');
var hdn_TotalLabourCast = document.getElementById('<%=hdn_TotalLabourCast.ClientID %>');

txt_TotalLabourCast.innerHTML = parseFloat(txt_LabourCost.value) - parseFloat(txt_LabourDiscount.value);
hdn_TotalLabourCast.value =  parseFloat(txt_LabourCost.value) - parseFloat(txt_LabourDiscount.value);

CalculateTotalCost();
}

function CalculatePartsCost()
{
var txt_PartCost = document.getElementById('<%=txt_PartCost.ClientID %>');
var txt_PartDiscount = document.getElementById('<%=txt_PartDiscount.ClientID %>');
var txt_TotalPartCast = document.getElementById('<%=txt_TotalPartCast.ClientID %>');
var hdn_TotalPartCast = document.getElementById('<%=hdn_TotalPartCast.ClientID %>');

txt_TotalPartCast.innerHTML = parseFloat(txt_PartCost.value) - parseFloat(txt_PartDiscount.value);
hdn_TotalPartCast.value = parseFloat(txt_PartCost.value) - parseFloat(txt_PartDiscount.value);

CalculateTotalCost();
}

function CalculateTotalCost()
{
var hdn_TotalLabourCast = document.getElementById('<%=hdn_TotalLabourCast.ClientID %>');
var hdn_TotalPartCast = document.getElementById('<%=hdn_TotalPartCast.ClientID %>');
var txt_TotalCast = document.getElementById('<%=txt_TotalCast.ClientID %>');
var hdn_TotalCast = document.getElementById('<%=hdn_TotalCast.ClientID %>');

txt_TotalCast.innerHTML = parseFloat(hdn_TotalLabourCast.value) + parseFloat(hdn_TotalPartCast.value);
hdn_TotalCast.value =  parseFloat(hdn_TotalLabourCast.value) + parseFloat(hdn_TotalPartCast.value);
}
    </script>

</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%" border="0">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Vehicle Maintenance"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:HiddenField ID="hdn_menuitem_id" Value="1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                        Transaction No.:</td>
                    <td style="width: 29%;">
                        <asp:Label ID="lbl_TransactionNo" runat="server" Font-Bold="True"></asp:Label></td>
                    <td style="width: 1%;">
                    </td>
                    <td class="TD1" style="width: 20%;">
                        Date:</td>
                    <td style="width: 29%;">
                        <uc1:WucDatePicker ID="dtp_Transaction_Date" runat="server" />
                    </td>
                    <td style="width: 1%;" class="TDMANDATORY">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                        Vehicle No.:</td>
                    <td style="width: 29%;">
                        <uc2:WucVehicleSearch ID="WucVehicleSearch2" runat="server" />
                    </td>
                    <td style="width: 1%;">
                        &nbsp;</td>
                    <td class="TD1" style="width: 20%;">
                        Odometer:</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_odometer" runat="server" MaxLength="10" CssClass="TEXTBOXNOS"
                                    onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this);" onkeypress="return Only_Numbers(this,event);"></asp:TextBox>
                                <asp:HiddenField ID="hdn_odometer" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch2" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;">
                        &nbsp;</td>
                </tr>
                <tr class="HIDEGRIDCOL">
                    <td class="TD1" style="width: 20%;">
                        Worked At:</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:RadioButtonList ID="rblTobeWorkedAt" runat="Server" AutoPostBack="True" OnSelectedIndexChanged="rblTobeWorkedAt_SelectedIndexChanged"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Enabled="false">Internal</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">External</asp:ListItem>
                                </asp:RadioButtonList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rblTobeWorkedAt" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;" class="TDMANDATORY">
                    </td>
                    <td style="width: 50%;" colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                        Worked At:</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                            <ContentTemplate>
                                <asp:RadioButtonList ID="rblWorkShop" runat="Server" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">Company WorkShop</asp:ListItem>
                                    <asp:ListItem Value="0">Other WorkShop</asp:ListItem>
                                </asp:RadioButtonList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rblTobeWorkedAt" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;" class="TDMANDATORY">
                    </td>
                    <td style="width: 50%;" colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_VendorLocation_Caption" runat="server" Text="Vendor"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rblTobeWorkedAt" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <cc1:DDLSearch ID="ddl_Vendor" runat="server" IsCallBack="True" PostBack="True" OnTxtChange="ddl_Vendor_OnTxtChange"
                                    AllowNewText="True" CallBackAfter="2" CallBackFunction="ClassLibrary.UIControl.CallBack.GetSearchResult"
                                    InjectJSFunction="" Text="" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rblTobeWorkedAt" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;">
                        &nbsp;</td>
                    <td class="TD1" style="width: 20%;">
                        Invoice No.:</td>
                    <td style="width: 29%;">
                        <asp:TextBox ID="txt_InvoiceNo" runat="server" MaxLength="20" CssClass="TEXTBOX"
                            onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this);"></asp:TextBox>
                    </td>
                    <td style="width: 1%;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                        Repair Service Category:</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Repair_Service_Category" AutoPostBack="True" runat="server"
                                    CssClass="DROPDOWN" OnSelectedIndexChanged="ddl_Repair_Service_Category_SelectedIndexChanged" />
                            </ContentTemplate>
                            <%-- <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_hidden"/>
                            </Triggers>--%>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;">
                        &nbsp;</td>
                    <td class="TD1" style="width: 50%;" colspan="3">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                        Repair Service:</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Repair_Service" AutoPostBack="True" runat="server" CssClass="DROPDOWN"
                                    OnSelectedIndexChanged="ddl_Repair_Service_SelectedIndexChanged" />
                            </ContentTemplate>
                            <%--<Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_hidden"/>
                            </Triggers>--%>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;">
                        &nbsp;</td>
                    <td class="TD1" style="width: 50%;" colspan="3">
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                        &nbsp;</td>
                    <td colspan="5">
                        <asp:Label ID="lbl_TaskService" runat="server" Text="Task Done" Font-Bold="true"
                            ForeColor="DarkBlue"></asp:Label></td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                    </td>
                    <td colspan="5">
                        <asp:UpdatePanel ID="upnl_chk_List_ServiceTask" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:CheckBoxList ID="chk_List_ServiceTask" CssClass="CHECKBOXLIST" runat="server"
                                    RepeatDirection="Horizontal" RepeatColumns="1" ForeColor="darkRed" Font-Bold="true">
                                </asp:CheckBoxList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Repair_Service" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                        Service Against:</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddl_Service_Against" AutoPostBack="True" runat="server" CssClass="DROPDOWN"
                                    OnSelectedIndexChanged="ddl_Service_Against_SelectedIndexChanged" />
                            </ContentTemplate>
                            <%--<Triggers>
                                <asp:AsyncPostBackTrigger ControlID="DDLVehicle" />
                            </Triggers>--%>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;" class="TDMANDATORY">
                    </td>
                    <td class="TD1" style="width: 50%;" colspan="3">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                        Labour Cost:</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_LabourCost" runat="server" CssClass="TEXTBOXNOS" MaxLength="10"
                                    Text="0" onfocus="txtbox_onfocus(this);" onkeypress="return Only_Numbers(this,event);"
                                    onblur="CalculateLabourCost();txtbox_onlostfocus(this);"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Service_Against" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;" class="TDMANDATORY">
                    </td>
                    <td class="TD1" style="width: 20%;">
                        Part Cost:</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_PartCost" runat="server" CssClass="TEXTBOXNOS" MaxLength="10"
                                    Text="0" onfocus="txtbox_onfocus(this);" onkeypress="return Only_Numbers(this,event);"
                                    onblur="CalculatePartsCost();txtbox_onlostfocus(this);"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Service_Against" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;" class="TDMANDATORY">
                    </td>
                </tr>
                <tr class="HIDEGRIDCOL">
                    <td class="TD1" style="width: 20%;">
                        Discount:</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_LabourDiscount" runat="server" CssClass="TEXTBOXNOS" MaxLength="10"
                                    Text="0" onfocus="txtbox_onfocus(this);" onkeypress="return Only_Numbers(this,event);"
                                    onblur="CalculateLabourCost();txtbox_onlostfocus(this);"></asp:TextBox>
                            </ContentTemplate>
                            <%--<Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_driver" />                                
                            </Triggers>--%>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;" class="TDMANDATORY">
                    </td>
                    <td class="TD1" style="width: 20%;">
                        Discount:</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txt_PartDiscount" runat="server" CssClass="TEXTBOXNOS" MaxLength="10"
                                    Text="0" onfocus="txtbox_onfocus(this);" onkeypress="return Only_Numbers(this,event);"
                                    onblur="CalculatePartsCost();txtbox_onlostfocus(this);"></asp:TextBox>
                            </ContentTemplate>
                            <%--<Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_driver" />                                
                            </Triggers>--%>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;" class="TDMANDATORY">
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr class="HIDEGRIDCOL">
                    <td class="TD1" style="width: 20%;">
                        Total Labour Cost:</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="txt_TotalLabourCast" Font-Bold="true" runat="server" CssClass="LABEL"
                                    Text="0"></asp:Label>
                                <asp:HiddenField ID="hdn_TotalLabourCast" runat="server" Value="0"></asp:HiddenField>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Service_Against" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;" class="TDMANDATORY">
                    </td>
                    <td class="TD1" style="width: 20%;">
                        Total Part Cost:</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="txt_TotalPartCast" Font-Bold="true" runat="server" CssClass="LABEL"
                                    Text="0"></asp:Label>
                                <asp:HiddenField ID="hdn_TotalPartCast" runat="server" Value="0"></asp:HiddenField>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Service_Against" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                        Total Cost:</td>
                    <td style="width: 29%;">
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="txt_TotalCast" Font-Bold="true" runat="server" CssClass="LABEL" Text="0"></asp:Label>
                                <asp:HiddenField ID="hdn_TotalCast" runat="server" Value="0"></asp:HiddenField>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddl_Service_Against" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 1%;" class="TDMANDATORY">
                    </td>
                    <td class="TD1" style="width: 50%;" colspan="3">
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%;">
                        Remarks:</td>
                    <td style="width: 79%;" colspan="4">
                        <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX" Height="60px" TextMode="MultiLine"
                            Width="99.5%" MaxLength="250" onfocus="txtbox_onfocus(this);" onblur="txtbox_onlostfocus(this);"></asp:TextBox>
                    </td>
                    <td style="width: 1%;">
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="6" style="height: 20px">
                        <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click" />&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6">
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
</body>
</html>
