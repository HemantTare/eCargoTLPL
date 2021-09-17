<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucRightsIrrespective.ascx.cs" Inherits="Administration_Rights_WucRightsIrrespective" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../Javascript/Administration/Rights/RightsIrrespective.js"></script>


<asp:ScriptManager ID="scm_RightsIrrespective" runat="server">
</asp:ScriptManager>

<table width="100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="3">
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="RIGHTS IRRESPECTIVE"
                meta:resourcekey="lbl_HeadingResource1"></asp:Label>
        </td>
    </tr>
    <tr>
        <td style="width: 20%;" class="TD1">
            Menu System Name:</td>
        <td style="width: 78%;">
            <asp:DropDownList ID="ddl_MenuSystem" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                OnSelectedIndexChanged="ddl_MenuSystem_SelectedIndexChanged" meta:resourcekey="ddl_MenuSystemResource1">
            </asp:DropDownList></td>
        <td style="width: 2%;" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            Menu Head Name:</td>
        <td style="width: 78%">
            <asp:UpdatePanel ID="Upd_Pnl_ddl_MenuHead" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuSystem" />
                </Triggers>
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_MenuHead" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                        meta:resourcekey="ddl_MenuHeadResource1" OnSelectedIndexChanged="ddl_MenuHead_SelectedIndexChanged">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
            Menu Group Name:</td>
        <td style="width: 78%">
            <asp:UpdatePanel ID="Upd_Pnl_ddl_MenuGroup" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuHead" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuSystem" />
                </Triggers>
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_MenuGroup" runat="server" CssClass="DROPDOWN" AutoPostBack="True"
                        meta:resourcekey="ddl_MenuGroupResource1" OnSelectedIndexChanged="ddl_MenuGroup_SelectedIndexChanged">
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td style="width: 2%" class="TDMANDATORY">
            *</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Menu Item Name:</td>
        <td style="width: 78%">
            <asp:UpdatePanel ID="Upd_Pnl_ddl_MenuItem" runat="server">
                <ContentTemplate>
                    <asp:DropDownList ID="ddl_MenuItem" runat="server" AutoPostBack="True" CssClass="DROPDOWN" OnSelectedIndexChanged="ddl_MenuItem_SelectedIndexChanged" meta:resourcekey="ddl_MenuItemResource1"
                        >
                    </asp:DropDownList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 2%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            Assign Rights</td>
        <td style="width: 78%">
        
        <asp:UpdatePanel ID="Upd_Pnl_Rights" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
  
          <asp:DataGrid ID="dg_Rights" runat="server" AutoGenerateColumns="False"
                Width="98%" CssClass="GRID" AllowSorting="True"  ShowFooter="True"
                DataKeyField="Heirarchy_Code" OnItemDataBound="dg_Rights_ItemDataBound" meta:resourcekey="dg_RightsResource1"  >
            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
            <HeaderStyle CssClass="GRIDHEADERCSS" />
            <FooterStyle CssClass="GRIDFOOTERCSS" />
            
                 
              <Columns>
                  <asp:BoundColumn HeaderText="Hierarchy Code" DataField="Heirarchy_Code" FooterText="Select Column">
                      <FooterStyle Font-Bold="True" />
                  </asp:BoundColumn>
                  
                  <asp:TemplateColumn HeaderText="Can Add">
                      <ItemTemplate>
                          <asp:CheckBox ID="chk_CanAdd" runat="server" 
                          Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Can_Add")) %>'
                              meta:resourcekey="chk_CanAddResource1" />
                      </ItemTemplate>
                      <FooterTemplate>
                          <asp:CheckBox ID="chk_CanAddFoot" runat="server" 
                          onclick="SelectColoumn(this,'dg_Rights','Can_Add')"
                              meta:resourcekey="chk_CanAddFootResource1" />
                      </FooterTemplate>
                  </asp:TemplateColumn>
                  
                  <asp:TemplateColumn HeaderText="Can Edit">
                      <ItemTemplate>
                          <asp:CheckBox ID="chk_CanEdit" runat="server" 
                          Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Can_Edit")) %>'
                              meta:resourcekey="chk_CanEditResource1" />
                      </ItemTemplate>
                      <FooterTemplate>
                          <asp:CheckBox ID="chk_CanEditFoot" runat="server"
                           onclick="SelectColoumn(this,'dg_Rights','Can_Edit')"
                              meta:resourcekey="chk_CanEditFootResource1" />
                      </FooterTemplate>
                  </asp:TemplateColumn>
                  
                  <asp:TemplateColumn HeaderText="Can Cancel">
                      <ItemTemplate>
                          <asp:CheckBox ID="chk_CanCancel" runat="server" 
                          Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Can_Cancel")) %>' meta:resourcekey="chk_CanCancelResource1"
                               />
                      </ItemTemplate>
                      <FooterTemplate>
                          <asp:CheckBox ID="chk_CanCancelFoot" runat="server" 
                          onclick="SelectColoumn(this,'dg_Rights','Can_Cancel')" meta:resourcekey="chk_CanCancelFootResource1"
                              />
                      </FooterTemplate>
                  </asp:TemplateColumn>
                  <asp:TemplateColumn HeaderText="Can Read">
                      <ItemTemplate>
                          <asp:CheckBox ID="chk_CanRead" runat="server" 
                          Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Can_Read")) %>'
                              meta:resourcekey="chk_CanReadResource1" />
                      </ItemTemplate>
                      <FooterTemplate>
                          <asp:CheckBox ID="chk_CanReadFoot" runat="server" 
                          onclick="SelectColoumn(this,'dg_Rights','Can_Read')"
                              meta:resourcekey="chk_CanReadFootResource1" />
                      </FooterTemplate>
                  </asp:TemplateColumn>
                  <asp:TemplateColumn HeaderText="Select Row">
                      <ItemTemplate>
                          <asp:CheckBox ID="chk_SelectRow" runat="server" 
                          onclick="SelectRow(this,'dg_Rights')"
                              meta:resourcekey="chk_SelectRowResource1" />
                      </ItemTemplate>
                  </asp:TemplateColumn>
              </Columns>
                 
            </asp:DataGrid>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_MenuSystem" />
                <asp:AsyncPostBackTrigger ControlID="ddl_MenuHead" />
                <asp:AsyncPostBackTrigger ControlID="ddl_MenuGroup" />
                <asp:AsyncPostBackTrigger ControlID="ddl_MenuItem" />
        </Triggers>
        </asp:UpdatePanel>
        </td>
        <td class="TDMANDATORY" style="width: 2%">
        </td>
    </tr>
    <tr id="tr_Transacation" runat="server">
        <td class="TD1" style="width: 20%">
        </td>
        <td>
            Select <a id="A3" href="javaScript:SelectAllNone('A','dg_Rights')">All </a>
            , <a id="A4" href="javaScript:SelectAllNone('N','dg_Rights')">None</a>
        </td>
        <td>
        </td>
    </tr>
    <tr>
        <td colspan="3">
        </td>
    </tr>
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"
                meta:resourcekey="btn_SaveResource1" /></td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_RightsIrrespective" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuSystem" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuHead" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuGroup" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_MenuItem" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                </Triggers>
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                        meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
</table>
