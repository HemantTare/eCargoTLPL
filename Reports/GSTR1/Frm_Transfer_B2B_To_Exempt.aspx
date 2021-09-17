<%@ Page AutoEventWireup="true" CodeFile="Frm_Transfer_B2B_To_Exempt.aspx.cs" Inherits="Reports_GSTR1_Frm_Transfer_B2B_To_Exempt"
    Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript" src="../../JQuery/jquery-1.12.4.min.js"></script>

<script language="javascript" type="text/javascript" src="../../Javascript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/txtsearch_common.js"></script>

<script type="text/javascript" language="javascript" src="../../Javascript/DatePicker.js"></script>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>

<script type="text/javascript">


function GetGCDetails()
{
 
 
 
    var txt_GCNo =  document.getElementById('txt_GCNo');
    var ddl_Month =  document.getElementById('ddl_Month');
    var hdn_YearCode =  document.getElementById('hdn_YearCode');
 
 
      
    if(txt_GCNo.value != '')
    {
        Raj.EF.CallBackFunction.CallBack.GetGCDetails(val(txt_GCNo.value), val(ddl_Month.value), val(hdn_YearCode.value), handleValidGCNo);

    }
    
}


function handleValidGCNo(Results)
{

    var txt_GCNo =  document.getElementById('txt_GCNo');
    var lbl_LRNo = document.getElementById('lbl_LRNo');
    var hdn_GC_ID = document.getElementById('hdn_GC_ID');
    var lbl_LRDate = document.getElementById('lbl_LRDate');
    var lbl_Consignor = document.getElementById('lbl_Consignor');
    var lbl_Consignee = document.getElementById('lbl_Consignee');

   
    var hdn_GC_Date = document.getElementById('hdn_GC_Date');
    var hdn_Consignor = document.getElementById('hdn_Consignor');
    var hdn_Consignee = document.getElementById('hdn_Consignee');


    var btn_AddLR = document.getElementById('btn_AddLR.ClientID');



    var btn_AddLR = '<%= btn_AddLR.ClientID %>';

    var Result = Results.value.Rows[0]['Is_Found'];
    
    if(Result == false)
    {
        lbl_LRNo.innerHTML = 'LR No. : ' 
        lbl_LRDate.innerHTML = 'Bkg Date : ' 
        lbl_Consignor.innerHTML = 'Consignee : ' 
        lbl_Consignee.innerHTML = 'Consignee : ' 
        hdn_GC_ID.value= '0';
        
        hdn_Consignor.value = '';
        hdn_Consignee.value = '';
        
        
        document.getElementById(btn_AddLR).disabled = true;
        
        alert("No Record Found.");
        txt_GCNo.focus();
    }
    else
    {
        lbl_LRNo.innerHTML = 'LR No. : ' +  Results.value.Rows[0]['GC_No_For_Print'];
        
        hdn_GC_ID.value = Results.value.Rows[0]['GC_ID'];

        lbl_LRDate.innerHTML = 'Bkg Date : ' + Results.value.Rows[0]['GC_Date'];
        lbl_Consignor.innerHTML = 'Consignor : ' + Results.value.Rows[0]['Consignor']; 
        lbl_Consignee.innerHTML = 'Consignee : ' + Results.value.Rows[0]['Consignee']; 
                
        hdn_GC_Date.value = Results.value.Rows[0]['GC_Date'];
        hdn_Consignor.value = Results.value.Rows[0]['Consignor'];
        hdn_Consignee.value = Results.value.Rows[0]['Consignee'];
        
        
        document.getElementById(btn_AddLR).disabled = false;

        document.getElementById(btn_AddLR).focus();

    }
    
}
 

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>B2B GSTIN Verification</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scm_DeliveryStockList" runat="server">
        </asp:ScriptManager>
        <table id="Table1" runat="server" class="TABLE">
            <tr>
                <td class="TDGRADIENT" style="width: 100%">
                    <asp:Label ID="lbl_Heading" runat="server" CssClass="HEADINGLABEL" Text="B2B GSTIN Verification"></asp:Label>
                </td>
            </tr>
        </table>
        <table id="tbl_input_screen" runat="server" class="TABLE">
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%" id="TABLE2" onclick="return TABLE2_onclick()">
                        <tr>
                            <td style="width: 34%; height: 15px;" colspan="2" class="TD1">
                                <asp:Label ID="lbl_Month" runat="server" CssClass="LABEL" Text="Month :" Font-Bold="true"
                                    ForeColor="#990066" /></td>
                            <td style="width: 33%; height: 15px;" colspan="2">
                                <asp:DropDownList ID="ddl_Month" runat="server">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdn_YearCode" Value="0" runat="server" />
                            </td>
                            <td style="width: 9%; height: 15px;">
                            </td>
                            <td style="width: 24%; height: 15px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 67%; height: 20px; background-color: Thistle;" colspan="4">
                                <asp:Label ID="lbl_GCNo" runat="server" CssClass="LABEL" Text="Enter LR No. : " Font-Bold="true"
                                    Font-Size="Medium" />
                                <asp:TextBox ID="txt_GCNo" runat="server" CssClass="TEXTBOX" MaxLength="7" Width="10%"
                                    onkeypress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)"
                                    onblur="txtbox_onlostfocus(this); GetGCDetails();" Font-Size="Medium"></asp:TextBox>
                            </td>
                            <td style="width: 9%; height: 20px;">
                                &nbsp;
                            </td>
                            <td style="width: 24%; height: 20px;">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="width: 34%; height: 20px; background-color: LightYellow;" colspan="2">
                                <asp:Label ID="lbl_LRNo" runat="server" CssClass="LABEL" Text="LR No." Font-Bold="true" />
                                <asp:HiddenField ID="hdn_GC_ID" Value="0" runat="server" />
                                &nbsp;
                            </td>
                            <td style="width: 33%; height: 20px; background-color: LightYellow;" colspan="2">
                                <asp:Label ID="lbl_LRDate" runat="server" CssClass="LABEL" Text="Bkg Date :" Font-Bold="true" />
                                <asp:HiddenField ID="hdn_GC_Date" Value="0" runat="server" />
                            </td>
                            <td style="width: 9%; height: 20px;">
                                &nbsp;
                            </td>
                            <td style="width: 9%; height: 24px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 34%; height: 20px; background-color: LightYellow;" colspan="2">
                                <asp:Label ID="lbl_Consignee" runat="server" CssClass="LABEL" Text="Consignee : "
                                    Font-Bold="true" />
                                <asp:HiddenField ID="hdn_Consignee" Value="0" runat="server" />
                            </td>
                            <td style="width: 33%; height: 20px; background-color: LightYellow;" colspan="2">
                                <asp:Label ID="lbl_Consignor" runat="server" CssClass="LABEL" Text="Consignor : "
                                    Font-Bold="true" />
                                <asp:HiddenField ID="hdn_Consignor" Value="0" runat="server" />
                            </td>
                            <td style="width: 9%; height: 20px;">
                                <asp:Button ID="btn_AddLR" runat="server" CssClass="BUTTON" OnClick="btn_AddLR_Click"
                                    Text="Add LR" Enabled="false" /></td>
                            <td style="width: 24%; height: 20px;" class="HIDEGRIDCOL">
                                <asp:Button ID="btn_Close" runat="server" CssClass="BUTTON" OnClick="btn_null_session_Click"
                                    Text="Close Window" /></td>
                        </tr>
                        <tr>
                            <td class="TD1" colspan="6">
                                <asp:UpdatePanel ID="UpdatePanel" UpdateMode="Conditional" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbl_Error" runat="server" CssClass="LABELERROR" Text=""></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table class="TABLE" width="100%">
            <tr id="tr_Grid" runat="server">
                <td style="width: 100%; vertical-align: top; height: 390px;">
                    <div id="Grid" class="DIV" style="height: 380px; vertical-align: top;">
                        <table style="width: 100%">
                            <tr>
                                <td style="height: 214px; vertical-align: top;">
                                    <asp:UpdatePanel ID="up_dgGrid" runat="server">
                                        <ContentTemplate>
                                            <asp:DataGrid Style="width: 100%" ID="dg_Grid" runat="server" AutoGenerateColumns="False"
                                                BorderStyle="Solid" BorderColor="Black" ShowFooter="false" OnItemDataBound="dg_Grid_ItemDataBound">
                                                <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                                <FooterStyle CssClass="GRIDFOOTERCSS" />
                                                <HeaderStyle CssClass="GRIDHEADERCSS" />
                                                <Columns>
                                                    <asp:TemplateColumn HeaderText="BkgDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_BkgDate" runat="server" Text='<%#Eval("GC_Date")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="LR No." HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="lnk_LRNo" runat="server" CssClass="LABEL" Font-Bold="true" Text='<%# DataBinder.Eval(Container.DataItem, "GC_No") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Consignor" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_ConsignorName" runat="server" Text='<%#Eval("Consignor")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Consignee" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbl_ConsigneeName" runat="server" Text='<%#Eval("Consignee")%>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                                            <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr id="tr4" runat="server">
                <td style="width: 100%; vertical-align: top;" align="left">
                    <asp:UpdatePanel ID="UpdatePanel1" UpdateMode="Conditional" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbl_TotalLR" runat="server" CssClass="TEXBOX" Text="Total LR : 0"
                                Font-Bold="true" Font-Size="Medium" ForeColor="#9900cc"></asp:Label>
                            <asp:HiddenField ID="hdn_TotalLR" Value="0" runat="server" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                            <asp:AsyncPostBackTrigger ControlID="btn_Save" />
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr id="tr2" runat="server">
                <td style="width: 100%; vertical-align: top;" align="center">
                    &nbsp;</td>
            </tr>
            <tr id="tr3" runat="server">
                <td style="width: 100%; vertical-align: top;" align="center">
                    <asp:Button ID="btn_Save" runat="server" AccessKey="S" CssClass="BUTTON" Text="Save"
                        OnClick="btn_Save_Click" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
