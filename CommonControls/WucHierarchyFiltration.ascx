<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucHierarchyFiltration.ascx.cs" Inherits="CommonControls_WucHierarchyFiltration" %>

<script type="text/javascript">

function get_hierarchy_id()
{
var ddl_hierarchy = document.getElementById('<%=ddl_hierarchy.ClientID%>');
return ddl_hierarchy.value;
}

function get_IsConsol()
{
var chk_ViewConsolidated = document.getElementById('<%=chk_ViewConsolidated.ClientID %>');

    if(get_hierarchy_id() == 'BO' || chk_ViewConsolidated == null)
    {
        return false;
    }
    else
    {
        return chk_ViewConsolidated.checked;
    }
}

function get_location_id()
{

if(get_hierarchy_id() != 0)
{
    if (get_hierarchy_id() == 'HO')
      {
        return 0;
      }
    else
      {
      var ddl_location = document.getElementById('<%=ddl_location.ClientID%>');
      return ddl_location.value;
      }
} 
else
{
return -1;
}
}


function get_hierarchy_name()
{
var ddl_hierarchy = document.getElementById('<%=ddl_hierarchy.ClientID%>');
return ddl_hierarchy.options[ddl_hierarchy.selectedIndex].text;
}

function get_location_name()
{
var ddl_location = document.getElementById('<%=ddl_location.ClientID%>');
return ddl_location.options[ddl_location.selectedIndex].text;
}
</script>


<table style="width: 100%">
    <tr>
        <td id="td_Hierarchy_caption" runat="server" class="TD1"  visible="true" style="width: 13%;"   >
            <asp:Label ID="lbl_Hierarchy_caption" runat="server" Text="Hierarchy :" />
        </td>
        
        <td id="td_Hierarchy_data" runat="server" style="width: 22%;"   >
            <asp:DropDownList ID="ddl_hierarchy" CssClass="DROPDOWN" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_hierarchy_SelectedIndexChanged"  Width="95%"/>
        </td>
        
        <td id="td_location_caption" runat="server" visible="true" class="TD1" style="width: 13%;"    >
            <asp:UpdatePanel ID="upd_lbllocation" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_location_caption" runat="server" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_hierarchy" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        
        <td id="td_location_data" runat="server" style="width: 21%;">
            <asp:UpdatePanel ID="upd_ddllocation" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table>
                        <tr>
                            <td valign="bottom">
                                <asp:DropDownList ID="ddl_location" CssClass="DROPDOWN" AutoPostBack="false" runat="server" 
                                    Width="100%" />
                            </td>
                            <td runat="server" style="width: 1%"  id="td_MDddl" align="left" class="TDMANDATORY">*
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_hierarchy" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        
        <td style="width: 9%" align="right">
            <asp:UpdatePanel ID="up_consolidated" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:CheckBox ID="chk_ViewConsolidated" runat="server" />
                </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_hierarchy" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        
        <td style="width: 27%">
        <asp:UpdatePanel ID="up_lblConsolidated" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
        <asp:Label ID="lbl_ViewConsolidated" runat="server" Text=": View Consolidated" CssClass="LABEL" Width="90%"/>
         </ContentTemplate>
                <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddl_hierarchy" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
       
    </tr>
   
    <tr visible="false">
        <asp:CheckBox ID="chk_allow_all_hierarchy" Visible="false" Checked="false" runat="server" /></tr>
</table>
