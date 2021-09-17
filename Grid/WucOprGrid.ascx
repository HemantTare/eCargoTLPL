<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucOprGrid.ascx.cs" Inherits="Grid_WucOprGrid" %>
<%@ Register Src="../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc3" %>
<%@ Register Src="../CommonControls/WucLinkName.ascx" TagName="WucLinkName" TagPrefix="uc2" %>
<%@ Register Src="../CommonControls/WucSearch.ascx" TagName="WucSearch" TagPrefix="uc1" %>

<script type="text/javascript" src="../JavaScript/Common.js"></script>

<script type="text/javascript">
 
 function BookingClosed_Window()
 {
  var lbl_Errors = document.getElementById('WucOprGrid1_lbl_Errors');  
  lbl_Errors.value = "Booking Closed";     
  alert(lbl_Errors.value); 
 }
 
    function On_Load()
    { 
        var hdn_MenuItemId = document.getElementById('WucOprGrid1_hdn_MenuItemId');       
        var tr_Document_Type_Details = document.getElementById('WucOprGrid1_tr_Document_Type_Details'); 
        
        if( hdn_MenuItemId.value == 30 ||  hdn_MenuItemId.value == 188 )
        {
            tr_Document_Type_Details.style.display='inline';
        }
        else
        {
           tr_Document_Type_Details.style.display='none';
        }
    }
    
    function Open_Add_Window(Path,Menuitem_Id)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
////        var popW = (w-100);
////        var popH = h-40;//(h-100);
////        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2;  
        if (Menuitem_Id == 278)
        {
            var popW = (w);
            var popH = (h-30);
            var leftPos = (w-popW)/2;
        }
        else
        {
            var popW = (w-100);
            var popH = h-40;//(h-100);
            var leftPos = (w-popW)/2;
        
        }
        
        window.open(Path, 'MainPopUp_Add_' + Menuitem_Id, 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }

    function Open_View_Window(Path,Menuitem_Id)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2;
                
        window.open(Path, 'MainPopUp_View_' + Menuitem_Id, 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    
    function Open_Edit_Window(Path,Menuitem_Id)
    {

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = h-40;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2;
                
        window.open(Path, 'MainPopUp_Edit_' + Menuitem_Id, 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    
    function Open_Cancel_Window(Path,Menuitem_Id)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
        window.open(Path, 'MainPopUp_Cancel_' + Menuitem_Id, 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
</script>

<asp:ScriptManager ID="scm_oprgrid" runat="server">
</asp:ScriptManager>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td colspan="4">
            <uc2:WucLinkName ID="Link" runat="server" />
        </td>
        <td  style="width: 6%">
        </td>
        <td style="width: 42%; text-align: right;">
            <asp:Button id="btn_ExportToExcel" onclick="btn_ExportToExcel_Click" runat="server" CssClass="BUTTON" Text="Export To Excel" ></asp:Button></td>
    </tr>
    <tr>
        <td style="width: 1%; vertical-align: middle;" class="TDTEXT">
            <asp:Label ID="lbl_From" runat="server" Text="From :"></asp:Label></td>
        <td style="width: 25%; vertical-align: middle;">
            <uc3:WucDatePicker ID="PickerFrom" runat="server"></uc3:WucDatePicker>
        </td>
        <td style="width: 1%; vertical-align: middle;" class="TDTEXT">
            <asp:Label ID="lbl_To" runat="server" Text="To :"></asp:Label>
        </td>
        <td style="width: 25%; vertical-align: middle;">
            <uc3:WucDatePicker ID="PickerTo" runat="server"></uc3:WucDatePicker>
        </td>
        <td style="width: 6%; vertical-align: middle;">
            &nbsp;</td>
        <td class="TD1" style="width: 42%; vertical-align: middle;">
            <uc1:WucSearch ID="Search" runat="server" />
        </td>
    </tr>
    <tr>
        <td colspan="2" style="font-size: xx-small; vertical-align: middle; width: 100%">
            &nbsp;
        </td>
    </tr>
</table>
<table cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100%;">
            <asp:UpdatePanel ID="UP_Grid" runat="server">
                <ContentTemplate>
                    <asp:DataGrid ID="dg_Grid" runat="server" AllowPaging="True" CssClass="GRID" PageSize="15"
                        AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                        OnSortCommand="dg_Grid_SortCommand" OnItemDataBound="dg_Grid_ItemDataBound">
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="A" SortExpression="col1" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#Eval("Col1")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="B" SortExpression="col2">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Col2")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="C" SortExpression="col3">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Col3")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="D" SortExpression="col4">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Col4")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="E" SortExpression="col5">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Col5")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="F" SortExpression="col6">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Col6")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="G" SortExpression="col7">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Col7")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="H" SortExpression="col8">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Col8")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="I" SortExpression="col9">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Col9")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="J" SortExpression="col10">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Col10")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="View">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_View" runat="server" Text="View"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_Edit" runat="server" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Cancel" Visible="false">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_Cancel" runat="server" Text="Cancel"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:Panel ID="pnl_Total" runat="server" Width="100%">
                <table style="width: 100%;">
                    <tr style="width: 90%">
                        <td style="width: 30%">
                        </td>
                        <td style="width: 40%">
                        </td>
                        <td align="right" style="text-align: left;width: 30%">
                            &nbsp<asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="true" Text="TOTAL:"></asp:Label>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="font-size: xx-small; vertical-align: middle; width: 100%" colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr id="tr_Document_Type_Details" runat="server">
        <td colspan="3" style="font-weight: bold; font-size: 11px; font-family: Verdana">
            <asp:Label ID="lbl_NotUpdated" runat="server" BorderStyle="Solid" BorderWidth="1px"
                CssClass="NOTUPDATEDLBL" Width="50px"></asp:Label>&nbsp; Reserved &nbsp;
            <asp:Label ID="lbl_Attached" runat="server" BorderStyle="Solid" BorderWidth="1px"
                CssClass="ATTACHED" Width="50px"></asp:Label>&nbsp; Attached &nbsp;
            <asp:Label ID="lbl_ReBooked" runat="server" BorderStyle="Solid" BorderWidth="1px"
                CssClass="REBOOK" Width="50px"></asp:Label>&nbsp; ReBooked
            <asp:HiddenField runat="server" ID="hdn_MenuItemId"></asp:HiddenField>
        </td>
    </tr>
    <tr>
        <td style="width: 50%">
            &nbsp;<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" />
                    <asp:HiddenField ID="hdn_Sort_Dir" runat="Server" />
                    <asp:HiddenField ID="hdn_Sort_Expression" runat="Server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 25%" align="right">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="Lbl_Total_Records" runat="server" CssClass="LABEL" Font-Bold="True"
                        EnableViewState="False"></asp:Label>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 15%" class="TD1">
            &nbsp;
            <asp:Button ID="btn_Add" runat="server" Text="Add New Record" CssClass="BUTTON" /></td>
    </tr>
    <tr>
        <td colspan="3">
            &nbsp;</td>
    </tr>
</table>

<script type="text/javascript" language="javascript">
   On_Load();
</script>

