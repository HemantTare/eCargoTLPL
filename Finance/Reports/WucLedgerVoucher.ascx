<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLedgerVoucher.ascx.cs" Inherits="Finance_Reports_WucLedgerVoucher" %>
<%@ Register Src="../../CommonControls/WucGridSearch.ascx" TagName="WucGridSearch"
    TagPrefix="uc2" %>
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
    function Open_Popup_Window(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w-100, popH = h-150;
        var leftPos = (w-popW)/2, topPos = (h-popH)/2;
        
        window.open (Path,'CustomPopUp','width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', menubar=no, resizable=yes,scrollbars=yes');
        return false;    
    }
    
    function ViewReport()
    {
    
        var Path='../../Finance/Reports/Rpt_Viewer/frm_Ledger_Voucher_View.aspx'
       var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w-100, popH = h-150;
        var leftPos = (w-popW)/2, topPos = (h-popH)/2;
        
        window.open (Path,'CustomPopUp','width=' + popW + ', height=' + popH + ',top=' + topPos + ',left=' + leftPos + ', menubar=no, resizable=yes,scrollbars=yes');
        return false;
    }
    </script>
      
<table  class="TABLE"   style="width: 100%" border="0" cellpadding="0" cellspacing="0">
        <tr >
            <td    class="TDGRADIENT" colspan="1" style="width: 44%;height:20% ">
                 &nbsp;<asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="LEDGER VOUCHER" Font-Bold="True" />
             
                 
                 </td>
                 <td class="TDGRADIENT" colspan="2" style="width: 22%; "  align="left" >
           <asp:Button ID="btn_RecoStatement"   runat="server" Text="RECONCILE STATEMENT" Height="20px" CssClass="BUTTON" Width="98%"/>
        </td>
                      <td class="TDGRADIENT" colspan="2" style="width: 12%; "  align="right"  >
           <asp:Button ID="btn_Reconcilliation"   runat="server" Width="98%" Text="RECONCILE" CssClass="BUTTON" Height="20px"/>
        </td>
         <td class="TDGRADIENT" style="width: 22%; "    align="right" >
           <asp:Button ID="btn_ReconcileBankStatement"   Width="98%" runat="server" Text="RECONCILE BANK STATEMENT" CssClass="BUTTON" Height="20px"/>
        </td>
        </tr>
        <tr>
        <td colspan="6">
            &nbsp;
        </td>
        </tr>
         <tr>
          <td  align="left"   colspan="2"  >
            <asp:Label ID="lbl_Ledger_Name" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label> 
            </td>
         <td colspan="2">
            <asp:Button ID="btn_Show_Preview" runat="server" CssClass="BUTTON" 
                 OnClientClick="return ViewReport();" Text="PREVIEW REPORT" />
   </td>   
   <td colspan="2">
                 <asp:UpdatePanel runat="server" ID="UpdatePanel2">
   <ContentTemplate>
         &nbsp;&nbsp;<asp:Button ID="Imgbtn_Expand"   runat="server" CssClass="BUTTON" OnClick="Imgbtn_Expand_Click"/>
          </ContentTemplate>
   <Triggers>
   <asp:AsyncPostBackTrigger  ControlID="Imgbtn_Expand"/>
   </Triggers>
   </asp:UpdatePanel>  
   </td>   
        </tr>
        
         <tr>
       
    </tr>
    
    <tr>
        <td colspan="6" width="100%">        
        <table width="100%">
            <tr>
                <td width="100%">
                    <uc2:WucGridSearch ID="WucGridSearch1" runat="server" />    
                </td>
            </tr>
        </table> 
            
           
        </td>
    </tr>
    
    <tr>
        <td colspan="6">
         </td>
    </tr>
     <tr><td colspan="6" >
   <asp:UpdatePanel runat="server" ID="upnl_DataGrid">
   <ContentTemplate>
   <asp:DataGrid ID="dgLedgerVoucher" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
    CellPadding="2" CssClass="Grid" PageSize="20" Width="100%" OnItemDataBound="dgLedgerVoucher_ItemDataBound" OnPageIndexChanged="dgLedgerVoucher_PageIndexChanged">
  <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
            <HeaderStyle CssClass="GRIDHEADERCSS" />
    <FooterStyle CssClass="GRIDFOOTERTB" /> 
   <PagerStyle Mode="NumericPages" />
    
    <Columns>
        <asp:BoundColumn Visible ="False" DataField="Voucher_Id"> </asp:BoundColumn>
        <asp:BoundColumn Visible ="False" DataField="Voucher_Type_Id"> </asp:BoundColumn>

       
        <asp:TemplateColumn HeaderText="Date" ItemStyle-VerticalAlign="Top">
        <HeaderStyle Width = "13%"/>
            <ItemTemplate>
             <asp:Label runat="server" ID = "lbl_Date">   <%#DataBinder.Eval(Container.DataItem, "Date")%></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:TemplateColumn HeaderText="Particulars" ItemStyle-VerticalAlign="Top">
        <HeaderStyle Width = "40%"/>
            <ItemTemplate >
              <asp:LinkButton ID="lbtn_View" Text='<%#DataBinder.Eval(Container.DataItem, "Particulars")%>' runat="server" Font-Underline ="false" ForeColor ="black" >  </asp:LinkButton><br />
              
    <asp:DataGrid ID="dg_VoucherDetails" runat="server" AutoGenerateColumns="False"
     Width="100%" BorderWidth="0">    
<%--    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
--%>    <HeaderStyle CssClass="HIDEGRIDCOL" />
<%--    <FooterStyle CssClass="GRIDFOOTERTB"/> 
--%>    <Columns>
    <asp:TemplateColumn>
        <HeaderStyle Width = "70%"/>
            <ItemTemplate>
            <asp:Label  Font-Bold="true" Font-Italic="true" runat="server" ID = "lbl_LedgerName"><%#DataBinder.Eval(Container.DataItem, "Ledger_Name")%></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:TemplateColumn HeaderText="" >
        <HeaderStyle Width = "30%"/>
            <ItemTemplate>
            <asp:Label Font-Bold="true"  Font-Italic="true" runat="server" ID = "lbl_LedgerAmount"><%#DataBinder.Eval(Container.DataItem, "Ledger_Amount")%></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        
    </Columns>
   
   </asp:DataGrid>

<asp:Label ID ="lbl_Narration" runat="server" ForeColor ="Red"> <%#DataBinder.Eval(Container.DataItem, "Narration")%> </asp:Label>
        
</ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:TemplateColumn HeaderText="Voucher Type" ItemStyle-VerticalAlign="Top">
        <HeaderStyle Width = "14%"/>
            <ItemTemplate>
            <asp:Label runat="server" ID = "lbl_Voucher_Type"><%#DataBinder.Eval(Container.DataItem, "Voucher_Type")%></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:TemplateColumn HeaderText="Voucher No" ItemStyle-VerticalAlign="Top">
        <HeaderStyle Width = "13%"/>
            <ItemTemplate>
             <asp:Label runat="server" ID = "lbl_Voucher_No"><%#DataBinder.Eval(Container.DataItem, "Voucher_No")%></asp:Label>
            </ItemTemplate>
        </asp:TemplateColumn>
        
        <asp:TemplateColumn HeaderText="Debit" ItemStyle-VerticalAlign="Top">
        <HeaderStyle Width = "10%" HorizontalAlign="Right"/>
            <ItemTemplate>
               <asp:Label runat="server" ID = "lbl_Debit"><%#DataBinder.Eval(Container.DataItem, "Debit")%></asp:Label> 
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Right" />
        </asp:TemplateColumn>
        
        <asp:TemplateColumn HeaderText="Credit" ItemStyle-VerticalAlign="Top">
        <HeaderStyle Width = "10%" HorizontalAlign="Right"/>
            <ItemTemplate>
                <asp:Label runat="server" ID = "lbl_Credit"><%#DataBinder.Eval(Container.DataItem, "Credit")%></asp:Label>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Right" />
        </asp:TemplateColumn>
    </Columns>
</asp:DataGrid>
   </ContentTemplate>
   <Triggers>
   <asp:AsyncPostBackTrigger  ControlID="Imgbtn_Expand"/>
   <asp:AsyncPostBackTrigger  ControlID="WucGridSearch1"/>
   </Triggers>
   </asp:UpdatePanel>     

    </td>
    </tr>
   
   <tr>
   <td colspan="6">
   <asp:UpdatePanel runat="server" ID="upnl_Totals">
   <ContentTemplate>
   <table  class="TABLE">
    <tr>
        <td style="width: 36%" colspan="1">&nbsp;</td>
        <td class="TD1" colspan="2" style="font-weight: bold; width: 20%;">
        Opening Balance:</td>
        <td style="width: 22%;font-weight: bold;"  align="right">
            <asp:Label ID="lbl_opening_Balance_Debit" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>&nbsp;</td>
        <td style="width: 22%;font-weight: bold;"  align="right">
            <asp:Label ID="lbl_opening_Balance_Credit" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 36%;border-bottom: gray 2px solid; font-weight:bold ; " colspan="1">
            &nbsp;</td>
        <td class="TD1" colspan="2" style="font-weight: bold; border-bottom: gray 2px solid; width: 20%; ">
        Current Total:</td>
        <td style="width: 22%;border-bottom: gray 2px solid;font-weight: bold; " align="right">
            <asp:Label ID="lbl_Current_Total_Debit" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>&nbsp;</td>
        <td style="width: 22%;border-bottom: gray 2px solid;font-weight: bold; "  align="right">
            <asp:Label ID="lbl_Current_Total_Credit" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 36%; font-weight: bold;" colspan="1">
        &nbsp;
        </td>
        <td class="TD1" colspan="2" style="font-weight: bold; width: 20%;">
            Closing Balance:</td>
        <td id="TD_CL_Dr" runat ="server" style="width: 22%; font-weight: bold; " align="right">
            <asp:Label ID="lbl_Closing_Balance_Debit" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>&nbsp;</td>
        <td id="TD_CL_Cr" runat="server" style="width: 22%; font-weight: bold;"  align="right">
            <asp:Label ID="lbl_Closing_Balance_Credit" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>&nbsp;</td>
    </tr>
    <tr>
        <td align="center" colspan="6">
            </td>
    </tr>
       
   </table>
 </ContentTemplate>
   <Triggers>
   <asp:AsyncPostBackTrigger  ControlID="Imgbtn_Expand"/>
   <asp:AsyncPostBackTrigger  ControlID="WucGridSearch1"/>
   </Triggers>
   </asp:UpdatePanel>     
   </td>
   </tr>
<tr>
        <td colspan="6">
        <br runat="server"  id="br_grid"/>

            <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR"></asp:Label></td>
    </tr>
    
</table>
 <asp:UpdateProgress ID="UpdateProgress1" runat="server">
  <ProgressTemplate>
    <div style="position: absolute; bottom:50%; left:50%; font-size: 11px; font-family: Verdana; z-index:100">
	<span id="ajaxloading">            
	<table>
	  <tr>
	    <td><asp:image ID="Ajax_Image_ID" runat="server" ImageUrl="~/images/ajax-loader-Squares.gif" /></td>
	  </tr>
	  <tr>
	    <td align="center" >Wait! Action in Progress...</td>
	  </tr>
	</table>
	</span>
    </div>
  </ProgressTemplate>
 </asp:UpdateProgress>
    