<%@ Page AutoEventWireup="true" CodeFile="FrmPendingFreightDeliverySummary.aspx.cs"
    Inherits="Reports_CL_Nandwana_UserDesk_FrmPendingFreightDeliverySummary" Language="C#" %>

<%@ Register Src="~/CommonControls/WucDivisions.ascx" TagName="WucDivisions" TagPrefix="uc5" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc1" %>
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
        var popW = (w-100);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        
 
        window.open(PathDlyStk, 'CustomPopUpDeliveryArea', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Pending Freight Delivery</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Pending Freight Delivery"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                </td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 24%">
                                <uc1:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                            </td>
                            <td style="width: 10%" class="TD1">
                                <asp:Label ID="lbl_division" runat="server" CssClass="LABEL" Text="label" /></td>
                            <td style="width: 24%">
                                <uc5:WucDivisions ID="WucDivisions1" runat="server" />
                            </td>
                            <td class="TD1" style="width: 9%">
                            </td>
                            <td style="width: 9%">
                                <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                                    Text="Close Window" /></td>
                            <td style="width: 24%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" colspan="6" style="text-align: left">
                                <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_DeliveryStockList" UpdateMode="Conditional" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_DeliveryStockList" runat="server" Height="500px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Details" runat="server" ShowFooter="True" AllowPaging="true"
                                    CssClass="GRID" AllowSorting="True" AllowCustomPaging="false" AutoGenerateColumns="False"
                                    OnPageIndexChanged="dg_Details_PageIndexChanged" PageSize="25" OnItemDataBound="dg_Details_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Dly Branch" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_DlyBranch" Text='<%# DataBinder.Eval(Container, "DataItem.Branch_Name") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Branch_Id") %>' />
                                                <asp:HiddenField ID="hdn_Delivery_branch_Id" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Branch_Id") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="No of LR" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalLR")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotal_Gc" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total Freight" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalFreight")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotalFreight" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                         <asp:TemplateColumn HeaderText="Total Articles" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "TotalArticles")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotalArticles" runat="server" CssClass="LABEL" Font-Bold="true" />
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
