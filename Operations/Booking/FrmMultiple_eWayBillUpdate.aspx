<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmMultiple_eWayBillUpdate.aspx.cs"
    Inherits="Operations_Booking_FrmMultiple_eWayBillUpdate" %>


<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Multiple eWay Bills Update</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scm_Comm">
        </asp:ScriptManager>
        <table class="TABLE" width="100%">
            <tr>
                <td class="TDGRADIENT" colspan="8">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Multiple eWay Bills Update"></asp:Label>
                </td>
            </tr>
            <tr runat="server">
                <td class="TD1" style="width: 20%;">
                </td>
                <td style="width: 20%;">
                </td>
                <td class="TD1" style="width: 5%;">
                </td>
                <td class="TD1" style="width: 20%;">
                </td>
                 <td class="TD1" style="width: 20%;">
                </td>
                 <td class="TD1" style="width: 20%;">
                </td>
                <td style="width: 5%;">
                </td>
                <td class="TD1" style="width: 5%;">
                </td>
            </tr>

            <tr >
                <td class="TD1" style="width: 20%; height: 15px;">
                    LR No. :&nbsp;</td>
                <td  style="width: 30%; text-align:left; font-weight:bold; height: 15px;">
                <asp:Label runat="server" ID="lbl_GCNo" Text="123456789" CommandName="Add"></asp:Label>
                </td>
                
                
                <td class="TD1" style="width: 20%; height: 15px;">
                    eWayBill No. In LR :&nbsp;</td>
                <td  style="width: 30%; text-align:left; font-weight:bold; height: 15px;">
                <asp:Label runat="server" ID="lbl_LReWayBillNo" Text="123456789012" ForeColor="Red" CommandName="Add"></asp:Label>
                </td>
                
            </tr>
            
            <tr >
                <td class="TD1" style="width: 20%;">
                </td>
            </tr>

            <tr>
                <td class="TD1" style="width: 20%">
                    &nbsp;</td>
                <td colspan="5">
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
                                        <asp:BoundColumn DataField="GC_ID">
                                            <ItemStyle CssClass="HIDEGRIDCOL" />
                                            <HeaderStyle CssClass="HIDEGRIDCOL" />
                                            <FooterStyle CssClass="HIDEGRIDCOL" />
                                        </asp:BoundColumn>
                              
                                        <asp:TemplateColumn HeaderText="eWay Bill No">
                                            <HeaderStyle Width="20%" HorizontalAlign="Left"></HeaderStyle>
                                            <ItemStyle Width="20%" HorizontalAlign="Left"></ItemStyle>
                                            <FooterStyle Width="20%" HorizontalAlign="Left"></FooterStyle>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txt_eWayBillNo" Width="95%" runat="server" CssClass="TEXTBOXNOS"
                                                    onkeyPress="return Only_Numbers(this,event);" MaxLength="12" onfocus="txtbox_onfocus(this)"
                                                    onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "eWayBillNo")%>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txt_eWayBillNo" Width="95%" runat="server" CssClass="TEXTBOXNOS"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "eWayBillNo") %>'
                                                    MaxLength="12" onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)"
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
                <td class="TD1" style="width: 20%">
                </td>
                <td colspan="5" style="text-align: right; width: 40%">
                    <%--<asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="UpdatePanel1">
                        <ContentTemplate>
                            <table class="TABLENOBORDER">
                                <tr>
                                    <td style="width: 5%">
                                    </td>
                                    <td class="TD1" style="width: 40%">
                                         </td>
                                    <td style="width: 5%" class="TD1">
                                      
                                    </td>
                                    <td style="width: 5%" class="TD1">
                                         
                                    </td>
                                    <td style="width: 7%; text-align: right; vertical-align:text-top" class="TD1" runat="server" id="td_length">
                                        &nbsp;
                                    </td>
                                    <td style="width: 7%; text-align: left; vertical-align:text-top" class="TD1" runat="server" id="td_width">
                                        &nbsp;
                                    </td>
                                    <td style="width: 15%" class="TD1">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                        </Triggers>
                    </asp:UpdatePanel>--%>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="6">
                    <asp:Button ID="btnSave" runat="server" CssClass="BUTTON" OnClick="btnSave_Click"
                        Text="Save" />
                </td>
            </tr>
            <tr>
                <td align="left" colspan="6">
                    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                Text="Fields with * mark are mandatory"></asp:Label>
                            <asp:HiddenField ID="hdnKeyID" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            <asp:AsyncPostBackTrigger ControlID="dg_Commodity" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
