<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucUserRights.ascx.cs" Inherits="Admin_Rights_WucUserRights" %>

<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../JavaScript/Administration/Rights/UserRights.js"></script>

<asp:ScriptManager ID="scm_UserRights" runat="server"></asp:ScriptManager>
<table width="100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="3">
            <asp:Label ID="lbl_Heading" CssClass = "HEADINGLABEL" runat="server" Text="USER RIGHTS" meta:resourcekey="lbl_HeadingResource1"></asp:Label>
        </td>        
    </tr>        
    <tr>
        <td style="width: 20%; height: 24px;" class="TD1">Hierarchy Name:</td>
        <td style="width: 78%; height: 24px;">
            <asp:DropDownList ID="ddl_Hierarchy" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_Hierarchy_SelectedIndexChanged" meta:resourcekey="ddl_HierarchyResource1">
            </asp:DropDownList></td>
        <td style="width: 100px; height: 24px;" class="TDMANDATORY">*</td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">Profile Name:</td>
        <td style="width: 78%">
        <asp:UpdatePanel ID="Upd_Pnl_ddl_Profile" UpdateMode="Conditional" runat="server">
                <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="ddl_Hierarchy" />
                </Triggers>
                <ContentTemplate>
                        <asp:DropDownList ID="ddl_Profile" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_Profile_SelectedIndexChanged" meta:resourcekey="ddl_ProfileResource1">
            </asp:DropDownList>
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>
        <td style="width: 2%" class="TDMANDATORY">*</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">User Name:</td>
        <td style="width: 78%">
          <asp:UpdatePanel ID="Upd_Pnl_ddl_MenuSystem" UpdateMode="Conditional" runat="server">
                <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="ddl_Hierarchy" />   
                     <asp:AsyncPostBackTrigger ControlID="ddl_Profile" />
                 </Triggers>
                <ContentTemplate>
            <asp:DropDownList ID="ddl_UserName" runat="server"  CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_UserName_SelectedIndexChanged" meta:resourcekey="ddl_UserNameResource1">
            </asp:DropDownList>
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>
        <td class="TDMANDATORY" style="width: 2%">
            *</td>
    </tr>
    <tr>
        <td style="width: 20%; " class="TD1">Menu System Name:</td>
        <td style="width: 78%; ">
            <asp:DropDownList ID="ddl_MenuSystem" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_MenuSystem_SelectedIndexChanged" meta:resourcekey="ddl_MenuSystemResource1">
            </asp:DropDownList></td>
        <td style="width: 2%; " class="TDMANDATORY">*</td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">Menu Head Name:</td>
        <td style="width: 78%">
        <asp:UpdatePanel ID="Upd_Pnl_ddl_MenuHead" UpdateMode="Conditional" runat="server">
                <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="ddl_MenuSystem" />
                 </Triggers>
                <ContentTemplate>             
            <asp:DropDownList ID="ddl_MenuHead" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_MenuHead_SelectedIndexChanged" meta:resourcekey="ddl_MenuHeadResource1">
            </asp:DropDownList>
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>
        <td style="width: 2%" class="TDMANDATORY">*</td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">Menu Group Name:</td>
        <td style="width: 78%">
         <asp:UpdatePanel ID="Upd_Pnl_ddl_MenuGroup" UpdateMode="Conditional" runat="server">
                <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="ddl_MenuHead" />
                     <asp:AsyncPostBackTrigger ControlID="ddl_MenuSystem" />
                 </Triggers>
                <ContentTemplate>
                        <asp:DropDownList ID="ddl_MenuGroup" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_MenuGroup_SelectedIndexChanged" meta:resourcekey="ddl_MenuGroupResource1">
            </asp:DropDownList>
            </ContentTemplate>
            </asp:UpdatePanel>
            </td>
        <td style="width: 2%" class="TDMANDATORY">*</td>
    </tr>
 
    <tr>
        <td style="width: 20%" class="TD1" valign="top">Assign Rights:</td>
        <td >
        <asp:UpdatePanel ID="Upd_Pnl_MenuItemsRights" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
  
          <asp:DataGrid ID="dg_MenuItemsRights" runat="server" AutoGenerateColumns="False"
                Width="98%" CssClass="GRID" AllowSorting="True"  ShowFooter="True"
                DataKeyField="MenuItem_ID" OnItemDataBound="dg_MenuItemsRights_ItemDataBound" meta:resourcekey="dg_MenuItemsRightsResource1"  >
            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
            <HeaderStyle CssClass="GRIDHEADERCSS" />
            <FooterStyle CssClass="GRIDFOOTERCSS" />           
                 
                <Columns>
                <asp:BoundColumn HeaderText="Menu Items" DataField="MenuItem_Name" FooterText="Select Column">
                    <FooterStyle Font-Bold="True" />
                </asp:BoundColumn>                  
                     
                     <asp:TemplateColumn HeaderText="Can Read">                        
                        <ItemTemplate>
                               <asp:CheckBox ID="chk_CanRead" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Can_Read")) %>' meta:resourcekey="chk_CanReadResource1"/>
                        </ItemTemplate>                     
                         <FooterTemplate>
                             <asp:CheckBox ID="chk_CanReadFoot" runat="server" onclick="SelectColoumn(this,'dg_MenuItemsRights','Can_Read')" meta:resourcekey="chk_CanReadFootResource1"/>
                        </FooterTemplate>     
                    </asp:TemplateColumn>                                        
                    <asp:TemplateColumn HeaderText="Can Add" >                       
                        <ItemTemplate>
                               <asp:CheckBox ID="chk_CanAdd" runat="server"  Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Can_Add")) %>' meta:resourcekey="chk_CanAddResource1"/>
                        </ItemTemplate> 
                    <FooterTemplate>
                             <asp:CheckBox ID="chk_CanAddFoot" runat="server"   onclick="SelectColoumn(this,'dg_MenuItemsRights','Can_Add')" meta:resourcekey="chk_CanAddFootResource1"/>
                        </FooterTemplate>    
                    </asp:TemplateColumn>                    
                    
                    <asp:TemplateColumn HeaderText="Can Edit">                        
                        <ItemTemplate>
                               <asp:CheckBox ID="chk_CanEdit" runat="server"  Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Can_Edit")) %>' meta:resourcekey="chk_CanEditResource1"/>
                        </ItemTemplate>                         
                         <FooterTemplate>
                             <asp:CheckBox ID="chk_CanEditFoot" runat="server"    onclick="SelectColoumn(this,'dg_MenuItemsRights','Can_Edit')" meta:resourcekey="chk_CanEditFootResource1"  />
                        </FooterTemplate>
                    </asp:TemplateColumn>                    
                    <asp:TemplateColumn HeaderText="Can Delete">                        
                        <ItemTemplate>
                               <asp:CheckBox ID="chk_CanDelete" runat="server" Checked='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "Can_Delete")) %>' meta:resourcekey="chk_CanDeleteResource1"/>
                        </ItemTemplate>
                        <FooterTemplate>
                         <asp:CheckBox ID="chk_CanDeleteFoot" runat="server"    onclick="SelectColoumn(this,'dg_MenuItemsRights','Can_Delete')" meta:resourcekey="chk_CanDeleteFootResource1"/>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    
                      <asp:TemplateColumn HeaderText="Select Row">                        
                        <ItemTemplate>
                               <asp:CheckBox ID="chk_SelectRow" runat="server" onclick="SelectRow(this,'dg_MenuItemsRights')" meta:resourcekey="chk_SelectRowResource1" />
                        </ItemTemplate>                        
                    </asp:TemplateColumn>                    
                 </Columns>
                 
            </asp:DataGrid>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_Profile" />
                <asp:AsyncPostBackTrigger ControlID="ddl_Hierarchy" />
                <asp:AsyncPostBackTrigger ControlID="ddl_MenuHead" />
                <asp:AsyncPostBackTrigger ControlID="ddl_MenuSystem" />
                <asp:AsyncPostBackTrigger ControlID="ddl_MenuGroup" />
                <asp:AsyncPostBackTrigger ControlID="ddl_UserName" />
        </Triggers>
        </asp:UpdatePanel>
        </td>
        <td></td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td>
            Select
            <a id="A1" href="javaScript:SelectAllNone('A','dg_MenuItemsRights')">
                All
            </a>, <a id="A2" href="javaScript:SelectAllNone('N','dg_MenuItemsRights')">
                None</a>
        </td>
        <td>
        </td>
    </tr>       
    <tr>
        <td align="center" colspan="3">
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click"  OnClientClick="return ValidateUI()" meta:resourcekey="btn_SaveResource1" />
        </td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:UpdatePanel ID="Upd_Pnl_Profile_Rights_Save" UpdateMode="Conditional" runat="server">
                <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="btn_Save"/>
                     <asp:AsyncPostBackTrigger ControlID="ddl_Profile" />
                     <asp:AsyncPostBackTrigger ControlID="ddl_Hierarchy" /> 
                     <asp:AsyncPostBackTrigger ControlID="ddl_MenuHead" />
                     <asp:AsyncPostBackTrigger ControlID="ddl_MenuSystem" />
                     <asp:AsyncPostBackTrigger ControlID="ddl_MenuGroup" />
                     <asp:AsyncPostBackTrigger ControlID="ddl_UserName" />                
                </Triggers>
                <ContentTemplate>
	                <asp:Label  ID="lbl_Errors"  runat="server" CssClass="LABELERROR" EnableViewState="False" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
                </ContentTemplate>
          </asp:UpdatePanel>
    </td>
    </tr>
</table>
<asp:HiddenField ID="hdf_ResourceString" runat="server" />
