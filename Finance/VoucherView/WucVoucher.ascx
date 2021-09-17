<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVoucher.ascx.cs" Inherits="Finance_VoucherView_WucVoucher" %>

<script type = "text/javascript" src="../../Javascript/Common.js" ></script>   

<script type = "text/javascript" >
 
 function OpenPopup(Path)
    {

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-200);
        var popH = (h-200);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
        window.open(Path, 'MainPopUp1', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    
    function OpenSmallPopup(Path)
    {

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-500);
        var popH = (h-500);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
        window.open(Path, 'MainPopUp1', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    
    
    
</script>
      
        
        <table  style="width:100%;" class = "TABLE" width="100%">
        
            <tr id="tr_Heading" runat="server">
                <td class = "TDGRADIENT" colspan="6">
                    <asp:Label ID = "Label1" runat = "server" Text="VOUCHER VIEW" CssClass="HEADINGLABEL"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                </td>
            </tr>
            <tr>
                <td colspan="6">
                </td>
            </tr>
        
           
            <tr>
                 <td colspan = "2"  style="width:20%; text-align: center; background :silver; height: 21px;">
                    <asp:Label ID="lbl_VoucherType" runat="server"
                      Font-Bold ="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right">
                </td>
                <td style="width: 5%">
                </td>
                <td>
                </td>
                <td style="width: 15%; text-align: right">
                </td>
                <td style="width: 15%">
                </td>
            </tr>
            <tr>
                <td style="width: 10%; text-align: right">
                </td>
                <td style="width: 5%">
                </td>
                <td>
                </td>
                <td style="width: 15%; text-align: right">
                </td>
                <td style="width: 15%">
                </td>
            </tr>
            
            <tr>
               
                <td style="width: 10%;text-align: right;">
                  Voucher No. : 
                </td>
              
                <td style ="width:5%" align="left" >
                    <asp:Label ID="lbl_VoucherNo" runat="server" CssClass="LABEL" STYLE ="width:auto" Font-Bold="true"></asp:Label>
                </td>
                <td ></td> 
                <td style="width: 15%;text-align: right;" >
                    Date: </td>
                <td style="width: 15%" align="left">
                   <asp:Label ID="lbl_VoucherDate" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                </td>
                   
            </tr>
          
            <tr>
               
                <td style="width: 10%;text-align: right;">
                    Ref No :
                </td>
                <td style="width:15%" align="left">
                   <asp:Label ID="lbl_RefNo" runat="server" CssClass="LABEL" Font-Bold="true"></asp:Label>
                </td>
                <td style="width:20%">
                </td>
                <td style="width:15%">
                </td>
                <td style="width:15%">
                </td>
                
            </tr>
            <tr>
                <td style="width: 10%; text-align: right">
                </td>
                <td style="width: 15%">
                </td>
                <td style="width: 20%">
                </td>
                <td style="width: 15%">
                </td>
                <td style="width: 15%">
                </td>
            </tr>
           
            <tr>
                <td colspan="5" >
                <fieldset id="fld_Grid">
                <legend>Voucher Details :</legend>
                  <table cellpadding="3" cellspacing="3" border="0" width="100%">
                  <tr>
                   <td>
                   <asp:DataGrid ID = "dg_Voucher" runat = "server" style="width:100%" CssClass = "Grid"
                        AutoGenerateColumns="False" 
                        ShowFooter ="false" OnItemDataBound="dg_Voucher_ItemDataBound" 
                        >
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <PagerStyle Mode="NumericPages" CssClass="GRIDVIEWPAGERCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <Columns>
                        
                            <%--<***************************************************************************>
                            <********************************crdr***********************************>
                            <***************************************************************************>--%>
                            <asp:TemplateColumn HeaderText="CR/DR"  >
                                <HeaderStyle Width="5%" Font-Bold ="True"    />
                                <ItemTemplate>
                                    <asp:label ID = "lbl_CrDr" runat = "server"  CssClass = "DROPDOWN"
                                        style ="width:100%" Text = '<%#convertToDrCr(Eval("Debit"))%>'>
                                    </asp:label>
                                </ItemTemplate>
                                                     
                            </asp:TemplateColumn>
                            
                            <%--<***************************************************************************>
                            <********************************PARTICULARS ***********************************>
                            <***************************************************************************>--%>
                            <asp:TemplateColumn HeaderText="Particulars">
                                <HeaderStyle Width="35%"  Font-Bold ="True"  />
                                <ItemTemplate>
                                    <asp:label ID = "lbl_Ledger" runat = "server" style ="width:100%;" 
                                    text ='<%#DataBinder.Eval(Container.DataItem,"Ledger_Name")%>'></asp:label>
                                </ItemTemplate>
                                
                            </asp:TemplateColumn>
                           
                            <%--<***************************************************************************>
                            <********************************DEBIT ***********************************>
                            <***************************************************************************>--%>
                            <asp:TemplateColumn HeaderText="Debit">
                                <HeaderStyle Width="15%" HorizontalAlign ="Right" Font-Bold ="True"  />
                                <ItemStyle HorizontalAlign = "right" />
                                <ItemTemplate>
                                    <asp:label ID = "lbl_Debit" runat = "server" style ="width:100%;text-align:right;" CssClass = "TEXTBOXNOS"
                                    text ='<%#DataBinder.Eval(Container.DataItem,"Debit")%>'></asp:label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <%--<***************************************************************************>
                            <********************************CREDIT ***********************************>
                            <***************************************************************************>--%>
                            <asp:TemplateColumn HeaderText="Credit">
                                <HeaderStyle Width="15%" HorizontalAlign ="Right" Font-Bold ="True"   />
                                <ItemStyle HorizontalAlign = "right" />
                                <ItemTemplate >
                                    <asp:label ID = "lbl_Credit" runat = "server" style ="width:100%;text-align:right;" CssClass = "TEXTBOX" 
                                    text ='<%#DataBinder.Eval(Container.DataItem,"Credit")%>'></asp:label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <%--<***************************************************************************>
                            <********************************COST CENTRE***********************************>
                            <***************************************************************************>--%>
                            <asp:TemplateColumn HeaderText="CostCentre"  >
                                <HeaderStyle Width = "10%" Font-Bold = "true"  />
                                <ItemTemplate >
                                    <asp:LinkButton ID = "lnk_CostCentre"  runat = "server" Text = "CostCentre" Visible='<%#Convert.ToBoolean(Eval("IsCostCentre"))%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <%--<***************************************************************************>
                            <********************************BILL BY BILL***********************************>
                            <***************************************************************************>--%>
                            <asp:TemplateColumn HeaderText="BillByBill"  >
                                <HeaderStyle Width = "10%"  Font-Bold = "true"   />
                                <ItemTemplate >
                                    <asp:LinkButton ID = "lnk_BillbyBill"  runat = "server" Text = "BillByBill" Visible='<%#Convert.ToBoolean(Eval("IsBillByBill"))%>' ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                             <%--<***************************************************************************>
                            <********************************FBT Category***********************************>
                            <***************************************************************************>--%>
                            <asp:TemplateColumn HeaderText="FBTCategory"  >
                                <HeaderStyle Width = "10%"  Font-Bold = "true"   />
                                <ItemTemplate >
                                    <asp:LinkButton ID = "lnk_FBTCategory"  runat = "server" Text = "FBTCateg" Visible='<%#Convert.ToBoolean(Eval("IsFBTApplicable"))%>' ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                                <%--<***************************************************************************>
                            <********************************Bank ***********************************>
                            <***************************************************************************>--%>
                            <asp:TemplateColumn HeaderText="BankDetails"  >
                                <HeaderStyle Width = "10%"  Font-Bold = "true"   />
                                <ItemTemplate >
                                    <asp:LinkButton ID = "lnk_Bank"  runat = "server" Text = "BankDetail" Visible='<%#Convert.ToBoolean(Eval("IsBankApplicable"))%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                                                                             
                        </Columns>
                    </asp:DataGrid>
                    </td>
                 </tr>
             </table>
                    </fieldset>
                </td>
            </tr>
            
            <%--< ***************************************************************************** />--%>
            <%--< *************************debit/credit total***************************** />--%>
            <%--< ***************************************************************************** />--%>
            <tr>
                <td colspan="5">
                
                    <table style="width:100%" border="0">
                        <tr>
                            <td style="width:5%"></td>
                            <td style="width:30%"></td>
                            <td style="width:16%" align = "right" >
                                <asp:Label ID = "lbl_TotalDebit" runat = "server" CssClass = "TEXTBOX" style="text-align :right; font-weight :bold ;
                                border-top-style: solid; border-top-color: black; border-bottom: black thick double;"></asp:Label>
                            </td>
                            <td style="width:13%" align = "right">
                                <asp:label ID = "lbl_TotalCredit" runat = "server" CssClass = "TEXTBOX" style="text-align :right;font-weight:bold ; 
                                border-top-style: solid; border-top-color: black; border-bottom: black thick double;" ></asp:label>
                            </td>
                            <td style ="width:10%"></td>
                            <td style ="width:10%"></td>
                            <td style="width:20%"></td>
                             
                        </tr>
                    </table>
               </td>
            </tr>
            
            
            
            <%--< ***************************************************************************** />--%>
            <%--< ********************************narration************************************ />--%>
            <%--< ***************************************************************************** />--%>
            <tr>
                <td colspan="5">
                    <table style="width:100%" class = "TABLE">
                        <tr>
                            <td style="width: 15%; text-align: right">
                            </td>
                            <td colspan="7">
                            </td>
                            <td class="TDMANDATORY" style="width: 1%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 15%; text-align: right">
                            </td>
                            <td colspan="7">
                            </td>
                            <td class="TDMANDATORY" style="width: 1%">
                            </td>
                        </tr>
                                             
                         <tr id="tr_FBT" runat="server">
                             <td style="width:15%; text-align: right;">
                                    FBT Payment Type :</td>
                                <td colspan = "7" align="left">
                                    <asp:Label ID="lbl_FBTPaymentType" CssClass="LABEL" runat="server" Font-Bold="true"></asp:Label>
                                </td>
                                <td style="width: 1%" class="TDMANDATORY">
                                    &nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td style="width:15%; text-align: right;">
                                Narration :</td>
                            <td colspan = "7" >
                                <asp:TextBox ID = "txt_Narration" runat = "server" height = "50px" 
                                TextMode = "MultiLine" CssClass = "TEXTBOX" BorderWidth = "1px" Width = "98%" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td style="width: 1%" class="TDMANDATORY">
                                    &nbsp;&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
           <td style="width: 110px">
           <table style="width:100%">
          
           </table>
           </td>
               
            </tr>
            <tr>
                <td colspan="5" style="text-align: center">
                    </td>
            </tr>
            
            <tr>
                <td colspan="5">
                    &nbsp;</td>
            </tr>
      </table>

