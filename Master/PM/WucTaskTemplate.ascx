<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTaskTemplate.ascx.cs"
    Inherits="Master_PM_WucTaskTemplate" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script type="text/javascript" src="../../JavaScript/Master/PM/TaskTemplate.js"></script>

<script type="text/javascript" src="../../JavaScript/ddlsearch.js"></script>

<script type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<asp:ScriptManager ID="scm_TaskTemplate" runat="server" />
<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            &nbsp;
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Task Template"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr id="tr_task_template" runat="server">
        <td class="TD1" style="width: 25%">
            <%--Task Template:--%>
            <asp:Label ID="lbl_Task_Template" runat="server" CssClass="LABEL" Text="Task Template:"
                EnableViewState="false"></asp:Label>
        </td>
        <td style="width: 66%" colspan="4">
            <asp:DropDownList ID="ddl_Task_Template" TabIndex="1" CssClass="DROPDOWN" runat="server"
                Width="50%" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
            <%--Task Template Name:--%>
            <asp:Label ID="lbl_Task_Template_Name" runat="server" CssClass="LABEL" Text="Task Template Name:"
                EnableViewState="false"></asp:Label>
        </td>
        <td style="width: 66%" colspan="4">
            <asp:TextBox ID="txt_TaskTemplateName" TabIndex="2" runat="server" CssClass="TEXTBOX"
                BorderWidth="1px" MaxLength="50" Width="50%"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
    </tr>
    <tr>
        <td style="width: 100%" colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%; height: 21px;">
            <asp:Label ID="lbl_VehicleManufacturer"  runat="server" CssClass="LABEL" EnableViewState="false"
                Text="Vehicle Manufacturer:"></asp:Label></td>
        <td style="width: 25%; height: 21px;">
            <asp:UpdatePanel ID="UpdatePanel13" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_VehicleManufacturer" TabIndex="3" CssClass="DROPDOWN" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_VehicleManufacturer_SelectedIndexChanged" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%; height: 21px;">
            &nbsp;</td>
        <td class="TD1" style="width: 25%; height: 21px;">
            <asp:Label ID="lbl_VehicleModel"  runat="server" CssClass="LABEL" EnableViewState="false"
                Text="Vehicle Model:"></asp:Label></td>
        <td style="width: 25%; height: 21px;">
            <asp:UpdatePanel ID="UpdatePanel14" UpdateMode="Conditional" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_VehicleModel" TabIndex="4" CssClass="DROPDOWN" runat="server"/>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_VehicleManufacturer" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="height: 21px">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="width: 100%" colspan="6">
            &nbsp;</td>
    </tr>
    <tr class="HIDEGRIDCOL">
        <td class="TD1" style="width: 25%">
            Is Custom:</td>
        <td style="width: 25%" colspan="5">
            <asp:UpdatePanel ID="UpdatePanel6" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rblst_Schedule_By" />
                    <asp:AsyncPostBackTrigger ControlID="dg_Custom_Alert_On_Details" />
                </Triggers>
                <ContentTemplate>
                    <asp:CheckBox ID="Chk_Is_Custom" runat="server" TabIndex="5" CssClass="CHECKBOX"
                        onClick="Is_Custome_Checked();Show_Last_Permormed_On()" AutoPostBack="true" OnCheckedChanged="Chk_Is_Custom_CheckedChanged" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr id="tr_Is_Custome_Details_False" runat="server">
        <td class="TD1" style="width: 25%">
            Schedule By:</td>
        <td style="width: 66%" colspan="4">
            <asp:UpdatePanel ID="UpdatePanel4" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Chk_Is_Custom" />
                    <asp:AsyncPostBackTrigger ControlID="rblst_Schedule_By" />
                    <asp:AsyncPostBackTrigger ControlID="dg_Custom_Alert_On_Details" />
                </Triggers>
                <ContentTemplate>
                    <asp:RadioButtonList ID="rblst_Schedule_By" TabIndex="6"  runat="server" AutoPostBack="true" RepeatDirection="Horizontal"
                        onclick="Show_Odometer_Time_Detail();Show_Last_Permormed_On()" OnSelectedIndexChanged="rblst_Schedule_By_SelectedIndexChanged">
                    </asp:RadioButtonList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            &nbsp;</td>
    </tr>
    <tr id="tr_Odometer_Time0" runat="server">
        <td class="TD1" style="width: 25%">
            Due Every:</td>
        <td style="width: 66%" colspan="4">
            <asp:UpdatePanel ID="UpdatePanel5" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rblst_Schedule_By" />
                    <asp:AsyncPostBackTrigger ControlID="Chk_Is_Custom" />
                </Triggers>
                <ContentTemplate>
                    <asp:TextBox ID="txt_Due_Every" TabIndex="7" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                        onkeypress="return Only_Numbers(this,event)" MaxLength="50" Width="20%"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="lbl_Due_Every" runat="server" CssClass="LABEL" Text="Kms" EnableViewState="false"></asp:Label>
                    <asp:RadioButton ID="rbl_Days" TabIndex="8" Text="Days" runat="server" Checked="true"
                        GroupName="Is_Custome_False1" />
                    <asp:RadioButton ID="rbl_Months" TabIndex="8" Text="Months" runat="server" GroupName="Is_Custome_False1" />
                    &nbsp; &nbsp; &nbsp; Alert Before:
                    <asp:TextBox ID="txt_Alert_Before" runat="server" TabIndex="9" onkeypress="return Only_Numbers(this,event)"
                        CssClass="TEXTBOX" BorderWidth="1px" MaxLength="50" Width="20%"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="lbl_Alert_Before" runat="server" CssClass="LABEL" Text="Kms" EnableViewState="false"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
    </tr>
    <tr id="tr_Odometer_Time1" runat="server">
        <td class="TD1" style="width: 25%">
            Due Every:</td>
        <td style="width: 66%" colspan="4">
            <asp:UpdatePanel ID="UpdatePanel7" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rblst_Schedule_By" />
                    <asp:AsyncPostBackTrigger ControlID="Chk_Is_Custom" />
                </Triggers>
                <ContentTemplate>
                    <asp:TextBox ID="txt_Kms" runat="server" TabIndex="10" onkeypress="return Only_Numbers(this,event)"
                        CssClass="TEXTBOX" BorderWidth="1px" MaxLength="50" Width="20%"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="lbl_Kms" runat="server" CssClass="LABEL" Text="Kms" EnableViewState="false">
                    </asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; Or:
                    <asp:TextBox ID="txt_Days_Months" onkeypress="return Only_Numbers(this,event)" runat="server"
                        TabIndex="11" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="50" Width="20%"></asp:TextBox>
                    &nbsp;
                    <asp:RadioButton ID="rbl_Is_Custome_Days" Text="Days" TabIndex="12" runat="server"
                        AutoPostBack="true" Checked="true" GroupName="Is_Custome_False2" OnCheckedChanged="rbl_Is_Custome_Days_CheckedChanged" />
                    <asp:RadioButton ID="rbl_Is_Custome_Months" AutoPostBack="true" TabIndex="12" Text="Months"
                        runat="server" GroupName="Is_Custome_False2" OnCheckedChanged="rbl_Is_Custome_Months_CheckedChanged" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
    </tr>
    <tr id="tr_Odometer_Time2" runat="server">
        <td class="TD1" style="width: 25%">
            Alert Before:</td>
        <td style="width: 66%" colspan="4">
            <asp:UpdatePanel ID="UpdatePanel8" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rblst_Schedule_By" />
                    <asp:AsyncPostBackTrigger ControlID="rbl_Is_Custome_Days" />
                    <asp:AsyncPostBackTrigger ControlID="rbl_Is_Custome_Months" />
                    <asp:AsyncPostBackTrigger ControlID="Chk_Is_Custom" />
                </Triggers>
                <ContentTemplate>
                    <asp:TextBox ID="txt_Odometer_Time_Kms" onkeypress="return Only_Numbers(this,event)"
                        runat="server" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="50" TabIndex="13"
                        Width="20%"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="lbl_Odometer_Time_Kms" runat="server" CssClass="LABEL" Text="Kms"
                        EnableViewState="false"></asp:Label>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp; Or:
                    <asp:TextBox ID="txt_Odometer_Time_Days_Months" onkeypress="return Only_Numbers(this,event)"
                        runat="server" CssClass="TEXTBOX" BorderWidth="1px" MaxLength="50" TabIndex="14"
                        Width="20%"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="lbl_Is_Custome_Days_Months" runat="server" Text="Days" CssClass="LABEL"
                        Width="10"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
    </tr>
    <tr id="tr_Custom_Odometer_Time" runat="server">
        <td class="TD1" style="width: 25%">
            Alert On:</td>
        <td colspan="5" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
                    <td>
                        <div class="DIV" id="Div_Custom_Alert_On_Details" style="height: 150px; width: 99%">
                            <asp:UpdatePanel ID="upd_pnl_dg_Custom_Alert_On_Details" runat="server" UpdateMode="Conditional">
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="dg_Custom_Alert_On_Details" />
                                    <asp:AsyncPostBackTrigger ControlID="rblst_Schedule_By" />
                                    <asp:AsyncPostBackTrigger ControlID="Chk_Is_Custom" />
                                </Triggers>
                                <ContentTemplate>
                                    <asp:Panel ID="pnl_Custom_Alert_On_Details" runat="server" GroupingText="Custom Alert On Details "
                                        CssClass="PANEL" Width="95%">
                                        <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                            <tr>
                                                <td style="width: 100%;" colspan="8">
                                                    <asp:DataGrid ID="dg_Custom_Alert_On_Details" AutoGenerateColumns="False" ShowFooter="True"
                                                        CellPadding="3" CssClass="GRID" Width="98%" runat="server" OnCancelCommand="dg_Custom_Alert_On_Details_CancelCommand"
                                                        OnEditCommand="dg_Custom_Alert_On_Details_EditCommand" OnItemCommand="dg_Custom_Alert_On_Details_ItemCommand"
                                                        OnItemDataBound="dg_Custom_Alert_On_Details_ItemDataBound" OnUpdateCommand="dg_Custom_Alert_On_Details_UpdateCommand"
                                                        OnDeleteCommand="dg_Custom_Alert_On_Details_DeleteCommand" meta:resourcekey="dg_Custom_Alert_On_DetailsResource1">
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="Sr.No.">
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "Sr_No") %>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="5%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Date">
                                                                <FooterTemplate>
                                                                    <ComponentArt:Calendar ID="dtp_Custom_Grid_Date" runat="server" PickerFormat="Custom"
                                                                        PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                                                                        AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" />
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Grid_Date")%>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <ComponentArt:Calendar ID="dtp_Custom_Grid_Date" runat="server" PickerFormat="Custom"
                                                                        PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                                                                        AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" />
                                                                </EditItemTemplate>
                                                                <%--<HeaderStyle Width="100" />--%>
                                                                <HeaderStyle Width="10%" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Odometer">
                                                                <FooterTemplate>
                                                                    <asp:TextBox ID="txt_Custom_Grid_Odometer" onkeypress="return Only_Numbers(this,event)"
                                                                        runat="server" CssClass="TEXTBOXNOS" Text="0" BorderWidth="1px" Width="100" MaxLength="10"
                                                                        TabIndex="14"></asp:TextBox>
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <%# DataBinder.Eval(Container.DataItem, "Grid_Odometer") %>
                                                                </ItemTemplate>
                                                                <EditItemTemplate>
                                                                    <asp:TextBox ID="txt_Custom_Grid_Odometer" onkeypress="return Only_Numbers(this,event)"
                                                                        runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px" Width="100" MaxLength="10"
                                                                        TabIndex="14"></asp:TextBox>
                                                                </EditItemTemplate>
                                                                <%--<HeaderStyle Width="100" />--%>
                                                                <HeaderStyle Width="10%" />
                                                            </asp:TemplateColumn>
                                                            <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit"
                                                                meta:resourcekey="EditCommandColumnResource1">
                                                                <HeaderStyle Width="10%" />
                                                            </asp:EditCommandColumn>
                                                            <asp:TemplateColumn HeaderText="Delete">
                                                                <FooterTemplate>
                                                                    <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" meta:resourcekey="lbtn_AddResource1" />
                                                                </FooterTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete"
                                                                        meta:resourcekey="lbtn_DeleteResource1" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="5%" />
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                    </asp:DataGrid>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr id="tr_Custom_Alert_Before" runat="server">
        <td class="TD1" style="width: 25%">
            Alert Before:</td>
        <td style="width: 66%" colspan="4">
            <asp:UpdatePanel ID="UpdatePanel10" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rblst_Schedule_By" />
                    <asp:AsyncPostBackTrigger ControlID="rbl_Is_Custome_Days" />
                    <asp:AsyncPostBackTrigger ControlID="rbl_Is_Custome_Months" />
                    <asp:AsyncPostBackTrigger ControlID="Chk_Is_Custom" />
                </Triggers>
                <ContentTemplate>
                    <asp:TextBox ID="txt_Custom_Alert_Before" onkeypress="return Only_Numbers(this,event)"
                        runat="server" CssClass="TEXTBOXNOS" BorderWidth="1px" MaxLength="50" TabIndex="13"
                        Width="20%"></asp:TextBox>
                    &nbsp;
                    <asp:Label ID="lbl_Custome_Alert_Before" runat="server" CssClass="LABEL" Text="Kms"
                        EnableViewState="false"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
            Repair Service Category:</td>
        <td style="width: 25%">
            <asp:DropDownList ID="ddl_Repair_Service_Category" TabIndex="15" CssClass="DROPDOWN"
                runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddl_Repair_Service_Category_SelectedIndexChanged" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
            Repair Service:</td>
        <td style="width: 25%">
            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Repair_Service_Category" />
                </Triggers>
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_Repair_Service" TabIndex="16" CssClass="DROPDOWN" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
            Completion:</td>
        <td style="width: 25%">
            <asp:DropDownList ID="ddl_Completion" TabIndex="16" CssClass="DROPDOWN" runat="server" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr class="HIDEGRIDCOL" runat="server">
        <td class="TD1" style="width: 25%">
            To Be Worked At:</td>
        <td colspan="4">
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rbl_Internal" />
                    <asp:AsyncPostBackTrigger ControlID="rbl_External" />
                </Triggers>
                <ContentTemplate>
                    <asp:RadioButton ID="rbl_Internal" TabIndex="17" runat="server" Checked="true" AutoPostBack="True"
                        Text="Internal" GroupName="To_Be_Worked_At" OnCheckedChanged="rbl_Internal_CheckedChanged"
                        Enabled="false" />
                    <asp:RadioButton ID="rbl_External" TabIndex="17" runat="server" AutoPostBack="True"
                        Checked="true" GroupName="To_Be_Worked_At" Text="External" OnCheckedChanged="rbl_External_CheckedChanged" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr id="Tr2" runat="server">
        <td class="TD1" style="width: 25%">
            To Be Worked At:</td>
        <td colspan="4">
            <asp:UpdatePanel ID="UpdatePanel12" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rbl_Internal" />
                    <asp:AsyncPostBackTrigger ControlID="rbl_External" />
                </Triggers>
                <ContentTemplate>
                    <asp:RadioButtonList ID="rblWorkShop" TabIndex="18" runat="Server" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">Company WorkShop</asp:ListItem>
                        <asp:ListItem Value="0">Other WorkShop</asp:ListItem>
                    </asp:RadioButtonList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr id="Tr1" runat="server">
        <td class="TD1" style="width: 25%;">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rbl_Internal" />
                    <asp:AsyncPostBackTrigger ControlID="rbl_External" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_To_Be_Worked_At" runat="server" Text="Location:" CssClass="LABEL"
                        Width="100%"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td align="left" style="width: 25%">
            <table>
                <tr>
                    <td style="width: 80%">
                        <asp:UpdatePanel ID="UpdatePanel9" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rbl_Internal" />
                                <asp:AsyncPostBackTrigger ControlID="rbl_External" />
                            </Triggers>
                            <ContentTemplate>
                                <cc1:DDLSearch ID="ddl_To_Be_Worked_At" CallBackAfter="2" TabIndex="19" PostBack="true"
                                    IsCallBack="True" AllowNewText="true" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchToBeWorkedAt"
                                    runat="server" OnTxtChange="ddl_To_Be_Worked_At_TxtChange" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 20%;" align="left">
                        *
                    </td>
                </tr>
            </table>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            &nbsp;</td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
            Estimated Cost:</td>
        <td colspan="4">
            <asp:TextBox ID="txt_Cost" runat="server" CssClass="TEXTBOXNOS" onkeyup="valid(this)"
                onblur="Format_Number(this,2,99999999.99,'WucTaskTemplate1_lbl_Errors')" BorderWidth="1px"
                TabIndex="21" MaxLength="8" Width="20%"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%; height: 64px;">
            Description:</td>
        <td style="height: 64px;" colspan="4">
            <asp:TextBox ID="txt_Description" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                MaxLength="255" TextMode="MultiLine" TabIndex="22" Width="99%" Height="60px"></asp:TextBox></td>
        <td class="TDMANDATORY" style="width: 1%; height: 64px;">
            &nbsp;
        </td>
    </tr>
    <tr id="tr_Last_Permormed_On_Value" runat="server">
        <td class="TD1" style="width: 25%">
            Last Performed On:</td>
        <td style="width: 50%" colspan="5">
            <asp:UpdatePanel ID="UpdatePanel11" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Chk_Is_Custom" />
                    <%--<asp:AsyncPostBackTrigger ControlID="rblst_Schedule_By" />--%>
                </Triggers>
                <ContentTemplate>
                    <table>
                        <tr>
                            <td style="width: 40%;" runat="server" id="tdtxt_Last_Permormed_On">
                                <asp:TextBox ID="txt_Last_Permormed_On" TabIndex="23" runat="server" onkeypress="return Only_Numbers(this,event)"
                                    CssClass="TEXTBOXNOS" Width="98%"></asp:TextBox>
                            </td>
                            <td style="width: 10%;" runat="server" id="tdkms">
                                <asp:Label ID="lbl_Last_Permormed_On" runat="server" CssClass="LABEL" Text="Kms"
                                    EnableViewState="false"></asp:Label>
                            </td>
                            <td style="width: 5%;" runat="server" id="tdon">
                                <asp:Label ID="Label1" runat="server" CssClass="LABEL" Text="On" EnableViewState="false"></asp:Label>
                            </td>
                            <td style="width: 40%;" runat="server" id="tddtp_Last_Permormed_Date">
                                <uc1:WucDatePicker ID="dtp_Last_Permormed_Date" TabIndex="24" runat="server"></uc1:WucDatePicker>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" Text="Save" TabIndex="25" CssClass="BUTTON"
                OnClick="btn_Save_Click" OnClientClick="return validateUI()" /></td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="Upd_Pnl_TaskTemplate" UpdateMode="Always" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                    <asp:AsyncPostBackTrigger ControlID="dg_Custom_Alert_On_Details" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" Visible="true" CssClass="LABELERROR" EnableViewState="false">
                    </asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:HiddenField ID="hdn_Vehicle_Id" runat="server" />
            <asp:HiddenField ID="hdn_Form_Type" runat="server" />
            <asp:HiddenField ID="hdn_Task_Completion_ID" runat="server" />
            <asp:HiddenField ID="hdn_DueOn_Value" runat="server" />
            <asp:HiddenField ID="hdn_DueOn_Days" runat="server" />
            <asp:HiddenField ID="hdn_Month_Value" runat="server" />
            <asp:HiddenField ID="hdn_Alert_Before_Value" runat="server" />
            <asp:HiddenField ID="hdn_Alert_Before_Days" runat="server" />
            <asp:HiddenField ID="hdn_Is_Days_Selected" runat="server" />
            <asp:HiddenField ID="hdn_Schedule_By" runat="server" />
            <asp:UpdatePanel ID="Upd_Pnl_To_Be_Worked_At" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_To_Be_Worked_At" />
                    <asp:AsyncPostBackTrigger ControlID="rbl_Internal" />
                    <asp:AsyncPostBackTrigger ControlID="rbl_External" />
                </Triggers>
                <ContentTemplate>
                    <asp:HiddenField ID="hdn_Vendor_Id" runat="server" />
                    <asp:HiddenField ID="hdn_Branch_Id" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>

<script type="text/javascript" language="javascript">
  
    Is_Custome_Checked();
    Show_Odometer_Time_Detail();
    Show_Last_Permormed_On();
    
</script>

