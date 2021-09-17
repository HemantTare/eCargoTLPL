<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPartyInvoiceWeightUpdate.aspx.cs"
    Inherits="Operations_Booking_FrmPartyInvoiceWeightUpdate" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="../../CommonControls/Wuc_From_To_Datepicker.ascx" TagName="Wuc_From_To_Datepicker"
    TagPrefix="uc2" %>
<%--<script type="text/javascript" src="../../Javascript/Common.js"></script>
--%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
   
   function OpenF4Menu(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        window.open(Path, 'NewConsigneeDetails', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Party Invoice Weight Update In LR</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scm_Comm">
        </asp:ScriptManager>
        <table class="TABLE" border="0" cellpadding="0" cellspacing="0" style="width: 100%"
            id="TABLE1">
            <tr>
                <td class="TDGRADIENT" colspan="5">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Party Invoice Weight Update In LR"></asp:Label>
                </td>
            </tr>
            <tr runat="server">
                <td style="width: 10%; height: 15px;">
                </td>
                <td class="TD1" style="width: 10%; height: 15px;">
                </td>
                <td class="TD1" style="width: 40%; height: 15px;">
                </td>
                <td class="TD1" style="width: 20%; height: 15px;">
                </td>
                <td class="TD1" style="width: 20%; height: 15px;">
                </td>
            </tr>
            <tr id="Tr1" runat="server">
                <td class="TD1" style="width: 10%; height: 15px;">
                    Billing Client : &nbsp;
                </td>
                <td colspan="2" style="width: 40%; height: 15px; text-align: left; font-weight: bold;">
                    VASHI ELECTRICALS PVT. LTD.</td>
                <td class="TD1" style="width: 20%; height: 15px;">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 10%;">
                </td>
                <td id="Td1" runat="server" class="TD1" style="width: 10%;">
                    &nbsp;</td>
                <td class="TDMANDATORY" style="width: 40%;">
                </td>
                <td class="TD1" style="width: 20%;">
                    &nbsp;</td>
                <td class="TD1" style="width: 20%;">
                </td>
            </tr>
            <tr>
                <td class="TD1" colspan="4">
                    <uc2:Wuc_From_To_Datepicker ID="Wuc_From_To_Datepicker1" runat="server" />
                </td>
                <td class="TD1" align="left">
                    <asp:Button ID="btn_View" runat="server" CssClass="BUTTON" Text="View" OnClick="btnView_Click" /></td>
            </tr>
            <tr>
                <td class="TD1" style="width: 10%; height: 13px;">
                    </td>
                <td id="Td2" runat="server" style="width: 10%; height: 13px;" align="left">
                    <asp:LinkButton ID="lnk_btnNewConsignee" runat="server" Font-Names="Verdana" Font-Size="12px"
                        Font-Underline="True" Font-Bold="True" ForeColor="Blue" Text="New Consignee" Visible="false" />&nbsp;</td>
                <td class="TDMANDATORY" style="width: 40%; height: 13px;">
                    &nbsp;</td>
                <td class="TD1" style="width: 20%; height: 13px;">
                    Total Record :&nbsp;</td>
                <td style="width: 20%; height: 13px;" align="left">
                    <asp:Label ID="lblTotalRecords" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <td class="TD1" style="width: 10%;">
                </td>
                <td id="Td3" runat="server" class="TD1" style="width: 10%;">
                    &nbsp;</td>
                <td class="TDMANDATORY" style="width: 40%;">
                </td>
                <td class="TD1" style="width: 20%;">
                    &nbsp;</td>
                <td class="TD1" style="width: 20%;">
                </td>
            </tr>
            <tr>
                <td colspan="5" style="height: 233px;" align="center">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upnl_Comm">
                        <ContentTemplate>
                            <div id="Div_Commodity" class="DIV" style="height: 400px; width: 100%; text-align: center;">
                                <asp:DataGrid ID="dg_Commodity" AutoGenerateColumns="False" ShowFooter="false" CellPadding="3"
                                    CssClass="Grid" runat="server" OnItemDataBound="dg_Commodity_ItemDataBound">
                                    <ItemStyle HorizontalAlign="Left" />
                                    <HeaderStyle Height="15px" Font-Size="11px" Font-Names="Verdana" Font-Bold="True"
                                        HorizontalAlign="Left" ForeColor="Black" BorderStyle="Solid" BorderColor="#9495A2"
                                        BorderWidth="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader">
                                    </HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="BkgBranch" ItemStyle-Width="15%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblBkgBr" CssClass="LABEL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BkgBranch")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="LRDate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLRDate" CssClass="LABEL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LRDate")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="LRNo.">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdnGCID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "GC_ID")%>' />
                                                <asp:Label ID="lblGCNo" CssClass="LABEL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GC_No")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Consignee" ItemStyle-Width="35%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblConsignee" CssClass="LABEL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Consignee_Name")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Invoice No." ItemStyle-Width="30%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoiceNo" CssClass="LABEL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Private_Mark")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Weight">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txt_Weight" runat="server" CssClass="TEXTBOX" Text='<%# DataBinder.Eval(Container.DataItem, "GCWeight")%>'
                                                    onkeyPress="return Only_Integers(this,event);" onfocus="txtbox_onfocus(this);this.select();"
                                                    onblur="txtbox_onlostfocus(this);"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="5">
                    <asp:Button ID="btnSave" runat="server" CssClass="BUTTON" OnClick="btnSave_Click"
                        Text="Save" />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="5">
                    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                Text="Records In Yellow Colors Are Not Updated"></asp:Label>
                            <asp:HiddenField ID="hdnKeyID" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
