<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucPrintingGrid.ascx.cs" Inherits="Printing_WucPrintingGrid" %>
<%@ Register Src="~/CommonControls/WucLinkName.ascx" TagName="WucLinkName" TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/WucSearch.ascx" TagName="WucSearch" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc3" %>

<script type="text/javascript" src="../Javascript/Common.js"></script>

<script type="text/javascript">
    
    function Open_View_Window(Path)
    {

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-40);
        var leftPos = (w-popW)/2;
        var topPos = 0;

      if(Path == '')
      {
        alert('You do not have rights for view !');
      }
      else
      {
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
      }
        return false;
    } 
    
    function Open_Print_Window(Path)
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
<asp:ScriptManager ID="scm_printgrid" runat="server"></asp:ScriptManager>
 <table border="0" cellpadding="0" cellspacing="0" style="width: 100%" >
        
    <tr>
        <td style="width: 100%;" colspan="2">
            <uc2:WucLinkName ID="Link" runat="server" />
        </td>
    </tr>
    <tr>

        <td style="width:1%;vertical-align:middle;" class="TDTEXT">
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
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="conditional">
            <ContentTemplate>
                <uc1:WucSearch ID="Search" runat="server" />
             </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />                 
            </Triggers>
        </asp:UpdatePanel>
        </td>
     </tr>     
     <tr>
         <td colspan="2" style="font-size: xx-small; vertical-align: middle; width: 100%">&nbsp;</td>
     </tr>
 </table>    
    
 <table class="TABLE">
    <tr>
        <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_DocumentType" runat="server" CssClass="LABEL" Font-Bold="true" Text="Select Document :"></asp:Label>
        </td>
        <td style="width: 29%;">
          <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="conditional">
            <ContentTemplate>
                <asp:DropDownList ID="ddl_DocumentType" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_DocumentType_SelectedIndexChanged"></asp:DropDownList>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />                 
            </Triggers>
        </asp:UpdatePanel>
        </td>
        <td style="width: 51%;"></td>
    </tr>
    <tr><td class="TD1" colspan="3">&nbsp;</td></tr> 
    <tr>  
         <td style="width:100%;" colspan="3">     
           <asp:UpdatePanel ID="UP_PrintGrid" runat="server">
            <ContentTemplate>      
            <asp:DataGrid ID="dg_PrintingGrid" runat="server" AllowPaging="True" CssClass="GRID" PageSize="15" 
            AllowSorting="True" AutoGenerateColumns="False" OnPageIndexChanged="Grid_PageIndexChanged" 
            OnSortCommand="Grid_SortCommand" OnItemDataBound="Grid_ItemDataBound">
                            
            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
            <HeaderStyle CssClass="GRIDHEADERCSS" />
            <FooterStyle CssClass="GRIDFOOTERCSS" />
            <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />            
            <Columns>
                   <asp:TemplateColumn HeaderText="A" SortExpression = "col1" Visible="False" >
                        <ItemTemplate>
                            <asp:Label ID="lbl_Id" runat="server" Text='<%#Eval("Col1")%>' ></asp:Label>                
                        </ItemTemplate>
                        <HeaderStyle  cssclass  = "SORTINGLNKBTN" HorizontalAlign="Left" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="B" SortExpression = "col2"   >
                        <ItemTemplate>
                            <asp:LinkButton ID="lbl_Name" runat="server" Text='<%#Eval("Col2")%>' ></asp:LinkButton>
                            <asp:HiddenField ID="hdn_Path" runat="server"></asp:HiddenField>
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
                    
                    <asp:TemplateColumn HeaderText="Print"> 
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk_Print" runat="server" Text="Print"></asp:LinkButton>    
                        </ItemTemplate>
                    </asp:TemplateColumn>
        </Columns>
        </asp:DataGrid>
          </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />
                <asp:AsyncPostBackTrigger ControlID="Search" />
                <asp:AsyncPostBackTrigger ControlID="dg_PrintingGrid" />
            </Triggers>
        </asp:UpdatePanel>
        </td>      
      </tr>
 </table> 

 <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">        
        <tr>
            <td style="font-size: xx-small; vertical-align: middle; width: 100%" colspan="2">&nbsp;</td>
        </tr>        
        <tr>
           <td style="width: 85%" align="right">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <asp:Label ID="Lbl_Total_Records" runat="server"  CssClass="LABEL"  Font-Bold="True" EnableViewState="False"></asp:Label>&nbsp;
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />   
                    <asp:AsyncPostBackTrigger ControlID="Search" />              
                </Triggers>
            </asp:UpdatePanel>
           </td>     
        </tr>        
        <tr>
            <td colspan="2">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <asp:HiddenField ID="hdn_Sort_Dir" runat="Server" />
                    <asp:HiddenField ID="hdn_Sort_Expression" runat="Server" />   
                    <asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR"/>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />   
                    <asp:AsyncPostBackTrigger ControlID="Search" />              
                </Triggers>
            </asp:UpdatePanel>
            </td>
        </tr>        
    </table>