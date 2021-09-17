<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FrmDriverCleanerUpdateHistory.aspx.cs"
    Inherits="Master_Driver_FrmDriverCleanerUpdateHistory" %>

<%@ Register Src="../../CommonControls/WucDatePicker.ascx" TagName="WucDatePicker"
    TagPrefix="uc1" %>
<%@ Register Src="~/CommonControls/Wuc_Export_To_Excel.ascx" TagName="Wuc_Export_To_Excel"
    TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script language="javascript" type="text/javascript" src="../../JavaScript/Common.js"></script>

<script type="text/javascript" src="../../Javascript/txtsearch_common.js"></script>

<script type="text/javascript">

function DriverClicked()
{

    var hdn_IsCleaner = document.getElementById("hdn_IsCleaner");
    var hdn_DriverId = document.getElementById("hdn_DriverId");
    var hdn_VehicleId = document.getElementById("hdn_VehicleId");
    
    var txtDriver = document.getElementById("txtDriver");
    var txtVehicle = document.getElementById("txtVehicle");
    
    hdn_DriverId.value = '0';
    hdn_VehicleId.value = '0';
    
    txtDriver.value = '';
    txtVehicle.value = '';
    
   
    hdn_IsCleaner.value='0';
    
    
}

function CleanerClicked()
{

var hdn_IsCleaner = document.getElementById("hdn_IsCleaner");
    var hdn_DriverId = document.getElementById("hdn_DriverId");
    var hdn_VehicleId = document.getElementById("hdn_VehicleId");
    
    var txtDriver = document.getElementById("txtDriver");
    var txtVehicle = document.getElementById("txtVehicle");
    
    hdn_DriverId.value = '0';
    hdn_VehicleId.value = '0';
    
    txtDriver.value = '';
    txtVehicle.value = '';
            
    hdn_IsCleaner.value='1';
    
    
}

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
    


        var hdn_DriverId = document.getElementById("hdn_DriverId");
        var hdn_VehicleId = document.getElementById("hdn_VehicleId");



        if (Search_Type == 'Driver' || Search_Type == 'Cleaner')
        {
            hdn_DriverId.value = list_control_value;
            
        }
        else if (Search_Type == 'Vehicle')
        {
            hdn_VehicleId.value = list_control_value;
        
        }
        
    }

    
}


function viewwindow_Driver(DriverId,IsCleaner)
{
        if(IsCleaner == '0')
        {
            var Path='../../Master/Driver/FrmDriver.aspx?Menu_Item_Id=NgAzAA==&Mode=NAA=&Id=' + DriverId;
        }
        else
        {
            var Path='../../Master/Driver/FrmDriver.aspx?Menu_Item_Id=NgA0AA==&Mode=NAA=&Id=' + DriverId;
        }
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'DriverCleanerHistoryDriver', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

function viewwindow_Vehicle(VehicleId)
{
        
        var Path='../../Master/Vehicle/FrmVehicle.aspx?Menu_Item_Id=OAA1AA==&Mode=NAA=&Id=' + VehicleId;
        
        var w = screen.availWidth;
        var h = screen.availHeight;
        var popW = w;
        var popH = h;
        var leftPos = (w-popW)/2;
        var topPos = (h-popH)/2;
 
        window.open(Path, 'DriverCleanerHistoryVehicle', 'width='+ popW +', height='+ popH +',top='+ topPos +',left='+ leftPos +', menubar=no, resizable=no,scrollbars=yes')
        return false;
}

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Driver / Cleaner Update History</title>
    <link href="../../CommonStyleSheet.css" rel="stylesheet" type="text/css" />
</head>
<body leftmargin="0" topmargin="0" rightmargin="0" bottommargin="0">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager" runat="server" />
        <div>
            <table class="TABLE" width="100%">
                <tr>
                    <td class="TDGRADIENT" colspan="6">
                        <asp:Label ID="lbl_Heading" CssClass="HEADINGLABEL" runat="server" Text="Driver / Cleaner Update History"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;" colspan="6" align="center">
                        <asp:RadioButtonList ID="rbl_DriverCleaner" runat="server" AutoPostBack="true" ForeColor="#660099"
                            Font-Size="Medium" Font-Bold="true" RepeatDirection="Horizontal">
                            <asp:ListItem OnClick="DriverClicked();" Selected="True" Text="Driver" Value="0"></asp:ListItem>
                            <asp:ListItem OnClick="CleanerClicked();" Text="Cleaner" Value="1"></asp:ListItem>
                        </asp:RadioButtonList>
                        <asp:HiddenField ID="hdn_IsCleaner" runat="server" Value="0" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        <asp:Label ID="lbl_DriverCleaner" runat="server" Text="Name :"></asp:Label>
                    </td>
                    <td style="width: 29%">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtDriver" autocomplete="off" runat="server" CssClass="TEXTBOX"
                                    onblur="On_txtLostFocus('txtDriver','lstDriver','hdn_DriverId'); txtbox_onlostfocus(this);"
                                    onkeyup="Search_txtSearch(event,this,'lstDriver','Driver',2);" onkeydown="return on_keydown(event,'txtDriver','lstDriver');"
                                    onfocus="On_Focus('txtDriver','lstDriver'); txtbox_onfocus(this);" MaxLength="150"
                                    EnableViewState="False" Width="80%"></asp:TextBox>
                                <asp:ListBox ID="lstDriver" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txtDriver')"
                                    TabIndex="5" runat="server"></asp:ListBox>
                                <asp:HiddenField ID="hdn_DriverId" Value="0" runat="server" />
                                &nbsp; &nbsp;
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtDriver" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td style="width: 20%" class="TD1">
                        Vehicle No :</td>
                    <td style="width: 29%" align="left">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtVehicle" autocomplete="off" runat="server" CssClass="TEXTBOX"
                                    onblur="On_txtLostFocus('txtVehicle','lstVehicle','hdn_VehicleIncoming'); txtbox_onlostfocus(this);"
                                    onkeyup="Search_txtSearch(event,this,'lstVehicle','Vehicle',3);" onkeydown="return on_keydown(event,'txtVehicle','lstVehicle');"
                                    onfocus="On_Focus('txtVehicle','lstVehicle'); txtbox_onfocus(this);" MaxLength="150"
                                    EnableViewState="False" Width="40%"></asp:TextBox>
                                <asp:ListBox ID="lstVehicle" Style="position: absolute; z-index: 1000" onfocus="listboxonfocus('txtVehicle')"
                                    TabIndex="5" runat="server"></asp:ListBox>
                                <asp:HiddenField ID="hdn_VehicleId" Value="0" runat="server" />
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txtVehicle" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;" class="TD1" colspan="6">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="width: 20%" class="TD1">
                        From Date :</td>
                    <td style="width: 29%">
                        <uc1:WucDatePicker ID="dtp_FromDate" runat="server" />
                    </td>
                    <td class="TDMANDATORY" style="width: 1%">
                    </td>
                    <td style="width: 20%" class="TD1">
                        To Date :</td>
                    <td style="width: 29%" align="left" colspan="2">
                        <uc1:WucDatePicker ID="dtp_ToDate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td style="width: 51%" colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnView" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Label ID="lblErrors" runat="server" CssClass="LABELERROR" EnableViewState="False"
                                    Text="Fields with * mark are mandatory"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width: 20%">
                        <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnview_Click" /></td>
                    <td style="width: 29%">
                        <uc1:Wuc_Export_To_Excel ID="Wuc_Export_To_Excel1" runat="server"></uc1:Wuc_Export_To_Excel>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="left">
                        <asp:UpdatePanel ID="Upd_Pnl1" UpdateMode="Conditional" runat="server">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btnView" />
                                <asp:AsyncPostBackTrigger ControlID="dg_Grid" />
                            </Triggers>
                            <ContentTemplate>
                                <asp:Panel ID="pnl_Pnl1" runat="server" Height="500px" ScrollBars="Auto">
                                    <asp:DataGrid ID="dg_Grid" runat="server" ShowFooter="True" AllowPaging="True" CssClass="GRID"
                                        AllowSorting="True" AllowCustomPaging="True" AutoGenerateColumns="False" OnPageIndexChanged="dg_Grid_PageIndexChanged"
                                        OnItemDataBound="dg_Grid_ItemDataBound" PageSize="20">
                                        <AlternatingItemStyle CssClass="GRIDALTERNATEROWCSS" />
                                        <HeaderStyle CssClass="GRIDHEADERCSS" />
                                        <FooterStyle CssClass="GRIDFOOTERCSS" />
                                        <PagerStyle CssClass="GRIDPAGERCSS" Mode="NumericPages" />
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="Name" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtn_Name" Text='<%# DataBinder.Eval(Container, "DataItem.Driver_Name") %>'
                                                        OnClick="lbtn_Name_Click" Font-Bold="True" Font-Underline="True" runat="server" CommandName="Description"
                                                        CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Driver_Name") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Vehicle" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtn_Vehicle" Text='<%# DataBinder.Eval(Container, "DataItem.Vehicle_No") %>'
                                                        OnClick="lbtn_Vehicle_Click" Font-Bold="True" Font-Underline="True" runat="server"
                                                        CommandName="Description" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.Vehicle_No") %>' />
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="JoiningDate" HeaderText="Joined On"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="LeavingDate" HeaderText="Left On"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="Remark" HeaderText="Remark"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="IsLeftPermanent" HeaderText="LeftPermanent?"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="0%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbl_VehicleID" Text='<%# DataBinder.Eval(Container, "DataItem.Vehicle_ID") %>'
                                                        CssClass="HIDEGRIDCOL"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="0%">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lbl_DriverID" Text='<%# DataBinder.Eval(Container, "DataItem.Driver_ID") %>'
                                                        CssClass="HIDEGRIDCOL"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
