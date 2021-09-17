<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucEscalationMatrix.ascx.cs" Inherits="CRM_Masters_WucEscalationMatrix" %>
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script language="javascript" type="text/javascript">

function OpenProfile(Profile_Id,ComplaintNatureId)
  {
  var Path ='';
  Path='../Transaction/FrmIncludeExcludeUser.aspx?Profile_Id=' + Profile_Id + '&Complaint_Nature_Id=' + ComplaintNatureId ;
  var w = screen.availWidth;
  var h = screen.availHeight;
  var popW = (w-90);
  var popH = (h-290);
  var leftPos = (w-popW)/2;
  var topPos = (h-popH)/2;
  window.open(Path, 'Profile_Window', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, res izable=yes,scrollbars=yes');
  return false;
  }
  </script>
  
  <asp:ScriptManager ID="scm_EscalationMatrix" runat="server"></asp:ScriptManager>
<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="ESCALATION MATRIX"
                ></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6">
            &nbsp;
        </td>
    </tr>
    
    <tr>
    <td style="width: 20%" class="TD1">
    <asp:Label ID="lbl_ComplaintNature" runat="server" Text="Complaint Nature:" ></asp:Label></td>
    <td  colspan="5"><asp:DropDownList ID="ddl_ComplaintNature" runat="server" AutoPostBack="true" CssClass="DROPDOWN" Width="50%"  OnSelectedIndexChanged="ddl_ComplaintNature_SelectedIndexChanged"  ></asp:DropDownList>
    <asp:Label ID="lbl_Mandatory" runat="Server" CssClass="TDMANDATORY" EnableViewState="False" Text="*" /> 
    </td>
</tr>
<tr>
   <td colspan="6" style="width:100%">
  
    <asp:UpdatePanel ID="UpdatePanel_DgEscalationMatrix" runat="server" UpdateMode="Conditional">
      <ContentTemplate>
       <asp:Panel ID="pnl_Escalation_Matrix" runat="server" GroupingText="Escalation Matrix Details " CssClass="PANEL"
                Width="100%">
                <div class="DIV" id="Div_Escalation_Matrix" style="height: 150px; width: 100%">
         <asp:DataGrid ID="dgEscalationMatrix" runat="server" AutoGenerateColumns="False" CellPadding="3" CssClass="Grid" ShowFooter="True" Width="100%" OnItemDataBound="dgEscalationMatrix_ItemDataBound" OnItemCommand="dgEscalationMatrix_ItemCommand" 
         OnCancelCommand="dgEscalationMatrix_CancelCommand" OnDeleteCommand="dgEscalationMatrix_DeleteCommand" OnEditCommand="dgEscalationMatrix_EditCommand" >
         <PagerStyle Mode="NumericPages" />
        <AlternatingItemStyle CssClass="GridAlternateRowCss" />
        <FooterStyle CssClass="GridFooterCss" />
        <HeaderStyle CssClass="GridHeaderCss" /> 
        <Columns>
        
            <asp:TemplateColumn HeaderText="Profile Id" Visible="false">
                    <ItemTemplate>
                     <asp:Label ID="lbl_ProfileId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Profile_Id") %>' />
                    </ItemTemplate>
            </asp:TemplateColumn>
                    
             <asp:TemplateColumn HeaderText="Profile"> 
              <HeaderStyle Width="30%" />
              <ItemStyle Width="30%" />
                <ItemTemplate>
                 <asp:LinkButton ID="lnk_Profile" runat="server"  Text='<%# DataBinder.Eval(Container.DataItem, "Profile_Name") %>'></asp:LinkButton>
                 </ItemTemplate>
                 <EditItemTemplate>
                  <asp:DropDownList ID="ddl_Profile" CssClass="DROPDOWN"  runat="server"></asp:DropDownList>
                 </EditItemTemplate>
                 <FooterTemplate>
                    <asp:DropDownList ID="ddl_Profile" CssClass="DROPDOWN"  runat="server"></asp:DropDownList>
                 </FooterTemplate>
               </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Time From">
                <HeaderStyle Width="20%" />
                <ItemStyle Width="20%" />
                 <ItemTemplate>
                     <%# Eval("Time_From") %>
                 </ItemTemplate>
                 <EditItemTemplate>
                   <asp:DropDownList ID="ddl_TimeFrom" CssClass="DROPDOWN" runat="server" ></asp:DropDownList>
                 </EditItemTemplate>
                 <FooterTemplate>
                   <asp:DropDownList ID="ddl_TimeFrom" CssClass="DROPDOWN" runat="server"></asp:DropDownList>
                    </FooterTemplate>
               </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Time To">
                <HeaderStyle Width="20%" />
                <ItemStyle Width="20%" />
                 <ItemTemplate>
                     <%# Eval("Time_To") %>
                 </ItemTemplate>
                 <EditItemTemplate>
                   <asp:DropDownList ID="ddl_TimeTo"  CssClass="DROPDOWN" runat="server"></asp:DropDownList>
                 </EditItemTemplate>
                 <FooterTemplate>
                   <asp:DropDownList ID="ddl_TimeTo" CssClass="DROPDOWN" runat="server"></asp:DropDownList>
                  </FooterTemplate>
                   </asp:TemplateColumn>
               <asp:EditCommandColumn  CancelText="Cancel" EditText="Edit" HeaderText="Edit" UpdateText="Update" ></asp:EditCommandColumn>
               <asp:TemplateColumn HeaderText="Add/Delete">
               <HeaderStyle Width="10%" />
                <ItemStyle Width="10%" />
                 <ItemTemplate>
                   <asp:LinkButton ID="lnk_Delete" runat="server" CommandName="Delete" Text="Delete" ></asp:LinkButton>
                 </ItemTemplate>
                 <FooterTemplate>
                   <asp:LinkButton ID="lnk_Add" runat="server" CommandName="Add" Text="Add" ></asp:LinkButton>
                 </FooterTemplate>
                 </asp:TemplateColumn>
         </Columns> 
                                                                
         </asp:DataGrid>
         </div>
         </asp:Panel>
      </ContentTemplate>
      <Triggers>
        <asp:AsyncPostBackTrigger ControlID="ddl_ComplaintNature" />
        <asp:AsyncPostBackTrigger ControlID="dgEscalationMatrix" />
      </Triggers>
    </asp:UpdatePanel>     
   </td>
</tr>
<tr>
 <td colspan="6">
   <asp:UpdatePanel ID="updatePnl1" runat="Server" UpdateMode="Conditional">
    <ContentTemplate>
      <asp:Label ID="lbl_Errors" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"  ></asp:Label>
   </ContentTemplate>
   <Triggers>
    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
    <asp:AsyncPostBackTrigger ControlID="dgEscalationMatrix" />
   </Triggers>
   </asp:UpdatePanel>
   </td>
</tr>
<tr> 
 <td colspan="6" style="width:100%;text-align:center; height: 26px;">
  <asp:Button ID="btn_Save" runat="Server" Text="Save" CssClass="BUTTON" OnClick="btn_Save_Click" />
 </td>
</tr>
 <tr>
      <td colspan="6">&nbsp;</td>
 </tr>
 <tr>
    <td colspan="6">
          <asp:Label ID="Label1" CssClass="LABELERROR" runat="server" Text=" Fields with * mark are mandatory"></asp:Label>
    </td>
    </tr>
</table>