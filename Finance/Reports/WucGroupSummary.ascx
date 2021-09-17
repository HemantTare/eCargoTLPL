<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucGroupSummary.ascx.cs"
  Inherits="Finance_Reports_WucGroupSummary" %>
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

<table class="TABLE" style="width: 100%">
  <tr>
    <td colspan="7" class="TDGRADIENT">
      &nbsp;<asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="GROUP SUMMARY"
        Font-Bold="True" /></td>
  </tr>
  <tr>
    <td colspan="7">
    </td>
  </tr>
  <tr>
    <td colspan="7" style="height: 5px; width: 50%" align="left">
      &nbsp;<asp:Button ID="Btn_Preview" runat="server" CssClass="BUTTON" Text="Print Preview"
        Width="181px" /></td>
  </tr>
<%--  <tr>
    <td colspan="7">
    </td>
  </tr>
--%>  <tr>
    <td colspan="7">
    </td>
  </tr>
  <tr>
    <td style="width: 15%; font-weight: bold" class="TD1">
      <asp:Label ID="lblGroup" runat="server" CssClass="LABEL" Font-Bold="true" Text="Group :"></asp:Label>
    </td>
    <td colspan="6">
      <asp:Label ID="lbl_Group" runat="server" CssClass="LABEL" Font-Bold="true" Width="100%"></asp:Label>
    </td>
  </tr>
  <tr>
    <td style="width: 15%; font-weight: bold" class="TD1">
      <asp:Label ID="lblUnder" runat="server" CssClass="LABEL" Font-Bold="true" Text="Under :"></asp:Label>
    </td>
    <td>
      <asp:Label ID="lbl_Under" runat="server" CssClass="LABEL" Font-Bold="true" Width="100%"></asp:Label>
    </td>
    <td align="center" style="width: 50%">
      <asp:Label ID="lbl_Company_Name" runat="server" ForeColor="Maroon" Font-Bold="True"
        Font-Italic="True"></asp:Label><br />
    </td>
  </tr>
  <tr>
    <td colspan="7" style="width: 100%">
      <%--   <uc1:WucStartEndDate ID="Wuc_StartEndDate" runat="server"  /> --%>
      <uc2:WucGridSearch ID="WucGridSearch1" runat="server" />
    </td>
  </tr>
  <tr>
    <td colspan="7">
    </td>
  </tr>
  <tr>
    <td colspan="4">
      <asp:DataGrid ID="dgGroupSummary" CssClass="GRID" runat="server" ShowFooter="True"
        AllowSorting="True" AutoGenerateColumns="False" OnItemDataBound="dgGroupSummary_RowDataBound"
        OnPageIndexChanged="dgGroupSummary_PageIndexChanged" AllowPaging="True" PageSize="15"
        PagerStyle-Mode="NumericPages" OnPreRender="dgGroupSummary_PreRender">
        <HeaderStyle CssClass="GRIDHEADERCSS" />
        <FooterStyle CssClass="GRIDFOOTERTB" HorizontalAlign="Right" />
        <Columns>
          <asp:TemplateColumn FooterText="GRAND TOTAL">
            <ItemTemplate>
              <asp:HyperLink runat="server" ID="lnkname" Text='<%# DataBinder.Eval(Container.DataItem,"Name") %>'
                NavigateUrl='<%#"FrmGroupSummary.aspx?Menu_Item_Id=" + ClassLibraryMVP.Util.EncryptInteger(MenuItemId) + "&Mode=" + ClassLibraryMVP.Util.EncryptInteger(Mode) + "&IsConsolidated=" + Is_Consol + "&Hierarchy_Code=" + Hierarchy_Code + "&Main_Id=" + Main_Id +"&Id=" +ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Id"))) +"&Category=" +ClassLibraryMVP.Util.EncryptInteger(Convert.ToInt32(DataBinder.Eval(Container.DataItem,"Category")))  +"&StartDate=" + ClassLibrary.UIControl.Util.EncryptString(Convert.ToString(StartDate)) + "&EndDate=" + ClassLibrary.UIControl.Util.EncryptString(Convert.ToString(EndDate)) + "&Group=" + Convert.ToString(DataBinder.Eval(Container.DataItem,"Name")) + "&Under=" + Group + "&DivisionId=" + DivisionId%>'
                CssClass="LINKREPORTS"></asp:HyperLink>
            </ItemTemplate>
            <HeaderStyle Width="22%" HorizontalAlign="Left" />
            <FooterStyle HorizontalAlign="Left" />
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Debit">
            <ItemTemplate>
              <%# Eval("OpeningDr").ToString() %>
            </ItemTemplate>
            <HeaderStyle Width="13%" HorizontalAlign="Right" />
            <ItemStyle HorizontalAlign="Right" />
            <FooterTemplate>
              <%# FootTotal_OP_DR().ToString() %>
            </FooterTemplate>
            <FooterStyle CssClass="GRIDFOOTERTB" />
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Credit">
            <ItemTemplate>
              <%# Eval("OpeningCr").ToString()%>
            </ItemTemplate>
            <HeaderStyle Width="13%" HorizontalAlign="Right" />
            <ItemStyle HorizontalAlign="Right" />
            <FooterTemplate>
              <%# FootTotal_OP_CR().ToString() %>
            </FooterTemplate>
            <FooterStyle CssClass="GRIDFOOTERTB" />
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Debit">
            <ItemTemplate>
              <%# Eval("CurrentDr").ToString()%>
            </ItemTemplate>
            <HeaderStyle Width="13%" HorizontalAlign="Right" />
            <ItemStyle HorizontalAlign="Right" />
            <FooterTemplate>
              <%# FootTotal_CRNT_DR().ToString() %>
            </FooterTemplate>
            <FooterStyle CssClass="GRIDFOOTERTB" />
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Credit">
            <ItemTemplate>
              <%# Eval("CurrentCr").ToString()%>
            </ItemTemplate>
            <HeaderStyle Width="13%" HorizontalAlign="Right" />
            <ItemStyle HorizontalAlign="Right" />
            <FooterTemplate>
              <%# FootTotal_CRNT_CR().ToString() %>
            </FooterTemplate>
            <FooterStyle CssClass="GRIDFOOTERTB" />
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Debit">
            <ItemTemplate>
              <%# Eval("ClosingDr").ToString()%>
            </ItemTemplate>
            <HeaderStyle Width="13%" HorizontalAlign="Right" />
            <ItemStyle HorizontalAlign="Right" />
            <FooterTemplate>
              <%# FootTotal_CL_DR().ToString()%>
            </FooterTemplate>
            <FooterStyle CssClass="GRIDFOOTERTB" />
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="Credit">
            <ItemTemplate>
              <%# Eval("ClosingCr").ToString()%>
            </ItemTemplate>
            <HeaderStyle Width="13%" HorizontalAlign="Right" />
            <ItemStyle HorizontalAlign="Right" />
            <FooterTemplate>
              <%# FootTotal_CL_CR().ToString() %>
            </FooterTemplate>
            <FooterStyle CssClass="GRIDFOOTERTB" />
          </asp:TemplateColumn>
          <asp:TemplateColumn HeaderText="ID" Visible="False">
            <ItemTemplate>
              <%#DataBinder.Eval(Container.DataItem, "Id")%>
            </ItemTemplate>
            <HeaderStyle HorizontalAlign="Left" />
          </asp:TemplateColumn>
        </Columns>
      </asp:DataGrid>
    </td>
  </tr>
  <tr>
    <td colspan="5">
      <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR"></asp:Label></td>
  </tr>
</table>
