<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucFBTComputation.ascx.cs" Inherits="FA_Common_Reports_WucFBTComputation" %>
<%@ Register Src="../../CommonControls/WucStartEndDate.ascx" TagName="WucStartEndDate"
    TagPrefix="uc1" %>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    
   <%-- <script language="javascript" type="text/javascript">

function OpenFBTLedger(FBTCategoryId)
  {
  var Path ='';
  Path='~/FA_Common/Reports/FrmFBTComputationLedgerWise.aspx?FBTCategoryId=' + FBTCategoryId  ;
  var w = screen.availWidth;
  var h = screen.availHeight;
  var popW = (w-90);
  var popH = (h-290);
  var leftPos = (w-popW)/2;
  var topPos = (h-popH)/2;
  window.open(Path, 'FBTLedgerWindow', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, res izable=yes,scrollbars=yes');
  return false;
  }
  </script>--%>

<table  class="TABLE" style="width: 100%">
        <tr>
            <td colspan="7"  class="TDGRADIENT">
                 &nbsp;<asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="FBT COMPUTATION" Font-Bold="True" /></td>
        </tr>
        <tr>
        <td colspan="7"></td>
        </tr>
         <tr>
        <td colspan="7"></td>
        </tr>
        
         <tr>
        <td colspan="4" style="height: 5px" align="left">
            <uc1:WucStartEndDate ID="WucStartEndDate1" runat="server" />
        
            </td>
    </tr>
     
    <tr>
        <td colspan="6" style="width: 100%" align="left">
                    <table cellpadding="3" cellspacing="3" border="0" width="100%">
                            <tr>
                                <td>
                                    <asp:DataGrid ID="dg_FBTComputation" CssClass="GRID" runat="server" AutoGenerateColumns="False"
                                        Width="100%" ShowFooter="true" >
                                       
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                        <Columns>
                                        <asp:TemplateColumn HeaderText="FBT Category Id" Visible="false" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                  <asp:Label ID="lbl_FBTCategoryId" runat="server" Text=' <%#DataBinder.Eval(Container.DataItem, "FBT_Category_Id")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            
                                            <asp:TemplateColumn HeaderText="FBT Section" Visible="false" ItemStyle-Width="5%">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "FBT_Section")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>                                            
                                            
                                            <asp:TemplateColumn HeaderText="Particulars" ItemStyle-Width="15%">
                                                <ItemTemplate>
                                                 <a href="FrmFBTComputationLedgerWise.aspx?FBTCategoryId=<%#ClassLibrary.UIControl.Util.EncryptString(Convert.ToString( DataBinder.Eval(Container.DataItem,"FBT_Category_Id")))%>&Hierarchy_Code=<%# Hierarchy_Code%>&Main_Id=<%# Main_Id%>&Is_Consol=<%# Is_Consol%>&StartDate=<%# Start_Date %>&EndDate=<%# End_Date %>"
                                                 style="color: Black;  font-weight:bold; font-family:Verdana; text-decoration: none">&nbsp;<%# DataBinder.Eval(Container.DataItem, "Particulars")%></a>
<%--                                                 <asp:LinkButton ID="lnk_Particulars" runat="server"  ForeColor="black" Text='<%#DataBinder.Eval(Container.DataItem, "Particulars")%>' ></asp:LinkButton>
--%>                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Expenditure Amount" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Expenditure_Amount")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Amount Recovered" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Amount_Recovered")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Nett Expenditure" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Net_Expenditure")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Percentage as per Sec 115WC" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "Percentage")%>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Value of fringe Benefit" ItemStyle-Width="15%" ItemStyle-HorizontalAlign="right">
                                                <ItemTemplate>
                                                    <%#DataBinder.Eval(Container.DataItem, "FBT_Value")%>
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
                <td  style="width: 5%;">                       
                    </td>
                    
                <td  style="width: 5%; ">                       
                    </td>
                    
                    <td  style="width: 9%; font-weight:bold" >
                        TOTAL
                    </td>
                    <td style="width: 20%">
                        <asp:Label ID="lbl_TotExpenditureAmt"  Style="text-align: right" runat="server" Font-Bold="True" ></asp:Label>
                    </td>
                    <td style="width: 18%">
                        <asp:Label ID="lbl_TotAmtRecovered"  Style="text-align: right" runat="server" Font-Bold="True" ></asp:Label>
                    </td>
                    <td style="width: 18%">
                        <asp:Label ID="lbl_TotNetExpenditure"  Style="text-align: right" runat="server" Font-Bold="True" ></asp:Label>
                    </td>
                    <td style="width: 19%">
                        <asp:Label ID="lbl_TotPercentageAsPerSec"  Style="text-align: right" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td style="width: 21%">
                        <asp:Label ID="lbl_TotValueOfFringeBenefit"   Style="text-align: right" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
     </table>
    <table   class ="TABLE" style="width: 100%">
  <tr>
     <td class="TD1" style="width: 60%">
        Fringe Benefits Tax(User Defined Ledger) @ :
     </td>
     <td style="width: 10%" align="right" class="TD1">
         <asp:Label ID="lbl_FringeBenefitPercent" Style="text-align: left" runat="server" CssClass="LABEL"
             Font-Bold="true"></asp:Label>
         %
     </td>
     <td class="TD1" style="width: 20%" align="right">
         <asp:Label ID="lbl_FringeBenefitAmount" Style="text-align: left" runat="server" CssClass="LABEL"
             Font-Bold="true"></asp:Label>
     </td>
     <td align="right" style="width: 10%">
     </td>
    
 </tr>
 <tr>
     <td class="TD1" style="width: 60%">
        Surcharge @ :
     </td>
     <td style="width: 10%" align="right" class="TD1">
         <asp:Label ID="lbl_SurchargePercent" Style="text-align: left" runat="server" CssClass="LABEL"
             Font-Bold="true"></asp:Label>
         %
     </td>
     <td class="TD1" style="width: 20%" align="right">
         <asp:Label ID="lbl_SurchargeAmount" Style="text-align: left" runat="server" CssClass="LABEL"
             Font-Bold="true"></asp:Label>
     </td>
     <td align="right" style="width: 10%">
     </td>
    
 </tr>
 <tr>
     <td class="TD1" style="width: 60%">
       Education Cess @ :
     </td>
     <td style="width: 10%" align="right" class="TD1">
         <asp:Label ID="lbl_EducationCessPercent" Style="text-align: left" runat="server" CssClass="LABEL"
             Font-Bold="true"></asp:Label>
         %
     </td>
     <td class="TD1" style="width: 20%" align="right">
         <asp:Label ID="lbl_EducationCessAmt" Style="text-align: left" runat="server" CssClass="LABEL"
             Font-Bold="true"></asp:Label>
     </td>
     <td align="right" style="width: 10%">
     </td>
    
 </tr>
 
 <tr>
     <td class="TD1" style="width: 60%">
       Addl. Education Cess @ :
     </td>
     <td style="width: 10%" align="right" class="TD1">
         <asp:Label ID="lbl_AddEducationCessPercent" Style="text-align: left" runat="server" CssClass="LABEL"
             Font-Bold="true"></asp:Label>
         %
     </td>
     <td class="TD1" style="width: 20%" align="right">
         <asp:Label ID="lbl_AddEducationCessAmt" Style="text-align: left" runat="server" CssClass="LABEL"
             Font-Bold="true"></asp:Label>
     </td>
     <td align="right" style="width: 10%">
     </td>
    
 </tr>
 <tr>
     <td class="TD1" style="width: 60%; height: 21px;">
      Total Tax Payable (Rounded) :
     </td>
     <td style="width: 10%; height: 21px;" align="right" class="TD1">
        
         
     </td>
     <td class="TD1" style="width: 20%; " align="right">
         <asp:Label ID="lbl_TotalTaxPayableAmt" Style="text-align: left" runat="server" CssClass="LABEL"
             Font-Bold="true"></asp:Label>
     </td>
     <td align="right" style="width: 10%; height: 21px;">
     </td>
    
 </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lbl_Errors" runat="server" CssClass="ERRORLABEL" EnableViewState="false"></asp:Label></td>
        </tr>
 </table>
       
        