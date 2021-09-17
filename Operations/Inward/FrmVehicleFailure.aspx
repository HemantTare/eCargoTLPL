<%@ Page AutoEventWireup="true" CodeFile="FrmVehicleFailure.aspx.cs" Inherits="Operations_Inward_FrmVehicleFailure"
    Language="C#" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc3" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">



</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Vehicle Failure</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_VehicleFailure" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Vehicle Failure"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1">
                </td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1">
                    Vehicle No.</td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                    <uc3:WucVehicleSearch ID="WucVehicleSearch1" runat="server" />
                </td>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1">
                </td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1">
                    Trip Memo No. :</td>
                <td style="width: 30%; height: 15px;">
                    <asp:UpdatePanel ID="upd_LHPONo" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbl_LHPONo" runat="server" CssClass="LABEL" Style="font-weight: bolder"></asp:Label>
                            <asp:HiddenField runat="server" ID="hdn_Main_LHPO_Id"></asp:HiddenField>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td style="width: 20%; height: 15px;" class="TD1">
                    &nbsp; Trip Memo Date :</td>
                <td style="width: 30%; height: 15px;">
                    <asp:UpdatePanel ID="upd_LHPODate" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbl_LHPODate" runat="server" CssClass="LABEL" Style="font-weight: bolder"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1"></asp:AsyncPostBackTrigger>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1">
                </td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1" colspan="4">
                    <asp:UpdatePanel ID="Upd_Pnl_dg_Grid" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_dg_Grid" runat="server" Height="200px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="False" AllowPaging="False"
                                    CssClass="GRID" AllowSorting="True" AllowCustomPaging="False" AutoGenerateColumns="False"
                                    OnItemDataBound="dg_Grid_ItemDataBound" PageSize="25">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:BoundColumn DataField="TripMemoBranch" HeaderText="Trip Memo Branch"></asp:BoundColumn>
                                        <%--<asp:BoundColumn DataField="TripMemoDate" HeaderText="Trip Memo Date"></asp:BoundColumn>--%>
                                        <asp:TemplateColumn HeaderText="Trip Memo Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lbl_Trip_Memo_Date" Text='<%# DataBinder.Eval(Container.DataItem, "TripMemoDate") %>'
                                                    runat="server" CssClass="TEXTBOXNOS" BackColor="Transparent" BorderStyle="None"
                                                    BorderColor="Transparent" Style="text-align: Left" Font-Size="11px" Font-Names="Verdana"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="InvoiceNo" HeaderText="Inv. No."></asp:BoundColumn>
                                        <asp:BoundColumn DataField="InvoiceDate" HeaderText="Inv. Date"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="InvoiceFrom" HeaderText="Inv. From"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="InvoiceTo" HeaderText="Inv. To"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="MemoType" HeaderText="MemoType"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Status" HeaderText="Status"></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1">
                </td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1">
                    Actual Location :&nbsp;</td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                    <asp:TextBox ID="txt_ActualLocation" runat="server" Font-Bold="True" MaxLength="50"
                        onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" Text="" Width="95%"></asp:TextBox></td>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1">
                </td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1">
                    Crossing Branch :</td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                    <asp:DropDownList ID="ddl_CrossingBranch" runat="server" CssClass="DROPDOWN">
                    </asp:DropDownList></td>
                <td style="width: 20%; height: 15px;" class="TD1">
                    &nbsp; Date :</td>
                <td style="width: 30%; height: 15px;">
                    <uc2:WucDatePicker ID="Dtp_CrossingDate" runat="server"></uc2:WucDatePicker>
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1">
                </td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1">
                    Reason For Crossing :</td>
                <td style="width: 30%; height: 15px;" colspan="3">
                    &nbsp;
                    <asp:DropDownList ID="ddl_CrossingReason" runat="server" CssClass="DROPDOWN">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1">
                </td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 20%; height: 15px;">
                    &nbsp;
                </td>
                <td style="width: 30%; height: 15px;">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 20%; height: 15px;" class="TD1">
                    Remark :</td>
                <td style="width: 30%; height: 15px;" colspan="3">
                    &nbsp;
                    <asp:TextBox ID="txt_Remarks" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                        onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" Height="40px"
                        MaxLength="250" TextMode="multiline"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="left" colspan="4">
                    <asp:UpdatePanel ID="UpdatePanel" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                Text=""></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="WucVehicleSearch1" />
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
