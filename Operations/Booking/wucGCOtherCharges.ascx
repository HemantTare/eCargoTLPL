<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucGCOtherCharges.ascx.cs" Inherits="Operations_Booking_wucGCOtherCharges" %>
<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script language="javascript" type="text/javascript" src="../../Javascript/Operations/Booking/GC.js"></script>

<asp:ScriptManager ID="SM_ConsigneeDoorDeliveryAddress" runat="server"></asp:ScriptManager>

<script type="text/javascript">
  
 function UpdateOtherCharges()
 { 
  window.opener.GetOtherCharges(); 
 } 
 
 function Allow_To_Exit()
{   
    var ATE = false;
    
    if (confirm("Do you want to Exit...")==false)
        {
            ATE=false;
        }
    else
        {
            ATE=true;
        }
        
    if (ATE)
        {  
            window.close();
            return true;
        }
    else
        {
            return false;
        }
}

function On_View()
{    
    var hdn_Mode = document.getElementById('wucGCOtherCharges1_hdn_Mode'); 
    var btn_Exit = document.getElementById('wucGCOtherCharges1_btn_Exit');
    var btn_Save = document.getElementById('wucGCOtherCharges1_btn_Save');

    var Enable = true; 
        
    if (val(hdn_Mode.value ) == 4)
    {           
        for(i = 0; i < document.forms[0].elements.length; i++) 
        {        
            elm = document.forms[0].elements[i];

            if(elm.id!='')
            {
                var elm_id = document.getElementById(elm.id);
                var elm_name = elm.name;
                var arr = elm_name.split("$");                                     
                
                if (elm.type != 'lable')
                {                    
                   elm.disabled = Enable;
                }
            }
//             Btn_Save.style.display = 'none';
        }
        btn_Exit.disabled = false;
    }  
 }
</script>


<table class="TABLE" cellpadding="4">
    <tr>
        <td class="TDGRADIENT" colspan="3">
            &nbsp;<asp:Label ID="lbl_GCOtherCharges" runat="server" CssClass="HEADINGLABEL" ></asp:Label></td>
    </tr>
    <tr>
        <td class="TDUnderline" colspan="3"></td>
    </tr>
    <tr>
        <td colspan="3">&nbsp;</td>
    </tr>     
    <tr>      
        <td colspan="3"  align="center"  style="height: 77px">
            <asp:Panel ID="pnl_GCOtherChargeHead" runat="server" GroupingText="Gc Other Charges" Font-Bold="True"
                Width="100%" CssClass="TABLE" BorderColor="White" Height ="250" ScrollBars="vertical" BorderStyle="None" BorderWidth="0px">
                <asp:DataGrid ID="dg_GCOtherChargeHead" TabIndex="370" runat="server" meta:resourcekey="dg_CommodityResource1"
                    CssClass="Grid" Width="100%" AutoGenerateColumns="False" ShowFooter="True" PageSize="5"
                    OnItemDataBound="dg_GCOtherChargeHead_ItemDataBound"
                    CellPadding="3" AllowSorting="True">
                    <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                    <PagerStyle Mode="NumericPages" CssClass="GRIDPAGERCSS"></PagerStyle>
                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                    <HeaderStyle CssClass="GRIDHEADERCSS"></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="Attach">
                            <HeaderTemplate>
                                <input id="chk_CheckedAll" type="checkbox" onclick="Check_All(this,'wucGCOtherCharges1_dg_GCOtherChargeHead');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_Checked" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Checked").ToString())%>'
                                OnClick="Calculate_Summary(this,'wucGCOtherCharges1_dg_GCOtherChargeHead');"     runat="server" />
                            </ItemTemplate>
                            <ItemStyle Width="5%" />
                            <HeaderStyle  Width="5%" />
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="GC_ID" HeaderText="GC Id" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="GC_Other_Charge_Head_Id" HeaderText="GC Id" Visible="False">
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="Other Charge Head" SortExpression="GC_Other_Charge_Head">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "GC_Other_Charge_Head")) %>'
                                    CssClass="LABEL" ID="lbl_GC_Other_Charge_Head" ItemStyle-HorizontalAlign="Left"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="30%" CssClass="SORTINGLNKBTN" HorizontalAlign="Center"></HeaderStyle>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Description" SortExpression="Description">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox runat="server" BorderWidth="1px" Width="80%" CssClass="TEXTBOX" ID="txt_Description"
                                    onblur="txtbox_onlostfocus(this)"  Text='<%# (DataBinder.Eval(Container.DataItem, "Description")) %>' onfocus="txtbox_onfocus(this)"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="30%" CssClass="SORTINGLNKBTN" HorizontalAlign="Center"></HeaderStyle>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Amount" SortExpression="Amount">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txt_Amount" runat="server" BorderWidth="1px" Width="80%" CssClass="TEXTBOX"
                                    onblur="txtbox_onlostfocus(this)"  MaxLength="7" Text='<%# (DataBinder.Eval(Container.DataItem, "Amount")) %>' onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="10%" CssClass="SORTINGLNKBTN" HorizontalAlign="Center"></HeaderStyle>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="2" align="right"></td>
    </tr>
    
     <tr>
     <td class="TD1" style="width: 30%;">
     <asp:Label ID="lbl_Total" runat="server" Font-Bold="true" Text="Total" CssClass="LABEL" Visible="true"></asp:Label>
     </td>
        <td align="right">
            <asp:Label ID="lbl_TotalGCOtherCharges" runat="server" Font-Bold="true" CssClass="LABEL" Visible="true"></asp:Label>
             &nbsp; &nbsp; &nbsp; &nbsp; <asp:HiddenField ID="hdn_OtherChargesCount" runat="server" />
            <asp:HiddenField ID="hdn_TotalGCOtherCharges" runat="server" />            
            </td>
    </tr>    
    <tr>
        <td colspan="2">
          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
            <asp:Label ID="lbl_Errors" runat="server" Font-Bold="true" CssClass="LABEL" ForeColor="Red"
                Visible="true"></asp:Label>
                 </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save"></asp:AsyncPostBackTrigger>
                </Triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr align="left">
        <td align="center" colspan="2">
         <asp:Button ID="Btn_Save" runat="server" CssClass="SMALLBUTTON" Text="Save" OnClick="btn_Save_Click"
                ValidationGroup="k"  AccessKey="S" />
            &nbsp;<asp:Button ID="btn_Exit" runat="server" OnClick="btn_Exit_Click" CssClass="SMALLBUTTON"
                Text="Exit" AccessKey="E" />
        </td>
    </tr>
    <tr>
        <td>
            <input id="hdn_Consignee_Addess" runat="server" type="hidden" /></td>
        <td>
            <input id="hdn_Consignee_TelNo" runat="server" type="hidden" />
            <input id="hdn_Co_Name" runat="server" type="hidden" />            
            <asp:HiddenField runat="server" ID="hdn_Mode"></asp:HiddenField>
        </td>
    </tr>
</table>

<script type="text/javascript" language="javascript">    
    On_View();    
</script>
