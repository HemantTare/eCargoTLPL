<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Wuc_Region_Area_Branch.ascx.vb" Inherits="Controls_Wuc_Region_Area_Branch" %>


<script type="text/javascript">

function get_region_id()
{
var ddl_region = document.getElementById('Wuc_Region_Area_Branch1_ddl_region');
return ddl_region.value;
}

function get_area_id()
{
var ddl_area = document.getElementById('Wuc_Region_Area_Branch1_ddl_Area');
return ddl_area.value;
}

function get_branch_id()
{
var ddl_branch = document.getElementById('Wuc_Region_Area_Branch1_ddl_Branch');
return ddl_branch.value;
}


function get_region_name()
{
var ddl_region = document.getElementById('Wuc_Region_Area_Branch1_ddl_region');
return ddl_region.options[ddl_region.selectedIndex].text;
}

function get_area_name()
{
var ddl_area = document.getElementById('Wuc_Region_Area_Branch1_ddl_area');
return ddl_area.options[ddl_area.selectedIndex].text;
}

function get_branch_name()
{
var ddl_branch = document.getElementById('Wuc_Region_Area_Branch1_ddl_branch');
return ddl_branch.options[ddl_branch.selectedIndex].text;
}
</script>

<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
  <tr id="tr_ho" runat="server" visible="false">
    <td id="td_ho_caption" runat="server" class="TD1" style="width:10%;">
      <asp:Label ID="lbl_HO_Caption" runat="server" Text="HO:" />
    </td>

    <td id="td_ho_data" runat="server" style="width:10%;">
      <asp:CheckBox ID="chk_IsHO" Checked="false" AutoPostBack="true"  runat="server" />  
    </td>
  </tr>
  
  
  <tr>
    <td id="td_region_caption" runat="server" class="TD1" style="width:10%">
      <asp:Label ID="lbl_region_caption" runat="server" Text="Region:" />
    </td>
    <td id="td_region_data" runat="server" style="width:23%">
      <asp:DropDownList ID="ddl_region" CssClass="DROPDOWN" AutoPostBack="true" runat="server"/>
    </td>
    
    <td id="td_area_caption" runat="server" class="TD1" style="width:10%">
      <asp:Label ID="lbl_area_caption" runat="server" Text="Area:" />
    </td>
    <td id="td_area_data" runat="server" style="width:23%">
      <asp:DropDownList ID="ddl_Area" CssClass="DROPDOWN" AutoPostBack="true" runat="server"/>
    </td>
    
    <td id="td_branch_caption" runat="server" class="TD1" style="width:10%">
      <asp:Label ID="lbl_branch_cation" runat="server" Text="Branch:" />
    </td>
    <td id="td_branch_data" runat="server" style="width:23%">
      <asp:DropDownList ID="ddl_Branch" CssClass="DROPDOWN" AutoPostBack="false" runat="server"/>
    </td>
  </tr>
  
  
  <tr id="tr_selected_locations_only" runat="server" visible="false">
    <td id="td_region_only_caption" runat="server" class="TD1" style="width:10%;">
      <asp:Label ID="lbl_region_only_caption" runat="server" Text="Selected Region Only:" />
    </td>

    <td id="td_region_only_data" runat="server" style="width:10%;">
      <asp:CheckBox ID="chk_selected_region_only" Text="" TextAlign="Right"
       Checked="false" AutoPostBack="true" runat="server" />  
    </td>
    
    <td id="td_area_only_caption" runat="server" class="TD1" style="width:10%;">
      <asp:Label ID="lbl_area_only_caption" runat="server" Text="Selected Area Only:" />
    </td>

    <td id="td_area_only_data" runat="server" style="width:10%;">
      <asp:CheckBox ID="chk_selected_area_only" Text="" TextAlign="Right"
       Checked="false" AutoPostBack="true" runat="server" />  
    </td>
  </tr>
</table><asp:CheckBox ID="chk_show_all" Visible="false" Checked="false" runat="server" />
