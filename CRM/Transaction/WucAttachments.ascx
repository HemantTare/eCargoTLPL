<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucAttachments.ascx.cs"
    Inherits="CommonControls_WucAttachments" %>

<script type="text/javascript">
function LoadAttachmentForm(AttachmentName)
  {
  if (AttachmentName == '')
    {
      alert("Please Select Attachment");
    }
   else
   {
    var hdn_baseURL = document.getElementById('<%=hdn_baseURL.ClientID%>');
    var Path =''
    Path=hdn_baseURL.value + AttachmentName //+ '/FrmShowAttachment.aspx?AttachmentName=' + AttachmentName;

    var w = screen.availWidth;
    var h = screen.availHeight;
    var popW = (w-400);
    var popH = (h-290);
    var leftPos = (w-popW)/2;
    var topPos = (h-popH)/2;
    window.open(Path, 'ShowAttachments', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=yes, resizable=yes,scrollbars=yes');
    return false;
    }
  }
  
  function  CloseForm()
  {
      self.close();
      return false;
  }
  
</script>

<%--<asp:ScriptManager ID = "scm_iamges" runat="server"></asp:ScriptManager>--%>
<table class="TABLE">

    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL"
                Text="ATTACHMENTS"></asp:Label>
        </td>
    </tr>

    <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
                <tr>
                    <td>
<%--                        <asp:UpdatePanel ID="upd_pnl_dg_Attachment" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dg_AttachmentsGrid" />
                            </Triggers>
                            <ContentTemplate>--%>
                                <asp:Panel ID="pnl_Attachment" runat="server" GroupingText="Attachment Details" CssClass="PANEL"
                                    Width="100%" meta:resourcekey="pnl_SpecificationResource1">
                                    <table cellpadding="3" cellspacing="3" border="0" width="100%">
                                        <tr>
                                            <td style="width: 100%;">
                                                <asp:DataGrid  ID="dg_AttachmentsGrid" AutoGenerateColumns="False" ShowFooter="True"
                                                    CellPadding="3" CssClass="GRID" runat="server" OnItemDataBound="dg_AttachmentsGrid_ItemDataBound"
                                                    OnItemCommand="dg_AttachmentsGrid_ItemCommand" OnEditCommand="dg_AttachmentsGrid_EditCommand"
                                                    OnCancelCommand="dg_AttachmentsGrid_CancelCommand" OnUpdateCommand="dg_AttachmentsGrid_UpdateCommand"
                                                    OnDeleteCommand="dg_AttachmentsGrid_DeleteCommand">
                                                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                    <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                    <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                    <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderText="Sr No.">
                                                            <ItemTemplate>
                                                                <%#SrNo++%>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn> 
                                                        <asp:TemplateColumn HeaderText="Title">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtn_Description" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "Title")%>' />
                                                            </ItemTemplate>
                                                            <FooterTemplate>
                                                                <asp:TextBox ID="txt_Description" MaxLength="250" runat="server" CssClass="TEXTBOX"
                                                                    Width="90%"></asp:TextBox>
                                                            </FooterTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txt_Description" MaxLength="250" runat="server" CssClass="TEXTBOX"
                                                                    Width="90%"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="25%" />
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="">
                                                            <FooterTemplate>
                                                                <asp:FileUpload ID="fileUpload_Drawing" runat="server" />
                                                            </FooterTemplate>
                                                            <EditItemTemplate>
                                                                <asp:FileUpload ID="fileUpload_Drawing" runat="server" />
                                                            </EditItemTemplate>
                                                            <HeaderStyle Width="45%" />
                                                        </asp:TemplateColumn>
                                                        <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit">
                                                            <HeaderStyle Width="10%" />
                                                        </asp:EditCommandColumn>
                                                        <asp:TemplateColumn HeaderText="Delete">
                                                            <FooterTemplate>
                                                                <asp:LinkButton ID="lbtn_Add" Text="Add" runat="Server" CommandName="Add" />
                                                            </FooterTemplate>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lbtn_Delete" runat="Server" Text="Delete" CommandName="Delete" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="5%" />
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
<%--                            </ContentTemplate>
                        </asp:UpdatePanel>--%>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btn_Close" CssClass="BUTTON" runat="server" Text="Close" OnClientClick="return CloseForm()"/></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
           <asp:Label ID="lbl_Errors" runat="Server" CssClass="LABELERROR" EnableViewState="false"
                Text="" />
            <asp:HiddenField ID="hdn_baseURL" runat="server" />
        </td>
    </tr>
</table>
