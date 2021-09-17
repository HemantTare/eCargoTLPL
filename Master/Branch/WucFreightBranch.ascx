<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucFreightBranch.ascx.cs"
  Inherits="Master_Branch_WucFreightBranch" %>

<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>

<asp:ScriptManager ID="scm_FreightBranch" runat="server">
</asp:ScriptManager>

<script type="text/javascript">

function NewWindow()
    {
        var hdnQueryString = document.getElementById('WucFreightBranch1_hdnQueryString');
        var Path='FrmFreightBranchCopy.aspx?mode='+ hdnQueryString.value;
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-20);
        var popH = (h-250);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                
          window.open(Path, 'CustomPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes');
          return false;
    }


</script>

<table style="width: 100%" class="TABLE">
  <tr>
    <td class="TDGRADIENT" colspan="7">
      &nbsp;<asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="FREIGHT BRANCH TO BRANCH"
        meta:resourcekey="lbl_HeadingResource1"></asp:Label>
        
      <asp:HiddenField ID="hdnQueryString" runat="server" />
    </td>
  </tr>
  <tr>
    <td colspan="7">
      &nbsp;
    </td>
  </tr>
  <tr>
    <td class="TD1" style="width: 15%">
      <asp:Label ID="lbl_FromBranch" runat="server" Text="From Branch:" CssClass="LABEL"
        meta:resourcekey="lbl_FromBranchResource1"></asp:Label></td>
    <td style="width: 20%">
      <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="dg_FreightBranch" />
        </Triggers>
        <ContentTemplate>
          <asp:DropDownList ID="ddl_Branch" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
            OnSelectedIndexChanged="ddl_Branch_SelectedIndexChanged" meta:resourcekey="ddl_BranchResource1">
          </asp:DropDownList>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 10%" class="TD1">
      <asp:Label ID="lbl_ToArea" runat="server" Text="To Area:" CssClass="LABEL" meta:resourcekey="lbl_ToAreaResource1"></asp:Label></td>
    <td style="width: 20%">
      <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="dg_FreightBranch" />
        </Triggers>
        <ContentTemplate>
          <asp:DropDownList ID="ddl_Area" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
            OnSelectedIndexChanged="ddl_Area_SelectedIndexChanged" meta:resourcekey="ddl_AreaResource1">
          </asp:DropDownList>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
    <td style="width: 10%" class="TD1">
      <asp:Label ID="lbl_Commodity" runat="server" Text="Commodity:" CssClass="LABEL" /></td>
    <td style="width: 20%">
      <asp:DropDownList ID="ddl_Commodity" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_Commodity_SelectedIndexChanged" />
    </td>
    <td style="width: 5%">
      <asp:Button ID="btn_Copy" runat="server" CssClass="BUTTON" OnClick="btn_Copy_Click"
        OnClientClick="return NewWindow()" Text="Copy" meta:resourcekey="btn_CopyResource1" /></td>
  </tr>
  <tr>
    <td colspan="7">
      <asp:UpdatePanel ID="Upd_Pnl_dg_FreightBranch" runat="server">
        <Triggers>
          <asp:AsyncPostBackTrigger ControlID="ddl_Area" />
          <asp:AsyncPostBackTrigger ControlID="ddl_Branch" />
        </Triggers>
        <ContentTemplate>
          <asp:DataGrid ID="dg_FreightBranch" runat="server" AutoGenerateColumns="False" CssClass="GRID"
            AllowPaging="True" AllowSorting="True" Width="100%" CellPadding="2" PageSize="15"
            OnCancelCommand="dg_FreightBranch_CancelCommand" OnEditCommand="dg_FreightBranch_EditCommand"
            OnItemDataBound="dg_FreightBranch_ItemDataBound" OnPageIndexChanged="dg_FreightBranch_PageIndexChanged"
            OnUpdateCommand="dg_FreightBranch_UpdateCommand" meta:resourcekey="dg_FreightBranchResource1">
            <HeaderStyle CssClass="GRIDHEADERCSS" />
            <Columns>
              <asp:BoundColumn DataField="From_Branch_Id">
                <ItemStyle CssClass="HIDEGRIDCOL" />
                <HeaderStyle CssClass="HIDEGRIDCOL" />
              </asp:BoundColumn>
              <asp:TemplateColumn>
                <ItemTemplate>
                  <asp:Label ID="lbl_ToBranchId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "To_Branch_Id") %>'
                    meta:resourcekey="lbl_ToBranchIdResource1"></asp:Label>
                </ItemTemplate>
                <ItemStyle CssClass="HIDEGRIDCOL" />
                <HeaderStyle CssClass="HIDEGRIDCOL" />
              </asp:TemplateColumn>
              <asp:TemplateColumn>
                <ItemTemplate>
                  <asp:Label ID="lbl_FreightId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FreightId") %>'
                    meta:resourcekey="lbl_FreightIdResource1"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                  <asp:Label ID="lbl_FreightId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FreightId") %>'
                    meta:resourcekey="lbl_FreightIdResource2"></asp:Label>
                </EditItemTemplate>
                <ItemStyle CssClass="HIDEGRIDCOL" />
                <HeaderStyle CssClass="HIDEGRIDCOL" />
              </asp:TemplateColumn>
              <asp:TemplateColumn HeaderText="To Branch">
                <ItemTemplate>
                  <%# DataBinder.Eval(Container.DataItem, "Branch_Name") %>
                </ItemTemplate>
              </asp:TemplateColumn>
              <asp:TemplateColumn HeaderText="Rate per parcel(Regular)">
                <ItemTemplate>
                  <asp:Label ID="lbl_FreightRate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FrieghtRate") %>'
                    Font-Names="Verdana" meta:resourcekey="lbl_FreightRateResource1"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                  <asp:TextBox ID="txt_FreightRate" runat="server" onkeypress="return Only_Numbers(this,event)"
                    onblur="return valid(this)" onfocus="this.select()" Text='<%# DataBinder.Eval(Container.DataItem, "FrieghtRate") %>'
                    Font-Names="Verdana" BorderWidth="1px" CssClass="TEXTBOXNOS" MaxLength="9" meta:resourcekey="txt_FreightRateResource1"></asp:TextBox>
                </EditItemTemplate>
              </asp:TemplateColumn>
              <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" UpdateText="Update" HeaderText="Edit"
                meta:resourcekey="EditCommandColumnResource1"></asp:EditCommandColumn>
              <asp:TemplateColumn HeaderText="View" Visible="False">
                <ItemTemplate>
                  <asp:LinkButton ID="lnk_btn_view" runat="server" meta:resourcekey="lnk_btn_viewResource1">View</asp:LinkButton>
                </ItemTemplate>
              </asp:TemplateColumn>
            </Columns>
            <PagerStyle Mode="NumericPages" />
          </asp:DataGrid>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
  </tr>
  <tr>
    <td colspan="6" style="font-weight: bold; font-size: 11px; font-family: Verdana">
      <asp:Label ID="lbl_Updated" runat="server" BorderStyle="Solid" BorderWidth="1px"
        CssClass="UPDATEDLBL" Width="50px" meta:resourcekey="lbl_UpdatedResource1"></asp:Label>&nbsp;
      Updated &nbsp;
      <asp:Label ID="lbl_NotUpdated" runat="server" BorderStyle="Solid" BorderWidth="1px"
        CssClass="NOTUPDATEDLBL" Width="50px" meta:resourcekey="lbl_NotUpdatedResource1"></asp:Label>&nbsp;
      Not Updated
    </td>
  </tr>
  <tr>
    <td colspan="6">
    </td>
  </tr>
  <tr>
    <td colspan="6">
      <asp:UpdatePanel ID="up_error" runat="server">
        <ContentTemplate>
          <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
            meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
        </ContentTemplate>
      </asp:UpdatePanel>
    </td>
  </tr>
</table>
