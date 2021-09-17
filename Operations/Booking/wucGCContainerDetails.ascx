<%@ Control Language="C#" AutoEventWireup="true" CodeFile="wucGCContainerDetails.ascx.cs" Inherits="Operations_Booking_wucGCContainerDetails" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>
<link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="../../Javascript/Common.js"></script>
<script type="text/javascript" src="../../Javascript/ddlsearch.js"></script>
<script language="javascript" type="text/javascript" src="../../Javascript/Operations/Booking/GC.js"></script>

<asp:ScriptManager ID="SM_GCContainerDetails" runat="server"></asp:ScriptManager>

<script type="text/javascript">
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
        var hdn_Mode = document.getElementById('wucGCContainerDetails1_hdn_Mode'); 
        var btn_Exit = document.getElementById('wucGCContainerDetails1_btn_Exit');
        var Btn_Save = document.getElementById('wucGCContainerDetails1_Btn_Save');

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
              Btn_Save.style.display = 'none';
            }
            btn_Exit.disabled = false;
        }  
    }
</script>

<table class="TABLE" border="0" cellpadding="4">
    <tr>
        <td class="TDGRADIENT" colspan="4">
            &nbsp;<asp:Label ID="lbl_ContainerDetails" runat="server" CssClass="HEADINGLABEL" Text="Container Details"></asp:Label></td>
    </tr>    
    <tr>
        <td class="TD1" style="width: 30%;">
            <asp:Label ID="lbl_ContainerType" CssClass="LABEL" Text="Container Type :" runat="server"></asp:Label>
        </td>
        <td style="width: 50%;">
            <asp:DropDownList ID="ddl_ContainerType" runat="server" CssClass="DROPDOWN" Width="170"></asp:DropDownList>
        </td>
        <td style="width: 1%;">&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_ContainerNo" CssClass="LABEL" Text="Container No :" runat="server"></asp:Label>
        </td>
        <td style="width: 50%;">
            <asp:TextBox ID="txt_ContainerNoPart1" onblur="txtbox_onlostfocus(this)" onfocus="txtbox_onfocus(this)"
                runat="server" Width="50" BorderWidth="1px" CssClass="TEXTBOX" MaxLength="4"
                onkeyPress="return Only_characterswithspace(this,event);"></asp:TextBox>
            -
            <asp:TextBox ID="txt_ContainerNoPart2" onblur="txtbox_onlostfocus(this)" onfocus="txtbox_onfocus(this)"
                runat="server" Width="60" BorderWidth="1px" CssClass="TEXTBOXNOS" onkeyPress="return Only_Numbers(this,event);"
                MaxLength="7" style="text-align:left " ></asp:TextBox>
        </td>
        <td style="width: 1%;">&nbsp;</td>        
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_SealNo" CssClass="LABEL" Text="Seal No :" runat="server"></asp:Label>
        </td>
        <td style="width: 50%;">
            <asp:TextBox ID="txt_SealNo" onblur="txtbox_onlostfocus(this)" onfocus="txtbox_onfocus(this)"
                runat="server" Width="50" BorderWidth="1px" CssClass="TEXTBOXNOS"  style="text-align:left " 
                MaxLength="6"></asp:TextBox>
        </td>
        <td style="width: 1%;">&nbsp;</td>        
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_ReturnToYard" CssClass="LABEL" Text="Return To Yard :" runat="server"></asp:Label>
        </td>
        <td style="width: 50%;">
            <asp:UpdatePanel ID="upd_ddl_ReturnToYard" runat="server">
                <contenttemplate>
                    <cc1:DDLSearch ID="ddl_ReturnToYard"  runat="server" Text="" Width="95%" 
                    OnTxtChange="ddl_ReturnToYard_TxtChange" PostBack="True"
                    IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchLocation"
                    AllowNewText="True" CallBackAfter="2"></cc1:DDLSearch>                        
                    <asp:HiddenField ID="hdn_ReturnToYardId" runat="server"></asp:HiddenField>                    
                </contenttemplate>
                <triggers>                    
                    <asp:AsyncPostBackTrigger ControlID="ddl_ReturnToYard"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
        <td style="width: 1%;"> &nbsp;</td>        
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_NFormNo" CssClass="LABEL" Text="N Form No :" runat="server"></asp:Label>
        </td>
        <td style="width: 50%;">
            <asp:TextBox ID="txt_NFormNo" onblur="txtbox_onlostfocus(this)" onfocus="txtbox_onfocus(this)"
                runat="server" Width="60%" BorderWidth="1px" CssClass="TEXTBOX" MaxLength="20"></asp:TextBox>
        </td>
        <td style="width: 1%;">&nbsp;</td>        
    </tr>   
    <tr>
        <td colspan="4">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <contenttemplate>
                    <asp:Label ID="lbl_Errors" runat="server" Font-Bold="true" CssClass="LABEL" 
                        ForeColor="Red" Visible="true"></asp:Label>
                 </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr align="center">
        <td colspan="4">
            <asp:Button ID="Btn_Save" runat="server" CssClass="SMALLBUTTON" Text="Save" OnClick="btn_Save_Click"
                ValidationGroup="k" AccessKey="S" />
            &nbsp;<asp:Button ID="btn_Exit" runat="server" CssClass="SMALLBUTTON" Text="Exit" AccessKey="E" />
        </td>
        
    </tr>
    <tr>
      <td colspan="4">
            <input id="hdn_Consignee_Addess" runat="server" type="hidden" /> 
            <input id="hdn_Consignee_TelNo" runat="server" type="hidden" />
            <input id="hdn_Co_Name" runat="server" type="hidden" />
            <asp:HiddenField runat="server" ID="hdn_Mode"></asp:HiddenField>
        </td>
    </tr>
</table>

<script type="text/javascript" language="javascript">    
    On_View();    
</script>

