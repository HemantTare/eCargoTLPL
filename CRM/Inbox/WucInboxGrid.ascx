<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WucInboxGrid.ascx.vb" Inherits="WucInboxGrid" %>

<script type="text/javascript">

    function Show_Ticket_History(Url)
    {
     
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
          window.open(Url, 'CustomPopUp1', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
          return false;
    }
    
</script>



    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="width: 50%;">
            <asp:Image ID="Image1" runat="server" ImageUrl="~/images/red_block.gif" ImageAlign="Middle" />
                <asp:Label ID="lbl_Heading" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px"
                Text="BRANCH LIST"></asp:Label>
                <asp:Image ID="Image2" runat="server" ImageUrl="~/images/strip.gif" />
                &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;&nbsp;
                    
                </td>
            <td style="width: 50%; text-align: right;">
            <asp:DropDownList
                ID="ddl_Serach" runat="server" Font-Names="Verdana" Font-Size="11px">
            </asp:DropDownList>
                <asp:TextBox ID="txt_Search" runat="server" CssClass="TEXTBOXSEARCH" BorderWidth="1px"></asp:TextBox>
                <asp:Button
                ID="btn_Search" runat="server" Text="Search" CssClass="BUTTON" width="50px" Height="19px"/></td>
        </tr>
        <tr>
            <td style="width: 50%;">
                </td>
            <td style="width: 50%; text-align: right;">
                </td>
        </tr>
    </table>


<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="font-size: medium; color:ActiveBorder; vertical-align: middle; width: 100%">
            &nbsp;</td>
    </tr>
    <tr>
<td style="width: 100%">


<asp:DataGrid ID="Grid" runat="server" AllowPaging="True" CssClass="GRID" CellPadding="2" PageSize="15" AutoGenerateColumns="False" AllowSorting="True">
<AlternatingItemStyle CssClass ="GRIDALTERNATEROWCSS" />
<HeaderStyle CssClass="GRIDHEADERCSS" />
<FooterStyle CssClass="GRIDFOOTERCSS" />
<PagerStyle Mode="NumericPages"/>
    
    
    
    <Columns>
        <asp:TemplateColumn HeaderText="A" SortExpression = "col1">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%#Eval("Col1")%>' ></asp:Label>
            </ItemTemplate>
            <HeaderStyle  CssClass  = "SORTINGLNKBTN"  />
        </asp:TemplateColumn>
        
         <asp:TemplateColumn HeaderText="L" SortExpression = "col2">
            <ItemTemplate>
                <asp:Label ID="Label2" runat="server" Text='<%#Eval("Col2")%>' ></asp:Label>
            </ItemTemplate>
            <HeaderStyle  CssClass  = "SORTINGLNKBTN"  />
        </asp:TemplateColumn>
        
        <asp:TemplateColumn  SortExpression = "col3" HeaderImageUrl="~/images/Attachments.jpg">
            <ItemTemplate>
               <asp:Image ID="img_Attachment" runat="server" Visible='<%#Convert.ToBoolean(Eval("Col3"))%>' ImageUrl="~/images/Attachments.jpg" />
            </ItemTemplate>
             <HeaderStyle  CssClass  = "SORTINGLNKBTN"/>
        </asp:TemplateColumn>
        
        <asp:TemplateColumn HeaderText="B" SortExpression = "col4">
            <ItemTemplate>
                <%#DataBinder.Eval(Container.DataItem, "Col4")%>
            </ItemTemplate>
             <HeaderStyle  CssClass  = "SORTINGLNKBTN"  />
        </asp:TemplateColumn>

        <asp:TemplateColumn HeaderText="C" SortExpression = "col5">
            <ItemTemplate>
                <%#DataBinder.Eval(Container.DataItem, "Col5")%>
            </ItemTemplate>
             <HeaderStyle  CssClass  = "SORTINGLNKBTN"  />
        </asp:TemplateColumn>
        
        <asp:TemplateColumn HeaderText="D" SortExpression = "col6">
            <ItemTemplate>
                <%#DataBinder.Eval(Container.DataItem, "Col6")%>
            </ItemTemplate>
             <HeaderStyle  CssClass  = "SORTINGLNKBTN"  />
        </asp:TemplateColumn>

        <asp:TemplateColumn HeaderText="E" SortExpression = "col7">
            <ItemTemplate>
                <%#DataBinder.Eval(Container.DataItem, "Col7")%>
            </ItemTemplate>
             <HeaderStyle  CssClass  = "SORTINGLNKBTN"  />
        </asp:TemplateColumn>
        
        <asp:TemplateColumn HeaderText="F" SortExpression = "col8">
            <ItemTemplate>
                <%#DataBinder.Eval(Container.DataItem, "Col8")%>
            </ItemTemplate>
             <HeaderStyle  CssClass  = "SORTINGLNKBTN"  />
        </asp:TemplateColumn>
        
        <asp:TemplateColumn HeaderText="G" SortExpression = "col9">
            <ItemTemplate>
                <%#DataBinder.Eval(Container.DataItem, "Col9")%>
            </ItemTemplate>
             <HeaderStyle  CssClass  = "SORTINGLNKBTN"  />
        </asp:TemplateColumn>
        
        <asp:TemplateColumn HeaderText="H" SortExpression = "col10">
            <ItemTemplate>
                <%#DataBinder.Eval(Container.DataItem, "Col10")%>
            </ItemTemplate>
             <HeaderStyle  CssClass  = "SORTINGLNKBTN"  />
        </asp:TemplateColumn>
        
        <asp:TemplateColumn HeaderText="I" SortExpression = "col11">
            <ItemTemplate>
              <asp:Label ID="lbl_UserIds" runat="server" Text='<%#Eval("Col11")%>' ></asp:Label>
             </ItemTemplate>
             <HeaderStyle  CssClass  = "SORTINGLNKBTN"  />
        </asp:TemplateColumn>
        
      <asp:TemplateColumn HeaderText="L" SortExpression = "col12" Visible="false">
            <ItemTemplate>
                <asp:Label ID="lbl_GcId" runat="server" Text='<%#Eval("Col12")%>' ></asp:Label>
            </ItemTemplate>
            <HeaderStyle  CssClass  = "SORTINGLNKBTN"  />
        </asp:TemplateColumn>
                       
        <asp:TemplateColumn HeaderText="M" SortExpression = "col13">
            <ItemTemplate>
                <%#DataBinder.Eval(Container.DataItem, "Col13")%>
            </ItemTemplate>
             <HeaderStyle  CssClass  = "SORTINGLNKBTN"  />
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>

</td>
    </tr>
    <tr>
        <td style="width: 100%;">
            <table style="width: 100%">
                <tr>
                    <td style="width:10%">
                        <asp:Label ID="Label2" runat="server" BorderStyle="Solid" BorderWidth="1px" CssClass="LOWPRIORITY"
                            Width="100%"></asp:Label></td>
                    <td style="width: 10%" align="left" style="font-weight: bold; font-size: 11px; font-family: Verdana">
                        Low</td>
                    <td style="width: 10%">
                        <asp:Label ID="Label3" runat="server" BorderStyle="Solid" BorderWidth="1px" CssClass="MEDIUMPRIORITY"
                            Width="100%"></asp:Label></td>
                    <td style="width:10%" align="left" style="font-weight: bold; font-size: 11px; font-family: Verdana">
                        Medium</td>
                    <td style="width: 10%" >
                        <asp:Label ID="Label4" runat="server" BorderStyle="Solid" BorderWidth="1px" CssClass="HIGHPRIORITY"
                            Width="100%"></asp:Label></td>
                    <td style="width: 10%" align="left" style="font-weight: bold; font-size: 11px; font-family: Verdana">
                        High</td>
                    <td style="width:10%">
                        <asp:Label ID="Label5" runat="server" BorderStyle="Solid" BorderWidth="1px" CssClass="URGENTPRIORITY"
                           Width="100%"></asp:Label></td>
                    <td style="width:10%" align="left" style="font-weight: bold; font-size: 11px; font-family: Verdana">
                        Urgent</td>
                    <td style="width:10%">
                        <asp:Label ID="Label6" runat="server" BorderStyle="Solid" BorderWidth="1px" CssClass="CRITICALPRIORITY"
                            Width="100%"></asp:Label></td>
                    <td style="width:10%" align="left" style="font-weight: bold; font-size: 11px; font-family: Verdana">
                        Critical</td>
                </tr>
            </table>
        </td>
    </tr>
    
</table>

