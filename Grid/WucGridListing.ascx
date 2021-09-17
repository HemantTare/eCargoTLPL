<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucGridListing.ascx.cs" Inherits="Grid_WucGridListing" %>

<%@ Register Src="../CommonControls/WucLinkName.ascx" TagName="WucLinkName" TagPrefix="uc2" %>

<%@ Register Src="../CommonControls/WucAPPSearch.ascx" TagName="WucSearch" TagPrefix="uc1" %>


    <script type="text/javascript">

    function Open_Popup_Window(Path)
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
    
    function Show_Ledger_Book(MenuItemId,Mode,Is_Consol,Hierarchy_Code,Main_Id,Ledger_Id,Ledger_Name)
    {
        var Path='<%= BaseUrl %>' + '/Finance/Reports/FrmLedgerMonthly.aspx?Menu_Item_Id=' + MenuItemId + '&Mode=' + Mode + '&Is_Consol=' + Is_Consol + '&Hierarchy_Code=' + Hierarchy_Code + '&Main_Id=' + Main_Id + '&Id=' + Ledger_Id + '&Name=' + Ledger_Name;
        location.href(Path)
       return false; 
       
    }
    </script>



        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        
        <tr>
            <td style="width: 50%">
                <uc2:WucLinkName ID="Link" runat="server" />
            </td>
                
            <td style="width: 50%; text-align: right;">
                <uc1:WucSearch ID="Search" runat="server" /></td>
        </tr>
        
        <tr>
                <td colspan="2" style="font-size: xx-small; vertical-align: middle; width: 100%">
                    &nbsp;

                </td>
        </tr>
    </table>
    
    
    <table cellpadding="0" cellspacing="0" style="width: 100%">
        
        <tr>
            <td style="width:100%;">                       
           
            <asp:DataGrid ID="dg_Grid" runat="server"  
                            AllowPaging="True" CssClass="GRID"  
                            PageSize="15" AllowSorting="True" AutoGenerateColumns="False" 
                            
                            OnPageIndexChanged="Grid_PageIndexChanged" 
                            OnSortCommand="Grid_SortCommand" OnItemDataBound="Grid_ItemDataBound">
                            
            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
            <HeaderStyle CssClass="GRIDHEADERCSS" />
            <FooterStyle CssClass="GRIDFOOTERCSS" />
            <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
            
        <Columns>

                   <asp:TemplateColumn HeaderText="A" SortExpression = "col1" Visible="false" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Eval("Col1")%>' ></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="B" SortExpression = "col2"   >
                        <ItemTemplate>
                            <asp:Label ID="lbl_Name" runat="server" Text='<%#Eval("Col2")%>' ></asp:Label>
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="C" SortExpression = "col3"   >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Col3")%>
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="D" SortExpression = "col4"   >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Col4")%>
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="E" SortExpression = "col5"   >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Col5")%>
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="F" SortExpression = "col6"   >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Col6")%>
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="G" SortExpression = "col7"   >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Col7")%>
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="H" SortExpression = "col8"   >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Col8")%>
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="I" SortExpression = "col9"   >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Col9")%>
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="J" SortExpression = "col10"   >
                        <ItemTemplate>
                            <%#DataBinder.Eval(Container.DataItem, "Col10")%>
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn> 
                                       
                    
                   
        </Columns>           
        </asp:DataGrid>
        <asp:Panel ID="pnl_Total" runat="server" Width="100%">
        <table style="width:100%;">
        
       <tr  style="width: 90%">
       <td style="width: 58%"></td>  
        <td align="right">
         &nbsp<asp:Label Id="lbl_OpeningBalance" runat="server" CssClass="LABEL" Font-Bold="true" Text="TOTAL:"></asp:Label> 
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label Id="lbl_ClosingBalance" runat="server" CssClass="LABEL" Font-Bold="true" Text="TOTAL:"></asp:Label> 
         </td>
        
    </tr>
</table>
</asp:Panel>
        
                
          </td>
       </tr>                    
      </table> 
      

      
      <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        
        <tr>
                <td style="font-size: xx-small; vertical-align: middle; width: 100%" colspan="2">
                    &nbsp;
                </td>
        </tr>
        
        <tr>
           <td style="width: 85%" align="right">
           <asp:Label ID="Lbl_Total_Records" runat="server"  CssClass="LABEL"  Font-Bold="True" EnableViewState="False"></asp:Label>&nbsp;
           </td>     
          
        </tr>
        
        
        <tr>
            <td colspan="2">

                    
                    <asp:HiddenField ID="hdn_Sort_Dir" runat="Server" />
                    <asp:HiddenField ID="hdn_Sort_Expression" runat="Server" />
                        
                        
            </td>
        </tr>           
        
    </table>      
        