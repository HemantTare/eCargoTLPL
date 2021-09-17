<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDiscountTitle.ascx.cs"
    Inherits="Master_Sales_WucDiscountTitle" %>

<script language="javascript" type="text/javascript" src="../../Javascript/Master/Sales/DiscountTitle.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<asp:ScriptManager ID="scm_DiscountTitle" runat="server" />
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="5">
            &nbsp;<asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="Discount Title"
                meta:resourcekey="lbl_HeadResource1"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 20%">
            &nbsp;</td>
        <td style="width: 79%" colspan="3">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_DiscountTitleName" Text="Title :" runat="server"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_DiscountTitleName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="50" ></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Remarks" Text="Remarks:" runat="server"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_Remarks" runat="server" BorderWidth="1px" TextMode="MultiLine" CssClass="TEXTBOX"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%; vertical-align: top;" class="TD1">
        </td>
        <td style="width: 79%" colspan="3">
            &nbsp;</td>
    </tr>
    <%--
            <asp:DataGrid ID="dg_DiscountTitleForm" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
            CellPadding="2" CssClass="Grid"   style="border-top-style: none" Width ="98%" >
                <FooterStyle CssClass="GridFooterCss" />
                <HeaderStyle CssClass="GridHeaderCss" />
                <AlternatingItemStyle CssClass ="GridAlternateRowCss" />

                <Columns>
                    <asp:TemplateColumn HeaderText="Attach">
                        <HeaderTemplate>
                            <input id="chkAllItems" type="checkbox" onclick="CheckAllDataGridCheckBoxes(this,'dg_DiscountTitleForm')" />
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="Chk_Attach" runat="server"/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Form Id" Visible="false">
                    <ItemTemplate>
                     <asp:Label ID="lbl_FormId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Form_Id") %>' />
                    </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Select Form">
                    <ItemTemplate>
                    <asp:Label ID="lbl_FormName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Form_Name") %>' />
                    </ItemTemplate>
                    </asp:TemplateColumn>                    
                    </Columns>
                    </asp:DataGrid>
                    </td>
                    </tr>          --%>
    <tr>
        <td class="TD1" style="vertical-align: top; width: 20%; height: 21px;">
        </td>
        <td colspan="3" style="width: 79%; height: 21px;">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" Text='Save' CssClass="BUTTON" OnClientClick="return validateUI()"
                OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" /></td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:HiddenField ID="hdf_ResourecString" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_DiscountTitle" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Font-Bold="True"
                        EnableViewState="False" Text="Fields With * Mark Are Mandatory" meta:resourcekey="lbl_ErrorsResource2"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
