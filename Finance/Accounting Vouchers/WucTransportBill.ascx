<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTransportBill.ascx.cs"
    Inherits="Finance_Accounting_Vouchers_WucTransportBill" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="../../CommonControls/Wuc_Region_Area_Branch.ascx" TagName="Wuc_Region_Area_Branch"
    TagPrefix="uc1" %>

<script type="text/javascript" language="javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" language="javascript" src="../../Javascript/Finance/Accounting Vouchers/TransportBill.js"></script>

<asp:ScriptManager AsyncPostBackTimeout="180" ID="scm_Bill" runat="server">
</asp:ScriptManager>

<script type="text/javascript" language="javascript">
    
    function Show_GC_SubTotal(Path,chk_attach)
    {
        var chk_attach1 = document.getElementById(chk_attach);
        Path = Path + "&Attched=" + chk_attach1.checked;
        window.open(Path,'Freight','width=400,height=450,top=200,left=250,menubar=no,resizable=no,scrollbars=no');
        return false;
    }
       
    function Show_GC_OtherCharge(Path,chk_attach)
    {
        var chk_attach1 = document.getElementById(chk_attach); 
        if (chk_attach1.checked == true)
        {
            Path = Path + "&Attched=" + chk_attach1.checked;
            window.open(Path,'OtherCharge','width=700,height=400,top=200,left=250,menubar=no,resizable=no,scrollbars=no');
        }
        return false;
    }
</script>

<table class="TABLE">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            &nbsp;
            <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="Transportation Bill"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_TransBill_No" runat="server" CssClass="LABEL" Text="Bill No. :"></asp:Label>
        </td>
        <td style="width: 34%;">
            <asp:TextBox ID="lbl_TransBillNo" runat="server" Text="0" Width="45%" Font-Bold="true"
                CssClass="TEXTBOX"></asp:TextBox>
            <asp:Label ID="lbl_Start_End_No" runat="server" Width="50%" Font-Bold="true"></asp:Label>
        </td>
        <td class="TD1" style="width: 1%;">
        </td>
        <td class="TD1" style="width: 10%;">
            <asp:Label ID="lbl_Date" runat="server" CssClass="LABEL" Text="Bill Date :"></asp:Label>
        </td>
        <td style="width: 34%;" class="TDMANDATORY">
            <uc1:WucDatePicker ID="dtp_BillDate" runat="server" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr style="display: none;">
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Bill_Type" runat="server" CssClass="LABEL" Text="Bill Type :"></asp:Label></td>
        <td style="width: 34%">
            <asp:DropDownList ID="ddl_BillType" CssClass="DROPDOWN" runat="server" /></td>
        <td class="TD1" style="width: 1%">
            *</td>
        <td class="TD1" style="width: 10%">
        </td>
        <td class="TDMANDATORY" style="width: 34%">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Client" runat="server" CssClass="LABEL" Text="Client Name :"></asp:Label>
        </td>
        <td style="width: 34%;">
            <cc1:DDLSearch ID="ddl_Client" runat="server" DBTableName="EC_Master_Client_Vtrans"
                IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetClientForTransportBill"
                CallBackAfter="2" Text="" PostBack="True" OnTxtChange="ddl_Client_TxtChange" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Ref_no" runat="server" CssClass="LABEL" Text="Ref. No. :" Visible="false"></asp:Label>
        </td>
        <td style="width: 34%;">
            <asp:TextBox ID="txt_Ref_No" runat="server" CssClass="TEXTBOX" MaxLength="50" Visible="false"></asp:TextBox>
        </td>
        <td style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 100%" colspan="5">
            <asp:UpdatePanel ID="upd_ClientBillingDetails" runat="server" UpdateMode="conditional">
                <ContentTemplate>
                    <table width="100%" visible="false">
                        <tr>
                            <td class="TD1" style="width: 20%">
                                <asp:Label ID="lbl_BillingName" runat="server" CssClass="LABEL" Text="Billing Name :"></asp:Label>
                            </td>
                            <td style="width: 34%;">
                                <asp:TextBox ID="txt_BillingName" runat="server" MaxLength="50" CssClass="TEXTBOX"></asp:TextBox>
                            </td>
                            <td class="TDMANDATORY" style="width: 1%">
                            </td>
                            <td class="TD1" style="width: 20%">
                                <asp:Label ID="lbl_ContactPerson" runat="server" CssClass="LABEL" Text="Contact Person :"
                                    Visible="False"></asp:Label>
                            </td>
                            <td style="width: 34%;">
                                <asp:TextBox ID="txt_ContactPerson" runat="server" MaxLength="50" CssClass="TEXTBOX"
                                    Visible="False"></asp:TextBox>
                            </td>
                            <td class="TD1" style="width: 1%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                <asp:Label ID="lbl_Address" runat="server" CssClass="LABEL" Text="Billing Address :"></asp:Label>
                            </td>
                            <td style="width: 34%;">
                                <asp:TextBox ID="txt_BillingAddress" runat="server" CssClass="TEXTBOX" TextMode="MultiLine"
                                    Height="60" MaxLength="1000"></asp:TextBox>
                            </td>
                            <td class="TDMANDATORY" style="width: 1%">
                            </td>
                            <td class="TD1" style="width: 50%" colspan="3">
                                <table width="100%">
                                    <tr>
                                        <td class="TD1" style="width: 20%">
                                            <asp:Label ID="lbl_ContactNo" runat="server" CssClass="LABEL" Text="Contact No :"
                                                Visible="False"></asp:Label>
                                        </td>
                                        <td style="width: 34%;">
                                            <asp:TextBox ID="txt_ContactNo" runat="server" MaxLength="20" CssClass="TEXTBOX"
                                                Visible="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 20%">
                                            <asp:Label ID="lbl_Email" runat="server" CssClass="LABEL" Text="Email :" Visible="False"></asp:Label>
                                        </td>
                                        <td style="width: 34%;">
                                            <asp:TextBox ID="txt_Email" runat="server" MaxLength="50" CssClass="TEXTBOX" Visible="False"></asp:TextBox>
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Client" />
                    <asp:AsyncPostBackTrigger ControlID="btn_update_grid" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Save_Print" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TD1" colspan="1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 20%;" align="right">
            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:RadioButtonList ID="rdl_BkgBranchOrDlyBranchWise" runat="server" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="rdl_BkgBranchOrDlyBranchWise_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem Value="1" Selected="True">Bkg BranchWise</asp:ListItem>
                        <asp:ListItem Value="0">Dly BranchWise</asp:ListItem>
                    </asp:RadioButtonList>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td colspan="4" style="width: 80%;" align="left">
            <asp:UpdatePanel ID="up_Rbtn" runat="server" UpdateMode="Always">
                <ContentTemplate>
                    <asp:RadioButtonList ID="rbtn_TransBillFor" runat="server" RepeatDirection="Horizontal"
                        OnSelectedIndexChanged="rbtn_TransBillFor_SelectedIndexChanged" AutoPostBack="True">
                    </asp:RadioButtonList>
                    <uc1:Wuc_Region_Area_Branch ID="Wuc_Region_Area_Branch1" runat="server" />
                </ContentTemplate>
                
            </asp:UpdatePanel>
        </td>
        <td align="left" colspan="1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td colspan="6" align="left">
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <%--<asp:Panel ID="pnl_Transport_Bill" CssClass="PANEL" runat="server" ScrollBars="Vertical">--%>
            <div runat="server" id="div_bill" class="DIV" style="height: 300px; width: 99%">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:DataGrid ID="dg_Bill" runat="server" AutoGenerateColumns="False" CellPadding="2"
                            CssClass="GRID" Style="border-top-style: none;" Width="98%" OnItemDataBound="dg_Bill_ItemDataBound">
                            <FooterStyle CssClass="GRIDFOOTERCSS" />
                            <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                            <HeaderStyle CssClass="GRIDHEADERCSS" />
                            <Columns>
                                <asp:TemplateColumn HeaderText="Attach">
                                    <HeaderTemplate>
                                        <input id="chkAllItems" type="checkbox" onclick="Check_All(this,'WucTransportBill1_dg_Bill');" disabled="disabled" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>'
                                            OnClick="Check_Single(this,'WucTransportBill1_dg_Bill','1');" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="GC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_GCNo" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "GC_No_For_Print") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbl_GCDate" runat="server" CssClass="LABEL" Text='<%# DataBinder.Eval(Container.DataItem, "GC_Date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="GC_Date" HeaderText="Bkg Date" DataFormatString="{0:dd/MM/yyyy}">
                                </asp:BoundColumn>
                                <%--<asp:BoundColumn DataField="Booking_Branch_Name" HeaderText="Bkg Branch"></asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="Consignor_Name" HeaderText="Consignor"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Delivery_Branch_Name" HeaderText="Dly Branch"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Dly_Date" HeaderText="Dly Date" DataFormatString="{0:dd/MM/yyyy}">
                                </asp:BoundColumn>
                                <%--<asp:BoundColumn DataField="Booking_Type" HeaderText="Bkg Type"></asp:BoundColumn>--%>
                                <asp:BoundColumn DataField="Consignee_Name" HeaderText="Consignee"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Delivery_Type" HeaderText="Dly Type"></asp:BoundColumn>
                                <asp:BoundColumn DataField="Articles" HeaderText="Pkgs">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="Charged_Wt" HeaderText="Charge Wt">
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="GC Sub Total">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_SubTotal" Text='<%# DataBinder.Eval(Container.DataItem, "Actual_Sub_Total") %>'
                                            runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                            Style="text-align: right; display: none" Width="90%" Font-Size="11px" Font-Names="Verdana"
                                            ReadOnly="True"></asp:TextBox>
                                        <asp:LinkButton ID="lbtn_SubTotal" runat="server" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "Actual_Sub_Total") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="LR Tax">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_LRSerTax" Text='<%# DataBinder.Eval(Container.DataItem, "GCService_Tax_Amount") %>'
                                            runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                            Style="text-align: right;" Width="90%" Font-Size="11px" Font-Names="Verdana"
                                            ReadOnly="True"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Round Off">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Round_Off" Text='<%# DataBinder.Eval(Container.DataItem, "Round_Off") %>'
                                            runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                            Style="text-align: right;" Width="90%" Font-Size="11px" Font-Names="Verdana"
                                            ReadOnly="True"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="LR Total">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_LRTotal" Text='<%# DataBinder.Eval(Container.DataItem, "Total_GC_Amount") %>'
                                            runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                            Style="text-align: right;" Width="90%" Font-Size="11px" Font-Names="Verdana"
                                            ReadOnly="True"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Other Charges">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_OtherCharge" Text='<%# DataBinder.Eval(Container.DataItem, "FA_Other_Charges") %>'
                                            runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                            Style="text-align: right; display: none" Width="90%" Font-Size="11px" Font-Names="Verdana"
                                            ReadOnly="True"></asp:TextBox>
                                        <asp:LinkButton ID="lbtn_OtherCharge" Font-Bold="true" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FA_Other_Charges") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Octroi Form Charges">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Octroi_Form_Charges" Text='<%# DataBinder.Eval(Container.DataItem, "Octroi_Form_Charges") %>'
                                            runat="server" AutoPostBack="True" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                            MaxLength="10" Width="95%" OnTextChanged="txt_Octroi_Form_Charges_TextChanged"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Octroi Service Charges">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Octroi_Service_Charges" Text='<%# DataBinder.Eval(Container.DataItem, "Octroi_Service_Charges") %>'
                                            runat="server" AutoPostBack="True" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                            MaxLength="10" Width="95%" OnTextChanged="txt_Octroi_Service_Charges_TextChanged"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="GST">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Service_Tax" Text='<%# DataBinder.Eval(Container.DataItem, "Service_Tax_Amount") %>'
                                            runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                            Style="text-align: right" Width="90%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Octroi Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Octroi_Amount" Text='<%# DataBinder.Eval(Container.DataItem, "Oct_Amount") %>'
                                            runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                            Style="text-align: right" Width="90%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Total Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_Total_Amount" Text='<%# DataBinder.Eval(Container.DataItem, "Bill_GC_Amt") %>'
                                            runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                            Style="text-align: right" Width="90%" Font-Size="11px" Font-Names="Verdana" ReadOnly="True"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Right" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txt_GCRemarks" Text='<%# DataBinder.Eval(Container.DataItem, "GC_Remarks") %>'
                                            runat="server" MaxLength="50" CssClass="TEXTBOX"></asp:TextBox>
                                        <asp:HiddenField ID="hdn_GcId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "GC_Id") %>'>
                                        </asp:HiddenField>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Width="30%" />
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddl_Client" />
                        <asp:AsyncPostBackTrigger ControlID="btn_update_grid" />
                        <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                        <asp:AsyncPostBackTrigger ControlID="btn_Save_Print" />
                        <asp:AsyncPostBackTrigger ControlID="rbtn_TransBillFor" />
                        <asp:AsyncPostBackTrigger ControlID="rdl_BkgBranchOrDlyBranchWise" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <%--</asp:Panel>--%>
        </td>
        <td colspan="1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td style="width: 17%">
                                <asp:Label ID="Lbl_TotalGC_Text" Font-Bold="True" runat="server" Width="150px"></asp:Label></td>
                            <td style="width: 80%" align="left">
                                <asp:Label ID="lbl_totalgc" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                                <asp:HiddenField ID="hdn_totalgc" runat="server" />
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Client" />
                    <asp:AsyncPostBackTrigger ControlID="btn_update_grid" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Save_Print" />
                    <asp:AsyncPostBackTrigger ControlID="rbtn_TransBillFor" />
                    <asp:AsyncPostBackTrigger ControlID="rdl_BkgBranchOrDlyBranchWise" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TD1" colspan="4">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td class="TD1" style="width: 10%">
                            </td>
                            <td class="TD1" runat="server" style="width: 10%" id="td_SubTotal">
                                <asp:Label ID="lbl_totalsubtot" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" /><asp:HiddenField
                                    ID="hdn_totalsubtot" runat="server" />
                            </td>
                            <td style="width: 10%" class="TD1" id="td_LRSerTax">
                                <asp:Label ID="lbl_totalLRSerTax" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                                <asp:HiddenField ID="hdn_totalLRSerTax" runat="server" />
                            </td>
                            <td runat="server" class="TD1" style="width: 10%">
                                <asp:Label ID="lbl_totalRound_Off" runat="server" Text="0" CssClass="LABEL" Font-Bold="True"
                                    Width="34px" /><asp:HiddenField ID="hdn_totalRound_Off" runat="server" />
                            </td>
                            <td runat="server" class="TD1" style="width: 10%">
                                <asp:Label ID="lbl_totalLRTotal" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" /><asp:HiddenField
                                    ID="hdn_totalLRTotal" runat="server" />
                            </td>
                            <td style="width: 10%" class="TD1" id="td_OtherCharge" runat="server">
                                <asp:Label ID="lbl_totalothercharge" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                                <asp:HiddenField ID="hdn_totalothercharge" runat="server" />
                            </td>
                            <td style="width: 5%" class="TD1" id="td_OFC" runat="server">
                                <asp:Label ID="lbl_TotalOctroiFormCharge" runat="server" Text="0" CssClass="HIDEGRIDCOL"
                                    Font-Bold="True" />
                                <asp:HiddenField ID="hdn_TotalOctroiFormCharge" runat="server" />
                            </td>
                            <td style="width: 5%" class="TD1" id="td_OSC" runat="server">
                                <asp:Label ID="lbl_TotalOctroiServiceCharge" runat="server" Text="0" CssClass="HIDEGRIDCOL"
                                    Font-Bold="True" />
                                <asp:HiddenField ID="hdn_TotalOctroiServiceCharge" runat="server" />
                            </td>
                            <td style="width: 10%" class="TD1" id="td_ServiceTax" runat="server">
                                <asp:Label ID="lbl_totalservicetax" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                                <asp:HiddenField ID="hdn_totalservicetax" runat="server" />
                            </td>
                            <td style="width: 5%" class="TD1" id="td_OctroiAmount" runat="server">
                                <asp:Label ID="lbl_totaloctamt" runat="server" Text="0" CssClass="HIDEGRIDCOL" Font-Bold="True"  />
                                <asp:HiddenField ID="hdn_totaloctamt" runat="server" />
                            </td>
                            <td style="width: 10%" class="TD1" id="td_GCAmount" runat="server">
                                <asp:Label ID="lbl_totalgcamount" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                                <asp:HiddenField ID="hdn_totalgcamount" runat="server" />
                            </td>
                            <td style="width: 5%">
                                &nbsp;&nbsp;</td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_update_grid" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_Client" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Save_Print" />
                    <asp:AsyncPostBackTrigger ControlID="rbtn_TransBillFor" />
                    <asp:AsyncPostBackTrigger ControlID="rdl_BkgBranchOrDlyBranchWise" />
                    <asp:AsyncPostBackTrigger ControlID="dg_Bill" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TD1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td style="width: 25%" colspan="3">
            &nbsp;</td>
        <td style="width: 75%" colspan="2">
            &nbsp;</td>
        <td colspan="1" style="width: 1%">
        </td>
    </tr>
    <tr style="display: none">
        <td class="TD1" colspan="5">
            <asp:Button ID="btn_update_grid" runat="server" Text="UpdateGrid" OnClick="btn_update_grid_Click" />
        </td>
        <td class="TD1" colspan="1" style="width: 1%">
        </td>
    </tr>
    <tr style="display: none">
        <td class="TD1" style="width: 20%">
        </td>
        <td colspan="1" style="width: 34%">
        </td>
        <td class="TD1" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
            <asp:Label ID="lbl_Less_Amt" runat="server" CssClass="LABEL" Text="Less Amount :"></asp:Label></td>
        <td class="TD1" style="width: 34%">
            <asp:TextBox ID="txt_Less_Amt" runat="server" onkeypress="return Only_Numbers(this,event)"
                CssClass="TEXTBOXNOS" Width="50%" MaxLength="10"></asp:TextBox></td>
        <td class="TD1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%; vertical-align: top">
            <asp:Label ID="lbl_Remarks" runat="server" CssClass="LABEL" Text="Remarks :"></asp:Label>
        </td>
        <td style="width: 34%; vertical-align: top" colspan="1">
            <asp:TextBox ID="txt_Remarks" runat="server" CssClass="TEXTBOX" TextMode="MultiLine"
                Height="40px" MaxLength="250"></asp:TextBox>
        </td>
        <td class="TD1" style="width: 1%;">
        </td>
        <td class="TD1" style="width: 10%">
        </td>
        <td class="TD1" style="width: 34%">
            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                <ContentTemplate>
                    <asp:DataGrid ID="dg_Voucher" runat="server" Style="width: 100%" CssClass="Grid"
                        AutoGenerateColumns="False" OnItemCommand="dg_Voucher_ItemCommand" OnItemDataBound="dg_Voucher_ItemDataBound"
                        ShowFooter="True">
                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                        <PagerStyle Mode="NumericPages" CssClass="GRIDVIEWPAGERCSS" />
                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                        <Columns>
                            <asp:TemplateColumn HeaderText="Additional Charges">
                                <HeaderStyle Width="35%" Font-Bold="True" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Ledger" runat="server" Style="width: 100%;" Text='<%#DataBinder.Eval(Container.DataItem,"Ledger_Name")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <cc1:DDLSearch ID="ddl_Ledger" runat="server" AllowNewText="false" IsCallBack="true"
                                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerTransportBill"></cc1:DDLSearch>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <cc1:DDLSearch ID="ddl_Ledger" runat="server" AllowNewText="false" IsCallBack="true"
                                        CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetLedgerTransportBill"></cc1:DDLSearch>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Amount">
                                <HeaderStyle Width="15%" HorizontalAlign="Right" Font-Bold="True" />
                                <ItemStyle HorizontalAlign="right" />
                                <ItemTemplate>
                                    <asp:Label ID="lbl_Amount" runat="server" Style="width: 100%; text-align: right;"
                                        CssClass="TEXTBOXNOS" Text='<%#DataBinder.Eval(Container.DataItem,"Amount")%>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txt_Amount" runat="server" Style="width: 75%; text-align: right;"
                                        CssClass="TEXTBOXNOS" onkeyup="valid(this)" Text='<%#DataBinder.Eval(Container.DataItem,"Amount")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txt_Amount" runat="server" Style="width: 75%; text-align: right;"
                                        CssClass="TEXTBOXNOS" BorderWidth="1px" onkeyup="valid(this)"></asp:TextBox>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:EditCommandColumn CancelText="Cancel" EditText="Edit" HeaderText="Edit" UpdateText="Update">
                                <HeaderStyle Width="5%" Font-Bold="true" />
                            </asp:EditCommandColumn>
                            <asp:TemplateColumn HeaderText="Add">
                                <HeaderStyle Width="5%" Font-Bold="true" />
                                <FooterTemplate>
                                    <asp:LinkButton ID="lnk_Add" runat="Server" CommandName="Add" Text="Add">
                                    </asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Delete">
                                <HeaderStyle Width="5%" Font-Bold="true" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnk_Delete" runat="Server" CommandName="Delete" Text="Delete">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td class="TD1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
        </td>
        <td colspan="1" style="width: 34%">
        </td>
        <td class="TD1" style="width: 1%">
        </td>
        <td class="TD1" style="width: 10%">
        </td>
        <td class="TD1" style="width: 34%">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <table border="0" style="width: 100%">
                        <tr>
                            <td style="width: 5%">
                            </td>
                            <td style="width: 25%">
                            </td>
                            <td align="right" style="width: 15%">
                            </td>
                            <td align="right" style="width: 15%">
                                <asp:Label ID="lbl_TotalAmount" runat="server" CssClass="TEXTBOX" Style="font-weight: bold;
                                    border-top-style: solid; border-top-color: black; border-bottom: black thick double;
                                    text-align: right"></asp:Label>
                                <asp:HiddenField ID="hdn_TotalAmount" runat="server" />
                            </td>
                            <td style="width: 10%">
                            </td>
                            <td style="width: 10%">
                            </td>
                            <td style="width: 20%">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 5%">
                            </td>
                            <td style="width: 25%">
                            </td>
                            <td align="right" style="width: 15%">
                            </td>
                            <td align="right" style="width: 15%">
                            </td>
                            <td style="width: 10%">
                            </td>
                            <td style="width: 10%">
                            </td>
                            <td style="width: 20%">
                            </td>
                        </tr>
                        <tr>
                            <td align="right" colspan="3">
                                <asp:Label ID="lbl_lblTotal_Additional_Charges" runat="server" CssClass="TEXTBOX"
                                    Style="font-weight: bold; border-top-style: solid; border-top-color: black; border-bottom: black thick double;
                                    text-align: right" Text="Total Bill Amount :" Width="129px"></asp:Label>
                            </td>
                            <td align="right" style="width: 15%">
                                <asp:Label ID="lbl_Total_Additional_Charges" runat="server" CssClass="TEXTBOX" Style="font-weight: bold;
                                    border-top-style: solid; border-top-color: black; border-bottom: black thick double;
                                    text-align: right"></asp:Label>
                                <asp:HiddenField ID="hdn_Total_Additional_Charges" runat="server" />
                            </td>
                            <td style="width: 10%">
                            </td>
                            <td style="width: 10%">
                            </td>
                            <td style="width: 20%">
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dg_Voucher" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
        <td class="TD1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="5" style="text-align: left">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label><br />
                    &nbsp;
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                    <asp:AsyncPostBackTrigger ControlID="btn_Save_Print" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:Label ID="lbl_Error_Client" runat="server" CssClass="LABELERROR"></asp:Label></td>
        <td class="TD1" colspan="1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="5" style="text-align: center">
            <asp:Button ID="btn_Save" runat="server" CssClass="BUTTON" Text="Save" OnClientClick="return Allow_To_Save()"
                OnClick="btn_Save_Click" /><asp:Button ID="btn_Save_Print" runat="server" CssClass="BUTTON"
                    Text="Save & Print" ValidationGroup="Save" OnClientClick="return Allow_To_Save()"
                    OnClick="btn_Save_Print_Click" /><asp:HiddenField ID="hdn_Is_Series_Required" runat="server">
                    </asp:HiddenField>
            <asp:HiddenField ID="hdn_Document_Allocation_ID" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Start_No" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_End_No" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Next_No" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Padded_Next_No" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Max_Length" runat="server" Value="0" />
        </td>
        <td class="TD1" colspan="1" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" colspan="5" style="text-align: left">
            <asp:Label ID="Label2" runat="server" CssClass="LABELERROR" Text="fields with * mark are mandatory"></asp:Label></td>
        <td class="TD1" colspan="1" style="width: 1%">
        </td>
    </tr>
</table>
<asp:UpdateProgress ID="UpdateProgress1" runat="server">
        <ProgressTemplate>
            <div style="position: absolute; bottom: 80%; left: 60%; font-size: 11px; font-family: Verdana;
                z-index: 100">
                <span id="ajaxloading">
                    <table>
                        <tr>
                            <td>
                                <asp:Image ID="Ajax_Image_ID" runat="server" ImageUrl="~/images/ajax-loader-Squares.gif" /></td>
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
<script type="text/javascript" language="javascript">
//document.getElementById('<%=btn_update_grid.ClientID%>').style.display = "none";
  
function update_grid_GCDetails()
{
document.getElementById('<%=btn_update_grid.ClientID%>').style.display = "none";
document.getElementById('<%=btn_update_grid.ClientID%>').style.visibility = "hidden";
document.getElementById('<%=btn_update_grid.ClientID%>').click();
}
</script>

&nbsp; 