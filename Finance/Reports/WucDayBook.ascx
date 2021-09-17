<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDayBook.ascx.cs" Inherits="Finance_Reports_WucDayBook" %>
<%@ Register Src="../../CommonControls/WucGridSearch.ascx" TagName="WucGridSearch"
    TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel" TagPrefix="uc1" %>

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
    
    function Open_Popup_Window(Path)  //added by Ankit champaneriya: 29-12-08 : 6.15 pm
    {
        //var Path='../Accounting Vouchers/FrmVoucher.aspx'
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w-100, popH = h-150;
        var leftPos = (w-popW)/2, topPos = (h-popH)/2;
        
        window.open (Path,'CustomPopUp','width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', menubar=no, resizable=yes,scrollbars=yes');
        return false;    
    }


    </script>
      
<table  class="TABLE" style="width: 100%">
        <tr>
            <td colspan="7"  class="TDGRADIENT">
                 &nbsp;<asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="DAY BOOK" Font-Bold="True" /></td>
        </tr>
        <tr>
        <td colspan="7"></td>
        </tr>
         <tr>
        <td colspan="7"></td>
        </tr>
        
         <tr>
        <td colspan="4" style="height: 5px" align="left">
      <%--  <uc1:WucStartEndDate ID="WucStartEndDate" runat="server" />--%>
            <uc2:WucGridSearch ID="WucGridSearch1" runat="server" />
      
        </td>
    </tr>
    
    <tr>
        <td colspan="7">
        </td>
    </tr>
    
    <tr>
        <td colspan="7">
            <uc1:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server"/>
        </td>
    </tr>
     <tr>
              
        <td style="color:Gray; font-weight: bold;" colspan="4;">
       <asp:DataGrid ID="DG_Monthly" runat="server" AutoGenerateColumns="False" 
           CssClass="GRID" PageSize="15" AllowSorting="True" OnSortCommand="DG_Monthly_SortCommand" 
           ShowFooter="true" AllowPaging="True" OnPageIndexChanged="DG_Monthly_PageIndexChanged" 
           OnItemDataBound="DG_Monthly_ItemDataBound"  >
          <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
          <HeaderStyle CssClass="GRIDHEADERCSS" />
          <FooterStyle CssClass="GRIDFOOTERCSS" />
          <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages"/>
        <Columns>
            <asp:BoundColumn HeaderText="Voucher_ID" Visible ="False" DataField="Voucher_ID" ReadOnly="True">
            </asp:BoundColumn>
            <asp:BoundColumn HeaderText="Voucher_Type_Id" Visible ="False" DataField="Voucher_Type_Id" ReadOnly="True">
            </asp:BoundColumn>
           <asp:TemplateColumn HeaderText="Date" SortExpression ="Voucher_Date" >
                 <ItemTemplate>
                 &nbsp;&nbsp<%#Eval("Voucher_Date")%>
                 </ItemTemplate>
                <HeaderStyle Width="15%" cssclass  = "SORTINGLNKBTN" />
                <ItemStyle Font-Bold ="False"/>
           </asp:TemplateColumn>
           <asp:TemplateColumn HeaderText ="Particulars" SortExpression ="Particulars">
                 <ItemTemplate>
                <%#Eval("Particular")%> &nbsp;&nbsp;
                 </ItemTemplate>
                <HeaderStyle Width="15%" cssclass  = "SORTINGLNKBTN"/>
                <ItemStyle Font-Bold ="False"/>
                <FooterTemplate>
                    <asp:Label ID="lbl_Total" Text="Total:" runat="server" CssClass="LABEL" Font-Bold="true"/>
                 </FooterTemplate>
           </asp:TemplateColumn>
           <asp:TemplateColumn HeaderText ="Voucher Type" SortExpression="Voucher_Name">
                 <ItemTemplate>
                 <%#Eval("Voucher_Name")%>&nbsp;&nbsp;
                 </ItemTemplate>
                <HeaderStyle Width="15%" cssclass  = "SORTINGLNKBTN"/>
                <ItemStyle Font-Bold ="False"/>
           </asp:TemplateColumn>
           
           <asp:TemplateColumn HeaderText ="Voucher No." SortExpression ="Voucher_No">
                 <ItemTemplate>
                 <%#Eval("Voucher_No")%>
                 </ItemTemplate>
                <HeaderStyle Width="15%" cssclass  = "SORTINGLNKBTN"/>
            <ItemStyle Font-Bold ="False"/>
           </asp:TemplateColumn>
      
           <asp:TemplateColumn  HeaderText ="Debit Amount"  SortExpression="Debit">
           <ItemTemplate>
                 <%#Eval("Debit")%>
           </ItemTemplate>
                <HeaderStyle Width="15%" cssclass  = "SORTINGLNKBTN" HorizontalAlign ="Right" />
                <ItemStyle HorizontalAlign ="Right" Font-Bold ="False" /> 
                <FooterTemplate>
                    <asp:Label ID="lbl_Total_Debit" runat="server" CssClass="LABEL" Font-Bold="true"/>
                 </FooterTemplate>                
                <FooterStyle HorizontalAlign="Right" />  
           </asp:TemplateColumn>
           <asp:TemplateColumn  HeaderText ="Credit Amount" SortExpression ="Credit">
           <ItemTemplate>
                 <%#Eval("Credit")%>&nbsp;&nbsp
                 </ItemTemplate>
                <HeaderStyle Width="15%" cssclass  = "SORTINGLNKBTN" HorizontalAlign ="Right"/>
                <ItemStyle HorizontalAlign ="Right" Font-Bold ="False"/>
                <FooterTemplate>
                    <asp:Label ID="lbl_Total_Credit" runat="server" CssClass="LABEL" Font-Bold="true"/>
                </FooterTemplate>                
                <FooterStyle HorizontalAlign="Right" />
           </asp:TemplateColumn>
      
        </Columns>
        </asp:DataGrid>&nbsp;&nbsp;
        </td>
         </tr>
     <tr>
        <td style="width: 40%;">
            &nbsp;<asp:HiddenField ID="hdn_Sort_Dir" runat="Server" />
         <asp:HiddenField ID="hdn_Sort_Expression" runat="Server" />
         </td>
        <td align="center" style="width: 20%;">
            &nbsp;</td>
        <td align="center" style="width: 20%; ">
            &nbsp;</td>
        <td align="center" style="width: 20%;">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="font-weight: bold; width: 40%; border-bottom: gray 1px solid">
            <asp:Label ID="lbl_Error" runat="server"></asp:Label></td>
        <td align="center" style="font-weight: bold; width: 20%; border-bottom: gray 1px solid">
        </td>
        <td align="center" style="font-weight: bold; width: 20%; border-bottom: gray 1px solid">
        </td>
        <td align="center" style="width: 20%">
        </td>
    </tr>     
 </table>

     