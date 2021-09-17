<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDeliveryArea.aspx.cs"
    Inherits="Master_Location_FrmDeliveryArea" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc2" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Delivery Area</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scmdelarea" runat="server" />
        <div>
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSave" />
                </Triggers>
                <ContentTemplate>
                    <table class="TABLE" width="100%">
                        <tr>
                            <td class="TDGRADIENT" colspan="6">
                                <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Delivery Area"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                Delivery Area Code</td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtDeliveryAreaCode" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                    MaxLength="4" />
                            </td>
                            <td class="TDMANDATORY" style="width: 1%">
                                *</td>
                            <td class="TD1" style="width: 20%">
                                Delivery Area Name</td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtDeliveryAreaName" runat="server" BorderWidth="1px" CssClass="TEXTBOX"
                                    MaxLength="30" />
                            </td>
                            <td class="TDMANDATORY" style="width: 1%">
                                *</td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%; height: 50px;">
                                Branch</td>
                            <td style="width: 29%; height: 50px;">
                                <cc1:DDLSearch ID="DDLBranch" runat="server" AllowNewText="False" IsCallBack="True"
                                    CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetBranch" CallBackAfter="2"
                                    PostBack="False" InjectJSFunction="" Text="" />
                            </td>
                            <td class="TDMANDATORY" style="width: 1%; height: 50px;">
                                *</td>
                            <td style="width: 20%; height: 50px;" class="TD1">
                                Distance from branch</td>
                            <td style="width: 29%; height: 50px;">
                                <asp:TextBox ID="txtDistanceFromBranch" runat="server" Width="100px" CssClass="TEXTBOXNOS"
                                    onkeyPress="return Only_Integers(this,event);"></asp:TextBox>
                            </td>
                            <td class="TDMANDATORY" style="width: 1%; height: 50px;">
                                *</td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                Is ODA?</td>
                            <td style="width: 29%">
                                <asp:CheckBox ID="Chk_IsODALocation" CssClass="CHECKBOX" runat="server" AutoPostBack="True" />
                            </td>
                            <td class="TDMANDATORY" style="width: 1%">
                            </td>
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                Delivery Charge</td>
                            <td style="width: 29%">
                                <asp:DropDownList ID="ddl_DeliveryCharge" CssClass="DROPDOWN" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddl_DeliveryCharge_SelectedIndexChanged">
                                    <asp:ListItem Value="1" Text="Per Parcel"></asp:ListItem>
                                    <asp:ListItem Value="2" Text="Per Consignment"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td class="TDMANDATORY" style="width: 1%">
                            </td>
                            <td colspan="3">
                            </td>
                        </tr>
                        <tr runat="server" id="trPerParcel">
                            <td class="TD1" style="width: 20%">
                                DD in LR</td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtPerParcel" runat="server" CssClass="TEXTBOXNOS" MaxLength="10"
                                    Width="100px" onkeyPress="return Only_Numbers_With_Dot(this,event);"></asp:TextBox>/Kg
                            </td>
                            <td class="TDMANDATORY" style="width: 1%">
                            </td>
                            <td class="TD1" style="width: 20%">
                                Tempo Frt To be Paid</td>
                            <td style="width: 29%">
                                <asp:TextBox ID="txtPerParcelToBePaid" runat="server" CssClass="TEXTBOXNOS" MaxLength="10"
                                    Width="100px" onkeyPress="return Only_Numbers_With_Dot(this,event);"></asp:TextBox>/Kg
                            </td>
                            <td class="TDMANDATORY" style="width: 1%">
                            </td>
                        </tr>
                        <tr runat="server" id="trPerConsignment" visible="false">
                            <td class="TD1" style="width: 20%">
                            </td>
                            <td colspan="4">
                                <asp:DataGrid ID="dgGrid" AutoGenerateColumns="False" ShowFooter="True" CellPadding="3"
                                    CssClass="Grid" runat="server" OnCancelCommand="dgGrid_CancelCommand" OnDeleteCommand="dgGrid_DeleteCommand"
                                    OnEditCommand="dgGrid_EditCommand" OnItemCommand="dgGrid_ItemCommand" OnItemDataBound="dgGrid_ItemDataBound"
                                    OnUpdateCommand="dgGrid_UpdateCommand">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle Height="15px" Font-Size="11px" Font-Names="Verdana" Font-Bold="True"
                                        HorizontalAlign="Left" ForeColor="Black" BorderStyle="Solid" BorderColor="#9495A2"
                                        BorderWidth="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader">
                                    </HeaderStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Description" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Description")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtDescriptionEdit" MaxLength="50" runat="server" onfocus="this.select()"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' CssClass="TEXTBOX" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtDescriptionAdd" MaxLength="50" runat="server" onfocus="this.select()"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' CssClass="TEXTBOX" />
                                            </FooterTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Above KG">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "UpToKg")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtUpToKgEdit" MaxLength="10" runat="server" onkeypress="return Only_Numbers(this,event)"
                                                    onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "UpToKg") %>'
                                                    CssClass="TEXTBOXNOS" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtUpToKgAdd" MaxLength="10" runat="server" onkeypress="return Only_Numbers(this,event)"
                                                    onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "UpToKg") %>'
                                                    CssClass="TEXTBOXNOS" />
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Amt To Be Chrgd">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "AmountPerLR")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAmountPerLREdit" MaxLength="10" runat="server" onkeypress="return Only_Numbers(this,event)"
                                                    onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "AmountPerLR") %>'
                                                    CssClass="TEXTBOXNOS" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAmountPerLRAdd" MaxLength="10" runat="server" onkeypress="return Only_Numbers(this,event)"
                                                    onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "AmountPerLR") %>'
                                                    CssClass="TEXTBOXNOS" />
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Tempo Frt">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "AmountPerLRToBePaid")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAmountPerLRTBPEdit" MaxLength="10" runat="server" onkeypress="return Only_Numbers(this,event)"
                                                    onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "AmountPerLRToBePaid") %>'
                                                    CssClass="TEXTBOXNOS" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAmountPerLRTBPAdd" MaxLength="10" runat="server" onkeypress="return Only_Numbers(this,event)"
                                                    onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "AmountPerLRToBePaid") %>'
                                                    CssClass="TEXTBOXNOS" />
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Add on Tempo Frt">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "AddOnTempoFreight")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtAddOnTempoFreightEdit" MaxLength="10" runat="server" onkeypress="return Only_Numbers(this,event)"
                                                    onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "AddOnTempoFreight") %>'
                                                    CssClass="TEXTBOXNOS" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtAddOnTempoFreightAdd" MaxLength="10" runat="server" onkeypress="return Only_Numbers(this,event)"
                                                    onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "AddOnTempoFreight") %>'
                                                    CssClass="TEXTBOXNOS" />
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit">
                                            <HeaderStyle Width="10%" />
                                        </asp:EditCommandColumn>
                                        <asp:TemplateColumn HeaderText="Delete">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" />
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete" />
                                            </ItemTemplate>
                                            <HeaderStyle Width="5%" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </td>
                            <td class="TDMANDATORY" style="width: 1%">
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td class="TD1" align="right" >
                            Tempo No.
                            </td>
                            <td class="TD1" align="left">
                                <uc2:WucVehicleSearch ID="DDLVehicle" runat="server"/>
                            </td>
                            <td colspan="4">
                            &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="center" colspan="6">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" colspan="6">
                                <asp:Button ID="btnSave" runat="server" Text="Save" CssClass="BUTTON" OnClick="btnSave_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="6">
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                                <asp:HiddenField ID="hdnKeyID" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
