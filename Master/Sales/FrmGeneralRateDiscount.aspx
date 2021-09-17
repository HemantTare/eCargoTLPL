<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmGeneralRateDiscount.aspx.cs"
    Inherits="Master_Sales_FrmGeneralRateDiscount" %>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script language="javascript" type="text/javascript" src="../../Javascript/Master/Sales/GeneralRateDiscount.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>General Rate Discount</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="General Rate Discount"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                                    <td style="width: 29%; text-align: right; height: 21px;">
                        <asp:Label ID="lbl_DiscountTitle" runat="server" Text="Discount Title"></asp:Label>
                    </td>
                    <td style="width: 1%; height: 21px;">
                        <asp:DropDownList ID="ddl_DiscountTitle" runat="server" CssClass="DROPDOWN" Width="99%">
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 21px;">
                        Rate Discount For</td>
                    <td style="width: 29%; height: 21px;">
                        <asp:DropDownList ID="ddlRateDiscountFor" onchange="ddlRateDiscountForChange()" runat="server"
                            CssClass="DROPDOWN">
                            <asp:ListItem Value="1">General</asp:ListItem>
                            <asp:ListItem Value="2">Party</asp:ListItem>
                            <asp:ListItem Value="3">Category</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%; height: 21px;">
                        *</td>
                    <td style="width: 29%; text-align: right; height: 21px;">
                        <asp:Label ID="lbl_Category" runat="server" Text="Category"></asp:Label>
                    </td>
                    <td style="width: 1%; height: 21px;">
                        <asp:DropDownList ID="ddl_ClientCategory" runat="server" CssClass="DROPDOWN" Width="99%">
                        </asp:DropDownList></td>
                    <td style="width: 1%; height: 21px;">
                    </td>
                </tr>
                <tr id="trParty" runat="server">
                    <td class="TD1" style="width: 20%">
                        Party</td>
                    <td style="width: 29%">
                        <cc1:DDLSearch ID="ddlParty" runat="server" AllowNewText="False" IsCallBack="True"
                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchPartyType" CallBackAfter="2"
                            PostBack="True" InjectJSFunction="" Text="" OnTxtChange="ddlParty_TxtChange" />
                        <asp:HiddenField ID="hdn_PartyCategory_ID" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 20%">
                        Client As</td>
                    <td style="width: 29%">
                        <asp:DropDownList ID="ddlPartyAs" runat="server" CssClass="DROPDOWN">
                            <asp:ListItem Value="1">Consignor</asp:ListItem>
                            <asp:ListItem Value="2">Consignee</asp:ListItem>
                            <asp:ListItem Value="3">Billing Party</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr id="trPromisedQtyPerMonth" runat="server">
                    <td class="TD1" style="width: 20%">
                        Promised Qty Per Month</td>
                    <td style="width: 29%">
                        <asp:TextBox ID="txtPromisedQtyPerMonth" runat="server" onkeypress="return Only_Integers(this,event)"
                            CssClass="TEXTBOXNOS" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%; height: 21px;">
                        *</td>

                    <td style="width: 1%; height: 21px;">
                    </td>                        
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%">
                        Discount Branch</td>
                    <td style="width: 29%">
                        <cc1:DDLSearch ID="ddlDiscountBranch" runat="server" AllowNewText="False" IsCallBack="True"
                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" CallBackAfter="2"
                            PostBack="True" InjectJSFunction="" Text="" OnTxtChange="ddlDiscountBranch_TxtChange" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 20%">
                        Branch As</td>
                    <td style="width: 29%">
                        <asp:DropDownList ID="ddlBranchAs" runat="server" CssClass="DROPDOWN" onchange="ddlBrchAsDlyForChange();">
                            <asp:ListItem Value="1">Booking</asp:ListItem>
                            <asp:ListItem Value="2">Delivery</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr id="trDeliveryArea" runat="server">
                    <td class="TD1" style="width: 20%; height: 17px">
                        Delivery Area</td>
                    <td style="width: 29%; height: 17px">
                        <asp:DropDownList ID="ddl_dlyArea" runat="server" CssClass="DROPDOWN" Width="173px">
                        </asp:DropDownList></td>
                    <td class="TDMANDATORY" style="width: 1%; height: 17px">
                    </td>
                    <td class="TD1" style="width: 20%; height: 17px">
                    </td>
                    <td style="width: 29%; height: 17px">
                    </td>
                    <td class="TDMANDATORY" style="width: 1%; height: 17px">
                    </td>
                </tr>
                <tr>
                    <td class="TD1">
                        Valid From</td>
                    <td>
                        <uc1:WucDatePicker ID="dtpValidFrom" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1">
                        Valid Upto</td>
                    <td>
                        <uc1:WucDatePicker ID="dtpValidUpto" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        &nbsp;</td>
                    <td colspan="5" width="400px">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DataGrid ID="dgGrid" AutoGenerateColumns="False" ShowFooter="True" CellPadding="3"
                                    CssClass="Grid" runat="server" OnCancelCommand="dgGrid_CancelCommand" OnDeleteCommand="dgGrid_DeleteCommand"
                                    OnEditCommand="dgGrid_EditCommand" OnItemCommand="dgGrid_ItemCommand" OnItemDataBound="dgGrid_ItemDataBound"
                                    OnUpdateCommand="dgGrid_UpdateCommand">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Height="15px" Font-Size="11px" Font-Names="Verdana" Font-Bold="True"
                                        HorizontalAlign="Left" ForeColor="Black" BorderStyle="Solid" BorderColor="#9495A2"
                                        BorderWidth="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader">
                                    </HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Minimum Qty">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "MinimumQty")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtMinimumQtyEdit" runat="server" onkeypress="return Only_Integers(this,event)"
                                                    onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "MinimumQty") %>'
                                                    CssClass="TEXTBOXNOS" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtMinimumQtyAdd" runat="server" onkeypress="return Only_Integers(this,event)"
                                                    onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "MinimumQty") %>'
                                                    CssClass="TEXTBOXNOS" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Minimum Freight">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "MinimumFreight")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtMinimumFreightEdit" runat="server" onkeypress="return Only_Numbers(this,event)"
                                                    onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "MinimumFreight") %>'
                                                    CssClass="TEXTBOXNOS" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtMinimumFreightAdd" runat="server" onkeypress="return Only_Numbers(this,event)"
                                                    onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "MinimumFreight") %>'
                                                    CssClass="TEXTBOXNOS" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Discount%">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "DiscountPercent")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDiscountPercentEdit" runat="server" onkeypress="return Only_Numbers(this,event)"
                                                    onfocus="this.select()" onblur="validateDiscount(this);" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountPercent") %>'
                                                    CssClass="TEXTBOXNOS" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtDiscountPercentAdd" runat="server" onkeypress="return Only_Numbers(this,event)"
                                                    onfocus="this.select()" onblur="validateDiscount(this);" Text='<%# DataBinder.Eval(Container.DataItem, "DiscountPercent") %>'
                                                    CssClass="TEXTBOXNOS" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit">
                                            <HeaderStyle Width="10%" />
                                        </asp:EditCommandColumn>
                                        <asp:TemplateColumn HeaderText="Delete">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dgGrid" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClick="btnSave_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="6">
                        <asp:UpdatePanel ID="UpdatePanel" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                                <asp:HiddenField ID="hdnKeyID" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                <asp:AsyncPostBackTrigger ControlID="dgGrid" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>

<script type="text/javascript">
  ddlRateDiscountForChange();
  ddlBrchAsDlyForChange();
</script>

