<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmBranchBharaiRates.aspx.cs"
    Inherits="Master_Branch_FrmBranchBharaiRates" %>

<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Branch Bharai Rates</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scm_Comm">
        </asp:ScriptManager>
        <table class="TABLE" width="100%">
            <tr>
                <td class="TDGRADIENT" colspan="6">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Bharai Rates"></asp:Label>
                </td>
            </tr>
            <tr runat="server">
                <td class="TD1" style="width: 20%;">
                </td>
                <td style="width: 29%;">
                </td>
                <td class="TDMANDATORY" style="width: 1%;">
                </td>
                <td class="TD1" style="width: 20%;">
                </td>
                <td style="width: 29%;">
                </td>
                <td class="TDMANDATORY" style="width: 1%;">
                </td>
            </tr>
            <tr>
                <td class="TD1">
                    Applicable From</td>
                <td id="td_ApplicableFrom" runat="server">
                    <uc1:WucDatePicker ID="dtpApplicableFrom" runat="server"></uc1:WucDatePicker>
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                    *</td>
                <td class="TD1">
                </td>
                <td>
                    &nbsp;</td>
                <td class="TDMANDATORY" style="width: 1%">
                    *</td>
            </tr>
            <tr>
                <td class="TD1">
                    Branch</td>
                <td>
                    <cc1:DDLSearch ID="ddlBranch" runat="server" AllowNewText="False" IsCallBack="True"
                        CallBackAfter="2" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch"
                        OnTxtChange="ddlBranch_TxtChange" InjectJSFunction="" PostBack="True" Text=""></cc1:DDLSearch>
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                </td>
                <td class="TD1">
                </td>
                <td>
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                    &nbsp;</td>
                <td colspan="5">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upnl_Comm">
                        <ContentTemplate>
                            <div id="Div_Commodity" class="DIV" style="height: auto; width: 100%; text-align: left">
                                <asp:DataGrid ID="dg_Commodity" runat="server" CellPadding="3" CssClass="Grid" AutoGenerateColumns="False"
                                    ShowFooter="True" OnCancelCommand="dg_Commodity_CancelCommand" OnDeleteCommand="dg_Commodity_DeleteCommand"
                                    OnEditCommand="dg_Commodity_EditCommand" OnItemCommand="dg_Commodity_ItemCommand"
                                    OnItemDataBound="dg_Commodity_ItemDataBound" OnUpdateCommand="dg_Commodity_UpdateCommand">
                                    <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                                    <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundColumn DataField="BharaiRateID">
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                            <FooterStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="BharaiRateDetailID">
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                            <FooterStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Item">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddl_Item" runat="server" Width="98%" CssClass="DROPDOWN" OnSelectedIndexChanged="ddl_Item_SelectedIndexChanged"
                                                    AutoPostBack="true" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "Item_Name")) %>
                                            </ItemTemplate>
                                            <HeaderStyle Width="45%" HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Width="45%" HorizontalAlign="Left"></ItemStyle>
                                            <FooterStyle Width="45%" HorizontalAlign="Left"></FooterStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_Item" runat="server" Width="98%" CssClass="DROPDOWN" OnSelectedIndexChanged="ddl_Item_SelectedIndexChanged"
                                                    AutoPostBack="true" />
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Size">
                                            <HeaderStyle Width="25%" HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Width="25%" HorizontalAlign="Left"></ItemStyle>
                                            <FooterStyle Width="25%" HorizontalAlign="Left"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddl_Size" runat="server" Width="98%" CssClass="DROPDOWN" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "SizeName")) %>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_Size" runat="server" CssClass="DROPDOWN" Width="98%">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Rate">
                                            <HeaderStyle Width="30%" HorizontalAlign="right"></HeaderStyle>
                                            <ItemStyle Width="30%" HorizontalAlign="right"></ItemStyle>
                                            <FooterStyle Width="30%" HorizontalAlign="right"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_RatePerArticle" Width="95%" runat="server" CssClass="TEXTBOXNOS"
                                                    onkeyPress="return Only_Numbers(this,event);" MaxLength="7" onfocus="txtbox_onfocus(this)"
                                                    onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "RatePerArticle"))%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_RatePerArticle" Width="95%" runat="server" CssClass="TEXTBOXNOS"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "RatePerArticle")) %>'
                                                    MaxLength="7" onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)"
                                                    onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" UpdateText="Update"
                                            EditText="Edit">
                                            <HeaderStyle Width="5%"></HeaderStyle>
                                        </asp:EditCommandColumn>
                                        <asp:TemplateColumn HeaderText="Delete">
                                            <FooterTemplate>
                                                <asp:LinkButton runat="server" ID="lbtn_Add_Commodity" Text="Add" CommandName="Add"></asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbtn_Delete_Commodity" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%"></HeaderStyle>
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
                <td class="TD1" style="width: 20%">
                </td>
                <td colspan="5" style="text-align: right; width: 80%">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel1">
                        <ContentTemplate>
                            <table class="TABLENOBORDER">
                                <tr>
                                    <td style="width: 20%">
                                    </td>
                                    <td class="TD1" style="width: 10%">
                                         </td>
                                    <td style="width: 7%" class="TD1">
                                      
                                    </td>
                                    <td style="width: 7%" class="TD1">
                                         
                                    </td>
                                    <td style="width: 7%; text-align: right; vertical-align:text-top" class="TD1" runat="server" id="td_length">
                                        <asp:Label Style="text-align: right" ID="lbl_CommodityTotal" runat="server" CssClass="LABEL"
                                            Text="Total :" Width="98%" Font-Bold="True"></asp:Label>&nbsp;
                                    </td>
                                    <td style="width: 7%; text-align: left; vertical-align:text-top" class="TD1" runat="server" id="td_width">
                                         <asp:Label Style="text-align: right" ID="lbl_TotalRate" runat="server" CssClass="LABEL"
                                            Text="0" Width="98%" Font-Bold="True"></asp:Label>
                                        <asp:HiddenField ID="hdn_TotalRate" runat="server"></asp:HiddenField>
                                    </td>
                                    <td style="width: 15%" class="TD1">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
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
                    <asp:Button ID="btnSave" runat="server" CssClass="BUTTON" OnClick="btnSave_Click"
                        Text="Save" />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="6">
                    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                Text="Fields with * mark are mandatory"></asp:Label>
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
