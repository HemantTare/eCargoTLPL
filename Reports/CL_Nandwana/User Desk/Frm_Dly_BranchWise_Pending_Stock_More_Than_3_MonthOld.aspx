<%@ Page AutoEventWireup="true" CodeFile="Frm_Dly_BranchWise_Pending_Stock_More_Than_3_MonthOld.aspx.cs"
    Inherits="Reports_CL_Nandwana_UserDesk_Frm_Dly_BranchWise_Pending_Stock_More_Than_3_MonthOld"
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


function viewwindow_DeliveryArea(PathDlyStk)
{
              
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
<head runat="server">
    <title>Delivery Stock List View More Than 3 Months Old</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Delivery Stock List More Than 3 Months Old"></asp:Label>
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
            <tr style="background-color: Navy" align="center">
                <td>
                    <asp:LinkButton ID="lnkbtn_OldStock" runat="server" CssClass="HEADINGLABEL" Font-Bold="true"
                        ForeColor="Yellow" Text="More Than 3 Months Old"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                        <Triggers>
                        </Triggers>
                        <ContentTemplate>
                            <asp:Panel ID="Panel2" runat="server" Height="100px" ScrollBars="Auto">
                                <asp:DataGrid ID="dg_Details3" runat="server" ShowFooter="True" AllowPaging="false"
                                    CssClass="GRID" AllowSorting="True" AllowCustomPaging="false" AutoGenerateColumns="False"
                                    PageSize="25" OnItemDataBound="dg_Details3_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Dly Branch" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_DlyBranch3" Text='<%# DataBinder.Eval(Container, "DataItem.Dly Branch") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Delivery_branch_Id") %>' />
                                                <asp:HiddenField ID="hdn_Delivery_branch_Id2" runat="server" Value='<%# DataBinder.Eval(Container, "DataItem.Delivery_branch_Id") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle Width="100px" />
                                            <ItemStyle Width="100px" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="No of LR" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "NoOfLR")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotal_Gc3" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="200px" />
                                            <ItemStyle Width="200px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Qty" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Pkgs")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumArticles3" runat="server" CssClass="LABEL" Font-Bold="true" />
                                            </FooterTemplate>
                                            <HeaderStyle Width="200px" />
                                            <ItemStyle Width="200px" />
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Total Freight" HeaderStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <%# DataBinder.Eval(Container.DataItem, "Total Freight")%>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbl_SumTotalFreight3" runat="server" CssClass="LABEL" Font-Bold="true" />
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
