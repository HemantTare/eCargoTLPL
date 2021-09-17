<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucAdminGrid.ascx.cs" Inherits="GridWucAdminGrid" %>


<%@ Register Src="../CommonControls/WucLinkName.ascx" TagName="WucLinkName" TagPrefix="uc2" %>

<%@ Register Src="../CommonControls/WucSearch.ascx" TagName="WucSearch" TagPrefix="uc1" %>


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
    function Open_Popup_Window1(Path1)
    {

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-300);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
        window.open(Path1, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
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
                            OnSortCommand="Grid_SortCommand" OnEditCommand="Grid_EditCommand" OnItemDataBound="Grid_ItemDataBound">
                            
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
                    
                    
                    <asp:TemplateColumn HeaderText="View">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_View" runat="server" Text="View"></asp:LinkButton>    
                        </ItemTemplate>
                    </asp:TemplateColumn>                    
                    
                    
                    <asp:ButtonColumn CommandName="Edit" HeaderText="Edit" Text="Edit"></asp:ButtonColumn>
                    <asp:TemplateColumn HeaderText="Delete">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_Delete" runat="server" Text="Delete"></asp:LinkButton>    
                        </ItemTemplate>
                    </asp:TemplateColumn>
 
        </Columns>           
        </asp:DataGrid>
        
        
                
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
     
            <td style="width: 15%" class="TD1">
                &nbsp;
                <asp:Button ID="btn_Add" runat="server" Text="Add New Record" CssClass="BUTTON" OnClick="btn_Add_Click" /></td>
        </tr>
        
        
        <tr>
            <td colspan="2">

                    
                    <asp:HiddenField ID="hdn_Sort_Dir" runat="Server" />
                    <asp:HiddenField ID="hdn_Sort_Expression" runat="Server" />
                        
                        
            </td>
        </tr>           
        
    </table>      
        