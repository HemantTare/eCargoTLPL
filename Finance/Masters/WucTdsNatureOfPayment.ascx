<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTdsNatureOfPayment.ascx.cs"
    Inherits="FA_Common_Accounting_Masters_WucTdsNatureOfPayment" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript">
function Allow_To_Save()
{
    return true;
}
</script>
<asp:ScriptManager ID="script_Nature" runat="server">
</asp:ScriptManager>

<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="4">
            <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="TDS NATURE OF PAYMENT"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            &nbsp;</td>
        <td style="width: 25%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 49%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Nature Of Payment:</td>
        <td style="width: 60%">
            <asp:DropDownList ID="ddl_Nature" CssClass="DROPDOWN" runat="server" AutoPostBack="True"
                OnSelectedIndexChanged="ddl_Nature_SelectedIndexChanged">
            </asp:DropDownList></td>
        <td style="width: 1%">
        </td>
        <td style="width: 29%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
            Section:</td>
        <td style="width: 25%">
            <asp:UpdatePanel ID="Update_Section" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Section" CssClass="LABEL" runat="server"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Nature" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 49%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Payment Code:</td>
        <td style="width: 25%">
            <asp:UpdatePanel ID="Update_Payment" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Payment_Code" runat="server" CssClass="LABEL"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Nature" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 49%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td style="width: 25%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 49%">
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
                    <td>
                        <%-- <fieldset style="font-size: 12px; font-style: normal; font-family: Verdana; font-variant: normal">
                            <legend>Nature Of Payment</legend>--%>
                        <table cellpadding="3" cellspacing="3" border="0" width="100%">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="Update_Nature" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DataGrid ID="dg_Nature_Payment" CssClass="GRID" runat="server" AutoGenerateColumns="False"
                                                Width="100%" ShowFooter="true" OnItemDataBound="dg_Nature_Payment_ItemDataBound"
                                                OnItemCommand="dg_Nature_Payment_ItemCommand" OnUpdateCommand="dg_Nature_Payment_UpdateCommand"
                                                OnEditCommand="dg_Nature_Payment_EditCommand" OnCancelCommand="dg_Nature_Payment_CancelCommand"
                                                OnDeleteCommand="dg_Nature_Payment_DeleteCommand">
                                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                <Columns>
                                                    <asp:TemplateColumn Visible="false">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "Nature_Payment_Id")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="false">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "TDS_Deductee_Type_Id")%>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Deductee Type">
                                                        <FooterTemplate>
                                                            <asp:DropDownList ID="ddl_Deductee_Type" runat="server" CssClass="DROPDOWN">
                                                            </asp:DropDownList>
                                                        </FooterTemplate>
                                                        <EditItemTemplate>
                                                            <asp:DropDownList ID="ddl_Deductee_Type" runat="server" CssClass="DROPDOWN">
                                                            </asp:DropDownList>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Deductee" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"TDS_Deductee_Type_Name")%>'
                                                                CssClass="LABEL"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Applicable From">
                                                        <FooterTemplate>
                                                            <ComponentArt:Calendar ID="Picker_Applicable_From" runat="server" PickerFormat="Custom"
                                                                PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                                                                AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" SelectedDate="<%#DateTime.Now%>" />
                                                        </FooterTemplate>
                                                        <EditItemTemplate>
                                                            <ComponentArt:Calendar ID="Picker_Applicable_From" runat="server" PickerFormat="Custom"
                                                                PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                                                                AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" SelectedDate="<%#DateTime.Now%>" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Applicable_From" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Applicable_From","{0:dd MMM yyyy}")%>'
                                                                CssClass="LABEL"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Exemption Limit" FooterStyle-HorizontalAlign="Right"
                                                        ItemStyle-HorizontalAlign="Right">
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txt_Exemption_Limit" Width="95%" Text="0" runat="server" BorderWidth="1px"
                                                                CssClass="TEXTBOXNOS"  onkeypress="return Only_Numbers(this,event)" MaxLength="9"></asp:TextBox>
                                                        </FooterTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txt_Exemption_Limit" Width="95%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Exemption_Limit")%>'
                                                                BorderWidth="1px" CssClass="TEXTBOXNOS"  onkeypress="return Only_Numbers(this,event)" MaxLength="9"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Exemption_Limit" Width="95%" CssClass="LABEL" Text='<%#DataBinder.Eval(Container.DataItem,"Exemption_Limit")%>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Rate" FooterStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">
                                                        <FooterTemplate>
                                                            <asp:TextBox ID="txt_Rate" runat="server" Width="85%" Text="0" 
                                                                BorderWidth="1px" CssClass="TEXTBOXNOS"  onkeypress="return Only_Numbers(this,event)" MaxLength="9"></asp:TextBox>
                                                        </FooterTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txt_Rate" runat="server" Width="85%"
                                                                Text='<%#DataBinder.Eval(Container.DataItem,"Rate")%>' BorderWidth="1px" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)" MaxLength="9"></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_Rate" Width="85%" CssClass="LABEL" Text='<%#DataBinder.Eval(Container.DataItem,"Rate")%>'
                                                                runat="server"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" EditText="Edit" UpdateText="Update">
                                                        <HeaderStyle Width="5%" />
                                                    </asp:EditCommandColumn>
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
                                            <asp:AsyncPostBackTrigger ControlID="ddl_Nature" />
                                            <asp:AsyncPostBackTrigger ControlID="dg_Nature_Payment" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        <%-- </fieldset>--%>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="Upd_pnl_Errors" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="false"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Nature" />
                    <asp:AsyncPostBackTrigger ControlID="dg_Nature_Payment" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="4" align="center">
            <asp:Button ID="btn_Save" runat="server" Text="Save" OnClick="btn_Save_Click" CssClass="BUTTON" /></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
        </td>
        <td style="width: 25%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 49%">
        </td>
    </tr>
</table>
