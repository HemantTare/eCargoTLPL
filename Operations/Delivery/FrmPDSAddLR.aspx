<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPDSAddLR.aspx.cs" Inherits="Operations_Outward_FrmPDSAddLR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/Operations/Delivery/PreDeliverySheet.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add LR To Manifest</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <table class="TABLE">
            <tr>
                <td class="TDGRADIENT" colspan="4">
                    &nbsp;
                    <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="ADD LR for PDS"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 30%" valign="top">
                    <asp:DropDownList ID="ddlArea" CssClass="DROPDOWN" runat="server" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlArea_SelectedIndexChanged" Visible="False" Width="38px" /></td>
                <td colspan="3" style="width: 70%">
                    <asp:Button ID="btnGo" runat="server" CssClass="BUTTON" OnClick="btnGo_Click"
                        Text="Go" /></td>
            </tr>
            <tr>
                <td valign="top" style="width: 30%">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="Div_servicelocations" class="DIV" style="height: 450px; width: 99%;">
                                <asp:DataGrid ID="dg_servicelocations" runat="server" AutoGenerateColumns="False"
                                    CellPadding="2" CssClass="GRID" DataKeyField="DeliveryAreaID" Style="border-top-style: none"
                                    Width="90%">
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Attach">
                                            <HeaderTemplate>
                                                <input id="chkAllItems" onclick="Check_All_Add_LR(this,'dg_servicelocations');" type="checkbox" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="Chk_Attach" runat="server" />
                                                <asp:HiddenField ID="hdnDeliveryAreaID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "DeliveryAreaID") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="5px" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Dly Area" HeaderText="Dly Area"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="NoOfLR" HeaderText="No Of LR"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Pkgs" HeaderText="Qty"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlArea" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td colspan="3" style="width: 70%">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div id="Div_Memo" class="DIV" style="height: 450px">
                                <asp:DataGrid ID="dg_Memo" runat="server" AutoGenerateColumns="False" DataKeyField="GC_Id"
                                    CellPadding="2" CssClass="GRID" Style="border-top-style: none" Width="98%">
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Attach">
                                            <HeaderTemplate>
                                                <input id="chkAllItems" type="checkbox" onclick="Check_All_Add_LR(this,'dg_Memo');" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="Chk_Attach" runat="server" />
                                                <asp:HiddenField ID="hdnGcNo" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "GC_No_For_Print") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="GC_No_For_Print" HeaderText="LR No" />
                                        <asp:BoundColumn DataField="DeliveryAreaName" HeaderText="Dly Area" />
                                        <asp:BoundColumn DataField="GC_Date" HeaderText="BkgDate" DataFormatString="{0:dd-MM-yy}" />
                                        <asp:BoundColumn DataField="Consignee_Name" HeaderText="Consignee Name" />
                                        <asp:BoundColumn DataField="Booking_Branch_Name" HeaderText="Bkg Branch" />
                                        <asp:BoundColumn DataField="Payment_Type" HeaderText="PayMode" />
                                        <asp:BoundColumn DataField="Balance_Articles" HeaderText="Pkgs" />
                                        <asp:BoundColumn DataField="Total_GC_Amount" HeaderText="Amt" />
                                        <asp:BoundColumn DataField="Consignee_Add1" HeaderText="Address" ItemStyle-HorizontalAlign="Left" />
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddlArea" />
                            <asp:AsyncPostBackTrigger ControlID="btnAddLR" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="4" valign="top">
                    <table class="TABLE">
                        <tr>
                            <td align="left" style="width: 5%">
                                <asp:Button ID="btnAddLR" runat="server" CssClass="BUTTON" Text="Add LR" OnClick="btnAddLR_Click" />
                            </td>
                            <td align="center" colspan="2" style="width: 90%">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Label ID="lblLRAdded" runat="server" CssClass="LABELERROR" Text=""></asp:Label>&nbsp;
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnAddLR" />
                                        <asp:AsyncPostBackTrigger ControlID="ddlArea" />
                                    </Triggers>
                                </asp:UpdatePanel>
                                <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label>
                            </td>
                            <td align="right" style="width: 5%">
                                <asp:Button ID="btnAddLRToMemo" runat="server" CssClass="BUTTON" Text="Add LR to PDS"
                                    OnClick="btnAddLRToMemo_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5%; font-weight: bold" align="right" valign="top">
                                Selected LR(s):
                            </td>
                            <td colspan="3" style="width: 95%">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:TextBox ID="lblSelectedLRs" runat="server" CssClass="TEXTBOXASLABEL" Text=""
                                            Font-Bold="true" TextMode="MultiLine" Height="51px" Width="600px"></asp:TextBox>&nbsp;
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnAddLR" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: absolute; bottom: 50%; left: 50%; font-size: 11px; font-family: Verdana;
                z-index: 100">
                <span id="ajaxloading">
                    <table>
                        <tr>
                            <td>
                                <asp:Image ID="Ajax_Image_ID" runat="server" ImageUrl="~/images/ajax-loader-Squares.gif" /></td>
                        </tr>
                        <tr>
                            <td align="center">
                                Wait! Action in Progress...</td>
                        </tr>
                    </table>
                </span>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</body>
</html>
