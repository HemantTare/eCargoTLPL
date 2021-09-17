<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucFBTCategories.ascx.cs"
    Inherits="FA_Common_Accounting_Masters_WucFBTCategories" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<asp:ScriptManager ID="scm_FBTCategories" runat="server">
</asp:ScriptManager>
<script type="text/javascript" language="javascript">
function Allow_To_Save()
{
    return true;
}
</script>

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="FBT CATEGORIES"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="left" class="TD1" valign="middle" style="width: 20%">
            FBT Categories</td>
        <td align="left" class="TD2" style="width: 1%" valign="middle">
            :</td>
        <td align="left" valign="middle" class="TD2" style="width: 90%">
            <asp:DropDownList ID="ddl_FBT_Categories" runat="server" CssClass="DROPDOWN" AutoPostBack="true"
                OnSelectedIndexChanged="ddl_FBT_Categories_SelectedIndexChanged">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td align="left" class="TD1" valign="middle" style="width: 20%">
            Section</td>
        <td align="left" class="TD2" style="width: 1%" valign="middle">
            :</td>
        <td align="left" valign="middle" class="TD2" style="width: 90%">
            <asp:UpdatePanel ID="up_Section" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_FBT_Categories" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Section" runat="server" CssClass="LABEL"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td align="left" colspan="3" valign="middle">
            <asp:UpdatePanel ID="up_Grid" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_FBT_Categories" />
                    <asp:AsyncPostBackTrigger ControlID="dg_Category_Details" />
                </Triggers>
                <ContentTemplate>
                    <asp:DataGrid ID="dg_Category_Details" runat="server" AutoGenerateColumns="False"
                        CssClass="GRID" ShowFooter="True" OnItemDataBound="dg_Category_Details_ItemDataBound"
                        OnItemCommand="dg_Category_Details_ItemCommand" OnEditCommand="dg_Category_Details_EditCommand"
                        OnCancelCommand="dg_Category_Details_CancelCommand" OnUpdateCommand="dg_Category_Details_UpdateCommand"
                        OnDeleteCommand="dg_Category_Details_DeleteCommand">
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <PagerStyle CssClass="GRIDPAGERCSS" />
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <Columns>
                            <asp:TemplateColumn Visible="false">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "FBT_Category_Id")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Assesse Category">
                                <FooterTemplate>
                                    <asp:DropDownList ID="ddl_Assessee_Category" runat="server" CssClass="DROPDOWN">
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"Assesse_Category_Name") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddl_Assessee_Category" runat="server" CssClass="DROPDOWN">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <HeaderStyle Width="50%" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Applicable From ">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem,"Applicable_From","{0:dd MMM yyyy}")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <ComponentArt:Calendar ID="dtp_Applicable" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                                        ControlType="Picker" PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True"
                                        MinDate="1900-01-01" Width="5px" SelectedDate="<%#DateTime.Now%>" />
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <ComponentArt:Calendar ID="dtp_Applicable" runat="server" PickerFormat="Custom" PickerCustomFormat="MMMM d yyyy"
                                        ControlType="Picker" PickerCssClass="picker" AllowDaySelection="True" AllowMonthSelection="True"
                                        MinDate="1900-01-01" Width="5px" SelectedDate="<%#DateTime.Now%>" />
                                </EditItemTemplate>
                                <HeaderStyle Width="20%" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Eligible %" FooterStyle-HorizontalAlign="Right">
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_Eligible" runat="server" Text="0" CssClass="TEXTBOXNOS" Width="70%"></asp:TextBox>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Eligible")%>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Eligible" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Eligible")%>'
                                        CssClass="TEXTBOXNOS" Width="70%"></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderStyle Width="15%" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update">
                                <HeaderStyle Width="10%" />
                            </asp:EditCommandColumn>
                            <asp:TemplateColumn>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtn_Add" runat="server" CommandName="ADD">Add</asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_Delete" runat="server" CommandName="DELETE">Delete</asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle Width="10%" />
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td align="center" valign="middle" colspan="3">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click" /></td>
    </tr>
    <tr>
        <td align="left" colspan="3" valign="middle">
            <asp:UpdatePanel ID="up_Errors" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_FBT_Categories" />
                    <asp:AsyncPostBackTrigger ControlID="dg_Category_Details" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR"></asp:Label>
                    <asp:HiddenField ID="hdf_ResourceString" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
