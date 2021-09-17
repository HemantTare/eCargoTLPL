<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTransitSchedule.ascx.cs" Inherits="Master_Branch_WucTransitSchedule" %>
<asp:ScriptManager ID="scm_TransitSchedule" runat="server"></asp:ScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" class="TABLE" style="width: 100%">
        
        <tr>
            <td colspan="2" >
            <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px" meta:resourcekey="Label1Resource1"
                ></asp:Label>
             </td>
        </tr>
        
        <tr>
            <td colspan="2" class="TDGRADIENT">
             <%--   <asp:Image ID="Image1" runat="server" ImageUrl="~/images/red_block.gif" ImageAlign="Middle" />--%>
           
                <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL"
                Text="TRANSIT DAYS SCHEDULE" meta:resourcekey="lbl_HeadingResource1"></asp:Label>
            
              <%-- <asp:Image ID="Image2" runat="server" ImageUrl="~/images/strip.gif" />--%>
                &nbsp;&nbsp;</td>
        </tr>
        
        <tr>
            
            <td style="font-size: xx-small; width: 100%; text-align: left" colspan="2">
            &nbsp;</td>
        </tr>
        
        <tr>
            <td style="font-size: 11px; font-family: Verdana;color: maroon; height: 19px;" colspan="2" >
                <asp:Label ID="lbl_FromState" CssClass="LABEL" runat="server" Text="From State:" meta:resourcekey="lbl_FromStateResource1"></asp:Label>&nbsp;<asp:DropDownList ID="ddl_FromState"   runat="server" AutoPostBack="True" Font-Names="Verdana" Font-Size="11px" OnSelectedIndexChanged="ddl_FromState_SelectedIndexChanged" Width="22%" meta:resourcekey="ddl_FromStateResource1"></asp:DropDownList>
                <asp:Label ID="Label2" runat="server" ForeColor="Red" meta:resourcekey="Label2Resource1"
                    Text="*"></asp:Label>&nbsp;
                <asp:Label ID="lbl_ToState" CssClass="LABEL" runat="server" Text="To State:" meta:resourcekey="lbl_ToStateResource1"></asp:Label>&nbsp;<asp:DropDownList ID="ddl_ToState" runat="server"  AutoPostBack="True" Font-Names="Verdana" Font-Size="11px" OnSelectedIndexChanged="ddl_ToState_SelectedIndexChanged" Width="22%" meta:resourcekey="ddl_ToStateResource1"></asp:DropDownList>&nbsp;<asp:Label ID="Label3" runat="server" ForeColor="Red" meta:resourcekey="Label3Resource1"
                    Text="*"></asp:Label>
                <asp:Label ID="lbl_VehicleType" CssClass="LABEL" runat="server" Text="Vehicle Type:" meta:resourcekey="lbl_VehicleTypeResource1"></asp:Label>&nbsp;<asp:DropDownList ID="ddl_VehicleType" runat="server"  AutoPostBack="True" Font-Names="Verdana" Font-Size="11px" OnSelectedIndexChanged="ddl_VehicleType_SelectedIndexChanged" Width="20%" meta:resourcekey="ddl_VehicleTypeResource1" ></asp:DropDownList>
                <asp:Label ID="Label4" runat="server" ForeColor="Red" meta:resourcekey="Label4Resource1"
                    Text="*"></asp:Label></td>
        </tr>
          <tr>
            
            <td style="font-size: xx-small; width: 100%; text-align: left" colspan="2">
            &nbsp;</td>
        </tr>
    </table>
<table border="0" cellpadding="0" cellspacing="0" style="width: 100%" class="TABLE">
    <tr>
            <td style="font-size: xx-small; width: 100%; text-align: left">
            &nbsp;</td>
    </tr>

    <tr>
        <td style="width: 100%;">
        <asp:Panel ID="pnl_Grid" runat="server" Width="760px"   ScrollBars="Horizontal" meta:resourcekey="pnl_GridResource1">
        <asp:UpdatePanel ID="Upd_Pnl_dg_TransitSchedule"  runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="ddl_FromState" />
            <asp:AsyncPostBackTrigger ControlID="ddl_ToState" />
            <asp:AsyncPostBackTrigger ControlID="ddl_VehicleType" />    
        </Triggers>
            <ContentTemplate>
            
            <asp:DataGrid ID="dg_TransitSchedule" runat="server" Width="750px" CssClass="GRID" AllowPaging="True" PageSize="15" OnItemDataBound="dg_TransitSchedule_ItemDataBound" OnPageIndexChanged="dg_TransitSchedule_PageIndexChanged" meta:resourcekey="dg_TransitScheduleResource1">
            <AlternatingItemStyle CssClass="GridAlternateRowCss" />
            <HeaderStyle CssClass="GRIDHEADERCSS" />
            <FooterStyle CssClass="GRIDFOOTERCSS" />
            <PagerStyle Mode="NumericPages"/>
            </asp:DataGrid>
            </ContentTemplate>
        </asp:UpdatePanel>
        </asp:Panel>
            </td>
    </tr>
    <tr>
        <td style="font-size: xx-small; width: 100%; text-align: left">
            &nbsp;</td>
    </tr>
    
    </table>
    
    <table  border="0" cellpadding="0" cellspacing="0" style="width: 100%" class="TABLE">
        <tr>
            <td style="width: 50%">
            <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" meta:resourcekey="lbl_ErrorsResource1" Text="Fields With * Mark Are Mandatory"></asp:Label></td>
            <td style="width: 50%; text-align: right">
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                &nbsp;</td>
            <td style="width: 50%; text-align: right">
            </td>
        </tr>
    <tr>
    
    
        <td style="width: 50%;">
            <asp:Label ID="lbl_NA" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px"
            ForeColor="Red" Text="NA - Not Defined" meta:resourcekey="lbl_NAResource1"></asp:Label>&nbsp;
            </td>

        <td style="width: 50%; text-align: right">
            &nbsp;</td>
        
    </tr>
    
    <tr>
        <td style="font-size: xx-small; width: 100%; text-align: left" colspan="2">
            &nbsp;
        </td>
    </tr>
    
    </table> 

<table  border="1" cellpadding="0" cellspacing="0" style="width: 100%">
    <tr>
        <td style="width: 25%;" class="TDTRANSIT">
            ND - Next Day
        </td>

        <td style="width: 25%" class="TDTRANSIT">
            1 - One Day Transit
        </td>

        <td style="width: 25%" class="TDTRANSIT">
            2 - Two Days Transit
        </td>

        <td style="width: 25%" class="TDTRANSIT">
            3 - Three Days Transit
        </td>
        
    </tr>
    
    <tr>
        <td style="width: 25%" class="TDTRANSIT">
            4 - Four Days Transit
        </td>

        <td style="width: 25%" class="TDTRANSIT">
            5 - Five Days Transit
        </td>

        <td style="width: 25%" class="TDTRANSIT">
            NC - No Commitment
        </td>

        <td style="width: 25%" class="TDTRANSIT">
            0 - Source to Source
        </td>
    
    
    
    </tr>
    
</table>

