<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucAssesseeDetails.ascx.cs" Inherits="FA_Common_Accounting_Masters_WucAssesseeDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="wuc_Date_Picker"
    TagPrefix="uc1" %>
    
    
    <%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<table class="TABLE" width="100%">
    <tr>
        <td style="width: 30%;" class="TDGRADIENT" colspan="4">
            <asp:Label ID="lbl_Header" runat="server" CssClass="HEADINGLABEL" Text="FBT ASSESSEE DETAILS"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 25%">
            &nbsp;</td>
        <td style="width: 25%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 49%">
        </td>
    </tr>
    <tr>
        <td style="width: 30%;" class="TD1">
            Name of Assessee Type:</td>
        <td style="width: 100px">
            <asp:DropDownList ID="ddl_AssesseeType" runat="server" AutoPostBack="True" CssClass="DROPDOWN"
                Width="290px" OnSelectedIndexChanged="ddl_AssesseeType_SelectedIndexChanged">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%" align="left">
             <asp:UpdatePanel ID="UpdatePanel_FBTAssesseeDetails" runat="server">
                <ContentTemplate>
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
                    <td>
                        <asp:DataGrid ID="dg_FBTAssesseeDetails" runat="server" AutoGenerateColumns="False"
                            CssClass="GRID" Width="100%" ShowFooter="True" OnItemCommand="dg_FBTAssesseeDetails_ItemCommand"
                            OnItemDataBound="dg_FBTAssesseeDetails_ItemDataBound" OnCancelCommand="dg_FBTAssesseeDetails_CancelCommand"
                            OnEditCommand="dg_FBTAssesseeDetails_EditCommand" OnUpdateCommand="dg_FBTAssesseeDetails_UpdateCommand">
                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <PagerStyle CssClass="GRIDPAGERCSS" />
                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                            <HeaderStyle CssClass="GRIDHEADERCSS" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Applicable From">
                                    <FooterTemplate>
                                        <ComponentArt:Calendar ID="Picker_FromDate" runat="server" PickerFormat="Custom"
                                            PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                                            AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" Width="5px" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        &nbsp;<asp:Label ID="lbl_Applicable_From" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Applicable_From","{0:dd/MMM/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle />
                                    <EditItemTemplate>
                                        <ComponentArt:Calendar ID="Picker_FromDate" runat="server" PickerFormat="Custom"
                                            PickerCustomFormat="MMMM d yyyy" ControlType="Picker" PickerCssClass="picker"
                                            AllowDaySelection="True" AllowMonthSelection="True" MinDate="1900-01-01" Width="5px" />
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Rate of FBT">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_FBT_Rate" BorderWidth="1px" Width="70%" runat="server" onkeypress="return Only_Numbers(this,event)"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_FBT_Rate" Width="70%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"FBT_Rate") %>'
                                            onkeypress="return Only_Numbers(this,event)"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_FBT_Rate" BorderWidth="1px" Width="70%" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Surcharge">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_Surcharge" BorderWidth="1px" Width="70%" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Surcharge" Width="70%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Surcharge") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_Surcharge" BorderWidth="1px" Width="70%" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Addl Surcharge">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_Addl_Surcharge" BorderWidth="1px" Width="70%" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Addl_Surcharge" Width="70%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Additional_Surcharge_Cess") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_Addl_Surcharge" BorderWidth="1px" Width="70%" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Addl Education Cess">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txt_Addl_Education_Cess" BorderWidth="1px" Width="70%" runat="server"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_Addl_Education_Cess" Width="70%" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Additional_Educational_Cess") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txt_Addl_Education_Cess" BorderWidth="1px" Width="70%" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" HeaderText="Edit / Update"
                                    UpdateText="Update"></asp:EditCommandColumn>
                                <asp:TemplateColumn HeaderText="Add">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnk_Add" runat="server" CommandName="add">Add</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Delete" runat="server" CommandName="delete">Delete</asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                         </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID = "dg_FBTAssesseeDetails" />
                <asp:AsyncPostBackTrigger ControlID="ddl_AssesseeType" />
                <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
            </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2" valign="middle">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click" /></td>
    </tr>
    <tr>
        <td style="width: 30%;" colspan="2">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
            <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR"></asp:Label>
               </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
