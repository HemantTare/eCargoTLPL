<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucIBTVoucherGrid.ascx.cs" Inherits="Finance_IBT_WucVoucherForApprovalGrid" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc3" %>
<%@ Register Src="~/CommonControls/WucLinkName.ascx" TagName="WucLinkName" TagPrefix="uc2" %>


<script type="text/javascript" src="~/JavaScript/Common.js"></script>

<script type="text/javascript">

    function Open_Add_Window(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }

    function Open_View_Window(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    
    function Open_Edit_Window(Path)
    {

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    

</script>

<asp:ScriptManager ID="scm_grid" runat="server">
</asp:ScriptManager>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100%;" colspan="2">
            <uc2:WucLinkName ID="Link" runat="server" />
        </td>
    </tr>
    <tr>

        <td style="width:1%;vertical-align:middle;" class="TDTEXT" >
            <asp:Label ID="lbl_From" runat="server" Text="From :"></asp:Label>
         </td>
         
         <td style="width:29%;vertical-align:middle;">
            <uc3:WucDatePicker id="PickerFrom" runat="server"></uc3:WucDatePicker>
         </td>
        
        <td style="width:1%;vertical-align:middle;" class="TDTEXT">
            <asp:Label ID="lbl_To" runat="server" Text="To :"></asp:Label>
         </td>
         
        <td style="width:29%;vertical-align:middle;">
            <uc3:WucDatePicker id="PickerTo" runat="server"></uc3:WucDatePicker>
        </td>

        <td class="TD1" style="width:40%;vertical-align:Top;">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 100%">
            <asp:DropDownList ID="ddl_Search" runat="server" Font-Names="Verdana" Font-Size="11px">
            <asp:ListItem Text="Voucher Date" Value="Voucher_Date"></asp:ListItem>
            <asp:ListItem Text="Ledger Name" Value="Ledger_Name"></asp:ListItem>
            <asp:ListItem Text="Voucher No" Value="Voucher_No"></asp:ListItem>
            <asp:ListItem Text="Ref No" Value="Ref_No"></asp:ListItem>
            <asp:ListItem Text="Debit" Value="Debit"></asp:ListItem>
            <asp:ListItem Text="Credit" Value="Credit"></asp:ListItem>
            </asp:DropDownList>
        
            <asp:TextBox ID="txt_Search" runat="server" BorderWidth="1px" CssClass="TEXTBOXSEARCH" MaxLength="50" ></asp:TextBox>
            <asp:ImageButton ID="btn_Search" runat="server" ImageAlign="TextTop" ImageUrl="~/Images/Search.GIF" OnClick="btn_Search_Click" /></td>
    </tr>
</table>

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
                        AllowSorting="True" AutoGenerateColumns="false" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                        OnSortCommand="dg_Grid_SortCommand" OnItemDataBound="dg_Grid_ItemDataBound">
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                        <Columns>

                            <asp:TemplateColumn HeaderText="Voucher Id" SortExpression="Voucher_Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Id" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Voucher_Id")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                             <asp:TemplateColumn HeaderText="VoucherTypeId" SortExpression="Voucher_Id" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_VoucherTypeId" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Voucher_Type_Id")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Voucher Date" SortExpression="Voucher_Date">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Voucher_Date")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Ledger Name" SortExpression="Ledger_Name">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Ledger_Name")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Voucher No" SortExpression="Voucher_No">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Voucher_No")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Location" SortExpression="Location">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Location")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Ref No" SortExpression="Ref_No">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container.DataItem, "Ref_No")%>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Debit" SortExpression="Debit">
                                <ItemTemplate>
                                <asp:Label ID="lbl_Debit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Debit")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Credit" SortExpression="Credit">
                                <ItemTemplate>
                                 <asp:Label ID="lbl_Credit" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Credit")%>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle CssClass="SORTINGLNKBTN" HorizontalAlign="Left" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="View" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_View" runat="server" Text="View" ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Edit" >
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_Edit" runat="server" Text="Edit"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn >
                            <asp:TemplateColumn HeaderText="Cancel">
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
        </td>
    </tr>
</table>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="font-size: xx-small; vertical-align: middle; width: 100%" colspan="3">
            &nbsp;
        </td>
    </tr>
    <tr>
        <td style="width: 50%">
            &nbsp;<asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR"/>
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
                    <asp:Label ID="Lbl_Total_Records" runat="server" CssClass="LABEL"
                        Font-Bold="True" EnableViewState="False"></asp:Label>
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
