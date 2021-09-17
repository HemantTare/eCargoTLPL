<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmeWayBill_Pending_Vehicle_Update_Json_Creation_PDS.aspx.cs"
    Inherits="Reports_CL_Nandwana_UserDesk_FrmeWayBill_Pending_Vehicle_Update_Json_Creation_PDS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../../JavaScript/Common.js"></script>

<script type="text/javascript">

function refreshParentPage() 
     {
        window.opener.location.href = window.opener.location.href;
        if (window.opener.progressWindow) 
        {
            window.opener.progressWindow.close();
        }
        window.close();
    }
    

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>eWayBill Pending Vehicle Update JSON Creation PDS</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body onunload="refreshParentPage();">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div style="vertical-align: top;">
            <table class="TABLE" style="width: 100%; background-color: LightPink">
                <tr>
                    <td class="TDGRADIENT" colspan="3" style="height: 16px">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="eWayBill Pending Vehicle Update JSON Creation PDS"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 30%;">
                        &nbsp;</td>
                    <td style="width: 30%;">
                        &nbsp;</td>
                    <td class="TD1" style="width: 40%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 30%; height: 20px;">
                        <asp:Label ID="lbl_Vehicle_No" CssClass="LABEL" Text="Vehicle No. :" runat="server"></asp:Label></td>
                    <td style="width: 30%; height: 20px;">
                        &nbsp;<asp:Label ID="lblVehicleNoValue" runat="server" CssClass="LABEL" Text="VehicleNo"
                            Style="font-weight: bolder" Font-Size="Medium" ForeColor="DarkRed"></asp:Label></td>
                    <td class="TDMANDATORY" style="width: 40%; height: 20px;">
                        <asp:Button ID="btn_CreateJson" runat="server" CssClass="BUTTON" Text="Create JSON"
                            Font-Bold="true" ForeColor="Red" OnClick="btn_CreateJson_Click"></asp:Button></td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 30%;">
                        &nbsp;</td>
                    <td style="width: 30%;">
                        &nbsp;</td>
                    <td class="TD1" style="width: 40%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 30%; height: 15px;">
                        &nbsp;</td>
                    <td style="width: 30%; height: 15px;">
                        &nbsp;</td>
                    <td class="TD1" style="width: 40%; height: 15px;">
                    &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 30%;">
                        <asp:Label ID="lbl_Consolidated_eWayBillNo" CssClass="LABEL" Text="Consolidated eWayBill No. :"
                            runat="server"></asp:Label></td>
                    <td style="width: 30%;">
                        &nbsp;<asp:TextBox ID="txt_Consolidated_eWayBillNo" runat="server" CssClass="TEXBOX"
                            Text="" Width="80%" onkeyPress="return Only_Integers(this,event)" onblur="txtbox_onlostfocus(this);"
                            onfocus="txtbox_onfocus(this)"></asp:TextBox>
                    </td>
                    <td class="TDMANDATORY" style="width: 40%;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 30%;">
                        &nbsp;</td>
                    <td style="width: 30%;">
                        &nbsp;</td>
                    <td class="TD1" style="width: 40%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 30%;">
                        <asp:Label ID="lblMobile1" runat="server" CssClass="LABEL" Text="Mobile No. 1 :"></asp:Label></td>
                    <td style="width: 30%;">
                        &nbsp;<asp:TextBox ID="txt_Mobile1" runat="server" CssClass="TEXBOX" Text="" Width="80%"
                            onkeyPress="return Only_Integers(this,event)" onblur="txtbox_onlostfocus(this);"
                            onfocus="txtbox_onfocus(this)"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 40%;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 30%;">
                        &nbsp;</td>
                    <td style="width: 30%;">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 40%;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 30%;">
                        <asp:Label ID="lblMobile2" runat="server" CssClass="LABEL" Text="Mobile No. 2 :"></asp:Label></td>
                    <td style="width: 30%;">
                        &nbsp;<asp:TextBox ID="txt_Mobile2" runat="server" CssClass="TEXBOX" Text="" Width="80%"
                            onkeyPress="return Only_Integers(this,event)" onblur="txtbox_onlostfocus(this);"
                            onfocus="txtbox_onfocus(this)"></asp:TextBox></td>
                    <td class="TDMANDATORY" style="width: 40%;">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 30%;">
                        &nbsp;</td>
                    <td style="width: 30%;">
                        &nbsp;</td>
                    <td class="TD1" style="width: 40%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="width: 20%; height: 15px">
                        <asp:UpdatePanel runat="server" UpdateMode="Conditional" ID="upnl_Comm">
                            <ContentTemplate>
                                <asp:Panel ID="pnl_BranchMobileNos" runat="server" Height="200px" ScrollBars="None">
                                    <asp:DataGrid ID="dg_BranchMobileNos" runat="server" AllowCustomPaging="False" AllowPaging="False"
                                        AllowSorting="False" AutoGenerateColumns="False" CssClass="GRID" OnItemDataBound="dg_BranchMobileNos_ItemDataBound"
                                        PageSize="15" ShowFooter="False">
                                        
                                        <HeaderStyle BackColor="Orange" CssClass="GRIDHEADERCSS" />
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderStyle-HorizontalAlign="Left" HeaderText="BranchID" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBranchID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PDS_Branch_ID")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderStyle-HorizontalAlign="Left" HeaderText="Branch">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblBranchName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BranchName")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderStyle-HorizontalAlign="Left" HeaderText="Contact Person">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblContactPerson" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ContactPerson")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderStyle-HorizontalAlign="Left" HeaderText="Mobile No.">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtContactPersonMobile" runat="server" AutoPostBack="true" CssClass="TEXTBOX"
                                                        OnTextChanged="txtContactPersonMobile_TextChanged" onblur="txtbox_onlostfocus(this);"
                                                        onfocus="txtbox_onfocus(this)" onkeyPress="return Only_Integers(this,event)"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "Phone1")%>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </asp:Panel>
                                &nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TD1" style="width: 80%">
                        &nbsp;</td>
                </tr>
            </table>
            <table class="TABLE" style="width: 100%; text-align: left;">
                <tr>
                    <td>
                        &nbsp;<asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Text="Fields with * mark are mandatory"></asp:Label>&nbsp;
                                <asp:HiddenField ID="hdnVehicleId" runat="server" />
                                <asp:HiddenField ID="hdnCurrent_PDS_ID" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_Save_Exit" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
            <table class="TABLE" style="width: 100%; text-align: center; background-color: LightPink"">
                <tr>
                    <td style="width: 100%">
                        &nbsp;<asp:Button ID="btn_Save_Exit" runat="server" CssClass="BUTTON" Text="Save & Exit"
                            OnClick="btn_Save_Exit_Click"></asp:Button>&nbsp;
                        <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" Text="Exit" OnClick="btn_Close_Click">
                        </asp:Button>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
