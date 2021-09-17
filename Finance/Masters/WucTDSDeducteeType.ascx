<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTDSDeducteeType.ascx.cs"
    Inherits="FA_Common_Accounting_Masters_WucTDSDeducteeType" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" src="../../JavaScript/Common.js"></script>
<script type="text/javascript" language="javascript">
function Allow_To_Save()
{
    return true;
}
</script>

<asp:ScriptManager ID="Script_update" runat="server">
</asp:ScriptManager>
<%--/// Author        : Ankit Champaneriya 
/// Created On    : 17/10/2008
/// description   : TDS Deductee type details
--%>
<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="4">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Deductee Type"
                meta:resourcekey="lbl_HeadingResource1"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
            &nbsp;</td>
        <td style="width: 40%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 49%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
            Deductee Type:</td>
        <td style="width: 40%">
            <asp:DropDownList ID="ddl_Deductee_Type" AutoPostBack="True" CssClass="DROPDOWN"
                runat="server" OnSelectedIndexChanged="ddl_Deductee_Type_SelectedIndexChanged"
                meta:resourcekey="ddl_Deductee_TypeResource1">
            </asp:DropDownList></td>
        <td style="width: 1%">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
            Residential Status:</td>
        <td style="width: 40%">
            <asp:UpdatePanel ID="Update_Residence" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_Residential_Status" runat="server" CssClass="DROPDOWN"
                        meta:resourcekey="ddl_Residential_StatusResource1">
                        <asp:ListItem Text="Resident" Value="0" meta:resourcekey="ListItemResource1"></asp:ListItem>
                        <asp:ListItem Text="Non Resident" Value="1" meta:resourcekey="ListItemResource2"></asp:ListItem>
                    </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Deductee_Type" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
            Deductee Status:</td>
        <td style="width: 40%">
            <asp:UpdatePanel ID="Update_Deductee_Status" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_Deductee_Status" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_Deductee_StatusResource1">
                        <asp:ListItem Text="Company" Value="0" meta:resourcekey="ListItemResource3"></asp:ListItem>
                        <asp:ListItem Text="Non Company" Value="1" meta:resourcekey="ListItemResource4"></asp:ListItem>
                    </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Deductee_Type" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td>
        </td>
    </tr>
    
    <tr>
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
                    <td>
                        <%--<fieldset style="font-size: 12px; font-style: normal; font-family: Verdana; font-variant: normal">
                            <legend>Deuctee TDS Details</legend>--%>
                        <table cellpadding="3" cellspacing="3" border="0" width="100%">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="Update_Deductee" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DataGrid ID="dg_Deuctee_TDS" CssClass="GRID" runat="server" AutoGenerateColumns="False"
                                                Width="100%" ShowFooter="true" OnItemDataBound="dg_Deuctee_TDS_ItemDataBound"
                                                OnEditCommand="dg_Deuctee_TDS_EditCommand" OnUpdateCommand="dg_Deuctee_TDS_UpdateCommand"
                                                OnItemCommand="dg_Deuctee_TDS_ItemCommand" OnCancelCommand="dg_Deuctee_TDS_CancelCommand"
                                                OnDeleteCommand="dg_Deuctee_TDS_DeleteCommand">
                                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                <Columns>
                                                    <asp:TemplateColumn Visible="false">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "TDS_Deductee_Type_Id")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Applicable From" HeaderStyle-Width="15%">
                                                        <FooterTemplate>
                                                            <ComponentArt:Calendar ID="Picker_Applicable_From" runat="server" PickerFormat="Custom"
                                                                PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                                                                AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" Width="5px"
                                                                SelectedDate="<%#DateTime.Now%>" />
                                                        </FooterTemplate>
                                                        <EditItemTemplate>
                                                            <ComponentArt:Calendar ID="Picker_Applicable_From" runat="server" PickerFormat="Custom"
                                                                PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                                                                AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" Width="5px"
                                                                SelectedDate="<%#DateTime.Now%>" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Applicable_From" CssClass="LABEL" Text='<%#DataBinder.Eval(Container.DataItem,"Applicable_From","{0:dd MMM yyyy}")%>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Surcharge Exemption Limit" FooterStyle-HorizontalAlign="Right"
                                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="24%">
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txt_Exemption_Limit" Text="0" runat="server" onkeypress="return Only_Numbers(this,event)" MaxLength="9"
                                                                Width="95%" BorderWidth="1px" CssClass="TEXTBOXNOS"></asp:TextBox>
                                                        </FooterTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txt_Exemption_Limit" runat="server" onkeypress="return Only_Numbers(this,event)" MaxLength="9"
                                                                Width="95%" Text='<%#DataBinder.Eval(Container.DataItem,"Exemption_Limit")%>'
                                                                BorderWidth="1px" CssClass="TEXTBOXNOS"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Exemption_Limit" CssClass="LABEL" Text='<%#DataBinder.Eval(Container.DataItem,"Exemption_Limit")%>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Surcharge" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right"
                                                        HeaderStyle-Width="14%">
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txt_Surcharge" runat="server" Text="0" Width="95%" BorderWidth="1px"
                                                                CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)" MaxLength="9"></asp:TextBox>
                                                        </FooterTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txt_Surcharge" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Surcharge")%>'
                                                                Width="95%" BorderWidth="1px" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)" MaxLength="9"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Surcharge" CssClass="LABEL" Text='<%#DataBinder.Eval(Container.DataItem,"Surcharge")%>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Addl. Surcharge (Cess)" FooterStyle-HorizontalAlign="Right"
                                                        ItemStyle-HorizontalAlign="Right" HeaderStyle-Width="19%">
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txt_Addl_Surcharge" runat="server" Text="0" onkeypress="return Only_Numbers(this,event)" MaxLength="9"
                                                                Width="95%" BorderWidth="1px" CssClass="TEXTBOXNOS"></asp:TextBox>
                                                        </FooterTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txt_Addl_Surcharge" onkeypress="return Only_Numbers(this,event)" MaxLength="9"
                                                                Text='<%#DataBinder.Eval(Container.DataItem,"Addl_Surcharge_Cess")%>' runat="server"
                                                                Width="95%" BorderWidth="1px" CssClass="TEXTBOXNOS"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Addl_Surcharge" Text='<%#DataBinder.Eval(Container.DataItem,"Addl_Surcharge_Cess")%>'
                                                                CssClass="LABEL" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Addl. Education (Cess)" HeaderStyle-Width="19%" FooterStyle-HorizontalAlign="Right"
                                                        ItemStyle-HorizontalAlign="Right">
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txt_Edu_Add_Education" runat="server" Text="0" onkeypress="return Only_Numbers(this,event)" MaxLength="9"
                                                                Width="95%" BorderWidth="1px" CssClass="TEXTBOXNOS"></asp:TextBox>
                                                        </FooterTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txt_Edu_Add_Education" onkeypress="return Only_Numbers(this,event)" MaxLength="9"
                                                                Text='<%#DataBinder.Eval(Container.DataItem,"Addl_Education_Cess")%>' runat="server"
                                                                Width="95%" BorderWidth="1px" CssClass="TEXTBOXNOS"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Addl_Education" Text='<%#DataBinder.Eval(Container.DataItem,"Addl_Education_Cess")%>'
                                                                CssClass="LABEL" runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:EditCommandColumn HeaderStyle-Width="5%" HeaderText="Edit" CancelText="Cancel"
                                                        EditText="Edit" UpdateText="Update"></asp:EditCommandColumn>
                                                    <asp:TemplateColumn HeaderText="Add/Delete">
                                                        <FooterTemplate>
                                                            <asp:LinkButton ID="lbtn_Add" CommandName="Add" Text="Add" runat="server"></asp:LinkButton>
                                                        </FooterTemplate>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lbtn_Delete" CommandName="Delete" Text="Delete" runat="server"></asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dg_Deuctee_TDS" />
                                            <asp:AsyncPostBackTrigger ControlID="ddl_Deductee_Type" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        <%--</fieldset>--%>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"
                        meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_Deuctee_TDS" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_Deductee_Type" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="4">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" OnClick="btn_Save_Click"
                Text="Save" meta:resourcekey="btn_SaveResource1" /></td>
    </tr>
</table>
