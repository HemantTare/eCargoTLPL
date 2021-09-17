<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_Mac_Id.aspx.cs" Inherits="Administration_Rights_Frm_Mac_Id" %>

<%@ Register Src="../../CommonControls/WucHierarchyWithID.ascx" TagName="WucHierarchyWithID"
  TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Mac Id</title>
  <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
<script language="javascript" type="text/javascript">
function ValidateUI()
{
return confirm("Are you Sure to Save")
}
</script>
  <form id="form1" runat="server">
    <div>
      <asp:ScriptManager ID="scm_Profile" runat="server">
      </asp:ScriptManager>
      <table class="TABLE" style="width: 100%">
        <tr>
          <td class="TDGRADIENT" colspan="6">
            &nbsp;<asp:Label ID="lbl_Header" runat="server" CssClass="HEADINGLABEL" Text="MAC ID"
              meta:resourcekey="lbl_HeaderResource1"></asp:Label></td>
        </tr>
        <tr>
          <td style="width: 20%">
            &nbsp;
          </td>
          <td style="width: 29%">
          </td>
          <td style="width: 1%">
          </td>
          <td style="width: 20%">
          </td>
          <td style="width: 29%">
          </td>
          <td style="width: 1%">
          </td>
        </tr>
        <tr>
          <td colspan="6" style="width: 100%">
            <uc1:WucHierarchyWithID ID="WucHierarchyWithID1" runat="server" />
          </td>
        </tr>
        <tr>
          <td colspan="6" width="%100%">
            <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                <asp:AsyncPostBackTrigger ControlID="WucHierarchyWithID1" />
                <asp:AsyncPostBackTrigger ControlID="dg_Mac_Id" />
              </Triggers>
              <ContentTemplate>
                <asp:DataGrid ID="dg_Mac_Id" AutoGenerateColumns="False" ShowFooter="True" CellPadding="3"
                  CssClass="Grid" runat="server" OnCancelCommand="dg_Mac_Id_CancelCommand" OnDeleteCommand="dg_Mac_Id_DeleteCommand"
                  OnItemCommand="dg_Mac_Id_ItemCommand" OnItemDataBound="dg_Mac_Id_ItemDataBound"
                  OnUpdateCommand="dg_Mac_Id_UpdateCommand" OnEditCommand="dg_Mac_Id_EditCommand"
                  meta:resourcekey="dg_Mac_IdResource1" Width="100%">
                  <ItemStyle HorizontalAlign="Left" />
                  <HeaderStyle Height="15px" Font-Size="11px" Font-Names="Verdana" Font-Bold="True"
                    HorizontalAlign="Left" ForeColor="Black" BorderStyle="Solid" BorderColor="#9495A2"
                    BorderWidth="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader">
                  </HeaderStyle>
                  <Columns>
                    <asp:TemplateColumn HeaderText="Computer Name">
                      <FooterTemplate>
                        <asp:TextBox ID="txt_Computer_Name" CssClass="TEXTBOX" MaxLength="25" runat="server"
                          BorderWidth="1px" />
                      </FooterTemplate>
                      <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "Computer_Name") %>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:TextBox ID="txt_Computer_Name" CssClass="TEXTBOX" MaxLength="25" runat="server"
                          BorderWidth="1px" />
                      </EditItemTemplate>
                      <HeaderStyle Width="40%" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Mac ID">
                      <FooterTemplate>
                        <asp:TextBox ID="txt_Mac_Id" CssClass="TEXTBOX" MaxLength="25" runat="server" BorderWidth="1px" />
                      </FooterTemplate>
                      <ItemTemplate>
                        <%# DataBinder.Eval(Container.DataItem, "MAc_Id") %>
                      </ItemTemplate>
                      <EditItemTemplate>
                        <asp:TextBox ID="txt_Mac_Id" CssClass="TEXTBOX" MaxLength="25" runat="server" BorderWidth="1px" />
                      </EditItemTemplate>
                      <HeaderStyle Width="40%" />
                    </asp:TemplateColumn>
                    <asp:EditCommandColumn UpdateText="Update" CancelText="Cancel" EditText="Edit" HeaderText="Edit">
                      <HeaderStyle Width="10%" />
                    </asp:EditCommandColumn>
                    <asp:TemplateColumn HeaderText="Delete">
                      <FooterTemplate>
                        <asp:LinkButton ID="lbtn_Add" Text="Add" runat="server" CommandName="Add" />
                      </FooterTemplate>
                      <ItemTemplate>
                        <asp:LinkButton ID="lbtn_Delete" runat="server" Text="Delete" CommandName="Delete" /></ItemTemplate>
                      <HeaderStyle Width="10%" />
                    </asp:TemplateColumn>
                  </Columns>
                </asp:DataGrid>
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
        </tr>
        <tr>
          <td align="center" colspan="6">
            <asp:Button ID="btn_save" runat="server" CssClass="BUTTON" Text="Save" OnClick="btn_Save_Click"
              OnClientClick="return ValidateUI()" /></td>
        </tr>
        <tr>
          <td colspan="6">
            <asp:UpdatePanel ID="Upd_Pnl_Mac" UpdateMode="Conditional" runat="server">
              <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                <asp:AsyncPostBackTrigger ControlID="dg_Mac_Id" />
              </Triggers>
              <ContentTemplate>
                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"></asp:Label>
              </ContentTemplate>
            </asp:UpdatePanel>
          </td>
        </tr>
      </table>
    </div>

    <script type="text/javascript">
       self.parent.hideload();
    </script>

  </form>
</body>
</html>
