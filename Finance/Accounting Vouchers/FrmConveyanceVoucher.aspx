<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmConveyanceVoucher.aspx.cs"
    Inherits="Finance_Accounting_Vouchers_FrmConveyanceVoucher" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/txtsearch_common.js"></script>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc2" %>

<script type="text/javascript">

function Allow_To_Save()
{
    var ATS = false;
    var txt_Name = document.getElementById('txt_Name');
    var txt_Reason = document.getElementById('txt_Reason');

    var lblErrors = document.getElementById('lblErrors');
    
    
    if (txt_Name.value  == '')
    {
        lblErrors.innerHTML = 'Enter Name';
        txt_Name.focus();
    }
    else if (txt_Reason.value  == '')
    {
        lblErrors.innerHTML = 'Enter Proper Reason';
        txt_Reason.focus();
    }
    
    else
    {
        ATS = true;
    }
    return ATS;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Conveyance Voucher</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div style="vertical-align: top;">
            <table class="TABLE" style="width: 100%">
                <tr>
                    <td class="TDGRADIENT" style="width: 100%" colspan="4">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Conveyance Voucher"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_VoucherNo" CssClass="LABEL" Text="Voucher No :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <asp:Label ID="lbl_VoucherNoValue" CssClass="LABEL" Style="font-weight: bolder" runat="server"></asp:Label></td>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_Date" CssClass="LABEL" Text="Date :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <uc1:WucDatePicker ID="dtpVoucherDate" runat="server"></uc1:WucDatePicker>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_Name" CssClass="LABEL" Text="Paid To :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <asp:TextBox ID="txt_Name" runat="server" CssClass="TEXTBOX" MaxLength="100" Width="99%"></asp:TextBox></td>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_PaidToMobile" CssClass="LABEL" Text="Mobile No.  :" runat="server"></asp:Label></td>
                    <td style="width: 30%;">
                        <asp:TextBox ID="txt_PaidToMobile" runat="server" CssClass="TEXTBOXNOS" MaxLength="10"
                            onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)" onkeypress="return Only_Integers(this,event)"
                            Width="50%"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_Reason" CssClass="LABEL" Text="Reason :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 50%;" colspan="2">
                        <asp:TextBox ID="txt_Reason" runat="server" CssClass="TEXTBOX" MaxLength="500" Width="99%"></asp:TextBox>
                    </td>
                    <td style="width: 30%;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="4">
                        <asp:UpdatePanel ID="upnl_Comm" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="Div_Commodity" class="DIV" style="height: 150px; width: 100%; text-align: left">
                                    <asp:DataGrid ID="dg_Commodity" runat="server" CellPadding="3" CssClass="Grid" AutoGenerateColumns="False"
                                        ShowFooter="True" OnCancelCommand="dg_Commodity_CancelCommand" OnDeleteCommand="dg_Commodity_DeleteCommand"
                                        OnEditCommand="dg_Commodity_EditCommand" OnItemCommand="dg_Commodity_ItemCommand"
                                        OnItemDataBound="dg_Commodity_ItemDataBound" OnUpdateCommand="dg_Commodity_UpdateCommand"
                                        Width="85%">
                                        <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                                        <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="From">
                                                <HeaderStyle Width="20%" HorizontalAlign="left"></HeaderStyle>
                                                <ItemStyle Width="20%" HorizontalAlign="left"></ItemStyle>
                                                <FooterStyle Width="20%" HorizontalAlign="left"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_FromLoc" autocomplete="off" Width="95%" runat="server" CssClass="TEXTBOX"
                                                        MaxLength="50" EnableViewState="False"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "From_Location")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_FromLoc" autocomplete="off" Width="95%" runat="server" CssClass="TEXTBOX"
                                                        MaxLength="50" EnableViewState="False" Text='<%# DataBinder.Eval(Container.DataItem, "From_Location") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="To">
                                                <HeaderStyle Width="20%" HorizontalAlign="left"></HeaderStyle>
                                                <ItemStyle Width="20%" HorizontalAlign="left"></ItemStyle>
                                                <FooterStyle Width="20%" HorizontalAlign="left"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_ToLoc" autocomplete="off" Width="95%" runat="server" CssClass="TEXTBOX"
                                                        MaxLength="50" EnableViewState="False"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "To_Location")%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_ToLoc" autocomplete="off" Width="95%" runat="server" CssClass="TEXTBOX"
                                                        MaxLength="50" EnableViewState="False" Text='<%# DataBinder.Eval(Container.DataItem, "To_Location") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Mode">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:DropDownList ID="ddl_TravelBy" runat="server" Width="98%" CssClass="DROPDOWN">
                                                    </asp:DropDownList>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# (DataBinder.Eval(Container.DataItem, "Travel_By"))%>
                                                </ItemTemplate>
                                                <HeaderStyle Width="20%" HorizontalAlign="Left"></HeaderStyle>
                                                <ItemStyle Width="20%" HorizontalAlign="Left"></ItemStyle>
                                                <FooterStyle Width="20%" HorizontalAlign="Left"></FooterStyle>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddl_TravelBy" runat="server" Width="98%" CssClass="DROPDOWN">
                                                    </asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Amount">
                                                <HeaderStyle Width="20%" HorizontalAlign="right"></HeaderStyle>
                                                <ItemStyle Width="20%" HorizontalAlign="right"></ItemStyle>
                                                <FooterStyle Width="20%" HorizontalAlign="right"></FooterStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txt_Amount" Width="95%" runat="server" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);"
                                                        MaxLength="8" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Amount"))%>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txt_Amount" Width="95%" runat="server" CssClass="TEXTBOXNOS" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Amount")) %>'
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
                    <td style="width: 100%" colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" class="TD1">
                        &nbsp;
                    </td>
                    <td style="width: 50%;" class="TD1" colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lbl_TotalAmount" CssClass="LABEL" Text="0.00" runat="server"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 30%;">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_Remarks" CssClass="LABEL" Text="Remarks :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 80%;" colspan="3">
                        <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX" MaxLength="500" onblur="txtbox_onlostfocus(this);"
                            onfocus="txtbox_onfocus(this)" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr class="HIDEGRIDCOL">
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_PaidTo" CssClass="LABEL" Text="Paid To :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <asp:TextBox ID="txt_PaidTo" runat="server" CssClass="TEXTBOX" MaxLength="100" onblur="txtbox_onlostfocus(this);"
                            onfocus="txtbox_onfocus(this)" Width="99%"></asp:TextBox>
                    </td>
                    <td style="width: 20%" class="TD1">
                        &nbsp;</td>
                    <td style="width: 30%">
                        </td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="4">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%;" class="TD1">
                        <asp:Label ID="lbl_PaidByH" CssClass="LABEL" Text="Paid By :" runat="server"></asp:Label>
                    </td>
                    <td style="width: 30%;">
                        <asp:Label ID="lbl_PaidBy" CssClass="LABEL" Text="" runat="server"></asp:Label>
                    </td>
                    <td style="width: 20%" class="TD1">
                        &nbsp;
                    </td>
                    <td style="width: 30%">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
        <div>
            <table class="TABLE" style="width: 100%; text-align: left;">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" Text="Fields with * mark are mandatory"></asp:Label>&nbsp;
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Save_Exit" />
                                <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <asp:HiddenField ID="hdnKeyID" runat="server" />
                    </td>
                </tr>
            </table>
            <table class="TABLE" style="width: 100%; text-align: center;">
                <tr>
                    <td style="width: 100%">
                        &nbsp;<asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
                            OnClientClick="return Allow_To_Save()" OnClick="btn_Save_Exit_Click"></asp:Button>&nbsp;
                        <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="Exit" OnClick="btn_Close_Click">
                        </asp:Button>&nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
