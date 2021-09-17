<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPTLFTLClient.aspx.cs"
    Inherits="Master_Sales_FrmPTLFTLClient" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PTL FTL CLIENT</title>

    <script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

    <script type="text/javascript" src="../../Javascript/Common.js"></script>

    <script type="text/javascript" src="../../Javascript/txtsearch_common.js"></script>

    <script type="text/javascript" src="../../Javascript/Operations/Outward/VehicleTrackingSMS.js">function TABLE1_onclick() {

}

    </script>

    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scm_Comm">
        </asp:ScriptManager>
        <table class="TABLE" style="width: 100%" id="TABLE1" onclick="return TABLE1_onclick()">
            <tr>
                <td class="TDGRADIENT" colspan="6">
                    &nbsp;<asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="PTL FTL CLIENT"></asp:Label></td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                    Client Name:</td>
                <td style="width: 79%;" colspan="4">
                    <asp:TextBox ID="txt_ClientName" runat="server" CssClass="TEXTBOX" MaxLength="50"
                        onkeypress="return Only_AlphaSpaceNumbers(this,event);" onblur="return Uppercase(this);"
                        Width="99%"></asp:TextBox>
                </td>
                <td class="TDMANDATORY" style="width: 1%;">
                    *</td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                    Contact Person 1:</td>
                <td style="width: 29%">
                    <asp:TextBox ID="txt_ContactPerson1" runat="server" CssClass="TEXTBOX" MaxLength="50"></asp:TextBox>
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                    *</td>
                <td style="width: 50%" colspan="3">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                    Mobile 1:</td>
                <td class="TD" style="width: 29%">
                    <asp:TextBox ID="txt_Contact1Mobile1" onkeypress="return Only_Integers(this,event)"
                        onblur="valid(this)" runat="server" CssClass="TEXTBOX" MaxLength="15"></asp:TextBox>
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                    *</td>
                <td class="TD1" style="width: 20%">
                    Mobile 2:</td>
                <td style="width: 29%">
                    <asp:TextBox ID="txt_Contact1Mobile2" onkeypress="return Only_Integers(this,event)"
                        onblur="valid(this)" runat="server" CssClass="TEXTBOX" MaxLength="15"></asp:TextBox>
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                    Contact Person 2:</td>
                <td class="TD" style="width: 29%">
                    <asp:TextBox ID="txt_ContactPerson2" runat="server" CssClass="TEXTBOX" MaxLength="50"></asp:TextBox>
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                </td>
                <td style="width: 50%" colspan="3">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                    Mobile 1:</td>
                <td class="TD" style="width: 29%">
                    <asp:TextBox ID="txt_Contact2Mobile1" onkeypress="return Only_Integers(this,event)"
                        onblur="valid(this)" runat="server" CssClass="TEXTBOX" MaxLength="15"></asp:TextBox>
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                </td>
                <td class="TD1" style="width: 20%">
                    Mobile 2:</td>
                <td style="width: 29%">
                    <asp:TextBox ID="txt_Contact2Mobile2" onkeypress="return Only_Integers(this,event)"
                        onblur="valid(this)" runat="server" CssClass="TEXTBOX" MaxLength="15"></asp:TextBox>
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                    Address 1:</td>
                <td style="width: 79%" colspan="4">
                    <asp:TextBox ID="txt_Address1" runat="server" CssClass="TEXTBOX" MaxLength="100"
                        Width="99%"></asp:TextBox>
                </td>
                <td class="TD1" style="width: 1%">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                    Address 2:</td>
                <td style="width: 79%" colspan="4">
                    <asp:TextBox ID="txt_Address2" runat="server" CssClass="TEXTBOX" MaxLength="100"
                        Width="99%"></asp:TextBox>
                </td>
                <td class="TD1" style="width: 1%">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                    City:</td>
                <td style="width: 29%">
                    <asp:TextBox ID="txt_City" autocomplete="off" runat="server" CssClass="TEXTBOX" onblur="On_txtLostFocus('txt_City','lst_City','hdn_City')"
                        onkeyup="Search_txtSearch(event,this,'lst_City','City',2);" onkeydown="return on_keydown(event,'txt_City','lst_City');"
                        onfocus="On_Focus('txt_City','lst_City');" MaxLength="50" EnableViewState="False"></asp:TextBox>
                    <asp:ListBox ID="lst_City" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txt_City')"
                        runat="server" TabIndex="20"></asp:ListBox>
                    <asp:HiddenField ID="hdn_City" Value="0" runat="server"></asp:HiddenField>
                </td>
                <td class="TDMANDATORY" style="width: 1%">
                    *</td>
                <td class="TD1" style="width: 50%" colspan="3">
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%; height: 15px;">
                </td>
                <td style="width: 80%; height: 15px;" colspan="5">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                </td>
                <td style="width: 80%" colspan="5">
                    <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upnl_Comm">
                        <ContentTemplate>
                            <div id="Div_Commodity" class="DIV" style="height: auto; width: 100%; text-align: left">
                                <asp:DataGrid ID="dg_Commodity" runat="server" CellPadding="3" CssClass="Grid" AutoGenerateColumns="False"
                                    ShowFooter="True" OnCancelCommand="dg_Commodity_CancelCommand" OnDeleteCommand="dg_Commodity_DeleteCommand"
                                    OnEditCommand="dg_Commodity_EditCommand" OnItemCommand="dg_Commodity_ItemCommand"
                                    OnItemDataBound="dg_Commodity_ItemDataBound" OnUpdateCommand="dg_Commodity_UpdateCommand">
                                    <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                                    <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                    <Columns>
                                        <asp:BoundColumn DataField="Client_ID">
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                            <FooterStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DetailID">
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                            <FooterStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Vehicle Manufacturer">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddl_VehicleManufacturer" runat="server" Width="98%" CssClass="DROPDOWN"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddl_VehicleManufacturer_SelectedIndexChanged" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "Manufacturer"))%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="45%" HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Width="45%" HorizontalAlign="Left"></ItemStyle>
                                            <FooterStyle Width="45%" HorizontalAlign="Left"></FooterStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_VehicleManufacturer" runat="server" Width="98%" CssClass="DROPDOWN"
                                                    AutoPostBack="true" OnSelectedIndexChanged="ddl_VehicleManufacturer_SelectedIndexChanged" />
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Vehicle Model">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <FooterTemplate>
                                                <asp:DropDownList ID="ddl_VehicleModel" runat="server" Width="98%" CssClass="DROPDOWN"
                                                    AutoPostBack="true" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <%# (DataBinder.Eval(Container.DataItem, "Vehicle_Model"))%>
                                            </ItemTemplate>
                                            <HeaderStyle Width="45%" HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Width="45%" HorizontalAlign="Left"></ItemStyle>
                                            <FooterStyle Width="45%" HorizontalAlign="Left"></FooterStyle>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddl_VehicleModel" runat="server" Width="98%" CssClass="DROPDOWN"
                                                    AutoPostBack="true" />
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Load Weigth">
                                            <HeaderStyle Width="30%" HorizontalAlign="right"></HeaderStyle>
                                            <ItemStyle Width="30%" HorizontalAlign="right"></ItemStyle>
                                            <FooterStyle Width="30%" HorizontalAlign="right"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_LoadWeight" Width="95%" runat="server" CssClass="TEXTBOXNOS"
                                                    onkeyPress="return Only_Numbers(this,event);" MaxLength="6" onfocus="txtbox_onfocus(this)"
                                                    onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "LoadWeight"))%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_LoadWeight" Width="95%" runat="server" CssClass="TEXTBOXNOS"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "LoadWeight")) %>'
                                                    MaxLength="7" onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)"
                                                    onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:EditCommandColumn HeaderText="Edit" CancelText="Cancel" UpdateText="Update"
                                            EditText="Edit">
                                            <HeaderStyle Width="5%"></HeaderStyle>
                                        </asp:EditCommandColumn>
                                        <asp:TemplateColumn HeaderText="Delete">
                                            <FooterTemplate>
                                                <asp:LinkButton runat="server" ID="lbtn_Add_Commodity" Text="Add" CommandName="Add"></asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" ID="lbtn_Delete_Commodity" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%"></HeaderStyle>
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    &nbsp;
                    <asp:HiddenField ID="hdnKeyID" runat="server" Value="0" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                Text="Fields with * mark are mandatory"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="6">
                    <asp:Button ID="btn_Save" runat="server" Text='Save' CssClass="BUTTON" OnClientClick="return validateUI();"
                        OnClick="btnSave_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    &nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
