<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmVarnarMaster.aspx.cs"
    Inherits="Master_Branch_FrmVarnarMaster" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Varnar Master</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scm_Comm">
        </asp:ScriptManager>
        <table class="TABLE" width="100%">
            <tr>
                <td class="TDGRADIENT" colspan="3">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Varnar Master"></asp:Label>
                </td>
            </tr>
            <tr runat="server">
                <td class="TD1" style="width: 100%;" colspan="3">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%; height: 15px;">
                    Name :</td>
                <td runat="server" style="width: 59%; height: 15px;">
                    <asp:TextBox ID="txt_Name" CssClass="TEXTBOX" runat="server" Width="60%" MaxLength="100"
                        onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                </td>
                <td class="TDMANDATORY" style="width: 11%; height: 15px;">
                    *</td>
            </tr>
            <tr id="Tr1" runat="server">
                <td class="TD1" style="width: 100%;" colspan="3">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                    Mobile No. :</td>
                <td style="width: 59%">
                    <asp:TextBox ID="txt_MobileNo" onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOX"
                        runat="server" Width="40%" MaxLength="10" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                </td>
                <td class="TDMANDATORY" style="width: 11%">
                    *</td>
            </tr>
            <tr>
                <td class="TD1" style="width: 100%;" colspan="3">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                    Preferred Mode :</td>
                <td style="width: 59%">
                    <asp:DropDownList ID="ddlPayType" runat="server" CssClass="DROPDOWN" Width="50%"
                        AutoPostBack="true" OnSelectedIndexChanged="ddlPayType_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td class="TDMANDATORY" style="width: 11%">
                    *</td>
            </tr>
            <tr>
                <td class="TD1" style="width: 100%;" colspan="3">
                </td>
            </tr>
            <tr id="tr_BenificiaryName" runat="server">
                <td class="TD1" style="width: 20%">
                    Beneficiary Name :</td>
                <td style="width: 59%">
                    <asp:TextBox ID="txt_Beneficiary" CssClass="TEXTBOX" runat="server" Width="40%" MaxLength="100"
                        onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                    <asp:ImageButton ID="btnCopyName" runat="server" AlternateText="Click To Copy Beneficiary Name."
                        ImageUrl="~/Images/Copy.png" onmouseout="this.src='../../Images/Copy.png'" title="Click To Copy Beneficiary Name."
                        OnClick="btnCopyName_Click" /></td>
                <td class="TDMANDATORY" style="width: 11%">
                    *</td>
            </tr>
            <tr id="tr_Blank0" runat="server">
                <td class="TD1" style="width: 100%;" colspan="3">
                </td>
            </tr>
            <tr id="tr_BenificiaryMobile" runat="server">
                <td class="TD1" style="width: 20%">
                    Beneficiary Mobile :</td>
                <td style="width: 59%">
                    <asp:TextBox ID="txt_BeneficiaryMobile" CssClass="TEXTBOX" runat="server" Width="40%"
                        MaxLength="10" onkeypress="return Only_Integers(this,event)" onfocus="txtbox_onfocus(this)"
                        onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                    <asp:ImageButton ID="btnCopyMobileNo" runat="server" AlternateText="Click To Copy Beneficiary Mobile No."
                        ImageUrl="~/Images/Copy.png" onmouseout="this.src='../../Images/Copy.png'" title="Click To Copy Beneficiary Mobile No."
                        OnClick="btnCopyMobileNo_Click" />
                </td>
                <td class="TDMANDATORY" style="width: 11%">
                    *</td>
            </tr>
            <tr>
                <td class="TD1" style="width: 100%;" colspan="3">
                </td>
            </tr>
            <tr id="tr_AccNo" runat="server">
                <td class="TD1" style="width: 20%">
                    A/c No. :</td>
                <td style="width: 59%">
                    <asp:TextBox ID="txt_AccNo" onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOX"
                        runat="server" Width="40%" MaxLength="30" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                </td>
                <td class="TDMANDATORY" style="width: 11%">
                    *</td>
            </tr>
            <tr id="tr_Blank1" runat="server">
                <td class="TD1" style="width: 100%;" colspan="3">
                </td>
            </tr>
            <tr id="tr_IFSCCode" runat="server">
                <td class="TD1" style="width: 20%">
                    IFSC Code :</td>
                <td style="width: 59%">
                    <asp:TextBox ID="txt_IFSCCode" CssClass="TEXTBOX" runat="server" Width="40%" MaxLength="20"
                        onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                </td>
                <td class="TDMANDATORY" style="width: 11%">
                    *</td>
            </tr>
            <tr id="tr_Blank2" runat="server">
                <td class="TD1" style="width: 100%;" colspan="3">
                </td>
            </tr>
            <tr id="tr_BankName" runat="server">
                <td class="TD1" style="width: 20%">
                    Bank Name :</td>
                <td style="width: 59%">
                    <asp:TextBox ID="txt_BankName" CssClass="TEXTBOX" runat="server" Width="60%" MaxLength="100"
                        onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                </td>
                <td class="TDMANDATORY" style="width: 11%">
                    *</td>
            </tr>
            <tr>
                <td class="TD1" style="width: 100%;" colspan="3">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                    Varai Rates:</td>
                <td style="width: 80%;" colspan="2">
                    <asp:DataGrid ID="dg_Grid" AutoGenerateColumns="False" ShowFooter="False" CellPadding="3"
                        CssClass="Grid" runat="server" Width="30%">
                        <ItemStyle HorizontalAlign="Left" />
                        <HeaderStyle Height="15px" Font-Size="11px" Font-Names="Verdana" Font-Bold="True"
                            HorizontalAlign="Left" ForeColor="Black" BorderStyle="Solid" BorderColor="#9495A2"
                            BorderWidth="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader">
                        </HeaderStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="Packing">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdn_Packing_Id" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Packing_Id")%>' />
                                    <asp:Label ID="lbl_PackingType" CssClass="LABEL" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Packing_Type")%>' />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Rate/Parcel" HeaderStyle-HorizontalAlign="Right"
                                ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRate" runat="server" CssClass="TEXTBOXNOS" onfocus="this.select()"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "Rate")%>' onkeyPress="return Only_Integers(this,event);"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Last Rate" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastRate" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "LastRate")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 100%;" colspan="3">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="3">
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

<script type="text/javascript">
function updateparentdata(VarnarName,MobileNo,BeneficiaryName,BeneficiaryMobile,AccNo,IFSCCode,Bank)
 { 

   window.opener.call_UpdateVarnarMaster(VarnarName,MobileNo,BeneficiaryName,BeneficiaryMobile,AccNo,IFSCCode,Bank);
    
 }
</script>

