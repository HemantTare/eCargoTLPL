<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucLedgerAgeing.ascx.cs" Inherits="Finance_Reports_WucLedgerAgeing" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>
<%@ Register Src="~/CommonControls/WucStartEndDate.ascx" TagName="WucStartEndDate" TagPrefix="uc1" %>


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
    </script>

<asp:ScriptManager ID= "SM_LedgerAgeing" runat="server">
</asp:ScriptManager>

<style type="text/css">
.GRIDFIXHEADER
{ 
POSITION: relative; TOP: expression(this.offsetParent.scrollTop);BACKGROUND-COLOR:#E2E2E2;  border:solid 2px #9495A2; FONT-SIZE: 11px; FONT-FAMILY: Verdana;color:Black;font-weight:bold  ;    }
  .invisible {display:none}    
}
</style>

<table class="TABLE" width="100%">
    <tr>
        <td class="TDGRADIENT" colspan="5">
            &nbsp;<asp:Label ID="lbl_Heading" runat="server" Text="LEDGER AGEING OUTSTANDINGS"
                CssClass="HEADINGLABEL"></asp:Label></td>
    </tr>
    <tr>
        <td class="TD1" colspan="5">
            &nbsp;</td>
    </tr>
    <tr>
        <td style="width: 15%; font-weight: bold;" class="TD1">
            Ledger Name :</td>
        <td class="TD" style="width: 40%">
            <asp:Label ID="lbl_Ledger_Name" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label></td>
     
        <td class="TD" colspan="1" style="width: 15%" align="right">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btn_Details" runat="server" CssClass="BUTTON" Text="View Detailed"  OnClick="btn_Details_Click" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Details" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TD" colspan="1" style="width: 15%">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="font-weight: bold; width: 15%">
        </td>
        <td class="TD" style="width: 40%">
        </td>
        <td align="right" class="TD" colspan="1" style="width: 15%">
        </td>
        <td class="TD" colspan="1" style="width: 15%">
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="4">
        <uc1:WucStartEndDate ID="WucStartEndDate" runat="server" />
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="4" style="font-weight: bold">
        </td>
    </tr>
    <tr>
        <td colspan="5" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
                    <td>
                         <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                <tr>
                                    <td align="left"> 
                                     <div id="Div1" runat="server" class="DIV" style="height: 450px; text-align: left;">                                     
                                            <asp:UpdatePanel ID="UpdatePanel_Ledger_Outstandings" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <asp:DataGrid ID="dg_Ageing_Outstanding_Grid" runat="server" AutoGenerateColumns="False"
                                                        CellPadding="3" CssClass="GRID" ShowFooter="True" Width="98%" OnItemDataBound="dg_Ageing_Outstanding_Grid_ItemDataBound">
                                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                        <PagerStyle Mode="NumericPages" CssClass="GRIDVIEWPAGERCSS" />
                                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                         <%-- <HeaderStyle CssClass="GRIDFIXHEADER" />--%>
                                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                        <Columns>
                                                         <asp:TemplateColumn Visible="false">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Ledger_Id")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>
                                                            
                                                             <asp:TemplateColumn Visible="false">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "HO")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>
                                                            
                                                             <asp:TemplateColumn Visible="false">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Region")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>
                                                            
                                                             <asp:TemplateColumn Visible="false">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Area")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>
                                                            
                                                             <asp:TemplateColumn Visible="false">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Branch")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>
                                                            
                                                             <asp:TemplateColumn Visible="false">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Ledger_Name")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>
                                                            
                                                            <asp:TemplateColumn HeaderText="Date" ItemStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Bill_Date")%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    Total Amount :
                                                                </FooterTemplate>
                                                                <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                <FooterStyle Font-Bold="True" ForeColor="#993300" HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Ref No." ItemStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Ref_No")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>
                                                            
                                                             <asp:TemplateColumn Visible="false" HeaderText="Ref Type">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Ref_Type")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>
                                                            
                                                             <asp:TemplateColumn Visible="False"  HeaderText="Voucher Date" ItemStyle-Font-Italic="true">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Voucher_Date")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="7%" HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>
                                                            
                                                             <asp:TemplateColumn Visible="False" HeaderText="Voucher Type" ItemStyle-Font-Italic="true">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Voucher_Type")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="7%" HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>
                                                            
                                                              <asp:TemplateColumn Visible="False" ItemStyle-Font-Italic="true">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Voucher_No")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="8%" HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>   
                                                            
                                                              <asp:TemplateColumn Visible="False" ItemStyle-Font-Italic="true">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Amount")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="8%" HorizontalAlign="Right" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateColumn>                                                         
                                                            
                                                            
                                                             <asp:TemplateColumn HeaderText="Opening Amount" ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Opening_Amount")%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lbl_Total_Opening_Amount" runat="server" CssClass="LABEL"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle Width="10%" HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" ForeColor="#993300" HorizontalAlign="Right" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="Pending Amount" ItemStyle-HorizontalAlign="Right">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Pending_Amount")%>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="lbl_Total_Pending_Amount" runat="server" CssClass="LABEL"></asp:Label>
                                                                </FooterTemplate>
                                                                <HeaderStyle Width="10%" HorizontalAlign="Right" />
                                                                <FooterStyle Font-Bold="True" ForeColor="#993300" HorizontalAlign="Right" />
                                                            </asp:TemplateColumn>
                                                            
                                                            <asp:TemplateColumn Visible="false">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Due_Date")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn Visible="false">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "OverDueDays")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>
                                                            
                                                             <asp:TemplateColumn Visible="False" ItemStyle-Font-Italic="true">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Date")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="7%" HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>
                                                            
                                                            <asp:TemplateColumn Visible="False" ItemStyle-Font-Italic="true">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Voucher_Name")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>
                                                            
                                                             <asp:TemplateColumn Visible="false">
                                                                <ItemTemplate>
                                                                    <%#DataBinder.Eval(Container.DataItem, "Voucher_ID")%>
                                                                </ItemTemplate>
                                                                <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                            </asp:TemplateColumn>                                                      
                                                            
                                                            
                                                           
                                                            
                                                          
                                                          
                                                           
                                                            
                                                            <%--   <asp:TemplateColumn Visible="false">
                                                        <ItemTemplate>
                                                            <%#DataBinder.Eval(Container.DataItem, "Ref_Type_ID")%>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="10%" HorizontalAlign="Left" />
                                                    </asp:TemplateColumn>--%>
                                                           
                                                        </Columns>
                                                    </asp:DataGrid>
                                                
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="dg_Ageing_Outstanding_Grid" />
                                                    <%--<asp:AsyncPostBackTrigger ControlID="btn_Change_Period" />--%>
                                                    <asp:AsyncPostBackTrigger ControlID="btn_Details" />
                                                </Triggers>
                                            </asp:UpdatePanel> 
                                            </div>                                      
                                    </td>
                                </tr>
                         </table>
                    </td>
                </tr>
            </table>
            </td> 
    </tr>
</table>
