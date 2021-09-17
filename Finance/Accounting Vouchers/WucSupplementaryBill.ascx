<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucSupplementaryBill.ascx.cs"
    Inherits="Finance_Accounting_Vouchers_WucSupplementaryBill" %>
<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<%@ Register Src="~/CommonControls/WucSelectedItems.ascx" TagName="WucSelectedItems"
    TagPrefix="uc1" %>

<script type="text/javascript" language="javascript" src="../../Javascript/ddlsearch.js"></script>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<script type="text/javascript" language="javascript" src="../../Javascript/Common.js"></script>

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>

<script type="text/javascript" language="javascript">

    function ShowPopUp(Path)
    {   
        Path = Path + "&Attched=True";
        window.open(Path,'OtherCharge','width=700,height=400,top=200,left=250,menubar=no,resizable=no,scrollbars=no');
        return false;
    }
    
    function Allow_To_Save()
    {
        var ATS = false;
        var hdn_totalgc = document.getElementById('WucSupplementaryBill1_hdn_totalgc');
        var lbl_Error = document.getElementById('WucSupplementaryBill1_lbl_Errors');
        
        if(val(hdn_totalgc.value) == 0)
        {
            lbl_Error.innerHTML = "Please Select Atleast One GC";
        }
        else
        {
            ATS = true;
        }
        return ATS;
    }
    
    function Check_All(chk,gridname)
    {              
        var grid = document.getElementById(gridname);
        var i,j=0;
        var checkbox,txt_OtherCharge,lbl_Service_Tax,lbl_Total;
        var sum_TotalOtherCharge = 0,sum_TotalServiceTax = 0;sum_TotalAmount = 0;
        
        var lbl_OtherCharge = document.getElementById('WucSupplementaryBill1_lbl_OtherCharge');
        var lbl_ServiceTax = document.getElementById('WucSupplementaryBill1_lbl_ServiceTax');
        var lbl_GrandTotalValue = document.getElementById('WucSupplementaryBill1_lbl_GrandTotalValue');

        var hdn_OtherCharge = document.getElementById('WucSupplementaryBill1_hdn_OtherCharge');
        var hdn_ServiceTax = document.getElementById('WucSupplementaryBill1_hdn_ServiceTax');
        var hdn_GrandTotalValue = document.getElementById('WucSupplementaryBill1_hdn_GrandTotalValue');
        var hdn_totalgc = document.getElementById('WucSupplementaryBill1_hdn_totalgc');

        var max = (grid.rows.length - 1);
        
        for(i=1;i<grid.rows.length - 1;i++)
        {
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            txt_OtherCharge= grid.rows[i].cells[6].getElementsByTagName('input');
            txt_ServiceTax = grid.rows[i].cells[7].getElementsByTagName('input');
            txt_Total = grid.rows[i].cells[8].getElementsByTagName('input');
            
            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            }
            
            if(chk.checked == true)
            {
                if(txt_OtherCharge[0].type =='text')
                {
                    sum_TotalOtherCharge = sum_TotalOtherCharge + val(txt_OtherCharge[0].value);
                } 
                if(txt_ServiceTax[0].type =='text')
                {
                    sum_TotalServiceTax = sum_TotalServiceTax + val(txt_ServiceTax[0].value);
                }
                if(txt_Total[0].type =='text')
                {
                    sum_TotalAmount = sum_TotalAmount + val(txt_Total[0].value);
                }
            }
            
            if(chk.checked == true)
            {
                lbl_OtherCharge.innerHTML  = roundNumber(sum_TotalOtherCharge,2);
                lbl_ServiceTax.innerHTML  = roundNumber(sum_TotalServiceTax,2);
                lbl_GrandTotalValue.innerHTML  = roundNumber(sum_TotalAmount,2);
                        
                hdn_totalgc.value = max;
                hdn_OtherCharge.value = roundNumber(sum_TotalOtherCharge,2);
                hdn_ServiceTax.value = roundNumber(sum_TotalServiceTax,2);
                hdn_GrandTotalValue.value = roundNumber(sum_TotalAmount,2);
            }
            else
            {
                lbl_OtherCharge.innerHTML  = 0;
                lbl_ServiceTax.innerHTML  = 0;
                lbl_GrandTotalValue.innerHTML  = 0;
                     
                hdn_totalgc.value = max;        
                hdn_OtherCharge.value = 0;
                hdn_ServiceTax.value = 0;
                hdn_GrandTotalValue.value = 0;
            }
        }
    }    
    
    function Check_Single(chk,gridname)
    {
        var grid = document.getElementById(gridname);
       
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        var row = chk.parentElement.parentElement;

        var lbl_OtherCharge = document.getElementById('WucSupplementaryBill1_lbl_OtherCharge');
        var lbl_ServiceTax = document.getElementById('WucSupplementaryBill1_lbl_ServiceTax');
        var lbl_GrandTotalValue = document.getElementById('WucSupplementaryBill1_lbl_GrandTotalValue');

        var hdn_OtherCharge = document.getElementById('WucSupplementaryBill1_hdn_OtherCharge');
        var hdn_ServiceTax = document.getElementById('WucSupplementaryBill1_hdn_ServiceTax');
        var hdn_GrandTotalValue = document.getElementById('WucSupplementaryBill1_hdn_GrandTotalValue');
        var hdn_totalgc = document.getElementById('WucSupplementaryBill1_hdn_totalgc');
 
        txt_OtherCharge= row.cells[6].getElementsByTagName('input');
        txt_ServiceTax  = row.cells[7].getElementsByTagName('input');
        txt_Total = row.cells[8].getElementsByTagName('input');
        
        if(chk.checked == true)
        {
           lbl_OtherCharge.innerHTML = roundNumber(val(lbl_OtherCharge.innerHTML) + val(txt_OtherCharge[0].value),2);
           lbl_ServiceTax.innerHTML =roundNumber(val(lbl_ServiceTax.innerHTML) + val(txt_ServiceTax[0].value),2);
           lbl_GrandTotalValue.innerHTML = roundNumber(val(lbl_GrandTotalValue.innerHTML) + val(txt_Total[0].value),2);

           hdn_totalgc.value = val(hdn_totalgc.value) + 1;
           hdn_OtherCharge.value = roundNumber(val(hdn_OtherCharge.value) + val(txt_OtherCharge[0].value),2);
           hdn_ServiceTax.value = roundNumber(val(hdn_ServiceTax.value) + val(txt_ServiceTax[0].value),2);
           hdn_GrandTotalValue.value = roundNumber(val(hdn_GrandTotalValue.value) + val(txt_Total[0].value),2);
        }
        else
        {
           lbl_OtherCharge.innerHTML = roundNumber(val(lbl_OtherCharge.innerHTML) - val(txt_OtherCharge[0].value),2);
           lbl_ServiceTax.innerHTML =roundNumber(val(lbl_ServiceTax.innerHTML) - val(txt_ServiceTax[0].value),2);
           lbl_GrandTotalValue.innerHTML = roundNumber(val(lbl_GrandTotalValue.innerHTML) - val(txt_Total[0].value),2);

           hdn_totalgc.value = val(hdn_totalgc.value) - 1;
           hdn_OtherCharge.value = roundNumber(val(hdn_OtherCharge.value) - val(txt_OtherCharge[0].value),2);
           hdn_ServiceTax.value = roundNumber(val(hdn_ServiceTax.value) - val(txt_ServiceTax[0].value),2);
           hdn_GrandTotalValue.value = roundNumber(val(hdn_GrandTotalValue.value) - val(txt_Total[0].value),2);
        }
    }
    
</script>

<table class="TABLE" width="100%">
    <tr>
        <td class="TDGRADIENT" colspan="6">
            <asp:Label ID="lbl_heading" runat="server" CssClass="HEADINGLABEL" Text="SUPPLEMENTARY BILL"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_SupplementaryBill_No" runat="server" CssClass="LABEL" Text="Bill No. :"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:TextBox ID="lbl_SupplementaryBillNo" runat="server" Text="0" Width="45%" Font-Bold="true"
                CssClass="TEXTBOX"></asp:TextBox>
            <asp:Label ID="lbl_Start_End_No" runat="server" Width="50%" Font-Bold="true"></asp:Label>
        </td>
        <td class="TD1" style="width: 1%;">
        </td>
        <td class="TD1" style="width: 20%;">
            <asp:Label ID="lbl_Date" runat="server" CssClass="LABEL" Text="Bill Date :"></asp:Label>
        </td>
        <td style="width: 29%;" class="TDMANDATORY">
            <uc1:WucDatePicker ID="dtp_BillDate" runat="server" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Client" runat="server" CssClass="LABEL" Text="Client :"></asp:Label>
        </td>
        <td style="width: 29%;">
            <cc1:DDLSearch ID="ddl_Client" runat="server" DBTableName="EC_Master_Client_Vtrans"
                IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetClientForTransportBill"
                CallBackAfter="2" Text="" PostBack="True" OnTxtChange="ddl_Client_TxtChange" />
        </td>
        <td class="TDMANDATORY" style="width: 1%">
            *</td>
        <td class="TD1" style="width: 20%">
            <asp:Label ID="lbl_Ref_no" runat="server" CssClass="LABEL" Text="Ref. No. :"></asp:Label>
        </td>
        <td style="width: 29%;">
            <asp:TextBox ID="txt_Ref_No" runat="server" CssClass="TEXTBOX" MaxLength="25"></asp:TextBox>
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 100%" colspan="6">
            <asp:UpdatePanel ID="upd_ClientBillingDetails" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td class="TD1" style="width: 20%">
                                <asp:Label ID="lbl_BillingName" runat="server" CssClass="LABEL" Text="Billing Name :"></asp:Label>
                            </td>
                            <td style="width: 29%;">
                                <asp:TextBox ID="txt_BillingName" runat="server" MaxLength="50" CssClass="TEXTBOX"></asp:TextBox>
                            </td>
                            <td class="TDMANDATORY" style="width: 1%">
                            </td>
                            <td class="TD1" style="width: 20%">
                                <asp:Label ID="lbl_ContactPerson" runat="server" CssClass="LABEL" Text="Contact Person :"></asp:Label>
                            </td>
                            <td style="width: 29%;">
                                <asp:TextBox ID="txt_ContactPerson" runat="server" MaxLength="50" CssClass="TEXTBOX"></asp:TextBox>
                            </td>
                            <td class="TD1" style="width: 1%">
                            </td>
                        </tr>
                        <tr>
                            <td class="TD1" style="width: 20%">
                                <asp:Label ID="lbl_Address" runat="server" CssClass="LABEL" Text="Billing Address :"></asp:Label>
                            </td>
                            <td style="width: 29%;">
                                <asp:TextBox ID="txt_BillingAddress" runat="server" CssClass="TEXTBOX" TextMode="MultiLine"
                                    Height="60" MaxLength="1000"></asp:TextBox>
                            </td>
                            <td class="TDMANDATORY" style="width: 1%">
                            </td>
                            <td class="TD1" style="width: 50%" colspan="3">
                                <table width="100%">
                                    <tr>
                                        <td class="TD1" style="width: 20%">
                                            <asp:Label ID="lbl_ContactNo" runat="server" CssClass="LABEL" Text="Contact No :"></asp:Label>
                                        </td>
                                        <td style="width: 29%;">
                                            <asp:TextBox ID="txt_ContactNo" runat="server" MaxLength="20" CssClass="TEXTBOX"></asp:TextBox>
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 20%">
                                            <asp:Label ID="lbl_Email" runat="server" CssClass="LABEL" Text="Email :"></asp:Label>
                                        </td>
                                        <td style="width: 29%;">
                                            <asp:TextBox ID="txt_Email" runat="server" MaxLength="50" CssClass="TEXTBOX"></asp:TextBox>
                                        </td>
                                        <td style="width: 1%;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="TD1" style="width: 20%">
                                            <asp:Label ID="lbl_Service_Type" runat="server" CssClass="LABEL" Text="Service Type :"></asp:Label></td>
                                        <td style="width: 29%">
                                            <asp:DropDownList ID="ddl_Service_Type" runat="server" CssClass="DROPDOWN" AutoPostBack="True" OnSelectedIndexChanged="ddl_Service_Type_SelectedIndexChanged">
                                            </asp:DropDownList></td>
                                        <td style="width: 1%">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddl_Client" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr id="tr_GC_No" runat="server">
        <td style="width: 100%" colspan="6">
            <asp:UpdatePanel ID="upSelectedItem" runat="server">
                <ContentTemplate>
                    <uc1:WucSelectedItems runat="server" ID="WucSelectedItem" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td colspan="5">
            <fieldset id="fld_Details" style="width: 100%">
                <legend>Details :</legend>
                <div id="Div_Supplementary" class="DIV" style="height: 250px">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DataGrid ID="dg_SupplementaryGrid" runat="server" Style="width: 100%" CssClass="Grid"
                                AutoGenerateColumns="False" ShowFooter="True" OnItemDataBound="dg_SupplementaryGrid_ItemDataBound">
                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                <PagerStyle Mode="NumericPages" CssClass="GRIDVIEWPAGERCSS" />
                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                <Columns>
                                    <asp:TemplateColumn Visible="false">
                                        <HeaderStyle Width="10%" Font-Bold="True" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_GCID" runat="server" CssClass="LABEL" Text='<%#Eval("GC_ID")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Attach">
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAllItems" runat="server" onclick="Check_All(this,'WucSupplementaryBill1_dg_SupplementaryGrid');" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="Chk_Attach" runat="server" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>'
                                                onclick="Check_Single(this,'WucSupplementaryBill1_dg_SupplementaryGrid');" />
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle Width="10%" Font-Bold="True" />
                                        <ItemStyle HorizontalAlign="left" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_CNNo" runat="server" CssClass="LABEL" Text='<%#Eval("GC_No_For_Print")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Bkg. Date">
                                        <HeaderStyle Width="12%" Font-Bold="True" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_BookingDate" runat="server" CssClass="LABEL" Text='<%#DataBinder.Eval(Container.DataItem,"BookingDate")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <%--<asp:TemplateColumn HeaderText="Bill No">
                                        <HeaderStyle Width="12%" Font-Bold="True" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_BillNo" runat="server" CssClass="LABEL" Text='<%#DataBinder.Eval(Container.DataItem,"BillNo")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Bill Date">
                                        <HeaderStyle Width="20%" Font-Bold="True" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_BillDate" runat="server" CssClass="LABEL" Text='<%#DataBinder.Eval(Container.DataItem,"Bill_Date")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>--%>
                                    <asp:TemplateColumn HeaderText="Bkg Branch">
                                        <HeaderStyle Width="20%" Font-Bold="True" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_BillingBranch" runat="server" CssClass="LABEL" Text='<%#DataBinder.Eval(Container.DataItem,"BillingBranch")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Dly Location">
                                        <HeaderStyle Width="20%" Font-Bold="True" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Service_Location_Name" runat="server" CssClass="LABEL" Text='<%#DataBinder.Eval(Container.DataItem,"Service_Location_Name")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Total LR Amount">
                                        <HeaderStyle Width="20%" Font-Bold="True" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Total_GC_Amount" runat="server" CssClass="LABEL" Text='<%#DataBinder.Eval(Container.DataItem,"Total_GC_Amount")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Other Charges">
                                        <HeaderStyle Width="10%" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
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
                                    <asp:TemplateColumn HeaderText="Service Tax">
                                        <HeaderStyle Width="10%" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_ServiceTax" runat="server" CssClass="LABEL" Text='<%#DataBinder.Eval(Container.DataItem,"ServiceTax")%>'></asp:Label>
                                            <asp:TextBox ID="txt_ServiceTax" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceTax") %>'
                                                runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                                Style="text-align: right; display: none" Width="90%" Font-Size="11px" Font-Names="Verdana"
                                                ReadOnly="True"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Total">
                                        <HeaderStyle Width="10%" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <ItemTemplate>
                                            <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Text='<%#DataBinder.Eval(Container.DataItem,"Bill_Total_Amount")%>'></asp:Label>
                                            <asp:TextBox ID="txt_Total" Text='<%# DataBinder.Eval(Container.DataItem, "Bill_Total_Amount") %>'
                                                runat="server" BackColor="Transparent" BorderStyle="None" BorderColor="Transparent"
                                                Style="text-align: right; display: none" Width="90%" Font-Size="11px" Font-Names="Verdana"
                                                ReadOnly="True"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                            </asp:DataGrid>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_SupplementaryGrid" />
                            <asp:AsyncPostBackTrigger ControlID="dtp_BillDate" />
                            <asp:AsyncPostBackTrigger ControlID="btn_update_grid" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_Client" />
                            <asp:AsyncPostBackTrigger ControlID="ddl_Service_Type" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </fieldset>
        </td>
    </tr>
    <tr>
        <td colspan="5">
        </td>
    </tr>
    <tr>
        <td style="width: 25%" colspan="3">
        </td>
        <td style="width: 75%" colspan="3">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <table width="100%">
                        <tr>
                            <td style="width: 17%" class="TD1">
                                <b>Grand Total :</b></td>
                            <td style="width: 20%" class="TD1" id="td_OtherCharge" runat="server">
                                <asp:Label ID="lbl_OtherCharge" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                                <asp:HiddenField ID="hdn_OtherCharge" runat="server" />
                            </td>
                            <td style="width: 10%" class="TD1" id="td_ServiceTax" runat="server">
                                <asp:Label ID="lbl_ServiceTax" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                                <asp:HiddenField ID="hdn_ServiceTax" runat="server" />
                            </td>
                            <td style="width: 10%" class="TD1" id="td_GrandTotal" runat="server">
                                <asp:Label ID="lbl_GrandTotalValue" runat="server" Text="0" CssClass="LABEL" Font-Bold="True" />
                                <asp:HiddenField ID="hdn_GrandTotalValue" runat="server" />
                            </td>
                            <td style="width: 3%">
                                &nbsp;</td>
                        </tr>
                    </table>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_update_grid" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_Client" />
                    <asp:AsyncPostBackTrigger ControlID="dg_SupplementaryGrid" />
                    <asp:AsyncPostBackTrigger ControlID="ddl_Service_Type" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr>
        <td align="right" colspan="5" style="font-weight: bold">
        </td>
    </tr>
    <tr>
        <td align="right" colspan="5" style="font-weight: bold">
        </td>
    </tr>
    <tr>
        <td align="right" colspan="5" style="font-weight: bold">
        </td>
    </tr>
    <tr id="tr_Remarks" runat="server">
        <td style="width: 20%; vertical-align: top" class="TD1">
            <asp:Label ID="lbl_Remarks" runat="server" Text="Remarks :" CssClass="LABEL"></asp:Label>
        </td>
        <td colspan="4">
            <asp:TextBox ID="txt_Remarks" runat="server" Height="50px" TextMode="MultiLine" CssClass="TEXTBOX"
                BorderWidth="1px" Width="98%" MaxLength="250"></asp:TextBox>
        </td>
        <td style="width: 1%" class="TDMANDATORY">
            *</td>
    </tr>
    <tr runat="server">
        <td class="TD1" style="vertical-align: top; width: 20%">
        </td>
        <td colspan="4">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr runat="server">
        <td class="TD1" style="vertical-align: top; width: 20%">
        </td>
        <td colspan="4">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr runat="server">
        <td class="TD1" style="vertical-align: top; width: 20%">
        </td>
        <td colspan="4">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr>
        <td colspan="6" align="center">
            <asp:Button ID="btn_Save" CssClass="BUTTON" runat="server" Text="Save" OnClick="btn_Save_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btn_SaveNew" runat="server" Text="Save & New" CssClass="BUTTON" OnClick="btn_SaveNew_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btn_Close" runat="server" Text="Exit" CssClass="BUTTON" OnClick="btn_Close_Click" />
            <asp:HiddenField ID="hdn_Is_Series_Required" runat="server"></asp:HiddenField>
            <asp:HiddenField ID="hdn_Document_Allocation_ID" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Start_No" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_End_No" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Next_No" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Padded_Next_No" runat="server" Value="0" />
            <asp:HiddenField ID="hdn_Max_Length" runat="server" Value="0" />
        </td>
    </tr>
    <tr>
        <td colspan="6">
            <asp:UpdatePanel ID="upError" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Text="Fields With * Mark are Mandatory"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Button ID="btn_update_grid" runat="server" Text="UpdateGrid" OnClick="btn_update_grid_Click" />
        </td>
    </tr>
    <tr runat="server">
        <td class="TD1" style="vertical-align: top; width: 20%">
            <asp:UpdatePanel ID="up_Hidden" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:HiddenField ID="hdn_totalgc" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </td>
        <td colspan="4">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
    <tr runat="server">
        <td class="TD1" style="vertical-align: top; width: 20%">
        </td>
        <td colspan="4">
        </td>
        <td class="TDMANDATORY" style="width: 1%">
        </td>
    </tr>
</table>

<script type="text/javascript" language="javascript">
document.getElementById('<%=btn_update_grid.ClientID%>').style.display = "none";
  
function update_grid_GCDetails()
{
document.getElementById('<%=btn_update_grid.ClientID%>').style.display = "none";
document.getElementById('<%=btn_update_grid.ClientID%>').style.visibility = "hidden";
document.getElementById('<%=btn_update_grid.ClientID%>').click();
}
</script>

