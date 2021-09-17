<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmNewGCConsigneeDDAddress.aspx.cs" Inherits="Operations_Booking_NewGC_FrmNewGCConsigneeDDAddress" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Door Delivery Address</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../../Javascript/Common.js"></script>
    <script type="text/javascript" src="../../../Javascript/Operations/Booking/GCNew.js"></script>
 <script type="text/javascript">
 
 function UpdateConsigneeDDAddress(Add1,Add2)
 { 
    window.opener.call_ConsigneeDDAddress(Add1,Add2); 
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
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager runat="server" ID="scm_DDAddress"></asp:ScriptManager>
    <div>
    <table class="TABLE" cellpadding="4">
    <tr>
        <td class="TDGRADIENT" colspan="2">
            &nbsp;<asp:Label ID="Label2" runat="server" CssClass="HEADINGLABEL" Text="Door Delivery Address.."></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%;">
            <asp:Label ID="lbl_Consignee_Name" CssClass="LABEL" Text="Consignee Name :" runat="server"></asp:Label>
        </td>
        <td style="width: 70%;">
            <asp:Label ID="lbl_Consignee_Name_Value" runat="server" Style="font-weight: bold;"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%;">
            <asp:Label ID="lbl_Consignee_AddressLine1" CssClass="LABEL" Text="Address Line 1 :" runat="server"></asp:Label>
        </td>
        <td style="width: 70%;">
            <asp:TextBox ID="Txt_Consignee_AddressLine1" CssClass="TEXTBOX" runat="server"
                onkeyPress=" return Check_Max_Length_For_Multiline(this,event,100);" Width="270px" Height="45px" TextMode="MultiLine"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%;">
            <asp:Label ID="lbl_Consignee_AddressLine2" CssClass="LABEL" Text="Address Line 2 :" runat="server"></asp:Label>
        </td>
        <td style="width: 70%;">
            <asp:TextBox ID="Txt_Consignee_AddressLine2" CssClass="TEXTBOX" runat="server"
                onkeyPress=" return Check_Max_Length_For_Multiline(this,event,100);" Width="270px" Height="45px" TextMode="MultiLine">
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
            </Triggers>
        </asp:UpdatePanel>               
        </td>
    </tr>              
    <tr>
        <td align="center" colspan="2">
            <asp:Button ID="Btn_Save" runat="server" CssClass="SMALLBUTTON" Text="Save" OnClick="btn_Save_Click" AccessKey="S" />
            <asp:Button ID="btn_Exit" runat="server" OnClick="btn_Exit_Click" OnClientClick="return Allow_To_Exit()" CssClass="SMALLBUTTON" Text="Exit" AccessKey="E" />
        </td>
    </tr>
    <tr>
        <td colspan="2" style="display:none"><asp:HiddenField runat="server" ID="hdn_Mode"></asp:HiddenField> </td>
    </tr>
</table>
    </div>
    </form>
</body>
</html>
<script type="text/javascript" language="javascript">    
    On_View();    
</script>