<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDriverCleanerUpdate_Outgoing.aspx.cs"
    Inherits="Master_Driver_FrmDriverCleanerUpdate_Outgoing" %>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Src="../../CommonControls/WucVehicleSearch.ascx" TagName="WucVehicleSearch"
    TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/txtsearch_common.js"></script>

<script type="text/javascript">

var Search_Type;
var lst_control_id;
function Search_txtSearch(e,txtbox,lstBox,SearchType,length)
{    
    Search_Type = SearchType;
    lst_control_id = lstBox;
    if (txtbox.value == '')
    {
        Clear_listbox(lstBox);
        hidecontrol(document.getElementById(lstBox));
        return;
    }
    var hdn_DriverId = document.getElementById("hdn_DriverId");

    var hdn_IsCleaner = document.getElementById("hdn_IsCleaner");

        
    var txtvalue = txtbox.value.toUpperCase();
    if(txtvalue.length >= length)
    {
        if (keyCode != 37 && keyCode != 38 && keyCode != 39 && keyCode != 40)
        {
            if(oldvalue != txtvalue)
            {

                if (Search_Type == 'VehicleOld' || Search_Type == 'VehicleNew')
                {
                    Raj.EF.CallBackFunction.CallBack.GetTxtSearchVehicle(txtvalue,handleResults);
                }
            }
        }
    }
    if (keyCode == 38 || keyCode == 40)
        listboxupdown(txtbox,lstBox);
}

function handleResults(results)
{  
  var list_control = document.getElementById(lst_control_id);
  
  var tot = results.value.Rows.length -1;
  var count = 0;
  
  for (var count = list_control.options.length-1; count >-1; count--)
  {
    list_control.options[count] = null;
  }

  for (count = 0;count <= tot;count ++)
  { 
    list_control.options[count] = new Option(results.value.Rows[count][results.value.Columns[0].Name],results.value.Rows[count][results.value.Columns[1].Name]); 
  }
  
    if (list_control.options.length == 0)
      hidecontrol(list_control);
    else
      showcontrol(list_control);
}

function On_txtLostFocus(txtbox,list_control,hdn_control)
{
    var txtbox_value = document.getElementById(txtbox).value.toUpperCase();
    var listcontrol = document.getElementById(list_control); 
    var list_control_index = listcontrol.selectedIndex;
    var list_control_value;
    var list_control_text;
    
    hidecontrol(listcontrol);
    if (oldvalue != txtbox_value)
    {
    
        if (list_control_index != -1){
            list_control_value = listcontrol.options[list_control_index].value;
            list_control_text = listcontrol.options[list_control_index].text;
        }
        else{
            list_control_value = '0';
            list_control_text = '';
        }
    
        document.getElementById(hdn_control).value = list_control_value;
        document.getElementById(txtbox).value = list_control_text;

        var hdn_IsCleaner = document.getElementById("hdn_IsCleaner");

        
        if (Search_Type == 'VehicleOld')
        {
            Raj.EF.CallBackFunction.CallBack.GetTxtDriverCleanerInfoFromVehicle(list_control_value,val(hdn_IsCleaner.value),handleDriverCleanerInfo);
        }
    }

}

function handleDriverCleanerInfo(results)
{  

    var driverid = results.value.Rows[0]["Driver_Id"];
    var nickname = results.value.Rows[0]["Nick_Name"];
    var name = results.value.Rows[0]["Driver_Name"];

    var hdn_DriverId = document.getElementById("hdn_DriverId");

    hdn_DriverId.value = driverid;
    document.getElementById("lbl_NickName").innerHTML = nickname;
    document.getElementById("lbl_Name").innerHTML = name;
    
    
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Driver Cleaner Update - Outgoing</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Driver Cleaner Update - Outgoing"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="TD1" style="width: 25%; height: 15px;">
                    </td>
                    <td style="width: 24%; height: 15px;">
                        &nbsp;</td>
                    <td class="TDMANDATORY" style="width: 1%; height: 15px;">
                    </td>
                    <td style="width: 50%; height: 15px;" align="left" colspan="3">
                        &nbsp</td>
                </tr>
                <tr>
                    <td class="TD1">
                        Vehicle No:</td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtVehicleOld" autocomplete="off" runat="server" CssClass="TEXTBOX"
                                    onblur="On_txtLostFocus('txtVehicleOld','lstVehicleOld','hdn_VehicleIdOld'); txtbox_onlostfocus(this);"
                                    onkeyup="Search_txtSearch(event,this,'lstVehicleOld','VehicleOld',3);" onkeydown="return on_keydown(event,'txtVehicleOld','lstVehicleOld');"
                                    onfocus="On_Focus('txtVehicleOld','lstVehicleOld'); txtbox_onfocus(this);" MaxLength="150"
                                    EnableViewState="False" Width="50%"></asp:TextBox>
                                <asp:ListBox ID="lstVehicleOld" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txtVehicleOld')"
                                    TabIndex="5" runat="server"></asp:ListBox>
                                <asp:HiddenField ID="hdn_VehicleIdOld" Value="0" runat="server" />
                                <asp:HiddenField ID="hdn_IsCleaner" Value="0" runat="server" />
                                <asp:HiddenField ID="hdn_DriverId" Value="0" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtVehicleOld" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 50%" align="left" colspan="3">
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                    </td>
                    <td colspan="5" style="width: 200px">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_NickNameH" runat="server" Text="Nick Name :"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:Label ID="lbl_NickName" runat="server" Text="" Font-Bold="true" ForeColor="#660099"></asp:Label>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td style="width: 50%" align="left" colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_DriverCleaner" runat="server" Text="Name :"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:Label ID="lbl_Name" runat="server" Text="" Font-Bold="true" ForeColor="#660099"></asp:Label>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td style="width: 50%" align="left" colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_OnLeaveOrLeft" runat="server" Text="On Leave Or Permanent Left :"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:RadioButtonList ID="rbl_OnLeaveOrLeft" runat="server" ForeColor="#660099" Font-Size="Medium"
                            Font-Bold="true" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Text="On Leave" Value="0"></asp:ListItem>
                            <asp:ListItem Text="Left Permanent" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td style="width: 50%" align="left" colspan="3">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        On Leave From :&nbsp;</td>
                    <td style="width: 29%">
                        <uc1:WucDatePicker ID="dtp_LeavingDate" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 20%" class="TD1">
                        &nbsp;</td>
                    <td style="width: 30%" align="left" colspan="2">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 15px;" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        Reason :</td>
                    <td style="width: 80%" colspan="5">
                        <asp:TextBox ID="txtReason" CssClass="TEXTBOX" TextMode="MultiLine" Height="30px"
                            MaxLength="100" runat="server" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" colspan="6" align="center">
                        <asp:Button ID="btnSave" runat="server" OnClick="btn_Save_Click" Text="Save & Exit" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="left">
                        <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
