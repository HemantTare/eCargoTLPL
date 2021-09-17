<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucMultiDocuPrintgGrid.ascx.cs"
    Inherits="Printing_WucMultiDocuPrintgGrid" %>
<%@ Register Src="~/CommonControls/WucLinkName.ascx" TagName="WucLinkName" TagPrefix="uc2" %>
<%@ Register Src="~/CommonControls/WucSearch.ascx" TagName="WucSearch" TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc3" %>

<script type="text/javascript" src="../Javascript/Common.js"></script>

<script type="text/javascript">
    
    function Open_View_Window(Path)
    {

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-40);
        var leftPos = (w-popW)/2;
        var topPos = 0;

      if(Path == '')
      {
        alert('You do not have rights for view !');
      }
      else
      {
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
      }
        return false;
    } 
    
    function Open_Print_Window(Path)
    {

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;              
         
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    } 
    
</script>

<asp:ScriptManager ID="scm_printgrid" runat="server">
</asp:ScriptManager>
<table class="TABLE">
    <tr>
        <td colspan="4">
            <uc2:WucLinkName ID="Link" runat="server" />
        </td>
    </tr>
    <tr>
        <td style="width: 20%;" class="TD1">
            <asp:Label ID="lbl_DocumentType" runat="server" CssClass="LABEL" Font-Bold="True"
                Text="Select Document :" Width="132px"></asp:Label></td>
        <td style="width: 30%;">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_DocumentType" runat="server" CssClass="DROPDOWN" style="width: 100%;" AutoPostBack="True"
                        OnSelectedIndexChanged="ddl_DocumentType_SelectedIndexChanged">
                    </asp:DropDownList>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 20%;">
        </td>
        <td style="width: 30%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%; text-align: right">
            <asp:Label ID="lblMemoNoForPrint" runat="server" Text="Invoice No.:" CssClass="LABEL" Font-Bold="True"></asp:Label>
        </td>
        <td style="width: 40%; text-align: left;">
            <asp:TextBox ID="txtMemoNoForPrint" runat="server" CssClass="TEXTBOX" style="width: 100%; text-align: left;"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%; text-align: right">
        </td>
        <td style="width: 30%; text-align: left">
        </td>
        <td style="width: 20%; text-align: right">
        </td>
        <td style="width: 30%; text-align: left">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%; text-align: right">
        </td>
        <td style="width: 30%; text-align: right">
            <asp:Button ID="btnPrint" runat="server" Text="Print" CssClass="BUTTON" OnClick="btnPrint_Click" /></td>
        <td style="width: 20%; text-align: right">
        </td>
        <td style="width: 30%; text-align: left">
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="4">
        </td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_DocumentType" /> 
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
