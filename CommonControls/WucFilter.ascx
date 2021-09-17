<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucFilter.ascx.cs" Inherits="CommonControls_WucFilter" %>
<%@ Register Src="WucDatePicker.ascx" TagName="WucDatePicker" TagPrefix="uc1" %>

<%--<script type="text/javascript" src="../Javascript/Common.js"></script>
--%>

<script type="text/javascript">
function ddl_column_change()
{
var ddl_column = document.getElementById('<%=ddl_column.ClientID%>');

var td_txtbox = document.getElementById('<%=td_txtbox.ClientID%>');
var td_datepicker = document.getElementById('<%=td_datepicker.ClientID%>');
var td_truefalse = document.getElementById('<%=td_truefalse.ClientID%>');

var txt_search = document.getElementById('<%=txt_search.ClientID%>');
var ddl_truefalse = document.getElementById('<%=ddl_truefalse.ClientID%>');


var column_values = ddl_column.value.split(',');
var datatype = column_values[0];

if (datatype == 's' || datatype == 'n')
  {
  //txt_search.value = '';
  td_txtbox.style.display='inline';
  td_datepicker.style.display='none';
  td_truefalse.style.display='none';
  }
if (datatype == 'b')
  {
  ddl_truefalse.value = '1';
  td_txtbox.style.display='none';
  td_datepicker.style.display='none';
  td_truefalse.style.display='inline';
  }
if (datatype == 'd')
  {
  td_txtbox.style.display='none';
  td_datepicker.style.display='inline';
  td_truefalse.style.display='none';
  }  
}
</script>

<table id="tbl_filter" runat="server" border="0" cellpadding="0" cellspacing="0" style="width: 100%">
  <tr>
    <td id="td_column_caption" runat="server" class="TD1" style="width:10%">
      <asp:Label ID="lbl_column_caption" runat="server" Text="Column:" />
    </td>
    <td id="td_column_data" runat="server" style="width:23%">
      <asp:DropDownList ID="ddl_column" onchange="ddl_column_change()" CssClass="DROPDOWN" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddl_column_SelectedIndexChanged">
      </asp:DropDownList>
    </td>

    <td id="td_criteria_caption" runat="server" class="TD1" style="width:10%">
      <asp:Label ID="lbl_criteria_caption" runat="server" Text="Criteria:" />
    </td>
    <td id="td_criteria_data" runat="server" style="width:23%">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
                  <asp:DropDownList ID="ddl_criteria" AutoPostBack="false" CssClass="DROPDOWN" runat="server"/>
             </ContentTemplate>
             <Triggers>
               <asp:AsyncPostBackTrigger ControlID="ddl_column"/>
             </Triggers>
        </asp:UpdatePanel>
    </td>
    
    <td id="td_searchfor_Caption" runat="server" class="TD1" style="width:10%">
      <asp:Label ID="lbl_SearchFor_caption" runat="server" Text="Search For:" />
    </td> 
       
     <td id="td_searchfor_data" runat="server" style="width:23%">
       <table width="100%">
       <tr>
       <td id = "td_txtbox" runat="server">
       <asp:TextBox ID="txt_search" CssClass="TEXTBOX" runat="server"></asp:TextBox>
       </td>
       
       <td id = "td_datepicker" runat="server">
       <uc1:WucDatePicker ID="WucDatePicker1" runat="server" />  
       </td>
       
       <td id = "td_truefalse" runat="server">
        <asp:DropDownList ID="ddl_truefalse" AutoPostBack="true" CssClass="DROPDOWN" runat="server">
          <asp:ListItem Value="1">True</asp:ListItem>
          <asp:ListItem Value="0">False</asp:ListItem>
        </asp:DropDownList>
       </td>
       </tr>
       </table>
    </td>
  </tr>
</table>
