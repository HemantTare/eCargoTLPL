<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucFBTComputationLedgerWise.ascx.cs" Inherits="FA_Common_Reports_WucFBTComputationLedgerWise" %>
<%@ Register Src="../../CommonControls/WucStartEndDate.ascx" TagName="WucStartEndDate"
    TagPrefix="uc1" %>
<script type="text/javascript">

    function ChangePeriod()
    {
    
        var Path='../../Controls/frm_Date_Range.aspx'
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-600);
        var popH = (h-500);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
    </script>

<table  class="TABLE" style="width: 100%">
        <tr>
            <td colspan="7"  class="TDGRADIENT">
                 &nbsp;<asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="LEDGERS OF FBT CATEGORY" Font-Bold="True" /></td>
        </tr>
        <tr>
        <td colspan="7"></td>
        </tr>
        <tr>
        <td colspan="7"></td>
        </tr>
         <tr>
        
    
      <td colspan="2" style="height: 5px" align="left" >
     <asp:Label ID="lbl_FBTCategory" runat="server" Font-Bold="true" Text="FBT Category:"></asp:Label>
      &nbsp;&nbsp;&nbsp;    
    <asp:Label ID="lbl_FBTCategoryName" runat="server" Font-Bold="true"></asp:Label>
     &nbsp;&nbsp; 
    <asp:Label ID="lbl_FBTSection" Font-Bold="true" runat="server" Text=" FBT Section:"></asp:Label>
     &nbsp;&nbsp;
    <asp:Label ID="lbl_FBTSectionName" runat="server" Font-Bold="true"></asp:Label>
    </td>
    
    </tr>
     <tr>
        <td colspan="7"></td>
        </tr>
        
         <tr>
             <td colspan="4" style="height: 5px">
                 <uc1:WucStartEndDate ID="WucStartEndDate1" runat="server" />
                 &nbsp;&nbsp;</td>
    </tr>
     
    <tr>
        <td colspan="6" style="width: 100%" align="left">
                    <table cellpadding="3" cellspacing="3" border="0" width="100%">
                            <tr>
                                <td>
                                    <asp:DataGrid ID="dg_FBTComputationLedgerWise" CssClass="GRID" runat="server" AutoGenerateColumns="False"
                                        Width="100%" ShowFooter="true" >
                                       
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                        <Columns>
                                        
                                          <asp:TemplateColumn HeaderText="Ledger Id" Visible="false" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Ledger_Id")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Name Of The Ledger" ItemStyle-Width="16%">
                                                <ItemTemplate>
                                                 <asp:HyperLink runat="server" ID="lnkname"  Text='<%# DataBinder.Eval(Container.DataItem,"Ledger_Name") %>' 
                                                 NavigateUrl='<%# "../../Finance/Reports/FrmLedgerVoucher.aspx?Is_Consol=" + Is_Consol + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id +"&Id=" +DataBinder.Eval(Container.DataItem,"Ledger_Id")+ "&StartDate=" +Start_Date + "&EndDate=" +End_Date + "&Name=" +DataBinder.Eval(Container.DataItem,"Ledger_Name")%>'
                                                 style="color: Black;  font-weight:bold; font-family:Verdana; text-decoration: none" ></asp:HyperLink>
                                               </ItemTemplate>                                               
                                                </asp:TemplateColumn>                             
                                                                               
                                                                                      
                                            <asp:TemplateColumn HeaderText="Expenditure Amount" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Expenditure_Amount")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Amount Recovered" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Amount_Recovered")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nett Expenditure" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Net_Expenditure")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Percentage as per Sec 115WC" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Percentage")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Value of fringe Benefit" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "FBT_Value")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                             <asp:TemplateColumn HeaderText="FBT" ItemStyle-Width="8%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "FBT_Amount")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                            <asp:TemplateColumn HeaderText="Education Cess" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Add_Surcharge_Value")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                             <asp:TemplateColumn HeaderText="Additional Education Cess" ItemStyle-Width="10%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Add_Education_Value")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                             <asp:TemplateColumn HeaderText="Total Tax" ItemStyle-Width="12%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Total_Tax")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                        </table>
                    </td>                
    </tr>
    <tr>
        <td style="width: 100%;" colspan="4" >
            <table  class="TABLE" style="width: 100%;">
                <tr>
               
                    
                    <td  style="width: 22%;font-weight:bold">
                      GRAND TOTAL
                    </td>
                    <td style="width: 10%">
                        <asp:Label ID="lbl_TotExpenditureAmt"  Style="text-align: right" runat="server" Font-Bold="True" ></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <asp:Label ID="lbl_TotAmtRecovered"  Style="text-align: right" runat="server" Font-Bold="True" ></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <asp:Label ID="lbl_TotNetExpenditure"  Style="text-align: right" runat="server" Font-Bold="True" ></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <asp:Label ID="lbl_TotPercentageAsPerSec"  Style="text-align: right" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <asp:Label ID="lbl_TotValueOfFringeBenefit"   Style="text-align: right" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 8%">
                        <asp:Label ID="lbl_TotalFBT"   Style="text-align: right" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <asp:Label ID="lbl_EducationCess"   Style="text-align: right" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 10%">
                        <asp:Label ID="lbl_AddlEducationCess"   Style="text-align: right" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 12%">
                        <asp:Label ID="lbl_TotalTax"   Style="text-align: right; width:100%" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Label ID="lbl_Error" runat="server" EnableViewState="false" CssClass="LABELERROR"></asp:Label></td>
    </tr>
     </table>
    