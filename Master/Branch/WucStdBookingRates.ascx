<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucStdBookingRates.ascx.cs"
    Inherits="Master_Branch_WucStdBookingRates" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/CommonReports.js"></script>
<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" src="../../Javascript/DatePicker.js"></script>

<asp:ScriptManager ID="scm_StandardRate" runat="server">
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    function check_profitracio()
    {
        var txt_ProfitRatio = document.getElementById('<%=txt_ProfitRatio.ClientID%>');
        if(val(txt_ProfitRatio.value) > 100)
        {
            alert('Profit Ratio can not be greater than 100');
            txt_ProfitRatio.value = 0;
            txt_ProfitRatio.focus();
            return false;
        }
    }

function get_button_nullsession_clientid()
{
    btn_nullsession = document.getElementById('<%=btn_null_sessions.ClientID%>');
}

</script>

<table class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            &nbsp;
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Standard Booking Rates"
                meta:resourcekey="lbl_HeadingResource1"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Applicable_From" runat="server" CssClass="LABEL" Text="Applicable From :"
                meta:resourcekey="lbl_Applicable_FromResource1"></asp:Label>
        </td>
        <td class="TDMANDATORY" style="width: 29%">
            <uc1:WucDatePicker ID="dtp_ApplicableFrom" runat="server" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td style="width: 50%" colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Booking_Branch" runat="server" CssClass="LABEL" Text="Booking Branch :"
                meta:resourcekey="lbl_Booking_BranchResource1"></asp:Label>
        </td>
        <td class="TDMANDATORY" style="width: 29%">
            <cc1:DDLSearch ID="DDL_BookingBranch" runat="server" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch"
                CallBackAfter="2" PostBack="True" OnTxtChange="DDL_BookingBranch_TxtChange" AllowNewText="True"
                meta:resourcekey="DDL_BookingBranchResource2" />
            &nbsp;*
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Delivery_Branch" runat="server" CssClass="LABEL" Text="Delivery Branch :"
                meta:resourcekey="lbl_Delivery_BranchResource1"></asp:Label>
        </td>
        <td class="TDMANDATORY" style="width: 29%">
            <cc1:DDLSearch ID="DDL_DeliveryBranch" runat="server" IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch"
                CallBackAfter="2" PostBack="True" OnTxtChange="DDL_DeliveryBranch_TxtChange"
                AllowNewText="True" meta:resourcekey="DDL_DeliveryBranchResource2" />
            &nbsp;*
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="80%">
                        <tr>
                            <td style="width: 100%">
                                <asp:Panel ID="pnl_Rate" runat="server" CssClass="PANEL" HorizontalAlign="Center"
                                    GroupingText="&lt;b&gt;Crossing Points&lt;/b&gt;" meta:resourcekey="pnl_RateResource2">
                                    <asp:DataGrid ID="dg_StandardRate" runat="server" AutoGenerateColumns="False" CssClass="GRID"
                                        Width="98%" ShowFooter="True" Style="border-top-style: none" OnCancelCommand="dg_StandardRate_CancelCommand"
                                        OnDeleteCommand="dg_StandardRate_DeleteCommand" OnEditCommand="dg_StandardRate_EditCommand"
                                        OnItemCommand="dg_StandardRate_ItemCommand" OnItemDataBound="dg_StandardRate_ItemDataBound"
                                        OnUpdateCommand="dg_StandardRate_UpdateCommand" meta:resourcekey="dg_StandardRateResource2">
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="From Branch">
                                                <FooterTemplate>
                                                    <asp:Label ID="lbl_FromBranch" Font-Bold="True" runat="server" CssClass="LABEL" meta:resourcekey="lbl_FromBranchResource3" />
                                                    <asp:HiddenField ID="hdn_FromBranch_Id" runat="server" />
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "From_Branch_Name") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lbl_FromBranch" Font-Bold="True" runat="server" CssClass="LABEL" meta:resourcekey="lbl_FromBranchResource4" />
                                                    <asp:HiddenField ID="hdn_FromBranch_Id" runat="server" />
                                                </EditItemTemplate>
                                                <HeaderStyle Width="20%" HorizontalAlign="Left" />
                                                <ItemStyle Width="20%" HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="To Branch">
                                                <FooterTemplate>
                                                    <cc1:DDLSearch ID="ddl_ToBranch" CallBackAfter="2" runat="server" IsCallBack="True"
                                                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" PostBack="True"
                                                        OnTxtChange="ddl_ToBranch_TxtChange" AllowNewText="True" InjectJSFunction=""
                                                        meta:resourcekey="ddl_ToBranchResource3" Text="" />
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "To_Branch_Name") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <cc1:DDLSearch ID="ddl_ToBranch" CallBackAfter="2" runat="server" IsCallBack="True"
                                                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" PostBack="True"
                                                        OnTxtChange="ddl_ToBranch_TxtChange" AllowNewText="True" InjectJSFunction=""
                                                        meta:resourcekey="ddl_ToBranchResource4" Text="" />
                                                </EditItemTemplate>
                                                <HeaderStyle Width="25%" HorizontalAlign="Left" />
                                                <ItemStyle Width="25%" HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Crossing Rate/100kg">
                                                <FooterTemplate>
                                                    <asp:Label Font-Bold="True" ID="lbl_CrossingRate" runat="server" Text="0" CssClass="TEXTBOXNOS"
                                                        meta:resourcekey="lbl_CrossingRateResource3" />
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <%# DataBinder.Eval(Container.DataItem, "Crossing_Rate") %>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label Font-Bold="True" ID="lbl_CrossingRate" runat="server" Text="0" CssClass="TEXTBOXNOS"
                                                        meta:resourcekey="lbl_CrossingRateResource4" />
                                                </EditItemTemplate>
                                                <HeaderStyle Width="27%" HorizontalAlign="Right" />
                                                <ItemStyle Width="27%" HorizontalAlign="Right" />
                                            </asp:TemplateColumn>
                                            <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit"
                                                meta:resourcekey="EditCommandColumnResource2">
                                                <HeaderStyle Width="15%" />
                                            </asp:EditCommandColumn>
                                            <asp:TemplateColumn HeaderText="Delete">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" meta:resourcekey="lbtn_AddResource2"></asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete"
                                                        meta:resourcekey="lbtn_DeleteResource2"></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle Width="10%" />
                                                <ItemStyle Width="10%" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_StandardRate" />
                    <asp:AsyncPostBackTrigger ControlID="DDL_BookingBranch" />
                    <asp:AsyncPostBackTrigger ControlID="DDL_DeliveryBranch" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td class="TD1" style="width: 29%">
            <asp:Label ID="lbl_Profit_Ratio" runat="server" CssClass="LABEL" Text="Profit Ratio :"
                meta:resourcekey="lbl_Profit_RatioResource2"></asp:Label>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td style="width: 20%">
            <asp:TextBox ID="txt_ProfitRatio" AutoPostBack="True" onblur="return check_profitracio()"
                Text="0" runat="server" CssClass="TEXTBOXNOS" MaxLength="6" OnTextChanged="txt_ProfitRatio_TextChanged"
                meta:resourcekey="txt_ProfitRatioResource2"></asp:TextBox>
        </td>
        <td style="width: 29%">
            <b>%</b></td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td class="TD1" style="width: 29%">
            <asp:Label ID="lbl_Booking_Rate" runat="server" CssClass="LABEL" Text="Booking Rate :"
                meta:resourcekey="lbl_Booking_RateResource1"></asp:Label>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
        <td style="width: 20%">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_BookingRate" BorderWidth="1px" Font-Bold="True" Text="0" runat="server"
                        CssClass="TEXTBOXNOS" meta:resourcekey="lbl_BookingRateResource2"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_StandardRate" />
                    <asp:AsyncPostBackTrigger ControlID="DDL_BookingBranch" />
                    <asp:AsyncPostBackTrigger ControlID="txt_ProfitRatio" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 29%">
            <b>
                <asp:Label ID="lbl_StdFreightRate" runat="server" meta:resourcekey="lbl_StdFreightRateResource1"></asp:Label></b></td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" style="text-align: center">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" AccessKey="N"
                OnClick="btn_Save_Click" meta:resourcekey="btn_SaveResource1" />
                <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" Text="Close Window" OnClick="btn_null_session_Click" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource2"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_StandardRate" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
</table>
