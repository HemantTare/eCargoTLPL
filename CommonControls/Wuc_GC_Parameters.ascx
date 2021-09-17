<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Wuc_GC_Parameters.ascx.cs" Inherits="CommonControls_Wuc_GC_Parameters" %>

<script type="text/javascript">

function get_bkg_type_id()
{
var ddl_Bkg_type = document.getElementById('Wuc_GC_Parameters1_ddl_Bkg_type');
return ddl_Bkg_type.value;
}

function get_dly_type_id()
{
var ddl_dly_type = document.getElementById('Wuc_GC_Parameters1_ddl_dly_type');
return ddl_dly_type.value;
}

function get_payment_type_id()
{
var ddl_payment_type = document.getElementById('Wuc_GC_Parameters1_ddl_payment_type');
return ddl_payment_type.value;
}


function get_Bkg_type()
{
var ddl_Bkg_type = document.getElementById('Wuc_GC_Parameters1_ddl_Bkg_type');
return ddl_Bkg_type.options[ddl_Bkg_type.selectedIndex].text;
}

function get_dly_type()
{
var ddl_dly_type = document.getElementById('Wuc_GC_Parameters1_ddl_dly_type');
return ddl_dly_type.options[ddl_dly_type.selectedIndex].text;
}

function get_payment_type()
{
var ddl_payment_type = document.getElementById('Wuc_GC_Parameters1_ddl_payment_type');
return ddl_payment_type.options[ddl_payment_type.selectedIndex].text;
}
</script>

<table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
  <tr>
    <td id="td_Bkg_type_caption" runat="server" class="TD1" style="width:10%">
      <asp:Label ID="lbl_Bkg_type_caption" runat="server" Text="Bkg Type:" />
    </td>
    <td id="td_Bkg_type_data" runat="server" style="width:23%">
      <asp:DropDownList ID="ddl_Bkg_type" CssClass="DROPDOWN" AutoPostBack="false" runat="server"/>
    </td>
    
    <td id="td_dly_type_caption" runat="server" class="TD1" style="width:10%">
      <asp:Label ID="lbl_dly_type_caption" runat="server" Text="Dly Type:" />
    </td>
    <td id="td_dly_type_data" runat="server" style="width:23%">
      <asp:DropDownList ID="ddl_dly_type" CssClass="DROPDOWN" AutoPostBack="false" runat="server"/>
    </td>
    
    <td id="td_payment_type_caption" runat="server" class="TD1" style="width:10%">
      <asp:Label ID="lbl_payment_type_cation" runat="server" Text="Pymt Type:" />
    </td>
    <td id="td_payment_type_data" runat="server" style="width:23%">
      <asp:DropDownList ID="ddl_payment_type" CssClass="DROPDOWN" AutoPostBack="false" runat="server"/>
    </td>
  </tr>
</table>
