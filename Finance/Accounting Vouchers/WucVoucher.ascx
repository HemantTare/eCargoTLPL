<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVoucher.ascx.cs" Inherits="Finance_Accounting_Vouchers_Voucher" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<script type = "text/javascript" src="../../Javascript/Common.js" ></script>   
<script language="javascript" type="text/javascript" src="../../Javascript/ddlsearch.js" ></script>
 

<script type = "text/javascript" >
 
 function Allow_To_Save()
 {
    return true;
 }
 
 function DesableDebitCredit(ddl_DrCr,txt_Debit,txt_Credit)
 {
     txt_Credit.value='';
     txt_Debit.value='';

     if(ddl_DrCr.options[ddl_DrCr.selectedIndex].value=='Cr')
     {
      txt_Credit.disabled=false;
      txt_Debit.disabled=true;
     }
     else
     {
      txt_Debit.disabled=false;
      txt_Credit.disabled=true;
     }
 }
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

        <asp:ScriptManager id = "scm_Voucher" runat = "server"></asp:ScriptManager>
        
        <table  style="width:100%; border-top-style: solid; border-top-color: black;" class = "TABLE" width="100%">
        
            <tr>
                <td class = "TDGRADIENT" colspan="6">
                    <asp:Label ID = "Label1" runat = "server" Text="VOUCHER" CssClass="HEADINGLABEL"></asp:Label>
                </td>
            </tr>
        
           <tr>
                <td colspan="3" align="right">
                    <asp:Button ID="btn_TDSHelper" runat="server" Text="TDS Helper" CssClass = "BUTTON"/>
                </td>
                <td colspan="3" align="left">
                    <asp:Button ID="btn_FBTHelper" runat="server" Text="FBT Helper" CssClass = "BUTTON"/>
                </td>
            </tr>
            
            <tr>
                 <td colspan = "2"  style="width:20%; text-align: center; background :silver; height: 21px;">
                    <asp:Label ID="lbl_VoucherType" runat="server"
                      Font-Bold ="true"></asp:Label>
                </td>
            </tr>
            
            <tr>
               
                <td style="width:5%; text-align: right;">
                    No. : 
                </td>
              
                <td style ="width:15%" >
                    <asp:Label ID="txt_VoucherNo" runat="server" ReadOnly = "true"   STYLE ="width:auto"></asp:Label>
                </td>
                <td ></td> 
                <td style="width: 15%; text-align: right;">
                    Date: </td>
                <td style="width: 15%">
                    <uc1:WucDatePicker ID="dtp_VoucherDate" runat="server" />
                </td>
                   
            </tr>
          
            <tr>
               
                <td style="width:5%; text-align: right;">
                    Ref No :
                </td>
                <td style="width:15%">
                    <asp:TextBox ID = "txt_RefNo" runat = "server"   CssClass = "TEXTBOX"  BorderWidth = "1px" style ="width:auto;"> </asp:TextBox>
                </td>
                <td style="width:20%">
                </td>
                <td style="width:15%">
                </td>
                <td style="width:15%">
                </td>
                
            </tr>
           
            <tr>
                <td colspan="5" >
                  <asp:UpdatePanel ID = "UpdatePanel1" runat = "server" >
                    <ContentTemplate >
                    <asp:DataGrid ID = "dg_Voucher" runat = "server" style="width:100%" CssClass = "Grid"
                        AutoGenerateColumns="False" OnItemCommand="dg_Voucher_ItemCommand" OnItemDataBound="dg_Voucher_ItemDataBound"
                        ShowFooter = "True" 
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
                                
                                <EditItemTemplate>
                                     <asp:DropDownList ID = "ddl_CrDr" runat = "server"  CssClass = "DROPDOWN" AutoPostBack="true" OnSelectedIndexChanged="ddl_dgDrCr_SelectedIndexChanged"
                                        style ="width:100%"  SelectedValue = '<%#convertToDrCr(Eval("Debit"))%>' >
                                        <asp:ListItem Value="Cr" Text = "Cr"></asp:ListItem>
                                        <asp:ListItem Value="Dr" Text = "Dr"></asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                
                                <FooterTemplate>
                                    <asp:DropDownList ID = "ddl_CrDr" runat = "server"  CssClass = "DROPDOWN" AutoPostBack="true" OnSelectedIndexChanged="ddl_dgDrCr_SelectedIndexChanged"
                                       style ="width:100%">
                                        <asp:ListItem Value="Cr" Text = "Cr"></asp:ListItem>
                                        <asp:ListItem Value="Dr" Text = "Dr"></asp:ListItem>
                                    </asp:DropDownList>
                                </FooterTemplate>
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
                                
                                <EditItemTemplate >
                                     <cc1:DDLSearch ID="ddl_Ledger" runat="server" AllowNewText="false"  IsCallBack="true" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchLedgerForVoucher"></cc1:DDLSearch>
                                </EditItemTemplate>
                                
                                <FooterTemplate>
                                     <cc1:DDLSearch ID="ddl_Ledger" runat="server" AllowNewText="false"  IsCallBack="true" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchLedgerForVoucher"></cc1:DDLSearch>
                                </FooterTemplate>
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
                                <EditItemTemplate>
                                     <asp:TextBox ID = "txt_Debit" runat = "server" style ="width:75%;text-align:right;" CssClass = "TEXTBOXNOS" onkeyup = "valid(this)"
                                    text ='<%#DataBinder.Eval(Container.DataItem,"Debit")%>'></asp:TextBox>
                                    
                                </EditItemTemplate>
                                <FooterTemplate >
                                    <asp:TextBox ID = "txt_Debit" runat = "server"  style ="width:75%;text-align:right;" CssClass = "TEXTBOXNOS" BorderWidth = "1px"
                                    onkeyup = "valid(this)"></asp:TextBox>
                                </FooterTemplate>
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
                                <EditItemTemplate>
                                    <asp:TextBox ID = "txt_Credit" runat = "server" style ="width:75%;text-align:right;" CssClass = "TEXTBOXNOS" BorderWidth = "1px" onkeyup = "valid(this)"
                                    text ='<%#DataBinder.Eval(Container.DataItem,"Credit")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate >
                                     <asp:TextBox ID = "txt_Credit" runat = "server" style ="width:75%;text-align:right;" CssClass = "TEXTBOXNOS" BorderWidth = "1px"
                                    onkeyup = "valid(this)" ></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            
                            <%--<***************************************************************************>
                            <********************************COST CENTRE***********************************>
                            <***************************************************************************>--%>
                            <asp:TemplateColumn HeaderText="Cost Centre"  >
                                <HeaderStyle Width = "10%" Font-Bold = "true"  />
                                <ItemTemplate >
                                    <asp:LinkButton ID = "lnk_CostCentre"  runat = "server" Text = "CostCentre" Visible='<%#Convert.ToBoolean(Eval("IsCostCentre"))%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            <%--<***************************************************************************>
                            <********************************BILL BY BILL***********************************>
                            <***************************************************************************>--%>
                            <asp:TemplateColumn HeaderText="Bill By Bill"  >
                                <HeaderStyle Width = "10%"  Font-Bold = "true"   />
                                <ItemTemplate >
                                    <asp:LinkButton ID = "lnk_BillbyBill"  runat = "server" Text = "BillByBill" Visible='<%#Convert.ToBoolean(Eval("IsBillByBill"))%>' ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                             <%--<***************************************************************************>
                            <********************************FBT Category***********************************>
                            <***************************************************************************>--%>
                            <asp:TemplateColumn HeaderText="FBT Category"  >
                                <HeaderStyle Width = "10%"  Font-Bold = "true"   />
                                <ItemTemplate >
                                    <asp:LinkButton ID = "lnk_FBTCategory"  runat = "server" Text = "FBTCateg" Visible='<%#Convert.ToBoolean(Eval("IsFBTApplicable"))%>' ></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                                <%--<***************************************************************************>
                            <********************************Bank ***********************************>
                            <***************************************************************************>--%>
                            <asp:TemplateColumn HeaderText="Bank Details"  >
                                <HeaderStyle Width = "10%"  Font-Bold = "true"   />
                                <ItemTemplate >
                                    <asp:LinkButton ID = "lnk_Bank"  runat = "server" Text = "BankDetail" Visible='<%#Convert.ToBoolean(Eval("IsBankApplicable"))%>'></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            
                            
                            <asp:EditCommandColumn  CancelText="Cancel" EditText="Edit"
                                HeaderText="Edit" UpdateText="Update">
                                <HeaderStyle   Width="5%" Font-Bold = "true" />
                                
                            </asp:EditCommandColumn>
                             <asp:TemplateColumn HeaderText="Add">
                                <HeaderStyle Width="5%"  Font-Bold = "true"  />
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnk_Add" runat="Server" CommandName="Add" Text="Add">
                                    </asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateColumn>    
                              <asp:TemplateColumn HeaderText="Delete">
                                <HeaderStyle Width="5%"  Font-Bold = "true"  />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_Delete" runat="Server" CommandName="Delete" Text="Delete">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>                                                  
                        </Columns>
                    </asp:DataGrid>
                     </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID = "dg_Voucher" />
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            
            <%--< ***************************************************************************** />--%>
            <%--< *************************debit/credit total***************************** />--%>
            <%--< ***************************************************************************** />--%>
            <tr>
                <td colspan="5">
                 <asp:UpdatePanel ID = "UpdatePanel2" runat = "server" >
                    <ContentTemplate >
                    <table style="width:100%" border="0">
                        <tr>
                            <td style="width:5%"></td>
                            <td style="width:25%"></td>
                            <td style="width:15%" align = "right" >
                                <asp:Label ID = "lbl_TotalDebit" runat = "server" CssClass = "TEXTBOX" style="text-align :right; font-weight :bold ;
                                border-top-style: solid; border-top-color: black; border-bottom: black thick double;"></asp:Label>
                            </td>
                            <td style="width:15%" align = "right">
                                <asp:label ID = "lbl_TotalCredit" runat = "server" CssClass = "TEXTBOX" style="text-align :right;font-weight:bold ; 
                                border-top-style: solid; border-top-color: black; border-bottom: black thick double;" ></asp:label>
                            </td>
                            <td style ="width:10%"></td>
                            <td style ="width:10%"></td>
                            <td style="width:20%"></td>
                             
                        </tr>
                    </table>
                     </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID = "dg_Voucher" />
                       <%-- <asp:AsyncPostBackTrigger ControlID = "btn_Save" />--%>
                    </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            
            
            
            <%--< ***************************************************************************** />--%>
            <%--< ********************************narration************************************ />--%>
            <%--< ***************************************************************************** />--%>
            <tr>
                <td colspan="5">
                    <table style="width:100%" class = "TABLE">
                                             
                         <tr class="HIDEGRIDCOL">
                             <td style="width:9%; text-align: right;">
                                    FBT Payment Type :</td>
                                <td colspan = "7">
                                    <asp:DropDownList ID="ddl_FBTPaymentType"  CssClass="DROPDOWN" runat="server">
                                        <asp:ListItem Selected="True" Value="0">--Select One--</asp:ListItem>
                                        <asp:ListItem Value="Advance Tax(100)">Advance Tax(100)</asp:ListItem>
                                        <asp:ListItem Value="Self Assessment Tax(300)">Self Assessment Tax(300)</asp:ListItem>
                                        <asp:ListItem Value="Tax On Regular Assessment(400)">Tax On Regular Assessment(400)</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td style="width: 1%" class="TDMANDATORY">
                                    &nbsp;</td>
                        </tr>
                        
                        <tr>
                            <td style="width:9%; text-align: right;">
                                Narration :</td>
                            <td colspan = "7" >
                                <asp:TextBox ID = "txt_Narration" runat = "server" height = "50px" 
                                TextMode = "MultiLine" CssClass = "TEXTBOX" BorderWidth = "1px" Width = "98%"></asp:TextBox>
                                &nbsp;</td>
                                <td style="width: 1%" class="TDMANDATORY">*
                                    &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
           <td>
           <table style="width:100%">
          
           </table>
           </td>
               
            </tr>
            <tr>
                <td colspan="5" style="text-align: center">
                    <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass = "BUTTON" OnClick="btn_Save_Click"/></td>
            </tr>
            
            <tr>
                <td colspan="5">
                    <asp:UpdatePanel ID = "up_lbl_Errors" runat = "server" >
                    <ContentTemplate >
                        &nbsp;<asp:Label ID = "lbl_Errors" runat = "server" ForeColor = "red" Font-Bold = "true" EnableViewState="false"></asp:Label>
                    </ContentTemplate>
                    
                    </asp:UpdatePanel>
                </td>
            </tr>
      </table>

