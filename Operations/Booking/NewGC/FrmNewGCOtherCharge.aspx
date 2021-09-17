<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmNewGCOtherCharge.aspx.cs" Inherits="Operations_Booking_NewGC_FrmNewGCOtherCharge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript" language="javascript" src="../../../Javascript/Common.js"></script>
<script type="text/javascript" language="javascript" src="../../../Javascript/Operations/Booking/GCNew.js"></script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title><%=CompanyManager.getCompanyParam().GcCaption%> Other Charge</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript">
  
 function UpdateOtherCharges(TotalOtherCharges)
 { 
  window.opener.call_OtherCharges(TotalOtherCharges); 
 } 
 
function Allow_To_Exit()
{   
    var ATE = false;

    if (confirm("Do you want to Exit ?")==false)
    {
        ATE=false;
    }
    else
    {
        window.close();
        ATE=true;
    } 
    return ATE;
}

function On_View()
{    
    var hdn_Mode = document.getElementById('hdn_Mode');
    var btn_Exit = document.getElementById('btn_Exit');
    var btn_Save = document.getElementById('btn_Save');

    var Enable = true; 
        
    if (val(hdn_Mode.value)== 4)
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
        }
        btn_Exit.disabled = false;
        btn_Save.style.visibility = 'hidden';
    }  
 }
</script>
</head>
<body leftmargin="2" topmargin="5" rightmargin="2" bottommargin="5">
<form id="form1" runat="server">
<asp:ScriptManager ID="scm_OtherCharges" runat="server"></asp:ScriptManager>
<div>
<table class="TABLE" border="0">
    <tr>
        <td class="TDGRADIENT" colspan="2">&nbsp;
            <asp:Label ID="lbl_GCOtherCharges" runat="server" CssClass="HEADINGLABEL"></asp:Label></td>
    </tr>   
    <tr>
        <td colspan="2">&nbsp;</td>
    </tr>     
    <tr>      
        <td colspan="2" align="center">
            <asp:Panel ID="pnl_GCOtherChargeHead" runat="server" GroupingText="Gc Other Charges" Font-Bold="True"
                Width="100%" CssClass="TABLE" BorderColor="White" Height ="250" ScrollBars="vertical">
                <asp:DataGrid ID="dg_GCOtherChargeHead" runat="server" CssClass="Grid" Width="100%" AutoGenerateColumns="False"
                    OnItemDataBound="dg_GCOtherChargeHead_ItemDataBound" CellPadding="3">
                    <FooterStyle CssClass="GRIDFOOTERCSS"></FooterStyle>
                    <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS"></AlternatingItemStyle>
                    <HeaderStyle CssClass="GRIDHEADERCSS"></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn HeaderText="Attach">
                            <HeaderTemplate>
                                <input id="chk_CheckedAll" type="checkbox" onclick="Check_All(this,'dg_GCOtherChargeHead');" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="chk_Checked" Checked='<%# ClassLibraryMVP.Util.String2Bool(DataBinder.Eval(Container.DataItem, "Checked").ToString())%>'
                                OnClick="Check_Single(this,'dg_GCOtherChargeHead');" runat="server" />
                            </ItemTemplate>
                            <HeaderStyle Width="5%"></HeaderStyle>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Other Charge Head">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# (DataBinder.Eval(Container.DataItem, "GC_Other_Charge_Head")) %>' CssClass="LABEL" ID="lbl_GC_Other_Charge_Head"/>
                            </ItemTemplate>
                            <HeaderStyle Width="50%" HorizontalAlign="left"></HeaderStyle>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Description">
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox runat="server" Width="80%" CssClass="TEXTBOX" ID="txt_Description" Text='<%#(DataBinder.Eval(Container.DataItem, "Description"))%>' onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="30%" HorizontalAlign="left"></HeaderStyle>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Amount">
                            <ItemStyle HorizontalAlign="right"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="txt_Amount" runat="server" Width="80%" CssClass="TEXTBOXNOS"
                                    MaxLength="7" Text='<%# (DataBinder.Eval(Container.DataItem, "Amount")) %>' onkeyPress="return Only_Numbers(this,event);" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Width="15%" HorizontalAlign="Right"></HeaderStyle>
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
            </asp:Panel>
        </td>
    </tr>
    <tr>
        <td colspan="2">&nbsp;</td>
    </tr>    
    <tr>
        <td class="TD1" style="width: 80%;">
            <asp:Label ID="lbl_Total" runat="server" Font-Bold="true" Text="Total :" CssClass="LABEL"></asp:Label>
        </td>
        <td align="center">
            <asp:Label ID="lbl_TotalGCOtherCharges" runat="server" Font-Bold="true" CssClass="TEXTBOXNOS"></asp:Label>
        </td>
    </tr>    
    <tr>
        <td colspan="2">
          <asp:UpdatePanel ID="UpdatePanel2" runat="server">
          <ContentTemplate>
                <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR"></asp:Label>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_Save"></asp:AsyncPostBackTrigger>
            </Triggers>
         </asp:UpdatePanel>
        </td>
    </tr>
    <tr align="left">
        <td align="center" colspan="2">
            <asp:Button ID="Btn_Save" runat="server" CssClass="SMALLBUTTON" Text="Save" OnClick="btn_Save_Click" AccessKey="S" />&nbsp;
            <asp:Button ID="btn_Exit" runat="server" OnClick="btn_Exit_Click" CssClass="SMALLBUTTON" Text="Exit" OnClientClick="return Allow_To_Exit()" AccessKey="E" />
        </td>
    </tr>
    <tr>        
        <td colspan="2">&nbsp;
            <asp:HiddenField runat="server" ID="hdn_Mode"></asp:HiddenField>
            <asp:HiddenField ID="hdn_OtherChargesCount" runat="server" />
            <asp:HiddenField ID="hdn_TotalGCOtherCharges" runat="server" />
        </td>
    </tr>
</table>
</div>
</form>
</body>
</html>
<script type="text/javascript" language="javascript">    
    On_View();    
</script>
