<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmAUSNewExcessDetails.aspx.cs"
    Inherits="Operations_Inward_FrmAUSNewExcessDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/DatePicker.js"></script>
<script type="text/javascript" src="../../Javascript/Operations/Inward/AUS.js"></script>
<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript">
  

  function updateparent()
    {
        window.opener.update_ExcessDetails();
    }
       
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Truck Unloading Sheet Excess Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" style="width: 100%">
                <tr>
                    <td colspan="6">&nbsp;</td></tr>
                <tr>
                    <td colspan="6" style="width: 100%">
                        <asp:UpdatePanel ID="upd_pnl_dg_AUSExcessDetails" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <div id="Div_PDS" class="DIV" style="height: 300px; left: 0px; top: 0px;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td colspan="6">
                                                <asp:DataGrid ID="dg_ExcessDetails" runat="server" Width="100%" CssClass="GRID" AutoGenerateColumns="False"
                                                    ShowFooter="True" OnCancelCommand="dg_ExcessDetails_CancelCommand" OnDeleteCommand="dg_ExcessDetails_DeleteCommand"
                                                    OnEditCommand="dg_ExcessDetails_EditCommand" OnItemCommand="dg_ExcessDetails_ItemCommand"
                                                    OnItemDataBound="dg_ExcessDetails_ItemDataBound" OnUpdateCommand="dg_ExcessDetails_UpdateCommand">
                                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                    <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="LR No">
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txt_GCNo" runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Integers(this,event)"
                                                                    BorderWidth="1px" MaxLength="10"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "GC_No") %>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_GCNo" runat="server" onkeypress="return Only_Integers(this,event)"
                                                                    CssClass="TEXTBOXNOS" BorderWidth="1px" MaxLength="10"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="8%" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Excess Articles">
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txt_ExcessArticles" onkeypress="return Only_Integers(this,event)"
                                                                    Width="94%" CssClass="TEXTBOXNOS" BorderWidth="1px" MaxLength="4" runat="server"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "Excess_Articles") %>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_ExcessArticles" onkeypress="return Only_Integers(this,event)"
                                                                    Width="94%" CssClass="TEXTBOXNOS" BorderWidth="1px" MaxLength="4" runat="server"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="5%" />
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Marking On The Package">
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txt_Marking" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                                                    Width="94%" MaxLength="50"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "Marking_On_Package") %>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_Marking" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                                                    Width="94%" MaxLength="50"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Packing Type">
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="ddl_PackingType" runat="server" CssClass="DROPDOWN">
                                                                </asp:DropDownList>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "Packing_Type") %>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddl_PackingType" runat="server" CssClass="DROPDOWN">
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="8%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Commodity">
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="ddl_Commodity" runat="server" CssClass="DROPDOWN">
                                                                </asp:DropDownList>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "Commodity_Name")%>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddl_Commodity" runat="server" CssClass="DROPDOWN">
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="8%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Item">
                                                            <FooterTemplate>
                                                                <asp:DropDownList ID="ddl_Item" runat="server" CssClass="DROPDOWN">
                                                                </asp:DropDownList>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "Item_Name")%>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="ddl_Item" runat="server" CssClass="DROPDOWN">
                                                                </asp:DropDownList>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Remarks">
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                                                    MaxLength="100"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <%# DataBinder.Eval(Container.DataItem, "Remarks") %>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX" BorderWidth="1px"
                                                                    MaxLength="100"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="10%" />
                                                        </asp:TemplateColumn>
                                                        <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit">
                                                            <HeaderStyle Width="8%" />
                                                        </asp:EditCommandColumn>
                                                        <asp:TemplateColumn HeaderText="Delete">
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add"></asp:LinkButton>
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="5%" />
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 2%; height: 21px;">
                                            </td>
                                            <td style="width: 20%; height: 21px;">
                                                <asp:Label ID="lbl_TotalExcessArticles" Text="Total :" CssClass="LABEL" Style="font-weight: bolder"
                                                    runat="server">
                                                </asp:Label>
                                                <asp:Label ID="lbl_TotalExcessArticlesValue" CssClass="LABEL" Style="font-weight: bolder"
                                                    runat="server">
                                                </asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <%-- </asp:Panel>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_ExcessDetails" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="100%">
                <tr>
                    <td style="width: 100%; height: 21px;" align="center">
                    <asp:Button id="btn_Exit" runat="server" Text="Exit" CssClass="BUTTON" OnClick="btn_Exit_Click">
                        </asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
