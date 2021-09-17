<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucDoorDelAndLocalCartVoucher.ascx.cs" Inherits="Finance_Accounting_Vouchers_WucDoorDelAndLocalCartVoucher" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<script  type="text/javascript" src="../../Javascript/Common.js"></script>
<script  type="text/javascript" src="../../Javascript/Finance/Accounting Vouchers/DoorDelAndLocalCartVoucher.js"></script>
<script type="text/javascript"  src ="../../Javascript/ddlsearch.js"></script>
<asp:ScriptManager ID="scm_DoorDelAndLocalCartVoucher" runat="server"></asp:ScriptManager>
<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="4">
            <asp:Label ID="lbl_heading_name" runat="server" CssClass="HEADINGLABEL" Font-Bold="True" Text="DOOR DELIVERY VOUCHER "></asp:Label></td>
    </tr>
    <tr>
        <td colspan="4">&nbsp;</td>
    </tr>
    <tr>
        <td style="width: 15%" class="TD1">Voucher No.:</td>
        <td style="width: 15%">
            <asp:Label ID="lbl_Vch_No" runat="server" CssClass="LABEL" Font-Bold="True" ForeColor="Black"></asp:Label></td>
        <td style="width: 45%" class="TD1">Voucher Date :</td>
        <td style="width: 25%">     
            <uc1:WucDatePicker ID="WucVoucherDate" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="width: 15%; vertical-align: top;" class="TD1">Ref. No.:</td>
        <td style="width: 15%; vertical-align: top;" class="TD1">
            <asp:TextBox ID="txt_RefNo" runat="server" CssClass="TEXTBOX" MaxLength="15" BorderWidth="1px"></asp:TextBox></td>
        <td style="width: 45%"></td>
        <td style="width: 25%"></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 15%; vertical-align: top;">
            <asp:Label runat="server" ID="lbl_gc_no" CssClass="LABEL" Text="Enter GC No(s):"></asp:Label>
        </td>
        <td colspan="3" style="width: 20%;">
          <asp:TextBox ID="txt_GCNo" onkeyup ="valid_for_Docket(this)" Height="30px" CssClass="TEXTBOX" BorderWidth="1px" runat="server" TextMode="MultiLine"></asp:TextBox></td>
  
    </tr>
    <tr>
        <td class="TD1" style="width: 15%; vertical-align: top;">
            <asp:Label runat="server" ID="lbl_gc_not_found" CssClass="LABEL" Text="GC Not Found:"></asp:Label>
        </td>
        <td colspan="3" style="width: 20%;">
         <asp:UpdatePanel ID="Upd_Pnl_txt_GCNotFound" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Get_Details" />
             </Triggers>
        <ContentTemplate>     
            <asp:TextBox ID="txt_GCNotFound" Height="30px" CssClass="TEXTBOX" BorderWidth="1px" runat="server" TextMode="MultiLine"></asp:TextBox>
         </ContentTemplate>
         </asp:UpdatePanel>   
        </td>
    </tr>
    <tr>
        <td style="width: 15%; vertical-align: top;" class="TD1"></td>
        <td style="width: 15%; vertical-align: top;" class="TD1">
          <asp:Button ID="btn_Get_Details" CssClass="BUTTON" OnClientClick="return get_details()" runat="server" Text="Get Details" AccessKey="D" OnClick="btn_Get_Details_Click"  /></td>
        <td style="width: 45%" class="TD1">        </td>
        <td style="width: 25%"></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lbl_Client_Error" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
    </tr>
    <tr>
        <td style="vertical-align: top; width: 15%">&nbsp;</td>
        <td class="TD1" style="width: 15%"></td>
        <td style="width: 45%"></td>
        <td style="width: 25%"></td>
    </tr>
    <tr>
        <td colspan="4">
        <table width="100%">
            <tr>
                <td width="100%">
                    <asp:Panel ID="Panel1" runat="server"  GroupingText="GC Details"  CssClass="PANEL" Width="100%" >
                   
        <%--<div class="DIV" id="dv_DG" runat="server"  title="GC Details" >--%>
            <asp:Panel ID="pnl_dg" runat="server"   ScrollBars="Vertical" Height="140" CssClass="PANEL" Width="100%" >
                        <table cellpadding="3" cellspacing="3" border="0" width="98%">
                            <tr>
                                <td style="width: 100%; " colspan="8">
        <asp:UpdatePanel ID="Upd_Pnl_Grid" UpdateMode="Conditional" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Get_Details" />
             </Triggers>
        <ContentTemplate>               
          
        <asp:DataGrid ID="dg_Voucher" runat="server" AutoGenerateColumns="False" 
            AllowSorting="true"  DataKeyField = "Sr_No" 
             CellPadding  = "3" CssClass="GRID" ShowHeader="true" OnItemDataBound="dg_Voucher_ItemDataBound" >        
            <HeaderStyle CssClass="GRIDHEADERCSS" />
            <Columns>
                
                    <asp:TemplateColumn>
                     <HeaderStyle Width="2%"/>
                       <ItemTemplate  >
                           <asp:CheckBox ID="chk_Attach" Checked="true" runat="server" onclick="Txt_Enable()"/>
                       </ItemTemplate> 
                    </asp:TemplateColumn>
                    
                     <asp:TemplateColumn  HeaderText="Sr.No" Visible="False">
                      <HeaderStyle Width="1%" />
                        <ItemTemplate>                           
                           <asp:Label ID="lbl_Sr_No" runat="server" Text = '<%#DataBinder.Eval(Container.DataItem, "Sr_No")%>'/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="GC No."  ItemStyle-Width="8%">
                     <HeaderStyle />
                       <ItemTemplate>
                            <asp:Label ID="lbl_GC_No" runat="server" Text=' <%#(DataBinder.Eval(Container.DataItem, "GC_No_For_Print"))%>' />
                       </ItemTemplate>     
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Booking Date"  ItemStyle-Width="10%">
                     <HeaderStyle/>
                       <ItemTemplate>
                            <asp:Label ID="lbl_BookingDate" runat="server" Text=' <%#(DataBinder.Eval(Container.DataItem, "BookingDate"))%>' />
                       </ItemTemplate>     
                    </asp:TemplateColumn>
                                                                             
                    <asp:TemplateColumn HeaderText="Actual Weight" ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="12%">
                     <HeaderStyle  />    
                       <ItemTemplate>
                             <asp:Label ID="lbl_Weight_Act" runat="server" Text='<%#(DataBinder.Eval(Container.DataItem, "Total_Actual_Weight"))%>'/>     
                       </ItemTemplate>
                    </asp:TemplateColumn>
                    
                      <asp:TemplateColumn HeaderText="No. of Articles" ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="10%">
                      <HeaderStyle />
                             <ItemTemplate>
                             <asp:Label ID="lbl_Pkgs" runat="server" Text='<%#(DataBinder.Eval(Container.DataItem, "Total_Articles"))%>'/>
                       </ItemTemplate>
                    </asp:TemplateColumn>
                   
                      <asp:TemplateColumn HeaderText="Client Name"  ItemStyle-Width="20%">
                      <HeaderStyle  />
                       <ItemTemplate>
                             <asp:Label ID="lbl_Client_Name" runat="server" Text='<%#(DataBinder.Eval(Container.DataItem, "Consignee_Name"))%>'/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                      <asp:TemplateColumn HeaderText="Amount Charged" ItemStyle-HorizontalAlign="Right"  ItemStyle-Width="10%">
                      <HeaderStyle  />
                       <ItemTemplate>
                             <asp:Label ID="lbl_AmountCharged" runat="server" Text='<%#(DataBinder.Eval(Container.DataItem, "AmountCharged"))%>'/>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    
                     
                     <asp:TemplateColumn HeaderText="Amount Spent"  ItemStyle-Width="12%">
                      <HeaderStyle  />
                       <ItemTemplate>
                            <asp:Textbox CssClass="TEXTBOXNOS" width="96%" runat="server"  Text='<%#(DataBinder.Eval(Container.DataItem, "AmountSpent"))%>' onkeyup="valid(this)" onblur="Txt_Enable()" BorderWidth="1px" id="txt_Amount_Spent_Add" MaxLength="10"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                    
                             
                     <asp:TemplateColumn HeaderText="Remark"  ItemStyle-Width="38%">
                      <HeaderStyle />
                       <ItemTemplate>
                            <asp:Textbox CssClass="TEXTBOX" width="96%" runat="server" MaxLength="100" Text='<%#(DataBinder.Eval(Container.DataItem, "Remark"))%>'  BorderWidth="1px" id="txt_Remark" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateColumn>
                                 
                    <asp:TemplateColumn Visible="false"  >
                                        
                       <ItemTemplate>
                        <asp:Label ID="lbl_GC_Id" runat="server" Text=' <%#(DataBinder.Eval(Container.DataItem, "GC_Id"))%>' ></asp:Label>
                       </ItemTemplate> 
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>
           
            </ContentTemplate>
        </asp:UpdatePanel>
            </td>
                             </tr>
                             </table>
                             </asp:Panel>
                              </asp:Panel>
                             <%--</div>--%>
                            
                </td>
            </tr>
        </table>

            </td>
    </tr>
    <tr>
        <td colspan="4">
            <table style="width: 100%">
            <tr>
                <td style="width: 8%" align="left">
                    <asp:Label ID="lbl_TotalGCText" runat="server" CssClass="LABEL" Font-Bold="True" ForeColor="Black"></asp:Label></td>
                <td style="width: 42%" align="left">
                <asp:Label ID="lbl_TotalGC" runat="server" CssClass="LABEL" Font-Bold="True" ForeColor="Black">0</asp:Label>
                </td>
                <td style="width: 20%" class="TD1">
                    <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Font-Bold="True" ForeColor="Black">Total Amount Spent : </asp:Label></td>
                <td style="width: 11%" align="right" >
                    <asp:Label ID="lbl_Total_Spent" runat="server" CssClass="LABEL" Font-Bold="True" ForeColor="Black"></asp:Label></td>
                <td style="width: 19%">
                    &nbsp;</td>
            </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td  colspan="4"> 
        <table style="width: 100%;font-weight:normal;">
            <tr>
                <td style="width: 18%;" class="TD1">Select Ledger Type : </td>
                <td style="width: 20%;" align="left">
                    <asp:RadioButtonList ID="RadioButtonList1" onclick="return Cheque()" runat="server" RepeatDirection="Horizontal" CssClass="CHECKBOXLIST" >
                        <asp:ListItem Value="Bank">Bank</asp:ListItem>
                        <asp:ListItem Selected="True" Value="Cash">Cash</asp:ListItem>
                        <asp:ListItem Value="CreditTo">CreditTo</asp:ListItem>

                    </asp:RadioButtonList></td>
                  <td style="width: 30%;">
                      &nbsp;&nbsp;
                </td>
                
                 <td id="ddl_Ledger" runat="server" style="width: 35%; ">
               <asp:Label ID="lbl_ddlLedgers" runat="server" Text="Ledgers:" CssClass="LABEL"></asp:Label>&nbsp;
               <cc1:DDLSearch ID="ddl_GetLedgers" CallBackAfter="2" IsCallBack="True" AllowNewText="True"
                   CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerForCreditTo"
                   runat="server" />
               </td>
                
            </tr>
            <tr id="tr_Cheque" runat="server">
                <td class="TD1" colspan="4" >
                    <table style="width: 100%">
                        <tr>
                            <td style="width: 18%; " class="TD1">
                    <asp:Label ID="lbl_Cheque" runat="server" Text="Cheque No:" CssClass="LABEL"></asp:Label></td>
                            <td style="width: 15%; ">
                    <asp:TextBox  ID="txt_Cheque" runat="server" BorderWidth="1px"  Width="90%" CssClass="TEXTBOXNOS" onkeyup="valid(this)" onblur="valid(this)" MaxLength="6"></asp:TextBox></td>
                            <td style="width: 12%; " class="TD1">
                                <asp:Label ID="lbl_ChequeDate" runat="server" Text="Cheque Date:"></asp:Label></td>
                            <td style="width: 15%; ">
                                <uc1:WucDatePicker ID="WucChequeDate" runat="server" />
                            </td>
                            <td style="width: 18%; " class="TD1">
                <asp:Label ID="lbl_ChequeInFavourOf" runat="server" CssClass="LABEL" Text="Cheque In Favour Of:"></asp:Label></td>
                            <td style="width: 24%; ">
            <asp:TextBox ID="txt_ChequeInFavourOf" runat="server" BorderWidth="1px" CssClass="TEXTBOX" Width="80%"
                MaxLength="100" ></asp:TextBox>&nbsp;
            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="4" style="vertical-align: top">
          <table style="width: 100%">
                <tr>
                    <td class="TD1" style="vertical-align: top; width: 15%">Remark :</td>
                    <td style="width: 84%">
                        <asp:TextBox ID="txt_Remark" runat="server" BorderWidth="1px" CssClass="TEXTBOX" Height="60px" TextMode="MultiLine"></asp:TextBox></td>
                    <td style="width: 1%; vertical-align: top;">
                       </td>
                </tr>
          </table>  
        </td>
    </tr>
    <tr>
        <td style="width: 15%"><asp:HiddenField runat="server" ID="hdn_GCCaption" /></td>
       
    </tr>
    <tr>
        <td align="center" colspan="4"><asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" ValidationGroup="btn_Save1" OnClick="btn_Save_Click"  /></td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;<asp:Label ID="lbl_Error" Width="100%" runat="server" Font-Bold="True" ForeColor="Red" ></asp:Label>&nbsp;<asp:HiddenField ID="hdn_Total_Spent_Add" runat="server" /> </td>
    </tr>
</table>

<script type="text/javascript">
Cheque();
Txt_Enable();
</script>