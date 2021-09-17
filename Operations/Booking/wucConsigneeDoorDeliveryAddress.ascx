<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucConsigneeDoorDeliveryAddress.ascx.cs"
    Inherits="Operations_Booking_wucConsigneeDoorDeliveryAddress" %>
<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />

<script type="text/javascript" src="../../Javascript/Common.js"></script>

<%--<script language="javascript" type="text/javascript" src="../../Javascript/Operations/Booking/GC.js"></script>--%>

<asp:ScriptManager ID="SM_ConsigneeDoorDeliveryAddress" runat="server">
</asp:ScriptManager>

<script type="text/javascript">
 function Update_Door_Delivery_Adderess()
 { 
   window.opener.SetDoor_Delivery_Adderess(); 
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
    var hdn_Mode = document.getElementById('wucConsigneeDoorDeliveryAddress1_hdn_Mode');   
    var btn_Exit = document.getElementById('wucConsigneeDoorDeliveryAddress1_btn_Exit');
    var Enable = true;
        
    if (val(hdn_Mode.value) == 4)
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
    }  
}

</script>

<table class="TABLE" cellpadding="4">
    <tr>
        <td class="TDGRADIENT" colspan="3">
            &nbsp;<asp:Label ID="Label2" runat="server" CssClass="HEADINGLABEL" Text="Door Delivery Address..."></asp:Label></td>
    </tr>
    <tr>
    <td class="TDUnderline" colspan="3">
    </td>
</tr>
    <tr>
        <td colspan="3">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_Consignee_Name" CssClass="LABEL" Text="Consignee Name :" runat="server"></asp:Label>
        </td>
        <td>
            <asp:Label ID="lbl_Consignee_Name_Value" runat="server" Text="Consignee Name:" Style="font-weight: bold;"
                Width="394px"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_Consignee_AddressLine1" CssClass="LABEL" Text="Address Line 1 :"
                runat="server"></asp:Label>
        </td>
        <td style="height: 77px">
            <asp:TextBox ID="Txt_Consignee_AddressLine1" CssClass="TEXTBOX" MaxLength="100" runat="server" EnableViewState="false" TabIndex="3"
                onkeyPress=" return Check_Max_Length_For_Multiline(this,event,100);" Width="224px" Height="67px" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 50%;">
            <asp:Label ID="lbl_Consignee_AddressLine2" CssClass="LABEL" Text="Address Line 2 :"
                runat="server"></asp:Label>
        </td>
        <td style="height: 77px">
            <asp:TextBox ID="Txt_Consignee_AddressLine2" CssClass="TEXTBOX" MaxLength="100" runat="server" EnableViewState="false" TabIndex="3"
                onkeyPress=" return Check_Max_Length_For_Multiline(this,event,100);" Width="224px" Height="67px" TextMode="MultiLine">
            </asp:TextBox>
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
                    <asp:AsyncPostBackTrigger ControlID="btn_Exit"></asp:AsyncPostBackTrigger>
                </Triggers>
            </asp:UpdatePanel>
               
        </td>
    </tr>
              
    <tr align="left">
        <td align="center" colspan="2">
         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
            <asp:Button ID="Btn_Save" runat="server" CssClass="SMALLBUTTON" Text="Save" OnClick="btn_Save_Click"
                ValidationGroup="k" TabIndex="5" AccessKey="S" />
            <asp:Button ID="btn_Exit" runat="server" OnClick="btn_Exit_Click" CssClass="SMALLBUTTON"  ValidationGroup="k"
                Text="Exit" AccessKey="E" />
                  </ContentTemplate>
                <Triggers>                    
                    <asp:AsyncPostBackTrigger ControlID="Btn_Save"></asp:AsyncPostBackTrigger>
                    <asp:AsyncPostBackTrigger ControlID="btn_Exit"></asp:AsyncPostBackTrigger>
                </Triggers>
            </asp:UpdatePanel>
                
               
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