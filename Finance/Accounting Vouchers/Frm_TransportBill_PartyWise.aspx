<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Frm_TransportBill_PartyWise.aspx.cs"
    Inherits="Finance_Accounting_Vouchers_Frm_TransportBill_PartyWise" %>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register TagPrefix="ComponentArt" Namespace="ComponentArt.Web.UI" Assembly="ComponentArt.Web.UI" %>

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript">

   function Check_All(chk,gridname)
    {              
        var grid = document.getElementById(gridname);
        var i,j=0;
        var sum_TotalFreight=0,sum_TotalOtherCharges1=0,sum_TotalOtherCharges2=0,sum_Total=0,sum_TotalGST=0;
        var checkbox,txt_Freight,txtOtherCharge1,txtOtherCharge2,txt_Total, txt_GST;

        var lbl_TotalGC = document.getElementById('lbl_TotalGC');
        var lbl_TotalFreight = document.getElementById('lbl_TotalFreight');
        var lbl_TotalOtherCharge1 = document.getElementById('lbl_TotalOtherCharge1');
        var lbl_TotalOtherCharge2 = document.getElementById('lbl_TotalOtherCharge2');
        var lbl_Total = document.getElementById('lbl_Total');
        var lbl_TotalGST = document.getElementById('lbl_TotalGST');
        
        var hdn_TotalGC = document.getElementById('hdn_TotalGC');
        var hdn_TotalFreight = document.getElementById('hdn_TotalFreight');
        var hdn_TotalOtherCharge1 = document.getElementById('hdn_TotalOtherCharge1');
        var hdn_TotalOtherCharge2 = document.getElementById('hdn_TotalOtherCharge2');
        var hdn_Total = document.getElementById('hdn_Total');
        var hdn_TotalGST = document.getElementById('hdn_TotalGST');
        
        var max = (grid.rows.length - 1);

        for(i=1;i<grid.rows.length;i++)
        {            
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            txt_Freight = grid.rows[i].cells[10].getElementsByTagName('input');
            
            txtOtherCharge1 = grid.rows[i].cells[11].getElementsByTagName('input');
            txtOtherCharge2 = grid.rows[i].cells[12].getElementsByTagName('input');
            txt_Total = grid.rows[i].cells[13].getElementsByTagName('input');
            txt_GST = grid.rows[i].cells[14].getElementsByTagName('input');
            

            if(checkbox[0].type = 'checkbox')
            {
                checkbox[0].checked = chk.checked;
            }
                      
            if(chk.checked == true)
            {
                sum_TotalFreight = sum_TotalFreight + val(txt_Freight[0].value);
                sum_TotalOtherCharges1 = sum_TotalOtherCharges1 + val(txtOtherCharge1[0].value);
                sum_TotalOtherCharges2 = sum_TotalOtherCharges2 + val(txtOtherCharge2[0].value);
                sum_Total = sum_Total + val(txt_Total[0].value);
                sum_TotalGST = sum_TotalGST + val(txt_GST[0].value);
            }            
        }
        
        if(chk.checked == true)
        {
            lbl_TotalGC.innerHTML = max;
            lbl_TotalFreight.innerHTML = roundNumber(sum_TotalFreight,2);
            lbl_TotalOtherCharge1.innerHTML = roundNumber(sum_TotalOtherCharges1,2);
            lbl_TotalOtherCharge2.innerHTML = roundNumber(sum_TotalOtherCharges2,2);
            lbl_Total.innerHTML = roundNumber(sum_Total,2);
            lbl_TotalGST.innerHTML = roundNumber(sum_TotalGST,2);
            
        }
        else
        {
            lbl_TotalGC.innerHTML = '0';
            lbl_TotalFreight.innerHTML = '0';
            lbl_TotalOtherCharge1.innerHTML = '0';
            lbl_TotalOtherCharge2.innerHTML ='0';
            lbl_Total.innerHTML =  '0';
            lbl_TotalGST.innerHTML = '0';
            
        }
        
        
        hdn_TotalGC.value = lbl_TotalGC.innerHTML;
        hdn_TotalFreight = lbl_TotalFreight.innerHTML;
        hdn_TotalOtherCharge1 = lbl_TotalOtherCharge1.innerHTML;
        hdn_TotalOtherCharge2 = lbl_TotalOtherCharge2.innerHTML;
        hdn_Total = lbl_Total.innerHTML;
        hdn_TotalGST = lbl_TotalGST.innerHTML;
        
    }
    

function Check_Single(chk,gridname,callfrom)
    {

        var grid = document.getElementById(gridname);
        
        var checkall = grid.rows[0].cells[0].getElementsByTagName('input');
        
        var row = chk.parentElement.parentElement;
        var txt_Freight,txtOtherCharge1,txtOtherCharge2,txt_Total, txt_GST, txt_GSTPercent;


        
        var lbl_TotalGC = document.getElementById('lbl_TotalGC');
        var lbl_TotalFreight = document.getElementById('lbl_TotalFreight');
        var lbl_TotalOtherCharge1 = document.getElementById('lbl_TotalOtherCharge1');
        var lbl_TotalOtherCharge2 = document.getElementById('lbl_TotalOtherCharge2');
        var lbl_Total = document.getElementById('lbl_Total');
        var lbl_TotalGST = document.getElementById('lbl_TotalGST');
        
       
        txt_Freight = row.cells[10].getElementsByTagName('input');
        txtOtherCharge1 = row.cells[11].getElementsByTagName('input');
        txtOtherCharge2 = row.cells[12].getElementsByTagName('input'); 
        txt_Total  = row.cells[13].getElementsByTagName('input');
        txt_GST = row.cells[14].getElementsByTagName('input');
        txt_GSTPercent = row.cells[15].getElementsByTagName('input');


        var hdn_TotalGC = document.getElementById('hdn_TotalGC');
        var hdn_TotalFreight = document.getElementById('hdn_TotalFreight');
        var hdn_TotalOtherCharge1 = document.getElementById('hdn_TotalOtherCharge1');
        var hdn_TotalOtherCharge2 = document.getElementById('hdn_TotalOtherCharge2');
        var hdn_Total = document.getElementById('hdn_Total');
        var hdn_TotalGST = document.getElementById('hdn_TotalGST');
                
             
        if(callfrom == 1)
        {
            if(chk.checked == true)
            {
               lbl_TotalGC.innerHTML = val(lbl_TotalGC.innerHTML) + 1;
               lbl_TotalFreight.innerHTML = roundNumber(val(lbl_TotalFreight.innerHTML) + val(txt_Freight[0].value),2);
               lbl_TotalOtherCharge1.innerHTML = roundNumber(val(lbl_TotalOtherCharge1.innerHTML) + val(txtOtherCharge1[0].value),2);
               lbl_TotalOtherCharge2.innerHTML = roundNumber(val(lbl_TotalOtherCharge2.innerHTML) + val(txtOtherCharge2[0].value),2);
               lbl_Total.innerHTML = roundNumber(val(lbl_Total.innerHTML) + val(txt_Total[0].value),2);
               lbl_TotalGST.innerHTML = roundNumber(val(lbl_TotalGST.innerHTML) + val(txt_GST[0].value),2);
               
            }
            else
            {
               lbl_TotalGC.innerHTML = val(lbl_TotalGC.innerHTML) - 1;
               lbl_TotalFreight.innerHTML = roundNumber(val(lbl_TotalFreight.innerHTML) - val(txt_Freight[0].value),2);
               lbl_TotalOtherCharge1.innerHTML = roundNumber(val(lbl_TotalOtherCharge1.innerHTML) - val(txtOtherCharge1[0].value),2);
               lbl_TotalOtherCharge2.innerHTML = roundNumber(val(lbl_TotalOtherCharge2.innerHTML) - val(txtOtherCharge2[0].value),2);
               lbl_Total.innerHTML = roundNumber(val(lbl_Total.innerHTML) - val(txt_Total[0].value),2);
               lbl_TotalGST.innerHTML = roundNumber(val(lbl_TotalGST.innerHTML) - val(txt_GST[0].value),2);
        
            }
            
            if((grid.rows.length-1) == val(lbl_totalgc.innerHTML))
            {
                checkall[0].checked = true;
            }
            else
            {
                checkall[0].checked = false;
            }
       }
       else if(callfrom == 2)
       {
       
            txt_Total[0].value = val(txt_Freight[0].value) + val(txtOtherCharge1[0].value) + val(txtOtherCharge2[0].value) 

            txt_GST[0].value = roundNumber( (val(txt_Total[0].value) * val(txt_GSTPercent[0].value) / 100),0);
       }
       
        hdn_TotalGC.value = lbl_TotalGC.innerHTML;
        hdn_TotalFreight = lbl_TotalFreight.innerHTML;
        hdn_TotalOtherCharge1 = lbl_TotalOtherCharge1.innerHTML;
        hdn_TotalOtherCharge2 = lbl_TotalOtherCharge2.innerHTML;
        hdn_Total = lbl_Total.innerHTML;
        hdn_TotalGST = lbl_TotalGST.innerHTML;
    }    
    
function jsCalculateTotal(gridname)
    {        
    
        var grid = document.getElementById(gridname);
        var i;
        var sum_TotalFreight=0,sum_TotalOtherCharges1=0,sum_TotalOtherCharges2=0,sum_Total=0,sum_TotalGST=0;
        var checkbox,txt_Freight,txtOtherCharge1,txtOtherCharge2,txt_Total, txt_GST;

        var lbl_TotalGC = document.getElementById('lbl_TotalGC');
        var lbl_TotalFreight = document.getElementById('lbl_TotalFreight');
        var lbl_TotalOtherCharge1 = document.getElementById('lbl_TotalOtherCharge1');
        var lbl_TotalOtherCharge2 = document.getElementById('lbl_TotalOtherCharge2');
        var lbl_Total = document.getElementById('lbl_Total');
        var lbl_TotalGST = document.getElementById('lbl_TotalGST');
        
        var max = (grid.rows.length - 1);

        for(i=1;i<grid.rows.length;i++)
        {            
            checkbox = grid.rows[i].cells[0].getElementsByTagName('input');
            txt_Freight = grid.rows[i].cells[10].getElementsByTagName('input');
            
            txtOtherCharge1 = grid.rows[i].cells[11].getElementsByTagName('input');
            txtOtherCharge2 = grid.rows[i].cells[12].getElementsByTagName('input');
            txt_Total = grid.rows[i].cells[13].getElementsByTagName('input');
            txt_GST = grid.rows[i].cells[14].getElementsByTagName('input');

            if(checkbox[0].checked == true)
            {

                sum_TotalFreight = sum_TotalFreight + val(txt_Freight[0].value);
                sum_TotalOtherCharges1 = sum_TotalOtherCharges1 + val(txtOtherCharge1[0].value);
                sum_TotalOtherCharges2 = sum_TotalOtherCharges2 + val(txtOtherCharge2[0].value);
                sum_Total = sum_Total + val(txt_Total[0].value);
                sum_TotalGST = sum_TotalGST + val(txt_GST[0].value);

            }            
        }
        

        lbl_TotalGC.innerHTML = max;
        lbl_TotalFreight.innerHTML = roundNumber(sum_TotalFreight,2);
        lbl_TotalOtherCharge1.innerHTML = roundNumber(sum_TotalOtherCharges1,2);
        lbl_TotalOtherCharge2.innerHTML = roundNumber(sum_TotalOtherCharges2,2);
        lbl_Total.innerHTML = roundNumber(sum_Total,2);
        lbl_TotalGST.innerHTML = roundNumber(sum_TotalGST,2);
            
    }

    function Allow_To_Save()
    {
        var ATS = false;
        
        var lbl_TotalGC = document.getElementById('lbl_TotalGC');
        var lbl_Total = document.getElementById('lbl_Total');
        var lblErrors = document.getElementById('lblErrors');

        
        lblErrors.innerHTML ="";                
         
        if(val(lbl_TotalGC.innerHTML) == 0)
        {
            lblErrors.innerHTML = "Please Select Atleast One LR";
        }
        else if (val(lbl_Total.innerHTML) <= 0)
        {
            lblErrors.innerHTML =  "Total LR Amount should be greater than Zero";

        }       
        else
        {
            ATS = true;
        }
        return ATS;
     }    
     

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Transport Bill</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body onunload="refreshParentPage();">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="3">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Transport Bill"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 10%">
                        Bill Date :</td>
                    <td style="width: 20%">
                        <uc1:WucDatePicker ID="dtp_BillDate" runat="server" />
                    </td>
                    <td style="width: 70%">
                        Bill No. :
                        <asp:Label ID="lbl_TransBillNo" runat="server" Text="" Width="45%" Font-Bold="true"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 10%; height: 15px;">
                        Client Name :</td>
                    <td style="width: 20%; height: 15px;">
                        <asp:Label ID="lbl_ClientName" runat="server" CssClass="LABELERROR" EnableViewState="False"
                            Text=""></asp:Label></td>
                    <td style="width: 70%; height: 15px;">
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        &nbsp;
                    </td>
                </tr>
                <tr style="height: 500px" valign="top">
                    <td style="width: 100%" align="left" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div id="Div_GRID" class="DIV" style="height: 500px;">
                                    <asp:DataGrid ID="dg_Details" runat="server" AutoGenerateColumns="False" CssClass="GRID"
                                        Style="border-top-style: none" Width="90%" OnItemDataBound="dg_Details_ItemDataBound">
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="DATAGRIDFIXEDHEADER"></HeaderStyle>
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Att" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                    <input id="chkAllItems" type="checkbox" onclick="Check_All(this,'dg_Details');" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="Chk_Attach" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Att").ToString()) %>'
                                                        OnClick="Check_Single(this,'dg_Details','1');" runat="server" />
                                                </ItemTemplate>
                                                <ItemStyle Width="10px" />
                                                <HeaderStyle Width="10px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Bkg Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_GCDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GC_Date") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="LR No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbl_GCNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GC_No") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="Consignor" HeaderText="Consignor"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Consignee" HeaderText="Consignee"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FromLoc" HeaderText="From Loc"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="FromLoc" HeaderText="From Loc"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="ToLoc" HeaderText="To Loc"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="BookingType" HeaderText="Load"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="InvoiceNo" HeaderText="Invoice No"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Freight">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_Freight" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        BorderColor="Transparent" Style="text-align: right;" Width="90%" Font-Size="11px"
                                                        Font-Names="Verdana" ReadOnly="True" Text='<%# DataBinder.Eval(Container.DataItem, "Freight") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="80px" />
                                                <HeaderStyle Width="80px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Other Charges1">
                                                <HeaderTemplate>
                                                    <asp:DropDownList ID="ddl_OtherCharges1" runat="server" CssClass="DROPDOWN" Width="98%"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_OtherCharges1_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOtherCharge1" Text='<%# DataBinder.Eval(Container.DataItem, "OtherCharges1") %>'
                                                        runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                                        Width="95%" MaxLength="8" Onblur="Check_Single(this,'dg_Details','2'); jsCalculateTotal('dg_Details');"
                                                        onfocus="this.select();" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="150px" />
                                                <HeaderStyle Width="150px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Other Charges2">
                                                <HeaderTemplate>
                                                    <asp:DropDownList ID="ddl_OtherCharges2" runat="server" CssClass="DROPDOWN" Width="98%"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddl_OtherCharges2_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOtherCharge2" Text='<%# DataBinder.Eval(Container.DataItem, "OtherCharges2") %>'
                                                        runat="server" CssClass="TEXTBOXNOS" onkeypress="return Only_Numbers(this,event)"
                                                        Width="95%" MaxLength="8" Onblur="Check_Single(this,'dg_Details','2'); jsCalculateTotal('dg_Details');"
                                                        onfocus="this.select();" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="150px" />
                                                <HeaderStyle Width="150px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_Total" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        BorderColor="Transparent" Style="text-align: right;" Width="90%" Font-Size="11px"
                                                        Font-Names="Verdana" ReadOnly="True" Text='<%# DataBinder.Eval(Container.DataItem, "Total") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="80px" />
                                                <HeaderStyle Width="80px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="GST">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_GST" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        BorderColor="Transparent" Style="text-align: right;" Width="90%" Font-Size="11px"
                                                        Font-Names="Verdana" ReadOnly="True" Text='<%# DataBinder.Eval(Container.DataItem, "GST") %>'></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="60px" />
                                                <HeaderStyle Width="60px" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="" HeaderStyle-Width="0%">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txt_GSTPercent" runat="server" BackColor="Transparent" BorderStyle="None"
                                                        BorderColor="Transparent" Style="text-align: right; display: none" Width="90%"
                                                        Font-Size="11px" Font-Names="Verdana" ReadOnly="True" Text='<%# DataBinder.Eval(Container.DataItem, "GSTPercent") %>'></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                <asp:AsyncPostBackTrigger ControlID="dg_Details" EventName="ItemCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" align="left" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <table class="TABLE" width="100%">
                                    <tr>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 5%">
                                            <asp:Label ID="lbl_TotalH" runat="server" CssClass="LABEL" Text="Total" Width="100%"
                                                Font-Bold="true"></asp:Label></td>
                                        <td style="width: 3%">
                                            <asp:Label ID="lbl_TotalGC" runat="server" CssClass="LABEL" Text="0" Width="100%"
                                                Font-Bold="true"></asp:Label>
                                            <asp:HiddenField ID="hdn_TotalGC" runat="server" />
                                        </td>
                                        <td class="TD1" style="width: 5%">
                                            <asp:Label ID="lbl_TotalFreight" runat="server" CssClass="LABEL" Text="0.00" Width="100%"
                                                Font-Bold="true"></asp:Label>
                                            <asp:HiddenField ID="hdn_TotalFreight" runat="server" />
                                        </td>
                                        <td class="TD1" style="width: 5%">
                                            <asp:Label ID="lbl_TotalOtherCharge1" runat="server" CssClass="LABEL" Text="0.00"
                                                Width="100%" Font-Bold="true"></asp:Label>
                                            <asp:HiddenField ID="hdn_TotalOtherCharge1" runat="server" />
                                        </td>
                                        <td class="TD1" style="width: 5%">
                                            <asp:Label ID="lbl_TotalOtherCharge2" runat="server" CssClass="LABEL" Text="0.00"
                                                Width="100%" Font-Bold="true"></asp:Label>
                                            <asp:HiddenField ID="hdn_TotalOtherCharge2" runat="server" />
                                        </td>
                                        <td class="TD1" style="width: 5%">
                                            <asp:Label ID="lbl_Total" runat="server" CssClass="LABEL" Text="0.00" Width="100%"
                                                Font-Bold="true"></asp:Label>
                                            <asp:HiddenField ID="hdn_Total" runat="server" />
                                        </td>
                                        <td class="TD1" style="width: 5%">
                                            <asp:Label ID="lbl_TotalGST" runat="server" CssClass="LABEL" Text="0.00" Width="100%"
                                                Font-Bold="true"></asp:Label>
                                            <asp:HiddenField ID="hdn_TotalGST" runat="server" />
                                        </td>
                                        <td class="TD1" style="width: 2%">
                                            &nbsp;
                                            <asp:HiddenField ID="hdn_OtherCharge1_Id" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdn_OtherCharge1" runat="server" Value="" />
                                            <asp:HiddenField ID="hdn_OtherCharge2_Id" runat="server" Value="0" />
                                            <asp:HiddenField ID="hdn_OtherCharge2" runat="server" Value="" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                <asp:AsyncPostBackTrigger ControlID="dg_Details" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel" UpdateMode="Conditional" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text=""></asp:Label>
                                <asp:HiddenField ID="hdnKeyID" runat="server" />
                                <asp:HiddenField ID="hdn_Client_Id" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                <asp:AsyncPostBackTrigger ControlID="dg_Details" EventName="ItemCommand" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <asp:Button ID="btnSave" runat="server" CssClass="BUTTON" Text="Save & Print" OnClick="btnSave_Click" />
                        &nbsp; &nbsp; &nbsp;
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
