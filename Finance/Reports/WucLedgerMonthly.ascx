<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLedgerMonthly.ascx.cs" Inherits="Finance_Reports_WucLedgerMonthly" %>
  <script type="text/javascript">

   function ViewReport()
    {
        var Path='../../Finance/Reports/Rpt_Viewer/frm_Ledger_Monthly_View.aspx'
         var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w-100, popH = h-150;
        var leftPos = (w-popW)/2, topPos = (h-popH)/2;
        
        window.open (Path,'CustomPopUp','width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', menubar=no, resizable=no,scrollbars=yes');
        return false;
    }

</script>
   <table  class="TABLE" style="width: 100%">
        <tr>
            <td colspan="7"  class="TDGRADIENT">
                 &nbsp;<asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="LEDGER MONTHLY" Font-Bold="True" /></td>
        </tr>
        <tr>
        <td colspan="7"></td>
        </tr>
         <tr>
        <td colspan="7"></td>
        </tr>
    
    <tr>
        <td  rowspan="7" style="font-weight: bold; border-bottom: gray 1px solid; font-size:medium;  ">
            &nbsp;Particulars<br />
            &nbsp;<asp:Label ID="lbl_Ledger_Name" runat="server" CssClass="LABEL" Font-Bold="true" ></asp:Label></td>
        <td align="center" colspan="3" rowspan="3" style="border-bottom: gray 1px solid;border-left: gray 1px solid;">
            <br />
            <asp:Label ID="lbl_Company_Name" runat="server" CssClass="LABEL" Font-Bold="True" ForeColor="Maroon"></asp:Label><br />
            <asp:Label ID="lbl_From_date" runat="server" CssClass="LABEL" Font-Bold="False"></asp:Label>
            -
            To -
            <asp:Label ID="lbl_To_date" runat="server" CssClass="LABEL" Font-Bold="False"></asp:Label></td>
    </tr>
    <tr>
    </tr>
    <tr>
    </tr>
    <tr>
        <td align="center" colspan="2" rowspan="3" style="font-weight: bold; border-left: gray 1px solid;
            border-Right: gray 1px solid;border-bottom: gray 1px solid; height: 20px;">
            &nbsp;Transaction&nbsp;</td>
        <td align="center" rowspan="4" style="border-right: gray 1px solid; font-weight: bold;
            border-left: gray 1px solid; width: 20%; border-bottom: gray 1px solid">
            <strong>
            Closing</strong>
            <strong>Balance</strong>&nbsp;</td>
    </tr>
    <tr>
    </tr>
    <tr>
    </tr>
    <tr>
        <td align="center" style="width: 20%; font-weight: bold; border-bottom: gray 1px solid;border-left: gray 1px solid;border-right: gray 1px solid; height: 20px;">
            Debit</td>
        <td align="center" style="width: 20%; font-weight: bold; border-bottom: gray 1px solid; height: 20px;">
            Credit</td>
    </tr>
    <tr>
        <td style="width: 40%;">
            &nbsp;</td>
        <td style="width: 20%;border-Right: gray 1px solid;">
            </td>
        <td style="width: 20%; font-weight: bold; border-bottom: gray 1px solid;border-Right: gray 1px solid;">
        </td>
        <td style="width: 20%;border-Right: gray 1px solid;" align="right">
        </td>
    </tr>
    
    <tr>
         
        
        <td style="border-bottom: gray 1px solid; color:Gray; font-weight: bold;" colspan="4">
        <asp:DataGrid ID="dgLedgerMonthly" runat="server" AutoGenerateColumns="False" CssClass="GRID" OnItemDataBound="dgLedgerMonthly_ItemDataBound" >
        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
            <HeaderStyle CssClass="GRIDHEADERCSS" />
         <FooterStyle CssClass="GRIDFOOTERTB" HorizontalAlign="Right" />
        <Columns>
           
           <asp:TemplateColumn  HeaderStyle-HorizontalAlign="Left">
                 <ItemTemplate>                 
                 &nbsp;&nbsp;<asp:HyperLink ID="lnk_Month_Name" runat="Server" 
                  NavigateUrl='<%# "FrmLedgerVoucher.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + ClassLibraryMVP.Util.EncryptInteger(Mode) + "&Is_Consol=" + Is_Consol + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id + "&Id=" +Request.QueryString["Id"] +"&Name=" + Request.QueryString["Name"] +"&StartDate=" + Eval("Start_Date") + "&EndDate=" +Eval("End_Date") %>'
                  Text='<%#Eval("Month_Name")%>'
                 CssClass ="LINKREPORTS"></asp:HyperLink>
                 </ItemTemplate>
                <HeaderStyle Width="39%" />
           </asp:TemplateColumn>
           <asp:TemplateColumn>
                 <ItemTemplate>
                 <%#Eval("Debit")%>&nbsp;&nbsp; 
                 </ItemTemplate>
                <HeaderStyle Width="20%"/>
                <ItemStyle  HorizontalAlign="right"/>
           </asp:TemplateColumn>
           <asp:TemplateColumn>
                 <ItemTemplate>
                 <%#Eval("Credit")%>&nbsp;&nbsp;
                 </ItemTemplate>
                <HeaderStyle Width="20%"/>
                <ItemStyle  HorizontalAlign="right"/>
           </asp:TemplateColumn>
           
           <asp:TemplateColumn>
                 <ItemTemplate>
                 <%#Eval("Closing_Balance")%>&nbsp;&nbsp;
                 </ItemTemplate>
                <HeaderStyle Width="20%" HorizontalAlign="Right"/>
                <ItemStyle  HorizontalAlign="right"/>
           </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="id" Visible="false">
                <ItemTemplate>
                    <%#Request.QueryString["value1"] %>
                </ItemTemplate>
            </asp:TemplateColumn>         
     <asp:BoundColumn HeaderText="StDt" DataField="Start_Date" Visible="false"></asp:BoundColumn>
     <asp:BoundColumn HeaderText="EdDt" DataField="End_Date" Visible="false"></asp:BoundColumn>
        </Columns>
        </asp:DataGrid>
        </td>
         </tr>
    <tr>
        <td style="width: 40%; border-bottom: gray 1px solid">
            &nbsp;</td>
        <td align="center" style="width: 20%; border-bottom: gray 1px solid">
            &nbsp;</td>
        <td align="center" style="width: 20%; border-bottom: gray 1px solid; font-weight: bold;">
            &nbsp;</td>
        <td align="center" style="width: 20%;border-bottom: gray 1px solid; font-weight: bold;">
            &nbsp;</td>
    </tr>
     
     <tr>
     <td style="font-weight: bold; width: 40%; border-bottom: gray 1px solid">
            &nbsp;&nbsp;Grand Total</td>
     <td align="Right" style="width: 20%; border-bottom: gray 1px solid; font-weight: bold;">
     <asp:Label ID="lbl_Total_Debit" runat ="server" Text='<%=Total_Debit%>'></asp:Label>
            &nbsp;&nbsp;</td>
          
       <td align="Right" style="width: 20%; border-bottom: gray 1px solid; font-weight: bold;">
        <asp:Label ID="lbl_Total_Credit" runat ="server" Text='<%=Total_Credit%>'></asp:Label>
           &nbsp;&nbsp;</td>
                     
        <td align="Right" style="width: 20%;border-bottom: gray 1px solid; font-weight: bold;">
        <asp:Label ID="lbl_Closing_Balance" runat ="server" Text='<%=Closing_Balance%>'></asp:Label>
        &nbsp;&nbsp;</td>
          
        </tr> 
       <tr>
           <td style="font-weight: bold; width: 40%; border-bottom: gray 1px solid">
           </td>
           <td align="right" style="font-weight: bold; width: 20%; border-bottom: gray 1px solid">
               <asp:Button ID="btn_Show_Preview" runat="server" CssClass="BUTTON" OnClientClick="return ViewReport();"
                   Text="PREVIEW REPORT" /></td>
           <td align="right" style="font-weight: bold; width: 20%; border-bottom: gray 1px solid">
           </td>
           <td align="right" style="font-weight: bold; width: 20%; border-bottom: gray 1px solid">
           </td>
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
