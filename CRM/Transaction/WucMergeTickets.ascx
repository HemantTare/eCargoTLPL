<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucMergeTickets.ascx.cs" Inherits="CRM_WucMergeTickets" %>
<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"  ></script>

 <asp:ScriptManager id="ScriptManager1" runat="server"></asp:ScriptManager>
 
<table class="TABLE" style="width: 100%">
    <tr>
            <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL"
                Text="MERGE TICKETS"></asp:Label>
        </td>
    </tr>
    <tr>
    <td colspan="6">&nbsp;</td>
    </tr>
    
    <tr>
        <td class="TD1" style="width: 20%;">
        &nbsp;
        </td>
        <td colspan="5" style="width: 80%;">
             <asp:Label ID="lbl_Note" runat="server" Text="Note:- From Ticket Will Delete Permanently." CssClass="LABELERROR" Font-Bold="true"></asp:Label></td>
    </tr>
     <tr>
        <td colspan="6">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
           <asp:Label ID="lbl_GcDocCaption" runat="server"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:DropDownList ID="ddl_GcDoc" runat="server" CssClass="DROPDOWN" AutoPostBack="true" OnSelectedIndexChanged="ddl_GcDoc_SelectedIndexChanged">
            </asp:DropDownList></td>
        <td class="TDMANDATORY" style="width: 1%;">
        *</td>
        <td style="width: 50%;">
        &nbsp;
        </td>
    </tr>
    
    <tr>
        <td class="TD1" style="width: 20%;">
            From Ticket :
        </td>
        <td style="width: 29%;">
        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_GcDoc"/>
                    <asp:AsyncPostBackTrigger ControlID="btn_MargeTicket"/>
                 </Triggers>
            <ContentTemplate> 
            
            <asp:DropDownList ID="ddl_FromTicket" runat="server" CssClass="DROPDOWN" AutoPostBack="true" OnSelectedIndexChanged="ddl_FromTicket_SelectedIndexChanged">
            </asp:DropDownList>
                  </ContentTemplate>
          </asp:UpdatePanel> 
            
            </td>
        <td class="TDMANDATORY" style="width: 1%;">
        *</td>
        
        <td style="width: 50%;">
          &nbsp;
        </td>
    </tr>
    
    <tr>
        <td class="TD1" style="width: 20%;">
            To Ticket :
        </td>
        <td style="width: 60%;">
            <asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_FromTicket"/>
                      <asp:AsyncPostBackTrigger ControlID="btn_MargeTicket"/>
                 </Triggers>
              <ContentTemplate> 
        
            <asp:DropDownList ID="ddl_ToTicket" runat="server" CssClass="DROPDOWN">
            </asp:DropDownList>
             </ContentTemplate>
          </asp:UpdatePanel> 
          
          </td>
        <td class="TDMANDATORY" style="width: 1%;">
        *</td>
         <td style="width: 20%;">
        &nbsp;
        </td>
    </tr>
    <tr><td>&nbsp;</td></tr>
 
     <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="UpdatePanel2" UpdateMode="Conditional" runat="server">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_MargeTicket"/>
                    <asp:AsyncPostBackTrigger ControlID="ddl_FromTicket"/>
                    <asp:AsyncPostBackTrigger ControlID="ddl_GcDoc"/>
                 </Triggers>
            <ContentTemplate> 
                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" EnableViewState="False"
               ></asp:Label>
                  </ContentTemplate>
          </asp:UpdatePanel> 
        </td>
    </tr>
    <tr>
        <td colspan="6" align="center">
            <asp:Button ID="btn_MargeTicket" runat="server" Text="Merge" CssClass="BUTTON"   OnClick="btn_MargeTicket_Click"/></td>
    </tr>
  
    <tr><td>&nbsp;</td></tr>
    <tr><td>&nbsp;</td></tr>


</table>
 
