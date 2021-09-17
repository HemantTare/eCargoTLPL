<%@ Control Language="C#" AutoEventWireup="true" CodeFile="WucTrackNTraceGeneralDetails.ascx.cs"
    Inherits="TrackNTrace_WucTrackNTraceGeneralDetails" %>

<script type="text/javascript">
    function viewwindow_ForGC(Path)            //For GC View
    {        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
      window.open(Path, 'ViewGC', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
      return false;
    }  
    
    function Open_Print_Window(Path)
    {

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;              
     
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }    

    function Open_PDF_Window(Path)
    {

        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = (w-100);
        var popH = (h-100);
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;              
     
        window.open(Path, 'MainPopUp', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
    }    

</script>

<table class="TABLE">
    <tr runat="server" id="tr_Error" visible="false">
        <td class="HeadingNoBGColor" colspan="2">
            <asp:Label runat="server" ID="lbl_ErrorMsg" CssClass="LABELERROR" Text="No Data Found."></asp:Label></td>
    </tr>
    <tr>
        <td style="width: 100%">
            <table width="100%" runat="server" id="tbl_GC_View">
                <tr>
                    <td align="center" class="TopHeading" colspan="6">
                        <asp:Label ID="lbl_GC_Text" runat="server" CssClass="LABEL" />&nbsp;
                        <asp:Label ID="lbl_GC_No_For_Print" ForeColor="DarkBlue" runat="server"></asp:Label>
                        &nbsp;-&nbsp;
                        <asp:LinkButton ID="btn_View_GC" runat="server" Text="View" ForeColor="DarkBlue"></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="btn_Print_GC" runat="server" Text="Print" ForeColor="DarkBlue"></asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="btn_PDF" runat="server" ForeColor="DarkBlue" Text="PDF"></asp:LinkButton></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td style="width: 100%">
            <asp:UpdatePanel runat="server" ID="upnl_rep_general" UpdateMode="conditional">
                <ContentTemplate>
                    <asp:Repeater ID="Rep_GC_Details" runat="server" OnItemDataBound="Rep_GC_Details_ItemDataBound">
                        <ItemTemplate>
                            <table style="width: 100%; background-color: #E6D4F8" cellspacing="1" cellpadding="1"
                                border="0">
                                <tr>
                                    <td colspan="6" class="Heading" style="width: 15%">
                                        BOOKING DETAILS</td>
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        <asp:Label ID="lbl_GC_No" runat="server" CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%; font-weight: bold; color :Red">
                                        <%#Eval("GC No")%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        <asp:Label ID="lbl_GC_Date" runat="server" CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%; font-weight: bold; color: Red">
                                        <%#Eval("GC Date")%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Actual Articles</td>
                                    <td class="NumericValueNoBGColor" style="width: 15%; font-weight: bold; color: Red">
                                        <%#Eval("Actual Articles")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Booking Branch</td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%; font-weight: bold; color: Red">
                                        <%#Eval("Booking Branch")%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Delivery Branch</td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%;">
                                        <%#Eval("Delivery Branch")%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%">
                                        Private Mark</td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%">
                                        <%#Eval("Private Mark")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Booking Type</td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%;">
                                        <%#Eval("Booking Type")%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Delivery Location</td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%; font-weight: bold; color: Red">
                                        <%#Eval("To Location")%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Charged Weight</td>
                                    <td class="NumericValueNoBGColor" style="width: 20%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Charged Weight").ToString()), 0)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Delivery Type</td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%;">
                                        <%#Eval("Delivery Type")%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Payment Type
                                    </td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%; font-weight: bold; color: Red">
                                        <%#Eval("Payment Type")%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        <asp:Label ID="lbl_GC_Tot_Amt" runat="server" CssClass="LABEL"></asp:Label></td>
                                    <td class="NumericValueNoBGColor" style="width: 20%; font-weight: bold; color: Red">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Total GC Amount").ToString()), 0)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Invoice Value</td>
                                    <td class="AlphaValueNoBGColor" style="width: 15%;" align="left">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Invoice Value").ToString()), 0)%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Is Cheque</td>
                                    <td class="AlphaValueNoBGColor" style="width: 15%;">
                                        <%#Eval("Is_ChequeText")%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Is Billed</td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%;">
                                        <%#Eval("Is Billed")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Cash Amount</td>
                                    <td class="AlphaValueNoBGColor" style="width: 15%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Cash Amount").ToString()), 0)%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Risk Type</td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%;">
                                        <%#Eval("Risk Type")%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                    Tax Payable By</td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%;">
                                    <%#Eval("Tax Payable By")%></td>
                                </tr>
                                <tr runat="server" id="TD_Cheque_Details">
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Cheque No</td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%;">
                                        <%#Eval("Cheque No")%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Bank Name</td>
                                    <td class="AlphaValueNoBGColor" style="width: 15%;">
                                        <%#Eval("Bank Name")%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Cheque Amount</td>
                                    <td class="NumericValueNoBGColor" style="width: 20%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Cheque Amount").ToString()), 0)%>
                                    </td>
                                </tr> 
                                <tr runat="server" id="TD_Octroi_Details">
                                    <td class="FeildNoBGColor" style="width: 15%">
                                        Octroi Bill No</td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%">
                                        <%#Eval("Octroi Bill No")%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%">
                                        Octroi Receipt No</td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%">
                                        <%#Eval("Octroi Receipt No")%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%">
                                        Octroi Amount</td>
                                    <td class="NumericValueNoBGColor" style="width: 15%">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Octroi Amount").ToString()), 0)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        <asp:Label ID="lbl_GC_UpdatedBy" runat="server" CssClass="LABEL"></asp:Label>
                                    </td> 
                                    <td class="AlphaValueNoBGColor" style="width: 15%;">
                                        <asp:LinkButton ID="btn_GC_updated_by" Text='<%#Eval("Created By")%>' runat="server"></asp:LinkButton>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        <asp:Label ID="lbl_GC_UpdatedOn" runat="server" CssClass="LABEL"></asp:Label>
                                    </td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%;">
                                        <%#Eval("Updated On")%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                       Delivery Area </td>
                                    <td class="AlphaValueNoBGColor" style="width: 20%; font-weight: bold; color: Red">
                                       <%#Eval("DeliveryArea")%> 
                                    </td>
                                </tr>



                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        <asp:Label ID="lbl_eWayBillNo" runat="server" CssClass="LABEL">eWay Bill No.</asp:Label>
                                    </td> 
                                    <td class="AlphaValueNoBGColor" style="width: 15%; font-weight: bold; color: Red">
                                       <%#Eval("eWayBillNo")%> 
                                    </td>
                                </tr>
                                                                
                            </table>
                            <table style="width: 100%; background-color: #DFE0F8">
                                <tr>
                                    <td colspan="6" class="Heading" style="width: 15%">
                                        CLIENT DETAILS</td>
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Consignor</td>
                                    <td colspan="2" align="left" class="AlphaValueNoBGColor" style="width: 15%;">
                                        <%#Eval("Consignor Name")%>
                                    </td>
                                    <td align="left" class="FeildNoBGColor" colspan="1" style="width: 15%;">
                                        Consignee</td>
                                    <td colspan="2" class="AlphaValueNoBGColor" style="width: 20%;">
                                        <%#Eval("Consignee Name")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Consignor Contact No</td>
                                    <td class="AlphaValueNoBGColor" align="left" colspan="2" style="width: 15%;">
                                        <%#Eval("Consignor Tel No")%>
                                    </td>
                                    <td class="FeildNoBGColor" colspan="1" style="width: 15%;">
                                        Consignee Contact No</td>
                                    <td align="left" class="AlphaValueNoBGColor" colspan="2" style="width: 15%;">
                                        <%#Eval("Consignee Tel No")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        <%#Eval("Consignor_Is_Regular_Text")%>
                                    </td>
                                    <td class="AlphaValueNoBGColor" align="left" colspan="2" style="width: 15%;">
                                        <%#Eval("Consignor Is Regular")%>
                                    </td>
                                    <td class="FeildNoBGColor" colspan="1" style="width: 15%;">
                                        <%#Eval("Consignee_Is_Regular_Text")%>
                                    </td>
                                    <td class="AlphaValueNoBGColor" colspan="2" style="width: 15%;">
                                        <%#Eval("Consignee Is Regular")%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        <%#Eval("ConsigneeAddLRText")%>
                                    </td>
                                    <td class="AlphaValueNoBGColor" align="left" colspan="5" style="width: 15%;">
                                        <%#Eval("Consignee_LRAdd")%>
                                    </td> 
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        <%#Eval("ConsigneeAddMstText")%>
                                    </td>
                                    <td class="AlphaValueNoBGColor" align="left" colspan="5" style="width: 15%;">
                                        <%#Eval("Consignee_ClMstAdd")%>
                                    </td> 
                                </tr>
                            </table>
                            <table style="width: 100%; background-color: #DFE0F8">
                                <tr>
                                    <td colspan="6" class="Heading" style="width: 15%">
                                        FREIGHT COMPONENTS</td>
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Freight</td>
                                    <td class="NumericValueNoBGColor" style="width: 20%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Freight Amt").ToString()), 0)%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        FOV /Risk Charges</td>
                                    <td class="NumericValueNoBGColor" style="width: 20%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("FOV").ToString()), 0)%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        AOC</td>
                                    <td class="NumericValueNoBGColor" style="width: 20%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("AOC").ToString()), 0)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Discount</td>
                                    <td class="NumericValueNoBGColor" style="width: 20%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Discount").ToString()), 0)%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        ODA Charges</td>
                                    <td class="NumericValueNoBGColor" style="width: 15%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("ODA_Charges").ToString()), 0)%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Sub Total</td>
                                    <td class="NumericValueNoBGColor" style="width: 20%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Sub Total").ToString()), 0)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Local Charges</td>
                                    <td class="NumericValueNoBGColor" style="width: 20%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Local Charges").ToString()), 0)%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Length Charges</td>
                                    <td class="NumericValueNoBGColor" style="width: 20%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Length_Charge").ToString()), 0)%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        GST </td>
                                    <td class="NumericValueNoBGColor" style="width: 20%;">
                                        <font color='<%#Eval("STA_Color") %>'>
                                            <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Service Tax Amount").ToString()), 0)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Hamali Charges</td>
                                    <td class="NumericValueNoBGColor" style="width: 20%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Hamali Charges").ToString()), 0)%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        DD Charges</td>
                                    <td class="NumericValueNoBGColor" style="width: 15%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("DD Charges").ToString()), 0)%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Round Off(+/-)</td>
                                    <td class="NumericValueNoBGColor" style="width: 20%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Round_Off").ToString()), 0)%>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Statistics Charges</td>
                                    <td class="NumericValueNoBGColor" style="width: 20%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Bilti Charges").ToString()), 0)%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Others Charges</td>
                                    <td class="NumericValueNoBGColor" style="width: 20%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Other Charges").ToString()), 0)%>
                                    </td>
                                    <td class="FeildNoBGColor" style="width: 15%;">
                                        Total LR Amount</td>
                                    <td class="NumericValueNoBGColor" style="width: 15%;">
                                        <%#Math.Round(ClassLibraryMVP.Util.String2Decimal(Eval("Total GC Amount").ToString()), 0)%>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:Repeater>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="Rep_GC_Details" />
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr runat="server" id="tr_Billing_Party">
        <td style="width: 100%">
            <div style="width: 100%; background-color: #DFE0F8">
                <table style="width: 100%;">
                    <tr>
                        <td class="Heading" colspan="2">
                            BILLING CLIENT DETAILS</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:UpdatePanel runat="server" ID="upnl_dg_billing_det" UpdateMode="conditional">
                                <ContentTemplate>
                                    <asp:DataGrid ID="DG_Billing_Details" Width="100%" AutoGenerateColumns="false" CssClass="Grid"
                                        runat="server" OnItemDataBound="DG_Billing_Details_ItemDataBound">
                                        <AlternatingItemStyle />
                                        <HeaderStyle Font-Bold="true" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Octroi Bill No" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_Octroi_Bill_No" Text='<%#Eval("Octroi_Bill_No")%>' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Freight Bill No" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_Freight_Bill_No" Text='<%#Eval("Freight_Bill_No")%>' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Client_Name" HeaderText="Billing Client" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundColumn DataField="Bill_Ratio" HeaderText="Bill Ratio (%)" HeaderStyle-HorizontalAlign="right"
                                                ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundColumn DataField="Hierarchy_Name" HeaderText="Billing Hierarchy" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundColumn DataField="Billing_Location" HeaderText="Billing Location" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                        </Columns>
                                    </asp:DataGrid>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DG_Billing_Details" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr runat="server" id="tr_commodity">
        <td style="width: 100%">
            <div style="width: 100%; background-color: #DFE0F8">
                <table style="width: 100%;">
                    <tr>
                        <td class="Heading" colspan="2">
                            COMMODITY DETAILS</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="conditional">
                                <ContentTemplate>
                                    <asp:DataGrid ID="DG_GC_Commodity_details" Width="100%" AutoGenerateColumns="false"
                                        CssClass="Grid" runat="server">
                                        <AlternatingItemStyle />
                                        <HeaderStyle Font-Bold="true" />
                                        <Columns>
                                            <asp:BoundColumn DataField="Commodity" HeaderText="Commodity" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundColumn DataField="Item" HeaderText="Item" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundColumn DataField="Packing" HeaderText="Packing Type" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundColumn DataField="Articles" HeaderText="Articles" HeaderStyle-HorizontalAlign="right"
                                                ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundColumn DataField="Weight" HeaderText="Weight" HeaderStyle-HorizontalAlign="right"
                                                ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundColumn DataField="Width" HeaderText="Width" HeaderStyle-HorizontalAlign="right"
                                                ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundColumn DataField="Length" HeaderText="Length" HeaderStyle-HorizontalAlign="right"
                                                ItemStyle-HorizontalAlign="right" />
                                            <asp:BoundColumn DataField="Height" HeaderText="Height" HeaderStyle-HorizontalAlign="right"
                                                ItemStyle-HorizontalAlign="right" />
                                        </Columns>
                                    </asp:DataGrid>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DG_GC_Commodity_details" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr runat="server" id="tr_booking_MR">
        <td style="width: 100%">
            <div style="width: 100%; background-color: #DFE0F8">
                <table style="width: 100%;">
                    <tr>
                        <td class="Heading" colspan="2">
                            BOOKING MR DETAILS</td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:UpdatePanel runat="server" ID="upnl_dg_bookmr" UpdateMode="conditional">
                                <ContentTemplate>
                                    <asp:DataGrid ID="DG_GC_MR_Details" Width="100%" AutoGenerateColumns="false" CssClass="Grid"
                                        runat="server" OnItemDataBound="DG_GC_MR_Details_ItemDataBound">
                                        <AlternatingItemStyle />
                                        <HeaderStyle Font-Bold="true" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="MR No" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_MR_No" Text='<%#Eval("MR No")%>' runat="server"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="MR Date" HeaderText="MR Date" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundColumn DataField="MR Branch" HeaderText="MR Branch" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left" />
                                            <asp:BoundColumn DataField="MR Amount" HeaderText="MR Amount" HeaderStyle-HorizontalAlign="right"
                                                ItemStyle-HorizontalAlign="right" />
                                            <asp:TemplateColumn HeaderText="MR Updated By" HeaderStyle-HorizontalAlign="Left"
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btn_MR_UpdatedBy" Text='<%#Eval("MR Updated By")%>' runat="server"></asp:LinkButton>
                                                    <asp:HiddenField ID="hdn_MR_Id" Value='<%#Eval("MR_Id")%>' runat="server"></asp:HiddenField>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="DG_GC_MR_Details" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
</table>
