<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucVehicleHireDetails.ascx.cs" Inherits="Master_Vehicle_WucVehicleHireDetails" %>

<table class="TABLE" width="100%">
    <tr>
        <td colspan="6">&nbsp;</td></tr>
    <tr>
        <td colspan="6" style="width: 100%" align="left">
            <table cellpadding="5" cellspacing="5" border="0" width="100%">
        <tr>        
         <td>
        <asp:Panel ID="pnl_Hire_Details" runat="server"  GroupingText="Hire Details" CssClass="PANEL" Width="100%" meta:resourcekey="pnl_Hire_DetailsResource1">
        <table cellpadding="3" cellspacing="3" border="0" width="100%">
    <tr>
        <td colspan="6">&nbsp;</td> 
    </tr>

    <tr>
        <td style="width: 20%" class="TD1">
        <asp:Label ID="lbl_Multiple_Trips_A_Day"  runat="server" Text="Multiple Trips A Day?" meta:resourcekey="lbl_Multiple_Trips_DayResource1"/>
        </td>
        <td style="width: 29%"><asp:CheckBox ID="chk_Multiple_Trips_A_Day" runat="server" meta:resourcekey="chk_Multiple_Trips_A_DayResource1" /></td>
        <td style="width: 1%"></td>
        <td style="width: 20%"></td>
        <td style="width: 29%"></td>
        <td style="width: 1%"></td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
        <asp:Label ID="lbl_Hire_Type"  runat="server" Text="Hire Type :" meta:resourcekey="lbl_Hire_TypeResource1"/>
        </td>
        <td style="width: 29%"><asp:DropDownList ID="ddl_Hire_Type" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_Hire_TypeResource1"/></td>
        <td style="width: 1%" class="TDMANDATORY"></td>
        <td style="width: 20%" class="TD1">
        <asp:Label ID="lbl_Hire_Amount"  runat="server" Text="Hire Amount :" meta:resourcekey="lbl_Hire_AmountResource1"/>
        </td>
        <td style="width: 29%"><asp:TextBox ID="txt_Hire_Amount" onkeypress="return Only_Numbers(this,event)" MaxLength="9" runat="server" CssClass="TEXTBOXNOS" meta:resourcekey="txt_Hire_AmountResource1"></asp:TextBox></td>
        <td style="width: 1%" class="TDMANDATORY"></td>
    </tr>
    <tr>
        <td style="width: 20%" class="TD1">
        <asp:Label ID="lbl_Maintained_By"  runat="server" Text="Maintained By :" meta:resourcekey="lbl_Maintained_ByResource1"/>
        </td>
        <td style="width: 29%">
            <asp:RadioButtonList ID="rdl_MaintainedBy" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rdl_MaintainedBy_SelectedIndexChanged" meta:resourcekey="rdl_MaintainedByResource1">
            <asp:ListItem Value="1" onclick="javascript:setTimeout('__doPostBack(\'WucVehicle1$WucVehicleHireDetails1$rdl_MaintainedBy$1\',\'\')', 0)" Selected="True" meta:resourcekey="ListItemResource1">Region</asp:ListItem>
            <asp:ListItem Value="2" meta:resourcekey="ListItemResource2">Area</asp:ListItem>
            <asp:ListItem Value="3" meta:resourcekey="ListItemResource3">Branch</asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td style="width: 1%"></td>
        <td style="width: 20%" class="TD1">
          <asp:UpdatePanel ID="upd_pnl_lbl_MaintainedBy" UpdateMode="Conditional" runat="server">
            <Triggers><asp:AsyncPostBackTrigger ControlID="rdl_MaintainedBy"/></Triggers>
            <ContentTemplate>
              <asp:Label ID="lbl_MaintainedBy" runat="server" Text="Region Name :" meta:resourcekey="lbl_MaintainedByResource1"></asp:Label>
            </ContentTemplate>
          </asp:UpdatePanel>
        </td>
                    
        <td style="width: 29%">
        <asp:UpdatePanel ID="upd_pnl_ddl_MaintainedBy" UpdateMode="Conditional" runat="server">
          <Triggers><asp:AsyncPostBackTrigger ControlID="rdl_MaintainedBy"/></Triggers>
          <ContentTemplate>
            <asp:DropDownList ID="ddl_MaintainedBy" runat="server" CssClass="DROPDOWN" meta:resourcekey="ddl_MaintainedByResource1"/>
          </ContentTemplate>
          </asp:UpdatePanel>
        </td>
        <td style="width: 1%" class="TDMANDATORY"></td>
    </tr>

   <tr>
      <td colspan="6">
        <%--<asp:UpdatePanel ID="UpdatePanel3" UpdateMode="Conditional" runat="server">
            <ContentTemplate>
                <asp:Label ID="lbl_Errors" CssClass="LABELERROR" runat="server" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Save" />
            </Triggers>
        </asp:UpdatePanel>--%>
        <asp:Label ID="lbl_Errors" CssClass="LABELERROR" runat="server" meta:resourcekey="lbl_ErrorsResource1"></asp:Label>

    </td>
    </tr>
</table>
    </asp:Panel></td></tr>
    </table>
    </td>
    </tr>
</table>