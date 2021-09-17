<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frm_ClosingStockTally.aspx.cs" Inherits="Reports_User_Desk_frm_ClosingStockTally" %>
<%@ Register Src="~/CommonControls/WucSelectedItems.ascx" TagName="WucSelectedItems" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Closing Stock Tally</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
    function viewwindow_ForGC(Path)            //For GC View
    {        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
      window.open(Path, 'ViewGC', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
      return false;
    }
    
    function viewwindow_TANDT(DocType,DocNo)
    {
        var Path="../../TrackNTrace/FrmMainTrackNTrace.aspx?Doc_Type="+ DocType +"&Doc_No=" + DocNo+"&Doc_SubType=STATUS" ;

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
            
        window.open(Path, 'View_Track_And_Trace', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
        return false;
    }
    function viewwindow_ClientWalkIn(ClientId)
    {
        var Path='../../Master/Sales/FrmRegularClient.aspx?Menu_Item_Id=MwA2AA==&Mode=NAA=&Id=' + ClientId;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'ClientSearchWalkinClient', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }

    function viewwindow_ClientRegular(ClientId)
    {
        var Path='../../Master/Sales/FrmClient.aspx?Menu_Item_Id=MgA0AA==&Mode=NAA=&Id=' + ClientId;
        
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:ScriptManager ID="scm_cst" runat="server">
            </asp:ScriptManager>
            <table class="TABLE">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="Closing Stock Tally"></asp:Label>
                    </td>
                </tr>           
                <tr>
                    <td colspan="6">
                        <uc2:WucSelectedItems ID="WucSelectedItems1" runat="server" />
                    </td>
                </tr>
                <tr id="tr_Crossingstocktally" runat="server">
                    <td colspan="6">
                        <asp:UpdatePanel ID="up_Crossingstocktally" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="pnl_DlyBranch" runat="server" CssClass="PANEL" ScrollBars="Auto">
                                    <asp:DataGrid ID="dg_Crossingstocktally" runat="server" AutoGenerateColumns="False" OnItemDataBound="dg_Crossingstocktally_ItemDataBound"
                                        CssClass="GRID">
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                        <ItemStyle CssClass="GRIDITEMCSS" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="LR No">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_LR_No" Text='<%#Eval("gcno")%>' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="gcdate" HeaderText="LR DATE"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="BkgBranch" HeaderText="Booking Branch"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DlyBranch" HeaderText="Delivery Branch"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Consignee Name">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_Consignee" Text='<%#Eval("Consignee_Name")%>' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Total_Articles" HeaderText="Qty"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Packing" HeaderText="Packing"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Descrp" HeaderText="Desc"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Current Status">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_CurrentStatus" Text='<%#Eval("CurrentStatus")%>' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>                                            
                                            <asp:BoundColumn DataField="CurrentBranch" HeaderText="Current Branch"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="IDs" Visible="false">
                                                <ItemTemplate>
                                                    <asp:HiddenField ID="hdn_GC_Id" Value='<%#Eval("GC_Id")%>' runat="server"></asp:HiddenField>
                                                    <asp:HiddenField ID="hdn_Consignee_Client_ID" Value='<%#Eval("Consignee_Client_ID")%>' runat="server"></asp:HiddenField>
                                                    <asp:HiddenField ID="hdn_Is_Consignee_Regular_Client" Value='<%#Eval("Is_Consignee_Regular_Client")%>' runat="server"></asp:HiddenField>
                                                    <asp:HiddenField ID="hdn_Status_ID" Value='<%#Eval("Status_ID")%>' runat="server"></asp:HiddenField>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>                                        
                                    </asp:DataGrid>
                                </asp:Panel>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="WucSelectedItems1" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                </tr>
               
               
                <tr>
                    <td colspan="6">
                         <asp:Label ID="lbl_Error" runat="server" Text="" CssClass="LABELERROR"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <script type="text/javascript">
    self.parent.hideload()
    </script>

</body>
</html>
