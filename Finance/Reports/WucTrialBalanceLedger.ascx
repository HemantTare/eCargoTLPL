<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTrialBalanceLedger.ascx.cs" Inherits="Finance_Reports_WucTrialBalanceLedger" %>
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
     // For Print Preview
    function Open_Show_Window(Path)
    {            
        queryString = Path;
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-190);
        var popH = (h-75);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                  
        window.open(queryString, 'FMainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes') ;
        return false;
    }
    </script>
      
<table  class="TABLE" style="width: 100%">
        <tr>
            <td colspan="7"  class="TDGRADIENT">
                 &nbsp;<asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="TRIAL BALANCE (LEDGER)" Font-Bold="True" />
                </td>
        </tr>
        <tr>
        <td colspan="7"></td>
        </tr>
        
         <tr>
        <td colspan="2" style="height: 5px;width:50%" align="left">
            &nbsp;<asp:Button ID="Btn_Preview" runat="server" CssClass="BUTTON" Text="Print Preview" Width="181px" /></td>
        <td align="center" style="height: 5px;width:50%">
                <asp:Label id="lbl_Company_Name" runat="server" ForeColor="Maroon" Font-Bold="True" Font-Italic="True"></asp:Label>&nbsp;<br />
            </td>
    </tr>
    
    <tr>
        <td colspan="7">     
            <uc2:WucGridSearch ID="WucGridSearch1" runat="server" />
       
        </td>
    </tr>
    
    <tr>
        <td colspan="7">
        </td>
    </tr>
    
    <tr>
        <td colspan="4">
            <asp:DataGrid id="dgTrialBalance" runat="server" OnItemDataBound="dgTrialBalance_RowDataBound" AutoGenerateColumns="False" AllowSorting="True" CssClass="GRID" ShowFooter="True">
             <HeaderStyle CssClass="GRIDHEADERCSS" />
             <FooterStyle CssClass="GRIDFOOTERTB" HorizontalAlign="Right" />
             <Columns>
                    <asp:TemplateColumn  FooterText="GRAND TOTAL">
                        <ItemTemplate>
                        
                        <asp:HyperLink runat="server" ID="lnkname" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>' 
                        NavigateUrl='<%#"FrmGroupSummary.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + ClassLibraryMVP.Util.EncryptInteger(Mode) + "&IsConsolidated=" + Is_Consol + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id +"&Id=" +ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Id"))) +"&Category=" + ClassLibraryMVP.Util.EncryptInteger(2) +"&StartDate=" + ClassLibrary.UIControl.Util.EncryptString(Convert.ToString(StartDate)) + "&EndDate=" + ClassLibrary.UIControl.Util.EncryptString(Convert.ToString(EndDate)) + "&Group=" + Convert.ToString(DataBinder.Eval(Container.DataItem,"Name"))%>'
                        CssClass="LINKREPORTS"></asp:HyperLink>                        

                        </ItemTemplate>
                        <HeaderStyle Width="22%"   HorizontalAlign="Left" />
                        <FooterStyle  HorizontalAlign="Left" />
                   
                    </asp:TemplateColumn>
                    
                    <asp:TemplateColumn HeaderText="Debit">  
                        <ItemTemplate> 
                            <%# Eval("OpeningDr").ToString()%>
                        </ItemTemplate>
                        <HeaderStyle Width="13%"   HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                     <FooterTemplate>
                           <%# FootTotal_OP_DR().ToString() %>
                     </FooterTemplate> 
                      <FooterStyle  CssClass="GRIDFOOTERTB" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Credit"  >
                        <ItemTemplate>
                            <%# Eval("OpeningCr").ToString()%>
                        </ItemTemplate>
                        <HeaderStyle Width="13%"   HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <%# FootTotal_OP_CR().ToString() %>  
                        </FooterTemplate> 
                         <FooterStyle  CssClass="GRIDFOOTERTB" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Debit" >
                        <ItemTemplate>
                            <%# Eval("CurrentDr").ToString()%>
                        </ItemTemplate>
                        <HeaderStyle Width="13%"   HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                         <FooterTemplate>
                            <%# FootTotal_CRNT_DR().ToString() %>
                        </FooterTemplate> 
                        <FooterStyle  CssClass="GRIDFOOTERTB" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Credit" >
                        <ItemTemplate>
                            <%# Eval("CurrentCr").ToString()%>
                        </ItemTemplate>
                        <HeaderStyle Width="13%"   HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <%# FootTotal_CRNT_CR().ToString() %>  
                        </FooterTemplate> 
                        <FooterStyle  CssClass="GRIDFOOTERTB" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Debit" >
                        <ItemTemplate>
                            <%# Eval("ClosingDr").ToString()%>
                        </ItemTemplate>
                        <HeaderStyle Width="13%"   HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                        <FooterTemplate>
                            <%# FootTotal_CL_DR().ToString()%>  
                        </FooterTemplate>
                        <FooterStyle  CssClass="GRIDFOOTERTB" />
                    </asp:TemplateColumn>

                    <asp:TemplateColumn HeaderText="Credit" >
                        <ItemTemplate>
                            <%# Eval("ClosingCr").ToString()%>
                        </ItemTemplate>
                        <HeaderStyle Width="13%"   HorizontalAlign="Right" />
                        <ItemStyle HorizontalAlign="Right" />
                         <FooterTemplate>
                            <%# FootTotal_CL_CR().ToString() %>  
                        </FooterTemplate>
                        <FooterStyle  CssClass="GRIDFOOTERTB" />
                    </asp:TemplateColumn>
<%--                 <asp:BoundColumn DataField="Category" HeaderText="Cat" Visible="False" />
                  <asp:TemplateColumn HeaderText="ID"   Visible="False" >
                        <ItemTemplate>
                         <%#DataBinder.Eval(Container.DataItem, "Id")%> 
                        </ItemTemplate>
                        <HeaderStyle   HorizontalAlign="Left" />
                    </asp:TemplateColumn>   --%>        
                </Columns>
              </asp:DataGrid>
            </td>
        </tr>
    <tr>
        <td colspan="4">
        </td>
    </tr>
             <tr>
                <td colspan="4">
                <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR"></asp:Label></td>
            </tr>
     
    </table>