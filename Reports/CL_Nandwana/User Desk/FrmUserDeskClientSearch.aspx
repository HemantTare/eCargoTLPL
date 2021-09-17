<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmUserDeskClientSearch.aspx.cs"
    Inherits="Reports_CL_Nandwana_User_Desk_FrmUserDeskClientSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../../../Javascript/Common.js"></script>

<script type="text/javascript">


function viewwindow_ClientWalkIn(ClientId)
{
        var Path='../../../Master/Sales/FrmRegularClient.aspx?Menu_Item_Id=MwA2AA==&Mode=NAA=&Id=' + ClientId;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'ClientSearchWalkinClient', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function NewWalkInClient()
{
        var Path='../../../Master/Sales/FrmRegularClient.aspx?Menu_Item_Id=MwA2AA==&Mode=MQA=';
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'NewWalkInClient', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_ClientRegular(ClientId)
{
        var Path='../../../Master/Sales/FrmClient.aspx?Menu_Item_Id=MgA0AA==&Mode=NAA=&Id=' + ClientId;
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'ClientSearchRegularClient', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Client Search</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_PendingPDS" runat="server">
        </asp:ScriptManager>
        <table runat="server" id="Table1" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="Client Search"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr id="tr_Copy" runat="server">
                <td align="center">
                    <asp:Button ID="btn_CopyMobileNo" runat="server" CssClass="BUTTON" OnClick="btn_CopyMobileNo_Click"
                        Text="MobileNo." BorderColor="Gray" BackColor="#ccff33" Width="7%" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_CopyName" runat="server" CssClass="BUTTON" OnClick="btn_CopyName_Click"
                        Text="Name" BorderColor="Gray" BackColor="#ffcc66" Width="7%" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_CopyGSTNo" runat="server" CssClass="BUTTON" OnClick="btn_CopyGSTNo_Click"
                        Text="GSTNo." BorderColor="Gray" BackColor="#33ccff" Width="7%" />
                    <asp:HiddenField ID="hdn_MobileNo" runat="server" />
                    <asp:HiddenField ID="hdn_ClientName" runat="server" />
                    <asp:HiddenField ID="hdn_GSTNo" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
        <table class="TABLE">
            <tr>
                <td style="width: 25%; height: 20px;">
                    <asp:TextBox ID="txt_Search" runat="server" CssClass="TEXTBOX" MaxLength="20" Font-Bold="true"
                        onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)"></asp:TextBox></td>
                <td style="width: 25%; height: 20px;">
                    <asp:Label ID="lblCity" runat="server" CssClass="LABEL" Text="City :"></asp:Label>&nbsp;&nbsp;<asp:DropDownList
                        ID="ddl_City" runat="server" Width="75%">
                    </asp:DropDownList></td>
                <td style="width: 25%; height: 20px;">
                    &nbsp;<asp:Button ID="btn_Search" runat="server" CssClass="BUTTON" OnClick="btn_Search_Click"
                        Text="Search" /></td>
                <td style="width: 25%; height: 20px;">
                    &nbsp;<asp:Button ID="btn_null_sessions" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                        Text="Close Window" /></td>
            </tr>
        </table>
        <table runat="server" id="tbl_input_screen" class="TABLE">
            <tr>
                <td style="width: 80%">
                    <asp:Label ID="lbl_Error" Text="" runat="server" CssClass="LABELERROR"></asp:Label>
                </td>
                <td style="width: 20%">
                    &nbsp</td>
            </tr>
            <tr style="background-color: #006699;">
                <td align="Left" style="height: 17px; width: 80%">
                    <asp:Label ID="lbl_WalkInClient" runat="server" Font-Bold="true" ForeColor="White"
                        Text="Walk In Client"></asp:Label>
                </td>
                <td style="width: 20%; height: 17px; background-color: #47004d;" align="center">
                    <asp:LinkButton ID="lnk_btnAddWalkInClient" runat="server" Font-Bold="True" Font-Names="Verdana"
                        Font-Size="11px" Font-Underline="true" ForeColor="White" Text="ADD CLIENT"></asp:LinkButton></td>
            </tr>
        </table>
        <table class="TABLE">
            <tr id="Walkin">
                <td align="Left">
                    <asp:UpdatePanel ID="Upd_Pnl_PendingPDS" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV1" style="height: 250px; width: 95%; left: 1px; top: 1px;">
                                <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="false" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false"
                                    PagerStyle-HorizontalAlign="Left" OnItemDataBound="dg_Grid_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Client Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="25%"
                                            ItemStyle-Width="25%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_WalkInClientName" Text='<%# DataBinder.Eval(Container, "DataItem.Client_Name") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Client_Name") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Address1" HeaderText="Address Line 1"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Address2" HeaderText="Address Line 2"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="City_Name" HeaderText="City"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Mobile_No" HeaderText="Mobile No."></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Phone1" HeaderText="Phone 1"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Phone2" HeaderText="Phone 2"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="GSTNo" HeaderText="GST No."></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <table runat="server" id="Table2" class="TABLE">
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr style="background-color: #006699;">
                <td align="Left" style="height: 15px">
                    <asp:Label ID="lbl_RegularClient" runat="server" Font-Bold="true" ForeColor="White"
                        Text="Regular Client"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="TABLE">
            <tr id="Regular">
                <td align="Left">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid2" />
                        </Triggers>
                        <ContentTemplate>
                            <div class="DIV1" style="height: 250px; width: 95%;">
                                <asp:DataGrid ID="dg_Grid2" runat="server" ShowFooter="false" ItemStyle-HorizontalAlign="Left"
                                    HeaderStyle-HorizontalAlign="Left" CssClass="GRID" AllowSorting="True" AutoGenerateColumns="false"
                                    PagerStyle-HorizontalAlign="Left" OnItemDataBound="dg_Grid2_ItemDataBound">
                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Client Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="25%"
                                            ItemStyle-Width="25%">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtn_RegularClientName" Text='<%# DataBinder.Eval(Container, "DataItem.Client_Name") %>'
                                                    Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                    CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Client_Name") %>' />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <asp:BoundColumn DataField="Address1" HeaderText="Address Line 1"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Address2" HeaderText="Address Line 2"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="City_Name" HeaderText="City"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Mobile_No" HeaderText="Mobile No."></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Phone1" HeaderText="Phone 1"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="Phone2" HeaderText="Phone 2"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="GSTNo" HeaderText="GST No."></asp:BoundColumn>
                                    </Columns>
                                </asp:DataGrid>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
