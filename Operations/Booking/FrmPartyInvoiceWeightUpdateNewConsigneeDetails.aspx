<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmPartyInvoiceWeightUpdateNewConsigneeDetails.aspx.cs"
    Inherits="Operations_Booking_FrmPartyInvoiceWeightUpdateNewConsigneeDetails" %>

<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">

function viewwindow_ClientConsignee(ClientId,IsRegular)
{
        if(IsRegular == 'True')
        {
            var Path='../../Master/Sales/FrmRegularClient.aspx?Menu_Item_Id=MwA2AA==&Mode=MwA=&Id=' + ClientId;
        }
        else
        {
            var Path='../../Master/Sales/FrmClient.aspx?Menu_Item_Id=MgA0AA==&Mode=MwA=&Id=' + ClientId;
        }
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'NewConsigneeDetailsVashi', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_DeliveryArea(DlyAreaId)
{
        var Path='../../Master/Location/FrmDeliveryArea.aspx?Menu_Item_Id=MgA2ADMA&Mode=MwA=&Id=' + DlyAreaId;
    
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'NewDeliveryAreaVashi', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Party Invoice Weight Update New Consignee Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scm_Comm">
        </asp:ScriptManager>
        <table class="TABLE" border="0" cellpadding="0" cellspacing="0" style="width: 100%"
            id="TABLE1">
            <tr>
                <td align="left" style="background-color: Yellow;">
                    <asp:Label ID="lbl_Heading" runat="server" Text="Records In Green Colors Are Verified"
                        Font-Bold="true" ForeColor="DarkGreen" Font-Size="Medium"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" style="height: 13px">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="height: 575px;" valign="top">
                    <div id="Div_PDS" class="DIV" style="height: 550px; width: 100%; text-align: center;">
                        <asp:DataGrid ID="datagrid1" runat="server" AutoGenerateColumns="False" DataKeyField="Consignee_Client_ID"
                            CellPadding="3" CssClass="Grid" AllowPaging="False" Style="width: 98%" OnItemDataBound="datagrid1_ItemDataBound">
                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <HeaderStyle Height="15px" Font-Size="11px" Font-Names="Verdana" Font-Bold="True"
                                HorizontalAlign="Left" ForeColor="Black" BorderStyle="Solid" BorderColor="#9495A2"
                                BorderWidth="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader">
                            </HeaderStyle>
                            <Columns>

                                <asp:BoundColumn DataField="DlyAreaID" Visible="False"></asp:BoundColumn>
                                
                                <asp:TemplateColumn HeaderText="City" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "City")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                
                                <asp:TemplateColumn HeaderText="Delivery Area" HeaderStyle-HorizontalAlign="Left"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_DlyArea" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "DlyArea") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>

                                <asp:BoundColumn DataField="Consignee_Client_ID" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Consignee" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Consignee_Name" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "Consignee_Name") %>' />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="Is_Consignee_Regular_Client" Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Is ODA" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "IsODA")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Km From Branch" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "KmFromBranch")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="IsVerified" HeaderStyle-HorizontalAlign="Center"
                                    ItemStyle-Width="1%">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_Verified" Checked='<%#DataBinder.Eval(Container.DataItem,"IsVerified")%>'
                                            runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="IsVerifiedPrevious" Visible="False">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_VerifiedPrevious" Checked='<%#DataBinder.Eval(Container.DataItem,"IsVerifiedPrevious")%>'
                                            runat="server" Style="text-align: center" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="VerifiedBy" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "VerifiedBy")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Mode="NumericPages" HorizontalAlign="Left" PageButtonCount="50" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td align="center" style="height: 13px">
                    &nbsp;<asp:Button ID="btnSave" runat="server" CssClass="BUTTON" OnClick="Btn_Save_Click"
                        Text="Save" /></td>
            </tr>
            <tr>
                <td align="left">
                    <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Label ID="lbl_errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                Text=""></asp:Label>
                            <asp:HiddenField ID="hdnKeyID" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
