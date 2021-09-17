<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDriverCleanerUpdateNew.aspx.cs"
    Inherits="Master_Driver_FrmDriverCleanerUpdateNew" %>

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
    

    if (Search_Type == 'Vehicle')
    {
        Search_Type = Search_Type;
    }
    else
    {

        if (hdn_IsCleaner.value == '1')
        {
            Search_Type = 'Cleaner';
        }
        else
        {
            Search_Type = 'Driver';    
        }
    
    }
    
    var txtvalue = txtbox.value.toUpperCase();
    if(txtvalue.length >= length)
    {
        if (keyCode != 37 && keyCode != 38 && keyCode != 39 && keyCode != 40)
        {
            if(oldvalue != txtvalue)
            {
                    if (Search_Type == 'Cleaner' || Search_Type == 'Driver')
                    {
                        Raj.EF.CallBackFunction.CallBack.GetTxtSearchDriverCleaner(val(hdn_IsCleaner.value),txtvalue,handleResults);
                    }
                    else
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
        
        if (Search_Type == 'Driver' || Search_Type == 'Cleaner')
        {
                document.getElementById('<%=btn_hidden.ClientID%>').style.display = "none";
                document.getElementById('<%=btn_hidden.ClientID%>').style.visibility = "hidden";
                document.getElementById('<%=btn_hidden.ClientID%>').click();

        }
        else if (Search_Type == 'Vehicle')
        {
                Raj.EF.CallBackFunction.CallBack.GetTxtDriverCleanerInfoFromVehicle(list_control_value,val(hdn_IsCleaner.value),handleDriverCleanerInfo);

        }
    }

}

function handleDriverCleanerInfo(results)
{  

    var driverid = results.value.Rows[0]["Driver_Id"];
    var drivername = results.value.Rows[0]["Driver_Name"];
    
    hdn_PresentDriverCleanerId = document.getElementById("hdn_PresentDriverCleanerId");
    hdn_PresentDriverCleanerName = document.getElementById("hdn_PresentDriverCleanerName");
   
    hdn_PresentDriverCleanerId.value = driverid;
    hdn_PresentDriverCleanerName.value = drivername;
    
        
}

function IncomingClicked()
{

    var tr_Incoming = document.getElementById("tr_Incoming");
    var tr_VehicleChange = document.getElementById("tr_VehicleChange");
    var tr_Outgoing = document.getElementById("tr_Outgoing");
    
    var hdn_ChangeType = document.getElementById("hdn_ChangeType");
    
    hdn_ChangeType.value = '1';
     
    if (tr_Incoming != null) 
    {
        tr_Incoming.style.display = 'block';
    }

    if (tr_VehicleChange != null) 
    {
        tr_VehicleChange.style.display = 'none';
    }

    if (tr_Outgoing != null) 
    {
        tr_Outgoing.style.display = 'none';
    }
    
}

function VehicleChangeClicked()
{

    var tr_Incoming = document.getElementById("tr_Incoming");
    var tr_VehicleChange = document.getElementById("tr_VehicleChange");
    var tr_Outgoing = document.getElementById("tr_Outgoing");
    
    var hdn_ChangeType = document.getElementById("hdn_ChangeType");
        

    hdn_ChangeType.value = '2';
    
    if (tr_VehicleChange != null) 
    {
        tr_VehicleChange.style.display = 'block';
    }

    if (tr_Incoming != null) 
    {
        tr_Incoming.style.display = 'none';
    }

    if (tr_Outgoing != null) 
    {
        tr_Outgoing.style.display = 'none';
    }
    
}


function OutgoingClicked()
{

    var tr_Incoming = document.getElementById("tr_Incoming");
    var tr_VehicleChange = document.getElementById("tr_VehicleChange");
    var tr_Outgoing = document.getElementById("tr_Outgoing");
    
    var hdn_ChangeType = document.getElementById("hdn_ChangeType");
    
    hdn_ChangeType.value = '3';


    if (tr_Outgoing != null) 
    {
        tr_Outgoing.style.display = 'block';
    }

    if (tr_Incoming != null) 
    {
        tr_Incoming.style.display = 'none';
    }

    if (tr_VehicleChange != null) 
    {
        tr_VehicleChange.style.display = 'none';
    }    
}


function Add_New_Driver_Window()
{
    var hdn_DriverMaster_Path = document.getElementById('hdn_DriverMaster_Path');


    if(hdn_DriverMaster_Path.value == '')
    {
        alert('Please Select Driver / Cleaner OR You Don"t Have Rights to Add Driver / Cleaner');
    }
    else
    {
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = 900;
        var popH = 450;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
                    
        window.open(hdn_DriverMaster_Path.value, 'DriverCleaner', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')

    }

    return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Driver / Cleaner Update</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Driver / Cleaner Update"></asp:Label>
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
                        <asp:Label ID="lbl_DriverCleaner" runat="server" Text="Driver :"></asp:Label>
                        <asp:HiddenField ID="hdn_IsCleaner" runat="server" Value="0" />
                    </td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtDriver" autocomplete="off" runat="server" CssClass="TEXTBOX"
                                    onblur="On_txtLostFocus('txtDriver','lstDriver','hdn_DriverId'); txtbox_onlostfocus(this);"
                                    onkeyup="Search_txtSearch(event,this,'lstDriver','Driver',2);" onkeydown="return on_keydown(event,'txtDriver','lstDriver');"
                                    onfocus="On_Focus('txtDriver','lstDriver'); txtbox_onfocus(this);" MaxLength="150"
                                    EnableViewState="False" Width="50%"></asp:TextBox>
                                <asp:ListBox ID="lstDriver" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txtDriver')"
                                    TabIndex="5" runat="server"></asp:ListBox>
                                <asp:HiddenField ID="hdn_DriverId" Value="0" runat="server" />
                                <asp:HiddenField ID="hdn_DriverMaster_Path" Value="" runat="server" />
                                <asp:HiddenField ID="hdn_EncreptedDriverId" Value="" runat="server" />
                                
                                <asp:HiddenField ID="hdn_PresentDriverCleanerId" Value="0" runat="server" />
                                <asp:HiddenField ID="hdn_PresentDriverCleanerName" Value="" runat="server" />
                                                                
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtDriver" />
                            </Triggers>
                        </asp:UpdatePanel>
                        &nbsp;<asp:LinkButton ID="lbtn_EditDriver" runat="server" Text="Edit" OnClientClick="return Add_New_Driver_Window();"></asp:LinkButton>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                        *</td>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_NickNameH" runat="server" Text="Nick Name :"></asp:Label>
                    </td>
                    <td style="width: 29%" align="left">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_NickName" runat="server" Text="" Font-Bold="true" ForeColor="#660099"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtDriver" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_LicenseNoH" runat="server" Text="License No. :"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_LicenseNo" runat="server" Text="" Font-Bold="true" ForeColor="#660099"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtDriver" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_LicenseExpiryH" runat="server" Text="Expiry Date :"></asp:Label>
                    </td>
                    <td style="width: 29%" align="left" colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_LicenseExpiry" runat="server" Text="" Font-Bold="true" ForeColor="#660099"></asp:Label>
                                <asp:HiddenField ID="hdn_LicenseExpiry" Value="01-01-2021" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtDriver" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 15px;" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_AadharNoH" runat="server" Text="Aadhar No. :"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_AadharNo" runat="server" Font-Bold="true" ForeColor="#660099"
                                    Text=""></asp:Label>
                                <asp:HiddenField ID="hdn_AadharNo" Value="" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtDriver" />
                            </Triggers>
                        </asp:UpdatePanel>
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
                        <asp:Label ID="lbl_Mobile1H" runat="server" Text="Mobile No. 1 :"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_Mobile1" runat="server" Font-Bold="true" ForeColor="#660099" Text=""></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtDriver" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_Mobile2H" runat="server" Text="Mobile No. 2 :"></asp:Label>
                    </td>
                    <td style="width: 30%" align="left" colspan="2">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_Mobile2" runat="server" Font-Bold="true" ForeColor="#660099" Text=""></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtDriver" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%; height: 15px;" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_CurrentVehicleNoH" runat="server" Text="Current Vehicle No. :"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbl_CurrentVehicleNo" runat="server" Font-Bold="true" ForeColor="Red" Font-Size="Medium"
                                    Text=""></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtDriver" />
                            </Triggers>
                        </asp:UpdatePanel>
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
                    <td style="width: 100%" align="center" colspan="6">
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <asp:RadioButtonList ID="rbl_ChangeType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem OnClick="IncomingClicked();" Text="Incoming" Value="1"></asp:ListItem>
                                    <asp:ListItem OnClick="VehicleChangeClicked();" Text="Vehicle Change" Value="2"></asp:ListItem>
                                    <asp:ListItem OnClick="OutgoingClicked();" Text="Outgoing" Value="3"></asp:ListItem>
                                </asp:RadioButtonList>
                                <asp:HiddenField ID="hdn_ChangeType" Value="0" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnSave" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr id="tr_Incoming" runat="server">
                    <td style="width: 100%" class="TD1" colspan="6">
                        <table class="TABLE" width="100%" style="background-color: Linen;">
                            <tr>
                                <td style="width: 20%">
                                    Vehicle No :
                                </td>
                                <td style="width: 80%" align="left">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtVehicleIncoming" autocomplete="off" runat="server" CssClass="TEXTBOX"
                                                onblur="On_txtLostFocus('txtVehicleIncoming','lstVehicleIncoming','hdn_VehicleIncoming'); txtbox_onlostfocus(this);"
                                                onkeyup="Search_txtSearch(event,this,'lstVehicleIncoming','Vehicle',3);" onkeydown="return on_keydown(event,'txtVehicleIncoming','lstVehicleIncoming');"
                                                onfocus="On_Focus('txtVehicleIncoming','lstVehicleIncoming'); txtbox_onfocus(this);"
                                                MaxLength="150" EnableViewState="False" Width="20%"></asp:TextBox>
                                            <asp:ListBox ID="lstVehicleIncoming" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txtVehicleIncoming')"
                                                TabIndex="5" runat="server"></asp:ListBox>
                                            <asp:HiddenField ID="hdn_VehicleIncoming" Value="0" runat="server" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtVehicleIncoming" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%" colspan="2">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%;" class="TD1">
                                    Joining Date :
                                </td>
                                <td style="width: 80%;" align="left">
                                    <uc1:WucDatePicker ID="dtp_JoiningDate" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="tr_VehicleChange" runat="server">
                    <td style="width: 100%" class="TD1" colspan="6">
                        <table class="TABLE" width="100%" style="background-color: LightCyan">
                            <tr>
                                <td style="width: 20%" class="TD1">
                                    Old Vehicle No :
                                </td>
                                <td style="width: 80%" align="left">
                                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtVehicleOld" runat="server" autocomplete="off" CssClass="TEXTBOX"
                                                EnableViewState="False" MaxLength="150" onblur="On_txtLostFocus('txtVehicleOld','lstVehicleOld','hdn_VehicleIdOld'); txtbox_onlostfocus(this);"
                                                onfocus="On_Focus('txtVehicleOld','lstVehicleOld'); txtbox_onfocus(this);" onkeydown="return on_keydown(event,'txtVehicleOld','lstVehicleOld');"
                                                onkeyup="Search_txtSearch(event,this,'lstVehicleOld','Vehicle',3);" Width="20%" Enabled="false"></asp:TextBox>
                                            <asp:ListBox ID="lstVehicleOld" runat="server" onfocus="listboxonfocus('txtVehicleOld')"
                                                Style="z-index: 1000; position: absolute" TabIndex="5"></asp:ListBox>
                                            <asp:HiddenField ID="hdn_VehicleIdOld" runat="server" Value="0" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtVehicleOld" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" class="TD1">
                                    Leaving Date :
                                </td>
                                <td style="width: 80%" align="left">
                                    <uc1:WucDatePicker ID="dtp_LeavingDate" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" class="TD1">
                                    New Vehicle No :
                                </td>
                                <td style="width: 80%" align="left">
                                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtVehicleNew" runat="server" autocomplete="off" CssClass="TEXTBOX"
                                                EnableViewState="False" MaxLength="150" onblur="On_txtLostFocus('txtVehicleNew','lstVehicleNew','hdn_VehicleIdNew'); txtbox_onlostfocus(this);"
                                                onfocus="On_Focus('txtVehicleNew','lstVehicleNew'); txtbox_onfocus(this);" onkeydown="return on_keydown(event,'txtVehicleNew','lstVehicleNew');"
                                                onkeyup="Search_txtSearch(event,this,'lstVehicleNew','Vehicle',3);" Width="20%"></asp:TextBox>
                                            <asp:ListBox ID="lstVehicleNew" runat="server" onfocus="listboxonfocus('txtVehicleNew')"
                                                Style="z-index: 1000; position: absolute" TabIndex="5"></asp:ListBox>
                                            <asp:HiddenField ID="hdn_VehicleIdNew" runat="server" Value="0" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtVehicleNew" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" class="TD1">
                                    Effective Date :
                                </td>
                                <td style="width: 80%" align="left">
                                    <uc1:WucDatePicker ID="dtp_EffectiveDate" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr id="tr_Outgoing" runat="server">
                    <td style="width: 100%" class="TD1" colspan="6">
                        <table class="TABLE" width="100%" style="background-color: LightYellow">
                            <tr>
                                <td style="width: 20%" class="TD1">
                                    Vehicle No :
                                </td>
                                <td style="width: 79%" align="left">
                                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txtVehicleOutgoing" runat="server" autocomplete="off" CssClass="TEXTBOX"
                                                EnableViewState="False" MaxLength="150" onblur="On_txtLostFocus('txtVehicleOutgoing','lstVehicleOutgoing','hdn_VehicleIdOutgoing'); txtbox_onlostfocus(this);"
                                                onfocus="On_Focus('txtVehicleOutgoing','lstVehicleOutgoing'); txtbox_onfocus(this);"
                                                onkeydown="return on_keydown(event,'txtVehicleOutgoing','lstVehicleOutgoing');"
                                                onkeyup="Search_txtSearch(event,this,'lstVehicleOutgoing','Vehicle',3);" Width="20%" Enabled="false"></asp:TextBox>
                                            <asp:ListBox ID="lstVehicleOutgoing" runat="server" onfocus="listboxonfocus('txtVehicleOutgoing')"
                                                Style="z-index: 1000; position: absolute" TabIndex="5"></asp:ListBox>
                                            <asp:HiddenField ID="hdn_VehicleIdOutgoing" runat="server" Value="0" />
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="txtVehicleOutgoing" />
                                            <asp:AsyncPostBackTrigger ControlID="btnSave" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%" colspan="2">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 20%" class="TD1">
                                    On Leave Or Permanent :
                                </td>
                                <td style="width: 79%" align="left">
                                    <asp:RadioButtonList ID="rbl_OnLeaveOrLeft" runat="server" ForeColor="#660099" Font-Bold="true"
                                        RepeatDirection="Horizontal" Font-Size="Medium">
                                        <asp:ListItem Selected="True" Text="On Leave" Value="0"></asp:ListItem>
                                        <asp:ListItem Text="Left Permanent" Value="1"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 100%" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="width: 20%" class="TD1">
                                    On Leave From :
                                </td>
                                <td style="width: 79%" align="left">
                                    <uc1:WucDatePicker ID="dtp_OnLeaveFrom" runat="server"></uc1:WucDatePicker>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        Remarks:</td>
                    <td style="width: 80%" colspan="5">
                        <asp:TextBox ID="txtRemarks" CssClass="TEXTBOX" TextMode="MultiLine" Height="30px"
                            MaxLength="100" runat="server" onfocus="txtbox_onfocus(this)" onblur="txtbox_onlostfocus(this)" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%" align="center" colspan="6">
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
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:Button ID="btn_hidden" runat="server" Text="" OnClick="btn_hidden_Click" Style="display: none" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btn_hidden" />
                            </Triggers>
                        </asp:UpdatePanel>
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
