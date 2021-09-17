<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Search_Text.ascx.vb" Inherits="Search_Text" %>
<script type="text/javascript" >
function Search_Text(text_search)
{
   
  var SearchRange = document.body.createTextRange();
  if(text_search.value=='')
  {
    alert('Please Enter Text To be Searched');
    text_search.focus();
    return false;
  }
  if(SearchRange.findText(text_search.value))
  {
      SearchRange.select();
      SearchRange.scrollIntoView();
  }
  else{
    alert('No Data Found !')
    text_search.focus();
    }

  return false;
  
}

</script>
 <table width="100%" >
                <tr>
                    <td class="TD1" style="width: 90%">
                            <asp:TextBox runat="server" id="SearchText" onfocus ="this.select()"  BorderWidth ="1px" CssClass="TEXTBOX"/>
                    </td>          
              
                    <td class="TD1" style="width: 10%">
                            <asp:Button ID="btn_Search" runat="server" CssClass="BUTTON" Width="100%" Text="Search" />
                    </td>
                </tr>
 </table>