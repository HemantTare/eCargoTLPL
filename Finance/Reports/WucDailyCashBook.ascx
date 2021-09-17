<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDailyCashBook.ascx.cs"
    Inherits="Finance_Reports_WucDailyCashBook" %>
<%@ Register Src="~/CommonControls/WucHierarchyFiltration_FA.ascx" TagName="WucHierarchyFiltration"
    TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/WucGridSearch.ascx" TagName="WucGridSearch"
    TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc4" %>

<script type="text/javascript">

    function ChangePeriod()
    {
    
        var Path='../../CommonControls/FrmDateRange.aspx'
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-600);
        var popH = (h-500);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    function Open_Popup_Window(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w-100, popH = h-150;
        var leftPos = (w-popW)/2, topPos = (h-popH)/2;
        
        window.open (Path,'CustomPopUp','width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', menubar=no, resizable=yes,scrollbars=yes');
        return false;    
    }
    // OnClientClick="return ViewReport();"  var Path='../../Finance/Reports/Rpt_Viewer/frm_Ledger_Voucher_View.aspx'

    function ViewReport(Path)
    {  
       
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w-100, popH = h-150;
        var leftPos = (w-popW)/2, topPos = (h-popH)/2;
        
        window.open (Path,'CustomPopUp','width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', menubar=no, resizable=yes,scrollbars=yes');
        return false;
    }
</script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<table width="100%" class="TABLE">
    <tr>
        <td colspan="6" class="TDGRADIENT">
            &nbsp;<asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="LEDGER VOUCHER"
                Font-Bold="True" /></td>
    </tr>
    <tr>
        <td style="width: 17%">
        </td>
        <td style="width: 30%">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 34%">
        </td>
        <td style="width: 15%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 17%">
            <asp:Label ID="lbl_Branch_Name" runat="server" CssClass="LABEL" Font-Bold="True" Width="213px" Font-Size="Larger" ForeColor="#C00000"></asp:Label><br />
            <br />
            <asp:Label ID="lbl_Ledger_Name" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label></td>
        <td colspan="5">
            <uc2:WucHierarchyFiltration ID="WucHierarchyFiltration1" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <uc3:WucGridSearch ID="WucGridSearch1" runat="server"  />
        </td>
        <td style="width: 15%" valign="middle">
            <asp:Button ID="btn_Show_Preview" runat="server" CssClass="BUTTON" 
                Text="PREVIEW REPORT" Width="142px" OnClick="btn_Show_Preview_Click" /></td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:UpdatePanel runat="server" ID="upnl_DataGrid">
                <ContentTemplate>
                    <asp:DataGrid ID="dgLedgerVoucher" runat="server" AllowPaging="True" AllowSorting="True"
                        AutoGenerateColumns="False" CellPadding="2" CssClass="Grid" PageSize="20" Width="100%"
                        OnItemDataBound="dgLedgerVoucher_ItemDataBound" OnPageIndexChanged="dgLedgerVoucher_PageIndexChanged">
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <FooterStyle CssClass="GRIDFOOTERTB" />
                        <PagerStyle Mode="NumericPages" />
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="Voucher_Id"></asp:BoundColumn>
                            <asp:BoundColumn Visible="False" DataField="Voucher_Type_Id"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Date" ItemStyle-VerticalAlign="Top">
                                <HeaderStyle Width="13%" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbl_Date">   <%#DataBinder.Eval(Container.DataItem, "Date")%></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Particulars" ItemStyle-VerticalAlign="Top">
                                <HeaderStyle Width="40%" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtn_View" Text='<%#DataBinder.Eval(Container.DataItem, "Particulars")%>'
                                        runat="server" Font-Underline="false" ForeColor="black">  </asp:LinkButton><br />
                                    <asp:DataGrid ID="dg_VoucherDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                        BorderWidth="0">
                                        <%--    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
--%>
                                        <HeaderStyle CssClass="HIDEGRIDCOL" />
                                        <%--    <FooterStyle CssClass="GRIDFOOTERTB"/> 
--%>
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <HeaderStyle Width="70%" />
                                                <ItemTemplate>
                                                    <asp:Label Font-Bold="true" Font-Italic="true" runat="server" ID="lbl_LedgerName"><%#DataBinder.Eval(Container.DataItem, "Ledger_Name")%></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="">
                                                <HeaderStyle Width="30%" />
                                                <ItemTemplate>
                                                    <asp:Label Font-Bold="true" Font-Italic="true" runat="server" ID="lbl_LedgerAmount"><%#DataBinder.Eval(Container.DataItem, "Ledger_Amount")%></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                    <asp:Label ID="lbl_Narration" runat="server" ForeColor="Red"> <%#DataBinder.Eval(Container.DataItem, "Narration")%> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Voucher Type" ItemStyle-VerticalAlign="Top">
                                <HeaderStyle Width="14%" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbl_Voucher_Type"><%#DataBinder.Eval(Container.DataItem, "Voucher_Type")%></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Voucher No" ItemStyle-VerticalAlign="Top">
                                <HeaderStyle Width="13%" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbl_Voucher_No"><%#DataBinder.Eval(Container.DataItem, "Voucher_No")%></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Debit" ItemStyle-VerticalAlign="Top">
                                <HeaderStyle Width="10%" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbl_Debit"><%#DataBinder.Eval(Container.DataItem, "Debit")%></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Credit" ItemStyle-VerticalAlign="Top">
                                <HeaderStyle Width="10%" HorizontalAlign="Right" />
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbl_Credit"><%#DataBinder.Eval(Container.DataItem, "Credit")%></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucGridSearch1" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:UpdatePanel ID="upnl_Totals" runat="server">
                <ContentTemplate>
                    <table class="TABLE">
                        <tr>
                            <td colspan="1" style="width: 36%">
                                &nbsp;</td>
                            <td class="TD1" colspan="2" style="font-weight: bold; width: 20%">
                                Opening Balance:</td>
                            <td align="right" style="font-weight: bold; width: 22%">
                                <asp:Label ID="lbl_opening_Balance_Debit" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>&nbsp;</td>
                            <td align="right" style="font-weight: bold; width: 22%">
                                <asp:Label ID="lbl_opening_Balance_Credit" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="1" style="font-weight: bold; width: 36%; border-bottom: gray 2px solid">
                                &nbsp;</td>
                            <td class="TD1" colspan="2" style="font-weight: bold; width: 20%; border-bottom: gray 2px solid">
                                Current Total:</td>
                            <td align="right" style="font-weight: bold; width: 22%; border-bottom: gray 2px solid">
                                <asp:Label ID="lbl_Current_Total_Debit" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>&nbsp;</td>
                            <td align="right" style="font-weight: bold; width: 22%; border-bottom: gray 2px solid">
                                <asp:Label ID="lbl_Current_Total_Credit" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>&nbsp;</td>
                        </tr>
                        <tr>
                            <td colspan="1" style="font-weight: bold; width: 36%">
                                &nbsp;
                            </td>
                            <td class="TD1" colspan="2" style="font-weight: bold; width: 20%">
                                Closing Balance:</td>
                            <td id="TD_CL_Dr" runat="server" align="right" style="font-weight: bold; width: 22%">
                                <asp:Label ID="lbl_Closing_Balance_Debit" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>&nbsp;</td>
                            <td id="TD_CL_Cr" runat="server" align="right" style="font-weight: bold; width: 22%">
                                <asp:Label ID="lbl_Closing_Balance_Credit" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>&nbsp;</td>
                        </tr>
                        <tr>
                            <td align="center" colspan="6">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="WucGridSearch1" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 17%">
            <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR"></asp:Label></td>
        <td style="width: 30%; text-align: right;">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 34%">
        </td>
        <td style="width: 15%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 17%">
        </td>
        <td style="width: 30%; text-align: right">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 34%">
        </td>
        <td style="width: 15%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 17%">
        </td>
        <td style="width: 30%; text-align: right">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 34%">
        </td>
        <td style="width: 15%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 17%">
        </td>
        <td style="width: 30%; text-align: right">
        </td>
        <td style="width: 1%">
        </td>
        <td style="width: 34%">
        </td>
        <td style="width: 15%">
        </td>
        <td style="width: 1%">
        </td>
    </tr>
</table>
