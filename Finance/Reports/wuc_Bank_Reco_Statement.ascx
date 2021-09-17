<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wuc_Bank_Reco_Statement.ascx.cs" Inherits="FA_Common_Reports_wuc_Bank_Reco_Statement" %>
<%@ Register Src="../../CommonControls/WucStartEndDate.ascx" TagName="WucStartEndDate"
    TagPrefix="uc2" %>
<%@ Register Src="../../CommonControls/WucGridSearch.ascx" TagName="WucGridSearch"
    TagPrefix="uc1" %>
<script language="javascript" type="text/javascript">
    function hideload()
    {
        var loading = document.getElementById('loading');     
        loading.style.visibility = 'hidden'; 
    }
        
    function displayload()
    {
        var loading = document.getElementById('loading');     
        loading.style.visibility = 'visible'; 
        loading.style.position='absolute';
        loading.style.left=(document.body.clientWidth/2)-20+'px';
        loading.style.top=(document.body.clientHeight/2)-60+'px';
    }  
    
         function Open_Show_Window(Path)
    {            
        queryString = Path;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-190);
        var popH = (h-75);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;

        window.open(queryString, 'FMainPopUp1', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes') ;
    }
 
</script>





<asp:Panel ID="pnl_Main" runat="server">
<table  style=" width:100%" class="TABLE"  >
 
  <tr>
    <td  colspan="6" class="TDGRADIENT">
    &nbsp;<asp:Label ID="lbl_BankRecoStatementHeading" runat="server"  Text="BANK RECONCILATION STATEMENT" CssClass="HEADINGLABEL"></asp:Label>
    </td>
    </tr>
     

    <tr>
        <td colspan="6" style="text-align: center">
            &nbsp;<asp:Button ID="btn_Print" CssClass="BUTTON" runat="server" OnClick="btn_Print_Click" Text="Print Preview"
                Width="141px" /></td>
    </tr>
     
   <%-- <tr>
        <td style="width:40%" align="left">
           &nbsp; <asp:Label ID="lbl_LedgerName" runat="server" Font-Bold="true" ></asp:Label>
        </td>
        <td style="width:30%" colspan="2" align="right">
            <%--From-
            <asp:Label ID="lbl_Start_Date" runat="server" Font-Bold="true" Text="01-Apr-2008">
            </asp:Label>&nbsp; To &nbsp;<asp:Label
            ID="lbl_EndDate" runat="server" Font-Bold="true"></asp:Label>
            &nbsp;&nbsp;           
        </td>
    </tr>--%>
    
    <tr>
    <td style="width:40%" align="left">
           &nbsp; <asp:Label ID="lbl_LedgerName" runat="server" Font-Bold="true" ></asp:Label>
        </td>
         <td style="width:39%;" colspan="3"  align="right">
             <asp:Label ID="lbl_1" runat="server" Text="Balance As Per Company Books :" Font-Bold="true" ></asp:Label>
         </td>
         <td style="width:15%"  align="right" > 
             <asp:Label ID="lbl_BalanceAsPerCompany" runat="server" Font-Bold="true" ></asp:Label>
         </td> 
          <td style="width:6%">
          &nbsp;
         </td> 
     </tr>
    <tr>
        <td align="left" style="width: 40%">
            &nbsp;</td>
        <td align="right" colspan="3" style="width: 39%">
        </td>
        <td align="right" style="width: 15%">
        </td>
        <td style="width: 6%">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="6" style="height: 21px">
            <uc2:WucStartEndDate ID="WucStartEndDate1" runat="server" />
        </td>
    </tr>
    
   <tr>
     <td colspan="6" style="width:100%" align="left">
      <table cellpadding="5" cellspacing ="5" border="0" width="100%">
      
        <tr>
           <td>            
           <fieldset runat="server" id="fst"><legend>Add Amount Not Reflected In Bank Statement</legend>
              <table cellpadding="3" cellspacing ="3" border="0" width="100%">
              <tr>
        <td width="100%">
        <table>
            <tr>
                <td width="10%"> 
                <asp:Button ID="btn_ExportToExcel" CssClass="BUTTON" runat="server" Text="Export To Excel" OnClick="btn_ExportToExcel_Click" />
                </td>
                <td width="90%">
                    <uc1:WucGridSearch ID="WucGridSearch1" runat="server" />    
                </td>
            </tr>
        </table>
            
        
        </td>
      </tr>
                <tr>
                  <td> 
          <asp:DataGrid ID="dg_AddBankRecoStatement" runat="server" AutoGenerateColumns="False" PageSize="10"
              Width="100%" CssClass="GRID" PagerStyle-Mode="NumericPages" OnPageIndexChanged="dg_AddBankRecoStatement_PageIndexChanged" AllowPaging="True" >
            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
            <HeaderStyle CssClass="GRIDHEADERCSS" />
            <FooterStyle CssClass="GRIDFOOTERCSS" />   
                 <Columns>
                        <asp:TemplateColumn   Visible="False">
                            <ItemTemplate>
                                <asp:Label  ID="lbl_SrNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SrNo")%>'></asp:Label>
                            </ItemTemplate> 
                        </asp:TemplateColumn>                
        
                        <asp:BoundColumn  HeaderText="Date" ReadOnly="True" DataField="Voucher_Date1"  >
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn  HeaderText="Particulars" ReadOnly="True" DataField="Particulars"  >
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn  HeaderText="Voucher Type" ReadOnly="True" DataField="Voucher_Type"  >
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn  HeaderText="Credit" ReadOnly="True" DataField="Credit"  >
                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                        </asp:BoundColumn>
                 </Columns>
              <PagerStyle Mode="NumericPages" />
            </asp:DataGrid>
                    </td>
            </tr>
       </table>
       </fieldset> 
        </td>
      </tr>
  </table>
</td>
</tr>
    <tr>
         <td style="width:70%" colspan="4"  > &nbsp;</td> 
          <td style="width:6%" align="right">
          &nbsp;
             <asp:Label ID="lbl_AddAmmountNotReflectedInBank" Font-Bold="true" runat="server" ></asp:Label></td> 
     </tr>    
        
      <tr><td> &nbsp;</td></tr>
      
<tr>
     <td colspan="6" style="width:100%" align="left">
      <table cellpadding="5" cellspacing ="5" border="0" width="100%">
        <tr>
           <td>            
           <fieldset runat="server" id="Fieldset1"><legend>Less Amount Not Reflected In Bank Statement</legend>
              <table cellpadding="3" cellspacing ="3" border="0" width="100%">
              <tr>
                <td width="100%">
                <table width="100%">
                    <tr>
                        <td width="10%">
                        <asp:Button ID="btn_ExportToExcel1" CssClass="BUTTON" runat="server" Text="Export To Excel" OnClick="btn_ExportToExcel1_Click"  />
                        </td>
                        <td width="90%">
                            <uc1:WucGridSearch ID="WucGridSearch2" runat="server" />    
                        </td>
                     </tr>
                </table>
                    
                    
                </td>
              </tr>
                <tr>
                  <td> 
          <asp:DataGrid ID="dg_LessBankRecoStatement" runat="server" AutoGenerateColumns="False" PageSize="10"
              Width="100%" CssClass="GRID" PagerStyle-Mode="NumericPages" AllowPaging="True" OnPageIndexChanged="dg_LessBankRecoStatement_PageIndexChanged" >
            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
            <HeaderStyle CssClass="GRIDHEADERCSS" />
            <FooterStyle CssClass="GRIDFOOTERCSS" />   
                 <Columns>
                        <asp:TemplateColumn   Visible="False">
                            <ItemTemplate>
                                <asp:Label  ID="lbl_SrNo" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "SrNo")%>'></asp:Label>
                            </ItemTemplate> 
                        </asp:TemplateColumn>                
        
                        <asp:BoundColumn  HeaderText="Date" ReadOnly="True" DataField="Voucher_Date1"  >
                            <ItemStyle Width="10%" />
                        </asp:BoundColumn>
                        
                        <asp:BoundColumn  HeaderText="Particulars" ReadOnly="True" DataField="Particulars"  >
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn  HeaderText="Voucher Type" ReadOnly="True" DataField="Voucher_Type"  >
                            <ItemStyle Width="15%" />
                        </asp:BoundColumn>
                        <asp:BoundColumn  HeaderText="Debit" ReadOnly="True" DataField="Debit"  >
                            <ItemStyle HorizontalAlign="Right" Width="10%" />
                        </asp:BoundColumn>
                 </Columns>
              <PagerStyle Mode="NumericPages" />
            </asp:DataGrid>
                </td>
            </tr>
       </table>
       </fieldset> 
        </td>
      </tr>
  </table>
</td>
</tr> 

     <tr>
         <td style="width:70%" colspan="4"  > &nbsp;</td> 
          <td style="width:6%" align="right">
          &nbsp;
             <asp:Label ID="lbl_LessAmmountNotReflectedInBank" Font-Bold="true" runat="server" ></asp:Label></td> 
     </tr>       


<tr>
      <td style="width:70%" colspan="4"  align="right">
             <asp:Label ID="Label1" runat="server" Text="Balance as Per Bank Statement: " Font-Bold="true" ></asp:Label>
         </td>
         <td style="width:15%"   align="right" > 
             <asp:Label ID="lbl_BalanceAsPerBank" runat="server" Font-Bold="true" EnableViewState="true" ></asp:Label>
         </td> 
         <td style="width:6%">
            &nbsp;
         </td> 
 </tr>
 <tr>
 <td>
 </td>
 </tr>
 
        
</table>
</asp:Panel>


