<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucSelectedItems.ascx.cs" Inherits="CommonControls_WucSelectedItems" %>

<script type="text/javascript">
function validate_Input(f)
{
if (window.event.keyCode == 37 || window.event.keyCode == 39 || window.event.keyCode == 9 || window.event.keyCode == 16) return;
f.value = f.value.replace(/[^\,|\d]/g,"");
}

function validateTextbox()
{
    var txt_get_item = document.getElementById('<%=txt_get_item.ClientID%>');
    var hdn_gcCaption = document.getElementById('<%=hdn_gcCaption.ClientID%>');

    if (txt_get_item.value == '')
    {
        alert('please enter ' + hdn_gcCaption.value + ' no.');
        txt_get_item.focus();
        return false;
    }
    else
    {
      return true;
    }  
}

function Check_GCLength(txt)
{
    var hdn_gcCaption = document.getElementById('<%=hdn_gcCaption.ClientID%>');
    var hdn_gcMaxLength = document.getElementById('<%=hdn_gcMaxLength.ClientID%>');
    
    var Comparevalue=new Array();
    Comparevalue = txt.value.split(",");
    
    for (var i=0;i<Comparevalue.length;i++)
    {
        if(Comparevalue[i].length > hdn_gcMaxLength.value)
        {
            alert(hdn_gcCaption.value +' no can not be greater than '+ hdn_gcMaxLength.value +' digit');
            txt.focus();
            return false;
        }
    }
}

function Uppercase(Txt_Box)
{
  Txt_Box.value=Txt_Box.value.toUpperCase()
}
</script>

<table style="width:100%; font-size: 11px; font-family: Verdana;" border="0">
  <tr>
    <td class="TD1" id="td_get_item" runat="server" style="width: 20%"> 
      <asp:Label ID="lbl_get_item" runat="server" Text="Hierarchy :" />
    </td>
 
    <td id="td_get_item_data" runat="server" style="width: 80%" align="left">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
             <ContentTemplate><%--onkeyup="validate_Input(this)" onkeypress="return Check_GCLength(this)" onblur="validate_Input(this)"--%>
                  <asp:TextBox ID="txt_get_item" runat="server" Width="700px" CssClass="TEXTBOX"
                    Height="30px" TextMode="MultiLine" onkeypress="return Check_GCLength(this)" BorderWidth="1px"/>
             </ContentTemplate>
             <Triggers>
               <asp:AsyncPostBackTrigger ControlID="btn_get_data" />
             </Triggers>
        </asp:UpdatePanel>
    </td>
  </tr>
  <tr>  
    <td class="TD1" id="td_get_not_item" runat="server" style="width: 20%"> 
      <asp:Label ID="lbl_get_not_item" runat="server" Text="Hierarchy :" />
    </td>
    <td align="left" id="td_get_Not_item_data" runat="server" style="width: 80%">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
        
             <ContentTemplate><%--onkeyup="validate_Input(this)" onblur="validate_Input(this)"--%>
                  <asp:TextBox ID="txt_get_not_item" runat="server" Width="600px" CssClass="TEXTBOX"  
                    Height="30px" TextMode="MultiLine" BorderWidth="1px" ReadOnly="true"/>
     
            <asp:Button ID="btn_get_data" Width="50px" CssClass="BUTTON" runat="server" OnClientClick="return validateTextbox()" Text="GO" OnClick="btn_get_data_Click" />
            <asp:HiddenField runat="server" ID="hdn_gcCaption" Value="GC"/>
            <asp:HiddenField runat="server" ID="hdn_gcMaxLength"/>
             </ContentTemplate>
                 <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="btn_get_data" />
                 </Triggers>
           </asp:UpdatePanel>
    </td>
  </tr>
</table>
