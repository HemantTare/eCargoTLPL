<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmShortTripVehicles.aspx.cs"
    Inherits="Operations_Outward_FrmShortTripVehicles" EnableViewState="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript">


 function OpenShorTripDetails(Path)
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-400);
        var popH = h-80;//(h-100);
        var leftPos = (w-popW)/2;
        var topPos = 0;//(h-popH)/2; 
        window.open(Path, 'MainPopUp_Details', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }
        

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Short Trip Vehicles</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="3" style="width: 100%">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Short Trip Vehicles"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="3" style="width: 100%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" valign="top" align="left">
                        <asp:DataGrid ID="dgGrid" AutoGenerateColumns="False" ShowFooter="False" CellPadding="3"
                            CssClass="Grid" runat="server">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle Height="15px" Font-Size="11px" Font-Names="Verdana" Font-Bold="True"
                                HorizontalAlign="Left" ForeColor="Black" BorderStyle="Solid" BorderColor="#9495A2"
                                BorderWidth="1px" VerticalAlign="Bottom" BackColor="#D6D7E1" CssClass="DataGridFixedHeader">
                            </HeaderStyle>
                            <Columns>
                                <asp:BoundColumn DataField="SrNo" HeaderText="Sr No."></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Vehicle No">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="TEXTBOX" onkeyPress="return Only_Integers(this,event)"
                                            onblur="txtbox_onlostfocus(this);" onfocus="txtbox_onfocus(this)"  Width="150px" Text='<%# DataBinder.Eval(Container.DataItem, "VehicleNo")%>'></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                    <td style="width: 10%" valign="top" align="center">
                        <asp:Button ID="btnView" runat="server" Text="View Details" CssClass="BUTTON" OnClick="btnView_Click" />
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <div style="font-size: 11px; z-index: 100; left: 30%; bottom: 80%; font-family: Verdana;
                                    position: absolute">
                                    <span id="ajaxloading">
                                        <table>
                                            <tr>
                                                <td style="height: 34px">
                                                    <asp:Image ID="Ajax_Image_ID" runat="server" ImageUrl="~/images/ajax-loader1.gif" /></td>
                                            </tr>
                                            <tr>
                                                <td align="center">
                                                    Wait! Action in Progress...</td>
                                            </tr>
                                        </table>
                                    </span>
                                </div>
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                    <td style="width: 60%" valign="top" align="left">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="Div_Trips" class="DIV" style="height: 600px">
                                    <asp:DataGrid ID="dg_Trips" runat="server" AutoGenerateColumns="False" CellPadding="2"
                                        CssClass="GRID" DataKeyField="Vehicle_Id" Style="border-top-style: none" Width="90%"
                                        OnItemDataBound="dg_Trips_ItemDataBound" AlternatingItemStyle-BackColor="#ffff99"
                                        BackColor="White">
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="DATAGRIDFIXEDHEADER" BackColor="DarkMagenta" ForeColor="White"
                                            Font-Bold="true" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Vehicle No" ItemStyle-HorizontalAlign="Left" FooterStyle-HorizontalAlign="Left"
                                                HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtn_Vehicle_No" Text='<%# DataBinder.Eval(Container, "DataItem.Vehicle_No") %>'
                                                        Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Vehicle_Id") %>' />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="TripCount" HeaderText="Trip Count"></asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="dgGrid" />
                                <asp:AsyncPostBackTrigger ControlID="btnView" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="left" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnView" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>&nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
