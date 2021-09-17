<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_BranchWiseClosingCash.aspx.cs"
    Inherits="Finance_Reports_Frm_BranchWiseClosingCash" %>

<%@ Register Src="../../CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">

function viewwindow_general(Path)
{ 
 
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 2000;
        var popH = 800;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
         
        window.open(Path, 'NewPath', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=yes,scrollbars=yes')
        return false;
}
 
function GridDetails(Path)
{ 
    window.open(Path,'CashBalance','width=2000,height=800,top=50,left=50,menubar=no,resizable=yes,scrollbars=yes')
    return false;
} 


</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Branch Wise Closing Cash</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin: 0px">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_BookingRegister" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE" onclick="rr()">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Branch Wise Closing Cash"></asp:Label>
                </td>
            </tr>
        </table>

        <table class="TABLE">
            <tr>
                
                <td style="width: 10%">
                    <uc4:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server" />
                </td>

                <td style="width: 50%">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                    <asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
        
        
        <table class="TABLE">
            <tr>
                <td>
                    <asp:UpdatePanel ID="Upd_Pnl_BookingRegister" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV1" style="height: 610px; width: 100%;">
                                <asp:GridView ID="dg_Grid" runat="server" ShowFooter="False"   
                                    CssClass="GRID" AllowSorting="True" AutoGenerateColumns="False" PagerStyle-HorizontalAlign="Left"
                                    PageSize="100" OnPageIndexChanging="dg_Grid_PageIndexChanging" OnRowCreated="dg_Grid_RowCreated" OnRowDataBound="dg_Grid_RowDataBound" ShowHeader="False">
                                    <AlternatingRowStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" HorizontalAlign="Left" />
                                    <Columns>

                                        
<%--                                        <asp:BoundField DataField="BranchName" HeaderText="Branch">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" />
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundField>--%>
                                        
                                        <asp:TemplateField HeaderText="Branch">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_BranchName" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName") %>'></asp:LinkButton>
                                                <asp:HiddenField ID="hdfn_BranchId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "BranchId") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        
                                        <asp:BoundField DataField="Opening" HeaderText="Opening">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Receipt" HeaderText="Receipt">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Payment" HeaderText="Payment">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundField>
                                                                                             
                                        <asp:BoundField DataField="Closing" HeaderText="Balance">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundField>
                                        
                                        <asp:BoundField DataField="Divider" HeaderText="Divider">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" BackColor="White" />
                                            <HeaderStyle Width="6%" />
                                        </asp:BoundField>

<%--                                        <asp:BoundField DataField="BranchName_" HeaderText="Branch">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Left" />
                                            <HeaderStyle Width="15%" />
                                        </asp:BoundField>--%>
                                        
                                        <asp:TemplateField HeaderText="Branch">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_BranchName2" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName_") %>'></asp:LinkButton>
                                                <asp:HiddenField ID="hdfn_BranchId2" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "BranchId2") %>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        

                                        <asp:BoundField DataField="Opening_" HeaderText="Opening">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Receipt_" HeaderText="Receipt">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundField>

                                        <asp:BoundField DataField="Payment_" HeaderText="Payment">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundField>
                                                                                             
                                        <asp:BoundField DataField="Closing_" HeaderText="Balance">
                                            <ItemStyle Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" HorizontalAlign="Right" />
                                            <HeaderStyle Width="8%" />
                                        </asp:BoundField>
                                                                                
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
