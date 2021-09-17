<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucAgeing.ascx.cs" Inherits="Finance_Reports_WucAgeing" %>
<script language="javascript" src="../../Javascript/Common.js" type="text/javascript"></script>

<asp:ScriptManager ID="SM_Ageing" runat="server">
</asp:ScriptManager>
<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="2">
            &nbsp;<asp:Label ID="lbl_Heading" runat="server" Text="AGEING" CssClass="HEADINGLABEL"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="2" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
                    <td>
                        <fieldset>
                            <legend>Ageing Details</legend>
                            <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel_Ageing" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:DataGrid ID="dg_Ageing" runat="server" CssClass="GRID" Width="100%" ShowFooter="True"
                                                    AutoGenerateColumns="False" OnCancelCommand="dg_Ageing_CancelCommand" OnEditCommand="dg_Ageing_EditCommand"
                                                    OnItemCommand="dg_Ageing_ItemCommand" OnItemDataBound="dg_Ageing_ItemDataBound"
                                                    OnUpdateCommand="dg_Ageing_UpdateCommand">
                                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                    <Columns>
                                                        <asp:TemplateColumn Visible="false">
                                                            <ItemTemplate>
                                                                <%#DataBinder.Eval(Container.DataItem, "Sr_No")%>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn FooterStyle-Font-Bold="true" ItemStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                                                            ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                From :
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                From :
                                                            </FooterTemplate>
                                                            <EditItemTemplate>
                                                                From :
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="12%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn FooterStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_From_Days" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"From_Days")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:Label ID="lbl_From_Days" runat="server"></asp:Label>
                                                            </FooterTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lbl_From_Days" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"From_Days")%>'></asp:Label>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="15%" HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn FooterStyle-Font-Bold="true" ItemStyle-Font-Bold="true" FooterStyle-HorizontalAlign="Right"
                                                            ItemStyle-HorizontalAlign="Right">
                                                            <ItemTemplate>
                                                                To :
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                To :
                                                            </FooterTemplate>
                                                            <EditItemTemplate>
                                                                To :
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="12%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_To_Days" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"To_Days")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txt_To_Days" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                                                    MaxLength="8" Width="94%"  onkeypress="return Only_Integers(this,event)" ></asp:TextBox>
                                                            </FooterTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_To_Days" runat="server" BorderWidth="1px" CssClass="TEXTBOXNOS"
                                                                    MaxLength="8" Width="94%" Text=' <%#DataBinder.Eval(Container.DataItem, "To_Days")%>'
                                                                    onkeypress="return Only_Integers(this,event)"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="15%" HorizontalAlign="Right" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                            <FooterStyle HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>
                                                        <asp:EditCommandColumn FooterStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-Width="10%" ItemStyle-Width="10%" UpdateText="Update" CancelText="Cancel"
                                                            EditText="Edit"></asp:EditCommandColumn>
                                                        <asp:TemplateColumn FooterStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtn_Delete" CommandName="Delete" runat="server" Text="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lbtn_Add" CommandName="ADD" runat="server" Text="Add"></asp:LinkButton>
                                                            </FooterTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:AsyncPostBackTrigger ControlID="dg_Ageing" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 50%" align="right">
            <asp:Button ID="btn_Ageing_By_BillDate" runat="server" CssClass="BUTTON" Text="Ageing By Bill Date"
                OnClick="btn_Ageing_By_BillDate_Click" /></td>
        <td style="width: 50%" align="left">
            <asp:Button ID="btn_Ageing_By_DueDate" runat="server" CssClass="BUTTON" Text="Ageing By Due Date"
                OnClick="btn_Ageing_By_DueDate_Click" /></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:UpdatePanel ID="UpdatePanel_Error" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    &nbsp;<asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="false" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_Ageing" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Ageing_By_BillDate" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Ageing_By_DueDate" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
<asp:UpdatePanel ID="UpdatePanel_Hdn_Generate_Value" runat="server" UpdateMode="Conditional">
    <ContentTemplate>
        <asp:HiddenField ID="hdn_Generate_From_Value" runat="server" Value="0" />
        <asp:HiddenField ID="hdn_Old_Value" runat="server" />
        <asp:HiddenField ID="hdn_Max_Value" runat="server" />
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="dg_Ageing" />
    </Triggers>
</asp:UpdatePanel>
