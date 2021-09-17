<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Dly_BranchWise_Pending_Stock_Door_Godown_Summary.aspx.cs"
    Inherits="Reports_CL_Nandwana_UserDesk_Frm_Dly_BranchWise_Pending_Stock_Door_Godown_Summary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
function input_screen_action(action)
{
    if (action == 'view')
    {
        tbl_input_screen.style.display='inline';
    }
    else
    {
        tbl_input_screen.style.display='none';
    }
}

function viewwindow_DeliveryArea(PathDlyStk)
{
          
//        var w = screen.availWidth;
//        var h = screen.availHeight;
//        var popW = 900;
//        var popH = 650;
//        var leftPos = (w-popW)/2;
//        var topPos = (h-popH)/2; 
              
         var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-350);
        var popH = h-50;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        
 
        window.open(PathDlyStk, 'CustomPopUpDeliveryStockDetails', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Delivery Stock Door-Goodown Summary</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_Vehicle_Monitor" runat="server" />
        <table class="TABLE">
            <tr>
                <td class="TDGRADIENT" colspan="4">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Delivery Stock Door-Goodown Summary" /></td>
            </tr>
            <tr>
                <td class="TD1" colspan="4">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="TD1" style="width: 20%">
                    <asp:Label ID="lbl_Branch" runat="server" Text="Select Branch :" />
                </td>
                <td style="width: 40%">
                    <asp:DropDownList ID="ddl_Branch" runat="server" CssClass="DROPDOWN" />
                </td>
                <td class="TD1" style="width: 10%">
                </td>
                <td style="width: 30%">
                    <asp:Button ID="btn_view" runat="server" CssClass="BUTTON" Text="View" OnClick="btn_view_Click" /></td>
            </tr>
            <tr>
                <td class="TD1" colspan="4">
                    <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label></td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <table class="TABLE" id="tbl_Door" runat="server">
            <tr style="background-color: Lime" align="Center" >
                <td>
                    <asp:Label ID="lbl_Door" runat="server" CssClass="HEADINGLABEL" Font-Bold="true" ForeColor="Black" 
                         Text="Door Delivery"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_DeliveryStockList" UpdateMode="Conditional" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_DeliveryStockList" runat="server" Height="100px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_DoorDetails" runat="server" ShowFooter="True" AllowPaging="false"
                                    CssClass="GRID" AllowSorting="True" AllowCustomPaging="false" AutoGenerateColumns="False"
                                    PageSize="25" OnItemDataBound="dg_DetailsDoor_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Particulars" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_ParticularsDoor" Text='<%# DataBinder.Eval(Container, "DataItem.Particulars") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.SrNo") %>' />
                                                <asp:HiddenField ID="hdn_Door_SrNo" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.SrNo") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="No of LR" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "LrCount")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Door_SumTotal_Gc" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="200px" />
                                            <ItemStyle Width="200px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Qty" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Qty")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Door_SumArticles" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="200px" />
                                            <ItemStyle Width="200px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total Freight" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Freight")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Door_SumTotalFreight" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        
        
        <table>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <table class="TABLE" id="tbl_Godown" runat="server">
            <tr style="background-color: #ffff00" align="Center" >
                <td>
                    <asp:Label ID="lbl_Godown" runat="server" CssClass="HEADINGLABEL" Font-Bold="true" ForeColor="Red" 
                         Text="Godown Delivery"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="Panel1" runat="server" Height="100px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_GodownDetails" runat="server" ShowFooter="True" AllowPaging="false"
                                    CssClass="GRID" AllowSorting="True" AllowCustomPaging="false" AutoGenerateColumns="False"
                                    PageSize="25" OnItemDataBound="dg_DetailsGodown_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Particulars" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_ParticularsGodown" Text='<%# DataBinder.Eval(Container, "DataItem.Particulars") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.SrNo") %>' />
                                                <asp:HiddenField ID="hdn_Godown_SrNo" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.SrNo") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="No of LR" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "LrCount")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Godown_SumTotal_Gc" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="200px" />
                                            <ItemStyle Width="200px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Qty" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Qty")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Godown_SumArticles" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="200px" />
                                            <ItemStyle Width="200px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total Freight" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Freight")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Godown_SumTotalFreight" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
