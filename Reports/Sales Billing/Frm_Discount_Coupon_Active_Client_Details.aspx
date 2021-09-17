<%@ Page AutoEventWireup="true" CodeFile="Frm_Discount_Coupon_Active_Client_Details.aspx.cs"
    Inherits="Reports_SalesBilling_UserDesk_Frm_Discount_Coupon_Active_Client_Details"
    Language="C#" %>

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

function viewwindow_ClientCouponDetails(PathCouponsUsed)
{
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-400);
        var popH = h-200;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
 
        window.open(PathCouponsUsed, 'CouponsUsed', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}


</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Discount Coupon Active Client Details</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Discount Coupon Active Client Details"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                <asp:Label ID="lbl_Remarks" runat="server" CssClass="HEADINGLABEL" Font-Bold="true" ForeColor="#ff0066" Text="" ></asp:Label>
                </td>
            </tr>
            
            <tr>
                <td style="width: 50%">
                    <uc5:WucDivisions ID="WucDivisions1" runat="server" />
                    <asp:Label ID="lbl_division" runat="server" CssClass="LABEL" Text="label" /></td>
            </tr>
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 5%" class="TD1">
                            </td>
                            <td style="width: 24%">
                                <uc1:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                            </td>
                            <td class="TD1" style="width: 14%">
                                <asp:Label ID="lblCity" runat="server" Text="City :" Font-Bold="True"></asp:Label>
                            </td>
                            <td style="width: 24%">
                                <asp:Label ID="txtlblCity" runat="server" Text="Label" Font-Bold="True"></asp:Label></td>
                            <td style="width: 9%">
                            </td>
                            <td style="width: 24%">
                                <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                                    Text="Close Window" /></td>
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
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="pnl_DeliveryStockList" runat="server" Height="900px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID"
                                    AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                                    OnItemDataBound="dg_Grid_ItemDataBound" PageSize="25">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                    
                                        <asp:TemplateColumn HeaderText="ClientName">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "ClientName")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true" Text="Total : "/>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                    

                                        <asp:TemplateColumn HeaderText="Coupons">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Coupons")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Total_Client" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>

                                        <asp:TemplateColumn HeaderText="CouponsUsed" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                 <asp:LinkButton ID="lbtn_CouponsUsed" Text='<%# DataBinder.Eval(Container, "DataItem.CouponsUsed") %>'
                                                   Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                   CommandArgument='<%# DataBinder.Eval(Container, "DataItem.CouponsUsed") %>' />
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumCouponsUsed" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        
                                        <asp:TemplateColumn HeaderText="CouponDiscount" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "CouponDiscount")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_Total_Discount" runat="server" CssClass="LABEL" Font-Bold="true" />
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
