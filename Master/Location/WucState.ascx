<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucState.ascx.cs" Inherits="Master_Location_WucState" %>

<script language="javascript" type="text/javascript" src="../../Javascript/Master/Location/State.js"></script>

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<asp:ScriptManager ID="scm_State" runat="server" />
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="5">
            &nbsp;<asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="STATE"
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
            <asp:Label ID="lbl_StateName" Text="State Name:" runat="server" meta:resourcekey="lbl_StateNameResource1"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_StateName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                MaxLength="50" meta:resourcekey="txt_StateNameResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Region" Text="Region:" runat="server" meta:resourcekey="lbl_RegionResource1"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:DropDownList ID="ddl_Region" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                OnSelectedIndexChanged="ddl_Region_SelectedIndexChanged" meta:resourcekey="ddl_RegionResource1">
            </asp:DropDownList>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_countryName" Text="Country:" runat="server" meta:resourcekey="lbl_countryResource1"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:UpdatePanel ID="Upd_PnlRegion" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Region" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Country" Font-Bold="True" runat="server" CssClass="LABEL" meta:resourcekey="lbl_CountryResource2"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_NsdlCode" Text="State GST Code (TIN):" runat="server" meta:resourcekey="lbl_NsdlCodeResource1"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_NsdlCode" runat="server" BorderWidth="1px" Width="4%" MaxLength="2"
                onkeypress="return Only_Integers(this,event)" CssClass="TEXTBOX" meta:resourcekey="txt_NsdlCodeResource1"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_StateCode" Text="State Code :" runat="server"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:TextBox ID="txt_StateCode" runat="server" BorderWidth="1px" Width="4%" MaxLength="2"
                CssClass="TEXTBOX"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%; vertical-align: top;" class="TD1">
            <asp:Label ID="lbl_FormType" runat="server" Text="Select Form:" meta:resourcekey="lbl_FormTypeResource1"></asp:Label></td>
        <td style="width: 79%" colspan="3">
            <asp:CheckBoxList ID="ChkList_FormType" CellSpacing="5" RepeatColumns="4" BorderWidth="1px"
                BorderStyle="Solid" BorderColor="Black" runat="server" meta:resourcekey="ChkList_FormTypeResource1" />
        </td>
    </tr>
    <%--
            <asp:DataGrid ID="dg_StateForm" runat="server" AllowSorting="True" AutoGenerateColumns="False" 
            CellPadding="2" CssClass="Grid"   style="border-top-style: none" Width ="98%" >
                <FooterStyle CssClass="GridFooterCss" />
                <HeaderStyle CssClass="GridHeaderCss" />
                <AlternatingItemStyle CssClass ="GridAlternateRowCss" />

                <Columns>
                    <asp:TemplateColumn HeaderText="Attach">
                        <HeaderTemplate>
                            <input id="chkAllItems" type="checkbox" onclick="CheckAllDataGridCheckBoxes(this,'dg_StateForm')" />
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
        <td>
            &nbsp;
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
            <asp:UpdatePanel ID="Upd_Pnl_State" UpdateMode="Conditional" runat="server">
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
