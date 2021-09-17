<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmCouponDiscount.aspx.cs"
    Inherits="Master_Sales_FrmCouponDiscount" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Coupon Discount</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Coupon Discount"></asp:Label>
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
                        
                        <td class="TDMANDATORY" style="width: 1%; height: 21px;">*
                        </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 20%; height: 21px;">
                        </td>
                    <td style="width: 29%; height: 21px;">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%; height: 21px;">
                        </td>
                    <td style="width: 29%; text-align: right; height: 21px;">
                        &nbsp;</td>
                    <td style="width: 188px; height: 21px;">
                        </td>
                    <td style="width: 1%; height: 21px;">
                    </td>
                </tr>
                <tr id="trParty" runat="server">
                    <td class="TD1" style="width: 20%">
                        Party</td>
                    <td style="width: 29%">
                    <asp:UpdatePanel ID="UpdatePanelParty" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <cc1:DDLSearch ID="ddlParty" runat="server" AllowNewText="False" IsCallBack="True"
                                CallBackFunction="Raj.EF.CallBackFunction.CallBack.RegularContractualClient_Search" CallBackAfter="2"
                                PostBack="True" InjectJSFunction="" Text="" OnTxtChange="ddlParty_TxtChange" />
                                <asp:HiddenField ID="hdn_Is_Regular_Client" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="ddlParty" />
                            </Triggers>
                      </asp:UpdatePanel>                                       
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td class="TD1" style="width: 20%">
                        </td>
                    <td style="width: 188px">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%">
                        </td>
                </tr>

                <tr>
                    <td class="TD1">
                        </td>
                    <td>
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%">
                        </td>
                    <td class="TD1">
                        </td>
                    <td style="width: 188px">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%">
                        </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        &nbsp;</td>
                    <td colspan="5" width="400px">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:DataGrid ID="dgGrid" AutoGenerateColumns="False" ShowFooter="True" CellPadding="6"
                                    CssClass="Grid" runat="server" OnCancelCommand="dgGrid_CancelCommand" OnDeleteCommand="dgGrid_DeleteCommand"
                                    OnEditCommand="dgGrid_EditCommand" OnItemCommand="dgGrid_ItemCommand" OnItemDataBound="dgGrid_ItemDataBound"
                                    OnUpdateCommand="dgGrid_UpdateCommand">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Height="15px" Font-Size="11px" Font-Names="Verdana" Font-Bold="True"
                                        HorizontalAlign="Left" ForeColor="Black" BorderStyle="Solid" BorderColor="#9495A2"
                                        BorderWidth="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader">
                                    </HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Coupon No." >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "CouponNo")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtCouponNo" runat="server" onkeypress="return Only_Integers(this,event)"
                                                    onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "CouponNo") %>'
                                                    CssClass="TEXTBOXNOS" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtCouponNo" runat="server" onkeypress="return Only_Integers(this,event)"
                                                    onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "CouponNo") %>'
                                                    CssClass="TEXTBOXNOS" Width="75px" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Amount" >
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Amount")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAmount" runat="server" onkeypress="return Only_Numbers(this,event)"
                                                    onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'
                                                    CssClass="TEXTBOXNOS" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAmount" runat="server" onkeypress="return Only_Numbers(this,event)"
                                                    onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'
                                                    CssClass="TEXTBOXNOS" Width="75px" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="Valid From" HeaderStyle-Width="15%">
                                            <FooterTemplate>
                                                <ComponentArt:Calendar ID="ValidFrom" runat="server" PickerFormat="Custom"
                                                    PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                                                    AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" Width="5px"
                                                    SelectedDate="<%#DateTime.Now%>" />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <ComponentArt:Calendar ID="ValidFrom" runat="server" PickerFormat="Custom"
                                                    PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                                                    AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" Width="5px"
                                                    SelectedDate="<%#DateTime.Now%>" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="lbl_ValidFrom" CssClass="LABEL" Text='<%#DataBinder.Eval(Container.DataItem,"ValidFrom","{0:dd MMM yyyy}")%>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="Valid UpTo" HeaderStyle-Width="15%">
                                            <FooterTemplate>
                                                <ComponentArt:Calendar ID="ValidUpTo" runat="server" PickerFormat="Custom"
                                                    PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                                                    AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" Width="5px"
                                                    SelectedDate="<%#DateTime.Now%>" />
                                            </FooterTemplate>
                                            <EditItemTemplate>
                                                <ComponentArt:Calendar ID="ValidUpTo" runat="server" PickerFormat="Custom"
                                                    PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                                                    AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" Width="5px"
                                                    SelectedDate="<%#DateTime.Now%>" />
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                    <asp:Label ID="lbl_ValidUpTo" CssClass="LABEL" Text='<%#DataBinder.Eval(Container.DataItem,"ValidUpTo","{0:dd MMM yyyy}")%>'
                                                    runat="server"></asp:Label>
                                            </ItemTemplate>
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
                               <%-- <asp:AsyncPostBackTrigger ControlID="ddlParty" />--%>
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                <td class="TD1">
                    Remarks :&nbsp;</td>
                    <td colspan="5">
                        &nbsp;<asp:TextBox ID="txt_Remarks" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                            Height="30px" MaxLength="1000" TextMode="MultiLine" Width="99%"></asp:TextBox></td>
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
                    <td align="left" colspan="6" style="height: 97px">
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


