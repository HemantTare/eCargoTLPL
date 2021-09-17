<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmNewGCContainerDetails.aspx.cs" Inherits="Operations_Booking_NewGC_FrmNewGCContainerDetails" %>
<%@ Register Assembly="DDLSearch" Namespace="ClassLibrary.UIControl" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Container Details</title>
    <link href="../../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../../../Javascript/Common.js"></script>
    <script type="text/javascript" src="../../../Javascript/ddlsearch.js"></script>
 <script type="text/javascript">
    function updateContainerDetails(isContainerfilled)
    { 
        window.opener.call_ContainerDetails(isContainerfilled);
    } 
    
    function Allow_To_Exit()
    {
        var ATE = false;

        if (confirm("Do you want to Exit...") == false)
            ATE=false;		 
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
        var Btn_Save = document.getElementById('Btn_Save');

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
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="SM_NewGCContainerDetails" runat="server"></asp:ScriptManager>

    <div>
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
            <asp:TextBox ID="txt_ContainerNoPart1" runat="server" Width="50" CssClass="TEXTBOX" MaxLength="4"
                onkeyPress="return Only_characterswithspace(this,event);"></asp:TextBox>
            -
            <asp:TextBox ID="txt_ContainerNoPart2" runat="server" Width="60" CssClass="TEXTBOX" onkeyPress="return Only_Numbers(this,event);" MaxLength="7"></asp:TextBox>
        </td>
        <td style="width: 1%;">&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_SealNo" CssClass="LABEL" Text="Seal No :" runat="server"></asp:Label>
        </td>
        <td style="width: 50%;">
            <asp:TextBox ID="txt_SealNo" runat="server" Width="50" CssClass="TEXTBOX" MaxLength="6"></asp:TextBox>
        </td>
        <td style="width: 1%;">&nbsp;</td>
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_ReturnToYard" CssClass="LABEL" Text="Return To Yard :" runat="server"></asp:Label>
        </td>
        <td style="width: 50%;">
            <cc1:DDLSearch ID="ddl_ReturnToYard"  runat="server"                    
            IsCallBack="True" CallBackFunction="Raj.EF.CallBackFunction.CallBack.GetSearchLocation"
            AllowNewText="True" CallBackAfter="2"></cc1:DDLSearch>                        
        </td>
        <td style="width: 1%;"> &nbsp;</td>        
    </tr>
    <tr>
        <td class="TD1" style="width: 30%">
            <asp:Label ID="lbl_NFormNo" CssClass="LABEL" Text="N Form No :" runat="server"></asp:Label>
        </td>
        <td style="width: 50%;">
            <asp:TextBox ID="txt_NFormNo" runat="server" Width="60%" CssClass="TEXTBOX" MaxLength="20"></asp:TextBox>
        </td>
        <td style="width: 1%;">&nbsp;</td>        
    </tr>   
    <tr>
        <td colspan="4">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <contenttemplate>
                    <asp:Label ID="lbl_Errors" runat="server" CssClass="LABELERROR" Visible="true"></asp:Label>
                 </contenttemplate>
                <triggers>
                    <asp:AsyncPostBackTrigger ControlID="btn_Save"></asp:AsyncPostBackTrigger>
                </triggers>
            </asp:UpdatePanel>
        </td>
    </tr>
    <tr align="center">
        <td colspan="4">
            <asp:Button ID="Btn_Save" runat="server" CssClass="SMALLBUTTON" Text="Save" OnClick="btn_Save_Click" AccessKey="S" />
            &nbsp;<asp:Button ID="btn_Exit" runat="server" OnClick="btn_Exit_Click" OnClientClick="return Allow_To_Exit()" CssClass="SMALLBUTTON" Text="Exit" AccessKey="E"/>
        </td>
        
    </tr>
    <tr>
      <td colspan="4">
            <asp:HiddenField runat="server" ID="hdn_Mode"></asp:HiddenField>
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