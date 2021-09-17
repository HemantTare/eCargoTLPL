<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTDSComputation.ascx.cs" Inherits="FA_Common_Reports_WucTDSComputation" %>
<%@ Register Src="../../CommonControls/WucStartEndDate.ascx" TagName="WucStartEndDate"
    TagPrefix="uc1" %>

      
<table  class="TABLE" style="width: 100%">
        <tr>
            <td colspan="7"  class="TDGRADIENT">
                 &nbsp;<asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="TDS COMPUTATION" Font-Bold="True" /></td>
        </tr>
        <tr>
        <td colspan="7"></td>
        </tr>
         <tr>
        <td colspan="7">
            <uc1:WucStartEndDate ID="WucStartEndDate1" runat="server" />
        </td>
        </tr>
        
         <tr>
        <td colspan="6" style="height: 5px" align="left">
            &nbsp;</td>
    </tr>
     
    <tr>
        <td colspan="6" style="width: 100%" align="left">
                    <table cellpadding="3" cellspacing="3" border="0" width="100%">
                            <tr>
                                <td>
                                    <asp:DataGrid ID="dg_TDSComputation" CssClass="GRID" runat="server" AutoGenerateColumns="False"
                                        Width="100%" ShowFooter="true" >
                                       
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                        <Columns>
                                         <asp:TemplateColumn HeaderText="Ledger Id" Visible="false" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Ledger_Id")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Ts TDS Ledger"  Visible="false" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                 <asp:Label ID="lbl_Is_TDS_Ledger" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Is_TDS_Ledger")%>'></asp:Label>
                                                 </ItemTemplate>
                                            </asp:TemplateColumn>
                                        
                                            <asp:TemplateColumn HeaderText="Particulars" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                   <asp:HyperLink runat="server" ID="lnkname" Visible='<%#!Convert.ToBoolean(DataBinder.Eval(Container.DataItem,"Is_TDS_Ledger"))%>'  Text='<%# DataBinder.Eval(Container.DataItem,"Particulars") %>' 
                                                 NavigateUrl='<%# "../../Finance/Reports/FrmLedgerVoucher.aspx?Is_Consol=" + Is_Consol + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id +"&Id=" +DataBinder.Eval(Container.DataItem,"Ledger_Id")+ "&StartDate=" +Start_Date+ "&EndDate=" +End_Date + "&Name=" + DataBinder.Eval(Container.DataItem,"Particulars")%>'
                                                 style="color: Black;  font-weight:bold; font-family:Verdana; text-decoration: none" ></asp:HyperLink>
                                                 
                                                 <asp:Label ID="lbl_Id" runat="server"  Font-Italic="true" Visible='<%#Convert.ToBoolean(DataBinder.Eval(Container.DataItem,"Is_TDS_Ledger"))%>' Text='<%#DataBinder.Eval(Container.DataItem,"Particulars")%>'></asp:Label>
                                                 
                                              </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Amount Paid Payable Till Date" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Amount_Paid_Till_Date")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Tax" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Tax")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Surcharge" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Surcharge")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Addl Surcharge(cess)" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Add_Surcharge")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Addl Education(Cess)" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Add_Education_Cess")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Total TDS" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Total_TDS")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                             <asp:TemplateColumn HeaderText="Less:TDS Deducted Till Date" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Less_TDS_Deducted")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                            <asp:TemplateColumn HeaderText="Net TDS To Deduct" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Net_TDS_to_Deduct")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                        </table>
            <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" EnableViewState="false"></asp:Label></td>                
    </tr>
   
     </table>
  
    
