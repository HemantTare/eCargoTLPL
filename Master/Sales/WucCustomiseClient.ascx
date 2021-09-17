<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucCustomiseClient.ascx.cs"
    Inherits="Master_Sales_WucCustomiseClient" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>

<asp:ScriptManager ID="scm_CustomiseClient" runat="server" />

<script type="text/javascript">
  
    function Open_View_Window(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;

        window.open(Path, 'ViewPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    } 
    function Open_Main_Window(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;

        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    } 
</script>

<table class="TABLE" style="width: 100%">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            &nbsp;<asp:Label ID="lbl_Head" runat="server" CssClass="HEADINGLABEL" Text="MERGE CLIENT"></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100%" colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 15%;">
            <asp:Label ID="lbl_CustmClient" runat="server" CssClass="LABEL" Text="Client To Be Kept :"></asp:Label>
        </td>
        <td style="width: 20%;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_ClientDetails" />
                </Triggers>
                <ContentTemplate>
                    <cc1:DDLSearch ID="ddl_MergeClient" runat="server" AllowNewText="True" IsCallBack="True"
                        CallBackFunction="Raj.EC.SalesModel.CustomiseClientModel.Get_CustomiseClient"
                        PostBack="true" OnTxtChange="ddl_MergeClient_TxtChange" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%" class="TDMANDATORY">
            *</td>
        <td style="width: 65%" colspan="3">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_MergeClient" />
                </Triggers>
                <ContentTemplate>
                    <asp:LinkButton ID="lbtn_ViewMergeClient" Text="Client View" runat="server"></asp:LinkButton>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%">
            <asp:UpdatePanel ID="upd_pnl_dg_ClientDetails" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_ClientDetails" />
                </Triggers>
                <ContentTemplate>
                    <asp:DataGrid ID="dg_ClientDetails" runat="server" CssClass="GRID" AutoGenerateColumns="False"
                        ShowFooter="True" OnDeleteCommand="dg_ClientDetails_DeleteCommand" OnItemCommand="dg_ClientDetails_ItemCommand"
                        OnItemDataBound="dg_ClientDetails_ItemDataBound">
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <Columns>
                            <asp:BoundColumn DataField="Client_Id" Visible="false"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Clients To Be Merged" HeaderStyle-Width="80%" HeaderStyle-HorizontalAlign="Left">
                                <FooterTemplate>
                                    <cc1:DDLSearch ID="ddl_Client" CallBackAfter="2" runat="server" IsCallBack="True"
                                        CallBackFunction="Raj.EC.SalesModel.CustomiseClientModel.Get_CustomiseClient"
                                        AllowNewText="True" Text="" />
                                </FooterTemplate>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem, "Client_Name") %>
                                </ItemTemplate>
                                <ItemStyle Width="80%" />
                                <ItemStyle HorizontalAlign="Left" />
                                <FooterStyle HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="View" HeaderStyle-Width="10%">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_View" Text="View" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Add/Delete" HeaderStyle-Width="10%">
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add"></asp:LinkButton>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete"></asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Width="10%" />
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click" />
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="Upd_Pnla" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                    <asp:AsyncPostBackTrigger ControlID="dg_ClientDetails" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:Label ID="Label1" runat="server" CssClass="LABELERROR" Text="Fields with * mark are mandatory"></asp:Label>
        </td>
    </tr>
</table>
