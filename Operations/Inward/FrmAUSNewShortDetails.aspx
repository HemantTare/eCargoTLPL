<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmAUSNewShortDetails.aspx.cs" Inherits="Operations_Inward_FrmAUSNewShortDetails" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>
<script  language="javascript" type="text/javascript" src="../../Javascript/Operations/Inward/AUS.js"></script>
<script  language="javascript" type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript">
  function updateparent()
    {
        window.opener.update_ShortDetails();
    }

function Short_Articles(TextBox)
{
    var grid = document.getElementById('dg_ShortDetails');
    var row = TextBox.parentElement.parentElement;

    lbl_LoadedArticles= row.cells[4].getElementsByTagName('span');
    txt_ReceivedArticles= row.cells[5].getElementsByTagName('input');
    lbl_ShortArticles = row.cells[6].getElementsByTagName('span');

    var Short = 0;
    Short = val(lbl_LoadedArticles[0].innerText) - val(txt_ReceivedArticles[0].value);
    
    if (Short < 0)
    {
        alert('Received Articles Cannot be Greater Than Loaded Articles');
        txt_ReceivedArticles[0].value = lbl_LoadedArticles[0].innerText;
        lbl_ShortArticles[0].innerText = '0';
    }
    else if (Short == 0)
    {
        alert('Loaded Articles And Received Articles Can Not Be Equal');
        txt_ReceivedArticles[0].value = lbl_LoadedArticles[0].innerText;
        lbl_ShortArticles[0].innerText = '0';
    }
    else
    {
        lbl_ShortArticles[0].innerText = Short;
    }
}       
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Truck Unloading Sheet Short Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" style="width: 100%">
                <tr>
                    <td colspan="6">&nbsp;</td>
                </tr>
                <tr>
                    <td colspan="6" style="width: 100%">
                        <asp:UpdatePanel ID="upd_pnl_dg_AUSShortDetails" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <div id="Div_PDS" class="DIV" style="height: 300px; left: 0px; top: 0px;">
                                    <table style="width: 100%" border="0">
                                        <tr>
                                            <td colspan="6">
                                                <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                                                    <ContentTemplate>
                                                        <asp:DataGrid ID="dg_ShortDetails" runat="server" Width="100%" CssClass="GRID" AutoGenerateColumns="False"
                                                            ShowFooter="True" OnCancelCommand="dg_ShortDetails_CancelCommand" OnDeleteCommand="dg_ShortDetails_DeleteCommand"
                                                            OnEditCommand="dg_ShortDetails_EditCommand" OnItemCommand="dg_ShortDetails_ItemCommand"
                                                            OnItemDataBound="dg_ShortDetails_ItemDataBound" OnUpdateCommand="dg_ShortDetails_UpdateCommand">
                                                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                            <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                            <Columns>
                                                                <asp:TemplateColumn HeaderText="LR No." FooterStyle-Width="15%" HeaderStyle-Width="15%">
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "GC_No")%>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <cc1:DDLSearch ID="ddl_GC_No" runat="server" AllowNewText="False" IsCallBack="True"
                                                                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetGCNoForAUS" CallBackAfter="2"
                                                                            PostBack="False" InjectJSFunction="" Text="" OnTxtChange="ddl_GC_No_OnSelectedIndexChanged"/>
                                                                    </EditItemTemplate>
                                                                    <FooterTemplate>
                                                                        <cc1:DDLSearch ID="ddl_GC_No" runat="server" AllowNewText="False" IsCallBack="True"
                                                                            CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetGCNoForAUS" CallBackAfter="2"
                                                                            PostBack="True" InjectJSFunction="" Text=""  OnTxtChange="ddl_GC_No_OnSelectedIndexChanged"/>
                                                                    </FooterTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Item">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl_ItemName" runat="server" CssClass="TEXTBOXASLABEL"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "Item_Name") %>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lbl_ItemName" runat="server" CssClass="TEXTBOXASLABEL"></asp:Label>
                                                                    </EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Packing">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl_Packing" runat="server" CssClass="TEXTBOXASLABEL"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "Packing_Type") %>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lbl_Packing" runat="server" CssClass="TEXTBOXASLABEL"></asp:Label>
                                                                    </EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center" />
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Size">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl_Size" runat="server" CssClass="TEXTBOXASLABEL"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "SizeName") %>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lbl_Size" runat="server" CssClass="TEXTBOXASLABEL"></asp:Label>
                                                                    </EditItemTemplate>
                                                                    <ItemStyle HorizontalAlign="Center"  />
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Loaded Articles">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl_LoadedArticles" runat="server" CssClass="TEXTBOXNOSASLABEL" Height="17px"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "Loaded_Articles") %>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lbl_LoadedArticles" runat="server" CssClass="TEXTBOXNOSASLABEL" Height="17px"></asp:Label>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Received Articles">
                                                                    <FooterTemplate>
                                                                        <asp:TextBox ID="txt_ReceivedArticles" CssClass="TEXTBOXNOS" BorderWidth="1px" onblur="Short_Articles(this)"
                                                                            MaxLength="4" runat="server" onkeypress="return Only_Integers(this,event)" Height="17px"></asp:TextBox>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "Received_Articles")%>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txt_ReceivedArticles" onkeypress="return Only_Integers(this,event)" onblur="Short_Articles(this)"
                                                                            CssClass="TEXTBOXNOS" MaxLength="4" runat="server" Height="17px"></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Short Articles">
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbl_ShortArticles" runat="server" CssClass="TEXTBOXNOSASLABEL"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <ItemTemplate>
                                                                        <%# DataBinder.Eval(Container.DataItem, "Short_Articles") %>
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:Label ID="lbl_ShortArticles" runat="server" CssClass="TEXTBOXNOSASLABEL"></asp:Label>
                                                                    </EditItemTemplate>
                                                                    <HeaderStyle Width="10%" />
                                                                    <ItemStyle HorizontalAlign="Right" />
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
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="dg_ShortDetails" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2" style="width:30%"></td>
                                            <td align="right" style="width:20%"> <asp:Label ID="lbl_TotalShortArticles" Text="Total Short:" CssClass="LABEL" Style="font-weight: bolder"
                                                    runat="server">
                                                </asp:Label></td>
                                            <td align="left" style="width:20%">
                                               
                                                <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                                                    <ContentTemplate>
                                                <asp:Label ID="lbl_TotalShortArticlesValue" CssClass="LABEL" Style="font-weight: bolder"
                                                    runat="server">
                                                </asp:Label>
                                                </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="dg_ShortDetails" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td colspan="2" style="width:30%"></td>
                                        </tr>
                                    </table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_ShortDetails" />
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
                        <asp:Button ID="btn_Exit" runat="server" Text="Exit" CssClass="BUTTON" OnClick="btn_Exit_Click">
                        </asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
