<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucProfitAndLossAccount.ascx.cs" Inherits="Finance_Reports_WucProfitAndLossAccount" %>
<%@ Register Src="../Accounting Vouchers/WucOnaccountAdjustment.ascx" TagName="WucOnaccountAdjustment"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucStartEndDate.ascx" TagName="WucStartEndDate"
    TagPrefix="uc2" %>
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
    
     function Open_Show_Window(Path)
    {            
        queryString = Path;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-190);
        var popH = (h-75);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                  
        window.open(queryString, 'FMainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', + menubar=no, resizable=no,scrollbars=yes') ;
        return false;
    }
    
    </script>
    <table style="width: 100%" class="TABLE" cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td style="height: 20px" colspan="7" class="TDGRADIENT">
            &nbsp;
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="PROFIT AND LOSS"></asp:Label>&nbsp;
            </td>
        
    </tr>
    <tr>
        <td colspan="4" style="height: 5px">
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 5px" align="left">
            &nbsp;<asp:Button ID="cmdprintPreview" runat="server" CssClass="BUTTON" Text="Print Preview" Width="181px" /></td>
        <td align="center" style="height: 5px" colspan="2">
            &nbsp;<asp:Label ID="lbl_Company_Name" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label><br />
            </td>
    </tr>
        <tr>
            <td align="left" colspan="4">
                <uc2:WucStartEndDate ID="WucStartEndDate1" runat="server" />
            </td>
        </tr>
    <tr>
        <td colspan="4" style="height: 5px">
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <table id="tb1" class="TABLE" border="1" cellspacing="0" style="width: 100%; border-collapse: collapse">
                <tr>
                    <td colspan="1" style="width: 72%; background: #E2E2E2">
                        <span style="font-size: 11px; font-family: Verdana; color: Black; font-weight: bold">
                            &nbsp;&nbsp;EXPENSES</span></td>
                    <td colspan="1" style="width: 28%; background: #E2E2E2" align="center">
                        <span style="font-size: 11px; font-family: Verdana; color: Black; font-weight: bold">
                            AMOUNT</span></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div style="overflow: auto; height: 300px">
                            <asp:DataGrid ID="dg_Expenses" CssClass="GRIDBALANCESHEET" runat="server" AllowSorting="True"
                                AutoGenerateColumns="False" ShowHeader="False" OnItemDataBound="DG_ItemDataBound_Expenses">
                                <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="EXPENSES">
                                        <ItemStyle HorizontalAlign="Left" Width="70%" />
                                        <ItemTemplate>                                            
                                            <a href="FrmGroupSummary.aspx?Id=<%# ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Ledger_Group_Id")))%>&Category=0&Hierarchy_Code=<%# Hierarchy_Code%>&Main_Id=<%# Main_Id%>&Menu_Item_Id=<%#ClassLibraryMVP.Util.EncryptInteger(MenuItemId)%>&Mode=<%#ClassLibraryMVP.Util.EncryptInteger(Mode)%>&IsConsolidated=<%# Is_Consol%>&StartDate=<%# ClassLibrary.UIControl.Util.EncryptString(Convert.ToString(StartDate)) %>&EndDate=<%# ClassLibrary.UIControl.Util.EncryptString(Convert.ToString(EndDate)) %>&Group=<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"Ledger_Group_Name"))%>"
                                                style="color: Black; font-family: Verdana; text-decoration: none">&nbsp;<%# DataBinder.Eval(Container.DataItem, "Ledger_Group_Name")%></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateColumn>
                                    
                                    <asp:TemplateColumn HeaderText="AMOUNT">
                                        <ItemStyle HorizontalAlign="Right" Width="30%" />
                                        <ItemTemplate>
                                            <%# Eval("Amount").ToString() %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="ID" Visible="False">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Ledger_Group_Id")%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
        <td colspan="2">
            <table id="Table1" class="TABLE" border="1" cellspacing="0" style="width: 100%; border-collapse: collapse">
                <tr style="border: width  1px">
                    <td colspan="1" style="width: 72%; background: #E2E2E2">
                        <span style="font-size: 11px; font-family: Verdana; color: Black; font-weight: bold">
                            &nbsp;&nbsp;INCOMES</span></td>
                    <td colspan="1" style="width: 28%; background: #E2E2E2" align="center">
                        <span style="font-size: 11px; font-family: Verdana; color: Black; font-weight: bold">
                            AMOUNT</span></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div style="overflow: auto; height: 300px">
                            <asp:DataGrid ID="dg_Income" CssClass="GRIDBALANCESHEET" runat="server" AllowSorting="True"
                                AutoGenerateColumns="False" ShowHeader="False" OnItemDataBound="DG_ItemDataBound_Income">
                                <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="INCOMES">
                                        <ItemStyle HorizontalAlign="Left" Width="70%" />
                                        <ItemTemplate>                                         
                                              <a href="FrmGroupSummary.aspx?Id=<%# ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Ledger_Group_Id")))%>&Category=0&Hierarchy_Code=<%# Hierarchy_Code%>&Main_Id=<%# Main_Id%>&Menu_Item_Id=<%#ClassLibraryMVP.Util.EncryptInteger(MenuItemId)%>&Mode=<%#ClassLibraryMVP.Util.EncryptInteger(Mode)%>&IsConsolidated=<%# Is_Consol%>&StartDate=<%# ClassLibrary.UIControl.Util.EncryptString(Convert.ToString(StartDate)) %>&EndDate=<%# ClassLibrary.UIControl.Util.EncryptString(Convert.ToString(EndDate)) %>&Group=<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"Ledger_Group_Name"))%>"
                                                style="color: Black; font-family: Verdana; text-decoration: none">&nbsp;<%# DataBinder.Eval(Container.DataItem, "Ledger_Group_Name")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="AMOUNT">
                                        <ItemStyle HorizontalAlign="Right" Width="30%" />
                                        <ItemTemplate>
                                            <%# Eval("Amount").ToString()%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="ID" Visible="False">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Ledger_Group_Id")%>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 30%; background-color: #ffeec2;">
            &nbsp;&nbsp;<asp:Label ID="Label2" runat="server" Text="TOTAL" CssClass="LABEL" Font-Bold="True"></asp:Label></td>
        <td style="width: 20%; background-color: #ffeec2;" align="right">
            <asp:Label ID="lbl_Liability_Total" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        <td style="width: 30%; background-color: #ffeec2;">
            &nbsp;&nbsp;<asp:Label ID="Label3" runat="server" Text="TOTAL" CssClass="LABEL" Font-Bold="True"></asp:Label></td>
        <td style="width: 30%; background-color: #ffeec2;" align="right">
            <asp:Label ID="lbl_Assets_Total" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
    </tr>
    <tr>
        <td colspan="5">
            <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR"></asp:Label>
        </td>
    </tr>
</table>
