<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucBalanceSheet.ascx.cs"
    Inherits="Reports_WucBalanceSheet" %>
<%@ Register Src="~/CommonControls/WucStartEndDate.ascx" TagName="WucStartEndDate" TagPrefix="uc1" %>
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

   //PRINT PREVIEW
   function Open_Show_Window(Path)
    {            
        queryString = Path;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-210);
        var popH = (h-75);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                  
        window.open(queryString, 'FMainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes') ;
        return false;
    }
</script>

<table style="width: 100%" class="TABLE" cellspacing="0" cellpadding="0" border="0">
    <tr>
        <td style="height: 20px" colspan="3" class="TDGRADIENT">
            &nbsp;
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="BALANCE SHEET"></asp:Label>&nbsp;
            </td>
        <td colspan="1" class="TDGRADIENT" align="right" style="height: 20px">
            <asp:Button ID="btn_Detail" runat="server" CssClass="BUTTONO" OnClick="btn_Detail_Click"
                Text="Detailed" Width="150px" Font-Names="verdana" Height="20px" />&nbsp;&nbsp;</td>
    </tr>
    <tr>
        <td colspan="4" style="height: 5px">
        </td>
    </tr>
    <tr>
        <td colspan="2" style="height: 5px" align="left">
            &nbsp;
            <asp:Button ID="cmdPreview" runat="server" CssClass="BUTTON" Text="Print Preview" 
                Width="181px" /></td>
        <td align="center" style="height: 5px" colspan="2">
            &nbsp;<asp:Label ID="lbl_Company_Name" runat="server" Font-Bold="True" ForeColor="Maroon"></asp:Label><br />
            </td>
    </tr>
    <tr>
        <td align="left" colspan="4" style="height: 5px">
        <uc1:WucStartEndDate ID="WucStartEndDate" runat="server" />
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
                            &nbsp;&nbsp;LIABILITIES</span></td>
                    <td colspan="1" style="width: 28%; background: #E2E2E2" align="center">
                        <span style="font-size: 11px; font-family: Verdana; color: Black; font-weight: bold">
                            AMOUNT</span></td>
                </tr>
                <tr>
                    <td colspan="3" style="height: 322px">
                        <div style="overflow: auto; height: 300px">
                            <asp:DataGrid ID="dg_Libs" CssClass="GRIDBALANCESHEET" runat="server" AllowSorting="True"
                                AutoGenerateColumns="False" ShowHeader="False" OnItemDataBound="DG_ItemDataBound_Lib">
                                <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="LIABILITIES">
                                        <ItemStyle HorizontalAlign="Left" Width="60%" />
                                        <ItemTemplate>
                                            <%--<asp:HyperLink ID="lnkname" runat="server" CssClass="LINKREPORTS" NavigateUrl='<%# "FrmGroupsummary.aspx?Id=" +ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Id")))+"&Name=" +ClassLibraryMVP.Util.EncryptString(Convert.ToString(DataBinder.Eval(Container.DataItem,"Liabilities")))+ "&Menu_Item_Id=" +ClassLibraryMVP.Util.EncryptInteger(Menu_Item_Id)+ "&Cat_Id=" +ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Category")))%>'
                                                  Text='<%# DataBinder.Eval(Container.DataItem,"Liabilities") %>'></asp:HyperLink>--%>
                                            <a href="FrmGroupsummary.aspx?Id=<%# ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Id")))%>&Category=<%# ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Category")))%>&Hierarchy_Code=<%# Hierarchy_Code%>&Main_Id=<%# Main_Id%>&IsConsolidated=<%# Is_Consol%>&StartDate=<%# ClassLibrary.UIControl.Util.EncryptString(Convert.ToString(Start_Date)) %>&EndDate=<%# ClassLibrary.UIControl.Util.EncryptString(Convert.ToString(End_Date)) %>&Menu_Item_Id=<%# ClassLibrary.UIControl.Util.EncryptString(Convert.ToString(Menu_Item_Id)) %>&Mode=<%# ClassLibrary.UIControl.Util.EncryptString(Convert.ToString(Mode))%>&Group=<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"Liabilities"))%>""
                                                style="color: Black; font-family: Verdana; text-decoration: none">&nbsp;<%# DataBinder.Eval(Container.DataItem, "Liabilities")%></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="AMOUNT" Visible="false">
                                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                                        <ItemTemplate>
                                            <%# Eval("Opening_Bal").ToString() %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="AMOUNT">
                                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                                        <ItemTemplate>
                                            <%# Eval("Closing_Bal").ToString() %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="ID" Visible="False">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Id")%>
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
                            &nbsp;&nbsp;ASSETS</span></td>
                    <td colspan="1" style="width: 28%; background: #E2E2E2" align="center">
                        <span style="font-size: 11px; font-family: Verdana; color: Black; font-weight: bold">
                            AMOUNT</span></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <div style="overflow: auto; height: 300px">
                            <asp:DataGrid ID="dg_Assets" CssClass="GRIDBALANCESHEET" runat="server" AllowSorting="True"
                                AutoGenerateColumns="False" ShowHeader="False" OnItemDataBound="DG_ItemDataBound_Asset">
                                <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" />
                                <Columns>
                                    <asp:TemplateColumn HeaderText="ASSETS">
                                        <ItemStyle HorizontalAlign="Left" Width="60%" />
                                        <ItemTemplate>
                                            <%--<asp:HyperLink ID="lnkname" runat="server" CssClass="LINKREPORTS" 
                                                NavigateUrl='<%# "frm_Group_Summary.aspx?Id=" +ClassLibrary.UIControl.Util.EncryptInteger(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Id"))) +"&Name=" +ClassLibrary.UIControl.Util.EncryptString(Convert.ToString(DataBinder.Eval(Container.DataItem,"Assets")))+ "&Menu_Item_Id=" +ClassLibrary.UIControl.Util.EncryptInteger(Menu_Item_Id)+ "&Cat_Id=" +ClassLibrary.UIControl.Util.EncryptInteger(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Cat")))%>'
                                                Text='<%# DataBinder.Eval(Container.DataItem,"Assets") %>'></asp:HyperLink>--%>
                                            <%--<a href="FrmGroupSummary.aspx?Id=<%#ClassLibrary.UIControl.Util.EncryptString(Convert.ToString( DataBinder.Eval(Container.DataItem,"Id")))%>&Cat_Id=<%# ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Category")))%>&Hierarchy_Code=<%# Hierarchy_Code%>&Main_Id=<%# Main_Id%>&Is_Consol=<%# Is_Consol%>&Start_Date=<%# ClassLibrary.UIControl.Util.EncryptString(Convert.ToString( Start_Date)) %>&End_Date=<%# ClassLibrary.UIControl.Util.EncryptString(Convert.ToString( End_Date)) %>"
                                                style="color: Black; font-family: Verdana; text-decoration: none">&nbsp;<%# DataBinder.Eval(Container.DataItem, "Assets")%></a>
                                            --%>
                                            <a href="FrmGroupsummary.aspx?Id=<%# ClassLibraryMVP.Util.EncryptString(Convert.ToString(DataBinder.Eval(Container.DataItem,"Id")))%>&Category=<%# ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Category")))%>&Hierarchy_Code=<%# Hierarchy_Code%>&Main_Id=<%# Main_Id%>&IsConsolidated=<%# Is_Consol%>&StartDate=<%# ClassLibrary.UIControl.Util.EncryptString(Convert.ToString( Start_Date)) %>&EndDate=<%# ClassLibrary.UIControl.Util.EncryptString(Convert.ToString( End_Date)) %>&Menu_Item_Id=<%# ClassLibrary.UIControl.Util.EncryptString(Convert.ToString(Menu_Item_Id)) %>&Mode=<%# ClassLibrary.UIControl.Util.EncryptString(Convert.ToString(Mode))%>&Group=<%# Convert.ToString(DataBinder.Eval(Container.DataItem,"Assets"))%>""
                                                style="color: Black; font-family: Verdana; text-decoration: none">&nbsp;<%# DataBinder.Eval(Container.DataItem, "Assets")%></a>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="AMOUNT" Visible="false">
                                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                                        <ItemTemplate>
                                            <%# Eval("Opening_Bal").ToString() %>
                                            &nbsp;
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="AMOUNT">
                                        <ItemStyle HorizontalAlign="Right" Width="20%" />
                                        <ItemTemplate>
                                            <%# Eval("Closing_Bal").ToString() %>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="ID" Visible="False">
                                        <ItemTemplate>
                                            <%#DataBinder.Eval(Container.DataItem, "Id")%>
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
