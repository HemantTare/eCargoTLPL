<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTicketResolution.ascx.cs" Inherits="CRM_Transaction_WucTicketResolution" %>
<script language="javascript" type="text/javascript" src="../../Javascript/CRM/Transaction/TicketResolution.js"></script>
<asp:ScriptManager ID="scm_TicketResolution" runat="server">
</asp:ScriptManager>
<table style="width: 100%" class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="CLOSE TICKET/RESOLUTION ENTRY"
                ></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="6" style="width: 25%">
            &nbsp;
        </td>
    </tr>
   
   <%-- <tr>
     <td style="width: 20%" class="TD1">
    <asp:Label ID="lbl_SearchBy" runat="server" CssClass="LABEL" Text="Search By:" ></asp:Label></td>
     <td style="width: 25%" >
     <asp:RadioButtonList ID="rbl_SearchBy" runat="server" Font-Size="11px" RepeatDirection="Horizontal"
                AutoPostBack="True" OnSelectedIndexChanged="rbl_SearchBy_SelectedIndexChanged" >
                <asp:ListItem Value="0">Ticket No</asp:ListItem>
                <asp:ListItem Value="1">GC No</asp:ListItem>
            </asp:RadioButtonList>
            </td>
            <td style="width: 5%">
        </td>
    </tr>       --%>
    
    <tr>
     <td style="width: 35%" class="TD1" >
    <asp:Label ID="lbl_TicketNo" runat="server" CssClass="LABEL" Text="Ticket No:" ></asp:Label></td>
    <td style="width: 65%" >
    <asp:Label ID="lbl_Ticket" runat="server" CssClass="LABEL" Font-Bold="True"> </asp:Label>
     <%--<td style="width: 1%" class="TDMANDATORY">--%><%--</td>--%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
   <%-- <td style="width: 20%;text-align: left" class="TD1">--%>
    <asp:Label ID="lbl_GCDocketNo" runat="server" CssClass="LABEL"></asp:Label>&nbsp;&nbsp;&nbsp
    <%--<td style="width: 29%;text-align: left" >--%>
    <asp:Label ID="lbl_GCDocket" runat="server" CssClass="LABEL" Font-Bold="True"></asp:Label>
    <%--<td style="width: 1%" class="TDMANDATORY">--%><%--</td>--%>
    <%--<td style="width: 10%">
        </td>--%>
    </tr>
                 
    <%--<tr>
        <td style="width: 20%" class="TD1">--%>
            <%--<asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                    <%--<asp:Label ID="lbl_Head" runat="server" CssClass="LABEL"></asp:Label></td>--%>
                <%--</ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rbl_SearchBy" />
                </Triggers>
            </asp:UpdatePanel>--%>
       
        <%--<td style="width: 25%">--%>
            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>--%>
                   <%-- <asp:DropDownList ID="ddl_TicketNo" Width="30%" runat="server" AutoPostBack="true"
                        CssClass="DROPDOWN">
                    </asp:DropDownList>--%>
                <%--</ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="rbl_SearchBy" />
                </Triggers>
            </asp:UpdatePanel>--%>
       <%-- </td>
        <td style="width: 5%">
        </td>
       
    </tr>--%>
    <tr>
        <td style="width: 25%; vertical-align: top;" class="TD1">How Resolved:
        <%--<asp:Label ID="lbl_HowResolved" runat="server" CssClass="LABEL" Text="How Resolved:"></asp:Label>--%></td>
        <td style="width: 25%;" colspan="1">
            <asp:TextBox ID="txt_HowResolved" runat="server" CssClass="TEXTBOX" TextMode="MultiLine"
                Height="60px" Width="99%" BorderWidth="1px"></asp:TextBox>
        </td>
        <td style="width: 50%" class="TDMANDATORY" align="left" >*</td>      
    </tr>
   
    <tr>
     <td style="width: 25%;" class="TD1">Whether Customer Satisfied:
    <%-- <asp:Label ID="lbl_WhetherSatisfied" runat="server" CssClass="LABEL" Text="Whether Customer Satisfied:" ></asp:Label>--%></td>
     <td style="width: 25%;">
     <asp:DropDownList ID="ddl_CustomerSatisfied" width="20%" runat="server" CssClass="DROPDOWN" AutoPostBack="true" OnSelectedIndexChanged="ddl_CustomerSatisfied_SelectedIndexChanged">
                <asp:ListItem Value="1">Yes</asp:ListItem>               
                <asp:ListItem Value="0">No</asp:ListItem>
            </asp:DropDownList>
            </td>
            
            
       <td style="width: 50%" class="TD1"></td>
        
    </tr>
    
    <tr>
     
     <td style="width: 25%; vertical-align: top;" class="TD1">  
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
    <asp:Label ID="lbl_Reason" runat="server" CssClass="LABEL" Text="Reason:"></asp:Label>
     </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_CustomerSatisfied" />
                </Triggers>
            </asp:UpdatePanel>
    </td>
     <td style="width: 25%;" colspan="1" class="TD1">
     <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                <table>
                <tr>
                
                <td style="width:99%">
                   <asp:TextBox ID="txt_Reason" runat="server" CssClass="TEXTBOX" TextMode="MultiLine" Height="60px" Width="99%" BorderWidth="1px"></asp:TextBox>                
                </td>
                
                 <td style="width: 1%" class="TDMANDATORY" runat="server" id="td_Mandetory">*</td>
                 
                </tr>
                </table>
      </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_CustomerSatisfied" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
      
       
      
    </tr>
    
    <tr>
   <td colspan="6">
    <asp:Label ID="lbl_Errors" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"  ></asp:Label>
     </td>
</tr>
<tr> 
 <td colspan="6" style="width:100%;text-align:center; height: 26px;">
  <asp:Button ID="btn_Save" runat="Server" Text="Save" CssClass="BUTTON"  OnClick="btn_Save_Click"  Visible="false"/>
 </td>
</tr>
   
    </table>
  <%--  <script type="text/javascript">
DisableControlOnChecked();
</script>--%>
    