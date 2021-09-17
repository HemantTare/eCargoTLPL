<%@ Control Language="VB" AutoEventWireup="false" CodeFile="WucHierarchyWithID.ascx.vb" Inherits="Controls_WucHierarchyWithID" %>

<script type="text/javascript">

function get_hierarchy_id()
{
var ddl_hierarchy = document.getElementById('<%=ddl_hierarchy.ClientID%>');
return ddl_hierarchy.value;
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


<table style="width: 100%; font-size: 11px; font-family: Verdana;">
    <tr>
        <td id="td_Hierarchy_caption" runat="server" class="TD1"  visible="true" style="width: 20%;"   >
            <asp:Label ID="lbl_Hierarchy_caption" runat="server" Text="Hierarchy :" />
        </td>
        <td id="td_Hierarchy_data" runat="server" style="width: 29%;"   >
            <asp:DropDownList ID="ddl_hierarchy" CssClass="DROPDOWN" AutoPostBack="true" runat="server" />
        </td>
        <td style="width: 1%;" class="TDMANDATORY">
            *</td>
        <td id="td_location_caption" runat="server" visible="true" class="TD1" style="width: 20%;"    >
            <asp:UpdatePanel ID="upd_lbllocation" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_location_caption" runat="server" Text="Area"  />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_hierarchy" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td id="td_location_data" runat="server" style="width: 29%;">
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
    </tr>
    <tr visible="false">
        <asp:CheckBox ID="chk_allow_all_hierarchy" Visible="false" Checked="false" runat="server" />
        <asp:CheckBox ID="chk_selectone_or_all" Visible="false" Checked="true" runat="server" />
     </tr>
</table>
